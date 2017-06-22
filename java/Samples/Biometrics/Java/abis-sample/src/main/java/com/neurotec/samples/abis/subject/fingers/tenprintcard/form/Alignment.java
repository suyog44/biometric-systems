package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

enum Alignment {

	// ===========================================================
	// Values
	// ===========================================================

	LEFT("left"),
	RIGHT("right"),
	TOP("top"),
	BOTTOM("bottom"),
	CENTER("center");

	// ===========================================================
	// Static methods
	// ===========================================================

	public static Alignment fromString(String name) {
		if (name == null) {
			throw new NullPointerException("name");
		} else {
			for (Alignment alignment : Alignment.values()) {
				if (name.equalsIgnoreCase(alignment.getName())) {
					return alignment;
				}
			}
		}
		throw new IllegalArgumentException("No such Alignment: " + name);
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private final String name;

	// ===========================================================
	// Private methods
	// ===========================================================

	private Alignment(String name) {
		this.name = name;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public String getName() {
		return name;
	}

}
