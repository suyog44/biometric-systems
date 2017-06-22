package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;
import com.sun.jna.Platform;

public final class EnrollIrisFromImage {
	private static final String DESCRIPTION = "Demonstrates enrollment from one image";
	private static final String NAME = "enroll-iris-from-image";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("%s [input image] [output template]%n", NAME);
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Biometrics.IrisExtraction";

		LibraryManager.initLibraryPath();

		Platform.isAndroid();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NIris iris = null;

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			iris = new NIris();
			
			// Set file name with face image
			iris.setFileName(args[0]);

			subject.getIrises().add(iris);

			// Set face template size (recommended, for enroll to database, is
			// large) (optional)
			biometricClient.setIrisesTemplateSize(NTemplateSize.LARGE);

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (subject.getIrises().size() > 1)
				System.out.format("Found %d iris(es)\n", subject.getIrises().size() - 1);

			if (status == NBiometricStatus.OK) {
				System.out.println("template extracted");
				NFile.writeAllBytes(args[1], subject.getTemplate().save());
				System.out.println("template saved successfully");
			} else {
				System.out.format("extraction failed: %s%n", status.toString());
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
			if (iris != null) iris.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
