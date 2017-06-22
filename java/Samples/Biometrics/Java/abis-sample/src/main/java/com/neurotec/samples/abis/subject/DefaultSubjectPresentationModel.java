package com.neurotec.samples.abis.subject;

import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.List;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NSubject;
import com.neurotec.samples.abis.AbisController;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.subject.faces.CaptureFaceController;
import com.neurotec.samples.abis.subject.faces.CaptureFaceModel;
import com.neurotec.samples.abis.subject.faces.CaptureFacePage;
import com.neurotec.samples.abis.subject.fingers.CaptureFingerController;
import com.neurotec.samples.abis.subject.fingers.CaptureFingerModel;
import com.neurotec.samples.abis.subject.fingers.CaptureFingersPage;
import com.neurotec.samples.abis.subject.irises.CaptureIrisController;
import com.neurotec.samples.abis.subject.irises.CaptureIrisModel;
import com.neurotec.samples.abis.subject.irises.CaptureIrisPage;
import com.neurotec.samples.abis.subject.palms.CapturePalmController;
import com.neurotec.samples.abis.subject.palms.CapturePalmModel;
import com.neurotec.samples.abis.subject.palms.CapturePalmsPage;
import com.neurotec.samples.abis.subject.voices.CaptureVoiceController;
import com.neurotec.samples.abis.subject.voices.CaptureVoiceModel;
import com.neurotec.samples.abis.subject.voices.CaptureVoicePage;
import com.neurotec.samples.abis.swing.Node;
import com.neurotec.samples.abis.swing.SubjectTree;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.ClassInstanceContainer;
import com.neurotec.samples.util.LicenseManager;

public final class DefaultSubjectPresentationModel implements SubjectPresentationModel, PropertyChangeListener {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final List<SubjectPresentationListener> presentationListeners;
	private BiometricModel model;
	private final AbisModel abisModel;
	private final AbisController abisController;

	private String presentationTitle;
	private Object selectedSubjectElement;
	private Page shownPage;
	private EnumSet<NBiometricType> allowedNewTypes;

	private final ClassInstanceContainer pages;
	private final ClassInstanceContainer models;
	private final ClassInstanceContainer controllers;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public DefaultSubjectPresentationModel(BiometricModel model, AbisModel abisModel, AbisController abisController) {
		this.model = model;
		this.abisModel = abisModel;
		this.abisController = abisController;
		presentationListeners = new ArrayList<SubjectPresentationListener>();
		presentationTitle = "Subject";
		updateAllowedNewTypes();
		pages = new ClassInstanceContainer();
		models = new ClassInstanceContainer();
		controllers = new ClassInstanceContainer();
		navigateToStartPage();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private Page getPage(Node selected) {
		if (selected == null || selected.isSubjectNode()) {
			SubjectOverviewPage page = getPage(SubjectOverviewPage.class);
			page.setBiometricModel(model);
			DefaultBiometricController subjectController = new DefaultBiometricController(model, abisController);
			subjectController.setEnrollDataSerializer(EnrollDataSerializer.getInstance());
			page.setBiometricController(subjectController);
			return page;
		} else {
			if (selected.isBiometricNode()) {
				BiometricPreviewPage page = getPage(BiometricPreviewPage.class);
				page.setNode(selected);
				return page;
			} else {
				if (selected.getBiometricType().contains(NBiometricType.FINGER)) {
					CaptureFingerModel captureModel = getModel(CaptureFingerModel.class);
					CaptureFingersPage page = getPage(CaptureFingersPage.class);
					page.setBiometricModel(captureModel);
					CaptureFingerController controller = getController(CaptureFingerController.class, captureModel);
					controller.setView(page);
					page.setBiometricController(controller);
					return page;
				} else if (selected.getBiometricType().contains(NBiometricType.FACE)) {
					CaptureFaceModel captureModel = getModel(CaptureFaceModel.class);
					CaptureFacePage page = getPage(CaptureFacePage.class);
					page.setBiometricModel(captureModel);
					CaptureFaceController controller = getController(CaptureFaceController.class, captureModel);
					controller.setView(page);
					page.setBiometricController(controller);
					return page;
				} else if (selected.getBiometricType().contains(NBiometricType.IRIS)) {
					CaptureIrisModel captureModel = getModel(CaptureIrisModel.class);
					CaptureIrisPage page = getPage(CaptureIrisPage.class);
					page.setBiometricModel(captureModel);
					CaptureIrisController captureIrisController = getController(CaptureIrisController.class, captureModel);
					captureIrisController.setView(page);
					page.setBiometricController(captureIrisController);
					return page;
				} else if (selected.getBiometricType().contains(NBiometricType.PALM)) {
					CapturePalmModel captureModel = getModel(CapturePalmModel.class);
					CapturePalmsPage page = getPage(CapturePalmsPage.class);
					page.setBiometricModel(captureModel);
					CapturePalmController controller = getController(CapturePalmController.class, captureModel);
					controller.setView(page);
					page.setBiometricController(controller);
					return page;
				} else if (selected.getBiometricType().contains(NBiometricType.VOICE)) {
					CaptureVoiceModel captureModel = getModel(CaptureVoiceModel.class);
					CaptureVoicePage page = getPage(CaptureVoicePage.class);
					page.setBiometricModel(captureModel);
					CaptureVoiceController controller = getController(CaptureVoiceController.class, captureModel);
					controller.setView(page);
					page.setBiometricController(controller);
					return page;
				}
			}
		}
		return null;
	}

	private <T> T getPage(Class<T> type) {
		T page = pages.get(type);
		if (page == null) {
			try {
				page = type.getDeclaredConstructor(PageNavigationController.class).newInstance(this);
			} catch (Exception e) {
				e.printStackTrace();
			}
			pages.put(type, page);
		}
		return page;
	}

	private <T> T getModel(Class<T> type) {
		T page = models.get(type);
		if (page == null) {
			try {
				page = type.getDeclaredConstructor(NSubject.class, NSubject.class, AbisModel.class).newInstance(model.getSubject(), new NSubject(), abisModel);
			} catch (Exception e) {
				e.printStackTrace();
			}
			models.put(type, page);
		}
		return page;
	}

	private <T> T getController(Class<T> type, CaptureBiometricModel<?> captureBiometricModel) {
		T page = controllers.get(type);
		if (page == null) {
			try {
				page = type.getDeclaredConstructor(captureBiometricModel.getClass()).newInstance(captureBiometricModel);
			} catch (Exception e) {
				e.printStackTrace();
			}
			controllers.put(type, page);
		}
		return page;
	}

	private void updateAllowedNewTypes() {
		allowedNewTypes = EnumSet.noneOf(NBiometricType.class);
		if (LicenseManager.getInstance().isActivated("Biometrics.FingerExtraction", true) || !model.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			allowedNewTypes.add(NBiometricType.FINGER);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.FaceExtraction", true) || !model.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			allowedNewTypes.add(NBiometricType.FACE);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.IrisExtraction", true) || !model.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			allowedNewTypes.add(NBiometricType.IRIS);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.VoiceExtraction", true) || !model.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			allowedNewTypes.add(NBiometricType.VOICE);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.PalmExtraction", true) || !model.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			allowedNewTypes.add(NBiometricType.PALM);
		}
	}

	private void updatePresentationTitle(String id) {
		presentationTitle = "Subject";
		if (!id.isEmpty()) {
			presentationTitle += String.format(": %s", id);
		}
		firePresentationChanged();
	}

	private void firePresentationChanged() {
		for (SubjectPresentationListener listener : presentationListeners) {
			listener.subjectPresentationChanged();
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public String getPresentationTitle() {
		return presentationTitle;
	}

	@Override
	public Object getSelectedSubjectElement() {
		return selectedSubjectElement;
	}

	@Override
	public Page getShownPage() {
		return shownPage;
	}

	@Override
	public EnumSet<NBiometricType> getAllowedNewTypes() {
		return allowedNewTypes.clone();
	}

	@Override
	public void dispose() {
		presentationListeners.clear();
		pages.clear();
		models.clear();
		controllers.clear();
		shownPage = null;
	}

	@Override
	public void navigateToPage(Object pageObject) {
		if ((selectedSubjectElement == null) || !selectedSubjectElement.equals(pageObject)) {
			selectedSubjectElement = pageObject;
		}
		shownPage = getPage((Node) pageObject);
		firePresentationChanged();
	}

	@Override
	public void navigateToStartPage() {
		navigateToPage(null);
	}

	@Override
	public void propertyChange(PropertyChangeEvent ev) {
		if ((ev.getOldValue() == ev.getNewValue()) || ((ev.getOldValue() != null) && (ev.getNewValue() != null) && ev.getOldValue().equals(ev.getNewValue()))) {
			return;
		}
		if (SubjectTree.PROPERTY_CHANGE_SUBJECT.equals(ev.getPropertyName())) {
			pages.clear();
			model = new DefaultBiometricModel((NSubject) ev.getNewValue(), abisModel);
			updateAllowedNewTypes();
			if (model.getSubject() == null) {
				updatePresentationTitle("");
				selectedSubjectElement = null;
				shownPage = null;
				firePresentationChanged();
			} else {
				updatePresentationTitle(model.getSubject().getId());
				navigateToStartPage();
			}
		} else if (SubjectTree.PROPERTY_CHANGE_SELECTED_ITEM.equals(ev.getPropertyName())) {
			onSubjectTreeSelectionItemChanged((Node) ev.getNewValue());
		} else if (ev.getSource().equals(model)) {
			if ("SubjectID".equals(ev.getPropertyName())) {
				updatePresentationTitle((String) ev.getNewValue());
			}
		}
	}

	private void onSubjectTreeSelectionItemChanged(Node selected) {
		if (selected != null && !selected.equals(selectedSubjectElement)) {
			selectedSubjectElement = selected;
			firePresentationChanged();
			navigateToPage(selectedSubjectElement);
		} else {
			navigateToStartPage();
		}
	}

	public void addPresentationListener(SubjectPresentationListener listener) {
		presentationListeners.add(listener);
	}

	public void removePresentationListener(SubjectPresentationListener listener) {
		presentationListeners.remove(listener);
	}

}
