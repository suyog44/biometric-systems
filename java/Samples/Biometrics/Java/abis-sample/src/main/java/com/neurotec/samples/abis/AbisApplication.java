package com.neurotec.samples.abis;

import com.neurotec.samples.abis.tabbedview.AbisApplicationPanel;
import com.neurotec.samples.abis.tabbedview.AbisTabbedPanel;
import com.neurotec.samples.abis.tabbedview.LicenseLogTab;
import com.neurotec.samples.abis.tabbedview.TabbedAbisController;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.util.LibraryManager;

import java.io.IOException;

public final class AbisApplication {

	// ===========================================================
	// Static constructor
	// ===========================================================

	static {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();

		TabbedAbisController controller = null;
		try {
			AbisTabbedPanel view = new AbisApplicationPanel();
			AbisModel model = new AbisModel(null, null, null);
			controller = new TabbedAbisController(view, model);
			view.setController(controller);
			view.setModel(model);

			view.setBusy(true);
			view.launch();

			LicenseLogTab licenseLogTab = (LicenseLogTab) controller.showTab(LicenseLogTab.class, true, false, null);
			model.addPropertyChangeListener(licenseLogTab);
			try {
				model.obtainLicenses();
			} catch (IOException e) {
				e.printStackTrace();
				MessageUtils.showError(licenseLogTab, "Error", "Could not obtain licenses.\nProbable cause - PG not running.");
				controller.dispose();
				return;
			}
			model.removePropertyChangeListener(licenseLogTab);
			controller.closeTab(licenseLogTab);
			view.setBusy(false);

			controller.changeDatabase();
		} catch (Throwable e) {
			MessageUtils.showError(null, e);
			if (controller != null) {
				controller.dispose();
			}
		}
	}

	// ===========================================================
	// Public static methods
	// ===========================================================

	public AbisApplication() {
		// Suppress default constructor for noninstantiation.
	}

}
