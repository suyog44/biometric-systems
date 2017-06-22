package com.neurotec.tutorials.server;

import java.util.regex.Pattern;

public final class VerifyUtil {

	// ==============================================
	// Private static fields
	// ==============================================

	private static VerifyUtil sInstance;

	// ==============================================
	// Public static methods
	// ==============================================

	public static VerifyUtil getInstance() {
		synchronized (VerifyUtil.class) {
			if (sInstance == null) {
				sInstance = new VerifyUtil();
			}
			return sInstance;
		}
	}

	// ==============================================
	// Private Constructor
	// ==============================================

	private VerifyUtil() {

	}

	// ==============================================
	// Public methods
	// ==============================================

	public boolean verifyIpAddress(String ipAddress) {
		if (ipAddress != null) {
			Pattern ipAddressPattern = Pattern.compile("^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\"
					+ ".([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])$");
			return ipAddressPattern.matcher(ipAddress).matches();

		} else {
			return false;
		}
	}

}
