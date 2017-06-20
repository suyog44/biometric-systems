package com.neurotec.samples.multibiometric.irises;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.preference.PreferenceManager;
import android.util.Log;
import android.widget.CompoundButton;
import android.widget.LinearLayout;
import android.widget.Switch;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.view.NIrisView;
import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.devices.NIrisScanner;
import com.neurotec.images.NImage;
import com.neurotec.samples.licensing.LicensingManager;
import com.neurotec.samples.multibiometric.BiometricActivity;
import com.neurotec.samples.multibiometric.R;
import com.neurotec.samples.multibiometric.irises.preference.IrisPreferences;
import com.neurotec.samples.util.NImageUtils;

public class IrisActivity extends BiometricActivity {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final String MODALITY_ASSET_DIRECTORY = "irises";
	private static final String TAG = "IrisActivity";

	// ===========================================================
	// Private fields
	// ===========================================================

	private NIrisView mIrisView;
	private Bitmap mDefaultBitmap;

	// ===========================================================
	// Private methods
	// ===========================================================

	private NIrisScanner getScanner() {
		for (NDevice device : client.getDeviceManager().getDevices()) {
			if (device.getDeviceType().contains(NDeviceType.IRIS_SCANNER)) {
				return (NIrisScanner) device;
			}
		}
		return null;
	}

	private NSubject createSubjectFromImage(Uri uri) {
		NSubject subject = null;
		try {
			NImage image = NImageUtils.fromUri(this, uri);
			subject = new NSubject();
			NIris iris = new NIris();
			iris.setImage(image);
			subject.getIrises().add(iris);
		} catch (Exception e){
			Log.i(TAG, "Failed to load file as NImage");
		}
		return subject;
	}

	private NSubject createSubjectFromFile(Uri uri) {
		NSubject subject = null;
		try {
			subject = NSubject.fromFile(uri.toString());
		} catch (IOException e) {
			Log.i(TAG, "Failed to load from file");
		}
		return subject;
	}

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		try {
			PreferenceManager.setDefaultValues(this, R.xml.iris_preferences, false);
			LinearLayout layout = ((LinearLayout) findViewById(R.id.biometric_layout));
			

			mIrisView = new NIrisView(this);
			layout.addView(mIrisView);
			mDefaultBitmap = BitmapFactory.decodeResource(getResources(), R.drawable.iris);
			if (savedInstanceState == null) {
				NIris iris = new NIris();
				iris.setImage(NImage.fromBitmap(mDefaultBitmap));
				mIrisView.setIris(iris);
			}
		} catch (Exception e) {
			showError(e);
		}
	}

	@Override
	protected List<String> getComponents() {
		return Arrays.asList(LicensingManager.LICENSE_IRIS_DETECTION,
			LicensingManager.LICENSE_IRIS_EXTRACTION,
			LicensingManager.LICENSE_IRIS_MATCHING,
			LicensingManager.LICENSE_IRIS_MATCHING_FAST,
			LicensingManager.LICENSE_IRIS_STANDARDS);
	}

	@Override
	protected List<String> getMandatoryComponents() {
		return Arrays.asList(LicensingManager.LICENSE_IRIS_DETECTION,
				LicensingManager.LICENSE_IRIS_EXTRACTION,
				LicensingManager.LICENSE_IRIS_MATCHING);
	}

	@Override
	protected Class<?> getPreferences() {
		return IrisPreferences.class;
	}

	@Override
	protected void updatePreferences(NBiometricClient client) {
		IrisPreferences.updateClient(client, this);
	}

	@Override
	protected boolean isCheckForDuplicates() {
		return IrisPreferences.isCheckForDuplicates(this);
	}

	@Override
	protected String getModalityAssetDirectory() {
		return MODALITY_ASSET_DIRECTORY;
	}

	@Override
	protected void onFileSelected(Uri uri) throws Exception {
		NSubject subject = null;

		subject = createSubjectFromImage(uri);

		if (subject == null) {
			subject = createSubjectFromFile(uri);
		}

		if (subject != null) {
			if (!subject.getIrises().isEmpty()) {
				mIrisView.setIris(subject.getIrises().get(0));
			}
			extract(subject);
		} else {
			showInfo("File did not contain valid information for subject");
		}
	}

	@Override
	protected void onStartCapturing() {
		NIrisScanner scanner = getScanner();
		if (scanner == null) {
			showError(R.string.msg_capturing_device_is_unavailable);
		} else {
			client.setIrisScanner(scanner);
			NSubject subject = new NSubject();
			NIris iris = new NIris();
			iris.addPropertyChangeListener(biometricPropertyChanged);
			mIrisView.setIris(iris);
			subject.getIrises().add(iris);
			capture(subject, null);
		}
	}
}
