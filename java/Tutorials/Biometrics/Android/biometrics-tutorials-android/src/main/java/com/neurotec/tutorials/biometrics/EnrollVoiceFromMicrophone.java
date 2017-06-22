package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceManager.DeviceCollection;
import com.neurotec.devices.NDeviceType;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.util.concurrent.CompletionHandler;

public final class EnrollVoiceFromMicrophone extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = EnrollVoiceFromMicrophone.class.getSimpleName();

	private static final String[] LICENSES = {LicensingManager.LICENSE_VOICE_EXTRACTION, LicensingManager.LICENSE_DEVICES_MICROPHONES};

	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NBiometricClient mBiometricClient;

	private CompletionHandler<NBiometricTask, NSubject> completionHandler = new CompletionHandler<NBiometricTask, NSubject>() {
		@Override
		public void completed(NBiometricTask result, NSubject subject) {
			if (result.getError() != null) {
				showError(result.getError());
				return;
			}

			if (result.getStatus() == NBiometricStatus.OK) {
				showMessage(getString(R.string.msg_template_created));
				// Save base image to file.
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "voice-from-microphone.wav");
				try {
					subject.getVoices().get(1).getSoundBuffer().save(outputFile.getAbsolutePath());
					showMessage(getString(R.string.format_extracted_voice_audio_file_saved_to, outputFile.getAbsolutePath()));
				} catch (IOException e) {}

				// Save template to file.
				outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "nsrecord-from-microphone.dat");
				try {
					NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());
					showMessage(getString(R.string.format_template_saved_to, outputFile.getAbsolutePath()));
				} catch (IOException e) {
				}
			} else {
				showMessage(getString(R.string.format_extraction_failed, result.getStatus().name()));
			}
		}

		@Override
		public void failed(Throwable exc, NSubject subject) {exc.printStackTrace();
		}
	};

	@Override
	public void onLicensingStateChanged(final LicensingStateResult state) {
		final Context context = this;
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				switch (state.getState()) {
				case OBTAINING:
					Log.i(TAG, getString(R.string.format_obtaining_licenses, Arrays.asList(LICENSES)));
					mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_licenses));
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
					capture();
					break;
				case NOT_OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_not_obtained));
					if (state.getException() != null) {
						showMessage(state.getException().getMessage());
					}
					break;
				default:
					throw new AssertionError("Unknown state: " + state);
				}
			}
		});
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		NCore.setContext(this);
		setContentView(R.layout.tutorial_enroll_voice_from_microphone);
		mResult = (TextView) findViewById(R.id.tutorials_results);

		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSES));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		if (mBiometricClient != null) {
			mBiometricClient.cancel();
			mBiometricClient.dispose();
			mBiometricClient = null;
		}
		try {
			LicensingManager.getInstance().release(Arrays.asList(LICENSES));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}

		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}
	private void capture() {
		if (!LicensingManager.isVoiceExtractionActivated()) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_VOICE_EXTRACTION));
			return; // The following operation is not activated, so return
		}
		mBiometricClient = new NBiometricClient();
		mBiometricClient.setUseDeviceManager(true);
		NDeviceManager deviceManager = mBiometricClient.getDeviceManager();
		// set type of the device used
		deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.MICROPHONE));
		mBiometricClient.initialize();

		DeviceCollection devices = deviceManager.getDevices();
		NVoice voice = new NVoice();
		NSubject subject = new NSubject();
		subject.getVoices().add(voice);

		if (devices.size() > 0) {
			showMessage(String.format("Found %d audio input devices", devices.size()));
		} else {
			showMessage(getString(R.string.msg_no_microphone_found));
			return;
		}
		showMessage("Capturing....");
		NBiometricTask task = mBiometricClient.createTask(EnumSet.of(NBiometricOperation.CAPTURE , NBiometricOperation.SEGMENT), subject);

		mBiometricClient.performTask(task, subject, completionHandler);
	}

}
