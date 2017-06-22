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
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.images.NImage;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class CreateTokenFaceImage extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = CreateTokenFaceImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FACE_EXTRACTION, LicensingManager.LICENSE_FACE_SEGMENTATION};

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
		setContentView(R.layout.tutorial_create_token_face_image);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getImage();
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
		if (requestCode == REQUEST_CODE_GET_IMAGE) {
			if (resultCode == RESULT_OK) {
				try {
					create(data.getData());
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


	private void create(Uri imageUri) throws IOException {
		if (!LicensingManager.isFaceExtractionActivated()) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FACE_EXTRACTION));
			return; // The following operation is not activated, so return
		}
		if (!LicensingManager.isActivated(LicensingManager.LICENSE_FACE_SEGMENTATION)) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FACE_SEGMENTATION));
			return; // The following operation is not activated, so return
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NFace face = null;
		NImage image = null;
		NBiometricTask task = null;

		try {
			biometricClient = new NBiometricClient();
			subject = new NSubject();
			face = new NFace();
			// Create NImage from Uri
			image = NImageUtils.fromUri(this, imageUri);
			face.setImage(image);
			subject.getFaces().add(face);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.SEGMENT, NBiometricOperation.ASSESS_QUALITY), subject);

			biometricClient.performTask(task);

			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (task.getStatus() == NBiometricStatus.OK) {
				NLAttributes originalAttributes = face.getObjects().get(0);
				NLAttributes attributes = ((NFace) originalAttributes.getChild()).getObjects().get(0);
				showMessage(getString(R.string.format_token_face_quality_attributes, attributes.getQuality()));
				showMessage(getString(R.string.format_token_face_sharpness, attributes.getSharpness()));
				showMessage(getString(R.string.format_token_face_background_uniformity, attributes.getBackgroundUniformity()));
				showMessage(getString(R.string.format_token_face_grayscale_density, attributes.getGrayscaleDensity()));

				//Save token Image to file
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "token-face-image.png");
				subject.getFaces().get(1).getImage().save(outputFile.getAbsolutePath());
				showMessage(getString(R.string.format_token_face_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage(getString(R.string.format_token_face_image_creation_failed, task.getStatus()));
			}
		} finally {
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (face != null) face.dispose();
			if (image != null) image.dispose();
			if (task != null) task.dispose();
		}
	}
}
