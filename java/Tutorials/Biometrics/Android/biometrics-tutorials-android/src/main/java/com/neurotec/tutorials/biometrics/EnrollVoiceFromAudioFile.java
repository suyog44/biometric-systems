package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NSoundBufferUtils;
import com.neurotec.sound.NSoundBuffer;

public final class EnrollVoiceFromAudioFile extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = EnrollVoiceFromAudioFile.class.getSimpleName();
	private static final int REQUEST_CODE_GET_FILE = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_MEDIA, LicensingManager.LICENSE_VOICE_EXTRACTION};

	private Button mButton;
	private TextView mResult;
	private ProgressDialog mProgressDialog;

	@Override
	public void onLicensingStateChanged(final LicensingStateResult state) {
		final Context context = this;
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				switch (state.getState()) {
				case OBTAINING:
					Log.i(TAG, getString(R.string.format_obtaining_licenses, Arrays.toString(LICENSES)));
					mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_licenses));
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
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
		setContentView(R.layout.tutorial_enroll_voice_from_audio_file);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_audio_file);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getFile();
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);

		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSES));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		try {
			LicensingManager.getInstance().release(Arrays.asList(LICENSES));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}

		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_FILE) {
			if (resultCode == RESULT_OK) {
				try {
					enroll(data.getData());
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

	private void getFile() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_FILE);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private void enroll(Uri voiceUri) throws IOException {
		InputStream is = null;
		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NVoice voice = null;
		NBiometricTask task = null;
		NBiometricStatus status = null;

		try {
			biometricClient = new NBiometricClient();
			subject = new NSubject();
			voice = new NVoice();
			NSoundBuffer soundBuffer = NSoundBufferUtils.fromUri(this, voiceUri);

			voice.setSoundBuffer(soundBuffer);
			subject.getVoices().add(voice);
			showMessage(getString(R.string.msg_reading_from_audio_file));

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);
			biometricClient.performTask(task);
			status = task.getStatus();
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (subject.getVoices().size() > 1)
				System.out.format("Found %d voice(s)\n", subject.getVoices().size() - 1);

			if (status == NBiometricStatus.OK) {
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "nsrecord-from-microphone.dat");
				NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());
				showMessage(getString(R.string.format_voice_record_saved_to, outputFile.getAbsolutePath()));

				// Save voice to file.
				outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "voice-from-file.wav");
				subject.getVoices().get(0).getSoundBuffer().save(outputFile.getAbsolutePath());
				showMessage(getString(R.string.format_extracted_voice_audio_file_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage(getString(R.string.format_extraction_failed, status));
			}
		} finally {
			if (is != null) {
				try {
					is.close();
				} catch (IOException e) { }
			}
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (voice != null) voice.dispose();
		}
	}
}
