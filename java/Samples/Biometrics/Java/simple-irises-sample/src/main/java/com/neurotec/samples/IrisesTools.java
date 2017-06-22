package com.neurotec.samples;

import java.io.IOException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.licensing.NLicense;

public final class IrisesTools {

	// ===========================================================
	// Public static fields
	// ===========================================================

	private static IrisesTools instance;

	private static final String ADDRESS = "/local";
	private static final String PORT = "5000";

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static IrisesTools getInstance() {
		synchronized (IrisesTools.class) {
			if (instance == null) {
				instance = new IrisesTools();
			}
			return instance;
		}
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private final Map<String, Boolean> licenses;
	private final NBiometricClient client;
	private final NBiometricClient defaultClient;

	// ===========================================================
	// Private constructor
	// ===========================================================

	private IrisesTools() {
		licenses = new HashMap<String, Boolean>();
		client = new NBiometricClient();
		defaultClient = new NBiometricClient();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private boolean isLicenseObtained(String license) {
		if (licenses.containsKey(license)) {
			return licenses.get(license);
		} else {
			return false;
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public boolean obtainLicenses(List<String> names) throws IOException {
		boolean result = true;
		for (String license : names) {
			if (isLicenseObtained(license)) {
				System.out.println(license + ": " + " already obtained");
			} else {
				boolean state = NLicense.obtainComponents(ADDRESS, PORT, license);
				licenses.put(license, state);
				if (state) {
					System.out.println(license + ": obtainted");
				} else {
					result = false;
					System.out.println(license + ": not obtained");
				}
			}
		}
		return result;
	}

	public void releaseLicenses() {
		Set<Entry<String, Boolean>> entries = licenses.entrySet();
		StringBuilder sb = new StringBuilder(256);
		for (Entry<String, Boolean> entry : entries) {
			if (entry.getValue()) {
				sb.append(entry.getKey()).append(',');
			}
		}
		if (sb.length() > 0) {
			sb.deleteCharAt(sb.length() - 1);
			try {
				System.out.print("Releasing licenses... ");
				NLicense.releaseComponents(sb.toString());
				System.out.print("done.\n");
				licenses.clear();
			} catch (IOException e) {
				e.printStackTrace();
			}
		} else {
			System.out.print("Releasing licenses... Nothing to release.\n");
		}
	}

	public NBiometricClient getClient() {
		return client;
	}

	public NBiometricClient getDefaultClient() {
		return defaultClient;
	}

}
