package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.WSQInfo;
import com.neurotec.lang.NThrowable;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class CreateWSQ {

	private static final String DESCRIPTION = "Demonstrates WSQ format image creation from another image.";
	private static final String NAME = "create-wsq";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [srcImage] [dstImage] <optional: bitRate>%n", NAME);
		System.out.println();
		System.out.println("\tsrcImage - filename of source finger image.");
		System.out.println("\tdstImage - name of a file to save the created WSQ image to.");
		System.out.println("\tbitRate  - specifies WSQ image compression level. Typical bit rates: 0.75, 2.25.");
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
		WSQInfo info = null;
		try {
			// Obtain license
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			// Create an NImage from file
			image = NImage.fromFile(args[0]);

			// Create WSQInfo to store bit rate
			info = (WSQInfo) NImageFormat.getWSQ().createInfo(image);

			// Set specified bit rate (or default if bit rate was not specified)
			float bitrate;
			if (args.length > 2) {
				bitrate = Float.parseFloat(args[2]);
			} else {
				bitrate = WSQInfo.DEFAULT_BIT_RATE;
			}
			info.setBitRate(bitrate);

			// Save image in WSQ format and bitrate to file
			image.save(args[1], info);
			System.out.println("WSQ image with bit rate " + bitrate + " was saved to " + args[1]);
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
			if (image != null) {image.dispose(); }
			if (info != null) {info.dispose(); }
		}

	}
}
