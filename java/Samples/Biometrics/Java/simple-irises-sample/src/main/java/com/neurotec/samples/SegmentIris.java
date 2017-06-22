package com.neurotec.samples;

import java.awt.BorderLayout;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.EnumSet;

import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextField;
import javax.swing.SwingUtilities;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NEAttributes;
import com.neurotec.biometrics.NEImageType;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.images.NImage;
import com.neurotec.images.NImages;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;
import com.neurotec.util.concurrent.CompletionHandler;

public final class SegmentIris extends BasePanel implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private NIris iris;
	private NIris segmentedIris;
	private NIrisView imageView;
	private NIrisView resultView;
	private NViewZoomSlider imageZoomSlider;
	private NViewZoomSlider resultZoomSlider;

	private final SegmentationHandler segmentationHandler = new SegmentationHandler();

	private ImageThumbnailFileChooser fcOpen;
	private ImageThumbnailFileChooser fcSave;
	private File oldImageFile;

	private JButton btnOpen;
	private JButton btnSave;
	private JButton btnSegment;
	private JPanel detailsPanel;
	private JPanel imagePanel;
	private JLabel jLabel1;
	private JLabel jLabel10;
	private JLabel jLabel11;
	private JLabel jLabel12;
	private JLabel jLabel13;
	private JLabel jLabel3;
	private JLabel jLabel4;
	private JLabel jLabel5;
	private JLabel jLabel6;
	private JLabel jLabel7;
	private JLabel jLabel9;
	private JPanel mainPanel;
	private JPanel openButtonPanel;
	private JPanel resultsPanel;
	private JPanel savePanel;
	private JPanel saveButtonPanel;
	private JScrollPane scrollPaneImage;
	private JScrollPane scrollPaneSegmented;
	private JPanel segmentPanel;
	private JPanel segmentButtonPanel;
	private JTextField tfGrayScaleUtilisation;
	private JTextField tfInterlace;
	private JTextField tfPupilBoundaryCircularity;
	private JTextField tfIrisPupilConcentricity;
	private JTextField tfIrisPupilContrast;
	private JTextField tfIrisScleraContrast;
	private JTextField tfMarginAdequacy;
	private JTextField tfPupilToIrisRatio;
	private JTextField tfQuality;
	private JTextField tfSharpness;
	private JTextField tfUsableIrisArea;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SegmentIris() {
		super();
		licenses = new ArrayList<String>();
		licenses.add("Biometrics.IrisExtraction");
		licenses.add("Biometrics.IrisSegmentation");
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void openImage() throws IOException {
		tfGrayScaleUtilisation.setText("");
		tfInterlace.setText("");
		tfPupilBoundaryCircularity.setText("");
		tfIrisPupilContrast.setText("");
		tfIrisScleraContrast.setText("");
		tfIrisPupilConcentricity.setText("");
		tfMarginAdequacy.setText("");
		tfPupilToIrisRatio.setText("");
		tfQuality.setText("");
		tfSharpness.setText("");
		tfUsableIrisArea.setText("");

		fcOpen.setSelectedFile(oldImageFile);
		if (fcOpen.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			oldImageFile = fcOpen.getSelectedFile();
			String fileName = oldImageFile.getAbsolutePath();

			// Read image from file.
			iris = new NIris();
			iris.setImage(NImage.fromFile(fileName));
			imageView.setIris(iris);

			segmentedIris = null;
			resultView.setIris(null);
		}
		updateControls();
	}

	private void segmentIris() {
		if (iris != null) {
			iris.setImageType(NEImageType.CROPPED_AND_MASKED);
			NSubject subject = new NSubject();
			subject.getIrises().add(iris);

			NBiometricTask segmentTask = IrisesTools.getInstance().getClient().createTask(EnumSet.of(NBiometricOperation.SEGMENT), subject);
			IrisesTools.getInstance().getClient().performTask(segmentTask, null, segmentationHandler);
			disableControls();
		}
	}

	private void saveImage() throws IOException {
		if (oldImageFile != null) {
			fcSave.setSelectedFile(oldImageFile);
		}
		if (fcSave.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			oldImageFile = fcSave.getSelectedFile();
			String fileName = fcSave.getSelectedFile().getAbsolutePath();
			segmentedIris.getImage().save(fileName);
		}
	}

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void initGUI() {
		GridBagConstraints gridBagConstraints;
		setLayout(new BorderLayout());
		{
			panelLicensing = new LicensingPanel(licenses);
			add(panelLicensing, java.awt.BorderLayout.NORTH);
		}
		{
			mainPanel = new JPanel();
			mainPanel.setLayout(new GridLayout(2, 1));
			add(mainPanel, BorderLayout.CENTER);
			{
				imagePanel = new JPanel();
				imagePanel.setBorder(BorderFactory.createTitledBorder("Image"));
				imagePanel.setLayout(new BorderLayout());
				mainPanel.add(imagePanel);
				{
					openButtonPanel = new JPanel();
					openButtonPanel.setLayout(new FlowLayout(FlowLayout.LEADING));
					imagePanel.add(openButtonPanel, BorderLayout.NORTH);
					{
						btnOpen = new JButton();
						btnOpen.setText("Open image");
						btnOpen.addActionListener(this);
						openButtonPanel.add(btnOpen);
					}
				}
				{
					scrollPaneImage = new JScrollPane();
					imagePanel.add(scrollPaneImage, BorderLayout.CENTER);
					{
						imageView = new NIrisView();
						imageView.setAutofit(true);
						scrollPaneImage.setViewportView(imageView);
					}
				}
				{
					segmentPanel = new JPanel();
					segmentPanel.setLayout(new BorderLayout());
					imagePanel.add(segmentPanel, BorderLayout.SOUTH);
					{
						segmentButtonPanel = new JPanel();
						segmentPanel.add(segmentButtonPanel, BorderLayout.WEST);
						{
							btnSegment = new JButton();
							btnSegment.setText("Segment iris");
							btnSegment.setEnabled(false);
							btnSegment.addActionListener(this);
							segmentButtonPanel.add(btnSegment);
						}
					}
					{
						imageZoomSlider = new NViewZoomSlider();
						imageZoomSlider.setView(imageView);
						segmentPanel.add(imageZoomSlider, BorderLayout.EAST);
					}
				}
			}
			{
				resultsPanel = new JPanel();
				resultsPanel.setBorder(BorderFactory.createTitledBorder("Results"));
				resultsPanel.setLayout(new BorderLayout());
				mainPanel.add(resultsPanel);
				{
					detailsPanel = new JPanel();
					GridBagLayout detailsPanelLayout = new GridBagLayout();
					detailsPanelLayout.columnWidths = new int[] {0, 5, 0, 5, 0, 5, 0, 5, 0, 5, 0};
					detailsPanelLayout.rowHeights = new int[] {0, 5, 0, 5, 0, 5, 0, 5, 0};
					detailsPanel.setLayout(detailsPanelLayout);
					resultsPanel.add(detailsPanel, BorderLayout.NORTH);
					{
						jLabel1 = new JLabel();
						jLabel1.setText("Quality:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 0;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel1, gridBagConstraints);
					}
					{
						jLabel3 = new JLabel();
						jLabel3.setText("Pupil to iris ratio:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 0;
						gridBagConstraints.gridy = 2;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel3, gridBagConstraints);
					}
					{
						jLabel4 = new JLabel();
						jLabel4.setText("Usable iris area:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 0;
						gridBagConstraints.gridy = 4;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel4, gridBagConstraints);
					}
					{
						jLabel5 = new JLabel();
						jLabel5.setText("Grayscale utilisation:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 0;
						gridBagConstraints.gridy = 6;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel5, gridBagConstraints);
					}
					{
						tfQuality = new JTextField();
						tfQuality.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 2;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						gridBagConstraints.weightx = 0.1;
						detailsPanel.add(tfQuality, gridBagConstraints);
					}
					{
						tfPupilToIrisRatio = new JTextField();
						tfPupilToIrisRatio.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 2;
						gridBagConstraints.gridy = 2;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfPupilToIrisRatio, gridBagConstraints);
					}
					{
						tfUsableIrisArea = new JTextField();
						tfUsableIrisArea.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 2;
						gridBagConstraints.gridy = 4;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfUsableIrisArea, gridBagConstraints);
					}
					{
						tfGrayScaleUtilisation = new JTextField();
						tfGrayScaleUtilisation.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 2;
						gridBagConstraints.gridy = 6;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfGrayScaleUtilisation, gridBagConstraints);
					}
					{
						jLabel6 = new JLabel();
						jLabel6.setText("Iris sclera contrast:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 4;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel6, gridBagConstraints);
					}
					{
						jLabel7 = new JLabel();
						jLabel7.setText("Iris pupil contrast:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 4;
						gridBagConstraints.gridy = 2;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel7, gridBagConstraints);
					}
					{
						jLabel9 = new JLabel();
						jLabel9.setText("Iris boundary circularity:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 4;
						gridBagConstraints.gridy = 6;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel9, gridBagConstraints);
					}
					{
						tfIrisScleraContrast = new JTextField();
						tfIrisScleraContrast.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 6;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						gridBagConstraints.weightx = 0.1;
						detailsPanel.add(tfIrisScleraContrast, gridBagConstraints);
					}
					{
						tfIrisPupilContrast = new JTextField();
						tfIrisPupilContrast.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 6;
						gridBagConstraints.gridy = 2;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfIrisPupilContrast, gridBagConstraints);
					}
					{
						tfPupilBoundaryCircularity = new JTextField();
						tfPupilBoundaryCircularity.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 6;
						gridBagConstraints.gridy = 6;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfPupilBoundaryCircularity, gridBagConstraints);
					}
					{
						jLabel10 = new JLabel();
						jLabel10.setText("Sharpness:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 8;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel10, gridBagConstraints);
					}
					{
						jLabel11 = new JLabel();
						jLabel11.setText("Iris pupil concentricity:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 8;
						gridBagConstraints.gridy = 2;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel11, gridBagConstraints);
					}
					{
						jLabel12 = new JLabel();
						jLabel12.setText("Interlace:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 8;
						gridBagConstraints.gridy = 4;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel12, gridBagConstraints);
					}
					{
						jLabel13 = new JLabel();
						jLabel13.setText("Margin adequacy:");
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 4;
						gridBagConstraints.gridy = 4;
						gridBagConstraints.anchor = GridBagConstraints.LINE_END;
						detailsPanel.add(jLabel13, gridBagConstraints);
					}
					{
						tfSharpness = new JTextField();
						tfSharpness.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 10;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						gridBagConstraints.weightx = 0.1;
						detailsPanel.add(tfSharpness, gridBagConstraints);
					}
					{
						tfIrisPupilConcentricity = new JTextField();
						tfIrisPupilConcentricity.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 10;
						gridBagConstraints.gridy = 2;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfIrisPupilConcentricity, gridBagConstraints);
					}
					{
						tfInterlace = new JTextField();
						tfInterlace.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 10;
						gridBagConstraints.gridy = 4;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfInterlace, gridBagConstraints);
					}
					{
						tfMarginAdequacy = new JTextField();
						tfMarginAdequacy.setEnabled(false);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 6;
						gridBagConstraints.gridy = 4;
						gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
						detailsPanel.add(tfMarginAdequacy, gridBagConstraints);
					}
				}
				{
					scrollPaneSegmented = new JScrollPane();
					resultsPanel.add(scrollPaneSegmented, BorderLayout.CENTER);
					{
						resultView = new NIrisView();
						resultView.setAutofit(true);
						scrollPaneSegmented.setViewportView(resultView);
					}
				}
			}
		}
		{
			savePanel = new JPanel();
			savePanel.setLayout(new BorderLayout());
			add(savePanel, BorderLayout.SOUTH);
			{
				saveButtonPanel = new JPanel();
				savePanel.add(saveButtonPanel, BorderLayout.WEST);
				{
					btnSave = new JButton();
					btnSave.setText("Save image");
					btnSave.setEnabled(false);
					btnSave.addActionListener(this);
					saveButtonPanel.add(btnSave);
				}
			}
			{
				resultZoomSlider = new NViewZoomSlider();
				resultZoomSlider.setView(resultView);
				savePanel.add(resultZoomSlider, BorderLayout.EAST);
			}
		}
		fcOpen = new ImageThumbnailFileChooser();
		fcOpen.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcOpen.setFileFilter(new Utils.ImageFileFilter(NImages.getOpenFileFilter()));
		fcSave = new ImageThumbnailFileChooser();
		fcSave.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcSave.setFileFilter(new Utils.ImageFileFilter(NImages.getSaveFileFilter()));
	}

	@Override
	protected void setDefaultValues() {
		// No default values.
	}

	@Override
	protected void updateControls() {
		btnSave.setEnabled((segmentedIris != null) && (segmentedIris.getImage() != null));
		btnSegment.setEnabled((iris != null) && (iris.getImage() != null) && (iris.getOwner() == null));
	}

	@Override
	protected void updateIrisesTools() {
		IrisesTools.getInstance().getClient().reset();
	}

	// ===========================================================
	// Package private methods
	// ===========================================================

	void disableControls() {
		btnSave.setEnabled(false);
		btnSegment.setEnabled(false);
	}

	void updateSegmentedIris(NEAttributes attributes) {
		resultView.setIris(getSegmentedIris());

		tfQuality.setText(String.valueOf(attributes.getQuality()));
		tfGrayScaleUtilisation.setText(String.valueOf(attributes.getGrayScaleUtilisation() & 0xFF));
		tfPupilToIrisRatio.setText(String.valueOf(attributes.getPupilToIrisRatio() & 0xFF));
		tfUsableIrisArea.setText(String.valueOf(attributes.getUsableIrisArea() & 0xFF));
		tfIrisScleraContrast.setText(String.valueOf(attributes.getIrisScleraContrast() & 0xFF));
		tfIrisPupilContrast.setText(String.valueOf(attributes.getIrisPupilContrast() & 0xFF));
		tfIrisPupilConcentricity.setText(String.valueOf(attributes.getIrisPupilConcentricity() & 0xFF));
		tfPupilBoundaryCircularity.setText(String.valueOf(attributes.getPupilBoundaryCircularity() & 0xFF));
		tfMarginAdequacy.setText(String.valueOf(attributes.getMarginAdequacy() & 0xFF));
		tfSharpness.setText(String.valueOf(attributes.getSharpness() & 0xFF));
		tfInterlace.setText(String.valueOf(attributes.getInterlace() & 0xFF));
	}

	NIris getIris() {
		return iris;
	}

	void setIris(NIris iris) {
		this.iris = iris;
	}

	NIris getSegmentedIris() {
		return segmentedIris;
	}

	void setSegmentedIris(NIris segmentedIris) {
		this.segmentedIris = segmentedIris;
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == btnOpen) {
				openImage();
			} else if (ev.getSource() == btnSegment) {
				segmentIris();
			} else if (ev.getSource() == btnSave) {
				saveImage();
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

	private class SegmentationHandler implements CompletionHandler<NBiometricTask, Object> {

		@Override
		public void completed(final NBiometricTask task, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					if (task.getStatus() == NBiometricStatus.OK) {
						NEAttributes attributes = getIris().getObjects().get(0);
						setSegmentedIris((NIris) attributes.getChild());
						updateSegmentedIris(attributes);
					} else {
						JOptionPane.showMessageDialog(SegmentIris.this, "Segmentation failed: " + task.getStatus(), "Error", JOptionPane.WARNING_MESSAGE);
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
					showErrorDialog(th);
				}

			});
		}

	}

}
