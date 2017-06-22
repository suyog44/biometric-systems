package com.neurotec.samples.abis.swing;

import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.schema.SchemaBuilderModel;
import com.neurotec.samples.abis.schema.SchemaColumnType;
import com.neurotec.samples.abis.schema.SchemaElementGroup;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.tabbedview.Tab;
import com.neurotec.samples.abis.tabbedview.TabbedAbisController;
import com.neurotec.samples.abis.util.MessageUtils;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.JTextField;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;

public class SchemaBuilderTab extends Tab implements ActionListener, ListSelectionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private DatabaseSchema schema;
	private DatabaseSchema original;
	private final TabbedAbisController controller;
	private final AbisModel abisModel;

	private JTable tableSample;
	private JTable tableBiographic;
	private JTable tableCustom;
	private SchemaBuilderModel biographicDataModel;
	private SchemaBuilderModel sampleDataModel;
	private SchemaBuilderModel customDataModel;

	private JButton btnAdd;
	private JButton btnCancel;
	private JButton btnDelete;
	private JButton btnOk;
	private JComboBox cmbType;
	private JLabel lblBiographicData;
	private JLabel lblCustomData;
	private JLabel lblDbColumn;
	private JLabel lblName;
	private JLabel lblSampleData;
	private JLabel lblType;
	private JPanel panelBiographicData;
	private JPanel panelBottom;
	private JPanel panelCenter;
	private JPanel panelCustomData;
	private JPanel panelLeft;
	private JPanel panelSampleData;
	private JPanel panelTop;
	private JScrollPane spBiographicData;
	private JScrollPane spCustomData;
	private JScrollPane spSampleData;
	private JTextField tfDbColumn;
	private JTextField tfName;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SchemaBuilderTab(TabbedAbisController controller, AbisModel abisModel) {
		super("Schema Builder");
		this.controller = controller;
		this.abisModel = abisModel;
		initGui();
		updateControls();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGui() {
		GridBagConstraints gridBagConstraints;

		panelTop = new JPanel();
		lblType = new JLabel();
		cmbType = new JComboBox();
		lblName = new JLabel();
		tfName = new JTextField();
		lblDbColumn = new JLabel();
		tfDbColumn = new JTextField();
		btnAdd = new JButton();
		panelCenter = new JPanel();
		panelSampleData = new JPanel();
		lblSampleData = new JLabel();
		spSampleData = new JScrollPane();
		panelBiographicData = new JPanel();
		lblBiographicData = new JLabel();
		spBiographicData = new JScrollPane();
		panelCustomData = new JPanel();
		lblCustomData = new JLabel();
		spCustomData = new JScrollPane();
		panelLeft = new JPanel();
		btnDelete = new JButton();
		panelBottom = new JPanel();
		btnOk = new JButton();
		btnCancel = new JButton();

		setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		setLayout(new BorderLayout(10, 5));

		GridBagLayout panelTopLayout = new GridBagLayout();
		panelTopLayout.columnWidths = new int[] {0, 5, 0, 5, 0, 5, 120, 5, 0, 5, 120, 5, 80};
		panelTopLayout.rowHeights = new int[] {0};
		panelTop.setLayout(panelTopLayout);

		lblType.setText("Type:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		panelTop.add(lblType, gridBagConstraints);

		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
		gridBagConstraints.weightx = 1.0;
		panelTop.add(cmbType, gridBagConstraints);

		lblName.setText("Name:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 4;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		panelTop.add(lblName, gridBagConstraints);
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 6;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
		panelTop.add(tfName, gridBagConstraints);

		lblDbColumn.setText("DB Column");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 8;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		panelTop.add(lblDbColumn, gridBagConstraints);
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 10;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
		panelTop.add(tfDbColumn, gridBagConstraints);

		btnAdd.setText("Add");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 12;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelTop.add(btnAdd, gridBagConstraints);

		add(panelTop, BorderLayout.NORTH);

		panelCenter.setLayout(new GridLayout(3, 1, 0, 5));

		panelSampleData.setLayout(new BorderLayout());

		lblSampleData.setText("Sample Data");
		panelSampleData.add(lblSampleData, BorderLayout.NORTH);
		panelSampleData.add(spSampleData, BorderLayout.CENTER);

		panelCenter.add(panelSampleData);

		panelBiographicData.setLayout(new BorderLayout());

		lblBiographicData.setText("Biographic Data");
		panelBiographicData.add(lblBiographicData, BorderLayout.NORTH);
		panelBiographicData.add(spBiographicData, BorderLayout.CENTER);

		panelCenter.add(panelBiographicData);

		panelCustomData.setLayout(new BorderLayout());

		lblCustomData.setText("Custom Data");
		panelCustomData.add(lblCustomData, BorderLayout.NORTH);
		panelCustomData.add(spCustomData, BorderLayout.CENTER);

		panelCenter.add(panelCustomData);

		add(panelCenter, BorderLayout.CENTER);

		panelLeft.setLayout(new BoxLayout(panelLeft, BoxLayout.Y_AXIS));

		btnDelete.setText("Delete");
		panelLeft.add(btnDelete);

		add(panelLeft, BorderLayout.WEST);

		panelBottom.setLayout(new FlowLayout(FlowLayout.TRAILING));

		btnOk.setText("OK");
		btnOk.setPreferredSize(new Dimension(70, 23));
		panelBottom.add(btnOk);

		btnCancel.setText("Cancel");
		btnCancel.setPreferredSize(new Dimension(70, 23));
		panelBottom.add(btnCancel);

		add(panelBottom, BorderLayout.SOUTH);

		sampleDataModel = new SchemaBuilderModel(DatabaseSchema.EMPTY, SchemaElementGroup.SAMPLE);
		biographicDataModel = new SchemaBuilderModel(DatabaseSchema.EMPTY, SchemaElementGroup.BIOGRAPHIC);
		customDataModel = new SchemaBuilderModel(DatabaseSchema.EMPTY, SchemaElementGroup.CUSTOM);
		tableSample = new JTable(sampleDataModel);
		tableBiographic = new JTable(biographicDataModel);
		tableCustom = new JTable(customDataModel);
		tableSample.getSelectionModel().addListSelectionListener(this);
		tableBiographic.getSelectionModel().addListSelectionListener(this);
		tableCustom.getSelectionModel().addListSelectionListener(this);
		spSampleData.setViewportView(tableSample);
		spBiographicData.setViewportView(tableBiographic);
		spCustomData.setViewportView(tableCustom);

		for (SchemaColumnType type : SchemaColumnType.values()) {
			if (type != SchemaColumnType.UNKNOWN) {
				cmbType.addItem(type);
			}
		}

		btnAdd.addActionListener(this);
		btnDelete.addActionListener(this);
		btnOk.addActionListener(this);
		btnCancel.addActionListener(this);
	}

	private void addPressed() {
		String name = tfName.getText().trim();
		if (name.isEmpty()) {
			MessageUtils.showInformation(this, "Name field cannot be empty");
			return;
		}
		SchemaColumnType type = (SchemaColumnType) cmbType.getSelectedItem();
		String dbColumn = tfDbColumn.getText().trim();
		schema.addElement(type, name, dbColumn);
		sampleDataModel.refresh();
		biographicDataModel.refresh();
		customDataModel.refresh();
		tfName.setText("");
		tfDbColumn.setText("");
	}

	private void deletePressed() {
		JTable selectedTable;
		if (tableSample.getSelectedRow() >= 0) {
			selectedTable = tableSample;
		} else if (tableBiographic.getSelectedRow() >= 0) {
			selectedTable = tableBiographic;
		} else if (tableCustom.getSelectedRow() >= 0) {
			selectedTable = tableCustom;
		} else {
			throw new IllegalStateException("Nothing selected");
		}
		int selectedIndex = selectedTable.getSelectedRow();
		if (!schema.removeElement((String) selectedTable.getModel().getValueAt(selectedIndex, 1))) {
			throw new IllegalStateException("Remove operation failed.");
		}
		((SchemaBuilderModel) selectedTable.getModel()).refresh();
		int rowCount = selectedTable.getRowCount();
		if (rowCount > selectedIndex) {
			selectedTable.setRowSelectionInterval(selectedIndex, selectedIndex);
		} else if (rowCount > 0) {
			selectedTable.setRowSelectionInterval(rowCount - 1, rowCount - 1);
		}
		repaint();
	}

	private void updateControls() {
		btnDelete.setEnabled((tableSample.getSelectedRow() >= 0) || (tableBiographic.getSelectedRow() >= 0) || (tableCustom.getSelectedRow() >= 0));
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setSchema(DatabaseSchema schema) {
		this.original = schema;
		this.schema = DatabaseSchema.parse(original.save());
		sampleDataModel.setSchema(this.schema);
		biographicDataModel.setSchema(this.schema);
		customDataModel.setSchema(this.schema);
	}

	public DatabaseSchema getSchema() {
		return schema;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource().equals(btnAdd)) {
				addPressed();
			} else if (ev.getSource().equals(btnDelete)) {
				deletePressed();
			} else if (ev.getSource().equals(btnCancel)) {
				controller.closeTab(this);
			} else if (ev.getSource().equals(btnOk)) {
				List<DatabaseSchema> schemas = SettingsManager.getSchemas();
				int index = schemas.indexOf(original);
				schemas.remove(index);
				schemas.add(index, schema);
				SettingsManager.setSchemas(schemas);
				controller.closeTab(this);
			} else {
				throw new AssertionError("Unknown source: " + ev.getSource());
			}
		} catch (Exception e) {
			e.printStackTrace();
			MessageUtils.showError(this, e);
		} finally {
			updateControls();
		}
	}

	@Override
	public void valueChanged(ListSelectionEvent ev) {
		if (ev.getSource().equals(tableSample.getSelectionModel())) {
			tableBiographic.clearSelection();
			tableCustom.clearSelection();
		} else if (ev.getSource().equals(tableBiographic.getSelectionModel())) {
			tableSample.clearSelection();
			tableCustom.clearSelection();
		} else if (ev.getSource().equals(tableCustom.getSelectionModel())) {
			tableSample.clearSelection();
			tableBiographic.clearSelection();
		} else {
			throw new AssertionError("Unknown source: " + ev.getSource());
		}
		updateControls();
	}

}
