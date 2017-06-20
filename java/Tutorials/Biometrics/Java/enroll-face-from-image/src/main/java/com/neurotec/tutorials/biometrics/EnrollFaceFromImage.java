package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class EnrollFaceFromImage {
	private static final String DESCRIPTION = "Demonstrates enrollment from one image";
	private static final String NAME = "enroll-face-from-image";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("%s [input image] [output template]%n", NAME);
		System.out.println();
	}

	public static void main(String[] args) {
		String components = "Biometrics.FaceExtraction";
		final String additionalComponents = "Biometrics.FaceSegmentsDetection";

		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFace face = null;

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}
			if (!NLicense.obtainComponents("/local", 5000, additionalComponents)) {
				components += "," + additionalComponents;
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			face = new NFace();

			// Set file name with face image
			face.setFileName(args[0]);

			subject.getFaces().add(face);

			// Set face template size (recommended, for enroll to database, is
			// large) (optional)
			biometricClient.setFacesTemplateSize(NTemplateSize.LARGE);

			// Detect all faces features
			boolean isAdditionalComponentActivated = NLicense.isComponentActivated(additionalComponents);
			biometricClient.setFacesDetectAllFeaturePoints(isAdditionalComponentActivated);

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (subject.getFaces().size() > 1)
				System.out.format("Found %d faces\n", subject.getFaces().size() - 1);

			// List attributes for all located faces
			for (NFace nface : subject.getFaces()) {
				for (NLAttributes attribute : nface.getObjects()) {
					System.out.println("face:");
					System.out.format("\tlocation = (%d, %d), width = %d, height = %d\n", attribute.getBoundingRect().getBounds().x, attribute.getBoundingRect().getBounds().y,
							attribute.getBoundingRect().width, attribute.getBoundingRect().height);

					if ((attribute.getRightEyeCenter().confidence > 0) || (attribute.getLeftEyeCenter().confidence > 0)) {
						System.out.println("\tfound eyes:");
						if (attribute.getRightEyeCenter().confidence > 0) {
							System.out.format("\t\tRight: location = (%d, %d), confidence = %d%n", attribute.getRightEyeCenter().x, attribute.getRightEyeCenter().y,
									attribute.getRightEyeCenter().confidence);
						}
						if (attribute.getLeftEyeCenter().confidence > 0) {
							System.out.format("\t\tLeft: location = (%d, %d), confidence = %d%n", attribute.getLeftEyeCenter().x, attribute.getLeftEyeCenter().y,
									attribute.getLeftEyeCenter().confidence);
						}
					}
					if (isAdditionalComponentActivated) {
						if (attribute.getNoseTip().confidence > 0) {
							System.out.println("\tfound nose:");
							System.out.format("\t\tlocation = (%d, %d), confidence = %d%n", attribute.getNoseTip().x, attribute.getNoseTip().y, attribute.getNoseTip().confidence);
						}
						if (attribute.getMouthCenter().confidence > 0) {
							System.out.println("\tfound mouth:");
							System.out.printf("\t\tlocation = (%d, %d), confidence = %d%n", attribute.getMouthCenter().x, attribute.getMouthCenter().y, attribute.getMouthCenter().confidence);
						}
					}
				}
			}

			if (status == NBiometricStatus.OK) {
				System.out.println("template extracted");
				// save compressed template to file
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
			if (face != null) face.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
