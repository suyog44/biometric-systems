package com.neurotec.samples.abis.util;

import java.util.List;

public final class CollectionUtils {

	public static <T> T getFirst(List<T> list) {
		return list == null || list.isEmpty() ? null : list.get(0);
	}

	public static <T> T getLast(List<T> list) {
		return list == null || list.isEmpty() ? null : list.get(list.size() - 1);
	}

}
