package com.neurotec.samples.abis.event;

import java.util.EventListener;

public interface NodeChangeListener extends EventListener {

	void nodeAdded(NodeChangeEvent ev);
	void nodeRemoved(NodeChangeEvent ev);

}
