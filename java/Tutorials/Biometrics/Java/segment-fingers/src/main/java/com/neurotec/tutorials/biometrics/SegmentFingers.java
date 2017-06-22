package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class SegmentFingers {
	private static final String DESCRIPTION = "Demonstrates fingers segmenter";
	private static final String NAME = "segment-fingers";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [image] [position] <optional: missing positions>%n", NAME);
		System.out.println("\t[image]             - image containing fingerprints");
		System.out.println("\t[position]          - fingerprints' position in provided image");
		System.out.println("\t[missing positions] - one or more NFPosition values of missing fingers");
		System.out.println();
		System.out.println("\tvalid positions:");
		System.out.println("\t\tPLAIN_RIGHT_FOUR_FINGERS = 13, PLAIN_LEFT_FOUR_FINGERS = 14, PLAIN_THUMBS = 15");
		System.out.println("\t\tRIGHT_THUMB = 1, RIGHT_INDEX_FINGER = 2, RIGHT_MIDDLE_FINGER = 3, RIGHT_RING_FINGER = 4, RIGHT_LITTLE_FINGER = 5");
		System.out.println("\t\tLEFT_THUMB = 6, LEFT_INDEX_FINGER = 7, LEFT_MIDDLE_FINGER = 8, LEFT_RING_FINGER = 9, LEFT_LITTLE_FINGER = 10");
		System.out.println();
		System.out.format("\texample: %s image.png 15%n", NAME);
		System.out.format("\texample: %s image.png 13 2 3%n", NAME);
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Biometrics.FingerSegmentation,Biometrics.FingerExtraction";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFinger finger = null;
		NBiometricTask task = null;

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();

			finger.setFileName(args[0]);
			finger.setPosition(NFPosition.get(Integer.parseInt(args[1])));
			subject.getFingers().add(finger);

			for (int i = 2; i < args.length; i++) {
				subject.getMissingFingers().add(NFPosition.get(Integer.parseInt(args[i])));
			}

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.SEGMENT, NBiometricOperation.CREATE_TEMPLATE), subject);

			biometricClient.performTask(task);

			if (task.getStatus() == NBiometricStatus.OK) {
				if(finger.getWrongHandWarning()) {
					System.out.println("Warning: possibly wrong hand.");
				}
				System.out.format("Found %d segments\n", subject.getFingers().size() - 1);
				for (int i = 1; i < subject.getFingers().size(); i++) {
					NFinger segmentedFinger = subject.getFingers().get(i);
					if (segmentedFinger.getStatus() == NBiometricStatus.OK) {
						System.out.format("\t %s:\n", segmentedFinger.getPosition());
						segmentedFinger.getImage().save(segmentedFinger.getPosition().toString() + ".png");
						System.out.println("Saving image...");
					} else {
						System.out.format("\t %s: %s\n", segmentedFinger.getPosition(), segmentedFinger.getStatus());
					}
				}
			} else {
				System.out.format("Segmentation failed. Status: %s\n", task.getStatus());
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
			if (finger != null) finger.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
