package com.neurotec.samples.abis.util;

import java.io.File;

import com.neurotec.samples.util.Utils;

public class SettingsUtils {

	// ===========================================================
	// Public static fields
	// ===========================================================

	public static final String PROJECT_NAME = "abis-sample";

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static String pathname = Utils.getHomeDirectory() + Utils.FILE_SEPARATOR + ".neurotec" + Utils.FILE_SEPARATOR + PROJECT_NAME;

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static String getSettingsFolder() {
		File settingsFolder = new File(pathname);
		if (!settingsFolder.exists()) {
			settingsFolder.mkdirs();
		}
		return settingsFolder.getAbsolutePath();
	}

}
