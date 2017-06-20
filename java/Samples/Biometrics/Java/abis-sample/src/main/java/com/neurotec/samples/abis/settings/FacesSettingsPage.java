package com.neurotec.samples.abis.settings;

import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;

import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NLivenessMode;
import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NCamera;
import com.neurotec.devices.NCaptureDevice;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.media.NMediaFormat;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.tabbedview.ConnectToDevicePanel;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

public final class FacesSettingsPage extends SettingsPage implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JComboBox comboBoxCamera;
	private JButton btnConnect;
	private JButton btnDisconnect;
	private JComboBox comboBoxFormat;
	private JCheckBox cbDetermineGender;
	private JCheckBox cbDetermineAge;
	private JCheckBox cbDetectProperties;
	private JCheckBox cbDetectBaseFeaturePoints;
	private JCheckBox cbDetectAllFeaturePoints;
	private JCheckBox cbRecognizeExpression;
	private JCheckBox cbRecognizeEmotion;
	private JCheckBox cbCreateThumbnailImage;
	private JComboBox comboBoxTemplateSize;
	private JComboBox comboBoxMatchingSpeed;
	private JComboBox comboBoxLivenessMode;
	private JLabel lblCamera;
	private JLabel lblFormat;
	private JLabel lblMinIod;
	private JLabel lblTemplateSize;
	private JLabel lblMaximalRoll;
	private JLabel lblLivenessThreshold;
	private JLabel lblMatchingSpeed;
	private JLabel lblQualityThreshold;
	private JLabel lblConfidenceThreshold;
	private JLabel lblMaximalYaw;
	private JLabel lblWidth;
	private JLabel lblGeneralizationRecordCount;
	private JLabel lblLivenessMode;
	private JSpinner spinnerLivenessThreshold;
	private JSpinner spinnerMinIod;
	private JSpinner spinnerMaximalRoll;
	private JSpinner spinnerQualityThreshold;
	private JSpinner spinnerConfidenceThreshold;
	private JSpinner spinnerMaximalYaw;
	private JSpinner spinnerWidth;
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

	public FacesSettingsPage(NBiometricClient client, PageNavigationController pageController) {
		super("Faces", pageController, client);
		initGUI();

		comboBoxMatchingSpeed.addItem(NMatchingSpeed.HIGH);
		comboBoxMatchingSpeed.addItem(NMatchingSpeed.MEDIUM);
		comboBoxMatchingSpeed.addItem(NMatchingSpeed.LOW);

		comboBoxTemplateSize.addItem(NTemplateSize.LARGE);
		comboBoxTemplateSize.addItem(NTemplateSize.MEDIUM);
		comboBoxTemplateSize.addItem(NTemplateSize.SMALL);

		comboBoxLivenessMode.addItem(NLivenessMode.SIMPLE);
		comboBoxLivenessMode.addItem(NLivenessMode.PASSIVE_AND_ACTIVE);
		comboBoxLivenessMode.addItem(NLivenessMode.ACTIVE);
		comboBoxLivenessMode.addItem(NLivenessMode.PASSIVE);
		comboBoxLivenessMode.addItem(NLivenessMode.NONE);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		try {
			GridBagLayout thisLayout = new GridBagLayout();
			thisLayout.rowWeights = new double[] {0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 0.1, 1.0};
			thisLayout.rowHeights = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
			thisLayout.columnWeights = new double[] {0.5, 0.5, 0.5, 1.0};
			thisLayout.columnWidths = new int[] {10, 10, 10, 10};
			this.setLayout(thisLayout);
			Insets insets = new Insets(5, 10, 5, 10);
			{
				lblCamera = new JLabel("Camera:");
				GridBagConstraints gbc_lblCamera = new GridBagConstraints();
				gbc_lblCamera.anchor = GridBagConstraints.EAST;
				gbc_lblCamera.insets = new Insets(0, 0, 5, 5);
				gbc_lblCamera.gridx = 0;
				gbc_lblCamera.gridy = 0;
				add(lblCamera, gbc_lblCamera);

				comboBoxCamera = new JComboBox(new DefaultComboBoxModel());
				comboBoxCamera.setPreferredSize(new Dimension(200, 20));
				this.add(comboBoxCamera, new GridBagConstraints(1, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxCamera.addItemListener(new ItemListener() {
					@Override
					public void itemStateChanged(ItemEvent event) {
						if (event.getStateChange() == ItemEvent.SELECTED) {
							client.setFaceCaptureDevice((NCamera) comboBoxCamera.getSelectedItem());
							listVideoFormats();
							btnDisconnect.setEnabled((client.getFaceCaptureDevice() != null) && client.getFaceCaptureDevice().isDisconnectable());
						}
					}
				});

				btnConnect = new JButton("Connect to...");
				this.add(btnConnect, new GridBagConstraints(2, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(0, 0, 5, 5), 0, 0));
				btnConnect.addActionListener(this);
				btnDisconnect = new JButton("Disconnect");
				btnDisconnect.setEnabled(false);
				this.add(btnDisconnect, new GridBagConstraints(3, 0, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(0, 0, 5, 0), 0, 0));
				btnDisconnect.addActionListener(this);
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
			}
			{
				lblTemplateSize = new JLabel("Template size:");
				this.add(lblTemplateSize, new GridBagConstraints(0, 2, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxTemplateSize = new JComboBox(new DefaultComboBoxModel());
				comboBoxTemplateSize.setPreferredSize(new Dimension(100, 20));
				this.add(comboBoxTemplateSize, new GridBagConstraints(1, 2, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblMatchingSpeed = new JLabel("Matching speed:");
				this.add(lblMatchingSpeed, new GridBagConstraints(0, 3, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxMatchingSpeed = new JComboBox(new DefaultComboBoxModel());
				comboBoxMatchingSpeed.setPreferredSize(new Dimension(100, 20));
				this.add(comboBoxMatchingSpeed, new GridBagConstraints(1, 3, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblMinIod = new JLabel("Minimal inter ocular distance:");
				this.add(lblMinIod, new GridBagConstraints(0, 4, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				spinnerMinIod = new JSpinner(new SpinnerNumberModel(40, 40, 16384, 1));
				spinnerMinIod.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerMinIod.getEditor()).getTextField().setEditable(false);
				this.add(spinnerMinIod, new GridBagConstraints(1, 4, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblConfidenceThreshold = new JLabel("Confidence threshold:");
				this.add(lblConfidenceThreshold, new GridBagConstraints(0, 5, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				spinnerConfidenceThreshold = new JSpinner(new SpinnerNumberModel(0, 0, 100, 1));
				spinnerConfidenceThreshold.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerConfidenceThreshold.getEditor()).getTextField().setEditable(false);
				this.add(spinnerConfidenceThreshold, new GridBagConstraints(1, 5, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblMaximalRoll = new JLabel("Maximal roll:");
				this.add(lblMaximalRoll, new GridBagConstraints(0, 6, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				spinnerMaximalRoll = new JSpinner(new SpinnerNumberModel(0, 0, 180.0, 1));
				spinnerMaximalRoll.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerMaximalRoll.getEditor()).getTextField().setEditable(false);
				this.add(spinnerMaximalRoll, new GridBagConstraints(1, 6, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblMaximalYaw = new JLabel("Maximal yaw:");
				this.add(lblMaximalYaw, new GridBagConstraints(0, 7, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				spinnerMaximalYaw = new JSpinner(new SpinnerNumberModel(15.0, 0, 90.0, 1));
				spinnerMaximalYaw.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerMaximalYaw.getEditor()).getTextField().setEditable(false);
				this.add(spinnerMaximalYaw, new GridBagConstraints(1, 7, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}
			{
				lblQualityThreshold = new JLabel("Quality threshold:");
				this.add(lblQualityThreshold, new GridBagConstraints(0, 8, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				spinnerQualityThreshold = new JSpinner(new SpinnerNumberModel(0, 0, 100, 1));
				spinnerQualityThreshold.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerQualityThreshold.getEditor()).getTextField().setEditable(false);
				this.add(spinnerQualityThreshold, new GridBagConstraints(1, 8, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
			}

			{
				lblGeneralizationRecordCount = new JLabel("Generalization record count:");
				this.add(lblGeneralizationRecordCount , new GridBagConstraints(0, 9, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				SpinnerNumberModel model = new SpinnerNumberModel(3, 3, 100, 1);
				spinnerGeneralizationRecordCount = new JSpinner(model);
				((JSpinner.DefaultEditor) spinnerGeneralizationRecordCount.getEditor()).getTextField().setEditable(false);
				spinnerGeneralizationRecordCount.setPreferredSize(new Dimension(100, 20));
				this.add(spinnerGeneralizationRecordCount, new GridBagConstraints(1, 9, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				lblLivenessMode = new JLabel("Liveness mode:");
				this.add(lblLivenessMode, new GridBagConstraints(0, 10, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxLivenessMode = new JComboBox(new DefaultComboBoxModel());
				comboBoxLivenessMode.setPreferredSize(new Dimension(200, 20));
				this.add(comboBoxLivenessMode, new GridBagConstraints(1, 10, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, insets, 0, 0));
				comboBoxLivenessMode.addItemListener(new ItemListener() {
					@Override
					public void itemStateChanged(ItemEvent e) {
						updateView();
					}
				});
			}
			{
				lblLivenessThreshold = new JLabel("Liveness threshold:");
				this.add(lblLivenessThreshold, new GridBagConstraints(0, 11, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				spinnerLivenessThreshold = new JSpinner(new SpinnerNumberModel(0, 0, 100, 1));
				spinnerLivenessThreshold.setPreferredSize(new Dimension(100, 20));
				((JSpinner.DefaultEditor) spinnerLivenessThreshold.getEditor()).getTextField().setEditable(false);
				this.add(spinnerLivenessThreshold, new GridBagConstraints(1, 11, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbDetectAllFeaturePoints = new JCheckBox();
				cbDetectAllFeaturePoints.setText("Detect all facial feature points");
				this.add(cbDetectAllFeaturePoints, new GridBagConstraints(1, 12, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbDetectBaseFeaturePoints = new JCheckBox("Detect base feature points");
				this.add(cbDetectBaseFeaturePoints, new GridBagConstraints(1, 13, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbDetermineGender = new JCheckBox("Determine gender");
				this.add(cbDetermineGender, new GridBagConstraints(1, 14, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbDetermineAge = new JCheckBox("Determine age");
				this.add(cbDetermineAge, new GridBagConstraints(1, 15, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbDetectProperties = new JCheckBox("Detect properties");
				add(cbDetectProperties, new GridBagConstraints(1, 16, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbRecognizeExpression = new JCheckBox("Recognize expression");
				add(cbRecognizeExpression, new GridBagConstraints(1, 17, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbRecognizeEmotion = new JCheckBox("Recognize emotion");
				add(cbRecognizeEmotion, new GridBagConstraints(1, 18, 1, 1, 0.0, 0.0, GridBagConstraints.WEST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				cbCreateThumbnailImage = new JCheckBox("Create thumbnail image");
				cbCreateThumbnailImage.setSelected(false);
				cbCreateThumbnailImage.addItemListener(new ItemListener() {
					@Override
					public void itemStateChanged(ItemEvent e) {
						updateView();
					}
				});
				GridBagConstraints gbc_cbCreateThumbnailImage = new GridBagConstraints();
				gbc_cbCreateThumbnailImage.anchor = GridBagConstraints.EAST;
				gbc_cbCreateThumbnailImage.insets = new Insets(0, 0, 5, 5);
				gbc_cbCreateThumbnailImage.gridx = 0;
				gbc_cbCreateThumbnailImage.gridy = 19;
				add(cbCreateThumbnailImage, gbc_cbCreateThumbnailImage);
			}
			{
				lblWidth = new JLabel("Width:");
				this.add(lblWidth, new GridBagConstraints(0, 20, 1, 1, 0.0, 0.0, GridBagConstraints.EAST, GridBagConstraints.NONE, new Insets(5, 10, 5, 10), 0, 0));
			}
			{
				spinnerWidth = new JSpinner(new SpinnerNumberModel(30, 30, 1000, 1));
				spinnerWidth.setPreferredSize(new Dimension(100, 20));
				GridBagConstraints gbc_spinnerWidth = new GridBagConstraints();
				gbc_spinnerWidth.insets = new Insets(0, 0, 5, 5);
				gbc_spinnerWidth.anchor = GridBagConstraints.WEST;
				gbc_spinnerWidth.gridx = 1;
				gbc_spinnerWidth.gridy = 20;
				this.add(spinnerWidth, gbc_spinnerWidth);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private void updateView() {
		spinnerLivenessThreshold.setEnabled(!(((NLivenessMode) comboBoxLivenessMode.getSelectedItem()).equals(NLivenessMode.NONE)));
		spinnerWidth.setEnabled(cbCreateThumbnailImage.isSelected());
	}

	private NCamera getCamera() {
		if (comboBoxCamera.getSelectedIndex() == -1) {
			return null;
		} else {
			return (NCamera) comboBoxCamera.getSelectedItem();
		}
	}

	private void setCamera(NCamera value) {
		comboBoxCamera.setSelectedItem(value);
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

	private byte getConfidenceThreshold() {
		return ((Number) spinnerConfidenceThreshold.getValue()).byteValue();
	}

	private void setConfidenceThreshold(byte value) {
		spinnerConfidenceThreshold.setValue(value);
	}

	private byte getQualityThreshold() {
		return ((Number) spinnerQualityThreshold.getValue()).byteValue();
	}

	private int getGeneralizationRecordCount() {
		return (Integer) spinnerGeneralizationRecordCount.getValue();
	}

	private void setGeneralizationRecordCount(int value) {
		spinnerGeneralizationRecordCount.setValue(value);
	}

	private void setQuality(byte value) {
		spinnerQualityThreshold.setValue(value);
	}

	private int getMinimalIOD() {
		return (Integer) spinnerMinIod.getValue();
	}
	private void setMinimalIOD(int value) {
		spinnerMinIod.setValue(value);
	}

	private float getMaximalRoll() {
		return ((Number) spinnerMaximalRoll.getValue()).floatValue();
	}

	private void setMaximalRoll(float value) {
		spinnerMaximalRoll.setValue(value);
	}

	private float getMaximalYaw() {
		return ((Number) spinnerMaximalYaw.getValue()).floatValue();
	}

	private void setMaximalYaw(float value) {
		this.spinnerMaximalYaw.setValue(value);
	}

	private NLivenessMode getLivenessMode() {
		return (NLivenessMode) comboBoxLivenessMode.getSelectedItem();
	}

	private void setLivenessMode(NLivenessMode value) {
		DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxLivenessMode.getModel();
		comboBoxLivenessMode.setSelectedItem(value);
		if ((comboBoxLivenessMode.getSelectedItem() != null) && !comboBoxLivenessMode.getSelectedItem().equals(value)) {
			model.addElement(value);
			comboBoxLivenessMode.setSelectedItem(value);
		}
	}

	private int getLivenessThreshold() {
		return (Integer) spinnerLivenessThreshold.getValue();
	}

	private void setLivenessThreshold(int value) {
		spinnerLivenessThreshold.setValue(value);
	}

	private boolean isDetectAllFeaturePoints() {
		return cbDetectAllFeaturePoints.isSelected();
	}

	private void setDetectAllFeaturePoints(boolean value) {
		this.cbDetectAllFeaturePoints.setSelected(value);
	}

	private boolean isDetermineGender() {
		return cbDetermineGender.isSelected();
	}

	private void setDetermineGender(boolean value) {
		this.cbDetermineGender.setSelected(value);
	}

	private boolean isDetermineAge() {
		return cbDetermineAge.isSelected();
	}

	private void setDetermineAge(boolean value) {
		this.cbDetermineAge.setSelected(value);
	}

	private boolean isDetectProperties() {
		return cbDetectProperties.isSelected();
	}

	private void setDetectProperties(boolean value) {
		this.cbDetectProperties.setSelected(value);
	}

	private boolean isRecognizeExpression() {
		return cbRecognizeExpression.isSelected();
	}

	private void setRecognizeExpression(boolean value) {
		this.cbRecognizeExpression.setSelected(value);
	}

	private boolean isRecognizeEmotion() {
		return cbRecognizeEmotion.isSelected();
	}

	private void setRecognizeEmotion(boolean value) {
		this.cbRecognizeEmotion.setSelected(value);
	}

	private boolean isDetectBaseFeaturePoints() {
		return cbDetectBaseFeaturePoints.isSelected();
	}

	private void setDetectBaseFeaturePoints(boolean value) {
		this.cbDetectBaseFeaturePoints.setSelected(value);
	}

	private boolean isCreateThumbnail() {
		return cbCreateThumbnailImage.isSelected();
	}

	private void setCreateThumbnail(boolean value) {
		this.cbCreateThumbnailImage.setSelected(value);
	}

	private void setThumbnailWidth(int value) {
		this.spinnerWidth.setValue(value);
	}

	private int getThumbnailWidth() {
		return (Integer) spinnerWidth.getValue();
	}

	private void setVideoFormat(NMediaFormat value) {
		DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxFormat.getModel();
		comboBoxFormat.setSelectedItem(value);
		if ((comboBoxFormat.getSelectedItem() != null) && !comboBoxFormat.getSelectedItem().equals(value)) {
			model.addElement(value);
			comboBoxFormat.setSelectedItem(value);
		}
	}

	private NMediaFormat getVideoFormat() {
		return (NMediaFormat) comboBoxFormat.getSelectedItem();
	}

	private void listDevices() {
		try {
			Object selected = client.getFaceCaptureDevice();
			DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxCamera.getModel();
			model.removeAllElements();
			for (NDevice item : client.getDeviceManager().getDevices()) {
				if ((item.getDeviceType().contains(NDeviceType.CAMERA))) {
					model.addElement(item);
				}
			}
			comboBoxCamera.setSelectedItem(selected);
			if (comboBoxCamera.getSelectedIndex() == -1 && model.getSize() > 0) {
				comboBoxCamera.setSelectedIndex(0);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	private void listVideoFormats() {
		DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxFormat.getModel();
		model.removeAllElements();
		NCaptureDevice device = (NCaptureDevice) comboBoxCamera.getSelectedItem();
		if (device != null) {
			for (NMediaFormat item : device.getFormats()) {
				model.addElement(item);
			}
			setVideoFormat(device.getCurrentFormat());
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void saveSettings() {
		try {
			client.setFaceCaptureDevice(getCamera());
			client.setFacesTemplateSize(getTemplateSize());
			client.setFacesMatchingSpeed(getMatchingSpeed());
			client.setFacesMinimalInterOcularDistance(getMinimalIOD());
			client.setFacesConfidenceThreshold(getConfidenceThreshold());
			client.setFacesMaximalRoll(getMaximalRoll());
			client.setFacesMaximalYaw(getMaximalYaw());
			client.setFacesQualityThreshold(getQualityThreshold());
			client.setFacesLivenessMode(getLivenessMode());
			client.setFacesLivenessThreshold((byte) getLivenessThreshold());
			client.setFacesDetectAllFeaturePoints(isDetectAllFeaturePoints());
			client.setFacesDetectBaseFeaturePoints(isDetectBaseFeaturePoints());
			client.setFacesDetermineGender(isDetermineGender());
			client.setFacesDetermineAge(isDetermineAge());
			client.setFacesDetectProperties(isDetectProperties());
			client.setFacesRecognizeExpression(isRecognizeExpression());
			client.setFacesRecognizeEmotion(isRecognizeEmotion());
			client.setFacesCreateThumbnailImage(isCreateThumbnail());
			client.setFacesThumbnailImageWidth(getThumbnailWidth());
			NMediaFormat format = getVideoFormat();
			if ((client.getFaceCaptureDevice() != null) && (format != null)) {
				client.getFaceCaptureDevice().setCurrentFormat(format);
			}
			SettingsManager.setFacesGeneralizationRecordCount(getGeneralizationRecordCount());
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void loadSettings() {
		listDevices();
		setCamera(client.getFaceCaptureDevice());
		setTemplateSize(client.getFacesTemplateSize());
		setMatchingSpeed(client.getFacesMatchingSpeed());
		setMinimalIOD(client.getFacesMinimalInterOcularDistance());
		setConfidenceThreshold(client.getFacesConfidenceThreshold());
		setMaximalRoll(client.getFacesMaximalRoll());
		setMaximalYaw(client.getFacesMaximalYaw());
		setQuality(client.getFacesQualityThreshold());
		setLivenessMode(client.getFacesLivenessMode());
		setLivenessThreshold(client.getFacesLivenessThreshold());
		setDetectAllFeaturePoints(client.isFacesDetectAllFeaturePoints());
		setDetectBaseFeaturePoints(client.isFacesDetectBaseFeaturePoints());
		setDetermineGender(client.isFacesDetermineGender());
		setDetermineAge(client.isFacesDetermineAge());
		setDetectProperties(client.isFacesDetectProperties());
		setRecognizeExpression(client.isFacesRecognizeExpression());
		setRecognizeEmotion(client.isFacesRecognizeEmotion());
		setCreateThumbnail(client.isFacesCreateThumbnailImage());
		setThumbnailWidth(client.getFacesThumbnailImageWidth());
		if (client.getFaceCaptureDevice() != null) {
			setVideoFormat(client.getFaceCaptureDevice().getCurrentFormat());
		}
		setGeneralizationRecordCount(SettingsManager.getFacesGeneralizationRecordCount());
		updateView();
		repaint();
	}


	@Override
	public void defaultSettings() {
		if (comboBoxCamera.getModel().getSize() > 0) {
			comboBoxCamera.setSelectedIndex(0);
		}
		defaultClientProperties.getFaces().applyTo(client);
		SettingsManager.setFacesGeneralizationRecordCount(3);
		updateView();
		super.defaultSettings();
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		client.getDeviceManager().getDevices().addCollectionChangeListener(devicesCollectionChanged);
		boolean isActivated = LicenseManager.getInstance().isActivated("Biometrics.FaceSegmentation", true);
		if (!isActivated && client.getLocalOperations().contains(NBiometricOperation.DETECT_SEGMENTS)) {
			cbDetectAllFeaturePoints.setEnabled(false);
			cbDetectBaseFeaturePoints.setEnabled(false);
			cbDetermineGender.setEnabled(false);
			cbDetermineAge.setEnabled(false);
			cbRecognizeEmotion.setEnabled(false);
			cbDetectProperties.setEnabled(false);
			cbRecognizeExpression.setEnabled(false);

			cbDetectAllFeaturePoints.setText(cbDetectAllFeaturePoints.getText() + " (Not activated)");
			cbDetectBaseFeaturePoints.setText(cbDetectBaseFeaturePoints.getText() + " (Not activated)");
			cbDetermineGender.setText(cbDetermineGender.getText() + " (Not activated)");
			cbDetermineAge.setText(cbDetermineAge.getText() + " (Not activated)");
			cbRecognizeEmotion.setText(cbRecognizeEmotion.getText() + " (Not activated)");
			cbDetectProperties.setText(cbDetectProperties.getText() + " (Not activated)");
			cbRecognizeExpression.setText(cbRecognizeExpression.getText() + " (Not activated)");
		}
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
				dialog.getContentPane().add(connectPanel);
				dialog.setSize(new Dimension(500, 500));
				dialog.setLocationRelativeTo(this);
				dialog.setVisible(true);
				if (connectPanel.isResultOk()) {
					NDevice newDevice = null;
					try {
						newDevice = client.getDeviceManager().connectToDevice(connectPanel.getSelectedPlugin(), connectPanel.getParameters());
						listDevices();
						comboBoxCamera.setSelectedItem(newDevice);

						if (comboBoxCamera.getSelectedItem() != newDevice) {
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
				NDevice device = (NDevice) comboBoxCamera.getSelectedItem();
				if (device != null) {
					client.getDeviceManager().disconnectFromDevice(device);
				}
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

}
