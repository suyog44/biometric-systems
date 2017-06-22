package com.neurotec.samples.devices;

import java.util.EnumSet;

import android.app.Activity;
import android.app.FragmentTransaction;
import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.NavUtils;
import android.util.Log;
import android.view.MenuItem;
import android.widget.Toast;

import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceType;
import com.neurotec.plugins.NPlugin;
import com.neurotec.samples.devices.view.ConnectToDeviceFragment;
import com.neurotec.samples.devices.view.ConnectToDeviceFragment.ConnectToDeviceListener;
import com.neurotec.samples.devices.view.DeviceManagerFragment;
import com.neurotec.samples.devices.view.DeviceManagerFragment.DeviceManagerListener;
import com.neurotec.samples.devices.view.DevicesFragment;
import com.neurotec.util.NPropertyBag;

public class MainActivity extends Activity implements DevicesFragment.DeviceListListener, DeviceManagerListener, ConnectToDeviceListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String TAG = MainActivity.class.getSimpleName();
	private static final String FRAGMENT_DEVICES = "fragment-devices";

	// ==============================================
	// Private methods
	// ==============================================

	private void showDevicesList() {
		FragmentTransaction transition = getFragmentManager().beginTransaction();
		transition.replace(R.id.fragment_container, new DevicesFragment(), FRAGMENT_DEVICES);
		transition.addToBackStack(null);
		transition.commit();
	}

	private void showError(final String message) {
		runOnUiThread(new Runnable() {
			@Override
			public void run() {
				Toast.makeText(getApplicationContext(), message, Toast.LENGTH_LONG).show();
			}
		});
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.main);
		showDevicesList();
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case android.R.id.home:
			NavUtils.navigateUpTo(this, new Intent(this, MainActivity.class));
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	@Override
	public void onDeviceSelected(NDevice device) {
		String id = device.getId();
		Class<?> fragment = null;
		if (device.getDeviceType().contains(NDeviceType.CAMERA)) {
			fragment = CameraActivity.class;
		} else if (device.getDeviceType().contains(NDeviceType.MICROPHONE)) {
			fragment = MicrophoneActivity.class;
		} else if (device.getDeviceType().contains(NDeviceType.FSCANNER)) {
			fragment = FScannerActivity.class;
		} else if (device.getDeviceType().contains(NDeviceType.IRIS_SCANNER)) {
			fragment = IrisScannerActivity.class;
		}
		Intent intent = new Intent(this, fragment);
		intent.putExtra(CaptureActivity.ARG_DEVICE_ID, id);
		startActivity(intent);
	}

	@Override
	public void onCreateDeviceManager(EnumSet<NDeviceType> deviceTypes) {
		getFragmentManager().popBackStackImmediate();
		DeviceManagerHelper.initialize(deviceTypes);
		showDevicesList();
	}

	@Override
	public void onConnectToDevice(NPlugin plugin, NPropertyBag propertyBag) {
		getFragmentManager().popBackStackImmediate();
		try {
			if (plugin != null && propertyBag != null) {
				DeviceManagerHelper.getDeviceManager().connectToDevice(plugin, propertyBag);
			}
		} catch (Throwable e) {
			Log.e(TAG, "", e);
			showError(e.toString());
		}
	}

	@Override
	public void onCreateDeviceManager() {
		FragmentTransaction transition = getFragmentManager().beginTransaction();
		transition.replace(R.id.fragment_container, new DeviceManagerFragment());
		transition.addToBackStack(null);
		transition.commit();
	}

	@Override
	public void onConnectToDevice() {
		FragmentTransaction transition = getFragmentManager().beginTransaction();
		transition.replace(R.id.fragment_container, new ConnectToDeviceFragment());
		transition.addToBackStack(null);
		transition.commit();
	}
}
