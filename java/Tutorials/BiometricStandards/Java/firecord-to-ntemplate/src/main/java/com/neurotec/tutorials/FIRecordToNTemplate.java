package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FIRFingerView;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class FIRecordToNTemplate {
	private static final String DESCRIPTION = "Convert FIRecord to NTemplate tutorial";
	private static final String NAME = "firecord-to-ntemplate";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage: %s [FIRecord] [NTemplate]%n", NAME);
		System.out.println("\t[FIRecord]  - input FIRecord");
		System.out.println("\t[NTemplate] - output NTemplate");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.FingerExtraction,Biometrics.Standards.Fingers";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		FIRecord fiRec = null;
		NBiometricClient biometricClient = null;
		NSubject subject = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();

			// Read FIRecord from file
			NBuffer fiRecordData = NFile.readAllBytes(args[0]);

			// Create FIRecord
			fiRec = new FIRecord(fiRecordData, BDIFStandard.ISO);

			// Read all images from FIRecord
			for (FIRFingerView fv : fiRec.getFingerViews()) {
				NFinger finger = new NFinger();
				finger.setImage(fv.toNImage());
				subject.getFingers().add(finger);
			}

			// Set finger template size (recommended, for enroll to database, is large) (optional)
			biometricClient.setFingersTemplateSize(NTemplateSize.LARGE);

			// Create template from added finger image(s)
			NBiometricStatus status = biometricClient.createTemplate(subject);

			System.out.println(status == NBiometricStatus.OK
					? "Template extracted"
					: String.format("Extraction failed: %s", status));

			if (status == NBiometricStatus.OK) {
				NFile.writeAllBytes(args[1], subject.getTemplateBuffer());
				System.out.println("NTemplate saved to file " + args[1]);
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (fiRec != null) fiRec.dispose();
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
		}
	}
}
