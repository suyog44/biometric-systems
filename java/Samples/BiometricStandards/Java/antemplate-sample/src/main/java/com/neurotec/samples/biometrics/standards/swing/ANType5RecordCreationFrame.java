package com.neurotec.samples.biometrics.standards.swing;

import java.awt.Frame;
import java.nio.ByteBuffer;

import com.neurotec.biometrics.standards.ANBinaryImageCompressionAlgorithm;
import com.neurotec.biometrics.standards.ANRecord;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType5Record;
import com.neurotec.images.NImage;
import com.neurotec.samples.biometrics.standards.events.MainFrameEventListener;

public final class ANType5RecordCreationFrame extends ANImageBinaryRecordCreationFrame {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Public constructor
	// ==============================================

	public ANType5RecordCreationFrame(Frame owner, MainFrameEventListener listener) {
		super(owner, listener);
		setTitle("Add Type-5 ANRecord");
		setLowResolution(true);
	}

	// ==============================================
	// Protected overridden methods
	// ==============================================

	@Override
	protected ANRecord onCreateRecord(ANTemplate template) {
		ANType5Record record = null;
		if (isCreateFromImage()) {
			NImage image = getImage();
			if (image != null) {
				record = new ANType5Record(ANTemplate.VERSION_CURRENT, getIdc(), isIsrFlagUp(), (ANBinaryImageCompressionAlgorithm) getCompressionAlgorithm(), image);
			}
		} else {
			byte[] imageData = getImageData();
			if (imageData != null) {
				NImage image = NImage.fromMemory(ByteBuffer.wrap(imageData));
				record = new ANType5Record(ANTemplate.VERSION_CURRENT, getIdc(), isIsrFlagUp(), (ANBinaryImageCompressionAlgorithm) getCompressionAlgorithm(), image);
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
		return ANBinaryImageCompressionAlgorithm.class;
	}

}
