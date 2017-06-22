package com.neurotec.tutorials.biometrics;

import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.client.NClusterBiometricConnection;
import com.neurotec.io.NFile;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;



public final class IdentifyOnServer {
	
	private static final String DESCRIPTION = "Demonstrates how to identify template on server";
	private static final String NAME = "identify-on-server";
	private static final String VERSION = "9.0.0.0";
	
	private static final String DEFAULT_ADDRESS = "127.0.0.1";
	private static final int DEFAULT_PORT = 24932;

	private static void usage() {
		System.out.format("usage:\n");
		System.out.format("\t%s -s [server:port] -t [template]\n", NAME);
		System.out.format("");
		System.out.format("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)\n");
		System.out.format("\t-t template      - template to be sent for enrollment (required)\n");
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
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

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NClusterBiometricConnection connection = null;
		NBiometricTask task = null;

		try {
			biometricClient = new NBiometricClient();
			subject = createSubject(parseArgsResult.templateFile, parseArgsResult.templateFile);
			
			connection = new NClusterBiometricConnection();
			connection.setHost(parseArgsResult.serverAddress);
			connection.setAdminPort(parseArgsResult.serverPort);
			
			biometricClient.getRemoteConnections().add(connection);
			
			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.IDENTIFY), subject);
			
			biometricClient.performTask(task);
			
			if (task.getStatus() != NBiometricStatus.OK) {
				System.out.format("Identification was unsuccessful. Status: %s.\n", task.getStatus());
				if (task.getError() != null) throw task.getError();
				System.exit(-1);
			}
			
			for (NMatchingResult matchingResult : subject.getMatchingResults()) {
				System.out.format("Matched with ID: '%s' with score %d", matchingResult.getId(), matchingResult.getScore());
			}
			
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			if (task != null) task.dispose();
			if (connection != null) connection.dispose();
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
	
	private static NSubject createSubject(String fileName, String subjectId) throws IOException {
		NSubject subject = new NSubject();
		subject.setTemplateBuffer(NFile.readAllBytes(fileName));
		subject.setId(subjectId);

		return subject;
	}
	
	private static ParseArgsResult parseArgs(String[] args) throws Exception {
		ParseArgsResult result = new ParseArgsResult();
		result.serverAddress = DEFAULT_ADDRESS;
		result.serverPort = DEFAULT_PORT;

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
			case 't':
				i++;
				result.templateFile = optarg;
				break;
			default:
				throw new Exception("Wrong parameter found!");
			}
		}

		if (result.templateFile.equals("")) throw new Exception("Template - required parameter - not specified");
		return result;
	}
	
	private static class ParseArgsResult {
		private String serverAddress;
		private int serverPort;
		private String templateFile;
	}

}
