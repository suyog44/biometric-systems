package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NLFeaturePoint;
import com.neurotec.biometrics.NLProperty;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;
import com.neurotec.samples.util.ResourceUtils;

public final class DetectFacialFeatures extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = DetectFacialFeatures.class.getSimpleName();
	private static final int REQUEST_CODE_GET_IMAGE = 1;
	private static final String[] LICENSES = {
		LicensingManager.LICENSE_FACE_DETECTION,
		LicensingManager.LICENSE_FACE_EXTRACTION,
		LicensingManager.LICENSE_FACE_SEGMENTS_DETECTION
	};

	private Button mButton;
	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private BackgroundTask mTask;

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
		setContentView(R.layout.tutorial_detect_facial_features);
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
		if (mTask != null) {
			mTask.cancel(true);
		}
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
					mTask = new BackgroundTask();
					showMessage(getString(R.string.msg_detecting));
					mTask.execute(data.getData());
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

	private class BackgroundTask extends AsyncTask<Uri, String, Boolean> {

		@Override
		protected void onProgressUpdate(String... messages) {
			for (String message : messages) {
				showMessage(message);
			}
		}

		@Override
		protected Boolean doInBackground(Uri... params) {
			if (isCancelled()) {
				return null;
			}

			NBiometricClient biometricClient = null;
			NSubject subject = null;
			NFace face = null;
			NBiometricTask task = null;

			try {
				if (!LicensingManager.isActivated(LicensingManager.LICENSE_FACE_DETECTION)) {
					showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FACE_DETECTION));
					return false;
				}

				biometricClient = new NBiometricClient();
				subject = new NSubject();
				face = new NFace();

				face.setImage(NImageUtils.fromUri(DetectFacialFeatures.this, params[0]));

				subject.getFaces().add(face);

				subject.setMultipleSubjects(true);

				boolean isSegmentationActivated = LicensingManager.isActivated(LicensingManager.LICENSE_FACE_SEGMENTS_DETECTION);
				if (isSegmentationActivated) {
					biometricClient.setFacesDetectBaseFeaturePoints(true);
					biometricClient.setFacesRecognizeExpression(true);
					biometricClient.setFacesDetectProperties(true);
					biometricClient.setFacesDetermineGender(true);
					biometricClient.setFacesDetermineAge(true);
				}

				biometricClient.setFacesTemplateSize(NTemplateSize.MEDIUM);

				task = biometricClient.createTask(EnumSet.of(NBiometricOperation.DETECT_SEGMENTS), subject);

				biometricClient.performTask(task);

				Throwable taskError = task.getError();
				if (taskError != null) {
					publishProgress(taskError.getMessage());
					return false;
				}

				if (task.getStatus() == NBiometricStatus.OK) {
					publishProgress(getString(R.string.format_found_faces, face.getObjects().size()));

					for (NLAttributes attributes : face.getObjects()) {
						publishProgress(getString(R.string.format_face_rect,
								attributes.getBoundingRect().left, attributes.getBoundingRect().top,
								attributes.getBoundingRect().right, attributes.getBoundingRect().bottom
								));

						printNleFeaturePoint("LeftEyeCenter", attributes.getLeftEyeCenter());
						printNleFeaturePoint("RightEyeCenter", attributes.getRightEyeCenter());

						if (isSegmentationActivated) {
							printNleFeaturePoint("MouthCenter", attributes.getMouthCenter());
							printNleFeaturePoint("NoseTip", attributes.getNoseTip());

							if (attributes.getAge() == 254) {
								publishProgress(getString(R.string.msg_age_not_detected));
							} else {
								publishProgress(getString(R.string.format_age, attributes.getAge()));
							}
							if (attributes.getGenderConfidence() == 255) {
								publishProgress(getString(R.string.msg_gender_not_detected));
							} else {
								publishProgress(getString(R.string.format_gender_and_confidence, attributes.getGender(), attributes.getGenderConfidence()));
							}
							if (attributes.getExpressionConfidence() == 255) {
								publishProgress(getString(R.string.msg_expression_not_detected));
							} else {
								publishProgress(getString(R.string.msg_expression_and_confidence, ResourceUtils.getEnum(DetectFacialFeatures.this, attributes.getExpression()), attributes.getExpressionConfidence()));
							}
							if (attributes.getBlinkConfidence() == 255) {
								publishProgress(getString(R.string.msg_blink_not_detected));
							} else {
								publishProgress(getString(R.string.msg_blink_and_confidence, attributes.getProperties().contains(NLProperty.BLINK), attributes.getBlinkConfidence()));
							}
							if (attributes.getMouthOpenConfidence() == 255) {
								publishProgress(getString(R.string.msg_mouth_open_not_detected));
							} else {
								publishProgress(getString(R.string.msg_mouth_open_and_confidence, attributes.getProperties().contains(NLProperty.MOUTH_OPEN), attributes.getMouthOpenConfidence()));
							}
							if (attributes.getGlassesConfidence() == 255) {
								publishProgress(getString(R.string.msg_glasses_not_detected));
							} else {
								publishProgress(getString(R.string.msg_glasses_and_confidence, attributes.getProperties().contains(NLProperty.GLASSES), attributes.getGlassesConfidence()));
							}
							if (attributes.getDarkGlassesConfidence() == 255) {
								publishProgress(getString(R.string.msg_dark_glasses_not_detected));
							} else {
								publishProgress(getString(R.string.msg_dark_glasses_and_confidence, attributes.getProperties().contains(NLProperty.DARK_GLASSES), attributes.getDarkGlassesConfidence()));
							}
						}
					}
				} else {
					publishProgress(getString(R.string.msg_faces_not_found, task.getStatus()));
				}
			} catch (Exception e) {
				publishProgress(e.getMessage());
				return false;
			} finally {
				if (biometricClient != null) biometricClient.dispose();
				if (subject != null) subject.dispose();
				if (face != null) face.dispose();
				if (task != null) task.dispose();
			}
			return true;
		}

		private void printNleFeaturePoint(String name, NLFeaturePoint point) {
			if (point.confidence == 0) {
				publishProgress(String.format("\t%s feature unavailable. confidence: 0%n", name));
				return;
			}
			publishProgress(String.format("\t%s feature found. X: %d, Y: %d, confidence: %d%n", name, point.x, point.y, point.confidence));
		}
	}
}
