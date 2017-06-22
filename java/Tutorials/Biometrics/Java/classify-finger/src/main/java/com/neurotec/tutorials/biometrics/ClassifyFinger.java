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

public final class ClassifyFinger {

	private static final String DESCRIPTION = "Demonstrates fingerprint classification.";
	private static final String NAME = "classify-finger";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [image]%n", NAME);
		System.out.println("");
		System.out.println("\t[image] - image of fingerprint to be classified.");
		System.out.println("");
	}

	public static void main(String[] args) {
		final String components = "Biometrics.FingerSegmentsDetection";

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
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();

			finger.setFileName(args[0]);
			subject.getFingers().add(finger);

			biometricClient.setFingersDeterminePatternClass(true);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.DETECT_SEGMENTS), subject);

			biometricClient.performTask(task);

			if (task.getStatus() == NBiometricStatus.OK) {
				subject.getFingers().get(1).getObjects().get(0).getPatternClass();
				System.out.format("Fingerprint pattern class is \"%s\", confidence %d\n", ((NFinger) finger.getObjects().get(0).getChild()).getObjects().get(0).getPatternClass(), ((NFinger) finger
						.getObjects().get(0).getChild()).getObjects().get(0).getPatternClassConfidence());
			} else {
				System.out.format("Classification failed. Status: %s\n", task.getStatus());
				Throwable error = task.getError();
				if (error != null) throw error;
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
