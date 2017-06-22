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
import java.nio.ByteBuffer;
import java.util.Arrays;

import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public final class NTemplateToANTemplate extends Activity implements LicensingStateCallback {

	private static final String TAG = NTemplateToANTemplate.class.getSimpleName();
	private static final int REQUEST_CODE_GET_TEMPLATE = 1;

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
		setContentView(R.layout.tutorial_ntemplate_to_antemplate);
		mFieldTot = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldTot.setHint(R.string.hint_tot);
		mFieldDai = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldDai.setHint(R.string.hint_dai);
		mFieldOri = (EditText) findViewById(R.id.tutorials_field_3);
		mFieldOri.setHint(R.string.hint_ori);
		mFieldTcn = (EditText) findViewById(R.id.tutorials_field_4);
		mFieldTcn.setHint(R.string.hint_tcn);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_ntemplate);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFingerStandardsActivated()) {
					mTot = mFieldTot.getText().toString();
					mDai = mFieldDai.getText().toString();
					mOri = mFieldOri.getText().toString();
					mTcn = mFieldTcn.getText().toString();
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

	private void convert(Uri ntemplateUri) throws IOException {
		NTemplate nTemplate = null;
		ANTemplate anTemplate = null;

		try {
			ByteBuffer packedNTemplate = IOUtils.toByteBuffer(this, ntemplateUri);

			// Create NTemplate object from packed NTemplate
			nTemplate = new NTemplate(packedNTemplate);

			// Create ANTemplate object from NTemplate object
			anTemplate = new ANTemplate(ANTemplate.VERSION_CURRENT, mTot, mDai, mOri, mTcn, true, nTemplate);

			// Save ANTemplate to file
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "antemplate-from-ntemplate.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), anTemplate.save());

			showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));
		} finally {
			if (nTemplate != null) nTemplate.dispose();
			if (anTemplate != null) anTemplate.dispose();
		}
	}

}
