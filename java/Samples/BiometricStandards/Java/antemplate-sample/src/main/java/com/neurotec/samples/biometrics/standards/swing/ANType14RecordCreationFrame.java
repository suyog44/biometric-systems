package com.neurotec.samples.biometrics.standards.swing;

import java.awt.Dimension;
import java.awt.Frame;
import java.io.IOException;
import java.nio.ByteBuffer;

import com.neurotec.biometrics.standards.ANImageColorSpace;
import com.neurotec.biometrics.standards.ANImageCompressionAlgorithm;
import com.neurotec.biometrics.standards.ANRecord;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANType14Record;
import com.neurotec.biometrics.standards.BDIFScaleUnits;
import com.neurotec.images.NImage;
import com.neurotec.samples.biometrics.standards.events.MainFrameEventListener;

public final class ANType14RecordCreationFrame extends ANRecordCreationFrame {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private ImageLoaderPanel imageLoader;

	// ==============================================
	// Public constructor
	// ==============================================

	public ANType14RecordCreationFrame(Frame owner, MainFrameEventListener listener) {
		super(owner, listener);
		setPreferredSize(new Dimension(320, 510));
		setTitle("Add Type-14 ANRecord");
		initializeComponents();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {

		imageLoader = new ImageLoaderPanel();
		imageLoader.setBpx(0);
		imageLoader.setColorSpace(ANImageColorSpace.UNSPECIFIED);
		imageLoader.setCompressionAlgorithm(ANImageCompressionAlgorithm.NONE);
		imageLoader.setHasBpx(true);
		imageLoader.setHasColorSpace(false);
		imageLoader.setHll(0);
		imageLoader.setHps(0);
		imageLoader.setScaleUnits(BDIFScaleUnits.NONE);
		imageLoader.setSrc("");
		imageLoader.setVll(0);
		imageLoader.setVps(0);

		getContentPane().add(imageLoader);
		imageLoader.setBounds(5, 55, 305, 390);
		getButtonPanel().setBounds(0, 450, 310, 25);
		pack();

	}

	// ==============================================
	// Protected overridden methods
	// ==============================================

	@Override
	protected ANRecord onCreateRecord(ANTemplate template) throws IOException {
		ANType14Record record = null;
		String source = imageLoader.getSrc();
		if (source != null) {
			if (imageLoader.isCreateFromImage()) {
				NImage image = imageLoader.getImage();
				if (image != null) {
					record = new ANType14Record(ANTemplate.VERSION_CURRENT, getIdc(), source, imageLoader.getScaleUnits(), imageLoader.getCompressionAlgorithm(), image);
				}
			} else {
				byte[] imageData = imageLoader.getImageData();
				if (imageData != null) {
					NImage image = NImage.fromMemory(ByteBuffer.wrap(imageData));
					record = new ANType14Record(ANTemplate.VERSION_CURRENT, getIdc(), source, imageLoader.getScaleUnits(), imageLoader.getCompressionAlgorithm(), image);
					record.setHorzLineLength((short) imageLoader.getHll());
					record.setVertLineLength((short) imageLoader.getVll());
					record.setHorzPixelScale((short) imageLoader.getHps());
					record.setVertPixelScale((short) imageLoader.getVps());
					record.setBitsPerPixel((byte) imageLoader.getBpx());
				}
			}
		}
		if (record != null) {
			getTemplate().getRecords().add(record);
		}
		return record;
	}

}
