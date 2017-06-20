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
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.io.File;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Arrays;

import com.neurotec.biometrics.NFTemplate;
import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FMRFingerView;
import com.neurotec.biometrics.standards.FMRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;
import com.neurotec.util.NVersion;

public final class NTemplateToFMRecord extends Activity implements LicensingStateCallback {

	private static final String TAG = NTemplateToFMRecord.class.getSimpleName();
	private static final int REQUEST_CODE_GET_TEMPLATE = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FINGER_STANDARDS_FINGERS, LicensingManager.LICENSE_FINGER_STANDARDS_FINGER_TEMPLATES};

	private EditText mFieldStandard;
	private EditText mFieldFlag;
	private EditText mFieldVersion;
	private Button mButton;
	private TextView mResult;
	private BDIFStandard mStandard;
	private int mFlag;
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
		setContentView(R.layout.tutorial_ntemplate_to_fmrecord);
		mFieldStandard = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldStandard.setHint(R.string.hint_standard_iso_ansi);
		mFieldFlag = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldFlag.setHint(R.string.hint_flag_use_neurotec_fields);
		mFieldVersion = (EditText) findViewById(R.id.tutorials_field_3);
		mFieldVersion.setHint(R.string.hint_fm_record_version);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_ntemplate);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFingerStandardsActivated()) {
					if (validateInput()) {
						getTemplate();
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
		if (requestCode == REQUEST_CODE_GET_TEMPLATE) {
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

	private void getTemplate() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_TEMPLATE);
	}

	private boolean validateInput() {
		String standard = mFieldStandard.getText().toString();
		if (standard.equalsIgnoreCase(getString(R.string.standard_iso))) {
			mStandard = BDIFStandard.ISO;
		} else if (standard.equalsIgnoreCase(getString(R.string.standard_ansi))) {
			mStandard = BDIFStandard.ANSI;
		} else {
			showMessage(getString(R.string.format_unknown_standard, standard));
			return false;
		}

		try {
			mFlag = Integer.parseInt(mFieldFlag.getText().toString());
		} catch (NumberFormatException e) {
			showMessage(getString(R.string.format_flag_not_valid, mFieldFlag.getText().toString()));
			return false;
		}

		return true;
	}

	private void convert(Uri ntemplateUri) throws IOException {
		NTemplate nTemplate = null;
		NFTemplate nfTemplate = null;
		FMRecord fmRecord = null;
		NVersion version;

		try {
			float versionNumber = Float.parseFloat(mFieldVersion.getText().toString());
			if (versionNumber == 2) {
				version = (mStandard == BDIFStandard.ANSI) ? FMRecord.VERSION_ANSI_20 : FMRecord.VERSION_ISO_20;
			} else if (versionNumber == 3) {
				if (mStandard != BDIFStandard.ISO) {
					throw new IllegalArgumentException("Standard and version is incompatible");
				}
				version = FMRecord.VERSION_ISO_30;
			} else if (versionNumber == 3.5){
				if (mStandard != BDIFStandard.ANSI) {
					throw new IllegalArgumentException("Standard and version is incompatible");
				}
				version = FMRecord.VERSION_ANSI_35;
			} else {
				showMessage(getString(R.string.format_version_not_valid, mFieldVersion.getText().toString()));
				return;
			}

			ByteBuffer packedNTemplate = IOUtils.toByteBuffer(this, ntemplateUri);

			// Create NTemplate object from packed NTemplate
			nTemplate = new NTemplate(packedNTemplate);

			// Retrieve NFTemplate object from NTemplate object
			nfTemplate = nTemplate.getFingers();

			if (nfTemplate != null) {

				// Create FMRecord object from NFTemplate object
				fmRecord = new FMRecord(nfTemplate, mStandard, version);

				// Store FMRecord object in memory
				NBuffer storedFmRecord;
				if (mFlag == 1) {
					storedFmRecord = fmRecord.save(FMRFingerView.FLAG_USE_NEUROTEC_FIELDS);
				} else {
					storedFmRecord = fmRecord.save();
				}

				// Save converted template to file
				File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "fmrecord-from-ntemplate.dat");
				NFile.writeAllBytes(outputFile.getAbsolutePath(), storedFmRecord);

				showMessage(getString(R.string.format_fmrecord_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage(getString(R.string.msg_no_nfrecords_in_ntemplate));
			}
		} finally {
			if (nTemplate != null) nTemplate.dispose();
			if (nfTemplate != null) nfTemplate.dispose();
			if (fmRecord != null) fmRecord.dispose();
		}
	}

}
