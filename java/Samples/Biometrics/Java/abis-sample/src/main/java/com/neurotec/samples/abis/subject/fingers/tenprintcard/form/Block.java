package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

import java.util.ArrayList;
import java.util.List;

final class Block {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final int x;
	private final int y;
	private final int width;
	private final int height;
	private final boolean fingerBlock;
	private final List<Cell> cells;
	private final List<BorderInfo> borders;

	// ===========================================================
	// Package private constructor
	// ===========================================================

	Block(int x, int y, int width, int height, boolean fingerBlock) {
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.fingerBlock = fingerBlock;
		this.cells = new ArrayList<Cell>();
		this.borders = new ArrayList<BorderInfo>();
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}

	public int getWidth() {
		return width;
	}

	public int getHeight() {
		return height;
	}

	public boolean isFingerBlock() {
		return fingerBlock;
	}

	public List<Cell> getCells() {
		return cells;
	}

	public List<BorderInfo> getBorders() {
		return borders;
	}

}
