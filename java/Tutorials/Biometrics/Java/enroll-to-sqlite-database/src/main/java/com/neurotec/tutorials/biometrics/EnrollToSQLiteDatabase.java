package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class EnrollToSQLiteDatabase {
	
	private static final String DESCRIPTION = "Demonstrates how to enroll template to SQLite database";
	private static final String NAME = "enroll-to-sqlite-database";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [template] [path to database file]%n", NAME);
		System.out.println();
		System.out.println("\ttemplate                   - template for enrollment");
		System.out.println("\tpath to database file      - path to SQLite database file");

	}
	
	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NBiometricTask enrollTask = null;
		
		try {
			biometricClient = new NBiometricClient();
			subject = createSubject(args[0], args[0]);
			
			biometricClient.setDatabaseConnectionToSQLite(args[1]);
			
			enrollTask = biometricClient.createTask(EnumSet.of(NBiometricOperation.ENROLL), subject);
			
			biometricClient.performTask(enrollTask);
			
			if (enrollTask.getStatus() != NBiometricStatus.OK) {
				System.out.format("Enrollment was unsuccessful. Status: %s.\n", enrollTask.getStatus());
				if (enrollTask.getError() != null) throw enrollTask.getError();
				System.exit(-1);
			}
			
			System.out.format("Enrollment was successful. The SQLite database conatins these IDs:");
			
			for (NSubject subj : biometricClient.list()) {
				System.out.format("\t%s\n", subj.getId());
			}
			
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			if (enrollTask != null) enrollTask.dispose();
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
