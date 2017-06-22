package com.neurotec.tutorials.biometricstandards;

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

import java.io.File;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Arrays;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FCRFaceImage;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public final class FCRecordToNTemplate extends Activity implements LicensingStateCallback {

	private static final String TAG = FCRecordToNTemplate.class.getSimpleName();
	private static final int REQUEST_CODE_GET_RECORD = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FACE_EXTRACTION, LicensingManager.LICENSE_FACE_STANDARDS};

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
					Log.i(TAG, getString(R.string.format_obtaining_licenses, Arrays.asList(LICENSES)));
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
		setContentView(R.layout.tutorial_fcrecord_to_ntemplate);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_fcrecord);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				getRecord();
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
		if (requestCode == REQUEST_CODE_GET_RECORD) {
			if (resultCode == RESULT_OK) {
				try {
					convert(data.getData());
				} catch (Exception e) {
					showMessage(e.toString());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private void getRecord() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
	}

	private void convert(Uri recordUri) throws IOException {
		NBiometricClient biometricClient = null;
		NSubject subject = null;

		FCRecord fcRecord = null;

		try {
			if (!LicensingManager.isFaceStandardsActivated()) {
				showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FACE_STANDARDS));
				return; // The following operation is not activated, so return
			}
			if (!LicensingManager.isFaceExtractionActivated()) {
				showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FACE_EXTRACTION));
				return; // Extraction is not activated, so return
			}

			ByteBuffer fcRecordData = IOUtils.toByteBuffer(this, recordUri);
			fcRecord = new FCRecord(fcRecordData, BDIFStandard.ISO);

			biometricClient = new NBiometricClient();
			subject = new NSubject();

			// Read all images from FCRecord
			for (FCRFaceImage fv : fcRecord.getFaceImages()) {
				NFace face = new NFace();
				face.setImage(fv.toNImage());
				subject.getFaces().add(face);
			}

			// Create template from added face image(s)
			NBiometricStatus status = biometricClient.createTemplate(subject);

			if (status == NBiometricStatus.OK) {
				// Save converted template to file
				File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "ntemplate-from-fcrecord.dat");
				NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());

				showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage(getString(R.string.format_template_creation_failed_with_status, status));
			}
		} finally {
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (fcRecord != null) fcRecord.dispose();
		}
	}

}
