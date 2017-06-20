package com.neurotec.samples.abis;

public final class ConnectionParams {

	// ===========================================================
	// Private fields
	// ===========================================================

	private ConnectionType type;
	private String connectionString;
	private String table;
	private int clientPort;
	private int adminPort;
	private String host;
	private boolean clearDatabase;

	// ===========================================================
	// Public methods
	// ===========================================================

	public ConnectionType getType() {
		return type;
	}

	public void setType(ConnectionType type) {
		this.type = type;
	}

	public String getConnectionString() {
		return connectionString;
	}

	public void setConnectionString(String connectionString) {
		this.connectionString = connectionString;
	}

	public String getTable() {
		return table;
	}

	public void setTable(String table) {
		this.table = table;
	}

	public int getClientPort() {
		return clientPort;
	}

	public void setClientPort(int clientPort) {
		this.clientPort = clientPort;
	}

	public int getAdminPort() {
		return adminPort;
	}

	public void setAdminPort(int adminPort) {
		this.adminPort = adminPort;
	}

	public String getHost() {
		return host;
	}

	public void setHost(String host) {
		this.host = host;
	}

	public boolean isClearDatabase() {
		return clearDatabase;
	}

	public void setClearDatabase(boolean clearDatabase) {
		this.clearDatabase = clearDatabase;
	}

}
