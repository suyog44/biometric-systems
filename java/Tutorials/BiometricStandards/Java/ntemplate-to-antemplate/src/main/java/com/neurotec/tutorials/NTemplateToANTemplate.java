package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class NTemplateToANTemplate {

	private static final String DESCRIPTION = "Converting NTemplate to ANTemplate";
	private static final String NAME = "ntemplate-to-antemplate";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [NTemplate] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]%n", NAME);
		System.out.println("");
		System.out.println("\t[NTemplate]     - filename of NTemplate.");
		System.out.println("\t[ANTemplate]    - filename of ANTemplate.");
		System.out.println("\t[Tot] - specifies type of transaction.");
		System.out.println("\t[Dai] - specifies destination agency identifier.");
		System.out.println("\t[Ori] - specifies originating agency identifier.");
		System.out.println("\t[Tcn] - specifies transaction control number.");
		System.out.println("");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		// Depending on NTemplate contents choose licenses: if you only have finger templates in NTemplate, leave finger templates license only.
		final String components = "Biometrics.Standards.FingerTemplates,Biometrics.Standards.PalmTemplates";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 6) {
			usage();
			System.exit(1);
		}

		if (args[0] == "/?" || args[0] == "help") {
			usage();
			System.exit(-1);
		}

		NTemplate nTemplate = null;
		ANTemplate anTemplate = null;

		try {
			String nTemplateFileName = args[0];

			String tot = args[2]; // type of transaction
			String dai = args[3]; // destination agency identifier
			String ori = args[4]; // originating agency identifier
			String tcn = args[5]; // transaction control number

			if ((tot.length() < 3) || (tot.length() > 4)) {
				System.out.println("Tot parameter should be 3 or 4 characters length.");
				System.exit(-1);
			}

			NBuffer packedNTemplate = NFile.readAllBytes(nTemplateFileName);

			// Creating NTemplate object from packed NTemplate
			nTemplate = new NTemplate(packedNTemplate);

			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			// Creating ANTemplate object from NTemplate object
			anTemplate = new ANTemplate(ANTemplate.VERSION_CURRENT, tot, dai, ori, tcn, true, nTemplate);

			// Storing ANTemplate object in file
			anTemplate.save(args[1]);
			System.out.println("Program produced file: " + args[1]);
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (nTemplate != null) nTemplate.dispose();
			if (anTemplate != null) anTemplate.dispose();
		}
	}
}
