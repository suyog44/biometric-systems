package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

import java.util.EnumSet;

final class BorderInfo {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final int size;
	private final EnumSet<Alignment> alignment;

	// ===========================================================
	// Package private constructor
	// ===========================================================

	BorderInfo(int size, EnumSet<Alignment> alignment) {
		this.size = size;
		this.alignment = alignment;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public int getSize() {
		return size;
	}

	public EnumSet<Alignment> getAlignment() {
		return EnumSet.copyOf(alignment);
	}

}
