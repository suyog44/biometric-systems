package com.neurotec.samples.abis.util;

public interface ParameterizedRunnable<V> extends Runnable {

	ParameterizedRunnable<V> setParameter(V parameter);

}
