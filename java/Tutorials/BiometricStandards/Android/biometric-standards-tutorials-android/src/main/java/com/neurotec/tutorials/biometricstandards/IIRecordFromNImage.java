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
import com.neurotec.biometrics.standards.IIRIrisImage;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.util.NVersion;

public final class IIRecordFromNImage extends Activity implements LicensingStateCallback {

	private static final String TAG = IIRecordFromNImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String LICENSE = LicensingManager.LICENSE_IRIS_STANDARDS;

	private final List<Uri> mImages = new ArrayList<Uri>();

	private Button mButton;
	private EditText mFieldNumber;
	private EditText mFieldStandard;
	private EditText mFieldVersion;
	private TextView mResult;
	private int mImagesNumber;
	private ProgressDialog mProgressDialog;

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
		setContentView(R.layout.tutorial_iirecord_from_nimage);
		mFieldNumber = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldNumber.setHint(R.string.hint_open_images_number);
		mFieldStandard = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldStandard.setHint(R.string.hint_standard_iso_ansi);
		mFieldVersion = (EditText) findViewById(R.id.tutorials_field_3);
		mFieldVersion.setHint(R.string.hint_ii_record_version);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isIrisStandardsActivated()) {
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
		IIRecord iiRec = null;

		BDIFStandard standard;
		String standardField = mFieldStandard.getText().toString();
		if (standardField.equalsIgnoreCase(getString(R.string.standard_iso))) {
			standard = BDIFStandard.ISO;
		} else if (standardField.equalsIgnoreCase(getString(R.string.standard_ansi))) {
			standard = BDIFStandard.ANSI;
		} else {
			showMessage(getString(R.string.format_unknown_standard, mFieldStandard.getText().toString()));
			return;
		}

		NVersion version;
		int versionNumber = Integer.parseInt(mFieldVersion.getText().toString());
		if (versionNumber == 1) {
			version = standard == BDIFStandard.ANSI ? IIRecord.VERSION_ANSI_10 : IIRecord.VERSION_ISO_10;
		} else if (versionNumber == 2) {
			if (standard != BDIFStandard.ISO) {
				throw new IllegalArgumentException("Standard and version is incompatible");
			}
			version = IIRecord.VERSION_ISO_20;
		}else {
			showMessage(getString(R.string.format_version_not_valid, mFieldVersion.getText().toString()));
			return;
		}

		try {
			for (Uri uri : images) {
				NImage imageFromFile = null;
				NImage image = null;
				try {
					// Get NImage from Uri
					imageFromFile = NImageUtils.fromUri(this, uri);

					// Convert to grayscale image
					image = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, imageFromFile);

					if (iiRec == null) {
						// Create empty IIRecord
						iiRec = new IIRecord(standard, version);
						if (isRecordFirstVersion(version)) {
							iiRec.setRawImageHeight(image.getHeight());
							iiRec.setRawImageWidth(image.getWidth());
							iiRec.setIntensityDepth((byte) 8);
						}
					}

					IIRIrisImage iirIrisImage = new IIRIrisImage(standard, version);
					if (!isRecordFirstVersion(version)) {
						iirIrisImage.setImageWidth(image.getWidth());
						iirIrisImage.setImageHeight(image.getHeight());
						iirIrisImage.setIntensityDepth((byte) 8);
					}
					iirIrisImage.setNImage(image);

					// Add grayscale image to IIRecord
					iiRec.getIrisImages().add(iirIrisImage);
				} finally {
					if (imageFromFile != null) imageFromFile.dispose();
					if (image != null) image.dispose();
				}
			}

			// Save converted template to file
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "iirecord-from-nimage.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), iiRec.save());

			showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));
		} finally {
			if (iiRec != null) iiRec.dispose();
		}
	}

	private static boolean isRecordFirstVersion(NVersion version) {
		return  (version.equals(IIRecord.VERSION_ANSI_10)) || (version.equals(IIRecord.VERSION_ISO_10));
	}

}
