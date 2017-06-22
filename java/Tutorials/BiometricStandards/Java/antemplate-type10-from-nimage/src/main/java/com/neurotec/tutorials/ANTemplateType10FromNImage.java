package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.standards.ANImageCompressionAlgorithm;
import com.neurotec.biometrics.standards.ANImageType;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType10Record;
import com.neurotec.biometrics.standards.BDIFScaleUnits;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ANTemplateType10FromNImage {

	private static final String DESCRIPTION = "Demonstrates creation of ANTemplate with type 10 record in it.";
	private static final String NAME = "antemplate-type10-from-nimage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn] [Src]%n", NAME);
		System.out.println("");
		System.out.println("\t[NImage]     - filename with Image file.");
		System.out.println("\t[ANTemplate] - filename for ANTemplate.");
		System.out.println("\t[Tot] - specifies type of transaction.");
		System.out.println("\t[Dai] - specifies destination agency identifier.");
		System.out.println("\t[Ori] - specifies originating agency identifier.");
		System.out.println("\t[Tcn] - specifies transaction control number.");
		System.out.println("\t[Src] - specifies source agency number.");
		System.out.println("");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.Faces";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 7) {
			usage();
			System.exit(1);
		}

		ANTemplate template = null;
		NImage image = null;
		NImage rgbImage = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			String tot = args[2]; // type of transaction
			String dai = args[3]; // destination agency identifier
			String ori = args[4]; // originating agency identifier
			String tcn = args[5]; // transaction control number
			String src = args[6]; // source agency number

			if ((tot.length() < 3) || (tot.length() > 4)) {
				System.out.println("Tot parameter should be 3 or 4 characters length.");
				System.exit(-1);
			}

			// Create empty ANTemplate object with only type 1 record in it
			template = new ANTemplate(ANTemplate.VERSION_CURRENT, tot, dai, ori, tcn, 0);

			// Create NImage object from image file
			image = NImage.fromFile(args[0]);

			rgbImage = NImage.fromImage(NPixelFormat.RGB_8U, 0, image);

			rgbImage.setResolutionIsAspectRatio(true);

			// Add Type 10 record to ANTemplate object
			// Image type contained in file, surely face
			ANType10Record record = new ANType10Record(ANTemplate.VERSION_CURRENT, 0, ANImageType.FACE, src, BDIFScaleUnits.NONE, ANImageCompressionAlgorithm.NONE, null, rgbImage);
			template.getRecords().add(record);

			// Storing ANTemplate object in file
			template.save(args[1]);
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (template != null) template.dispose();
			if (image != null) image.dispose();
			if (rgbImage != null) rgbImage.dispose();
		}
	}
}
