package com.neurotec.samples.faceverification;

import com.neurotec.face.verification.NFaceVerification;
import com.neurotec.samples.faceverification.utils.Utils;

public final class NFV {

	private static NFaceVerification instance;

	protected NFV() {
	}

	public static synchronized NFaceVerification getInstance() {
		if (instance == null) {
			instance = new NFaceVerification(Utils.combinePath(Utils.NEUROTECHNOLOGY_DIRECTORY, "face_database.db"), "database_password");
		}
		return instance;
	}

	public static synchronized void dispose() {
		if (instance != null) {
			instance.dispose();
			instance = null;
		}
	}


}
