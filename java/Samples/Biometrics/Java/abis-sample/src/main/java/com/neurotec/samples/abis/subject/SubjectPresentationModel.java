package com.neurotec.samples.abis.subject;

import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricType;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;

public interface SubjectPresentationModel extends PageNavigationController {

	String getPresentationTitle();
	Object getSelectedSubjectElement();
	Page getShownPage();
	EnumSet<NBiometricType> getAllowedNewTypes();
	void dispose();

}
