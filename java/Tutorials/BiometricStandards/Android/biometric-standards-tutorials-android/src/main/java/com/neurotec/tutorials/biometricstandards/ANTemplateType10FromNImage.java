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

import com.neurotec.biometrics.standards.ANImageCompressionAlgorithm;
import com.neurotec.biometrics.standards.ANImageType;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType10Record;
import com.neurotec.biometrics.standards.BDIFScaleUnits;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public final class ANTemplateType10FromNImage extends Activity implements LicensingStateCallback {

	private static final String TAG = ANTemplateType10FromNImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String LICENSE = LicensingManager.LICENSE_FACE_STANDARDS;

	private EditText mFieldTot;
	private EditText mFieldDai;
	private EditText mFieldOri;
	private EditText mFieldTcn;
	private EditText mFieldSrc;
	private Button mButton;
	private TextView mResult;
	private String mTot;
	private String mDai;
	private String mOri;
	private String mTcn;
	private String mSrc;
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
		setContentView(R.layout.tutorial_antemplate_type_10_from_nimage);
		mFieldTot = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldTot.setHint(R.string.hint_tot);
		mFieldDai = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldDai.setHint(R.string.hint_dai);
		mFieldOri = (EditText) findViewById(R.id.tutorials_field_3);
		mFieldOri.setHint(R.string.hint_ori);
		mFieldTcn = (EditText) findViewById(R.id.tutorials_field_4);
		mFieldTcn.setHint(R.string.hint_tcn);
		mFieldSrc = (EditText) findViewById(R.id.tutorials_field_5);
		mFieldSrc.setHint(R.string.hint_src);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFaceStandardsActivated()) {
					mTot = mFieldTot.getText().toString();
					mDai = mFieldDai.getText().toString();
					mOri = mFieldOri.getText().toString();
					mTcn = mFieldTcn.getText().toString();
					mSrc = mFieldSrc.getText().toString();
					if (validateInput()) {
						getImage();
					}
				} else {
					Toast.makeText(getApplicationContext(), R.string.msg_licenses_not_obtained, Toast.LENGTH_SHORT).show();
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

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private boolean validateInput() {
		if (TextUtils.isEmpty(mDai) || TextUtils.isEmpty(mOri) || TextUtils.isEmpty(mTcn) || TextUtils.isEmpty(mSrc)) {
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
		NImage rgbImage = null;

		try {
			// Create an empty ANTemplate object with only type 1 record in it
			template = new ANTemplate(ANTemplate.VERSION_CURRENT, mTot, mDai, mOri, mTcn, 0);

			// Get NImage from Uri
			image = NImage.fromMemory(IOUtils.toByteBuffer(this, imageUri));

			// Convert to rgb image
			rgbImage = NImage.fromImage(NPixelFormat.RGB_8U, 0, image);

			rgbImage.setResolutionIsAspectRatio(true);

			// Add Type 10 record to ANTemplate object
			// Image type contained in the file, surely a face
			ANType10Record record = new ANType10Record(ANTemplate.VERSION_CURRENT, 0, ANImageType.FACE, mSrc, BDIFScaleUnits.NONE, ANImageCompressionAlgorithm.NONE, null, rgbImage);
			template.getRecords().add(record);

			// Save template to file.
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "antemplate-type-10.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), template.save());

			showMessage(getString(R.string.format_antemplate_saved_to, 10, outputFile.getAbsolutePath()));
		} finally {
			if (template != null) template.dispose();
			if (image != null) image.dispose();
			if (rgbImage != null) rgbImage.dispose();
		}
	}

}
