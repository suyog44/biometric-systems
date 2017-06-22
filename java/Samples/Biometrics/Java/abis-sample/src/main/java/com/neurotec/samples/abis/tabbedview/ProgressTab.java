package com.neurotec.samples.abis.tabbedview;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.GridLayout;
import java.awt.SystemColor;

import javax.swing.BorderFactory;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JTextPane;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

public class ProgressTab extends Tab {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private boolean canceled;

	private JPanel panelProgressBar;
	private JPanel panelStatus;
	private JPanel panelTop;
	private JProgressBar progressBar;
	private JTextPane tpStatus;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public ProgressTab() {
		initGUI();
		panelStatus.setVisible(false);
		panelProgressBar.setVisible(false);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		panelTop = new JPanel();
		panelProgressBar = new JPanel();
		progressBar = new JProgressBar();
		panelStatus = new JPanel();
		tpStatus = new JTextPane();
		StyledDocument doc = tpStatus.getStyledDocument();
		SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
		StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
		StyleConstants.setSpaceAbove(styleAttributes, 1);
		StyleConstants.setSpaceBelow(styleAttributes, 1);
		StyleConstants.setLeftIndent(styleAttributes, 3);
		StyleConstants.setRightIndent(styleAttributes, 3);
		StyleConstants.setFontSize(styleAttributes, 13);
		StyleConstants.setBold(styleAttributes, true);
		StyleConstants.setForeground(styleAttributes, SystemColor.menu);
		doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);

		setLayout(new BorderLayout());

		panelTop.setLayout(new BorderLayout());

		progressBar.setIndeterminate(true);
		progressBar.setStringPainted(true);
		panelProgressBar.add(progressBar);

		panelTop.add(panelProgressBar, BorderLayout.NORTH);

		panelStatus.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
		panelStatus.setLayout(new GridLayout());

		tpStatus.setEditable(false);
		panelStatus.add(tpStatus);

		panelTop.add(panelStatus, BorderLayout.SOUTH);

		add(panelTop, BorderLayout.NORTH);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setStatus(String msg, Color color) {
		tpStatus.setText(msg);
		tpStatus.setBackground(color);
	}

	public void showProgress() {
		progressBar.setIndeterminate(true);
		panelProgressBar.setVisible(true);
		panelStatus.setVisible(false);
	}

	public void showProgress(int min, int max) {
		progressBar.setIndeterminate(false);
		progressBar.setMinimum(min);
		progressBar.setMaximum(max);
		panelProgressBar.setVisible(true);
		panelStatus.setVisible(false);
	}

	public void setProgress(String text) {
		progressBar.setString(text);
	}

	public void setProgress(String text, int value) {
		if (progressBar.isIndeterminate()) {
			throw new IllegalStateException("Cannot set integer value for an indeterminate progress bar.");
		}
		progressBar.setValue(value);
		progressBar.setString(text);
	}

	public void hideProgress() {
		panelProgressBar.setVisible(false);
		panelStatus.setVisible(true);
	}

	public boolean isCanceled() {
		return canceled;
	}

	public void setCanceled(boolean cancelled) {
		this.canceled = cancelled;
	}

}
