package com.neurotec.samples.server.connection;

import com.neurotec.cluster.Admin;
import com.neurotec.samples.server.settings.Settings;

public class ServerConnection {

	// ==============================================
	// Public static methods
	// ==============================================

	public static ServerConnection createInstance() {
		Settings settings = Settings.getInstance();
		if (settings.isServerModeAccelerator()) {
			return new AcceleratorConnection(settings.getServer(), settings.getClientPort(), settings.getAdminPort(), settings.getMMAUser(),
					settings.getMMAPassword());
		}
		return new ServerConnection(settings.getServer(), settings.getClientPort(), settings.getAdminPort());
	}

	public static boolean checkConnection(String serverAddress, int adminPort) throws Throwable {
		Admin admin = null;
		try {
			admin = new Admin(serverAddress, adminPort);
			if (admin.getClusterNodeInfo() != null) {
				return true;
			}
		} catch (Exception e) {
			return false;
		} finally {
			if (admin != null) {
				try {
					admin.finalize();
				} catch (Exception e) {
					throw e;
				}
			}
		}
		return false;
	}

	// ==============================================
	// Private fields
	// ==============================================

	private final String server;
	private final int clientPort;
	private final int adminPort;

	// ==============================================
	// Public constructor
	// ==============================================

	public ServerConnection(String url, int clientPort, int adminPort) {
		this.server = url;
		this.clientPort = clientPort;
		this.adminPort = adminPort;
	}

	// ==============================================
	// Public methods
	// ==============================================

	public final String getServer() {
		return server;
	}

	public final int getClientPort() {
		return clientPort;
	}

	public final int getAdminPort() {
		return adminPort;
	}

	public final boolean checkConnection() throws Throwable {
		return checkConnection(server, adminPort);
	}

}
