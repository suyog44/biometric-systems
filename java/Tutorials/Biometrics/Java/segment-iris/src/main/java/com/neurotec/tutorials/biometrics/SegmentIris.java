package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NEAttributes;
import com.neurotec.biometrics.NEImageType;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class SegmentIris {

	private static final String DESCRIPTION = "Demonstrates iris segmenter";
	private static final String NAME = "segment-iris";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [input image] [output image]%n", NAME);
		System.out.println();
	}

	public static void main(String[] args) {
		final String components = "Biometrics.IrisExtraction,Biometrics.IrisSegmentation";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NIris iris = null;
		NBiometricTask task = null;
		
		try {
			// Obtain license.
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.format("Could not obtain licenses for components: %s%n", components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			iris = new NIris();

			iris.setFileName(args[0]);

			iris.setImageType(NEImageType.CROPPED_AND_MASKED);
			subject.getIrises().add(iris);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.SEGMENT), subject);

			biometricClient.performTask(task);

			if (task.getStatus() == NBiometricStatus.OK) {
				for (NEAttributes attributes : iris.getObjects()) {
					System.out.format("overall quality\t%d\n", attributes.getQuality());
					System.out.format("GrayScaleUtilisation\t%d\n", attributes.getGrayScaleUtilisation());
					System.out.format("Interlace\t%d\n", attributes.getInterlace());
					System.out.format("IrisPupilConcentricity\t%d\n", attributes.getIrisPupilConcentricity());
					System.out.format("IrisPupilContrast\t%d\n", attributes.getIrisPupilContrast());
					System.out.format("IrisRadius\t%d\n", attributes.getIrisRadius());
					System.out.format("IrisScleraContrast\t%d\n", attributes.getIrisScleraContrast());
					System.out.format("MarginAdequacy\t%d\n", attributes.getMarginAdequacy());
					System.out.format("PupilBoundaryCircularity\t%d\n", attributes.getPupilBoundaryCircularity());
					System.out.format("PupilToIrisRatio\t%d\n", attributes.getPupilToIrisRatio());
					System.out.format("Sharpness\t%d\n", attributes.getSharpness());
					System.out.format("UsableIrisArea\t%d\n", attributes.getUsableIrisArea());
				}

				// Save segmented image
				((NIris) iris.getObjects().get(0).getChild()).getImage().save(args[1]);
			} else {
				System.err.println("Segmentation failed: " + task.getStatus());
				if (task.getError() != null) throw task.getError();
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
			if (task != null) task.dispose();
			if (iris != null) iris.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
