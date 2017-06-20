package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NEMatchingDetails;
import com.neurotec.biometrics.NFMatchingDetails;
import com.neurotec.biometrics.NLMatchingDetails;
import com.neurotec.biometrics.NMatchingDetails;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSMatchingDetails;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class Identify {

	private static final String DESCRIPTION = "Demonstrates template verification";
	private static final String NAME = "identify";
	private static final String VERSION = "9.0.0.0";

	private static final String[] MATCHING_COMPONENTS = { "Biometrics.FingerMatching", "Biometrics.FaceMatching", "Biometrics.IrisMatching", "Biometrics.PalmMatching", "Biometrics.VoiceMatching", };

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [probe template] [one or more gallery templates]%n", NAME);
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		List<String> obtainedLicenses = new ArrayList<String>();

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject probeSubject = null;
		NBiometricTask enrollTask = null;

		try {
			// obtain licenses
			for (String matchingComponent : MATCHING_COMPONENTS) {
				if (NLicense.obtainComponents("/local", 5000, matchingComponent)) {
					System.out.format("Obtained license for component: %s%n", matchingComponent);
					obtainedLicenses.add(matchingComponent);
				}
			}
			if (obtainedLicenses.size() == 0) {
				System.err.println("Could not obtain any matching license.");
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			probeSubject = createSubject(args[0], "ProbeSubject");

			enrollTask = biometricClient.createTask(EnumSet.of(NBiometricOperation.ENROLL), null);

			for (int i = 1; i < args.length; i++) {
				enrollTask.getSubjects().add(createSubject(args[i], String.format("GallerySubject_%d", i)));
			}

			biometricClient.performTask(enrollTask);
			NBiometricStatus status = enrollTask.getStatus();

			if (status != NBiometricStatus.OK) {
				System.out.format("Enrollment was unsuccessful. Status: %s.\n", status);
				if (enrollTask.getError() != null) throw enrollTask.getError();
				System.exit(-1);
			}

			biometricClient.setMatchingThreshold(48);

			biometricClient.setMatchingWithDetails(true);

			status = biometricClient.identify(probeSubject);

			if (status == NBiometricStatus.OK) {
				for (NMatchingResult matchingResult : probeSubject.getMatchingResults()) {
					System.out.format("Matched with ID: '%s' with score %d\n", matchingResult.getId(), matchingResult.getScore());
					if (matchingResult.getMatchingDetails() != null) {
						System.out.format("%s", getMatchingDetailsToString(matchingResult.getMatchingDetails()));
					}
				}
			} else if (status == NBiometricStatus.MATCH_NOT_FOUND) {
				System.out.format("Match not found");
			} else {
				System.out.format("Identification failed. Status: %s.\n", status);
				System.exit(-1);
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			for (String matchingComponent : MATCHING_COMPONENTS) {
				try {
					NLicense.releaseComponents(matchingComponent);
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
			if (enrollTask != null) enrollTask.dispose();
			if (probeSubject != null) probeSubject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}

	private static NSubject createSubject(String fileName, String subjectId) throws IOException {
		NSubject subject = new NSubject();
		subject.setTemplateBuffer(NFile.readAllBytes(fileName));
		subject.setId(subjectId);

		return subject;
	}

	private static String getMatchingDetailsToString(NMatchingDetails details) {
		StringBuffer sb = new StringBuffer();
		if (details.getBiometricType().contains(NBiometricType.FINGER)) {
			sb.append("    Fingerprint match details: ");
			sb.append(String.format(" score = %d%n", details.getFingersScore()));
			for (NFMatchingDetails fngrDetails : details.getFingers()) {
				sb.append(String.format("    fingerprint index: %d; score: %d;%n", fngrDetails.getMatchedIndex(), fngrDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.FACE)) {
			sb.append("    Face match details: ");
			sb.append(String.format(" score = %d%n", details.getFacesScore()));
			for (NLMatchingDetails faceDetails : details.getFaces()) {
				sb.append(String.format("    face index: %d; score: %d;%n", faceDetails.getMatchedIndex(), faceDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.IRIS)) {
			sb.append("    Irises match details: ");
			sb.append(String.format(" score = %d%n", details.getIrisesScore()));
			for (NEMatchingDetails irisesDetails : details.getIrises()) {
				sb.append(String.format("    irises index: %d; score: %d;%n", irisesDetails.getMatchedIndex(), irisesDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.PALM)) {
			sb.append("    Palmprint match details: ");
			sb.append(String.format(" score = %d%n", details.getPalmsScore()));
			for (NFMatchingDetails fngrDetails : details.getPalms()) {
				sb.append(String.format("    palmprint index: %d; score: %d;%n", fngrDetails.getMatchedIndex(), fngrDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.VOICE)) {
			sb.append("    Voice match details: ");
			sb.append(String.format(" score = %d%n", details.getVoicesScore()));
			for (NSMatchingDetails voicesDetails : details.getVoices()) {
				sb.append(String.format("    voices index: %d; score: %d;%n", voicesDetails.getMatchedIndex(), voicesDetails.getScore()));
			}
		}
		return sb.toString();
	}
}
