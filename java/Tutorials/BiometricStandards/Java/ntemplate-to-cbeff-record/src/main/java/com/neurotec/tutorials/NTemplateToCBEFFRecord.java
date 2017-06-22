package com.neurotec.tutorials;

import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;

public class NTemplateToCBEFFRecord {

	private static final String DESCRIPTION = "Converting CbeffRecord to NTemplate";
	private static final String NAME = "ntemplate-to-cbeff-record";
	private static final String VERSION = "9.0.0.0";

	private static final String LICENSES = "Biometrics.Standards.Base";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [NTemplate] [CbeffRecord] [PatronFormat]", NAME);
		System.out.println();
		System.out.println("\t[NTemplate] - filename of NTemplate.");
		System.out.println("\t[CbeffRecord] - filename of CbeffRecord.");
		System.out.println("\t[PatronFormat] - number identifying patron format (all supported values can be found in CbeffRecord class documentation).");
		System.out.println();
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 3) {
			usage();
			System.exit(1);
		}

		try {
			if (!NLicense.obtainComponents("/local", 5000, LICENSES)) {
				System.err.println("Could not obtain licenses");
				System.exit(-1);
			}

			// Read NTemplate buffer
			NBuffer packedNTemplate =  NFile.readAllBytes(args[0]);

			// Combine NTemplate BDB format
			int bdbFormat = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.NEUROTECHNOLOGIJA, CBEFFBDBFormatIdentifiers.NEUROTECHNOLOGIJA_NTEMPLATE);

			// Get CbeffRecord patron format
			// all supported patron formats can be found in CbeffRecord class documentation
			int patronFormat = Integer.parseInt(args[2], 16);

			// Create CbeffRecord from NTemplate buffer
			CBEFFRecord cbeffRecord = new CBEFFRecord(bdbFormat, packedNTemplate, patronFormat);

			NFile.writeAllBytes(args[1], cbeffRecord.save());
			System.out.println("Template successfully saved");
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(LICENSES);
			} catch (IOException e) {
				Utils.handleError(e);
			}
		}
	}

}
