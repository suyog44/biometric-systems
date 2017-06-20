package com.neurotec.tutorials;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FIRFingerView;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;
import com.neurotec.util.NVersion;

import java.io.IOException;

public final class FIRecordFromNImage {

	private static final String DESCRIPTION = "Create FIRecord from image tutorial";
	private static final String NAME = "firecord-from-nimage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage: %s [FIRecord] [Standard] [Version] {[image]}%n", NAME);
		System.out.println("\t[FIRecord] - output FIRecord");
		System.out.println("\t[Standard] - standard for the record (ANSI or ISO)");
		System.out.println("\t[Version] - version for the record");
		System.out.println("\t\t 1 - ANSI/INCITS 381-2004");
		System.out.println("\t\t 2.5 - ANSI/INCITS 381-2009");
		System.out.println("\t\t 1 - ISO/IEC 19794-4:2005");
		System.out.println("\t\t 2 - ISO/IEC 19794-4:2011");
		System.out.println("\t[image] - one or more images");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.Standards.Fingers";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 4) {
			usage();
			System.exit(1);
		}

		FIRecord fi = null;
		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			BDIFStandard standard = BDIFStandard.valueOf(args[1]);
			NVersion version;
			if (args[2].equals("1")) {
				version = (standard == BDIFStandard.ANSI) ? FIRecord.VERSION_ANSI_10 : FIRecord.VERSION_ISO_10;
			} else if (args[2].equals("2")) {
				if (standard != BDIFStandard.ISO) {
					throw new IllegalArgumentException("Standard and version is incompatible");
				}
				version = FIRecord.VERSION_ISO_20;
			} else if (args[2].equals("2.5")) {
				if (standard != BDIFStandard.ANSI) {
					throw new IllegalArgumentException("Standard and version is incompatible");
				}
				version = FIRecord.VERSION_ANSI_25;
			} else {
				throw new IllegalArgumentException("Version was not recognized");
			}

			for (int i = 3; i < args.length; i++) {
				NImage imageFromFile = null;
				NImage grayscaleImage = null;
				try {
					imageFromFile = NImage.fromFile(args[i]);
					grayscaleImage = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, imageFromFile);
					if (grayscaleImage.isResolutionIsAspectRatio() || grayscaleImage.getHorzResolution() < 250 || grayscaleImage.getVertResolution() < 250) {
						grayscaleImage.setHorzResolution(500);
						grayscaleImage.setVertResolution(500);
						grayscaleImage.setResolutionIsAspectRatio(false);
					}

					if (fi == null) {
						fi = new FIRecord(standard, version);
						if (isRecordFirstVersion(fi)) {
							fi.setPixelDepth((byte) 8);
							fi.setHorzImageResolution((int) grayscaleImage.getHorzResolution());
							fi.setHorzScanResolution((int) grayscaleImage.getHorzResolution());
							fi.setVertImageResolution((int) grayscaleImage.getVertResolution());
							fi.setVertScanResolution((int) grayscaleImage.getVertResolution());
						}
					}
					FIRFingerView fingerView = new FIRFingerView(fi.getStandard(), fi.getVersion());
					if (!isRecordFirstVersion(fi)) {
						fingerView.setPixelDepth((byte) 8);
						fingerView.setHorzImageResolution((int) grayscaleImage.getHorzResolution());
						fingerView.setHorzScanResolution((int) grayscaleImage.getHorzResolution());
						fingerView.setVertImageResolution((int) grayscaleImage.getVertResolution());
						fingerView.setVertScanResolution((int) grayscaleImage.getVertResolution());
					}
					fi.getFingerViews().add(fingerView);
					fingerView.setImage(grayscaleImage);
				} finally {
					if (imageFromFile != null) imageFromFile.dispose();
					if (grayscaleImage != null) grayscaleImage.dispose();
				}
			}

			if (fi != null) {
				NFile.writeAllBytes(args[0], fi.save());
				System.out.println("FIRecord saved to " + args[0]);
			} else {
				System.out.println("no images were added to FIRecord");
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (fi != null) fi.dispose();
		}
	}

	private static boolean isRecordFirstVersion(FIRecord record) {
		return (record.getStandard() == BDIFStandard.ANSI) && (record.getVersion().equals(FIRecord.VERSION_ANSI_10))
			   || (record.getStandard() == BDIFStandard.ISO) && (record.getVersion().equals(FIRecord.VERSION_ISO_10));
	}

}
