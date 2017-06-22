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

import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.util.concurrent.CompletionHandler;

public final class IdentifyFinger extends BaseActivity implements LicensingManager.LicensingStateCallback {

	private static final String TAG = IdentifyFinger.class.getSimpleName();
	private static final int REQUEST_CODE_GET_CANDIDATE = 1;
	private static final int REQUEST_CODE_GET_PROBE = 2;

	private static final String[] LICENSES = {
		LicensingManager.LICENSE_FINGER_MATCHING,
		LicensingManager.LICENSE_FINGER_MATCHING_FAST,
		LicensingManager.LICENSE_FINGER_EXTRACTION
	};

	private Button mButtonCandidate;
	private Button mButtonProbe;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NBiometricClient mBiometricClient;

	private CompletionHandler<NBiometricTask, NBiometricOperation> completionHandler = new CompletionHandler<NBiometricTask, NBiometricOperation>() {

		@Override
		public void completed(NBiometricTask result, NBiometricOperation attachment) {
			if (result.getError() != null) {
				showError(result.getError());
				return;
			}

			switch (attachment) {
			case ENROLL:
				if (result.getStatus() == NBiometricStatus.OK) {
					showMessage(getString(R.string.format_enrollment_successful, result.getStatus()));
				} else {
					showMessage(getString(R.string.format_enrollment_unsuccessful, result.getStatus()));
				}
				break;
			case IDENTIFY:
				if (result.getStatus() == NBiometricStatus.OK) {
					for (NMatchingResult matchingResult : result.getSubjects().get(0).getMatchingResults()) {
						showMessage(getString(R.string.format_template_identified_score, matchingResult.getId(), matchingResult.getScore()));
					}
				} else {
					showMessage(getString(R.string.format_identification_unsuccessful, result.getStatus()));
				}
				break;
			default:
				break;
			}
		}

		@Override
		public void failed(Throwable th, NBiometricOperation attachment) {
			showMessage(th.getMessage() != null ? th.getMessage() : th.toString());
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
					Log.i(TAG, getString(R.string.format_obtaining_licenses, Arrays.toString(LICENSES)));
					mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_licenses));
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
					init();
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

	private void init() {
		mBiometricClient = new NBiometricClient();
		mBiometricClient.setMatchingThreshold(48);
		mBiometricClient.setFingersMatchingSpeed(NMatchingSpeed.LOW);
		mButtonCandidate.setEnabled(true);
		mButtonProbe.setEnabled(true);
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_identify_finger);
		mButtonCandidate = (Button) findViewById(R.id.tutorials_button_1);
		mButtonCandidate.setText(R.string.msg_select_candidate_images);
		mButtonCandidate.setEnabled(false);
		mButtonCandidate.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_CANDIDATE);
			}
		});
		mButtonProbe = (Button) findViewById(R.id.tutorials_button_2);
		mButtonProbe.setText(R.string.msg_select_probe_image);
		mButtonProbe.setEnabled(false);
		mButtonProbe.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_PROBE);
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
		if (mBiometricClient != null) {
			mBiometricClient.cancel();
			mBiometricClient.dispose();
			mBiometricClient = null;
		}

		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_CANDIDATE) {
			if (resultCode == RESULT_OK) {
				enroll(data.getData());
			}
		} else if (requestCode == REQUEST_CODE_GET_PROBE) {
			if (resultCode == RESULT_OK) {
				identify(data.getData());
			}
		}
	}

	private void getImage(int requestCode) {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, requestCode);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private NSubject createSubject(Uri uri) throws IOException {
		NSubject subject = new NSubject();
		NFinger finger = new NFinger();
		finger.setImage(NImageUtils.fromUri(this, uri));
		subject.getFingers().add(finger);
		subject.setId(uri.getPath());
		return subject;
	}

	private void enroll(Uri candidate) {
		try {
			NSubject candidateSubject = createSubject(candidate);
			NBiometricTask enrollTask = mBiometricClient.createTask(EnumSet.of(NBiometricOperation.ENROLL), candidateSubject);
			mBiometricClient.performTask(enrollTask, NBiometricOperation.ENROLL, completionHandler);
		} catch (Exception e) {
			showMessage(e.getMessage() != null ? e.getMessage() : e.toString());
		}
	}

	private void identify(Uri probe) {
		try {
			NSubject probeSubject = createSubject(probe);
			NBiometricTask identifyTask = mBiometricClient.createTask(EnumSet.of(NBiometricOperation.IDENTIFY), probeSubject);
			mBiometricClient.performTask(identifyTask, NBiometricOperation.IDENTIFY, completionHandler);
		} catch (Exception e) {
			showMessage(e.getMessage() != null ? e.getMessage() : e.toString());
		}
	}

}
