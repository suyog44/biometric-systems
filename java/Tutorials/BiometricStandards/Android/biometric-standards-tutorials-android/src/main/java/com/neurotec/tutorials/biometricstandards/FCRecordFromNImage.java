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
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FCRFaceImage;
import com.neurotec.biometrics.standards.FCRFaceImageType;
import com.neurotec.biometrics.standards.FCRImageDataType;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.images.NImage;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class FCRecordFromNImage extends Activity implements LicensingStateCallback {

	private static final String TAG = FCRecordFromNImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String LICENSE = LicensingManager.LICENSE_FACE_STANDARDS;

	private Button mButton;
	private EditText mFieldNumber;
	private TextView mResult;
	private int mImagesNumber;
	private ProgressDialog mProgressDialog;
	private List<Uri> mImages = new ArrayList<Uri>();

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
		setContentView(R.layout.tutorial_fcrecord_from_nimage);
		mFieldNumber = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldNumber.setHint(R.string.hint_open_images_number);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFaceStandardsActivated()) {
					if (validateInput()) {
						for (int i = 0; i < mImagesNumber; i++) {
							getImage();
						}
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
					mImages.add(data.getData());
					if (mImages.size() == mImagesNumber) {
						List<Uri> temp = new ArrayList<Uri>(mImages);
						mImages.clear();
						convert(temp);
					}
				} catch (Exception e) {
					showMessage(e.toString());
					Log.e(TAG, "Exception", e);
				}
			} else {
				mImages.clear();
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
		try {
			mImagesNumber = Integer.parseInt(mFieldNumber.getText().toString());
		} catch (NumberFormatException e) {
			showMessage(getString(R.string.format_number_not_valid, mFieldNumber.getText().toString()));
			return false;
		}
		return true;
	}

	private void convert(List<Uri> images) throws IOException {
		FCRecord fc = null;
		try {
			// Specify standard and version to be used
			fc = new FCRecord(BDIFStandard.ISO, FCRecord.VERSION_ISO_30);
			for (Uri uri : images) {
				NImage image = null;
				try {
					// Get NImage from Uri
					image = NImageUtils.fromUri(this, uri);

					// Add grayscale image to FCRecord
					FCRFaceImage img = new FCRFaceImage(fc.getStandard(), fc.getVersion());
					img.setFaceImageType(FCRFaceImageType.BASIC);
					img.setImageDataType(FCRImageDataType.JPEG);
					img.setImage(image);
					fc.getFaceImages().add(img);
				} finally {
					if (image != null) image.dispose();
				}
			}

			// Save converted template to file
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "fcrecord-from-nimage.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), fc.save());

			showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));
		} catch (Exception e) {
			showMessage(e.toString());
		} finally {
			if (fc != null) fc.dispose();
		}
	}

}
