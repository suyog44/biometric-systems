package com.neurotec.samples.multibiometric.voices;

import android.net.Uri;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.view.Gravity;
import android.widget.FrameLayout.LayoutParams;
import android.widget.LinearLayout;

import java.io.IOException;
import java.io.InputStream;
import java.util.Arrays;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.view.NVoiceView;
import com.neurotec.io.NStream;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.multibiometric.BiometricActivity;
import com.neurotec.samples.multibiometric.R;
import com.neurotec.samples.multibiometric.faces.preference.FacePreferences;
import com.neurotec.samples.multibiometric.voices.preference.VoicePreferences;
import com.neurotec.samples.multibiometric.voices.view.MicrophoneView;
import com.neurotec.sound.NSoundBuffer;

public final class VoiceActivity extends BiometricActivity {

	// ===========================================================
	// Private static field
	// ===========================================================

	private static final String TAG = VoiceActivity.class.getSimpleName();
	private static final String MODALITY_ASSET_DIRECTORY = "voices";

	// ===========================================================
	// Private field
	// ===========================================================

	private NVoiceView mVoiceView;
	private MicrophoneView mMicrophoneView;

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);

		try {
			PreferenceManager.setDefaultValues(this, R.xml.voice_preferences, false);
			LinearLayout layout = (LinearLayout) findViewById(R.id.biometric_layout);
			mMicrophoneView = new MicrophoneView(this);
			LinearLayout.LayoutParams params = new LinearLayout.LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
			params.gravity = Gravity.CENTER;
			mMicrophoneView.setLayoutParams(params);
//			controlsView.setVisibility(View.GONE);
			layout.addView(mMicrophoneView);
			mVoiceView = new NVoiceView(this);
			layout.addView(mVoiceView);
		} catch (Exception e) {
			showError(e);
		}
	}

	@Override
	protected void onSaveInstanceState(Bundle outState) {
		outState.putString("WORKAROUND_FOR_BUG_19917_KEY", "WORKAROUND_FOR_BUG_19917_VALUE");
		super.onSaveInstanceState(outState);
	}

	@Override
	protected void onRestoreInstanceState(Bundle savedInstanceState) {
		super.onRestoreInstanceState(savedInstanceState);
	}

	@Override
	protected List<String> getComponents() {
		return Arrays.asList(LicensingManager.LICENSE_VOICE_DETECTION,
				LicensingManager.LICENSE_VOICE_EXTRACTION,
				LicensingManager.LICENSE_VOICE_MATCHING,
				LicensingManager.LICENSE_VOICE_MATCHING_FAST);
	}

	@Override
	protected List<String> getMandatoryComponents() {
		return Arrays.asList(LicensingManager.LICENSE_VOICE_DETECTION,
				LicensingManager.LICENSE_VOICE_EXTRACTION,
				LicensingManager.LICENSE_VOICE_MATCHING);
	}

	@Override
	protected Class<?> getPreferences() {
		return VoicePreferences.class;
	}

	@Override
	protected void updatePreferences(NBiometricClient client) {
		VoicePreferences.updateClient(client, this);
	}

	@Override
	protected boolean isCheckForDuplicates() {
		return VoicePreferences.isCheckForDuplicates(this);
	}

	@Override
	protected String getModalityAssetDirectory() {
		return MODALITY_ASSET_DIRECTORY;
	}

	@Override
	protected void onFileSelected(Uri uri) throws Exception {
		InputStream is = null;
		try {
			NSoundBuffer soundBuffer = NSoundBuffer.fromFile(uri.getPath());
			NSubject subject = new NSubject();
			NVoice voice = new NVoice();
			voice.setSoundBuffer(soundBuffer);
			subject.getVoices().add(voice);
			extract(subject);
		} finally {
			if (is != null) {
				try {
					is.close();
				} catch (IOException e) {
					Log.e(TAG, e.toString(), e);
				}
			}
		}
	}

	@Override
	protected void onOperationStarted(final NBiometricOperation operation) {
		super.onOperationStarted(operation);
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				if (operation == NBiometricOperation.CAPTURE) {
					mMicrophoneView.start();
				}
			}
		});
	}

	@Override
	protected void onOperationCompleted(final NBiometricOperation operation, NBiometricTask task) {
		super.onOperationCompleted(operation, task);
		if (operation == NBiometricOperation.CAPTURE || operation == NBiometricOperation.CREATE_TEMPLATE) {
			stop();
		}
	}

	@Override
	protected void onStartCapturing() {
		showToast(R.string.msg_say_your_phrase);
		NSubject subject = new NSubject();
		NVoice voice = new NVoice();
		mVoiceView.setVoice(voice);
		subject.getVoices().add(voice);
		capture(subject, null);
	}

	@Override
	protected void onStopCapturing() {
		stop();
	}

	@Override
	protected void stop() {
		super.stop();
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mMicrophoneView.stop();
			}
		});
	}


}
