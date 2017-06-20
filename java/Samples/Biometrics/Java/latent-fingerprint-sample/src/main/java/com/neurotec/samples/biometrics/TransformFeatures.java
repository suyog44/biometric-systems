package com.neurotec.samples.biometrics;

import com.neurotec.biometrics.NFCore;
import com.neurotec.biometrics.NFDelta;
import com.neurotec.biometrics.NFDoubleCore;
import com.neurotec.biometrics.NFMinutia;
import com.neurotec.biometrics.NFRecord;
import com.neurotec.images.NImage;

public final class TransformFeatures {

	// ==============================================
	// Private static fields
	// ==============================================

	private static TransformFeatures defaultInstance;

	// ==============================================
	// Public static methods
	// ==============================================

	public static TransformFeatures getInstance() {
		synchronized (TransformFeatures.class) {
			if (defaultInstance == null) {
				defaultInstance = new TransformFeatures();
			}
			return defaultInstance;
		}

	}

	// ==============================================
	// Private constructor
	// ==============================================

	private TransformFeatures() {

	}

	// ==============================================
	// Private methods
	// ==============================================

	private double flipFeatureAngleHorizontally(double angle) {
		angle = Math.PI - angle;
		while (angle > Math.PI) {
			angle -= Math.PI * 2;
		}
		return angle;

	}

	private double flipFeatureAngleVertically(double angle) {
		return -angle;
	}

	private double rotateFeatureAngle(double angle, double rotateAngle) {

		angle -= rotateAngle;
		if (angle < -Math.PI) {
			angle += Math.PI * 2;
		} else if (angle > Math.PI) {
			angle -= Math.PI * 2;
		}
		return angle;

	}

	// ==============================================
	// Public methods
	// ==============================================

	public NFRecord flipHorizontally(NFRecord template) {
		NFRecord record = new NFRecord(template.getWidth(), template.getHeight(), template.getHorzResolution(), template.getVertResolution());
		record.setMinutiaFormat(template.getMinutiaFormat());
		record.setCBEFFProductType(template.getCBEFFProductType());

		int templateWidth = (int) ((double) template.getWidth() * NFRecord.RESOLUTION / template.getHorzResolution());
		for (NFMinutia minutia : template.getMinutiae()) {
			NFMinutia newMinutia = minutia;
			newMinutia.x = (short) (templateWidth - minutia.x - 1);
			newMinutia.setAngle(flipFeatureAngleHorizontally(minutia.getAngle()));
			record.getMinutiae().add(newMinutia);
		}
		for (NFDelta delta : template.getDeltas()) {
			NFDelta newDelta = delta;
			newDelta.x = (short) (templateWidth - delta.x - 1);
			newDelta.setAngle1(flipFeatureAngleHorizontally(delta.getAngle1()));
			newDelta.setAngle2(flipFeatureAngleHorizontally(delta.getAngle2()));
			newDelta.setAngle3(flipFeatureAngleHorizontally(delta.getAngle3()));

			record.getDeltas().add(newDelta);
		}
		for (NFCore core : template.getCores()) {
			NFCore newCore = core;
			newCore.x = (short) (templateWidth - core.x - 1);
			newCore.setAngle(flipFeatureAngleHorizontally(core.getAngle()));

			record.getCores().add(newCore);
		}
		for (NFDoubleCore doubleCore : template.getDoubleCores()) {
			NFDoubleCore newDoubleCore = doubleCore;
			newDoubleCore.x = (short) (templateWidth - doubleCore.x - 1);
			record.getDoubleCores().add(newDoubleCore);
		}

		return record;
	}

	public NFRecord flipVertically(NFRecord template) {
		NFRecord record = new NFRecord(template.getWidth(), template.getHeight(), template.getHorzResolution(), template.getVertResolution());
		record.setMinutiaFormat(template.getMinutiaFormat());
		record.setCBEFFProductType(template.getCBEFFProductType());

		int templateHeight = (int) ((double) template.getHeight() * NFRecord.RESOLUTION / template.getVertResolution());
		for (NFMinutia minutia : template.getMinutiae()) {
			NFMinutia newMinutia = minutia;
			newMinutia.y = (short) (templateHeight - minutia.y - 1);
			newMinutia.setAngle(flipFeatureAngleVertically(minutia.getAngle()));
			record.getMinutiae().add(newMinutia);
		}
		for (NFDelta delta : template.getDeltas()) {
			NFDelta newDelta = delta;
			newDelta.y = (short) (templateHeight - delta.y - 1);
			newDelta.setAngle1(flipFeatureAngleVertically(delta.getAngle1()));
			newDelta.setAngle2(flipFeatureAngleVertically(delta.getAngle2()));
			newDelta.setAngle3(flipFeatureAngleVertically(delta.getAngle3()));
			record.getDeltas().add(newDelta);
		}
		for (NFCore core : template.getCores()) {
			NFCore newCore = core;
			newCore.y = (short) (templateHeight - core.y - 1);
			newCore.setAngle(flipFeatureAngleVertically(core.getAngle()));
			record.getCores().add(newCore);
		}
		for (NFDoubleCore doubleCore : template.getDoubleCores()) {
			NFDoubleCore newDoubleCore = doubleCore;
			newDoubleCore.y = (short) (templateHeight - doubleCore.y - 1);
			record.getDoubleCores().add(newDoubleCore);
		}

		return record;
	}

	public NFRecord rotate90(NFRecord template) {
		NFRecord record = new NFRecord(template.getHeight(), template.getWidth(), template.getVertResolution(), template.getHorzResolution());
		record.setMinutiaFormat(template.getMinutiaFormat());
		record.setCBEFFProductType(template.getCBEFFProductType());

		int templateHeight = (int) ((double) template.getHeight() * NFRecord.RESOLUTION / template.getVertResolution());
		int oldX;
		int oldY;
		for (NFMinutia minutia : template.getMinutiae()) {
			oldX = minutia.x;
			oldY = minutia.y;
			NFMinutia newMinutia = minutia;
			newMinutia.x = (short) (templateHeight - oldY - 1);
			newMinutia.y = (short) (oldX);
			newMinutia.setAngle(rotateFeatureAngle(minutia.getAngle(), -Math.PI / 2));
			record.getMinutiae().add(newMinutia);
		}
		for (NFDelta delta : template.getDeltas()) {
			oldX = delta.x;
			oldY = delta.y;
			NFDelta newDelta = delta;
			newDelta.x = (short) (templateHeight - oldY - 1);
			newDelta.y = (short) (oldX);
			newDelta.setAngle1(rotateFeatureAngle(delta.getAngle1(), -Math.PI / 2));
			newDelta.setAngle2(rotateFeatureAngle(delta.getAngle2(), -Math.PI / 2));
			newDelta.setAngle3(rotateFeatureAngle(delta.getAngle3(), -Math.PI / 2));
			record.getDeltas().add(newDelta);
		}
		for (NFCore core : template.getCores()) {
			oldX = core.x;
			oldY = core.y;
			NFCore newCore = core;
			newCore.x = (short) (templateHeight - oldY - 1);
			newCore.y = (short) (oldX);
			newCore.setAngle(rotateFeatureAngle(core.getAngle(), -Math.PI / 2));
			record.getCores().add(newCore);
		}
		for (NFDoubleCore doubleCore : template.getDoubleCores()) {
			oldX = doubleCore.x;
			oldY = doubleCore.y;
			NFDoubleCore newDoubleCore = doubleCore;
			newDoubleCore.x = (short) (templateHeight - oldY - 1);
			newDoubleCore.y = (short) (oldX);
			record.getDoubleCores().add(newDoubleCore);
		}

		return record;
	}

	public NFRecord rotate180(NFRecord template) {
		NFRecord record = new NFRecord(template.getWidth(), template.getHeight(), template.getHorzResolution(), template.getVertResolution());
		record.setMinutiaFormat(template.getMinutiaFormat());
		record.setCBEFFProductType(template.getCBEFFProductType());

		int templateWidth = (int) ((double) template.getWidth() * NFRecord.RESOLUTION / template.getHorzResolution());
		int templateHeight = (int) ((double) template.getHeight() * NFRecord.RESOLUTION / template.getVertResolution());
		int oldX;
		int oldY;
		for (NFMinutia minutia : template.getMinutiae()) {
			oldX = minutia.x;
			oldY = minutia.y;
			NFMinutia newMinutia = minutia;
			newMinutia.x = (short) (templateWidth - oldX - 1);
			newMinutia.y = (short) (templateHeight - oldY - 1);
			newMinutia.setAngle(rotateFeatureAngle(minutia.getAngle(), Math.PI));
			record.getMinutiae().add(newMinutia);
		}
		for (NFDelta delta : template.getDeltas()) {
			oldX = delta.x;
			oldY = delta.y;
			NFDelta newDelta = delta;
			newDelta.x = (short) (templateWidth - oldX - 1);
			newDelta.y = (short) (templateHeight - oldY - 1);
			newDelta.setAngle1(rotateFeatureAngle(delta.getAngle1(), Math.PI));
			newDelta.setAngle2(rotateFeatureAngle(delta.getAngle2(), Math.PI));
			newDelta.setAngle3(rotateFeatureAngle(delta.getAngle3(), Math.PI));
			record.getDeltas().add(newDelta);
		}
		for (NFCore core : template.getCores()) {
			oldX = core.x;
			oldY = core.y;
			NFCore newCore = core;
			newCore.x = (short) (templateWidth - oldX - 1);
			newCore.y = (short) (templateHeight - oldY - 1);
			newCore.setAngle(rotateFeatureAngle(core.getAngle(), Math.PI));
			record.getCores().add(newCore);
		}
		for (NFDoubleCore doubleCore : template.getDoubleCores()) {
			oldX = doubleCore.x;
			oldY = doubleCore.y;
			NFDoubleCore newDoubleCore = doubleCore;
			newDoubleCore.x = (short) (templateWidth - oldX - 1);
			newDoubleCore.y = (short) (templateHeight - oldY - 1);
			record.getDoubleCores().add(newDoubleCore);
		}

		return record;
	}

	public NFRecord rotate270(NFRecord template) {
		NFRecord record = new NFRecord(template.getHeight(), template.getWidth(), template.getVertResolution(), template.getHorzResolution());
		record.setMinutiaFormat(template.getMinutiaFormat());
		record.setCBEFFProductType(template.getCBEFFProductType());

		int templateWidth = (int) ((double) template.getWidth() * NFRecord.RESOLUTION / template.getHorzResolution());
		int oldX;
		int oldY;
		for (NFMinutia minutia : template.getMinutiae()) {
			oldX = minutia.x;
			oldY = minutia.y;
			NFMinutia newMinutia = minutia;
			newMinutia.x = (short) (oldY);
			newMinutia.y = (short) (templateWidth - oldX - 1);
			newMinutia.setAngle(rotateFeatureAngle(minutia.getAngle(), Math.PI / 2));
			record.getMinutiae().add(newMinutia);
		}
		for (NFDelta delta : template.getDeltas()) {
			oldX = delta.x;
			oldY = delta.y;
			NFDelta newDelta = delta;
			newDelta.x = (short) (oldY);
			newDelta.y = (short) (templateWidth - oldX - 1);
			newDelta.setAngle1(rotateFeatureAngle(delta.getAngle1(), Math.PI / 2));
			newDelta.setAngle2(rotateFeatureAngle(delta.getAngle2(), Math.PI / 2));
			newDelta.setAngle3(rotateFeatureAngle(delta.getAngle3(), Math.PI / 2));
			record.getDeltas().add(newDelta);
		}
		for (NFCore core : template.getCores()) {
			oldX = core.x;
			oldY = core.y;
			NFCore newCore = core;
			newCore.x = (short) (oldY);
			newCore.y = (short) (templateWidth - oldX - 1);
			newCore.setAngle(rotateFeatureAngle(core.getAngle(), Math.PI / 2));
			record.getCores().add(newCore);
		}
		for (NFDoubleCore doubleCore : template.getDoubleCores()) {
			oldX = doubleCore.x;
			oldY = doubleCore.y;
			NFDoubleCore newDoubleCore = doubleCore;
			newDoubleCore.x = (short) (oldY);
			newDoubleCore.y = (short) (templateWidth - oldX - 1);
			record.getDoubleCores().add(newDoubleCore);
		}

		return record;
	}

	public NFRecord crop(NFRecord template, NImage image, int x, int y, int width, int height) {
		NFRecord record = new NFRecord((short) image.getWidth(), (short) image.getHeight(), template.getHorzResolution(), template.getVertResolution());
		record.setMinutiaFormat(template.getMinutiaFormat());
		record.setCBEFFProductType(template.getCBEFFProductType());

		for (NFMinutia minutia : template.getMinutiae()) {
			if (minutia.x > x && minutia.y > y && minutia.x - x < width && minutia.y - y < height) {
				NFMinutia newMinutia = minutia;
				newMinutia.x = (short) (minutia.x - x);
				newMinutia.y = (short) (minutia.y - y);
				record.getMinutiae().add(newMinutia);
			}
		}
		for (NFDelta delta : template.getDeltas()) {
			if (delta.x > x && delta.y > y && delta.x - x < width && delta.y - y < height) {
				NFDelta newDelta = delta;
				newDelta.x = (short) (delta.x - x);
				newDelta.y = (short) (delta.y - y);
				record.getDeltas().add(newDelta);
			}
		}
		for (NFCore core : template.getCores()) {
			if (core.x > x && core.y > y && core.x - x < width && core.y - y < height) {
				NFCore newCore = core;
				newCore.x = (short) (core.x - x);
				newCore.y = (short) (core.y - y);
				record.getCores().add(newCore);
			}
		}
		for (NFDoubleCore doubleCore : template.getDoubleCores()) {
			if (doubleCore.x > x && doubleCore.y > y && doubleCore.x - x < width && doubleCore.y - y < height) {
				NFDoubleCore newDoubleCore = doubleCore;
				newDoubleCore.x = (short) (doubleCore.x - x);
				newDoubleCore.y = (short) (doubleCore.y - y);
				record.getDoubleCores().add(newDoubleCore);
			}
		}

		return record;
	}

}
