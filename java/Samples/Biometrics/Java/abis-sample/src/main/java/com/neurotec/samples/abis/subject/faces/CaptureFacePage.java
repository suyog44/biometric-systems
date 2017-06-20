package com.neurotec.samples.abis.subject.faces;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.Insets;
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
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JFileChooser;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.swing.NFaceView;
import com.neurotec.devices.NCamera;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.NodeChangeEvent;
import com.neurotec.samples.abis.event.NodeChangeListener;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.settings.Settings;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.samples.abis.swing.GeneralizeProgressView;
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

public final class CaptureFacePage extends Page implements CaptureFaceView, PageNavigationListener, ActionListener, NodeChangeListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private CaptureFaceModel biometricModel;
	private CaptureBiometricController biometricController;
	private NFace currentBiometric;
	private Operation currentOperation;

	private ImageThumbnailFileChooser fcOpenImage;
	private File lastFile;
	private NFaceView faceView;
	private IcaoWarningsPanel icaoWarningsView;	

	private SubjectTree facesTree;
	private JButton btnCapture;
	private JButton btnFinish;
	private JButton btnCancel;
	private JButton btnForceEnd;
	private JButton btnForceStart;
	private JButton btnRepeat;
	private ButtonGroup buttonGroupSource;
	private JCheckBox cbManual;
	private JCheckBox cbStream;
	private JCheckBox cbIcao;
	private JRadioButton rbCamera;
	private JRadioButton rbImage;
	private JRadioButton rbVideo;
	private JCheckBox cbMirrorView;
	private NViewZoomSlider horizontalZoomSlider;
	private JScrollPane spFace;
	private JTextPane tpStatus;
	private JPanel panelVideoButtons;
	private JPanel panelStatus;
	private GeneralizeProgressView generalizeProgressView;

	private final LockingTask cancelTask = new LockingTask() {
		@Override
		public void performTask() {
			biometricController.cancel();
		}
	};
	private JCheckBox cbWithGeneralization;
	private GridBagConstraints gridBagConstraints_1;
	private GridBagConstraints gridBagConstraints_2;
	private GridBagConstraints gridBagConstraints_3;
	private GridBagConstraints gridBagConstraints_4;
	private GridBagConstraints gridBagConstraints_5;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureFacePage(PageNavigationController pageController) {
		super("New...", pageController);
		currentOperation = Operation.NONE;
		initGUI();
		lastFile = new File(Settings.getInstance().getLastFaceFilePath());
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		setLayout(new BorderLayout());
		JPanel panelTop = new JPanel();
		panelTop.setLayout(new FlowLayout(FlowLayout.LEADING));
		add(panelTop, BorderLayout.NORTH);
		{
			JPanel panelCaptureOptions = new JPanel();
			panelCaptureOptions.setBorder(BorderFactory.createTitledBorder("Capture options"));
			panelTop.add(panelCaptureOptions);
			GridBagLayout panelCaptureOptionsLayout = new GridBagLayout();
			panelCaptureOptionsLayout.columnWidths = new int[] {0, 0, 0};
			panelCaptureOptionsLayout.rowHeights = new int[] {0, 0, 0};
			panelCaptureOptions.setLayout(panelCaptureOptionsLayout);
			buttonGroupSource = new ButtonGroup();
			{
				rbCamera = new JRadioButton("From camera");
				buttonGroupSource.add(rbCamera);
				rbCamera.addActionListener(this);
				GridBagConstraints gridBagConstraints = new GridBagConstraints();
				gridBagConstraints.insets = new Insets(0, 0, 5, 5);
				gridBagConstraints.gridx = 0;
				gridBagConstraints.gridy = 0;
				gridBagConstraints.anchor = GridBagConstraints.LINE_START;
				panelCaptureOptions.add(rbCamera, gridBagConstraints);
			}
			{
				rbImage = new JRadioButton("From image file");
				buttonGroupSource.add(rbImage);
				rbImage.addActionListener(this);
				gridBagConstraints_1 = new GridBagConstraints();
				gridBagConstraints_1.insets = new Insets(0, 0, 5, 5);
				gridBagConstraints_1.gridx = 0;
				gridBagConstraints_1.gridy = 1;
				gridBagConstraints_1.anchor = GridBagConstraints.LINE_START;
				panelCaptureOptions.add(rbImage, gridBagConstraints_1);
			}
			{
				cbWithGeneralization = new JCheckBox("With generalization");
				GridBagConstraints gbc_cbWithGeneralization = new GridBagConstraints();
				gbc_cbWithGeneralization.anchor = GridBagConstraints.WEST;
				gbc_cbWithGeneralization.insets = new Insets(0, 0, 5, 5);
				gbc_cbWithGeneralization.gridx = 1;
				gbc_cbWithGeneralization.gridy = 1;
				gbc_cbWithGeneralization.gridwidth = 2;
				panelCaptureOptions.add(cbWithGeneralization, gbc_cbWithGeneralization);
			}
			{
				rbVideo = new JRadioButton("From video file");
				rbVideo.addActionListener(this);
				buttonGroupSource.add(rbVideo);
				gridBagConstraints_2 = new GridBagConstraints();
				gridBagConstraints_2.insets = new Insets(0, 0, 5, 5);
				gridBagConstraints_2.gridx = 0;
				gridBagConstraints_2.gridy = 2;
				gridBagConstraints_2.anchor = GridBagConstraints.LINE_START;
				panelCaptureOptions.add(rbVideo, gridBagConstraints_2);
			}
			{
				cbIcao = new JCheckBox("Check ICAO compliance");
				cbIcao.setSelected(true);
				cbIcao.addItemListener(new CheckBoxListener());
				gridBagConstraints_3 = new GridBagConstraints();
				gridBagConstraints_3.anchor = GridBagConstraints.WEST;
				gridBagConstraints_3.insets = new Insets(0, 0, 5, 5);
				gridBagConstraints_3.gridx = 1;
				gridBagConstraints_3.gridy = 0;
				panelCaptureOptions.add(cbIcao, gridBagConstraints_3);
			}
			{
				cbStream = new JCheckBox("Stream");
				cbStream.setSelected(true);
				gridBagConstraints_4 = new GridBagConstraints();
				gridBagConstraints_4.anchor = GridBagConstraints.WEST;
				gridBagConstraints_4.insets = new Insets(0, 0, 5, 5);
				gridBagConstraints_4.gridx = 2;
				gridBagConstraints_4.gridy = 0;
				panelCaptureOptions.add(cbStream, gridBagConstraints_4);
			}
			{
				cbManual = new JCheckBox("Manual");
				cbManual.setSelected(false);
				gridBagConstraints_5 = new GridBagConstraints();
				gridBagConstraints_5.anchor = GridBagConstraints.WEST;
				gridBagConstraints_5.insets = new Insets(0, 0, 5, 0);
				gridBagConstraints_5.gridx = 3;
				gridBagConstraints_5.gridy = 0;
				panelCaptureOptions.add(cbManual, gridBagConstraints_5);
			}
			{
				btnCapture = new JButton("Capture");
				GridBagConstraints gbc_btnCapture = new GridBagConstraints();
				gbc_btnCapture.anchor = GridBagConstraints.WEST;
				gbc_btnCapture.insets = new Insets(0, 0, 5, 5);
				gbc_btnCapture.gridx = 1;
				gbc_btnCapture.gridy = 2;
				panelCaptureOptions.add(btnCapture, gbc_btnCapture);
				btnCapture.addActionListener(this);
			}
		}

		JPanel panelMain = new JPanel();
		panelMain.setLayout(new BorderLayout());
		add(panelMain, BorderLayout.CENTER);
		{
			{
				spFace = new JScrollPane();
				spFace.setBorder(BorderFactory.createLineBorder(new Color(0, 0, 0)));
				panelMain.add(spFace, BorderLayout.CENTER);
				faceView = new NFaceView();
				faceView.setShowGender(true);
				faceView.setShowGenderConfidence(false);
				faceView.setAutofit(true);
				faceView.setShowIcaoArrows(false);
				spFace.setViewportView(faceView);
			}
			{
				JPanel panelBottom = new JPanel();
				panelBottom.setLayout(new BoxLayout(panelBottom, BoxLayout.Y_AXIS));
				panelMain.add(panelBottom, BorderLayout.SOUTH);
				{
					{
						JPanel panelGeneralization = new JPanel();
						panelBottom.add(panelGeneralization);
						panelGeneralization.setLayout(new BorderLayout(0, 0));
						generalizeProgressView = new GeneralizeProgressView();
						generalizeProgressView.setView(faceView);
						panelGeneralization.add(generalizeProgressView);
					}
					{
						panelStatus = new JPanel();
						panelStatus.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
						panelStatus.setLayout(new GridLayout(1, 0));
						panelBottom.add(panelStatus);
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
							panelStatus.add(tpStatus);
						}
					}
					{
						Dimension buttonSize = new Dimension(80, 23);
						panelVideoButtons = new JPanel();
						panelBottom.add(panelVideoButtons);
						{
							btnCancel = new JButton("Cancel");
							btnCancel.setPreferredSize(buttonSize);
							btnCancel.addActionListener(this);
							panelVideoButtons.add(btnCancel);
						}
						{
							btnForceStart = new JButton("Start");
							btnForceStart.setPreferredSize(buttonSize);
							btnForceStart.addActionListener(this);
							panelVideoButtons.add(btnForceStart);
						}
						{
							btnForceEnd = new JButton("End");
							btnForceEnd.setPreferredSize(buttonSize);
							btnForceEnd.addActionListener(this);
							panelVideoButtons.add(btnForceEnd);
						}
						{
							btnRepeat = new JButton("Repeat");
							btnRepeat.setPreferredSize(buttonSize);
							btnRepeat.addActionListener(this);
							panelVideoButtons.add(btnRepeat);
						}
					}
					{
						JPanel panelLast = new JPanel();
						panelLast.setLayout(new BorderLayout());
						panelBottom.add(panelLast);
						{
							JPanel panelZoom = new JPanel();
							panelZoom.setLayout(new FlowLayout(FlowLayout.LEFT));
							panelLast.add(panelZoom, BorderLayout.WEST);
							{
								cbMirrorView = new JCheckBox("Mirror view");
								cbMirrorView.addActionListener(this);
								panelZoom.add(cbMirrorView);
								horizontalZoomSlider = new NViewZoomSlider();
								horizontalZoomSlider.setView(faceView);
								panelZoom.add(horizontalZoomSlider);
							}
						}
						{
							JPanel panelFinishButton = new JPanel();
							panelFinishButton.setLayout(new FlowLayout(FlowLayout.TRAILING));
							panelLast.add(panelFinishButton, BorderLayout.EAST);
							{
								btnFinish = new JButton();
								btnFinish.setText("Finish");
								btnFinish.addActionListener(this);
								panelFinishButton.add(btnFinish);
							}
						}
					}
				}
				
				JPanel panelSide = new JPanel();
				panelSide.setLayout(new BorderLayout());
				panelMain.add(panelSide, BorderLayout.WEST);
				{
					{
						icaoWarningsView = new IcaoWarningsPanel();
						icaoWarningsView.setVisible(false);
						panelSide.add(icaoWarningsView, BorderLayout.NORTH);

						facesTree = new SubjectTree();
						facesTree.setBorder(BorderFactory.createLineBorder(new Color(0, 0, 0)));
						facesTree.addPropertyChangeListener(facesTreePropertyChanged);
						facesTree.setShownTypes(EnumSet.of(NBiometricType.FACE));
						facesTree.setShowBiometricsOnly(true);
						facesTree.setShowRemoveButton(false);
						facesTree.addNodeChangeListener(this);
						facesTree.setVisible(false);
						panelSide.add(facesTree, BorderLayout.CENTER);
					}
				}
			}
		}
		fcOpenImage = new ImageThumbnailFileChooser();
		fcOpenImage.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcOpenImage.setMultiSelectionEnabled(false);
	}

	private final PropertyChangeListener facesTreePropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(final PropertyChangeEvent ev) {
			if (ev.getPropertyName().equals("SelectedItem")) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						Node selection = facesTree.getSelectedItem();
						generalizeProgressView.clear();
						NFace selected;
						if (selection == null) {
							generalizeProgressView.setVisible(false);
							faceView.setFace(null);
						} else if (selection.isGeneralizedNode()) {
							selected = (NFace) CollectionUtils.getFirst(selection.getAllGeneralized());
							generalizeProgressView.setBiometrics(selection.getItems());
							generalizeProgressView.setGeneralized(selection.getAllGeneralized());
							generalizeProgressView.setSelected(selected);
							generalizeProgressView.setVisible(true);
						} else {
							generalizeProgressView.setVisible(false);
							try {
								faceView.setFace((NFace) CollectionUtils.getFirst(selection.getItems()));
							} catch (OutOfMemoryError e) {
								e.printStackTrace();
								MessageUtils.showError(CaptureFacePage.this, "Not enough memory", "System does not have enough memory to proceed. Please try a smaller face image.");
								updateControls();
							}
						}
					}
				});
			}
		}
	};

	private void updateControls() {
		if (rbImage.isSelected()) {
			setVideoControlsVisible(false);
		} else if (rbCamera.isSelected()) {
			setVideoControlsVisible(true);
		} else if (rbVideo.isSelected()) {
			setVideoControlsVisible(true);
		} else {
			throw new AssertionError("Source radiobox must be selected.");
		}

		boolean idle = !cancelTask.isBusy();

		rbCamera.setEnabled(idle && (biometricModel.getClient().getFaceCaptureDevice() != null));
		rbImage.setEnabled(idle);
		rbVideo.setEnabled(idle);

		facesTree.setEnabled(idle);
		cbIcao.setEnabled(idle);
		cbManual.setEnabled(idle);
		cbStream.setEnabled(idle && biometricModel.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE));
		cbStream.setSelected(cbStream.isSelected() && biometricModel.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE));
		cbWithGeneralization.setEnabled(idle);

		boolean canCancel = rbCamera.isSelected() || rbVideo.isSelected();
		boolean isManual = cbManual.isSelected();

		btnCancel.setEnabled(!idle && canCancel);
		btnForceStart.setEnabled(!idle && isManual && (currentOperation != Operation.EXTARCTING));
		btnForceEnd.setEnabled(!idle && (currentOperation == Operation.EXTARCTING));
		btnCancel.setVisible(btnForceStart.isVisible() && btnForceEnd.isVisible() && canCancel);
		btnRepeat.setEnabled(false);
		btnRepeat.setVisible(cbWithGeneralization.isSelected() && btnCancel.isVisible());

		btnCapture.setEnabled(idle);
		btnFinish.setEnabled(idle);
	}

	private void setVideoControlsVisible(boolean visible) {
		panelVideoButtons.setVisible(visible);
		cbManual.setVisible(visible);
		cbStream.setVisible(visible);
	}

	private void faceCaptureDeviceChanged() {
		NCamera device = biometricModel.getClient().getFaceCaptureDevice();
		if ((device == null) || !device.isAvailable()) {
			if (rbCamera.isSelected()) {
				rbImage.setSelected(true);
			}
			rbCamera.setText("From camera (Not connected)");
		} else {
			rbCamera.setText(String.format("From camera (%s)", device.getDisplayName()));
		}
	}

	private void onFaceStatusChanged(NBiometricStatus status) {
		String msg = "";
		switch(currentOperation) {
		case DETECTING:
			msg = "Detection status: ";
			break;
		case EXTARCTING:
			msg = "Extraction status: ";
			break;
		case NONE:
			msg = "Status: ";
			break;
		}
		if (status == null) {
			setStatus(msg, Color.ORANGE);
		} else  if ((status == NBiometricStatus.OK) || (status == NBiometricStatus.NONE)) {
			setStatus(msg + status, COLOR_OK);
		} else {
			setStatus(msg + status, COLOR_ERROR);
		}
	}

	private class CheckBoxListener implements ItemListener {
		@Override
		public void itemStateChanged(ItemEvent e) {
			if (e.getSource() == cbIcao) {
				if (cbIcao.isSelected()) {
					cbStream.setSelected(true);
				}
			}
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setBiometricModel(CaptureFaceModel biometricModel) {
		if (biometricModel == null) throw new NullPointerException("biometricModel");
		this.biometricModel = biometricModel;
	}

	public void setBiometricController(CaptureBiometricController biometricController) {
		if (biometricController == null) throw new NullPointerException("biometricController");
		this.biometricController = biometricController;
	}

	@Override
	public void setStatus(String status, Color color) {
		tpStatus.setText(status);
		tpStatus.setBackground(color);
		tpStatus.setVisible(true);
	}

	@Override
	public Source getSource() {
		if (rbImage.isSelected()) {
			return Source.FILE;
		} else if (rbCamera.isSelected()) {
			return Source.DEVICE;
		} else if (rbVideo.isSelected()) {
			return Source.VIDEO;
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
	}

	@Override
	public File getFile() {
		fcOpenImage.setCurrentDirectory(lastFile);
		if (fcOpenImage.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			lastFile = fcOpenImage.getSelectedFile();
			if (!Settings.getInstance().getLastFaceFilePath().equals(lastFile.getAbsolutePath())) {
				Settings.getInstance().setLastFaceFilePath(lastFile.getAbsolutePath());
				Settings.getInstance().save();
			}
			return lastFile;
		} else {
			return null;
		}
	}

	@Override
	public EnumSet<NBiometricCaptureOption> getCaptureOptions() {
		EnumSet<NBiometricCaptureOption> options = EnumSet.noneOf(NBiometricCaptureOption.class);
		if (cbManual.isSelected()) {
			options.add(NBiometricCaptureOption.MANUAL);
		}
		if (cbStream.isSelected()) {
			options.add(NBiometricCaptureOption.STREAM);
		}
		return options;
	}

	@Override
	public void captureStarted() {
		faceView.setShowIcaoArrows(cbIcao.isSelected());
		icaoWarningsView.setVisible(cbIcao.isSelected());
		facesTree.setVisible(false);
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				faceView.setFace(CollectionUtils.getFirst(biometricModel.getLocalSubject().getFaces()));
				icaoWarningsView.setFace(CollectionUtils.getFirst(biometricModel.getLocalSubject().getFaces()));
			}
		});
	}

	private void updateTaskResult(NBiometricTask task) {
		if (!task.getSubjects().isEmpty() && !task.getSubjects().get(0).getFaces().isEmpty()) {
			List<NFace> faces = task.getSubjects().get(0).getFaces();
			facesTree.setSelectedItem(facesTree.getBiometricNode(faces.get(faces.size() - 1)));
		}
		Node selected = facesTree.getSelectedItem();
		if (selected != null && selected.isGeneralizedNode()) {
			List<NBiometric> generalized = selected.getAllGeneralized();
			generalizeProgressView.setBiometrics(selected.getItems());
			generalizeProgressView.setGeneralized(generalized);
			generalizeProgressView.setSelected(CollectionUtils.getFirst(generalized));
			generalizeProgressView.setVisible(true);
		} else if (selected != null) {
			generalizeProgressView.clear();
			generalizeProgressView.setVisible(false);
			faceView.setFace((NFace) CollectionUtils.getFirst(selected.getItems()));
		}
	}

	@Override
	public void captureCompleted(final NBiometricStatus status, final NBiometricTask task) {
		cancelTask.setBusy(false);
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				facesTree.updateTree();
				facesTree.setVisible(cbIcao.isSelected());
				tpStatus.setVisible(true);
				if (status == NBiometricStatus.OK) {
					faceView.setShowIcaoArrows(false);
					updateTaskResult(task);
					setStatus("Extraction completed successfully", COLOR_OK);
					if (isGeneralize()) {
						NSubject subject = CollectionUtils.getFirst(task.getSubjects());
						if (subject != null) {
							NBiometric generalized = CollectionUtils.getLast(subject.getFaces());
							generalizeProgressView.setGeneralized(Arrays.asList(generalized));
							generalizeProgressView.setSelected(generalized);
						}
					}
				} else {
					icaoWarningsView.setVisible(false);
					facesTree.setVisible(false);
					setStatus(String.format("Extraction failed: %s", status == NBiometricStatus.TIMEOUT ? "Liveness check failed" : status), COLOR_ERROR);
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
				panelStatus.setVisible(true);
				updateControls();
				setStatus("Extraction failed: " + msg, COLOR_ERROR);
			}
		});
	}

	@Override
	public boolean isGeneralize() {
		return cbWithGeneralization.isSelected();
	}
	
	@Override
	public boolean isIcao() {
		return cbIcao.isSelected();
	}	

	@Override
	public void setCurrentOperation(Operation currentOperation) {
		this.currentOperation = currentOperation;
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				updateControls();
			}
		});
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			icaoWarningsView.setVisible(false);
			facesTree.setVisible(false);
			facesTree.setSubject(biometricModel.getLocalSubject());
			biometricModel.getClient().addPropertyChangeListener(onBiometricClientPropertyChanged);
			biometricModel.getClient().setCurrentBiometricCompletedTimeout(5000);
			biometricModel.getClient().addCurrentBiometricCompletedListener(onCurrentBiometricCompleted);
			generalizeProgressView.setVisible(false);
			rbCamera.setSelected(true);
			faceCaptureDeviceChanged();
			tpStatus.setVisible(false);
			cbMirrorView.setSelected(SettingsManager.isMirrorFaceView());
			faceView.setMirrorHorizontally(cbMirrorView.isSelected());
			updateControls();
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
			facesTree.setVisible(false);
			facesTree.setSubject(null);
			faceView.setFace(null);
			biometricController.finish();
			biometricModel.getClient().removePropertyChangeListener(onBiometricClientPropertyChanged);
			biometricModel.getClient().removeCurrentBiometricCompletedListener(onCurrentBiometricCompleted);
			biometricModel.getClient().setCurrentBiometricCompletedTimeout(0);
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == rbCamera) {
				updateControls();
			} else if (ev.getSource() == rbImage) {
				updateControls();
			} else if (ev.getSource() == rbVideo) {
				updateControls();
			} else if (ev.getSource() == btnCapture) {
				facesTree.setSelectedItem(null);
				cancelTask.setBusy(true);
				updateControls();
				biometricController.capture();
			} else if (ev.getSource() == btnCancel) {
				biometricController.cancel();
			} else if (ev.getSource() == btnForceStart) {
				biometricController.forceStart();
			} else if (ev.getSource() == btnForceEnd) {
				biometricController.force();
			} else if (ev.getSource() == btnRepeat) {
				biometricController.repeat();
			} else if (ev.getSource() == cbMirrorView) {
				faceView.setMirrorHorizontally(cbMirrorView.isSelected());
			} else if (ev.getSource() == btnFinish) {
				getPageController().navigateToStartPage();
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	private com.neurotec.event.ChangeListener onCurrentBiometricCompleted = new com.neurotec.event.ChangeListener() {
		@Override
		public void stateChanged(com.neurotec.event.ChangeEvent e) {
			SwingUtils.runOnEDT(new Runnable() {
				@Override
				public void run() {
					NBiometricStatus status = biometricModel.getClient().getCurrentBiometric().getStatus();
					boolean allowRepeat = btnRepeat.isVisible() && status != NBiometricStatus.OK;
					if (!allowRepeat) {
						biometricModel.getClient().force();
					}
					btnRepeat.setEnabled(allowRepeat);
				}
			});
		}
	};

	private PropertyChangeListener onBiometricClientPropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent evt) {
			if ("FaceCaptureDevice".equals(evt.getPropertyName())) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						faceCaptureDeviceChanged();
						updateControls();
					}
				});
			} else if ("CurrentBiometric".equals(evt.getPropertyName())) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						NFace face = (NFace) biometricModel.getClient().getCurrentBiometric();
						if (face != null) {
							faceView.setFace(face);
							icaoWarningsView.setFace(face);
						}
						if (cbWithGeneralization.isSelected() && face != null) {
							generalizeProgressView.setSelected(face);
						}
						if (rbCamera.isSelected() || rbVideo.isSelected()) {
							setCurrentOperation(cbManual.isSelected() ? Operation.DETECTING : Operation.EXTARCTING);
							if (currentBiometric != null) currentBiometric.removePropertyChangeListener(onFacePropertyChanged);
							currentBiometric = face;
							if (currentBiometric != null) currentBiometric.addPropertyChangeListener(onFacePropertyChanged);
						}
						updateControls();
					}
				});
			}
		}
	};

	private PropertyChangeListener onFacePropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(final PropertyChangeEvent evt) {
			if ("Status".equals(evt.getPropertyName())) {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						onFaceStatusChanged(((NBiometric) evt.getSource()).getStatus());
					}
				});
			}
		}
	};

	@Override
	public void updateGeneralization(List<NBiometric> biometrics, List<NBiometric> generalized) {
		generalizeProgressView.setVisible(true);
		generalizeProgressView.setBiometrics(biometrics);
		generalizeProgressView.setGeneralized(generalized);
		generalizeProgressView.setSelected(CollectionUtils.getFirst(biometrics));
	}

	@Override
	public void nodeAdded(final NodeChangeEvent ev) {
//		if (ev.getNode() instanceof DefaultMutableTreeNode) {
//			if (currentBiometric == null) {
//				final Object nodeObject = ((DefaultMutableTreeNode) ev.getNode()).getUserObject();
//				if (nodeObject instanceof NFace) {
//					SwingUtils.runOnEDT(new Runnable() {
//						@Override
//						public void run() {
//							facesTree.setSelectedItem(facesTree.getNodeFor(nodeObject));
//						}
//					});
//				}
//			}
//		}
	}

	@Override
	public void nodeRemoved(NodeChangeEvent ev) {
		// Do nothing
	}
}
