package com.neurotec.samples.server.util;

import java.awt.Container;

import javax.swing.JOptionPane;

public final class MessageUtils {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String ERROR_TITLE = "Error";
	private static final String INFORMATION_TITLE = "Information";
	private static final String QUESTION_TITLE = "Question";

	// ==============================================
	// Public static methods
	// ==============================================

	public static String getCurrentApplicationName() {
		return "ServerSampleJava";
	}

	public static void showError(Container owner, String message) {
		JOptionPane.showMessageDialog(owner, message, String.format("%s: %s", getCurrentApplicationName(), ERROR_TITLE), JOptionPane.ERROR_MESSAGE);
	}

	public static void showError(Container owner, Exception exception) {
		if (exception == null) throw new NullPointerException("exception");
		exception.printStackTrace();
		showError(owner, exception.toString());
	}

	public static void showError(Container owner, String format, Object[] args) {
		String str = String.format(format, args);
		showError(owner, str);
	}

	public static void showInformation(Container owner, String message) {
		JOptionPane.showMessageDialog(owner, message, String.format("%s: %s", getCurrentApplicationName(), INFORMATION_TITLE), JOptionPane.INFORMATION_MESSAGE);
	}

	public static void showInformation(Container owner, String format, Object[] args) {
		String str = String.format(format, args);
		showInformation(owner, str);
	}

	public static boolean showQuestion(Container owner, String message) {
		return JOptionPane.YES_OPTION == JOptionPane.showConfirmDialog(owner, message, String.format("%s: %s", getCurrentApplicationName(), QUESTION_TITLE),
				JOptionPane.YES_NO_OPTION);
	}

	public static boolean showQuestion(Container owner, String format, Object[] args) {
		String str = String.format(format, args);
		return showQuestion(owner, str);
	}

	// ==============================================
	// Private constructor
	// ==============================================

	private MessageUtils() {
	}

}
