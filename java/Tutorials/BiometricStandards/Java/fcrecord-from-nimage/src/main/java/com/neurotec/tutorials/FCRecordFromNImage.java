package com.neurotec.tutorials;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FCRFaceImage;
import com.neurotec.biometrics.standards.FCRFaceImageType;
import com.neurotec.biometrics.standards.FCRImageDataType;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;

public final class FCRecordFromNImage {

	private static final String DESCRIPTION = "Create FCRecord from image tutorial";
	private static final String NAME = "fcrecord-from-nimage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage: %s [FCRecord] {[image]}%n", NAME);
		System.out.println("\t[FCRecord] - output FCRecord");
		System.out.println("\t[image]    - one or more images");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.Faces";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		FCRecord fc = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			for (int i = 1; i < args.length; i++) {
				NImage imageFromFile = null;
				NImage image = null;
				try {
					imageFromFile = NImage.fromFile(args[i]);
					image = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, imageFromFile);
					if (fc == null) {
						// Specify standard and version to be used
						fc = new FCRecord(BDIFStandard.ISO, FCRecord.VERSION_ISO_30);
					}
					FCRFaceImage img = new FCRFaceImage(BDIFStandard.ISO, fc.getVersion());
					img.setFaceImageType(FCRFaceImageType.BASIC);
					img.setImageDataType(FCRImageDataType.JPEG);
					img.setImage(image);
					fc.getFaceImages().add(img);
				} finally {
					if (imageFromFile != null) imageFromFile.dispose();
					if (image != null) image.dispose();
				}
			}
			if (fc != null) {
				NFile.writeAllBytes(args[0], fc.save());
				System.out.println("FCRecord saved to " + args[0]);
			} else {
				System.out.println("no images were added to FCRecord");
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (fc != null) fc.dispose();
		}
	}
}
