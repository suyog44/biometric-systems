package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class CreateTokenFaceImage {
	private static final String DESCRIPTION = "Demonstrates creation of token face image.";
	private static final String NAME = "create-token-face-image";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [FaceImage] [CreateTokenFaceImage]%n", NAME);
		System.out.println();
		System.out.println("\t[FaceImage] - an image containing frontal face.");
		System.out.println("\t[CreateTokenFaceImage] - filename of created token face image.");
	}

	public static void main(String[] args) {
		final String components = "Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment";
		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFace face = null;
		NBiometricTask task = null;
		
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			face = new NFace();

			face.setFileName(args[0]);
			subject.getFaces().add(face);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.SEGMENT, NBiometricOperation.ASSESS_QUALITY), subject);

			biometricClient.performTask(task);

			if (task.getStatus() == NBiometricStatus.OK) {
				NLAttributes originalAttributes = face.getObjects().get(0);
				NLAttributes attributes = ((NFace) originalAttributes.getChild()).getObjects().get(0);
				System.out.format("global token face image quality score = %d Tested attributes details:\n", attributes.getQuality());
				System.out.format("\tsharpness score = %d\n", attributes.getSharpness());
				System.out.format("\tbackground uniformity score = %d\n", attributes.getBackgroundUniformity());
				System.out.format("\tgrayscale density score = %d\n", attributes.getGrayscaleDensity());

				// Save token Image to file
				subject.getFaces().get(1).getImage().save(args[1]);
			} else {
				System.out.format("Token Face Image creation failed! Status = %s\n", task.getStatus());
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
			if (face != null) face.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
