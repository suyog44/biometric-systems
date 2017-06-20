package com.neurotec.samples;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.EnumSet;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JList;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.ListSelectionModel;
import javax.swing.SwingUtilities;
import javax.swing.border.BevelBorder;
import javax.swing.border.LineBorder;
import javax.swing.border.SoftBevelBorder;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NEPosition;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.images.NImages;
import com.neurotec.io.NFile;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;
import com.neurotec.util.concurrent.CompletionHandler;

public final class EnrollFromScanner extends BasePanel implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private NSubject subject;
	private final NDeviceManager deviceManager;
	private boolean scanning;

	private final CaptureCompletionHandler captureCompletionHandler = new CaptureCompletionHandler();

	private NIrisView view;
	private NViewZoomSlider zoomSlider;
	private JFileChooser fcImage;
	private JFileChooser fcTemplateF;
	private File oldImageFile;
	private File oldTemplateFile;

	private JButton btnCancel;
	private JButton btnForce;
	private JButton btnRefresh;
	private JButton btnSaveImage;
	private JButton btnSaveTemplate;
	private JButton btnScan;
	private JCheckBox cbAutomatic;
	private JLabel lblInfo;
	private JPanel panelButtons;
	private JPanel panelInfo;
	private JPanel panelMain;
	private JPanel panelPosition;
	private JPanel panelPositionOuter;
	private JPanel panelSave;
	private JPanel panelScanners;
	private JPanel panelSouth;
	private ButtonGroup rbGroupPosition;
	private JRadioButton rbLeft;
	private JRadioButton rbRight;
	private JList scannerList;
	private JScrollPane scrollPane;
	private JScrollPane scrollPaneList;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public EnrollFromScanner() {
		super();
		licenses = new ArrayList<String>();
		licenses.add("Biometrics.IrisExtraction");
		licenses.add("Devices.IrisScanners");

		IrisesTools.getInstance().getClient().setUseDeviceManager(true);
		deviceManager = IrisesTools.getInstance().getClient().getDeviceManager();
		deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.IRIS_SCANNER));
		deviceManager.initialize();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void startCapturing() {
		lblInfo.setText("");
		if (IrisesTools.getInstance().getClient().getIrisScanner() == null) {
			JOptionPane.showMessageDialog(this, "Please select scanner from the list.", "No scanner selected", JOptionPane.PLAIN_MESSAGE);
			return;
		}

		// Create iris.
		NIris iris = new NIris();
		if (rbRight.isSelected()) {
			iris.setPosition(NEPosition.RIGHT);
		} else if (rbLeft.isSelected()) {
			iris.setPosition(NEPosition.LEFT);
		} else {
			iris.setPosition(NEPosition.UNKNOWN);
		}

		// Set Manual capturing mode if automatic isn't selected.
		if (!cbAutomatic.isSelected()) {
			iris.setCaptureOptions(EnumSet.of(NBiometricCaptureOption.MANUAL));
		}

		// Add iris to subject and iris view.
		subject = new NSubject();
		subject.getIrises().add(iris);
		view.setIris(iris);

		// Begin capturing.
		NBiometricTask task = IrisesTools.getInstance().getClient().createTask(EnumSet.of(NBiometricOperation.CAPTURE, NBiometricOperation.CREATE_TEMPLATE), subject);
		scanning = true;
		IrisesTools.getInstance().getClient().performTask(task, null, captureCompletionHandler);
		updateControls();
	}

	private void saveTemplate() throws IOException {
		if (subject != null) {
			if (oldTemplateFile != null) {
				fcTemplateF.setSelectedFile(oldTemplateFile);
			}
			if (fcTemplateF.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
				oldTemplateFile = fcTemplateF.getSelectedFile();
				NFile.writeAllBytes(fcTemplateF.getSelectedFile().getAbsolutePath(), subject.getTemplateBuffer());
			}
		}
	}

	private void saveImage() throws IOException {
		if (subject != null) {
			if (oldImageFile != null) {
				fcImage.setSelectedFile(oldImageFile);
			}
			if (fcImage.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
				oldImageFile = fcImage.getSelectedFile();
				subject.getIrises().get(0).getImage().save(fcImage.getSelectedFile().getAbsolutePath());
			}
		}
	}

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void initGUI() {
		setLayout(new BorderLayout());

		{
			panelLicensing = new LicensingPanel(licenses);
			add(panelLicensing, java.awt.BorderLayout.NORTH);
		}
		{
			panelMain = new JPanel();
			panelMain.setLayout(new BorderLayout());
			add(panelMain, BorderLayout.CENTER);
			{
				panelScanners = new JPanel();
				panelScanners.setBorder(BorderFactory.createTitledBorder("Scanners list"));
				panelScanners.setLayout(new BorderLayout());
				panelMain.add(panelScanners, BorderLayout.NORTH);
				{
					scrollPaneList = new JScrollPane();
					scrollPaneList.setPreferredSize(new Dimension(0, 90));
					panelScanners.add(scrollPaneList, BorderLayout.CENTER);
					{
						scannerList = new JList();
						scannerList.setModel(new DefaultListModel());
						scannerList.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
						scannerList.setBorder(LineBorder.createBlackLineBorder());
						scannerList.addListSelectionListener(new ScannerSelectionListener());
						scrollPaneList.setViewportView(scannerList);
					}
				}
				{
					panelButtons = new JPanel();
					panelButtons.setLayout(new FlowLayout(FlowLayout.LEADING));
					panelScanners.add(panelButtons, BorderLayout.SOUTH);
					{
						btnRefresh = new JButton();
						btnRefresh.setText("Refresh list");
						btnRefresh.addActionListener(this);
						panelButtons.add(btnRefresh);
					}
					{
						btnScan = new JButton();
						btnScan.setText("Scan");
						btnScan.addActionListener(this);
						panelButtons.add(btnScan);
					}
					{
						btnCancel = new JButton();
						btnCancel.setText("Cancel");
						btnCancel.setEnabled(false);
						btnCancel.addActionListener(this);
						panelButtons.add(btnCancel);
					}
					{
						btnForce = new JButton();
						btnForce.setText("Force");
						btnForce.addActionListener(this);
						panelButtons.add(btnForce);
					}
					{
						cbAutomatic = new JCheckBox();
						cbAutomatic.setSelected(true);
						cbAutomatic.setText("Scan automatically");
						panelButtons.add(cbAutomatic);
					}
				}
				{
					panelPositionOuter = new JPanel();
					panelScanners.add(panelPositionOuter, BorderLayout.EAST);
					{
						panelPosition = new JPanel();
						panelPosition.setLayout(new BoxLayout(panelPosition, BoxLayout.Y_AXIS));
						panelPositionOuter.add(panelPosition);
						{
							rbLeft = new JRadioButton();
							rbLeft.setSelected(true);
							rbLeft.setText("Left iris");
							rbLeft.addActionListener(this);
							rbGroupPosition = new ButtonGroup();
							rbGroupPosition.add(rbLeft);
							panelPosition.add(rbLeft);
						}
						{
							rbRight = new JRadioButton();
							rbRight.setText("Right iris");
							rbRight.addActionListener(this);
							rbGroupPosition.add(rbRight);
							panelPosition.add(rbRight);
						}
					}
				}
			}
			{
				scrollPane = new JScrollPane();
				panelMain.add(scrollPane, BorderLayout.CENTER);
				{
					view = new NIrisView();
					view.setAutofit(true);
					scrollPane.setViewportView(view);
				}
			}
			{
				panelSouth = new JPanel();
				panelSouth.setLayout(new BorderLayout());
				panelMain.add(panelSouth, BorderLayout.SOUTH);
				{
					panelInfo = new JPanel();
					panelInfo.setBorder(new SoftBevelBorder(BevelBorder.LOWERED));
					panelInfo.setLayout(new GridLayout(1, 1));
					panelSouth.add(panelInfo, BorderLayout.NORTH);
					{
						lblInfo = new JLabel();
						lblInfo.setText(" ");
						panelInfo.add(lblInfo);
					}
				}
				{
					panelSave = new JPanel();
					panelSave.setLayout(new FlowLayout(FlowLayout.LEADING));
					panelSouth.add(panelSave, BorderLayout.SOUTH);
					{
						btnSaveImage = new JButton();
						btnSaveImage.setText("Save image");
						btnSaveImage.setEnabled(false);
						btnSaveImage.addActionListener(this);
						panelSave.add(btnSaveImage);
					}
					{
						btnSaveTemplate = new JButton();
						btnSaveTemplate.setText("Save template");
						btnSaveTemplate.setEnabled(false);
						btnSaveTemplate.addActionListener(this);
						panelSave.add(btnSaveTemplate);
					}
					{
						zoomSlider = new NViewZoomSlider();
						zoomSlider.setView(view);
						panelSave.add(zoomSlider);
					}
				}
			}
		}
		fcImage = new JFileChooser();
		fcImage.setFileFilter(new Utils.ImageFileFilter(NImages.getSaveFileFilter()));
		fcTemplateF = new JFileChooser();
	}

	@Override
	protected void setDefaultValues() {
		// No default values.
	}

	@Override
	protected void updateControls() {
		btnScan.setEnabled(!scanning);
		btnCancel.setEnabled(scanning);
		btnForce.setEnabled(scanning && !cbAutomatic.isSelected());
		btnRefresh.setEnabled(!scanning);
		btnSaveTemplate.setEnabled(!scanning && (subject != null) && (subject.getStatus() == NBiometricStatus.OK));
		btnSaveImage.setEnabled(!scanning && (subject != null) && (subject.getStatus() == NBiometricStatus.OK));
		cbAutomatic.setEnabled(!scanning);
		rbLeft.setEnabled(!scanning);
		rbRight.setEnabled(!scanning);
	}

	@Override
	protected void updateIrisesTools() {
		IrisesTools.getInstance().getClient().reset();
		IrisesTools.getInstance().getClient().setUseDeviceManager(true);
	}

	// ===========================================================
	// Package private methods
	// ===========================================================

	void updateStatus(String status) {
		lblInfo.setText(status);
	}

	NSubject getSubject() {
		return subject;
	}

	NIrisScanner getSelectedScanner() {
		return (NIrisScanner) scannerList.getSelectedValue();
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void updateScannerList() {
		DefaultListModel model = (DefaultListModel) scannerList.getModel();
		model.clear();
		for (NDevice device : deviceManager.getDevices()) {
			model.addElement(device);
		}
		NIrisScanner scanner = IrisesTools.getInstance().getClient().getIrisScanner();
		if ((scanner == null) && (model.getSize() > 0)) {
			scannerList.setSelectedIndex(0);
		} else if (scanner != null) {
			scannerList.setSelectedValue(scanner, true);
		}
	}

	public void cancelCapturing() {
		IrisesTools.getInstance().getClient().cancel();
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == btnRefresh) {
				updateScannerList();
			} else if (ev.getSource() == btnScan) {
				startCapturing();
			} else if (ev.getSource() == btnCancel) {
				cancelCapturing();
			} else if (ev.getSource() == btnForce) {
				IrisesTools.getInstance().getClient().force();
			} else if (ev.getSource() == btnSaveImage) {
				saveImage();
			} else if (ev.getSource() == btnSaveTemplate) {
				saveTemplate();
			}
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, e.toString(), "Error", JOptionPane.ERROR_MESSAGE);
		}
	}

	// ===========================================================
	// Inner classes
	// ===========================================================


	private class CaptureCompletionHandler implements CompletionHandler<NBiometricTask, Object> {

		@Override
		public void completed(final NBiometricTask result, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					scanning = false;
					if (result.getStatus() == NBiometricStatus.OK) {
						updateStatus("Quality: " + getSubject().getIrises().get(0).getObjects().get(0).getQuality());
					} else {
						updateStatus(result.getStatus().toString());
					}
					updateControls();
				}

			});
		}

		@Override
		public void failed(final Throwable th, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					th.printStackTrace();
					scanning = false;
					showErrorDialog(th);
					updateControls();
				}

			});
		}

	}

	private class ScannerSelectionListener implements ListSelectionListener {

		@Override
		public void valueChanged(ListSelectionEvent e) {
			IrisesTools.getInstance().getClient().setIrisScanner(getSelectedScanner());
		}

	}

}
