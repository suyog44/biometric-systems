package com.neurotec.samples.abis.subject;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.devices.NFScanner;

public final class Scenario {

	// ===========================================================
	// Nested classes
	// ===========================================================

	public enum Attribute {
		KNOWN,
		ROLLED,
		SLAP
	}

	// ===========================================================
	// Public static fields
	// ===========================================================

	public static final List<NFPosition> FINGERS = Arrays.asList(NFPosition.LEFT_LITTLE_FINGER, NFPosition.LEFT_RING_FINGER, NFPosition.LEFT_MIDDLE_FINGER, NFPosition.LEFT_INDEX_FINGER, NFPosition.LEFT_THUMB, NFPosition.RIGHT_THUMB, NFPosition.RIGHT_INDEX_FINGER, NFPosition.RIGHT_MIDDLE_FINGER, NFPosition.RIGHT_RING_FINGER, NFPosition.RIGHT_LITTLE_FINGER);

	public static final List<NFPosition> SLAP_AND_TWO_THUMBS = Arrays.asList(NFPosition.PLAIN_LEFT_FOUR_FINGERS, NFPosition.PLAIN_RIGHT_FOUR_FINGERS, NFPosition.PLAIN_THUMBS);

	public static final List<NFPosition> SLAP_AND_SEPARATE_THUMBS = Arrays.asList(NFPosition.PLAIN_LEFT_FOUR_FINGERS, NFPosition.PLAIN_RIGHT_FOUR_FINGERS, NFPosition.LEFT_THUMB, NFPosition.RIGHT_THUMB);
	public static final List<NFPosition> PALMS = Arrays.asList(NFPosition.LEFT_FULL_PALM, NFPosition.LEFT_HYPOTHENAR, NFPosition.LEFT_INTERDIGITAL, NFPosition.LEFT_LOWER_PALM, NFPosition.LEFT_THENAR, NFPosition.LEFT_UPPER_PALM/*, NFPosition.LEFT_WRITERS_PALM*/, NFPosition.RIGHT_FULL_PALM, NFPosition.RIGHT_HYPOTHENAR, NFPosition.RIGHT_INTERDIGITAL, NFPosition.RIGHT_LOWER_PALM, NFPosition.RIGHT_THENAR, NFPosition.RIGHT_UPPER_PALM/*, NFPosition.RIGHT_WRITERS_PALM*/);

	public static final Scenario PLAIN_FINGER = new Scenario("Single plain finger");
	public static final Scenario ROLLED_FINGER = new Scenario("Single rolled finger", EnumSet.of(Attribute.ROLLED));
	public static final Scenario ALL_PLAIN_FINGERS = new Scenario("All plain fingers", FINGERS, EnumSet.of(Attribute.KNOWN));
	public static final Scenario ALL_ROLLED_FINGERS = new Scenario("All Rolled fingers", FINGERS, EnumSet.of(Attribute.ROLLED, Attribute.KNOWN));
	public static final Scenario SLAP_AND_THUMB = new Scenario("4-4-2", SLAP_AND_TWO_THUMBS, EnumSet.of(Attribute.SLAP, Attribute.KNOWN));
	public static final Scenario SLAPS_2_THUMBS = new Scenario("4-4-1-1", SLAP_AND_SEPARATE_THUMBS, EnumSet.of(Attribute.SLAP, Attribute.KNOWN));
	//public static final Scenario ROLLED_PLUS_SLAPS = new Scenario("Rolled fingers + 4-4-2", SelectionMode.SLAPS_AND_SEPARATE_THUMBS, Position.SLAP_AND_THUMB);
	//public static final Scenario ROLLED_PLUS_SLAPS_2_TUMBS;
	public static final Scenario ALL_PALMS = new Scenario("All palms", PALMS);
	public static final Scenario NONE = new Scenario("None");

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static Scenario[] getAllFingerScenarios() {
		//TODO: fix me
		return new Scenario[] { Scenario.PLAIN_FINGER, Scenario.ROLLED_FINGER, Scenario.ALL_PLAIN_FINGERS, Scenario.ALL_ROLLED_FINGERS, Scenario.SLAP_AND_THUMB, Scenario.SLAPS_2_THUMBS,
		/* Scenario.ROLLED_PLUS_SLAPS */
		};
	}

	public static boolean isSelectAll(Scenario scenario) {
		return scenario.equals(ALL_PLAIN_FINGERS) || scenario.equals(ALL_ROLLED_FINGERS);
	}

	public static boolean isSlapsAndThumbs(Scenario scenario) {
		return scenario.equals(SLAP_AND_THUMB) || scenario.equals(SLAPS_2_THUMBS);
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private String message;
	private List<NFPosition> allowedPositions = new ArrayList<NFPosition>();
	private final EnumSet<Attribute> attributes;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public Scenario(String message) {
		this(message, new ArrayList<NFPosition>(), EnumSet.noneOf(Attribute.class));
	}

	public Scenario(String message, EnumSet<Attribute> attributes) {
		this(message, new ArrayList<NFPosition>(), attributes);
	}

	public Scenario(String message, List<NFPosition> allowedPositions) {
		this(message, allowedPositions, EnumSet.noneOf(Attribute.class));
	}

	public Scenario(String message, List<NFPosition> allowedPositions, EnumSet<Attribute> attributes) {
		this.message = message;
		this.allowedPositions = allowedPositions;
		this.attributes = attributes;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public EnumSet<Attribute> getAttributes() {
		return attributes.clone();
	}

	public List<NFPosition> getAllowedPositions() {
		return allowedPositions;
	}

	public void setAllowedPositions(List<NFPosition> positions) {
		allowedPositions = positions;
	}

	public static Scenario[] getSupportedScenarios(NFScanner scanner) {
		List<Scenario> set = new ArrayList<Scenario>();
		Scenario[] result;
		set.add(Scenario.PLAIN_FINGER);
		boolean hasSlaps = false;
		boolean hasRolled = false;

		for (NFPosition position : scanner.getSupportedPositions()) {
			if (position.isFourFingers()) {
				hasSlaps = true;
				break;
			}
		}
		for (NFImpressionType impresion : scanner.getSupportedImpressionTypes()) {
			if (impresion.isRolled()) {
				hasRolled = true;
				break;
			}
		}

		if (hasRolled) {
			set.add(Scenario.ROLLED_FINGER);
		}
		set.add(Scenario.ALL_PLAIN_FINGERS);
		if (hasRolled) {
			set.add(Scenario.ALL_ROLLED_FINGERS);
		}
		if (hasSlaps) {
			set.add(Scenario.SLAPS_2_THUMBS);
			set.add(Scenario.SLAP_AND_THUMB);
//			if (hasRolled)
//				set.add(Scenario.ROLLED_PLUS_SLAPS);
		}

		result = new Scenario[set.size()];
		return set.toArray(result);
	}

	@Override
	public String toString() {
		return message;
	}

}
