package com.neurotec.samples.abis.subject;

import java.io.File;
import java.io.IOException;

import com.neurotec.images.NImage;
import com.neurotec.util.NPropertyBag;

public interface BiometricController {

	public enum TenPrintCardType {
		FINGER_FRAME,
		CRIMINAL,
		APPLICANT
	}

	void setID(String id);
	void setThumbnail(NImage thumbnail);
	void resetProperties(NPropertyBag properties);
	void prepareEnrollData();

	void enroll(boolean checkDuplicates);
	void identify();
	void identify(String query);
	void verify();
	void update();
	void saveTemplate(File file) throws IOException;
	void printTenPrintCard(TenPrintCardType type);

	void forceStart();
	void force();
	void repeat();
	void skip();
	void cancel();

}
