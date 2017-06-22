package com.neurotec.samples.abis.util;

import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class BidirectionalMap<KeyType, ValueType> {

	private final Map<KeyType, ValueType> keyToValueMap = new ConcurrentHashMap<KeyType, ValueType>();
	private final Map<ValueType, KeyType> valueToKeyMap = new ConcurrentHashMap<ValueType, KeyType>();

	public void put(KeyType key, ValueType value) {
		synchronized (this) {
			keyToValueMap.put(key, value);
			valueToKeyMap.put(value, key);
		}
	}

	public ValueType removeByKey(KeyType key) {
		synchronized (this) {
			ValueType removedValue = keyToValueMap.remove(key);
			valueToKeyMap.remove(removedValue);
			return removedValue;
		}
	}

	public KeyType removeByValue(ValueType value) {
		synchronized (this) {
			KeyType removedKey = valueToKeyMap.remove(value);
			keyToValueMap.remove(removedKey);
			return removedKey;
		}
	}

	public boolean containsKey(KeyType key) {
		return keyToValueMap.containsKey(key);
	}

	public boolean containsValue(ValueType value) {
		return valueToKeyMap.containsKey(value);
	}

	public KeyType getKey(ValueType value) {
		return valueToKeyMap.get(value);
	}

	public ValueType get(KeyType key) {
		return keyToValueMap.get(key);
	}

}
