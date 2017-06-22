package com.neurotec.samples.abis.subject;

import java.io.File;
import java.io.IOException;
import java.util.EnumSet;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.images.NImage;
import com.neurotec.images.NImageFormat;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.samples.abis.AbisController;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.TenPrintCard;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.TenPrintCardPrinter;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.util.NPropertyBag;
import com.neurotec.util.concurrent.CompletionHandler;

public class DefaultBiometricController implements BiometricController {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final BiometricModel model;
	private AbisController abisController;
	private EnrollDataSerializer enrollDataSerializer;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public DefaultBiometricController(BiometricModel model) {
		this(model, null);
	}

	public DefaultBiometricController(BiometricModel model, AbisController abisController) {
		if (model == null) {
			throw new NullPointerException("model");
		}
		this.model = model;
		this.abisController = abisController;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setEnrollDataSerializer(EnrollDataSerializer enrollDataSerializer) {
		this.enrollDataSerializer = enrollDataSerializer;
	}

	@Override
	public final void setID(String id) {
		if (id == null)	{
			throw new NullPointerException("id");
		}
		String oldID = model.getSubject().getId();
		model.getSubject().setId(id);
		model.firePropertyChange("SubjectID", oldID, id);
	}

	@Override
	public void setThumbnail(NImage thumbnail) {
		if (model.getDatabaseSchema().getThumbnailDataName().isEmpty()) {
			throw new IllegalStateException("No thumbnail column in database schema");
		}
		if (thumbnail == null) {
			model.getSubject().setProperty(model.getDatabaseSchema().getThumbnailDataName(), NBuffer.getEmpty());
		} else {
			NImageFormat format = thumbnail.getInfo().getFormat();
			if (format == null || !format.isCanWrite()) {
				format = NImageFormat.getPNG();
			}
			model.getSubject().setProperty(model.getDatabaseSchema().getThumbnailDataName(), thumbnail.save(format));
		}
	}

	@Override
	public void resetProperties(NPropertyBag properties) {
		model.getSubject().getProperties().clear();
		properties.applyTo(model.getSubject());
	}

	@Override
	public void prepareEnrollData() {
		NBuffer buffer = enrollDataSerializer.serialize(model.getSubject(), LicenseManager.getInstance().isActivated("Images.WSQ", true));
		model.getSubject().setProperty(model.getDatabaseSchema().getEnrollDataName(), buffer);
	}

	@Override
	public final void enroll(boolean checkDuplicates) {
		NBiometricTask task;
		if (checkDuplicates) {
			task = model.getClient().createTask(EnumSet.of(NBiometricOperation.ENROLL_WITH_DUPLICATE_CHECK), model.getSubject());
		} else {
			task = model.getClient().createTask(EnumSet.of(NBiometricOperation.ENROLL), model.getSubject());
		}
		CompletionHandler<NBiometricTask, Void> callback = null;
		if (abisController != null) {
			callback = abisController.databaseOperation("Enroll: " + model.getSubject().getId(), "Enrolling...", model.getSubject());
		}
		model.getSubject().setQueryString(null);
		model.getClient().performTask(task, null, callback);
	}

	@Override
	public final void identify() {
		identify(null);
	}

	@Override
	public final void identify(String query) {
		NBiometricTask task = model.getClient().createTask(EnumSet.of(NBiometricOperation.IDENTIFY), model.getSubject());
		CompletionHandler<NBiometricTask, Void> callback = null;
		if (abisController != null) {
			String name = model.getSubject().getId() == null ? "Identify" : "Identify: " + model.getSubject().getId();
			callback = abisController.databaseOperation(name, "Identifying...", model.getSubject());
		}
		model.getSubject().setQueryString(query);
		model.getClient().performTask(task, null, callback);
	}

	@Override
	public final void verify() {
		NBiometricTask task = model.getClient().createTask(EnumSet.of(NBiometricOperation.VERIFY), model.getSubject());
		CompletionHandler<NBiometricTask, Void> callback = null;
		if (abisController != null) {
			callback = abisController.databaseOperation("Verify: " + model.getSubject().getId(), "Verifying...", model.getSubject());
		}
		model.getSubject().setQueryString(null);
		model.getClient().performTask(task, null, callback);
	}

	@Override
	public final void update() {
		NBiometricTask task = model.getClient().createTask(EnumSet.of(NBiometricOperation.UPDATE), model.getSubject());
		CompletionHandler<NBiometricTask, Void> callback = null;
		if (abisController != null) {
			callback = abisController.databaseOperation("Update: " + model.getSubject().getId(), "Updating...", model.getSubject());
		}
		model.getSubject().setQueryString(null);
		model.getClient().performTask(task, null, callback);
	}

	@Override
	public void printTenPrintCard(final TenPrintCardType type) {
		try {
			TenPrintCard card = TenPrintCard.fromSubject(model.getSubject());
			TenPrintCardPrinter printer = new TenPrintCardPrinter(card);
			printer.printCard(type);
		} catch (IOException e) {
			e.printStackTrace();
			throw new IllegalStateException("Error reading card form: " + e.getMessage());
		}
	}

	@Override
	public final void saveTemplate(File file) throws IOException {
		if (file == null) {
			throw new NullPointerException("file");
		}
		NFile.writeAllBytes(file.getAbsolutePath(), model.getSubject().getTemplateBuffer());
	}

	@Override
	public void forceStart() {
		model.getClient().forceStart();
	}

	@Override
	public void force() {
		model.getClient().force();
	}

	@Override
	public void repeat() {
		model.getClient().repeat();
	}

	@Override
	public void skip() {
		model.getClient().skip();
	}

	@Override
	public void cancel() {
		model.getClient().cancel();
	}

}
