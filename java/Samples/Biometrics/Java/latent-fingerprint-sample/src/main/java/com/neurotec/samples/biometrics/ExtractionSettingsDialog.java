package com.neurotec.samples.biometrics;

import java.awt.Component;
import java.awt.Container;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;

public final class ExtractionSettingsDialog extends JDialog implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final int DEFAUTL_EXTRACTION_THRESHOLD = 39;

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private final MainFrameEventListener listener;
	private final int quality;
	private JLabel lblThreshold;
	private JSpinner spinnerThreshold;
	private JButton btnDefault;
	private JButton btnOk;
	private JButton btnCancel;

	// ==============================================
	// Public constructor
	// ==============================================

	public ExtractionSettingsDialog(Frame owner, MainFrameEventListener listener, int quality) {
		super(owner, "Extraction Settings", true);
		this.listener = listener;
		this.quality = quality;
		setPreferredSize(new Dimension(250, 130));
		setResizable(false);
		initializeComponents();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		Container contentPane = getContentPane();
		contentPane.setLayout(new BoxLayout(contentPane, BoxLayout.Y_AXIS));

		JPanel thresholdPanel = new JPanel();
		thresholdPanel.setAlignmentY(Component.TOP_ALIGNMENT);
		thresholdPanel.setBorder(BorderFactory.createTitledBorder("Quality threshold"));
		thresholdPanel.setLayout(new BoxLayout(thresholdPanel, BoxLayout.Y_AXIS));

		JPanel valuePanel = new JPanel();
		valuePanel.setLayout(new BoxLayout(valuePanel, BoxLayout.X_AXIS));

		lblThreshold = new JLabel("Threshold:");
		spinnerThreshold = new JSpinner(new SpinnerNumberModel(quality, 0, 100, 1));
		btnDefault = new JButton("Default");
		btnDefault.addActionListener(this);

		lblThreshold.setEnabled(true);
		spinnerThreshold.setEnabled(true);
		btnDefault.setEnabled(true);

		valuePanel.add(Box.createHorizontalStrut(15));
		valuePanel.add(lblThreshold);
		valuePanel.add(Box.createHorizontalStrut(5));
		spinnerThreshold.setSize(20, 10);
		valuePanel.add(spinnerThreshold);
		valuePanel.add(Box.createHorizontalStrut(5));
		valuePanel.add(btnDefault);
		thresholdPanel.add(Box.createVerticalStrut(4));
		thresholdPanel.add(valuePanel);
		thresholdPanel.add(Box.createVerticalStrut(4));
		valuePanel.setAlignmentX(LEFT_ALIGNMENT);

		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));

		btnOk = new JButton("OK");
		btnOk.setPreferredSize(new Dimension(75, 25));
		btnOk.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setPreferredSize(new Dimension(75, 25));
		btnCancel.addActionListener(this);

		buttonPanel.add(Box.createHorizontalGlue());
		buttonPanel.add(btnOk);
		buttonPanel.add(Box.createHorizontalStrut(5));
		buttonPanel.add(btnCancel);
		buttonPanel.add(Box.createHorizontalStrut(5));

		contentPane.add(Box.createVerticalStrut(8));
		contentPane.add(thresholdPanel);
		contentPane.add(Box.createVerticalStrut(3));
		contentPane.add(buttonPanel);
		contentPane.add(Box.createVerticalStrut(5));

		thresholdPanel.setAlignmentX(LEFT_ALIGNMENT);
		buttonPanel.setAlignmentX(LEFT_ALIGNMENT);
		pack();
	}

	// ==============================================
	// Public methods
	// ==============================================

	public int getQualityThreshold() {
		return (Integer) spinnerThreshold.getValue();
	}

	public void setQualityThreshold(int value) {
		spinnerThreshold.setValue(value);
	}

	// ==============================================
	// Event handling
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnDefault) {
			spinnerThreshold.setValue(DEFAUTL_EXTRACTION_THRESHOLD);
		} else if (source == btnOk) {
			listener.extractionSettingsSelected(getQualityThreshold());
			dispose();
		} else if (source == btnCancel) {
			dispose();
		}
	}

}
