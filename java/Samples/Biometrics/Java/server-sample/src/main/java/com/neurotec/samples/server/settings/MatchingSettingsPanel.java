package com.neurotec.samples.server.settings;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.util.concurrent.ExecutionException;

import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.InputVerifier;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JComponent;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JSpinner;
import javax.swing.JTabbedPane;
import javax.swing.JTextField;
import javax.swing.SpinnerNumberModel;

import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.samples.server.controls.BasePanel;
import com.neurotec.samples.server.util.BiometricUtils;
import com.neurotec.samples.server.util.GridBagUtils;

public final class MatchingSettingsPanel extends BasePanel {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private GridBagUtils gridBagUtils;
	private Settings currentSettings = Settings.getInstance();

	// ==============================================
	// Private GUI controls
	// ==============================================

	@SuppressWarnings("rawtypes")
	private JComboBox cmbThreshold;
	@SuppressWarnings("rawtypes")
	private JComboBox cmbFingersSpeed;
	private JSpinner spinnerFingersRotation;
	@SuppressWarnings("rawtypes")
	private JComboBox cmbFacesSpeed;
	@SuppressWarnings("rawtypes")
	private JComboBox cmbIrisesSpeed;
	private JSpinner spinnerIrisesRotation;
	@SuppressWarnings("rawtypes")
	private JComboBox cmbPalmsSpeed;
	private JSpinner spinnerPalmsRotation;
	private JButton btnReset;
	private JButton btnApply;

	// ==============================================
	// Public constructor
	// ==============================================

	@SuppressWarnings("unchecked")
	public MatchingSettingsPanel(Frame owner) {
		super(owner);

		initializeComponents();
		String[] thresholds = {
				BiometricUtils.matchingThresholdToString(12),
				BiometricUtils.matchingThresholdToString(24),
				BiometricUtils.matchingThresholdToString(36),
				BiometricUtils.matchingThresholdToString(48),
				BiometricUtils.matchingThresholdToString(60),
				BiometricUtils.matchingThresholdToString(72), };

		for (String t : thresholds) {
			cmbThreshold.addItem(t);
		}

		NMatchingSpeed[] speeds = { NMatchingSpeed.HIGH, NMatchingSpeed.MEDIUM, NMatchingSpeed.LOW };
		for (NMatchingSpeed s : speeds) {
			cmbFingersSpeed.addItem(s);
			cmbFacesSpeed.addItem(s);
			cmbIrisesSpeed.addItem(s);
			cmbPalmsSpeed.addItem(s);
		}

		addComponentListener(new ComponentAdapter() {

			@Override
			public void componentShown(ComponentEvent e) {
				loadSettings();
			}
		});

	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		gridBagUtils = new GridBagUtils(GridBagConstraints.BOTH, new Insets(4, 4, 4, 4));

		setLayout(new BorderLayout(5, 5));

		JTabbedPane tabbedPane = new JTabbedPane();
		tabbedPane.addTab("General", initializeGeneralPanel());
		tabbedPane.addTab("Fingers", initializeFingersPanel());
		tabbedPane.addTab("Faces", initializeFacesPanel());
		tabbedPane.addTab("Irises", initializeIrisesPanel());
		tabbedPane.addTab("Palms", initializePalmsPanel());

		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));

		btnReset = new JButton("Reset");
		btnReset.addActionListener(this);

		btnApply = new JButton("Apply");
		btnApply.addActionListener(this);

		buttonPanel.add(btnReset);
		buttonPanel.add(Box.createHorizontalStrut(5));
		buttonPanel.add(btnApply);
		buttonPanel.add(Box.createGlue());

		add(tabbedPane, BorderLayout.CENTER);
		add(buttonPanel, BorderLayout.AFTER_LAST_LINE);
	}

	@SuppressWarnings("rawtypes")
	private JPanel initializeGeneralPanel() {
		JPanel generalPanel = new JPanel();
		generalPanel.setBackground(Color.WHITE);
		generalPanel.setOpaque(true);

		GridBagLayout generalPanelLayout = new GridBagLayout();
		generalPanelLayout.columnWidths = new int[] { 100, 150, 70, 150, 50 };
		generalPanelLayout.rowHeights = new int[] { 25, 25, 100 };
		generalPanel.setLayout(generalPanelLayout);

		cmbThreshold = new JComboBox();
		cmbThreshold.setEditable(true);
		((JTextField) cmbThreshold.getEditor().getEditorComponent()).setInputVerifier(new ThresholdComboBoxVerifier(cmbThreshold));

		gridBagUtils.addToGridBagLayout(0, 0, generalPanel, new JLabel("Matching threshold:"));
		gridBagUtils.addToGridBagLayout(1, 0, generalPanel, cmbThreshold);
		gridBagUtils.addToGridBagLayout(0, 2, 3, 1, 0, 1, generalPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(4, 0, 3, 1, 1, 0, generalPanel, new JLabel());
		gridBagUtils.clearGridBagConstraints();

		return generalPanel;
	}

	@SuppressWarnings("rawtypes")
	private JPanel initializeFingersPanel() {
		JPanel fingersPanel = new JPanel();
		fingersPanel.setBackground(Color.WHITE);
		fingersPanel.setOpaque(true);

		GridBagLayout fingersPanelLayout = new GridBagLayout();
		fingersPanelLayout.columnWidths = new int[] { 175, 150, 100 };
		fingersPanelLayout.rowHeights = new int[] { 25, 25, 25, 25, 25, 100 };
		fingersPanel.setLayout(fingersPanelLayout);

		cmbFingersSpeed = new JComboBox();
		spinnerFingersRotation = new JSpinner(new SpinnerNumberModel(0, 0, 180, 1));

		gridBagUtils.addToGridBagLayout(0, 0, fingersPanel, new JLabel("Speed:"));
		gridBagUtils.addToGridBagLayout(1, 0, fingersPanel, cmbFingersSpeed);
		gridBagUtils.addToGridBagLayout(0, 1, fingersPanel, new JLabel("Maximal rotation:"));
		gridBagUtils.addToGridBagLayout(1, 1, fingersPanel, spinnerFingersRotation);
		gridBagUtils.addToGridBagLayout(0, 5, 1, 1, 0, 1, fingersPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, 1, 0, fingersPanel, new JLabel());
		gridBagUtils.clearGridBagConstraints();

		return fingersPanel;
	}

	@SuppressWarnings("rawtypes")
	private JPanel initializeFacesPanel() {
		JPanel facesPanel = new JPanel();
		facesPanel.setBackground(Color.WHITE);
		facesPanel.setOpaque(true);

		GridBagLayout facesPanelLayout = new GridBagLayout();
		facesPanelLayout.columnWidths = new int[] { 60, 175, 100 };
		facesPanelLayout.rowHeights = new int[] { 25, 25 };
		facesPanel.setLayout(facesPanelLayout);

		cmbFacesSpeed = new JComboBox();

		gridBagUtils.setInsets(new Insets(6, 6, 6, 6));
		gridBagUtils.addToGridBagLayout(0, 0, facesPanel, new JLabel("Speed:"));
		gridBagUtils.addToGridBagLayout(1, 0, facesPanel, cmbFacesSpeed);
		gridBagUtils.addToGridBagLayout(0, 2, 1, 1, 0, 1, facesPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, 1, 0, facesPanel, new JLabel());
		gridBagUtils.clearGridBagConstraints();

		return facesPanel;
	}

	@SuppressWarnings("rawtypes")
	private JPanel initializeIrisesPanel() {
		JPanel irisesPanel = new JPanel();
		irisesPanel.setBackground(Color.WHITE);
		irisesPanel.setOpaque(true);

		GridBagLayout irisesPanelLayout = new GridBagLayout();
		irisesPanelLayout.columnWidths = new int[] { 175, 150, 100 };
		irisesPanelLayout.rowHeights = new int[] { 25, 25, 25, 25, 25 };
		irisesPanel.setLayout(irisesPanelLayout);

		cmbIrisesSpeed = new JComboBox();

		spinnerIrisesRotation = new JSpinner(new SpinnerNumberModel(0, 0, 180, 1));

		gridBagUtils.setInsets(new Insets(4, 4, 4, 4));
		gridBagUtils.addToGridBagLayout(0, 0, irisesPanel, new JLabel("Speed:"));
		gridBagUtils.addToGridBagLayout(1, 0, irisesPanel, cmbIrisesSpeed);
		gridBagUtils.addToGridBagLayout(0, 1, irisesPanel, new JLabel("Maximal rotation:"));
		gridBagUtils.addToGridBagLayout(1, 1, irisesPanel, spinnerIrisesRotation);
		gridBagUtils.addToGridBagLayout(0, 5, 1, 1, 0, 1, irisesPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, 1, 0, irisesPanel, new JLabel());
		gridBagUtils.clearGridBagConstraints();

		return irisesPanel;
	}

	@SuppressWarnings("rawtypes")
	private JPanel initializePalmsPanel() {
		JPanel palmsPanel = new JPanel();
		palmsPanel.setBackground(Color.WHITE);
		palmsPanel.setOpaque(true);

		GridBagLayout palmsPanelLayout = new GridBagLayout();
		palmsPanelLayout.columnWidths = new int[] { 175, 150, 100 };
		palmsPanelLayout.rowHeights = new int[] { 25, 25, 25, 25 };
		palmsPanel.setLayout(palmsPanelLayout);

		cmbPalmsSpeed = new JComboBox();
		spinnerPalmsRotation = new JSpinner(new SpinnerNumberModel(0, 0, 180, 1));

		gridBagUtils.addToGridBagLayout(0, 0, palmsPanel, new JLabel("Speed:"));
		gridBagUtils.addToGridBagLayout(1, 0, palmsPanel, cmbPalmsSpeed);
		gridBagUtils.addToGridBagLayout(0, 1, palmsPanel, new JLabel("Maximal rotation:"));
		gridBagUtils.addToGridBagLayout(1, 1, palmsPanel, spinnerPalmsRotation);
		gridBagUtils.addToGridBagLayout(0, 4, 1, 1, 0, 1, palmsPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, 1, 0, palmsPanel, new JLabel());

		return palmsPanel;
	}

	private void applyChanges() {
		currentSettings.setMatchingThreshold(BiometricUtils.matchingThresholdFromString(String.valueOf(cmbThreshold.getSelectedItem())));

		currentSettings.setFingersMatchingSpeed((NMatchingSpeed) cmbFingersSpeed.getSelectedItem());
		currentSettings.setFingersMaximalRotation(BiometricUtils.maximalRotationFromDegrees((Integer) spinnerFingersRotation.getValue()));

		currentSettings.setFacesMatchingSpeed((NMatchingSpeed) cmbFacesSpeed.getSelectedItem());
		currentSettings.setIrisesMatchingSpeed((NMatchingSpeed) cmbIrisesSpeed.getSelectedItem());
		currentSettings.setIrisesMaximalRotation(BiometricUtils.maximalRotationFromDegrees((Integer) spinnerIrisesRotation.getValue()));

		currentSettings.setPalmsMatchingSpeed((NMatchingSpeed) cmbPalmsSpeed.getSelectedItem());
		currentSettings.setPalmsMaximalRotation(BiometricUtils.maximalRotationFromDegrees((Integer) spinnerPalmsRotation.getValue()));

		currentSettings.save();
	}

	private void loadSettings() {
		selectThreshold(cmbThreshold, currentSettings.getMatchingThreshold());
		cmbFingersSpeed.setSelectedItem(currentSettings.getFingersMatchingSpeed());

		spinnerFingersRotation.setValue(BiometricUtils.maximalRotationToDegrees(currentSettings.getFingersMaximalRotation()));
		spinnerIrisesRotation.setValue(BiometricUtils.maximalRotationToDegrees(currentSettings.getIrisesMaximalRotation()));
		spinnerPalmsRotation.setValue(BiometricUtils.maximalRotationToDegrees(currentSettings.getPalmsMaximalRotation()));

		cmbFacesSpeed.setSelectedItem(currentSettings.getFacesMatchingSpeed());
		cmbIrisesSpeed.setSelectedItem(currentSettings.getIrisesMatchingSpeed());

		cmbPalmsSpeed.setSelectedItem(currentSettings.getPalmsMatchingSpeed());
	}

	@SuppressWarnings({ "rawtypes", "unchecked" })
	private void selectThreshold(JComboBox target, int value) {
		String str = BiometricUtils.matchingThresholdToString(value);
		int index = getIndexOfComboBoxItem(target, str);
		if (index != -1) {
			target.setSelectedIndex(index);
		} else {
			target.addItem(str);
			target.setSelectedItem(str);
		}
	}

	@SuppressWarnings("rawtypes")
	private int getIndexOfComboBoxItem(JComboBox cmb, Object item) {
		for (int i = 0; i < cmb.getItemCount(); i++) {
			Object o = cmb.getItemAt(i);
			if (o.equals(item)) {
				return i;
			}
		}
		return -1;
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public String getTitle() {
		return "Matching parameters";
	}

	@Override
	public boolean isBusy() {
		return false;
	}

	@Override
	public void cancel() {
	}

	@Override
	public void waitForCurrentProcessToFinish() throws InterruptedException, ExecutionException {
	}

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnApply) {
			applyChanges();
		} else if (source == btnReset) {
			currentSettings.loadDefaultMatchingSettings();
			loadSettings();
		}
	}

	// ==============================================
	// Private classes
	// ==============================================

	private static class ThresholdComboBoxVerifier extends InputVerifier {

		// ==============================================
		// Private fields
		// ==============================================

		@SuppressWarnings("rawtypes")
		private JComboBox comboBox;

		// ==============================================
		// Package private constructor
		// ==============================================
		@SuppressWarnings("rawtypes")
		ThresholdComboBoxVerifier(JComboBox comboBox) {
			this.comboBox = comboBox;
		}

		// ==============================================
		// Public overridden methods
		// ==============================================

		@SuppressWarnings("unchecked")
		@Override
		public boolean verify(JComponent input) {
			try {
				if (input instanceof JTextField) {
					int value = BiometricUtils.matchingThresholdFromString(((JTextField) input).getText());
					String item = BiometricUtils.matchingThresholdToString(value);
					comboBox.addItem(item);
					comboBox.setSelectedItem(item);
					return true;
				}
			} catch (Exception e) {
				JOptionPane.showMessageDialog(input, "Matching threshold is invalid");
			}
			return false;
		}

	}

}
