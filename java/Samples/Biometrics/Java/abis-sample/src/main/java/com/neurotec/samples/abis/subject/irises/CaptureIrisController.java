package com.neurotec.samples.abis.subject.irises;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NIris;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.CaptureErrorHandler;
import com.neurotec.samples.abis.subject.DefaultBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.util.concurrent.CompletionHandler;

import java.io.File;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

public final class CaptureIrisController extends DefaultBiometricController implements CaptureBiometricController, CompletionHandler<NBiometricTask, Object> {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final CaptureIrisModel model;
	private CaptureIrisView view;
	private CaptureErrorHandler errorHandler;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureIrisController(CaptureIrisModel model) {
		super(model);
		if (model == null) {
			throw new NullPointerException("model");
		}
		this.model = model;
		this.errorHandler = new CaptureErrorHandler(model);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private NIris prepareIris() {
		NIris iris = new NIris();
		iris.setPosition(view.getPosition());
		iris.setCaptureOptions(view.getCaptureOptions());
		model.getLocalSubject().getIrises().add(iris);
		return iris;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setView(CaptureIrisView view) {
		if (view == null) {
			throw new NullPointerException("view");
		}
		this.view = view;
	}

	@Override
	public void capture() {
		Source source = view.getSource();
		if ((source != Source.FILE) && (source != Source.DEVICE)) {
			throw new IllegalArgumentException("source");
		}
		model.getLocalSubject().clear();
		NBiometricTask task;
		if (source == Source.FILE) {
			File file = view.getFile();
			if (file == null) {
				view.captureFailed(null, "no file selected");
				return;
			} else {
				NIris iris = prepareIris();
				iris.setFileName(file.getAbsolutePath());
				task = model.getClient().createTask(EnumSet.of(NBiometricOperation.CREATE_TEMPLATE), model.getLocalSubject());
			}
		} else if (source == Source.DEVICE) {
			prepareIris();
			task = model.getClient().createTask(EnumSet.of(NBiometricOperation.CAPTURE, NBiometricOperation.CREATE_TEMPLATE), model.getLocalSubject());
		} else {
			throw new AssertionError("Unknown source: " + source);
		}
		model.getClient().performTask(task, source, this);
	}

	@Override
	public void finish() {
		List<NIris> irises = new ArrayList<NIris>(model.getLocalSubject().getIrises());
		model.getLocalSubject().getIrises().clear();
		model.getSubject().getIrises().addAll(irises);
	}

	@Override
	public void completed(NBiometricTask task, Object attachment) {
		NBiometricStatus status = task.getStatus();
		Throwable error = task.getError();
		errorHandler.handleError(error, status);
		if (error == null) {
			view.captureCompleted(status);
		} else {
			view.captureFailed(error, error.toString());
		}
	}

	@Override
	public void failed(Throwable th, Object attachment) {
		errorHandler.handleError(th, null);
		view.captureFailed(th, th.toString());
	}

}
