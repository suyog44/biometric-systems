package com.neurotec.samples.abis.settings;

import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;

public abstract class SettingsPage extends Page implements SettingsController {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Protected fields
	// ===========================================================

	protected final NBiometricClient client;
	protected final DefaultClientProperties defaultClientProperties;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SettingsPage(String title, PageNavigationController pageController, NBiometricClient client) {
		super(title, pageController);
		this.defaultClientProperties = DefaultClientProperties.getInstance();
		this.client = client;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void defaultSettings() {
		loadSettings();
	}
}
