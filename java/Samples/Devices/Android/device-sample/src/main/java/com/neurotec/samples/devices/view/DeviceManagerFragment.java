package com.neurotec.samples.devices.view;

import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;

import com.neurotec.devices.NDeviceType;
import com.neurotec.samples.devices.R;

public class DeviceManagerFragment extends Fragment {

	public interface DeviceManagerListener {
		public void onCreateDeviceManager(EnumSet<NDeviceType> deviceTypes);
	}

	// ==============================================
	// Private fields
	// ==============================================

	private DeviceManagerListener mListener;
	private List<CheckBox> checkboxes = new ArrayList<CheckBox>();
	private CheckBox chkAny;
	private CheckBox chkCaptureDevice;
	private CheckBox chkCamera;
	private CheckBox chkMicrophone;
	private CheckBox chkBiometricDevice;
	private CheckBox chkFScanner;
	private CheckBox chkFingerScanner;
	private CheckBox chkPalmScanner;
	private CheckBox chkIrisScanner;

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		if (activity instanceof DeviceManagerListener) {
			mListener = (DeviceManagerListener) activity;
		} else {
			throw new ClassCastException(activity.toString() + " must implement ConnectToDeviceListener");
		}
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setHasOptionsMenu(false);
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_device_manager, container, false);
		chkAny = ((CheckBox) rootView.findViewById(R.id.checkBox_any));
		chkAny.setTag(NDeviceType.ANY);
		checkboxes.add(chkAny);
		chkCaptureDevice = ((CheckBox) rootView.findViewById(R.id.checkBox_capture_device));
		chkCaptureDevice.setTag(NDeviceType.CAPTURE_DEVICE);
		checkboxes.add(chkCaptureDevice);
		chkCamera = ((CheckBox) rootView.findViewById(R.id.checkBox_camera));
		chkCamera.setTag(NDeviceType.CAMERA);
		checkboxes.add(chkCamera);
		chkMicrophone = ((CheckBox) rootView.findViewById(R.id.checkBox_microphone));
		chkMicrophone.setTag(NDeviceType.MICROPHONE);
		checkboxes.add(chkMicrophone);
		chkBiometricDevice = ((CheckBox) rootView.findViewById(R.id.checkBox_biometric_device));
		chkBiometricDevice.setTag(NDeviceType.BIOMETRIC_DEVICE);
		checkboxes.add(chkBiometricDevice);
		chkFScanner = ((CheckBox) rootView.findViewById(R.id.checkBox_fscanner));
		chkFScanner.setTag(NDeviceType.FSCANNER);
		checkboxes.add(chkFScanner);
		chkFingerScanner = ((CheckBox) rootView.findViewById(R.id.checkBox_finger_scanner));
		chkFingerScanner.setTag(NDeviceType.FINGER_SCANNER);
		checkboxes.add(chkFingerScanner);
		chkPalmScanner = ((CheckBox) rootView.findViewById(R.id.checkBox_palm_scanner));
		chkPalmScanner.setTag(NDeviceType.PALM_SCANNER);
		checkboxes.add(chkPalmScanner);
		chkIrisScanner = ((CheckBox) rootView.findViewById(R.id.checkBox_iris_scanner));
		chkIrisScanner.setTag(NDeviceType.IRIS_SCANNER);
		checkboxes.add(chkIrisScanner);
		Button buttonOk = ((Button) rootView.findViewById(R.id.button_ok));
		buttonOk.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mListener.onCreateDeviceManager(getDeviceTypes());
			}
		});
		Button buttonCancel = ((Button) rootView.findViewById(R.id.button_cancel));
		buttonCancel.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mListener.onCreateDeviceManager(null);
			}
		});
		return rootView;
	}

	public EnumSet<NDeviceType> getDeviceTypes() {
		List<NDeviceType> types = new ArrayList<NDeviceType>();
		for (CheckBox checkBox : checkboxes) {
			if (checkBox.isChecked()) {
				types.add((NDeviceType) checkBox.getTag());
			}
		}
		EnumSet<NDeviceType> deviceTypes = EnumSet.noneOf(NDeviceType.class);
		deviceTypes.addAll(types);
		return deviceTypes;
	}
}