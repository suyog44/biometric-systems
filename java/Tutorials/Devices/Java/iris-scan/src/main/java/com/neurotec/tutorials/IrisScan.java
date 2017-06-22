package com.neurotec.tutorials;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NEPosition;
import com.neurotec.biometrics.NIris;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.lang.NCore;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class IrisScan {

	private static final String DESCRIPTION = "Demonstrates capturing iris image from iris scanner";
	private static final String NAME = "iris-scan";
	private static final String VERSION = "9.0.0.0";

	public static void main(String[] args) {
		final String components = "Devices.IrisScanners";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		NDeviceManager deviceManager = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			deviceManager = new NDeviceManager();
			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.IRIS_SCANNER));
			deviceManager.setAutoPlug(true);
			deviceManager.initialize();
			System.out.println("device manager created. found scanners: " + deviceManager.getDevices().size());

			for (NDevice device : deviceManager.getDevices()) {
				NIrisScanner scanner = (NIrisScanner) device;
				System.out.println("found scanner " + scanner.getDisplayName());
				captureIris(scanner, NEPosition.RIGHT);
				captureIris(scanner, NEPosition.LEFT);
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

	private static void captureIris(NIrisScanner scanner, NEPosition position) throws IOException {
		NIris iris = null;
		try {
			System.out.format("\tcapturing %s iris:%n", position.name().toLowerCase());
			iris = new NIris();
			iris.setPosition(position);
			NBiometricStatus status = scanner.capture(iris, -1);
			if (status != NBiometricStatus.OK) {
				System.err.format("failed to capture from scanner, status: %s%n", status);
			} else {
				iris.getImage().save(String.format("%s_iris_%s.jpg", scanner.getDisplayName(), position.name().toLowerCase()));
				System.out.println("done");
			}
		} finally {
			if (iris != null) iris.dispose();
		}
	}
}
