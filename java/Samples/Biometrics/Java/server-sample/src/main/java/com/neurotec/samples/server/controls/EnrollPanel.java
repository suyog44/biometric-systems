package com.neurotec.samples.server.controls;

import java.awt.Color;
import java.awt.Component;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;

import javax.swing.BorderFactory;
import javax.swing.Icon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JScrollPane;
import javax.swing.JSpinner;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.SpinnerNumberModel;
import javax.swing.SwingConstants;
import javax.swing.event.AncestorEvent;
import javax.swing.event.AncestorListener;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.samples.server.TaskListener;
import com.neurotec.samples.server.TaskSender;
import com.neurotec.samples.server.util.GridBagUtils;
import com.neurotec.samples.server.util.MessageUtils;
import com.neurotec.samples.util.Utils;

public final class EnrollPanel extends BasePanel {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private TaskSender enrollmentTaskSender;
	private long startTime;
	private GridBagUtils gridBagUtils;
	private TaskListener taskListener;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JButton btnStart;
	private JButton btnCancel;
	private Icon iconOk;
	private Icon iconError;
	private JLabel lblRemaining;
	private JLabel lblProgress;
	private JLabel lblStatusIcon;
	private JPanel panelProperties;
	private JProgressBar progressBar;
	private JSpinner spinnerBunchSize;
	private JTextField txtTemplatesCount;
	private JTextField txtTimeElapsed;
	private JTextArea txtStatus;

	// ==============================================
	// Public constructor
	// ==============================================

	public EnrollPanel(Frame owner) {
		super(owner);
		initializeComponents();
		addAncestorListener(new AncestorListener() {
			@Override
			public void ancestorRemoved(AncestorEvent event) {
			}
			@Override
			public void ancestorMoved(AncestorEvent event) {
			}
			@Override
			public void ancestorAdded(AncestorEvent event) {
				enrollPanelLoaded();
			}
		});
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		gridBagUtils = new GridBagUtils(GridBagConstraints.BOTH, new Insets(3, 3, 3, 3));
		GridBagLayout enrollLayout = new GridBagLayout();
		enrollLayout.columnWidths = new int[] { 75, 70, 320, 140, 50 };
		enrollLayout.rowHeights = new int[] { 25, 25, 20, 25, 180 };
		setLayout(enrollLayout);

		btnStart = new JButton("Start");
		btnStart.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setEnabled(false);
		btnCancel.addActionListener(this);

		initializePropertiesPanel();

		lblRemaining = new JLabel("Estimated time remaining:");
		lblProgress = new JLabel("progress", SwingConstants.RIGHT);
		progressBar = new JProgressBar(0, 100);

		gridBagUtils.addToGridBagLayout(0, 0, this, btnStart);
		gridBagUtils.addToGridBagLayout(0, 1, this, btnCancel);
		gridBagUtils.addToGridBagLayout(1, 0, 2, 2, this, panelProperties);
		gridBagUtils.addToGridBagLayout(0, 2, 3, 1, this, lblRemaining);
		gridBagUtils.addToGridBagLayout(3, 2, 1, 1, 1, 0, this, new JLabel());
		gridBagUtils.addToGridBagLayout(4, 2, 1, 1, 0, 0, this, lblProgress);
		gridBagUtils.addToGridBagLayout(0, 3, 5, 1, this, progressBar);
		gridBagUtils.addToGridBagLayout(0, 4, 5, 1, 0, 1, this, initializeResultsPanel());

	}

	private void initializePropertiesPanel() {
		panelProperties = new JPanel();
		panelProperties.setBorder(BorderFactory.createTitledBorder("Properties"));
		GridBagLayout propertiesPanelLayout = new GridBagLayout();
		propertiesPanelLayout.columnWidths = new int[] { 100, 125, 125 };
		panelProperties.setLayout(propertiesPanelLayout);

		spinnerBunchSize = new JSpinner(new SpinnerNumberModel(350, 1, 10000, 1));

		gridBagUtils.addToGridBagLayout(0, 2, panelProperties, new JLabel("Bunch size:"));
		gridBagUtils.addToGridBagLayout(1, 2, panelProperties, spinnerBunchSize);
		gridBagUtils.clearGridBagConstraints();
	}

	private JPanel initializeResultsPanel() {
		JPanel resultsPanel = new JPanel();
		resultsPanel.setBorder(BorderFactory.createTitledBorder("Results"));
		GridBagLayout resultsPanelLayout = new GridBagLayout();
		resultsPanelLayout.columnWidths = new int[] { 55, 45, 110, 80, 135, 240 };
		resultsPanelLayout.rowHeights = new int[] { 25, 50, 75 };
		resultsPanel.setLayout(resultsPanelLayout);

		txtTemplatesCount = new JTextField();
		txtTemplatesCount.setEditable(false);

		txtTimeElapsed = new JTextField("N/A");
		txtTimeElapsed.setEditable(false);

		lblStatusIcon = new JLabel();
		txtStatus = new JTextArea();
		iconOk = Utils.createIcon("images/ok.png");
		iconError = Utils.createIcon("images/error.png");
		JScrollPane txtStatusScrollPane = new JScrollPane(txtStatus, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);

		gridBagUtils.addToGridBagLayout(0, 0, 2, 1, resultsPanel, new JLabel("Templates to enroll:"));
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, resultsPanel, txtTemplatesCount);
		gridBagUtils.addToGridBagLayout(3, 0, resultsPanel, new JLabel("Time elapsed:"));
		gridBagUtils.addToGridBagLayout(4, 0, resultsPanel, txtTimeElapsed);
		gridBagUtils.addToGridBagLayout(5, 0, 1, 1, 1, 0, resultsPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(0, 1, 1, 1, 0, 0, resultsPanel, lblStatusIcon);
		gridBagUtils.addToGridBagLayout(0, 2, 1, 1, 0, 1, resultsPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(1, 1, 5, 2, 0, 0, resultsPanel, txtStatusScrollPane);
		gridBagUtils.clearGridBagConstraints();
		return resultsPanel;
	}

	private void startEnrolling() {
		try {
			if (isBusy()) {
				return;
			}
			setStatus("Preparing...", Color.BLACK, null);
			appendStatus(String.format("Enrolling from: %s\r\n", getTemplateLoader()));
			progressBar.setValue(0);
			int templateCount = getTemplateCount();
			progressBar.setMaximum(templateCount);
			txtTemplatesCount.setText(String.valueOf(templateCount));

			lblStatusIcon.setIcon(null);
			txtTimeElapsed.setText("N/A");

			enrollmentTaskSender.setBunchSize((Integer) spinnerBunchSize.getValue());
			enrollmentTaskSender.setBiometricClient(getBiometricClient());
			enrollmentTaskSender.setTemplateLoader(getTemplateLoader());
			enrollmentTaskSender.setAccelerator(getAccelerator() != null);

			startTime = System.currentTimeMillis();
			enrollmentTaskSender.start();
			enableControls(false);
		} catch (Exception e) {
			MessageUtils.showError(this, e);
			setStatus("Enrollment failed due to: " + e.toString(), Color.RED.darker(), iconError);
		}
	}

	private void enrollPanelLoaded() {
		enrollmentTaskSender = new TaskSender(getBiometricClient(), getTemplateLoader(), NBiometricOperation.ENROLL);
		taskListener = new TaskListener() {
			@Override
			public void taskFinished() {
				taskSenderFinished();
			}
			@Override
			public void taskErrorOccured(Exception e) {
				taskSenderExceptionOccured(e);
			}
			@Override
			public void taskProgressChanged(int completed) {
				taskSenderProgressChanged(completed);
			}
			@Override
			public void matchingTaskCompleted(NBiometricTask task) {
			}

		};
		enrollmentTaskSender.addTaskListener(taskListener);

		lblProgress.setText("");
		lblRemaining.setText("");
		progressBar.setValue(0);
	}

	private void taskSenderProgressChanged(int numberOfTasksCompleted) {
		int currentProgress = progressBar.getValue();
		int progressValue = numberOfTasksCompleted < progressBar.getMaximum() ? numberOfTasksCompleted : progressBar.getMaximum();
		for (int i = currentProgress; i <= progressValue; ++i) {
			progressBar.setValue(i);
		}

		long elapsed = System.currentTimeMillis() - startTime;
		lblProgress.setText(String.format("%s / %s", numberOfTasksCompleted, txtTemplatesCount.getText()));
		txtTimeElapsed.setText(String.format("%.2f s", (double) (elapsed / 1000)));

		long remaining = elapsed / numberOfTasksCompleted * (progressBar.getMaximum() - numberOfTasksCompleted);
		if (remaining / 1000 < 0) {
			remaining = 0;
		}

		long days = TimeUnit.MILLISECONDS.toDays(remaining);
		long hr = TimeUnit.MILLISECONDS.toHours(remaining - TimeUnit.DAYS.toMillis(days));
		long min = TimeUnit.MILLISECONDS.toMinutes(remaining - TimeUnit.DAYS.toMillis(days) - TimeUnit.HOURS.toMillis(hr));
		long sec = TimeUnit.MILLISECONDS.toSeconds(remaining - TimeUnit.DAYS.toMillis(days) - TimeUnit.HOURS.toMillis(hr) - TimeUnit.MINUTES.toMillis(min));
		lblRemaining.setText(String.format("Estimated time remaining: %02d.%02d:%02d:%02d", days, hr, min, sec));
	}

	private void taskSenderExceptionOccured(Exception e) {
		appendStatus(String.format("%s\r\n", e), Color.RED.darker());
	}

	private void taskSenderFinished() {
		enableControls(true);
		long elapsed = enrollmentTaskSender.getElapsedTime();

		long hr = TimeUnit.MILLISECONDS.toHours(elapsed);
		long min = TimeUnit.MILLISECONDS.toMinutes(elapsed - TimeUnit.HOURS.toMillis(hr));
		long sec = TimeUnit.MILLISECONDS.toSeconds(elapsed - TimeUnit.HOURS.toMillis(hr) - TimeUnit.MINUTES.toMillis(min));
		txtTimeElapsed.setText(String.format("%02d:%02d:%02d", hr, min, sec));
		lblRemaining.setText("");
		lblProgress.setText("");
		if (enrollmentTaskSender.isSuccessful()) {
			appendStatus("\r\nEnrollment successful", txtStatus.getForeground());
			lblStatusIcon.setIcon(iconOk);
		} else if (enrollmentTaskSender.isCanceled()) {
			appendStatus("\r\nEnrollment canceled", Color.RED.darker());
			lblStatusIcon.setIcon(iconError);
			btnStart.setEnabled(true);
			progressBar.setValue(0);
		} else {
			appendStatus("\r\nEnrollment finished with errors", txtStatus.getForeground());
			lblStatusIcon.setIcon(iconError);
		}
	}

	private void enableControls(boolean isIdle) {
		btnStart.setEnabled(isIdle);
		btnCancel.setEnabled(!isIdle);

		for (Component c : panelProperties.getComponents()) {
			c.setEnabled(isIdle);
		}
	}

	private void setStatus(String msg, Color color, Icon icon) {
		txtStatus.setForeground(color);
		txtStatus.setText(msg);
		lblStatusIcon.setIcon(icon);
	}

	private void appendStatus(String msg) {
		appendStatus(msg, Color.BLACK);
	}

	private void appendStatus(String msg, Color color) {
		txtStatus.setText(txtStatus.getText() + msg);
		txtStatus.setForeground(color);
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public String getTitle() {
		return "Enroll Templates";
	}

	@Override
	public boolean isBusy() {
		if (enrollmentTaskSender != null) {
			return enrollmentTaskSender.isBusy();
		}
		return false;
	}

	@Override
	public void cancel() {
		appendStatus("\r\nCanceling, please wait ...\r\n");
		enrollmentTaskSender.cancel();
		btnCancel.setEnabled(false);
	}

	@Override
	public void waitForCurrentProcessToFinish() throws InterruptedException, ExecutionException {
		enrollmentTaskSender.waitForCurrentProcessToFinish(getOwner());
	}

	// ==============================================
	// Event handling
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnStart) {
			startEnrolling();
		} else if (source == btnCancel) {
			cancel();
		}
	}

}
