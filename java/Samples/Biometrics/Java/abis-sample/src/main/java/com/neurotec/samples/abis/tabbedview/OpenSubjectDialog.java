package com.neurotec.samples.abis.tabbedview;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.ArrayList;
import java.util.List;

import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.samples.util.Utils;

public final class OpenSubjectDialog extends JDialog implements ActionListener, ItemListener {

	private static class ListItem {

		public String name;
		public int value;

		ListItem(String name, int value) {
			this.name = name;
			this.value = value;
		}

		@Override
		public String toString() {
			return name;
		}

	}


	private static final long serialVersionUID = 1L;

	private JButton btnBrowse;
	private JButton btnCancel;
	private JButton btnOk;
	private JComboBox comboBoxOwner;
	private JComboBox comboBoxType;
	private JTextField tfFileName;
	private JLabel lblFileName;
	private JLabel lblFormatOwner;
	private JLabel lblFormatType;
	private JPanel panelButtons;
	private JPanel panelCenter;
	private JPanel panelGrid;

	private JFileChooser fc;
	private DialogAction action;

	public OpenSubjectDialog() {
		action = DialogAction.NONE;
		initGUI();
		updateOwners();
	}

	private void initGUI() {
		java.awt.GridBagConstraints gridBagConstraints;

		panelCenter = new javax.swing.JPanel();
		panelGrid = new javax.swing.JPanel();
		lblFileName = new javax.swing.JLabel();
		lblFormatOwner = new javax.swing.JLabel();
		lblFormatType = new javax.swing.JLabel();
		tfFileName = new javax.swing.JTextField();
		btnBrowse = new javax.swing.JButton();
		comboBoxOwner = new javax.swing.JComboBox();
		comboBoxType = new javax.swing.JComboBox();
		panelButtons = new javax.swing.JPanel();
		btnOk = new javax.swing.JButton();
		btnCancel = new javax.swing.JButton();

		setLayout(new java.awt.BorderLayout());

		panelCenter.setLayout(new javax.swing.BoxLayout(panelCenter, javax.swing.BoxLayout.Y_AXIS));

		panelGrid.setAlignmentX(0.0F);
		java.awt.GridBagLayout panelGridLayout = new java.awt.GridBagLayout();
		panelGridLayout.columnWidths = new int[] {0, 5, 250, 5, 0, 5, 0, 5, 0};
		panelGridLayout.rowHeights = new int[] {0, 5, 0, 5, 0, 5, 0};
		panelGrid.setLayout(panelGridLayout);

		lblFileName.setText("File name:");
		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = java.awt.GridBagConstraints.LINE_END;
		panelGrid.add(lblFileName, gridBagConstraints);

		lblFormatOwner.setText("Format owner:");
		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.anchor = java.awt.GridBagConstraints.LINE_END;
		panelGrid.add(lblFormatOwner, gridBagConstraints);

		lblFormatType.setText("Format type:");
		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 4;
		gridBagConstraints.anchor = java.awt.GridBagConstraints.LINE_END;
		panelGrid.add(lblFormatType, gridBagConstraints);

		tfFileName.setEditable(false);
		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.gridwidth = 5;
		gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
		panelGrid.add(tfFileName, gridBagConstraints);

		btnBrowse.setText(" ... ");
		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 8;
		gridBagConstraints.gridy = 0;
		panelGrid.add(btnBrowse, gridBagConstraints);

		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.gridwidth = 7;
		gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
		panelGrid.add(comboBoxOwner, gridBagConstraints);

		gridBagConstraints = new java.awt.GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 4;
		gridBagConstraints.gridwidth = 7;
		gridBagConstraints.fill = java.awt.GridBagConstraints.HORIZONTAL;
		panelGrid.add(comboBoxType, gridBagConstraints);

		panelCenter.add(panelGrid);

		panelButtons.setAlignmentX(0.0F);
		panelButtons.setLayout(new java.awt.FlowLayout(java.awt.FlowLayout.TRAILING));

		btnOk.setText("OK");
		btnOk.setPreferredSize(new java.awt.Dimension(65, 23));
		panelButtons.add(btnOk);

		btnCancel.setText("Cancel");
		panelButtons.add(btnCancel);

		panelCenter.add(panelButtons);

		add(panelCenter, java.awt.BorderLayout.CENTER);

		setIconImage(Utils.createIconImage("images/Logo16x16.png"));
		pack();

		btnOk.setEnabled(false);

		btnBrowse.addActionListener(this);
		btnOk.addActionListener(this);
		btnCancel.addActionListener(this);
		comboBoxOwner.addItemListener(this);

		fc = new JFileChooser();
	}

	private List<ListItem> getTypes(ListItem owner) {
		List<ListItem> types = new ArrayList<ListItem>();
		try {
			for (Field field : CBEFFBDBFormatIdentifiers.class.getDeclaredFields()) {
				int mod = field.getModifiers();
				if (Modifier.isPublic(mod) && Modifier.isStatic(mod) && field.getName().toLowerCase().startsWith(owner.name.toLowerCase().replace(' ', '_'))) {
						int ownerNameLength = owner.name.length();
						types.add(new ListItem(field.getName().substring(ownerNameLength + 1), field.getInt(null)));
				}
			}
		} catch (IllegalAccessException e) {
			throw new AssertionError("Cannot happen - field is public and static");
		}
		return types;
	}

	private List<ListItem> getOwners() {
		List<ListItem> owners = new ArrayList<ListItem>();
		owners.add(new ListItem("Auto detect", CBEFFBiometricOrganizations.NOT_FOR_USE));
		owners.add(new ListItem("Neurotechnologija", CBEFFBiometricOrganizations.NEUROTECHNOLOGIJA));
		owners.add(new ListItem("INCITS TC M1 Biometrics", CBEFFBiometricOrganizations.INCITS_TC_M1_BIOMETRICS));
		owners.add(new ListItem("ISO IEC JTC 1 SC 37 Biometrics", CBEFFBiometricOrganizations.ISO_IEC_JTC_1_SC_37_BIOMETRICS));
		return owners;
	}

	private void browse() {
		if (fc.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			tfFileName.setText(fc.getSelectedFile().getAbsolutePath());
		}
		btnOk.setEnabled(!tfFileName.getText().isEmpty());
	}

	private void updateTypes(ListItem owner) {
		comboBoxType.removeAllItems();
		List<ListItem> items = getTypes(owner);
		for (ListItem item : items) {
			comboBoxType.addItem(item);
		}
		if (items.isEmpty()) {
			comboBoxType.setEnabled(false);
		} else {
			comboBoxType.setEnabled(true);
			comboBoxType.setSelectedIndex(0);
		}
		pack();
	}

	private void updateOwners() {
		comboBoxOwner.removeAllItems();
		List<ListItem> items = getOwners();
		for (ListItem item : items) {
			comboBoxOwner.addItem(item);
		}
		if (items.isEmpty()) {
			comboBoxOwner.setEnabled(false);
		} else {
			comboBoxOwner.setEnabled(true);
			comboBoxOwner.setSelectedIndex(0);
		}
		pack();
	}

	public String getFileName() {
		return tfFileName.getText();
	}

	public int getFormatOwner() {
		if (comboBoxOwner.getSelectedIndex() == -1) {
			return 0;
		} else {
			return ((ListItem) comboBoxOwner.getSelectedItem()).value;
		}
	}

	public int getFormatType() {
		if (comboBoxType.getSelectedIndex() == -1) {
			return 0;
		} else {
			return ((ListItem) comboBoxType.getSelectedItem()).value;
		}
	}

	public DialogAction getAction() {
		return action;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource().equals(btnBrowse)) {
			browse();
		} else if (ev.getSource().equals(btnOk)) {
			action = DialogAction.OK;
			dispose();
		} else if (ev.getSource().equals(btnCancel)) {
			action = DialogAction.CANCEL;
			dispose();
		}
	}

	@Override
	public void itemStateChanged(ItemEvent ev) {
		if (ev.getStateChange() == ItemEvent.SELECTED) {
			if (comboBoxOwner.equals(ev.getSource())) {
				updateTypes((ListItem) comboBoxOwner.getSelectedItem());
			}
		}
	}

}
