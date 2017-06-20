package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class VerifyPalm {

	private static final String DESCRIPTION = "Demonstrates verification of two palm images";
	private static final String NAME = "verify-palm";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [reference] [candidate]%n", NAME);
		System.out.println();
		System.out.println("\t[reference] - reference image");
		System.out.println("\t[candidate] - image to verify with");
	}

	public static void main(String[] args) {
		final String components = "Biometrics.PalmExtraction,Biometrics.PalmMatching";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject referenceSubject = null;
		NSubject candidateSubject = null;

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			// Create NBiometricClient
			biometricClient = new NBiometricClient();

			// Create subjects with face
			referenceSubject = createSubject(args[0], args[0]);
			candidateSubject = createSubject(args[1], args[1]);

			// Set matching threshold
			biometricClient.setMatchingThreshold(48);

			// Set matching speed
			biometricClient.setPalmsMatchingSpeed(NMatchingSpeed.LOW);

			// Verify subjects
			NBiometricStatus status = biometricClient.verify(referenceSubject, candidateSubject);

			if (status == NBiometricStatus.OK || status == NBiometricStatus.MATCH_NOT_FOUND) {
				int score = referenceSubject.getMatchingResults().get(0).getScore();
				System.out.format("image scored %d, verification.. ", score);
				if (status == NBiometricStatus.OK) {
					System.out.println("succeeded");
				} else {
					System.out.println("failed");
				}
			} else {
				System.out.format("Verification failed. Status: %s", status);
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
			if (referenceSubject != null) referenceSubject.dispose();
			if (candidateSubject != null) candidateSubject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}

	private static NSubject createSubject(String fileName, String subjectId) {
		NSubject subject = new NSubject();
		subject.setId(subjectId);
		NPalm palm = new NPalm();
		palm.setFileName(subjectId);
		subject.getPalms().add(palm);
		return subject;
	}

}
