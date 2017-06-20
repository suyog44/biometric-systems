package com.neurotec.samples.devices;

import java.util.EnumSet;

import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceType;

public final class DeviceManagerHelper {

	private static NDeviceManager sDeviceManager;

	public static synchronized void initialize() {
		initialize(EnumSet.of(NDeviceType.ANY));
	}

	public static synchronized void initialize(EnumSet<NDeviceType> deviceTypes) {
		if (deviceTypes == null) throw new NullPointerException("deviceTypes");
		if (sDeviceManager != null) {
			sDeviceManager.dispose();
			sDeviceManager = null;
		}
		sDeviceManager = new NDeviceManager();
		sDeviceManager.setDeviceTypes(deviceTypes);
		sDeviceManager.setAutoPlug(true);
		sDeviceManager.initialize();
	}

	public static synchronized NDeviceManager getDeviceManager() {
		return sDeviceManager;
	}

}
