package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.standards.ANImageASCIIBinaryRecord;
import com.neurotec.biometrics.standards.ANImageBinaryRecord;
import com.neurotec.biometrics.standards.ANRecord;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANValidationLevel;
import com.neurotec.images.NImage;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ANTemplateToNImage {

	private static final String DESCRIPTION = "Demonstrates how to save images stored in ANTemplate";
	private static final String NAME = "antemplate-to-nimage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [ANTemplate]%n", NAME);
		System.out.println("");
		System.out.println("\t[ATemplate] - filename of ANTemplate.");
		System.out.println("");
		System.out.println("examples:");
		System.out.format("\t%s antemplate.data%n", NAME);
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.Base,Biometrics.Standards.PalmTemplates,Biometrics.Standards.Irises,Biometrics.Standards.Faces";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 1) {
			usage();
			System.exit(1);
		}

		ANTemplate template = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			template = new ANTemplate(args[0], ANValidationLevel.STANDARD);

			for (int i = 0; i < template.getRecords().size(); i++) {
				ANRecord record = template.getRecords().get(i);
				NImage image = null;
				int number = record.getRecordType().getNumber();
				if (number >= 3 && number <= 8 && number != 7) {
					image = ((ANImageBinaryRecord)record).toNImage();
				} else if (number >= 10 && number <= 17) {
					image = ((ANImageASCIIBinaryRecord)record).toNImage();
				}

				if (image != null) {
					String fileName = String.format("record%d_type%d.jpg", i + 1, number);
					image.save(fileName);
					image.dispose();
					System.out.format("Image saved to %s%n", fileName);
				}
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (template != null) template.dispose();
		}
	}
}
