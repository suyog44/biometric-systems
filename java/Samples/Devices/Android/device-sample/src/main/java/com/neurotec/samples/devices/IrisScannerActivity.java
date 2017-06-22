package com.neurotec.samples.devices;

import java.util.EnumSet;

import android.os.Bundle;
import android.util.Log;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NEAttributes;
import com.neurotec.biometrics.NEPosition;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.devices.event.NBiometricDeviceCapturePreviewEvent;
import com.neurotec.devices.event.NBiometricDeviceCapturePreviewListener;
import com.neurotec.samples.devices.view.IrisScannerFragment;

public final class IrisScannerActivity extends BiometricDeviceActivity implements NBiometricDeviceCapturePreviewListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = IrisScannerActivity.class.getSimpleName();

	// ==============================================
	// Private fields
	// ==============================================

	private IrisScannerFragment mIrisScannerFragment;

	// ==============================================
	// Private methods
	// ==============================================

	private boolean onImage(NIris biometric, boolean isFinal) {
		StringBuilder sb = new StringBuilder();
		sb.append(biometric.getStatus());
		for (NEAttributes obj : biometric.getObjects()) {
			sb.append("\n");
			sb.append(String.format("\t%s: %s (Position: %s)", obj.getPosition(), obj.getStatus(), obj.getBoundingRect()));
		}
		return onImage(biometric.getImage(), sb.toString(), (biometric.getStatus() != NBiometricStatus.NONE ? biometric.getStatus() : NBiometricStatus.OK).toString(), isFinal);
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return super.isValidDeviceType(value) && value.contains(NDeviceType.IRIS_SCANNER);
	}

	@Override
	protected void onCapture() {
		NIrisScanner irisScanner = (NIrisScanner) getDevice();
		irisScanner.addCapturePreviewListener(this);
		NSubject subject = new NSubject();
		try {
			if (getMissingPositions() != null) {
				for (NEPosition missingPosition : getMissingPositions()) {
					subject.getMissingEyes().add(missingPosition);
				}
			}
			NIris biometric = new NIris();
			try {
				biometric.setPosition(getPosition());
				if (!isAutomatic()) biometric.setCaptureOptions(EnumSet.of(NBiometricCaptureOption.MANUAL));
				irisScanner.capture(biometric, getTimeout());
				onImage(biometric, true);
			} finally {
				biometric.dispose();
				biometric = null;
			}
		} catch (Exception e) {
			Log.e(TAG, "", e);
			showError(e.getMessage());
		} finally {
			irisScanner.removeCapturePreviewListener(this);
			subject.dispose();
			subject = null;
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.irisscanner_view);
		NIrisScanner irisScanner = (NIrisScanner) getDevice();
		mIrisScannerFragment = (IrisScannerFragment) getFragmentManager().findFragmentById(R.id.fragment_irisscanner);
		mIrisScannerFragment.setPositions(irisScanner.getSupportedPositions());
	}


	public NEPosition getPosition() {
		return mIrisScannerFragment.getPosition();
	}

	public NEPosition[] getMissingPositions() {
		return mIrisScannerFragment.getMissingPositions();
	}

	@Override
	public void capturePreview(NBiometricDeviceCapturePreviewEvent event) {
		boolean force = onImage((NIris)event.getBiometric(), false);
		if (!isAutomatic()) {
			event.getBiometric().setStatus(force ? NBiometricStatus.OK : NBiometricStatus.BAD_OBJECT);
		}
	}
}
