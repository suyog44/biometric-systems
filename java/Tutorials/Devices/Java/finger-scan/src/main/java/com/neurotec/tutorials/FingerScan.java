package com.neurotec.tutorials;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NFScanner;
import com.neurotec.lang.NCore;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class FingerScan {

	private static final String DESCRIPTION = "Demonstrates fingerprint image capturing from scanner";
	private static final String NAME = "finger-scan";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [imageCount]%n", NAME);
		System.out.println();
		System.out.println("\timageCount - count of fingerprint images to be scanned.");
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Devices.FingerScanners";

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

			int imageCount = Integer.parseInt(args[0]);
			if (imageCount <= 0) {
				System.err.println("no frames will be captured as frame count is not specified");
				System.exit(-1);
			}

			deviceManager = new NDeviceManager();
			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.FINGER_SCANNER));
			deviceManager.setAutoPlug(true);
			deviceManager.initialize();
			System.out.println("device manager created. found scanners: " + deviceManager.getDevices().size());

			for (NDevice device : deviceManager.getDevices()) {
				NFScanner scanner = (NFScanner) device;
				System.out.format("found scanner %s, capturing fingerprints%n", scanner.getDisplayName());

				for (int i = 0; i < imageCount; i++) {
					System.out.format("\timage %d of %d. please put your fingerprint on scanner:", i + 1, imageCount);
					String filename = String.format("%s_%d.jpg", scanner.getDisplayName(), i);
					NFinger biometric = null;
					try {
						biometric = new NFinger();
						biometric.setPosition(NFPosition.UNKNOWN);
						NBiometricStatus status = scanner.capture(biometric, -1);
						if (status != NBiometricStatus.OK) {
							System.err.format("failed to capture from scanner, status: %s%n", status);
							continue;
						}
						biometric.getImage().save(filename);
						System.out.println(" image captured");
					} finally {
						if (biometric != null) biometric.dispose();
					}
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
