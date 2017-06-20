package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NEAttributes;
import com.neurotec.biometrics.NEImageType;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public final class SegmentIris extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = SegmentIris.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;

	private static final String[] LICENSES = {
		LicensingManager.LICENSE_IRIS_EXTRACTION,
		LicensingManager.LICENSE_IRIS_SEGMENTATION
	};

	private Button mButton;
	private TextView mResult;
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
		setContentView(R.layout.tutorial_segment_iris);
		mButton = (Button) findViewById(R.id.tutorials_button_1);
		mButton.setText(R.string.msg_select_image);
		mButton.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
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
		if (requestCode == REQUEST_CODE_GET_IMAGE) {
			if (resultCode == RESULT_OK) {
				try {
					segment(data.getData());
				} catch (Exception e) {
					showMessage(e.getMessage());
					Log.e(TAG, "Exception", e);
				}
			}
		}
	}

	private void getImage() {
		Intent intent = new Intent(this, DirectoryViewer.class);
		intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
		startActivityForResult(intent, REQUEST_CODE_GET_IMAGE);
	}

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private void segment(Uri imageUri) throws IOException {
		if (!LicensingManager.isIrisExtractionActivated()) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_IRIS_EXTRACTION));
			return; // Iris extraction is not activated, so return
		}
		if (!LicensingManager.isActivated(LicensingManager.LICENSE_IRIS_SEGMENTATION)) {
			showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_IRIS_SEGMENTATION));
			return; // Iris segmentation is not activated, so return
		}

		NBiometricClient biometricClient = null;
		NSubject subject = null;
		NIris iris = null;
		NBiometricTask task = null;

		try {
			biometricClient = new NBiometricClient();
			subject = new NSubject();
			iris = new NIris();

			iris.setImage(NImageUtils.fromUri(this, imageUri));

			iris.setImageType(NEImageType.CROPPED_AND_MASKED);
			subject.getIrises().add(iris);

			task = biometricClient.createTask(EnumSet.of(NBiometricOperation.SEGMENT), subject);

			biometricClient.performTask(task);
			if (task.getError() != null) {
				showError(task.getError());
				return;
			}

			if (task.getStatus() == NBiometricStatus.OK) {
				for (NEAttributes attributes : iris.getObjects()) {
					// Display iris data
					showMessage(getString(R.string.format_overall_quality, attributes.getQuality()));
					showMessage(getString(R.string.format_gray_level_spread, attributes.getGrayScaleUtilisation()));
					showMessage(getString(R.string.format_interlace, attributes.getInterlace()));
					showMessage(getString(R.string.format_iris_pupil_concentricity, attributes.getIrisPupilConcentricity()));
					showMessage(getString(R.string.format_iris_pupil_contrast, attributes.getIrisPupilContrast()));
					showMessage(getString(R.string.format_iris_radius, attributes.getIrisRadius()));
					showMessage(getString(R.string.format_iris_sclera_contrast, attributes.getIrisScleraContrast()));
					showMessage(getString(R.string.format_margin_adequacy, attributes.getMarginAdequacy()));
					showMessage(getString(R.string.format_pupil_boundary_circularity, attributes.getPupilBoundaryCircularity()));
					showMessage(getString(R.string.format_pupil_to_iris_ratio, attributes.getPupilToIrisRatio()));
					showMessage(getString(R.string.format_sharpness, attributes.getSharpness()));
					showMessage(getString(R.string.format_usable_iris_area, attributes.getUsableIrisArea()));
				}

				// Save segmented image

				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "cropped-and-masked-iris.png");
				((NIris) iris.getObjects().get(0).getChild()).getImage().save(outputFile.getAbsolutePath());
				showMessage(getString(R.string.format_segmented_iris_image_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage(getString(R.string.format_segmentation_failed, task.getStatus()));
			}
		} finally {
			if (biometricClient != null) biometricClient.dispose();
			if (subject != null) subject.dispose();
			if (iris != null) iris.dispose();
			if (task != null) task.dispose();
		}
	}
}
