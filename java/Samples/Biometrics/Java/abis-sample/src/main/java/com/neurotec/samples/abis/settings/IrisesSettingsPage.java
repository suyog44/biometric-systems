package com.neurotec.samples.abis.settings;

import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;

import javax.swing.ComboBoxModel;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;

public final class IrisesSettingsPage extends SettingsPage {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JComboBox comboBoxScanner;
	private JComboBox comboBoxTemplateSize;
	private JComboBox comboBoxMatchingSpeed;
	private JCheckBox cbFastExtraction;
	private JLabel lblTemplateSize;
	private JLabel lblScanner;
	private JLabel lblMatchingSpeed;
	private JLabel lblMaximalRotation;
	private JLabel lblQualityThreshold;
	private JSpinner spinnerQualityThreshold;
	private JSpinner spinnerMaximalRotation;

	private final NCollectionChangeListener devicesCollectionChanged = new NCollectionChangeListener() {
		@Override
		public void collectionChanged(NCollectionChangeEvent event) {
			SwingUtils.runOnEDT(new Runnable() {

				@Override
				public void run() {
					listDevices();
				}
			});
		}
	};

	// ===========================================================
	// Public constructor
	// ===========================================================

	public IrisesSettingsPage(NBiometricClient client, PageNavigationController pageController) {
		super("Irises", pageController, client);
		initGUI();
		comboBoxMatchingSpeed.addItem(NMatchingSpeed.HIGH);
		comboBoxMatchingSpeed.addItem(NMatchingSpeed.MEDIUM);
		comboBoxMatchingSpeed.addItem(NMatchingSpeed.LOW);

		comboBoxTemplateSize.addItem(NTemplateSize.LARGE);
		comboBoxTemplateSize.addItem(NTemplateSize.MEDIUM);
		comboBoxTemplateSize.addItem(NTemplateSize.SMALL);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		try {
			GridBagLayout thisLayout = new GridBagLayout();
			thisLayout.rowWeights = new double[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 1.0 };
			thisLayout.rowHeights = new int[] { 5, 5, 5, 5, 5, 5, 7 };
			thisLayout.columnWeights = new double[] { 0.1, 0.5 };
			thisLayout.columnWidths = new int[] { 7, 7 };
			this.setLayout(thisLayout);
			{
				lblScanner = new JLabel();
				this.add(lblScanner, new GridBagConstraints(0, 0, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
				lblScanner.setText("Iris scanner:");
			}
			{
				DefaultComboBoxModel model = new DefaultComboBoxModel();
				comboBoxScanner = new JComboBox(model);
				comboBoxScanner.setPreferredSize(new Dimension(150, 20));
				this.add(comboBoxScanner, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				lblTemplateSize = new JLabel();
				this.add(lblTemplateSize, new GridBagConstraints(0, 1, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
				lblTemplateSize.setText("Template size:");
			}
			{
				DefaultComboBoxModel model = new DefaultComboBoxModel();
				comboBoxTemplateSize = new JComboBox(model);
				comboBoxTemplateSize.setPreferredSize(new Dimension(150, 20));
				this.add(comboBoxTemplateSize, new GridBagConstraints(1, 1, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				lblMatchingSpeed = new JLabel();
				this.add(lblMatchingSpeed, new GridBagConstraints(0, 2, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
				lblMatchingSpeed.setText("Matching speed:");
			}
			{
				ComboBoxModel model = new DefaultComboBoxModel();
				comboBoxMatchingSpeed = new JComboBox(model);
				comboBoxMatchingSpeed.setPreferredSize(new Dimension(150, 20));
				this.add(comboBoxMatchingSpeed, new GridBagConstraints(1, 2, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				lblMaximalRotation = new JLabel("Maximal rotation:");
				this.add(lblMaximalRotation, new GridBagConstraints(0, 3, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				SpinnerNumberModel model = new SpinnerNumberModel(0.0, 0, 180, 1);
				spinnerMaximalRotation = new JSpinner(model);
				spinnerMaximalRotation.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerMaximalRotation.getEditor()).getTextField().setEditable(false);
				this.add(spinnerMaximalRotation, new GridBagConstraints(1, 3, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				lblQualityThreshold = new JLabel("Quality threshold:");
				this.add(lblQualityThreshold , new GridBagConstraints(0, 4, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				SpinnerNumberModel model = new SpinnerNumberModel(0, 0, 100, 1);
				spinnerQualityThreshold = new JSpinner(model);
				((JSpinner.DefaultEditor) spinnerQualityThreshold.getEditor()).getTextField().setEditable(false);
				spinnerQualityThreshold.setPreferredSize(new Dimension(100, 20));
				this.add(spinnerQualityThreshold, new GridBagConstraints(1, 4, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbFastExtraction = new JCheckBox("Fast extraction");
				this.add(cbFastExtraction, new GridBagConstraints(1, 5, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private NIrisScanner getScanner() {
		if (comboBoxScanner.getSelectedIndex() == -1) {
			return null;
		} else {
			return (NIrisScanner) comboBoxScanner.getSelectedItem();
		}
	}

	private void setScanner(NIrisScanner value) {
		comboBoxScanner.setSelectedItem(value);
	}

	private NTemplateSize getTemplateSize() {
		return (NTemplateSize) comboBoxTemplateSize.getSelectedItem();
	}

	private void setTemplateSize(NTemplateSize value) {
		comboBoxTemplateSize.setSelectedItem(value);
	}

	private NMatchingSpeed getMatchingSpeed() {
		return (NMatchingSpeed) comboBoxMatchingSpeed.getSelectedItem();
	}

	private void setMatchingSpeed(NMatchingSpeed value) {
		comboBoxMatchingSpeed.setSelectedItem(value);
	}

	private void setFastExtraction(boolean value) {
		cbFastExtraction.setSelected(value);
	}

	private boolean isFastExtraction() {
		return cbFastExtraction.isSelected();
	}

	private double getMaximalRotation() {
		return (Double) spinnerMaximalRotation.getValue();
	}

	private void setMaximalRotation(double value) {
		spinnerMaximalRotation.setValue(value);
	}

	private int getQualityThreshold() {
		return (Integer) spinnerQualityThreshold.getValue();
	}

	private void setQualityThreshold(int value) {
		spinnerQualityThreshold.setValue(value);
	}

	private void listDevices() {
		try {
			Object selected = client.getIrisScanner();
			DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxScanner.getModel();
			model.removeAllElements();
			for (NDevice item : client.getDeviceManager().getDevices()) {
				if ((item.getDeviceType().contains(NDeviceType.IRIS_SCANNER))) {
					model.addElement(item);
				}
			}
			comboBoxScanner.setSelectedItem(selected);
			if (comboBoxScanner.getSelectedIndex() == -1 && model.getSize() > 0) {
				comboBoxScanner.setSelectedIndex(0);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void saveSettings() {
		try {
			client.setIrisScanner(getScanner());
			client.setIrisesTemplateSize(getTemplateSize());
			client.setIrisesMatchingSpeed(getMatchingSpeed());
			client.setIrisesMaximalRotation((float) getMaximalRotation());
			client.setIrisesQualityThreshold((byte) getQualityThreshold());
			client.setIrisesFastExtraction(isFastExtraction());
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void loadSettings() {
		listDevices();
		setScanner(client.getIrisScanner());
		setTemplateSize(client.getIrisesTemplateSize());
		setMatchingSpeed(client.getIrisesMatchingSpeed());
		setMaximalRotation(client.getIrisesMaximalRotation());
		setQualityThreshold(client.getIrisesQualityThreshold());
		setFastExtraction(client.isIrisesFastExtraction());
		repaint();
	}

	@Override
	public void defaultSettings() {
		if (comboBoxScanner.getModel().getSize() > 0) {
			comboBoxScanner.setSelectedIndex(0);
		}
		defaultClientProperties.getIrises().applyTo(client);
		super.defaultSettings();
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		client.getDeviceManager().getDevices().addCollectionChangeListener(devicesCollectionChanged);
	}

	@Override
	public void navigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		client.getDeviceManager().getDevices().removeCollectionChangeListener(devicesCollectionChanged);
	}

}
