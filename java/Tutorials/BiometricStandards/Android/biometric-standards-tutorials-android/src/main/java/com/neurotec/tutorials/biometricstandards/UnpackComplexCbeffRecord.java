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
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;

public class UnpackComplexCbeffRecord extends Activity implements LicensingStateCallback {

	private static final String TAG = "UnpackComplexCbeffRecord";
	private static final int REQUEST_CODE_GET_RECORD = 1;

	private static final List<String> LICENSES = Arrays.asList("Biometrics.Standards.Base");

	private static final Map<Integer, BdbFormat> lookup = new HashMap<Integer, BdbFormat>();

	static {
		for (BdbFormat v : BdbFormat.values()) {
			lookup.put(v.value, v);
		}
	}

	private enum BdbFormat {
		AN_TEMPLATE(0x001B8019),
		FC_RECORD_ANSI(0x001B0501),
		FC_RECORD_ISO(0x01010008),
		FI_RECORD_ANSI(0x001B0401),
		FI_RECORD_ISO(0x01010007),
		FM_RECORD_ANSI(0x001B0202),
		FM_RECORD_ISO(0x01010002),
		II_RECORD_ANSI_POLAR(0x001B0602),
		II_RECORD_ISO_POLAR(0x0101000B),
		II_RECORD_ANSI_RECTILINEAR(0x001B0601),
		II_RECORD_ISO_RECTILINEAR(0x01010009);


		private int value;

		private BdbFormat(int value) {
			this.value = value;
		}

	}

	private ProgressDialog mProgressDialog;
	private TextView mResult;
	private EditText mPatronFormat;
	private Button mLoadCBEFFRecord;
	private int mIndex = 0;

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
		mLoadCBEFFRecord.setEnabled(enable);
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
			return false;
		}
		return true;
	}

	private void getRecord() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
	}

	private void unpack(Uri recordUri) throws IOException {
		// Read CBEFFRecord buffer
		NBuffer packedCBEFFRecord = NBuffer.fromByteBuffer(IOUtils.toByteBuffer(this, recordUri));

		// Get CBEFFRecord patron format
		// all supported patron formats can be found in CBEFFRecord class documentation
		int patronFormat = Integer.parseInt(mPatronFormat.getText().toString(), 16);

		// Creating CBEFFRecord object from NBuffer object
		CBEFFRecord cbeffRecord = new CBEFFRecord(packedCBEFFRecord, patronFormat);

		unpackRecords(cbeffRecord);
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_unpack_complex_cbeff_record);
		mPatronFormat = (EditText) findViewById(R.id.tutorials_field_1);
		mPatronFormat.setHint(getString(R.string.hint_patron_format));
		mLoadCBEFFRecord = (Button) findViewById(R.id.tutorials_button_1);
		mLoadCBEFFRecord.setText(R.string.msg_select_complex_cbeffrecord);
		mLoadCBEFFRecord.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validateInput()) {
					getRecord();
				} else {
					showMessage(getString(R.string.format_patron_format_not_valid, mPatronFormat.getText().toString()));
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
					mIndex = 0;
					unpack(data.getData());
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

	private void unpackRecords(CBEFFRecord cbeffRecord) {
		for (CBEFFRecord record : cbeffRecord.getRecords()) {
			if (record.getRecords().size() == 0) {
				recordToFile(record);
			} else {
				unpackRecords(record);
			}
		}
	}

	private void recordToFile(CBEFFRecord record) {
		String fileName;
		try {
			fileName = String.format("Record_%s_%d.dat", lookup.get(record.getBdbFormat()), ++mIndex);
		} catch (Exception ex) {
			fileName = String.format("Record_%d.dat", ++mIndex);
		}
		try {
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, fileName);
			NFile.writeAllBytes(outputFile.getAbsolutePath(), record.getBdbBuffer());
			showMessage("writing: " + fileName);
		} catch (IOException e) {
			showMessage(e.toString());
			Log.e(TAG, "Exception", e);
		}
	}

}
