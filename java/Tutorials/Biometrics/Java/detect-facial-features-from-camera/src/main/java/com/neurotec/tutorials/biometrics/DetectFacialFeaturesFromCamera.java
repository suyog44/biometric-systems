package com.neurotec.tutorials.biometrics;

import com.neurotec.beans.NParameterBag;
import com.neurotec.beans.NParameterDescriptor;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NLFeaturePoint;
import com.neurotec.biometrics.NLProperty;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NCamera;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.plugins.NPlugin;
import com.neurotec.plugins.NPluginState;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;
import java.util.EnumSet;

public final class DetectFacialFeaturesFromCamera {

	private static final String DESCRIPTION = "Demonstrates facial features detection from camera";
	private static final String NAME = "detect-facial-features-from-camera";
	private static final String VERSION = "9.0.0.0";

	private static final String ADDITIONAL_COMPONENTS = "Biometrics.FaceSegmentsDetection";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [FaceTemplate] [FaceImage] [TokenFaceImage]%n", NAME);
		System.out.format("\t%s [FaceTemplate] [FaceImage] [TokenFaceImage] [-u url](optional)%n", NAME);
		System.out.format("\t%s [FaceTemplate] [FaceImage] [TokenFaceImage] [-f filename](optional)%n", NAME);
		System.out.println();
		System.out.println("\t[FaceTemplate] - filename for template.");
		System.out.println("\t[FaceImage] -	filename for face image.");
		System.out.println("\t[TokenFaceImage] - filename for token face image.");
		System.out.println("\t[-u url] - (optional) url to RTSP stream.");
		System.out.println("\t[-f filename] - (optional) video file containing a face.");
		System.out.println("\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used.");
		System.out.println();
	}

	public static void main(String[] args) {
		String components = "Biometrics.FaceDetection,Biometrics.FaceExtraction,Devices.Cameras,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if ((args.length != 3) && (args.length != 5)) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFace face = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println(String.format("Could not obtain licenses for components: %1$s\n", components));
				System.exit(-1);
			}
			if (!NLicense.obtainComponents("/local", 5000, ADDITIONAL_COMPONENTS)) {
				components += "," + ADDITIONAL_COMPONENTS;
			}

			biometricClient = new NBiometricClient();
			biometricClient.setUseDeviceManager(true);
			biometricClient.setBiometricTypes(EnumSet.of(NBiometricType.FACE));
			subject = new NSubject();
			face = new NFace();
			face.setCaptureOptions(EnumSet.of(NBiometricCaptureOption.STREAM));

			biometricClient.initialize();

			// Create camera from filename or RTSP stram if attached.
			NCamera camera;
			NDeviceManager deviceManager = biometricClient.getDeviceManager();
			if (args.length == 5) {
				camera = (NCamera) connectDevice(deviceManager, args[4], args[3].equals("-u"));
			} else {
				// Get count of connected devices.
				int count = deviceManager.getDevices().size();
				if (count == 0) {
					System.out.format("no cameras found, exiting ...\n");
					return;
				}
				// Select the first available camera.
				camera = (NCamera) deviceManager.getDevices().get(0);
			}

			// Set the selected camera as NBiometricClient Face Capturing Device.
			biometricClient.setFaceCaptureDevice(camera);

			boolean additionalComponentActivated = NLicense.isComponentActivated(ADDITIONAL_COMPONENTS);
			if (additionalComponentActivated) {

				// Set which features should be detected.
				biometricClient.setFacesDetectBaseFeaturePoints(true);
				biometricClient.setFacesDetectAllFeaturePoints(true);
				biometricClient.setFacesRecognizeEmotion(true);
				biometricClient.setFacesRecognizeExpression(true);
				biometricClient.setFacesDetectProperties(true);
				biometricClient.setFacesDetermineGender(true);
				biometricClient.setFacesDetermineAge(true);
			}

			// Set face template size (recommended, for enroll to database, is large) (optional).
			biometricClient.setFacesTemplateSize(NTemplateSize.LARGE);

			// Add NFace to NSubject.
			subject.getFaces().add(face);

			NBiometricTask task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CAPTURE,
																		NBiometricOperation.DETECT_SEGMENTS,
																		NBiometricOperation.SEGMENT,
																		NBiometricOperation.ASSESS_QUALITY), subject);

			// Start capturing.
			System.out.print("Starting to capture. Please look into the camera... ");
			biometricClient.performTask(task);
			System.out.println("Done.");

			if (task.getStatus() == NBiometricStatus.OK) {

				// Print face attributes.
				printFaceAttributes(face);

				// Save template to file.
				NFile.writeAllBytes(args[0], subject.getTemplateBuffer());

				// Save original face.
				face.getImage().save(args[1]);

				// Save token face image.
				subject.getFaces().get(1).getImage().save(args[2]);
			} else {
				System.out.format("Capturing failed: %s\n", task.getStatus());
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
			if (face != null) {
				face.dispose();
			}
			if (subject != null) {
				subject.dispose();
			}
			if (biometricClient != null) {
				biometricClient.dispose();
			}
		}
	}

	private static NDevice connectDevice(NDeviceManager deviceManager, String url, boolean isUrl) {
		NPlugin plugin = NDeviceManager.getPluginManager().getPlugins().get("Media");
		if ((plugin.getState() == NPluginState.PLUGGED) && (NDeviceManager.isConnectToDeviceSupported(plugin))) {
			NParameterDescriptor[] parameters = NDeviceManager.getConnectToDeviceParameters(plugin);
			NParameterBag bag = new NParameterBag(parameters);
			if (isUrl) {
				bag.setProperty("DisplayName", "IP Camera");
				bag.setProperty("Url", url);
			} else {
				bag.setProperty("DisplayName", "Video file");
				bag.setProperty("FileName", url);
			}
			return deviceManager.connectToDevice(plugin, bag.toPropertyBag());
		}
		throw new RuntimeException("Failed to connect specified device!");
	}

	private static void printFaceAttributes(NFace face) {
		for (NLAttributes attributes : face.getObjects()) {
			System.out.format("\tlocation = (%d, %d), width = %d, height = %d%n",
							  attributes.getBoundingRect().x,
							  attributes.getBoundingRect().y,
							  attributes.getBoundingRect().width,
							  attributes.getBoundingRect().height);

			printNleFeaturePoint("LeftEyeCenter", attributes.getLeftEyeCenter());
			printNleFeaturePoint("RightEyeCenter", attributes.getRightEyeCenter());

			try {
				if (NLicense.isComponentActivated(ADDITIONAL_COMPONENTS)) {
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
						System.out.println("\t\tGender not detected");
					} else {
						System.out.format("\t\tGender: %s, Confidence: %d%n", attributes.getGender(), attributes.getGenderConfidence());
					}
					if (attributes.getExpressionConfidence() == 255) {
						System.out.println("\t\tExpression not detected");
					} else {
						System.out.format("\t\tExpression: %s, Confidence: %d%n", attributes.getExpression(), attributes.getExpressionConfidence());
					}
					if (attributes.getBlinkConfidence() == 255) {
						System.out.println("\t\tBlink not detected");
					} else {
						System.out.format("\t\tBlink: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.BLINK), attributes.getBlinkConfidence());
					}
					if (attributes.getMouthOpenConfidence() == 255) {
						System.out.println("\t\tMouth open not detected");
					} else {
						System.out.format("\t\tMouth open: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.MOUTH_OPEN), attributes.getMouthOpenConfidence());
					}
					if (attributes.getGlassesConfidence() == 255) {
						System.out.println("\t\tGlasses not detected");
					} else {
						System.out.format("\t\tGlasses: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.GLASSES), attributes.getGlassesConfidence());
					}
					if (attributes.getDarkGlassesConfidence() == 255) {
						System.out.println("\t\tDark glasses not detected");
					} else {
						System.out.format("\t\tDark glasses: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.DARK_GLASSES), attributes.getDarkGlassesConfidence());
					}
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
			System.out.println();
		}
	}

	private static void printNleFeaturePoint(String name, NLFeaturePoint point) {
		if (point.confidence == 0) {
			System.out.format("\t\t{0} feature unavailable. confidence: 0%n", name);
			return;
		}
		System.out.format("\t\t%s feature found. X: %d, Y: %d, confidence: %d%n", name, point.x, point.y, point.confidence);
	}

	private static void printBaseFeaturePoint(NLFeaturePoint point) {
		if (point.confidence == 0) {
			System.out.println("\t\tBase feature point unavailable. confidence: 0");
			return;
		}
		System.out.format("\t\tBase feature point found. X: %d, Y: %d, confidence: %d, Code: %d%n", point.x, point.y, point.confidence, point.code);
	}

}
