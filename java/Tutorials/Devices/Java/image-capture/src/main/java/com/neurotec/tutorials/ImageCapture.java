package com.neurotec.tutorials;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.devices.NCamera;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.images.NImage;
import com.neurotec.lang.NCore;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ImageCapture {

	private static final String DESCRIPTION = "Demonstrates capturing images from cameras";
	private static final String NAME = "image-capture";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [frameCount]%n", NAME);
		System.out.println();
		System.out.println("\tframeCount - number of frames to capture from each camera to current directory.");
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Devices.Cameras";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			usage();
			System.exit(1);
		}

		NDeviceManager deviceManager = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			int frameCount = Integer.parseInt(args[0]);
			if (frameCount <= 0) {
				System.out.println("no frames will be captured as frame count is not specified");
			}

			deviceManager = new NDeviceManager();
			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.CAMERA));
			deviceManager.setAutoPlug(true);
			System.out.println("device manager created. found cameras: " + deviceManager.getDevices().size());

			for (NDevice device : deviceManager.getDevices()) {
				NCamera camera = (NCamera) device;
				System.out.print("found camera " + camera.getDisplayName());
				try {
					camera.startCapturing();
					if (frameCount > 0) {
						System.out.print(", capturing");
						for (int i = 0; i < frameCount; ++i) {
							NImage image = null;
							try {
								image = camera.getFrame();
								image.save(String.format("%s_%d.jpg", camera.getDisplayName(), i));
							} finally {
								if (image != null) image.dispose();
							}
							System.out.print(".");
						}
						System.out.println(" done");
					}
				} finally {
					if (camera  != null) camera.stopCapturing();
				}
			}
			System.out.println("done");
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			if (deviceManager != null) deviceManager.dispose();
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			NCore.shutdown();
		}
	}
}
