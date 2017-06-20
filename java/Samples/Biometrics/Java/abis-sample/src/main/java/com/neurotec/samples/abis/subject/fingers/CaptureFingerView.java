package com.neurotec.samples.abis.subject.fingers;

import java.io.File;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.samples.abis.subject.Scenario;
import com.neurotec.samples.abis.subject.Source;

public interface CaptureFingerView {

	public Scenario getScenario();
	public NFPosition getPosition();
	public NFImpressionType getImpressionType();
	public Source getSource();
	public EnumSet<NBiometricCaptureOption> getCaptureOptions();
	public File getFile();
	public File[] getFiles();
	public void captureCompleted(NBiometricStatus status, NBiometricTask task);
	public void captureFailed(Throwable th, String msg);
	public void progress(NBiometricStatus status, String msg);

	public boolean isGeneralize();
	public void updateGeneralization(List<NBiometric> nowCapturing, List<NBiometric> generalized);
}
