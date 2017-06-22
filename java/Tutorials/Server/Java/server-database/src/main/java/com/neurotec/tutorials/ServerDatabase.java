package com.neurotec.tutorials;

import java.io.FileInputStream;

import com.neurotec.cluster.Admin;
import com.neurotec.cluster.InsertDeleteResult;
import com.neurotec.cluster.InsertDeleteStatus;
import com.neurotec.cluster.InsertDeleteTemplateResult;
import com.neurotec.lang.NThrowable;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ServerDatabase {

	private static final String DESCRIPTION = "Displays various information about a matching server and nodes.";
	private static final String NAME = "server-status";
	private static final String VERSION = "9.0.0.0";

	private static final int DEFAULT_PORT = 24932;
	private static final String DEFAULT_ADRESS = "127.0.0.1";

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s -s [server:port] -c [command] -i [template id] -t [template]%n", NAME);
		System.out.println("");
		System.out.println("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)");
		System.out.println("\t-c command       - command to be performed (either insert or delete) (required)");
		System.out.println("\t-i template id   - id of template to be deleted or inserted (required)");
		System.out.println("\t-t template      - template to be inserted (valid for insert to MegaMatcher Accelerator only)");
		System.out.println("examples:");
		System.out.format("\t%s -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp %n", NAME);
		System.out.format("\t%s -s 127.0.0.1:24932 -c delete -i testId%n", NAME);
	}

	public static void main(String[] args) throws Throwable {
		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);
		if (args.length < 2) {
			usage();
			System.exit(-1);
		}

		Admin admin = null;
		ParseArgsResult results = null;
		try {
			results = parseArgs(args);
		} catch (Exception e) {
			System.err.println(e.getMessage());
			usage();
			System.exit(-1);
		}

		try {
			admin = new Admin(results.getServerIp(), results.getServerPort());
			int taskId = -1;
			switch (results.getTaskType()) {
			case INSERT: {
				String[] ids = new String[] { results.getId() };
				byte[][] templates = new byte[1][];
				FileInputStream fis = new FileInputStream(results.getTemplateFile());
				templates[0] = new byte[fis.available()];
				fis.read(templates[0]);
				fis.close();
				taskId = admin.insertDBRecords(ids, templates);
			}
				break;
			case DELETE: {
				String[] ids = new String[] { results.getId() };
				taskId = admin.deleteDBRecords(ids);
			}
				break;
			default:
				usage();
				return;
			}

			waitForResult(taskId, results.getTaskType(), admin);
		} catch (Throwable th) {
			System.err.println(((Exception)th).getMessage());

			int errorCode = -1;

			if (th instanceof NThrowable) {
				errorCode = ((NThrowable)th).getCode();
			}

			System.exit(errorCode);
		} finally {
			if (admin != null) {
				admin.finalize();
			}
		}
	}

	private static void waitForResult(int taskId, TaskType type, Admin admin) throws Exception {
		System.out.println("Waiting for results ...");
		InsertDeleteStatus status = InsertDeleteStatus.waiting;
		InsertDeleteResult result;

		do {
			switch (type) {
			case INSERT:
				result = admin.getInsertResult(taskId);
				if (result != null) {
					status = result.getInsertDeleteStatus();
				} else
					throw new Exception("Failed to get insert results");
				break;
			case DELETE:
				result = admin.getDeleteResult(taskId);
				if (result != null) {
					status = result.getInsertDeleteStatus();
				} else
					throw new Exception("Failed to get delete results");
			default:
				throw new Exception("Invalid task type");
			}

			if (status == InsertDeleteStatus.waiting) {
				System.out.format("Waiting for \"%s\" task result ...%n", type);
				Thread.sleep(100);
			}
		} while (status == InsertDeleteStatus.waiting);

		switch (status) {
		case succeeded:
			System.out.format("%s task succeeded%n", type);
			break;
		case failed:
		case notReady:
		case partiallySucceeded:
			if (status == InsertDeleteStatus.partiallySucceeded) {
				System.out.format("%s task partially succeded%n", type);
			} else if (status == InsertDeleteStatus.notReady) {
				System.out.format("%s task failed - server is not yet ready%n", type);
			} else if (status == InsertDeleteStatus.failed) {
				System.out.format("%s task failed%n", type);
			}

			int i = 0;
			for (InsertDeleteTemplateResult r : result.getInsertDeleteTemplateResult()) {
				System.out.format("Template %d status: %s%n", i++, r.getStatus());
			}
			break;
		default:
			System.out.println("Unknown result: " + status);
			break;
		}
	}

	private static ParseArgsResult parseArgs(String[] args) throws Exception {
		ParseArgsResult result = new ParseArgsResult();

		for (int i = 0; i < args.length; i++) {
			String optarg = "";

			if (args[i].length() != 2 || args[i].charAt(0) != '-') {
				throw new Exception("parameter parse error");
			}

			if (args.length > i + 1 && args[i + 1].charAt(0) != '-') {
				optarg = args[i + 1]; // we have a parameter for given flag
			}

			if (optarg.equals("")) {
				throw new Exception("Parameter parse error");
			}

			switch (args[i].charAt(1)) {
			case 's':
				i++;
				if (optarg.contains(":")) {
					String[] splitAddress = optarg.split(":");
					result.setServerIp(splitAddress[0]);
					result.setServerPort(Integer.parseInt(splitAddress[1]));
				} else {
					result.setServerIp(optarg);
					result.setServerPort(DEFAULT_PORT);
				}
				break;
			case 't':
				i++;
				result.setTemplateFile(optarg);
				break;
			case 'c':
				i++;
				result.setTaskType(TaskType.valueOf(optarg.toUpperCase()));
				break;
			case 'i':
				i++;
				result.setId(optarg);
				break;
			default:
				throw new Exception("Wrong parameter found!");
			}
		}

		System.out.println("Selecting task type: " + result.getTaskType());

		if (result.getId().equals("")) throw new Exception("id - required parameter - not specified");
		if (result.getTaskType() == TaskType.INSERT) {
			if (result.getTemplateFile().equals("")) throw new Exception("template - required parameter - not specified");
		}
		return result;
	}

	private static class ParseArgsResult {
		private String serverIp;
		private int serverPort;
		private String id;
		private TaskType taskType;
		private String templateFile;

		public String getServerIp() {
			return serverIp;
		}

		public void setServerIp(String value) {
			serverIp = value;
		}

		public int getServerPort() {
			return serverPort;
		}

		public void setServerPort(int value) {
			serverPort = value;
		}

		public String getId() {
			return id;
		}

		public void setId(String value) {
			id = value;
		}

		public TaskType getTaskType() {
			return taskType;
		}

		public void setTaskType(TaskType value) {
			taskType = value;
		}

		public String getTemplateFile() {
			return templateFile;
		}

		public void setTemplateFile(String value) {
			templateFile = value;
		}

		public ParseArgsResult() {
			serverIp = DEFAULT_ADRESS;
			serverPort = DEFAULT_PORT;
			id = "";
			taskType = TaskType.INSERT;
			templateFile = "";
		}
	}

	private enum TaskType {
		INSERT, DELETE;
	}
}
