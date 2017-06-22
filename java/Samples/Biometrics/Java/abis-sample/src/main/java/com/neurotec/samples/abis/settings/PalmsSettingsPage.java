package com.neurotec.samples.abis.settings;

import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NPalmScanner;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.tabbedview.ConnectToDevicePanel;
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
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;

import javax.swing.ComboBoxModel;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;

public final class PalmsSettingsPage extends SettingsPage implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JComboBox comboBoxScanner;
	private JButton btnConnect;
	private JButton btnDisconnect;
	private JComboBox comboBoxTemplateSize;
	private JComboBox comboBoxMatchingSpeed;
	private JCheckBox cbReturnBinarizedImage;
	private JLabel lblTemplateSize;
	private JLabel lblScanner;
	private JLabel lblMatchingSpeed;
	private JLabel lblMaximalRotation;
	private JLabel lblQualityThreshold;
	private JLabel lblGeneralizationRecordCount;
	private JSpinner spinnerQualityThreshold;
	private JSpinner spinnerMaximalRotation;
	private JSpinner spinnerGeneralizationRecordCount;

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

	public PalmsSettingsPage(NBiometricClient client, PageNavigationController pageController) {
		super("Palms", pageController, client);
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
			thisLayout.rowWeights = new double[] { 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 1.0 };
			thisLayout.rowHeights = new int[] { 0, 0, 0, 0, 0, 0, 0, 10 };
			thisLayout.columnWeights = new double[] { 0.5, 0.5, 0.5, 1.0 };
			thisLayout.columnWidths = new int[] { 10, 10, 10, 10 };
			this.setLayout(thisLayout);
			{
				lblScanner = new JLabel();
				this.add(lblScanner, new GridBagConstraints(0, 0, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
				lblScanner.setText("Palm scanner:");
			}
			{
				DefaultComboBoxModel model = new DefaultComboBoxModel();
				comboBoxScanner = new JComboBox(model);
				comboBoxScanner.setPreferredSize(new Dimension(150, 20));
				this.add(comboBoxScanner, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
				comboBoxScanner.addItemListener(new ItemListener() {

					@Override
					public void itemStateChanged(ItemEvent ev) {
						if (ev.getStateChange() == ItemEvent.SELECTED) {
							btnDisconnect.setEnabled((client.getFingerScanner() != null) && client.getFingerScanner().isDisconnectable());
						}
					}
				});

				btnConnect = new JButton("Connect to...");
				this.add(btnConnect, new GridBagConstraints(2, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(0, 0, 0, 0), 0, 0));
				btnConnect.addActionListener(this);
				btnDisconnect = new JButton("Disconnect");
				btnDisconnect.setEnabled(false);
				this.add(btnDisconnect, new GridBagConstraints(3, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(0, 0, 0, 0), 0, 0));
				btnDisconnect.addActionListener(this);
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
				lblMaximalRotation = new JLabel("Maximal Rotation:");
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
				lblGeneralizationRecordCount = new JLabel("Generalization record count:");
				this.add(lblGeneralizationRecordCount , new GridBagConstraints(0, 5, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				SpinnerNumberModel model = new SpinnerNumberModel(3, 3, 100, 1);
				spinnerGeneralizationRecordCount = new JSpinner(model);
				((JSpinner.DefaultEditor) spinnerGeneralizationRecordCount.getEditor()).getTextField().setEditable(false);
				spinnerGeneralizationRecordCount.setPreferredSize(new Dimension(100, 20));
				this.add(spinnerGeneralizationRecordCount, new GridBagConstraints(1, 5, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbReturnBinarizedImage = new JCheckBox("Return binarized image");
				this.add(cbReturnBinarizedImage, new GridBagConstraints(1, 6, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private NPalmScanner getScanner() {
		if (comboBoxScanner.getSelectedIndex() == -1) {
			return null;
		} else {
			return (NPalmScanner) comboBoxScanner.getSelectedItem();
		}
	}

	private void setScanner(NPalmScanner value) {
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

	private double getMaximalRotation() {
		return (Double) spinnerMaximalRotation.getValue();
	}

	private void setMaximalRotation(double value) {
		spinnerMaximalRotation.setValue(value);
	}

	private void setReturnBinarizedImage(boolean value) {
		cbReturnBinarizedImage.setSelected(value);
	}

	private boolean isReturnBinarizedImage() {
		return cbReturnBinarizedImage.isSelected();
	}

	private int getQualityThreshold() {
		return (Integer) spinnerQualityThreshold.getValue();
	}

	private void setQualityThreshold(int value) {
		spinnerQualityThreshold.setValue(value);
	}

	private int getGeneralizationRecordCount() {
		return (Integer) spinnerGeneralizationRecordCount.getValue();
	}

	private void setGeneralizationRecordCount(int value) {
		spinnerGeneralizationRecordCount.setValue(value);
	}

	private void listDevices() {
		try {
			Object selected = client.getPalmScanner();
			DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxScanner.getModel();
			model.removeAllElements();
			for (NDevice item : client.getDeviceManager().getDevices()) {
				if ((item.getDeviceType().contains(NDeviceType.PALM_SCANNER))) {
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
			client.setPalmScanner(getScanner());
			client.setPalmsTemplateSize(getTemplateSize());
			client.setPalmsMatchingSpeed(getMatchingSpeed());
			client.setPalmsMaximalRotation((float) getMaximalRotation());
			client.setPalmsQualityThreshold((byte) getQualityThreshold());
			client.setPalmsReturnBinarizedImage(isReturnBinarizedImage());
			SettingsManager.setPalmsGeneralizationRecordCount(getGeneralizationRecordCount());
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void loadSettings() {
		listDevices();
		setScanner((NPalmScanner) client.getPalmScanner());
		setTemplateSize(client.getPalmsTemplateSize());
		setMatchingSpeed(client.getPalmsMatchingSpeed());
		setMaximalRotation(client.getPalmsMaximalRotation());
		setQualityThreshold(client.getPalmsQualityThreshold());
		setReturnBinarizedImage(client.isPalmsReturnBinarizedImage());
		setGeneralizationRecordCount(SettingsManager.getPalmsGeneralizationRecordCount());
		repaint();
	}

	@Override
	public void defaultSettings() {
		if (comboBoxScanner.getModel().getSize() > 0) {
			comboBoxScanner.setSelectedIndex(0);
		}
		defaultClientProperties.getPalms().applyTo(client);
		SettingsManager.setPalmsGeneralizationRecordCount(3);
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

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource().equals(btnConnect)) {
				JDialog dialog = new JDialog();
				dialog.setModal(true);
				ConnectToDevicePanel connectPanel = new ConnectToDevicePanel(dialog);
				dialog.add(connectPanel);
				dialog.setSize(new Dimension(500, 500));
				dialog.setLocationRelativeTo(this);
				dialog.setVisible(true);
				if (connectPanel.isResultOk()) {
					NDevice newDevice = null;
					try {
						newDevice = client.getDeviceManager().connectToDevice(connectPanel.getSelectedPlugin(), connectPanel.getParameters());
						listDevices();
						comboBoxScanner.setSelectedItem(newDevice);
						if (comboBoxScanner.getSelectedItem() != newDevice) {
							if (newDevice != null) {
								client.getDeviceManager().disconnectFromDevice(newDevice);
							}
							MessageUtils.showError(this, "Error", "Failed to create connection to device using specified connection details");
						}
					} catch (Exception e) {
						if (newDevice != null) {
							client.getDeviceManager().disconnectFromDevice(newDevice);
						}
						throw e;
					}
				}
			} else if (ev.getSource().equals(btnDisconnect)) {
				NDevice device = (NDevice) comboBoxScanner.getSelectedItem();
				if (device != null) {
					client.getDeviceManager().disconnectFromDevice(device);
				}
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

}
