package com.neurotec.samples.abis;

import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;

public enum LocalOperationsOption {

	NONE("None", EnumSet.noneOf(NBiometricOperation.class)),
	DETECT("Detect", EnumSet.of(NBiometricOperation.DETECT)),
	DETECT_TO_DETECT_SEGMENTS("Detect - Detect segments", EnumSet.of(NBiometricOperation.DETECT,
																	 NBiometricOperation.DETECT_SEGMENTS)),
	DETECT_TO_SEGMENT("Detect - Segment", EnumSet.of(NBiometricOperation.DETECT,
													 NBiometricOperation.DETECT_SEGMENTS,
													 NBiometricOperation.SEGMENT)),
	DETECT_TO_ASSESS_QUALITY("Detect - Assess quality", EnumSet.of(NBiometricOperation.DETECT,
																   NBiometricOperation.DETECT_SEGMENTS,
																   NBiometricOperation.SEGMENT,
																   NBiometricOperation.ASSESS_QUALITY)),
	ALL("All", EnumSet.of(NBiometricOperation.DETECT,
						  NBiometricOperation.DETECT_SEGMENTS,
						  NBiometricOperation.SEGMENT,
						  NBiometricOperation.ASSESS_QUALITY,
						  NBiometricOperation.CREATE_TEMPLATE));

	private final String name;
	private final EnumSet<NBiometricOperation> operations;

	private LocalOperationsOption(String name, EnumSet<NBiometricOperation> operations) {
		this.name = name;
		this.operations = operations;
	}

	public EnumSet<NBiometricOperation> getOperations() {
		return operations.clone();
	}

	@Override
	public String toString() {
		return name;
	}

}
