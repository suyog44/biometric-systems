package com.neurotec.samples;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.text.DecimalFormatSymbols;
import java.text.NumberFormat;
import java.text.ParseException;
import java.util.ArrayList;

import javax.swing.BorderFactory;
import javax.swing.Box.Filler;
import javax.swing.BoxLayout;
import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.SwingUtilities;
import javax.swing.border.TitledBorder;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NSubject.IrisCollection;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NViewZoomSlider;
import com.neurotec.util.concurrent.CompletionHandler;

public final class VerifyIris extends BasePanel implements ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final String SUBJECT_LEFT = "left";
	private static final String SUBJECT_RIGHT = "right";

	private static final String LEFT_LABEL_TEXT = "Image or template left: ";
	private static final String RIGHT_LABEL_TEXT = "Image or template right: ";

	// ===========================================================
	// Private fields
	// ===========================================================

	private NSubject subjectLeft;
	private NSubject subjectRight;
	private NIrisView viewLeft;
	private NIrisView viewRight;
	private NViewZoomSlider leftZoomSlider;
	private NViewZoomSlider rightZoomSlider;
	private ImageThumbnailFileChooser fileChooser;

	private final TemplateCreationHandler templateCreationHandler = new TemplateCreationHandler();
	private final VerificationHandler verificationHandler = new VerificationHandler();

	private JButton btnClear;
	private JButton btnDefault;
	private JButton btnLeftOpen;
	private JButton btnRightOpen;
	private JButton btnVerify;
	private JComboBox comboBoxFar;
	private Filler filler1;
	private Filler filler2;
	private Filler filler3;
	private JLabel lblLeft;
	private JLabel lblRight;
	private JLabel lblVerify;
	private JPanel panelCenter;
	private JPanel panelClearButton;
	private JPanel panelFar;
	private JPanel panelMain;
	private JPanel panelNorth;
	private JPanel panelSouth;
	private JPanel panelVerify;
	private JPanel leftPanel;
	private JPanel leftAlignSlider;
	private JPanel rightPanel;
	private JPanel rightAlignPanel;
	private JScrollPane scrollPaneLeft;
	private JScrollPane scrollPaneRight;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public VerifyIris() {
		super();
		licenses = new ArrayList<String>();
		licenses.add("Biometrics.IrisExtraction");
		licenses.add("Biometrics.IrisMatching");

		subjectLeft = new NSubject();
		subjectRight = new NSubject();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void loadItem(String position) throws IOException {
		fileChooser.setMultiSelectionEnabled(false);
		if (fileChooser.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			lblVerify.setText("");
			NSubject subjectTmp = null;
			NIris iris = null;
			try {
				subjectTmp = NSubject.fromFile(fileChooser.getSelectedFile().getAbsolutePath());
				IrisCollection irises = subjectTmp.getIrises();
				if (irises.isEmpty()) {
					subjectTmp = null;
					throw new IllegalArgumentException("Template contains no iris records.");
				}
				iris = irises.get(0);
				templateCreationHandler.completed(NBiometricStatus.OK, position);
			} catch (UnsupportedOperationException e) {
				// Ignore. UnsupportedOperationException means file is not a valid template.
			}

			// If file is not a template, try to load it as an image.
			if (subjectTmp == null) {
				iris = new NIris();
				iris.setFileName(fileChooser.getSelectedFile().getAbsolutePath());
				subjectTmp = new NSubject();
				subjectTmp.getIrises().add(iris);
				updateIrisesTools();
				IrisesTools.getInstance().getClient().createTemplate(subjectTmp, position, templateCreationHandler);
			}

			if (SUBJECT_LEFT.equals(position)) {
				subjectLeft = subjectTmp;
				lblLeft.setText(fileChooser.getSelectedFile().getAbsolutePath());
				viewLeft.setIris(iris);
			} else if (SUBJECT_RIGHT.equals(position)) {
				subjectRight = subjectTmp;
				lblRight.setText(fileChooser.getSelectedFile().getAbsolutePath());
				viewRight.setIris(iris);
			} else {
				throw new AssertionError("Unknown subject position: " + position);
			}
		}
	}

	private void verify() {
		updateIrisesTools();
		IrisesTools.getInstance().getClient().verify(subjectLeft, subjectRight, null, verificationHandler);
	}

	private void clear() {
		viewLeft.setIris(null);
		viewRight.setIris(null);
		subjectLeft.clear();
		subjectRight.clear();
		updateControls();
		lblVerify.setText(" ");
		lblLeft.setText(LEFT_LABEL_TEXT);
		lblRight.setText(RIGHT_LABEL_TEXT);
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
				panelNorth = new JPanel();
				panelMain.add(panelNorth, BorderLayout.NORTH);
				{
					btnLeftOpen = new JButton();
					btnLeftOpen.setText("Open");
					btnLeftOpen.addActionListener(this);
					panelNorth.add(btnLeftOpen);
				}
				{
					panelFar = new JPanel();
					panelFar.setBorder(BorderFactory.createTitledBorder(null, "Matching FAR", TitledBorder.CENTER, TitledBorder.DEFAULT_POSITION));
					panelFar.setLayout(new GridBagLayout());
					panelNorth.add(panelFar);
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
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 0;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.insets = new Insets(3, 3, 3, 3);
						panelFar.add(comboBoxFar, gridBagConstraints);
					}
					{
						btnDefault = new JButton();
						btnDefault.setText("Default");
						btnDefault.addActionListener(this);
						gridBagConstraints = new GridBagConstraints();
						gridBagConstraints.gridx = 1;
						gridBagConstraints.gridy = 0;
						gridBagConstraints.insets = new Insets(3, 3, 3, 3);
						panelFar.add(btnDefault, gridBagConstraints);
					}
				}
				{
					btnRightOpen = new JButton();
					btnRightOpen.setText("Open");
					btnRightOpen.addActionListener(this);
					panelNorth.add(btnRightOpen);
				}
			}
			{
				panelCenter = new JPanel();
				panelCenter.setLayout(new GridLayout(1, 2, 5, 0));
				panelMain.add(panelCenter, BorderLayout.CENTER);
				{
					leftPanel = new JPanel();
					leftPanel.setLayout(new BorderLayout());
					panelCenter.add(leftPanel);
					{
						scrollPaneLeft = new JScrollPane();
						scrollPaneLeft.setPreferredSize(new Dimension(200, 200));
						leftPanel.add(scrollPaneLeft, BorderLayout.CENTER);
						{
							viewLeft = new NIrisView();
							viewLeft.setAutofit(true);
							scrollPaneLeft.setViewportView(viewLeft);
						}
					}
					{
						leftAlignSlider = new JPanel();
						leftAlignSlider.setLayout(new BorderLayout());
						leftPanel.add(leftAlignSlider, BorderLayout.SOUTH);
						{
							leftZoomSlider = new NViewZoomSlider();
							leftZoomSlider.setView(viewLeft);
							leftAlignSlider.add(leftZoomSlider, BorderLayout.WEST);
						}
					}
				}
				{
					rightPanel = new JPanel();
					rightPanel.setLayout(new BorderLayout());
					panelCenter.add(rightPanel);
					{
						scrollPaneRight = new JScrollPane();
						scrollPaneRight.setPreferredSize(new Dimension(200, 200));
						rightPanel.add(scrollPaneRight, BorderLayout.CENTER);
						{
							viewRight = new NIrisView();
							viewRight.setAutofit(true);
							scrollPaneRight.setViewportView(viewRight);
						}
					}
					{
						rightAlignPanel = new JPanel();
						rightAlignPanel.setLayout(new BorderLayout());
						rightPanel.add(rightAlignPanel, BorderLayout.SOUTH);
						{
							rightZoomSlider = new NViewZoomSlider();
							rightZoomSlider.setView(viewRight);
							rightAlignPanel.add(rightZoomSlider, BorderLayout.EAST);
						}
					}
				}
			}
			{
				panelSouth = new JPanel();
				panelSouth.setLayout(new BorderLayout());
				panelMain.add(panelSouth, BorderLayout.SOUTH);
				{
					panelClearButton = new JPanel();
					panelSouth.add(panelClearButton, BorderLayout.NORTH);
					{
						btnClear = new JButton();
						btnClear.setText("Clear images");
						btnClear.addActionListener(this);
						panelClearButton.add(btnClear);
					}
				}
				{
					panelVerify = new JPanel();
					panelVerify.setLayout(new BoxLayout(panelVerify, BoxLayout.Y_AXIS));
					panelSouth.add(panelVerify, BorderLayout.WEST);
					{
						lblLeft = new JLabel();
						lblLeft.setText(LEFT_LABEL_TEXT);
						panelVerify.add(lblLeft);
					}
					{
						filler1 = new Filler(new Dimension(0, 3), new Dimension(0, 3), new Dimension(32767, 3));
						panelVerify.add(filler1);
					}
					{
						lblRight = new JLabel();
						lblRight.setText(RIGHT_LABEL_TEXT);
						panelVerify.add(lblRight);
					}
					{
						filler2 = new Filler(new Dimension(0, 3), new Dimension(0, 3), new Dimension(32767, 3));
						panelVerify.add(filler2);
					}
					{
						btnVerify = new JButton();
						btnVerify.setText("Verify");
						btnVerify.setEnabled(false);
						btnVerify.addActionListener(this);
						panelVerify.add(btnVerify);
					}
					{
						filler3 = new Filler(new Dimension(0, 3), new Dimension(0, 3), new Dimension(32767, 3));
						panelVerify.add(filler3);
					}
					{
						lblVerify = new JLabel();
						lblVerify.setText("     ");
						panelVerify.add(lblVerify);
					}
				}
			}
		}
		fileChooser = new ImageThumbnailFileChooser();
		fileChooser.setIcon(Utils.createIconImage("images/Logo16x16.png"));
	}

	@Override
	protected void setDefaultValues() {
		comboBoxFar.setSelectedItem(Utils.matchingThresholdToString(IrisesTools.getInstance().getDefaultClient().getMatchingThreshold()));
	}

	@Override
	protected void updateControls() {
		if (subjectLeft.getIrises().isEmpty()
			|| (subjectLeft.getIrises().get(0).getObjects().get(0).getTemplate() == null)
			|| subjectRight.getIrises().isEmpty()
			|| (subjectRight.getIrises().get(0).getObjects().get(0).getTemplate() == null)) {
			btnVerify.setEnabled(false);
		} else {
			btnVerify.setEnabled(true);
		}
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

	void updateLabel(String msg) {
		lblVerify.setText(msg);
	}

	NSubject getLeft() {
		return subjectLeft;
	}

	NSubject getRight() {
		return subjectRight;
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == btnDefault) {
				comboBoxFar.setSelectedItem(Utils.matchingThresholdToString(IrisesTools.getInstance().getDefaultClient().getMatchingThreshold()));
			} else if (ev.getSource() == btnVerify) {
				verify();
			} else if (ev.getSource() == btnLeftOpen) {
				loadItem(SUBJECT_LEFT);
			} else if (ev.getSource() == btnRightOpen) {
				loadItem(SUBJECT_RIGHT);
			} else if (ev.getSource() == btnClear) {
				clear();
			}
		} catch (Exception e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, e, "Error", JOptionPane.ERROR_MESSAGE);
		}
	}

	// ===========================================================
	// Inner classes
	// ===========================================================

	private class TemplateCreationHandler implements CompletionHandler<NBiometricStatus, String> {

		@Override
		public void completed(final NBiometricStatus status, final String subject) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					if (status != NBiometricStatus.OK) {
						JOptionPane.showMessageDialog(VerifyIris.this, "Template was not created: " + status, "Error", JOptionPane.WARNING_MESSAGE);
					}
					updateControls();
				}

			});
		}

		@Override
		public void failed(final Throwable th, final String subject) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					th.printStackTrace();
					showErrorDialog(th);
				}

			});
		}

	}

	private class VerificationHandler implements CompletionHandler<NBiometricStatus, String> {

		@Override
		public void completed(final NBiometricStatus status, final String subject) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					if (status == NBiometricStatus.OK) {
						int score = getLeft().getMatchingResults().get(0).getScore();
						String msg = "Score of matched templates: " + score;
						updateLabel(msg);
						JOptionPane.showMessageDialog(VerifyIris.this, msg, "Match", JOptionPane.PLAIN_MESSAGE);
					} else {
						JOptionPane.showMessageDialog(VerifyIris.this, "Templates didn't match.", "No match", JOptionPane.WARNING_MESSAGE);
					}
				}

			});
		}

		@Override
		public void failed(final Throwable th, final String subject) {
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
