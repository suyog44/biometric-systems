package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class IdentifyOnSQLiteDatabase {

	private static final String DESCRIPTION = "Demonstrates how to identify template on SQLite database";
	private static final String NAME = "identify-on-sqlite-database";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage:\n");
		System.out.format("\t%s [template] [path to database file]\n", NAME);
		System.out.format("\n");
		System.out.format("\ttemplate                   - template for identification\n");
		System.out.format("\tpath to database file      - path to SQLite database file\n");
	}

	public static void main(String[] args) {
		List<String> components = new ArrayList<String>();
		components.add("Biometrics.FingerMatching");
		components.add("Biometrics.FaceMatching");
		components.add("Biometrics.IrisMatching");
		components.add("Biometrics.PalmMatching");
		components.add("Biometrics.VoiceMatching");

		List<String> obtainedComponents = new ArrayList<String>();

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NBiometricTask task = null;

		try {
			for (String component : components) {
				if (NLicense.obtainComponents("/local", 5000, component)) {
					obtainedComponents.add(component);
				}
			}
			if (obtainedComponents.isEmpty()) {
				System.err.format("Could not obtain any of components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = createSubject(args[0], args[0]);

			biometricClient.setDatabaseConnectionToSQLite(args[1]);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.IDENTIFY), subject);

			biometricClient.performTask(task);

			if (task.getStatus() != NBiometricStatus.OK) {
				System.out.format("Identification was unsuccessful. Status: {0}.", task.getStatus());
				if (task.getError() != null) throw task.getError();
				System.exit(-1);
			}
			for (NMatchingResult matchingResult : subject.getMatchingResults()) {
				System.out.format("Matched with ID: '%s' with score %d\n", matchingResult.getId(), matchingResult.getScore());
			}

		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				for (String component : obtainedComponents) {
					NLicense.releaseComponents(component);
				}
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (task != null) task.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}

	private static NSubject createSubject(String fileName, String subjectId) throws IOException {
		NSubject subject = new NSubject();
		subject.setTemplateBuffer(NFile.readAllBytes(fileName));
		subject.setId(subjectId);

		return subject;
	}

}
