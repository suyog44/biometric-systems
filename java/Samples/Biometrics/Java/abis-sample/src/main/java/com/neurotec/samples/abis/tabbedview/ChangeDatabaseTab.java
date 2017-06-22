package com.neurotec.samples.abis.tabbedview;

import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.concurrent.ExecutionException;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.SwingWorker;

import com.neurotec.biometrics.NBiographicDataElement;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.ConnectionParams;
import com.neurotec.samples.abis.ConnectionType;
import com.neurotec.samples.abis.LocalOperationsOption;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.swing.JNumericField;
import com.neurotec.samples.abis.util.MessageUtils;

public final class ChangeDatabaseTab extends ProgressTab implements TabNavigationListener, ActionListener, ItemListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final AbisModel model;
	private final TabbedAbisController abisController;
	private boolean busy;
	private boolean updating;

	private JButton btnOK;
	private JButton btnCancel;
	private JCheckBox cbClear;
	private JButton btnEdit;
	private JPanel panelButtons;
	private JPanel panelSQLite;
	private JPanel panelRemote;
	private JPanel panelOdbc;
	private JPanel panelSchema;
	private JPanel panelHint;
	private JRadioButton rbSQLite;
	private JRadioButton rbRemoteServer;
	private JRadioButton rbOdbc;
	private JNumericField numericFieldClientPort;
	private JNumericField numericFieldAdminPort;
	private JTextField textFieldServerAddress;
	private JTextField textFieldConnectionString;
	private JLabel lblAdminPort;
	private JLabel lblClientPort;
	private JLabel lblConnectionString;
	private JLabel lblExample;
	private JLabel lblTableName;
	private JLabel lblServerAddress;
	private JLabel lblLocalOperations;
	private JLabel lblLocalOperationsInfoIcon;
	private JLabel lblExampleDescription;
	private JLabel lblDatabaseSchema;
	private JLabel lblHint;
	private JTextField tfTable;
	private JComboBox comboBoxLocalOperations;
	private JComboBox comboBoxSchemas;


	// ===========================================================
	// Public constructor
	// ===========================================================

	public ChangeDatabaseTab(AbisModel model, TabbedAbisController abisController) {
		super();
		this.model = model;
		this.abisController = abisController;
		initGUI();
		updateSettings();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private ConnectionParams getConnectionParams() {
		ConnectionParams params = new ConnectionParams();
		params.setType(getSelected());
		params.setConnectionString(textFieldConnectionString.getText());
		params.setTable(tfTable.getText());
		params.setClientPort(Integer.parseInt(numericFieldClientPort.getText()));
		params.setAdminPort(Integer.parseInt(numericFieldAdminPort.getText()));
		params.setHost(textFieldServerAddress.getText());
		params.setClearDatabase(isClearDatabase());
		return params;
	}

	private boolean isClearDatabase() {
		return  cbClear.isSelected() && !rbRemoteServer.isSelected();
	}

	private ConnectionType getSelected() {
		return rbSQLite.isSelected() ? ConnectionType.SQLITE_DATABASE : (rbOdbc.isSelected() ? ConnectionType.ODBC_DATABASE : ConnectionType.REMOTE_MATCHING_SERVER);
	}

	private void updateControls() {
		ConnectionType type = getSelected();

		boolean isOdbc = type == ConnectionType.ODBC_DATABASE;
		boolean isRemote = type == ConnectionType.REMOTE_MATCHING_SERVER;

		tfTable.setEnabled(isOdbc);
		textFieldConnectionString.setEnabled(isOdbc);

		textFieldServerAddress.setEnabled(isRemote);
		numericFieldAdminPort.setEnabled(isRemote);
		numericFieldClientPort.setEnabled(isRemote);
		comboBoxLocalOperations.setEnabled(isRemote);

		cbClear.setEnabled(isOdbc || type == ConnectionType.SQLITE_DATABASE);
		cbClear.setSelected(cbClear.isEnabled());

		btnEdit.setEnabled((comboBoxSchemas.getSelectedItem() != null) && !((DatabaseSchema) comboBoxSchemas.getSelectedItem()).isEmpty());

		panelHint.setVisible(isRemote);
	}

	private void listLocalOperationOptions() {
		updating = true;
		try {
			DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxLocalOperations.getModel();
			model.removeAllElements();
			for (LocalOperationsOption option : LocalOperationsOption.values()) {
				model.addElement(option);
			}
			comboBoxLocalOperations.setSelectedItem(SettingsManager.getCurrentLocalOperationsOption());
		} finally {
			updating = false;
		}
	}

	private void listSchemas() {
		updating = true;
		try {
			DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxSchemas.getModel();
			model.removeAllElements();
			for (DatabaseSchema schema : SettingsManager.getSchemas()) {
				model.addElement(schema);
			}
			model.addElement(DatabaseSchema.EMPTY);
			comboBoxSchemas.setSelectedIndex(SettingsManager.getCurrentSchemaIndex());
		} finally {
			updating = false;
		}
	}

	private void updateSettings() {
		switch (SettingsManager.getConnectionType()) {
		case SQLITE_DATABASE:
			rbSQLite.setSelected(true);
			break;
		case ODBC_DATABASE:
			rbOdbc.setSelected(true);
			break;
		case REMOTE_MATCHING_SERVER:
			rbRemoteServer.setSelected(true);
			break;
		}
		textFieldConnectionString.setText(SettingsManager.getOdbcConnectionString());

		String tableName = SettingsManager.getTableName();
		if (tableName != null && !tableName.equals("")) {
			tfTable.setText(tableName);
		}

		textFieldServerAddress.setText(SettingsManager.getRemoteServerAddress());
		numericFieldClientPort.setText(String.valueOf(SettingsManager.getRemoteServerPort()));
		numericFieldAdminPort.setText(String.valueOf(SettingsManager.getRemoteServerAdminPort()));
		updateControls();
	}

	private void setBusy(boolean busy) {
		this.busy = busy;
		btnOK.setEnabled(!busy);
	}

	private void save() {
		DatabaseSchema schema = (DatabaseSchema) comboBoxSchemas.getSelectedItem();
		if (rbRemoteServer.isSelected() && schema.hasCustomData()) {
			StringBuilder sb = new StringBuilder();
			for (NBiographicDataElement element : schema.getCustomData().getElements()) {
				sb.append(element.name);
				sb.append(", ");
			}
			sb.delete(sb.length() - 2, sb.length());
			MessageUtils.showInformation(this, "Warning", "Current schema contains custom data (Columns: %s). Only biographic data is supported with remote matching server. Please select different schema or edit current one.", sb.toString());
			return;
		}
		if (!rbSQLite.isSelected() && SettingsManager.isWarnHasSchema() && !schema.isEmpty()) {
			if (MessageUtils.showQuestion(this, "Warning", "Please note that biometric client will not automaticly create columns specified in database schema for odbc connection or matching server. User must ensure that columns specified in schema exist. Continue anyway?")) {
				SettingsManager.setWarnHasSchema(false);
			} else {
				return;
			}
		}

		setBusy(true);
		showProgress(0, 100);
		setTitle("Initializing biometric client ...");

		SwingWorker<NBiometricStatus, Void> worker = new SwingWorker<NBiometricStatus, Void>() {

			@Override
			protected NBiometricStatus doInBackground() throws Exception {
				model.setDatabaseSchema((DatabaseSchema) comboBoxSchemas.getSelectedItem());
				return model.initClient(getConnectionParams());
			}

			@Override
			protected void done() {
				super.done();
				setProgress(100);
				setBusy(false);
				try {
					NBiometricStatus status = get();
					if ((status != NBiometricStatus.OK) && (status != NBiometricStatus.CANCELED)) {
						MessageUtils.showInformation(ChangeDatabaseTab.this, String.format("Failed to clear database: %s", status));
					}
				} catch (InterruptedException e) {
					e.printStackTrace();
					Thread.currentThread().interrupt();
				} catch (ExecutionException e) {
					MessageUtils.showError(ChangeDatabaseTab.this, e);
				}
				abisController.closeTab(ChangeDatabaseTab.this);
				abisController.start();
			}
		};

		model.addPropertyChangeListener(new PropertyChangeListener() {

			@Override
			public void propertyChange(PropertyChangeEvent ev) {
				if (AbisModel.PROPERTY_PROGRESS.equals(ev.getPropertyName())) {
					setProgress("Initializing ...", (Integer) ev.getNewValue());
				}
			}
		});

		worker.execute();
	}

	private void initGUI() {
		JPanel panelOuter = new JPanel(new FlowLayout(FlowLayout.CENTER));
		JPanel panelMain = new JPanel();
		panelMain.setBorder(BorderFactory.createTitledBorder("Connection Settings"));
		ButtonGroup bgroup = new ButtonGroup();
		{
			this.setTitle("Connection Settings");
			panelMain.setLayout(new BoxLayout(panelMain, BoxLayout.Y_AXIS));
			{
				panelSQLite = new JPanel();
				panelMain.add(panelSQLite);
				panelSQLite.setLayout(new FlowLayout(FlowLayout.LEFT, 5, 5));
				{
					rbSQLite = new JRadioButton("SQLite database connection");
					panelSQLite.add(rbSQLite);
					rbSQLite.setSelected(true);
					rbSQLite.addActionListener(this);
					bgroup.add(rbSQLite);
				}
			}
		}
		{
			JPanel panel_1 = new JPanel(new FlowLayout(FlowLayout.LEFT));
			panelMain.add(panel_1);
			{
				rbOdbc = new JRadioButton("Odbc database connection");
				panel_1.add(rbOdbc);
				rbOdbc.setSelected(false);
				rbOdbc.addActionListener(this);
				bgroup.add(rbOdbc);
			}
		}
		{
			panelOdbc = new JPanel();
			panelOdbc.setEnabled(false);
			panelMain.add(panelOdbc);
			GridBagLayout gbl_panelOdbc = new GridBagLayout();
			gbl_panelOdbc.columnWidths = new int[] {100, 142, 50, 0};
			gbl_panelOdbc.columnWeights = new double[]{0.1, 0.1, 0.1, 1.0};
			gbl_panelOdbc.rowWeights = new double[]{0.1, 0.1, 0.1};
			panelOdbc.setLayout(gbl_panelOdbc);
			{
				lblConnectionString = new JLabel("Connection string:");
				panelOdbc.add(lblConnectionString, new GridBagConstraints(0, 0, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 0, 5, 5), 0, 0));
			}
			{
				textFieldConnectionString = new JTextField();
				panelOdbc.add(textFieldConnectionString, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.CENTER, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 5, 5), 0, 0));
				textFieldConnectionString.setPreferredSize(new Dimension(171, 23));
			}
			{
				lblExample = new JLabel("Example:");
				panelOdbc.add(lblExample, new GridBagConstraints(0, 1, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 0, 5, 5), 0, 0));
			}
			{
				lblExampleDescription = new JLabel("Dsn=mysql_dsn;UID=user;PWD=password");
				panelOdbc.add(lblExampleDescription, new GridBagConstraints(1, 1, 1, 1, 0.0, 0.0, GridBagConstraints.CENTER, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 5, 5), 0, 0));
			}
			{
				lblTableName = new JLabel("Table name:");
				panelOdbc.add(lblTableName, new GridBagConstraints(0, 2, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 0, 5, 5), 0, 0));
			}
			{
				tfTable = new JTextField();
				panelOdbc.add(tfTable, new GridBagConstraints(1, 2, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 5, 5), 0, 0));
				tfTable.setPreferredSize(new Dimension(100, 23));
				tfTable.setText("Subjects");

			}
		}
		{
			JPanel panel = new JPanel(new FlowLayout(FlowLayout.LEFT));
			panelMain.add(panel);
			{
				rbRemoteServer = new JRadioButton();
				panel.add(rbRemoteServer);
				rbRemoteServer.setAlignmentY(0.0f);
				BoxLayout rbUseServerLayout = new BoxLayout(rbRemoteServer, javax.swing.BoxLayout.Y_AXIS);
				rbRemoteServer.setLayout(rbUseServerLayout);
				rbRemoteServer.setText("Use remote server");
			}
			bgroup.add(rbRemoteServer);
			rbRemoteServer.addActionListener(this);
		}
		{
			panelRemote = new JPanel();
			panelMain.add(panelRemote);

			GridBagLayout gbl_panelRemote = new GridBagLayout();
			gbl_panelRemote.rowWeights = new double[] {0.1, 0.1, 0.1};
			gbl_panelRemote.columnWeights = new double[] {0.0, 0.0, 1.0};
			gbl_panelRemote.columnWidths = new int[] {100, 142, 0};
			panelRemote.setLayout(gbl_panelRemote);
			{
				lblServerAddress = new JLabel("Server address:");
				panelRemote.add(lblServerAddress, new GridBagConstraints(0, 0, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 20, 0, 5), 0, 0));
			}
			{
				textFieldServerAddress = new JTextField();
				panelRemote.add(textFieldServerAddress, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.CENTER, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 0, 0), 0, 0));
				textFieldServerAddress.setPreferredSize(new java.awt.Dimension(171, 23));
			}
			{
				lblClientPort = new JLabel("Client port:");
				panelRemote.add(lblClientPort, new GridBagConstraints(0, 1, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 0, 0, 5), 0, 0));
			}
			{
				numericFieldClientPort = new JNumericField();
				panelRemote.add(numericFieldClientPort, new GridBagConstraints(1, 1, 1, 1, 0.0, 0.0, GridBagConstraints.CENTER, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 0, 0), 0, 0));
				numericFieldClientPort.setPreferredSize(new java.awt.Dimension(170, 23));
				numericFieldClientPort.setText("25452");
			}
			{
				lblAdminPort = new JLabel("Admin port:");
				panelRemote.add(lblAdminPort, new GridBagConstraints(0, 2, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 0, 0, 5), 0, 0));
			}
			{
				numericFieldAdminPort = new JNumericField();
				panelRemote.add(numericFieldAdminPort, new GridBagConstraints(1, 2, 1, 1, 0.0, 0.0, GridBagConstraints.CENTER, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 0, 0), 0, 0));
				numericFieldAdminPort.setPreferredSize(new java.awt.Dimension(170, 23));
				numericFieldAdminPort.setText("24932");
			}
			{
				lblLocalOperations = new JLabel("Local operations:");
				panelRemote.add(lblLocalOperations, new GridBagConstraints(0, 3, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(0, 0, 0, 5), 0, 0));
			}
			{
				comboBoxLocalOperations = new JComboBox(new DefaultComboBoxModel());
				panelRemote.add(comboBoxLocalOperations, new GridBagConstraints(1, 3, 1, 1, 0.0, 0.0, GridBagConstraints.CENTER, GridBagConstraints.HORIZONTAL, new Insets(0, 0, 0, 0), 0, 0));
				listLocalOperationOptions();
				comboBoxLocalOperations.addItemListener(this);
			}
		}
		{
			JPanel panel_2 = new JPanel(new FlowLayout(FlowLayout.LEFT));
			panelMain.add(panel_2);
			{
				cbClear = new JCheckBox("Clear all data");
				panel_2.add(cbClear);
				cbClear.setMargin(new Insets(2, 20, 2, 2));
				cbClear.setHorizontalAlignment(SwingConstants.LEFT);
			}
		}
		{
			panelSchema = new JPanel(new FlowLayout(FlowLayout.LEADING));
			panelMain.add(panelSchema);
			{
				lblDatabaseSchema = new JLabel("Database schema: ");
				panelSchema.add(lblDatabaseSchema);
			}
			{
				comboBoxSchemas = new JComboBox(new DefaultComboBoxModel());
				panelSchema.add(comboBoxSchemas);
				listSchemas();
				comboBoxSchemas.addItemListener(this);
			}
			{
				btnEdit = new JButton("Edit");
				panelSchema.add(btnEdit);
				btnEdit.addActionListener(this);
			}
		}

		{
			panelButtons = new JPanel();
			FlowLayout flowLayout = (FlowLayout) panelButtons.getLayout();
			flowLayout.setAlignment(FlowLayout.RIGHT);
			panelMain.add(panelButtons);
			btnOK = new JButton();
			panelButtons.add(btnOK);
			btnOK.setText("OK");
			btnOK.addActionListener(this);
			btnCancel = new JButton();
			panelButtons.add(btnCancel);
			btnCancel.setText("Cancel");
			btnCancel.addActionListener(this);
		}

		{
			panelHint = new JPanel(new FlowLayout(FlowLayout.LEADING));
			lblHint = new JLabel("Please make sure database schema is correct for current database or remote matching server.");
			panelHint.add(lblHint);
			panelMain.add(panelHint);
		}
		panelOuter.add(panelMain);
		panelMain.setPreferredSize(new Dimension(panelHint.getPreferredSize().width + 30, panelMain.getPreferredSize().height));
		add(panelOuter, SwingConstants.CENTER);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void tabAdded(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing;
	}

	@Override
	public void tabEnter(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			listSchemas();
			listLocalOperationOptions();
			updateControls();
		}
	}

	@Override
	public void tabLeave(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing;
	}

	@Override
	public void tabClose(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource().equals(btnCancel)) {
				if (busy) {
					abisController.closeTab(ChangeDatabaseTab.this);
					model.setCanceled(true);
				} else {
					abisController.closeTab(ChangeDatabaseTab.this);
					abisController.start();
				}
			} else if (ev.getSource().equals(btnOK)) {
				save();
			} else if (ev.getSource().equals(rbRemoteServer)) {
				if (rbRemoteServer.isSelected()) {
					updateControls();
				}
			} else if (ev.getSource().equals(rbOdbc)) {
				if (rbOdbc.isSelected()) {
					updateControls();
				}
			} else if (ev.getSource().equals(rbSQLite)) {
				if (rbSQLite.isSelected()) {
					updateControls();
				}
			} else if (ev.getSource().equals(btnEdit)) {
				abisController.editSchema((DatabaseSchema) comboBoxSchemas.getSelectedItem());
			}
		} catch (Exception ex) {
			MessageUtils.showError(this, ex);
		}
	}

	@Override
	public void itemStateChanged(ItemEvent ev) {
		if (ev.getStateChange() == ItemEvent.SELECTED) {
			if (ev.getSource().equals(comboBoxSchemas)) {
				if (!updating) {
					SettingsManager.setCurrentSchema(comboBoxSchemas.getSelectedIndex());
					updateControls();
				}
			} else if (ev.getSource().equals(comboBoxLocalOperations)) {
				if (!updating) {
					SettingsManager.setCurrentLocalOperationsOption((LocalOperationsOption) comboBoxLocalOperations.getSelectedItem());
					updateControls();
				}
			}
		}
	}

}
