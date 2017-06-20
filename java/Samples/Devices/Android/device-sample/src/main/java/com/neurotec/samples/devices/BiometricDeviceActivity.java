package com.neurotec.samples.devices;

import java.util.EnumSet;

import android.util.Log;

import com.neurotec.devices.NBiometricDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.samples.devices.view.BiometricDeviceFragment;

public abstract class BiometricDeviceActivity extends CaptureActivity {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = BiometricDeviceActivity.class.getSimpleName();

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return super.isValidDeviceType(value) && value.contains(NDeviceType.BIOMETRIC_DEVICE);
	}

	@Override
	protected final void onCancelCapture() {
		//TODO Check if required
	}

	// ============================================
	// Public methods
	// ============================================

	@Override
	public void onStopCapturing() {
		super.onStopCapturing();
		Runnable runnable = new Runnable() {
			public void run() {
				try {
					if (getDevice().isAvailable()) {
						((NBiometricDevice) getDevice()).cancel();
					}
				} catch (Exception e) {
					Log.e(TAG, "", e);
					showError(e.toString());
				}
			}
		};
		new Thread(runnable).start();
	}

	public final boolean isAutomatic() {
		BiometricDeviceFragment fragment = (BiometricDeviceFragment) getFragmentManager().findFragmentById(R.id.fragment_biometric_device);
		return fragment.isAutomatic();
	}

	public final int getTimeout() {
		BiometricDeviceFragment fragment = (BiometricDeviceFragment) getFragmentManager().findFragmentById(R.id.fragment_biometric_device);
		return fragment.getTimeout();
	}
}
