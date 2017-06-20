package com.neurotec.samples.abis;

import com.neurotec.samples.abis.tabbedview.AbisAppletPanel;
import com.neurotec.samples.abis.tabbedview.LicenseLogTab;
import com.neurotec.samples.abis.tabbedview.TabbedAbisController;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.util.LibraryManager;
import com.neurotec.samples.util.Utils;

import java.io.IOException;

import javax.swing.JApplet;

public final class AbisApplet extends JApplet {

	// ===========================================================
	// Private static final fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

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
	// Private fields
	// ===========================================================

	private TabbedAbisController controller;

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void init() {
		try {
			Utils.initDataFiles(this);
			LibraryManager.initLibraryPath();
			final String address = getParameter("server_address");
			final String port = getParameter("server_port");

			AbisAppletPanel view = new AbisAppletPanel(this);
			AbisModel model = new AbisModel(null, address, port);
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
				if (controller != null) {
					controller.dispose();
				}
				return;
			}
			model.removePropertyChangeListener(licenseLogTab);
			controller.closeTab(licenseLogTab);
			view.setBusy(false);

			controller.changeDatabase();
		} catch (Exception e) {
			MessageUtils.showError(null, e);
			if (controller != null) {
				controller.dispose();
			}
			stop();
		}
	}

	@Override
	public void destroy() {
		if (controller != null) {
			controller.dispose();
		}
		super.destroy();
	}

}
