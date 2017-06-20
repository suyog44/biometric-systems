package com.neurotec.tutorials.biometrics;

import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.client.NClusterBiometricConnection;
import com.neurotec.io.NFile;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class CreateFingerTemplateOnServer {
	private static final String DESCRIPTION = "Demonstrates how to create finger template from image on server";
	private static final String NAME = "create-finger-template-on-server";
	private static final String VERSION = "9.0.0.0";

	private static final String DEFAULT_ADDRESS = "127.0.0.1";
	private static final int DEFAULT_PORT = 24932;

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s -s [server:port] -i [input image] -t [output template]%n", NAME);
		System.out.println();
		System.out.println("\t-s [server:port]   - matching server address (optional parameter, if address specified - port is optional)");
		System.out.println("\t-i [image]   - image filename to store finger image.");
		System.out.println("\t-t [output template]   - filename to store finger template.");
		System.out.println();
		System.out.println();
		System.out.println("example:");
		System.out.format("\t%s -s 127.0.0.1 -i image.jpg -t template.dat%n", NAME);
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 3) {
			usage();
			System.exit(1);
		}

		ParseArgsResult parseArgsResult = null;

		try {
			parseArgsResult = parseArgs(args);
		} catch (Exception e) {
			usage();
			System.exit(-1);
		}

		try {
			NBiometricClient biometricClient = new NBiometricClient();
			NSubject subject = new NSubject();
			NFinger finger = new NFinger();

			// perform all biometric operations on remote server only
			biometricClient.setLocalOperations(EnumSet.noneOf(NBiometricOperation.class));
			NClusterBiometricConnection connection = new NClusterBiometricConnection();
			connection.setHost(parseArgsResult.serverAddress);
			connection.setAdminPort(parseArgsResult.serverPort);
			biometricClient.getRemoteConnections().add(connection);

			finger.setSampleBuffer(NFile.readAllBytes(parseArgsResult.imageFile));

			subject.getFingers().add(finger);
			biometricClient.setFingersTemplateSize(NTemplateSize.LARGE);

			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (status == NBiometricStatus.OK) {
				System.out.println("Template extracted");
				NFile.writeAllBytes(parseArgsResult.templateFile, subject.getTemplateBuffer());
				System.out.println("Template saved successfully");
			} else {
				System.out.format("Extraction failed: %s\n", status);
			}

		} catch (Throwable th) {
			Utils.handleError(th);
		}
	}

	private static ParseArgsResult parseArgs(String[] args) throws Exception {
		ParseArgsResult result = new ParseArgsResult();
		result.serverAddress = DEFAULT_ADDRESS;
		result.serverPort = DEFAULT_PORT;

		result.imageFile = "";
		result.templateFile = "";

		for (int i = 0; i < args.length; i++) {
			String optarg = "";

			if (args[i].length() != 2 || args[i].charAt(0) != '-') {
				throw new Exception("Parameter parse error");
			}

			if (args.length > i + 1 && args[i + 1].charAt(0) != '-') {
				optarg = args[i + 1]; // we have a parameter for given flag
			}

			if (optarg == "") {
				throw new Exception("Parameter parse error");
			}

			switch (args[i].charAt(1)) {
			case 's':
				i++;
				if (optarg.contains(":")) {
					String[] splitAddress = optarg.split(":");
					result.serverAddress = splitAddress[0];
					result.serverPort = Integer.parseInt(splitAddress[1]);
				} else {
					result.serverAddress = optarg;
					result.serverPort = DEFAULT_PORT;
				}
				break;
			case 'i':
				i++;
				result.imageFile = optarg;
				break;
			case 't':
				i++;
				result.templateFile = optarg;
				break;
			default:
				throw new Exception("Wrong parameter found!");
			}
		}
		if (result.imageFile.equals("")) throw new Exception("Image - required parameter - not specified");
		if (result.templateFile.equals("")) throw new Exception("Template - required parameter - not specified");
		return result;
	}

	private static class ParseArgsResult {
		private String serverAddress;
		private int serverPort;
		private String imageFile;
		private String templateFile;
	}
}