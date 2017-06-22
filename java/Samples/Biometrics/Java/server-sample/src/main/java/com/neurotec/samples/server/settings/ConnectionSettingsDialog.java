package com.neurotec.samples.server.settings;

import java.awt.Component;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.io.File;
import java.sql.SQLException;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JRadioButton;
import javax.swing.JSpinner;
import javax.swing.JTextField;
import javax.swing.SpinnerNumberModel;

import com.neurotec.samples.server.connection.DatabaseConnection;
import com.neurotec.samples.server.connection.ServerConnection;
import com.neurotec.samples.server.util.GridBagUtils;
import com.neurotec.samples.server.util.MessageUtils;

public final class ConnectionSettingsDialog extends JDialog implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private GridBagUtils gridBagUtils;
	private Settings settings = Settings.getInstance();

	private JTextField txtServer;
	private JSpinner spinnerClientPort;
	private JSpinner spinnerAdminPort;

	private JCheckBox chkAccelerator;
	private JTextField txtUserName;
	private JPasswordField txtPassword;

	private JButton btnCheckConnection;

	private JRadioButton radioFromDirectory;
	private JRadioButton radioFromDatabase;

	private JTextField txtDirectoryPath;
	private JButton btnBrowse;

	private JPanel databasePanel;

	private JTextField txtDSN;
	private JTextField txtDBUser;
	private JPasswordField txtDBPassword;

	private JButton btnConnect;
	private JButton btnReset;

	@SuppressWarnings("rawtypes")
	private JComboBox cmbTable;
	@SuppressWarnings("rawtypes")
	private JComboBox cmbTemplateColumn;
	@SuppressWarnings("rawtypes")
	private JComboBox cmbIdColumn;

	private JLabel lblDBConnectMsg;

	private JButton btnResetAll;
	private JButton btnOK;
	private JButton btnCancel;

	private JFileChooser folderBrowserDialog;

	// ==============================================
	// Public constructor
	// ==============================================

	public ConnectionSettingsDialog(Frame owner) {
		super(owner, "Connection settings", true);
		setPreferredSize(new Dimension(320, 690));
		setMinimumSize(new Dimension(280, 290));

		initializeComponents();
		folderBrowserDialog = new JFileChooser();
		folderBrowserDialog.setFileSelectionMode(JFileChooser.DIRECTORIES_ONLY);

		loadSettings();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		gridBagUtils = new GridBagUtils(GridBagConstraints.BOTH);
		Container contentPane = getContentPane();
		contentPane.setLayout(new BoxLayout(contentPane, BoxLayout.Y_AXIS));
		contentPane.add(initializeServerConnectionPanel());
		contentPane.add(initializeTemplatesPanel());
		contentPane.add(initializeButtonPanel());
		pack();
	}

	private JPanel initializeServerConnectionPanel() {
		JPanel serverConnectionPanel = new JPanel();
		serverConnectionPanel.setBorder(BorderFactory.createTitledBorder("Server connection"));

		GridBagLayout serverConnectionLayout = new GridBagLayout();
		serverConnectionLayout.columnWidths = new int[] { 85, 70, 10, 100 };
		serverConnectionPanel.setLayout(serverConnectionLayout);

		txtServer = new JTextField();
		spinnerClientPort = new JSpinner(new SpinnerNumberModel(0, 0, 99999, 1));
		JSpinner.NumberEditor editorClientPort = new JSpinner.NumberEditor(spinnerClientPort, "#");
		spinnerClientPort.setEditor(editorClientPort);
		spinnerAdminPort = new JSpinner(new SpinnerNumberModel(0, 0, 99999, 1));
		JSpinner.NumberEditor editorAdminPort = new JSpinner.NumberEditor(spinnerAdminPort, "#");
		spinnerAdminPort.setEditor(editorAdminPort);

		btnCheckConnection = new JButton("Check connection");
		btnCheckConnection.addActionListener(this);

		JPanel acceleratorPanel = new JPanel();
		acceleratorPanel.setBorder(BorderFactory.createTitledBorder(""));

		GridBagLayout acceleratorLayout = new GridBagLayout();
		acceleratorLayout.columnWidths = new int[] { 25, 60, 170 };
		acceleratorPanel.setLayout(acceleratorLayout);

		chkAccelerator = new JCheckBox("Accelerator");
		chkAccelerator.addActionListener(this);

		txtUserName = new JTextField("Admin");
		txtUserName.setEnabled(false);

		txtPassword = new JPasswordField("Admin");
		txtPassword.setEnabled(false);

		gridBagUtils.setInsets(new Insets(3, 3, 3, 3));

		gridBagUtils.addToGridBagLayout(0, 0, 3, 1, acceleratorPanel, chkAccelerator);
		gridBagUtils.addToGridBagLayout(1, 1, 1, 1, acceleratorPanel, new JLabel("User name:"));
		gridBagUtils.addToGridBagLayout(1, 2, acceleratorPanel, new JLabel("Password:"));
		gridBagUtils.addToGridBagLayout(2, 1, 1, 1, 1, 0, acceleratorPanel, txtUserName);
		gridBagUtils.addToGridBagLayout(2, 2, acceleratorPanel, txtPassword);
		gridBagUtils.clearGridBagConstraints();

		gridBagUtils.addToGridBagLayout(0, 0, serverConnectionPanel, new JLabel("Server:"));
		gridBagUtils.addToGridBagLayout(0, 1, serverConnectionPanel, new JLabel("Client port:"));
		gridBagUtils.addToGridBagLayout(0, 2, serverConnectionPanel, new JLabel("Admin port:"));
		gridBagUtils.addToGridBagLayout(1, 0, 3, 1, serverConnectionPanel, txtServer);
		gridBagUtils.addToGridBagLayout(1, 1, 1, 1, serverConnectionPanel, spinnerClientPort);
		gridBagUtils.addToGridBagLayout(1, 2, serverConnectionPanel, spinnerAdminPort);
		gridBagUtils.addToGridBagLayout(2, 1, 1, 1, 1, 0, serverConnectionPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(3, 6, 1, 1, 0, 0, serverConnectionPanel, btnCheckConnection);
		gridBagUtils.addToGridBagLayout(0, 3, 4, 3, 0, 0, serverConnectionPanel, acceleratorPanel);
		gridBagUtils.clearGridBagConstraints();
		return serverConnectionPanel;
	}

	private JPanel initializeTemplatesPanel() {
		JPanel templatesPanel = new JPanel();
		templatesPanel.setBorder(BorderFactory.createTitledBorder("Templates"));
		GridBagLayout templatesPanelLayout = new GridBagLayout();
		templatesPanelLayout.columnWidths = new int[] { 20, 270 };
		templatesPanelLayout.rowHeights = new int[] { 20, 25, 20, 275 };
		templatesPanel.setLayout(templatesPanelLayout);

		radioFromDirectory = new JRadioButton("Load templates from directory");
		radioFromDirectory.addActionListener(this);
		radioFromDirectory.setSelected(true);

		radioFromDatabase = new JRadioButton("Load templates from database");
		radioFromDatabase.addActionListener(this);

		ButtonGroup sourceGroup = new ButtonGroup();
		sourceGroup.add(radioFromDirectory);
		sourceGroup.add(radioFromDatabase);

		txtDirectoryPath = new JTextField("c:\\");

		btnBrowse = new JButton("...");
		btnBrowse.addActionListener(this);

		JPanel fromDirectoryPanel = new JPanel();
		fromDirectoryPanel.setLayout(new BoxLayout(fromDirectoryPanel, BoxLayout.X_AXIS));
		fromDirectoryPanel.add(txtDirectoryPath);
		fromDirectoryPanel.add(Box.createHorizontalStrut(5));
		fromDirectoryPanel.add(btnBrowse);

		initializeDatabasePanel();

		gridBagUtils.setInsets(new Insets(2, 2, 2, 2));
		gridBagUtils.addToGridBagLayout(0, 0, 2, 1, templatesPanel, radioFromDirectory);
		gridBagUtils.addToGridBagLayout(1, 1, 1, 1, 1, 0, templatesPanel, fromDirectoryPanel);
		gridBagUtils.addToGridBagLayout(0, 2, 2, 1, 0, 0, templatesPanel, radioFromDatabase);
		gridBagUtils.addToGridBagLayout(0, 3, templatesPanel, databasePanel);
		gridBagUtils.addToGridBagLayout(0, 4, 1, 1, 0, 1, templatesPanel, new JLabel());
		gridBagUtils.clearGridBagConstraints();

		radioFromDirectory.setAlignmentX(LEFT_ALIGNMENT);
		radioFromDatabase.setAlignmentX(LEFT_ALIGNMENT);
		return templatesPanel;
	}

	@SuppressWarnings("rawtypes")
	private void initializeDatabasePanel() {
		databasePanel = new JPanel();
		databasePanel.setBorder(BorderFactory.createTitledBorder(""));

		GridBagLayout databaseLayout = new GridBagLayout();
		databaseLayout.columnWidths = new int[] { 55, 40, 90, 90, 1 };
		databasePanel.setLayout(databaseLayout);

		KeyListener textFieldKeyListener = new DatabaseTextFieldKeyListener();
		txtDSN = new JTextField();
		txtDSN.addKeyListener(textFieldKeyListener);

		txtDBUser = new JTextField();
		txtDBUser.addKeyListener(textFieldKeyListener);

		txtDBPassword = new JPasswordField();
		txtDBPassword.addKeyListener(textFieldKeyListener);

		btnConnect = new JButton("Connect*");
		btnConnect.addActionListener(this);

		btnReset = new JButton("Reset");
		btnReset.addActionListener(this);

		cmbTable = new JComboBox();
		cmbTable.addActionListener(this);

		cmbTemplateColumn = new JComboBox();
		cmbIdColumn = new JComboBox();

		lblDBConnectMsg = new JLabel("*- Connect database to change table settings");

		gridBagUtils.setInsets(new Insets(5, 5, 5, 5));
		gridBagUtils.addToGridBagLayout(0, 1, 4, 1, databasePanel, new JLabel("DSN:"));
		gridBagUtils.addToGridBagLayout(0, 3, databasePanel, new JLabel("UID:"));
		gridBagUtils.addToGridBagLayout(0, 4, databasePanel, new JLabel("PWD:"));
		gridBagUtils.addToGridBagLayout(1, 1, databasePanel, txtDSN);
		gridBagUtils.addToGridBagLayout(1, 3, databasePanel, txtDBUser);
		gridBagUtils.addToGridBagLayout(1, 4, databasePanel, txtDBPassword);
		gridBagUtils.addToGridBagLayout(2, 5, 1, 1, databasePanel, btnConnect);
		gridBagUtils.addToGridBagLayout(3, 5, databasePanel, btnReset);
		gridBagUtils.addToGridBagLayout(4, 5, 1, 1, 1, 0, databasePanel, new JLabel());
		gridBagUtils.addToGridBagLayout(0, 6, 2, 1, 0, 0, databasePanel, new JLabel("Table:"));
		gridBagUtils.addToGridBagLayout(0, 7, databasePanel, new JLabel("Template column:"));
		gridBagUtils.addToGridBagLayout(0, 8, databasePanel, new JLabel("ID column:"));
		gridBagUtils.addToGridBagLayout(2, 6, databasePanel, cmbTable);
		gridBagUtils.addToGridBagLayout(2, 7, 3, 1, databasePanel, cmbTemplateColumn);
		gridBagUtils.addToGridBagLayout(2, 8, databasePanel, cmbIdColumn);
		gridBagUtils.addToGridBagLayout(0, 9, 4, 1, databasePanel, lblDBConnectMsg);
	}

	private JPanel initializeButtonPanel() {
		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));

		btnResetAll = new JButton("Reset all");
		btnResetAll.addActionListener(this);

		btnOK = new JButton("OK");
		btnOK.setPreferredSize(new Dimension(75, 25));
		btnOK.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setPreferredSize(new Dimension(75, 25));
		btnCancel.addActionListener(this);

		buttonPanel.add(Box.createHorizontalStrut(3));
		buttonPanel.add(btnResetAll);
		buttonPanel.add(Box.createGlue());
		buttonPanel.add(btnOK);
		buttonPanel.add(Box.createHorizontalStrut(5));
		buttonPanel.add(btnCancel);
		buttonPanel.add(Box.createHorizontalStrut(3));
		return buttonPanel;
	}

	private void loadSettings() {
		txtServer.setText(settings.getServer());
		spinnerClientPort.setValue(settings.getClientPort());
		spinnerAdminPort.setValue(settings.getAdminPort());
		chkAccelerator.setSelected(settings.isServerModeAccelerator());
		txtUserName.setText(settings.getMMAUser());
		txtPassword.setText(settings.getMMAPassword());
		selectAccelerator(settings.isServerModeAccelerator());

		txtDSN.setText(settings.getDSN());
		txtDBUser.setText(settings.getDBUser());
		txtDBPassword.setText(settings.getDBPassword());

		setUseDb(settings.isTemplateSourceDb());
		txtDirectoryPath.setText(settings.getTemplateDirectory());

		if (settings.isDSNConnected()) {
			listTables();
		}
		setTableSettingSelectionEnabled(settings.isDSNConnected());
	}

	private void resetAllToDefault() {
		settings.loadDefaultConnectionSettings();
		settings.loadDefaultDatabaseConnectionSettings();
		loadSettings();
	}

	private void resetDatabaseConnectionSettings() {
		settings.loadDefaultDatabaseConnectionSettings();
		txtDSN.setText(settings.getDSN());
		txtDBUser.setText(settings.getDBUser());
		txtDBPassword.setText(settings.getDBPassword());

		cmbTable.removeActionListener(this);
		cmbTable.removeAllItems();
		cmbIdColumn.removeAllItems();
		cmbTemplateColumn.removeAllItems();
	}

	private void selectTemplateSource() {
		boolean isUseDB = radioFromDatabase.isSelected();
		settings.setTemplateSourceDb(isUseDB);
		setDatabasePanelEnabled(isUseDB);
		btnBrowse.setEnabled(!isUseDB);
		txtDirectoryPath.setEnabled(!isUseDB);
	}

	private void setDatabasePanelEnabled(boolean enabled) {
		for (Component c : databasePanel.getComponents()) {
			c.setEnabled(enabled);
		}
		if (enabled) {
			txtDSN.setEnabled(enabled);
			txtDBUser.setEnabled(enabled);
			txtDBPassword.setEnabled(enabled);
			cmbTable.setEnabled(settings.isDSNConnected() && enabled);
			cmbTemplateColumn.setEnabled(settings.isDSNConnected() && enabled);
			cmbIdColumn.setEnabled(settings.isDSNConnected() && enabled);
		}
	}

	private void setTableSettingSelectionEnabled(boolean enabled) {
		cmbTable.setEnabled(settings.isDSNConnected() && enabled);
		cmbTemplateColumn.setEnabled(settings.isDSNConnected() && enabled);
		cmbIdColumn.setEnabled(settings.isDSNConnected() && enabled);
		lblDBConnectMsg.setVisible(!enabled);
		btnConnect.setText(enabled ? "Connect" : "Connect*");
	}

	private void applyChanges() {
		settings.setTemplateSourceDb(radioFromDatabase.isSelected());
		settings.setDSN(txtDSN.getText().trim());
		String value = String.valueOf(cmbTable.getSelectedItem());
		if (value != null && !value.equals("")) {
			settings.setTable(value);
		}
		settings.setDBUser(txtDBUser.getText().trim());
		settings.setDBPassword(String.valueOf(txtDBPassword.getPassword()));
		value = String.valueOf(cmbIdColumn.getSelectedItem());
		if (value != null && !value.equals("")) {
			settings.setIdColumn(value);
		}
		value = String.valueOf(cmbTemplateColumn.getSelectedItem());
		if (value != null && !value.equals("")) {
			settings.setTemplateColumn(value);
		}
		settings.setMMAPassword(String.valueOf(txtPassword.getPassword()));
		settings.setMMAUser(txtUserName.getText().trim());
		settings.setClientPort((Integer) spinnerClientPort.getValue());
		settings.setAdminPort((Integer) spinnerAdminPort.getValue());
		settings.setServer(txtServer.getText().trim());
		settings.setTemplateDirectory(txtDirectoryPath.getText().trim());
		settings.setServerModeAccelerator(chkAccelerator.isSelected());
		settings.save();
		dispose();
	}

	@SuppressWarnings("unchecked")
	private void listCollumns(DatabaseConnection db) {
		cmbIdColumn.removeAllItems();
		cmbTemplateColumn.removeAllItems();

		if (cmbTable.getSelectedItem() != null) {
			try {
				String[] collumns = db.getColumns(String.valueOf(cmbTable.getSelectedItem()));
				for (String c : collumns) {
					cmbIdColumn.addItem(c);
					cmbTemplateColumn.addItem(c);
				}
			} catch (SQLException e) {
				e.printStackTrace();
				MessageUtils.showError(this, e);
			}
		}
	}

	private void connectToDatabase() {
		settings.setDSN(txtDSN.getText().trim());
		settings.setDBUser(txtDBUser.getText().trim());
		settings.setDBPassword(String.valueOf(txtDBPassword.getPassword()));
		settings.setDSNConnection(true);
		listTables();
	}

	@SuppressWarnings("unchecked")
	private void listTables() {
		try {
			String table = settings.getTable();
			DatabaseConnection db = new DatabaseConnection();
			db.checkConnection();
			cmbTable.removeAllItems();
			cmbTable.removeActionListener(this);

			String[] tables = db.getTables();
			for (String t : tables) {
				cmbTable.addItem(t);
			}
			cmbTable.addActionListener(this);
			if (comboBoxContainsItem(cmbTable, table)) {
				cmbTable.setSelectedItem(table);
			} else if (tables.length > 0) {
				cmbTable.setSelectedIndex(0);
			}

			setTableSettingSelectionEnabled(true);
		} catch (SQLException ex) {
			settings.setDSNConnection(false);
			MessageUtils.showError(this, "Database connetion failed due to: %s", new Object[] { ex });
		}
	}

	@SuppressWarnings("rawtypes")
	private boolean comboBoxContainsItem(JComboBox cmbBox, Object item) {
		for (int i = 0; i < cmbBox.getItemCount(); i++) {
			if (cmbBox.getItemAt(i).equals(item)) {
				return true;
			}
		}
		return false;
	}

	private void loadTableInformation() {
		settings.setTable(String.valueOf(cmbTable.getSelectedItem()));
		DatabaseConnection db = new DatabaseConnection();
		try {
			db.checkConnection();
		} catch (SQLException e) {
			e.printStackTrace();
			return;
		}
		listCollumns(db);
		String column = settings.getIdColumn();
		if (comboBoxContainsItem(cmbIdColumn, column)) {
			cmbIdColumn.setSelectedItem(column);
		} else if (cmbIdColumn.getItemCount() > 0) {
			cmbIdColumn.setSelectedIndex(0);
		}
		column = settings.getTemplateColumn();
		if (comboBoxContainsItem(cmbTemplateColumn, column)) {
			cmbTemplateColumn.setSelectedItem(column);
		} else if (cmbTemplateColumn.getItemCount() > 0) {
			cmbTemplateColumn.setSelectedIndex(0);
		}
	}

	private void selectAccelerator(boolean select) {
		txtUserName.setEnabled(select);
		txtPassword.setEnabled(select);
	}

	private void checkConnection() {
		try {
			if (ServerConnection.checkConnection(txtServer.getText().trim(), (Integer) spinnerAdminPort.getValue())) {
				MessageUtils.showInformation(this, "Connection test successful");
			} else {
				MessageUtils.showError(this, "Connection failed");
			}
		} catch (Throwable e) {
			e.printStackTrace();
			MessageUtils.showError(this, e.getMessage());
		}
	}

	private void browseForDirectory() {
		String path = txtDirectoryPath.getText().trim();
		if (path != null && !path.equals("")) {
			File dir = new File(path);
			if (dir.exists() && dir.isDirectory()) {
				folderBrowserDialog.setCurrentDirectory(dir);
			}
		}
		if (folderBrowserDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			txtDirectoryPath.setText(folderBrowserDialog.getSelectedFile().getPath());
		}
	}

	private void setUseDb(boolean useDb) {
		if (useDb) {
			radioFromDatabase.setSelected(true);
		} else {
			radioFromDirectory.setSelected(true);
		}
		selectTemplateSource();
	}

	private boolean checkDBStatus() {
		boolean isUseDB = radioFromDatabase.isSelected();

		if (isUseDB) {
			if (!settings.isDSNConnected()) {
				MessageUtils.showInformation(this, "Connection with database must be established before proceeding");
				return false;
			}
		} else {
			String templateDir = txtDirectoryPath.getText().trim();
			if (templateDir == null || templateDir.equals("")) {
				MessageUtils.showInformation(this, "Specified directory doesn't exists");
				return false;
			}
			File directory = new File(templateDir);
			if (!directory.exists() || !directory.isDirectory()) {
				MessageUtils.showInformation(this, "Specified directory doesn't exists");
				return false;
			}
		}
		return true;
	}

	// ==============================================
	// Event handling
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnCheckConnection) {
			checkConnection();
		} else if (source == chkAccelerator) {
			selectAccelerator(chkAccelerator.isSelected());
		} else if (source == radioFromDatabase || source == radioFromDirectory) {
			selectTemplateSource();
		} else if (source == btnBrowse) {
			browseForDirectory();
		} else if (source == btnConnect) {
			connectToDatabase();
		} else if (source == btnReset) {
			resetDatabaseConnectionSettings();
		} else if (source == cmbTable) {
			loadTableInformation();
		} else if (source == btnResetAll) {
			resetAllToDefault();
		} else if (source == btnOK && checkDBStatus()) {
			applyChanges();
		} else if (source == btnCancel) {
			settings.load();
			dispose();
		}
	}

	// ==============================================
	// Private class extending KeyListener
	// ==============================================

	private class DatabaseTextFieldKeyListener extends KeyAdapter {
		@Override
		public void keyTyped(KeyEvent e) {
			if (settings.isDSNConnected()) {
				setTableSettingSelectionEnabled(false);
			}
		}
	}
}
