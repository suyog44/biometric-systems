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

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import com.neurotec.biometrics.standards.BDIFTypes;
import com.neurotec.biometrics.standards.CBEFFBDBFormatIdentifiers;
import com.neurotec.biometrics.standards.CBEFFBiometricOrganizations;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public class NTemplateToCbeffRecord extends Activity implements LicensingStateCallback {

	private static final String TAG = "NTemplateToCbeffRecord";
	private static final int REQUEST_CODE_GET_RECORD = 1;

	private static final List<String> LICENSES = Arrays.asList("Biometrics.Standards.Base");

	private ProgressDialog mProgressDialog;
	private TextView mResult;
	private EditText mPatronFormat;
	private Button mLoadNTemplate;

	@Override
	public void onLicensingStateChanged(final LicensingStateResult state) {
		final Context context = this;
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				switch (state.getState()) {
				case OBTAINING:
					Log.i(TAG, getString(R.string.format_obtaining_licenses, LICENSES));
					mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_licenses));
					enableControlls(false);
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
					enableControlls(true);
					break;
				case NOT_OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_not_obtained));
					if (state.getException() != null) {
						showMessage(state.getException().getMessage());
					}
					enableControlls(false);
					break;
				default:
					throw new AssertionError("Unknown state: " + state);
				}
			}
		});
	}

	private void enableControlls(boolean enable) {
		mPatronFormat.setEnabled(enable);
		mLoadNTemplate.setEnabled(enable);
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private boolean validateInput() {
		try {
			if (mPatronFormat.getText().toString().isEmpty()) {
				return false;
			}
			Integer.parseInt(mPatronFormat.getText().toString(), 16);
		} catch (NumberFormatException ex) {
			showMessage(getString(R.string.format_patron_format_not_valid, mPatronFormat.getText().toString()));
			return false;
		}
		return true;
	}

	private void getRecord() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
	}

	private void convert(Uri recordUri) throws IOException {
		// Read NTemplate buffer
		try {
			NBuffer packedCBEFFRecord = NBuffer.fromByteBuffer(IOUtils.toByteBuffer(this, recordUri));

			// Combine NTemplate BDB format
			int bdbFormat = BDIFTypes.makeFormat(CBEFFBiometricOrganizations.NEUROTECHNOLOGIJA, CBEFFBDBFormatIdentifiers.NEUROTECHNOLOGIJA_NTEMPLATE);

			// Get CbeffRecord patron format
			// all supported patron formats can be found in CbeffRecord class documentation
			int patronFormat = Integer.parseInt(mPatronFormat.getText().toString(), 16);

			// Create CbeffRecord from NTemplate buffer
			CBEFFRecord cbeffRecord = new CBEFFRecord(bdbFormat, packedCBEFFRecord, patronFormat);

			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "ntemplate-from-cbeffrecord.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), cbeffRecord.save());
			showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));

		} catch (Exception e) {
			showMessage(getString(R.string.format_template_conversion_failed, e.toString()));
		}
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_ntemplate_to_cbeff_record);
		mPatronFormat = (EditText) findViewById(R.id.tutorials_field_1);
		mPatronFormat.setHint(getString(R.string.hint_patron_format));
		mLoadNTemplate = (Button) findViewById(R.id.tutorials_button_1);
		mLoadNTemplate.setText(R.string.msg_select_ntemplate);
		mLoadNTemplate.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validateInput()) {
					getRecord();
				}
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);
		LicensingManager.getInstance().obtain(this, this, LICENSES);
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

	@Override
	protected void onDestroy() {
		super.onDestroy();
		try {
			LicensingManager.getInstance().release(LICENSES);
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}

		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

}
