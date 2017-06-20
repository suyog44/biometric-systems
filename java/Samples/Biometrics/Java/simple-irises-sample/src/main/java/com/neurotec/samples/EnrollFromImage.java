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

import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.SwingUtilities;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.images.NImages;
import com.neurotec.io.NFile;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;
import com.neurotec.util.concurrent.CompletionHandler;

public final class EnrollFromImage extends BasePanel implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private NSubject subject;

	private final TemplateCreationHandler templateCreationHandler = new TemplateCreationHandler();

	private NIrisView view;
	private NViewZoomSlider zoomSlider;
	private ImageThumbnailFileChooser fcOpen;
	private JFileChooser fcSaveTemplate;
	private ImageThumbnailFileChooser fcSaveFile;
	private File oldTemplateFile;

	private JButton btnOpenImage;
	private JButton btnSaveTemplate;
	private JLabel lblQuality;
	private JPanel panelActionButtons;
	private JPanel panelAction;
	private JPanel panelCenter;
	private JPanel panelSave;
	private JPanel panelSouth;
	private JScrollPane scrollPaneImage;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public EnrollFromImage() {
		super();
		licenses = new ArrayList<String>();
		licenses.add("Biometrics.IrisExtraction");
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void openImage() throws IOException {
		if (fcOpen.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			NIris iris = new NIris();
			iris.setFileName(fcOpen.getSelectedFile().getAbsolutePath());
			subject = new NSubject();
			subject.getIrises().add(iris);
			view.setIris(iris);
			lblQuality.setText("");
			updateControls();
			createTemplate();
		}
	}

	private void createTemplate() {
		if (subject != null) {
			NBiometricClient client = IrisesTools.getInstance().getClient();
			updateIrisesTools();
			client.createTemplate(subject, null, templateCreationHandler);
		}
	}

	private void saveTemplate() throws IOException {
		if (subject == null) {
			return;
		}
		if (oldTemplateFile != null) {
			fcSaveTemplate.setSelectedFile(oldTemplateFile);
		}
		if (fcSaveTemplate.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			oldTemplateFile = fcSaveTemplate.getSelectedFile();
			String fileName = fcSaveTemplate.getSelectedFile().getAbsolutePath();
			NFile.writeAllBytes(fileName, subject.getTemplateBuffer());
		}
	}

	private void updateTemplateCreationStatus(boolean created) {
		if (created) {
			lblQuality.setText(String.format("Quality: %d", (subject.getIrises().get(0).getObjects().get(0).getQuality() & 0xFF)));
		} else {
			lblQuality.setText("");
		}
		updateControls();
	}

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void initGUI() {
		setLayout(new BorderLayout());
		{
			panelLicensing = new LicensingPanel(licenses);
			add(panelLicensing, java.awt.BorderLayout.NORTH);{
			}
		}
		{
			panelCenter = new JPanel();
			panelCenter.setLayout(new GridLayout(1, 1));
			add(panelCenter, BorderLayout.CENTER);
			{
				scrollPaneImage = new JScrollPane();
				scrollPaneImage.setMinimumSize(new Dimension(100, 100));
				panelCenter.add(scrollPaneImage);
				{
					view = new NIrisView();
					view.setAutofit(true);
					scrollPaneImage.setViewportView(view);
				}
			}
		}
		{
			panelSouth = new JPanel();
			panelSouth.setLayout(new BorderLayout());
			add(panelSouth, BorderLayout.SOUTH);
			{
				panelAction = new JPanel();
				panelAction.setLayout(new BorderLayout());
				panelSouth.add(panelAction, BorderLayout.WEST);
				{
					panelActionButtons = new JPanel();
					panelActionButtons.setLayout(new FlowLayout());
					panelAction.add(panelActionButtons, BorderLayout.WEST);
					{
						btnOpenImage = new JButton();
						btnOpenImage.setText("Open image");
						btnOpenImage.addActionListener(this);
						panelActionButtons.add(btnOpenImage);
					}
					{
						zoomSlider = new NViewZoomSlider();
						zoomSlider.setView(view);
						panelActionButtons.add(zoomSlider);
					}
				}
			}
			{
				panelSave = new JPanel();
				panelSouth.add(panelSave, BorderLayout.EAST);
				{
					btnSaveTemplate = new JButton();
					btnSaveTemplate.setText("Save template");
					btnSaveTemplate.setEnabled(false);
					btnSaveTemplate.addActionListener(this);
					panelSave.add(btnSaveTemplate);
				}
				{
					lblQuality = new JLabel();
					panelSave.add(lblQuality);
				}
			}
		}

		fcOpen = new ImageThumbnailFileChooser();
		fcOpen.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcOpen.setFileFilter(new Utils.ImageFileFilter(NImages.getOpenFileFilter()));
		fcSaveTemplate = new JFileChooser();
		fcSaveFile = new ImageThumbnailFileChooser();
		fcSaveFile.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcSaveFile.setFileFilter(new Utils.ImageFileFilter(NImages.getSaveFileFilter()));
	}

	@Override
	protected void setDefaultValues() {
		// No default values.
	}

	@Override
	protected void updateControls() {
		btnSaveTemplate.setEnabled((subject != null) && (subject.getStatus() == NBiometricStatus.OK));
	}

	@Override
	protected void updateIrisesTools() {
		IrisesTools.getInstance().getClient().reset();
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == btnOpenImage) {
				openImage();
			} else if (ev.getSource() == btnSaveTemplate) {
				saveTemplate();
			}
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, e, "Error", JOptionPane.ERROR_MESSAGE);
			updateControls();
		}
	}

	// ===========================================================
	// Inner classes
	// ===========================================================

	private class TemplateCreationHandler implements CompletionHandler<NBiometricStatus, Object> {

		@Override
		public void completed(final NBiometricStatus result, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					if (result == NBiometricStatus.OK) {
						updateTemplateCreationStatus(true);
					} else if (result == NBiometricStatus.BAD_OBJECT) {
						JOptionPane.showMessageDialog(EnrollFromImage.this, "Iris image quality is too low.");
						updateTemplateCreationStatus(false);
					} else {
						JOptionPane.showMessageDialog(EnrollFromImage.this, result);
						updateTemplateCreationStatus(false);
					}
				}

			});
		}

		@Override
		public void failed(final Throwable th, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					th.printStackTrace();
					showErrorDialog(th);
					updateTemplateCreationStatus(false);
				}

			});
		}

	}

}
