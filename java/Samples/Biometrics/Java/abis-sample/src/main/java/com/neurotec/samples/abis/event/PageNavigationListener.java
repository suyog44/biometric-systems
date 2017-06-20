package com.neurotec.samples.abis.event;

import com.neurotec.samples.abis.tabbedview.Page;

import java.util.EventListener;

public interface PageNavigationListener extends EventListener {

	void navigatedTo(NavigationEvent<? extends Object, Page> ev);
	void navigatingFrom(NavigationEvent<? extends Object, Page> ev);

}
