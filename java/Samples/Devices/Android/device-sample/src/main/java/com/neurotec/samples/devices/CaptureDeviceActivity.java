package com.neurotec.samples.devices;

import java.util.EnumSet;

import com.neurotec.devices.NCaptureDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.event.ChangeEvent;
import com.neurotec.event.ChangeListener;
import com.neurotec.media.NMediaFormat;
import com.neurotec.samples.devices.view.CaptureDeviceFragment;
import com.neurotec.samples.devices.view.CaptureDeviceFragment.MediaFormatListener;

public abstract class CaptureDeviceActivity extends CaptureActivity implements ChangeListener, MediaFormatListener {

	// ==============================================
	// Private fields
	// ==============================================

	private Object nextMediaFormatLock = new Object();
	private NMediaFormat nextMediaFormat;

	// ==============================================
	// Private methods
	// ==============================================

	private void addMediaFormats(final NMediaFormat[] formats, final NMediaFormat currentFormat) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				CaptureDeviceFragment fragment = (CaptureDeviceFragment) getFragmentManager().findFragmentById(R.id.fragment_capture_device);
				if (fragment != null && fragment.isInLayout()) {
					fragment.addMediaFormats(formats, currentFormat);
				}
			}
		});
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return super.isValidDeviceType(value) && (value.contains(NDeviceType.CAPTURE_DEVICE));
	}

	@Override
	protected void onCapture() {
		NCaptureDevice captureDevice = (NCaptureDevice) getDevice();
		captureDevice.addCapturingChangedListener(this);
		onStartingCapture();
		boolean stoppedCapturing = false;
		try {
			captureDevice.startCapturing();
			boolean sampleObtained = true;
			try {
				addMediaFormats(captureDevice.getFormats(), captureDevice.getCurrentFormat());
				while (sampleObtained && !isCancellationPending()) {
					synchronized (nextMediaFormatLock) {
						if (nextMediaFormat != null) {
							captureDevice.setCurrentFormat(nextMediaFormat);
							nextMediaFormat = null;
						}
					}
					sampleObtained = onObtainSample();
				}
			} finally {
				if (sampleObtained && captureDevice.isAvailable()) {
					captureDevice.stopCapturing();
					stoppedCapturing = true;
				}
			}
		} finally {
			captureDevice.removeCapturingChangedListener(this);
			onFinishingCapture();
			if (!stoppedCapturing)
				onCaptureFinished();
		}
	}

	@Override
	protected void onCancelCapture() {
		super.onCancelCapture();
		if (getDevice().isAvailable()) {
			((NCaptureDevice) getDevice()).stopCapturing();
		}
	}

	protected boolean onObtainSample() {
		throw new UnsupportedOperationException();
	}

	protected void onStartingCapture() {
	}

	protected void onFinishingCapture() {
	}

	// ======================================================
	// Public methods
	// ======================================================

	//TODO Update this code
//	@Override
//	public void onActivityCreated(Bundle savedInstanceState) {
//		super.onActivityCreated(savedInstanceState);
//		NCaptureDevice device = (NCaptureDevice) getDevice();
//		addMediaFormats(device.getFormats(), device.getCurrentFormat());
//
//	}

	@Override
	public void stateChanged(ChangeEvent e) {
		if (getDevice().isAvailable() && ((NCaptureDevice) getDevice()).isCapturing()) {
			onCaptureStarted();
		} else {
			onCaptureFinished();
		}
	}

	@Override
	public void onMediaFormatChanged(NMediaFormat mediaFormat) {
		synchronized (nextMediaFormatLock) {
			nextMediaFormat = mediaFormat;
		}
	}

}
