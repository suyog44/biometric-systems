package com.neurotec.samples.abis.util;

import java.lang.reflect.InvocationTargetException;

import javax.swing.SwingUtilities;

public final class SwingUtils {

	public static void runOnEDT(Runnable runnable) {
		if (SwingUtilities.isEventDispatchThread()) {
			runnable.run();
		} else {
			SwingUtilities.invokeLater(runnable);
		}
	}

	public static void runOnEDTAndWait(Runnable runnable) throws InterruptedException, InvocationTargetException {
		if (SwingUtilities.isEventDispatchThread()) {
			runnable.run();
		} else {
			SwingUtilities.invokeAndWait(runnable);
		}
	}

	private SwingUtils() {
		// Suppress default constructor for noninstantiability.
	}

}
