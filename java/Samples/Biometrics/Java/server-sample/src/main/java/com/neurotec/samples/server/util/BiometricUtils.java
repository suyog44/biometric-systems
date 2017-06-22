package com.neurotec.samples.server.util;


public final class BiometricUtils {

	public static String matchingThresholdToString(int value) {
		double p = -value / 12.0 + 2;
		int decimals = (int) Math.max(0, Math.ceil(-p));
		return String.format("%." + decimals + "f %%", Math.pow(10, p));
	}

	public static int matchingThresholdFromString(String value) {
		double p = Math.log10(Math.max(0, Math.min(1, Double.parseDouble(value.replace("%", "")) / 100)));
		return Math.max(0, (int) Math.round(-12 * p));
	}

	public static int maximalRotationToDegrees(int value) {
		return (2 * value * 360 + 256) / (2 * 256);
	}

	public static int maximalRotationFromDegrees(int value) {
		return (2 * value * 256 + 360) / (2 * 360);
	}

	// ==============================================
	// Private constructor
	// ==============================================

	private BiometricUtils() {
	}

}
