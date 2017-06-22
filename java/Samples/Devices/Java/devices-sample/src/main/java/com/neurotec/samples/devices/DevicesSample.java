package com.neurotec.samples.devices;

import com.neurotec.samples.util.LibraryManager;
import com.neurotec.samples.util.LicenseManager;

import java.awt.Dimension;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.HashSet;
import java.util.Set;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.ProgressMonitor;
import javax.swing.SwingUtilities;

public final class DevicesSample implements PropertyChangeListener {

	// ===========================================================
	// Private static final fields
	// ===========================================================

	private static final Set<String> LICENSES;

	// ===========================================================
	// Static constructor
	// ===========================================================

	static {
		LICENSES = new HashSet<String>(6);
		LICENSES.add("Biometrics.FingerDetection");
		LICENSES.add("Biometrics.PalmDetection");
		LICENSES.add("Devices.FingerScanners");
		LICENSES.add("Devices.PalmScanners");
		LICENSES.add("Devices.Cameras");
		LICENSES.add("Biometrics.IrisDetection");
		LICENSES.add("Devices.IrisScanners");
		LICENSES.add("Devices.Microphones");
		LICENSES.add("Media");
	}

	// =============================================
	// Public static methods
	// =============================================

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		DevicesSample application = new DevicesSample();
		LicenseManager.getInstance().addPropertyChangeListener(application);
		try {
			LicenseManager.getInstance().obtain(LICENSES);
		} catch (Exception e) {
			JOptionPane.showMessageDialog(null, e.toString());
			return;
		}

		SwingUtilities.invokeLater(new Runnable() {
			public void run() {
				try {
					JFrame frame = new MainFrame();
					Dimension d = new Dimension(880, 770);
					frame.setSize(d);
					frame.setMinimumSize(d);
					frame.setPreferredSize(d);
					frame.setResizable(true);
					frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
					frame.setTitle("Multibiometric Sample");
					frame.setLocationRelativeTo(null);
					frame.setVisible(true);
				} catch (Throwable e) {
					JOptionPane.showMessageDialog(null, e.toString());
				}
			}
		});
	}

	// =============================================
	// Private fields
	// =============================================

	private final ProgressMonitor progressMonitor;

	// =============================================
	// Private constructor
	// =============================================

	private DevicesSample() {
		progressMonitor = new ProgressMonitor(null, "License obtain", "", 0, LICENSES.size());
	}

	// =============================================
	// Public methods
	// =============================================

	public void propertyChange(PropertyChangeEvent evt) {
		if (LicenseManager.PROGRESS_CHANGED_PROPERTY.equals(evt.getPropertyName())) {
			int progress = (Integer) evt.getNewValue();
			progressMonitor.setProgress(progress);
			String message = String.format("# of analyzed licenses: %d\n", progress);
			progressMonitor.setNote(message);
		}
	}

}
