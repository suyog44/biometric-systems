package com.neurotec.samples.abis.settings;

import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NCaptureDevice;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NMicrophone;
import com.neurotec.media.NMediaFormat;
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
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;

import javax.swing.DefaultComboBoxModel;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JLabel;

public final class VoicesSettingsPage extends SettingsPage {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JComboBox comboBoxMicrophone;
	private JComboBox comboBoxFormat;
	private JCheckBox cbUniquePhrasesOnly;
	private JCheckBox cbTextDependentFeatures;
	private JCheckBox cbTextIndependentFeatures;
	private JLabel lblMicrophone;
	private JLabel lblFormat;
	private JLabel lblUniquePhraseDescription;

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

	public VoicesSettingsPage(NBiometricClient client, PageNavigationController pageController) {
		super("Voices", pageController, client);
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		try {
			GridBagLayout thisLayout = new GridBagLayout();
			thisLayout.rowWeights = new double[] {0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 1.0};
			thisLayout.rowHeights = new int[] {5, 5, 5, 5, 5, 5, 100};
			thisLayout.columnWeights = new double[] {0.1, 1.0};
			thisLayout.columnWidths = new int[] {7, 7};
			this.setLayout(thisLayout);
			Insets insets = new Insets(5, 10, 5, 10);
			{
				lblMicrophone = new JLabel("Microphone:");
				GridBagConstraints gbc_lblCamera = new GridBagConstraints();
				gbc_lblCamera.anchor = GridBagConstraints.EAST;
				gbc_lblCamera.insets = new Insets(0, 0, 5, 5);
				gbc_lblCamera.gridx = 0;
				gbc_lblCamera.gridy = 0;
				add(lblMicrophone, gbc_lblCamera);

				comboBoxMicrophone = new JComboBox(new DefaultComboBoxModel());
				comboBoxMicrophone.setPreferredSize(new Dimension(200, 20));
				this.add(comboBoxMicrophone, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxMicrophone.addItemListener(new ItemListener() {
					@Override
					public void itemStateChanged(ItemEvent event) {
						if (event.getStateChange() == ItemEvent.SELECTED) {
							client.setVoiceCaptureDevice((NMicrophone) comboBoxMicrophone.getSelectedItem());
							listVideoFormats();
						}
					}
				});
			}
			{
				lblFormat = new JLabel("Format:");
				GridBagConstraints gbc_lblFormat = new GridBagConstraints();
				gbc_lblFormat.anchor = GridBagConstraints.EAST;
				gbc_lblFormat.insets = new Insets(0, 0, 5, 5);
				gbc_lblFormat.gridx = 0;
				gbc_lblFormat.gridy = 1;
				add(lblFormat, gbc_lblFormat);

				comboBoxFormat = new JComboBox(new DefaultComboBoxModel());
				comboBoxFormat.setPreferredSize(new Dimension(200, 20));
				this.add(comboBoxFormat, new GridBagConstraints(1, 1, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxFormat.addItemListener(new ItemListener() {
					@Override
					public void itemStateChanged(ItemEvent event) {
						if (event.getStateChange() == ItemEvent.SELECTED) {
							NMediaFormat format = (NMediaFormat) comboBoxFormat.getSelectedItem();
							if (format != null) {
								client.getVoiceCaptureDevice().setCurrentFormat(format);
							}
						}
					}
				});
			}
			{
				cbUniquePhrasesOnly = new JCheckBox();
				cbUniquePhrasesOnly.setText("Unique phrases only");
				this.add(cbUniquePhrasesOnly, new GridBagConstraints(1, 2, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblUniquePhraseDescription = new JLabel("<html>Specifies whether each user says a unique phrase.<br/>Unchecking this option allows to use the same phrase for different users but false rejection <br/>rate (FRR) increases, thus it is recommended to lower matcher matching threshold<br/>(this parameter can be found in Matching-&gt;General parameters tab).</html>");
				this.add(lblUniquePhraseDescription, new GridBagConstraints(1, 3, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(0, 0, 5, 0), 0, 0));
			}
			{
				cbTextDependentFeatures = new JCheckBox("Extract text dependent features");
				this.add(cbTextDependentFeatures, new GridBagConstraints(1, 4, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbTextIndependentFeatures = new JCheckBox("Extract text independent features");
				this.add(cbTextIndependentFeatures, new GridBagConstraints(1, 5, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private NMicrophone getMicrophone() {
		if (comboBoxMicrophone.getSelectedIndex() == -1) {
			return null;
		} else {
			return (NMicrophone) comboBoxMicrophone.getSelectedItem();
		}
	}

	private void setMicrophone(NMicrophone value) {
		comboBoxMicrophone.setSelectedItem(value);
	}

	private boolean isExtractTextIndependent() {
		return cbTextIndependentFeatures.isSelected();
	}

	private void setExtractTextIndependent(boolean value) {
		this.cbTextIndependentFeatures.setSelected(value);
	}

	private boolean isUniquePhrasesOnly() {
		return cbUniquePhrasesOnly.isSelected();
	}

	private void setUniquePhrasesOnly(boolean value) {
		this.cbUniquePhrasesOnly.setSelected(value);
	}

	private boolean isExtractTextDependent() {
		return cbTextDependentFeatures.isSelected();
	}

	private void setExtractTextDependent(boolean value) {
		this.cbTextDependentFeatures.setSelected(value);
	}

	private void listDevices() {
		try {
			Object selected = client.getVoiceCaptureDevice();
			DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxMicrophone.getModel();
			model.removeAllElements();
			for (NDevice item : client.getDeviceManager().getDevices()) {
				if ((item.getDeviceType().contains(NDeviceType.MICROPHONE))) {
					model.addElement(item);
				}
			}
			comboBoxMicrophone.setSelectedItem(selected);
			if (comboBoxMicrophone.getSelectedIndex() == -1 && model.getSize() > 0) {
				comboBoxMicrophone.setSelectedIndex(0);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private void listVideoFormats() {
		DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxFormat.getModel();
		NCaptureDevice device = (NCaptureDevice) comboBoxMicrophone.getSelectedItem();
		if (device != null) {
			for (NMediaFormat item : device.getFormats()) {
				model.addElement(item);
			}
			NMediaFormat current = device.getCurrentFormat();
			if (current != null) {
				int index = model.getIndexOf(current);
				if (index != -1) {
					comboBoxFormat.setSelectedIndex(index);
				} else {
					model.insertElementAt(current, 0);
					comboBoxFormat.setSelectedIndex(0);
				}
			}
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void saveSettings() {
		try {
			client.setVoiceCaptureDevice(getMicrophone());
			client.setVoicesUniquePhrasesOnly(isUniquePhrasesOnly());
			client.setVoicesExtractTextDependentFeatures(isExtractTextDependent());
			client.setVoicesExtractTextIndependentFeatures(isExtractTextIndependent());
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void loadSettings() {
		listDevices();
		setMicrophone(client.getVoiceCaptureDevice());
		setUniquePhrasesOnly(client.isVoicesUniquePhrasesOnly());
		setExtractTextDependent(client.isVoicesExtractTextDependentFeatures());
		setExtractTextIndependent(client.isVoicesExtractTextIndependentFeatures());
		repaint();
	}


	@Override
	public void defaultSettings() {
		if (comboBoxMicrophone.getModel().getSize() > 0) {
			comboBoxMicrophone.setSelectedIndex(0);
		}
		defaultClientProperties.getVoices().applyTo(client);
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
