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
import android.widget.Toast;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;

import com.neurotec.biometrics.NERecord;
import com.neurotec.biometrics.NETemplate;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public final class CreateTwoIrisTemplate extends Activity implements LicensingStateCallback {

	private static final String TAG = CreateTwoIrisTemplate.class.getSimpleName();
	private static final int REQUEST_CODE_GET_LEFT_EYE_RECORD = 1;
	private static final int REQUEST_CODE_GET_RIGHT_EYE_RECORD = 2;
	private static final String LICENSE = LicensingManager.LICENSE_IRIS_EXTRACTION;

	private Button mButtonLeft;
	private Button mButtonRight;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private Uri mLeftEyeRecord;
	private Uri mRightEyeRecord;

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
		setContentView(R.layout.tutorial_create_two_iris_template);
		mButtonLeft = (Button) findViewById(R.id.tutorials_button_1);
		mButtonLeft.setText(R.string.msg_select_left_iris_record);
		mButtonLeft.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isIrisExtractionActivated()) {
					getRecord(REQUEST_CODE_GET_LEFT_EYE_RECORD);
				} else {
					Toast.makeText(CreateTwoIrisTemplate.this, R.string.msg_licenses_not_obtained, Toast.LENGTH_SHORT).show();
				}
			}
		});
		mButtonRight = (Button) findViewById(R.id.tutorials_button_2);
		mButtonRight.setText(R.string.msg_select_right_iris_record);
		mButtonRight.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isIrisExtractionActivated()) {
					getRecord(REQUEST_CODE_GET_RIGHT_EYE_RECORD);
				} else {
					Toast.makeText(CreateTwoIrisTemplate.this, R.string.msg_licenses_not_obtained, Toast.LENGTH_SHORT).show();
				}
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
		if (resultCode == RESULT_OK) {
			try {
				if (requestCode == REQUEST_CODE_GET_LEFT_EYE_RECORD) {
					mLeftEyeRecord = data.getData();
					if (mRightEyeRecord != null) {
						create();
					}
				} else if (requestCode == REQUEST_CODE_GET_RIGHT_EYE_RECORD) {
					mRightEyeRecord = data.getData();
					if (mLeftEyeRecord != null) {
						create();
					}
				}
			} catch (Exception e) {
				showMessage(e.getMessage());
				Log.e(TAG, "Exception", e);
			}
		}
	}

	private void getRecord(int requestCode) {
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

	private void create() throws IOException {
		NERecord leftEyeRecord = null;
		NERecord rightEyeRecord = null;
		NETemplate template = null;

		try {
			// Create iris records from buffers
			leftEyeRecord = new NERecord(IOUtils.toByteBuffer(this, mLeftEyeRecord));
			rightEyeRecord = new NERecord(IOUtils.toByteBuffer(this, mRightEyeRecord));

			// Create NTemplate and add iris records into it
			template = new NETemplate();
			template.getRecords().add(leftEyeRecord);
			template.getRecords().add(rightEyeRecord);

			// Save NTemplate to file
			File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "two-iris-template.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), template.save());

			showMessage(getString(R.string.format_two_iris_template_saved_to, outputFile.getAbsolutePath()));
		} finally {
			if (leftEyeRecord != null) leftEyeRecord.dispose();
			if (rightEyeRecord != null) rightEyeRecord.dispose();
			if (template != null) template.dispose();
		}
	}
}
