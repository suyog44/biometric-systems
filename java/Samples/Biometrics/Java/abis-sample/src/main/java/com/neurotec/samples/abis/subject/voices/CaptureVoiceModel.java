package com.neurotec.samples.abis.subject.voices;

import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.util.NObjectCollection;

public class CaptureVoiceModel extends CaptureBiometricModel<NVoice> {

	public CaptureVoiceModel(NSubject mainSubject, NSubject localSubject, AbisModel abisModel) {
		super(mainSubject, localSubject, abisModel);
	}

	@Override
	public NObjectCollection<NVoice> getRelevantBiometricCollection() {
		return getLocalSubject().getVoices();
	}

}
