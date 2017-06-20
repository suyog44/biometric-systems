package com.neurotec.tutorials.media;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.text.method.DigitsKeyListener;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;

import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.WSQInfo;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class CreateWSQ extends Activity implements LicensingStateCallback {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final String TAG = CreateWSQ.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 345;
	private static final String LICENSE = LicensingManager.LICENSE_FINGER_WSQ;

	// ===========================================================
	// Private fields
	// ===========================================================

	private Button mButton;
	private TextView mResult;
	private EditText mTextOutputFile;
	private EditText mTextBitRate;
	private ProgressDialog mProgressDialog;
	private Uri mSelectedFile;

	// ===========================================================
	// Private Methods
	// ===========================================================

	private void convertImage() {
		NImage image = null;
		WSQInfo info = null;

		try {
			// Create an NImage from file
			image = NImageUtils.fromUri(this, mSelectedFile);

			// Create WSQInfo to store bit rate
			info = (WSQInfo) NImageFormat.getWSQ().createInfo(image);

			// Set specified bit rate (or default if bit rate was not
			// specified)
			float bitrate;
			try {
				if (mTextBitRate.getText().toString() != null) {
					bitrate = Float.parseFloat(mTextBitRate.getText().toString());
				} else {
					bitrate = WSQInfo.DEFAULT_BIT_RATE;
				}
			} catch (NumberFormatException e) {
				bitrate = WSQInfo.DEFAULT_BIT_RATE;
			}

			info.setBitRate(bitrate);

			// Save image in WSQ format and bit rate to file
			File outputFile = new File(MediaTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, mTextOutputFile.getText().toString());

			image.save(outputFile.getAbsolutePath(), info);
			showMessage(String.format("WSQ image with bit rate %s was saved to %s", bitrate, outputFile.getAbsolutePath()));
		} catch (Exception e) {
			Log.e(TAG, "Exception", e);
			showMessage(e.getMessage());
		} finally {
			if (image != null) {
				image.dispose();
			}
			if (info != null) {
				info.dispose();
			}
		}
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, MediaTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	// ===========================================================
	// Protected Methods
	// ===========================================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_create_wsq);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(getString(R.string.convert));
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mResult.setText("");
				getImage();
			}
		});
		mResult = (TextView) findViewById(R.id.tutorials_results);

		mTextOutputFile = (EditText) findViewById(R.id.tutorials_field_1);
		mTextOutputFile.setHint("Output File name");
		mTextBitRate = (EditText) findViewById(R.id.tutorials_field_2);
		mTextBitRate.setHint("BitRate");
		mTextBitRate.setKeyListener(DigitsKeyListener.getInstance("0123456789."));

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
				mSelectedFile = data.getData();
				convertImage();
			}
		}
	}

	// ===========================================================
	// Public Methods
	// ===========================================================

	@Override
	public void onLicensingStateChanged(final LicensingStateResult state) {
		final Context context = this;
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				switch (state.getState()) {
				case OBTAINING:
					Log.i(TAG, getString(R.string.msg_obtaining_licenses));
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

}
