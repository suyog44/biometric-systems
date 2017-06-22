package com.neurotec.samples.abis.tabbedview;

public interface TabController {

	Tab showTab(Class<? extends Tab> type, boolean alwaysCreateNew, boolean closeable, Object model);
	void closeTab(Tab tab);

}
