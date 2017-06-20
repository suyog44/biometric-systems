package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
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
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class MatchMultipleFaces extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = MatchMultipleFaces.class.getSimpleName();
	private static final int REQUEST_CODE_GET_SINGLE_FACE = 1;
	private static final int REQUEST_CODE_GET_MULTIFACE = 2;

	private static final String[] LICENSES = {
		LicensingManager.LICENSE_FACE_EXTRACTION,
		LicensingManager.LICENSE_FACE_DETECTION,
		LicensingManager.LICENSE_FACE_MATCHING,
		LicensingManager.LICENSE_FACE_MATCHING_FAST
	};

	private Button mButtonSingle;
	private Button mButtonMulti;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NSubject mReference;
	private NSubject mCandidate;
	private BackgroundTask mTask;

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
		setContentView(R.layout.tutorial_match_multiple_faces);
		mButtonSingle = (Button) findViewById(R.id.tutorials_button_1);
		mButtonSingle.setText(R.string.msg_select_single_face_image);
		mButtonSingle.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_SINGLE_FACE);
			}
		});
		mButtonMulti = (Button) findViewById(R.id.tutorials_button_2);
		mButtonMulti.setText(R.string.msg_select_multi_face_image);
		mButtonMulti.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_MULTIFACE);
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);
		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSES));

	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		if (mTask != null) {
			mTask.cancel(true);
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

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (resultCode == RESULT_OK) {
			try {
				if (requestCode == REQUEST_CODE_GET_SINGLE_FACE) {

					mReference = createSubject(data.getData(), false);
					if (mCandidate != null) {
						mTask = new BackgroundTask();
						mTask.execute(mReference, mCandidate);
					}
				} else if (requestCode == REQUEST_CODE_GET_MULTIFACE) {
					mCandidate = createSubject(data.getData(), true);
					if (mReference != null) {
						mTask = new BackgroundTask();
						mTask.execute(mReference, mCandidate);
					}
				}
			} catch (Exception e) {
				showMessage(e.getMessage());
				Log.e(TAG, "Exception", e);
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

	private NSubject createSubject(Uri uri, boolean isMultipleSubjects) throws IOException {
		NSubject subject = new NSubject();
		subject.setMultipleSubjects(isMultipleSubjects);
		NFace face = new NFace();
		face.setImage(NImageUtils.fromUri(this, uri));
		subject.getFaces().add(face);
		return subject;
	}

	private class BackgroundTask extends AsyncTask<NSubject, String, Void> {
		@Override
		protected Void doInBackground(NSubject... params) {
			if (isCancelled()) {
				return null;
			}

			NBiometricClient biometricClient = null;
			NSubject referenceSubject = null;
			NSubject candidateSubject = null;
			NBiometricTask enrollTask = null;

			try {
				biometricClient = new NBiometricClient();

				referenceSubject = params[0];
				candidateSubject = params[1];

				NBiometricStatus status = biometricClient.createTemplate(referenceSubject);
				if (status != NBiometricStatus.OK) {
					publishProgress(getString(R.string.format_template_creation_unsuccessful, status));
					return null;
				}

				status = biometricClient.createTemplate(candidateSubject);
				if (status != NBiometricStatus.OK) {
					publishProgress(getString(R.string.format_template_creation_unsuccessful, status));
					return null;
				}

				enrollTask = biometricClient.createTask(EnumSet.of(NBiometricOperation.ENROLL), null);

				int i = 0;
				candidateSubject.setId(new Integer(i++).toString());
				enrollTask.getSubjects().add(candidateSubject);
				for (NSubject relatedSubject : candidateSubject.getRelatedSubjects()) {
					relatedSubject.setId(new Integer(i++).toString());
					enrollTask.getSubjects().add(relatedSubject);
				}

				biometricClient.performTask(enrollTask);
				if (enrollTask.getError() != null) {
					showError(enrollTask.getError());
					return null;
				}
				if (enrollTask.getStatus() != NBiometricStatus.OK) {
					publishProgress(getString(R.string.format_enrollment_unsuccessful, status));
					return null;
				}

				biometricClient.setMatchingThreshold(48);

				biometricClient.setFacesMatchingSpeed(NMatchingSpeed.LOW);

				status = biometricClient.identify(referenceSubject);

				if (status == NBiometricStatus.OK) {
					for (NMatchingResult result : referenceSubject.getMatchingResults()) {
						publishProgress(getString(R.string.format_matched_with_id_and_score, result.getId(), result.getScore()));
					}
				} else {
					publishProgress(getString(R.string.format_identification_unsuccessful, status));
				}
			} catch (Throwable th) {
				Log.e(TAG, "Exception", th);
				publishProgress(th.getMessage());
			} finally {
				if (biometricClient != null) biometricClient.dispose();
				if (referenceSubject != null) referenceSubject.dispose();
				if (candidateSubject != null) candidateSubject.dispose();
				if (enrollTask != null) enrollTask.dispose();
			}
			return null;
		}

		@Override
		protected void onProgressUpdate(String... messages) {
			for (String message : messages) {
				showMessage(message);
			}
		}
	}
}
