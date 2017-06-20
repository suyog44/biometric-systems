package com.neurotec.samples.devices.view;

import android.app.DialogFragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.CompoundButton.OnCheckedChangeListener;
import android.widget.EditText;

import com.neurotec.samples.devices.R;

public class BiometricDeviceFragment extends DialogFragment {

	// ==============================================
	// Private fields
	// ==============================================

	private CheckBox checkBoxAutomatic;
	private CheckBox checkBoxUseTimeout;
	private EditText editTextTimeout;

	// ==============================================
	// Private methods
	// ==============================================
	private void onUseTimeoutChanged() {
		editTextTimeout.setEnabled(checkBoxUseTimeout.isChecked());
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_biometric_device, container, false);
		editTextTimeout = ((EditText) rootView.findViewById(R.id.editText_timeout));
		checkBoxAutomatic = ((CheckBox) rootView.findViewById(R.id.checkBox_automatic));
		checkBoxUseTimeout = ((CheckBox) rootView.findViewById(R.id.checkBox_use_timeout));
		checkBoxUseTimeout.setOnCheckedChangeListener(new OnCheckedChangeListener() {
			@Override
			public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
				onUseTimeoutChanged();
			}

		});
		return rootView;
	}

	@Override
	public void onStart() {
		super.onStart();
		onUseTimeoutChanged();
	}

	public boolean isAutomatic() {
		return checkBoxAutomatic.isChecked();
	}

	public int getTimeout() {
		return checkBoxUseTimeout.isChecked() ? Integer.valueOf(editTextTimeout.getText().toString()) : -1;
	}
}