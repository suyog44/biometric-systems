package com.neurotec.samples.abis.subject.fingers;

import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NFAttributes;
import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NSubject.FingerCollection;
import com.neurotec.devices.NFScanner;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.samples.abis.subject.Scenario;
import com.neurotec.samples.abis.subject.Scenario.Attribute;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.util.NObjectCollection;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;

public final class CaptureFingerModel extends CaptureBiometricModel<NFinger> {

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureFingerModel(NSubject mainSubject, NSubject localSubject, AbisModel abisModel) {
		super(mainSubject, localSubject, abisModel);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public List<Scenario> getSupportedScenarios(Source source) {
		if ((source != Source.FILE) && (source != Source.DEVICE) && (source != Source.TEN_PRINT_CARD)) {
			throw new IllegalArgumentException("source");
		}
		if (source == Source.TEN_PRINT_CARD) {
			List<Scenario> scenarios = new ArrayList<Scenario>();
			scenarios.add(Scenario.NONE);
			return scenarios;
		}
		List<Scenario> supportedScenarios = new ArrayList<Scenario>(Arrays.asList(Scenario.getAllFingerScenarios()));
		if (source == Source.DEVICE) {
			NFScanner scanner = getClient().getFingerScanner();
			NFImpressionType[] impressions = scanner.getSupportedImpressionTypes();
			NFPosition[] positions = scanner.getSupportedPositions();
			boolean supportsRolled = false;
			for (NFImpressionType impressionType : impressions) {
				if (impressionType.isRolled()) {
					supportsRolled = true;
					break;
				}
			}
			boolean supportsSlaps = false;
			for (NFPosition p : positions) {
				if (p.isFourFingers()) {
					supportsSlaps = true;
					break;
				}
			}
			if (!supportsRolled) {
				for (Scenario supportedScenario : supportedScenarios.toArray(new Scenario[supportedScenarios.size()])) {
					if (supportedScenario.getAttributes().contains(Attribute.ROLLED)) {
						supportedScenarios.remove(supportedScenario);
					}
				}
			}
			if (!supportsSlaps) {
				for (Scenario supportedScenario : supportedScenarios.toArray(new Scenario[supportedScenarios.size()])) {
					if (supportedScenario.getAttributes().contains(Attribute.SLAP)) {
						supportedScenarios.remove(supportedScenario);
					}
				}
			}
		}
		return supportedScenarios;
	}

	public List<NFImpressionType> getSupportedImpressionTypes(Source source, NFPosition position, boolean isRolled) {
		if ((source != Source.FILE) && (source != Source.DEVICE) && (source != Source.TEN_PRINT_CARD)) {
			throw new IllegalArgumentException("source");
		}
		List<NFImpressionType> impressions;
		if (source == Source.DEVICE) {
			impressions = new ArrayList<NFImpressionType>(Arrays.asList(getClient().getFingerScanner().getSupportedImpressionTypes()));
		} else if ((source == Source.FILE) || (source == Source.TEN_PRINT_CARD)) {
			impressions = new ArrayList<NFImpressionType>(Arrays.asList(NFImpressionType.values()));
		} else {
			throw new AssertionError("source");
		}
		Iterator<NFImpressionType> it = impressions.iterator();
		while (it.hasNext()) {
			NFImpressionType type = it.next();
			if ((type.isRolled() ^ isRolled) || !position.isCompatibleWith(type)) {
				it.remove();
			}
		}
		return impressions;
	}

	public Scenario getCompatibleScenario(NFinger finger) {
		NFPosition p = finger.getPosition();
		NFImpressionType impr = finger.getImpressionType();
		List<Scenario> scenarios = new ArrayList<Scenario>(Arrays.asList(Scenario.getAllFingerScenarios()));
		Iterator<Scenario> it = scenarios.iterator();
		while (it.hasNext()) {
			Scenario scenario = it.next();
			if (scenario.getAttributes().contains(Attribute.SLAP) ^ !p.isSingleFinger()) {
				it.remove();
			} else if (scenario.getAttributes().contains(Attribute.ROLLED) ^ impr.isRolled()) {
				it.remove();
			} else if (scenario.getAttributes().contains(Attribute.KNOWN) ^ p.isKnown()) {
				it.remove();
			} else if ((scenario == Scenario.SLAPS_2_THUMBS) && p.isTwoFingers()) {
				it.remove();
			}
		}
		if (scenarios.isEmpty()) {
			throw new AssertionError("At least one scenario must be compatible");
		}
		return scenarios.get(0);
	}

	@Override
	public void removeFailedBiometrics() {
		FingerCollection fingers = getLocalSubject().getFingers();
		for (NFinger f : fingers.toArray(new NFinger[fingers.size()])) {
			if ((f.getStatus() != NBiometricStatus.OK) && (f.getParentObject() == null)) {
				for (NFAttributes attributes : f.getObjects()) {
					fingers.remove(attributes.getChild());
				}
				fingers.remove(f);
			}
		}
	}

	@Override
	public NObjectCollection<NFinger> getRelevantBiometricCollection() {
		return getLocalSubject().getFingers();
	}

}
