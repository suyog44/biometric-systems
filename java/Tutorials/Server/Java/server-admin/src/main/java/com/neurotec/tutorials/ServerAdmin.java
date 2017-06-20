package com.neurotec.tutorials;

import com.neurotec.cluster.Admin;
import com.neurotec.cluster.ClusterNodeState;
import com.neurotec.cluster.ClusterShortInfo;
import com.neurotec.cluster.ClusterTaskInfo;
import com.neurotec.cluster.ServerStatus;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

public final class ServerAdmin {

	private static final String DESCRIPTION = "Demonstrates how to administrate matching server.";
	private static final String NAME = "server-admin";
	private static final String VERSION = "9.0.0.0";

	private static final int DEFAULT_PORT = 24932;

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [server:port] [command]%n", NAME);
		System.out.println("\tserver:port - matching server address (port is optional)");
		System.out.println();
		System.out.println("\tcommands:");
		System.out.println("\t\tstart                - Start cluster");
		System.out.println("\t\tstop <id>            - Stop (wait until finished task in progress) server (id is 0) or node (id is above or equal 4)");
		System.out.println("\t\tkill <id>            - Instantly stop server (id is 0) or node (id is above or equal 4)");
		System.out.println("\t\tinfo <info type>     - Print info about cluster or nodes");
		System.out.println("\t\t\tinfo type: tasks_short | tasks_complete | nodes | results");
		System.out.println("\t\tdbupdate             - DB update");
		System.out.println("\t\tdbchanged <id>      - Notify server of changed templates in DB");
		System.out.println("\t\tdbflush              - Flush the database");
		System.out.println("\t\tstatus               - Gets current server status");
		System.out.println();
	}

	public static void main(String[] args) throws Throwable {
		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 2) {
			usage();
			System.exit(-1);
		}

		String server = null;
		int port = -1;

		try {
			if (args[0].contains(":")) {
				String[] splitAddress = args[0].split(":");
				server = splitAddress[0];
				port = Integer.parseInt(splitAddress[1]);
			} else {
				server = args[0];
				port = DEFAULT_PORT;
				System.out.println("Port not specified; using default: " + DEFAULT_PORT);
				System.out.println();
			}
		} catch (Exception e) {
			System.err.println("Server address in wrong format.");
			System.exit(-1);
		}

		String[] cmd = new String[args.length - 1];
		for (int i = 0; i < cmd.length; i++) {
			cmd[i] = args[i + 1];
		}

		ServerAdmin serverAdmin = new ServerAdmin(server, port);
		serverAdmin.executeCommand(cmd);
	}

	private String server;
	private int port;

	public ServerAdmin(String server, int port) {
		this.server = server;
		this.port = port;
	}

	private int getNodeId(String idString) {
		if (idString.equals("server")) {
			return 0;
		}

		try {
			return Integer.parseInt(idString);
		} catch (Exception e) {
			return -1;
		}
	}

	public void executeCommand(String[] cmd) throws Throwable {
		Admin admin = new Admin(server, port);
		try {
			int nodeId;
			if (cmd[0].equals("start")) {
				admin.clusterStart();
				System.out.println("start command sent");
			} else if (cmd[0].equals("stop")) {
				if (cmd.length >= 2 && ((nodeId = getNodeId(cmd[1])) != -1)) {
					admin.nodeStop(nodeId);
					System.out.format("stop node %d command sent%n", nodeId);
				} else
					System.out.println("Missing parameter: id");
			} else if (cmd[0].equals("kill")) {
				if (cmd.length >= 2 && ((nodeId = getNodeId(cmd[1])) != -1)) {
					if (nodeId == 0) {
						admin.serverKill();
					} else {
						admin.nodeKill(nodeId);
					}
					System.out.format("kill %d command sent%n", nodeId);
				} else
					System.out.println("Missing parameter: id");
			} else if (cmd[0].equals("info")) {
				if (cmd.length >= 2) {
					if (cmd[1].equals("tasks_short")) {
						ClusterShortInfo[] info = admin.getClusterShortInfo();
						printShortRunningTasksInfo(info);
					} else if (cmd[1].equals("tasks_complete")) {
						ClusterTaskInfo[] info = admin.getClusterTaskInfo();
						printCompleteRunningTasksInfo(info);
					} else if (cmd[1].equals("nodes")) {
						ClusterNodeState[] info = admin.getClusterNodeInfo();
						printNodesInfo(info);
					} else if (cmd[1].equals("results")) {
						int[] info = admin.getResultIds();
						printTaskResultsInfo(info);
					} else
						System.out.println("Unknown info type: " + cmd[1]);
				} else {
					System.out.println("Missing parameter: info type");
				}
			} else if (cmd[0].equals("dbupdate")) {
				admin.updateDatabase();
				System.out.println("dbupdate command sent");
			} else if (cmd[0].equals("dbchanged")) {
				if (cmd.length >= 2) {
					String[] updateIDs = new String[cmd.length - 1];
					System.arraycopy(cmd, 1, updateIDs, 0, cmd.length - 1);
					admin.updateDBRecords(updateIDs);
					System.out.println("dbchanged command sent");
				} else {
					System.out.println("Missing parameters: Ids of records to update.");
				}
			} else if (cmd[0].equals("dbflush")) {
				admin.flush();
				System.out.println("dbflush command sent");
			} else if (cmd[0].equals("status")) {
				ServerStatus status = admin.getServerStatus();
				if (status != null) {
					System.out.println("Server status is: " + status);
				} else {
					System.out.println("Error while getting server info");
				}
			} else {
				System.out.println("Command not recognized.");
			}
		} catch (Exception e) {
			e.printStackTrace();
		} finally {
			if (admin != null) {
				admin.finalize();
			}
		}
	}

	private void printTaskResultsInfo(int[] info) {
		if (info == null) return;
		System.out.format("%s completed task(s):%n", info.length);
		for (int result : info) {
			System.out.println(result);
		}
		System.out.println();
	}

	private void printNodesInfo(ClusterNodeState[] info) {
		if (info == null) return;
		System.out.format("%s node(s) running:%n", info.length);
		for (ClusterNodeState nodeInfo : info) {
			System.out.format("%d (%s)%n", nodeInfo.getID(), nodeInfo.getState());
		}
		System.out.println();
	}

	private void printCompleteRunningTasksInfo(ClusterTaskInfo[] info) {
		if (info == null) return;
		System.out.format("%d task(s):%n", info.length);
		for (ClusterTaskInfo taskInfo : info) {
			System.out.println("\tid: " + taskInfo.getTaskId());
			System.out.println("\tprogress: " + taskInfo.getTaskProgress());
			System.out.println("\tnodes completed: " + taskInfo.getNodesCompleted());
			System.out.println("\tworking nodes: " + taskInfo.getWorkingNodesCount());
			for (int i = 0; i < taskInfo.getWorkingNodesCount(); i++) {
				System.out.println("\t\tnode ID: " + taskInfo.getWorkingNodesInfo()[i].getNodeId());
				System.out.println("\t\tnode progress: " + taskInfo.getWorkingNodesInfo()[i].getProgress());
			}
		}
		System.out.println();

	}

	private void printShortRunningTasksInfo(ClusterShortInfo[] info) {
		System.out.format("%d node(s) running:%n", info.length);
		for (ClusterShortInfo shortInfo : info) {
			System.out.println("\tid: " + shortInfo.getTaskId());
			System.out.println("\tnodes completed: " + shortInfo.getNodesCompleted());
			System.out.println("\tworking nodes: " + shortInfo.getWorkingNodesCount());
		}
		System.out.println();
	}
}
