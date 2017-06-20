package com.neurotec.samples.devices.view;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

import com.neurotec.media.NMediaFormat;
import com.neurotec.samples.devices.R;

public class CaptureDeviceFragment extends Fragment {

	public interface MediaFormatListener {
		public void onMediaFormatChanged(NMediaFormat mediaFormat);
	}

	// ==============================================
	// Private fields
	// ==============================================

	private MediaFormatListener mListener;
	private Spinner mediaFormats;

	// ==============================================
	// Private methods
	// ==============================================

	@SuppressWarnings("unchecked")
	private ArrayAdapter<NMediaFormat> getMediaFormatAdapter() {
		return ((ArrayAdapter<NMediaFormat>) mediaFormats.getAdapter());
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		if (activity instanceof MediaFormatListener) {
			mListener = (MediaFormatListener) activity;
		} else {
			throw new ClassCastException(activity.toString() + " must implement MediaFormatListener");
		}
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_capture_device, container, false);
		mediaFormats = ((Spinner) rootView.findViewById(R.id.spinner_media_formats));
		ArrayAdapter<NMediaFormat> adapter = new ArrayAdapter<NMediaFormat>(getActivity(), android.R.layout.simple_spinner_item);
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		mediaFormats.setAdapter(adapter);
		mediaFormats.setOnItemSelectedListener(new OnItemSelectedListener() {
			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
				mListener.onMediaFormatChanged(getMediaFormatAdapter().getItem(pos));
			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
				mListener.onMediaFormatChanged(null);
			}
		});
		return rootView;
	}

	public void addMediaFormats(final NMediaFormat[] formats, final NMediaFormat currentFormat) {
		if (formats == null) throw new NullPointerException("mediaFormats");
		getActivity().runOnUiThread(new Runnable() {
			@Override
			public void run() {
				ArrayAdapter<NMediaFormat> adapter = getMediaFormatAdapter();
				adapter.clear();
				for (NMediaFormat format : formats) {
					adapter.add(format);
				}

				if (currentFormat != null) {
					mediaFormats.setSelection(adapter.getPosition(currentFormat));
				}
				adapter.notifyDataSetChanged();
			}
		});
	}
}