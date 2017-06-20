package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

import java.util.EnumSet;

final class Text {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final String value;
	private final int size;
	private final EnumSet<Alignment> alignment;
	private final boolean drawCheckbox;
	private final boolean bold;

	// ===========================================================
	// Package private constructor
	// ===========================================================

	Text(String value, int size, EnumSet<Alignment> alignment, boolean drawCheckbox, boolean bold) {
		this.value = value;
		this.size = size;
		this.alignment = alignment;
		this.drawCheckbox = drawCheckbox;
		this.bold = bold;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public String getValue() {
		return value;
	}

	public int getSize() {
		return size;
	}

	public EnumSet<Alignment> getAlignment() {
		return EnumSet.copyOf(alignment);
	}

	public boolean isDrawCheckbox() {
		return drawCheckbox;
	}

	public boolean isBold() {
		return bold;
	}

}
