package com.neurotec.tutorials;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NMicrophone;
import com.neurotec.lang.NCore;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.sound.NSoundBuffer;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class SoundCapture {

	private static final String DESCRIPTION = "Demonstrates sound sample capturing from microphone";
	private static final String NAME = "sound-capture";
	private static final String VERSION = "9.0.0.0";

	private static void Usage() {
		System.out.println("usage:");
		System.out.format("\t{%s [bufferCount]%n", NAME);
		System.out.println();
		System.out.println("\tbufferCount - number of sound buffers to capture from each microphone");
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Devices.Microphones";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			Usage();
			System.exit(1);
		}

		NDeviceManager deviceManager = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			int bufferCount = Integer.parseInt(args[0]);
			if (bufferCount == 0) {
				System.err.println("no sound buffers will be captured as sound buffer count is not specified");
				System.exit(-1);
			}
			deviceManager = new NDeviceManager();
			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.MICROPHONE));
			deviceManager.setAutoPlug(true);
			deviceManager.initialize();
			System.out.format("device manager created. found microphones: %s%n", deviceManager.getDevices().size());

			for (NDevice device : deviceManager.getDevices()) {
				NMicrophone microphone = (NMicrophone) device;
				System.out.format("found microphone %s", microphone.getDisplayName());
				microphone.startCapturing();

				if (bufferCount > 0) {
					System.out.println(", capturing");
					for (int i = 0; i < bufferCount; i++) {
						NSoundBuffer soundSample = microphone.getSoundSample();
						System.out.format("sample buffer received. sample rate: %s, sample length: %s%n", soundSample.getSampleRate(), soundSample.getLength());

						System.out.print(" ... ");
					}
					System.out.print(" done");
					System.out.println();
				}
				microphone.stopCapturing();
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
