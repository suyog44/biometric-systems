package com.neurotec.samples.abis.subject.voices;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.samples.abis.subject.Source;

import java.io.File;
import java.util.EnumSet;

public interface CaptureVoiceView {

	Source getSource();
	EnumSet<NBiometricCaptureOption> getCaptureOptions();
	File getFile();
	Phrase getCurrentPhrase();
	void captureCompleted(NBiometricStatus status);
	void captureFailed(Throwable th, String msg);

}
