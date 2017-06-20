package com.neurotec.tutorials.biometrics;

import android.app.Activity;
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
import java.util.ArrayList;
import java.util.List;

import com.neurotec.biometrics.NFRecord;
import com.neurotec.biometrics.NFTemplate;
import com.neurotec.biometrics.NTemplate;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.util.IOUtils;

public final class CreateMultiFingerTemplate extends Activity {

	private static final String TAG = CreateMultiFingerTemplate.class.getSimpleName();
	private static final int REQUEST_CODE_GET_RECORD = 1;

	private final List<Uri> mRecords = new ArrayList<Uri>();

	private Button mButton;
	private EditText mFieldNumber;
	private TextView mResult;
	private int mRecordsNumber;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_multi_finger_template);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_nfrecord);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validateInput()) {
					for (int i = 0; i < mRecordsNumber; i++) {
						getRecord();
					}
				}
			}
		});
		mFieldNumber = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldNumber.setHint(R.string.hint_open_records_number);
		mResult = (TextView) findViewById(R.id.tutorials_results);
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_RECORD) {
			if (resultCode == RESULT_OK) {
				try {
					mRecords.add(data.getData());
					if (mRecords.size() == mRecordsNumber) {
						List<Uri> temp = new ArrayList<Uri>(mRecords);
						mRecords.clear();
						create(temp);
					}
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			} else {
				mRecords.clear();
			}
		}
	}

	private void getRecord() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private boolean validateInput() {
		try {
			mRecordsNumber = Integer.parseInt(mFieldNumber.getText().toString());
		} catch (NumberFormatException e) {
			showMessage(getString(R.string.format_number_not_valid, mFieldNumber.getText().toString()));
			return false;
		}
		return true;
	}

	private void create(List<Uri> images) throws IOException {
		NFTemplate nfTemplate = null;
		try {
			nfTemplate = new NFTemplate();
			// Read all input NFRecords
			for (Uri image : images) {
				showMessage(getString(R.string.format_reading, image.getPath()));
				NTemplate template = new NTemplate(IOUtils.toByteBuffer(CreateMultiFingerTemplate.this, image));
				if (template.getFingers() != null) {
					for (NFRecord record : template.getFingers().getRecords()) {
						nfTemplate.getRecords().add(record);
					}
				}
				template.dispose();
			}

			if (nfTemplate.getRecords().size() == 0) {
				showMessage(getString(R.string.msg_not_saving_no_records_found));
				return;
			}

			showMessage(getString(R.string.format_records_found, nfTemplate.getRecords().size()));

			// Save NTemplate to file
			File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "multifinger-ntemplate.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), nfTemplate.save());

			showMessage(getString(R.string.format_multifinger_template_saved_to, outputFile.getAbsolutePath()));
		} finally {
			if (nfTemplate != null) nfTemplate.dispose();
		}
	}

}
