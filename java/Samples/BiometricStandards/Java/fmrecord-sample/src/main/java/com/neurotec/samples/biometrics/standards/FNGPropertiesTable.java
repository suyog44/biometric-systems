package com.neurotec.samples.biometrics.standards;

import java.util.ArrayList;

public final class FNGPropertiesTable extends ArrayList<String> {

	// ==============================================
	// Private static Fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public constructor
	// ==============================================

	public FNGPropertiesTable() {
		super();
		loadProperties();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void loadProperties() {

		// FMRecord
		this.add("captureEquipmentCompliance");
		this.add("captureEquipmentId");
		this.add("CBEFFProductId");
		this.add("certificationFlag");
		this.add("fingers");
		this.add("fingerViews");
		this.add("flags");
		this.add("resolutionX");
		this.add("resolutionY");
		this.add("sizeX");
		this.add("sizeY");
		this.add("standard");
		this.add("version");

		// FMRFinger
		this.add("position");
		this.add("fingerViews");
		this.add("owner");

		// FMRFingerView
		this.add("captureDateAndTime");
		this.add("captureDeviceTechnology");
		this.add("captureDeviceTypeId");
		this.add("captureDeviceVendorId");
		this.add("certificationBlocks");
		this.add("cores");
		this.add("deltas");
		this.add("fingerQuality");
		this.add("hasEightNeighborRidgeCounts");
		this.add("hasFourNeighborRidgeCounts");
		this.add("horzImageResolution");
		this.add("impressionType");
		this.add("minutiae");
		this.add("minutiaeEightNeighbors");
		this.add("minutiaeFourNeighbors");
		this.add("minutiaeQualityFlag");
		this.add("qualityBlocks");
		this.add("ridgeEndingType");
		this.add("sizeX");
		this.add("sizeY");
		this.add("vertImageResolution");
		this.add("viewCount");
		this.add("viewNumber");

		// Feature details
		this.add("delta");
		this.add("core");
		this.add("minutia");

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
