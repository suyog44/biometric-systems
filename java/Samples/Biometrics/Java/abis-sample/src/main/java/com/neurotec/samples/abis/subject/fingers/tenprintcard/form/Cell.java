package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

import java.util.ArrayList;
import java.util.List;

class Cell {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final int width;
	private final int height;
	private final boolean drawRect;
	private final int fingerNumber;
	private final List<Text> textLines;

	// ===========================================================
	// Package private constructor
	// ===========================================================

	Cell(int width, int height, boolean drawRect, int fingerNumber) {
		this.width = width;
		this.height = height;
		this.drawRect = drawRect;
		this.fingerNumber = fingerNumber;
		this.textLines = new ArrayList<Text>();
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public int getWidth() {
		return width;
	}

	public int getHeight() {
		return height;
	}

	public boolean isDrawRect() {
		return drawRect;
	}

	public int getFingerNumber() {
		return fingerNumber;
	}

	public List<Text> getTextLines() {
		return textLines;
	}

}
