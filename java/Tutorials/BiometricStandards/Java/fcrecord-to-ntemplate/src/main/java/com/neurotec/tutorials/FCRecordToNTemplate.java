package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FCRFaceImage;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class FCRecordToNTemplate {

	private static final String DESCRIPTION = "Create NTemplate from FCRecord tutorial";
	private static final String NAME = "fcrecord-to-ntemplate";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage: %s [FCRecord] [NTemplate]%n", NAME);
		System.out.println("\t[FCRecord]  - input FCRecord");
		System.out.println("\t[NTemplate] - output NTemplate");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.FaceExtraction,Biometrics.Standards.Faces";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		FCRecord fcRec = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();

			// Read FCRecord from file
			NBuffer fcRecordData = NFile.readAllBytes(args[0]);
			// Create FCRecord
			fcRec = new FCRecord(fcRecordData, BDIFStandard.ISO);

			// Read all images from FCRecord
			for (FCRFaceImage fv : fcRec.getFaceImages()) {
				NFace face = new NFace();
				face.setImage(fv.toNImage());
				subject.getFaces().add(face);
			}

			// Set face template size (recommended, for enroll to database, is large) (optional)
			biometricClient.setFacesTemplateSize(NTemplateSize.LARGE);

			// Create template from added face image(s)
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
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (fcRec != null) fcRec.dispose();
		}
	}
}
