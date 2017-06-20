package com.neurotec.samples.server;

import java.util.EventListener;

import com.neurotec.biometrics.NBiometricTask;

public interface TaskListener extends EventListener {

	// ==============================================
	// Public methods
	// ==============================================

	void taskFinished();

	void taskErrorOccured(Exception e);

	void taskProgressChanged(int completed);

	void matchingTaskCompleted(NBiometricTask task);

}
