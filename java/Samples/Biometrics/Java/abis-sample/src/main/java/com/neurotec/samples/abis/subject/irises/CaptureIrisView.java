package com.neurotec.samples.abis.subject.irises;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NEPosition;
import com.neurotec.samples.abis.subject.Source;

import java.io.File;
import java.util.EnumSet;

public interface CaptureIrisView {

	Source getSource();
	NEPosition getPosition();
	EnumSet<NBiometricCaptureOption> getCaptureOptions();
	File getFile();
	void captureCompleted(NBiometricStatus status);
	void captureFailed(Throwable th, String msg);

}
