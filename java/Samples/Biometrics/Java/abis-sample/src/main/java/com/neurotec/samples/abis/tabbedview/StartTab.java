package com.neurotec.samples.abis.tabbedview;

import com.neurotec.samples.abis.AbisController;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.Box.Filler;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

public final class StartTab extends Tab implements ActionListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private AbisController controller;

	private JButton btnAbout;
	private JButton btnNewSubject;
	private JButton btnOpenSubject;
	private JButton btnSettings;
	private JButton btnChangeDatabase;
	private Filler filler1;
	private Filler filler2;
	private JLabel lblLogo;
	private JLabel lblNewSubject;
	private JLabel lblOpenSubject;
	private JLabel lblSettings;
	private JLabel lblChangeDatabase;
	private JPanel panelAboutButton;
	private JPanel panelBottom;
	private JPanel panelCenter;
	private JPanel panelMenu;
	private JPanel panelMenuOuter;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public StartTab() {
		super("Start page");
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		GridBagConstraints gridBagConstraints;

		panelCenter = new JPanel();
		panelMenuOuter = new JPanel();
		panelMenu = new JPanel();
		btnNewSubject = new JButton();
		btnOpenSubject = new JButton();
		btnSettings = new JButton();
		btnChangeDatabase = new JButton();
		lblNewSubject = new JLabel();
		lblOpenSubject = new JLabel();
		lblSettings = new JLabel();
		lblChangeDatabase = new JLabel();
		panelBottom = new JPanel();
		lblLogo = new JLabel();
		panelAboutButton = new JPanel();
		btnAbout = new JButton();
		filler1 = new Filler(new Dimension(5, 0), new Dimension(5, 0), new Dimension(5, 32767));
		filler2 = new Filler(new Dimension(0, 5), new Dimension(0, 5), new Dimension(32767, 5));

		setLayout(new BorderLayout());

		panelCenter.setLayout(new BorderLayout());

		panelMenuOuter.setLayout(new FlowLayout(FlowLayout.LEFT));

		GridBagLayout panelMenuLayout = new GridBagLayout();
		panelMenuLayout.columnWidths = new int[] {0, 10, 0};
		panelMenuLayout.rowHeights = new int[] {0, 10, 0, 10, 0, 10, 0};
		panelMenu.setLayout(panelMenuLayout);

		btnNewSubject.setText("New Subject");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.fill = GridBagConstraints.BOTH;
		panelMenu.add(btnNewSubject, gridBagConstraints);

		btnOpenSubject.setText("Open Subject");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.fill = GridBagConstraints.BOTH;
		panelMenu.add(btnOpenSubject, gridBagConstraints);

		btnSettings.setText("Settings");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 4;
		gridBagConstraints.fill = GridBagConstraints.BOTH;
		panelMenu.add(btnSettings, gridBagConstraints);

		btnChangeDatabase.setText("Change Database");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 6;
		gridBagConstraints.fill = GridBagConstraints.BOTH;
		panelMenu.add(btnChangeDatabase, gridBagConstraints);

		lblNewSubject.setText("<html>Create new subject<br/>&emsp;Capture biometrics (fingers, faces, etc.) from devices or create them from files.<br/>&emsp;Enroll, identify or verify subject using local database or remote matching server.</html");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelMenu.add(lblNewSubject, gridBagConstraints);

		lblOpenSubject.setText("<html>Open subject template<br/>&emsp;Open a Neurotechnology template or other supported standard templates.<br/>&emsp;Enroll, identify or verify subject using local database or remote matching server.</html>");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelMenu.add(lblOpenSubject, gridBagConstraints);

		lblSettings.setText("<html>Change settings<br/>&emsp;Change feature detection, extraction, matching, etc. settings.<br/>&emsp;</html>");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 4;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelMenu.add(lblSettings, gridBagConstraints);

		lblChangeDatabase.setText("<html>Change database<br/>&emsp;Configure to use local database or remote matching server.<br/>&emsp;</html>");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 6;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelMenu.add(lblChangeDatabase, gridBagConstraints);

		panelMenuOuter.add(panelMenu);

		panelCenter.add(panelMenuOuter, BorderLayout.NORTH);

		add(panelCenter, BorderLayout.CENTER);

		panelBottom.setLayout(new BorderLayout());

		lblLogo.setText(" ");
		panelBottom.add(lblLogo, BorderLayout.EAST);

		panelAboutButton.setLayout(new BorderLayout());

		btnAbout.setText("About");
		btnAbout.setPreferredSize(new Dimension(117, 40));
		panelAboutButton.add(btnAbout, BorderLayout.EAST);
		panelAboutButton.add(filler1, BorderLayout.WEST);
		panelAboutButton.add(filler2, BorderLayout.SOUTH);

		panelBottom.add(panelAboutButton, BorderLayout.WEST);

		add(panelBottom, BorderLayout.SOUTH);

		btnNewSubject.addActionListener(this);
		btnOpenSubject.addActionListener(this);
		btnSettings.addActionListener(this);
		btnChangeDatabase.addActionListener(this);
		btnAbout.addActionListener(this);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setController(AbisController controller) {
		this.controller = controller;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource() == btnNewSubject) {
			controller.createNewSubject();
		} else if (ev.getSource() == btnOpenSubject) {
			controller.openSubject();
		} else if (ev.getSource() == btnSettings) {
			controller.settings();
		} else if (ev.getSource() == btnChangeDatabase) {
			controller.changeDatabase();
		} else if (ev.getSource() == btnAbout) {
			controller.about();
		} else {
			throw new AssertionError("Unknown event source: " + ev);
		}
	}

}
