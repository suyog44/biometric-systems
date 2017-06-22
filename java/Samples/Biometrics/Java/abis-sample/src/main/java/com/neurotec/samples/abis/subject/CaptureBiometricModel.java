package com.neurotec.samples.abis.subject;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.util.NObjectCollection;

import java.util.Iterator;

public abstract class CaptureBiometricModel<T extends NBiometric> extends DefaultBiometricModel {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final NSubject localSubject;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureBiometricModel(NSubject mainSubject, NSubject localSubject, AbisModel abisModel) {
		super(mainSubject, abisModel);
		this.localSubject = localSubject;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public final NSubject getLocalSubject() {
		return localSubject;
	}

	public void removeFailedBiometrics() {
		Iterator<T> it = getRelevantBiometricCollection().iterator();
		while (it.hasNext()) {
			T biometric = it.next();
			if (biometric.getStatus() != NBiometricStatus.OK) {
				it.remove();
			}
		}
	}

	public abstract NObjectCollection<T> getRelevantBiometricCollection();

}
