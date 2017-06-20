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
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.util.concurrent.CompletionHandler;

public final class EnrollPalmFromImage extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = EnrollPalmFromImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String LICENSE = LicensingManager.LICENSE_PALM_EXTRACTION;

	private Button mButton;
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
				// Save base image to file
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "palm-from-image.png");
				try {
					subject.getFingers().get(0).getImage().save(outputFile.getAbsolutePath());
					showMessage(getString(R.string.format_finger_image_saved_to, outputFile.getAbsolutePath()));
				} catch (IOException e) {}

				// Save template to file
				outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "palm-nfrecord-from-image.dat");
				try {
					NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());
					showMessage(getString(R.string.format_palm_record_saved_to, outputFile.getAbsolutePath()));
				} catch (IOException e) {
				}
			} else {
				showMessage(getString(R.string.format_extraction_failed, result));
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
		setContentView(R.layout.tutorial_enroll_palm_from_image);
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
		if (mBiometricClient != null) {
			mBiometricClient.cancel();
			mBiometricClient = null;
		}
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
					// Palm extraction takes a long time, so do it in a separate thread
					showMessage(getString(R.string.msg_extracting));
					enroll(data.getData());
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private void enroll(Uri imageUri) throws IOException {
		NBiometricTask task = null;

		if (!LicensingManager.isActivated(LicensingManager.LICENSE_PALM_EXTRACTION)) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_PALM_EXTRACTION));
			return; // The following operation is not activated, so return
		}

		NBiometricClient biometricClient = new NBiometricClient();
		NSubject subject = new NSubject();
		NPalm palm = new NPalm();
		palm.setImage(NImageUtils.fromUri(this, imageUri));
		subject.getPalms().add(palm);
		biometricClient.setPalmsTemplateSize(NTemplateSize.LARGE);

		task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);
		biometricClient.performTask(task, null, completionHandler);
	}
}
