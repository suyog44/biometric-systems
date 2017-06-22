package com.neurotec.tutorials.biometrics;

import android.app.ListActivity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.ListView;
import android.widget.SimpleAdapter;

import java.text.Collator;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.neurotec.lang.NCore;
import com.neurotec.licensing.gui.ActivationActivity;
import com.neurotec.plugins.NDataFileManager;

public class BiometricsTutorials extends ListActivity {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final Comparator<Map<String, Object>> DISPLAY_NAME_COMPARATOR = new Comparator<Map<String, Object>>() {
		private final Collator collator = Collator.getInstance();
		@Override
		public int compare(Map<String, Object> map1, Map<String, Object> map2) {
			return collator.compare(map1.get(KEY_TITLE), map2.get(KEY_TITLE));
		}
	};
	private static final String KEY_TITLE = "title";
	private static final String KEY_INTENT = "intent";

	// ===========================================================
	// Public static fields
	// ===========================================================

	public static final String CATEGORY_NEUROTEC_TUTORIAL = BiometricsTutorials.class.getPackage().getName() + ".CATEGORY_NEUROTEC_TUTORIAL";

	// ===========================================================
	// Private methods
	// ===========================================================

	private List<Map<String, Object>> getData() {
		List<Map<String, Object>> myData = new ArrayList<Map<String, Object>>();

		Intent mainIntent = new Intent(Intent.ACTION_MAIN, null);
		mainIntent.addCategory(CATEGORY_NEUROTEC_TUTORIAL);

		PackageManager pm = getPackageManager();
		List<ResolveInfo> list = pm.queryIntentActivities(mainIntent, 0);

		if (null == list) {
			return myData;
		}

		int len = list.size();

		for (int i = 0; i < len; i++) {
			ResolveInfo info = list.get(i);
			CharSequence labelSeq = info.loadLabel(pm);
			String label;
			if (labelSeq == null) {
				label = info.activityInfo.name;
			} else {
				label = labelSeq.toString();
			}
			addItem(myData, label, activityIntent(info.activityInfo.applicationInfo.packageName, info.activityInfo.name));
		}

		Collections.sort(myData, DISPLAY_NAME_COMPARATOR);

		return myData;
	}

	private Intent activityIntent(String pkg, String componentName) {
		Intent result = new Intent();
		result.setClassName(pkg, componentName);
		return result;
	}

	private void addItem(List<Map<String, Object>> data, String name, Intent intent) {
		Map<String, Object> temp = new HashMap<String, Object>();
		temp.put(KEY_TITLE, name);
		temp.put(KEY_INTENT, intent);
		data.add(temp);
	}

	// ===========================================================
	// Activity events
	// ===========================================================

	@Override
	public void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		NCore.setContext(this);
		setListAdapter(new SimpleAdapter(this, getData(), android.R.layout.simple_list_item_1, new String[]{KEY_TITLE}, new int[]{android.R.id.text1}));
		getListView().setTextFilterEnabled(true);
		NDataFileManager.getInstance().addFromDirectory("data", false);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		MenuInflater inflater = getMenuInflater();
		inflater.inflate(R.menu.options_menu, menu);
		return true;
	}

	@Override
	public boolean onOptionsItemSelected(MenuItem item) {
		switch (item.getItemId()) {
			case R.id.action_activation: {
				startActivity(new Intent(this, ActivationActivity.class));
				break;
			}
		}
		return true;
	}

	// ===========================================================
	// List events
	// ===========================================================

	@SuppressWarnings("unchecked")
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		Map<String, Object> map = (Map<String, Object>) l.getItemAtPosition(position);
		Intent intent = (Intent) map.get(KEY_INTENT);
		startActivity(intent);
	}

}
