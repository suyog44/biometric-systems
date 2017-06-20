package com.neurotec.samples;

import java.awt.Dimension;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.IOException;
import java.util.HashSet;
import java.util.Set;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.ProgressMonitor;
import javax.swing.SwingUtilities;

import com.neurotec.samples.swing.MainFrame;
import com.neurotec.samples.util.LibraryManager;
import com.neurotec.samples.util.LicenseManager;

public final class EnrollmentSample implements PropertyChangeListener {

	// ===========================================================
	// Private static final fields
	// ===========================================================

	private static final Set<String> LICENSES;

	// ===========================================================
	// Static constructor
	// ===========================================================

	static {
		LICENSES = new HashSet<String>(1);
		LICENSES.add("Biometrics.FingerExtraction");
		LICENSES.add("Biometrics.FingerSegmentation");
		LICENSES.add("Biometrics.Tools.NFIQ"); // Optional.
		LICENSES.add("Devices.Cameras"); // Optional.
	}

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		EnrollmentSample sample = new EnrollmentSample();
		LicenseManager.getInstance().addPropertyChangeListener(sample);
		try {
			LicenseManager.getInstance().obtain(LICENSES);
		} catch (IOException e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(null, e.toString());
			return;
		}

		SwingUtilities.invokeLater(new Runnable() {
			@Override
			public void run() {
				try {
					JFrame frame = new MainFrame();
					Dimension d = new Dimension(1015, 625);

					frame.setSize(d);
					frame.setMinimumSize(new Dimension(800, 600));
					frame.setPreferredSize(d);

					frame.setResizable(true);
					frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
					frame.setTitle("Enrollment Sample");
					frame.setLocationRelativeTo(null);
					frame.setVisible(true);
				} catch (Exception e) {
					e.printStackTrace();
					JOptionPane.showMessageDialog(null, e.toString());
				}
			}
		});
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private final ProgressMonitor progressMonitor;

	// ===========================================================
	// Private methods
	// ===========================================================

	private EnrollmentSample() {
		progressMonitor = new ProgressMonitor(null, "License obtain", "", 0, LICENSES.size());
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void propertyChange(PropertyChangeEvent evt) {
		if (LicenseManager.PROGRESS_CHANGED_PROPERTY.equals(evt.getPropertyName())) {
			int progress = (Integer) evt.getNewValue();
			progressMonitor.setProgress(progress);
			String message = String.format("# of analyzed licenses: %d\n", progress);
			progressMonitor.setNote(message);
		}
	}
}
