package com.neurotec.samples;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Font;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.JLabel;
import javax.swing.JPanel;

public final class LicensingPanel extends JPanel {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final String REQUIRED_COMPONENT_LICENSES_LABEL_TEXT = "Required component licenses: ";
	private static final String COMPONENTS_OBTAINED_STATUS_TEXT = "Component licenses successfuly obtained";
	private static final String COMPONENTS_NOT_OBTAINED_STATUS_TEXT = "Component licenses not obtained";

	private static final Color COMPONENTS_OBTAINED_STATUS_TEXT_COLOR = Color.green.darker();
	private static final Color COMPONENTS_NOT_OBTAINED_STATUS_TEXT_COLOR = Color.red.darker();

	private static final int BORDER_WIDTH_TOP = 5;
	private static final int BORDER_WIDTH_LEFT = 5;
	private static final int BORDER_WIDTH_BOTTOM = 5;
	private static final int BORDER_WIDTH_RIGHT = 5;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JLabel lblRequiredComponentLicenses;
	private JLabel lblRequiredComponentLicensesList;
	private JLabel lblStatus;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public LicensingPanel(List<String> licenses) {
		init();
		setRequiredComponentLicensesList(licenses);
	}

	public LicensingPanel() {
		init();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void init() {
		setBorder(BorderFactory.createLineBorder(Color.BLACK));
		setLayout(new BorderLayout());

		{
			lblRequiredComponentLicenses = new JLabel(REQUIRED_COMPONENT_LICENSES_LABEL_TEXT);
			lblRequiredComponentLicenses.setFont(new Font(lblRequiredComponentLicenses.getFont().getName(), Font.BOLD, 11));
			lblRequiredComponentLicenses.setBorder(BorderFactory.createEmptyBorder(BORDER_WIDTH_TOP, BORDER_WIDTH_LEFT, BORDER_WIDTH_BOTTOM, BORDER_WIDTH_RIGHT));
			this.add(lblRequiredComponentLicenses, BorderLayout.LINE_START);
		}
		{
			lblRequiredComponentLicensesList = new JLabel();
			lblRequiredComponentLicensesList.setFont(new Font(lblRequiredComponentLicensesList.getFont().getName(), Font.PLAIN, 11));
			lblRequiredComponentLicensesList.setBorder(BorderFactory.createEmptyBorder(BORDER_WIDTH_TOP, BORDER_WIDTH_LEFT, BORDER_WIDTH_BOTTOM, BORDER_WIDTH_RIGHT));
			this.add(lblRequiredComponentLicensesList, BorderLayout.CENTER);
		}
		{
			lblStatus = new JLabel();
			lblStatus.setFont(new Font(lblStatus.getFont().getName(), Font.PLAIN, 11));
			lblStatus.setBorder(BorderFactory.createEmptyBorder(BORDER_WIDTH_TOP, BORDER_WIDTH_LEFT, BORDER_WIDTH_BOTTOM, BORDER_WIDTH_RIGHT));
			setComponentObtainingStatus(false);
			this.add(lblStatus, BorderLayout.PAGE_END);
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setRequiredComponentLicensesList(List<String> licenses) {
		lblRequiredComponentLicensesList.setText(licenses.toString().replace("[", "").replace("]", "").replace(" ", ""));
	}

	public void setComponentObtainingStatus(boolean succeeded) {
		if (succeeded) {
			lblStatus.setText(COMPONENTS_OBTAINED_STATUS_TEXT);
			lblStatus.setForeground(COMPONENTS_OBTAINED_STATUS_TEXT_COLOR);
		} else {
			lblStatus.setText(COMPONENTS_NOT_OBTAINED_STATUS_TEXT);
			lblStatus.setForeground(COMPONENTS_NOT_OBTAINED_STATUS_TEXT_COLOR);
		}
	}
}
