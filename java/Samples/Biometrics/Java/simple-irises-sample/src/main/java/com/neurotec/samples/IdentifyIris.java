package com.neurotec.samples;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.text.DecimalFormatSymbols;
import java.text.NumberFormat;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;
import javax.swing.SwingConstants;
import javax.swing.SwingUtilities;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.DefaultTableModel;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NSubject.IrisCollection;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;
import com.neurotec.util.concurrent.CompletionHandler;

public final class IdentifyIris extends BasePanel implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private ImageThumbnailFileChooser fcGallery;
	private ImageThumbnailFileChooser fcProbe;
	private NIrisView view;
	private NViewZoomSlider zoomSlider;

	private final EnrollHandler enrollHandler = new EnrollHandler();
	private final IdentificationHandler identificationHandler = new IdentificationHandler();
	private final TemplateCreationHandler templateCreationHandler = new TemplateCreationHandler();

	private NSubject subject;
	private final List<NSubject> subjects;

	private JButton btnFarDefault;
	private JButton identifyButton;
	private JButton openProbeButton;
	private JButton openTemplatesButton;
	private JComboBox comboBoxFar;
	private JLabel lblCount;
	private JLabel fileLabel;
	private JLabel templatesLabel;
	private JPanel panelFar;
	private JPanel panelIdentificationNorth;
	private JPanel panelIdentification;
	private JPanel panelIdentifyButton;
	private JPanel panelImage;
	private JPanel panelLeft;
	private JPanel panelMain;
	private JPanel panelNorth;
	private JPanel panelOpenImage;
	private JPanel panelResults;
	private JPanel panelSettings;
	private JPanel panelTop;
	private JScrollPane resultsScrollPane;
	private JTable resultsTable;
	private JScrollPane scrollPane;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public IdentifyIris() {
		super();
		subjects = new ArrayList<NSubject>();
		licenses = new ArrayList<String>();
		licenses.add("Biometrics.IrisExtraction");
		licenses.add("Biometrics.IrisMatching");
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void openTemplates() throws IOException {
		if (fcGallery.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			((DefaultTableModel) resultsTable.getModel()).setRowCount(0);
			subjects.clear();

			// Create subjects from selected templates.
			for (File file : fcGallery.getSelectedFiles()) {
				NSubject s = NSubject.fromFile(file.getAbsolutePath());
				s.setId(file.getName());
				subjects.add(s);
			}
			lblCount.setText(String.valueOf(subjects.size()));
		}
		updateControls();
	}

	private void openProbe() throws IOException {
		if (fcProbe.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			((DefaultTableModel) resultsTable.getModel()).setRowCount(0);
			fileLabel.setText("");
			subject = null;
			NIris iris = null;
			try {
				subject = NSubject.fromFile(fcProbe.getSelectedFile().getAbsolutePath());
				subject.setId(fcProbe.getSelectedFile().getName());
				IrisCollection irises = subject.getIrises();
				if (irises.isEmpty()) {
					subject = null;
					throw new IllegalArgumentException("Template contains no iris records.");
				}
				iris = irises.get(0);
				templateCreationHandler.completed(NBiometricStatus.OK, null);
			} catch (UnsupportedOperationException e) {
				// Ignore. UnsupportedOperationException means file is not a valid template.
			}

			// If file is not a template, try to load it as an image.
			if (subject == null) {
				iris = new NIris();
				iris.setFileName(fcProbe.getSelectedFile().getAbsolutePath());
				subject = new NSubject();
				subject.setId(fcProbe.getSelectedFile().getName());
				subject.getIrises().add(iris);
				updateIrisesTools();
				IrisesTools.getInstance().getClient().createTemplate(subject, null, templateCreationHandler);
			}

			view.setIris(iris);
			fileLabel.setText(fcProbe.getSelectedFile().getAbsolutePath());
		}
	}

	private void identify() {
		if ((subject != null) && !subjects.isEmpty()) {
			((DefaultTableModel) resultsTable.getModel()).setRowCount(0);
			updateIrisesTools();

			// Clean earlier data before proceeding, enroll new data
			IrisesTools.getInstance().getClient().clear();

			// Create enrollment task.
			NBiometricTask enrollmentTask = new NBiometricTask(EnumSet.of(NBiometricOperation.ENROLL));

			// Add subjects to be enrolled.
			for (NSubject s : subjects) {
				enrollmentTask.getSubjects().add(s);
			}

			// Enroll subjects.
			IrisesTools.getInstance().getClient().performTask(enrollmentTask, null, enrollHandler);
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
			panelMain = new JPanel();
			panelMain.setLayout(new BorderLayout());
			add(panelMain, BorderLayout.CENTER);
			{
				panelTop = new JPanel();
				panelTop.setLayout(new GridBagLayout());
				panelMain.add(panelTop, BorderLayout.CENTER);
				{
					panelLeft = new JPanel();
					panelLeft.setLayout(new BorderLayout());
					gridBagConstraints = new GridBagConstraints();
					gridBagConstraints.gridx = 0;
					gridBagConstraints.gridy = 0;
					gridBagConstraints.fill = GridBagConstraints.BOTH;
					gridBagConstraints.weightx = 1.0;
					gridBagConstraints.weighty = 1.0;
					panelTop.add(panelLeft, gridBagConstraints);
					{
						panelNorth = new JPanel();
						panelNorth.setBorder(BorderFactory.createTitledBorder("Templates loading"));
						panelNorth.setLayout(new FlowLayout(FlowLayout.LEADING));
						panelLeft.add(panelNorth, BorderLayout.NORTH);
						{
							openTemplatesButton = new JButton();
							openTemplatesButton.setText("Load");
							openTemplatesButton.addActionListener(this);
							panelNorth.add(openTemplatesButton);
						}
						{
							templatesLabel = new JLabel();
							templatesLabel.setText("Templates loaded: ");
							panelNorth.add(templatesLabel);
						}
						{
							lblCount = new JLabel();
							lblCount.setText("0");
							panelNorth.add(lblCount);
						}
					}
					{
						panelImage = new JPanel();
						panelImage.setBorder(BorderFactory.createTitledBorder("Image / template for identification"));
						panelImage.setLayout(new BorderLayout());
						panelLeft.add(panelImage, BorderLayout.CENTER);
						{
							scrollPane = new JScrollPane();
							panelImage.add(scrollPane, BorderLayout.CENTER);
							{
								view = new NIrisView();
								view.setAutofit(true);
								scrollPane.setViewportView(view);
							}
						}
						{
							panelOpenImage = new JPanel();
							panelOpenImage.setLayout(new FlowLayout(FlowLayout.LEADING));
							panelImage.add(panelOpenImage, BorderLayout.PAGE_START);
							{
								openProbeButton = new JButton();
								openProbeButton.setText("Open");
								openProbeButton.addActionListener(this);
								panelOpenImage.add(openProbeButton);
							}
							{
								fileLabel = new JLabel();
								panelOpenImage.add(fileLabel);
							}
						}
					}
				}
				{
					panelResults = new JPanel();
					panelResults.setBorder(BorderFactory.createTitledBorder("Results"));
					panelResults.setLayout(new GridLayout(1, 1));
					gridBagConstraints = new GridBagConstraints();
					gridBagConstraints.gridx = 1;
					gridBagConstraints.gridy = 0;
					gridBagConstraints.fill = GridBagConstraints.BOTH;
					gridBagConstraints.weightx = 1.0;
					gridBagConstraints.weighty = 1.0;
					panelTop.add(panelResults, gridBagConstraints);
					{
						resultsScrollPane = new JScrollPane();
						panelResults.add(resultsScrollPane);
						{
							resultsTable = new JTable();
							resultsTable.setModel(new DefaultTableModel(
									new Object[][] {},
									new String[] {"ID", "Score"}) {

								private final Class<?>[] types = new Class<?>[] {String.class, Integer.class};
								private final boolean[] canEdit = new boolean[] {false, false};

								@Override
								public Class<?> getColumnClass(int columnIndex) {
									return types[columnIndex];
								}

								@Override
								public boolean isCellEditable(int rowIndex, int columnIndex) {
									return canEdit[columnIndex];
								}

							});
							DefaultTableCellRenderer leftRenderer = new DefaultTableCellRenderer();
							leftRenderer.setHorizontalAlignment(SwingConstants.LEFT);
							resultsTable.getColumnModel().getColumn(1).setCellRenderer(leftRenderer);
							resultsTable.setMinimumSize(new Dimension(100, 500));
							resultsScrollPane.setViewportView(resultsTable);
						}
					}
				}
			}
			{
				panelIdentification = new JPanel();
				panelIdentification.setBorder(BorderFactory.createTitledBorder(""));
				panelIdentification.setLayout(new BorderLayout());
				panelMain.add(panelIdentification, BorderLayout.SOUTH);
				{
					panelIdentificationNorth = new JPanel();
					panelIdentificationNorth.setLayout(new BorderLayout());
					panelIdentification.add(panelIdentificationNorth, BorderLayout.NORTH);
					{
						panelIdentifyButton = new JPanel();
						panelIdentifyButton.setLayout(new FlowLayout());
						panelIdentificationNorth.add(panelIdentifyButton, BorderLayout.WEST);
						{
							identifyButton = new JButton();
							identifyButton.setText("Identify");
							identifyButton.addActionListener(this);
							panelIdentifyButton.add(identifyButton);
						}
						{
							zoomSlider = new NViewZoomSlider();
							zoomSlider.setView(view);
							panelIdentifyButton.add(zoomSlider);
						}
					}
					{
						panelSettings = new JPanel();
						panelSettings.setLayout(new FlowLayout(FlowLayout.TRAILING));
						panelIdentificationNorth.add(panelSettings, BorderLayout.EAST);
						{
							panelFar = new JPanel();
							panelFar.setBorder(BorderFactory.createTitledBorder("Matching FAR"));
							panelFar.setLayout(new FlowLayout());
							panelSettings.add(panelFar);
							{
								comboBoxFar = new JComboBox();
								char c = new DecimalFormatSymbols().getPercent();
								DefaultComboBoxModel model = (DefaultComboBoxModel) comboBoxFar.getModel();
								NumberFormat nf = NumberFormat.getNumberInstance();
								nf.setMaximumFractionDigits(5);
								model.addElement(nf.format(0.1) + c);
								model.addElement(nf.format(0.01) + c);
								model.addElement(nf.format(0.001) + c);
								comboBoxFar.setSelectedIndex(1);
								comboBoxFar.setEditable(true);
								comboBoxFar.setModel(model);
								panelFar.add(comboBoxFar);
							}
						}
						{
							btnFarDefault = new JButton();
							btnFarDefault.setText("Default");
							btnFarDefault.addActionListener(this);
							panelFar.add(btnFarDefault);
						}
					}
				}
			}
		}
		fcProbe = new ImageThumbnailFileChooser();
		fcProbe.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcGallery = new ImageThumbnailFileChooser();
		fcGallery.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fcGallery.setMultiSelectionEnabled(true);
	}

	@Override
	protected void setDefaultValues() {
		comboBoxFar.setSelectedItem(Utils.matchingThresholdToString(IrisesTools.getInstance().getDefaultClient().getMatchingThreshold()));
	}

	@Override
	protected void updateControls() {
		identifyButton.setEnabled(!subjects.isEmpty() && (subject != null) && ((subject.getStatus() == NBiometricStatus.OK) || (subject.getStatus() == NBiometricStatus.NONE)));
	}

	@Override
	protected void updateIrisesTools() {
		IrisesTools.getInstance().getClient().reset();
		try {
			IrisesTools.getInstance().getClient().setMatchingThreshold(Utils.matchingThresholdFromString(comboBoxFar.getSelectedItem().toString()));
		} catch (ParseException e) {
			e.printStackTrace();
			IrisesTools.getInstance().getClient().setMatchingThreshold(IrisesTools.getInstance().getDefaultClient().getMatchingThreshold());
			comboBoxFar.setSelectedItem(Utils.matchingThresholdToString(IrisesTools.getInstance().getDefaultClient().getMatchingThreshold()));
			JOptionPane.showMessageDialog(this, "FAR is not valid. Using default value.", "Error", JOptionPane.ERROR_MESSAGE);
		}
	}

	// ===========================================================
	// Package private methods
	// ===========================================================

	NSubject getSubject() {
		return subject;
	}

	void setSubject(NSubject subject) {
		this.subject = subject;
	}

	List<NSubject> getSubjects() {
		return subjects;
	}

	void appendIdentifyResult(String name, int score) {
		((DefaultTableModel) resultsTable.getModel()).addRow(new Object[] {name, score});
	}

	void prependIdentifyResult(String name, int score) {
		((DefaultTableModel) resultsTable.getModel()).insertRow(0, new Object[] {name, score});
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == openTemplatesButton) {
				openTemplates();
			} else if (ev.getSource() == openProbeButton) {
				openProbe();
			} else if (ev.getSource() == btnFarDefault) {
				comboBoxFar.setSelectedItem(Utils.matchingThresholdToString(IrisesTools.getInstance().getDefaultClient().getMatchingThreshold()));
			} else if (ev.getSource() == identifyButton) {
				identify();
			}
		} catch (Exception e) {
			e.printStackTrace();
			updateControls();
			JOptionPane.showMessageDialog(this, e, "Error", JOptionPane.ERROR_MESSAGE);
		}
	}

	// ===========================================================
	// Inner classes
	// ===========================================================

	private class TemplateCreationHandler implements CompletionHandler<NBiometricStatus, Object> {

		@Override
		public void completed(final NBiometricStatus status, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					updateControls();
					if (status != NBiometricStatus.OK) {
						setSubject(null);
						JOptionPane.showMessageDialog(IdentifyIris.this, "Template was not created: " + status, "Error", JOptionPane.WARNING_MESSAGE);
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
					updateControls();
					showErrorDialog(th);
				}

			});
		}

	}

	private class EnrollHandler implements CompletionHandler<NBiometricTask, Object> {

		@Override
		public void completed(final NBiometricTask task, final Object attachment) {
			if (task.getStatus() == NBiometricStatus.OK) {

				// Identify current subject in enrolled ones.
				IrisesTools.getInstance().getClient().identify(getSubject(), null, identificationHandler);
			} else {
				JOptionPane.showMessageDialog(IdentifyIris.this, "Enrollment failed: " + task.getStatus(), "Error", JOptionPane.WARNING_MESSAGE);
			}
		}

		@Override
		public void failed(final Throwable th, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					th.printStackTrace();
					updateControls();
					showErrorDialog(th);
				}

			});
		}

	}

	private class IdentificationHandler implements CompletionHandler<NBiometricStatus, Object> {

		@Override
		public void completed(final NBiometricStatus status, final Object attachment) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					if ((status == NBiometricStatus.OK) || (status == NBiometricStatus.MATCH_NOT_FOUND)) {

						// Match subjects.
						for (NSubject s : getSubjects()) {
							boolean match = false;
							for (NMatchingResult result : getSubject().getMatchingResults()) {
								if (s.getId().equals(result.getId())) {
									match = true;
									prependIdentifyResult(result.getId(), result.getScore());
									break;
								}
							}
							if (!match) {
								appendIdentifyResult(s.getId(), 0);
							}
						}
					} else {
						JOptionPane.showMessageDialog(IdentifyIris.this, "Identification failed: " + status, "Error", JOptionPane.WARNING_MESSAGE);
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
					updateControls();
					showErrorDialog(th);
				}

			});
		}

	}

}
