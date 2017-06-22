package com.neurotec.tutorials.biometrics;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.images.NImage;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.media.NMediaReader;
import com.neurotec.media.NMediaSource;
import com.neurotec.media.NMediaType;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;
import java.util.EnumSet;

public final class EnrollFaceFromImageStream {

	private static final String DESCRIPTION = "Demonstrates face enrollment from image stream";
	private static final String NAME = "enroll-face-from-image-stream";
	private static final String VERSION = "9.0.0.0";

	private static final String ADDITIONAL_COMPONENTS = "Biometrics.FaceSegmentsDetection";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [output template] [-u url]%n", NAME);
		System.out.format("\t%s [output template] [-f filename]%n", NAME);
		System.out.format("\t%s [output template] [-d directory]%n", NAME);
		System.out.println();
		System.out.println("\t[output template]  - filename to store face template.");
		System.out.println("\t[-u url] - url to RTSP stream");
		System.out.println("\t[-f filename] -  video file containing a face");
		System.out.println("\t[-d directory] - directory containing face images");
		System.out.println();
		System.out.format("\texample: %s template.dat -u rtsp://camera_url%n", NAME);
		System.out.format("\texample: %s template.dat -f video.wav%n", NAME);
		System.out.format("\texample: %s template.dat -d C:\templates%n", NAME);

	}

	public static void main(String[] args) {
		String components = "Biometrics.FaceExtraction";

		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length != 3) {
			usage();
			System.exit(1);
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFace face = null;

		try {
			if (!NLicense.obtainComponents("/local", 5000, components)) {
				System.err.println(String.format("Could not obtain licenses for components: %1$s\n", components));
				System.exit(-1);
			}
			if (!NLicense.obtainComponents("/local", 5000, ADDITIONAL_COMPONENTS)) {
				components += "," + ADDITIONAL_COMPONENTS;
			}

			biometricClient = new NBiometricClient();
			subject = new NSubject();
			face = new NFace();
			face.setHasMoreSamples(true);

			subject.getFaces().add(face);

			//Set face template size (recommended, for enroll to database, is large) (optional).
			biometricClient.setFacesTemplateSize(NTemplateSize.LARGE);

			// Detect all faces features.
			boolean additionalComponentActivated = NLicense.isComponentActivated(ADDITIONAL_COMPONENTS);
			biometricClient.setFacesDetectAllFeaturePoints(additionalComponentActivated);

			// Create NMedia reader or prepare to use a gallery
			NMediaReader reader = null;
			String[] files = null;
			if (args[1].equals("-f")) {
				reader = new NMediaReader(NMediaSource.fromFile(args[2]), EnumSet.of(NMediaType.VIDEO), true);
			} else if (args[1].equals("-u")) {
				reader = new NMediaReader(NMediaSource.fromUrl(args[2]), EnumSet.of(NMediaType.VIDEO), true);
			} else if (args[1].equals("-d")) {
				files = Utils.getDirectoryFilesList(args[2]);
			} else {
				throw new Exception("Unknown input options specified!");
			}

			// Start extraction from stream.
			boolean isReaderUsed = reader != null;
			NImage image = null;
			int i = 0;
			NBiometricStatus status = NBiometricStatus.NONE;
			NBiometricTask task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);

			if (isReaderUsed) {
				reader.start();
			}
			while (status == NBiometricStatus.NONE) {
				if (isReaderUsed) {
					image = reader.readVideoSample().getImage();
				} else {
					image = i < files.length ? NImage.fromFile(files[i++]) : null;
				}
				if (image == null) {
					break;
				}
				face.setImage(image);
				biometricClient.performTask(task);
				Throwable th = task.getError();
				if (th != null) {
					throw th;
				}
				status = task.getStatus();
				image.dispose();
			}
			if (isReaderUsed) {
				reader.stop();
			}

			// Reset HasMoreSamples value since we finished loading images
			face.setHasMoreSamples(false);

			// If loading was finished because MeadiaReaded had no more images we have to
			// finalize extraction by performing task after setting HasMoreSamples to false.
			if (image == null) {
				biometricClient.performTask(task);
				status = task.getStatus();
			}

			// Print extraction results.
			if (status == NBiometricStatus.OK) {

				// (Optional) Get face detection details if face was detected.
				for (NFace nface : subject.getFaces()) {
					for (NLAttributes attribute : nface.getObjects()) {
						System.out.println("face:");
						System.out.format("\tlocation = (%d, %d), width = %d, height = %d\n", attribute.getBoundingRect().getBounds().x, attribute.getBoundingRect().getBounds().y,
								attribute.getBoundingRect().width, attribute.getBoundingRect().height);

						if ((attribute.getRightEyeCenter().confidence > 0) || (attribute.getLeftEyeCenter().confidence > 0)) {
							System.out.println("\tfound eyes:");
							if (attribute.getRightEyeCenter().confidence > 0) {
								System.out.format("\t\tRight: location = (%d, %d), confidence = %d%n", attribute.getRightEyeCenter().x, attribute.getRightEyeCenter().y,
										attribute.getRightEyeCenter().confidence);
							}
							if (attribute.getLeftEyeCenter().confidence > 0) {
								System.out.format("\t\tLeft: location = (%d, %d), confidence = %d%n", attribute.getLeftEyeCenter().x, attribute.getLeftEyeCenter().y,
										attribute.getLeftEyeCenter().confidence);
							}
						}
						if (additionalComponentActivated) {
							if (attribute.getNoseTip().confidence > 0) {
								System.out.println("\tfound nose:");
								System.out.format("\t\tlocation = (%d, %d), confidence = %d%n", attribute.getNoseTip().x, attribute.getNoseTip().y, attribute.getNoseTip().confidence);
							}
							if (attribute.getMouthCenter().confidence > 0) {
								System.out.println("\tfound mouth:");
								System.out.printf("\t\tlocation = (%d, %d), confidence = %d%n", attribute.getMouthCenter().x, attribute.getMouthCenter().y, attribute.getMouthCenter().confidence);
							}
						}
					}
				}
				System.out.println("Template extracted.");

				// Save compressed template to file.
				NFile.writeAllBytes(args[0], subject.getTemplateBuffer());
				System.out.println("Template saved successfully.");
			} else {
				System.out.println("Extraction failed: " + status);
				Throwable th = task.getError();
				if (th != null) {
					throw th;
				}
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
			if (face != null) {
				face.dispose();
			}
			if (subject != null) {
				subject.dispose();
			}
			if (biometricClient != null) {
				biometricClient.dispose();
			}
		}
	}

}
