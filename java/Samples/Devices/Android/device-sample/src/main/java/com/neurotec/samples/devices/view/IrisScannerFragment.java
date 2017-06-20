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

import com.neurotec.biometrics.NEPosition;
import com.neurotec.samples.devices.R;

public class IrisScannerFragment extends Fragment {

	// ==============================================
	// Private fields
	// ==============================================

	private Spinner mPositions;

	// ==============================================
	// Private methods
	// ==============================================

	@SuppressWarnings("unchecked")
	private ArrayAdapter<NEPosition> getPositionAdapter() {
		return ((ArrayAdapter<NEPosition>) mPositions.getAdapter());
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View rootView = inflater.inflate(R.layout.fragment_irisscanner, container, false);
		mPositions = ((Spinner) rootView.findViewById(R.id.spinner_iris_positions));
		ArrayAdapter<NEPosition> adapterPositions = new ArrayAdapter<NEPosition>(getActivity(), android.R.layout.simple_spinner_item);
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
		return rootView;
	}

	public void setPositions(final NEPosition[] positions) {
		if (positions == null) throw new NullPointerException("positions");
		getActivity().runOnUiThread(new Runnable() {
			@Override
			public void run() {
				ArrayAdapter<NEPosition> adapter = getPositionAdapter();
				adapter.clear();
				for (NEPosition position : positions) {
					adapter.add(position);
				}
				if (positions != null) {
					mPositions.setSelection(0);
				}
				adapter.notifyDataSetChanged();
			}
		});
	}

	public NEPosition getPosition() {
		return (NEPosition) mPositions.getSelectedItem();
	}

	public NEPosition[] getMissingPositions() {
		return new NEPosition[0];
	}

	public void setMissingPositions(NEPosition[] missingPositions) {
		throw new UnsupportedOperationException("Not yet implemented");
	}

}