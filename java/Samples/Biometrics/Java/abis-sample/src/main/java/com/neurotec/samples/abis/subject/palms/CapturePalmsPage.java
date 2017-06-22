package com.neurotec.samples.abis.subject.palms;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.SystemColor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.File;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JSlider;
import javax.swing.JSpinner;
import javax.swing.JTextPane;
import javax.swing.SpinnerNumberModel;
import javax.swing.SwingConstants;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
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
import com.neurotec.biometrics.NPalm;
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
import com.neurotec.swing.NView;
import com.neurotec.swing.NViewZoomSlider;

public class CapturePalmsPage extends Page implements CapturePalmView, PageNavigationListener, ActionListener, ItemListener, SelectorChangeListener, NodeChangeListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final Color COLOR_OK = new Color(0, 128, 0);
	private static final Color COLOR_ERROR = new Color(255, 0, 0);

	// ===========================================================
	// Private fields
	// ===========================================================

	private CapturePalmModel biometricModel;
	private CaptureBiometricController biometricController;
	private NPalm currentBiometric;

	private ImageThumbnailFileChooser fcOpenImage;
	private File lastFile;
	private SubjectTree palmsTree;
	private NFingerView palmView;
	private HandSegmentSelector palmSelector;

	private final LockingTask cancelTask = new LockingTask() {

		@Override
		public void performTask() {
			biometricController.cancel();
		}

	};

	private ButtonGroup bgSource;
	private JButton btnCancel;
	private JButton btnCapture;
	private JButton btnFinish;
	private JButton btnForce;
	private JCheckBox cbAutomatic;
	private JCheckBox cbShowBinarized;
	private JCheckBox cbWithGeneralization;
	private JComboBox comboBoxImpression;
	private JComboBox comboBoxPosition;
	private JPanel panelAutomatic;
	private JPanel panelCenter;
	private JPanel panelCenterBottom;
	private JPanel panelCenterBottomOuter;
	private JPanel panelControlButtons;
	private JPanel panelControls;
	private JPanel panelFingersSelector;
	private JPanel panelFinish;
	private JPanel panelImpression;
	private JPanel panelLeft;
	private JPanel panelLeftCenter;
	private JPanel panelOptions;
	private JPanel panelSelectedPosition;
	private JPanel panelSource;
	private JPanel panelStatus;
	private JPanel panelZoom;
	private JProgressBar progressBar;
	private JRadioButton rbFile;
	private JRadioButton rbScanner;
	private JScrollPane spFingerView;
	private JTextPane tpStatus;
	private GeneralizeProgressView generalizeProgressView;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CapturePalmsPage(PageNavigationController pageController) {
		super("New...", pageController);
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		setLayout(new BorderLayout());
		panelLeft = new JPanel();
		panelLeft.setLayout(new BoxLayout(panelLeft, BoxLayout.Y_AXIS));
		add(panelLeft, BorderLayout.WEST);
		{
			panelSource = new JPanel();
			panelSource.setBorder(BorderFactory.createTitledBorder("Source"));
			panelSource.setLayout(new BoxLayout(panelSource, BoxLayout.Y_AXIS));
			panelLeft.add(panelSource);
			{
				bgSource = new ButtonGroup();
				{
					rbScanner = new JRadioButton();
					rbScanner.setText("Scanner");
					rbScanner.addActionListener(this);
					panelSource.add(rbScanner);
					bgSource.add(rbScanner);
				}
				{
					rbFile = new JRadioButton();
					rbFile.setText("File");
					rbFile.addActionListener(this);
					panelSource.add(rbFile);
					bgSource.add(rbFile);
				}
			}
			{
				panelOptions = new JPanel();
				panelOptions.setBorder(BorderFactory.createTitledBorder("Options"));
				panelOptions.setAlignmentX(0.0F);
				panelOptions.setLayout(new BoxLayout(panelOptions, BoxLayout.Y_AXIS));
				panelLeft.add(panelOptions);
				{
					panelImpression = new JPanel();
					panelImpression.setLayout(new FlowLayout(FlowLayout.LEADING));
					panelOptions.add(panelImpression);
					{
						JLabel lblImpression = new JLabel("Impression: ");
						panelImpression.add(lblImpression);
					}
					{
						comboBoxImpression = new JComboBox();
						comboBoxImpression.setModel(new DefaultComboBoxModel(new String[] {"Item 1", "Item 2", "Item 3", "Item 4"}));
						panelImpression.add(comboBoxImpression);
					}
				}
				{
					panelAutomatic = new JPanel();
					panelAutomatic.setLayout(new FlowLayout(FlowLayout.LEADING));
					panelOptions.add(panelAutomatic);
					{
						cbAutomatic = new JCheckBox();
						cbAutomatic.setSelected(true);
						cbAutomatic.setText("Capture automatically");
						panelAutomatic.add(cbAutomatic);
					}
				}
				{

					JPanel panelGeneralization = new JPanel();
					panelGeneralization.setLayout(new FlowLayout(FlowLayout.LEADING));
					panelOptions.add(panelGeneralization);
					cbWithGeneralization = new JCheckBox();
					cbWithGeneralization.setText("With generalization");
					panelGeneralization.add(cbWithGeneralization);
				}
			}
			{
				panelLeftCenter = new JPanel();
				panelLeftCenter.setAlignmentX(0.0F);
				panelLeftCenter.setLayout(new BorderLayout());
				panelLeft.add(panelLeftCenter);
				{
					panelControls = new JPanel();
					panelControls.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
					panelControls.setLayout(new BoxLayout(panelControls, BoxLayout.Y_AXIS));
					panelLeftCenter.add(panelControls, BorderLayout.NORTH);
					{
						panelFingersSelector = new JPanel();
						panelFingersSelector.setAlignmentX(0.0F);
						panelFingersSelector.setLayout(new BorderLayout());
						panelControls.add(panelFingersSelector);
						{
							palmSelector = new HandSegmentSelector();
							palmSelector.setMultipleSelection(false);
							palmSelector.setPreferredSize(new Dimension(210, 115));
							palmSelector.setScenario(Scenario.ALL_PALMS);
							panelFingersSelector.add(palmSelector, BorderLayout.WEST);
						}
						{
							panelSelectedPosition = new JPanel();
							panelSelectedPosition.setAlignmentX(0.0F);
							panelSelectedPosition.setLayout(new FlowLayout(FlowLayout.LEADING));
							panelFingersSelector.add(panelSelectedPosition, BorderLayout.SOUTH);
							{

								JLabel jLabel1 = new JLabel("Selected position: ");
								panelSelectedPosition.add(jLabel1);
							}
							{
								comboBoxPosition = new JComboBox();
								comboBoxPosition.setModel(new DefaultComboBoxModel(new String[] {"Item 1", "Item 2", "Item 3", "Item 4"}));
								comboBoxPosition.addItemListener(this);
								panelSelectedPosition.add(comboBoxPosition);
							}
						}
						{
							btnCapture = new JButton("Capture");
							btnCapture.setHorizontalAlignment(SwingConstants.LEFT);
							btnCapture.addActionListener(this);
							panelControls.add(btnCapture);
						}
						{
							palmsTree = new SubjectTree();
							palmsTree.setBorder(BorderFactory.createLineBorder(new Color(0, 0, 0)));
							palmsTree.addPropertyChangeListener(palmsTreePropertyChanged);
							palmsTree.setShownTypes(EnumSet.of(NBiometricType.PALM));
							palmsTree.setShowBiometricsOnly(true);
							palmsTree.addNodeChangeListener(this);
							panelLeftCenter.add(palmsTree, BorderLayout.CENTER);
						}
					}
				}
			}
		}
		{
			panelCenter = new JPanel();
			panelCenter.setLayout(new BorderLayout());
			add(panelCenter, BorderLayout.CENTER);
			{
				{
					spFingerView = new JScrollPane();
					panelCenter.add(spFingerView, BorderLayout.CENTER);
					{
						palmView = new NFingerView();
						palmView.setAutofit(true);
						spFingerView.setViewportView(palmView);
					}
				}
				{
					panelCenterBottomOuter = new JPanel();
					panelCenterBottomOuter.setLayout(new BoxLayout(panelCenterBottomOuter, BoxLayout.Y_AXIS));
					panelCenter.add(panelCenterBottomOuter, BorderLayout.SOUTH);
					{
						JPanel panelGeneralization = new JPanel();
						panelCenterBottomOuter.add(panelGeneralization);
						panelGeneralization.setLayout(new BorderLayout());
						generalizeProgressView = new GeneralizeProgressView();
						generalizeProgressView.setView(palmView);
						panelGeneralization.add(generalizeProgressView);
					}
					{
						panelStatus = new JPanel();
						panelStatus.setLayout(new BorderLayout());
						panelStatus.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
						panelCenterBottomOuter.add(panelStatus);
						{
							progressBar = new JProgressBar();
							progressBar.setIndeterminate(true);
							progressBar.setPreferredSize(new Dimension(0, 23));
							progressBar.setStringPainted(true);
							panelStatus.add(progressBar, BorderLayout.NORTH);
						}
						{
							tpStatus = new JTextPane();
							StyledDocument doc = tpStatus.getStyledDocument();
							SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
							StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
							StyleConstants.setLeftIndent(styleAttributes, 3);
							StyleConstants.setRightIndent(styleAttributes, 3);
							StyleConstants.setFontSize(styleAttributes, 16);
							StyleConstants.setForeground(styleAttributes, SystemColor.menu);
							doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);
							tpStatus.setEditable(false);
							panelStatus.add(tpStatus, BorderLayout.SOUTH);
						}
					}
					{
						panelControlButtons = new JPanel();
						panelCenterBottomOuter.add(panelControlButtons);
						{
							btnForce = new JButton();
							btnForce.setText("Force");
							btnForce.setPreferredSize(new Dimension(70, 23));
							btnForce.addActionListener(this);
							panelControlButtons.add(btnForce);

						}
						{
							btnCancel = new JButton();
							btnCancel.setText("Cancel");
							btnCancel.addActionListener(this);
							btnCancel.setPreferredSize(new Dimension(70, 23));
							panelControlButtons.add(btnCancel);
						}
					}
					{
						panelCenterBottom = new JPanel();
						panelCenterBottom.setLayout(new BorderLayout());
						{
							panelFinish = new JPanel();
							panelFinish.setLayout(new FlowLayout(FlowLayout.TRAILING));
							panelCenterBottom.add(panelFinish, BorderLayout.EAST);
							{
								btnFinish = new JButton();
								btnFinish.setText("Finish");
								btnFinish.addActionListener(this);
								panelFinish.add(btnFinish);
							}
						}
						{
							panelZoom = new JPanel();
							panelZoom.setLayout(new FlowLayout(FlowLayout.LEFT));
							panelCenterBottom.add(panelZoom, BorderLayout.WEST);
							panelCenterBottomOuter.add(panelCenterBottom);
							{
								NViewZoomSlider horizontalZoomSlider = new NViewZoomSlider();
								horizontalZoomSlider.setView(palmView);
								panelZoom.add(horizontalZoomSlider);
							}
							{
								cbShowBinarized = new JCheckBox("Show binarized");
								cbShowBinarized.addActionListener(this);
								panelZoom.add(cbShowBinarized);
							}
						}
					}
				}
			}
		}
		lastFile = new File(Settings.getInstance().getLastPalmFilePath());
		fcOpenImage = new ImageThumbnailFileChooser();
		fcOpenImage.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcOpenImage.setMultiSelectionEnabled(false);

	}

	private void updateControls() {
		if (rbFile.isSelected()) {
			btnCapture.setText("Open image");
			panelControlButtons.setVisible(false);
		} else if (rbScanner.isSelected()) {
			btnCapture.setText("Capture");
			panelControlButtons.setVisible(true);
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
		updateComboBoxes();

		boolean idle = !cancelTask.isBusy();
		rbScanner.setEnabled((biometricModel.getClient().getPalmScanner() != null) && idle);
		rbFile.setEnabled(idle);
		comboBoxImpression.setEnabled(idle && rbFile.isSelected());
		palmSelector.setEnabled(idle);
		comboBoxPosition.setEnabled(idle);
		palmsTree.setEnabled(idle);
		btnCapture.setEnabled(idle);
		btnCancel.setEnabled(!idle && rbScanner.isSelected());
		btnFinish.setEnabled(idle);
		btnForce.setEnabled(!idle && !cbAutomatic.isSelected());

		cbAutomatic.setEnabled(idle && rbScanner.isSelected());
		progressBar.setVisible(!idle);
	}

	private void updateComboBoxes() {
		Object selectedPosition = comboBoxPosition.getSelectedItem();
		Object selectedImpression = comboBoxImpression.getSelectedItem();
		comboBoxImpression.removeAllItems();
		palmSelector.setSelected(NFPosition.UNKNOWN_PALM, true);
		comboBoxPosition.removeAllItems();
		if (rbFile.isSelected()) {
			for (NFImpressionType impression : biometricModel.getPalmImpressionTypes()) {
				comboBoxImpression.addItem(impression);
			}
			for (NFPosition position : palmSelector.getScenario().getAllowedPositions()) {
				comboBoxPosition.addItem(position);
			}
		} else if (rbScanner.isSelected()) {
			NFScanner scanner = biometricModel.getClient().getPalmScanner();
			for (NFImpressionType impression : scanner.getSupportedImpressionTypes()) {
				if (impression.isPalm()) {
					comboBoxImpression.addItem(impression);
				}
			}
			List<NFPosition> supportedPositions = Arrays.asList(scanner.getSupportedPositions());
			for (NFPosition position : palmSelector.getScenario().getAllowedPositions()) {
				if (supportedPositions.contains(position)) {
					comboBoxPosition.addItem(position);
				}
			}
		} else {
			throw new AssertionError("A radiobutton must be selected.");
		}
		comboBoxImpression.setSelectedItem(selectedImpression);
		comboBoxPosition.setSelectedItem(selectedPosition);
		if (comboBoxPosition.getSelectedIndex() == -1) {
			comboBoxPosition.setSelectedIndex(0);
		}
		if (comboBoxImpression.getSelectedIndex() == -1) {
			comboBoxImpression.setSelectedIndex(0);
		}
	}

	private void palmScannerChanged() {
		NFScanner device = biometricModel.getClient().getPalmScanner();
		if ((device == null) || !device.isAvailable()) {
			if (rbScanner.isSelected()) {
				rbFile.setSelected(true);
			}
			rbScanner.setText("Scanner (Not connected)");
		} else {
			rbScanner.setText(String.format("Scanner (%s)", device.getDisplayName()));
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

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setBiometricModel(CapturePalmModel biometricModel) {
		if (biometricModel == null) throw new NullPointerException("biometricModel");
		this.biometricModel = biometricModel;
	}

	public void setBiometricController(CaptureBiometricController biometricController) {
		if (biometricController == null) throw new NullPointerException("biometricController");
		this.biometricController = biometricController;
	}

	@Override
	public Source getSource() {
		if (rbFile.isSelected()) {
			return Source.FILE;
		} else if (rbScanner.isSelected()) {
			return Source.DEVICE;
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
	}

	@Override
	public NFPosition getPosition() {
		return (NFPosition) comboBoxPosition.getSelectedItem();
	}

	@Override
	public NFImpressionType getImpressionType() {
		return (NFImpressionType) comboBoxImpression.getSelectedItem();
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
		if (fcOpenImage.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			lastFile = fcOpenImage.getSelectedFile();
			if (!Settings.getInstance().getLastPalmFilePath().equals(lastFile.getAbsolutePath())) {
				Settings.getInstance().setLastPalmFilePath(lastFile.getAbsolutePath());
				Settings.getInstance().save();
			}
			showProgress("Extracting record. Please wait ...");
			return lastFile;
		} else {
			return null;
		}
	}

	@Override
	public boolean isGeneralize() {
		return cbWithGeneralization.isSelected();
	}

	@Override
	public void captureCompleted(final NBiometricStatus status, final NBiometricTask task) {
		cancelTask.setBusy(false);
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				List<NPalm> palms = biometricModel.getLocalSubject().getPalms();
				palmsTree.setSelectedItem(palmsTree.getBiometricNode(palms.get(palms.size() - 1)));
			}
		});
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				palmsTree.updateTree();
				generalizeProgressView.setEnableMouseSelection(true);
				if (status == NBiometricStatus.OK) {
					showStatus("Extraction completed successfully", COLOR_OK);
					Node selected = palmsTree.getSelectedItem();
					if (selected != null && selected.isGeneralizedNode()) {
						List<NBiometric> generalized = selected.getAllGeneralized();
						generalizeProgressView.setBiometrics(selected.getItems());
						generalizeProgressView.setGeneralized(generalized);
						NBiometric selectedItem = null;
						selectedItem = CollectionUtils.getFirst(generalized);
						if (selectedItem == null) {
							selectedItem = CollectionUtils.getFirst(selected.getItems());
						}
						generalizeProgressView.setSelected(selectedItem);
					} else {
						generalizeProgressView.clear();
						generalizeProgressView.setVisible(false);
						if (selected != null) {
							try {
								palmView.setFinger((NPalm) CollectionUtils.getFirst(selected.getItems()));
							} catch (OutOfMemoryError e) {
								e.printStackTrace();
								updateControls();
								MessageUtils.showError(CapturePalmsPage.this, "Not enough memory", "System does not have enough memory to proceed. Please try a smaller palm image.");
								return;
							}
						}
					}
				} else {
					palmsTree.setSelectedItem(null);
					showStatus("Extraction failed: " + status, COLOR_ERROR);
				}
				updateControls();
			}
		});
	}

	@Override
	public void captureFailed(Throwable th, final String msg) {
		cancelTask.setBusy(false);
		if (th != null) {
			if ((th.getMessage() != null) && th.getMessage().contains("OutOfMemoryError")) {
				MessageUtils.showError(this, "Not enough memory", "Not enough memory to load this image. Please select a smaller one.");
			} else {
				MessageUtils.showError(this, th);
			}
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
			palmsTree.setSubject(biometricModel.getLocalSubject());

			biometricModel.getClient().addPropertyChangeListener(biometricClientPropertyChanged);

			rbScanner.setSelected(true);
			palmSelector.addChangeListener(this);
			cbShowBinarized.setVisible(biometricModel.getClient().isPalmsReturnBinarizedImage());
			cbShowBinarized.setSelected(false);
			palmView.setShownImage(ShownImage.ORIGINAL);
			panelStatus.setVisible(false);
			palmScannerChanged();
			updateControls();

			generalizeProgressView.clear();
			generalizeProgressView.setVisible(false);
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
			palmsTree.setSubject(null);
			palmView.setFinger(null);
			biometricModel.getClient().removePropertyChangeListener(biometricClientPropertyChanged);
			palmSelector.removeChangeListener(this);
			biometricController.finish();
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (rbFile.equals(ev.getSource())) {
				updateControls();
			} else if (rbScanner.equals(ev.getSource())) {
				updateControls();
			} else if (btnCapture.equals(ev.getSource())) {
				palmsTree.setSelectedItem(null);
				panelStatus.setVisible(true);
				Source source = getSource();
				if (source == Source.FILE) {
					showProgress("Waiting for image ...");
				} else if (source == Source.DEVICE) {
					showProgress("Starting capturing from scanner ...");
				} else {
					throw new AssertionError("A source radiobutton must be selected.");
				}
				cancelTask.setBusy(true);
				updateControls();
				biometricController.capture();
			} else if (btnCancel.equals(ev.getSource())) {
				biometricController.cancel();
			} else if (btnForce.equals(ev.getSource())) {
				biometricController.force();
			} else if (cbShowBinarized.equals(ev.getSource())) {
				if (cbShowBinarized.isSelected()) {
					palmView.setShownImage(ShownImage.RESULT);
				} else {
					palmView.setShownImage(ShownImage.ORIGINAL);
				}
			} else if (btnFinish.equals(ev.getSource())) {
				getPageController().navigateToStartPage();
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	private PropertyChangeListener biometricClientPropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent ev) {
			if ("PalmScanner".equals(ev.getPropertyName())) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						palmScannerChanged();
						updateControls();
					}
				});
			} else if ("CurrentBiometric".equals(ev.getPropertyName())) {
				if (currentBiometric != null) {
					currentBiometric.removePropertyChangeListener(onPalmPropertyChanged);
				}
				NPalm current = (NPalm) biometricModel.getClient().getCurrentBiometric();
				if (current != null) {
					Node node = palmsTree.getBiometricNode(current);
					if (isGeneralize() && node != null) {
						generalizeProgressView.setBiometrics(node.getItems());
						generalizeProgressView.setGeneralized(node.getAllGeneralized());
						generalizeProgressView.setSelected(current);
						generalizeProgressView.setVisible(true);
					}
					palmView.setFinger(current);
					currentBiometric = current;
					currentBiometric.addPropertyChangeListener(onPalmPropertyChanged);
					SwingUtils.runOnEDT(new Runnable() {
						@Override
						public void run() {
							palmsTree.setSelectedItem(palmsTree.getBiometricNode(currentBiometric));
						}
					});
				} else {
					generalizeProgressView.clear();
				}
			}
		}
	};

	private PropertyChangeListener onPalmPropertyChanged = new PropertyChangeListener() {
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

	private PropertyChangeListener palmsTreePropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(final PropertyChangeEvent ev) {
			if (ev.getPropertyName().equals("SelectedItem")) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						Node selection = palmsTree.getSelectedItem();
						NPalm first = selection != null ? (NPalm)CollectionUtils.getFirst(selection.getItems()) : null;
						generalizeProgressView.clear();
						if (selection != null && selection.isGeneralizedNode()) {
							List<NBiometric> generalized = selection.getAllGeneralized();
							generalizeProgressView.setBiometrics(selection.getItems());
							generalizeProgressView.setGeneralized(generalized);
							NBiometric selected = CollectionUtils.getFirst(generalized);
							generalizeProgressView.setSelected(selected != null ? selected : first);
							generalizeProgressView.setVisible(true);
						} else {
							generalizeProgressView.setVisible(false);
						}
						try {
							palmView.setFinger(first);
						} catch (OutOfMemoryError e) {
							e.printStackTrace();
							MessageUtils.showError(CapturePalmsPage.this, "Not enough memory", "System does not have enough memory to proceed. Please try a smaller palm image.");
							updateControls();
							return;
						}
						cbShowBinarized.setEnabled(first != null && first.getBinarizedImage() != null);
						if (cbShowBinarized.isSelected() && (first == null || first.getBinarizedImage() == null)) {
							cbShowBinarized.setSelected(false);
						}
					}
				});
			}
		}
	};

	@Override
	public void itemStateChanged(ItemEvent ev) {
		if (ev.getStateChange() == ItemEvent.SELECTED) {
			if (comboBoxPosition.equals(ev.getSource())) {
				if (comboBoxPosition.getSelectedItem() == null) {
					if (!palmSelector.getSelectedPositions().isEmpty()) {
						palmSelector.setSelected(NFPosition.UNKNOWN_PALM, true);
					}
				} else {
					if (palmSelector.getSelectedPositions().isEmpty() || (palmSelector.getSelectedPositions().get(0) != comboBoxPosition.getSelectedItem())) {
						palmSelector.setSelected((NFPosition) comboBoxPosition.getSelectedItem(), true);
					}
				}
			}
		}
	}

	@Override
	public void stateChanged(SelectorChangeEvent ev) {
		if (palmSelector.equals(ev.getSource())) {
			if (ev.getAction() == Action.SELECT) {
				if (palmSelector.getSelectedPositions().isEmpty()) {
					if (comboBoxPosition.getSelectedItem() != null) {
						comboBoxPosition.setSelectedItem(null);
					}
				} else {
					if (!palmSelector.getSelectedPositions().isEmpty() || (comboBoxPosition.getSelectedItem() != palmSelector.getSelectedPositions().get(0))) {
						comboBoxPosition.setSelectedItem(palmSelector.getSelectedPositions().get(0));
					}
				}
			} else if (ev.getAction() == Action.RESET) {
				if (comboBoxPosition.getSelectedItem() != null) {
					comboBoxPosition.setSelectedItem(null);
				}
			}
		}
	}

	@Override
	public void nodeAdded(final NodeChangeEvent ev) {
		if (ev.getNode() instanceof DefaultMutableTreeNode) {
			if (currentBiometric == null) {
				final Object nodeObject = ((DefaultMutableTreeNode) ev.getNode()).getUserObject();
				if (nodeObject instanceof NPalm) {
					SwingUtils.runOnEDT(new Runnable() {
						@Override
						public void run() {
							palmsTree.setSelectedItem(palmsTree.getNodeFor(nodeObject));
						}

					});
				}
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
		palmView.setFinger((NPalm)selected);
		palmsTree.updateTree();
		palmsTree.setSelectedItem(palmsTree.getBiometricNode(selected));

	}

}
