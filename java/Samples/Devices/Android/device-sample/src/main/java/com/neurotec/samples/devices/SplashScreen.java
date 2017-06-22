package com.neurotec.samples.devices;

import android.content.Intent;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.widget.TextView;
import android.widget.Toast;

import java.util.Arrays;

import com.neurotec.lang.NCore;
import com.neurotec.samples.app.BaseActivity;
import com.neurotec.samples.licensing.LicensingManager;

public final class SplashScreen extends BaseActivity {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = SplashScreen.class.getSimpleName();

	// ==============================================
	// Private fields
	// ==============================================

	private TextView mProgress;

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		NCore.setContext(this);
		setContentView(R.layout.splash);
		mProgress = (TextView) findViewById(R.id.textView_progress);
		new LicenseLoader().execute();
	}

	private class LicenseLoader extends AsyncTask<Void, String, Exception> {
		@Override
		protected void onPreExecute() {
			super.onPreExecute();
		}

		@Override
		protected Exception doInBackground(Void... arg0) {
			Exception ex = null;
			try {
				publishProgress(getString(R.string.msg_obtaining_licenses));
				LicensingManager.getInstance().obtain(getApplicationContext(), Arrays.asList("Devices.Cameras", "Devices.FingerScanners", "Devices.IrisScanners", "Devices.Microphones"));
				publishProgress(getString(R.string.msg_initializing));
				DeviceManagerHelper.initialize();
				return null;
			} catch (Exception e) {
				ex = e;
				Log.e(TAG, "", e);
			}
			return ex;
		}

		@Override
		protected void onProgressUpdate(String... values) {
			super.onProgressUpdate(values);
			for (String value : values) {
				mProgress.setText(value);
			}
		}

		@Override
		protected void onPostExecute(Exception result) {
			if (result == null) {
				startActivity(new Intent(SplashScreen.this, MainActivity.class));
				finish();
			} else {
				showError(result.getMessage(), true);
				Toast.makeText(getApplicationContext(), R.string.msg_licenses_not_obtained, Toast.LENGTH_LONG).show();
			}
		}
	}
}