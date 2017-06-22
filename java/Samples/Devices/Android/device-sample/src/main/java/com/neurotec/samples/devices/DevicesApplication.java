package com.neurotec.samples.devices;

import android.app.Application;

public class DevicesApplication extends Application {
	@Override
	public void onCreate() {
		super.onCreate();
		System.setProperty("jna.nounpack", "true");
		System.setProperty("java.io.tmpdir", getCacheDir().getAbsolutePath());

	}
}
