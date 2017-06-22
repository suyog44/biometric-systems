package com.neurotec.samples.biometrics.standards;

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

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.biometrics.standards.IIRIrisImage;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.biometrics.standards.swing.IIView;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.samples.biometrics.standards.IIRecordOptionsFrame.IIRecordOptions;
import com.neurotec.samples.swing.NPropertyGrid;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.AboutBox;
import com.neurotec.util.NVersion;

public final class MainFrame extends JFrame implements ActionListener, TreeSelectionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final int ANSI_BDB_FORMAT_POLAR = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.INCITS_TC_M1_BIOMETRICS, CBEFFBDBFormatIdentifiers.INCITS_TC_M1_BIOMETRICS_IRIS_POLAR);
	private static final int ISO_BDB_FORMAT_POLAR = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFFBDBFormatIdentifiers.ISO_IEC_JTC_1_SC_37_BIOMETRICS_IRIS_IMAGE_POLAR);
	private static final int ANSI_BDB_FORMAT_RECTILINEAR = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.INCITS_TC_M1_BIOMETRICS, CBEFFBDBFormatIdentifiers.INCITS_TC_M1_BIOMETRICS_IRIS_RECTILINEAR);
	private static final int ISO_BDB_FORMAT_RECTILINEAR =  BDIFTypes.makeFormat(CBEFFBiometricOrganizations.ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFFBDBFormatIdentifiers.ISO_IEC_JTC_1_SC_37_BIOMETRICS_IRIS_IMAGE_RECTILINEAR);

	static {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private JMenuItem menuItemNew;
	private JMenuItem menuItemOpen;
	private JMenuItem menuItemOpenCBEFFRecord;
	private JMenuItem menuItemSave;
	private JMenuItem menuItemExit;
	private JMenuItem menuItemAddIrisImage;
	private JMenuItem menuItemRemove;
	private JMenuItem menuItemSaveIrisImage;
	private JMenuItem menuItemConvert;
	private JMenuItem menuItemAbout;

	private DefaultMutableTreeNode treeRoot;
	private JTree treeView;
	private NPropertyGrid propertyGrid;
	private IIView iiView;

	private JFileChooser imageOpenFileDialog;
	private JFileChooser saveImageFileDialog;
	private JFileChooser cbeffRecordOpenFileDialog;
	private JFileChooser iiRecordOpenFileDialog;
	private JFileChooser iiRecordSaveFileDialog;

	private IIRecord record;
	private File file;

	private File openFile;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public MainFrame() {
		super();

		initGui();
		initializeFileChoosers();
		onSelectedItemChanged();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGui() {
		this.addWindowListener(new WindowAdapter() {

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

		JScrollPane irisTreeScrollPane = new JScrollPane(treeView);

		propertyGrid = new NPropertyGrid(true, new IIPropertiesTable());

		iiView = new IIView();
		JScrollPane imageScrollPane = new JScrollPane(iiView);

		leftSplitPane.setLeftComponent(irisTreeScrollPane);
		leftSplitPane.setRightComponent(propertyGrid);
		leftSplitPane.setDividerLocation(185);

		mainSplitPane.setLeftComponent(leftSplitPane);
		mainSplitPane.setRightComponent(imageScrollPane);
		mainSplitPane.setDividerLocation(250);

		this.getContentPane().add(mainSplitPane);
		this.pack();
	}

	private void initializeFileChoosers() {
		imageOpenFileDialog = new JFileChooser();
		saveImageFileDialog = new JFileChooser();
		iiRecordOpenFileDialog = new JFileChooser();
		cbeffRecordOpenFileDialog = new JFileChooser();
		iiRecordSaveFileDialog = new JFileChooser();

		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getBMP().getFileFilter(), "BMP files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getPNG().getFileFilter(), "PNG files"));
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getTIFF().getFileFilter(), "TIFF files"));
		StringBuilder allImageFilters = new StringBuilder(64);
		for (NImageFormat format : NImageFormat.getFormats()) {
			allImageFilters.append(format.getFileFilter()).append(';');
		}
		imageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(allImageFilters.toString(), "All image files"));

		saveImageFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getBMP().getFileFilter(), "BMP files"));
		saveImageFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
		saveImageFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
		saveImageFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getTIFF().getFileFilter(), "TIFF files"));
		saveImageFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getPNG().getFileFilter(), "PNG files"));

		iiRecordOpenFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("FCRecord files"));
		cbeffRecordOpenFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("CBEFFRecord files"));

		iiRecordSaveFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("FCRecord files"));
	}

	private void createMenuBar() {
		JMenuBar menuBar = new JMenuBar();

		JMenu menuFile = new JMenu("File");

		menuItemNew = new JMenuItem("New...");
		menuItemNew.addActionListener(this);
		menuItemNew.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_N, Event.CTRL_MASK));
		menuItemNew.setMnemonic(KeyEvent.VK_N);

		menuItemOpen = new JMenuItem("Open...");
		menuItemOpen.addActionListener(this);
		menuItemOpen.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, Event.CTRL_MASK));
		menuItemOpen.setMnemonic(KeyEvent.VK_O);

		menuItemOpenCBEFFRecord = new JMenuItem("Open CBEFFRecord...");
		menuItemOpenCBEFFRecord.addActionListener(this);
		menuItemOpenCBEFFRecord.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_C, Event.CTRL_MASK));
		menuItemOpenCBEFFRecord.setMnemonic(KeyEvent.VK_C);

		menuItemSave = new JMenuItem("Save...");
		menuItemSave.addActionListener(this);
		menuItemSave.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Event.CTRL_MASK));
		menuItemSave.setMnemonic(KeyEvent.VK_S);

		menuItemExit = new JMenuItem("Exit");
		menuItemExit.addActionListener(this);

		menuFile.add(menuItemNew);
		menuFile.add(menuItemOpen);
		menuFile.addSeparator();
		menuFile.add(menuItemOpenCBEFFRecord);
		menuFile.addSeparator();
		menuFile.add(menuItemSave);
		menuFile.addSeparator();
		menuFile.add(menuItemExit);

		JMenu menuEdit = new JMenu("Edit");

		menuItemAddIrisImage = new JMenuItem("Add iris image...");
		menuItemAddIrisImage.addActionListener(this);

		menuItemRemove = new JMenuItem("Remove iris/iris image...");
		menuItemRemove.addActionListener(this);

		menuItemSaveIrisImage = new JMenuItem("Save iris image...");
		menuItemSaveIrisImage.addActionListener(this);

		menuItemConvert = new JMenuItem("Convert ...");
		menuItemConvert.addActionListener(this);

		menuEdit.add(menuItemAddIrisImage);
		menuEdit.addSeparator();
		menuEdit.add(menuItemRemove);
		menuEdit.add(menuItemSaveIrisImage);
		menuEdit.addSeparator();
		menuEdit.add(menuItemConvert);

		JMenu menuHelp = new JMenu("Help");
		menuItemAbout = new JMenuItem("About");
		menuItemAbout.addActionListener(this);
		menuHelp.add(menuItemAbout);

		menuBar.add(menuFile);
		menuBar.add(menuEdit);
		menuBar.add(menuHelp);

		this.setJMenuBar(menuBar);
	}

	private IIRecordOptions getOptions(BDIFOptionsFormMode mode, BDIFStandard standard, int flags, NVersion version) {
		IIRecordOptionsFrame form = new IIRecordOptionsFrame(this);
		form.setMode(mode);
		form.setStandard(standard);
		form.setFlags(flags);
		form.setVersion(version);
		if (form.showDialog()) {
			return form.getIIRecordOptions();
		}
		return null;
	}

	private byte[] loadRecord(JFileChooser openFileDialog) {
		openFileDialog.setSelectedFile(null);
		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			openFile = openFileDialog.getSelectedFile();
			byte[] fileData = new byte[(int) openFile.length()];
			try {
				DataInputStream dis = new DataInputStream(new FileInputStream(openFile));
				dis.readFully(fileData);
				dis.close();
				return fileData;
			} catch (IOException e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
		openFile = null;
		return null;
	}

	private void saveRecord(JFileChooser saveFileDialog, File file, NBuffer buffer) {
		saveFileDialog.setSelectedFile(file);
		if (saveFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			try {
				String savePath = saveFileDialog.getSelectedFile().getPath();
				if (saveFileDialog.getFileFilter().getDescription().equals("IIRecord Files (*.dat)")) {
					if (savePath.lastIndexOf('.') == -1) {
						savePath += ".dat";
					}
				}
				NFile.writeAllBytes(savePath, buffer);
			} catch (IOException e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
	}

	private DefaultMutableTreeNode addIrisImage(DefaultMutableTreeNode irisNode, IIRIrisImage image) {
		String prefix = "IrisImage %s";
		int index = irisNode.getChildCount() + 1;
		DefaultMutableTreeNode irisImageNode = new IIRecordTreeNode(image, String.format(prefix, index));
		irisNode.add(irisImageNode);
		return irisNode;
	}

	private DefaultMutableTreeNode addIrisImages(DefaultMutableTreeNode irisNode, IIRecord iris) {
		for (IIRIrisImage item : iris.getIrisImages()) {
			addIrisImage(irisNode, item);
		}
		return irisNode;
	}

	private static DefaultMutableTreeNode addIris(DefaultMutableTreeNode recordNode, IIRIrisImage irisImage) {
		int index = recordNode.getChildCount() + 1;
		DefaultMutableTreeNode irisNode = new IIRecordTreeNode(irisImage, "Iris " + index);
		recordNode.add(irisNode);
		return irisNode;
	}

	private static DefaultMutableTreeNode addIrises(DefaultMutableTreeNode templateNode, IIRecord iiRecord) {
		for (IIRIrisImage irisImage : iiRecord.getIrisImages()) {
			addIris(templateNode, irisImage);
		}
		return templateNode;
	}

	private IIRecord convertToStandard(IIRecord record, BDIFStandard newStandard, int flags, NVersion version) {
		if (record.getStandard() == newStandard && record.getFlags() == flags && record.getVersion().equals(version)) {
			return record;
		}
		try {
			return new IIRecord(record, flags, newStandard, version);
		} catch (Exception e) {
			e.printStackTrace();
			showError(e.toString());
			return null;
		}
	}

	private void showError(String message) {
		JOptionPane.showMessageDialog(this, message, this.getTitle(), JOptionPane.ERROR_MESSAGE);
	}

	private void showWarning(String message) {
		JOptionPane.showMessageDialog(this, message, this.getTitle(), JOptionPane.WARNING_MESSAGE);
	}

	private void setTemplate(IIRecord template, File file) {
		setTemplate(template, file, "");
	}

	private void setTemplate(IIRecord iiRecord, File file, String version) {
		boolean newTemplate = record != iiRecord;
		record = iiRecord;
		this.file = file;
		treeRoot.removeAllChildren();
		treeView.updateUI();
		String fileName = null;
		if (file != null) {
			fileName = file.getName();
		}
		if (newTemplate) {
			DefaultMutableTreeNode recordNode = new IIRecordTreeNode(iiRecord, fileName == null ? "Untitled" : version == null ? fileName : fileName + version);
			if (iiRecord.getIrisImages() != null) {
				addIrises(recordNode, iiRecord);
			}
			treeRoot.add(recordNode);
			expandIrisTree();
			treeView.setRootVisible(false);
			treeView.setSelectionRow(0);
		}
		treeView.updateUI();
	}

	private void openCBEFF() {
		byte[] data = loadRecord(cbeffRecordOpenFileDialog);
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
		IIRecordTreeNode userObject = new IIRecordTreeNode(cbeffRecord, (fileName == null ? "Untitled" : fileName));
		addIrises(userObject, cbeffRecord);
		treeRoot.add(userObject);
		expandIrisTree();
		treeView.setRootVisible(false);
		treeView.setSelectionRow(0);
	}

	private static DefaultMutableTreeNode addIrises(DefaultMutableTreeNode recordNode, CBEFFRecord record) {
		getBiometricDataBlock(recordNode, record);
		for (CBEFFRecord child : record.getRecords()) {
			addIrises(recordNode, child);
		}
		return recordNode;
	}

	private static void getBiometricDataBlock(DefaultMutableTreeNode templateNode, CBEFFRecord cbeffRecord) {
		if (cbeffRecord.getBdbBuffer() != null) {
			if (cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT_POLAR ||cbeffRecord.getBdbFormat() == ISO_BDB_FORMAT_POLAR ||
					cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT_RECTILINEAR || cbeffRecord.getBdbFormat() == ISO_BDB_FORMAT_RECTILINEAR) {
				BDIFStandard standard = (cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT_POLAR || cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT_RECTILINEAR) ? BDIFStandard.ANSI : BDIFStandard.ISO;
				IIRecord iiRecord = new IIRecord(cbeffRecord.getBdbBuffer(), standard);
				DefaultMutableTreeNode recordNode = new IIRecordTreeNode(iiRecord, "IIRecord");
				addIrises(recordNode, iiRecord);
				templateNode.add(recordNode);
			}
		}
	}

	private void openRecord() {
		byte[] templ = loadRecord(iiRecordOpenFileDialog);
		if (templ == null) {
			return;
		}

		BDIFStandard standard = BDIFStandard.ISO;
		int flags = 0;
		NVersion version = IIRecord.VERSION_ISO_CURRENT;
		IIRecordOptions options = getOptions(BDIFOptionsFormMode.OPEN, standard, flags, version);
		if (options == null) {
			return;
		}

		IIRecord iirecord = null;
		try {
			iirecord = new IIRecord(new NBuffer(templ), options.getFlags(), options.getStandard());
		} catch (Exception ex) {
			ex.printStackTrace();
			showError("Failed to load template! Reason:\r\n" + ex);
			return;
		}
		setTemplate(iirecord, openFile);
	}

	private void saveRecord(IIRecord record, File file) {
		if (record == null) {
			showWarning("No template to save");
			return;
		}
		saveRecord(iiRecordSaveFileDialog, this.file, record.save());
	}

	private void onSelectedItemChanged() {
		DefaultMutableTreeNode selectedNode = null;
		if (treeView.getSelectionPath() != null) {
			selectedNode = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		}
		if (selectedNode != null) {
			Object taggedObject = selectedNode.getUserObject();

			IIRIrisImage image = null;
			if (taggedObject instanceof IIRIrisImage) {
				image = (IIRIrisImage) taggedObject;
			}

			iiView.setRecord(image);
			propertyGrid.setSource(taggedObject);

			if (taggedObject instanceof IIRecord) {
				this.record = (IIRecord) taggedObject;
			} else {
				taggedObject = selectedNode.getParent();
				if (taggedObject instanceof IIRecord) {
					this.record = (IIRecord) taggedObject;
				}
			}
		}
		updateControls();
	}

	private void expandIrisTree() {
		treeView.expandRow(0);
		for (int i = treeView.getRowCount() - 1; i > 0; i--) {
			treeView.expandRow(i);
		}
		treeView.updateUI();
	}

	private void newRecord() {
		BDIFStandard standard = BDIFStandard.ISO;
		int flags = 0;
		NVersion version = IIRecord.VERSION_ISO_CURRENT;

		IIRecordOptions options = getOptions(BDIFOptionsFormMode.NEW, standard, flags, version);
		if (options == null) {
			return;
		}

		try {
			setTemplate(new IIRecord(options.getStandard(), options.getVersion(), options.getFlags()), null, null);
		} catch (Exception ex) {
			ex.printStackTrace();
			showError(ex.toString());
		}
	}

	private void convert(IIRecord iiRecord) {
		if (iiRecord == null) {
			showWarning("IIRecord is not opened! Open or create new IIRecord first");
			return;
		}
		BDIFStandard standard = (iiRecord.getStandard() == BDIFStandard.ANSI) ? BDIFStandard.ISO : BDIFStandard.ANSI;
		int flags = 0;
		NVersion version = (standard == BDIFStandard.ISO) ? IIRecord.VERSION_ISO_CURRENT : IIRecord.VERSION_ANSI_CURRENT;

		IIRecordOptions options = getOptions(BDIFOptionsFormMode.CONVERT, standard, flags, version);
		if (options == null) {
			return;
		}
		setTemplate(convertToStandard(iiRecord, options.getStandard(), options.getFlags(), options.getVersion()), this.file, null);
		expandIrisTree();
	}

	private void remove() {
		if (treeView.getSelectionPath() == null) {
			showWarning("Iris or IrisImage must be selected!");
			return;
		}
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		int selectedRow = treeView.getSelectionRows()[0];
		if (selected != null && selected.getParent() != treeRoot) {
			Object taggedObject = selected.getUserObject();
			IIRecord iris = null;
			if (taggedObject instanceof IIRecord) {
				iris = (IIRecord) taggedObject;
			}
			try {
				if (iris == null) {
					IIRIrisImage iimage = null;
					if (taggedObject instanceof IIRIrisImage) {
						iimage = (IIRIrisImage) taggedObject;
					}
					if (iimage != null) {
						iris = (IIRecord) ((DefaultMutableTreeNode) selected.getParent()).getUserObject();
						iris.getIrisImages().remove(iimage);
					}
					((DefaultMutableTreeNode) selected.getParent()).remove(selected);
				} else {
						((DefaultMutableTreeNode) selected.getParent()).remove(selected);
				}
				if (selectedRow > 0) {
					treeView.setSelectionRow(selectedRow - 1);
				}
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
			treeView.updateUI();
		} else {
			showWarning("Iris or IrisImage must be selected!");
		}
	}

	private void saveIris() {
		if (treeView.getSelectionPath() == null) {
			showWarning("Please select iris image");
			return;
		}
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if (selected == null || selected.getParent() == treeRoot) {
			showWarning("Please select iris image");
			return;
		}
		if (saveImageFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			Object taggedObject = selected.getUserObject();
			IIRIrisImage iris = null;
			if (taggedObject instanceof IIRIrisImage) {
				iris = (IIRIrisImage) taggedObject;
			}
			if (iris != null) {
				try {
					NImage image = iris.toNImage();
					FileFilter selectedFilter = saveImageFileDialog.getFileFilter();
					if (selectedFilter instanceof Utils.ImageFileFilter) {
						image.save(saveImageFileDialog.getSelectedFile().getPath() + '.' + ((Utils.ImageFileFilter) saveImageFileDialog.getFileFilter()).getExtensions().get(0));
					} else {
						image.save(saveImageFileDialog.getSelectedFile().getPath());
					}
				} catch (IOException e) {
					e.printStackTrace();
					showError(e.toString());
				}
			}
		}
	}

	private void addIrisImage() {
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if (selected == null) {
			showWarning("Iris record must be selected before adding IrisImage!");
			return;
		}
		Object taggedObject = selected.getUserObject();

		IIRecord iris = null;
		if (taggedObject instanceof IIRecord) {
			iris = (IIRecord) taggedObject;
		} else {
			taggedObject = ((DefaultMutableTreeNode) selected.getParent()).getUserObject();
			if (taggedObject instanceof IIRecord) {
				iris = (IIRecord) taggedObject;
				selected = (DefaultMutableTreeNode) selected.getParent();
			}
		}

		if (iris != null) {
			if (imageOpenFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
				try {
					NImage image = NImage.fromFile(imageOpenFileDialog.getSelectedFile().getPath());
					if (image != null) {
						AddIrisFrame frame = new AddIrisFrame(this);
						if (frame.showDialog()) {
							IIRIrisImage irisImage = new IIRIrisImage(iris.getStandard(), iris.getVersion());
							irisImage.setPosition(frame.getEyePosition());
							if (isRecordFirstVersion(iris) && iris.getIrisImages().size() == 0) {
								iris.setRawImageHeight(image.getHeight());
								iris.setRawImageWidth(image.getWidth());
								iris.setIntensityDepth((byte) 8);
							}
							irisImage.setNImage(image);
							iris.getIrisImages().add(irisImage);
							addIrisImage(selected, irisImage);
						}
					}
					expandIrisTree();
				} catch (Exception ex) {
					ex.printStackTrace();
					showError(ex.toString());
				}
			}
		}
	}

	private boolean isRecordFirstVersion(IIRecord iiRecord) {
		return (iiRecord.getVersion().compareTo(IIRecord.VERSION_ISO_10) == 0) || (iiRecord.getVersion().compareTo(IIRecord.VERSION_ANSI_10) == 0);
	}

	private void updateControls() {
		menuItemSave.setEnabled(treeRoot.getChildCount() > 0);
		menuItemAddIrisImage.setEnabled(treeRoot.getChildCount() > 0);
		menuItemRemove.setEnabled(treeRoot.getChildCount() > 0);
		menuItemSaveIrisImage.setEnabled(treeRoot.getChildCount() > 0);
		menuItemConvert.setEnabled(treeRoot.getChildCount() > 0);
	}

	// ===========================================================
	// Event handling ActionListener
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == menuItemAbout) {
			AboutBox.show();
		} else if (source == menuItemNew) {
			newRecord();
		} else if (source == menuItemOpen) {
			openRecord();
		} else if (source == menuItemOpenCBEFFRecord) {
			openCBEFF();
		} else if (source == menuItemSave) {
			saveRecord(this.record, this.file);
		} else if (source == menuItemExit) {
			dispose();
		} else if (source == menuItemConvert) {
			convert(this.record);
		} else if (source == menuItemRemove) {
			remove();
		} else if (source == menuItemSaveIrisImage) {
			saveIris();
		} else if (source == menuItemAddIrisImage) {
			addIrisImage();
		}
	}

	// ===========================================================
	// Event handling TreeSelectionListener
	// ===========================================================

	@Override
	public void valueChanged(TreeSelectionEvent e) {
		onSelectedItemChanged();
	}

	// ==================================================================
	// Inner class
	// ==================================================================

	private static final class IIRecordTreeNode extends DefaultMutableTreeNode {

		private static final long serialVersionUID = 1L;

		private String name;

		IIRecordTreeNode(Object obj, String name) {
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
