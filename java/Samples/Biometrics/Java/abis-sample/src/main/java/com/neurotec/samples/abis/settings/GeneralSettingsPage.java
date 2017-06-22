package com.neurotec.samples.abis.settings;

import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;

import javax.swing.DefaultComboBoxModel;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;

import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.BiometricUtils;
import com.neurotec.samples.abis.util.MessageUtils;

public final class GeneralSettingsPage extends SettingsPage {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JComboBox comboBoxMatchingThreshold;
	private JCheckBox cbFirstResult;
	private JCheckBox cbMatchWithDetails;
	private JLabel lblMatchingThreshold;
	private JLabel lblMaximalResultsCount;
	private JSpinner spinnerMaximalResultsCount;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public GeneralSettingsPage(NBiometricClient client, PageNavigationController pageController) {
		super("General", pageController, client);
		initGUI();
		comboBoxMatchingThreshold.addItem(BiometricUtils.getMatchingThresholdToString(12));
		comboBoxMatchingThreshold.addItem(BiometricUtils.getMatchingThresholdToString(24));
		comboBoxMatchingThreshold.addItem(BiometricUtils.getMatchingThresholdToString(36));
		comboBoxMatchingThreshold.addItem(BiometricUtils.getMatchingThresholdToString(48));
		comboBoxMatchingThreshold.addItem(BiometricUtils.getMatchingThresholdToString(60));
		comboBoxMatchingThreshold.addItem(BiometricUtils.getMatchingThresholdToString(72));
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		try {
			GridBagLayout thisLayout = new GridBagLayout();
			thisLayout.rowWeights = new double[] { 0.1, 0.1, 0.1, 0.1, 1.0 };
			thisLayout.rowHeights = new int[] { 5, 5, 5, 5, 7 };
			thisLayout.columnWeights = new double[] { 0.1, 0.5 };
			thisLayout.columnWidths = new int[] { 7, 7 };
			this.setLayout(thisLayout);
			{
				lblMatchingThreshold = new JLabel();
				this.add(lblMatchingThreshold, new GridBagConstraints(0, 0, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
				lblMatchingThreshold.setText("Matching threshold:");
			}
			{
				DefaultComboBoxModel model = new DefaultComboBoxModel();
				comboBoxMatchingThreshold = new JComboBox(model);
				comboBoxMatchingThreshold.setPreferredSize(new Dimension(150, 20));
				this.add(comboBoxMatchingThreshold, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				lblMaximalResultsCount = new JLabel("Maximal results count:");
				this.add(lblMaximalResultsCount, new GridBagConstraints(0, 1, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				SpinnerNumberModel model = new SpinnerNumberModel(0, 0, 50000, 1);
				spinnerMaximalResultsCount = new JSpinner(model);
				spinnerMaximalResultsCount.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerMaximalResultsCount.getEditor()).getTextField().setEditable(false);
				this.add(spinnerMaximalResultsCount, new GridBagConstraints(1, 1, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbMatchWithDetails = new JCheckBox("Return matching details");
				this.add(cbMatchWithDetails, new GridBagConstraints(1, 2, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbFirstResult = new JCheckBox("First result only");
				this.add(cbFirstResult, new GridBagConstraints(1, 3, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private int getMachingThreshold() {
		return BiometricUtils.getMatchingThresholdFromString((String)comboBoxMatchingThreshold.getSelectedItem());
	}

	private void setMatchingThreshold(int value) {
		comboBoxMatchingThreshold.setSelectedItem(BiometricUtils.getMatchingThresholdToString(value));
	}


	private int getMaximalResultsCount() {
		return (Integer) spinnerMaximalResultsCount.getValue();
	}

	private void setMaximalResultsCount(int value) {
		spinnerMaximalResultsCount.setValue(value);
	}

	private boolean isMatchWithDetails() {
		return cbMatchWithDetails.isSelected();
	}

	private void setMatchWithDetails(boolean value) {
		cbMatchWithDetails.setSelected(value);
	}

	private boolean isFirstResult() {
		return cbFirstResult.isSelected();
	}

	private void setFirstResult(boolean value) {
		cbFirstResult.setSelected(value);
	}


	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void saveSettings() {
		try {
			client.setMatchingThreshold(getMachingThreshold());
			client.setMatchingMaximalResultCount(getMaximalResultsCount());
			client.setMatchingWithDetails(isMatchWithDetails());
			client.setMatchingFirstResultOnly(isFirstResult());
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void loadSettings() {
		setMatchingThreshold(client.getMatchingThreshold());
		setMaximalResultsCount(client.getMatchingMaximalResultCount());
		setMatchWithDetails(client.isMatchingWithDetails());
		setFirstResult(client.isMatchingFirstResultOnly());
		repaint();
	}

	@Override
	public void defaultSettings() {
		defaultClientProperties.getGeneral().applyTo(client);
		super.defaultSettings();
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		// Do nothing.
	}

	@Override
	public void navigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		// Do nothing.
	}
}
