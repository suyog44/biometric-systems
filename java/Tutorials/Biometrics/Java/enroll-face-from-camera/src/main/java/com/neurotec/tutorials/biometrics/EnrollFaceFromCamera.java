package com.neurotec.tutorials.biometrics;

import com.neurotec.beans.NParameterBag;
import com.neurotec.beans.NParameterDescriptor;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NCamera;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.plugins.NPlugin;
import com.neurotec.plugins.NPluginState;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;
import java.util.EnumSet;

public final class EnrollFaceFromCamera {

	private static final String DESCRIPTION = "Demonstrates face enrollment from camera";
	private static final String NAME = "enroll-face-from-camera";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [image] [template]%n", NAME);
		System.out.format("\t%s [image] [template] [-u url](optional)%n", NAME);
		System.out.format("\t%s [image] [template] [-f filename](optional)%n", NAME);
		System.out.println();
		System.out.println("\timage			- image filename to store face image.");
		System.out.println("\ttemplate		- filename to store face template.");
		System.out.println("\t[-u url]		- (optional) url to RTSP stream");
		System.out.println("\t[-f filename] - (optional) video file containing a face");
		System.out.println("\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used");
		System.out.println();
	}

	public static void main(String[] args) {
		String components = "Biometrics.FaceExtraction,Devices.Cameras";
		final String additionalComponents = "Biometrics.FaceSegmentsDetection";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if ((args.length != 2) && (args.length != 4)) {
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
			if (!NLicense.obtainComponents("/local", 5000, additionalComponents)) {
				components += "," + additionalComponents;
			}

			biometricClient = new NBiometricClient();
			biometricClient.setUseDeviceManager(true);
			NDeviceManager deviceManager = biometricClient.getDeviceManager();
			subject = new NSubject();
			face = new NFace();

			// Set type of the device used.
			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.CAMERA));

			// Initialize the NDeviceManager.
			deviceManager.initialize();

			// Create camera from filename or RTSP stram if attached
			NCamera camera;
			if (args.length == 4) {
				camera = (NCamera) connectDevice(deviceManager, args[3], args[2].equals("-u"));
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

			System.out.format("Capturing from %s. Please turn camera to face.\n", biometricClient.getFaceCaptureDevice().getDisplayName());

			// Define that the face source will be a stream.
			face.setCaptureOptions(EnumSet.of(NBiometricCaptureOption.STREAM));

			// Add NFace to NSubject.
			subject.getFaces().add(face);

			// Set face template size (recommended, for enroll to database, is
			// large) (optional).
			biometricClient.setFacesTemplateSize(NTemplateSize.LARGE);

			// Detect all faces features.
			boolean isAdditionalComponentActivated = NLicense.isComponentActivated(additionalComponents);
			biometricClient.setFacesDetectAllFeaturePoints(isAdditionalComponentActivated);

			// Start capturing.
			System.out.println("Capturing....");
			NBiometricStatus status = biometricClient.capture(subject);

			if (status == NBiometricStatus.OK) {
				System.out.format("Capturing succeeded\n");
			} else {
				System.out.format("Capturing failed: %s\n", status.toString());
				System.exit(-1);
			}

			// (Optional) Get face detection details if face was detected.
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

			// Save image to file.
			subject.getFaces().get(0).getImage().save(args[0]);
			System.out.format("Image saved successfully\n");

			// Save template to file.
			NFile.writeAllBytes(args[1], subject.getTemplateBuffer());
			System.out.format("Template saved successfully\n");
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

}
