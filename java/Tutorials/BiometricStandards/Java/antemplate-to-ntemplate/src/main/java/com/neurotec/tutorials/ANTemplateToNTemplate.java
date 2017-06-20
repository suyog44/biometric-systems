package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANValidationLevel;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ANTemplateToNTemplate {

	private static final String DESCRIPTION = "ANTemplate to NTemplate conversion tutorial";
	private static final String NAME = "antemplate-to-ntemplate";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [ANTemplate] [NTemplate]%n", NAME);
		System.out.println("");
		System.out.println("\t[ATemplate] - filename of ANTemplate.");
		System.out.println("\t[NTemplate] - filename of NTemplate.");
		System.out.println("");
		System.out.println("examples:");
		System.out.format("\t%s antemplate.data nTemplate.data%n", NAME);
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.FingerTemplates";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 2) {
			usage();
			System.exit(1);
		}

		ANTemplate anTemplate = null;
		NTemplate nTemplate = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			String aNTemplateFileName = args[0];

			// Creating ANTemplate object from file
			anTemplate = new ANTemplate(aNTemplateFileName, ANValidationLevel.STANDARD);

			// Converting ANTemplate object to NTemplate object
			nTemplate = anTemplate.toNTemplate();

			// Packing NTemplate object
			NBuffer packedNTemplate = nTemplate.save();

			// Storing NTemplate object in file
			NFile.writeAllBytes(args[1], packedNTemplate);
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (anTemplate != null) anTemplate.dispose();
			if (nTemplate != null) nTemplate.dispose();
		}
	}
}
