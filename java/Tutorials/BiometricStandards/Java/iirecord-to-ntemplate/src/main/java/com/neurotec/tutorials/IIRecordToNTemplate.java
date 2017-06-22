package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.IIRIrisImage;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class IIRecordToNTemplate {

	private static final String DESCRIPTION = "Create NTemplate from IIRecord tutorial";
	private static final String NAME = "iirecord-to-ntemplate";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.format("usage: %s [IIRecord] [NTemplate]%n", NAME);
		System.out.println("\t[IIRecord]  - input IIRecord");
		System.out.println("\t[NTemplate] - output NTemplate");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.IrisExtraction,Biometrics.Standards.Irises";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(1);
		}

		IIRecord iiRec = null;
		NBiometricClient biometricClient = null;
		NSubject subject = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();

			// Read IIRecord from file
			NBuffer iiRecordData = NFile.readAllBytes(args[0]);

			// Create IIRecord
			iiRec = new IIRecord(iiRecordData, BDIFStandard.ISO);

			// Read all images from IIRecord
			for (IIRIrisImage irisImage : iiRec.getIrisImages()) {
				NIris iris = new NIris();
				iris.setImage(irisImage.toNImage());
				subject.getIrises().add(iris);
			}

			// Set iris template size (recommended, for enroll to database, is large) (optional)
			biometricClient.setIrisesTemplateSize(NTemplateSize.LARGE);

			// Create template from added iris image(s)
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
			if (iiRec != null) iiRec.dispose();
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
		}
	}
}
