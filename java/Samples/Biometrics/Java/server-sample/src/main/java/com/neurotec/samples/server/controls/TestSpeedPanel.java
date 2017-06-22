package com.neurotec.samples.server.controls;

import java.awt.Color;
import java.awt.Component;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.text.DecimalFormat;
import java.text.NumberFormat;
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
import com.neurotec.samples.server.connection.AcceleratorConnection;
import com.neurotec.samples.server.util.GridBagUtils;
import com.neurotec.samples.server.util.MessageUtils;
import com.neurotec.samples.util.Utils;

public final class TestSpeedPanel extends BasePanel {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private TaskSender taskSender;
	private GridBagUtils gridBagUtils;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JButton btnStart;
	private JButton btnCancel;
	private JLabel lblCount;
	private JLabel lblRemaining;
	private JLabel lblStatusIcon;
	private JLabel lblTemplateInfo;
	private JLabel lblTemplatesOnAcc;
	private Icon iconOk;
	private Icon iconError;
	private JPanel panelProperties;
	private JProgressBar progressBar;
	private JSpinner spinnerMaxCount;
	private JTextField txtMatchedTemplatesCount;
	private JTextField txtTime;
	private JTextField txtDBSize;
	private JTextField txtSpeed;
	private JTextArea txtStatus;


	// ==============================================
	// Public constructor
	// ==============================================

	public TestSpeedPanel(Frame owner) {
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
				testSpeedPanelLoaded();
			}
		});
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		gridBagUtils = new GridBagUtils(GridBagConstraints.BOTH, new Insets(3, 3, 3, 3));

		GridBagLayout speedLayout = new GridBagLayout();
		speedLayout.columnWidths = new int[] { 75, 290, 160, 70 };
		speedLayout.rowHeights = new int[] { 25, 25, 25, 20, 25, 170 };
		setLayout(speedLayout);

		btnStart = new JButton("Start");
		btnStart.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setEnabled(false);
		btnCancel.addActionListener(this);

		initializePropertiesPanel();

		lblRemaining = new JLabel("Estimated time remaining:", SwingConstants.LEFT);
		lblCount = new JLabel("progress", SwingConstants.RIGHT);
		progressBar = new JProgressBar(0, 100);

		gridBagUtils.addToGridBagLayout(0, 0, this, btnStart);
		gridBagUtils.addToGridBagLayout(0, 1, this, btnCancel);
		gridBagUtils.addToGridBagLayout(1, 0, 1, 3, this, panelProperties);
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, 1, 0, this, new JLabel());
		gridBagUtils.addToGridBagLayout(0, 3, 1, 1, 0, 0, this, lblRemaining);
		gridBagUtils.addToGridBagLayout(3, 3, 1, 1, 0, 0, this, lblCount);
		gridBagUtils.addToGridBagLayout(0, 4, 4, 1, this, progressBar);
		gridBagUtils.addToGridBagLayout(0, 5, 4, 1, 0, 1, this, initializeResultsPanel());
	}

	private void initializePropertiesPanel() {
		panelProperties = new JPanel();
		panelProperties.setBorder(BorderFactory.createTitledBorder("Properties"));
		GridBagLayout propertiesPanelLayout = new GridBagLayout();
		propertiesPanelLayout.columnWidths = new int[] { 150, 100, 20 };
		panelProperties.setLayout(propertiesPanelLayout);

		spinnerMaxCount = new JSpinner(new SpinnerNumberModel(1000, 10, Integer.MAX_VALUE, 1));

		gridBagUtils.addToGridBagLayout(0, 2, panelProperties, new JLabel("Maximum templates to match:"));
		gridBagUtils.addToGridBagLayout(1, 2, panelProperties, spinnerMaxCount);
		gridBagUtils.addToGridBagLayout(2, 2, panelProperties, new JLabel("*"));
		gridBagUtils.addToGridBagLayout(0, 3, 3, 1, panelProperties, new JLabel("*- All templates should be able to fit into memory at once"));

		gridBagUtils.clearGridBagConstraints();
	}

	private JPanel initializeResultsPanel() {
		JPanel resultsPanel = new JPanel();
		resultsPanel.setBorder(BorderFactory.createTitledBorder("Results"));
		GridBagLayout resultsPanelLayout = new GridBagLayout();
		resultsPanelLayout.columnWidths = new int[] { 55, 50, 110, 130, 110, 110 };
		resultsPanelLayout.rowHeights = new int[] { 25, 25, 25, 50, 35 };
		resultsPanel.setLayout(resultsPanelLayout);

		txtMatchedTemplatesCount = new JTextField("N/A");
		txtMatchedTemplatesCount.setEditable(false);

		txtTime = new JTextField("N/A");
		txtTime.setEditable(false);

		txtDBSize = new JTextField("N/A");
		txtDBSize.setEditable(false);

		txtSpeed = new JTextField("N/A");
		txtSpeed.setEditable(false);

		lblStatusIcon = new JLabel();
		txtStatus = new JTextArea();
		iconOk = Utils.createIcon("images/ok.png");
		iconError = Utils.createIcon("images/error.png");
		JScrollPane txtStatusScrollPane = new JScrollPane(txtStatus, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);

		lblTemplateInfo = new JLabel("* - server template count is assumed to be equal to DB template count");
		lblTemplatesOnAcc = new JLabel("Templates on accelerator:");
		gridBagUtils.addToGridBagLayout(0, 0, 2, 1, resultsPanel, new JLabel("Templates matched:"));
		gridBagUtils.addToGridBagLayout(2, 0, 1, 1, resultsPanel, txtMatchedTemplatesCount);
		gridBagUtils.addToGridBagLayout(3, 0, resultsPanel, lblTemplatesOnAcc);
		gridBagUtils.addToGridBagLayout(4, 0, resultsPanel, txtDBSize);
		gridBagUtils.addToGridBagLayout(0, 1, 2, 1, resultsPanel, new JLabel("Time elapsed:"));
		gridBagUtils.addToGridBagLayout(2, 1, 1, 1, resultsPanel, txtTime);
		gridBagUtils.addToGridBagLayout(3, 1, resultsPanel, new JLabel("Speed:"));
		gridBagUtils.addToGridBagLayout(4, 1, resultsPanel, txtSpeed);
		gridBagUtils.addToGridBagLayout(1, 2, 3, 1, resultsPanel, lblTemplateInfo);
		gridBagUtils.addToGridBagLayout(5, 1, 1, 1, 1, 0, resultsPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(0, 3, 1, 1, 0, 0, resultsPanel, lblStatusIcon);
		gridBagUtils.addToGridBagLayout(0, 4, 1, 1, 0, 1, resultsPanel, new JLabel());
		gridBagUtils.addToGridBagLayout(1, 3, 5, 2, 0, 0, resultsPanel, txtStatusScrollPane);
		gridBagUtils.clearGridBagConstraints();
		return resultsPanel;
	}

	private void startSpeedTest() {
		boolean isAccelerator = getAccelerator() != null;
		boolean supportsGetCount = getBiometricClient().getRemoteConnections().get(0).getOperations().contains(NBiometricOperation.GET_COUNT);
		try {
			if (isBusy()) return;

			enableControls(false);
			txtSpeed.setText("N/A");
			txtTime.setText("N/A");
			txtMatchedTemplatesCount.setText("N/A");
			setStatus("Preparing ...", Color.BLACK, null);
			lblCount.setText("");
			progressBar.setValue(0);

			int maxCount = (Integer) spinnerMaxCount.getValue();
			int loaderTemplateCount = getTemplateCount();
			progressBar.setMaximum(maxCount > loaderTemplateCount ? loaderTemplateCount : maxCount);
			lblStatusIcon.setIcon(null);

			// if speed is counted not on MegaMatcher Accelerator and server does not support get count operation 
			// servers DB size is assumed to be equal to probe template databases size
			int templateCount = isAccelerator ? getAccelerator().getDbSize() : supportsGetCount ? getBiometricClient().getCount() : loaderTemplateCount;
			txtDBSize.setText(String.valueOf(templateCount));

			taskSender.setBunchSize(maxCount);
			taskSender.setSendOneBunchOnly(true);
			taskSender.setTemplateLoader(getTemplateLoader());
			taskSender.setAccelerator(isAccelerator);
			taskSender.setBiometricClient(getBiometricClient());
			taskSender.start();
		} catch (Exception e) {
			MessageUtils.showError(this, e);
			setStatus("Testing speed failed due to: " + e.getMessage(), Color.RED.darker(), iconError);
			enableControls(true);
		}
	}

	private void testSpeedPanelLoaded() {
		try {
			taskSender = new TaskSender(getBiometricClient(), getTemplateLoader(), NBiometricOperation.IDENTIFY);
			taskSender.addTaskListener(new TaskListener() {
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
			});
			lblCount.setText("");
			lblRemaining.setText("");
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	private void enableControls(boolean isIdle) {
		btnStart.setEnabled(isIdle);
		setPropertiesPanelEnabled(isIdle);
		btnCancel.setEnabled(!isIdle);
	}

	private void setPropertiesPanelEnabled(boolean enabled) {
		for (Component c : panelProperties.getComponents()) {
			c.setEnabled(enabled);
		}
	}

	private void taskSenderProgressChanged(int templatesMatched) {
		if (templatesMatched == 1) {
			setStatus("Matching templates ...", Color.BLACK, null);
		}

		txtMatchedTemplatesCount.setText(String.valueOf(templatesMatched));

		int dbSize = Integer.valueOf(txtDBSize.getText());
		long timeElapsed = taskSender.getElapsedTime();
		double timeElapsedSec = (double) timeElapsed / 1000;
		double speed = dbSize * templatesMatched / timeElapsedSec;

		DecimalFormat df = (DecimalFormat)NumberFormat.getInstance();
		df.applyPattern("###,###.##");
		txtSpeed.setText(df.format(speed));
		txtTime.setText(String.format("%.2f s", timeElapsedSec));

		int maxCount = (Integer) spinnerMaxCount.getValue();
		long remaining = Math.round(timeElapsed / templatesMatched * (maxCount - templatesMatched));
		long hr = TimeUnit.MILLISECONDS.toHours(remaining);
		long min = TimeUnit.MILLISECONDS.toMinutes(remaining - TimeUnit.HOURS.toMillis(hr));
		long sec = TimeUnit.MILLISECONDS.toSeconds(remaining - TimeUnit.HOURS.toMillis(hr) - TimeUnit.MINUTES.toMillis(min));
		lblRemaining.setText(String.format("Estimated time remaining: %02d:%02d:%02d", hr, min, sec));

		progressBar.setValue(templatesMatched);
		lblCount.setText(String.format("%s / %s", templatesMatched, progressBar.getMaximum()));
	}

	private void taskSenderFinished() {
		enableControls(true);
		if (taskSender.isCanceled()) {
			appendStatus("Speed test canceled\r\n", Color.RED);
			lblStatusIcon.setIcon(iconError);
			btnStart.setEnabled(true);
			progressBar.setValue(0);
			return;
		}

		if (taskSender.isSuccessful()) {
			txtMatchedTemplatesCount.setText(String.valueOf(taskSender.getPerformedTaskCount()));
			setStatus(
					String.format("Speed: %s templates per second.\nTotal of %s templates were sent and matched against %s templates per %s seconds.",
							txtSpeed.getText(), txtMatchedTemplatesCount.getText(), txtDBSize.getText(), txtTime.getText()),
					Color.BLACK, iconOk);
		} else {
			appendStatus("\r\nOperation completed with errors\r\n", Color.BLACK);
			lblStatusIcon.setIcon(iconError);
		}
		progressBar.setValue(progressBar.getMaximum());
	}

	private void taskSenderExceptionOccured(Exception e) {
		appendStatus(String.format("%s\r\n", e), Color.RED.darker());
	}

	private void setStatus(String msg, Color color, Icon icon) {
		txtStatus.setForeground(color);
		txtStatus.setText(msg);
		lblStatusIcon.setIcon(icon);
	}

	private void appendStatus(String msg, Color color) {
		txtStatus.setText(txtStatus.getText() + msg);
		txtStatus.setForeground(color);
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void setAccelerator(AcceleratorConnection value) {
		super.setAccelerator(value);
		if (isVisible()) {
			boolean isAccelerator = value != null;
			boolean supportsGetCount = getBiometricClient().getRemoteConnections().get(0).getOperations().contains(NBiometricOperation.GET_COUNT);
			lblTemplateInfo.setVisible(!(isAccelerator || supportsGetCount));
			lblTemplatesOnAcc.setText(String.format("Templates on %s:", isAccelerator ? "accelerator" : supportsGetCount ? "server" : "server*"));
		}

	}

	@Override
	public String getTitle() {
		return "Test matching speed";
	}

	@Override
	public boolean isBusy() {
		if (taskSender != null) {
			return taskSender.isBusy();
		}
		return false;
	}

	@Override
	public void cancel() {
		taskSender.cancel();
		setStatus("Canceling, please wait ...\r\n", Color.BLACK, null);
		btnCancel.setEnabled(false);
	}

	@Override
	public void waitForCurrentProcessToFinish() throws InterruptedException, ExecutionException {
		taskSender.waitForCurrentProcessToFinish(getOwner());
	}

	// ==============================================
	// Event handling
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnStart) {
			startSpeedTest();
		} else if (source == btnCancel) {
			cancel();
		}
	}
}
