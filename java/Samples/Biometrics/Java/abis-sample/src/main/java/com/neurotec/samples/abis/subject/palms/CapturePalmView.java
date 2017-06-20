package com.neurotec.samples.abis.subject.palms;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.samples.abis.subject.Source;

import java.io.File;
import java.util.EnumSet;
import java.util.List;

public interface CapturePalmView {

	public Source getSource();
	public NFPosition getPosition();
	public NFImpressionType getImpressionType();
	public EnumSet<NBiometricCaptureOption> getCaptureOptions();
	public File getFile();
	public void captureCompleted(NBiometricStatus status, NBiometricTask task);
	public void captureFailed(Throwable th, String msg);
	public boolean isGeneralize();
	public void updateGeneralization(List<NBiometric> nowCapturing, List<NBiometric> generalized);

}
