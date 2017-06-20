package com.neurotec.tutorials.biometrics;

import java.awt.Graphics2D;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;

import javax.imageio.ImageIO;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.swing.NFingerView;
import com.neurotec.images.NImage;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class DrawMinutiaeOnImage {
	private static final String DESCRIPTION = "Demonstrates minutiae drawing on image";
	private static final String NAME = "draw-minutiae-on-image";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("%s [input image] [output image]%n", NAME);
		System.out.println("\t[input image] - image file containing a finger");
		System.out.println("\t[output image] - filename to store image with minutiae");
		System.out.println();
	}

	public static void main(String[] args) {
		String components = "Biometrics.FingerExtraction";

		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NFingerView fingerView = null;
		NSubject subject = null;
		NFinger finger = null;
		NImage image = null;

		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();
			fingerView = new NFingerView();

			image = NImage.fromFile(args[0]);
			// setting fingers image
			finger.setImage(image);

			subject.getFingers().add(finger);

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (status == NBiometricStatus.OK) {
				System.out.println("Image with minutiae creation succeeded");

				fingerView.setSize(image.getWidth(), image.getHeight());

				// settings finger with template to finger view
				fingerView.setFinger(subject.getFingers().get(0));


				BufferedImage tempImage = new BufferedImage(fingerView.getWidth(), fingerView.getHeight(), BufferedImage.TYPE_INT_RGB);
				Graphics2D g2d = tempImage.createGraphics();
				fingerView.printAll(g2d);
				g2d.dispose();

				ImageIO.write(tempImage, "png", new File(args[1]));

			} else {
				System.err.format("Image with minutiae creation failed: %s%n", status.toString());
				System.exit(-1);
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (finger != null) finger.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
