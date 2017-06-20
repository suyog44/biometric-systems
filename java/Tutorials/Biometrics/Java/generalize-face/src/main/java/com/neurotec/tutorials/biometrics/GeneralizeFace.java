package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class GeneralizeFace {
	private static final String DESCRIPTION = "Demonstrates template creation and generalization of multiple faces";
	private static final String NAME = "generalize-face";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("%s [output template] [multiple face images]%n", NAME);
		System.out.format("\texample %s template image1.png image2.png image3.png%n", NAME);
		System.out.println();
	}

	public static void main(String[] args) {
		String components = "Biometrics.FaceExtraction";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();

			for (int i = 1; i < args.length; i++) {
				NFace face = new NFace();
				// Set file name with face image
				face.setFileName(args[i]);
				face.setSessionId(1);
				subject.getFaces().add(face);
			}

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (status != NBiometricStatus.OK) {
				System.out.format("Failed to create or generalize templates. Status: %s.%n", status);
				System.exit(-1);
			}

			System.out.println("Generalization completed successfully");
			System.out.format("Saving template to '%s' ... %n", args[0]);
			NFile.writeAllBytes(args[0], subject.getTemplateBuffer());
			System.out.println("done");
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
