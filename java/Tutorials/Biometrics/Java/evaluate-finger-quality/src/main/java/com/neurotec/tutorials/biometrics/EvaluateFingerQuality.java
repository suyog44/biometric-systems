package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class EvaluateFingerQuality {

	private static final String DESCRIPTION = "Demonstrates fingerprint image quality evaluation.";
	private static final String NAME = "evaluate-finger-quality";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [image]%n", NAME);
		System.out.println("");
		System.out.println("\t[image] - image of fingerprint to be evaluated.");
		System.out.println("");
	}

	public static void main(String[] args) {
		final String components = "Biometrics.FingerQualityAssessmentBase";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFinger finger = null;
		NBiometricTask task = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();

			finger.setFileName(args[0]);

			subject.getFingers().add(finger);

			biometricClient.setFingersCalculateNFIQ(true);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.ASSESS_QUALITY), subject);

			biometricClient.performTask(task);

			if (task.getStatus() == NBiometricStatus.OK) {
				System.out.format("Finger quality is: %s\n", subject.getFingers().get(0).getObjects().get(0).getNFIQQuality());
			} else {
				System.out.format("Quality assesment failed: %s\n", task.getStatus());
				if (task.getError() != null) throw task.getError();
				System.exit(-1);
			}

		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (task != null) task.dispose();
			if (finger != null) finger.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
