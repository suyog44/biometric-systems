package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.biometrics.standards.FCRFaceImage;
import com.neurotec.biometrics.standards.FCRImageDataType;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.biometrics.standards.swing.FCView;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.samples.biometrics.standards.AddFaceImageFrame.FaceImageOptions;
import com.neurotec.samples.biometrics.standards.FCRecordOptionsFrame.FCRecordOptions;
import com.neurotec.samples.biometrics.standards.RawFaceImageOptionsFrame.RawFaceImageOptions;
import com.neurotec.samples.swing.NPropertyGrid;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.AboutBox;
import com.neurotec.util.NVersion;

import java.awt.Event;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.KeyEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.DataInputStream;
import java.io.File;
import java.io.FileInputStream;

import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
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

	private static final int ANSI_BDB_FORMAT = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.INCITS_TC_M1_BIOMETRICS, CBEFFBDBFormatIdentifiers.INCITS_TC_M1_BIOMETRICS_FACE_IMAGE);
	private static final int ISO_BDB_FORMAT = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFFBDBFormatIdentifiers.ISO_IEC_JTC_1_SC_37_BIOMETRICS_FACE_IMAGE);

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

	private JFileChooser imageOpenFileDialog;
	private JFileChooser rawImageOpenFileDialog;
	private JFileChooser saveImageFileDialog;
	private JFileChooser fcRecordOpenFileDialog;
	private JFileChooser cbeffRecordOpenFileDialog;
	private JFileChooser fcRecordSaveFileDialog;
	private JFileChooser saveRawFileDialog;

	private JTree treeView;
	private DefaultMutableTreeNode treeRoot;
	private JPanel panelImage;
	private FCView imageView;
	private NPropertyGrid propertyGrid;

	private JMenuItem menuItemAbout;
	private JMenuItem menuItemSaveAsData;
	private JMenuItem menuItemSaveAsImage;
	private JMenuItem menuItemAddFromImage;
	private JMenuItem menuItemAddFromData;
	private JMenuItem menuItemRemove;
	private JMenuItem menuItemConvert;
	private JMenuItem menuItemNew;
	private JMenuItem menuItemOpen;
	private JMenuItem menuItemSave;
	private JMenuItem menuItemOpenCBEFF;
	private JMenuItem menuItemExit;

	// ==============================================
	// Private fields
	// ==============================================

	private FCRecord record;
	private File file;
	private File openedFile;

	// ==============================================
	// Public constructor
	// ==============================================

	public MainFrame() {
		super();

		initGui();
		initFileChoosers();

		menuItemAbout.setText(AboutBox.getName());

		onSelectedItemChanged();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initGui() {
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
		JScrollPane faceTreeScrollPane = new JScrollPane(treeView);

		propertyGrid = new NPropertyGrid(true, new FCPropertiesTable());

		imageView = new FCView();
		JScrollPane imageScrollPane = new JScrollPane();
		panelImage = new JPanel();
		panelImage.setLayout(new GridLayout(1, 1));
		panelImage.add(imageScrollPane);
		imageScrollPane.setViewportView(imageView);

		leftSplitPane.setLeftComponent(faceTreeScrollPane);
		leftSplitPane.setRightComponent(propertyGrid);
		leftSplitPane.setDividerLocation(185);

		mainSplitPane.setLeftComponent(leftSplitPane);
		mainSplitPane.setRightComponent(panelImage);
		mainSplitPane.setDividerLocation(250);

		this.getContentPane().add(mainSplitPane);
		this.pack();
	}

	private void initFileChoosers() {
		imageOpenFileDialog = new JFileChooser();
		rawImageOpenFileDialog = new JFileChooser();
		saveImageFileDialog = new JFileChooser();
		fcRecordOpenFileDialog = new JFileChooser();
		cbeffRecordOpenFileDialog = new JFileChooser();
		fcRecordSaveFileDialog = new JFileChooser();
		saveRawFileDialog = new JFileChooser();

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

		rawImageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
		rawImageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
		rawImageOpenFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter() + ";" + NImageFormat.getJPEG2K().getFileFilter(), "All supported files"));

		fcRecordSaveFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("FCRecord files"));

		fcRecordOpenFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("FCRecord files"));
		cbeffRecordOpenFileDialog.addChoosableFileFilter(new Utils.TemplateFileFilter("CBEFFRecord files"));
	}

	private void createMenuBar() {
		JMenuBar menuBar = new JMenuBar();

		JMenu menuFile = new JMenu("File");

		menuItemNew = new JMenuItem("New ...");
		menuItemNew.addActionListener(this);
		menuItemNew.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_N, Event.CTRL_MASK));
		menuItemNew.setMnemonic(KeyEvent.VK_N);

		menuItemOpen = new JMenuItem("Open ...");
		menuItemOpen.addActionListener(this);
		menuItemOpen.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, Event.CTRL_MASK));
		menuItemOpen.setMnemonic(KeyEvent.VK_O);

		menuItemSave = new JMenuItem("Save ...");
		menuItemSave.addActionListener(this);
		menuItemSave.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Event.CTRL_MASK));
		menuItemSave.setMnemonic(KeyEvent.VK_S);

		menuItemOpenCBEFF = new JMenuItem("Open CBEFFRecord ...");
		menuItemOpenCBEFF.addActionListener(this);
		menuItemOpenCBEFF.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_C, Event.CTRL_MASK));
		menuItemOpenCBEFF.setMnemonic(KeyEvent.VK_C);

		menuItemExit = new JMenuItem("Exit");
		menuItemExit.addActionListener(this);

		menuFile.add(menuItemNew);
		menuFile.add(menuItemOpen);
		menuFile.add(menuItemSave);
		menuFile.addSeparator();
		menuFile.add(menuItemOpenCBEFF);
		menuFile.addSeparator();
		menuFile.add(menuItemExit);

		JMenu menuEdit = new JMenu("Edit");

		menuItemAddFromImage = new JMenuItem("Add face from image ...");
		menuItemAddFromImage.addActionListener(this);

		menuItemAddFromData = new JMenuItem("Add face from data ...");
		menuItemAddFromData.addActionListener(this);

		menuItemRemove = new JMenuItem("Remove face ...");
		menuItemRemove.addActionListener(this);

		menuItemSaveAsImage = new JMenuItem("Save face as image ...");
		menuItemSaveAsImage.addActionListener(this);
		menuItemSaveAsData = new JMenuItem("Save face as data ...");
		menuItemSaveAsData.addActionListener(this);

		menuItemConvert = new JMenuItem("Convert ...");
		menuItemConvert.addActionListener(this);

		menuEdit.add(menuItemAddFromImage);
		menuEdit.add(menuItemAddFromData);
		menuEdit.addSeparator();
		menuEdit.add(menuItemRemove);
		menuEdit.addSeparator();
		menuEdit.add(menuItemSaveAsImage);
		menuEdit.add(menuItemSaveAsData);
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

	private void showError(String message) {
		JOptionPane.showMessageDialog(this, message, this.getTitle(), JOptionPane.ERROR_MESSAGE);
	}

	private FCRecordTreeNode getRecordNode() {
		return treeRoot.getChildCount() > 0 ? (FCRecordTreeNode) treeRoot.getFirstChild() : null;
	}

	private void setRecord(FCRecord fcRecord, File f) {
		setRecord(fcRecord, f, "");
	}

	private void setRecord(FCRecord fcRecord, File f, String version) {
		boolean newTemplate = this.record != fcRecord;
		this.record = fcRecord;
		this.file = f;
		String fileName = null;
		if (f != null) {
			fileName = f.getName();
		}
		treeRoot.removeAllChildren();
		if (newTemplate) {
			treeView.setRootVisible(true);
			FCRecordTreeNode userObject = new FCRecordTreeNode(fcRecord, (fileName == null ? "Untitled" : version == null ? fileName : fileName + version));
			addFaceImages(userObject, fcRecord);
			treeRoot.add(userObject);
			treeView.updateUI();
			expandFaceTree();
			treeView.setRootVisible(false);
			treeView.setSelectionRow(0);
		}
		treeView.updateUI();
	}

	private void setCBEFFRecord(CBEFFRecord cbeffRecord, File file) {
		this.file = file;
		String fileName = null;
		if (file != null) {
			fileName = file.getName();
		}
		treeRoot.removeAllChildren();
		treeView.setRootVisible(true);
		FCRecordTreeNode userObject = new FCRecordTreeNode(cbeffRecord, (fileName == null ? "Untitled" : fileName ));
		addFaceImages(userObject, cbeffRecord);
		treeRoot.add(userObject);
		expandFaceTree();
		treeView.setRootVisible(false);
		treeView.setSelectionRow(0);
	}

	private void newRecord() {
		BDIFStandard standard = BDIFStandard.ISO;
		int flags = 0;
		NVersion version = FCRecord.VERSION_ISO_CURRENT;
		FCRecordOptions recordOption = getOptions(BDIFOptionsFormMode.NEW, standard, flags, version);
		if (recordOption == null) {
			return;
		}
		newTemplate(recordOption.getStandard(), recordOption.getFlags(), recordOption.getVersion());
	}

	private void newTemplate(BDIFStandard standard, int flags, NVersion version) {
		try {
			FCRecord record = new FCRecord(standard, version, flags);
			setRecord(record, null, null);
		} catch (Exception e) {
			e.printStackTrace();
			showError(e.toString());
		}
	}

	private void openTemplate() {
		byte[] templ = loadTemplate(fcRecordOpenFileDialog);
		if (templ == null) {
			return;
		}

		BDIFStandard standard = BDIFStandard.ISO;
		int flags = 0;
		NVersion version = FCRecord.VERSION_ISO_CURRENT;
		FCRecordOptions recordOption = getOptions(BDIFOptionsFormMode.OPEN, standard, flags, version);
		if (recordOption == null) {
			return;
		}

		FCRecord fcrecord;
		try {
			fcrecord = new FCRecord(NBuffer.fromArray(templ), recordOption.getFlags(), recordOption.getStandard());
		} catch (Exception ex) {
			ex.printStackTrace();
			showError("Failed to load template! Reason:\r\n" + ex);
			return;
		}
		setRecord(fcrecord, openedFile);
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
				setCBEFFRecord(record, openedFile);
			} catch (Exception e) {
				e.printStackTrace();
				showError("Failed to open CBEFFRecord! Reason:\r\n" + e);
				return;
			}
		}
	}

	private void saveTemplate() {
		saveTemplate(fcRecordSaveFileDialog, file, record.save());
	}

	private void onSelectedItemChanged() {
		FCRFaceImage faceImage;
		if (treeView.getSelectionPath() == null) {
			faceImage = null;
		} else {
			DefaultMutableTreeNode userObject = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
			Object taggedObject = userObject.getUserObject();
			if (taggedObject instanceof FCRFaceImage) {
				faceImage = (FCRFaceImage) taggedObject;
			} else {
				faceImage = null;
			}

			if (taggedObject instanceof FCRecord) {
				this.record = (FCRecord) taggedObject;
			} else {
				taggedObject = ((DefaultMutableTreeNode) userObject.getParent()).getUserObject();
				if (taggedObject instanceof FCRecord) {
					this.record = (FCRecord) taggedObject;
				}
			}
		}
		imageView.setRecord(faceImage);
		if (faceImage == null) {
			if (treeView.getSelectionPath() != null) {
				propertyGrid.setSource(((DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent()).getUserObject());
			}
		} else {
			propertyGrid.setSource(faceImage);
		}
	}

	private void saveAsData() {
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if ((selected != null) && (selected.getParent() != treeRoot)) {
			FCRecordTreeNode userObject = (FCRecordTreeNode) selected;
			FCRFaceImage img = null;
			if (userObject.getUserObject() instanceof FCRFaceImage) {
				img = (FCRFaceImage) userObject.getUserObject();
			}
			if (img != null) {
				String extension;
				saveRawFileDialog.setAcceptAllFileFilterUsed(false);
				if (img.getImageDataType() == FCRImageDataType.JPEG) {
					saveRawFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG().getFileFilter(), "JPEG files"));
					extension = NImageFormat.getJPEG().getDefaultFileExtension();
				} else {
					saveRawFileDialog.addChoosableFileFilter(new Utils.ImageFileFilter(NImageFormat.getJPEG2K().getFileFilter(), "JPEG 2000 files"));
					extension = NImageFormat.getJPEG2K().getDefaultFileExtension();
				}

				if (saveRawFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
					try {
						String savePath = saveRawFileDialog.getSelectedFile().getPath();
						if (savePath.lastIndexOf('.') == -1) {
							savePath = savePath + '.' + extension;
						}
						NFile.writeAllBytes(savePath, img.getImageData());
					} catch (Exception ex) {
						ex.printStackTrace();
						showError("Failed to save face to data.\r\nReason: " + ex);
					}
				}
			}
		}
	}

	private void saveAsImage() {
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if ((selected != null) && (selected.getParent() != treeRoot)) {
			if (saveImageFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
				try {
					Object userObject = selected.getUserObject();
					FCRFaceImage img = null;
					if (userObject instanceof FCRFaceImage) {
						img = (FCRFaceImage) userObject;
					}
					if (img != null) {
						NImage image = img.toNImage();
						FileFilter selectedFilter = saveImageFileDialog.getFileFilter();
						if (selectedFilter instanceof Utils.ImageFileFilter) {
							image.save(saveImageFileDialog.getSelectedFile().getPath() + '.' + ((Utils.ImageFileFilter) saveImageFileDialog.getFileFilter()).getExtensions().get(0));
						} else {
							image.save(saveImageFileDialog.getSelectedFile().getPath());
						}
					}
				} catch (Exception ex) {
					ex.printStackTrace();
					showError("Failed to save image to file!\r\nReason: " + ex);
				}
			}
		}
	}

	private void addFaceFromImage() {
		if (treeView.getSelectionPath() == null) {
			return;
		}
		DefaultMutableTreeNode recordNode = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if (recordNode != null) {
			try {
				imageOpenFileDialog.setSelectedFile(null);
				NImage imgFromFile;
				if (imageOpenFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
					imgFromFile = NImage.fromFile(imageOpenFileDialog.getSelectedFile().getPath());
				} else {
					return;
				}

				Object taggedObject = recordNode.getUserObject();
				FCRecord record = null;
				if (taggedObject instanceof FCRecord) {
					record = (FCRecord) taggedObject;
				} else {
					taggedObject = ((DefaultMutableTreeNode) recordNode.getParent()).getUserObject();
					if (taggedObject instanceof FCRecord) {
						recordNode = (DefaultMutableTreeNode) recordNode.getParent();
						record = (FCRecord) taggedObject;
					}
				}

				if (record != null) {
					AddFaceImageFrame frame = new AddFaceImageFrame(this);
					String extension = getExtension(imageOpenFileDialog.getSelectedFile());
					FCRImageDataType dataType;
					if (extension != null && (NImageFormat.getJPEG2K().getFileFilter()).contains(extension)) {
						dataType = FCRImageDataType.JPEG_2000;
					} else {
						dataType = FCRImageDataType.JPEG;
					}
					frame.setImageDataType(dataType);
					if (!frame.showDialog()) {
						return;
					}
					FaceImageOptions faceOptions = frame.getFaceImageOptions();

					FCRFaceImage img = new FCRFaceImage(record.getStandard(), record.getVersion());
					img.setFaceImageType(faceOptions.getImageType());
					img.setImageDataType(faceOptions.getImageDataType());
					img.setImage(imgFromFile);
					record.getFaceImages().add(img);

					addFaceImage(recordNode, img);
					treeView.updateUI();
					expandFaceTree();
					treeView.setSelectionRow(treeView.getRowCount() - 1);
				}
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
	}

	private String getExtension(File f) {
		String ext = null;
		String s = f.getName();
		int i = s.lastIndexOf('.');

		if (i > 0 && i < s.length() - 1) {
			ext = s.substring(i + 1).toLowerCase();
		}
		return ext;
	}

	private void addFaceFromData() {
		DefaultMutableTreeNode recordNode = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if (recordNode != null) {
			try {
				if (rawImageOpenFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
					FCRImageDataType dataType;

					RawFaceImageOptionsFrame form = new RawFaceImageOptionsFrame(this);

					String extension = getExtension(rawImageOpenFileDialog.getSelectedFile());
					if (extension != null && (NImageFormat.getJPEG2K().getFileFilter()).contains(extension)) {
						dataType = FCRImageDataType.JPEG_2000;
					} else {
						dataType = FCRImageDataType.JPEG;
					}
					form.setImageDataType(dataType);
					if (!form.showDialog()) {
						return;
					}
					RawFaceImageOptions options = form.getRawFaceImageOptions();

					Object taggedObject = recordNode.getUserObject();
					FCRecord record = null;
					if (taggedObject instanceof FCRecord) {
						record = (FCRecord) taggedObject;

						FCRFaceImage img = new FCRFaceImage(record.getStandard(), record.getVersion());
						img.setFaceImageType(options.getFaceImageType());
						img.setImageDataType(options.getImageDataType());
						img.setWidth(options.getImageWidth());
						img.setHeight(options.getImageHeight());
						img.setImageColorSpace(options.getImageColorSpace(), (byte) options.getVendorColorSpace());
						NBuffer buffer = NFile.readAllBytes(rawImageOpenFileDialog.getSelectedFile().getAbsolutePath());
						img.setImageData(buffer);

						record.getFaceImages().add(img);
						addFaceImage((DefaultMutableTreeNode) recordNode, img);
						treeView.updateUI();
						expandFaceTree();
						treeView.setSelectionRow(treeView.getRowCount() - 1);
					}
				}
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
	}

	private void remove() {
		DefaultMutableTreeNode selected = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		if ((selected != null) && (selected.getParent() != treeRoot)) {
			try {
				FCRFaceImage img = (FCRFaceImage) selected.getUserObject();
				record.getFaceImages().remove(img);
				((DefaultMutableTreeNode) selected.getParent()).remove(selected);
				treeView.updateUI();
				treeView.setSelectionRow(0);
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
	}

	private void convert() {
		DefaultMutableTreeNode recordNode = (DefaultMutableTreeNode) treeView.getSelectionPath().getLastPathComponent();
		Object taggedObject = recordNode.getUserObject();
		FCRecord record = null;
		if (taggedObject instanceof FCRecord) {
			record = (FCRecord) taggedObject;
		} else {
			taggedObject = ((DefaultMutableTreeNode) recordNode.getParent()).getUserObject();
			if (taggedObject instanceof FCRecord) {
				record = (FCRecord) taggedObject;
			}
		}

		if (record != null) {
			int flags = 0;
			BDIFStandard standard = (record.getStandard() == BDIFStandard.ANSI) ? BDIFStandard.ISO : BDIFStandard.ANSI;
			NVersion version = (standard == BDIFStandard.ISO) ? FCRecord.VERSION_ISO_CURRENT : FCRecord.VERSION_ANSI_CURRENT;
			FCRecordOptions options = getOptions(BDIFOptionsFormMode.CONVERT, standard, flags, version);
			if (options != null) {
				if ((!record.getStandard().equals(options.getStandard())) || (record.getFlags() != options.getFlags()) || (!record.getVersion().equals(options.getVersion()))) {
					setRecord(convertToStandard(record, options.getStandard(), options.getFlags(), options.getVersion()), file, null);
				}
			}
		}
	}

	private FCRecordOptions getOptions(BDIFOptionsFormMode mode, BDIFStandard standard, int flags, NVersion version ) {
		FCRecordOptionsFrame form = new FCRecordOptionsFrame(MainFrame.this);

		form.setStandard(standard);
		form.setFlags(flags);
		form.setMode(mode);
		form.setVersion(version);
		if (form.showDialog()) {
			return form.getRecordOptions();
		}
		return null;
	}

	private byte[] loadTemplate(JFileChooser openFileDialog) {
		openFileDialog.setSelectedFile(null);
		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			openedFile = openFileDialog.getSelectedFile();
			byte[] fileData = new byte[(int) openedFile.length()];
			try {
				DataInputStream dis = new DataInputStream(new FileInputStream(openedFile));
				dis.readFully(fileData);
				dis.close();
				return fileData;
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
		openedFile = null;
		return null;
	}

	private void saveTemplate(JFileChooser saveFileDialog, File file, NBuffer template) {
		if (file != null) {
			saveFileDialog.setSelectedFile(file);
		}
		if (saveFileDialog.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			try {
				String savePath = saveFileDialog.getSelectedFile().getPath();
				if (saveFileDialog.getFileFilter().getDescription().equals("FCRecord Files (*.dat)")) {
					if (savePath.lastIndexOf('.') == -1) {
						savePath += ".dat";
					}
				}
				NFile.writeAllBytes(savePath, template);
			} catch (Exception e) {
				e.printStackTrace();
				showError(e.toString());
			}
		}
	}

	private void expandFaceTree() {
		treeView.expandRow(0);
		for (int i = treeView.getRowCount() - 1; i > 0; i--) {
			treeView.expandRow(i);
		}
		treeView.updateUI();
	}

	private DefaultMutableTreeNode addFaceImages(DefaultMutableTreeNode templateNode, FCRecord record) {
		for (FCRFaceImage faceImage : record.getFaceImages()) {
			addFaceImage(templateNode, faceImage);
		}
		return templateNode;
	}

	private void getBiometricDataBlock(DefaultMutableTreeNode templateNode, CBEFFRecord cbeffRecord) {
		if (cbeffRecord.getBdbBuffer() != null && (cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT || cbeffRecord.getBdbFormat() == ISO_BDB_FORMAT)) {
			BDIFStandard standard = cbeffRecord.getBdbFormat() == ANSI_BDB_FORMAT ? BDIFStandard.ANSI : BDIFStandard.ISO;
			FCRecord fcRecord = new FCRecord(cbeffRecord.getBdbBuffer(), standard);
			DefaultMutableTreeNode recordNode = new FCRecordTreeNode(fcRecord, "FCRecord");
			addFaceImages(recordNode, fcRecord);
			templateNode.add(recordNode);
		}
	}

	private DefaultMutableTreeNode addFaceImages(DefaultMutableTreeNode recordNode, CBEFFRecord record) {
		getBiometricDataBlock(recordNode, record);
		for (CBEFFRecord child : record.getRecords()) {
			addFaceImages(recordNode, child);
		}
		return recordNode;
	}

	private DefaultMutableTreeNode addFaceImage(DefaultMutableTreeNode facesNode, FCRFaceImage faceImage) {
		int index = facesNode.getChildCount() + 1;
		DefaultMutableTreeNode recordNode = new FCRecordTreeNode(faceImage, "FaceImage " + index);
		facesNode.add(recordNode);
		return recordNode;
	}

	private FCRecord convertToStandard(FCRecord record, BDIFStandard newStandard, int flags, NVersion version) {
		try {
			return new FCRecord(record, flags, newStandard, version);
		} catch (Exception e) {
			e.printStackTrace();
			showError(e.toString());
			return null;
		}
	}

	// ==============================================
	// Event handling ActionListener
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == menuItemSaveAsData) {
			saveAsData();
		} else if (source == menuItemSaveAsImage) {
			saveAsImage();
		} else if (source == menuItemAddFromImage) {
			addFaceFromImage();
		} else if (source == menuItemAddFromData) {
			addFaceFromData();
		} else if (source == menuItemRemove) {
			remove();
		} else if (source == menuItemConvert) {
			convert();
		} else if (source == menuItemAbout) {
			AboutBox.show();
		} else if (source == menuItemNew) {
			newRecord();
		} else if (source == menuItemOpen) {
			openTemplate();
		} else if (source == menuItemSave) {
			try {
				saveTemplate();
			} catch (Exception ex) {
				ex.printStackTrace();
				showError(ex.toString());
			}
		} else if (source == menuItemOpenCBEFF) {
			openCBEFFRecord();
		} else if (source == menuItemExit) {
			dispose();
		}

	}

	// ==============================================
	// Event handling TreeSelectionListener
	// ==============================================

	public void valueChanged(TreeSelectionEvent e) {
		onSelectedItemChanged();
	}

	// ==================================================================
	// Inner class
	// ==================================================================

	private static final class FCRecordTreeNode extends DefaultMutableTreeNode {

		private static final long serialVersionUID = 1L;

		private String name;

		public FCRecordTreeNode(Object obj, String name) {
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
