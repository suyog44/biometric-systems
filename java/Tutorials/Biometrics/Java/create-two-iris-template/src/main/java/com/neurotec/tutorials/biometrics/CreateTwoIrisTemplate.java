package com.neurotec.tutorials.biometrics;

import java.io.IOException;

import com.neurotec.biometrics.NETemplate;
import com.neurotec.biometrics.NTemplate;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public class CreateTwoIrisTemplate {

	private static final String DESCRIPTION = "Demonstrates how to create two eye NTemplate";
	private static final String NAME = "create-two-iris-template";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [left eye record] [right eye record] [template]%n", NAME);
		System.out.println();
		System.out.println("\t[left eye record]  - filename of left eye record.");
		System.out.println("\t[right eye record] - filename of right eye record.");
		System.out.println("\t[template]        - filename for template.");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		String components = "Biometrics.IrisExtraction";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 3) {
			usage();
			System.exit(1);
		}

		NTemplate nTemplate = new NTemplate();
		// create NTemplate
		NTemplate outputTemplate = new NTemplate();
		// create NETemplate
		NETemplate outputIrisesTemplate = new NETemplate();
		// set NETemplate to NTemplate
		outputTemplate.setIrises(outputIrisesTemplate);

		NETemplate irisesTemplate = new NETemplate();
		nTemplate.setIrises(irisesTemplate);

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			for (int i = 0; i < (args.length - 1); i++)
			{
				// read NTemplate/NETemplate/NERecord from input file
				NTemplate newTemplate = new NTemplate(NFile.readAllBytes(args[i]));
				NETemplate irisTemplate = new NETemplate();
				irisTemplate = newTemplate.getIrises();

				// retrieve NETemplate from NTemplate
				NETemplate inputIrisesTemplate = new NETemplate();
				inputIrisesTemplate = nTemplate.getIrises();

				// add NERecord to output NETemplate
				try {
					outputIrisesTemplate.getRecords().addAll(irisTemplate.getRecords());
				} finally {
					if (newTemplate != null) newTemplate.dispose();
					if (irisTemplate != null) irisTemplate.dispose();
					if (inputIrisesTemplate != null) inputIrisesTemplate.dispose();
				}
			}

			// Save compressed template to file.
			NFile.writeAllBytes(args[2], outputIrisesTemplate.save());
			System.out.format("Template successfully saved to file: %s%n", args[2]);

		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (nTemplate != null) nTemplate.dispose();
			if (outputTemplate != null) outputTemplate.dispose();
			if (outputIrisesTemplate != null) outputIrisesTemplate.dispose();
			if (irisesTemplate != null) irisesTemplate.dispose();
		}
	}
}
