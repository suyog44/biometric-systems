package com.neurotec.samples.devices.view;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;
import android.widget.ArrayAdapter;
import android.widget.Spinner;

import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.media.NMediaFormat;
import com.neurotec.samples.devices.R;

public class FScannerFragment extends Fragment {

	// ==============================================
	// Private fields
	// ==============================================

	private Spinner mPositions;
	private Spinner mImpressionTypes;

	// ==============================================
	// Private methods
	// ==============================================

	@SuppressWarnings("unchecked")
	private ArrayAdapter<NFPosition> getPositionAdapter() {
		return ((ArrayAdapter<NFPosition>) mPositions.getAdapter());
	}

	@SuppressWarnings("unchecked")
	private ArrayAdapter<NFImpressionType> getImpressionTypesAdapter() {
		return ((ArrayAdapter<NFImpressionType>) mImpressionTypes.getAdapter());
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_fscanner, container, false);
		mPositions = ((Spinner) rootView.findViewById(R.id.spinner_finger_positions));
		ArrayAdapter<NFPosition> adapterPositions = new ArrayAdapter<NFPosition>(getActivity(), android.R.layout.simple_spinner_item);
		adapterPositions.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		mPositions.setAdapter(adapterPositions);
		mPositions.setOnItemSelectedListener(new OnItemSelectedListener() {
			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
			}
		});
		mImpressionTypes = ((Spinner) rootView.findViewById(R.id.spinner_finger_impression_types));
		ArrayAdapter<NMediaFormat> adapterImpressionTypes = new ArrayAdapter<NMediaFormat>(getActivity(), android.R.layout.simple_spinner_item);
		adapterImpressionTypes.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		mImpressionTypes.setAdapter(adapterImpressionTypes);
		mImpressionTypes.setOnItemSelectedListener(new OnItemSelectedListener() {
			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
			}
		});
		return rootView;
	}

	public void setPositions(final NFPosition[] positions) {
		if (positions == null) throw new NullPointerException("positions");
		getActivity().runOnUiThread(new Runnable() {
			@Override
			public void run() {
				ArrayAdapter<NFPosition> adapter = getPositionAdapter();
				adapter.clear();
				for (NFPosition position : positions) {
					adapter.add(position);
				}
				if (positions != null) {
					mPositions.setSelection(0);
				}
				adapter.notifyDataSetChanged();
			}
		});
	}

	public NFPosition getPosition() {
		return (NFPosition) mPositions.getSelectedItem();
	}

	public void setImpressionTypes(final NFImpressionType[] impressionTypes) {
		if (impressionTypes == null) throw new NullPointerException("impressionTypes");
		getActivity().runOnUiThread(new Runnable() {
			@Override
			public void run() {
				ArrayAdapter<NFImpressionType> adapter = getImpressionTypesAdapter();
				adapter.clear();
				for (NFImpressionType position : impressionTypes) {
					adapter.add(position);
				}

				if (impressionTypes != null) {
					mImpressionTypes.setSelection(0);
				}
				adapter.notifyDataSetChanged();
			}
		});
	}

	public NFImpressionType getImpressionType() {
		return (NFImpressionType) mImpressionTypes.getSelectedItem();
	}

	public NFPosition[] getMissingPositions() {
		return new NFPosition[0];
	}

	public void setMissingPositions(NFPosition[] missingPositions) {
		throw new UnsupportedOperationException("Not yet implemented");
	}

}