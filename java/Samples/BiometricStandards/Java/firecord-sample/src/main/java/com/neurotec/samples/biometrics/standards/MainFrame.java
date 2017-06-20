package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.biometrics.standards.FIRFingerView;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.biometrics.standards.swing.FIView;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.samples.biometrics.standards.FIRecordOptionsFrame.FIRecordOptions;
import com.neurotec.samples.swing.NPropertyGrid;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.AboutBox;
import com.neurotec.util.NVersion;

import java.awt.Event;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JScrollPane;
import javax.swing.JSplitPane;
import javax.swing.JTree;
import javax.swing.KeyStroke;
import javax.swing.event.TreeSelectionEvent;
import javax.swing.event.TreeSelectionListener;
import javax.swing.filechooser.FileFilter;
import javax.swing.tree.DefaultMutableTreeNode;
import javax.swing.tree.DefaultTreeCellRenderer;

public final class MainFrame extends JFrame implements ActionListener, TreeSelectionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	private static final int ANSI_BDB_FORMAT = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.INCITS_TC_M1_BIOMETRICS, CBEFFBDBFormatIdentifiers.INCITS_TC_M1_BIOMETRICS_FINGER_IMAGE);
	private static final int ISO_BDB_FORMAT =  BDIFTypes.makeFormat(CBEFFBiometricOrganizations.ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFFBDBFormatIdentifiers.ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_IMAGE);

	static {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// ==============================================
	// Private fields
	// ==============================================

	private JMenuItem menuItemNewFromImage;
	private JMenuItem menuItemOpenFIRecord;
	private JMenuItem menuItemOpenCBEFFRecord;
	private JMenuItem menuItemSaveFIRecord;
	private JMenuItem menuItemExit;
	private JMenuItem menuItemAddFingerViewFromImage;
	private JMenuItem menuItemRemoveFinger;
	private JMenuItem menuItemSaveAsImage;
	private JMenuItem menuItemConvert;
	private JMenuItem menuItemAbout;

	private DefaultMutableTreeNode treeRoot;
	private JTree treeView;
	private NPropertyGrid propertyGrid;
	private FIView fiView;

	private JFileChooser fiRecordOpenFileDialog;
	private JFileChooser cbeffRecordOpenFileDialog;
	private JFileChooser fiRecordSaveFileDialog;
	private JFileChooser imageOpenFileDialog;
	private JFileChooser imageSaveFileDialog;

	private FIRecord record;
	private File file;

	// ==============================================
	// Public constructor
	// ==============================================

	public MainFrame() {
		super();
		initGUI();
		initFileChoosers();
		onSelectedItemChanged();
	}

	// ==============================================
	// Private static methods
	// ==============================================

	private static void saveTemplate(JFileChooser saveFileDialog, File file, NBuffer template) throws IOException {
		if (file != null) {
			saveFileDialog.setSelectedFile(file);
		}
		if (saveFileDialog.showSaveDialog(null) == JFileChooser.APPROVE_OPTION) {
			String savePath = saveFileDialog.getSelectedFile().getPath();
			if (saveFileDialog.getFileFilter().getDescription().equals("Data files")) {
				if (savePath.lastIndexOf('.') == -1) {
					savePath += ".dat";
				}
			}
			NFile.writeAllBytes(savePath, template);
		}
	}

	private static FIRecord convertToStandard(FIRecord record, BDIFStandard newStandard, int flags, NVersion version) {
		if (record.getStandard() == newStandard && record.getFlags() == flags && record.getVersion().equals(version)) {
			return record;
		}
		return new FIRecord(record, flags, newStandard, version);
	}
	// ==============================================
	// Private methods
	// ==============================================

	private void initGUI() {
		addWindowListener(new WindowAdapter() {
			@Override
			public void windowClosed(WindowEvent e) {
				try {
					if (record != null) {
						record.dispose();
					}
					LicenseManager.getInstance().releaseAll();
				} finally {
					NCore.shutdown();
				}
			}
		});
		setIconImage(Utils.createIconImage("images/Logo16x16.png"));

		createMenuBar();

		JSplitPane mainSplitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT);
		JSplitPane leftSplitPane = new JSplitPane(JSplitPane.VERTICAL_SPLIT);

		treeRoot = new DefaultMutableTreeNode();
		treeView = new JTree(treeRoot);
		treeView.addTreeSelectionListener(this);
		DefaultTreeCellRenderer renderer = new DefaultTreeCellRenderer();
		renderer.setLeafIcon(null);
		renderer.setClosedIcon(null);
		renderer.setOpenIcon(null);
		treeView.setCellRenderer(renderer);
		treeView.setShowsRootHandles(true);

		JScrollPane fingerTreeScrollPane = new JScrollPane(treeView);

		propertyGrid = new NPropertyGrid(true, new FIPropertiesTable());

		fiView = new FIView();
		JScrollPane imageScrollPane = new JScrollPane(fiView);

		leftSplitPane.setLeftComponent(fingerTreeScrollPane);
		leftSplitPane.setRightComponent(propertyGrid);
		leftSplitPane.setDividerLocation(185);

		mainSplitPane.setLeftComponent(leftSplitPane);
		mainSplitPane.setRightComponent(imageScrollPane);
		mainSplitPane.setDividerLocation(250);

		this.getContentPane().add(mainSplitPane);
		this.pack();
	}

	private void initFileChoosers() {
		fiRecordOpenFileDialog = new JFileChooser();
		cbeffRecordOpenFileDialog = new JFileChooser();
		fiRecordSaveFileDialog = new JFileChooser();
		imageOpenFileDialog = new JFileChooser();
		imageSaveFileDialog = new JFileChooser();

		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getBMP().getFileFilter(), "BMP files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getPNG().getFileFilter(), "PNG files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getTIFF().getFileFilter(), "TIFF files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getIHead().getFileFilter(), "NIST IHead files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getWSQ().getFileFilter(), "WSQ files"));
		StringBuilder allImageFilters = new StringBuilder(64);
		for (NImageFormat format : NImageFormat.getFormats()) {
			allImageFilters.append(format.getFileFilter()).append(';');
		}
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(allImageFilters.toString(), "All image files"));

		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getBMP().getFileFilter(), "BMP files"));
		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getTIFF().getFileFilter(), "TIFF files"));
		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getIHead().getFileFilter(), "NIST IHead files"));
		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getWSQ().getFileFilter(), "WSQ files"));
		imageSaveFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getPNG().getFileFilter(), "PNG files"));

		fiRecordOpenFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("FCRecord files"));
		cbeffRecordOpenFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("CBEFFRecord files"));

		fiRecordSaveFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("FCRecord files"));
	}

	private void createMenuBar() {
		JMenuBar menuBar = new JMenuBar();

		JMenu menuFile = new JMenu("File");

		menuItemNewFromImage = new JMenuItem("New...");
		menuItemNewFromImage.addActionListener(this);
		menuItemNewFromImage.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_N, Event.CTRL_MASK));
		menuItemNewFromImage.setMnemonic(KeyEvent.VK_N);

		menuItemOpenFIRecord = new JMenuItem("Open FIRecord...");
		menuItemOpenFIRecord.addActionListener(this);
		menuItemOpenFIRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, Event.CTRL_MASK));
		menuItemOpenFIRecord.setMnemonic(KeyEvent.VK_O);

		menuItemOpenCBEFFRecord = new JMenuItem("Open CBEFFRecord...");
		menuItemOpenCBEFFRecord.addActionListener(this);
		menuItemOpenCBEFFRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_C, Event.CTRL_MASK));
		menuItemOpenCBEFFRecord.setMnemonic(KeyEvent.VK_C);

		menuItemSaveFIRecord = new JMenuItem("Save FIRecord...");
		menuItemSaveFIRecord.addActionListener(this);
		menuItemSaveFIRecord.setEnabled(false);
		menuItemSaveFIRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Event.CTRL_MASK));
		menuItemSaveFIRecord.setMnemonic(KeyEvent.VK_S);

		menuItemExit = new JMenuItem("Exit");
		menuItemExit.addActionListener(this);

		menuFile.add(menuItemNewFromImage);
		menuFile.add(menuItemOpenFIRecord);
		menuFile.addSeparator();
		menuFile.add(menuItemOpenCBEFFRecord);
		menuFile.addSeparator();
		menuFile.add(menuItemSaveFIRecord);
		menuFile.addSeparator();
		menuFile.add(menuItemExit);

		JMenu menuEdit = new JMenu("Edit");

		menuItemAddFingerViewFromImage = new JMenuItem("Add finger view...");
		menuItemAddFingerViewFromImage.addActionListener(this);
		menuItemAddFingerViewFromImage.setEnabled(false);

		menuItemRemoveFinger = new JMenuItem("Remove fingerView");
		menuItemRemoveFinger.addActionListener(this);
		menuItemRemoveFinger.setEnabled(false);

		menuItemSaveAsImage = new JMenuItem("Save finger view as image...");
		menuItemSaveAsImage.addActionListener(this);
		menuItemSaveAsImage.setEnabled(false);

		menuItemConvert = new JMenuItem("Convert ...");
		menuItemConvert.addActionListener(this);
		menuItemConvert.setEnabled(false);

		menuEdit.add(menuItemAddFingerViewFromImage);
		menuEdit.add(menuItemRemoveFinger);
		menuEdit.addSeparator();
		menuEdit.add(menuItemSaveAsImage);
		menuEdit.addSeparator();
		menuEdit.add(menuItemConvert);

		JMenu menuHelp = new JMenu("Help");
		menuItemAbout = new JMenuItem(AboutBox.getName());
		menuItemAbout.addActionListener(this);
		menuHelp.add(menuItemAbout);

		menuBar.add(menuFile);
		menuBar.add(menuEdit);
		menuBar.add(menuHelp);

		this.setJMenuBar(menuBar);
	}

	private void onSelectedItemChanged() {
		DefaultMutableTreeNode selectedNode = null;
		if (treeView.getSelectionPath() != null) {
			selectedNode = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		}
		if (selectedNode == null) {
			return;
		}
		Object taggedObject = selectedNode.getUserObject();
		FIRFingerView fingerView = null;
		if (taggedObject instanceof FIRFingerView) {
			fingerView = (FIRFingerView) taggedObject;
		}

		if (fingerView != null) {
			fiView.setRecord(fingerView);
			propertyGrid.setSource(fingerView);
		} else {
			fiView.setRecord(null);
			propertyGrid.setSource(taggedObject);
		}

		if (taggedObject instanceof FIRecord) {
			this.record = (FIRecord) taggedObject;
		} else {
			taggedObject = selectedNode.getParent();
			if (taggedObject instanceof FIRecord) {
				this.record = (FIRecord) taggedObject;
			}
		}

		menuItemAddFingerViewFromImage.setEnabled(true);
		menuItemRemoveFinger.setEnabled(true);
		menuItemSaveFIRecord.setEnabled(true);
		menuItemConvert.setEnabled(true);
		menuItemSaveAsImage.setEnabled(true);

	}

	private void showError(String message) {
		JOptionPane.showMessageDialog(this, message, this.getTitle(), JOptionPane.ERROR_MESSAGE);
	}

	private void showWarning(String message) {
		JOptionPane.showMessageDialog(this, message, this.getTitle(), JOptionPane.WARNING_MESSAGE);
	}

	private void setTemplate(FIRecord record) {
		boolean newRecord = this.record != record;
		this.record = record;
		String fileName = null;
		if (file != null) {
			fileName = file.getName();
		}
		treeRoot.removeAllChildren();
		if (newRecord) {
			treeView.setRootVisible(true);
			DefaultMutableTreeNode templateNode = new FIRecordTreeNode(record, fileName == null ? "Untitled" : fileName);
			addFingers(templateNode, record);
			treeRoot.add(templateNode);
			treeView.updateUI();
			expandFingerTree();
			treeView.setRootVisible(false);
			treeView.setSelectionRow(0);
		}
		treeView.updateUI();
	}

	private void openTemplate() {
		byte[] data = loadTemplate(fiRecordOpenFileDialog);
		if (data == null) {
			return;
		}
		FIRecordOptions options = getOptions(BDIFOptionsFormMode.OPEN, BDIFStandard.ISO, FIRecord.VERSION_ISO_CURRENT, 0);
		if (options != null) {
			try {
				FIRecord template = new FIRecord(new NBuffer(data), options.getFlags(), options.getStandard());
				setTemplate(template);
			} catch (Exception e) {
				e.printStackTrace();
				showError("Failed to load template! Reason:\r\n" + e);
				return;
			}
		}
	}

	private void openCBEFFRecord() {
		byte[] data = loadTemplate(cbeffRecordOpenFileDialog);
		if (data == null) {
			return;
		}
		CBEFFRecordOptionsForm form = new CBEFFRecordOptionsForm(this);

		if (form.showDialog()) {
			try {
				CBEFFRecord record = new CBEFFRecord(new NBuffer(data), form.getPatronFormat());
				setCBEFFRecord(record, file);
			} catch (Exception e) {
				e.printStackTrace();
				showError("Failed to open CBEFFRecord! Reason:\r\n" + e);
				return;
			}
		}
	}

	private void setCBEFFRecord(CBEFFRecord cbeffRecord, File cbeffFile) {
		String fileName = null;
		if (cbeffFile != null) {
			fileName = cbeffFile.getName();
		}
		treeRoot.removeAllChildren();
		treeView.setRootVisible(true);
		FIRecordTreeNode userObject = new FIRecordTreeNode(cbeffRecord, (fileName == null ? "Untitled" : fileName ));
		addFingers(userObject, cbeffRecord);
		treeRoot.add(userObject);
		expandFingerTree();
		treeView.setRootVisible(false);
		treeView.setSelectionRow(0);
	}

	private FIRecordOptions getOptions(BDIFOptionsFormMode mode, BDIFStandard standard, NVersion version, int flags) {
		FIRecordOptionsFrame form = new FIRecordOptionsFrame(this);
		form.setMode(mode);
		form.setStandard(standard);
		form.setVersion(version);
		form.setFlags(flags);
		FIRecordOptions options = null;
		if (form.showDialog()) {
			options = form.getFiRecordOptions();
		}
		return options;
	}

	private FIRecordTreeNode getRecordNode() {
		return treeRoot.getChildCount() > 0 ? (FIRecordTreeNode)treeRoot.getFirstChild() : null;
	}
	private void saveTemplate(FIRecord record) {
		try {
			saveTemplate(fiRecordSaveFileDialog, file, record.save());
			((FIRecordTreeNode) treeRoot.getChildAt(0)).setName(fiRecordSaveFileDialog.getSelectedFile().getName());
		} catch (IOException e) {
			e.printStackTrace();
			showError(e.toString());
		}
	}

	private void convert() {
		if (record == null) {
			return;
		}
		BDIFStandard standard = (record.getStandard() == BDIFStandard.ANSI) ? BDIFStandard.ISO : BDIFStandard.ANSI;
		NVersion version = standard == BDIFStandard.ISO ? FIRecord.VERSION_ISO_CURRENT : FIRecord.VERSION_ANSI_CURRENT;
		FIRecordOptions options;
		try {
			options = getOptions(BDIFOptionsFormMode.CONVERT, standard, version, 0);
			if (options != null) {
				setTemplate(convertToStandard(record, options.getStandard(), options.getFlags(), options.getVersion()));
				expandFingerTree();
			}
		} catch (Exception e) {
			e.printStackTrace();
			showError(e.toString());
		}
	}

	private void saveFinger() {
		if (treeView.getSelectionPath() == null) {
			showWarning("Please select finger image");
			return;
		}
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		Object taggedObject = selected.getUserObject();
		if (taggedObject instanceof FIRFingerView) {
			imageSaveFileDialog.setSelectedFile(null);
			if (imageSaveFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
				try {
					FIRFingerView finger = (FIRFingerView) taggedObject;
					NImage image = finger.toNImage();
					FileFilter selectedFilter = imageSaveFileDialog.getFileFilter();
					if (selectedFilter instanceof Utils.ImageFileFilter) {
						image.save(imageSaveFileDialog.getSelectedFile().getPath() + '.' + ((Utils.ImageFileFilter) imageSaveFileDialog.getFileFilter()).getExtensions().get(0));
					} else {
						image.save(imageSaveFileDialog.getSelectedFile().getPath());
					}
				} catch (Exception e) {
					e.printStackTrace();
					showError("Failed to save image to file!\r\nReason: " + e);
				}
			}
		} else {
			showWarning("Please select finger image");
		}
	}

	private void removeFinger() {
		if (treeView.getSelectionPath() == null) {
			showWarning("FingerView must be selected");
			return;
		}
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if ((selected != null) && (selected.getParent() != treeRoot)) {
			Object taggedObject = selected.getUserObject();
			FIRFingerView fingerView = null;
			if (taggedObject instanceof FIRFingerView) {
				fingerView = (FIRFingerView) taggedObject;
			}
			try {
				if (fingerView != null) {
					record.getFingerViews().remove(fingerView);
					((DefaultMutableTreeNode) selected.getParent()).remove(selected);
				}
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
			treeView.updateUI();
			treeView.setSelectionRow(0);
		} else {
			showWarning("FingerView must be selected");
		}
	}

	private void newFromImage() {
		FIRecordOptions options = getOptions(BDIFOptionsFormMode.NEW, BDIFStandard.ISO, FIRecord.VERSION_ISO_CURRENT, 0);
		if (options != null) {
			try {
				FIRecord template = new FIRecord(options.getStandard(), options.getVersion(), options.getFlags());
				file = null;
				setTemplate(template);
			} catch (Exception e) {
				showError("Failed to create template! Reason:\r\n" + e);
			}
		}
	}

	private void saveFIRecord() {
		if (record == null) {
			showError("Add FIRecord first");
			return;
		}
		try {
			saveTemplate(record);
		} catch (Exception e) {
			e.printStackTrace();
			showError(e.toString());
		}
	}

	private void addFingerViewFromImage() {
//		if (treeView.getSelectionPath() == null) {
//			showWarning("Finger must be selected before adding fingerView");
//			return;
//		}
		FIRecordTreeNode recordNode = getRecordNode();
		if (recordNode != null) {
			try {
				imageOpenFileDialog.setSelectedFile(null);
				NImage imgFromFile;
				if (imageOpenFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
					imgFromFile = NImage.fromFile(imageOpenFileDialog.getSelectedFile().getPath());
				} else {
					return;
				}

				NImage image = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, imgFromFile);
				if (image.isResolutionIsAspectRatio() || image.getHorzResolution() < 250 || image.getVertResolution() < 250) {
					image.setHorzResolution(500);
					image.setVertResolution(500);
					image.setResolutionIsAspectRatio(false);
				}

				Object taggedObject = recordNode.getUserObject();
				FIRecord record = null;
				if (taggedObject instanceof FIRecord) {
					record = (FIRecord) taggedObject;

					AddFingerFrame frame = new AddFingerFrame(this);
					if (!frame.showDialog()) {
						return;
					}

					FIRFingerView fingerView = new FIRFingerView(record.getStandard(), record.getVersion());

					fingerView.setPosition(frame.getFingerPosition());

					if (!isRecordFirstVersion(record)) {
						fingerView.setPixelDepth((byte) 8);
						fingerView.setHorzImageResolution((int) image.getHorzResolution());
						fingerView.setHorzScanResolution((int) image.getHorzResolution());
						fingerView.setVertImageResolution((int) image.getVertResolution());
						fingerView.setVertScanResolution((int) image.getVertResolution());
					} else {
						if (record.getFingerViews().size() == 0) {
							record.setPixelDepth((byte) 8);
							record.setHorzImageResolution((int) image.getHorzResolution());
							record.setHorzScanResolution((int) image.getHorzResolution());
							record.setVertImageResolution((int) image.getVertResolution());
							record.setVertScanResolution((int) image.getVertResolution());
						}
					}

					record.getFingerViews().add(fingerView);
					fingerView.setImage(image);
					addFingerView(recordNode, fingerView);
				}
				treeView.updateUI();
				expandFingerTree();
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		} else {
			showWarning("Finger must be selected before adding fingerView");
		}
	}

	private boolean isRecordFirstVersion(FIRecord fiRecord) {
		return fiRecord.getStandard() == BDIFStandard.ANSI && fiRecord.getVersion().compareTo(FIRecord.VERSION_ANSI_10) == 0
				|| fiRecord.getStandard() == BDIFStandard.ISO && fiRecord.getVersion().compareTo(FIRecord.VERSION_ISO_10) == 0;
	}

	private byte[] loadTemplate(JFileChooser openFileDialog) {
		openFileDialog.setSelectedFile(null);
		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			file = openFileDialog.getSelectedFile();
			byte[] data = new byte[(int) file.length()];
			try {
				DataInputStream dis = new DataInputStream(new FileInputStream(file));
				dis.readFully(data);
				dis.close();
				return data;
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
		file = null;
		return null;
	}

	private static DefaultMutableTreeNode addFingerView(DefaultMutableTreeNode fingerNode, FIRFingerView fingerView) {
		int index = fingerNode.getChildCount() + 1;
		DefaultMutableTreeNode fingerViewNode = new FIRecordTreeNode(fingerView, "FingerView " + index);
		fingerNode.add(fingerViewNode);
		return fingerViewNode;
	}

	private static DefaultMutableTreeNode addFingers(DefaultMutableTreeNode templateNode, FIRecord record) {
		if (record.getFingerViews() != null) {
			for (FIRFingerView finger : record.getFingerViews()) {
				addFingerView(templateNode, finger);
			}
		}
		return templateNode;
	}

	private static DefaultMutableTreeNode addFingers(DefaultMutableTreeNode templateNode, CBEFFRecord record) {
		getBiometricDataBlock(templateNode, record);
		for (CBEFFRecord child : record.getRecords()) {
			addFingers(templateNode, child);
		}
		return templateNode;
	}

	private static void getBiometricDataBlock(DefaultMutableTreeNode templateNode, CBEFFRecord cbeffRecord) {
		if (cbeffRecord.getBdbBuffer() != null && (cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT || cbeffRecord.getBdbFormat() == ISO_BDB_FORMAT)) {
			BDIFStandard standard = cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT ? BDIFStandard.ANSI : BDIFStandard.ISO;
			FIRecord fiRecord = new FIRecord(cbeffRecord.getBdbBuffer(), standard);
			DefaultMutableTreeNode recordNode = new FIRecordTreeNode(fiRecord, "FIRecord");
			addFingers(recordNode, fiRecord);
			templateNode.add(recordNode);
		}
	}

	private void expandFingerTree() {
		treeView.expandRow(0);
		for (int i = treeView.getRowCount() - 1; i > 0; i--) {
			treeView.expandRow(i);
		}
		treeView.updateUI();
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == menuItemConvert) {
			convert();
		} else if (source == menuItemAbout) {
			AboutBox.show();
		} else if (source == menuItemSaveFIRecord) {
			saveFIRecord();
		} else if (source == menuItemRemoveFinger) {
			removeFinger();
		} else if (source == menuItemNewFromImage) {
			newFromImage();
		} else if (source == menuItemOpenFIRecord) {
			openTemplate();
		} else if (source == menuItemOpenCBEFFRecord) {
			openCBEFFRecord();
		} else if (source == menuItemSaveAsImage) {
			saveFinger();
		} else if (source == menuItemExit) {
			dispose();
		} else if (source == menuItemAddFingerViewFromImage) {
			addFingerViewFromImage();
		}
	}

	@Override
	public void valueChanged(TreeSelectionEvent e) {
		onSelectedItemChanged();
	}

	// ==================================================================
	// Inner class
	// ==================================================================

	private static final class FIRecordTreeNode extends DefaultMutableTreeNode {

		private static final long serialVersionUID = 1L;

		private String name;

		public FIRecordTreeNode(Object obj, String name) {
			setUserObject(obj);
			this.name = name;
		}

		public void setName(String name) {
			this.name = name;
		}

		@Override
		public String toString() {
			return name;
		}
	}
}

