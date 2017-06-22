package com.neurotec.samples.abis.event;

import com.neurotec.samples.abis.tabbedview.Tab;

import java.util.EventListener;

public interface TabNavigationListener extends EventListener {

	void tabAdded(NavigationEvent<? extends Object, Tab> ev);
	void tabEnter(NavigationEvent<? extends Object, Tab> ev);
	void tabLeave(NavigationEvent<? extends Object, Tab> ev);
	void tabClose(NavigationEvent<? extends Object, Tab> ev);

}
