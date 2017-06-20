package com.neurotec.tutorials.media;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.IOException;
import java.util.Arrays;

import com.neurotec.images.JPEG2KInfo;
import com.neurotec.images.JPEGInfo;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.images.PNGInfo;
import com.neurotec.images.WSQInfo;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class ShowImageInfo extends Activity implements LicensingStateCallback {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final String TAG = ShowImageInfo.class.getSimpleName();
	private static final int REQUEST_CODE_GET_FILE = 1;
	private static final String[] LICENSES = new String[] { LicensingManager.LICENSE_FINGER_WSQ, "Images.IHead", "Images.JPEG2000" };

	// ===========================================================
	// Private fields
	// ===========================================================

	private Button mButton;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private Uri mSelectedFile;

	// ===========================================================
	// Private Methods
	// ===========================================================

	private void showMessage(String message) {
		mResult.append(message + "\n");
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, MediaTutorialsApp.TUTORIALS_ASSET_DIRECTORY);
		startActivityForResult(intent, REQUEST_CODE_GET_FILE);
	}

	private void convertImage() {
		NImage image = null;
		try {
			// Create NImage from file
			image = NImageUtils.fromUri(this, mSelectedFile);

			// Get image format
			NImageFormat format = image.getInfo().getFormat();

			// Print info common to all formats
//			File file = new File(mSelectedFile.getPath());
//			showMessage("Image: " + file.getName());
//			showMessage("Size: " + (file.length() * 100 / 1024) / 100.0 + " KB");
//			showMessage("Format: " + format.getName());

			// Print format specific info
			if (NImageFormat.getJPEG2K().equals(format)) {
				JPEG2KInfo info = (JPEG2KInfo) image.getInfo();
				showMessage("Profile: " + info.getProfile());
				showMessage("Compression ratio: " + info.getRatio());
			} else if (NImageFormat.getJPEG().equals(format)) {
				JPEGInfo info = (JPEGInfo) image.getInfo();
				showMessage("Lossless: " + info.isLossless());
				showMessage("Quality: " + info.getQuality());
			} else if (NImageFormat.getPNG().equals(format)) {
				PNGInfo info = (PNGInfo) image.getInfo();
				showMessage("Compression level: " + info.getCompressionLevel());
			} else if (NImageFormat.getWSQ().equals(format)) {
				WSQInfo info = (WSQInfo) image.getInfo();
				showMessage("Bit rate: " + info.getBitRate());
				showMessage("Implementation number: " + info.getImplementationNumber());
			}
		} catch (Exception e) {
			Log.e(TAG, "", e);
			showMessage("Exception: " + e.getMessage());
		} finally {
			if (image != null) {
				image.dispose();
			}
		}
	}

	// ===========================================================
	// Protected Methods
	// ===========================================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.tutorial_show_image_info);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(getString(R.string.get_info));
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mResult.setText("");
				getImage();
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
		if (requestCode == REQUEST_CODE_GET_FILE) {
			if (resultCode == RESULT_OK) {
				mSelectedFile = data.getData();
				mResult.setText("");
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
