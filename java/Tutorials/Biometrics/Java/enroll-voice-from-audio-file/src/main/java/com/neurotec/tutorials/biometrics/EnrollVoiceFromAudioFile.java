package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class EnrollVoiceFromAudioFile {
	private static final String DESCRIPTION = "Demonstrates enrollment from one audio file";
	private static final String NAME = "enroll-voice-from-audio-file";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("%s [input image] [output template]%n", NAME);
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Biometrics.VoiceExtraction";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = new NSubject();
		NVoice voice = new NVoice();

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			voice = new NVoice();
			
			// Set file name with face image
			voice.setFileName(args[0]);

			subject.getVoices().add(voice);

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (subject.getVoices().size() > 1)
				System.out.format("Found %d voice(s)\n", subject.getVoices().size() - 1);

			if (status == NBiometricStatus.OK) {
				System.out.println("template extracted");
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
			if (voice != null) voice.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
