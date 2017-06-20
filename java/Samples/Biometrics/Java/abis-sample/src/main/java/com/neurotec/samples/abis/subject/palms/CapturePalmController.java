package com.neurotec.samples.abis.subject.palms;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NPalm;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.CaptureErrorHandler;
import com.neurotec.samples.abis.subject.DefaultBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.util.concurrent.CompletionHandler;

import java.io.File;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

public class CapturePalmController extends DefaultBiometricController implements CaptureBiometricController, CompletionHandler<NBiometricTask, Object> {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final CapturePalmModel model;
	private CapturePalmView view;
	private CaptureErrorHandler errorHandler;
	private int sessionId = -1;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CapturePalmController(CapturePalmModel model) {
		super(model);
		if (model == null) {
			throw new NullPointerException("model");
		}
		this.model = model;
		this.errorHandler = new CaptureErrorHandler(model);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setView(CapturePalmView view) {
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

		boolean generalize = view.isGeneralize();
		int sessionId = generalize ? ++this.sessionId : -1;
		int count = generalize ? SettingsManager.getPalmsGeneralizationRecordCount() : 1;
		List<NBiometric> nowCapturing = new ArrayList<NBiometric>();
		boolean fromFile = source == Source.FILE;
		NBiometricTask task;
		if (source == Source.FILE) {
			List<String> files = new ArrayList<String>();
			while (count > files.size()){
				File file = view.getFile();
				if (file == null) {
					view.captureFailed(null, "No file selected");
					return;
				} else {
					files.add(file.getAbsolutePath());
				}
			}
			for (int i = 0; i < count; i++) {
				NPalm palm = new NPalm();
				palm.setSessionId(sessionId);
				palm.setFileName(files.get(i));
				palm.setPosition(view.getPosition());
				palm.setImpressionType(view.getImpressionType());
				model.getLocalSubject().getPalms().add(palm);
				nowCapturing.add(palm);
			}
		} else if (source == Source.DEVICE) {
			for (int i = 0; i < count; i++) {
				NPalm palm = new NPalm();
				palm.setSessionId(sessionId);
				palm.setPosition(view.getPosition());
				palm.setImpressionType(view.getImpressionType());
				palm.setCaptureOptions(view.getCaptureOptions());
				model.getLocalSubject().getPalms().add(palm);
				nowCapturing.add(palm);
			}
		} else {
			throw new AssertionError("Unknown source: " + source);
		}
		if (generalize) {
			view.updateGeneralization(nowCapturing, null);
		}
		EnumSet<NBiometricOperation> operations = EnumSet.of(fromFile ? NBiometricOperation.CREATE_TEMPLATE : NBiometricOperation.CAPTURE, NBiometricOperation.CREATE_TEMPLATE);
		task = model.getClient().createTask(operations, model.getLocalSubject());
		model.getClient().performTask(task, null, this);
	}

	@Override
	public void finish() {
		List<NPalm> palms = new ArrayList<NPalm>(model.getLocalSubject().getPalms());
		model.getLocalSubject().getPalms().clear();
		model.getSubject().getPalms().addAll(palms);
	}

	@Override
	public void completed(NBiometricTask task, Object attachment) {
		NBiometricStatus status = task.getStatus();
		Throwable error = task.getError();
		errorHandler.handleError(error, status);
		if (error == null) {
			view.captureCompleted(status, task);
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
