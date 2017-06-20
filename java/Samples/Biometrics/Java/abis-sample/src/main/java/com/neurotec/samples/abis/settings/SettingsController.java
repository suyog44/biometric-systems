package com.neurotec.samples.abis.settings;

import com.neurotec.samples.abis.event.PageNavigationListener;

public interface SettingsController extends PageNavigationListener {

	void loadSettings();
	void saveSettings();
	void defaultSettings();

}
