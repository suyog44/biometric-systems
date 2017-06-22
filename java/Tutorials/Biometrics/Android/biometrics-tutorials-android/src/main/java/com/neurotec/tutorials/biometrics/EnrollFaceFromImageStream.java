package com.neurotec.tutorials.biometrics;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import java.io.File;
import java.io.FileFilter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.images.NImage;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.media.NMediaReader;
import com.neurotec.media.NMediaSource;
import com.neurotec.media.NMediaType;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.app.DirectoryViewer;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.samples.util.NImageUtils;

public class EnrollFaceFromImageStream extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = "EnrollFaceFromImageStream";
	private static final List<String> LICENSES = Arrays.asList("Biometrics.FaceExtraction");
	private static final List<String> ADDITIONAL_LICENSES = Arrays.asList("Biometrics.FaceSegmentsDetection");
	private static final int REQUEST_CODE_GET_RECORD = 1;
	private static final String IMAGES_FOLDER_PATH = "Neurotechnology/Data/biometrics-tutorials/input/faceImageCollection/";
	private static final String ANDROID_ASSET_DESCRIPTOR = "file:///android_asset/";

	private ProgressDialog mProgressDialog;
	private EditText mRTSPAddress;
	private Button mLoadFile;
	private Button mRTSPCamera;
	private Button mLoadFromDirectory;
	private TextView mStatus;

	private NBiometricClient mBiometricClient;
	private List<String> mObtainedLicenses;

	private LicensingStateCallback additionalLicenseCallback = new LicensingStateCallback() {

		@Override
		public void onLicensingStateChanged(final LicensingStateResult state) {
			final Context context = EnrollFaceFromImageStream.this;
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
							mObtainedLicenses.addAll(ADDITIONAL_LICENSES);
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
					mObtainedLicenses.addAll(LICENSES);
					LicensingManager.getInstance().obtain(EnrollFaceFromImageStream.this, additionalLicenseCallback, ADDITIONAL_LICENSES);
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
		mRTSPAddress.setEnabled(enabled);
		mLoadFile.setEnabled(enabled);
		mRTSPCamera.setEnabled(enabled);
		mLoadFromDirectory.setEnabled(enabled);
	}

	private boolean validateRTSPAddress(String rtspUrl) {
		return rtspUrl != null && !rtspUrl.isEmpty();
	}

	private void initializeBiometricClient(boolean additionalLicensesActivated) throws IOException {
		mBiometricClient = new NBiometricClient();

		//Set face template size (recommended, for enroll to database, is large) (optional).
		mBiometricClient.setFacesTemplateSize(NTemplateSize.LARGE);
		mBiometricClient.setFacesDetectAllFeaturePoints(additionalLicensesActivated);
	}

	private void startEnrolling(NMediaSource source, Uri[] files) {
		NSubject subject = new NSubject();
		NFace face = new NFace();
		face.setHasMoreSamples(true);
		subject.getFaces().add(face);

		try {
			NMediaReader reader = null;
			boolean isReaderUsed = false;
			if (source != null) {
				reader = new NMediaReader(source, EnumSet.of(NMediaType.VIDEO), true);
				isReaderUsed = true;
			} else if (files == null) {
				Throwable th = new Throwable("No source found.");
				if (th != null) {
					throw th;
				}
			}

			// Start extraction from stream.
			NBiometricStatus status = NBiometricStatus.NONE;
			NBiometricTask task = mBiometricClient.createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), subject);
			if (isReaderUsed)
				reader.start();

			NImage image = isReaderUsed ? reader.readVideoSample().getImage() : NImageUtils.fromUri(this, files[0]);

			int i = 1;
			while ((image != null) && (status == NBiometricStatus.NONE)) {
				face.setImage(image);
				mBiometricClient.performTask(task);
				Throwable th = task.getError();
				if (th != null) {
					throw th;
				}
				status = task.getStatus();
				image.dispose();

				if (!isReaderUsed && (i >= files.length)) break;

				image = isReaderUsed ? reader.readVideoSample().getImage() : NImageUtils.fromUri(this, files[i++]);
			}
			if (isReaderUsed)
				reader.stop();

			// Reset HasMoreSamples value since we finished loading images
			face.setHasMoreSamples(false);

			// If loading was finished because MeadiaReaded had no more images we have to
			// finalize extraction by performing task after setting HasMoreSamples to false.
			if (image == null) {
				mBiometricClient.performTask(task);
				if (task.getError() != null) {
					throw task.getError();
				}
				status = task.getStatus();
			}

			// Print extraction results.
			if (status == NBiometricStatus.OK) {
				// (Optional) Get face detection details if face was detected.
				for (NFace nface : subject.getFaces()) {
					for (NLAttributes attribute : nface.getObjects()) {
						showMessage("face:");
						showMessage(String.format("\tlocation = (%d, %d), width = %d, height = %d\n", attribute.getBoundingRect().left, attribute.getBoundingRect().top,
										attribute.getBoundingRect().width(), attribute.getBoundingRect().height()));

						if ((attribute.getRightEyeCenter().confidence > 0) || (attribute.getLeftEyeCenter().confidence > 0)) {
							showMessage("\tfound eyes:");
							if (attribute.getRightEyeCenter().confidence > 0) {
								showMessage(String.format("\t\tRight: location = (%d, %d), confidence = %d%n", attribute.getRightEyeCenter().x, attribute.getRightEyeCenter().y,
												attribute.getRightEyeCenter().confidence));
							}
							if (attribute.getLeftEyeCenter().confidence > 0) {
								showMessage(String.format("\t\tLeft: location = (%d, %d), confidence = %d%n", attribute.getLeftEyeCenter().x, attribute.getLeftEyeCenter().y,
												attribute.getLeftEyeCenter().confidence));
							}
						}
						if (mObtainedLicenses.contains(ADDITIONAL_LICENSES)) {
							if (attribute.getNoseTip().confidence > 0) {
								showMessage("\tfound nose:");
								showMessage(String.format("\t\tlocation = (%d, %d), confidence = %d%n", attribute.getNoseTip().x, attribute.getNoseTip().y, attribute.getNoseTip().confidence));
							}
							if (attribute.getMouthCenter().confidence > 0) {
								showMessage("\tfound mouth:");
								showMessage(String.format("\t\tlocation = (%d, %d), confidence = %d%n", attribute.getMouthCenter().x, attribute.getMouthCenter().y, attribute.getMouthCenter().confidence));
							}
						}
					}
				}
				showMessage("Template extracted.");

				// Save compressed template to file.
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "face_template.dat");
				NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());
				showMessage(getString(R.string.format_face_template_saved_to, outputFile.getAbsolutePath()));
			} else {
				showMessage("Extraction failed: " + status);
				Throwable th = task.getError();
				if (th != null) {
					throw th;
				}
			}
		} catch (Throwable th) {
			showError(th);
		} finally {
			if (face != null) {
				face.dispose();
			}
			if (subject != null) {
				subject.dispose();
			}
		}
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		NCore.setContext(this);
		setContentView(R.layout.tutorial_enroll_face_from_image_stream);

		mObtainedLicenses = new ArrayList<String>();

		mStatus = (TextView) findViewById(R.id.tutorials_results);

		mRTSPAddress = (EditText) findViewById(R.id.tutorials_field_1);
		mRTSPAddress.setHint(getString(R.string.msg_url_for_rtsp_camera));

		mRTSPCamera = (Button) findViewById(R.id.tutorials_button_1);
		mRTSPCamera.setText(getString(R.string.msg_rtsp_camera));
		mRTSPCamera.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				if (validateRTSPAddress(mRTSPAddress.getText().toString())) {
					NMediaSource source;
					try {
						source = NMediaSource.fromUrl(mRTSPAddress.getText().toString());
						startEnrolling(source, null);
					} catch (IOException e) {
						showMessage(e.toString());
						Log.e(TAG, "Exception", e);
					}
				} else {
					showMessage(getString(R.string.msg_no_rtsp_url));
				}
			}
		});
		mLoadFile = (Button) findViewById(R.id.tutorials_button_2);
		mLoadFile.setText(getString(R.string.msg_video_file));
		mLoadFile.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				Intent intent = new Intent(EnrollFaceFromImageStream.this, DirectoryViewer.class);
				intent.putExtra(DirectoryViewer.ASSET_DIRECTORY_LOCATION, BiometricsTutorialsApp.TUTORIALS_ASSETS_DIR);
				startActivityForResult(intent, REQUEST_CODE_GET_RECORD);
			}
		});
		mLoadFromDirectory = (Button) findViewById(R.id.tutorials_button_3);
		mLoadFromDirectory.setText(getString(R.string.msg_load_static_image_files));
		mLoadFromDirectory.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				// Loading static image files from given folder IMAGES_FOLDER_PATH.
				File dir = new File(Environment.getExternalStorageDirectory(), IMAGES_FOLDER_PATH);
				if (dir.isDirectory()) {
					File[] files = dir.listFiles(new FileFilter() {
						@Override
						public boolean accept(File pathname) {
							return pathname.isFile();
						}
					});
					Uri[] filesUri = new Uri[files.length];

					for (int i = 0; i < files.length; i++) {
						filesUri[i] = Uri.fromFile(files[i]);
						showMessage("File loaded: " + filesUri[i].getLastPathSegment());
					}

					startEnrolling(null, filesUri);
				} else {
					showMessage("Given path is not directory.");
					return;
				}
			}
		});
		enableControlls(false);

		LicensingManager.getInstance().obtain(this, this, LICENSES);
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		if (requestCode == REQUEST_CODE_GET_RECORD) {
			if (resultCode == RESULT_OK) {
				try {
					String path = null;
					if (data.getData().toString().contains(ANDROID_ASSET_DESCRIPTOR)) {
						path = data.getData().toString().replace(ANDROID_ASSET_DESCRIPTOR, "");
					} else {
						path = data.getData().getPath();
					}
					NMediaSource source = NMediaSource.fromFile(path);
					startEnrolling(source, null);
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
			LicensingManager.getInstance().release(mObtainedLicenses);
		} catch (IOException e) {
			Log.e(TAG, getString(R.string.msg_licenses_not_obtained), e);
		}

		if ((mProgressDialog != null) && (mProgressDialog.isShowing())) {
			mProgressDialog.dismiss();
		}
		if (mBiometricClient != null) {
			mBiometricClient.dispose();
		}
	}

}
