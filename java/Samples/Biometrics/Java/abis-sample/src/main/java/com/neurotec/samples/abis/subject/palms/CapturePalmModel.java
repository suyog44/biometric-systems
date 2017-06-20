package com.neurotec.samples.abis.subject.palms;

import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.util.NObjectCollection;

import java.util.ArrayList;
import java.util.List;

public class CapturePalmModel extends CaptureBiometricModel<NPalm> {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final List<NFImpressionType> PALM_IMPRESSION_TYPES;

	// ===========================================================
	// Static constructor
	// ===========================================================

	static {
		PALM_IMPRESSION_TYPES = new ArrayList<NFImpressionType>();
		for (NFImpressionType impression : NFImpressionType.values()) {
			if (impression.isPalm()) {
				PALM_IMPRESSION_TYPES.add(impression);
			}
		}
	}

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CapturePalmModel(NSubject mainSubject, NSubject localSubject, AbisModel abisModel) {
		super(mainSubject, localSubject, abisModel);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public List<NFImpressionType> getPalmImpressionTypes() {
		return new ArrayList<NFImpressionType>(PALM_IMPRESSION_TYPES);
	}

	@Override
	public NObjectCollection<NPalm> getRelevantBiometricCollection() {
		return getLocalSubject().getPalms();
	}

}
