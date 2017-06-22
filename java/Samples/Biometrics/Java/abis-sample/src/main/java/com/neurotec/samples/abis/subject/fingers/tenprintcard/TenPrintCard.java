package com.neurotec.samples.abis.subject.fingers.tenprintcard;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFAttributes;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.images.NImage;

import java.awt.Rectangle;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public final class TenPrintCard {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final List<NFPosition> POSITIONS_ROLLED;
	private static final EnumSet<NFPosition> POSITIONS_THUMB;

	// ===========================================================
	// Static constructor
	// ===========================================================

	static {
		POSITIONS_ROLLED = new ArrayList<NFPosition>(10);
		POSITIONS_ROLLED.add(NFPosition.RIGHT_THUMB);
		POSITIONS_ROLLED.add(NFPosition.RIGHT_INDEX_FINGER);
		POSITIONS_ROLLED.add(NFPosition.RIGHT_MIDDLE_FINGER);
		POSITIONS_ROLLED.add(NFPosition.RIGHT_RING_FINGER);
		POSITIONS_ROLLED.add(NFPosition.RIGHT_LITTLE_FINGER);
		POSITIONS_ROLLED.add(NFPosition.LEFT_THUMB);
		POSITIONS_ROLLED.add(NFPosition.LEFT_INDEX_FINGER);
		POSITIONS_ROLLED.add(NFPosition.LEFT_MIDDLE_FINGER);
		POSITIONS_ROLLED.add(NFPosition.LEFT_RING_FINGER);
		POSITIONS_ROLLED.add(NFPosition.LEFT_LITTLE_FINGER);
		POSITIONS_THUMB = EnumSet.of(NFPosition.LEFT_THUMB, NFPosition.RIGHT_THUMB, NFPosition.PLAIN_RIGHT_THUMB, NFPosition.PLAIN_LEFT_THUMB);
	}

	// ===========================================================
	// Static methods
	// ===========================================================

	public static TenPrintCard fromSubject(NSubject subject) {
		List<NFinger> rolled = new ArrayList<NFinger>();
		List<NFinger> plain = new ArrayList<NFinger>();
		EnumSet<NFPosition> existingRolledPositions = EnumSet.noneOf(NFPosition.class);
		EnumSet<NFPosition> existingPlainPositions = EnumSet.noneOf(NFPosition.class);
		for (NFinger finger : subject.getFingers()) {
			if (finger.getImage() != null) {
				if (finger.getImpressionType().isRolled()) {
					if (POSITIONS_ROLLED.contains(finger.getPosition()) && !existingRolledPositions.contains(finger.getPosition())) {
						NFinger cardFinger = new NFinger();
						cardFinger.setImage(finger.getImage());
						cardFinger.setPosition(finger.getPosition());
						cardFinger.setImpressionType(finger.getImpressionType());
						rolled.add(cardFinger);
						existingRolledPositions.add(finger.getPosition());
					}
				} else if (finger.getImpressionType().isPlain()) {
					if ((finger.getPosition().isFourFingers() || POSITIONS_THUMB.contains(finger.getPosition())) && !existingPlainPositions.contains(finger.getPosition())) {
						NFinger cardFinger = new NFinger();
						cardFinger.setImage(finger.getImage());
						cardFinger.setPosition(finger.getPosition());
						cardFinger.setImpressionType(finger.getImpressionType());
						plain.add(cardFinger);
						existingPlainPositions.add(finger.getPosition());
					}
				}
			}
		}
		NSubject cardSubject = new NSubject();
		cardSubject.getFingers().addAll(rolled);
		cardSubject.getFingers().addAll(plain);
		return new TenPrintCard(cardSubject);
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private final Map<Integer, NImage> images;
	private final NBiometricClient client;

	// ===========================================================
	// Private constructor
	// ===========================================================

	private TenPrintCard(NSubject subject) {
		client = new NBiometricClient();
		images = segmentFingers(subject);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private Map<Integer, NImage> segmentFingers(NSubject s) {
		NBiometricTask task = client.createTask(EnumSet.of(NBiometricOperation.SEGMENT), s);
		client.performTask(task);
		if (task.getError() != null) {
			throw new RuntimeException("Segmentation failed", task.getError());
		}
		if (task.getStatus() != NBiometricStatus.OK) {
			throw new RuntimeException("Segmentation failed: " + task.getStatus());
		}

		Map<Integer, NImage> imageMap = new HashMap<Integer, NImage>();

		List<NFinger> segmented = new ArrayList<NFinger>();
		for (NFinger f : s.getFingers()) {
			if (f.getParentObject() != null) {
				segmented.add(f);
			}
		}

		// Put rolled fingers.
		List<NFinger> rolled = new ArrayList<NFinger>();
		for (NFinger f : segmented) {
			if (f.getImpressionType().isRolled()) {
				rolled.add(f);
			}
		}
		for (NFinger f : rolled) {
			if (POSITIONS_ROLLED.contains(f.getPosition())) {
				imageMap.put(POSITIONS_ROLLED.indexOf(f.getPosition()) + 1, f.getImage());
			}
		}

		// Put thumbs.
		for (NFinger f : segmented) {
			if (f.getImpressionType().isPlain() && f.getPosition().isSingleFinger() && f.getPosition().toString().toUpperCase().contains("THUMB")) {
				Integer key;
				if (f.getPosition().isLeft()) {
					key = 12;
				} else if (f.getPosition().isRight()) {
					key = 13;
				} else {
					throw new AssertionError("can't be neither left nor right");
				}
				if (!imageMap.containsKey(key)) {
					imageMap.put(key, f.getImage());
				}
			}
		}

		// Put four fingers.
		for (NFinger f : s.getFingers()) {
			if ((f.getParentObject() == null) && f.getPosition().isFourFingers()) {
				Integer key;
				if (f.getPosition().isLeft()) {
					key = 11;
				} else if (f.getPosition().isRight()) {
					key = 14;
				} else {
					throw new AssertionError("can't be neither left nor right");
				}
				if (!imageMap.containsKey(key)) {
					imageMap.put(key, cropFingerImage(f));
				}
			}
		}

		return imageMap;
	}

	private NImage cropFingerImage(NFinger finger) {
		Rectangle rect = finger.getObjects().get(0).getBoundingRect();
		for (NFAttributes attributes : finger.getObjects()) {
			rect = rect.union(attributes.getBoundingRect());
		}

		NImage fingerImage = finger.getImage();
		if (rect.x < 0) {
			rect.x = 0;
		}
		if (rect.y < 0) {
			rect.y = 0;
		}
		if (rect.width + rect.x > fingerImage.getWidth()) {
			rect.width = fingerImage.getWidth() - rect.x;
		}
		if (rect.height + rect.y > fingerImage.getHeight()) {
			rect.height = fingerImage.getHeight() - rect.y;
		}

		return finger.getImage().crop(rect.x, rect.y, rect.width, rect.height);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public Map<Integer, NImage> getImages() {
		return images;
	}

}
