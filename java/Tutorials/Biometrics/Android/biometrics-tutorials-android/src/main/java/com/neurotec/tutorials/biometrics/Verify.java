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
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NEMatchingDetails;
import com.neurotec.biometrics.NFMatchingDetails;
import com.neurotec.biometrics.NLMatchingDetails;
import com.neurotec.biometrics.NMatchingDetails;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSMatchingDetails;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;
import com.neurotec.util.concurrent.CompletionHandler;

public final class Verify extends Activity implements LicensingStateCallback {

	private static final String TAG = Verify.class.getSimpleName();
	private static final int REQUEST_CODE_GET_REFERENCE_TEMPLATE = 1;
	private static final int REQUEST_CODE_GET_CANDIDATE_TEMPLATE = 2;

	private static final String[] LICENSES = {
		LicensingManager.LICENSE_FINGER_MATCHING,
		LicensingManager.LICENSE_FINGER_MATCHING_FAST,
		LicensingManager.LICENSE_FACE_MATCHING,
		LicensingManager.LICENSE_FACE_MATCHING_FAST,
		LicensingManager.LICENSE_IRIS_MATCHING,
		LicensingManager.LICENSE_IRIS_MATCHING_FAST,
		LicensingManager.LICENSE_VOICE_MATCHING,
		LicensingManager.LICENSE_VOICE_MATCHING_FAST,
		LicensingManager.LICENSE_PALM_MATCHING,
		LicensingManager.LICENSE_PALM_MATCHING_FAST
	};

	private Button mButtonReference;
	private Button mButtonCandidate;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NSubject mProbeSubject;
	private NSubject mCandidateSubject;
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
					mButtonReference.setEnabled(false);
					mButtonCandidate.setEnabled(false);
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
		setContentView(R.layout.tutorial_verify);
		mButtonReference = (Button) findViewById(R.id.tutorials_button_1);
		mButtonReference.setText(R.string.msg_select_reference_template);
		mButtonReference.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_REFERENCE_TEMPLATE);
			}
		});
		mButtonCandidate = (Button) findViewById(R.id.tutorials_button_2);
		mButtonCandidate.setText(R.string.msg_select_candidate_template);
		mButtonCandidate.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage(REQUEST_CODE_GET_CANDIDATE_TEMPLATE);
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
		if (mCandidateSubject != null) mCandidateSubject.dispose();
		if (mProbeSubject != null) mProbeSubject.dispose();
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (resultCode == RESULT_OK) {
			try {
				if (requestCode == REQUEST_CODE_GET_REFERENCE_TEMPLATE) {
					mProbeSubject = createSubject(data.getData());
					if (mCandidateSubject != null) {
						verify();
					}
				} else if (requestCode == REQUEST_CODE_GET_CANDIDATE_TEMPLATE) {
					mCandidateSubject = createSubject(data.getData());
					if (mProbeSubject != null) {
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
		mBiometricClient = new NBiometricClient();
		mBiometricClient.setMatchingThreshold(48);
		mBiometricClient.setMatchingWithDetails(true);
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
		subject.setTemplateBuffer(NBuffer.fromByteBuffer(IOUtils.toByteBuffer(this, uri)));
		subject.setId(uri.getPath());
		return subject;
	}

	private void verify() throws IOException {
		mBiometricClient.verify(mProbeSubject, mCandidateSubject, null, new CompletionHandler<NBiometricStatus, Void>() {
			@Override
			public void completed(NBiometricStatus result, Void attachment) {
				if (result == NBiometricStatus.OK) {
					for (NMatchingResult matchResult : mProbeSubject.getMatchingResults()) {
						showMessage(getString(R.string.format_template_identified_score, matchResult.getId(), matchResult.getScore()));
						if (matchResult.getMatchingDetails() != null) {
							showMessage(matchingDetailsToString(matchResult.getMatchingDetails()));
						}
					}
				}
			}

			@Override
			public void failed(Throwable exc, Void attachment) {
			}
		});
	}

	private String matchingDetailsToString(NMatchingDetails details) {
		StringBuilder sb = new StringBuilder(200);
		if (details.getBiometricType().contains(NBiometricType.FINGER)) {
			sb.append(getString(R.string.msg_fingerprint_match_details));
			sb.append(getString(R.string.format_score, details.getFingersScore()));
			for (NFMatchingDetails fngrDetails : details.getFingers()) {
				sb.append(getString(R.string.format_fingerprint_index_score, fngrDetails.getMatchedIndex(), fngrDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.FACE)) {
			sb.append(getString(R.string.msg_face_match_details));
			sb.append(getString(R.string.format_score, details.getFacesScore()));
			for (NLMatchingDetails faceDetails : details.getFaces()) {
				sb.append(getString(R.string.format_face_index_score, faceDetails.getMatchedIndex(), faceDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.IRIS)) {
			sb.append(getString(R.string.msg_iris_match_details));
			sb.append(getString(R.string.format_score, details.getIrisesScore()));
			for (NEMatchingDetails irisesDetails : details.getIrises()) {
				sb.append(getString(R.string.format_iris_index_score, irisesDetails.getMatchedIndex(), irisesDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.PALM)) {
			sb.append(getString(R.string.msg_palm_match_details));
			sb.append(getString(R.string.format_score, details.getPalmsScore()));
			for (NFMatchingDetails fngrDetails : details.getPalms()) {
				sb.append(getString(R.string.format_palm_index_score, fngrDetails.getMatchedIndex(), fngrDetails.getScore()));
			}
		}

		if (details.getBiometricType().contains(NBiometricType.VOICE)) {
			sb.append(getString(R.string.msg_voice_match_details));
			sb.append(getString(R.string.format_score, details.getVoicesScore()));
			for (NSMatchingDetails voicesDetails : details.getVoices()) {
				sb.append(getString(R.string.format_voice_index_score, voicesDetails.getMatchedIndex(), voicesDetails.getScore()));
			}
		}
		return sb.toString();
	}
}
