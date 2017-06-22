package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class EnrollPalmFromImage {
	private static final String DESCRIPTION = "Demonstrates palmprint feature extraction from image.";
	private static final String NAME = "enroll-palm-from-image";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [image] [template]%n", NAME);
		System.out.println();
		System.out.println("\t[image]    - image filename to extract");
		System.out.println("\t[template] - filename to store extracted features.");
		System.out.println();
		System.out.println();
		System.out.println("example:");
		System.out.format("\t%s image.jpg template.dat%n", NAME);
	}

	public static void main(String[] args) {
		final String components = "Biometrics.PalmExtraction";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NPalm palm = null;
		
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			palm = new NPalm();

			palm.setFileName(args[0]);

			subject.getPalms().add(palm);

			biometricClient.setPalmsTemplateSize(NTemplateSize.LARGE);

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (status == NBiometricStatus.OK) {
				System.out.println("Template extracted");
			} else {
				System.out.format("Extraction failed: %s\n", status);
				System.exit(-1);
			}

			NFile.writeAllBytes(args[1], subject.getTemplateBuffer());
			System.out.println("Template saved successfully");
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (palm != null) palm.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
