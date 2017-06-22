package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class GeneralizeFace extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = GeneralizeFace.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;
	private static final String[] LICENSES = {LicensingManager.LICENSE_FACE_EXTRACTION};

	private final List<Uri> mImages = new ArrayList<Uri>();
	private Button mButton;
	private EditText mFieldNumber;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private int mImageCount;

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
		setContentView(R.layout.tutorial_generalize_face);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validateInput()) {
					for (int i = 0; i < mImageCount; i++) {
						getImage();
					}
				}
			}
		});
		mFieldNumber = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldNumber.setHint(R.string.hint_open_images_number);
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
					mImages.add(data.getData());
					if (mImages.size() == mImageCount) {
						List<Uri> temp = new ArrayList<Uri>(mImages);
						mImages.clear();
						generalize(temp);
					}
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			} else {
				mImages.clear();
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

	private boolean validateInput() {
		try {
			mImageCount = Integer.parseInt(mFieldNumber.getText().toString());
		} catch (NumberFormatException e) {
			showMessage(getString(R.string.format_number_not_valid, mFieldNumber.getText().toString()));
			return false;
		}
		return true;
	}

	private void generalize(List<Uri> images) throws IOException {
		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NBiometricTask task = null;
		NBiometricStatus status = null;

		try {
			biometricClient = new NBiometricClient();
			subject = new NSubject();

			for (Uri image : images) {
				showMessage(getString(R.string.format_reading, image.getPath()));

				NFace face = new NFace();
				face.setImage(NImageUtils.fromUri(this, image));
				face.setSessionId(1);
				subject.getFaces().add(face);
			}

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);
			biometricClient.performTask(task);
			status = task.getStatus();
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (status != NBiometricStatus.OK) {
				showMessage(getString(R.string.format_failed_to_create_or_generalize_template, status));
				return;
			}
			showMessage(getString(R.string.msg_generalization_succeeded));
			File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "generalized-face-template.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());
			showMessage(getString(R.string.format_generalized_face_record_saved_to, outputFile.getAbsolutePath()));
		} finally {
			if (subject != null) subject.dispose();
			if (biometricClient != null) biometricClient.dispose();
		}
	}
}
