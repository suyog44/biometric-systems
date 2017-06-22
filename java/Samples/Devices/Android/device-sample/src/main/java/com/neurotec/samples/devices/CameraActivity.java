package com.neurotec.samples.devices;

import java.io.IOException;
import java.util.EnumSet;

import android.os.Bundle;

import com.neurotec.devices.NCamera;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.event.NCameraStillCaptureListener;
import com.neurotec.devices.event.NCameraStillCapturedEvent;
import com.neurotec.images.NImage;

public final class CameraActivity extends CaptureDeviceActivity implements NCameraStillCaptureListener {

	// ==============================================
	// Private fields
	// ==============================================

//	private RectF focusRegion;

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return super.isValidDeviceType(value) && (value.contains(NDeviceType.CAMERA));
	}

	@Override
	protected void onDeviceChanged() {
		super.onDeviceChanged();
		NCamera camera = (NCamera) getDevice();
		setForcedCaptureEnabled(camera != null && camera.isStillCaptureSupported());
		// TODO finish up
		// resetFocusButton.Visible = focusButton.Visible = camera != null &&
		// camera.IsFocusSupported;
		// clickToFocusCheckBox.Visible = camera != null &&
		// camera.IsFocusRegionSupported;
	}

	@Override
	protected void onStartingCapture() {
		super.onStartingCapture();
		NCamera camera = (NCamera) getDevice();
		camera.addStillCaptureListener(this);

	}

	@Override
	protected void onFinishingCapture() {
		super.onFinishingCapture();
		NCamera camera = (NCamera) getDevice();
		camera.removeStillCaptureListener(this);
	}

	@Override
	protected boolean onObtainSample() {
		NCamera camera = (NCamera) getDevice();
		NImage image = null;
		try {
			image = camera.getFrame();
			if (image != null) {
//				RectF focusRegion = camera.getFocusRegion();
				synchronized (statusLock) {
//					this.focusRegion = focusRegion;
				}
				onImage(image, null, null, false);
				return true;
			}
			return false;
		} finally {
			if (image != null)
				image.dispose();
		}
	}

	@Override
	protected void onCaptureFinished() {
		synchronized (statusLock) {
//			focusRegion = null;
		}
		super.onCaptureFinished();
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.camera_view);
		onDeviceChanged();
	}

	@Override
	public void stillCaptured(NCameraStillCapturedEvent event) {
		if (hasFinal()) {
			NImage image = null;
			try {
				try {
					image = NImage.fromStream(event.getStream());
					onImage(image, null, null, true);
				} catch (IOException e) {
					e.printStackTrace();
				}
			} finally {
				if (image != null)
					image.dispose();
			}
			onCancelCapture();
		}
	}
}
