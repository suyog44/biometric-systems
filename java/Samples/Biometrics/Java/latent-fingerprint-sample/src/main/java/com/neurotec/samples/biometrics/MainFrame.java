package com.neurotec.samples.biometrics;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.Graphics;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.Insets;
import java.awt.Point;
import java.awt.Rectangle;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.image.BufferedImage;
import java.io.File;
import java.text.ParseException;
import java.util.EnumSet;
import java.util.List;
import java.util.concurrent.ExecutionException;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JCheckBoxMenuItem;
import javax.swing.JComboBox;
import javax.swing.JComponent;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JPopupMenu;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JSeparator;
import javax.swing.JSlider;
import javax.swing.JSplitPane;
import javax.swing.JToolBar;
import javax.swing.SwingConstants;
import javax.swing.SwingWorker;
import javax.swing.border.EmptyBorder;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;
import javax.swing.filechooser.FileFilter;
import javax.swing.plaf.basic.BasicSliderUI;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFAttributes;
import com.neurotec.biometrics.NFCore;
import com.neurotec.biometrics.NFDelta;
import com.neurotec.biometrics.NFDoubleCore;
import com.neurotec.biometrics.NFMinutia;
import com.neurotec.biometrics.NFMinutiaFormat;
import com.neurotec.biometrics.NFMinutiaNeighbor;
import com.neurotec.biometrics.NFMinutiaType;
import com.neurotec.biometrics.NFRecord;
import com.neurotec.biometrics.NFRidgeCountsType;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NMatchingDetails;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSubject;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.NImageRotateFlipType;
import com.neurotec.images.NImages;
import com.neurotec.images.NPixelFormat;
import com.neurotec.images.NRGB;
import com.neurotec.images.processing.NRGBIP;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.jna.HNObject;
import com.neurotec.lang.NCore;
import com.neurotec.lang.NotActivatedException;
import com.neurotec.licensing.NLicense;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Settings;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.AboutBox;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.swing.AddFeaturesTool;
import com.neurotec.biometrics.swing.FeatureAddEvent;
import com.neurotec.biometrics.swing.FeatureAddListener;
import com.neurotec.biometrics.swing.MinutiaSelectionEvent;
import com.neurotec.biometrics.swing.MinutiaSelectionListener;
import com.neurotec.biometrics.swing.NFingerView;
import com.neurotec.biometrics.swing.NFingerViewBase.ShownImage;
import com.neurotec.biometrics.swing.PointerTool;
import com.neurotec.biometrics.swing.RectangleSelectionTool;
import com.neurotec.util.NIndexPair;

public final class MainFrame extends JFrame implements ActionListener, ChangeListener, FeatureAddListener, MainFrameEventListener, LongTaskInterface {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;
	private static final int DEFAULT_ZOOM_FACTOR = 5;

	// ==============================================
	// Private static enums
	// ==============================================

	private static enum AddMode {
		NONE, END_MINUTIA, BIFURCATION_MINUTIA, DELTA, CORE, DOUBLE_CORE
	}

	// ==============================================
	// Private fields
	// ==============================================

	private NBiometricClient biometricClient;

	private NImage imageLeft;
	private NImage imageLeftOriginal;
	private NImage imageRight;
	private NImage binarizedImage;

	private NFRecord record;

	private double[] brightness = new double[3];
	private double[] contrast = new double[3];
	private boolean invert;
	private boolean convertToGrayscale;
	private boolean processingRequested;

	private final float[] zoomFactors = { .25f, .33f, .50f, .66f, .80f, 1f, 1.25f, 1.5f, 2.0f, 2.5f, 3.0f };

	private int leftZoomFactor = DEFAULT_ZOOM_FACTOR;
	private int rightZoomFactor = DEFAULT_ZOOM_FACTOR;

	private float checkingHorzResolution;
	private float checkingVertResolution;
	private NImage checkingImage;
	private boolean isCheckingLeft;
	private AddMode addMode = AddMode.NONE;
	private GridBagConstraints c = new GridBagConstraints();

	private Settings settings = Settings.getDefault("LatentFingerprintSample");

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JFileChooser openFileDialog;
	private JFileChooser saveTemplateDialog;
	private JFileChooser saveImageDialog;

	private NFingerView nfViewLeft;
	private NFingerView nfViewRight;

	private JButton btnExtract;
	private JButton btnMatch;

	private JLabel lblLatentImageSize;
	private JLabel lblLatentImageResolution;
	private JLabel lblReferenceImageSize;
	private JLabel lblReferenceImageResolution;
	private JLabel lblLeftFileName;
	private JLabel lblRightFileName;

	private JCheckBox chkInvert;
	private JCheckBox chkToGray;
	private JCheckBox chkGroupBrightness;
	private JCheckBox chkGroupContrast;
	private JSlider brightnessSliderR;
	private JSlider brightnessSliderG;
	private JSlider brightnessSliderB;

	private JLabel lblBrightnessValueR;
	private JLabel lblBrightnessValueG;
	private JLabel lblBrightnessValueB;

	private JSlider contrastSliderR;
	private JSlider contrastSliderG;
	private JSlider contrastSliderB;

	private JLabel lblContrastValueR;
	private JLabel lblContrastValueG;
	private JLabel lblContrastValueB;

	private JRadioButton radioOriginal;
	private JRadioButton radioEdited;

	private JLabel lblScore;

	private JComboBox cmbMatchingFar;
	private JComboBox cmbZoomLeft;
	private JComboBox cmbZoomRight;

	private JButton btnOpenLeft;

	private JMenuItem menuItemSaveTemplate;
	private JMenuItem menuItemSaveLatentImage;
	private JMenuItem menuItemSaveReferenceImage;
	private JMenuItem menuItemExit;

	private JMenuItem menuItemExtraction;
	private JMenuItem menuItemAbout;

	private JRadioButton radioPointer;
	private JRadioButton radioSelectArea;
	private JRadioButton radioAddEndMinutia;
	private JRadioButton radioAddBifurcationMinutia;
	private JRadioButton radioAddDelta;
	private JRadioButton radioAddCore;
	private JRadioButton radioAddDoubleCore;

	private JButton btnResetBrightness;
	private JButton btnResetContrast;
	private JButton btnResetAll;

	private JButton btnSaveTemplate;
	private JButton btnZoomInLeft;
	private JButton btnZoomOutLeft;

	private ToolbarMenu viewMenuLeftButton;
	private ToolbarMenu transformMenuButton;

	private JPopupMenu viewMenuLeft;
	private JPopupMenu transformMenu;

	private JButton btnOpenRight;
	private JButton btnZoomInRight;
	private JButton btnZoomOutRight;

	private ToolbarMenu viewMenuRightButton;
	private JPopupMenu viewMenuRight;

	private JCheckBoxMenuItem menuItemViewOriginalLeft;
	private JMenuItem menuItemZoomInLeft;
	private JMenuItem menuItemZoomOutLeft;
	private JMenuItem menuItemOriginalLeft;

	private JMenuItem menuItemRotate90Clockwise;
	private JMenuItem menuItemRotate90CounterClockwise;
	private JMenuItem menuItemRotate180;
	private JMenuItem menuItemFlipHorizontally;
	private JMenuItem menuItemFlipVertically;
	private JMenuItem menuItemCropToSelection;
	private JMenuItem menuItemInvertMinutiae;
	private JMenuItem menuItemBandpassFiltering;

	private JCheckBoxMenuItem menuItemViewOriginalRight;
	private JMenuItem menuItemZoomInRight;
	private JMenuItem menuItemZoomOutRight;
	private JMenuItem menuItemOriginalRight;

	private JPopupMenu leftRightClickMenu;
	private JPopupMenu rightRightClickMenu;

	private JMenu leftRightClickZoomMenu;
	private JMenuItem menuItemLeftRightClickZoomIn;
	private JMenuItem menuItemLeftRightClickZoomOut;
	private JMenuItem menuItemLeftRightClickOriginal;
	private JMenuItem menuItemLeftRightClickDelete;

	private JMenuItem menuItemRightRightClickZoomIn;
	private JMenuItem menuItemRightRightClickZoomOut;
	private JMenuItem menuItemRightRightClickOriginal;

	// ==============================================
	// Public constructor
	// ==============================================

	public MainFrame() {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(null, e.toString());
		}
		initializeComponents();
		createRightClickMenus();

		menuItemAbout.setText(AboutBox.getName());

		setIconImage(Utils.createIconImage("images/Logo16x16.png"));

		addComponentListener(new ComponentAdapter() {

			@Override
			public void componentShown(ComponentEvent e) {
				loadingFrame();
			}
		});
		addWindowListener(new WindowAdapter() {

			@Override
			public void windowClosed(WindowEvent e) {
				settings.save();
			}

			@Override
			public void windowClosing(WindowEvent e) {
				try {
					dispose();
					LicenseManager.getInstance().releaseAll();
				} finally {
					NCore.shutdown();
				}

			}
		});

		openFileDialog = new JFileChooser();
		saveTemplateDialog = new JFileChooser();
		saveImageDialog = new JFileChooser();
	}

	// ==============================================
	// Private methods
	// ==============================================

	// ==============================================
	// Create GUI
	// ==============================================

	private void initializeComponents() {
		getContentPane().setLayout(new BorderLayout());
		createMenuBar();

		JPanel mainPanel = new JPanel();
		GridBagLayout mainPanelLayout = new GridBagLayout();
		mainPanelLayout.columnWidths = new int[] { 85, 185, 435, 185 };
		mainPanelLayout.rowHeights = new int[] { 575, 60 };
		mainPanel.setLayout(mainPanelLayout);

		c.fill = GridBagConstraints.BOTH;

		addToGridBagLayout(0, 0, 1, 2, mainPanel, createLeftPanel());
		addToGridBagLayout(1, 0, 3, 1, 1, 1, mainPanel, createSplitPanel());
		addToGridBagLayout(1, 1, 1, 1, 0, 0, mainPanel, createLatentImagePanel());
		addToGridBagLayout(2, 1, 1, 1, 1, 0, mainPanel, createMatchingPanel());
		addToGridBagLayout(3, 1, 1, 1, 0, 0, mainPanel, createReferenceImagePanel());

		getContentPane().add(mainPanel, BorderLayout.CENTER);
		pack();

	}

	private JPanel createLatentImagePanel() {
		JPanel latentImagePanel = new JPanel();
		latentImagePanel.setPreferredSize(new Dimension(185, 60));
		latentImagePanel.setMinimumSize(new Dimension(185, 60));
		latentImagePanel.setBorder(BorderFactory.createLineBorder(Color.BLACK));
		latentImagePanel.setLayout(new BoxLayout(latentImagePanel, BoxLayout.Y_AXIS));

		JLabel lblLatentImage = new JLabel("Latent image");
		Font currentFont = lblLatentImage.getFont();
		lblLatentImage.setFont(new Font(currentFont.getName(), Font.BOLD, currentFont.getSize() + 2));
		lblLatentImageSize = new JLabel("Size: ");
		lblLatentImageResolution = new JLabel("Resolution:");
		latentImagePanel.add(lblLatentImage);
		latentImagePanel.add(lblLatentImageSize);
		latentImagePanel.add(lblLatentImageResolution);
		return latentImagePanel;
	}

	private JPanel createReferenceImagePanel() {
		JPanel referenceImagePanel = new JPanel();
		referenceImagePanel.setPreferredSize(new Dimension(185, 60));
		referenceImagePanel.setMinimumSize(new Dimension(185, 60));
		referenceImagePanel.setBorder(BorderFactory.createLineBorder(Color.BLACK));
		referenceImagePanel.setLayout(new BoxLayout(referenceImagePanel, BoxLayout.Y_AXIS));

		JLabel lblReferenceImage = new JLabel("Reference image");
		Font currentFont = lblReferenceImage.getFont();
		lblReferenceImage.setFont(new Font(currentFont.getName(), Font.BOLD, currentFont.getSize() + 2));
		lblReferenceImageSize = new JLabel("Size: ");
		lblReferenceImageResolution = new JLabel("Resolution:");
		referenceImagePanel.add(lblReferenceImage);
		referenceImagePanel.add(lblReferenceImageSize);
		referenceImagePanel.add(lblReferenceImageResolution);
		return referenceImagePanel;
	}

	private void createMenuBar() {
		JMenuBar menuBar = new JMenuBar();

		JMenu settingsMenu = new JMenu("Settings");
		menuItemExtraction = new JMenuItem("Extraction...");
		menuItemExtraction.addActionListener(this);

		settingsMenu.add(menuItemExtraction);

		JMenu helpMenu = new JMenu("Help");
		menuItemAbout = new JMenuItem("About");
		menuItemAbout.addActionListener(this);

		helpMenu.add(menuItemAbout);

		menuBar.add(createFileMenu());
		menuBar.add(settingsMenu);
		menuBar.add(helpMenu);
		setJMenuBar(menuBar);
	}

	private JMenu createFileMenu() {
		JMenu fileMenu = new JMenu("File");
		menuItemSaveTemplate = new JMenuItem("Save template...");
		menuItemSaveTemplate.addActionListener(this);

		menuItemSaveLatentImage = new JMenuItem("Save latent image...");
		menuItemSaveLatentImage.addActionListener(this);

		menuItemSaveReferenceImage = new JMenuItem("Save reference image...");
		menuItemSaveReferenceImage.addActionListener(this);

		menuItemExit = new JMenuItem("Exit");
		menuItemExit.addActionListener(this);

		fileMenu.add(menuItemSaveTemplate);
		fileMenu.addSeparator();
		fileMenu.add(menuItemSaveLatentImage);
		fileMenu.add(menuItemSaveReferenceImage);
		fileMenu.addSeparator();
		fileMenu.add(menuItemExit);
		return fileMenu;
	}

	private JPanel createLeftPanel() {
		JPanel leftPanel = new JPanel();
		leftPanel.setBorder(BorderFactory.createLineBorder(Color.BLACK));
		leftPanel.setLayout(new BoxLayout(leftPanel, BoxLayout.Y_AXIS));

		JLabel lblTools = new JLabel("Tools");

		JPanel toolsTitlePanel = new JPanel();
		toolsTitlePanel.add(lblTools);
		toolsTitlePanel.setOpaque(true);
		toolsTitlePanel.setBackground(Color.GRAY.brighter());
		toolsTitlePanel.setPreferredSize(new Dimension(84, 25));
		toolsTitlePanel.setMaximumSize(new Dimension(84, 25));

		JLabel lblColors = new JLabel("Colors");

		JPanel colorsTitlePanel = new JPanel();
		colorsTitlePanel.add(lblColors);
		colorsTitlePanel.setOpaque(true);
		colorsTitlePanel.setBackground(Color.GRAY.brighter());
		colorsTitlePanel.setPreferredSize(new Dimension(84, 25));
		colorsTitlePanel.setMaximumSize(new Dimension(84, 25));

		leftPanel.add(toolsTitlePanel);
		leftPanel.add(createToolsPanel());
		leftPanel.add(colorsTitlePanel);
		leftPanel.add(createColorsPanel());

		leftPanel.add(Box.createVerticalGlue());

		return leftPanel;
	}

	private JPanel createToolsPanel() {
		JPanel toolsPanel = new JPanel(new GridLayout(4, 2));
		toolsPanel.setPreferredSize(new Dimension(84, 170));
		toolsPanel.setMaximumSize(new Dimension(84, 170));
		ButtonGroup toolsGroup = new ButtonGroup();

		radioPointer = new JRadioButton(Utils.createIcon("images/ToolMoveRotate.png"));
		radioSelectArea = new JRadioButton(Utils.createIcon("images/ToolAreaSelect.png"));
		radioAddEndMinutia = new JRadioButton(Utils.createIcon("images/ToolMinutiaEnd.png"));
		radioAddBifurcationMinutia = new JRadioButton(Utils.createIcon("images/ToolMinutiaBifurcation.png"));
		radioAddDelta = new JRadioButton(Utils.createIcon("images/ToolDelta.png"));
		radioAddCore = new JRadioButton(Utils.createIcon("images/ToolCore.png"));
		radioAddDoubleCore = new JRadioButton(Utils.createIcon("images/ToolDoubleCore.png"));

		setToolButtonUIs();

		toolsGroup.add(radioPointer);
		toolsGroup.add(radioSelectArea);
		toolsGroup.add(radioAddEndMinutia);
		toolsGroup.add(radioAddBifurcationMinutia);
		toolsGroup.add(radioAddDelta);
		toolsGroup.add(radioAddCore);
		toolsGroup.add(radioAddDoubleCore);

		radioPointer.setSelected(true);

		toolsPanel.add(radioPointer);
		toolsPanel.add(radioSelectArea);
		toolsPanel.add(radioAddEndMinutia);
		toolsPanel.add(radioAddBifurcationMinutia);
		toolsPanel.add(radioAddDelta);
		toolsPanel.add(radioAddCore);
		toolsPanel.add(radioAddDoubleCore);

		return toolsPanel;
	}

	private void setToolButtonUIs() {
		radioPointer.setSelectedIcon(Utils.createIcon("images/ToolMoveRotateSelected.png"));
		radioPointer.setRolloverIcon(Utils.createIcon("images/ToolMoveRotateSelected.png"));
		radioPointer.setToolTipText("Move/Rotate Tool");
		radioPointer.addActionListener(this);

		radioSelectArea.setSelectedIcon(Utils.createIcon("images/ToolAreaSelectSelected.png"));
		radioSelectArea.setRolloverIcon(Utils.createIcon("images/ToolAreaSelectSelected.png"));
		radioSelectArea.setToolTipText("Area Select Tool");
		radioSelectArea.addActionListener(this);

		radioAddEndMinutia.setSelectedIcon(Utils.createIcon("images/ToolMinutiaEndSelected.png"));
		radioAddEndMinutia.setRolloverIcon(Utils.createIcon("images/ToolMinutiaEndSelected.png"));
		radioAddEndMinutia.setToolTipText("Add End Minutia");
		radioAddEndMinutia.addActionListener(this);

		radioAddBifurcationMinutia.setSelectedIcon(Utils.createIcon("images/ToolMinutiaBifurcationSelected.png"));
		radioAddBifurcationMinutia.setRolloverIcon(Utils.createIcon("images/ToolMinutiaBifurcationSelected.png"));
		radioAddBifurcationMinutia.setToolTipText("Add Bifurcation Minutia");
		radioAddBifurcationMinutia.addActionListener(this);

		radioAddDelta.setSelectedIcon(Utils.createIcon("images/ToolDeltaSelected.png"));
		radioAddDelta.setRolloverIcon(Utils.createIcon("images/ToolDeltaSelected.png"));
		radioAddDelta.setToolTipText("Add Delta");
		radioAddDelta.addActionListener(this);

		radioAddCore.setSelectedIcon(Utils.createIcon("images/ToolCoreSelected.png"));
		radioAddCore.setRolloverIcon(Utils.createIcon("images/ToolCoreSelected.png"));
		radioAddCore.setToolTipText("Add Core");
		radioAddCore.addActionListener(this);

		radioAddDoubleCore.setSelectedIcon(Utils.createIcon("images/ToolDoubleCoreSelected.png"));
		radioAddDoubleCore.setRolloverIcon(Utils.createIcon("images/ToolDoubleCoreSelected.png"));
		radioAddDoubleCore.setToolTipText("Add Double Core");
		radioAddDoubleCore.addActionListener(this);

	}

	private JPanel createColorsPanel() {
		JPanel colorsPanel = new JPanel();

		GridBagLayout colorsPanelLayout = new GridBagLayout();
		colorsPanelLayout.columnWidths = new int[] { 28, 28, 28 };
		colorsPanelLayout.rowHeights = new int[] { 25, 25, 100, 25, 25, 25, 100, 25, 25, 25, 25 };
		colorsPanel.setLayout(colorsPanelLayout);

		colorsPanel.setPreferredSize(new Dimension(84, 425));
		colorsPanel.setMaximumSize(new Dimension(84, 425));

		chkInvert = new JCheckBox("invert");
		chkInvert.addActionListener(this);

		JLabel lblBrightness = new JLabel(Utils.createIcon("images/Brightness.png"));
		lblBrightness.setToolTipText("Brightness");

		btnResetBrightness = new JButton("reset");
		btnResetBrightness.addActionListener(this);

		brightnessSliderR = createColorSlider(Color.RED, 0);
		brightnessSliderG = createColorSlider(Color.GREEN, 1);
		brightnessSliderB = createColorSlider(Color.BLUE, 2);

		lblBrightnessValueR = new JLabel("0");
		lblBrightnessValueG = new JLabel("0");
		lblBrightnessValueB = new JLabel("0");

		lblBrightnessValueR.setBorder(new EmptyBorder(0, 8, 0, 0));
		lblBrightnessValueG.setBorder(new EmptyBorder(0, 8, 0, 0));
		lblBrightnessValueB.setBorder(new EmptyBorder(0, 8, 0, 0));

		chkGroupBrightness = new JCheckBox("group");
		chkGroupBrightness.setSelected(true);

		JLabel lblContrast = new JLabel(Utils.createIcon("images/Contrast.png"));
		lblContrast.setToolTipText("Contrast");

		btnResetContrast = new JButton("reset");
		btnResetContrast.addActionListener(this);

		contrastSliderR = createColorSlider(Color.RED, 0);
		contrastSliderG = createColorSlider(Color.GREEN, 1);
		contrastSliderB = createColorSlider(Color.BLUE, 2);

		lblContrastValueR = new JLabel("0");
		lblContrastValueG = new JLabel("0");
		lblContrastValueB = new JLabel("0");

		lblContrastValueR.setBorder(new EmptyBorder(0, 8, 0, 0));
		lblContrastValueG.setBorder(new EmptyBorder(0, 8, 0, 0));
		lblContrastValueB.setBorder(new EmptyBorder(0, 8, 0, 0));

		chkGroupContrast = new JCheckBox("group");
		chkGroupContrast.setSelected(true);

		chkToGray = new JCheckBox("to gray");
		chkToGray.addActionListener(this);

		btnResetAll = new JButton("reset all");
		btnResetAll.addActionListener(this);

		GridBagConstraints c = new GridBagConstraints();
		c.fill = GridBagConstraints.HORIZONTAL;
		c.insets = new Insets(1, 1, 1, 1);

		addToGridBagLayout(0, 0, 3, 1, colorsPanel, chkInvert);
		addToGridBagLayout(0, 1, 1, 1, colorsPanel, lblBrightness);
		addToGridBagLayout(1, 1, 2, 1, colorsPanel, btnResetBrightness);
		addToGridBagLayout(0, 2, 1, 1, colorsPanel, brightnessSliderR);
		addToGridBagLayout(1, 2, colorsPanel, brightnessSliderG);
		addToGridBagLayout(2, 2, colorsPanel, brightnessSliderB);
		addToGridBagLayout(0, 3, colorsPanel, lblBrightnessValueR);
		addToGridBagLayout(1, 3, colorsPanel, lblBrightnessValueG);
		addToGridBagLayout(2, 3, colorsPanel, lblBrightnessValueB);
		addToGridBagLayout(0, 4, 3, 1, colorsPanel, chkGroupBrightness);
		addToGridBagLayout(0, 5, 1, 1, colorsPanel, lblContrast);
		addToGridBagLayout(1, 5, 2, 1, colorsPanel, btnResetContrast);
		addToGridBagLayout(0, 6, 1, 1, colorsPanel, contrastSliderR);
		addToGridBagLayout(1, 6, colorsPanel, contrastSliderG);
		addToGridBagLayout(2, 6, colorsPanel, contrastSliderB);
		addToGridBagLayout(0, 7, colorsPanel, lblContrastValueR);
		addToGridBagLayout(1, 7, colorsPanel, lblContrastValueG);
		addToGridBagLayout(2, 7, colorsPanel, lblContrastValueB);
		addToGridBagLayout(0, 8, 3, 1, colorsPanel, chkGroupContrast);
		addToGridBagLayout(0, 9, colorsPanel, chkToGray);
		addToGridBagLayout(0, 10, colorsPanel, btnResetAll);
		clearGridBagConstraints();
		c.insets = new Insets(0, 0, 0, 0);

		return colorsPanel;
	}

	private JSlider createColorSlider(Color color, int tag) {
		JSlider slider = new JSlider(SwingConstants.VERTICAL, -100, 100, 0);
		slider.setUI(new ColorSliderUI(slider, color));
		slider.setInverted(true);
		slider.setPreferredSize(new Dimension(28, 100));
		slider.setMinimumSize(new Dimension(28, 100));
		slider.putClientProperty("tag", tag);
		slider.addChangeListener(this);
		return slider;
	}

	private JSplitPane createSplitPanel() {
		JSplitPane mainSplitPane = new JSplitPane();
		mainSplitPane.setMinimumSize(new Dimension(805, 590));

		mainSplitPane.setLeftComponent(createLeftSplitPanel());
		mainSplitPane.setRightComponent(createRightSplitPanel());
		mainSplitPane.setDividerLocation(0.5);
		mainSplitPane.setResizeWeight(0.5);

		return mainSplitPane;
	}

	private JPanel createMatchingPanel() {
		JPanel matchingPanel = new JPanel();
		matchingPanel.setPreferredSize(new Dimension(435, 60));
		matchingPanel.setMinimumSize(new Dimension(435, 60));
		matchingPanel.setBorder(BorderFactory.createLineBorder(Color.BLACK));
		matchingPanel.setLayout(new BoxLayout(matchingPanel, BoxLayout.X_AXIS));

		JPanel workingImagePanel = new JPanel(new FlowLayout());
		workingImagePanel.setBorder(BorderFactory.createTitledBorder("Working image:"));

		radioOriginal = new JRadioButton("Original");
		radioOriginal.addActionListener(this);

		radioEdited = new JRadioButton("Edited");
		radioEdited.addActionListener(this);

		ButtonGroup imageGroup = new ButtonGroup();
		imageGroup.add(radioOriginal);
		imageGroup.add(radioEdited);

		radioOriginal.setSelected(true);

		workingImagePanel.add(radioOriginal);
		workingImagePanel.add(radioEdited);
		workingImagePanel.setPreferredSize(new Dimension(150, 50));
		workingImagePanel.setMaximumSize(new Dimension(150, 50));

		JPanel matchingFARPanel = new JPanel();
		matchingFARPanel.setBorder(BorderFactory.createTitledBorder("Matching FAR"));

		cmbMatchingFar = new JComboBox();

		matchingFARPanel.add(cmbMatchingFar);
		matchingFARPanel.setPreferredSize(new Dimension(150, 50));
		matchingFARPanel.setMaximumSize(new Dimension(150, 50));

		JPanel rightPanel = new JPanel();
		rightPanel.setLayout(new BoxLayout(rightPanel, BoxLayout.Y_AXIS));

		btnMatch = new JButton("Match");
		btnMatch.setPreferredSize(new Dimension(90, 25));
		btnMatch.setMaximumSize(new Dimension(90, 25));
		btnMatch.addActionListener(this);
		btnMatch.setEnabled(false);

		lblScore = new JLabel("Score:");

		rightPanel.add(btnMatch);
		rightPanel.add(lblScore);

		matchingPanel.add(workingImagePanel);
		matchingPanel.add(Box.createHorizontalGlue());
		matchingPanel.add(matchingFARPanel);
		matchingPanel.add(Box.createHorizontalStrut(3));
		matchingPanel.add(rightPanel);
		matchingPanel.add(Box.createHorizontalStrut(5));
		return matchingPanel;
	}

	private JPanel createLeftSplitPanel() {
		JPanel leftSplitPanel = new JPanel();
		GridBagLayout leftSplitPanelLayout = new GridBagLayout();
		leftSplitPanelLayout.rowHeights = new int[] { 25, 30, 540 };
		leftSplitPanel.setLayout(leftSplitPanelLayout);

		lblLeftFileName = new JLabel("Untitled");

		JPanel leftTitlePanel = new JPanel();
		leftTitlePanel.add(lblLeftFileName);
		leftTitlePanel.setOpaque(true);
		leftTitlePanel.setBackground(Color.GRAY.brighter());

		nfViewLeft = new NFingerView() {
			private static final long serialVersionUID = 1L;

			@Override
			public void paintComponent(Graphics g) {
				super.paintComponent(g);
				if (processingRequested) {
					processImage();
				}
			}
		};

		nfViewLeft.addMinutiaSelectionListener(new MinutiaSelectionListener() {

			@Override
			public void selectedMinutiaIndexChanged(MinutiaSelectionEvent e) {
				if (nfViewRight != null && e != null) {
					nfViewRight.setSelectedMinutiaIndex(e.getSelelctedIndex());
				}

			}
		});
		nfViewLeft.setMatedMinutiaIndex(0);

		JScrollPane leftScrollPane = new JScrollPane(nfViewLeft, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		leftScrollPane.setPreferredSize(new Dimension(385, 540));

		c.fill = GridBagConstraints.BOTH;

		addToGridBagLayout(0, 0, 1, 1, 1, 0, leftSplitPanel, leftTitlePanel);
		addToGridBagLayout(0, 1, leftSplitPanel, createLeftToolbar());
		addToGridBagLayout(0, 2, 1, 1, 1, 1, leftSplitPanel, leftScrollPane);

		clearGridBagConstraints();
		return leftSplitPanel;

	}

	private JToolBar createLeftToolbar() {
		JToolBar leftToolbBar = new JToolBar();
		leftToolbBar.setFloatable(false);

		btnOpenLeft = new JButton(Utils.createIcon("images/OpenFolder.png"));
		btnOpenLeft.setToolTipText("Open");
		btnOpenLeft.addActionListener(this);

		btnSaveTemplate = new JButton(Utils.createIcon("images/SaveHS.png"));
		btnSaveTemplate.setToolTipText("Save template");
		btnSaveTemplate.addActionListener(this);

		btnExtract = new JButton(Utils.createIcon("images/Extract.png"));
		btnExtract.setToolTipText("Extract");
		btnExtract.addActionListener(this);

		cmbZoomLeft = new JComboBox();
		cmbZoomLeft.addActionListener(this);
		cmbZoomLeft.setPreferredSize(new Dimension(75, 25));
		cmbZoomLeft.setMaximumSize(new Dimension(75, 25));

		btnZoomInLeft = new JButton(Utils.createIcon("images/ZoomIn.png"));
		btnZoomInLeft.setToolTipText("Zoom In");
		btnZoomInLeft.addActionListener(this);

		btnZoomOutLeft = new JButton(Utils.createIcon("images/ZoomOut.png"));
		btnZoomOutLeft.setToolTipText("Zoom Out");
		btnZoomOutLeft.addActionListener(this);

		leftToolbBar.add(btnOpenLeft);
		leftToolbBar.add(btnSaveTemplate);
		leftToolbBar.addSeparator();
		leftToolbBar.add(btnExtract);
		leftToolbBar.addSeparator();
		leftToolbBar.add(cmbZoomLeft);
		leftToolbBar.add(btnZoomInLeft);
		leftToolbBar.add(btnZoomOutLeft);
		leftToolbBar.addSeparator();
		leftToolbBar.add(createViewMenuLeft());
		leftToolbBar.add(createTransformMenu());
		leftToolbBar.add(new JSeparator());
		return leftToolbBar;
	}

	private ToolbarMenu createViewMenuLeft() {
		viewMenuLeft = new JPopupMenu();
		viewMenuLeftButton = new ToolbarMenu("View", viewMenuLeft);
		viewMenuLeftButton.setToolTipText("View");

		menuItemViewOriginalLeft = new JCheckBoxMenuItem("Original");
		menuItemViewOriginalLeft.addActionListener(this);

		JMenu menuZoomLeft = new JMenu("Zoom");
		menuItemZoomInLeft = new JMenuItem("Zoom in");
		menuItemZoomInLeft.addActionListener(this);

		menuItemZoomOutLeft = new JMenuItem("Zoom out");
		menuItemZoomOutLeft.addActionListener(this);

		menuItemOriginalLeft = new JMenuItem("Original");
		menuItemOriginalLeft.addActionListener(this);

		menuZoomLeft.add(menuItemZoomInLeft);
		menuZoomLeft.add(menuItemZoomOutLeft);
		menuZoomLeft.add(menuItemOriginalLeft);

		viewMenuLeft.add(menuItemViewOriginalLeft);
		viewMenuLeft.addSeparator();
		viewMenuLeft.add(menuZoomLeft);
		return viewMenuLeftButton;
	}

	private ToolbarMenu createTransformMenu() {
		transformMenu = new JPopupMenu();
		transformMenuButton = new ToolbarMenu("Transform", transformMenu);
		transformMenuButton.setToolTipText("Transform");

		menuItemRotate90Clockwise = new JMenuItem("Rotate 90 clockwise");
		menuItemRotate90Clockwise.addActionListener(this);

		menuItemRotate90CounterClockwise = new JMenuItem("Rotate 90 counter-clockwise");
		menuItemRotate90CounterClockwise.addActionListener(this);

		menuItemRotate180 = new JMenuItem("Rotate 180");
		menuItemRotate180.addActionListener(this);

		menuItemFlipHorizontally = new JMenuItem("Flip horizontally");
		menuItemFlipHorizontally.addActionListener(this);

		menuItemFlipVertically = new JMenuItem("Flip vertically");
		menuItemFlipVertically.addActionListener(this);

		menuItemCropToSelection = new JMenuItem("Crop to selection");
		menuItemCropToSelection.addActionListener(this);

		menuItemInvertMinutiae = new JMenuItem("Invert minutiae");
		menuItemInvertMinutiae.addActionListener(this);

		menuItemBandpassFiltering = new JMenuItem("Perform Bandpass filtering");
		menuItemBandpassFiltering.addActionListener(this);

		transformMenu.add(menuItemRotate90Clockwise);
		transformMenu.add(menuItemRotate90CounterClockwise);
		transformMenu.add(menuItemRotate180);
		transformMenu.addSeparator();
		transformMenu.add(menuItemFlipHorizontally);
		transformMenu.add(menuItemFlipVertically);
		transformMenu.addSeparator();
		transformMenu.add(menuItemCropToSelection);
		transformMenu.addSeparator();
		transformMenu.add(menuItemInvertMinutiae);
		transformMenu.addSeparator();
		transformMenu.add(menuItemBandpassFiltering);
		return transformMenuButton;
	}

	private JPanel createRightSplitPanel() {
		JPanel rightSplitPanel = new JPanel();
		GridBagLayout rightSplitPanelLayout = new GridBagLayout();
		rightSplitPanelLayout.rowHeights = new int[] { 25, 30, 540 };
		rightSplitPanel.setLayout(rightSplitPanelLayout);

		lblRightFileName = new JLabel("Untitled");

		JPanel rightTitlePanel = new JPanel();
		rightTitlePanel.add(lblRightFileName);
		rightTitlePanel.setOpaque(true);
		rightTitlePanel.setBackground(Color.GRAY.brighter());

		JToolBar righttToolbBar = new JToolBar();
		righttToolbBar.setFloatable(false);

		btnOpenRight = new JButton(Utils.createIcon("images/OpenFolder.png"));
		btnOpenRight.setToolTipText("Open");
		btnOpenRight.addActionListener(this);

		cmbZoomRight = new JComboBox();
		cmbZoomRight.addActionListener(this);
		cmbZoomRight.setPreferredSize(new Dimension(75, 25));
		cmbZoomRight.setMaximumSize(new Dimension(75, 25));

		btnZoomInRight = new JButton(Utils.createIcon("images/ZoomIn.png"));
		btnZoomInRight.setToolTipText("Zoom In");
		btnZoomInRight.addActionListener(this);

		btnZoomOutRight = new JButton(Utils.createIcon("images/ZoomOut.png"));
		btnZoomOutRight.setToolTipText("Zoom Out");
		btnZoomOutRight.addActionListener(this);

		righttToolbBar.add(btnOpenRight);
		righttToolbBar.addSeparator();
		righttToolbBar.add(cmbZoomRight);
		righttToolbBar.add(btnZoomInRight);
		righttToolbBar.add(btnZoomOutRight);
		righttToolbBar.addSeparator();
		righttToolbBar.add(createViewMenuRight());
		righttToolbBar.add(new JSeparator());

		nfViewRight = new NFingerView();
		nfViewRight.addMinutiaSelectionListener(new MinutiaSelectionListener() {

			@Override
			public void selectedMinutiaIndexChanged(MinutiaSelectionEvent e) {
				if (nfViewLeft != null) {
					nfViewLeft.setSelectedMinutiaIndex(e.getSelelctedIndex());
				}
			}
		});
		nfViewRight.setMatedMinutiaIndex(1);

		JScrollPane rightScrollPane = new JScrollPane(nfViewRight, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		rightScrollPane.setPreferredSize(new Dimension(405, 540));

		c.fill = GridBagConstraints.BOTH;

		addToGridBagLayout(0, 0, 1, 1, 1, 0, rightSplitPanel, rightTitlePanel);
		addToGridBagLayout(0, 1, rightSplitPanel, righttToolbBar);
		addToGridBagLayout(0, 2, 1, 1, 1, 1, rightSplitPanel, rightScrollPane);

		return rightSplitPanel;
	}

	private ToolbarMenu createViewMenuRight() {
		viewMenuRight = new JPopupMenu();
		viewMenuRightButton = new ToolbarMenu("View", viewMenuRight);
		viewMenuRightButton.setToolTipText("View");

		menuItemViewOriginalRight = new JCheckBoxMenuItem("Original");
		menuItemViewOriginalRight.addActionListener(this);

		JMenu menuZoomRight = new JMenu("Zoom");
		menuItemZoomInRight = new JMenuItem("Zoom in");
		menuItemZoomInRight.addActionListener(this);

		menuItemZoomOutRight = new JMenuItem("Zoom out");
		menuItemZoomOutRight.addActionListener(this);

		menuItemOriginalRight = new JMenuItem("Original");
		menuItemOriginalRight.addActionListener(this);

		menuZoomRight.add(menuItemZoomInRight);
		menuZoomRight.add(menuItemZoomOutRight);
		menuZoomRight.add(menuItemOriginalRight);

		viewMenuRight.add(menuItemViewOriginalRight);
		viewMenuRight.addSeparator();
		viewMenuRight.add(menuZoomRight);
		return viewMenuRightButton;
	}

	private void createRightClickMenus() {
		leftRightClickMenu = new JPopupMenu();

		leftRightClickZoomMenu = new JMenu("Zoom");
		menuItemLeftRightClickZoomIn = new JMenuItem("Zoom in");
		menuItemLeftRightClickZoomIn.addActionListener(this);

		menuItemLeftRightClickZoomOut = new JMenuItem("Zoom out");
		menuItemLeftRightClickZoomOut.addActionListener(this);

		menuItemLeftRightClickOriginal = new JMenuItem("Original");
		menuItemLeftRightClickOriginal.addActionListener(this);

		leftRightClickZoomMenu.add(menuItemLeftRightClickZoomIn);
		leftRightClickZoomMenu.add(menuItemLeftRightClickZoomOut);
		leftRightClickZoomMenu.add(menuItemLeftRightClickOriginal);

		menuItemLeftRightClickDelete = new JMenuItem("Delete");
		menuItemLeftRightClickDelete.addActionListener(this);

		leftRightClickMenu.add(leftRightClickZoomMenu);
		leftRightClickMenu.addSeparator();
		leftRightClickMenu.add(menuItemLeftRightClickDelete);

		nfViewLeft.addMouseListener(new MouseAdapter() {

			@Override
			public void mouseClicked(MouseEvent e) {
				if (e.getButton() == MouseEvent.BUTTON3) {
					menuItemLeftRightClickDelete.setEnabled((nfViewLeft.getSelectedMinutiaIndex() >= 0 || nfViewLeft.getSelectedDeltaIndex() >= 0
							|| nfViewLeft.getSelectedCoreIndex() >= 0 || nfViewLeft.getSelectedDoubleCoreIndex() >= 0));
					leftRightClickMenu.show(nfViewLeft, e.getX(), e.getY());
				}
			}
		});

		rightRightClickMenu = new JPopupMenu();
		menuItemRightRightClickZoomIn = new JMenuItem("Zoom in");
		menuItemRightRightClickZoomIn.addActionListener(this);

		menuItemRightRightClickZoomOut = new JMenuItem("Zoom out");
		menuItemRightRightClickZoomOut.addActionListener(this);

		menuItemRightRightClickOriginal = new JMenuItem("Original");
		menuItemRightRightClickOriginal.addActionListener(this);

		rightRightClickMenu.add(menuItemRightRightClickZoomIn);
		rightRightClickMenu.add(menuItemRightRightClickZoomOut);
		rightRightClickMenu.add(menuItemRightRightClickOriginal);

		nfViewRight.addMouseListener(new MouseAdapter() {

			@Override
			public void mouseClicked(MouseEvent e) {
				if (e.getButton() == MouseEvent.BUTTON3) {
					rightRightClickMenu.show(nfViewRight, e.getX(), e.getY());
				}
			}
		});

	}

	// ==============================================
	// Image loading related methods
	// ==============================================

	private void checkResolution(boolean isLeft) {
		isCheckingLeft = isLeft;
		ResolutionDialog frmResolution = new ResolutionDialog(this, this);
		frmResolution.setHorzResolution(checkingHorzResolution);
		frmResolution.setVertResolution(checkingVertResolution);
		frmResolution.setFingerImage((BufferedImage) checkingImage.toImage());
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		frmResolution.setLocation(screenSize.width / 2 - frmResolution.getPreferredSize().width / 2, screenSize.height / 2
				- frmResolution.getPreferredSize().height / 2);
		frmResolution.setVisible(true);
	}

	private void openLeft() {
		if (settings.getLastDirectory() != null) {
			openFileDialog.setCurrentDirectory(new File(settings.getLastDirectory()));
		}
		openFileDialog.setSelectedFile(null);

		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			try {
				settings.setLastDirectory(openFileDialog.getCurrentDirectory().getPath());
				// read image
				NImage tmp = NImage.fromFile(openFileDialog.getSelectedFile().getPath());

				float horzResolution = tmp.getHorzResolution();
				if (horzResolution < 250.0f) {
					horzResolution = 500f;
				}
				float vertResolution = tmp.getVertResolution();
				if (vertResolution < 250.0f) {
					vertResolution = 500f;
				}
				checkingHorzResolution = horzResolution;
				checkingVertResolution = vertResolution;
				checkingImage = tmp;
				checkResolution(true);

			} catch (Exception ex) {
				ex.printStackTrace();
				JOptionPane.showMessageDialog(this,
						String.format("Error opening file \"%s\": %s", openFileDialog.getSelectedFile().getName(), ex.getMessage()), getTitle(),
						JOptionPane.ERROR_MESSAGE);
				return;
			}
		}
	}

	private void leftResolutionCheckSuceeded() {
		nfViewLeft.setTree(new NIndexPair[0]);
		nfViewRight.setTree(new NIndexPair[0]);

		NFinger finger = (NFinger) nfViewLeft.getFinger();
		nfViewLeft.setFinger(null);
		if (finger != null) {
			finger.dispose();
		}
		nfViewLeft.clearSelectedArea();

		if (record != null) {
			record.dispose();
		}
		record = null;

		if (imageLeft != null) {
			imageLeft.dispose();
			imageLeft = null;
		}

		if (binarizedImage != null) {
			binarizedImage.dispose();
			binarizedImage = null;
		}

		btnExtract.setEnabled(false);
		btnMatch.setEnabled(false);

		imageLeft = NImage.fromImage(NPixelFormat.RGB_8U, 0, checkingImage);
		imageLeft.setHorzResolution(checkingHorzResolution);
		imageLeft.setVertResolution(checkingVertResolution);
		imageLeft.setResolutionIsAspectRatio(false);

		nfViewLeft.setShownImage(ShownImage.ORIGINAL);
		menuItemViewOriginalLeft.setSelected(true);

		try {
			imageLeftOriginal = (NImage) imageLeft.clone();
		} catch (CloneNotSupportedException e) {
			e.printStackTrace();
			throw new AssertionError("Can't happen");
		}

		lblLatentImageSize.setText(String.format("Size: %s x %s", imageLeftOriginal.getWidth(), imageLeftOriginal.getHeight()));
		lblLatentImageResolution
				.setText(String.format("Resolution: %.2f x %.2f", imageLeftOriginal.getHorzResolution(), imageLeftOriginal.getVertResolution()));

		String filePath = openFileDialog.getSelectedFile().getPath();
		lblLeftFileName.setText(filePath);
		lblLeftFileName.setToolTipText(filePath);
		btnExtract.setEnabled(true);
		resetAll();
		checkingImage.dispose();
		checkingHorzResolution = -1;
		checkingVertResolution = -1;

		nfViewLeft.updateUI();
	}

	private void openRight() {
		if (settings.getLastDirectory() != null) {
			openFileDialog.setCurrentDirectory(new File(settings.getLastDirectory()));
		}
		openFileDialog.setSelectedFile(null);

		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			try {
				settings.setLastDirectory(openFileDialog.getCurrentDirectory().getPath());
				// read image
				NImage tmp = NImage.fromFile(openFileDialog.getSelectedFile().getPath());

				float horzResolution = tmp.getHorzResolution();
				if (horzResolution < 250.0f) {
					horzResolution = 500f;
				}
				float vertResolution = tmp.getVertResolution();
				if (vertResolution < 250.0f) {
					vertResolution = 500f;
				}

				checkingHorzResolution = horzResolution;
				checkingVertResolution = vertResolution;
				checkingImage = tmp;
				checkResolution(false);
			} catch (NotActivatedException ex) {
				ex.printStackTrace();
				JOptionPane.showMessageDialog(this, String.format("%s. Read in 'QuickStart.pdf' about component activation", ex.getMessage()));
			} catch (Exception ex) {
				ex.printStackTrace();
				JOptionPane.showMessageDialog(this,
						String.format("Error opening or extracting file \"%s\": %s", openFileDialog.getSelectedFile().getName(), ex.getMessage()), getTitle(),
						JOptionPane.ERROR_MESSAGE);
				return;
			}
		}
	}

	private void rightResolutionCheckSucceeded() {
		if (imageRight != null) {
			imageRight.dispose();
		}

		nfViewLeft.setTree(new NIndexPair[0]);
		nfViewRight.setTree(new NIndexPair[0]);

		NFinger finger = (NFinger) nfViewRight.getFinger();
		nfViewRight.setFinger(null);
		if (finger != null) {
			finger.dispose();
		}

		nfViewRight.clearSelectedArea();

		btnMatch.setEnabled(false);

		imageRight = NImage.fromImage(NPixelFormat.RGB_8U, 0, checkingImage);
		imageRight.setHorzResolution(checkingHorzResolution);
		imageRight.setVertResolution(checkingVertResolution);
		imageRight.setResolutionIsAspectRatio(false);

		checkingImage.dispose();

		lblReferenceImageSize.setText(String.format("Size: %s x %s", imageRight.getWidth(), imageRight.getHeight()));
		lblReferenceImageResolution.setText(String.format("Resolution: %.2f x %.2f", imageRight.getHorzResolution(), imageRight.getVertResolution()));

		String filePath = openFileDialog.getSelectedFile().getPath();
		lblRightFileName.setText(filePath);
		lblRightFileName.setToolTipText(filePath);

		finger = new NFinger();
		finger.setImage(imageRight);
		nfViewRight.setFinger(finger);
		nfViewRight.setShownImage(ShownImage.ORIGINAL);
		menuItemViewOriginalRight.setSelected(true);

		if (imageRight != null) {
			nfViewLeft.setTree(new NIndexPair[0]);
			nfViewRight.setTree(new NIndexPair[0]);

			NSubject subject = new NSubject();
			subject.getFingers().add(finger);

			try {
				NBiometricTask task = LongTaskDialog.runLongTask(this, this, "Extracting", subject);
				NBiometricStatus status = task.getStatus();

				if (task.getError() != null) {
					JOptionPane.showMessageDialog(this, task.getError());
					return;
				}
				if (status == NBiometricStatus.OK) {
					NFinger leftFinger = (NFinger) nfViewLeft.getFinger();
					if (leftFinger != null && leftFinger.getStatus() == NBiometricStatus.OK) {
						btnMatch.setEnabled(true);
					}
				} else {
					JOptionPane.showMessageDialog(this, "Failed to create template: " + status, getTitle(), JOptionPane.ERROR_MESSAGE);
				}
			} catch (InterruptedException e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this, e.toString());
			} catch (ExecutionException e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this, e.toString());
			}

		}
	}

	// ==============================================
	// Image processing related methods
	// ==============================================

	private void chkInvertCheckedChanged() {
		invert = chkInvert.isSelected();
		requestImageProcessing();
	}

	private void toGrayCheckedChanged() {
		convertToGrayscale = chkToGray.isSelected();
		requestImageProcessing();
	}

	private void brightnessChanged(JSlider colorSlider) {
		if (colorSlider != null) {
			int normalizedValue = -colorSlider.getValue();
			if (chkGroupBrightness.isSelected()) {
				lblBrightnessValueR.setText(String.valueOf(normalizedValue));
				lblBrightnessValueG.setText(String.valueOf(normalizedValue));
				lblBrightnessValueB.setText(String.valueOf(normalizedValue));

				if (brightnessSliderR.getValue() != colorSlider.getValue()) {
					brightnessSliderR.setValue(colorSlider.getValue());
				}
				if (brightnessSliderG.getValue() != colorSlider.getValue()) {
					brightnessSliderG.setValue(colorSlider.getValue());
				}
				if (brightnessSliderB.getValue() != colorSlider.getValue()) {
					brightnessSliderB.setValue(colorSlider.getValue());
				}
				for (int i = 0; i < 3; i++) {
					brightness[i] = normalizedValue / 100.0;
				}
			} else {
				brightness[(Integer) colorSlider.getClientProperty("tag")] = normalizedValue / 100.0;

				switch ((Integer) colorSlider.getClientProperty("tag")) {
				case 0:
					lblBrightnessValueR.setText(String.valueOf(normalizedValue));
					break;

				case 1:
					lblBrightnessValueG.setText(String.valueOf(normalizedValue));
					break;

				case 2:
					lblBrightnessValueB.setText(String.valueOf(normalizedValue));
					break;
				default:
					throw new AssertionError("Cannot happen");
				}
			}
			requestImageProcessing();
		}
	}

	private void contrastChanged(JSlider colorSlider) {
		if (colorSlider != null) {
			int normalizedValue = -colorSlider.getValue();
			if (chkGroupContrast.isSelected()) {
				lblContrastValueR.setText(String.valueOf(normalizedValue));
				lblContrastValueG.setText(String.valueOf(normalizedValue));
				lblContrastValueB.setText(String.valueOf(normalizedValue));

				if (contrastSliderR.getValue() != colorSlider.getValue()) {
					contrastSliderR.setValue(colorSlider.getValue());
				}
				if (contrastSliderG.getValue() != colorSlider.getValue()) {
					contrastSliderG.setValue(colorSlider.getValue());
				}
				if (contrastSliderB.getValue() != colorSlider.getValue()) {
					contrastSliderB.setValue(colorSlider.getValue());
				}

				for (int i = 0; i < 3; i++) {
					contrast[i] = normalizedValue / 100.0;
				}
			} else {
				contrast[(Integer) colorSlider.getClientProperty("tag")] = normalizedValue / 100.0;

				switch ((Integer) colorSlider.getClientProperty("tag")) {
				case 0:
					lblContrastValueR.setText(String.valueOf(normalizedValue));
					break;

				case 1:
					lblContrastValueG.setText(String.valueOf(normalizedValue));
					break;

				case 2:
					lblContrastValueB.setText(String.valueOf(normalizedValue));
					break;
				default:
					throw new AssertionError("Cannot happen");
				}
			}
			requestImageProcessing();
		}
	}

	private void requestImageProcessing() {
		processingRequested = true;
		nfViewLeft.revalidate();
		nfViewLeft.repaint();
	}

	private void processImage() {
		if (imageLeftOriginal != null) {
			NImage img = null;
			try {
				img = (NImage) imageLeftOriginal.clone();
			} catch (CloneNotSupportedException e) {
				e.printStackTrace();
				throw new AssertionError("Can't happen");
			}

			if (invert) {
				NRGBIP.invertSame(img);
			}

			if (Math.abs(brightness[0]) > 0 || Math.abs(brightness[1]) > 0 || Math.abs(brightness[2]) > 0 || Math.abs(contrast[0]) > 0
					|| Math.abs(contrast[1]) > 0 || Math.abs(contrast[2]) > 0) {
				NRGBIP.adjustBrightnessContrastSame(img, brightness[0], contrast[0], brightness[1], contrast[1], brightness[2], contrast[2]);
			}

			if (convertToGrayscale) {
				NImage grayImage = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, img);
				img.dispose();
				img = NImages.getGrayscaleColorWrapper(grayImage, new NRGB(), new NRGB(255, 255, 255));
			}

			NFinger oldFinger = (NFinger) nfViewLeft.getFinger();
			NFrictionRidge newFinger;
			if (record != null) {
				newFinger = NFinger.fromImageAndTemplate(img, record);
			} else {
				newFinger = new NFinger();
				newFinger.setImage(img);
			}
			newFinger.setBinarizedImage(binarizedImage);
			nfViewLeft.setFinger(newFinger);

			if (oldFinger != null) {
				oldFinger.dispose();
			}

			nfViewLeft.revalidate();
			nfViewLeft.repaint();

			menuItemViewOriginalLeft.setSelected(true);

			if (imageLeft != null) {
				imageLeft.dispose();
			}
			imageLeft = img;

			processingRequested = false;
		}
	}

	private void resetBrightness() {
		brightnessSliderR.setValue(0);
		brightnessSliderG.setValue(0);
		brightnessSliderB.setValue(0);

		requestImageProcessing();
	}

	private void resetContrast() {
		contrastSliderR.setValue(0);
		contrastSliderG.setValue(0);
		contrastSliderB.setValue(0);

		requestImageProcessing();
	}

	private void resetAll() {
		chkInvert.setSelected(false);
		chkInvertCheckedChanged();

		brightnessSliderR.setValue(0);
		brightnessSliderG.setValue(0);
		brightnessSliderB.setValue(0);

		contrastSliderR.setValue(0);
		contrastSliderG.setValue(0);
		contrastSliderB.setValue(0);

		chkToGray.setSelected(false);
		toGrayCheckedChanged();

		requestImageProcessing();
	}

	// ==============================================
	// Image transformation related methods
	// ==============================================

	private void onRecordChanged(NFRecord value) {
		NFinger finger = (NFinger) nfViewLeft.getFinger();
		HNObject handle = finger.getHandle();
		if (finger != null) {
			NFAttributes attributes = finger.getObjects().get(handle);
			if (attributes != null) {
				attributes.setTemplate(value);
			}
		}
		record = value;
	}

	private void rotate90Clockwise() {
		if (imageLeftOriginal != null) {
			NImage transformed = imageLeftOriginal.rotateFlip(NImageRotateFlipType.ROTATE_90_FLIP_NONE);
			if (record != null) {
				onRecordChanged(TransformFeatures.getInstance().rotate90(record));
			}
			onImageTransformed(transformed);
		}
	}

	private void rotate90CounterColckwise() {
		if (imageLeftOriginal != null) {
			NImage transformed = imageLeftOriginal.rotateFlip(NImageRotateFlipType.ROTATE_270_FLIP_NONE);
			if (record != null) {
				onRecordChanged(TransformFeatures.getInstance().rotate270(record));
			}
			onImageTransformed(transformed);
		}
	}

	private void rotate180() {
		if (imageLeftOriginal != null) {
			NImage transformed = imageLeftOriginal.rotateFlip(NImageRotateFlipType.ROTATE_180_FLIP_NONE);
			if (record != null) {
				onRecordChanged(TransformFeatures.getInstance().rotate180(record));
			}
			onImageTransformed(transformed);
		}
	}

	private void flipHorizontally() {
		if (imageLeftOriginal != null) {
			imageLeftOriginal.flipHorizontally();
			if (record != null) {
				onRecordChanged(TransformFeatures.getInstance().flipHorizontally(record));
			}
			onImageTransformed(null);
		}
	}

	private void flipVertically() {
		if (imageLeftOriginal != null) {
			imageLeftOriginal.flipVertically();
			if (record != null) {
				onRecordChanged(TransformFeatures.getInstance().flipVertically(record));
			}
			onImageTransformed(null);
		}
	}

	private void cropToSelection() {
		if (imageLeftOriginal != null) {
			Rectangle rect = nfViewLeft.getSelectedImageArea();
			if (!nfViewLeft.isPartOfImageSelected()) {
				JOptionPane.showMessageDialog(this, "Please select part of template with Area Selection Tool first!");
			} else if (rect.x < 0 || rect.y < 0 || rect.x + rect.width > imageLeftOriginal.getWidth() || rect.y + rect.height > imageLeftOriginal.getHeight()) {
				JOptionPane.showMessageDialog(this, "Please select part of template with Area Selection Tool first (only image)!");
			} else {
				try {
					NImage transformed = imageLeftOriginal.crop(rect.x, rect.y, (int) rect.getWidth(), (int) rect.getHeight());

					if (record != null) {
						double coeffX = (double) NFRecord.RESOLUTION / imageLeftOriginal.getHorzResolution();
						double coeffY = (double) NFRecord.RESOLUTION / imageLeftOriginal.getVertResolution();
						onRecordChanged(TransformFeatures.getInstance().crop(record, transformed, (int) (rect.x * coeffX), (int) (rect.y * coeffY),
								(int) Math.ceil(rect.getWidth() * coeffX), (int) Math.ceil(rect.getHeight() * coeffY)));
					}

					onImageTransformed(transformed);
				} catch (IllegalArgumentException e) {
					JOptionPane.showMessageDialog(this, "Please select part of template with Area Selection Tool first!");
					e.printStackTrace();
				} catch (Exception e) {
					JOptionPane.showMessageDialog(this, e.getMessage());
					e.printStackTrace();
				}
			}
		}
	}

	private void onImageTransformed(NImage transformed) {
		nfViewLeft.clearSelectedArea();
		if (transformed != null) {
			if (imageLeftOriginal != null) {
				imageLeftOriginal.dispose();
			}
			imageLeftOriginal = transformed;
		}
		processImage();
		nfViewLeft.updateUI();
	}

	private void invertMinutiae() {
		if (record != null) {
			try {
				Object[] minutiae = record.getMinutiae().toArray();
				record.getMinutiae().clear();
				for (int i = 0; i < minutiae.length; i++) {
					NFMinutia minutia = (NFMinutia) minutiae[i];
					if (minutia.type == NFMinutiaType.BIFURCATION) {
						minutia.type = NFMinutiaType.END;
					} else if (minutia.type == NFMinutiaType.END) {
						minutia.type = NFMinutiaType.BIFURCATION;
					}
					record.getMinutiae().add(minutia);
				}
				nfViewLeft.repaint();

			} catch (Exception e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this, e.getMessage());
			}

		}
	}

	private void performBandpassFiltering() {
		NImage workingImage = getWorkingImage();
		if (workingImage != null) {
			BandpassFilteringDialog fft;
			try {
				fft = new BandpassFilteringDialog(this, this, workingImage);
			} catch (Exception e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this, e.toString());
				return;
			}
			Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
			fft.setLocation(screenSize.width / 2 - fft.getPreferredSize().width / 2, screenSize.height / 2 - fft.getPreferredSize().height / 2);
			fft.setVisible(true);

		}
	}

	// ==============================================
	// Extraction related methods
	// ==============================================

	private NImage getWorkingImage() {
		if (radioEdited.isSelected()) {
			return imageLeft;
		}
		return imageLeftOriginal;
	}

	private NFRecord moveTemplateCoordinates(NFRecord inData, int width, int height, int horzResolution, int vertResolution, int offsetX, int offsetY) {
		if (inData != null) {
			double coeffX = (double) NFRecord.RESOLUTION / (float) horzResolution;
			double coeffY = (double) NFRecord.RESOLUTION / (float) vertResolution;

			NFRecord record = new NFRecord((short) width, (short) height, (short) horzResolution, (short) vertResolution);
			record.setMinutiaFormat(inData.getMinutiaFormat());
			record.setCBEFFProductType(inData.getCBEFFProductType());

			int templateWidth = (int) (width * coeffX);
			int templateHeight = (int) (height * coeffY);
			for (NFMinutia min : inData.getMinutiae()) {
				min.x += (short) (offsetX * coeffX);
				min.y += (short) (offsetY * coeffY);
				if (min.x >= 0 && min.x < templateWidth && min.y >= 0 && min.y < templateHeight) {
					record.getMinutiae().add(min);
				}
			}
			for (NFDelta delta : inData.getDeltas()) {
				delta.x += (short) (offsetX * coeffX);
				delta.y += (short) (offsetY * coeffY);
				if (delta.x >= 0 && delta.x < templateWidth && delta.y >= 0 && delta.y < templateHeight) {
					record.getDeltas().add(delta);
				}
			}
			for (NFCore core : inData.getCores()) {
				core.x += (short) (offsetX * coeffX);
				core.y += (short) (offsetY * coeffY);
				if (core.x >= 0 && core.x < templateWidth && core.y >= 0 && core.y < templateHeight) {
					record.getCores().add(core);
				}
			}
			for (NFDoubleCore doubleCore : inData.getDoubleCores()) {
				doubleCore.x += (short) (offsetX * coeffX);
				doubleCore.y += (short) (offsetY * coeffY);
				if (doubleCore.x >= 0 && doubleCore.x < templateWidth && doubleCore.y >= 0 && doubleCore.y < templateHeight) {
					record.getDoubleCores().add(doubleCore);
				}
			}

			return record;
		}
		return null;
	}

	private void extractImage(NImage image) {
		try {
			nfViewLeft.setTree(new NIndexPair[0]);
			nfViewRight.setTree(new NIndexPair[0]);

			boolean needCropping = nfViewLeft.isPartOfImageSelected();
			Rectangle cropBounds = new Rectangle();
			if (needCropping) {
				cropBounds = nfViewLeft.getSelectedImageArea();
			}

			NImage workingImage = image;
			if (needCropping) {
				workingImage = image.crop(cropBounds.x, cropBounds.y, cropBounds.width, cropBounds.height);
			}

			NSubject subject = new NSubject();
			NFinger finger = new NFinger();
			finger.setImage(workingImage);
			subject.getFingers().add(finger);
			NBiometricTask task = LongTaskDialog.runLongTask(this, this, "Extracting", subject);
			NBiometricStatus status = task.getStatus();
			if (task.getError() != null) {
				JOptionPane.showMessageDialog(this, task.getError());
				return;
			}
			if (status == NBiometricStatus.OK) {

				NFRecord record = finger.getObjects().get(0).getTemplate();
				if (needCropping) {
					NImage resultImage = (NImage) image.clone();
					NImage tempBinarizedImage = NImage.fromImage(NPixelFormat.RGB_8U, resultImage.getStride(), finger.getBinarizedImage());
					tempBinarizedImage.copyTo(resultImage, cropBounds.x, cropBounds.y);
					binarizedImage = resultImage;
					onRecordChanged(moveTemplateCoordinates(record, image.getWidth(), image.getHeight(), (int) image.getHorzResolution(),
							(int) image.getVertResolution(), cropBounds.x, cropBounds.y));
				} else {
					binarizedImage = finger.getBinarizedImage();
					onRecordChanged(record);
				}

				NFinger rightFinger = (NFinger) nfViewRight.getFinger();
				if (rightFinger != null && rightFinger.getStatus() == NBiometricStatus.OK) {
					btnMatch.setEnabled(true);
				}
			} else {

				if (record != null) {
					nfViewLeft.setSelectedDeltaIndex(-1);
					nfViewLeft.setSelectedMinutiaIndex(-1);
					nfViewLeft.setSelectedCoreIndex(-1);
					nfViewLeft.setSelectedDoubleCoreIndex(-1);

					NFRecord newRecord = new NFRecord(this.record.getWidth(), this.record.getHeight(), this.record.getHorzResolution(),
							this.record.getVertResolution());
					newRecord.setCBEFFProductType(this.record.getCBEFFProductType());
					newRecord.setMinutiaFormat(this.record.getMinutiaFormat());
					newRecord.setRidgeCountsType(this.record.getRidgeCountsType());

					onRecordChanged(newRecord);
				}
				binarizedImage = null;
				JOptionPane.showMessageDialog(this, "Failed to create template: " + status, getTitle(), JOptionPane.ERROR_MESSAGE);
			}
			requestImageProcessing();

		} catch (NotActivatedException ex) {
			ex.printStackTrace();
			JOptionPane.showMessageDialog(this, String.format("%s. Read in 'QuickStart.pdf' about component activation", ex.getMessage()));
		} catch (Exception ex) {
			ex.printStackTrace();
			JOptionPane.showMessageDialog(this, ex.getMessage(), "Extraction error", JOptionPane.ERROR_MESSAGE);
		}

	}

	private void extract() {
		NImage workingImage = getWorkingImage();
		if (workingImage != null) {
			extractImage(workingImage);
		}
	}

	private void showExtractionSettings() {
		ExtractionSettingsDialog frmExtractionSettings = new ExtractionSettingsDialog(this, this, biometricClient.getFingersQualityThreshold());
		frmExtractionSettings.setQualityThreshold(biometricClient.getFingersQualityThreshold());
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		frmExtractionSettings.setLocation(screenSize.width / 2 - frmExtractionSettings.getPreferredSize().width / 2, screenSize.height / 2
				- frmExtractionSettings.getPreferredSize().height / 2);
		frmExtractionSettings.setVisible(true);

	}

	private void updateRecord(NImage workingImage) {
		NSubject subject = new NSubject();
		NFinger finger = (NFinger) NFinger.fromImageAndTemplate(workingImage, record);
		subject.getFingers().add(finger);
		biometricClient.createTemplate(subject);
		nfViewLeft.revalidate();
		nfViewLeft.repaint();
	}

	// ==============================================
	// Matching related methods
	// ==============================================

	private void match() {
		if (!initMatching()) {
			return;
		}
		new MatchingWorker().execute();
	}

	private boolean initMatching() {
		try {

			if (!NLicense.obtainComponents("/local", 5000, "Biometrics.FingerMatching")) {
				JOptionPane.showMessageDialog(this, "Could not obtain license for fingerprint matcher.", "Error", JOptionPane.ERROR_MESSAGE);
				return false;
			} else {
				return true;
			}

		} catch (Exception e) {
			JOptionPane.showMessageDialog(this, e.toString());
			return false;
		}
	}

	// ==============================================
	// Add feature related methods
	// ==============================================

	private Dimension bitmapToTemplateSize(int ptX, int ptY) {
		ptX = (int) (ptX * (float) NFRecord.RESOLUTION / imageLeftOriginal.getHorzResolution() / nfViewLeft.getScale());
		ptY = (int) (ptY * (float) NFRecord.RESOLUTION / imageLeftOriginal.getVertResolution() / nfViewLeft.getScale());
		return new Dimension(ptX, ptY);
	}

	private Point bitmapToTemplateCoords(Point pt) {
		pt.x = (int) (pt.x * (float) NFRecord.RESOLUTION / imageLeftOriginal.getHorzResolution());
		pt.y = (int) (pt.y * (float) NFRecord.RESOLUTION / imageLeftOriginal.getVertResolution());
		return pt;
	}

	private double calculateDraggedAngle(Point pt, Point dragPt) {
		return Math.atan2(dragPt.y - pt.y, dragPt.x - pt.x);
	}

	private boolean addFeature(Point pt, Point dragPt) {
		if (imageLeftOriginal != null) {
			try {
				if (record == null) {
					// create a new template if not exists
					int templateWidth = imageLeftOriginal.getWidth();
					int templateHeight = imageLeftOriginal.getHeight();
					Dimension d = bitmapToTemplateSize(templateWidth, templateHeight);
					int vertResolution = (int) imageLeftOriginal.getVertResolution();
					int horzResolution = (int) imageLeftOriginal.getHorzResolution();
					if (vertResolution < 250) {
						vertResolution = NFRecord.RESOLUTION;
					}
					if (horzResolution < 250) {
						horzResolution = NFRecord.RESOLUTION;
					}
					record = new NFRecord((short) d.width, (short) d.height, (short) horzResolution, (short) vertResolution);
					record.setCBEFFProductType((short) 256);
					record.setMinutiaFormat(EnumSet.of(NFMinutiaFormat.HAS_QUALITY, NFMinutiaFormat.HAS_G, NFMinutiaFormat.HAS_CURVATURE));
					record.setRidgeCountsType(NFRidgeCountsType.FOUR_NEIGHBORS_WITH_INDEXES);

					processImage();
				}

				double angle = calculateDraggedAngle(pt, dragPt);

				pt = bitmapToTemplateCoords(pt);

				int addX = pt.x;
				int addY = pt.y;

				int index = -1;
				NFMinutia minutia;
				switch (addMode) {
				case END_MINUTIA:
					minutia = new NFMinutia((short) addX, (short) addY, NFMinutiaType.END, angle);
					index =	record.getMinutiae().addEx(minutia);
					break;

				case BIFURCATION_MINUTIA:
					minutia = new NFMinutia((short) addX, (short) addY, NFMinutiaType.BIFURCATION, angle);
					index =	record.getMinutiae().addEx(minutia);
					break;

				case DELTA:
					NFDelta delta = new NFDelta((short) addX, (short) addY);
					index =	record.getDeltas().addEx(delta);
					break;

				case CORE:
					NFCore core = new NFCore((short) addX, (short) addY, angle);
					index =	record.getCores().addEx(core);
					break;

				case DOUBLE_CORE:
					NFDoubleCore doubleCore = new NFDoubleCore((short) addX, (short) addY);
					index = record.getDoubleCores().addEx(doubleCore);
					break;
				case NONE:
					break;
				default:
					throw new AssertionError("Cannot happen");
				}

				if (index >= 0) {
					switch (addMode) {
					case END_MINUTIA:
					case BIFURCATION_MINUTIA:
						nfViewLeft.setSelectedMinutiaIndex(index);
						break;

					case DELTA:
						nfViewLeft.setSelectedDeltaIndex(index);
						break;

					case CORE:
						nfViewLeft.setSelectedCoreIndex(index);
						break;

					case DOUBLE_CORE:
						nfViewLeft.setSelectedDoubleCoreIndex(index);
						break;
					case NONE:
						break;
					default:
						throw new AssertionError("Cannot happen");
					}
					repaint();
				}

				NFinger fingerRight = (NFinger) nfViewRight.getFinger();
				if (fingerRight != null && fingerRight.getStatus() == NBiometricStatus.OK) {
					btnMatch.setEnabled(true);
				}
				return true;
			} catch (Exception ex) {
				ex.printStackTrace();
				JOptionPane.showMessageDialog(this, ex.getMessage());
				return false;
			}
		}
		JOptionPane.showMessageDialog(this, "Please open an image before editing!");
		return false;
	}

	// ==============================================
	// Delete feature related methods
	// ==============================================

	private void deleteFeature() {
		NFinger finger = (NFinger) nfViewLeft.getFinger();
		if (finger != null) {
			if (record != null) {
				boolean deleted = false;
				int index = nfViewLeft.getSelectedMinutiaIndex();
				if (index >= 0) {
					nfViewLeft.setSelectedMinutiaIndex(-1);
					record.getMinutiae().remove(index);
					deleted = true;
				}
				index = nfViewLeft.getSelectedDeltaIndex();
				if (index >= 0) {
					nfViewLeft.setSelectedDeltaIndex(-1);
					record.getDeltas().remove(index);
					deleted = true;
				}
				index = nfViewLeft.getSelectedCoreIndex();
				if (index >= 0) {
					nfViewLeft.setSelectedCoreIndex(-1);
					record.getCores().remove(index);
					deleted = true;
				}
				index = nfViewLeft.getSelectedDoubleCoreIndex();
				if (index >= 0) {
					nfViewLeft.setSelectedDoubleCoreIndex(-1);
					record.getDoubleCores().remove(index);
					deleted = true;
				}

				if (deleted) {
					nfViewLeft.setTree(new NIndexPair[0]);
					nfViewRight.setTree(new NIndexPair[0]);

					nfViewLeft.revalidate();
					nfViewLeft.repaint();
				}
			}
		}

	}

	// ==============================================
	// File Save related methods
	// ==============================================

	private void saveTemplate() {
		NImage workingImage = getWorkingImage();

		if (workingImage != null && record != null) {
			saveTemplateDialog.setCurrentDirectory(new File(settings.getLastDirectory()));
			if (saveTemplateDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
				try {
					settings.setLastDirectory(saveTemplateDialog.getCurrentDirectory().getPath());
					if (record.getRequiresUpdate()) {
						updateRecord(workingImage);
					}
					NBuffer buffer = record.save();

					String savePath = saveTemplateDialog.getSelectedFile().getPath();
					String fileName = saveTemplateDialog.getSelectedFile().getName();
					if (fileName.lastIndexOf(".") == -1) {
						FileFilter selectedFilter = saveTemplateDialog.getFileFilter();
						if (selectedFilter instanceof Utils.TemplateFileFilter) {
							savePath = savePath + ".data";
						}
					}
					NFile.writeAllBytes(savePath, buffer);
				} catch (Exception e) {
					e.printStackTrace();
					JOptionPane.showMessageDialog(this, "Error while saving template.");
				}
			}
		} else {
			JOptionPane.showMessageDialog(this, "Nothing to save.");
		}
	}

	private void saveImageToFile(NImage image) {
		saveImageDialog.setCurrentDirectory(new File(settings.getLastDirectory()));
		if (saveImageDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			try {
				settings.setLastDirectory(saveImageDialog.getCurrentDirectory().getPath());
				String savePath = saveImageDialog.getSelectedFile().getPath();
				String fileName = saveImageDialog.getSelectedFile().getName();
				if (fileName.lastIndexOf(".") == -1) {
					FileFilter selectedFilter = saveImageDialog.getFileFilter();
					if (selectedFilter instanceof Utils.ImageFileFilter) {
						savePath = savePath + '.' + ((Utils.ImageFileFilter) saveImageDialog.getFileFilter()).getExtensions().get(0);
					}
				}
				image.save(savePath);
			} catch (Exception e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this, "Failed to save image to file.");
			}
		}
	}

	private void saveLatentImage() {
		NImage workingImage = getWorkingImage();
		if (workingImage != null) {
			saveImageToFile(workingImage);
		} else {
			JOptionPane.showMessageDialog(this, "Nothing to save.");
		}
	}

	private void saveReferenceImage() {
		if (imageRight != null) {
			saveImageToFile(imageRight);
		} else {
			JOptionPane.showMessageDialog(this, "Nothing to save.");
		}
	}

	// ==============================================
	// Event handling private methods
	// ==============================================

	// ==============================================
	// Frame loading
	// ==============================================

	private void loadingFrame() {
		try {
			// initialize biometric client
			biometricClient = new NBiometricClient();
			biometricClient.setFingersQualityThreshold((byte) 0);
			biometricClient.setFingersReturnBinarizedImage(true);
			biometricClient.setMatchingWithDetails(true);
			biometricClient.setProperty("Fingers.MinimalMinutiaCount", 0);

			StringBuilder allImageFilters = new StringBuilder(64);
			for (NImageFormat format : NImageFormat.getFormats()) {
				allImageFilters.append(format.getFileFilter()).append(';');
			}
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getTIFF().getFileFilter(), "TIFF files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getPNG().getFileFilter(), "PNG files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getWSQ().getFileFilter(), "WSQ files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getBMP().getFileFilter(), "BMP files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getIHead().getFileFilter(), "NIST IHead files"));
			openFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(allImageFilters.toString(), "All image files"));

			saveImageDialog.removeChoosableFileFilter(saveImageDialog.getFileFilter());
			saveImageDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
			saveImageDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getPNG().getFileFilter(), "PNG files"));
			saveImageDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getWSQ().getFileFilter(), "WSQ files"));
			saveImageDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
			saveImageDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getBMP().getFileFilter(), "BMP files"));
			saveImageDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getIHead().getFileFilter(), "NIST IHead files"));

			saveTemplateDialog.removeChoosableFileFilter(saveTemplateDialog.getFileFilter());
			saveTemplateDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("Template files"));

			cmbMatchingFar.addItem("0.1%");
			cmbMatchingFar.addItem("0.01%");
			cmbMatchingFar.addItem("0.001%");
			cmbMatchingFar.addActionListener(this);
			cmbMatchingFar.updateUI();
			cmbMatchingFar.setSelectedIndex(1);

			loadZoomCombo(cmbZoomLeft);
			loadZoomCombo(cmbZoomRight);

			// select pointer tool by default
			nfViewLeft.setActiveTool(new PointerTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>());
			addMode = AddMode.NONE;
		} catch (Exception ex) {
			ex.printStackTrace();
			JOptionPane.showMessageDialog(this, ex.getMessage());
		}
	}

	private void loadZoomCombo(JComboBox comboBox) {
		for (float zoom : zoomFactors) {
			comboBox.addItem(String.valueOf(((int) (zoom * 100))) + "%");
		}
		comboBox.setSelectedIndex(DEFAULT_ZOOM_FACTOR);
	}

	// ==============================================
	// Zooming events
	// ==============================================

	private int setZoomFactor(NFingerView nfView, int currentZoomFactor, int newZoomFactor) {
		if (newZoomFactor >= zoomFactors.length) {
			return currentZoomFactor;
		}
		if (newZoomFactor < 0) {
			return currentZoomFactor;
		}

		float zoom = zoomFactors[newZoomFactor];
		nfView.setScale(zoom);

		return newZoomFactor;
	}

	private void zoomInLeft() {
		leftZoomFactor = setZoomFactor(nfViewLeft, leftZoomFactor, leftZoomFactor + 1);
		cmbZoomLeft.setSelectedIndex(leftZoomFactor);
	}

	private void zoomOutLeft() {
		leftZoomFactor = setZoomFactor(nfViewLeft, leftZoomFactor, leftZoomFactor - 1);
		cmbZoomLeft.setSelectedIndex(leftZoomFactor);
	}

	private void zoomOriginalLeft() {
		leftZoomFactor = setZoomFactor(nfViewLeft, leftZoomFactor, DEFAULT_ZOOM_FACTOR);
		cmbZoomLeft.setSelectedIndex(leftZoomFactor);
	}

	private void zoomInRight() {
		rightZoomFactor = setZoomFactor(nfViewRight, rightZoomFactor, rightZoomFactor + 1);
		cmbZoomRight.setSelectedIndex(rightZoomFactor);
	}

	private void zoomOutRight() {
		rightZoomFactor = setZoomFactor(nfViewRight, rightZoomFactor, rightZoomFactor - 1);
		cmbZoomRight.setSelectedIndex(rightZoomFactor);
	}

	private void zoomOriginalRight() {
		rightZoomFactor = setZoomFactor(nfViewRight, rightZoomFactor, DEFAULT_ZOOM_FACTOR);
		cmbZoomRight.setSelectedIndex(rightZoomFactor);
	}

	private void leftZoomChanged() {
		leftZoomFactor = setZoomFactor(nfViewLeft, leftZoomFactor, cmbZoomLeft.getSelectedIndex());
	}

	private void rightZoomChanged() {
		rightZoomFactor = setZoomFactor(nfViewRight, rightZoomFactor, cmbZoomRight.getSelectedIndex());
	}

	// ==============================================
	// view mode change events
	// ==============================================

	private void viewOriginalLeftChanged() {
		nfViewLeft.setShownImage((menuItemViewOriginalLeft.isSelected()) ? ShownImage.ORIGINAL : ShownImage.RESULT);
		nfViewLeft.revalidate();
		nfViewLeft.repaint();
	}

	private void viewOriginalRightChanged() {
		nfViewRight.setShownImage((menuItemViewOriginalRight.isSelected()) ? ShownImage.ORIGINAL : ShownImage.RESULT);
		nfViewRight.revalidate();
		nfViewRight.repaint();
	}

	// ==============================================
	// Tool selection events
	// ==============================================

	private void pointerToolSelected() {
		if (radioPointer.isSelected()) {
			nfViewLeft.setActiveTool(new PointerTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>());
			addMode = AddMode.NONE;
		}
	}

	private void selectAreaToolSelected() {
		if (radioSelectArea.isSelected()) {
			nfViewLeft.setActiveTool(new RectangleSelectionTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>());
			addMode = AddMode.NONE;
		}
	}

	private void addEndMinutiaToolSelected() {
		if (radioAddEndMinutia.isSelected()) {
			AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor> addFeatTool = new AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>();
			addFeatTool.addFeatureAddListener(this);
			nfViewLeft.setActiveTool(addFeatTool);
			addMode = AddMode.END_MINUTIA;
		}
	}

	private void addBifurcationMinutiaToolSelected() {
		if (radioAddBifurcationMinutia.isSelected()) {
			AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor> addFeatTool = new AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>();
			addFeatTool.addFeatureAddListener(this);
			nfViewLeft.setActiveTool(addFeatTool);
			addMode = AddMode.BIFURCATION_MINUTIA;
		}
	}

	private void addDeltaToolSelected() {
		if (radioAddDelta.isSelected()) {
			AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor> addFeatTool = new AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>(
					false);
			addFeatTool.addFeatureAddListener(this);
			nfViewLeft.setActiveTool(addFeatTool);
			addMode = AddMode.DELTA;
		}
	}

	private void addCoreToolSelected() {
		if (radioAddCore.isSelected()) {
			AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor> addFeatTool = new AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>();
			addFeatTool.addFeatureAddListener(this);
			nfViewLeft.setActiveTool(addFeatTool);
			addMode = AddMode.CORE;
		}
	}

	private void addDoubleCoreToolSelected() {
		if (radioAddDoubleCore.isSelected()) {
			AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor> addFeatTool = new AddFeaturesTool<NFMinutia, NFCore, NFDelta, NFMinutiaNeighbor>(
					false);
			addFeatTool.addFeatureAddListener(this);
			nfViewLeft.setActiveTool(addFeatTool);
			addMode = AddMode.DOUBLE_CORE;
		}
	}

	private void clearGridBagConstraints() {
		c.gridwidth = 1;
		c.gridheight = 1;
		c.weightx = 0;
		c.weighty = 0;
	}

	private void addToGridBagLayout(int x, int y, JPanel parent, JComponent component) {
		c.gridx = x;
		c.gridy = y;
		parent.add(component, c);
	}

	private void addToGridBagLayout(int x, int y, int width, int height, JPanel parent, JComponent component) {
		c.gridx = x;
		c.gridy = y;
		c.gridwidth = width;
		c.gridheight = height;
		parent.add(component, c);
	}

	private void addToGridBagLayout(int x, int y, int width, int height, int weightX, int weightY, JPanel parent, JComponent component) {
		c.gridx = x;
		c.gridy = y;
		c.gridwidth = width;
		c.gridheight = height;
		c.weightx = weightX;
		c.weighty = weightY;
		parent.add(component, c);
	}

	// ==============================================
	// Event handling ActionListener
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnOpenLeft) {
			openLeft();
		} else if (source == btnOpenRight) {
			openRight();
		} else if (source == chkInvert) {
			chkInvertCheckedChanged();
		} else if (source == chkToGray) {
			toGrayCheckedChanged();
		} else if (source == btnResetBrightness) {
			resetBrightness();
		} else if (source == btnResetContrast) {
			resetContrast();
		} else if (source == btnResetAll) {
			resetAll();
		} else if (source == menuItemRotate90Clockwise) {
			rotate90Clockwise();
		} else if (source == menuItemRotate90CounterClockwise) {
			rotate90CounterColckwise();
		} else if (source == menuItemRotate180) {
			rotate180();
		} else if (source == menuItemFlipHorizontally) {
			flipHorizontally();
		} else if (source == menuItemFlipVertically) {
			flipVertically();
		} else if (source == menuItemCropToSelection) {
			cropToSelection();
		} else if (source == menuItemInvertMinutiae) {
			invertMinutiae();
		} else if (source == btnExtract) {
			extract();
		} else if (source == menuItemExtraction) {
			showExtractionSettings();
		} else if (source == cmbMatchingFar) {
			try {
				biometricClient.setMatchingThreshold(Utils.matchingThresholdFromString(cmbMatchingFar.getSelectedItem().toString()));
			} catch (ParseException ex) {
				ex.printStackTrace();
			}
		} else if (source == btnMatch) {
			match();
		} else if (source == btnSaveTemplate || source == menuItemSaveTemplate) {
			saveTemplate();
		} else if (source == menuItemSaveLatentImage) {
			saveLatentImage();
		} else if (source == menuItemSaveReferenceImage) {
			saveReferenceImage();
		} else if (source == menuItemZoomInLeft || source == btnZoomInLeft || source == menuItemLeftRightClickZoomIn) {
			zoomInLeft();
		} else if (source == menuItemZoomOutLeft || source == btnZoomOutLeft || source == menuItemLeftRightClickZoomOut) {
			zoomOutLeft();
		} else if (source == menuItemOriginalLeft || source == menuItemLeftRightClickOriginal) {
			zoomOriginalLeft();
		} else if (source == menuItemZoomInRight || source == btnZoomInRight || source == menuItemRightRightClickZoomIn) {
			zoomInRight();
		} else if (source == menuItemZoomOutRight || source == btnZoomOutRight || source == menuItemRightRightClickZoomOut) {
			zoomOutRight();
		} else if (source == menuItemOriginalRight || source == menuItemRightRightClickOriginal) {
			zoomOriginalRight();
		} else if (source == cmbZoomLeft) {
			leftZoomChanged();
		} else if (source == cmbZoomRight) {
			rightZoomChanged();
		} else if (source == menuItemViewOriginalLeft) {
			viewOriginalLeftChanged();
		} else if (source == menuItemViewOriginalRight) {
			viewOriginalRightChanged();
		} else if (source == radioPointer) {
			pointerToolSelected();
		} else if (source == radioSelectArea) {
			selectAreaToolSelected();
		} else if (source == radioAddEndMinutia) {
			addEndMinutiaToolSelected();
		} else if (source == radioAddBifurcationMinutia) {
			addBifurcationMinutiaToolSelected();
		} else if (source == radioAddDelta) {
			addDeltaToolSelected();
		} else if (source == radioAddCore) {
			addCoreToolSelected();
		} else if (source == radioAddDoubleCore) {
			addDoubleCoreToolSelected();
		} else if (source == menuItemExit) {
			System.exit(0);
		} else if (source == menuItemAbout) {
			AboutBox.show();
		} else if (source == menuItemBandpassFiltering) {
			performBandpassFiltering();
		} else if (source == menuItemLeftRightClickDelete) {
			deleteFeature();
		}

	}

	// ==============================================
	// Event handling ChangeListener
	// ==============================================

	public void stateChanged(ChangeEvent e) {
		Object source = e.getSource();
		if (source == brightnessSliderR || source == brightnessSliderG || source == brightnessSliderB) {
			brightnessChanged((JSlider) source);
		} else if (source == contrastSliderR || source == contrastSliderG || source == contrastSliderB) {
			contrastChanged((JSlider) source);
		}
	}

	// ==============================================
	// Interface methods MainFrameEventListener
	// ==============================================

	public void resolutionCheckCompleted(boolean isOK, double horzRes, double vertRes) {
		if (isOK) {
			checkingHorzResolution = (float) horzRes;
			checkingVertResolution = (float) vertRes;
			if (isCheckingLeft) {
				leftResolutionCheckSuceeded();
			} else {
				rightResolutionCheckSucceeded();
			}
		} else {
			checkingHorzResolution = checkingImage.getHorzResolution();
			checkingVertResolution = checkingImage.getVertResolution();
		}
	}

	public NBiometricTask executeTask(NSubject subject) {
		NBiometricTask task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);
		biometricClient.performTask(task);
		return task;
	}

	public void bandPassFilteringAccepted(NImage resultImage) {
		imageLeftOriginal = NImage.fromImage(NPixelFormat.RGB_8U, 0, resultImage);
		processImage();
		nfViewLeft.setShownImage(ShownImage.ORIGINAL);
		nfViewLeft.updateUI();
	}

	public void extractionSettingsSelected(int value) {
		biometricClient.setFingersQualityThreshold((byte) value);
	}

	// ==============================================
	// Interface methods FeatureAddListener
	// ==============================================

	public void featureAddCompleted(FeatureAddEvent e) {
		addFeature(e.getStart(), e.getEnd());
	}

	// ===================================================================
	// Private class extending BasicSliderUI to create color slider
	// ===================================================================

	private static final class ColorSliderUI extends BasicSliderUI {

		// ==============================================
		// Private fields
		// ==============================================

		private Color color;

		// ==============================================
		// Private constructor
		// ==============================================

		private ColorSliderUI(JSlider slider, Color color) {
			super(slider);
			this.color = color;
		}

		// ==============================================
		// Overridden methods
		// ==============================================

		@Override
		public void paintTrack(Graphics g) {
			super.paintTrack(g);
			g.setColor(color);
			g.fillRect(trackRect.width / 2 - 1, trackRect.y, 3, trackRect.height);
		}
	}

	// =========================================================================
	// Private class extending JButton providing popup menus for toolbar
	// =========================================================================

	private static final class ToolbarMenu extends JButton implements ActionListener, MouseListener {

		// ==============================================
		// Private static fields
		// ==============================================

		private static final long serialVersionUID = 1L;

		// ==============================================
		// Private fields
		// ==============================================

		private JPopupMenu popupMenu;

		// ==============================================
		// Private constructor
		// ==============================================

		private ToolbarMenu(String text, JPopupMenu popupMenu) {
			super(text);
			this.popupMenu = popupMenu;
			addActionListener(this);
			addMouseListener(this);
		}

		// ==============================================
		// Interface methods
		// ==============================================

		public void actionPerformed(ActionEvent e) {
			if (popupMenu.isShowing()) {
				popupMenu.setVisible(false);
			} else {
				popupMenu.setVisible(true);
				popupMenu.show(this, 0, this.getHeight());
			}
		}

		public void mouseClicked(MouseEvent e) {
		}

		public void mouseEntered(MouseEvent e) {
		}

		public void mouseExited(MouseEvent e) {
		}

		public void mousePressed(MouseEvent e) {
			if (popupMenu.isShowing()) {
				popupMenu.setVisible(false);
			}
		}

		public void mouseReleased(MouseEvent e) {
			if (!popupMenu.isShowing()) {
				popupMenu.setVisible(true);
				popupMenu.show(this, 0, this.getHeight());
			}
		}
	}

	// =========================================================================
	// Private class extending SwingWorker which does matching templates
	// =========================================================================

	private final class MatchingWorker extends SwingWorker<Integer, String> {

		// ==============================================
		// Private fields
		// ==============================================

		private NMatchingDetails matchDetails;
		private int score = 0;

		// ==============================================
		// Overridden methods
		// ==============================================

		@Override
		protected Integer doInBackground() {
			try {
				NImage workingImage = getWorkingImage();
				publish("Matching...");

				NFRecord rightRec = nfViewRight.getFinger().getObjects().get(0).getTemplate();
				if (record.getRequiresUpdate()) {
					updateRecord(workingImage);
				}
				NSubject subjectRight = new NSubject();
				NSubject subjectLeft = new NSubject();

				subjectRight.setTemplateBuffer(rightRec.save());
				subjectLeft.setTemplateBuffer(record.save());

				NBiometricStatus status = biometricClient.verify(subjectLeft, subjectRight);
				NIndexPair[] matedMinutiae = new NIndexPair[0];
				if (status == NBiometricStatus.OK) {
					NMatchingResult matchingResults = subjectLeft.getMatchingResults().get(0);
					matchDetails = matchingResults.getMatchingDetails();
					score = matchingResults.getScore();
					matedMinutiae = matchDetails.getFingers().get(0).getMatedMinutiae();
				}
				nfViewLeft.setMatedMinutiae(matedMinutiae);
				nfViewRight.setMatedMinutiae(matedMinutiae);
			} catch (NotActivatedException e) {
				publish("Matching failed");
				e.printStackTrace();
				JOptionPane.showMessageDialog(MainFrame.this, "Matcher is not activated!");
			} catch (Exception e) {
				publish("Matching failed");
				e.printStackTrace();
				JOptionPane.showMessageDialog(MainFrame.this, "Error while matching. Please check matching settings.");
			}
			return score;
		}

		@Override
		protected void done() {
			if (score > 0) {
				nfViewLeft.prepareTree();
				nfViewRight.setTree(nfViewLeft.getTree());
			} else {
				JOptionPane.showMessageDialog(MainFrame.this, "Fingerprints do not match.");
			}

			publish(String.format("Score: %s", score));
		}

		@Override
		protected void process(List<String> chunks) {
			super.process(chunks);
			for (String s : chunks) {
				lblScore.setText(s);
			}
		}

	}

}
