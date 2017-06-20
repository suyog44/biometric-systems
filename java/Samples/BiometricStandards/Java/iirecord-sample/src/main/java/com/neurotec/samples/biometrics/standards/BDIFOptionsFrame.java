package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.util.NVersion;

import java.awt.Container;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JPanel;

public abstract class BDIFOptionsFrame extends JDialog implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JComboBox cbBiometricStandard;
	private JComboBox cbVersion;
	private JCheckBox chkNoStrictRead;
	private JCheckBox chkDoNotCheckCbeffProductId;
	private JButton btnOK;
	private JButton btnCancel;
	private JPanel commonPanel;
	private JPanel buttonPanel;

	// ==============================================
	// Private fields
	// ==============================================

	private BDIFOptionsFormMode mode = BDIFOptionsFormMode.NEW;
	private List<StandardVersion> standardVersions;

	// ==============================================
	// Protected fields
	// ==============================================

	protected boolean isOk;

	// ==============================================
	// Public constructor
	// ==============================================

	public BDIFOptionsFrame(Frame owner) {
		super(owner, "BDIFOptionsFrame", true);
		initGui();

		cbBiometricStandard.addItem(BDIFStandard.ANSI);
		cbBiometricStandard.addItem(BDIFStandard.ISO);
		cbBiometricStandard.setSelectedIndex(0);
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initGui() {
		this.setPreferredSize(new Dimension(310, 200));
		this.setResizable(false);
		Container contentPane = this.getContentPane();
		contentPane.setLayout(null);

		cbBiometricStandard = new JComboBox();
		cbBiometricStandard.addItemListener(new ItemListener() {
			@Override
			public void itemStateChanged(ItemEvent e) {
				onStandardChanged();
			}
		});
		cbVersion = new JComboBox();

		chkNoStrictRead = new JCheckBox("Non-strict read");
		chkDoNotCheckCbeffProductId = new JCheckBox("Do not check CBEFF product id");

		commonPanel = new JPanel();
		commonPanel.setPreferredSize(new Dimension(265, 110));
		commonPanel.setBorder(BorderFactory.createTitledBorder("Common"));

		GridBagLayout commonPanelLayout = new GridBagLayout();
		commonPanelLayout.columnWidths = new int[] { 120, 90 };
		commonPanelLayout.rowHeights = new int[] { 30, 25, 25 };
		commonPanel.setLayout(commonPanelLayout);

		GridBagConstraints c = new GridBagConstraints();
		c.fill = GridBagConstraints.HORIZONTAL;

		c.gridx = 0;
		c.gridy = 0;
		commonPanel.add(new JLabel("Biometric standard:"), c);

		c.gridx = 1;
		c.gridy = 0;
		commonPanel.add(cbBiometricStandard, c);

		c.gridx = 0;
		c.gridy = 1;
		commonPanel.add(new JLabel("Version:"), c);

		c.gridx = 1;
		c.gridy = 1;
		commonPanel.add(cbVersion, c);

		c.gridx = 0;
		c.gridy = 2;
		c.gridwidth = 2;
		commonPanel.add(chkNoStrictRead, c);

		c.gridx = 0;
		c.gridy = 3;
		commonPanel.add(chkDoNotCheckCbeffProductId, c);

		btnOK = new JButton("OK");
		btnOK.setPreferredSize(new Dimension(75, 25));
		btnOK.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setPreferredSize(new Dimension(75, 25));
		btnCancel.addActionListener(this);

		buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));
		buttonPanel.add(Box.createGlue());
		buttonPanel.add(btnOK);
		buttonPanel.add(btnCancel);

		contentPane.add(commonPanel);
		contentPane.add(buttonPanel);
		commonPanel.setBounds(15, 15, 300, 130);
		buttonPanel.setBounds(15, 115, 300, 25);
		this.pack();
	}

	private void onStandardChanged() {
		if (cbVersion == null || standardVersions == null) return;
		((DefaultComboBoxModel) cbVersion.getModel()).removeAllElements();
		for (StandardVersion version : standardVersions) {
			if (version.getStandard() == getStandard()) {
				cbVersion.addItem(version);
			}
		}
	}

	// ==============================================
	// Protected methods
	// ==============================================

	protected void onModeChanged() {
		setTitle(mode.name().toLowerCase());

		switch (mode) {
		case NEW:
		case SAVE:
		case CONVERT:
			chkNoStrictRead.setEnabled(false);
			break;
		default:
			chkNoStrictRead.setEnabled(true);
		}
	}

	protected List<StandardVersion> getStandardVersions() {
		return standardVersions;
	}

	protected void setStandardVersions(List<StandardVersion> standardVersions) {
		this.standardVersions = standardVersions;
		onStandardChanged();
	}

	// ==============================================
	// Public methods
	// ==============================================

	public final BDIFOptionsFormMode getMode() {
		return mode;
	}

	public final void setMode(BDIFOptionsFormMode mode) {
		if (this.mode != mode) {
			this.mode = mode;
			onModeChanged();
		}
	}

	public final BDIFStandard getStandard() {
		return (BDIFStandard) cbBiometricStandard.getSelectedItem();
	}

	public final void setStandard(BDIFStandard standard) {
		cbBiometricStandard.setSelectedItem(standard);
		onStandardChanged();
	}

	public final NVersion getVersion() {
		return ((StandardVersion)cbVersion.getSelectedItem()).getVersion();
	}

	public final void setVersion(NVersion version) {
		StandardVersion value = null;
		for (StandardVersion standardVersion : getStandardVersions()) {
			if (standardVersion.getStandard() == getStandard() && standardVersion.getVersion().getValue() == version.getValue()) {
				value = standardVersion;
				break;
			}
		}
		if (value == null) throw new IllegalArgumentException("Version is invalid");
		cbVersion.setSelectedItem(value);
	}

	public int getFlags() {
		int flags = 0;
		if (chkDoNotCheckCbeffProductId.isSelected()) {
			flags |= BDIFTypes.FLAG_DO_NOT_CHECK_CBEFF_PRODUCT_ID;
		}
		if (chkNoStrictRead.isSelected()) {
			flags |= BDIFTypes.FLAG_NON_STRICT_READ;
		}
		return flags;
	}

	public void setFlags(int flags) {
		if ((flags & BDIFTypes.FLAG_DO_NOT_CHECK_CBEFF_PRODUCT_ID) == BDIFTypes.FLAG_DO_NOT_CHECK_CBEFF_PRODUCT_ID) {
			chkDoNotCheckCbeffProductId.setSelected(true);
		}
		if ((flags & BDIFTypes.FLAG_NON_STRICT_READ) == BDIFTypes.FLAG_NON_STRICT_READ) {
			chkNoStrictRead.setSelected(true);
		}
	}

	public final JPanel getButtonPanel() {
		return buttonPanel;
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
		if (source == btnOK) {
			isOk = true;
			this.dispose();
		} else if (source == btnCancel) {
			isOk = false;
			this.dispose();
		}
	}

}
