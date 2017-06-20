package com.neurotec.samples.server.connection;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import com.neurotec.biometrics.NSubject;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.server.settings.Settings;

public class DatabaseConnection implements TemplateLoader {

	// ==============================================
	// Private fields
	// ==============================================

	private String dsn;
	private String user;
	private String password;
	private String table;
	private String templateColumn;
	private String idColumn;

	private Connection connection;
	private Statement statResultSet;
	private ResultSet resultSet;
	private boolean isStarted;

	private Settings settings = Settings.getInstance();

	// ==============================================
	// Public constructor
	// ==============================================

	public DatabaseConnection() {
		dsn = settings.getDSN();
		user = settings.getDBUser();
		password = settings.getDBPassword();
		table = settings.getTable();
		templateColumn = settings.getTemplateColumn();
		idColumn = settings.getIdColumn();
	}

	// ==============================================
	// Protected methods
	// ==============================================

	protected final Connection getConnection() {
		return connection;
	}

	protected final void setConnection(Connection connection) {
		this.connection = connection;
	}

	protected final ResultSet getResultSet() {
		return resultSet;
	}

	protected final String getDSN() {
		return dsn;
	}

	protected final String getUser() {
		return user;
	}

	protected final String getPassword() {
		return password;
	}

	protected final String getTable() {
		return table;
	}

	protected final void connect() throws SQLException {
		try {
			Class.forName("sun.jdbc.odbc.JdbcOdbcDriver");
			setConnection(DriverManager.getConnection(getConnectionString(), user, password));
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
	}

	protected final String getConnectionString() {
		return String.format("jdbc:odbc:%s", dsn);
	}

	// ==============================================
	// Public methods
	// ==============================================

	public final String[] getTables() throws SQLException {
		ResultSet result = null;
		List<String> results = new ArrayList<String>();
		connect();
		try {
			DatabaseMetaData meta = getConnection().getMetaData();
			result = meta.getTables(null, null, null, new String[] { "TABLE" });
			while (result.next()) {
				String tableName = result.getString("TABLE_NAME");
				results.add(tableName);
			}
			return results.toArray(new String[results.size()]);
		} finally {
			if (result != null) {
				result.close();
			}
			closeConnection();
		}

	}

	public final String[] getColumns(String table) throws SQLException {
		connect();
		ResultSet result = null;
		try {
			List<String> columnNames = new ArrayList<String>();

			DatabaseMetaData meta = getConnection().getMetaData();
			result = meta.getColumns(null, null, table, null);

			while (result.next()) {
				String columnName = result.getString("COLUMN_NAME");
				columnNames.add(columnName);
			}

			return columnNames.toArray(new String[columnNames.size()]);
		} finally {
			if (result != null) {
				result.close();
			}
			closeConnection();
		}
	}

	@Override
	public final boolean equals(Object obj) {
		if (!(obj instanceof DatabaseConnection)) {
			return false;
		}
		DatabaseConnection target = (DatabaseConnection) obj;
		return idColumn == target.idColumn && password == target.password && dsn == target.dsn && table == target.table
				&& templateColumn == target.templateColumn && user == target.user;
	}

	@Override
	public final int hashCode() {
		return super.hashCode();
	}

	@Override
	public final String toString() {
		return String.format("dsn: %s; table: %s;", dsn, table);
	}

	@Override
	public final synchronized void beginLoad() throws SQLException {
		if (isStarted) {
			throw new IllegalStateException();
		}
		isStarted = true;
		connect();
		statResultSet = connection.createStatement();
		resultSet = statResultSet.executeQuery(String.format("SELECT %s, %s FROM %s", idColumn, templateColumn, table));
	}

	@Override
	public final synchronized void endLoad() throws SQLException {
		if (resultSet != null) {
			resultSet.close();
		}
		if (statResultSet != null) {
			statResultSet.close();
		}
		closeConnection();
		isStarted = false;
	}

	@Override
	public final synchronized NSubject[] loadNext(int count) throws SQLException {
		if (!isStarted) {
			throw new IllegalStateException("Template loading not started");
		}

		List<NSubject> results = new ArrayList<NSubject>();

		while (results.size() < count && resultSet.next()) {
			NSubject tmpSubject = new NSubject();

			InputStream is = resultSet.getBinaryStream(templateColumn);
			String id = resultSet.getString(idColumn);
			ByteArrayOutputStream bos = new ByteArrayOutputStream();
			int len = 0;
			try {
				while ((len = is.read()) != -1) {
					bos.write(len);
				}
				byte[] buf = bos.toByteArray();
				tmpSubject.setTemplateBuffer(new NBuffer(buf));
				tmpSubject.setId(id);
				results.add(tmpSubject);
			} catch (IOException e) {
				e.printStackTrace();
			}

		}

		return results.toArray(new NSubject[results.size()]);
	}

	@Override
	public final synchronized int getTemplateCount() throws SQLException {
		if (isStarted) {
			throw new IllegalStateException("Can not get count while loading started");
		}
		connect();
		Statement stat = null;
		ResultSet res = null;
		try {
			stat = connection.createStatement();
			res = stat.executeQuery(String.format("SELECT COUNT(*) FROM %s", table));

			while (res.next()) {
				return res.getInt(1);
			}
		} finally {
			if (res != null) {
				res.close();
			}
			if (stat != null) {
				stat.close();
			}
			closeConnection();
		}
		return -1;
	}

	public final void checkConnection() throws SQLException {
		if (isStarted) {
			return;
		}
		connect();
		closeConnection();
	}

	protected final void closeConnection() throws SQLException {
		if (connection != null) {
			connection.close();
		}
	}

	public final void dispose() throws SQLException {
		closeConnection();
	}

}
