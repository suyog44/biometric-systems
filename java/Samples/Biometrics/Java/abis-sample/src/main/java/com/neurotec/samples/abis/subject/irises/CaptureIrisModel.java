package com.neurotec.samples.abis.subject.irises;

import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.util.NObjectCollection;

public class CaptureIrisModel extends CaptureBiometricModel<NIris> {

	public CaptureIrisModel(NSubject mainSubject, NSubject localSubject, AbisModel abisModel) {
		super(mainSubject, localSubject, abisModel);
	}

	@Override
	public NObjectCollection<NIris> getRelevantBiometricCollection() {
		return getLocalSubject().getIrises();
	}

}
