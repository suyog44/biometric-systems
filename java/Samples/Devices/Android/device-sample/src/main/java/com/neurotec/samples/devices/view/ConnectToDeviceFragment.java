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
import android.widget.Button;
import android.widget.Spinner;

import com.neurotec.beans.NParameterBag;
import com.neurotec.beans.NParameterDescriptor;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.plugins.NPlugin;
import com.neurotec.plugins.NPluginState;
import com.neurotec.samples.devices.R;
import com.neurotec.util.NPropertyBag;
import com.neurotec.view.NPropertyView;

public class ConnectToDeviceFragment extends Fragment {

	public interface ConnectToDeviceListener {
		public void onConnectToDevice(NPlugin plugin, NPropertyBag propertyBag);
	}

	// ==============================================
	// Private fields
	// ==============================================

	private ConnectToDeviceListener mListener;
	private Spinner mSpinnerPlugins;
	private NPropertyView mPropertyView;
	private NParameterDescriptor[] parameters;

	// ==============================================
	// Public constructor
	// ==============================================

	public ConnectToDeviceFragment() {
	}

	// ==============================================
	// Private methods
	// ==============================================

	@SuppressWarnings("unchecked")
	private ArrayAdapter<NPlugin> getPluginsAdapter() {
		return ((ArrayAdapter<NPlugin>) mSpinnerPlugins.getAdapter());
	}

	private NPlugin getSelectedPlugin() {
		return (NPlugin) mSpinnerPlugins.getSelectedItem();
	}

	private void onPluginChanged(NPlugin plugin) {
		parameters = plugin == null ? null : NDeviceManager.getConnectToDeviceParameters(plugin);
		NParameterBag bag = new NParameterBag(parameters);
		mPropertyView.setSource(bag);
	}

	private void init() {
		getActivity().runOnUiThread(new Runnable() {
			@Override
			public void run() {
				ArrayAdapter<NPlugin> adapter = getPluginsAdapter();
				adapter.clear();
				for (NPlugin plugin : NDeviceManager.getPluginManager().getPlugins()) {
					if (plugin.getState() == NPluginState.PLUGGED && NDeviceManager.isConnectToDeviceSupported(plugin)) {
						adapter.add(plugin);
					}
				}
				if (NDeviceManager.getPluginManager().getPlugins().size() > 0) {
					mSpinnerPlugins.setSelection(0);
				}
				adapter.notifyDataSetChanged();
			}
		});
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		if (activity instanceof ConnectToDeviceListener) {
			mListener = (ConnectToDeviceListener) activity;
		} else {
			throw new ClassCastException(activity.toString() + " must implement ConnectToDeviceListener");
		}
	}

	@Override
	public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
		View view = inflater.inflate(R.layout.fragment_connect_to_device, container, false);
		mPropertyView = ((NPropertyView) view.findViewById(R.id.propertyView_plugin));
		mSpinnerPlugins = ((Spinner) view.findViewById(R.id.spinner_plugins));
		ArrayAdapter<NPlugin> adapter = new ArrayAdapter<NPlugin>(getActivity(), android.R.layout.simple_spinner_item);
		adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
		mSpinnerPlugins.setAdapter(adapter);
		mSpinnerPlugins.setOnItemSelectedListener(new OnItemSelectedListener() {
			@Override
			public void onItemSelected(AdapterView<?> parent, View view, int pos, long id) {
				onPluginChanged(getPluginsAdapter().getItem(pos));
			}

			@Override
			public void onNothingSelected(AdapterView<?> arg0) {
				onPluginChanged(null);
			}
		});
		Button buttonOk = ((Button) view.findViewById(R.id.button_ok));
		buttonOk.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				NPropertyBag propertyBag = ((NParameterBag)mPropertyView.getSource()).toPropertyBag();
				mListener.onConnectToDevice(getSelectedPlugin(), propertyBag);
			}
		});
		Button buttonCancel = ((Button) view.findViewById(R.id.button_cancel));
		buttonCancel.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				mListener.onConnectToDevice(null, null);
			}
		});
		return view;
	}

	@Override
	public void onStart() {
		super.onStart();
		init();
	}

}