package com.neurotec.samples.abis.swing;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricAttributes;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.util.CollectionUtils;

public final class NodeTag {

	private Object objectTag;
	private List<NBiometric> items;
	private EnumSet<NBiometricType> type;
	private int sessionId;
	private NFPosition position;
	private NFImpressionType impressionType;

	public NodeTag(Object tag, boolean group)	{
		items = new ArrayList<NBiometric>();
		objectTag = tag;
		sessionId = -1;
		if (tag instanceof NSubject.FingerCollection) {
			type = EnumSet.of(NBiometricType.FINGER);
		} else if (tag instanceof NSubject.FaceCollection)
			type = EnumSet.of(NBiometricType.FACE);
		else if (tag instanceof NSubject.IrisCollection)
			type = EnumSet.of(NBiometricType.IRIS);
		else if (tag instanceof NSubject.PalmCollection)
			type = EnumSet.of(NBiometricType.PALM);
		else if (tag instanceof NSubject.VoiceCollection)
			type = EnumSet.of(NBiometricType.VOICE);
	}


	public NodeTag(NBiometric biometric) {
		this(Arrays.asList(biometric));
	}
	public NodeTag(List<? extends NBiometric> biometrics) {
		if (biometrics == null) throw new NullPointerException("biometrics");
		items = new ArrayList<NBiometric>(biometrics);
		NBiometric first = CollectionUtils.getFirst(biometrics);
		if (first != null) {
			type = first.getBiometricType();
			sessionId = first.getSessionId();
			if (type.contains(NBiometricType.FINGER)|| type.contains(NBiometricType.PALM)) {
				NFrictionRidge ridge = (NFrictionRidge)first;
				position = ridge.getPosition();
				impressionType = ridge.getImpressionType();
			}
		}
	}

	public boolean hasTag(Object tag) {
		return tag.equals(objectTag) || (items != null && tag instanceof NBiometric && items.contains((NBiometric)tag));
	}

	public boolean belongsToNode(Object tag) {
		if (tag instanceof NBiometric) {
			NBiometric item = (NBiometric) tag;
			if (item != null && item.getBiometricType().equals(type)) { //TODO TEST IT
				if (type.contains(NBiometricType.FINGER) || type.contains(NBiometricType.PALM)) {
					NFrictionRidge frictionRidge = (NFrictionRidge) item;
					if (position != frictionRidge.getPosition() || impressionType != frictionRidge.getImpressionType()) {
						return false;
					}
				}
				if (type.contains(NBiometricType.FACE)) {
					if (sessionId == item.getSessionId()) {
						if ((item.getParentObject() == null) ^ (items.isEmpty() || (items.get(0).getParentObject() == null))) {
							return false;
						}
					}
				}
				if (sessionId == -1 && item.getSessionId() == -1) {
					return items.contains(item);
				}
				if (sessionId == item.getSessionId()) {
					return true;
				}
				if (sessionId != -1 && item.getSessionId() == -1) {
					NBiometricAttributes parentObject = item.getParentObject();
					if (parentObject != null) {
						NBiometric parent = (NBiometric) parentObject.getOwner();
						return items.contains(parent);
					}
				}
			}
		}
		return false;
	}

	public Object getObjectTag() {
		return objectTag;
	}

	public void setObjectTag(Object objectTag) {
		this.objectTag = objectTag;
	}

	public List<NBiometric> getItems() {
		return items;
	}

	public void setItems(List<NBiometric> items) {
		this.items = items;
	}

	public EnumSet<NBiometricType> getType() {
		return type;
	}

	public void setType(EnumSet<NBiometricType> type) {
		this.type = type;
	}

	public int getSessionId() {
		return sessionId;
	}

	public void setSessionId(int sessionId) {
		this.sessionId = sessionId;
	}

	public NFPosition getPosition() {
		return position;
	}

	public void setPosition(NFPosition position) {
		this.position = position;
	}

	public NFImpressionType getImpressionType() {
		return impressionType;
	}

	public void setImpressionType(NFImpressionType impressionType) {
		this.impressionType = impressionType;
	}
}
