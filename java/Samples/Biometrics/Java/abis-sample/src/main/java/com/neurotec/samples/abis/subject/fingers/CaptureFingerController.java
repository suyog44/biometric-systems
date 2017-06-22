package com.neurotec.samples.abis.subject.fingers;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NSubject;
import com.neurotec.event.ChangeEvent;
import com.neurotec.event.ChangeListener;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.CaptureErrorHandler;
import com.neurotec.samples.abis.subject.DefaultBiometricController;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.TenPrintCardDialog;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.util.concurrent.CompletionHandler;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;

public class CaptureFingerController extends DefaultBiometricController implements CaptureBiometricController, CompletionHandler<NBiometricTask, Object>, ChangeListener {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final CaptureFingerModel model;
	private CaptureFingerView view;
	private boolean captureNeedsAction;
	private CaptureErrorHandler errorHandler;
	private int sessionId = -1;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureFingerController(CaptureFingerModel model) {
		super(model);
		this.model = model;
		this.errorHandler = new CaptureErrorHandler(model);
		copyMissingFingerPositions(this.model.getLocalSubject(), this.model.getSubject());
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void copyMissingFingerPositions(NSubject dst, NSubject src) {
		if (dst == null) throw new NullPointerException("dst");
		if (src == null) throw new NullPointerException("src");
		dst.getMissingFingers().clear();
		for (NFPosition position : src.getMissingFingers()) {
			dst.getMissingFingers().add(position);
		}
	}

	private void captureStarted() {
		model.getClient().setCurrentBiometricCompletedTimeout(-1);
		model.getClient().addCurrentBiometricCompletedListener(this);
	}

	private void captureEnded() {
		model.getClient().setCurrentBiometricCompletedTimeout(0);
		model.getClient().removeCurrentBiometricCompletedListener(this);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setView(CaptureFingerView view) {
		if (view == null) throw new NullPointerException("view");
		this.view = view;
	}

	@Override
	public void capture() {
		boolean generalize = view.isGeneralize();
		int sessionId = generalize ? ++this.sessionId : -1;
		int count = generalize ? SettingsManager.getFingersGeneralizationRecordCount() : 1;
		List<NBiometric> nowCapturing = new ArrayList<NBiometric>();

		switch (view.getSource()) {
		case FILE:
			List<String> files = new ArrayList<String>();
			while (count > files.size()) {
				File file = view.getFile();
				if (file == null) {
					view.captureFailed(null, "No file selected");
					return;
				} else {
					files.add(file.getAbsolutePath());
				}
			}

			for (int i = 0; i < count; i++) {
				NFinger finger = new NFinger();
				finger.setSessionId(sessionId);
				finger.setFileName(files.get(i));
				finger.setPosition(view.getPosition());
				finger.setImpressionType(view.getImpressionType());
				model.getLocalSubject().getFingers().add(finger);
				nowCapturing.add(finger);
			}
			if (generalize) {
				view.updateGeneralization(nowCapturing, null);
			}
			break;
		case DEVICE:
			List<NFPosition> allowedPositions = view.getScenario().getAllowedPositions();
			if (allowedPositions.isEmpty()) {
				allowedPositions.add(NFPosition.UNKNOWN);
			}
			for (NFPosition position : allowedPositions) {
				if (!model.getLocalSubject().getMissingFingers().contains(position)) {
					for (int i = 0; i < count; i++) {
						NFinger finger = new NFinger();
						finger.setSessionId(sessionId);
						finger.setPosition(position);
						finger.setImpressionType(view.getImpressionType());
						finger.setCaptureOptions(view.getCaptureOptions());
						model.getLocalSubject().getFingers().add(finger);
						nowCapturing.add(finger);
					}
				}
			}
			break;
		case TEN_PRINT_CARD:
			TenPrintCardDialog dialog;
			try {
				dialog = new TenPrintCardDialog();
			} catch (IOException e) {
				errorHandler.handleError(e, NBiometricStatus.NONE);
				view.captureFailed(e, e.toString());
				return;
			}
			dialog.setModal(true);
			dialog.setLocationRelativeTo(null);
			dialog.setVisible(true);
			List<NFinger> fingers = dialog.getFingers();
			if (fingers == null) {
				view.captureFailed(null, "No file selected");
				return;
			} else {
				for (NFinger f : fingers) {
					model.getLocalSubject().getFingers().add(f);
				}
			}
			break;
		default:
			throw new IllegalArgumentException("Unknown source: " + view.getSource());
		}

		EnumSet<NBiometricOperation> operations = EnumSet.of(NBiometricOperation.CREATE_TEMPLATE);
		if (model.getClient().isFingersCalculateNFIQ()) {
			operations.add(NBiometricOperation.ASSESS_QUALITY);
		}
		NBiometricTask task = model.getClient().createTask(operations, model.getLocalSubject());
		captureStarted();
		model.getClient().performTask(task, view.getSource(), this);
	}

	@Override
	public void repeat() {
		captureNeedsAction = false;
		super.repeat();
	}

	@Override
	public void skip() {
		captureNeedsAction = false;
		super.skip();
	}

	@Override
	public void cancel() {
		captureNeedsAction = false;
		super.cancel();
	}

	@Override
	public void finish() {
		List<NFinger> fingers = new ArrayList<NFinger>(model.getLocalSubject().getFingers());
		model.getLocalSubject().getFingers().clear();
		model.getSubject().getFingers().addAll(fingers);
		copyMissingFingerPositions(model.getSubject(), model.getLocalSubject());
	}

	@Override
	public void completed(NBiometricTask task, Object attachment) {
		captureEnded();
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
		captureEnded();
		errorHandler.handleError(th, null);
		view.captureFailed(th, th.toString());
	}

	@Override
	public void stateChanged(ChangeEvent ev) {
		if (model.getClient().equals(ev.getSource())) {
			final NBiometricStatus status = model.getClient().getCurrentBiometric().getStatus();
			if ((status == NBiometricStatus.OK) || (status == NBiometricStatus.SPOOF_DETECTED) || (status == NBiometricStatus.SOURCE_ERROR) || (status == NBiometricStatus.CAPTURE_ERROR)) {
				model.getClient().force();
			} else {
				SwingUtils.runOnEDT(new Runnable() {
					@Override
					public void run() {
						captureNeedsAction = true;
						view.progress(status, String.format("Capturing failed: %s. Trying again ...", status));
					}
				});
				new Timer().schedule(new TimerTask() {
					@Override
					public void run() {
						if (captureNeedsAction) {
							model.getClient().repeat();
							captureNeedsAction = false;
						}
					}
				}, 3000);
			}
		}
	}
}
