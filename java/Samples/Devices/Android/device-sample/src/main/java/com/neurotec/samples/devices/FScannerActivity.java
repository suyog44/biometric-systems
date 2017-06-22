package com.neurotec.samples.devices;

import java.util.EnumSet;

import android.os.Bundle;
import android.util.Log;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFAttributes;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NSubject;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NFScanner;
import com.neurotec.devices.event.NBiometricDeviceCapturePreviewEvent;
import com.neurotec.devices.event.NBiometricDeviceCapturePreviewListener;
import com.neurotec.samples.devices.view.FScannerFragment;

public final class FScannerActivity extends BiometricDeviceActivity implements NBiometricDeviceCapturePreviewListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = FScannerActivity.class.getSimpleName();

	// ==============================================
	// Private fields
	// ==============================================

	private FScannerFragment mFScannerFragment;

	// ==============================================
	// Private methods
	// ==============================================

	private boolean onImage(NFrictionRidge biometric, boolean isFinal) {
		StringBuilder sb = new StringBuilder();
		sb.append(biometric.getStatus());
		for (NFAttributes obj : biometric.getObjects()) {
			sb.append("\n");
			sb.append(String.format("\t{%s}: {%s}", obj.getPosition(), obj.getStatus()));
		}
		return onImage(biometric.getImage(), sb.toString(), (biometric.getStatus() != NBiometricStatus.NONE ? biometric.getStatus() : NBiometricStatus.OK).toString(), isFinal);
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return super.isValidDeviceType(value) && value.contains(NDeviceType.FSCANNER);
	}

	@Override
	protected void onCapture() {
		NFScanner fScanner = (NFScanner) getDevice();
		fScanner.addCapturePreviewListener(this);
		NSubject subject = new NSubject();
		try {
			if (getMissingPositions() != null) {
				for (NFPosition missingPosition : getMissingPositions()) {
					subject.getMissingFingers().add(missingPosition);
				}
			}
			NFrictionRidge biometric = NFrictionRidge.fromPosition(getPosition());
			try {
				biometric.setImpressionType(getImpressionType());
				biometric.setPosition(getPosition());
				if (!isAutomatic()) biometric.setCaptureOptions(EnumSet.of(NBiometricCaptureOption.MANUAL));
				fScanner.capture(biometric, getTimeout());
				onImage(biometric, true);
			} finally {
				biometric.dispose();
				biometric = null;
			}
		} catch (Exception e) {
			Log.e(TAG, "", e);
			showError(e.getMessage());
		} finally {
			fScanner.removeCapturePreviewListener(this);
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
		setContentView(R.layout.fscanner_view);
		NFScanner fScanner = (NFScanner) getDevice();
		mFScannerFragment = (FScannerFragment) getFragmentManager().findFragmentById(R.id.fragment_fscanner);
		mFScannerFragment.setPositions(fScanner.getSupportedPositions());
		mFScannerFragment.setImpressionTypes(fScanner.getSupportedImpressionTypes());
	}

	public NFImpressionType getImpressionType() {
		return mFScannerFragment.getImpressionType();
	}

	public NFPosition getPosition() {
		return mFScannerFragment.getPosition();
	}

	public NFPosition[] getMissingPositions() {
		return mFScannerFragment.getMissingPositions();
	}

	@Override
	public void capturePreview(NBiometricDeviceCapturePreviewEvent event) {
		boolean force = onImage((NFrictionRidge)event.getBiometric(), false);
		if (!isAutomatic()) {
			event.getBiometric().setStatus(force ? NBiometricStatus.OK : NBiometricStatus.BAD_OBJECT);
		}
	}
}
