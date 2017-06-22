package com.neurotec.tutorials.server;

import android.app.Application;
import android.util.Log;

public final class ServerTutorialsApp extends Application {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = ServerTutorialsApp.class.getSimpleName();
	private static final String APP_NAME = "server-tutorials";
	private static final String INPUT_DIR_NAME = "input";

	// ==============================================
	// Public static fields
	// ==============================================

	public static final String FILE_SEPARATOR = System.getProperty("file.separator");
	public static final String TUTORIALS_ASSETS_DIR = INPUT_DIR_NAME;


	// ==============================================
	// Private fields
	// ==============================================

	private static String sIpAddress;

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onCreate() {

		super.onCreate();
		try {
			System.setProperty("jna.nounpack", "true");
			System.setProperty("java.io.tmpdir", getCacheDir().getAbsolutePath());
		} catch (Exception e) {
			Log.e(TAG, e.toString(), e);
		}
	}

	public static String getIpAddress() {
		return sIpAddress;
	}

	public static void setIpAddress(String ipAddress) throws Exception {
		if (VerifyUtil.getInstance().verifyIpAddress(ipAddress)) {
			ServerTutorialsApp.sIpAddress = ipAddress;
		} else {
			throw new IllegalArgumentException("Invalid IP Address format");
		}
	}

}
