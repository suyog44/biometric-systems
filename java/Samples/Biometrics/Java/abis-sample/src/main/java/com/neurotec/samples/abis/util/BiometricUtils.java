package com.neurotec.samples.abis.util;

public final class BiometricUtils {

	public static int qualityToPercent(byte value) {
		return (2 * value * 100 + 255) / (2 * 255);
	}

	public static byte qualityFromPercent(int value) {
		return (byte) ((2 * value * 255 + 100) / (2 * 100));
	}

	public static String getMatchingThresholdToString(int value) {
		double p = -value / 12.0;
		return String.format(
				String.format("%%.%df %%%%",
						Math.max(0, (int) Math.ceil(-p) - 2)), Math.pow(10, p) * 100);
	}

	public static int getMatchingThresholdFromString(String value) {
		double p = Math.log10(Math.max(Double.MIN_VALUE, Math.min(1, Double.parseDouble(value.replace("%", "")) / 100)));
		return Math.max(0, (int) Math.round(-12 * p));
	}

	public static boolean isMatchingThresholdEqual(int thresholdValue, String percentString) {
		if (percentString == null) return false;
		int intVal = getMatchingThresholdFromString(percentString);
		return (intVal == thresholdValue);
	}

	public static int getMaximalRotationToDegrees(int value) {
		return (2 * value * 360 + 256) / (2 * 256);
	}

	public static int getMaximalRotationFromDegrees(int value) {
		return (int) ((2 * value * 256 + 360) / (2 * 360));
	}
}
