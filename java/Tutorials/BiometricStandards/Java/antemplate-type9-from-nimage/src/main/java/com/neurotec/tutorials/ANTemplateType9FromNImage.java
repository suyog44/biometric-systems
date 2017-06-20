package com.neurotec.tutorials;

import java.io.IOException;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType9Record;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ANTemplateType9FromNImage {

	private static final String DESCRIPTION = "Demonstrates creation of ANTemplate with type 9 record in it.";
	private static final String NAME = "antemplate-type9-from-nimage";
	private static final String VERSION = "9.0.0.0";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]%n", NAME);
		System.out.println("");
		System.out.println("\t[NImage]     - filename with image file.");
		System.out.println("\t[ANTemplate] - filename for ANTemplate.");
		System.out.println("\t[Tot] - specifies type of transaction.");
		System.out.println("\t[Dai] - specifies destination agency identifier.");
		System.out.println("\t[Ori] - specifies originating agency identifier.");
		System.out.println("\t[Tcn] - specifies transaction control number.");
		System.out.println("");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		final String components = "Biometrics.FingerExtraction,Biometrics.Standards.FingerTemplates";

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 6) {
			usage();
			System.exit(1);
		}

		ANTemplate template = null;
		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFinger finger = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println("Could not obtain licenses for components: " + components);
				System.exit(-1);
			}

			String tot = args[2]; // type of transaction
			String dai = args[3]; // destination agency identifier
			String ori = args[4]; // originating agency identifier
			String tcn = args[5]; // transaction control number

			if ((tot.length() < 3) || (tot.length() > 4)) {
				System.out.println("Tot parameter should be 3 or 4 characters length.");
				System.exit(-1);
			}

			// Create empty ANTemplate object with only type 1 record in it
			template = new ANTemplate(ANTemplate.VERSION_CURRENT, tot, dai, ori, tcn, 0);

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();

			finger.setFileName(args[0]);
			subject.getFingers().add(finger);

			biometricClient.createTemplate(subject);


			if (subject.getStatus() == NBiometricStatus.OK) {

				// Add Type 9 record to ANTemplate object
				ANType9Record record = new ANType9Record(ANTemplate.VERSION_CURRENT, 0, true, subject.getTemplate().getFingers().getRecords().get(0));
				template.getRecords().add(record);

				// Store ANTemplate object with type 9 record in file
				template.save(args[1]);
			} else {
				System.out.println("Fingerprint extraction failed");
			}
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			try {
				NLicense.releaseComponents(components);
			} catch (IOException e) {
				e.printStackTrace();
			}
			if (template != null) template.dispose();
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (finger != null) finger.dispose();
		}
	}

}
