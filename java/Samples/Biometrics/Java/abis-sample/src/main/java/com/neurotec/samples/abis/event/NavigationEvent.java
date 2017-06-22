package com.neurotec.samples.abis.event;

import java.util.EventObject;

public class NavigationEvent<T, U> extends EventObject {

	private static final long serialVersionUID = 1L;

	private final U destination;

	public NavigationEvent(T source, U destination) {
		super(source);
		this.destination = destination;
	}

	public U getDestination() {
		return destination;
	}

}
