package com.neurotec.samples.abis.subject.voices;

import org.simpleframework.xml.Default;

@Default
public class Phrase {

	// ===========================================================
	// Private fields
	// ===========================================================

	private int id;
	private String phrase;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public Phrase() { }

	public Phrase(int id, String phrase) {
		this.id = id;
		this.phrase = phrase;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public int getID() {
		return id;
	}

	public void setID(int id) {
		this.id = id;
	}

	public String getPhrase() {
		return phrase;
	}

	public void setPhrase(String phrase) {
		this.phrase = phrase;
	}

	@Override
	public String toString() {
		return phrase;
	}
}
