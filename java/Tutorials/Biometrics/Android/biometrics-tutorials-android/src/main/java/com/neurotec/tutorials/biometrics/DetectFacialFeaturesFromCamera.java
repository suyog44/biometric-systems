package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.beans.NParameterBag;
import com.neurotec.beans.NParameterDescriptor;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NLFeaturePoint;
import com.neurotec.biometrics.NLProperty;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NCamera;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.licensing.NLicense;
import com.neurotec.plugins.NPlugin;
import com.neurotec.plugins.NPluginState;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.util.concurrent.CompletionHandler;

public class DetectFacialFeaturesFromCamera extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = "DetectFacialFeaturesFromCamera";
	private static final String LICENSES = "Biometrics.FaceDetection,Biometrics.FaceExtraction,Devices.Cameras,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment";
	private static final String ADDITIONAL_LICENSES = "Biometrics.FaceSegmentsDetection";
	private static final int REQUEST_CODE_GET_RECORD = 1;
	private static final String ANDROID_ASSET_DESCRIPTOR = "file:///android_asset/";

	private ProgressDialog mProgressDialog;
	private Button mCameraButton;
	private TextView mStatus;
	private EditText mRtspAddress;
	private Button mRtspCameraButton;
	private Button mVideoFileButton;

	private NBiometricClient mBiometricClient;

	private CompletionHandler<NBiometricTask, NBiometricOperation> completionHandler = new CompletionHandler<NBiometricTask, NBiometricOperation>() {

		@Override
		public void completed(NBiometricTask result, NBiometricOperation attachment) {
			if (result.getError() != null) {
				showError(result.getError());
				return;
			}

			if (result.getStatus() == NBiometricStatus.OK) {
				int index = 0;
				for (NSubject subject : result.getSubjects()) {
					for (NFace face : subject.getFaces()) {
						printFaceAttributes(face);
					}
					try {
					File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, String.format("subject_template_%d.dat", ++index));
						NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());

					File outputImage = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, String.format("subject_image_%d.dat", index));
					NFile.writeAllBytes(outputImage.getAbsolutePath(), subject.getTemplateBuffer());
					} catch (IOException e) {
						showMessage(e.toString());
						Log.e(TAG, "Exception", e);
					}
				}
			} else {
				showMessage(String.format("Capturing failed: %s\n", result.getStatus()));
			}
		}

		@Override
		public void failed(Throwable exc, NBiometricOperation attachment) {
			showMessage(exc.toString());
			Log.e(TAG, "Throwable", exc);
		}
	};

	private LicensingStateCallback additionalLicenseCallback = new LicensingStateCallback() {

		@Override
		public void onLicensingStateChanged(final LicensingStateResult state) {
			final Context context = DetectFacialFeaturesFromCamera.this;
			runOnUiThread(new Runnable() {
				@Override
				public void run() {
					try {
						switch (state.getState()) {
						case OBTAINING:
							mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_additional_licenses));
							break;
						case OBTAINED:
							mProgressDialog.dismiss();
							showMessage(getString(R.string.msg_additional_licenses_obtained));
							initializeBiometricClient(true);
							break;
						case NOT_OBTAINED:
							mProgressDialog.dismiss();
							showMessage(getString(R.string.msg_additional_licenses_not_obtained));
							if (state.getException() != null) {
								showMessage(state.getException().getMessage());
							}
							initializeBiometricClient(false);
							break;
						default:
							throw new AssertionError("Unknown state: " + state);
						}
					} catch (Exception ex) {
						showMessage(ex.toString());
						Log.e(TAG, "Exception", ex);
					}
				}
			});
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
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
					enableControlls(true);
					LicensingManager.getInstance().obtain(DetectFacialFeaturesFromCamera.this, additionalLicenseCallback, Arrays.asList(ADDITIONAL_LICENSES));
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

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mStatus.append(message + "\n");
			}
		});
	}

	private void enableControlls(boolean enabled) {
		mCameraButton.setEnabled(enabled);
		mRtspCameraButton.setEnabled(enabled);
		mVideoFileButton.setEnabled(enabled);
	}

	private NBiometricTask createNewTask() {
		NSubject subject = new NSubject();
		NFace face = new NFace();
		face.setCaptureOptions(EnumSet.of(NBiometricCaptureOption.STREAM));
		// Add NFace to NSubject.
		subject.getFaces().add(face);

		return mBiometricClient.createTask(EnumSet.of(NBiometricOperation.CAPTURE,
				NBiometricOperation.DETECT_SEGMENTS,
				NBiometricOperation.SEGMENT,
				NBiometricOperation.ASSESS_QUALITY), subject);
	}

	private static NDevice connectDevice(NDeviceManager deviceManager, String url, boolean isUrl) {
		NPlugin plugin = NDeviceManager.getPluginManager().getPlugins().get("Media");
		if ((plugin.getState() == NPluginState.PLUGGED) && (NDeviceManager.isConnectToDeviceSupported(plugin))) {
			NParameterDescriptor[] parameters = NDeviceManager.getConnectToDeviceParameters(plugin);
			NParameterBag bag = new NParameterBag(parameters);
			if (isUrl) {
				bag.setProperty("DisplayName", "IP Camera");
				bag.setProperty("Url", url);
			} else {
				bag.setProperty("DisplayName", "Video file");
				bag.setProperty("FileName", url);
			}
			return deviceManager.connectToDevice(plugin, bag.toPropertyBag());
		}
		throw new RuntimeException("Failed to connect specified device!");
	}

	private void printFaceAttributes(NFace face) {
		for (NLAttributes attributes : face.getObjects()) {
			showMessage(String.format("\tlocation = (%d, %d), width = %d, height = %d%n",
							attributes.getBoundingRect().left,
							attributes.getBoundingRect().bottom,
							attributes.getBoundingRect().width(),
							attributes.getBoundingRect().height()));

			printNleFeaturePoint("LeftEyeCenter", attributes.getLeftEyeCenter());
			printNleFeaturePoint("RightEyeCenter", attributes.getRightEyeCenter());

			try {
				if (NLicense.isComponentActivated(ADDITIONAL_LICENSES)) {
					printNleFeaturePoint("MouthCenter", attributes.getMouthCenter());
					printNleFeaturePoint("NoseTip", attributes.getNoseTip());

					showMessage("");
					if (attributes.getAge() == 254) {
						showMessage("\t\tAge not detected");
					} else {
						showMessage(String.format("\t\tAge: %d%n", attributes.getAge()));
					}
					if (attributes.getGenderConfidence() == 255) {
						showMessage("\t\tGender not detected");
					} else {
						showMessage(String.format("\t\tGender: %s, Confidence: %d%n", attributes.getGender(), attributes.getGenderConfidence()));
					}
					if (attributes.getExpressionConfidence() == 255) {
						showMessage("\t\tExpression not detected");
					} else {
						showMessage(String.format("\t\tExpression: %s, Confidence: %d%n", attributes.getExpression(), attributes.getExpressionConfidence()));
					}
					if (attributes.getBlinkConfidence() == 255) {
						showMessage("\t\tBlink not detected");
					} else {
						showMessage(String.format("\t\tBlink: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.BLINK), attributes.getBlinkConfidence()));
					}
					if (attributes.getMouthOpenConfidence() == 255) {
						showMessage("\t\tMouth open not detected");
					} else {
						showMessage(String.format("\t\tMouth open: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.MOUTH_OPEN), attributes.getMouthOpenConfidence()));
					}
					if (attributes.getGlassesConfidence() == 255) {
						showMessage("\t\tGlasses not detected");
					} else {
						showMessage(String.format("\t\tGlasses: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.GLASSES), attributes.getGlassesConfidence()));
					}
					if (attributes.getDarkGlassesConfidence() == 255) {
						showMessage("\t\tDark glasses not detected");
					} else {
						showMessage(String.format("\t\tDark glasses: %s, Confidence: %d%n", attributes.getProperties().contains(NLProperty.DARK_GLASSES), attributes.getDarkGlassesConfidence()));
					}
				}
			} catch (IOException e) {
				showMessage(e.toString());
				Log.e(TAG, "Exception", e);
			}
			showMessage("");
		}
	}

	private void printNleFeaturePoint(String name, NLFeaturePoint point) {
		if (point.confidence == 0) {
			showMessage(String.format("\t\t{0} feature unavailable. confidence: 0%n", name));
			return;
		}
		showMessage(String.format("\t\t%s feature found. X: %d, Y: %d, confidence: %d%n", name, point.x, point.y, point.confidence));
	}

	private void initializeBiometricClient(boolean additionalLicensesActivated) throws IOException {
		mBiometricClient = new NBiometricClient();
		mBiometricClient.setUseDeviceManager(true);
		mBiometricClient.setBiometricTypes(EnumSet.of(NBiometricType.FACE));

		mBiometricClient.initialize();

		if (additionalLicensesActivated) {
			// Set which features should be detected.
			mBiometricClient.setFacesDetectAllFeaturePoints(true);
			mBiometricClient.setFacesRecognizeEmotion(true);
			mBiometricClient.setFacesRecognizeExpression(true);
			mBiometricClient.setFacesDetectProperties(true);
			mBiometricClient.setFacesDetermineGender(true);
			mBiometricClient.setFacesDetermineAge(true);
		}
		// Set face template size (recommended, for enroll to database, is large) (optional).
		mBiometricClient.setFacesTemplateSize(NTemplateSize.LARGE);
	}

	private boolean validateRTSPAddress(String rtspUrl) {
		return rtspUrl != null && !rtspUrl.isEmpty();
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		NCore.setContext(this);
		setContentView(R.layout.tutorial_detect_facial_features_from_camera);

		try {
			mStatus = (TextView) findViewById(R.id.tutorials_results);
			mRtspAddress = (EditText) findViewById(R.id.tutorials_field_1);
			mRtspAddress.setHint(R.string.msg_url_for_rtsp_camera);

			mCameraButton = (Button) findViewById(R.id.tutorials_button_1);
			mCameraButton.setText(getString(R.string.msg_camera));
			mCameraButton.setOnClickListener(new View.OnClickListener() {
				@Override
				public void onClick(View v) {
					// Using first found camera
					showMessage("Starting to capture. Please look into the camera... ");
					mBiometricClient.performTask(createNewTask(), null, completionHandler);
				}
			});

			mRtspCameraButton = (Button) findViewById(R.id.tutorials_button_2);
			mRtspCameraButton.setText(getString(R.string.msg_rtsp_camera));
			mRtspCameraButton.setOnClickListener(new View.OnClickListener() {
				@Override
				public void onClick(View v) {
					if (validateRTSPAddress(mRtspAddress.getText().toString())) {
						NDevice device = connectDevice(mBiometricClient.getDeviceManager(), mRtspAddress.getText().toString(), true);
						mBiometricClient.setFaceCaptureDevice((NCamera) device);
						showMessage("Starting to capture.");
						mBiometricClient.performTask(createNewTask(), null, completionHandler);
					} else {
						showMessage(getString(R.string.msg_no_rtsp_url));
					}
				}
			});

			mVideoFileButton = (Button) findViewById(R.id.tutorials_button_3);
			mVideoFileButton.setText(getString(R.string.msg_video_file));
			mVideoFileButton.setOnClickListener(new View.OnClickListener() {
				@Override
				public void onClick(View v) {
					Intent intent = new Intent(DetectFacialFeaturesFromCamera.this, DirectoryViewer.class);
					intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
					startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
				}
			});
			enableControlls(false);

			LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSES));
		} catch (Exception e) {
			showMessage(e.toString());
			Log.e(TAG, "Exception", e);
		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_RECORD) {
			if (resultCode == RESULT_OK) {
				try {
					//TODO:
					String path;
					if (data.getData().toString().contains(ANDROID_ASSET_DESCRIPTOR)) {
						path = data.getData().toString().replace(ANDROID_ASSET_DESCRIPTOR, "");
					} else {
						path = data.getData().getPath();
					}
					NDevice device = connectDevice(mBiometricClient.getDeviceManager(), path, false);
					mBiometricClient.setFaceCaptureDevice((NCamera) device);
					showMessage("Starting to capture.");
					mBiometricClient.performTask(createNewTask(), null, completionHandler);
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
			LicensingManager.getInstance().release(Arrays.asList(LICENSES));
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}
		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
	}

}
