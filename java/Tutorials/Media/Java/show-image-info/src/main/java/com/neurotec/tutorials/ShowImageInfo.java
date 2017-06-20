package com.neurotec.tutorials;

import java.io.File;
import java.io.IOException;

import com.neurotec.images.JPEG2KInfo;
import com.neurotec.images.JPEGInfo;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.PNGInfo;
import com.neurotec.images.WSQInfo;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ShowImageInfo {

	private static final String DESCRIPTION = "Displays information about an image.";
	private static final String NAME = "show-image-info";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [filename]%n", NAME);
		System.out.println();
		System.out.println("\tfilename - image filename.");
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Images.WSQ,Images.IHead,Images.JPEG2000";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			usage();
			System.exit(-1);
		}

		NImage image = null;
		try {

			// Obtain licenses
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
			}

			// Create NImage from file
			image = NImage.fromFile(args[0]);

			// Get image format
			NImageFormat format = image.getInfo().getFormat();

			// Print info common to all formats
			File file = new File(args[0]);
			System.out.println("Image: " + file.getName());
			System.out.println("Size: " + (file.length() * 100 / 1024) / 100.0 + " KB");
			System.out.println("Format: " + format.getName());

			// Print format specific info
			if (NImageFormat.getJPEG2K().equals(format)) {
				JPEG2KInfo info = (JPEG2KInfo) image.getInfo();
				System.out.println("Profile: " + info.getProfile());
				System.out.println("Compression ratio: " + info.getRatio());
			} else if (NImageFormat.getJPEG().equals(format)) {
				JPEGInfo info = (JPEGInfo) image.getInfo();
				System.out.println("Lossless: " + info.isLossless());
				System.out.println("Quality: " + info.getQuality());
			} else if (NImageFormat.getPNG().equals(format)) {
				PNGInfo info = (PNGInfo) image.getInfo();
				System.out.println("Compression level: " + info.getCompressionLevel());
			} else if (NImageFormat.getWSQ().equals(format)) {
				WSQInfo info = (WSQInfo) image.getInfo();
				System.out.println("Bit rate: " + info.getBitRate());
				System.out.println("Implementation number: " + info.getImplementationNumber());
			};
		} catch (Throwable th) {
			th.printStackTrace();

			int errorCode = -1;

			if (th instanceof NThrowable) {
				errorCode = ((NThrowable)th).getCode();
			}

			System.exit(errorCode);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (image != null) { image.dispose(); }
		}

	}
}
