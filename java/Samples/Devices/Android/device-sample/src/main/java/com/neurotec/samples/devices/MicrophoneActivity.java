package com.neurotec.samples.devices;

import java.util.EnumSet;

import android.os.Bundle;
import android.view.View;
import android.widget.ProgressBar;

import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NMicrophone;
import com.neurotec.sound.NSoundBuffer;
import com.neurotec.sound.processing.NSoundProc;

public final class MicrophoneActivity extends CaptureDeviceActivity {

	// ==============================================
	// Private fields
	// ==============================================

	private double soundLevel;
	private ProgressBar mProgressBar;

	// ==============================================
	// Private methods
	// ==============================================

	private void onSoundSample(NSoundBuffer soundBuffer) {
		synchronized (statusLock) {
			soundLevel = NSoundProc.getSoundLevel(soundBuffer);
		}
		onStatusChanged();
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onStatusChanged() {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				synchronized (statusLock) {
					int level = (int) (soundLevel * 100.0);
					mProgressBar.setProgress(level);
					mProgressBar.setVisibility(isCapturing() ? View.VISIBLE : View.INVISIBLE);
				}
			}
		});
		super.onStatusChanged();
	}

	@Override
	protected boolean isValidDeviceType(EnumSet<NDeviceType> value) {
		return super.isValidDeviceType(value) && (value.contains(NDeviceType.MICROPHONE));
	}

	@Override
	protected boolean onObtainSample() {
		NMicrophone microphone = (NMicrophone) getDevice();
		NSoundBuffer soundSample = null;
		try {
			soundSample = microphone.getSoundSample();
			if (soundSample != null) {
				onSoundSample(soundSample);
				return true;
			}
			return false;
		} finally {
			if (soundSample != null) soundSample.dispose();
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.microphone_view);
		mProgressBar = (ProgressBar) findViewById(R.id.progressBar);
	}
}
