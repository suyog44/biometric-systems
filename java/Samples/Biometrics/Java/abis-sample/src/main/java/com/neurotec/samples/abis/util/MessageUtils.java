package com.neurotec.samples.abis.util;

import com.neurotec.samples.util.Utils;
import com.neurotec.util.concurrent.AggregateExecutionException;

import java.awt.Component;

import javax.swing.JOptionPane;

public final class MessageUtils {

	public static void showError(Component component, Throwable error, String message) {
		String msg;
		if (!Utils.isNullOrEmpty(message)) {
			msg = message;
		} else {
			msg = "An error occurred: " + error;
		}
		String reason = null;
		if (error.getMessage() != null) {
			reason = error.getMessage();
		} else if (error.getCause() != null) {
			reason = error.getCause().getMessage();
		}

		if (!Utils.isNullOrEmpty(reason)) {
			msg.concat(System.getProperty("line.separator") + "Reason: " + reason);
		}

		showError(component, "Error", msg);
	}

	public static void showError(Component parentComponent, Throwable e) {
		if (e != null) {
			e.printStackTrace();
			if (e instanceof AggregateExecutionException) {
				StringBuilder sb = new StringBuilder(64);
				sb.append("Execution resulted in one or more errors:\n");
				for (Throwable cause : ((AggregateExecutionException) e).getCauses()) {
					sb.append(cause.toString()).append('\n');
				}
				showError(parentComponent, "Execution failed", sb.toString());
			} else {
				showError(parentComponent, e, null);
			}
		}
	}

	public static void showError(Component parentComponent, String dialogTitle, String message) {
		JOptionPane.showMessageDialog(parentComponent, message, dialogTitle, JOptionPane.ERROR_MESSAGE);
	}

	public void showWarningDialog(Component parentComponent, String dialogTitle, String msg) {
		JOptionPane.showMessageDialog(parentComponent, msg, dialogTitle, JOptionPane.WARNING_MESSAGE);
	}

	public static boolean showQuestion(Component component, String title, String format, Object... args) {
		String str = String.format(format, args);
		int n = JOptionPane.showConfirmDialog(component, str, title, JOptionPane.YES_NO_OPTION);
		if (n == JOptionPane.YES_OPTION) {
			return true;
		}
		return false;
	}

	/**
	 * Shows the information message with the exclamation mark.
	 * @param parentComponent Parent component of the message.
	 * @param title Title of the message.
	 * @param format Message format.
	 * @param args Message arguments.
	 */
	public static void showInformation(Component parentComponent, String title, String format, Object... args) {
		String str = String.format(format, args);
		showInformation(parentComponent, title, str);
	}

	public static void showInformation(Component parentComponent, String message) {
		JOptionPane.showMessageDialog(parentComponent, message, "Information", JOptionPane.INFORMATION_MESSAGE);
	}

	/**
	 * Shows the information message with the exclamation mark.
	 * @param parentComponent Parent component of the message.
	 * @param title Title of the message.
	 * @param message Error message.
	 */
	public static void showInformation(Component parentComponent, String title, String message) {
		JOptionPane.showMessageDialog(parentComponent, message, title, JOptionPane.INFORMATION_MESSAGE);
	}

	private MessageUtils() {
		// Suppress default constructor for noninstantiability.
	}

}
