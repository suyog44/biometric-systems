package com.neurotec.samples.server.controls;

import java.awt.Dimension;
import java.awt.Frame;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.util.concurrent.ExecutionException;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.SwingWorker;

import com.neurotec.samples.server.LongTask;

public final class LongTaskDialog extends JDialog {

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public static methods
	// ==============================================

	public static Object runLongTask(Frame owner, String title, LongTask longTask) throws InterruptedException, ExecutionException {
		LongTaskDialog frmLongTask = new LongTaskDialog(owner, title, longTask);
		frmLongTask.setLocationRelativeTo(owner);
		frmLongTask.setVisible(true);
		return frmLongTask.backgroundWorker.get();
	}

	// ==============================================
	// Private fields
	// ==============================================

	private final BackgroundWorker backgroundWorker = new BackgroundWorker();
	private LongTask longTask;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JLabel lblTitle;
	private JProgressBar progressBar;

	// ==============================================
	// Private constructor
	// ==============================================

	private LongTaskDialog(Frame owner, String text, LongTask longTask) {
		super(owner, "Working", true);
		setPreferredSize(new Dimension(375, 100));
		setResizable(false);
		initializeComponents();

		lblTitle.setText(text);
		this.longTask = longTask;
		addComponentListener(new ComponentAdapter() {
			@Override
			public void componentShown(ComponentEvent e) {
				backgroundWorker.execute();
			}
		});
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		JPanel mainPanel = new JPanel();
		mainPanel.setLayout(new BoxLayout(mainPanel, BoxLayout.Y_AXIS));
		mainPanel.setBorder(BorderFactory.createEmptyBorder(5, 10, 10, 10));

		lblTitle = new JLabel("Working...");
		lblTitle.setAlignmentX(LEFT_ALIGNMENT);

		progressBar = new JProgressBar(0, 100);
		progressBar.setPreferredSize(new Dimension(345, 25));
		progressBar.setMinimumSize(new Dimension(345, 25));
		progressBar.setAlignmentX(LEFT_ALIGNMENT);
		progressBar.setIndeterminate(true);

		mainPanel.add(Box.createVerticalStrut(8));
		mainPanel.add(lblTitle);
		mainPanel.add(progressBar);
		mainPanel.add(Box.createVerticalGlue());

		getContentPane().add(mainPanel);
		pack();
	}

	// =================================================================
	// Inner class extending SwingWorker which displays progress
	// =================================================================

	private final class BackgroundWorker extends SwingWorker<Object, Object> {

		// ==============================================
		// Protected methods
		// ==============================================

		@Override
		protected Object doInBackground() {
			return longTask.doInBackground();
		}

		@Override
		protected void done() {
			dispose();
		}
	}
}
