package com.neurotec.samples.abis.subject;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricStatus;

public class CaptureErrorHandler {

	private final CaptureBiometricModel<? extends NBiometric> model;

	public CaptureErrorHandler(CaptureBiometricModel<? extends NBiometric> model) {
		this.model = model;
	}

	public void handleError(Throwable error, NBiometricStatus status) {
		if ((error != null) && (error.getMessage() != null) && error.getMessage().contains("OutOfMemoryError")) {
			if (!model.getRelevantBiometricCollection().isEmpty()) {
				NBiometric last = model.getRelevantBiometricCollection().get(model.getRelevantBiometricCollection().size() - 1);
				model.getRelevantBiometricCollection().remove(last);
			}
		}
		if (status != NBiometricStatus.OK) {
			model.removeFailedBiometrics();
		}
	}

}
