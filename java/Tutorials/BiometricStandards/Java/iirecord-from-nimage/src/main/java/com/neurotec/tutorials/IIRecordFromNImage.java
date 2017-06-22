package com.neurotec.tutorials;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.IIRIrisImage;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;
import com.neurotec.util.NVersion;

import java.io.IOException;

public final class IIRecordFromNImage {

	private static final String DESCRIPTION = "Create IIRecord from image tutorial";
	private static final String NAME = "iirecord-from-inmage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage: %s [IIRecord] [Standard] [Version] {[image]}%n", NAME);
		System.out.println("\t[IIRecord] - output IIRecord");
		System.out.println("\t[Standard] - standard for the record (ANSI or ISO)");
		System.out.println("\t[Version] - version for the record");
		System.out.println("\t\t 1 - ANSI/INCITS 379-2004");
		System.out.println("\t\t 1 - ISO/IEC 19794-6:2005");
		System.out.println("\t\t 2 - ISO/IEC 19794-6:2011");
		System.out.println("\t[image] - one or more images");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.Irises";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 4) {
			usage();
			System.exit(1);
		}

		IIRecord iiRec = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			BDIFStandard standard = BDIFStandard.valueOf(args[1]);
			NVersion version;
			if (args[2].equals("1")) {
				version = (standard == BDIFStandard.ANSI) ? IIRecord.VERSION_ANSI_10 : IIRecord.VERSION_ISO_10;
			} else if (args[2].equals("2")) {
				if (standard != BDIFStandard.ISO) {
					throw new IllegalArgumentException("Standard and version is incompatible");
				}
				version = IIRecord.VERSION_ISO_20;
			} else {
				throw new IllegalArgumentException("Version was not recognized");
			}

			for (int i = 3; i < args.length; i++) {
				NImage imageFromFile = null;
				NImage image = null;
				try {
					imageFromFile = NImage.fromFile(args[i]);
					image = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, imageFromFile);

					if (iiRec == null) {
						iiRec = new IIRecord(standard, version);
						if (isRecordFirstVersion(iiRec)) {
							iiRec.setRawImageHeight(image.getHeight());
							iiRec.setRawImageWidth(image.getWidth());
							iiRec.setIntensityDepth((byte) 8);
						}
					}
					IIRIrisImage iirIrisImage = new IIRIrisImage(iiRec.getStandard(), iiRec.getVersion());
					if (!isRecordFirstVersion(iiRec)) {
						iirIrisImage.setImageWidth(image.getWidth());
						iirIrisImage.setImageHeight(image.getHeight());
						iirIrisImage.setIntensityDepth((byte) 8);
					}
					iirIrisImage.setNImage(image);
					iiRec.getIrisImages().add(iirIrisImage);
				} finally {
					if (imageFromFile != null) imageFromFile.dispose();
					if (image != null) image.dispose();
				}
			}

			if (iiRec != null) {
				NFile.writeAllBytes(args[0], iiRec.save());
				System.out.println("IIRecord saved to " + args[0]);
			} else {
				System.out.println("no images were added to IIRecord");
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (iiRec != null) iiRec.dispose();
		}
	}

	private static boolean isRecordFirstVersion(IIRecord record) {
		return (record.getStandard() == BDIFStandard.ANSI) && (record.getVersion().equals(IIRecord.VERSION_ANSI_10))
			   || (record.getStandard() == BDIFStandard.ISO) && (record.getVersion().equals(IIRecord.VERSION_ISO_10));
	}

}
