package com.neurotec.samples.abis.subject.faces;

import java.awt.Color;
import java.io.File;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFace;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.CaptureErrorHandler;
import com.neurotec.samples.abis.subject.DefaultBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.samples.abis.subject.faces.CaptureFaceView.Operation;
import com.neurotec.util.concurrent.CompletionHandler;

public final class CaptureFaceController extends DefaultBiometricController implements CaptureBiometricController, CompletionHandler<NBiometricTask, Object> {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final CaptureFaceModel model;
	private CaptureFaceView view;
	private CaptureErrorHandler errorHandler;
	private int sessionId = -1;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureFaceController(CaptureFaceModel model) {
		super(model);
		if (model == null) throw new NullPointerException("model");
		this.model = model;
		this.errorHandler = new CaptureErrorHandler(model);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setView(CaptureFaceView view) {
		if (view == null) throw new NullPointerException("view");
		this.view = view;
	}

	@Override
	public void capture() {
		boolean generalize = view.isGeneralize();
		boolean icao = view.isIcao();
		int sessionId = generalize ? ++this.sessionId : -1;
		int count = generalize ? SettingsManager.getFacesGeneralizationRecordCount() : 1;
		List<NBiometric> nowCapturing = new ArrayList<NBiometric>();
		Source source = view.getSource();
		boolean fromFile = source == Source.FILE;
		boolean fromCamera = source == Source.DEVICE;
		if ((source != Source.FILE) && (source != Source.DEVICE) && (source != Source.VIDEO)) {
			throw new IllegalArgumentException("source");
		}
		if (view.getCaptureOptions().contains(NBiometricCaptureOption.MANUAL)) {
			view.setCurrentOperation(Operation.DETECTING);
		} else {
			view.setCurrentOperation(Operation.EXTARCTING);
		}
		model.getClient().setFacesCheckIcaoCompliance(icao);
		model.getLocalSubject().clear();
		NBiometricTask task;
		List<String> files = new ArrayList<String>();
		if (source == Source.FILE || source == Source.VIDEO) {
			while (count > files.size()){
				File file = view.getFile();
				if (file == null) {
					view.captureFailed(null, "No file selected");
					return;
				} else {
					files.add(file.getAbsolutePath());
				}
			}
		}
		for (int i = 0; i < count; i++) {
			NFace face = new NFace();
			face.setSessionId(sessionId);
			face.setFileName(!fromCamera ? files.get(i) : null);
			face.setCaptureOptions(view.getCaptureOptions());
			model.getLocalSubject().getFaces().add(face);
			nowCapturing.add(face);
		}
		if (generalize) {
			view.updateGeneralization(nowCapturing, null);
		}
		EnumSet<NBiometricOperation> operations = EnumSet.of(fromFile ? NBiometricOperation.CREATE_TEMPLATE : NBiometricOperation.CAPTURE, NBiometricOperation.CREATE_TEMPLATE);
		if (icao) {
			operations.add(NBiometricOperation.SEGMENT);
		}
		task = model.getClient().createTask(operations, model.getLocalSubject());
		view.setStatus(fromFile ? "Extracting template ..." : "Starting capturing ...", Color.ORANGE);
		model.getClient().performTask(task, source, this);
		view.captureStarted();
	}

	@Override
	public void forceStart() {
		super.forceStart();
		view.setCurrentOperation(Operation.EXTARCTING);
	}

	@Override
	public void force() {
		super.force();
		view.setCurrentOperation(Operation.DETECTING);
	}

	@Override
	public void finish() {
		List<NFace> faces = new ArrayList<NFace>(model.getLocalSubject().getFaces());
		model.getLocalSubject().getFaces().clear();
		model.getSubject().getFaces().addAll(faces);
	}

	@Override
	public void completed(NBiometricTask task, Object attachment) {
		view.setCurrentOperation(Operation.NONE);
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
		view.setCurrentOperation(Operation.NONE);
		errorHandler.handleError(th, null);
		view.captureFailed(th, th.toString());
	}
}
