package com.neurotec.tutorials.biometrics;

import android.app.Activity;
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

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NMatchingSpeed;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.util.concurrent.CompletionHandler;

public final class VerifyPalm extends Activity implements LicensingManager.LicensingStateCallback {

	private static final String TAG = VerifyPalm.class.getSimpleName();
	private static final int REQUEST_CODE_GET_REFERENCE = 1;
	private static final int REQUEST_CODE_GET_CANDIDATE = 2;

	private static final String[] LICENSES = {
		LicensingManager.LICENSE_PALM_MATCHING,
		LicensingManager.LICENSE_PALM_MATCHING_FAST,
		LicensingManager.LICENSE_PALM_EXTRACTION
	};

	private Button mButtonReference;
	private Button mButtonCandidate;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NSubject mReference;
	private NSubject mCandidate;
	private NBiometricClient mBiometricClient;

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

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_verify_palm);
		mButtonReference = (Button) findViewById(R.id.tutorials_button_1);
		mButtonReference.setText(R.string.msg_select_reference_image);
		mButtonReference.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_REFERENCE);
			}
		});
		mButtonCandidate = (Button) findViewById(R.id.tutorials_button_2);
		mButtonCandidate.setText(R.string.msg_select_candidate_image);
		mButtonCandidate.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_CANDIDATE);
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
		if (mBiometricClient != null) {
			mBiometricClient.cancel();
			mBiometricClient.dispose();
			mBiometricClient = null;
		}
		if (mCandidate != null) mCandidate.dispose();
		if (mReference != null) mReference.dispose();
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (resultCode == RESULT_OK) {
			try {
				if (requestCode == REQUEST_CODE_GET_REFERENCE) {
					mReference = createSubject(data.getData());
					if (mCandidate != null) {
						verify();
					}
				} else if (requestCode == REQUEST_CODE_GET_CANDIDATE) {
					mCandidate = createSubject(data.getData());
					if (mReference != null) {
						verify();
					}
				}
			} catch (Exception e) {
				showMessage(e.getMessage());
				Log.e(TAG, "Exception", e);
			}
		}
	}

	private void init() {
		// Create NBiometricClient
		mBiometricClient = new NBiometricClient();
		// Set matching threshold
		mBiometricClient.setMatchingThreshold(48);
		// Set matching speed
		mBiometricClient.setFingersMatchingSpeed(NMatchingSpeed.LOW);
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
		NPalm palm = new NPalm();
		palm.setImage(NImageUtils.fromUri(this, uri));
		subject.getPalms().add(palm);
		return subject;
	}

	private void verify() throws IOException {
		if (!LicensingManager.isPalmMatchingActivated()) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_PALM_MATCHING));
			return; // The following operation is not activated, so return
		}
		// Verify subjects
		mBiometricClient.verify(mReference, mCandidate, null, new CompletionHandler<NBiometricStatus, Void>() {
			@Override
			public void completed(NBiometricStatus result, Void attachment) {
				if (result == NBiometricStatus.OK || result == NBiometricStatus.MATCH_NOT_FOUND) {
					int score = mReference.getMatchingResults().get(0).getScore();
					showMessage(getString(R.string.format_score, score));
					if (result == NBiometricStatus.OK) {
						showMessage(getString(R.string.msg_templates_matched, score));
					} else {
						showMessage(getString(R.string.msg_templates_not_matched, score));
					}
				}
			}

			@Override
			public void failed(Throwable exc, Void attachment) {
			}
		});
	}

}
