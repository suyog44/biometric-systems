package com.neurotec.samples.server;

import java.awt.Frame;
import java.util.EnumSet;
import java.util.List;
import java.util.concurrent.ExecutionException;

import javax.swing.SwingWorker;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.server.connection.TemplateLoader;
import com.neurotec.samples.server.controls.LongTaskDialog;
import com.neurotec.samples.server.settings.Settings;
import com.neurotec.util.concurrent.CompletionHandler;

public final class TaskSender {

	// ==============================================
	// Private fields
	// ==============================================

	private Object lock = new Object();
	private int tasksSentCount = 0;
	private int tasksCompletedCount = 0;

	private BackgroundWorker worker = null;
	private NBiometricOperation operation;
	private boolean isBackgroundWorkerCompleted;

	private long startTime = -1;
	private long stopTime = -1;

	private int bunchSize = 350;
	private int maxActiveTaskCount = 100;
	private boolean canceled;
	private boolean successful;
	private boolean sendOneBunchOnly;

	private TaskListener taskListener;

	private TemplateLoader templateLoader = null;
	private NBiometricClient biometricClient = null;
	private boolean isAccelerator;

	// ==============================================
	// Public constructor
	// ==============================================

	public TaskSender(NBiometricClient biometricClient, TemplateLoader templateLoader, NBiometricOperation operation) {
		if (biometricClient == null) throw new NullPointerException("biometricClient");
		if (templateLoader == null) throw new NullPointerException("templateLoader");
		this.templateLoader = templateLoader;
		this.biometricClient = biometricClient;
		this.operation = operation;
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void fireExceptionOccuredEvent(Exception e) {
		successful = false;
		taskListener.taskErrorOccured(e);
	}

	// ==============================================
	// Public methods
	// ==============================================

	public void setBunchSize(int bunchSize) {
		this.bunchSize = bunchSize;
	}

	public int getBunchSize() {
		return bunchSize;
	}

	public boolean isSuccessful() {
		return successful;
	}

	public boolean isCanceled() {
		return canceled;
	}

	public void addTaskListener(TaskListener l) {
		taskListener = l;
	}

	public void removeTaskListener(TaskListener l) {
		taskListener = null;
	}

	public boolean isBusy() {
		if (worker == null) {
			return false;
		}
		return (worker.getState() == SwingWorker.StateValue.STARTED || (tasksSentCount - tasksCompletedCount) > 0);
	}

	public void setSendOneBunchOnly(boolean sendOneBunchOnly) {
		this.sendOneBunchOnly = sendOneBunchOnly;
	}

	public boolean isSendOneBunchOnly() {
		return sendOneBunchOnly;
	}

	public int getPerformedTaskCount() {
		return tasksCompletedCount;
	}

	public long getElapsedTime() {
		if (startTime < 0) {
			return 0;
		}
		if (stopTime < 0) {
			return System.currentTimeMillis() - startTime;
		}
		return stopTime - startTime;
	}

	public final TemplateLoader getTemplateLoader() {
		return templateLoader;
	}

	public final void setTemplateLoader(TemplateLoader templateLoader) {
		this.templateLoader = templateLoader;
	}

	public final NBiometricClient getBiometricClient() {
		return biometricClient;
	}

	public final void setBiometricClient(NBiometricClient biometricClient) {
		this.biometricClient = biometricClient;
	}

	public final boolean isAccelerator() {
		return isAccelerator;
	}

	public final void setAccelerator(boolean value) {
		this.isAccelerator = value;
	}

	public void start() throws IllegalAccessException {
		if (isBusy()) {
			throw new IllegalAccessException("Already started");
		}
		Settings settings = Settings.getInstance();
		this.tasksSentCount = 0;
		this.tasksCompletedCount = 0;
		this.isBackgroundWorkerCompleted = false;
		this.successful = true;
		this.canceled = false;
		this.maxActiveTaskCount = isAccelerator() ? 1000 : 100;
		this.biometricClient.setMatchingThreshold(settings.getMatchingThreshold());
		this.worker = new BackgroundWorker();
		this.worker.execute();
	}

	public void cancel() {
		canceled = true;
		biometricClient.cancel();
		successful = false;
	}

	public void waitForCurrentProcessToFinish(Frame owner) {
		try {
			LongTaskDialog.runLongTask(owner, "Waiting to finish..", new WaitTask());
		} catch (InterruptedException e) {
			e.printStackTrace();
		} catch (ExecutionException e) {
			e.printStackTrace();
		}
	}

	// ==============================================
	// Private classes
	// ==============================================

	private class WaitTask implements LongTask {
		@Override
		public Object doInBackground() {
			try {
				worker.get();
				while (tasksSentCount > tasksCompletedCount) {
					Thread.sleep(200);
				}
			} catch (InterruptedException e) {
				e.printStackTrace();
			} catch (ExecutionException e) {
				e.printStackTrace();
			}
			return 0;
		}
	}

	private class BackgroundWorker extends SwingWorker<Boolean, Object> {

		// ==============================================
		// Package private constructor
		// ==============================================

		BackgroundWorker() {
		}

		// ==============================================
		// Overridden protected methods
		// ==============================================

		@Override
		protected Boolean doInBackground() {
			startTime = System.currentTimeMillis();
			stopTime = -1;
			try {
				templateLoader.beginLoad();
			} catch (Exception e) {
				publish(e);
				return null;
			}
			try {
				NSubject[] subjects;
				while (!canceled) {
					subjects = templateLoader.loadNext(bunchSize);
					if (subjects == null || subjects.length == 0) {
						break;
					}

					if (operation == NBiometricOperation.IDENTIFY) {
						for (NSubject subject : subjects) {
							while ((tasksSentCount - tasksCompletedCount) > maxActiveTaskCount && !canceled) {
								Thread.sleep(200);
							}
							if (canceled) {
								break;
							}
							NBiometricTask task = biometricClient.createTask(EnumSet.of(operation), subject);
							biometricClient.performTask(task, null, new TaskCompletionHandler());
							subject.dispose();
							task.dispose();
							synchronized (lock) {
								tasksSentCount++;
							}
						}
					} else {
						while ((tasksSentCount - tasksCompletedCount) > maxActiveTaskCount && !canceled) {
							Thread.sleep(200);
						}
						if (canceled) {
							break;
						}
						NBiometricTask task = biometricClient.createTask(EnumSet.of(operation), null);
						for (NSubject subject : subjects) {
							task.getSubjects().add(subject);
							subject.dispose();
						}
						biometricClient.performTask(task, null, new TaskCompletionHandler());
						task.dispose();
						synchronized (lock) {
							tasksSentCount += subjects.length;
						}
					}
					if (sendOneBunchOnly) {
						break;
					}
				}
				while (tasksCompletedCount < tasksSentCount) {
					Thread.sleep(200);
				}
				stopTime = System.currentTimeMillis();
				taskListener.taskFinished();
			} catch (Exception e) {
				e.printStackTrace();
				publish(e);
			}

			try {
				templateLoader.endLoad();
			} catch (Exception e) {
				e.printStackTrace();
				publish(e);
			}
			return true;
		}

		@Override
		protected void done() {
			super.done();
			isBackgroundWorkerCompleted = true;
		}

		@Override
		protected void process(List<Object> objects) {
			successful = false;
			if (objects.size() == 1 && objects.get(0) instanceof Exception) {
				fireExceptionOccuredEvent((Exception) objects.get(0));
			}
		}
	}

	private final class TaskCompletionHandler implements CompletionHandler<NBiometricTask, Object> {

		// ==============================================
		// Public methods
		// ==============================================

		@Override
		public void completed(NBiometricTask task, Object attachment) {
			synchronized (taskListener) {
				if (task.getError() != null) {
					successful = false;
					taskListener.taskErrorOccured(new Exception(task.getError()));
				} else {
					if (task.getOperations().contains(NBiometricOperation.IDENTIFY)) {
						if (task.getStatus() == NBiometricStatus.OK || task.getStatus() == NBiometricStatus.MATCH_NOT_FOUND) {
							try {
								taskListener.matchingTaskCompleted(task);
							} catch (Exception e) {
								e.printStackTrace();
							}
						}

					} else if (task.getOperations().contains(NBiometricOperation.ENROLL)) {
						switch (task.getStatus()) {
						case DUPLICATE_ID:
							successful = false;
							taskListener.taskErrorOccured(new Exception("Duplicate ID Found"));
							break;
						default:
						}
					}
				}
				tasksCompletedCount += task.getSubjects().size();
				taskListener.taskProgressChanged(tasksCompletedCount);
				task.dispose();
			}
		}

		@Override
		public void failed(Throwable exc, Object attachment) {
			synchronized (taskListener) {
				successful = false;
				tasksCompletedCount++;
				taskListener.taskProgressChanged(tasksCompletedCount);
				taskListener.taskErrorOccured((Exception) exc);
				if (tasksCompletedCount == tasksSentCount && isBackgroundWorkerCompleted) {
					stopTime = System.currentTimeMillis();
					taskListener.taskFinished();
				}
			}
		}
	}
}
