package com.neurotec.tutorials.biometricstandards;

import android.app.Activity;
import android.app.AlertDialog;
import android.app.Dialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANValidationLevel;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.biometrics.standards.FMRecord;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.IOUtils;
import com.neurotec.samples.view.BaseDialogFragment;

public class ComplexCBEFFRecord extends Activity implements LicensingStateCallback {

	private interface AddRecordListener {
		void onAddRecordPressed(int patronFormat, int recordType, int recordStandard);
	}

	private static final String TAG = "ComplexCBEFFRecord";
	private static final int REQUEST_CODE_GET_RECORD = 1;

	private static final List<String> LICENSES = Arrays.asList(
			"Biometrics.Standards.Base",
			"Biometrics.Standards.Irises",
			"Biometrics.Standards.Fingers",
			"Biometrics.Standards.Faces",
			"Biometrics.Standards.Palms"
			);

	private enum RecordType{
		ANTEMPLATE(0),
		FCRECORD(1),
		FIRECORD(2),
		FMRECORD(3),
		IIRecord(4);

		private int value;

		private RecordType(int value) {
			this.value = value;
		}

		int getValue() {
			return value;
		}

		static RecordType get(int value) {
			for (RecordType type : values()) {
				if (type.getValue() == value) {
					return type;
				}
			}
			throw new IllegalArgumentException("No such RecordType");
		}
	}

	private static class RecordInformation {
		public Uri recordFile;
		public BDIFStandard standard;
		public RecordType recordType;
		public int patronFormat;
	}

	private ProgressDialog mProgressDialog;
	private TextView mResult;
	private EditText mPatronFormat;
	private Button mAddRecord;
	private Button mCreateCBEFFRecord;
	private List<RecordInformation> recordList;
	private AddRecordListener mListener = new AddRecordListener() {

		@Override
		public void onAddRecordPressed(int patronFormat, int recordType, int recordStandard) {
			Intent intent = new Intent(ComplexCBEFFRecord.this, DirectoryViewer.class);
			Bundle bundle = new Bundle();
			bundle.putInt("patronFormat", patronFormat);
			bundle.putInt("recordType", recordType);
			bundle.putInt("recordStandard", recordStandard);
			intent.putExtra(DirectoryViewer.EXTRA_RETURNED_BUNDLE_DATA, bundle);
			intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricStandardsTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
			startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
		}
	};

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
		mAddRecord.setEnabled(enable);
		mCreateCBEFFRecord.setEnabled(enable);
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private boolean validatePatronFormat() {
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

	private boolean validateRecordList() {
		return recordList != null ? !recordList.isEmpty() : false;
	}

	private void createComplexCBEFFRecord() {
		try {
			showMessage("Creating complexed CBEFF Record");
			// Create root CbeffRecord
			int patronFormat = Integer.parseInt(mPatronFormat.getText().toString(), 16);
			CBEFFRecord rootRecord = new CBEFFRecord(patronFormat);

			for (RecordInformation recordInfo : recordList) {
				CBEFFRecord cbeffRecord;
				NBuffer buffer = NBuffer.fromByteBuffer(IOUtils.toByteBuffer(this, recordInfo.recordFile));

				// Create a record object according information specified in arguments
				switch (recordInfo.recordType) {
				case ANTEMPLATE:
					ANTemplate anTemplate = new ANTemplate(buffer, ANValidationLevel.STANDARD);
					cbeffRecord = new CBEFFRecord(anTemplate, recordInfo.patronFormat);
					break;
				case FCRECORD:
					FCRecord fcRecord = new FCRecord(buffer, recordInfo.standard);
					cbeffRecord = new CBEFFRecord(fcRecord, recordInfo.patronFormat);
					break;
				case FIRECORD:
					FIRecord fiRecord = new FIRecord(buffer, recordInfo.standard);
					cbeffRecord = new CBEFFRecord(fiRecord, recordInfo.patronFormat);
					break;
				case FMRECORD:
					FMRecord fmRecord = new FMRecord(buffer, recordInfo.standard);
					cbeffRecord = new CBEFFRecord(fmRecord, recordInfo.patronFormat);
					break;
				case IIRecord:
					IIRecord iiRecord = new IIRecord(buffer, recordInfo.standard);
					cbeffRecord = new CBEFFRecord(iiRecord, recordInfo.patronFormat);
					break;
				default:
					throw new AssertionError("Not recognised record format");
				}

				rootRecord.getRecords().add(cbeffRecord);
				showMessage(String.format("Rocord: \npatron format: %s\nrecordType: %s\npatronFormat: %d added", recordInfo.patronFormat, recordInfo.recordType, recordInfo.patronFormat));
			}
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "complex-cbeffrecord.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), rootRecord.save());
			showMessage("Record sucessfully saved: " + outputFile.getAbsolutePath());
		} catch (Exception e) {
			showMessage(e.toString());
			Log.e(TAG, "Exception", e);
		}
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_complex_cbeff_record);
		recordList = new ArrayList<RecordInformation>();

		mPatronFormat = (EditText) findViewById(R.id.tutorials_field_1);
		mPatronFormat.setHint(getString(R.string.hint_patron_format));

		mAddRecord = (Button) findViewById(R.id.tutorials_button_1);
		mAddRecord.setText(R.string.msg_add_record);
		mAddRecord.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validatePatronFormat()) {
					BaseDialogFragment fragment = new AddRecordDialogFragment(mListener);
					fragment.show(getFragmentManager(), "add_record");
				} else  {
					showMessage(getString(R.string.format_patron_format_not_valid, mPatronFormat.getText().toString()));
				}
			}
		});

		mCreateCBEFFRecord = (Button) findViewById(R.id.tutorials_button_2);
		mCreateCBEFFRecord.setText(R.string.msg_create_complex_cbeffrecord);
		mCreateCBEFFRecord.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validatePatronFormat() && validateRecordList()) {
					createComplexCBEFFRecord();
				} else  {
					if (!validatePatronFormat()) showMessage(getString(R.string.format_patron_format_not_valid, mPatronFormat.getText().toString()));
					if (!validateRecordList()) showMessage(getString(R.string.msg_record_list_empty));
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
					RecordInformation recordInfo = new RecordInformation();
					recordInfo.recordFile = data.getData();
					recordInfo.recordType = RecordType.get(data.getBundleExtra(DirectoryViewer.EXTRA_RETURNED_BUNDLE_DATA).getInt("recordType"));
					recordInfo.standard = BDIFStandard.get(data.getBundleExtra(DirectoryViewer.EXTRA_RETURNED_BUNDLE_DATA).getInt("recordStandard"));
					recordInfo.patronFormat = data.getBundleExtra(DirectoryViewer.EXTRA_RETURNED_BUNDLE_DATA).getInt("patronFormat");
					recordList.add(recordInfo);
					showMessage(String.format("Rocord: \npatron format: %s\nrecordType: %s\npatronFormat: %d added", recordInfo.patronFormat, recordInfo.recordType, recordInfo.patronFormat));
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

	// ===========================================================
	// Dialog fragment
	// ===========================================================

	private class AddRecordDialogFragment extends BaseDialogFragment {

		private AddRecordListener mAddRecordListener;

		private AddRecordDialogFragment(AddRecordListener listener) {
			mAddRecordListener = listener;
		}

		@Override
		public Dialog onCreateDialog(Bundle savedInstanceState) {
			AlertDialog.Builder dialog = new AlertDialog.Builder(getActivity());

			LayoutInflater factory = LayoutInflater.from(getActivity());
			ViewGroup root = null;
			final View addRecordDialog = factory.inflate(R.layout.tutorial_complex_cbeff_record_add_record_dialog, root);
			dialog.setView(addRecordDialog);

			dialog.setTitle(R.string.msg_add_record);

			final EditText patronFormat = (EditText) addRecordDialog.findViewById(R.id.tutorials_field_1);
			patronFormat.setHint(getString(R.string.hint_patron_format));
			patronFormat.setText("");

			final Spinner recordType = (Spinner) addRecordDialog.findViewById(R.id.tutorials_spinner_1);
			RecordType[] recordTypes = new RecordType[RecordType.values().length];
			for (int i = 0; i < RecordType.values().length; i++) {
				recordTypes[i] = RecordType.values()[i];
			}
			ArrayAdapter<RecordType> recordAdapter = new ArrayAdapter<RecordType>(dialog.getContext(), android.R.layout.simple_list_item_1, recordTypes);
			recordType.setAdapter(recordAdapter);

			final Spinner recordStandard = (Spinner) addRecordDialog.findViewById(R.id.tutorials_spinner_2);
			BDIFStandard[] standardTypes = new BDIFStandard[BDIFStandard.values().length];
			for (int i = 0; i < BDIFStandard.values().length; i++) {
				standardTypes[i] = BDIFStandard.values()[i];
			}
			ArrayAdapter<BDIFStandard> standardAdapter = new ArrayAdapter<BDIFStandard>(dialog.getContext(), android.R.layout.simple_list_item_1, standardTypes);
			recordStandard.setAdapter(standardAdapter);

			dialog.setPositiveButton(R.string.msg_add_record, new DialogInterface.OnClickListener() {

				@Override
				public void onClick(DialogInterface dialog, int which) {
					mAddRecordListener.onAddRecordPressed(Integer.parseInt(patronFormat.getText().toString(), 16), ((RecordType) recordType.getSelectedItem()).getValue(), ((BDIFStandard) recordStandard.getSelectedItem()).getValue());
					dialog.dismiss();
				}
			});
			return dialog.create();
		}
	}

}
