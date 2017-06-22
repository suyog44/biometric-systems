package com.neurotec.tutorials.biometrics;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NTemplateSize;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceManager.DeviceCollection;
import com.neurotec.devices.NDeviceType;
import com.neurotec.io.NFile;
import com.neurotec.lang.NCore;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.licensing.LicensingManager.LicensingStateCallback;
import com.neurotec.samples.licensing.LicensingStateResult;
import com.neurotec.util.concurrent.CompletionHandler;

public final class EnrollFingerFromScanner extends BaseActivity implements LicensingStateCallback {

	private static final String TAG = EnrollFingerFromScanner.class.getSimpleName();

	private static final String[] LICENSES = {LicensingManager.LICENSE_FINGER_EXTRACTION, LicensingManager.LICENSE_FINGER_DEVICES_SCANNERS};

	private TextView mResult;
	private ProgressDialog mProgressDialog;
	private NBiometricClient mBiometricClient;

	private CompletionHandler<NBiometricStatus, NSubject> completionHandler = new CompletionHandler<NBiometricStatus, NSubject>() {
		@Override
		public void completed(NBiometricStatus result, NSubject subject) {
			if (result == NBiometricStatus.OK) {
				showMessage(getString(R.string.msg_template_created));
				// Save base image to file
				File outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "finger-from-scanner.png");
				try {
					subject.getFingers().get(0).getImage().save(outputFile.getAbsolutePath());
					showMessage(getString(R.string.format_finger_image_saved_to, outputFile.getAbsolutePath()));
				} catch (IOException e) {}

				// Save template to file
				outputFile = new File(BiometricsTutorialsApp.TUTORIALS_OUTPUT_DATA_DIR, "finger-template-from-scanner.dat");
				try {
					NFile.writeAllBytes(outputFile.getAbsolutePath(), subject.getTemplateBuffer());
					showMessage(getString(R.string.format_finger_record_saved_to, outputFile.getAbsolutePath()));
				} catch (IOException e) {
				}
			} else {
				showMessage(getString(R.string.format_extraction_failed, result));
			}
		}

		@Override
		public void failed(Throwable exc, NSubject subject) {exc.printStackTrace();
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
					Log.i(TAG, getString(R.string.format_obtaining_licenses, Arrays.asList(LICENSES)));
					mProgressDialog = ProgressDialog.show(context, "", getString(R.string.msg_obtaining_licenses));
					break;
				case OBTAINED:
					mProgressDialog.dismiss();
					showMessage(getString(R.string.msg_licenses_obtained));
					capture();
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
		NCore.setContext(this);
		setContentView(R.layout.tutorial_enroll_finger_from_scanner);
		mResult = (TextView) findViewById(R.id.tutorials_results);
		LicensingManager.getInstance().obtain(this, this, Arrays.asList(LICENSES));
	}

	@Override
	protected void onDestroy() {
		super.onDestroy();
		if (mBiometricClient != null) {
			mBiometricClient.cancel();
			mBiometricClient.dispose();
			mBiometricClient = null;
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

	private void showMessage(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				mResult.append(message + "\n");
			}
		});
	}

	private void capture() {
		try {
			if (!LicensingManager.isActivated(LicensingManager.LICENSE_FINGER_DEVICES_SCANNERS)) {
				showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FINGER_DEVICES_SCANNERS));
				return; // The following operation is not activated, so return
			}
			if (!LicensingManager.isFingerExtractionActivated()) {
				showMessage(getString(R.string.format_not_activated, LicensingManager.LICENSE_FINGER_EXTRACTION));
				return; // The following operation is not activated, so return
			}
			mBiometricClient = new NBiometricClient();
			NSubject subject = new NSubject();
			NFinger finger = new NFinger();
	
			mBiometricClient.setUseDeviceManager(true);
			NDeviceManager deviceManager = mBiometricClient.getDeviceManager();
			deviceManager.setDeviceTypes(EnumSet.of(NDeviceType.FINGER_SCANNER));
			mBiometricClient.initialize();
	
			DeviceCollection devices = deviceManager.getDevices();
			if (devices.size() > 0) {
				System.out.format("Found %d fingerprint scanner\n", devices.size());
			} else {
				showMessage(getString(R.string.msg_no_scanners_found));
				return;
			}
	
			showMessage(getString(R.string.format_capturing_from_device_put_finger, mBiometricClient.getFingerScanner().getDisplayName()));
	
			subject.getFingers().add(finger);
	
			showMessage("Capturing....");
			mBiometricClient.setFingersTemplateSize(NTemplateSize.LARGE);
			mBiometricClient.createTemplate(subject, subject, completionHandler);
		} catch (Exception ex) {
			showError(ex);
		}
	}
}
