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
import com.neurotec.biometrics.standards.FIRFingerView;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.io.NFile;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.util.NVersion;

public final class FIRecordFromNImage extends Activity implements LicensingStateCallback {

	private static final String TAG = FIRecordFromNImage.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String[] LICENSES = {LicensingManager.LICENSE_FINGER_STANDARDS_FINGERS, LicensingManager.LICENSE_FINGER_STANDARDS_FINGER_TEMPLATES};


	private Button mButton;
	private EditText mFieldNumber;
	private EditText mFieldStandard;
	private EditText mFieldVersion;
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
		setContentView(R.layout.tutorial_firecord_from_nimage);
		mFieldNumber = (EditText) findViewById(R.id.tutorials_field_1);
		mFieldNumber.setHint(R.string.hint_open_images_number);
		mFieldStandard = (EditText) findViewById(R.id.tutorials_field_2);
		mFieldStandard.setHint(R.string.hint_standard_iso_ansi);
		mFieldVersion = (EditText) findViewById(R.id.tutorials_field_3);
		mFieldVersion.setHint(R.string.hint_fi_record_version);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (LicensingManager.isFingerStandardsActivated()) {
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
		FIRecord fi = null;
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
		float versionNumber = Float.parseFloat(mFieldVersion.getText().toString());
		if (versionNumber == 1) {
			version = standard == BDIFStandard.ANSI ? FIRecord.VERSION_ANSI_10 : FIRecord.VERSION_ISO_10;
		} else if (versionNumber == 2) {
			if (standard != BDIFStandard.ISO) {
				throw new IllegalArgumentException("Standard and version is incompatible");
			}
			version = FIRecord.VERSION_ISO_20;
		} else if (versionNumber == 2.5) {
			if (standard != BDIFStandard.ANSI) {
				throw new IllegalArgumentException("Standard and version is incompatible");
			}
			version = FIRecord.VERSION_ANSI_25;
		}else {
			showMessage(getString(R.string.format_version_not_valid, mFieldVersion.getText().toString()));
			return;
		}

		try {
			for (Uri uri : images) {
				NImage imageFromFile = null;
				NImage grayscaleImage = null;
				try {
					// Get NImage from Uri
					imageFromFile = NImageUtils.fromUri(this, uri);

					// Convert to grayscale image
					grayscaleImage = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, imageFromFile);
					if (grayscaleImage.isResolutionIsAspectRatio() || grayscaleImage.getHorzResolution() < 250 || grayscaleImage.getVertResolution() < 250) {
						grayscaleImage.setHorzResolution(500);
						grayscaleImage.setVertResolution(500);
						grayscaleImage.setResolutionIsAspectRatio(false);
					}

					if (fi == null) {

						// Create empty FIRecord
						fi = new FIRecord(standard, version);
						if (isRecordFirstVersion(fi)) {
							fi.setPixelDepth((byte) 8);
							fi.setHorzImageResolution((int) grayscaleImage.getHorzResolution());
							fi.setHorzScanResolution((int) grayscaleImage.getHorzResolution());
							fi.setVertImageResolution((int) grayscaleImage.getVertResolution());
							fi.setVertScanResolution((int) grayscaleImage.getVertResolution());
						}

						FIRFingerView fingerView = new FIRFingerView(fi.getStandard(), fi.getVersion());
						if (!isRecordFirstVersion(fi)) {
							fingerView.setPixelDepth((byte) 8);
							fingerView.setHorzImageResolution((int) grayscaleImage.getHorzResolution());
							fingerView.setHorzScanResolution((int) grayscaleImage.getHorzResolution());
							fingerView.setVertImageResolution((int) grayscaleImage.getVertResolution());
							fingerView.setVertScanResolution((int) grayscaleImage.getVertResolution());
						}
						// Add grayscale image to FIRecord
						fingerView.setImage(grayscaleImage);
						fi.getFingerViews().add(fingerView);
					}
				} finally {
					if (imageFromFile != null) imageFromFile.dispose();
					if (grayscaleImage != null) grayscaleImage.dispose();
				}
			}

			// Save converted template to file
			File outputFile = new File(BiometricStandardsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "firecord-from-nimage.dat");
			NFile.writeAllBytes(outputFile.getAbsolutePath(), fi.save());

			showMessage(getString(R.string.format_converted_template_saved_to, outputFile.getAbsolutePath()));
		} finally {
			if (fi != null) fi.dispose();
		}
	}

	private static boolean isRecordFirstVersion(FIRecord record) {
		return (record.getVersion().equals(FIRecord.VERSION_ANSI_10)) || (record.getVersion().equals(FIRecord.VERSION_ISO_10));
	}

}
