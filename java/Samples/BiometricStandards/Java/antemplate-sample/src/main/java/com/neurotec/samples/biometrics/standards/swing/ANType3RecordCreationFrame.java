package com.neurotec.samples.biometrics.standards.swing;

import java.awt.Frame;
import java.nio.ByteBuffer;

import com.neurotec.biometrics.standards.ANImageCompressionAlgorithm;
import com.neurotec.biometrics.standards.ANRecord;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType3Record;
import com.neurotec.images.NImage;
import com.neurotec.samples.biometrics.standards.events.MainFrameEventListener;

public final class ANType3RecordCreationFrame extends ANImageBinaryRecordCreationFrame {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public constructor
	// ==============================================

	public ANType3RecordCreationFrame(Frame owner, MainFrameEventListener listener) {
		super(owner, listener);
		setTitle("Add Type-3 ANRecord");
		setLowResolution(true);
	}

	// ==============================================
	// Overridden protected methods
	// ==============================================

	@Override
	protected ANRecord onCreateRecord(ANTemplate template) {
		ANType3Record record = null;
		if (isCreateFromImage()) {
			NImage image = getImage();
			if (image != null) {
				record = new ANType3Record(ANTemplate.VERSION_CURRENT, getIdc(), isIsrFlagUp(), (ANImageCompressionAlgorithm) getCompressionAlgorithm(), image);
			}
		} else {
			byte[] imageData = getImageData();
			if (imageData != null) {
				NImage image = NImage.fromMemory(ByteBuffer.wrap(imageData));
				record = new ANType3Record(ANTemplate.VERSION_CURRENT, getIdc(), isIsrFlagUp(), (ANImageCompressionAlgorithm) getCompressionAlgorithm(), image);
				record.setHorzLineLength((short) getHll());
				record.setVertLineLength((short) getVll());
			}
		}
		if (record != null) {
			getTemplate().getRecords().add(record);
		}
		return record;
	}

	@Override
	protected Class<?> getCompressionFormatsType() {
		return ANImageCompressionAlgorithm.class;
	}

}
