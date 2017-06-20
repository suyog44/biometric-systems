package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFFPMinutiaNeighbor;
import com.neurotec.biometrics.standards.BDIFFPMinutiaType;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.biometrics.standards.FMRCore;
import com.neurotec.biometrics.standards.FMRDelta;
import com.neurotec.biometrics.standards.FMRFingerView;
import com.neurotec.biometrics.standards.FMRMinutia;
import com.neurotec.biometrics.standards.FMRecord;
import com.neurotec.biometrics.standards.swing.FMView;
import com.neurotec.biometrics.standards.swing.SelectedFMRCore;
import com.neurotec.biometrics.standards.swing.SelectedFMRDelta;
import com.neurotec.biometrics.standards.swing.SelectedFMRMinutia;
import com.neurotec.biometrics.swing.AddFeaturesTool;
import com.neurotec.biometrics.swing.FeatureAddEvent;
import com.neurotec.biometrics.swing.FeatureAddListener;
import com.neurotec.biometrics.swing.IndexSelectionListener;
import com.neurotec.biometrics.swing.PointerTool;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.samples.biometrics.standards.BDIFOptionsFrame.BDIFOptionsFormMode;
import com.neurotec.samples.swing.NPropertyGrid;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Utils;
import com.neurotec.samples.util.Utils.TemplateFileFilter;
import com.neurotec.swing.AboutBox;
import com.neurotec.util.NVersion;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Event;
import java.awt.SystemColor;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.WindowAdapter;
import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JScrollPane;
import javax.swing.JSeparator;
import javax.swing.JSplitPane;
import javax.swing.JTree;
import javax.swing.KeyStroke;
import javax.swing.ToolTipManager;
import javax.swing.UnsupportedLookAndFeelException;
import javax.swing.event.TreeSelectionEvent;
import javax.swing.event.TreeSelectionListener;
import javax.swing.filechooser.FileFilter;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeModel;
import javax.swing.tree.TreePath;

public final class MainFrame extends JFrame implements ActionListener, TreeSelectionListener, IndexSelectionListener, FeatureAddListener {

	// ==============================================
	// Private types
	// ==============================================

	private static final class FingerTreeObject {

		// ==============================================
		// Private fields
		// ==============================================

		private final Object taggedObject;
		private String nodeText;

		// ==============================================
		// Public constructor
		// ==============================================

		FingerTreeObject(Object taggedObject, String nodeText) {
			super();
			this.taggedObject = taggedObject;
			this.nodeText = nodeText;
		}

		// ==============================================
		// Public getter
		// ==============================================

		public Object getTaggedObject() {
			return taggedObject;
		}

		// ==============================================
		// Overridden methods
		// ==============================================

		@Override
		public String toString() {
			return nodeText;
		}
	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	private static final int ANSI_BDB_FORMAT = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.INCITS_TC_M1_BIOMETRICS, CBEFFBDBFormatIdentifiers.INCITS_TC_M1_BIOMETRICS_FINGER_MINUTIAE_X);
	private static final int ISO_BDB_FORMAT = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFFBDBFormatIdentifiers.FEDERAL_OFFICE_FOR_INFORMATION_SECURITY_TR_BIOMETRICS_XML_FINGER_1_0);

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JMenuItem menuItemNewFMRecord;
	private JMenuItem menuItemOpenFMRecord;
	private JMenuItem menuItemSaveFMRecord;
	private JMenuItem menuItemOpenCBEFFRecord;
	private JMenuItem menuItemExit;
	private JMenuItem menuItemAddFingerView;
	private JMenuItem menuItemRemoveFingerView;
	private JMenuItem menuItemDeleteSelectedMinutia;
	private JMenuItem menuItemPointerTool;
	private JMenuItem menuItemAddFeatureTool;
	private JMenuItem menuItemConvert;
	private JMenuItem menuItemAbout;
	private DefaultMutableTreeNode fingerTreeRoot;
	private JTree fingerTree;
	private NPropertyGrid displayGrid;
	private JButton btnPointerTool;
	private JButton btnAddFeatureTool;
	private JButton btnDeleteFeature;
	private FMView fmView;
	private final JFileChooser recordOpenFileDialog;
	private final JFileChooser recordSaveFileDialog;

	// ==============================================
	// Private Fields
	// ==============================================

	private String fileName;
	private boolean dialogResultOK;
	private FMRFingerView selectedFingerView;
	private BDIFStandard currentStandard = BDIFStandard.ANSI;
	private NVersion currentVersion;

	private int currentFlags;
	private int selectedFeature;
	private BDIFFPMinutiaType selectedType;

	private final List<String> listofFmRecordProp = Arrays.asList("captureEquipmentCompliance",
																  "captureEquipmentId",
																  "CBEFFProductId",
																  "certificationFlag",
																  "fingers",
																  "fingerViews",
																  "flags",
																  "resolutionX",
																  "resolutionY",
																  "sizeX",
																  "sizeY",
																  "standard",
																  "version");

	private final List<String> listofFingerViewProp = Arrays.asList("captureDateAndTime",
																	"captureDeviceTechnology",
																	"captureDeviceTypeId",
																	"captureDeviceVendorId",
																	"certificationBlocks",
																	"cores",
																	"deltas",
																	"position",
																	"fingerQuality",
																	"flags",
																	"hasEightNeighborRidgeCounts",
																	"horzImageResolution",
																	"hasFourNeighborRidgeCounts",
																	"impressionType",
																	"minutiae",
																	"minutiaeEightNeighbors",
																	"minutiaeFourNeighbors",
																	"minutiaeQualityFlag",
																	"owner",
																	"qualityBlocks",
																	"ridgeEndingType",
																	"sizeX",
																	"sizeY",
																	"standard",
																	"version",
																	"vertImageResolution",
																	"viewCount",
																	"viewNumber");

	private final List<String> listofFeatureProp = Arrays.asList("delta",
																 "core",
																 "minutia");

	private final List<String> listofCbeffRecordProp = Arrays.asList("bdbBuffer",
																	 "bdbCreationDate",
																	 "bdbFormat",
																	 "bdbIndex",
																	 "bdbValidityPeriod",
																	 "biometricSubType",
																	 "biometricType",
																	 "birCreationDate",
																	 "birIndex",
																	 "birValidityPeriod",
																	 "captureDevice",
																	 "cbeffVersion",
																	 "challengeResponse",
																	 "comparisonAlgorithm",
																	 "compressionAlgorithm",
																	 "creator",
																	 "encryption",
																	 "fascnBuffer",
																	 "featureExtractionAlgorithm",
																	 "flags",
																	 "integrity",
																	 "integrityOptions",
																	 "owner",
																	 "patronFormat",
																	 "patronHeaderVersion",
																	 "payLoad",
																	 "processedLevel",
																	 "product",
																	 "purpose",
																	 "quality",
																	 "qualityAlgorithm",
																	 "records",
																	 "sbBuffer",
																	 "sbFormat");

	// ==============================================
	// Public constructor
	// ==============================================

	public MainFrame() {
		super();
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		} catch (InstantiationException e) {
			e.printStackTrace();
		} catch (UnsupportedLookAndFeelException e) {
			e.printStackTrace();
		}
		initializeComponents();

		menuItemAbout.setText(AboutBox.getName());

		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosing(java.awt.event.WindowEvent windowEvent) {
				try {
					LicenseManager.getInstance().releaseAll();
				} finally {
					NCore.shutdown();
				}
			}

		});
		FileFilter filter = new TemplateFileFilter();
		recordOpenFileDialog = new JFileChooser();
		recordOpenFileDialog.addChoosableFileFilter(filter);
		recordSaveFileDialog = new JFileChooser();
		recordSaveFileDialog.addChoosableFileFilter(filter);
		fileName = null;

	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {

		// Initialize the MenuBar
		createMenuBar();

		// Construct the Layout
		JSplitPane mainSplitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT);
		JSplitPane leftSplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT);

		// Tree View
		fingerTreeRoot = new DefaultMutableTreeNode();
		fingerTree = new JTree(fingerTreeRoot);
		fingerTree.addTreeSelectionListener(this);
		fingerTree.setPreferredSize(new Dimension(250, 80));
		JScrollPane faceTreeScrollPane = new JScrollPane(fingerTree);

		// Property Grid
		displayGrid = new NPropertyGrid(true, new FNGPropertiesTable());
		displayGrid.setPreferredSize(new Dimension(250, 243));

		// / fmView
		fmView = new FMView();
		fmView.setBackground(SystemColor.control);
		fmView.setDrawFeatureArea(true);
		fmView.setFeatureAreaColor(Color.LIGHT_GRAY);

		// fmView.setLocation(new Point(100, 100));
		fmView.setMinutiaColor(Color.RED);
		fmView.setName("fmView");
		fmView.setNeighborMinutiaColor(Color.ORANGE);
		fmView.setSelectedMinutiaColor(Color.MAGENTA);
		fmView.setSingularPointColor(Color.RED);
		fmView.addIndexChangeListener(this);

		JScrollPane fmViewScrollPane = new JScrollPane(fmView, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		fmViewScrollPane.setPreferredSize(new Dimension(800, 800));

		mainSplitPane.setRightComponent(fmViewScrollPane);
		leftSplitPane.setDividerLocation(200);
		leftSplitPane.setLeftComponent(faceTreeScrollPane);

		leftSplitPane.setRightComponent(displayGrid);
		mainSplitPane.setLeftComponent(leftSplitPane);

		mainSplitPane.setDividerLocation(246);
		setSize(960, 580);

		// MainForm
		getContentPane().add(mainSplitPane, BorderLayout.CENTER);
		setIconImage(Utils.createIconImage("images/Logo16x16.png"));
		setTitle("FMRecord Editor");
		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		setLocation(dim.width / 2 - getSize().width / 2, dim.height / 2	- getSize().height / 2);
		pack();
	}

	private void createMenuBar() {
		JMenuBar menuBar = new JMenuBar();
		menuBar.setLocation(0, 0);
		menuBar.setSize(944, 24);

		// File Menu
		JMenu menuFile = new JMenu("File");
		menuFile.setSize(37, 20);

		// New FMRecord
		menuItemNewFMRecord = new JMenuItem("New FMRecord");
		menuItemNewFMRecord.addActionListener(this);
		menuItemNewFMRecord.setSize(215, 22);
		menuItemNewFMRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_N, Event.CTRL_MASK));
		menuItemNewFMRecord.setMnemonic(KeyEvent.VK_N);

		// Open FMRecord
		menuItemOpenFMRecord = new JMenuItem("Open FMRecord");
		menuItemOpenFMRecord.addActionListener(this);
		menuItemOpenFMRecord.setSize(215, 22);
		menuItemOpenFMRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, Event.CTRL_MASK));
		menuItemOpenFMRecord.setMnemonic(KeyEvent.VK_O);

		// Save FMRecord
		menuItemSaveFMRecord = new JMenuItem("Save FMRecord");
		menuItemSaveFMRecord.addActionListener(this);
		menuItemSaveFMRecord.setSize(215, 22);
		menuItemSaveFMRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Event.CTRL_MASK));
		menuItemSaveFMRecord.setMnemonic(KeyEvent.VK_S);

		// Separator
		JSeparator menuItemSeperator1 = new JSeparator();
		menuItemSeperator1.setSize(212, 6);

		// Open CBEFF record
		menuItemOpenCBEFFRecord = new JMenuItem("Open CBEFF record");
		menuItemOpenCBEFFRecord.addActionListener(this);
		menuItemOpenCBEFFRecord.setSize(215, 22);

		// Separator
		JSeparator menuItemSeperator2 = new JSeparator();
		menuItemSeperator1.setSize(212, 6);

		// Exit
		menuItemExit = new JMenuItem("Exit");
		menuItemExit.addActionListener(this);
		menuItemExit.setSize(215, 22);

		// Edit Menu
		JMenu menuEdit = new JMenu("Edit");
		menuEdit.setSize(39, 20);

		// Add Finger View
		menuItemAddFingerView = new JMenuItem("Add finger view");
		menuItemAddFingerView.addActionListener(this);
		menuItemAddFingerView.setSize(298, 22);
		menuItemAddFingerView.setMnemonic(KeyEvent.VK_D);

		// Remove Finger/FingerView
		menuItemRemoveFingerView = new JMenuItem("Remove fingerView");
		menuItemRemoveFingerView.addActionListener(this);
		menuItemRemoveFingerView.setSize(298, 22);
		menuItemRemoveFingerView.setMnemonic(KeyEvent.VK_R);

		// Delete Selected Minutia
		menuItemDeleteSelectedMinutia = new JMenuItem("Delete selected minutia/core/delta");
		menuItemDeleteSelectedMinutia.addActionListener(this);
		menuItemDeleteSelectedMinutia.setSize(298, 22);
		menuItemDeleteSelectedMinutia.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_D, Event.CTRL_MASK));
		menuItemDeleteSelectedMinutia.setEnabled(false);

		// Separator
		JSeparator seperator2 = new JSeparator();
		seperator2.setSize(295, 6);

		// ActiveTool
		JMenu menuItemActiveTool = new JMenu("Active tool");
		menuItemActiveTool.setMnemonic(KeyEvent.VK_T);
		menuItemActiveTool.setSize(298, 22);

		// Pointer Tool
		menuItemPointerTool = new JMenuItem("Pointer tool");
		menuItemPointerTool.addActionListener(this);
		menuItemPointerTool.setSize(200, 22);
		menuItemPointerTool.setMnemonic(KeyEvent.VK_P);
		menuItemPointerTool.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_P, Event.CTRL_MASK));
		// Add Feature Tool
		menuItemAddFeatureTool = new JMenuItem("Add feature tool");
		menuItemAddFeatureTool.addActionListener(this);
		menuItemAddFeatureTool.setSize(200, 22);
		menuItemAddFeatureTool.setMnemonic(KeyEvent.VK_F);
		menuItemAddFeatureTool.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_F, Event.CTRL_MASK));

		// Convert
		menuItemConvert = new JMenuItem("Convert...");
		menuItemConvert.addActionListener(this);
		menuItemConvert.setSize(298, 22);
		menuItemConvert.setMnemonic(KeyEvent.VK_C);

		// Help
		JMenu menuHelp = new JMenu("Help");
		menuHelp.setSize(44, 20);

		// About
		menuItemAbout = new JMenuItem("About");
		menuItemAbout.addActionListener(this);
		menuItemAbout.setSize(107, 22);
		menuItemAbout.setMnemonic(KeyEvent.VK_A);

		menuFile.add(menuItemNewFMRecord);
		menuFile.add(menuItemOpenFMRecord);
		menuFile.add(menuItemSaveFMRecord);
		menuFile.add(menuItemSeperator1);
		menuFile.add(menuItemOpenCBEFFRecord);
		menuFile.add(menuItemSeperator2);
		menuFile.add(menuItemExit);

		menuEdit.add(menuItemAddFingerView);
		menuEdit.add(menuItemRemoveFingerView);
		menuEdit.add(menuItemDeleteSelectedMinutia);
		menuEdit.add(seperator2);
		menuItemActiveTool.add(menuItemPointerTool);
		menuItemActiveTool.add(menuItemAddFeatureTool);
		menuEdit.add(menuItemActiveTool);
		menuEdit.add(menuItemConvert);

		menuHelp.add(menuItemAbout);

		menuBar.add(menuFile);
		menuBar.add(menuEdit);
		menuBar.add(menuHelp);

		menuBar.add(new JSeparator());

		btnPointerTool = new JButton(Utils.createIcon("images/Pointer.png"));
		btnPointerTool.setToolTipText("Pointer Tool - Use it to move or rotate details");
		btnPointerTool.addActionListener(this);
		btnPointerTool.setSize(23, 20);
		menuBar.add(btnPointerTool);

		btnAddFeatureTool = new JButton(Utils.createIcon("images/AddFeature.png"));
		btnAddFeatureTool.setToolTipText("Add Feature tool - Add new minutiae, cores or deltas");
		btnAddFeatureTool.addActionListener(this);
		btnAddFeatureTool.setSize(23, 20);
		menuBar.add(btnAddFeatureTool);

		btnDeleteFeature = new JButton(Utils.createIcon("images/Delete.png"));
		ToolTipManager.sharedInstance().registerComponent(btnDeleteFeature);
		btnDeleteFeature.setToolTipText("Delete selected - delete unwanted minutiae, cores or deltas");
		btnDeleteFeature.addActionListener(this);
		btnDeleteFeature.setSize(23, 20);
		menuBar.add(btnDeleteFeature);

		setJMenuBar(menuBar);
	}

	private void getOptions(BDIFOptionsFrame.BDIFOptionsFormMode mode, BDIFStandard standard, NVersion version) {
		FMRecordOptionsForm form = new FMRecordOptionsForm(MainFrame.this, "FMRecordOptionsFrame", currentFlags);
		form.setMode(mode);
		form.setStandard(standard);
		form.setVersion(version);
		showDialogOnScreen(form);
	}

	private void getFeature(AddFeaturesForm form) {
		showDialogOnScreen(form);
	}

	private static FMRecord convertToStandard(FMRecord record, BDIFStandard newStandard, NVersion newVersion, int flags) {
		if ((record.getStandard() == newStandard) && (record.getVersion() == newVersion) && (record.getFlags() == flags)) {
			return record;
		} else {
			return new FMRecord(record, flags, newStandard, newVersion);
		}
	}

	private void addFingerView(DefaultMutableTreeNode node, FMRFingerView item) {
		int index = node.getChildCount() + 1;
		DefaultMutableTreeNode fingerViewNode = new DefaultMutableTreeNode(new FingerTreeObject(item, "Finger view " + index));
		node.add(fingerViewNode);
		fingerTree.updateUI();
		expandFingerTree();
	}

	private void addFingers(DefaultMutableTreeNode node, FMRecord record) {
		for (FMRFingerView finger : record.getFingerViews()) {
			addFingerView(node, finger);
		}
	}

	private void getBiometricDataBlock(DefaultMutableTreeNode node, CBEFFRecord record) {
		if ((record.getBdbBuffer() != null) && ((record.getBdbFormat() == ANSI_BDB_FORMAT) || (record.getBdbFormat() == ISO_BDB_FORMAT))) {
			BDIFStandard standard = (record.getBdbFormat() == ANSI_BDB_FORMAT) ? BDIFStandard.ANSI : BDIFStandard.ISO;
			FMRecord fmRecord = new FMRecord(record.getBdbBuffer(), standard);
			DefaultMutableTreeNode child = new DefaultMutableTreeNode(new FingerTreeObject(fmRecord, "FMRecord"));
			addFingers(child, fmRecord);
			((DefaultTreeModel) fingerTree.getModel()).insertNodeInto(child, node, node.getChildCount());
		}
	}

	private void addFingers(DefaultMutableTreeNode node, CBEFFRecord record) {
		getBiometricDataBlock(node, record);
		for (CBEFFRecord child : record.getRecords()) {
			addFingers(node, child);
		}
	}

	private DefaultMutableTreeNode getFirstNodeWithTag(Class<?> tagType) {
		DefaultMutableTreeNode node = (DefaultMutableTreeNode) fingerTree.getLastSelectedPathComponent();
		if ((node == null) || (node.getParent() == null)) {
			return null;
		}
		while ((node.getParent() != fingerTree.getModel().getRoot()) && (((FingerTreeObject) node.getUserObject()).getTaggedObject().getClass() != tagType)) {
			node = (DefaultMutableTreeNode) node.getParent();
		}
		if (((FingerTreeObject) node.getUserObject()).getTaggedObject().getClass() == tagType) {
			return node;
		} else {
			return null;
		}
	}

	private void onDetailSelected() {
		if ((fmView.getSelectedCoreIndex() == fmView.getSelectedDeltaIndex()) && (fmView.getSelectedCoreIndex() == fmView.getSelectedMinutiaIndex())) {
			btnDeleteFeature.setEnabled(false);
			menuItemDeleteSelectedMinutia.setEnabled(false);
		} else {
			btnDeleteFeature.setEnabled(true);
			menuItemDeleteSelectedMinutia.setEnabled(true);
		}

		if (selectedFingerView != null) {
			FMRFingerView fingerView = selectedFingerView;
			displayGrid.setSupportedProperties(listofFeatureProp);
			int index = fmView.getSelectedDeltaIndex();
			if (index != -1) {
				displayGrid.setSource(new SelectedFMRDelta(fingerView, index));
				return;
			}

			index = fmView.getSelectedCoreIndex();
			if (index != -1) {
				displayGrid.setSource(new SelectedFMRCore(fingerView, index));
				return;
			}

			index = fmView.getSelectedMinutiaIndex();
			if (index != -1) {
				displayGrid
						.setSource(new SelectedFMRMinutia(fingerView, index));
				return;
			}

			displayGrid.setSupportedProperties(listofFingerViewProp);
			displayGrid.setSource(fingerView);
		}
	}

	private void onFeatureAdded(FeatureAddEvent eventArgs) {
		DefaultMutableTreeNode recordNode = getFirstNodeWithTag(FMRecord.class);
		if (recordNode == null) {
			return;
		}

		FMRecord record = (FMRecord) ((FingerTreeObject) recordNode.getUserObject()).getTaggedObject();

		FMRFingerView fView = selectedFingerView;
		if (fView == null || eventArgs.getStart().equals(eventArgs.getEnd())) {
			return;
		}
		if (eventArgs.getStart().getX() < 0 || eventArgs.getStart().getY() < 0) {
			return;
		}
		int x = (int) eventArgs.getStart().getX();
		int y = (int) eventArgs.getStart().getY();
		boolean isVersion2 = isRecordSecondVersion(record);
		int sizeX = isVersion2 ? record.getSizeX() : fmView.getSize().width;
		int sizeY = isVersion2 ? record.getSizeY() : fmView.getSize().height;
		if ((x >= sizeX) || (y >= sizeY) || (x < 0) || (y < 0)) {
			return;
		}

		AddFeaturesForm form = new AddFeaturesForm(MainFrame.this);
		dialogResultOK = false;
		getFeature(form);
		if (!dialogResultOK) {
			return;
		}

		int w = (int) (eventArgs.getEnd().getX() - eventArgs.getStart().getX());
		int h = (int) (eventArgs.getEnd().getY() - eventArgs.getStart().getY());

		// Angle should be inverted, since Y axis direction is from top to bottom.
		double angle = -1 * Math.atan((float) h / w);
		if (Double.isNaN(angle)) {
			return;
		}

		if (w < 0) {
			angle += Math.PI;
		}

		switch (selectedFeature) {
		case 0:
			FMRMinutia fm = new FMRMinutia((short) x, (short) y, selectedType, angle, record.getStandard());
			fView.getMinutiae().add(fm);
			break;
		case 1:
			FMRCore fc = new FMRCore((short) x, (short) y, angle, record.getStandard());
			fView.getCores().add(fc);
			break;
		case 2:
			fView.getDeltas().add(new FMRDelta((short) x, (short) y));
			break;
		default:
			throw new AssertionError("Invalid Input");
		}
	}

	private void onSelectedItemChanged() {
		DefaultMutableTreeNode selectedNode;
		FingerTreeObject userObject;
		if (fingerTree.getSelectionPath() == null) {
			selectedFingerView = null;
			userObject = null;
		} else {
			selectedNode = (DefaultMutableTreeNode) fingerTree.getSelectionPath().getLastPathComponent();
			userObject = (FingerTreeObject) selectedNode.getUserObject();

			if (userObject == null) {
				return;
			}

			if (userObject.getTaggedObject() instanceof FMRFingerView) {
				selectedFingerView = (FMRFingerView) userObject.getTaggedObject();
			} else {
				selectedFingerView = null;
			}
		}

		if (selectedFingerView != null) {
			fmView.setTemplate(selectedFingerView);
			btnAddFeatureTool.setEnabled(true);
			btnPointerTool.setEnabled(true);
			menuItemAddFeatureTool.setEnabled(true);
			menuItemPointerTool.setEnabled(true);
		} else {
			fmView.setTemplate(null);
			btnAddFeatureTool.setEnabled(false);
			btnPointerTool.setEnabled(false);
			btnDeleteFeature.setEnabled(false);
			menuItemAddFeatureTool.setEnabled(false);
			menuItemPointerTool.setEnabled(false);
		}

		if (selectedFingerView != null) {
			displayGrid.setSupportedProperties(listofFingerViewProp);
			displayGrid.setSource(selectedFingerView);
		} else if ((userObject != null) && (userObject.getTaggedObject() != null)) {
			if (userObject.getTaggedObject() instanceof FMRecord) {
				displayGrid.setSupportedProperties(listofFmRecordProp);
				displayGrid.setSource(userObject.getTaggedObject());
			} else if (userObject.getTaggedObject() instanceof CBEFFRecord) {
				displayGrid.setSupportedProperties(listofCbeffRecordProp);
				displayGrid.setSource(userObject.getTaggedObject());
			}
		} else {
			displayGrid.setSource(null);
		}
	}

	private void showError(String message) {
		JOptionPane.showMessageDialog(this, message, "Error", JOptionPane.ERROR_MESSAGE);
	}

	private void showWarning(String message) {
		JOptionPane.showMessageDialog(this, message, "Warning",	JOptionPane.WARNING_MESSAGE);
	}

	private void setTemplate(FMRecord template) {
		setTemplate(template, null);
	}

	private void setTemplate(FMRecord record, String recordFileName) {
		boolean newRecord;
		DefaultMutableTreeNode recordNode = getFirstNodeWithTag(FMRecord.class);
		if (recordNode == null) {
			newRecord = true;
		} else {
			FingerTreeObject userObject = (FingerTreeObject) recordNode.getUserObject();
			newRecord = ((userObject == null) || (((FMRecord) userObject.getTaggedObject()) != record));
		}

		if (newRecord) {
			this.fileName = recordFileName;
			fingerTreeRoot.removeAllChildren();
			FingerTreeObject userObject = new FingerTreeObject(record, (recordFileName == null) ? "Untitled" : recordFileName);
			DefaultMutableTreeNode templateNode = new DefaultMutableTreeNode(userObject);

			addFingers(templateNode, record);
			fingerTreeRoot.add(templateNode);
			if (fingerTree.isRootVisible()) {
				fingerTree.setRootVisible(false);
			}
		}
		fingerTree.updateUI();
		expandFingerTree();

		selectedFingerView = null;
		DefaultMutableTreeNode root = (DefaultMutableTreeNode) fingerTree.getModel().getRoot();
		fingerTree.setSelectionPath(new TreePath(((DefaultMutableTreeNode) root.getFirstChild()).getPath()));
	}

	private void setCBEFF(CBEFFRecord record, String recordFileName) {
		boolean newRecord;
		DefaultMutableTreeNode recordNode = getFirstNodeWithTag(CBEFFRecord.class);
		if (recordNode == null) {
			newRecord = true;
		} else {
			FingerTreeObject userObject = (FingerTreeObject) recordNode.getUserObject();
			newRecord = ((userObject == null) || (((CBEFFRecord) userObject.getTaggedObject()) != record));
		}

		if (newRecord) {
			this.fileName = recordFileName;
			fingerTreeRoot.removeAllChildren();
			FingerTreeObject userObject = new FingerTreeObject(record, (recordFileName == null) ? "Untitled" : recordFileName);
			DefaultMutableTreeNode templateNode = new DefaultMutableTreeNode(userObject);

			addFingers(templateNode, record);
			fingerTreeRoot.add(templateNode);
			expandFingerTree();
			if (fingerTree.isRootVisible()) {
				fingerTree.setRootVisible(false);
			}
			fingerTree.setSelectionRow(fingerTree.getRowCount() - 1);
		}
		fingerTree.updateUI();
		expandFingerTree();
	}

	private NBuffer loadTemplate(JFileChooser openFileDialog) throws IOException {
		openFileDialog.setSelectedFile(null);
		if (openFileDialog.showDialog(this, "Open") == JFileChooser.APPROVE_OPTION) {
			fileName = openFileDialog.getSelectedFile().getName();
			String filePath = openFileDialog.getSelectedFile().getAbsolutePath();
			return NFile.readAllBytes(filePath);
		} else {
			return new NBuffer(0);
		}
	}

	private void openTemplate() {
		getOptions(BDIFOptionsFrame.BDIFOptionsFormMode.OPEN, BDIFStandard.ANSI, FMRecord.VERSION_ISO_CURRENT);
		if (!dialogResultOK) {
			return;
		}
		BDIFStandard standard = getCurrentStandard();
		int flags = 0;
		FMRecord template;
		try {
			NBuffer templ = loadTemplate(recordOpenFileDialog);
			if (templ.size() == 0) {
				return;
			}
			template = new FMRecord(templ, flags, standard);
		} catch (Exception e) {
			e.printStackTrace();
			showError("Failed to load template! Reason:\r\n" + e);
			return;
		}
		setTemplate(template, fileName);
	}

	private void openCBEFFRecord() {
		CBEFFRecordOptionsForm form = new CBEFFRecordOptionsForm(MainFrame.this);
		if (form.showDialog()) {
			CBEFFRecord record;
			try {
				NBuffer templ = loadTemplate(recordOpenFileDialog);
				if (templ.size() == 0) {
					return;
				}
				record = new CBEFFRecord(templ, form.getPatronFormat());
				setCBEFF(record, fileName);
			} catch (Exception e) {
				e.printStackTrace();
				showError("Failed to load template! Reason:\r\n" + e);
			}
		}
	}

	private void saveTemplate(JFileChooser saveFileDialog, NBuffer template) throws IOException {
		if (saveFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			NFile.writeAllBytes(saveFileDialog.getSelectedFile().getAbsolutePath(), template);
		}
	}

	private void saveTemplate(FMRecord record) {
		NBuffer b = record.save();
		try {
			saveTemplate(recordSaveFileDialog, b);
		} catch (IOException e) {
			e.printStackTrace();
			showError("Failed to save template! Reason:\r\n" + e);
			return;
		}
		DefaultMutableTreeNode root = (DefaultMutableTreeNode) fingerTreeRoot.getChildAt(0);
		FingerTreeObject obj = (FingerTreeObject) root.getUserObject();

		File savedFile = recordSaveFileDialog.getSelectedFile();
		if (savedFile != null) {
			obj.nodeText = savedFile.getName();
		}
		fingerTree.updateUI();
	}

	private boolean isRecordSecondVersion(FMRecord record) {
		return (record.getStandard() == BDIFStandard.ANSI) && record.getVersion().equals(FMRecord.VERSION_ANSI_20)
			|| (record.getStandard() == BDIFStandard.ISO) && record.getVersion().equals(FMRecord.VERSION_ISO_20);
	}

	private void btnPointerToolClick() {
		menuItemPointerToolClick();
	}

	private void btnAddFeatureToolClick() {
		menuItemAddFeatureClick();
	}

	private void menuItemConvertClick() {
		DefaultMutableTreeNode root = (DefaultMutableTreeNode) fingerTree.getModel().getRoot();
		if ((root == null) || (root.getFirstChild() == null)) {
			showWarning("FMRecord is not opened. Open FMRecord first.");
			return;
		}
		if (getFirstNodeWithTag(FMRecord.class) == null) {
			fingerTree.setSelectionPath(new TreePath(((DefaultMutableTreeNode) root.getFirstChild()).getPath()));
		}
		DefaultMutableTreeNode rootNode = getFirstNodeWithTag(FMRecord.class);
		if (rootNode == null) {
			showWarning("FMRecord is not selected. Select FMRecord first.");
			return;
		}
		FMRecord record = (FMRecord) ((FingerTreeObject) rootNode.getUserObject()).getTaggedObject();
		currentStandard = (record.getStandard() == BDIFStandard.ANSI) ? BDIFStandard.ISO : BDIFStandard.ANSI;
		currentVersion = (currentStandard == BDIFStandard.ISO) ? FMRecord.VERSION_ISO_CURRENT : FMRecord.VERSION_ANSI_CURRENT;
		currentFlags = 0;

		getOptions(BDIFOptionsFormMode.CONVERT, currentStandard, currentVersion);
		if (!dialogResultOK) {
			return;
		}
		setTemplate(convertToStandard(record, currentStandard, currentVersion, currentFlags));
	}

	private void menuItemAboutClick() {
		AboutBox.show();
	}

	private void menuItemRemoveFingerViewClick() {
		if (fingerTree.getSelectionPath() != null) {
			DefaultMutableTreeNode selected = (DefaultMutableTreeNode) fingerTree.getSelectionPath().getLastPathComponent();
			if ((selected != null) && (selected.getParent() != null)) {
				if (selectedFingerView != null) {
					FMRecord record = (FMRecord) ((FingerTreeObject) getFirstNodeWithTag(FMRecord.class).getUserObject()).getTaggedObject();
					record.getFingerViews().remove(selectedFingerView);
					selectedFingerView = null;
					fmView.setTemplate(null);
					fingerTree.setSelectionPath(fingerTree.getSelectionPath().getParentPath());
					onSelectedItemChanged();
					selected.removeFromParent();
					fingerTree.updateUI();
					expandFingerTree();
					return;
				}
			}
		}
		showWarning("Finger view must be selected");
	}

	private void menuItemOpenFMRecordClick() {
		openTemplate();
		expandFingerTree();
	}

	private void menuItemOpenCBEFFRecordClick() {
		openCBEFFRecord();
		expandFingerTree();
	}

	private void menuItemSaveFMRecordClick() {
		DefaultMutableTreeNode node = getFirstNodeWithTag(FMRecord.class);
		if (node == null) {
			showWarning("Add FMRecord first");
		} else {
			saveTemplate((FMRecord) ((FingerTreeObject) node.getUserObject()).getTaggedObject());
		}
	}

	private void menuItemPointerToolClick() {
		if (fmView.getActiveTool() instanceof PointerTool) {
			btnPointerTool.setSelected(false);
			return;
		}
		if (fmView.getActiveTool() instanceof AddFeaturesTool) {
			((AddFeaturesTool<FMRMinutia, FMRCore, FMRDelta, BDIFFPMinutiaNeighbor>) fmView.getActiveTool()).removeFeatureAddListener(this);

		}
		fmView.setActiveTool(new PointerTool<FMRMinutia, FMRCore, FMRDelta, BDIFFPMinutiaNeighbor>());

	}

	private void menuItemAddFeatureClick() {
		if (fmView.getActiveTool() instanceof AddFeaturesTool) {
			menuItemAddFeatureTool.setSelected(false);
			return;
		}
		AddFeaturesTool<FMRMinutia, FMRCore, FMRDelta, BDIFFPMinutiaNeighbor> tool = new AddFeaturesTool<FMRMinutia, FMRCore, FMRDelta, BDIFFPMinutiaNeighbor>();
		tool.addFeatureAddListener(this);
		fmView.setActiveTool(tool);
		menuItemPointerTool.setSelected(false);
		menuItemAddFeatureTool.setSelected(true);
	}

	private void menuItemNewFMRecordClick() {
		getOptions(BDIFOptionsFormMode.NEW, BDIFStandard.ISO, FMRecord.VERSION_ISO_CURRENT);
		if (!dialogResultOK) {
			return;
		}

		fileName = null;
		FMRecord template = new FMRecord(currentStandard, currentVersion, currentFlags);
		setTemplate(template);
	}

	private void menuItemAddFingerViewClick() {
		DefaultMutableTreeNode recordNode = getFirstNodeWithTag(FMRecord.class);

		if (recordNode == null) {
			showWarning("FMRecord has to be selected before adding finger view.");
			return;
		}

		FMRecord record = (FMRecord) ((FingerTreeObject) recordNode.getUserObject()).getTaggedObject();
		FMRFingerView fingerView = new FMRFingerView(record.getStandard(), record.getVersion());

		if (isRecordSecondVersion(record)) {
			if (record.getFingerViews().isEmpty()) {
				AddFingerViewForm form = new AddFingerViewForm(this, "New finger view");
				form.setModal(true);
				showDialogOnScreen(form);

				if (dialogResultOK) {
					record.setSizeX(form.getHorzSize());
					record.setSizeY(form.getVertSize());
					record.setResolutionX(form.getHorzResolution());
					record.setResolutionY(form.getVertResolution());
				} else {
					return;
				}
			}
		} else {
			AddFingerViewForm form = new AddFingerViewForm(this, "New finger view");
			form.setModal(true);
			showDialogOnScreen(form);
			if (dialogResultOK) {
				fingerView.setSizeX(form.getHorzSize());
				fingerView.setSizeY(form.getVertSize());
				fingerView.setHorzImageResolution(form.getHorzResolution());
				fingerView.setVertImageResolution(form.getVertResolution());
			} else {
				return;
			}
		}

		record.getFingerViews().add(fingerView);
		addFingerView(recordNode, fingerView);
	}

	private void btnDeleteFeatureClick() {
		menuItemDeleteSelectedMinutiaClick();
	}

	private void menuItemDeleteSelectedMinutiaClick() {
		if (selectedFingerView != null) {
			FMRFingerView fingerView = selectedFingerView;

			int index = fmView.getSelectedMinutiaIndex();
			if (index != -1) {
				fmView.setSelectedMinutiaIndex(-1);
				fingerView.getMinutiae().remove(index);
				return;
			}

			index = fmView.getSelectedCoreIndex();
			if (index != -1) {
				fmView.setSelectedCoreIndex(-1);
				fingerView.getCores().remove(index);
				return;
			}

			index = fmView.getSelectedDeltaIndex();
			if (index != -1) {
				fmView.setSelectedDeltaIndex(-1);
				fingerView.getDeltas().remove(index);
			}
		}
	}

	private void expandFingerTree() {
		for (int i = fingerTree.getRowCount() - 1; i >= 0; i--) {
			fingerTree.expandRow(i);
		}
	}

	private void showDialogOnScreen(JDialog dialog) {
		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		dialog.setLocation(screenSize.width / 2 - dialog.getPreferredSize().width / 2,
						screenSize.height / 2 - dialog.getPreferredSize().height / 2);
		dialog.setVisible(true);
	}

	// ==============================================
	// Public Methods
	// ==============================================

	@Override
	public void actionPerformed(ActionEvent ev) {
		Object sender = ev.getSource();
		try {
			if (sender == menuItemAbout) {
				menuItemAboutClick();
			} else if (sender == menuItemExit) {
				LicenseManager.getInstance().releaseAll();
				NCore.shutdown();
				dispose();
			} else if (sender == menuItemNewFMRecord) {
				menuItemNewFMRecordClick();
			} else if (sender == menuItemAddFeatureTool) {
				menuItemAddFeatureClick();
			} else if (sender == menuItemSaveFMRecord) {
				menuItemSaveFMRecordClick();
			} else if (sender == menuItemOpenFMRecord) {
				menuItemOpenFMRecordClick();
			} else if (sender == menuItemOpenCBEFFRecord) {
				menuItemOpenCBEFFRecordClick();
			} else if (sender == menuItemAddFingerView) {
				menuItemAddFingerViewClick();
			} else if (sender == menuItemRemoveFingerView) {
				menuItemRemoveFingerViewClick();
			} else if (sender == menuItemPointerTool) {
				menuItemPointerToolClick();
			} else if (sender == menuItemAddFeatureTool) {
				menuItemAddFeatureClick();
			} else if (sender == menuItemDeleteSelectedMinutia) {
				menuItemDeleteSelectedMinutiaClick();
			} else if (sender == menuItemConvert) {
				menuItemConvertClick();
			} else if (sender == btnPointerTool) {
				btnPointerToolClick();
			} else if (sender == btnAddFeatureTool) {
				btnAddFeatureToolClick();
			} else if (sender == btnDeleteFeature) {
				btnDeleteFeatureClick();
			} else if (sender == fmView) {
				onDetailSelected();
			}
		} catch (Exception e) {
			e.printStackTrace();
			showError(e.toString());
		}
	}

	@Override
	public void valueChanged(TreeSelectionEvent arg0) {
		onSelectedItemChanged();
	}

	public void updateFeatureFormDetails(int feature, BDIFFPMinutiaType type) {
		selectedFeature = feature;
		selectedType = type;
	}

	@Override
	public void selectedIndexChanged() {
		onDetailSelected();
	}

	@Override
	public void featureAddCompleted(FeatureAddEvent e) {
		onFeatureAdded(e);
	}

	public void setDialogResultOK(boolean dialogResultOK) {
		this.dialogResultOK = dialogResultOK;
	}

	public void setCurrentStandard(BDIFStandard currentStandard) {
		this.currentStandard = currentStandard;
	}

	public BDIFStandard getCurrentStandard() {
		return currentStandard;
	}

	public void setCurrentVersion(NVersion version) {
		this.currentVersion = version;
	}

	public void setCurrentFlags(int currentFlags) {
		this.currentFlags = currentFlags;
	}

}
