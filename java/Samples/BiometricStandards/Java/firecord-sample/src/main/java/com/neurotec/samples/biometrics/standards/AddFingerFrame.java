package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFFPPosition;

import java.awt.Container;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JPanel;

public final class AddFingerFrame extends JDialog implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private GridBagLayout mainPanelLayout;
	private JComboBox cmbPositions;
	private JPanel mainPanel;
	private JPanel buttonPanel;
	private JButton buttonOK;
	private JButton buttonCancel;
	private boolean isOK;

	// ==============================================
	// Public constructor
	// ==============================================

	public AddFingerFrame(Frame owner) {
		super(owner, "AddFingerFrame", true);

		this.setPreferredSize(new Dimension(275, 100));
		this.setResizable(false);

		initGUI();

		BDIFFPPosition[] positions = BDIFFPPosition.values();
		for (BDIFFPPosition position : positions) {
			cmbPositions.addItem(position.name());
		}
		cmbPositions.setSelectedIndex(0);
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initGUI() {
		Container contentPane = this.getContentPane();
		contentPane.setLayout(null);

		cmbPositions = new JComboBox();
		cmbPositions.setPreferredSize(new Dimension(150, 21));

		buttonOK = new JButton("OK");
		buttonOK.addActionListener(this);

		buttonCancel = new JButton("Cancel");
		buttonCancel.addActionListener(this);

		mainPanel = new JPanel();
		mainPanelLayout = new GridBagLayout();
		mainPanelLayout.columnWidths = new int[] {100, 50, 100};
		mainPanel.setLayout(mainPanelLayout);

		GridBagConstraints c = new GridBagConstraints();
		c.fill = GridBagConstraints.HORIZONTAL;

		c.gridx = 0;
		c.gridy = 0;
		mainPanel.add(new JLabel("Finger position:"), c);

		c.gridx = 1;
		c.gridy = 0;
		c.gridwidth = 2;
		mainPanel.add(cmbPositions, c);

		buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));
		buttonPanel.add(Box.createGlue());
		buttonPanel.add(buttonOK);
		buttonPanel.add(Box.createHorizontalStrut(5));
		buttonPanel.add(buttonCancel);

		contentPane.add(mainPanel);
		contentPane.add(buttonPanel);
		mainPanel.setBounds(5, 9, 260, 25);
		buttonPanel.setBounds(0, 35, 265, 25);
		this.pack();

	}

	// ==============================================
	// Public methods
	// ==============================================

	public final BDIFFPPosition getFingerPosition() {
		return BDIFFPPosition.valueOf((String) cmbPositions.getSelectedItem());
	}

	public final void setFingerPosition(BDIFFPPosition position) {
		cmbPositions.setSelectedItem(position.name());
	}

	public final JPanel getMainPanel() {
		return mainPanel;
	}

	public final JPanel getButtonPanel() {
		return buttonPanel;
	}

	public final GridBagLayout getMainPanelLayout() {
		return mainPanelLayout;
	}

	public boolean showDialog() {
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		setLocation(screenSize.width / 2 - getPreferredSize().width / 2, screenSize.height / 2 - getPreferredSize().height / 2);
		setVisible(true);
		return isOK;
	}

	@Override
	public final void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == buttonOK) {
			isOK = true;
			dispose();
		} else if (source == buttonCancel) {
			isOK = false;
			dispose();
		}
	}
}
