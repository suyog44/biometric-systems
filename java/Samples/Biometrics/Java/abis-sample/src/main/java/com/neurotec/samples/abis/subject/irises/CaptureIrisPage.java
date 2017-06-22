package com.neurotec.samples.abis.subject.irises;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.SystemColor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.File;
import java.util.EnumSet;

import javax.swing.BorderFactory;
import javax.swing.ButtonGroup;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NEPosition;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject.IrisCollection;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.settings.Settings;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.LockingTask;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

public final class CaptureIrisPage extends Page implements CaptureIrisView, PageNavigationListener, NCollectionChangeListener, PropertyChangeListener, ActionListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final Color COLOR_OK = new Color(0, 128, 0);
	private static final Color COLOR_ERROR = new Color(255, 0, 0);

	// ===========================================================
	// Private fields
	// ===========================================================

	private CaptureIrisModel biometricModel;
	private CaptureBiometricController biometricController;
	private NIris iris;

	private ImageThumbnailFileChooser fcOpenImage;
	private File lastFile;
	private NIrisView irisView;

	private JButton btnCancel;
	private JButton btnCapture;
	private JButton btnFinish;
	private JButton btnForce;
	private ButtonGroup btnGroupIrisSource;
	private JCheckBox cbAutomatic;
	private JComboBox comboBoxPosition;
	private JLabel lblPosition;
	private JPanel panelBottom;
	private JPanel panelButtons;
	private JPanel panelCaptureControls;
	private JPanel panelStatus;
	private JPanel panelTop;
	private JPanel panelZoom;
	private NViewZoomSlider horizontalZoomSlider;
	private JRadioButton rbFile;
	private JRadioButton rbScanner;
	private JScrollPane spIris;
	private JTextPane tpStatus;

	private final LockingTask cancelTask = new LockingTask() {

		@Override
		public void performTask() {
			biometricController.cancel();
		}
	};

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureIrisPage(PageNavigationController pageController) {
		super("New...", pageController);
		initGUI();
		lastFile = new File(Settings.getInstance().getLastIrisFilePath());
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		GridBagConstraints gridBagConstraints;

		btnGroupIrisSource = new ButtonGroup();
		panelTop = new JPanel();
		panelCaptureControls = new JPanel();
		rbScanner = new JRadioButton();
		rbFile = new JRadioButton();
		lblPosition = new JLabel();
		comboBoxPosition = new JComboBox();
		btnCapture = new JButton();
		cbAutomatic = new JCheckBox();
		spIris = new JScrollPane();
		panelBottom = new JPanel();
		panelStatus = new JPanel();
		tpStatus = new JTextPane();
		StyledDocument doc = tpStatus.getStyledDocument();
		SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
		StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
		StyleConstants.setLeftIndent(styleAttributes, 3);
		StyleConstants.setRightIndent(styleAttributes, 3);
		StyleConstants.setFontSize(styleAttributes, 16);
		StyleConstants.setForeground(styleAttributes, SystemColor.menu);
		doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);

		panelButtons = new JPanel();
		btnForce = new JButton();
		btnCancel = new JButton();
		btnFinish = new JButton();

		setLayout(new BorderLayout());

		panelTop.setLayout(new FlowLayout(FlowLayout.LEFT));

		panelCaptureControls.setBorder(BorderFactory.createTitledBorder("Capture options"));
		GridBagLayout panelCaptureControlsLayout = new GridBagLayout();
		panelCaptureControlsLayout.columnWidths = new int[] {0, 5, 0, 5, 0};
		panelCaptureControlsLayout.rowHeights = new int[] {0, 5, 0};
		panelCaptureControls.setLayout(panelCaptureControlsLayout);

		btnGroupIrisSource.add(rbScanner);
		rbScanner.setText("Scanner");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelCaptureControls.add(rbScanner, gridBagConstraints);

		btnGroupIrisSource.add(rbFile);
		rbFile.setText("File");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelCaptureControls.add(rbFile, gridBagConstraints);

		lblPosition.setText("Position:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		panelCaptureControls.add(lblPosition, gridBagConstraints);

		comboBoxPosition.setPreferredSize(new Dimension(100, 20));
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
		panelCaptureControls.add(comboBoxPosition, gridBagConstraints);

		btnCapture.setText("Capture");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 4;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		panelCaptureControls.add(btnCapture, gridBagConstraints);

		cbAutomatic.setSelected(true);
		cbAutomatic.setText("Capture automatically");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 4;
		gridBagConstraints.gridy = 0;
		panelCaptureControls.add(cbAutomatic, gridBagConstraints);

		panelTop.add(panelCaptureControls);

		add(panelTop, BorderLayout.NORTH);
		add(spIris, BorderLayout.CENTER);

		panelBottom.setLayout(new BorderLayout());

		panelStatus.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
		panelStatus.setLayout(new GridLayout(1, 0));

		tpStatus.setEditable(false);
		panelStatus.add(tpStatus);

		panelBottom.add(panelStatus, BorderLayout.NORTH);
		panelZoom = new JPanel();
		panelZoom.setLayout(new FlowLayout(FlowLayout.LEFT));

		panelBottom.add(panelZoom, BorderLayout.WEST);

		panelButtons.setLayout(new FlowLayout(FlowLayout.RIGHT));

		btnForce.setText("Force");
		panelButtons.add(btnForce);

		btnCancel.setText("Cancel");
		panelButtons.add(btnCancel);

		btnFinish.setText("Finish");
		panelButtons.add(btnFinish);

		panelBottom.add(panelButtons, BorderLayout.EAST);

		add(panelBottom, BorderLayout.SOUTH);

		irisView = new NIrisView();
		irisView.setAutofit(true);
		spIris.setViewportView(irisView);
		fcOpenImage = new ImageThumbnailFileChooser();
		fcOpenImage.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcOpenImage.setMultiSelectionEnabled(false);

		horizontalZoomSlider = new NViewZoomSlider();
		horizontalZoomSlider.setView(irisView);
		panelZoom.add(horizontalZoomSlider);

		irisView.addPropertyChangeListener(this);

		btnCapture.addActionListener(this);
		btnCancel.addActionListener(this);
		btnForce.addActionListener(this);
		btnFinish.addActionListener(this);
		rbFile.addActionListener(this);
		rbScanner.addActionListener(this);
	}

	private void updateControls() {
		if (rbFile.isSelected()) {
			Object selected = comboBoxPosition.getSelectedItem();
			comboBoxPosition.setModel(new DefaultComboBoxModel(new NEPosition[] {NEPosition.LEFT, NEPosition.RIGHT, NEPosition.UNKNOWN}));
			comboBoxPosition.setSelectedItem(selected);
			if (comboBoxPosition.getSelectedIndex() == -1) {
				comboBoxPosition.setSelectedIndex(0);
			}
			btnCapture.setText("Open image");
			btnForce.setVisible(false);
			cbAutomatic.setVisible(false);
		} else if (rbScanner.isSelected()) {
			Object selected = comboBoxPosition.getSelectedItem();
			comboBoxPosition.setModel(new DefaultComboBoxModel(biometricModel.getClient().getIrisScanner().getSupportedPositions()));
			comboBoxPosition.setSelectedItem(selected);
			if (comboBoxPosition.getSelectedIndex() == -1) {
				comboBoxPosition.setSelectedIndex(0);
			}
			btnCapture.setText("Capture");
			btnForce.setVisible(true);
			cbAutomatic.setVisible(true);
		} else {
			throw new AssertionError("Source radiobox must be selected.");
		}

		boolean idle = !cancelTask.isBusy();
		rbFile.setEnabled(idle);
		rbScanner.setEnabled(idle && (biometricModel.getClient().getIrisScanner() != null));
		comboBoxPosition.setEnabled(idle);
		btnCapture.setEnabled(idle);
		btnCancel.setEnabled(!idle);
		btnFinish.setEnabled(idle);
		btnForce.setEnabled(!idle && !cbAutomatic.isSelected());
		cbAutomatic.setEnabled(idle);
	}

	private void setStatus(String msg, Color color) {
		tpStatus.setText(msg);
		tpStatus.setBackground(color);
	}

	private void irisScannerChanged() {
		NIrisScanner device = biometricModel.getClient().getIrisScanner();
		if ((device == null) || !device.isAvailable()) {
			if (rbScanner.isSelected()) {
				rbFile.setSelected(true);
			}
			rbScanner.setText("Scanner (Not connected)");
		} else {
			rbScanner.setText(String.format("Scanner (%s)", device.getDisplayName()));
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setBiometricModel(CaptureIrisModel biometricModel) {
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
		} else {
			throw new AssertionError("Source radiobox must be selected.");
		}
	}

	@Override
	public NEPosition getPosition() {
		return (NEPosition) comboBoxPosition.getSelectedItem();
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
			if (!Settings.getInstance().getLastIrisFilePath().equals(lastFile.getAbsolutePath())) {
				Settings.getInstance().setLastIrisFilePath(lastFile.getAbsolutePath());
				Settings.getInstance().save();
			}
			return lastFile;
		} else {
			return null;
		}
	}

	@Override
	public void captureCompleted(final NBiometricStatus status) {
		cancelTask.setBusy(false);
		SwingUtils.runOnEDT(new Runnable() {

			@Override
			public void run() {
				tpStatus.setVisible(true);
				if (status == NBiometricStatus.OK) {
					setStatus("Extraction completed successfully", COLOR_OK);
				} else {
					setStatus("Extraction failed: " + status, COLOR_ERROR);
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
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			biometricModel.getClient().addPropertyChangeListener(this);
			biometricModel.getLocalSubject().getIrises().addCollectionChangeListener(this);

			rbScanner.setSelected(true);
			irisScannerChanged();
			tpStatus.setVisible(false);
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
			biometricController.finish();
			biometricModel.getClient().removePropertyChangeListener(this);
			biometricModel.getLocalSubject().getIrises().removeCollectionChangeListener(this);
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource().equals(rbScanner)) {
				updateControls();
			} else if (ev.getSource().equals(rbFile)) {
				updateControls();
			} else if (ev.getSource().equals(btnCapture)) {
				cancelTask.setBusy(true);
				updateControls();
				biometricController.capture();
			} else if (ev.getSource().equals(btnCancel)) {
				biometricController.cancel();
			} else if (btnForce.equals(ev.getSource())) {
				biometricController.force();
			} else if (ev.getSource().equals(btnFinish)) {
				getPageController().navigateToStartPage();
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void collectionChanged(final NCollectionChangeEvent ev) {
		final IrisCollection irises = biometricModel.getLocalSubject().getIrises();
		if (ev.getSource().equals(irises)) {
			if (iris != null) {
				iris.removePropertyChangeListener(this);
			}
			SwingUtils.runOnEDT(new Runnable() {

				@Override
				public void run() {
					if (irises.isEmpty()) {
						irisView.setIris(null);
						iris = null;
					} else {
						iris = irises.get(irises.size() - 1);
						irisView.setIris(iris);
						iris.addPropertyChangeListener(CaptureIrisPage.this);
					}
				}
			});
		}
	}

	@Override
	public void propertyChange(PropertyChangeEvent ev) {
		if (ev.getSource().equals(biometricModel.getClient()) && "IrisScanner".equals(ev.getPropertyName())) {
			SwingUtils.runOnEDT(new Runnable() {
				@Override
				public void run() {
					irisScannerChanged();
					updateControls();
				}
			});
		} else if (ev.getSource().equals(iris)) {
			if ("Status".equals(ev.getPropertyName())) {
				final NBiometricStatus status = iris.getStatus();
				SwingUtils.runOnEDT(new Runnable() {

					@Override
					public void run() {
						tpStatus.setVisible(true);
						if ((status == NBiometricStatus.OK) || (status == NBiometricStatus.NONE)) {
							setStatus("Status: " + status, COLOR_OK);
						} else {
							setStatus("Status: " + status, COLOR_ERROR);
						}
					}
				});
			}
		}
	}

}
