package com.neurotec.samples.devices.view;

import android.app.Activity;
import android.app.ListFragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;

import com.neurotec.devices.NDevice;
import com.neurotec.devices.NDeviceManager;
import com.neurotec.devices.NDeviceManager.DeviceCollection;
import com.neurotec.licensing.gui.ActivationActivity;
import com.neurotec.samples.devices.DeviceManagerHelper;
import com.neurotec.samples.devices.R;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

public final class DevicesFragment extends ListFragment {

	// ==============================================
	// Interface
	// ==============================================

	public interface DeviceListListener {
		public void onDeviceSelected(NDevice device);
		public void onCreateDeviceManager();
		public void onConnectToDevice();

	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final String STATE_ACTIVATED_POSITION = "activated_position";

	// ==============================================
	// Private fields
	// ==============================================

	private DeviceListListener mListener;
	private int mActivatedPosition = ListView.INVALID_POSITION;
	private DeviceCollection mDevices;

	private NCollectionChangeListener devicesCollectionChangeListener = new NCollectionChangeListener() {
		public void collectionChanged(final NCollectionChangeEvent event) {
			getActivity().runOnUiThread(new Runnable() {
				public void run() {
					update();
				}
			});
		}
	};

	// ==============================================
	// Public constructor
	// ==============================================

	public DevicesFragment() {
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void setActivatedPosition(int position) {
		if (position == ListView.INVALID_POSITION) {
			getListView().setItemChecked(mActivatedPosition, false);
		} else {
			getListView().setItemChecked(position, true);
		}
		mActivatedPosition = position;
	}


	@SuppressWarnings("unchecked")
	private void update() {
		((ArrayAdapter<NDevice>)getListAdapter()).notifyDataSetChanged();
	}

	// ==============================================
	// Public methods
	// ==============================================

	@Override
	public void onViewCreated(View view, Bundle savedInstanceState) {
		super.onViewCreated(view, savedInstanceState);
		if (savedInstanceState != null && savedInstanceState.containsKey(STATE_ACTIVATED_POSITION)) {
			setActivatedPosition(savedInstanceState.getInt(STATE_ACTIVATED_POSITION));
		}
	}

	@Override
	public void onAttach(Activity activity) {
		super.onAttach(activity);
		if (!(activity instanceof DeviceListListener)) {
			throw new IllegalStateException("Activity must implement fragment's callbacks.");
		}

		mListener = (DeviceListListener) activity;
	}

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setHasOptionsMenu(true);
		NDeviceManager dm = DeviceManagerHelper.getDeviceManager();
		mDevices = dm.getDevices();
		mDevices.addCollectionChangeListener(devicesCollectionChangeListener);
		setListAdapter(new ArrayAdapter<NDevice>(getActivity(), android.R.layout.simple_list_item_activated_1, android.R.id.text1, mDevices));
	}

	@Override
	public void onDestroy() {
		mDevices.removeCollectionChangeListener(devicesCollectionChangeListener);
		super.onDestroy();
	}

	@Override
	public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
		super.onCreateOptionsMenu(menu, inflater);
		inflater.inflate(R.menu.options_menu, menu);
	}
	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
		case R.id.action_new_device_manager:
			mListener.onCreateDeviceManager();
			return true;
		case R.id.action_connect:
			mListener.onConnectToDevice();
			return true;
		case R.id.action_activate:
			startActivity(new Intent(getActivity(), ActivationActivity.class));
			return true;
		}
		return super.onOptionsItemSelected(item);
	}

	@Override
	public void onListItemClick(ListView listView, View view, int position, long id) {
		super.onListItemClick(listView, view, position, id);
		mListener.onDeviceSelected(mDevices.get(position));
	}

	@Override
	public void onSaveInstanceState(Bundle outState) {
		super.onSaveInstanceState(outState);
		if (mActivatedPosition != ListView.INVALID_POSITION) {
			outState.putInt(STATE_ACTIVATED_POSITION, mActivatedPosition);
		}
	}

	public void setActivateOnItemClick(boolean activateOnItemClick) {
		getListView().setChoiceMode(activateOnItemClick ? ListView.CHOICE_MODE_SINGLE : ListView.CHOICE_MODE_NONE);
	}
}
