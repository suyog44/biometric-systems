package com.neurotec.samples.server;

import java.awt.Dimension;

import javax.swing.JFrame;
import javax.swing.SwingUtilities;

import com.neurotec.samples.server.util.MessageUtils;
import com.neurotec.samples.util.LibraryManager;

public final class ServerSample {

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		SwingUtilities.invokeLater(new Runnable() {
			public void run() {
				try {
					MainFrame frame = new MainFrame();
					Dimension d = new Dimension(935, 450);

					frame.setSize(d);
					frame.setMinimumSize(d);
					frame.setPreferredSize(d);

					frame.setResizable(true);
					frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
					frame.setTitle("Server Sample");
					frame.setLocationRelativeTo(null);
					frame.showMainFrame();
				} catch (Exception e) {
					e.printStackTrace();
					MessageUtils.showError(null, e);
				}
			}
		});
	}
}
