package com.neurotec.samples.abis.subject.voices;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NVoice;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.CaptureErrorHandler;
import com.neurotec.samples.abis.subject.DefaultBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.util.concurrent.CompletionHandler;

import java.io.File;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

public final class CaptureVoiceController extends DefaultBiometricController implements CaptureBiometricController, CompletionHandler<NBiometricTask, Object> {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final CaptureVoiceModel model;
	private CaptureVoiceView view;
	private CaptureErrorHandler errorHandler;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureVoiceController(CaptureVoiceModel model) {
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

	private NVoice prepareVoice(Phrase phrase) {
		NVoice voice = new NVoice();
		model.getLocalSubject().getVoices().add(voice);
		if (phrase == null) {
			voice.setPhraseID(0);
		} else {
			voice.setPhraseID(phrase.getID());
		}
		voice.setCaptureOptions(view.getCaptureOptions());
		return voice;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setView(CaptureVoiceView view) {
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
		if (source == Source.FILE) {
			File file = view.getFile();
			if (file == null) {
				view.captureFailed(null, "No file selected");
				return;
			} else {
				NVoice voice = prepareVoice(view.getCurrentPhrase());
				voice.setFileName(file.getAbsolutePath());
			}
			NBiometricTask task = model.getClient().createTask(EnumSet.of(NBiometricOperation.SEGMENT, NBiometricOperation.CREATE_TEMPLATE), model.getLocalSubject());
			model.getClient().performTask(task, null, this);
		} else {
			prepareVoice(view.getCurrentPhrase());
			NBiometricTask task = model.getClient().createTask(EnumSet.of(NBiometricOperation.CAPTURE, NBiometricOperation.SEGMENT, NBiometricOperation.CREATE_TEMPLATE), model.getLocalSubject());
			model.getClient().performTask(task, null, this);
		}
	}

	@Override
	public void finish() {
		List<NVoice> voices = new ArrayList<NVoice>(model.getLocalSubject().getVoices());
		model.getLocalSubject().getVoices().clear();
		model.getSubject().getVoices().addAll(voices);
	}

	@Override
	public void completed(final NBiometricTask task, Object attachment) {
		NBiometricStatus status = task.getStatus();
		Throwable error = task.getError();
		errorHandler.handleError(error, status);
		if (error == null) {
			view.captureCompleted(status);
		} else {
			view.captureFailed(error, error.toString());
		}
		view.captureCompleted(status);
	}

	@Override
	public void failed(Throwable th, Object attachment) {
		errorHandler.handleError(th, null);
		view.captureFailed(th, th.toString());
	}

}
