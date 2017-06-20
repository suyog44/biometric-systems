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
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class EvaluateFingerQuality extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = EvaluateFingerQuality.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String LICENSE = LicensingManager.LICENSE_FINGER_NFIQ;

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
					Log.i(TAG, getString(R.string.format_obtaining_licenses, LICENSE));
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
		setContentView(R.layout.tutorial_evaluate_finger_quality);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage();
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);

		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSE));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		try {
			LicensingManager.getInstance().release(Arrays.asList(LICENSE));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}

		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_IMAGE) {
			if (resultCode == RESULT_OK) {
				try {
					evaluate(data.getData());
				} catch (Exception e) {
					mResult.setText(e.toString());
					Log.e(TAG, "Exception", e);
				}
			}
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

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private void evaluate(Uri imageUri) throws IOException {
		if (!LicensingManager.isActivated(LicensingManager.LICENSE_FINGER_NFIQ)) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FINGER_NFIQ));
			return; // The following operation is not activated, so return
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFinger finger = null;
		NBiometricTask task = null;

		try {
			biometricClient = new NBiometricClient();
			subject = new NSubject();
			finger = new NFinger();

			finger.setImage(NImageUtils.fromUri(this, imageUri));

			subject.getFingers().add(finger);

			biometricClient.setFingersCalculateNFIQ(true);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.ASSESS_QUALITY), subject);
			biometricClient.performTask(task);
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (task.getStatus() == NBiometricStatus.OK) {
				showMessage(getString(R.string.format_fingerprint_quality, subject.getFingers().get(0).getObjects().get(0).getNFIQQuality()));
			} else {
				showMessage(String.format("Quality assesment failed: %s\n", task.getStatus()));
			}
		} finally {
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (finger != null) finger.dispose();
			if (task != null) task.dispose();
		}
	}

}
