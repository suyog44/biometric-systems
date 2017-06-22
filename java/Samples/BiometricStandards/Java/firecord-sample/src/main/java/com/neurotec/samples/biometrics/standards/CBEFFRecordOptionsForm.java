package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFPatronFormatIdentifiers;
import com.neurotec.samples.util.Utils;

import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.util.HashMap;
import java.util.Map;

import javax.swing.BorderFactory;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JTextField;
import javax.swing.WindowConstants;

public class CBEFFRecordOptionsForm extends JDialog implements ActionListener {

	//!doc
	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	//!doc
	// ===========================================================
	// Private fields
	// ===========================================================

	private JButton buttonOk;
	private JButton buttonClose;
	private JComboBox comboBoxOwner;
	private JComboBox comboBoxType;
	private JLabel labelFormat;
	private JLabel labelOwner;
	private JLabel labelType;
	private JPanel mainPanel;
	private JPanel commonAndType;
	private JPanel patronFormat;
	private JPanel ownerAndType;
	private JRadioButton radioOwnerAndType;
	private JRadioButton radioPatronFormat;
	private JTextField textFieldPatronFormat;

	private Map<String, Short> ownerLookup = new HashMap<String, Short>();
	private Map<String, Short> typesLookup = new HashMap<String, Short>();

	// ==============================================
	// Protected fields
	// ==============================================

	protected boolean isOk;

	//!doc
	// ===========================================================
	// Constructors
	// ===========================================================

	public CBEFFRecordOptionsForm(JFrame owner) {
		super(owner, true);
		initComponents();
		updateOwners();
		updateTypes();
	}

	//!doc
	// ===========================================================
	// Private methods
	// ===========================================================

	private void initComponents() {
		setIconImage(Utils.createIconImage("images/Logo16x16.png"));
		GridBagConstraints gridBagConstraints;
		this.setPreferredSize(new Dimension(600, 220));
		this.setResizable(false);

		setDefaultCloseOperation(WindowConstants.DISPOSE_ON_CLOSE);
		GridBagLayout layout = new GridBagLayout();
		layout.columnWidths = new int[] {0};
		layout.rowHeights = new int[] {0};
		getContentPane().setLayout(layout);

		mainPanel = new JPanel();
		mainPanel.setLayout(new GridBagLayout());
		{
			commonAndType = new JPanel();
			commonAndType.setBorder(BorderFactory.createTitledBorder("Common and Type"));
			commonAndType.setLayout(new java.awt.GridBagLayout());
			{
				radioOwnerAndType = new JRadioButton();
				radioOwnerAndType.setText("Owner and Type");
				radioOwnerAndType.addActionListener(new ActionListener() {

					@Override
					public void actionPerformed(ActionEvent e) {
						radioPatronFormat.setSelected(!radioOwnerAndType.isSelected());
						comboBoxOwner.setEnabled(radioOwnerAndType.isSelected());
						comboBoxType.setEnabled(radioOwnerAndType.isSelected());
						textFieldPatronFormat.setEnabled(!radioOwnerAndType.isSelected());
					}
				});
				radioOwnerAndType.setSelected(true);
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				commonAndType.add(radioOwnerAndType, gridBagConstraints);
			}
			{
				radioPatronFormat = new JRadioButton();
				radioPatronFormat.setText("Patron Format");
				radioPatronFormat.addActionListener(new ActionListener() {

					@Override
					public void actionPerformed(ActionEvent e) {
						radioOwnerAndType.setSelected(!radioPatronFormat.isSelected());
						comboBoxOwner.setEnabled(!radioPatronFormat.isSelected());
						comboBoxType.setEnabled(!radioPatronFormat.isSelected());
						textFieldPatronFormat.setEnabled(radioPatronFormat.isSelected());
					}
				});
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				commonAndType.add(radioPatronFormat, gridBagConstraints);
			}
			gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 0;
			gridBagConstraints.gridy = 0;
			gridBagConstraints.gridwidth = 3;
			gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
			gridBagConstraints.weightx = 0.5;
			gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
			mainPanel.add(commonAndType, gridBagConstraints);
		}
		{
			patronFormat = new JPanel();
			patronFormat.setBorder(BorderFactory.createTitledBorder("Patron Format"));
			patronFormat.setLayout(new java.awt.GridBagLayout());
			{
				labelFormat = new JLabel();
				labelFormat.setText("Format: ");
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.anchor = GridBagConstraints.LINE_START;
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				patronFormat.add(labelFormat, gridBagConstraints);
			}
			{
				textFieldPatronFormat = new JTextField();
				textFieldPatronFormat.setMinimumSize(new java.awt.Dimension(150, 20));
				textFieldPatronFormat.setPreferredSize(new java.awt.Dimension(150, 20));
				textFieldPatronFormat.setEnabled(false);
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.anchor = GridBagConstraints.BASELINE;
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				patronFormat.add(textFieldPatronFormat, gridBagConstraints);
			}
			gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridy = 0;
			gridBagConstraints.gridwidth = 3;
			gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
			gridBagConstraints.weightx = 0.5;
			gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
			mainPanel.add(patronFormat, gridBagConstraints);
		}
		{
			ownerAndType = new JPanel();
			ownerAndType.setBorder(BorderFactory.createTitledBorder("Owner and Type"));
			ownerAndType.setLayout(new java.awt.GridBagLayout());
			{
				labelOwner = new JLabel();
				labelOwner.setText("Owner:");
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.gridwidth = GridBagConstraints.RELATIVE;
				gridBagConstraints.anchor = GridBagConstraints.LINE_START;
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				ownerAndType.add(labelOwner, gridBagConstraints);
			}
			{
				comboBoxOwner = new JComboBox();
				comboBoxOwner.setModel(new DefaultComboBoxModel());
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
				gridBagConstraints.anchor = GridBagConstraints.LINE_START;
				gridBagConstraints.weightx = 0.1;
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				ownerAndType.add(comboBoxOwner, gridBagConstraints);
			}
			{
				labelType = new JLabel();
				labelType.setText("Type:");
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.gridx = 0;
				gridBagConstraints.gridy = 1;
				gridBagConstraints.gridwidth = GridBagConstraints.RELATIVE;
				gridBagConstraints.anchor = GridBagConstraints.LINE_START;
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				ownerAndType.add(labelType, gridBagConstraints);
			}
			{
				comboBoxType = new JComboBox();
				comboBoxType.setModel(new DefaultComboBoxModel());
				gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.gridx = 1;
				gridBagConstraints.gridy = 1;
				gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
				gridBagConstraints.anchor = GridBagConstraints.LINE_START;
				gridBagConstraints.weightx = 0.1;
				gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
				ownerAndType.add(comboBoxType, gridBagConstraints);
			}
			gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 0;
			gridBagConstraints.gridy = 1;
			gridBagConstraints.gridwidth = GridBagConstraints.REMAINDER;
			gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
			gridBagConstraints.insets = new java.awt.Insets(0, 5, 0, 5);
			mainPanel.add(ownerAndType, gridBagConstraints);
		}
		{
			buttonOk = new JButton();
			buttonOk.setText("Ok");
			buttonOk.setMaximumSize(new java.awt.Dimension(60, 25));
			buttonOk.setMinimumSize(new java.awt.Dimension(60, 25));
			buttonOk.setPreferredSize(new java.awt.Dimension(60, 25));
			buttonOk.addActionListener(this);
			gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 0;
			gridBagConstraints.gridy = 2;
			gridBagConstraints.gridwidth = 3;
			gridBagConstraints.anchor = GridBagConstraints.EAST;
			gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
			mainPanel.add(buttonOk, gridBagConstraints);
		}
		{
			buttonClose = new JButton();
			buttonClose.setText("Close");
			buttonClose.setMinimumSize(new java.awt.Dimension(60, 25));
			buttonClose.setPreferredSize(new java.awt.Dimension(60, 25));
			buttonClose.addActionListener(this);
			gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 3;
			gridBagConstraints.gridy = 2;
			gridBagConstraints.gridwidth = 3;
			gridBagConstraints.anchor = GridBagConstraints.WEST;
			gridBagConstraints.insets = new java.awt.Insets(5, 5, 5, 5);
			mainPanel.add(buttonClose, gridBagConstraints);
		}
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		getContentPane().add(mainPanel, gridBagConstraints);

		pack();
	}

	private void updateOwners() {
		DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxOwner.getModel();
		model.removeAllElements();
		ownerLookup.clear();
		for (Field field : CBEFFBiometricOrganizations.class.getDeclaredFields()) {
			if (Modifier.isStatic(field.getModifiers()) && Modifier.isPublic(field.getModifiers())) {
				//TODO: Check if is instance of short
				try {
					model.addElement(field.getName());
					ownerLookup.put(field.getName(), field.getShort(null));
				} catch (IllegalAccessException e) {
					e.printStackTrace();
				} catch (IllegalArgumentException e) {
					e.printStackTrace();
				}
			}
		}
	}

	private void updateTypes() {
		DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxType.getModel();
		model.removeAllElements();
		typesLookup.clear();
		for (Field field : CBEFFPatronFormatIdentifiers.class.getDeclaredFields()) {
			if (Modifier.isStatic(field.getModifiers()) && Modifier.isPublic(field.getModifiers())) {
				//TODO: Check if is instance of short
				try {
					model.addElement(field.getName());
					typesLookup.put(field.getName(), field.getShort(null));
				} catch (IllegalAccessException e) {
					e.printStackTrace();
				} catch (IllegalArgumentException e) {
					e.printStackTrace();
				}
			}
		}
	}

	public int getPatronFormat() {
		if (isOk) {
			if (radioPatronFormat.isSelected()) {
				return Integer.parseInt(textFieldPatronFormat.getText());
			} else if (radioOwnerAndType.isSelected()) {
				short owner = ownerLookup.get((String) comboBoxOwner.getSelectedItem());
				short type = typesLookup.get((String) comboBoxType.getSelectedItem());
				return BDIFTypes.makeFormat(owner, type);
			} else {
				throw new AssertionError("No possible patron format data type selected");
			}
		}
		return -1;
	}

	public boolean showDialog() {
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		setLocation(screenSize.width / 2 - getPreferredSize().width / 2, screenSize.height / 2 - getPreferredSize().height / 2);
		setVisible(true);
		return isOk;
	}

	// ==============================================
	// Event handling
	// ==============================================

	public final void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == buttonOk) {
			isOk = true;
			this.dispose();
		} else if (source == buttonClose) {
			isOk = false;
			this.dispose();
		}
	}
}
