package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.WSQInfo;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class WSQToNImage {

	private static final String DESCRIPTION = "Demonstrates WSQ to NImage conversion.";
	private static final String NAME = "wsq-to-nimage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [srcImage] [dstImage]%n", NAME);
		System.out.println();
		System.out.println("\tsrcImage - filename of source WSQ image.");
		System.out.println("\tdstImage - name of a file to save converted image to.");
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Images.WSQ";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(-1);
		}

		NImage image = null;
		try {
			// Obtain license
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			// Create an NImage from a WSQ image file
			image = NImage.fromFile(args[0], NImageFormat.getWSQ());

			System.out.println("Loaded WSQ bitrate: " + ((WSQInfo) image.getInfo()).getBitRate());

			// Pick a format to save in, e.g. JPEG
			NImageFormat dstFormat = NImageFormat.getJPEG();

			// Save image to specified file
			image.save(args[1], dstFormat);
			System.out.println(dstFormat.getName() + " image was saved to " + args[1]);
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
