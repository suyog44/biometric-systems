package com.neurotec.tutorials.biometricstandards;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;

import com.neurotec.biometrics.standards.ANBinaryImageCompressionAlgorithm;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType1Record;
import com.neurotec.biometrics.standards.ANType6Record;
import com.neurotec.images.NImage;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public final class ANTemplateType6FromNImage extends Activity implements LicensingStateCallback {

	private static final String TAG = ANTemplateType6FromNImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FINGER_STANDARDS_FINGERS, LicensingManager.LICENSE_FINGER_STANDARDS_FINGER_TEMPLATES};

	private EditText mFieldTot;
	private EditText mFieldDai;
	private EditText mFieldOri;
	private EditText mFieldTcn;
	private Button mButton;
	private TextView mResult;
	private String mTot;
	private String mDai;
	private String mOri;
	private String mTcn;
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
		setContentView(R.layout.tutorial_antemplate_type_6_from_nimage);
		mFieldTot = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldTot.setHint(R.string.hint_tot);
		mFieldDai = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldDai.setHint(R.string.hint_dai);
		mFieldOri = (EditText) findViewById(R.id.tutorials_field_3);
		mFieldOri.setHint(R.string.hint_ori);
		mFieldTcn = (EditText) findViewById(R.id.tutorials_field_4);
		mFieldTcn.setHint(R.string.hint_tcn);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFingerStandardsActivated()) {
					mTot = mFieldTot.getText().toString();
					mDai = mFieldDai.getText().toString();
					mOri = mFieldOri.getText().toString();
					mTcn = mFieldTcn.getText().toString();
					if (validateInput()) {
						getImage();
					}
				} else {
					Toast.makeText(getApplicationContext(), R.string.msg_licenses_not_obtained, Toast.LENGTH_SHORT).show();
				}
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
					convert(data.getData());
				} catch (Exception e) {
					showMessage(e.toString());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private boolean validateInput() {
		if (TextUtils.isEmpty(mDai) || TextUtils.isEmpty(mOri) || TextUtils.isEmpty(mTcn)) {
			showMessage(getString(R.string.msg_one_or_more_fields_empty));
			return false;
		}
		if ((mTot.length() > 4) || (mTot.length() < 3)) {
			showMessage(getString(R.string.msg_tot_length));
			return false;
		}
		return true;
	}

	private void convert(Uri imageUri) throws IOException {
		ANTemplate template = null;
		NImage image = null;

		try {
			// Create an empty ANTemplate object with only type 1 record in it
			template = new ANTemplate(ANTemplate.VERSION_CURRENT, mTot, mDai, mOri, mTcn, 0);

			// Get NImage from Uri
			image = NImage.fromMemory(IOUtils.toByteBuffer(this, imageUri));

			image.setHorzResolution(500);
			image.setVertResolution(500);
			image.setResolutionIsAspectRatio(false);

			// Add Type 6 record to ANTemplate object
			ANType6Record record = new ANType6Record(ANTemplate.VERSION_CURRENT, 0, true, ANBinaryImageCompressionAlgorithm.NONE, image);
			template.getRecords().add(record);

			ANType1Record type1 = (ANType1Record)template.getRecords().get(0);
			type1.setNativeScanningResolution(ANType1Record.MIN_SCANNING_RESOLUTION);
			type1.setNominalTransmittingResolutionPpi(image.getHorzResolution());

			// Save template to file
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "antemplate-type-6.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), template.save());

			showMessage(getString(R.string.format_antemplate_saved_to, 6, outputFile.getAbsolutePath()));
		} finally {
			if (template != null) template.dispose();
			if (image != null) image.dispose();
		}
	}

}
