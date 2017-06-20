package com.neurotec.samples.biometrics.standards;

import java.util.ArrayList;

public final class IIPropertiesTable extends ArrayList<String> {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public constructor
	// ==============================================

	public IIPropertiesTable() {
		super();
		loadProperties();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void loadProperties() {

		// IIRecord
		this.add("captureDeviceId");
		this.add("CBEFFProductId");
		this.add("deviceUniqueIdentifier");
		this.add("flags");
		this.add("GUID");
		this.add("imageFormat");
		this.add("imageTransformation");
		this.add("intensityDepth");
		this.add("irisBoundaryExtraction");
		this.add("irisDiameter");
		this.add("irises");
		this.add("irisHorzOrientation");
		this.add("irisImages");
		this.add("irisOcclusionFilling");
		this.add("irisOcclusions");
		this.add("irisScanType");
		this.add("irisVertOrientation");
		this.add("rawImageHeight");
		this.add("rawImageWidth");
		this.add("standard");
		this.add("version");

		// IIRIrisImage
		this.add("captureDateAndTime");
		this.add("captureDeviceTechnology");
		this.add("captureDeviceTypeId");
		this.add("captureDeviceVendorId");
		this.add("imageData");
		this.add("imageHeight");
		this.add("imageNumber");
		this.add("imageType");
		this.add("imageWidth");
		this.add("irisCenterLargestX");
		this.add("irisCenterLargestY");
		this.add("irisCenterSmallestX");
		this.add("irisCenterSmallestY");
		this.add("largestIrisDiameter");
		this.add("irisDiameterSmallest");
		this.add("owner");
		this.add("position");
		this.add("previousCompression");
		this.add("quality");
		this.add("qualityBlocks");
		this.add("range");
		this.add("rotationAngle");
		this.add("rotationAngleRaw");
		this.add("rotationAngleUncertainty");
		this.add("rotationAngleUncertaintyRaw");

		// CBEFFRecord
		this.add("bdbBuffer");
		this.add("bdbCreationDate");
		this.add("bdbFormat");
		this.add("bdbIndex");
		this.add("bdbValidityPeriod");
		this.add("biometricSubType");
		this.add("biometricType");
		this.add("birCreationDate");
		this.add("birIndex");
		this.add("birValidityPeriod");
		this.add("captureDevice");
		this.add("cbeffVersion");
		this.add("challengeResponse");
		this.add("comparisonAlgorithm");
		this.add("compressionAlgorithm");
		this.add("creator");
		this.add("encryption");
		this.add("fascnBuffer");
		this.add("featureExtractionAlgorithm");
		this.add("integrity");
		this.add("integrityOptions");
		this.add("patronFormat");
		this.add("patronHeaderVersion");
		this.add("payLoad");
		this.add("processedLevel");
		this.add("product");
		this.add("purpose");
		this.add("quality");
		this.add("qualityAlgorithm");
		this.add("records");
		this.add("sbBuffer");
		this.add("sbFormat");
	}

}
