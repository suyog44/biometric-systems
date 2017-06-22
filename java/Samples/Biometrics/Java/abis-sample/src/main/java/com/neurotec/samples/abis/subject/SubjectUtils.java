package com.neurotec.samples.abis.subject;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricAttributes;
import com.neurotec.biometrics.NEAttributes;
import com.neurotec.biometrics.NFAttributes;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.samples.abis.util.CollectionUtils;

public final class SubjectUtils {

	//Private constructor
	private SubjectUtils() { }

	//region Generalization helper functions

	public static boolean isBiometricGeneralizationResult(NBiometric biometric) {
		if (biometric.getSessionId() == -1) {
			NBiometricAttributes parentObject = biometric.getParentObject();
			if (parentObject != null) {
				NBiometric owner = (NBiometric) parentObject.getOwner();
				return owner != null && owner.getSessionId() != -1;
			}
		}
		return false;
	}

	public static boolean isBiometricGeneralizationSource(NBiometric biometric) {
		return biometric.getSessionId() != -1;
	}

	public static List<NFace[]> getFaceGeneralizationGroups(NSubject subject) {
		return getFaceGeneralizationGroups(subject.getFaces());
	}

	public static List<NFace[]> getFaceGeneralizationGroups(List<NFace> allFaces) {
		List<NFace[]> groups = new ArrayList<NFace[]>();
		for (NFace item : allFaces) {
			if (item.getSessionId() == -1 && item.getParentObject() == null) {
				groups.add(new NFace[]{item});
				return groups;
			}
		}
		List<Integer> ids = new ArrayList<Integer>();
		for (NFace item : allFaces) {
			if (item.getSessionId() != -1 && !ids.contains(item.getSessionId())) {
				ids.add(item.getSessionId());
			}
		}
		for (Integer id : ids) {
			List<NFace> parentFaces = new ArrayList<NFace>();
			List<NFace> childFaces = new ArrayList<NFace>();
			for (NFace item : allFaces) {
				if (item.getSessionId() == id) {
					if (item.getParentObject() == null) {
						parentFaces.add(item);
					} else {
						childFaces.add(item);
					}
				}
			}
			groups.add(parentFaces.toArray(new NFace[parentFaces.size()]));
			groups.add(childFaces.toArray(new NFace[parentFaces.size()]));
		}
		return groups;
	}

	public static List<List<NFinger>> getFingersGeneralizationGroups(NSubject subject) {
		return getFingersGeneralizationGroups(subject.getFingers());
	}

	public static List<List<NFinger>> getFingersGeneralizationGroups(List<NFinger> fingers) {
		return getFrictionRidgeGeneralizationGroups(fingers);
	}

	public static List<List<NPalm>> getPalmsGeneralizationGroups(NSubject subject) {
		return getPalmsGeneralizationGroups(subject.getPalms());
	}

	public static List<List<NPalm>> getPalmsGeneralizationGroups(List<NPalm> palms) {
		return getFrictionRidgeGeneralizationGroups(palms);
	}

	public static List<NFinger> getFingersInSameGroup(List<NFinger> fingers, NFinger finger) {
		return getFrictionRidgesInSameGroup(fingers, finger);
	}

	public static List<NPalm> getPalmsInSameGroup(List<NPalm> palms, NPalm palm) {
		return getFrictionRidgesInSameGroup(palms, palm);
	}

	public static List<NFace> getFacesInSameGroup(NFace[] faces, NFace face) {
		List<NFace> result = new ArrayList<NFace>();
		if (isBiometricGeneralizationSource(face)) {
			for (NFace f : faces) {
				if ((f.getSessionId() == face.getSessionId()) && ((f.getParentObject() == null) == (face.getParentObject() == null))) {
					result.add(f);
				}
			}
			NLAttributes attributes = CollectionUtils.getFirst(face.getObjects());
			NFace child = attributes != null ? (NFace) attributes.getChild() : null;
			if (child != null) {
				result.add(child);
			}
			if (result.size() > 0) {
				return result;
			}
		} else if (isBiometricGeneralizationResult(face)) {
			NLAttributes parentObject = (NLAttributes) face.getParentObject();
			NFace owner = parentObject.getOwner();
			return getFacesInSameGroup(faces, owner);
		}
		return Arrays.asList(face);
	}

	//region Flatten fingers

	public static List<NFinger> flattenFingers(List<NFinger> fingers) {
		List<NFinger> result = new ArrayList<NFinger>();
		for (NFinger item : fingers) {
			result.add(item);
			List<NFinger> children = new ArrayList<NFinger>();
			for (NFAttributes attribute : item.getObjects()) {
				if (attribute.getChild() != null) {
					children.add((NFinger) attribute.getChild());
				}
			}
			result.addAll(flattenFingers(children));
		}
		//TODO return distinct.
		return result;
	}

	public static List<NPalm> flattenPalms(List<NPalm> palms) {
		List<NPalm> result = new ArrayList<NPalm>();
		for (NPalm item : palms) {
			result.add(item);
			List<NPalm> children = new ArrayList<NPalm>();
			for (NFAttributes attribute : item.getObjects()) {
				if (attribute.getChild() != null) {
					children.add((NPalm) attribute.getChild());
				}
			}
			result.addAll(flattenPalms(children));
		}
		//TODO return distinct.
		return result;
	}


	//region Get template composites
	// Get biometrics of which template is made, ignoring biometrics not containing template or containing template meant for generalization

	public static List<NFinger> getTemplateCompositeFingers(NSubject subject) {
		List<NFinger> fingers = new ArrayList<NFinger>();
		for (NFinger finger : subject.getFingers()) {
			if (finger.getSessionId() == -1) {
				List<NFAttributes> attributes = finger.getObjects();
				if (attributes.size() == 1 && attributes.get(0).getTemplate() != null) {
					fingers.add(finger);
				}
			}
		}
		return fingers;
	}

	public static List<NFace> getTemplateCompositeFaces(NSubject subject) {
		List<NFace> faces = new ArrayList<NFace>();
		for (NFace face : subject.getFaces()) {
			if (face.getSessionId() == -1) {
				List<NLAttributes> attributes = face.getObjects();
				if (attributes.size() > 0 && attributes.get(0).getTemplate() != null) {
					faces.add(face);
				}
			}
		}
		return faces;
	}

	public static List<NIris> getTemplateCompositeIrises(NSubject subject) {
		List<NIris> irises = new ArrayList<NIris>();
		for (NIris iris : subject.getIrises()) {
			if (iris.getSessionId() == -1) {
				List<NEAttributes> attributes = iris.getObjects();
				if (attributes.size() == 1 && attributes.get(0).getTemplate() != null) {
					irises.add(iris);
				}
			}
		}
		return irises;
	}

	public static List<NPalm> getTemplateCompositePalms(NSubject subject) {
		List<NPalm> palms = new ArrayList<NPalm>();
		for (NPalm palm : subject.getPalms()) {
			if (palm.getSessionId() == -1) {
				List<NFAttributes> attributes = palm.getObjects();
				if (attributes.size() == 1 && attributes.get(0).getTemplate() != null) {
					palms.add(palm);
				}
			}
		}
		return palms;
	}

	public static List<NVoice> getTemplateCompositeVoices(NSubject subject) {
		List<NVoice> voices = new ArrayList<NVoice>();
		for (NVoice voice : subject.getVoices()) {
			if (voice.getSessionId() == -1) {
				List<NSAttributes> attributes = voice.getObjects();
				if (attributes.size() == 1 && attributes.get(0).getTemplate() != null) {
					voices.add(voice);
				}
			}
		}
		return voices;
	}

	//Private static methods

	@SuppressWarnings("unchecked")
	private static <T extends NFrictionRidge> List<T> getFrictionRidgesInSameGroup(List<T> allFingers, T finger) {
		if (isBiometricGeneralizationSource(finger)) {
			List<T> result = new ArrayList<T>();
			for (T item : allFingers) {
				if (item.getPosition() == finger.getPosition() && item.getImpressionType() == finger.getImpressionType() && item.getSessionId() == finger.getSessionId()) {
					result.add(item);
				}
			}

			NFAttributes attributes = CollectionUtils.getFirst(finger.getObjects());
			T child = attributes != null ? (T) attributes.getChild() : null;
			if (child != null && child.getSessionId() == -1) {
				result.add(child);
			}
			return result;
		} else if (isBiometricGeneralizationResult(finger)) {
			NBiometricAttributes parentObject = finger.getParentObject();
			T owner = parentObject != null ? (T) parentObject.getOwner() : null;
			if (owner != null && owner.getSessionId() != -1) {
				return getFrictionRidgesInSameGroup(allFingers, owner);
			}
		}
		return Arrays.asList(finger);
	}

	@SuppressWarnings("unchecked")
	private static <T extends NFrictionRidge> List<List<T>> getFrictionRidgeGeneralizationGroups(List<T> fingers) {
		List<List<T>> result = new ArrayList<List<T>>();
		for (T item : fingers) {
			if (item.getSessionId() == -1 && item.getParentObject() == null) {
				result.add(Arrays.asList(item));
			}
		}
		if (result.size() > 0) {
			return result;
		}

		List<FrictionRidgeID> ids = new ArrayList<FrictionRidgeID>();
		for (T item : fingers) {
			if (item.getSessionId() != -1) {
				FrictionRidgeID data = new FrictionRidgeID(item.getSessionId(), item.getPosition(), item.getImpressionType());
				if (!ids.contains(data)) {
					ids.add(data);
				}
			}
		}

		for (FrictionRidgeID id : ids) {
			List<T> r = new ArrayList<T>();
			for (T item : fingers) {
				if (item.getSessionId() == id.getSessionId() && item.getPosition() == id.getPosition() && item.getImpressionType() == id.getImpressionType()) {
					r.add(item);
				}
			}
			if (r.size() > 0 && r.get(0).getObjects().size() > 0) {
				NFAttributes attributes = r.get(0).getObjects().get(0);
				T child = attributes != null ? (T) attributes.getChild() : null;
				if (child != null && child.getSessionId() == -1 && child.getPosition() == r.get(0).getPosition()) {
					r.add(child);
				}
			}
			result.add(r);
		}
		return result;
	}
}

class FrictionRidgeID {

	private final int sessionId;
	private final NFPosition position;
	private final NFImpressionType impressionType;

	FrictionRidgeID(int sessionId, NFPosition position, NFImpressionType impressionType) {
		super();
		this.sessionId = sessionId;
		this.position = position;
		this.impressionType = impressionType;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((impressionType == null) ? 0 : impressionType.hashCode());
		result = prime * result + ((position == null) ? 0 : position.hashCode());
		result = prime * result + sessionId;
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (obj == null) {
			return false;
		}
		if (getClass() != obj.getClass()) {
			return false;
		}
		FrictionRidgeID other = (FrictionRidgeID) obj;
		if (impressionType != other.getImpressionType()) {
			return false;
		}
		if (position != other.getPosition()) {
			return false;
		}
		if (sessionId != other.getSessionId()) {
			return false;
		}
		return true;
	}

	public int getSessionId() {
		return sessionId;
	}

	public NFPosition getPosition() {
		return position;
	}

	public NFImpressionType getImpressionType() {
		return impressionType;
	}


}
