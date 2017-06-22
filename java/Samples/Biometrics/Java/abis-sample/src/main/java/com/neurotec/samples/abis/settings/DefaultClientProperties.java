package com.neurotec.samples.abis.settings;

import com.neurotec.biometrics.NBiometricEngine;
import com.neurotec.biometrics.NLivenessMode;
import com.neurotec.lang.NType;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.util.NPropertyBag;

public final class DefaultClientProperties {

	private static DefaultClientProperties instance;

	public static DefaultClientProperties getInstance() {
		synchronized (DefaultClientProperties.class) {
			if (instance == null) {
				instance = new DefaultClientProperties();
			}
			return instance;
		}
	}

	private final NPropertyBag general;
	private final NPropertyBag fingers;
	private final NPropertyBag faces;
	private final NPropertyBag irises;
	private final NPropertyBag voices;
	private final NPropertyBag palms;

	private DefaultClientProperties() {
		NType engine = NBiometricEngine.nativeTypeOf();

		general = new NPropertyBag();
		general.add("Matching.WithDetails", true);
		general.add("Matching.Threshold", engine.getDeclaredProperty("Matching.Threshold").getDefaultValue());
		general.add("Matching.MaximalResultCount", engine.getDeclaredProperty("Matching.MaximalResultCount").getDefaultValue());
		general.add("Matching.FirstResultOnly", engine.getDeclaredProperty("Matching.FirstResultOnly").getDefaultValue());

		fingers = new NPropertyBag();
		fingers.add("Fingers.ReturnBinarizedImage", true);
		boolean fqabActivated = LicenseManager.getInstance().isActivated("Biometrics.FingerQualityAssessmentBase", true);
		fingers.add("Fingers.CalculateNfiq", fqabActivated);
		boolean fsdActivated = LicenseManager.getInstance().isActivated("Biometrics.FingerSegmentsDetection", true);
		fingers.add("Fingers.DeterminePatternClass", fsdActivated);
		boolean fingerMatchingActivated = LicenseManager.getInstance().isActivated("Biometrics.FingerMatching", true);
		fingers.add("Fingers.CheckForDuplicatesWhenCapturing", fingerMatchingActivated);
		fingers.add("Fingers.TemplateSize", engine.getDeclaredProperty("Fingers.TemplateSize").getDefaultValue());
		fingers.add("Fingers.MatchingSpeed", engine.getDeclaredProperty("Fingers.MatchingSpeed").getDefaultValue());
		fingers.add("Fingers.MaximalRotation", engine.getDeclaredProperty("Fingers.MaximalRotation").getDefaultValue());
		fingers.add("Fingers.QualityThreshold", engine.getDeclaredProperty("Fingers.QualityThreshold").getDefaultValue());
		fingers.add("Fingers.FastExtraction", engine.getDeclaredProperty("Fingers.FastExtraction").getDefaultValue());

		faces = new NPropertyBag();
		boolean faceSegmentationActivated = LicenseManager.getInstance().isActivated("Biometrics.FaceSegmentation", true);
		faces.add("Faces.DetectAllFeaturePoints", faceSegmentationActivated);
		faces.add("Faces.DetermineGender", faceSegmentationActivated);
		faces.add("Faces.DetermineAge", faceSegmentationActivated);
		faces.add("Faces.DetectProperties", faceSegmentationActivated);
		faces.add("Faces.RecognizeExpression", faceSegmentationActivated);
		faces.add("Faces.RecognizeEmotion", faceSegmentationActivated);
		faces.add("Faces.CreateThumbnailImage", true);
		faces.add("Faces.ThumbnailImageWidth", 90);
		faces.add("Faces.TemplateSize", engine.getDeclaredProperty("Faces.TemplateSize").getDefaultValue());
		faces.add("Faces.MatchingSpeed", engine.getDeclaredProperty("Faces.MatchingSpeed").getDefaultValue());
		faces.add("Faces.MinimalInterOcularDistance", engine.getDeclaredProperty("Faces.MinimalInterOcularDistance").getDefaultValue());
		faces.add("Faces.ConfidenceThreshold", engine.getDeclaredProperty("Faces.ConfidenceThreshold").getDefaultValue());
		faces.add("Faces.MaximalRoll", engine.getDeclaredProperty("Faces.MaximalRoll").getDefaultValue());
		faces.add("Faces.MaximalYaw", engine.getDeclaredProperty("Faces.MaximalYaw").getDefaultValue());
		faces.add("Faces.QualityThreshold", engine.getDeclaredProperty("Faces.QualityThreshold").getDefaultValue());
		faces.add("Faces.LivenessMode", NLivenessMode.NONE);
		faces.add("Faces.LivenessThreshold", engine.getDeclaredProperty("Faces.LivenessThreshold").getDefaultValue());
		faces.add("Faces.DetectBaseFeaturePoints", engine.getDeclaredProperty("Faces.DetectBaseFeaturePoints").getDefaultValue());

		irises = new NPropertyBag();
		irises.add("Irises.TemplateSize", engine.getDeclaredProperty("Irises.TemplateSize").getDefaultValue());
		irises.add("Irises.MatchingSpeed", engine.getDeclaredProperty("Irises.MatchingSpeed").getDefaultValue());
		irises.add("Irises.MaximalRotation", engine.getDeclaredProperty("Irises.MaximalRotation").getDefaultValue());
		irises.add("Irises.QualityThreshold", engine.getDeclaredProperty("Irises.QualityThreshold").getDefaultValue());
		irises.add("Irises.FastExtraction", engine.getDeclaredProperty("Irises.FastExtraction").getDefaultValue());

		voices = new NPropertyBag();
		voices.add("Voices.UniquePhrasesOnly", engine.getDeclaredProperty("Voices.UniquePhrasesOnly").getDefaultValue());
		voices.add("Voices.ExtractTextDependentFeatures", engine.getDeclaredProperty("Voices.ExtractTextDependentFeatures").getDefaultValue());
		voices.add("Voices.ExtractTextIndependentFeatures", engine.getDeclaredProperty("Voices.ExtractTextIndependentFeatures").getDefaultValue());

		palms = new NPropertyBag();
		palms.add("Palms.ReturnBinarizedImage", true);
		palms.add("Palms.TemplateSize", engine.getDeclaredProperty("Palms.TemplateSize").getDefaultValue());
		palms.add("Palms.MatchingSpeed", engine.getDeclaredProperty("Palms.MatchingSpeed").getDefaultValue());
		palms.add("Palms.MaximalRotation", engine.getDeclaredProperty("Palms.MaximalRotation").getDefaultValue());
		palms.add("Palms.QualityThreshold", engine.getDeclaredProperty("Palms.QualityThreshold").getDefaultValue());
	}

	public NPropertyBag getGeneral() {
		return general;
	}

	public NPropertyBag getFingers() {
		return fingers;
	}

	public NPropertyBag getFaces() {
		return faces;
	}

	public NPropertyBag getIrises() {
		return irises;
	}

	public NPropertyBag getVoices() {
		return voices;
	}

	public NPropertyBag getPalms() {
		return palms;
	}

}
