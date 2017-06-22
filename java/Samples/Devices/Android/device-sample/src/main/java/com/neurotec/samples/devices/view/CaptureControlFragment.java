package com.neurotec.samples.devices.view;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.neurotec.samples.devices.R;

public class CaptureControlFragment extends Fragment {

	public interface CaptureControlListener {
		public void onStartCapturing();
		public void onStopCapturing();
	}

	// ==============================================
	// Private fields
	// ==============================================

	private CaptureControlListener mListener;
	private Button buttonStartCapture;
	private Button buttonStopCapture;

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		if (activity instanceof CaptureControlListener) {
			mListener = (CaptureControlListener) activity;
		} else {
			throw new ClassCastException(activity.toString() + " must implement CaptureControlListener");
		}
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.capture_buttons, container, false);
		buttonStartCapture = ((Button) rootView.findViewById(R.id.button_start_capturing));
		buttonStartCapture.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mListener.onStartCapturing();
			}
		});
		buttonStopCapture = ((Button) rootView.findViewById(R.id.button_stop_capturing));
		buttonStopCapture.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mListener.onStopCapturing();
			}
		});
		return rootView;
	}
}