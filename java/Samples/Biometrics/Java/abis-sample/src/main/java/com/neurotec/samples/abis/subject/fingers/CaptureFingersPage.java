package com.neurotec.samples.abis.subject.fingers;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.SystemColor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.File;
import java.util.EnumSet;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.SwingConstants;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;
import javax.swing.tree.DefaultMutableTreeNode;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.swing.NFingerView;
import com.neurotec.biometrics.swing.NFingerViewBase.ShownImage;
import com.neurotec.devices.NFScanner;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.NodeChangeEvent;
import com.neurotec.samples.abis.event.NodeChangeListener;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.settings.Settings;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.Scenario;
import com.neurotec.samples.abis.subject.Scenario.Attribute;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.samples.abis.swing.GeneralizeProgressView;
import com.neurotec.samples.abis.swing.HandSegmentSelector;
import com.neurotec.samples.abis.swing.HandSegmentSelector.Action;
import com.neurotec.samples.abis.swing.HandSegmentSelector.SelectorChangeEvent;
import com.neurotec.samples.abis.swing.HandSegmentSelector.SelectorChangeListener;
import com.neurotec.samples.abis.swing.Node;
import com.neurotec.samples.abis.swing.SubjectTree;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.CollectionUtils;
import com.neurotec.samples.abis.util.LockingTask;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;

public class CaptureFingersPage extends Page implements CaptureFingerView, PageNavigationListener, ActionListener, ItemListener, SelectorChangeListener, NodeChangeListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final Color COLOR_OK = new Color(0, 128, 0);
	private static final Color COLOR_ERROR = new Color(255, 0, 0);

	// ===========================================================
	// Private fields
	// ===========================================================

	private CaptureFingerModel biometricModel;
	private CaptureBiometricController biometricController;
	private NFinger currentBiometric;

	private NFinger finger;
	private NFPosition position;
	private Scenario scenario;
	private NFImpressionType impression;

	private ImageThumbnailFileChooser fcOpenImage;
	private File lastFile;
	private SubjectTree fingersTree;
	private NFingerView fingerView;
	private HandSegmentSelector fingerSelector;
	private boolean updating;

	private final LockingTask cancelTask = new LockingTask() {
		@Override
		public void performTask() {
			biometricController.cancel();
		}
	};

	private JButton btnCancel;
	private JButton btnCapture;
	private JButton btnFinish;
	private JButton btnForce;
	private JButton btnNext;
	private JButton btnRepeat;
	private ButtonGroup buttonGroupSource;
	private JCheckBox cbAutomatic;
	private JCheckBox cbShowBinarized;
	private JComboBox comboBoxImpression;
	private JComboBox comboBoxScenario;
	private JLabel lblHint;
	private JLabel lblImpression;
	private JLabel lblScenario;
	private JPanel panelCenter;
	private JPanel panelCenterBottom;
	private JPanel panelCenterBottomOuter;
	private JPanel panelControlButtons;
	private JPanel panelControls;
	private JPanel panelFingersSelector;
	private JPanel panelFinish;
	private JPanel panelLeft;
	private JPanel panelLeftCenter;
	private JPanel panelOptionsInner;
	private JPanel panelSource;
	private JPanel panelStatus;
	private JPanel panelTop;
	private JPanel panelZoom;
	private NViewZoomSlider horizontalZoomSlider;
	private JProgressBar progressBar;
	private JRadioButton rbFile;
	private JRadioButton rbScanner;
	private JRadioButton rbTenPrintCard;
	private JScrollPane spFingerView;
	private JTextPane tpStatus;
	private JPanel panelGeneralization;
	private GridBagConstraints gridBagConstraints_1;
	private GridBagConstraints gridBagConstraints_2;
	private GridBagConstraints gridBagConstraints_3;
	private GridBagConstraints gridBagConstraints_4;
	private JCheckBox cbWithGeneralization;
	private GeneralizeProgressView generalizeProgressView;
	private PropertyChangeListener generalizeProgressViewPropertyChangeListener = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent evt) {
			if (evt.getPropertyName().equals("Selected")) {
				updateShowReturned();
			}
		}
	};

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureFingersPage(PageNavigationController pageController) {
		super("New...", pageController);
		initGUI();
		lastFile = new File(Settings.getInstance().getLastFingerFilePath());
		fingersTree.setShownTypes(EnumSet.of(NBiometricType.FINGER));
		fingersTree.setShowBiometricsOnly(true);
		fingersTree.addNodeChangeListener(this);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		GridBagConstraints gridBagConstraints;
		buttonGroupSource = new ButtonGroup();
		panelTop = new JPanel();
		panelSource = new JPanel();
		rbScanner = new JRadioButton();
		rbFile = new JRadioButton();
		rbTenPrintCard = new JRadioButton();
		panelLeft = new JPanel();
		panelLeftCenter = new JPanel();
		panelControls = new JPanel();
		panelFingersSelector = new JPanel();
		lblHint = new JLabel();
		btnCapture = new JButton();
		panelCenter = new JPanel();
		spFingerView = new JScrollPane();
		panelCenterBottomOuter = new JPanel();
		panelStatus = new JPanel();
		progressBar = new JProgressBar();
		tpStatus = new JTextPane();
		StyledDocument doc = tpStatus.getStyledDocument();
		SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
		StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
		StyleConstants.setLeftIndent(styleAttributes, 3);
		StyleConstants.setRightIndent(styleAttributes, 3);
		StyleConstants.setFontSize(styleAttributes, 16);
		StyleConstants.setForeground(styleAttributes, SystemColor.menu);
		doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);
		panelControlButtons = new JPanel();
		btnRepeat = new JButton();
		btnNext = new JButton();
		btnForce = new JButton();
		btnCancel = new JButton();
		panelCenterBottom = new JPanel();
		panelZoom = new JPanel();
		cbShowBinarized = new JCheckBox();
		panelFinish = new JPanel();
		btnFinish = new JButton();

		setLayout(new BorderLayout());
		panelTop.setLayout(new FlowLayout(FlowLayout.LEFT, 5, 5));

		panelSource.setBorder(BorderFactory.createTitledBorder("Source"));
		panelSource.setLayout(new BoxLayout(panelSource, BoxLayout.Y_AXIS));

		buttonGroupSource.add(rbScanner);
		rbScanner.setText("Scanner");
		panelSource.add(rbScanner);

		buttonGroupSource.add(rbFile);
		rbFile.setText("File");
		panelSource.add(rbFile);

		buttonGroupSource.add(rbTenPrintCard);
		rbTenPrintCard.setText("Ten print card");
		panelSource.add(rbTenPrintCard);

		panelTop.add(panelSource);
		panelOptionsInner = new JPanel();
		panelOptionsInner.setBorder(BorderFactory.createTitledBorder("Options"));
		panelTop.add(panelOptionsInner);
		lblScenario = new JLabel();

		panelOptionsInner.setAlignmentX(0.0F);
		GridBagLayout panelOptionsInnerLayout = new GridBagLayout();
		panelOptionsInnerLayout.columnWidths = new int[] {0, 0, 0};
		panelOptionsInnerLayout.rowHeights = new int[] {0, 0, 0};
		panelOptionsInner.setLayout(panelOptionsInnerLayout);

		lblScenario.setText("Scenario");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.insets = new Insets(0, 0, 5, 5);
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelOptionsInner.add(lblScenario, gridBagConstraints);
		comboBoxScenario = new JComboBox();

		comboBoxScenario.setPreferredSize(new Dimension(200, 20));
		gridBagConstraints_1 = new GridBagConstraints();
		gridBagConstraints_1.insets = new Insets(0, 0, 5, 5);
		gridBagConstraints_1.gridwidth = 2;
		gridBagConstraints_1.gridx = 1;
		gridBagConstraints_1.gridy = 0;
		gridBagConstraints_1.fill = GridBagConstraints.HORIZONTAL;
		panelOptionsInner.add(comboBoxScenario, gridBagConstraints_1);
		comboBoxScenario.addItemListener(this);
		lblImpression = new JLabel();

		lblImpression.setText("Impression");
		gridBagConstraints_2 = new GridBagConstraints();
		gridBagConstraints_2.insets = new Insets(0, 0, 5, 5);
		gridBagConstraints_2.gridx = 0;
		gridBagConstraints_2.gridy = 1;
		gridBagConstraints_2.anchor = GridBagConstraints.LINE_START;
		panelOptionsInner.add(lblImpression, gridBagConstraints_2);
		comboBoxImpression = new JComboBox();

		comboBoxImpression.setPreferredSize(new Dimension(200, 20));
		gridBagConstraints_3 = new GridBagConstraints();
		gridBagConstraints_3.insets = new Insets(0, 0, 5, 5);
		gridBagConstraints_3.gridwidth = 2;
		gridBagConstraints_3.gridx = 1;
		gridBagConstraints_3.gridy = 1;
		gridBagConstraints_3.fill = GridBagConstraints.HORIZONTAL;
		panelOptionsInner.add(comboBoxImpression, gridBagConstraints_3);
		comboBoxImpression.addItemListener(this);
		cbAutomatic = new JCheckBox();

		cbAutomatic.setSelected(true);
		cbAutomatic.setText("Capture automatically");
		gridBagConstraints_4 = new GridBagConstraints();
		gridBagConstraints_4.insets = new Insets(0, 0, 0, 5);
		gridBagConstraints_4.gridx = 1;
		gridBagConstraints_4.gridy = 2;
		gridBagConstraints_4.anchor = GridBagConstraints.LINE_START;
		panelOptionsInner.add(cbAutomatic, gridBagConstraints_4);

		cbWithGeneralization = new JCheckBox("With generalization");
		GridBagConstraints gbc_chckbxWithGeneralization = new GridBagConstraints();
		gbc_chckbxWithGeneralization.gridx = 2;
		gbc_chckbxWithGeneralization.gridy = 2;
		panelOptionsInner.add(cbWithGeneralization, gbc_chckbxWithGeneralization);

		add(panelTop, BorderLayout.NORTH);

		panelLeft.setLayout(new BorderLayout());

		panelLeftCenter.setLayout(new BorderLayout());

		panelControls.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		panelControls.setLayout(new BoxLayout(panelControls, BoxLayout.Y_AXIS));

		panelFingersSelector.setAlignmentX(0.0F);
		panelFingersSelector.setLayout(new BorderLayout());

		lblHint.setText("Hint: ");
		panelFingersSelector.add(lblHint, BorderLayout.SOUTH);

		panelControls.add(panelFingersSelector);

		btnCapture.setText("Capture");
		btnCapture.setHorizontalAlignment(SwingConstants.LEFT);
		panelControls.add(btnCapture);

		panelLeftCenter.add(panelControls, BorderLayout.NORTH);

		panelLeft.add(panelLeftCenter, BorderLayout.CENTER);

		add(panelLeft, BorderLayout.WEST);

		panelCenter.setLayout(new BorderLayout());
		panelCenter.add(spFingerView, BorderLayout.CENTER);

		panelCenterBottomOuter.setLayout(new BoxLayout(panelCenterBottomOuter, BoxLayout.Y_AXIS));

		panelGeneralization = new JPanel();
		panelCenterBottomOuter.add(panelGeneralization);
		panelGeneralization.setLayout(new BorderLayout(0, 0));

		generalizeProgressView = new GeneralizeProgressView();
		panelGeneralization.add(generalizeProgressView);

		panelStatus.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
		panelStatus.setLayout(new BorderLayout());

		progressBar.setIndeterminate(true);
		progressBar.setPreferredSize(new Dimension(0, 23));
		progressBar.setStringPainted(true);
		panelStatus.add(progressBar, BorderLayout.NORTH);

		tpStatus.setEditable(false);
		panelStatus.add(tpStatus, BorderLayout.SOUTH);

		panelCenterBottomOuter.add(panelStatus);

		btnRepeat.setText("Repeat");
		btnRepeat.setPreferredSize(new Dimension(70, 23));
		panelControlButtons.add(btnRepeat);

		btnNext.setText("Next");
		btnNext.setPreferredSize(new Dimension(70, 23));
		panelControlButtons.add(btnNext);

		btnForce.setText("Force");
		btnForce.setPreferredSize(new Dimension(70, 23));
		panelControlButtons.add(btnForce);

		btnCancel.setText("Cancel");
		btnCancel.setPreferredSize(new Dimension(70, 23));
		panelControlButtons.add(btnCancel);

		panelCenterBottomOuter.add(panelControlButtons);

		panelCenterBottom.setLayout(new BorderLayout());

		panelZoom.setLayout(new FlowLayout(FlowLayout.LEFT));

		cbShowBinarized.setText("Show binarized");
		panelZoom.add(cbShowBinarized);

		panelCenterBottom.add(panelZoom, BorderLayout.WEST);

		panelFinish.setLayout(new FlowLayout(FlowLayout.TRAILING));

		btnFinish.setText("Finish");
		panelFinish.add(btnFinish);

		panelCenterBottom.add(panelFinish, BorderLayout.EAST);

		panelCenterBottomOuter.add(panelCenterBottom);

		panelCenter.add(panelCenterBottomOuter, BorderLayout.SOUTH);

		add(panelCenter, BorderLayout.CENTER);

		fingerSelector = new HandSegmentSelector();
		fingerSelector.setPreferredSize(new Dimension(275, 130));
		panelFingersSelector.add(fingerSelector, BorderLayout.NORTH);

		fingersTree = new SubjectTree();
		fingersTree.setBorder(BorderFactory.createLineBorder(new Color(0, 0, 0)));
		fingersTree.addPropertyChangeListener(fingersTreePropertyChanged);
		panelLeftCenter.add(fingersTree, BorderLayout.CENTER);
		fingerView = new NFingerView();
		fingerView.setAutofit(true);
		generalizeProgressView.setView(this.fingerView);
		spFingerView.setViewportView(fingerView);

		horizontalZoomSlider = new NViewZoomSlider();
		horizontalZoomSlider.setView(fingerView);
		panelZoom.add(horizontalZoomSlider);

		fcOpenImage = new ImageThumbnailFileChooser();
		fcOpenImage.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcOpenImage.setMultiSelectionEnabled(false);

		btnCapture.addActionListener(this);
		btnFinish.addActionListener(this);
		btnRepeat.addActionListener(this);
		btnNext.addActionListener(this);
		btnCancel.addActionListener(this);
		btnForce.addActionListener(this);
		rbFile.addActionListener(this);
		rbTenPrintCard.addActionListener(this);
		rbScanner.addActionListener(this);
		cbShowBinarized.addActionListener(this);
	}

	private void setFinger(NFinger finger) {
		this.finger = finger;
		fingerChanged();
	}

	private void setPosition(NFPosition position) {
		if (this.position != position) {
			this.position = position;
			positionChanged();
		}
	}

	private void setScenario(Scenario scenario) {
		if (this.scenario != scenario) {
			this.scenario = scenario;
			scenarioChanged();
		}
	}

	private void setImpression(NFImpressionType impression) {
		if (this.impression != impression) {
			this.impression = impression;
			impressionChanged();
		}
	}

	private void fingerScannerChanged() {
		NFScanner device = biometricModel.getClient().getFingerScanner();
		if ((device == null) || !device.isAvailable()) {
			if (rbScanner.isSelected()) {
				rbFile.setSelected(true);
			}
			rbScanner.setText("Scanner (Not connected)");
		} else {
			rbScanner.setText(String.format("Scanner (%s)", device.getDisplayName()));
		}
		sourceChanged();
	}

	private void sourceChanged() {
		updateScenarioComboBox(biometricModel.getSupportedScenarios(getSource()));
		setScenario((Scenario) comboBoxScenario.getSelectedItem());
		setImpression((NFImpressionType) comboBoxImpression.getSelectedItem());
		if (rbFile.isSelected()) {
			List<NFPosition> positions = scenario.getAllowedPositions();
			if (positions.isEmpty()) {
				setPosition(NFPosition.UNKNOWN);
			} else {
				setPosition(positions.get(0));
			}
		}
		updateControls();
	}

	private void updateTaskResult(NBiometricTask task) {
		fingersTree.updateTree();
		if (!task.getSubjects().isEmpty() && !task.getSubjects().get(0).getFingers().isEmpty()) {
			List<NFinger> fingers = task.getSubjects().get(0).getFingers();
			fingersTree.setSelectedItem(fingersTree.getBiometricNode(fingers.get(fingers.size() - 1)));
		}
		Node selected = fingersTree.getSelectedItem();
		if (selected != null && selected.isGeneralizedNode()) {
			List<NBiometric> generalized = selected.getAllGeneralized();
			generalizeProgressView.setBiometrics(selected.getItems());
			generalizeProgressView.setGeneralized(generalized);
			generalizeProgressView.setSelected(CollectionUtils.getFirst(generalized));
			generalizeProgressView.setVisible(true);
		} else {
			generalizeProgressView.clear();
			generalizeProgressView.setVisible(false);
			if (selected != null) {
				setFinger((NFinger) CollectionUtils.getFirst(selected.getItems()));
			}

		}
	}

	private void fingerChanged() {
		if (finger == null) {
			if (fingersTree.getSelectedItem() != null) {
				setSelectedFinger(null);
			}
			fingerView.setFinger(null);
			updateScenarioComboBox(biometricModel.getSupportedScenarios(getSource()));
			setScenario((Scenario) comboBoxScenario.getSelectedItem());
			setImpression((NFImpressionType) comboBoxImpression.getSelectedItem());
		} else {
			if (!cancelTask.isBusy()) {
				setScenario(biometricModel.getCompatibleScenario(finger));
			}
			setPosition(finger.getPosition());
			if (biometricModel.getSupportedImpressionTypes(getSource(), position, scenario.getAttributes().contains(Attribute.ROLLED)).contains(finger.getImpressionType())) {
				setImpression(finger.getImpressionType());
			} else {
				setImpression(null);
			}
			if (!finger.equals(fingersTree.getSelectedItem())) {
				setSelectedFinger(finger);
			}
			fingerView.setFinger(finger);
		}
	}

	private void scenarioChanged() {
		if (!comboBoxScenario.getSelectedItem().equals(scenario)) {
			setSelectedScenario(scenario);
		}
		if (position == null) {
			updateImpressionComboBox(biometricModel.getSupportedImpressionTypes(getSource(), NFPosition.UNKNOWN, scenario.getAttributes().contains(Attribute.ROLLED)));
		} else {
			updateImpressionComboBox(biometricModel.getSupportedImpressionTypes(getSource(), position, scenario.getAttributes().contains(Attribute.ROLLED)));
		}
		if (fingerSelector.getScenario() != scenario) {
			updating = true;
			fingerSelector.setScenario(scenario);
			updating = false;
		}
	}

	private void positionChanged() {
		if ((finger == null) || (!finger.getPosition().equals(position))) {
			boolean fingerExists = false;
			for (NFinger f : biometricModel.getLocalSubject().getFingers()) {
				if (f.getPosition() == position) {
					fingerExists = true;
					setFinger(f);
					break;
				}
			}
			if (!fingerExists) {
				setFinger(null);
			}
		} else if (position == null) {
			setFinger(null);
			updateImpressionComboBox(biometricModel.getSupportedImpressionTypes(getSource(), NFPosition.UNKNOWN, scenario.getAttributes().contains(Attribute.ROLLED)));
		}
		setSelectedPosition(position);
	}

	private void impressionChanged() {
		setSelectedImpression(impression);
	}

	private void updateShowReturned() {
		NFrictionRidge ridge = fingerView.getFinger();
		cbShowBinarized.setEnabled(ridge != null && ridge.getBinarizedImage() != null);
		if (!cbShowBinarized.isEnabled() && cbShowBinarized.isSelected())
			cbShowBinarized.setSelected(false);
	}

	private void updateControls() {
		boolean idle = !cancelTask.isBusy();
		rbFile.setEnabled(idle);
		rbScanner.setEnabled(idle && (biometricModel.getClient().getFingerScanner() != null));
		rbTenPrintCard.setEnabled(idle);
		btnCapture.setEnabled(idle && (impression != null));
		fingerSelector.setEnabled(idle);
		fingersTree.setEnabled(idle);
		btnFinish.setEnabled(idle);
		btnRepeat.setEnabled(!idle);
		btnNext.setEnabled(!idle);
		btnCancel.setEnabled(!idle);
		btnForce.setEnabled(!idle && !cbAutomatic.isSelected());
		cbShowBinarized.setEnabled(idle);
		cbWithGeneralization.setEnabled(idle);
		cbAutomatic.setEnabled(idle && !rbFile.isSelected() && !rbTenPrintCard.isSelected());

		progressBar.setVisible(!idle);

		if (rbFile.isSelected()) {
			btnCapture.setText("Open image");
			comboBoxScenario.setEnabled(idle);
			comboBoxImpression.setEnabled(idle);
			showHint();
		} else if (rbScanner.isSelected()) {
			btnCapture.setText("Capture");
			comboBoxScenario.setEnabled(idle);
			comboBoxImpression.setEnabled(false);
			showHint();
		} else if (rbTenPrintCard.isSelected()) {
			btnCapture.setText("Capture");
			comboBoxScenario.setEnabled(false);
			comboBoxImpression.setEnabled(false);
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
		panelFingersSelector.setVisible((rbScanner.isSelected() || rbFile.isSelected())
				&& (scenario != Scenario.NONE)
				&& (scenario != Scenario.PLAIN_FINGER)
				&& (scenario != Scenario.ROLLED_FINGER));
		panelControlButtons.setVisible(rbScanner.isSelected());
	}

	private void showHint() {
		if (rbFile.isSelected()) {
			lblHint.setText("<html>Hint: Click on finger to select it<br/>or mark as missing if \"amputate\" is pressed.</html>");
		} else if (rbScanner.isSelected()) {
			lblHint.setText("Hint: Press \"amputate\" to select missing fingers.");
		} else {
			throw new AssertionError("This method should not be called if neither rbFile nor rbScanner is selected.");
		}
	}

	private void showStatus(String msg, Color color) {
		tpStatus.setText(msg);
		tpStatus.setBackground(color);
		tpStatus.setVisible(true);
		progressBar.setVisible(false);
	}

	private void showProgress(String msg) {
		progressBar.setString(msg);
		progressBar.setVisible(true);
		tpStatus.setVisible(false);
	}

	private void setSelectedScenario(Scenario s) {
		updating = true;
		comboBoxScenario.setSelectedItem(s);
		updating = false;
	}

	private void setSelectedFinger(NFinger f) {
		updating = true;
		fingersTree.setSelectedItem(fingersTree.getBiometricNode(f));
		updating = false;
	}

	private void setSelectedPosition(NFPosition p) {
		updating = true;
		if (p == null) {
			fingerSelector.setSelectedAll(false);
		} else {
			fingerSelector.setSelected(position, true);
		}
		updating = false;
	}

	private void setSelectedImpression(NFImpressionType i) {
		updating = true;
		comboBoxImpression.setSelectedItem(i);
		updating = false;
	}

	private void updateScenarioComboBox(List<Scenario> scenarios) {
		updating = true;
		Scenario selected = (Scenario) comboBoxScenario.getSelectedItem();
		comboBoxScenario.removeAllItems();
		for (Scenario s : scenarios) {
			comboBoxScenario.addItem(s);
		}
		comboBoxScenario.setSelectedItem(selected);
		if ((comboBoxScenario.getSelectedIndex() == -1) && !scenarios.isEmpty()) {
			comboBoxScenario.setSelectedIndex(0);
		}
		updating = false;
	}

	private void updateImpressionComboBox(List<NFImpressionType> impressions) {
		updating = true;
		NFImpressionType selected = (NFImpressionType) comboBoxImpression.getSelectedItem();
		comboBoxImpression.removeAllItems();
		for (NFImpressionType impr : impressions) {
			comboBoxImpression.addItem(impr);
		}
		comboBoxImpression.setSelectedItem(selected);
		if ((comboBoxImpression.getSelectedIndex() == -1) && !impressions.isEmpty()) {
			comboBoxImpression.setSelectedIndex(0);
		}
		updating = false;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setBiometricModel(CaptureFingerModel biometricModel) {
		if (biometricModel == null) {
			throw new NullPointerException("biometricModel");
		}
		this.biometricModel = biometricModel;
	}

	public void setBiometricController(CaptureBiometricController biometricController) {
		if (biometricController == null) {
			throw new NullPointerException("biometricController");
		}
		this.biometricController = biometricController;
	}

	@Override
	public Source getSource() {
		if (rbFile.isSelected()) {
			return Source.FILE;
		} else if (rbScanner.isSelected()) {
			return Source.DEVICE;
		} else if (rbTenPrintCard.isSelected()) {
			return Source.TEN_PRINT_CARD;
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
	}

	@Override
	public Scenario getScenario() {
		return scenario;
	}

	@Override
	public NFPosition getPosition() {
		return position;
	}

	@Override
	public NFImpressionType getImpressionType() {
		return impression;
	}

	@Override
	public EnumSet<NBiometricCaptureOption> getCaptureOptions() {
		if (cbAutomatic.isSelected()) {
			return EnumSet.noneOf(NBiometricCaptureOption.class);
		} else {
			return EnumSet.of(NBiometricCaptureOption.MANUAL);
		}
	}

	@Override
	public File getFile() {
		fcOpenImage.setCurrentDirectory(lastFile);
		fcOpenImage.setMultiSelectionEnabled(false);
		if (fcOpenImage.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			lastFile = fcOpenImage.getSelectedFile();
			if (!Settings.getInstance().getLastFingerFilePath().equals(lastFile.getAbsolutePath())) {
				Settings.getInstance().setLastFingerFilePath(lastFile.getAbsolutePath());
				Settings.getInstance().save();
			}
			showProgress("Extracting record. Please wait ...");
			return lastFile;
		} else {
			return null;
		}
	}

	@Override
	public File[] getFiles() {
		fcOpenImage.setCurrentDirectory(lastFile);
		fcOpenImage.setMultiSelectionEnabled(false);
		if (fcOpenImage.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			lastFile = fcOpenImage.getSelectedFile();
			if (!Settings.getInstance().getLastFingerFilePath().equals(lastFile.getAbsolutePath())) {
				Settings.getInstance().setLastFingerFilePath(lastFile.getAbsolutePath());
				Settings.getInstance().save();
			}
			showProgress("Extracting record. Please wait ...");
			return fcOpenImage.getSelectedFiles();
		} else {
			return null;
		}
	}

	@Override
	public boolean isGeneralize() {
		return cbWithGeneralization.isSelected();
	}

	@Override
	public void progress(NBiometricStatus status, String msg) {
		if ((status == NBiometricStatus.OK) || (status == NBiometricStatus.NONE)) {
			showStatus(msg, COLOR_OK);
		} else {
			showStatus(msg, COLOR_ERROR);
		}
	}

	@Override
	public void captureCompleted(final NBiometricStatus status, final NBiometricTask task) {
		cancelTask.setBusy(false);
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				if (rbScanner.isSelected()) {
					if (status == NBiometricStatus.OK) {
						showStatus("Fingers captured successfully", COLOR_OK);
						updateTaskResult(task);
					} else {
						showStatus("Capture failed: " + status, COLOR_ERROR);
					}
				} else if (rbFile.isSelected() || rbTenPrintCard.isSelected()) {
					if (status == NBiometricStatus.OK) {
						showStatus("Extraction completed successfully", COLOR_OK);
						updateTaskResult(task);
					} else {
						showStatus("Extraction failed: " + status, COLOR_ERROR);
					}
				} else {
					throw new AssertionError("A source radiobutton must be selected.");
				}
				updateControls();
			}
		});
	}

	@Override
	public void captureFailed(Throwable th, final String msg) {
		cancelTask.setBusy(false);
		if (th != null) {
			th.printStackTrace();
		}
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				updateControls();
				panelStatus.setVisible(true);
				showStatus("Extraction failed: " + msg, COLOR_ERROR);
			}
		});
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			fingersTree.setSubject(biometricModel.getLocalSubject());
			biometricModel.getClient().addPropertyChangeListener(biometricClientPropertyChanged);
			rbScanner.setSelected(true);
			cbShowBinarized.setVisible(biometricModel.getClient().isFingersReturnBinarizedImage());
			cbShowBinarized.setSelected(false);
			fingerView.setShownImage(ShownImage.ORIGINAL);
			panelStatus.setVisible(false);
			generalizeProgressView.clear();
			generalizeProgressView.setVisible(false);
			generalizeProgressView.addPropertyChangeListener(generalizeProgressViewPropertyChangeListener);
			fingerScannerChanged();
			for (NFPosition p : fingerSelector.getScenario().getAllowedPositions()) {
				fingerSelector.setMissing(p, biometricModel.getLocalSubject().getMissingFingers().contains(p));
			}
			fingerSelector.setMultipleSelection(false);
			fingerSelector.addChangeListener(this);
		}
	}

	@Override
	public void navigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			try {
				cancelTask.startAndWait();
			} catch (InterruptedException e) {
				Thread.currentThread().interrupt();
				e.printStackTrace();
				return;
			}
			setFinger(null);
			generalizeProgressView.removePropertyChangeListener(generalizeProgressViewPropertyChangeListener);
			biometricModel.getClient().removePropertyChangeListener(biometricClientPropertyChanged);
			fingerSelector.removeChangeListener(this);
			biometricController.finish();
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (rbScanner.equals(ev.getSource())) {
				sourceChanged();
			} else if (rbFile.equals(ev.getSource())) {
				sourceChanged();
			} else if (rbTenPrintCard.equals(ev.getSource())) {
				sourceChanged();
			} else if (btnCapture.equals(ev.getSource())) {
				panelStatus.setVisible(true);
				switch (getSource()) {
				case FILE:
					break;
				case DEVICE:
					showProgress("Starting capturing from scanner ...");
					break;
				case TEN_PRINT_CARD:
					break;
				default:
					throw new AssertionError("source");
				}
				cancelTask.setBusy(true);
				updateControls();
				biometricController.capture();
			} else if (btnRepeat.equals(ev.getSource())) {
				biometricController.repeat();
			} else if (btnNext.equals(ev.getSource())) {
				biometricController.skip();
			} else if (btnCancel.equals(ev.getSource())) {
				biometricController.cancel();
			} else if (btnForce.equals(ev.getSource())) {
				biometricController.force();
			} else if (cbShowBinarized.equals(ev.getSource())) {
				if (cbShowBinarized.isSelected()) {
					fingerView.setShownImage(ShownImage.RESULT);
				} else {
					fingerView.setShownImage(ShownImage.ORIGINAL);
				}
			} else if (btnFinish.equals(ev.getSource())) {
				getPageController().navigateToStartPage();
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void itemStateChanged(final ItemEvent ev) {
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				if (!updating) {
					if (ev.getStateChange() == ItemEvent.SELECTED) {
						if (comboBoxScenario.equals(ev.getSource())) {
							setFinger(null);
							setScenario((Scenario) comboBoxScenario.getSelectedItem());
							if (rbFile.isSelected()) {
								List<NFPosition> positions = scenario.getAllowedPositions();
								if (positions.isEmpty()) {
									setPosition(NFPosition.UNKNOWN);
								} else {
									setPosition(positions.get(0));
								}
							} else {
								setFinger(null);
							}
							updateControls();
						} else if (comboBoxImpression.equals(ev.getSource())) {
							setFinger(null);
							setImpression((NFImpressionType) comboBoxImpression.getSelectedItem());
						}
					}
				}
			}
		});
	}

	private PropertyChangeListener currentBiometricPropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent evt) {
			if (evt.getSource().equals(currentBiometric) && evt.getPropertyName().equals("Status")) {
				final NBiometricStatus status = biometricModel.getClient().getCurrentBiometric().getStatus();
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						if ((status == NBiometricStatus.OK) || (status == NBiometricStatus.NONE)) {
							showStatus("Status: " + status, COLOR_OK);
						} else {
							showStatus("Status: " + status, COLOR_ERROR);
						}
					}
				});
			}
		}
	};

	private PropertyChangeListener biometricClientPropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent ev) {
			if ("CurrentBiometric".equals(ev.getPropertyName())) {
					SwingUtils.runOnEDT(new Runnable() {
						@Override
						public void run() {
							if (currentBiometric != null) {
								currentBiometric.removePropertyChangeListener(currentBiometricPropertyChanged);
							}
							NFinger current = (NFinger) biometricModel.getClient().getCurrentBiometric();
							if (current != null) {
								Node node = fingersTree.getBiometricNode(current);
				//					OnSelectedPositionChanged((Scenario)cbScenario.SelectedItem, current.Position);
								fingersTree.setSelectedItem(node);
								if (isGeneralize() && node != null) {
									generalizeProgressView.setBiometrics(node.getItems());
									generalizeProgressView.setGeneralized(node.getAllGeneralized());
									generalizeProgressView.setSelected(current);
									generalizeProgressView.setVisible(true);
	//						_titlePrefix = string.Format("Capturing {0} ({1} of {2}). ", current.Position, Array.IndexOf(node.Items, current) + 1, SettingsManager.FingersGeneralizationRecordCount);
								}
								setFinger(current);
								currentBiometric = current;
								currentBiometric.addPropertyChangeListener(currentBiometricPropertyChanged);
							} else {
								generalizeProgressView.clear();
							}
						}
					});
			} else if ("FingerScanner".equals(ev.getPropertyName())) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						fingerScannerChanged();
					}
				});
			}
		}
	};

	private PropertyChangeListener fingersTreePropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(final PropertyChangeEvent ev) {
			if (ev.getPropertyName().equals("SelectedItem")) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						if (!updating) {
							Node selection = fingersTree.getSelectedItem();

							NFinger first = selection != null ? (NFinger)CollectionUtils.getFirst(selection.getItems()) : null;
							generalizeProgressView.clear();
							if (selection != null && selection.isGeneralizedNode()) {
								List<NBiometric> generalized = selection.getAllGeneralized();
								generalizeProgressView.setBiometrics(selection.getItems());
								generalizeProgressView.setGeneralized(generalized);
								generalizeProgressView.setSelected(CollectionUtils.getFirst(generalized));
								generalizeProgressView.setVisible(true);
							} else {
								generalizeProgressView.setVisible(false);
							}

							fingerView.setFinger(first);
//							OnSelectedPositionChanged((Scenario)cbScenario.SelectedItem, position, NBiometricTypes.IsImpressionTypeRolled(impression));
//							lblStatus.Visible = lblStatus.Visible && IsBusy();
							updateShowReturned();
							updateControls();
						}
					}
				});
			}
		}
	};

	@Override
	public void stateChanged(SelectorChangeEvent ev) {
		if (fingerSelector.equals(ev.getSource())) {
			if (ev.getAction() == Action.MISSING) {
				for (NFPosition p : NFPosition.values()) {
					if (fingerSelector.getMissingPositions().contains(p) && (!biometricModel.getLocalSubject().getMissingFingers().contains(p))) {
						biometricModel.getLocalSubject().getMissingFingers().add(p);
					} else if (!fingerSelector.getMissingPositions().contains(p) && (biometricModel.getLocalSubject().getMissingFingers().contains(p))) {
						biometricModel.getLocalSubject().getMissingFingers().remove(p);
					}
				}
			} else if (ev.getAction() == Action.SELECT) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						if (!updating) {
							if (fingerSelector.getSelectedPositions().isEmpty()) {
								setPosition(null);
							} else {
								setPosition(fingerSelector.getSelectedPositions().get(0));
							}
						}
					}
				});
			} else if (ev.getAction() == Action.RESET) {
				biometricModel.getLocalSubject().getMissingFingers().clear();
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						if (!updating) {
							setPosition(null);
						}
					}
				});
			}
		}
	}

	@Override
	public void nodeAdded(NodeChangeEvent ev) {
		if (ev.getNode() instanceof DefaultMutableTreeNode) {
			if (currentBiometric == null) {
				final Object nodeObject = ((DefaultMutableTreeNode) ev.getNode()).getUserObject();
				if (nodeObject instanceof NFinger) {
					SwingUtils.runOnEDT(new Runnable() {
						@Override
						public void run() {
							setFinger((NFinger) nodeObject);
						}
					});
				}
			}
			if (rbTenPrintCard.isSelected()) {
				showProgress("Extracting record. Please wait ...");
			}
		}
	}

	@Override
	public void nodeRemoved(NodeChangeEvent ev) {
		// Do nothing.
	}

	@Override
	public void updateGeneralization(List<NBiometric> biometrics, List<NBiometric> generalized) {
		NBiometric selected = CollectionUtils.getFirst(biometrics);
		generalizeProgressView.setVisible(true);
		generalizeProgressView.setBiometrics(biometrics);
		generalizeProgressView.setGeneralized(generalized);
		generalizeProgressView.setSelected(selected);
		setFinger((NFinger)selected);
		fingersTree.updateTree();
		fingersTree.setSelectedItem(fingersTree.getBiometricNode(selected));
	}

}
