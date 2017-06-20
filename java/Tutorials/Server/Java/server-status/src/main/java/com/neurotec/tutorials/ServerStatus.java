package com.neurotec.tutorials;

import com.neurotec.cluster.Admin;
import com.neurotec.cluster.ClusterNodeInfo;
import com.neurotec.cluster.ClusterNodeState;
import com.neurotec.cluster.ClusterTaskInfo;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ServerStatus {

	private static final String DESCRIPTION = "Displays various information about a matching server and nodes.";
	private static final String NAME = "server-status";
	private static final String VERSION = "9.0.0.0";

	private static final int DEFAULT_PORT = 24932;

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [server:port]%n", NAME);
		System.out.println("\tserver:port - matching server address (port is optional)");
		System.out.println();
	}

	public static void main(String[] args) throws Throwable {
		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 1) {
			usage();
			System.exit(-1);
		}

		String server = null;
		int port = 0;

		try {
			if (args[0].contains(":")) {
				String[] splitAddress = args[0].split(":");
				server = splitAddress[0];
				port = Integer.parseInt(splitAddress[1]);
			} else {
				server = args[0];
				port = DEFAULT_PORT;
				System.out.println("port not specified; using default: " + DEFAULT_PORT);
				System.out.println();
			}
		} catch (Exception e) {
			System.err.println("Server address in wrong format.");
			System.exit(-1);
		}

		ServerStatus serverStatus = new ServerStatus(server, port);
		serverStatus.printServerStatus();
	}

	private final String server;
	private final int port;

	public ServerStatus(String server, int port) {
		this.server = server;
		this.port = port;
	}

	private void printServerStatus() throws Throwable {
		Admin admin = null;
		try {
			System.out.format("Asking info from %s: %d ...%n%n", server, port);
			System.out.println("Requesting info about server ...");

			admin = new Admin(server, port);
			com.neurotec.cluster.ServerStatus status = admin.getServerStatus();
			if (status != null) {
				System.out.println("Server status: " + status);
			} else {
				System.out.println("Unable to determine server status");
			}

			System.out.println();
			System.out.println("Requesting info about nodes ...");
			ClusterNodeState[] states = admin.getClusterNodeInfo();
			if (states != null) {
				System.out.format("%d node(s) running:%n", states.length);
				for (ClusterNodeState item : states) {
					System.out.format("%d (%s)%n", item.getID(), item.getState());
				}
			} else {
				System.out.println("Failed to recieve info about nodes");
			}

			System.out.println();
			System.out.println("Requesting info about tasks ...");
			ClusterTaskInfo[] tasks = admin.getClusterTaskInfo();
			if (tasks != null) {
				System.out.format("%d task(s):%n", tasks.length);
				for (ClusterTaskInfo taskInfo : tasks) {
					System.out.println("\tid: " + taskInfo.getTaskId());
					System.out.println("\tprogress: " + taskInfo.getTaskProgress());
					System.out.println("\tnodes completed: " + taskInfo.getNodesCompleted());
					System.out.println("\tworking nodes: " + taskInfo.getWorkingNodesCount());
					for (ClusterNodeInfo info : taskInfo.getWorkingNodesInfo()) {
						System.out.println("\t\tnode ID: " + info.getNodeId());
						System.out.println("\t\tnode progress: " + info.getProgress());
					}
				}
			} else {
				System.out.println("Failed to receive tasks info");
			}

			System.out.println();
			System.out.println("Requesting info about results ...");

			int[] results = admin.getResultIds();
			if (results != null) {

				System.out.format("%d completed task(s):%n", results.length);
				for (int result : results) {
					System.out.println(result);
				}
				System.out.println();
			} else {
				System.out.println("Failed to receive results info");
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			if (admin != null) {
				admin.finalize();
			}
		}
	}
}
