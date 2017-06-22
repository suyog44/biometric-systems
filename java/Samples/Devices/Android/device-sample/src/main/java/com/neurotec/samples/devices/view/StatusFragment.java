package com.neurotec.samples.devices.view;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.neurotec.samples.devices.R;

public class StatusFragment extends Fragment {

	// ==============================================
	// Private fields
	// ==============================================

	private TextView mTextViewStatus;

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_status, container, false);
		mTextViewStatus = ((TextView) rootView.findViewById(R.id.textview_status));
		return rootView;
	}

	public void setStatus(String status) {
		mTextViewStatus.setText(status);
	}
}