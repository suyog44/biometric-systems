package com.neurotec.samples.abis.subject.faces;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.samples.abis.subject.Source;

import java.awt.Color;
import java.io.File;
import java.util.EnumSet;
import java.util.List;

public interface CaptureFaceView {

	public static final Color COLOR_OK = new Color(0, 128, 0);
	public static final Color COLOR_ERROR = new Color(255, 0, 0);

	public enum Operation {
		NONE,
		DETECTING,
		EXTARCTING
	}

	public Source getSource();
	public File getFile();
	public EnumSet<NBiometricCaptureOption> getCaptureOptions();
	public void captureStarted();
	public void captureCompleted(NBiometricStatus status, NBiometricTask task);
	public void captureFailed(Throwable th, String msg);
	public void setCurrentOperation(Operation currentOperation);
	public boolean isGeneralize();
	public boolean isIcao();
	public void updateGeneralization(List<NBiometric> nowCapturing, List<NBiometric> generalized);
	public void setStatus(String status, Color color);
}
