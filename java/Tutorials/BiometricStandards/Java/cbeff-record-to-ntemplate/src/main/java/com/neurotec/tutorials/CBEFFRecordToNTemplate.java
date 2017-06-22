package com.neurotec.tutorials;

import com.neurotec.biometrics.NBiometricEngine;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class CBEFFRecordToNTemplate {

	private static final String DESCRIPTION = "Converting CbeffRecord to NTemplate";
	private static final String NAME = "complex-cbeff-record";
	private static final String VERSION = "9.0.0.0";

	private static final List<String> LICENSES = Arrays.asList(
			"Biometrics.Standards.Base",
			"Biometrics.Standards.Irises",
			"Biometrics.Standards.Fingers",
			"Biometrics.Standards.Faces",
			"Biometrics.Standards.Palms",
			"Biometrics.IrisExtraction",
			"Biometrics.FingerExtraction",
			"Biometrics.FaceExtraction",
			"Biometrics.PalmExtraction"
			);

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [CbeffRecord] [PatronFormat] [NTemplate] ...", NAME);
		System.out.println();
		System.out.println("\t[CbeffRecord] - filename of CbeffRecord.");
		System.out.println("\t[PatronFormat] - number identifying patron format (all supported values can be found in CbeffRecord class documentation).");
		System.out.println("\t[NTemplate] - filename of NTemplate.");
		System.out.println();
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 3) {
			usage();
			System.exit(1);
		}

		List<String> obtainedLicenses = new ArrayList<String>();

		try {
			for (String license : LICENSES) {
				if (NLicense.obtainComponents("/local", 5000, license)) {
					obtainedLicenses.add(license);
				}
			}
			if (obtainedLicenses.size() == 0) {
				System.err.println("Could not obtain licenses");
				System.exit(-1);
			}

			// Read CbeffRecord buffer
			NBuffer packedCBEFFRecord = NFile.readAllBytes(args[0]);

			// Get CbeffRecord patron format
			// all supported patron formats can be found in CbeffRecord class documentation
			int patronFormat = Integer.parseInt(args[1], 16);

			// Creating CbeffRecord object from NBuffer object
			CBEFFRecord cbeffRecord = new CBEFFRecord(packedCBEFFRecord, patronFormat);
			NSubject subject = new NSubject();
			NBiometricEngine engine = new NBiometricEngine();

			// Setting CbeffRecord
			subject.setTemplate(cbeffRecord);

			// Extracting template details from specified CbeffRecord data
			engine.createTemplate(subject);
			if (subject.getStatus() == NBiometricStatus.OK) {
				NFile.writeAllBytes(args[2], subject.getTemplate().save());
				System.out.println("Template successfully saved");
			} else {
				System.out.format("Template creation failed! Status: %s", subject.getStatus());
			}

		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			for(String license : obtainedLicenses) {
				try {
					NLicense.releaseComponents(license);
				} catch (IOException e) {
					Utils.handleError(e);
				}
			}
		}
	}

}
