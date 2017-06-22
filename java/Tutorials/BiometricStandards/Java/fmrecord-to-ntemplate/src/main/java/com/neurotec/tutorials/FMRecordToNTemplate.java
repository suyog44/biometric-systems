package com.neurotec.tutorials;

import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FMRFingerView;
import com.neurotec.biometrics.standards.FMRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;

public final class FMRecordToNTemplate {

	private static final String DESCRIPTION = "Converting FMRecord to NTemplate";
	private static final String NAME = "fmrecord-to-ntemplate";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t %s [FMRecord] [NTemplate] [Standard] [FlagUseNeurotecFields]%n", NAME);
		System.out.println();
		System.out.println("\t[FMRecord] - filename of FMRecord.");
		System.out.println("\t[NTemplate] - filename of NTemplate to be created.");
		System.out.println("\t[Standard] - FMRecord standard (ISO or ANSI).");
		System.out.println("\t[FlagUseNeurotecFields] - 1 if FMRFingerView.FlagUseNeurotecFields flag is used; otherwise, 0 if flag is not used.");
		System.out.println();
		System.out.println("example:");
		System.out.format("\t%s fmrecord.dat ntemplate.dat ISO 1%n", NAME);
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.FingerTemplates";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 4) {
			usage();
			System.exit(1);
		}

		FMRecord fmRecord = null;
		NTemplate nTemplate = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			String fmRecordFileName = args[0];
			String outputFileName = args[1];
			BDIFStandard standard = BDIFStandard.valueOf(args[2]);

			int flagUseNeurotecFields = Integer.parseInt(args[3]);

			if ("/?".equals(fmRecordFileName) || "help".equals(fmRecordFileName)) {
				usage();
				System.exit(0);
			}

			NBuffer storedFmRecord = NFile.readAllBytes(fmRecordFileName);

			// Creating FMRecord object from FMRecord stored in memory
			if (flagUseNeurotecFields == 1) {
				fmRecord = new FMRecord(storedFmRecord, FMRFingerView.FLAG_USE_NEUROTEC_FIELDS, standard);
			} else {
				fmRecord = new FMRecord(storedFmRecord, standard);
			}

			// Converting FMRecord object to NTemplate object
			nTemplate = fmRecord.toNTemplate();
			// Packing NTemplate object
			NBuffer packedNTemplate = nTemplate.save();

			NFile.writeAllBytes(outputFileName, packedNTemplate);
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (fmRecord != null) fmRecord.dispose();
			if (nTemplate != null) nTemplate.dispose();
		}
	}
}
