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
import java.util.Arrays;

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

public final class FMRecordToNTemplate extends Activity implements LicensingStateCallback {

	private static final String TAG = FMRecordToNTemplate.class.getSimpleName();
	private static final int REQUEST_CODE_GET_RECORD = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FINGER_STANDARDS_FINGERS, LicensingManager.LICENSE_FINGER_STANDARDS_FINGER_TEMPLATES};

	private EditText mFieldStandard;
	private EditText mFieldFlag;
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
		setContentView(R.layout.tutorial_fmrecord_to_ntemplate);
		mFieldStandard = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldStandard.setHint(R.string.hint_standard_iso_ansi);
		mFieldFlag = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldFlag.setHint(R.string.hint_flag_use_neurotec_fields);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_fmrecord);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFingerStandardsActivated()) {
					if (validateInput()) {
						getRecord();
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

	private void convert(Uri recordUri) throws IOException {

		// Create FMRecord object from FMRecord stored in memory
		FMRecord fmRecord;
		if (mFlag == 1) {
			fmRecord = new FMRecord(NBuffer.fromByteBuffer(IOUtils.toByteBuffer(this, recordUri)), FMRFingerView.FLAG_USE_NEUROTEC_FIELDS, mStandard);
		} else {
			fmRecord = new FMRecord(IOUtils.toByteBuffer(this, recordUri), mStandard);
		}

		// Convert FMRecord object to NTemplate object
		NTemplate nTemplate = fmRecord.toNTemplate();

		// Save converted template to file
		File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "ntemplate-from-fmrecord.dat");
		NFile.writeAllBytes(outputFile.getAbsolutePath(), nTemplate.save());

		showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));
	}

}
