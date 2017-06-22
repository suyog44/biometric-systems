package com.neurotec.samples.abis.util;

import java.util.HashMap;
import java.util.Map;

public class ClassInstanceContainer {

	private final Map<Class<?>, Object> map;

	public ClassInstanceContainer() {
		map = new HashMap<Class<?>, Object>();
	}

	public <T> void put(Class<T> type, T instance) {
		if (type == null) {
			throw new NullPointerException("type");
		}
		map.put(type, instance);
	}

	public <T> T get(Class<T> type) {
		return type.cast(map.get(type));
	}

	public void clear() {
		map.clear();
	}

}
