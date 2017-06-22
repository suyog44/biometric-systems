package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NLFeaturePoint;
import com.neurotec.biometrics.NLProperty;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class DetectFacialFeatures {
	private static final String DESCRIPTION = "Demonstrates detection of face and facial features in the image.";
	private static final String NAME = "detect-facial-features";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [image]%n", NAME);
		System.out.println();
		System.out.println("\t [image] - filename of image.");
		System.out.println();
	}

	public static void main(String[] args) {
		final String additionalComponents = "Biometrics.FaceSegmentsDetection";
		String components = "Biometrics.FaceDetection,Biometrics.FaceExtraction";
		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFace face = null;
		NBiometricTask task = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.out.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}
			if (!NLicense.obtainComponents("/local", 5000, additionalComponents)) {
				components += "," + additionalComponents;
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			face = new NFace();

			face.setFileName(args[0]);

			subject.getFaces().add(face);

			subject.setMultipleSubjects(true);

			boolean isAdditionalComponentActivated = NLicense.isComponentActivated(additionalComponents);
			if (isAdditionalComponentActivated) {
				biometricClient.setFacesDetectAllFeaturePoints(true);
				biometricClient.setFacesDetectBaseFeaturePoints(true);
				biometricClient.setFacesRecognizeExpression(true);
				biometricClient.setFacesDetectProperties(true);
				biometricClient.setFacesDetermineGender(true);
				biometricClient.setFacesDetermineAge(true);
			}

			biometricClient.setFacesTemplateSize(NTemplateSize.MEDIUM);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.DETECT_SEGMENTS), subject);

			biometricClient.performTask(task);

			if (task.getStatus() == NBiometricStatus.OK) {
				System.out.format("Found %d face(s)\n", face.getObjects().size());

				for (NLAttributes attributes : face.getObjects()) {
					System.out.format("\tlocation = (%d, %d), width = %d, height = %d\n", attributes.getBoundingRect().x, attributes.getBoundingRect().y, attributes.getBoundingRect().width,
							attributes.getBoundingRect().height);

					printNleFeaturePoint("LeftEyeCenter", attributes.getLeftEyeCenter());
					printNleFeaturePoint("RightEyeCenter", attributes.getRightEyeCenter());
					
					if (isAdditionalComponentActivated) {
						printNleFeaturePoint("MouthCenter", attributes.getMouthCenter());
						printNleFeaturePoint("NoseTip", attributes.getNoseTip());

						System.out.println();
						for (NLFeaturePoint featurePoint : attributes.getFeaturePoints()) {
							printBaseFeaturePoint(featurePoint);
						}

						System.out.println();
						if (attributes.getAge() == 254) {
							System.out.format("\t\tAge not detected\n");
						} else {
							System.out.format("\t\tAge: %d\n", attributes.getAge());
						}
						if (attributes.getGenderConfidence() == 255) {
							System.out.format("\t\tGender not detected\n");
						} else {
							System.out.format("\t\tGender: %s, Confidence: %d\n", attributes.getGender(), attributes.getGenderConfidence());
						}
						if (attributes.getExpressionConfidence() == 255) {
							System.out.format("\t\tExpression not detected\n");
						} else {
							System.out.format("\t\tExpression: %s, Confidence: %d\n", attributes.getExpression(), attributes.getExpressionConfidence());
						}
						if (attributes.getBlinkConfidence() == 255) {
							System.out.format("\t\tBlink not detected\n");
						} else {
							System.out.format("\t\tBlink: %s, Confidence: %d\n", (attributes.getProperties().contains(NLProperty.BLINK)), attributes.getBlinkConfidence());
						}
						if (attributes.getMouthOpenConfidence() == 255) {
							System.out.format("\t\tMouth open not detected\n");
						} else {
							System.out.format("\t\tMouth open: %s, Confidence: %d\n", (attributes.getProperties().contains(NLProperty.MOUTH_OPEN)), attributes.getMouthOpenConfidence());
						}
						if (attributes.getGlassesConfidence() == 255) {
							System.out.format("\t\tGlasses not detected\n");
						} else {
							System.out.format("\t\tGlasses: %s, Confidence: %d\n", (attributes.getProperties().contains(NLProperty.GLASSES)), attributes.getGlassesConfidence());
						}
						if (attributes.getDarkGlassesConfidence() == 255) {
							System.out.format("\t\tDark glasses not detected\n");
						} else {
							System.out.format("\t\tDark glasses: %s, Confidence: %d\n", (attributes.getProperties().contains(NLProperty.DARK_GLASSES)), attributes.getDarkGlassesConfidence());
						}
						System.out.println();
					}
				}
			} else {
				System.out.format("Face detection failed! Status = %s\n", task.getStatus());
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

	private static void printNleFeaturePoint(String name, NLFeaturePoint point) {
		if (point.confidence == 0) {
			System.out.format("\t\t%s feature unavailable. confidence: 0%n", name);
			return;
		}
		System.out.format("\t\t%s feature found. X: %d, Y: %d, confidence: %d%n", name, point.x, point.y, point.confidence);
	}

	private static void printBaseFeaturePoint(NLFeaturePoint point) {
		if (point.confidence == 0) {
			System.out.println("\t\tBase feature point unavailable. confidence: 0");
			return;
		}
		System.out.format("\t\tBase feature point found. X: %d, Y: %d, confidence: %d, Code: %d\n", point.x, point.y, point.confidence, point.code);
	}
}
