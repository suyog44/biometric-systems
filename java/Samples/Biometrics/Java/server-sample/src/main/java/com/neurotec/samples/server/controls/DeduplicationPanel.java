package com.neurotec.samples.server.controls;

import java.awt.Color;
import java.awt.Component;
import java.awt.Frame;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Insets;
import java.awt.event.ActionEvent;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;

import javax.swing.BorderFactory;
import javax.swing.Icon;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.event.AncestorEvent;
import javax.swing.event.AncestorListener;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NEMatchingDetails;
import com.neurotec.biometrics.NFMatchingDetails;
import com.neurotec.biometrics.NLMatchingDetails;
import com.neurotec.biometrics.NMatchingDetails;
import com.neurotec.biometrics.NMatchingDetails.FaceCollection;
import com.neurotec.biometrics.NMatchingDetails.FingerCollection;
import com.neurotec.biometrics.NMatchingDetails.IrisCollection;
import com.neurotec.biometrics.NMatchingDetails.PalmCollection;
import com.neurotec.biometrics.NMatchingDetails.VoiceCollection;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSMatchingDetails;
import com.neurotec.biometrics.NSubject;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.server.TaskListener;
import com.neurotec.samples.server.TaskSender;
import com.neurotec.samples.server.util.GridBagUtils;
import com.neurotec.samples.server.util.MessageUtils;
import com.neurotec.samples.util.Utils;

public final class DeduplicationPanel extends BasePanel {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private TaskSender deduplicationTaskSender;
	private long startTime;
	private String resultsFilePath = System.getProperty("user.dir") + File.separator + "results.csv";;
	private GridBagUtils gridBagUtils;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JButton btnStart;
	private JButton btnCancel;
	private JButton btnBrowseResultFile;
	private Icon iconOk;
	private Icon iconError;
	private JLabel lblProgress;
	private JLabel lblRemaining;
	private JLabel lblStatusIcon;
	private JPanel panelProperties;
	private JProgressBar progressBar;
	private JTextField txtResultFilePath;
	private JTextArea txtStatus;
	private JFileChooser openFileDialog;

	// ==============================================
	// Public constructor
	// ==============================================

	public DeduplicationPanel(Frame owner) {
		super(owner);
		initializeComponents();
		openFileDialog = new JFileChooser(resultsFilePath);
		addAncestorListener(new AncestorListener() {
			@Override
			public void ancestorRemoved(AncestorEvent event) {
			}
			@Override
			public void ancestorMoved(AncestorEvent event) {
			}
			@Override
			public void ancestorAdded(AncestorEvent event) {
				deduplicationPanelLoaded();
			}
		});
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		gridBagUtils = new GridBagUtils(GridBagConstraints.BOTH, new Insets(3, 3, 3, 3));
		GridBagLayout duplicationLayout = new GridBagLayout();
		duplicationLayout.columnWidths = new int[] { 75, 70, 400, 30, 50 };
		duplicationLayout.rowHeights = new int[] { 25, 25, 20, 25, 50, 150 };
		setLayout(duplicationLayout);

		btnStart = new JButton("Start");
		btnStart.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setEnabled(false);
		btnCancel.addActionListener(this);

		initializePropertiesPanel();

		lblRemaining = new JLabel("Estimated time remaining:");
		lblProgress = new JLabel("progress label", SwingConstants.RIGHT);
		progressBar = new JProgressBar(0, 100);
		lblStatusIcon = new JLabel();
		lblStatusIcon.setHorizontalAlignment(SwingConstants.CENTER);
		txtStatus = new JTextArea();

		iconOk = Utils.createIcon("images/ok.png");
		iconError = Utils.createIcon("images/error.png");
		JScrollPane txtStatusScrollPane = new JScrollPane(txtStatus, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);

		gridBagUtils.addToGridBagLayout(0, 0, this, btnStart);
		gridBagUtils.addToGridBagLayout(0, 1, this, btnCancel);
		gridBagUtils.addToGridBagLayout(1, 0, 2, 2, this, panelProperties);
		gridBagUtils.addToGridBagLayout(0, 2, 3, 1, this, lblRemaining);
		gridBagUtils.addToGridBagLayout(3, 2, 1, 1, 1, 0, this, new JLabel());
		gridBagUtils.addToGridBagLayout(4, 2, 1, 1, 0, 0, this, lblProgress);
		gridBagUtils.addToGridBagLayout(0, 3, 5, 1, this, progressBar);
		gridBagUtils.addToGridBagLayout(0, 4, 1, 1, this, lblStatusIcon);
		gridBagUtils.addToGridBagLayout(0, 5, 1, 1, 0, 1, this, new JLabel());
		gridBagUtils.addToGridBagLayout(1, 4, 4, 2, 0, 0, this, txtStatusScrollPane);
		gridBagUtils.clearGridBagConstraints();
	}

	private void initializePropertiesPanel() {
		panelProperties = new JPanel();
		panelProperties.setBorder(BorderFactory.createTitledBorder("Properties"));
		GridBagLayout propertiesPanelLayout = new GridBagLayout();
		propertiesPanelLayout.columnWidths = new int[] { 125, 75, 265, 40 };
		panelProperties.setLayout(propertiesPanelLayout);

		txtResultFilePath = new JTextField("results.csv");
		btnBrowseResultFile = new JButton("...");
		btnBrowseResultFile.addActionListener(this);

		gridBagUtils.addToGridBagLayout(0, 2, panelProperties, new JLabel("Duplication results file:"));
		gridBagUtils.addToGridBagLayout(1, 2, 2, 1, panelProperties, txtResultFilePath);
		gridBagUtils.addToGridBagLayout(3, 2, 1, 1, panelProperties, btnBrowseResultFile);
		gridBagUtils.clearGridBagConstraints();
	}

	private void setPropertiesPanelEnabled(boolean enabled) {
		for (Component c : panelProperties.getComponents()) {
			c.setEnabled(enabled);
		}
	}

	private void enableControls(boolean isIdle) {
		btnStart.setEnabled(isIdle);
		btnCancel.setEnabled(!isIdle);
		setPropertiesPanelEnabled(isIdle);
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

	private void writeLogHeader() {
		StringBuilder headerBuilder = new StringBuilder();
		headerBuilder.append("TemplateId,MatchedWith,Score,FingersScore,FingersScores,IrisesScore,IrisesScores");
		if (getAccelerator() != null) {
			headerBuilder.append(",FacesScore,FacesScores,VoicesScore,VoicesScores,PalmsScore,PalmsScores");
		}
		headerBuilder.append("\n");

		FileWriter fw = null;
		BufferedWriter writer = null;
		try {
			fw = new FileWriter(new File(resultsFilePath));
			writer = new BufferedWriter(fw);
			writer.write(headerBuilder.toString());

		} catch (IOException e) {
			appendStatus(String.format("%s\r\n", e), Color.RED.darker());
		} finally {
			if (writer != null) {
				try {
					writer.close();
				} catch (IOException e) {
					e.printStackTrace();
					MessageUtils.showError(this, e);
				}
			}
			if (fw != null) {
				try {
					fw.close();
				} catch (IOException e) {
					e.printStackTrace();
					MessageUtils.showError(this, e);
				}
			}
		}
	}

	private void matchingTasksCompleted(NBiometricTask task) {
		try {
			final StringBuilder builder = new StringBuilder();
			for (NSubject subject : task.getSubjects()) {
				if (subject.getMatchingResults() != null && subject.getMatchingResults().size() > 0) {
					for (NMatchingResult item : subject.getMatchingResults()) {
						NBuffer buffer = item.getMatchingDetailsBuffer();
						NMatchingDetails details = new NMatchingDetails(buffer);

						try {
							builder.append(String.format("%s,%s,%d", subject.getId(), item.getId(), item.getScore()));

							builder.append(String.format(",%d,", details.getFingersScore()));
							FingerCollection fingers = details.getFingers();
							for (NFMatchingDetails finger : fingers) {
								try {
									builder.append(String.format("%d;", finger.getScore()));
								} finally {
									finger.dispose();
								}
							}

							builder.append(String.format(",%d,", details.getIrisesScore()));
							IrisCollection irises = details.getIrises();
							for (NEMatchingDetails iris : irises) {
								try {
									builder.append(String.format("%d;", iris.getScore()));
								} finally {
									iris.dispose();
								}
							}

							builder.append(String.format(",%d,", details.getFacesScore()));
							FaceCollection faces = details.getFaces();
							for (NLMatchingDetails face : faces) {
								try {
									builder.append(String.format("%d;", face.getScore()));
								} finally {
									face.dispose();
								}
							}

							builder.append(String.format(",%d,", details.getVoicesScore()));
							VoiceCollection voices = details.getVoices();
							for (NSMatchingDetails voice : voices) {
								try {
									builder.append(String.format("%d;", voice.getScore()));
								} finally {
									voice.dispose();
								}
							}

							builder.append(String.format(",%d,", details.getPalmsScore()));
							PalmCollection palms = details.getPalms();
							for (NFMatchingDetails palm : palms) {
								try {
									builder.append(String.format("%d;", palm.getScore()));
								} finally {
									palm.dispose();
								}
							}
							builder.append("\n");
						} finally {
							details.dispose();
						}
					}
				} else {
					builder.append(String.format("%s,NoMatches", subject.getId()));
					builder.append("\n");
				}
			}

			FileWriter fw = null;
			BufferedWriter writer = null;
			try {
				fw = new FileWriter(new File(resultsFilePath), true);
				writer = new BufferedWriter(fw);
				writer.write(builder.toString());
				writer.flush();
			} finally {
				if (writer != null) {
					writer.close();
				}
				if (fw != null) {
					fw.close();
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
			appendStatus(String.format("%s\r\n", e), Color.RED.darker());
		}
	}

	private void taskSenderExceptionOccured(Exception ex) {
		StringBuilder stacktrace = new StringBuilder();
		StackTraceElement[] elements = ex.getStackTrace();
		for (StackTraceElement stackTraceElement : elements) {
			stacktrace.append(stackTraceElement.toString() + "\r\n");
		}
		appendStatus(String.format("%s\r\n", ex.getMessage()), Color.RED.darker());
		appendStatus(String.format("%s\r\n", stacktrace), Color.RED.darker());
	}

	private void taskSenderFinished() {
		enableControls(true);
		lblRemaining.setText("");
		if (deduplicationTaskSender.isSuccessful() && !deduplicationTaskSender.isCanceled()) {
			appendStatus("Deduplication completed without errors", Color.BLACK);
			lblStatusIcon.setIcon(iconOk);
		} else {
			appendStatus((deduplicationTaskSender.isCanceled() ? "Deduplication canceled." : "There were errors during deduplication"), Color.RED.darker());
			btnStart.setEnabled(true);
			lblStatusIcon.setIcon(iconError);
			progressBar.setValue(0);
		}
	}

	private void taskSenderProgressChanged(int numberOfTasksCompleted) {
		if (numberOfTasksCompleted == 1) {
			setStatus("Matching templates ...\r\n", Color.BLACK, null);
		}
		if (numberOfTasksCompleted % 10 == 0) {
			long remaining = (System.currentTimeMillis() - startTime) / numberOfTasksCompleted * (progressBar.getMaximum() - numberOfTasksCompleted);
			if (remaining / 1000 < 0) {
				remaining = 0;
			}
			long days = TimeUnit.MILLISECONDS.toDays(remaining);
			long hr = TimeUnit.MILLISECONDS.toHours(remaining - TimeUnit.DAYS.toMillis(days));
			long min = TimeUnit.MILLISECONDS.toMinutes(remaining - TimeUnit.DAYS.toMillis(days) - TimeUnit.HOURS.toMillis(hr));
			long sec = TimeUnit.MILLISECONDS.toSeconds(remaining - TimeUnit.DAYS.toMillis(days) - TimeUnit.HOURS.toMillis(hr) - TimeUnit.MINUTES.toMillis(min));
			lblRemaining.setText(String.format("Estimated time remaining: %02d.%02d:%02d:%02d", days, hr, min, sec));
		}
		if (numberOfTasksCompleted > progressBar.getMaximum()) {
			progressBar.setValue(progressBar.getMaximum());
		} else {
			progressBar.setValue(numberOfTasksCompleted);
		}
		lblProgress.setText(String.format("%s / %s", numberOfTasksCompleted, progressBar.getMaximum()));
	}

	private void startDeduplication() {
		try {
			if (isBusy()) {
				return;
			}
			setStatus("Preparing ...", Color.BLACK, null);
			lblProgress.setText("");
			lblRemaining.setText("");

			resultsFilePath = txtResultFilePath.getText().trim();
			if (resultsFilePath == null || resultsFilePath.isEmpty()) {
				resultsFilePath = System.getProperty("user.dir") + File.separator + "results.csv";
				txtResultFilePath.setText(resultsFilePath);
			}
			writeLogHeader();

			progressBar.setValue(0);
			int templateCount = getTemplateCount();
			progressBar.setMaximum(templateCount);

			getBiometricClient().setMatchingWithDetails(true);
			deduplicationTaskSender.setBunchSize(350);
			deduplicationTaskSender.setBiometricClient(getBiometricClient());
			deduplicationTaskSender.setTemplateLoader(getTemplateLoader());
			deduplicationTaskSender.setAccelerator(getAccelerator() != null);

			startTime = System.currentTimeMillis();
			deduplicationTaskSender.start();
			enableControls(false);
		} catch (Exception e) {
			e.printStackTrace();
			MessageUtils.showError(this, e);
			setStatus("Deduplication failed due to: " + e.toString(), Color.RED.darker(), iconError);
		}
	}

	private void deduplicationPanelLoaded() {
		try {
			deduplicationTaskSender = new TaskSender(getBiometricClient(), getTemplateLoader(), NBiometricOperation.IDENTIFY);
			deduplicationTaskSender.addTaskListener(new TaskListener() {
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
					matchingTasksCompleted(task);
				}
			});
			lblProgress.setText("");
			lblRemaining.setText("");
		} catch (Exception e) {
			e.printStackTrace();
			MessageUtils.showError(this, e);
		}
	}

	private void browseForResultFile() {
		if (openFileDialog.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			txtResultFilePath.setText(openFileDialog.getSelectedFile().getPath());
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void cancel() {
		appendStatus("\r\nCanceling, please wait ...\r\n", Color.BLACK);
		deduplicationTaskSender.cancel();
		btnCancel.setEnabled(false);

	}

	@Override
	public boolean isBusy() {
		if (deduplicationTaskSender != null) {
			return deduplicationTaskSender.isBusy();
		}
		return false;
	}

	@Override
	public void waitForCurrentProcessToFinish() throws InterruptedException, ExecutionException {
		deduplicationTaskSender.waitForCurrentProcessToFinish(getOwner());
	}

	@Override
	public String getTitle() {
		return "Deduplication";
	}

	// ==============================================
	// Event Handling
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnStart) {
			startDeduplication();
		} else if (source == btnCancel) {
			cancel();
		} else if (source == btnBrowseResultFile) {
			browseForResultFile();
		}
	}
}
