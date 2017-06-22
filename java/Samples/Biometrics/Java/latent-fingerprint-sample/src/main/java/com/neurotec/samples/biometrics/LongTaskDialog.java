package com.neurotec.samples.biometrics;

import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Toolkit;
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

public final class LongTaskDialog extends JDialog {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public static methods
	// ==============================================
	public static NBiometricTask runLongTask(Frame owner, LongTaskInterface callback, String title, NSubject subject) throws InterruptedException,
			ExecutionException {
		LongTaskDialog frmLongTask = new LongTaskDialog(owner, callback, title, subject);

		Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
		frmLongTask.setLocation(screenSize.width / 2 - frmLongTask.getPreferredSize().width / 2, screenSize.height / 2 - frmLongTask.getPreferredSize().height
				/ 2);
		frmLongTask.setVisible(true);
		return frmLongTask.backGroundWorker.get();

	}

	// ==============================================
	// Private fields
	// ==============================================

	private final BackgroundWorker backGroundWorker;
	private NSubject subject;
	private LongTaskInterface taskExecuter;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JLabel lblTitle;
	private JProgressBar progressBar;

	// ==============================================
	// Private constructor
	// ==============================================

	private LongTaskDialog(Frame owner, LongTaskInterface callback, String text, NSubject subject) {
		super(owner, "Working", true);
		this.backGroundWorker = new BackgroundWorker();
		setPreferredSize(new Dimension(375, 100));
		setResizable(false);
		this.taskExecuter = callback;
		initializeComponents();
		this.subject = subject;

		lblTitle.setText(text);

		addComponentListener(new ComponentAdapter() {
			@Override
			public void componentShown(ComponentEvent e) {
				execute();
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

	// ==============================================
	// Package private methods
	// ==============================================

	void execute() {
		backGroundWorker.execute();
	}

	// ==============================================
	// Inner classes
	// ==============================================

	private final class BackgroundWorker extends SwingWorker<NBiometricTask, Object> {

		@Override
		protected NBiometricTask doInBackground() {
			return taskExecuter.executeTask(subject);
		}

		@Override
		protected void done() {
			dispose();
		}
	}

}
