package com.neurotec.samples.abis.subject.faces;

import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.util.NObjectCollection;

public class CaptureFaceModel extends CaptureBiometricModel<NFace> {

	public CaptureFaceModel(NSubject mainSubject, NSubject localSubject, AbisModel abisModel) {
		super(mainSubject, localSubject, abisModel);
	}

	@Override
	public NObjectCollection<NFace> getRelevantBiometricCollection() {
		return getLocalSubject().getFaces();
	}

}
