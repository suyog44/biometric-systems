package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;
import java.util.Scanner;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NDeviceManager.DeviceCollection;
import com.neurotec.devices.NMicrophone;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class EnrollVoiceFromMicrophone {
	private static final String DESCRIPTION = "Demonstrates voice feature extraction from microphone.";
	private static final String NAME = "enroll-voice-from-microphone";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [audio_file] [template]%n", NAME);
		System.out.println("\t[audio_file]    - image filename to store audio file.");
		System.out.println("\t[template] - filename to store voice template.");
	}

	public static void main(String[] args) {
		final String components = "Devices.Microphones,Biometrics.VoiceExtraction";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NVoice voice = null;
		NBiometricTask task = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			voice = new NVoice();

			biometricClient.setUseDeviceManager(true);
			NDeviceManager deviceManager = biometricClient.getDeviceManager();

			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.MICROPHONE));

			deviceManager.initialize();

			DeviceCollection devices = deviceManager.getDevices();

			if (devices.size() > 0) {
				System.out.format("Found %d audio input devices\n", devices.size());
			} else {
				System.out.format("No audio inpu devices found\n");
				return;
			}

			if (devices.size() > 1)
				System.out.println("Please select microphone from the list:");

			for (int i = 0; i < devices.size(); i++)
				System.out.format("\t%d. %s\n", i + 1, devices.get(i).getDisplayName());

			int selection = 0;
			if (devices.size() > 1) {
				Scanner scanner = new Scanner(System.in);
				selection = scanner.nextInt() - 1;
				scanner.close();
			}

			biometricClient.setVoiceCaptureDevice((NMicrophone) devices.get(selection));

			subject.getVoices().add(voice);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CAPTURE , NBiometricOperation.SEGMENT), subject);

			System.out.println("Capturing....");
			biometricClient.performTask(task);
			NBiometricStatus status = task.getStatus();

			if (status == NBiometricStatus.OK) {
				System.out.println("Template extracted");

				subject.getVoices().get(1).getSoundBuffer().save(args[0]);
				System.out.println("Voice audio file saved successfully...");

				NFile.writeAllBytes(args[1], subject.getTemplate().save());
				System.out.println("Template file saved successfully...");
			} else {
				System.out.format("Extraction failed: %s\n", status);
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
			if (voice != null) voice.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
