package com.neurotec.samples.biometrics;

import com.neurotec.images.NImage;

public interface MainFrameEventListener {

	// ==============================================
	// Image loading related events
	// ==============================================

	void resolutionCheckCompleted(boolean isOK, double horzRes, double vertRes);

	// ==============================================
	// Image transformation related events
	// ==============================================

	void bandPassFilteringAccepted(NImage resultImage);

	// ==============================================
	// Extraction related events
	// ==============================================

	void extractionSettingsSelected(int value);
}
