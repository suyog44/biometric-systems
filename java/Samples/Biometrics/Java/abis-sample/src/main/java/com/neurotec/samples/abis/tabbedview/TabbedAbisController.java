package com.neurotec.samples.abis.tabbedview;

import java.awt.Component;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.concurrent.ExecutionException;

import javax.swing.ProgressMonitor;
import javax.swing.SwingWorker;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NSubject;
import com.neurotec.devices.NDevice;
import com.neurotec.lang.NCore;
import com.neurotec.samples.abis.AbisController;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.settings.SettingsTab;
import com.neurotec.samples.abis.subject.BiometricModel;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.samples.abis.subject.DefaultBiometricModel;
import com.neurotec.samples.abis.subject.DefaultSubjectPresentationModel;
import com.neurotec.samples.abis.subject.SubjectTab;
import com.neurotec.samples.abis.swing.CloseableTabHandle;
import com.neurotec.samples.abis.swing.SchemaBuilderTab;
import com.neurotec.samples.abis.swing.SubjectTree;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.util.LicenseManager;
import com.neurotec.swing.AboutBox;
import com.neurotec.util.NObjectCollection;
import com.neurotec.util.concurrent.CompletionHandler;

public final class TabbedAbisController implements AbisController, TabController {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final AbisTabbedPanel view;
	private final AbisModel model;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public TabbedAbisController(AbisTabbedPanel view, AbisModel model) {
		this.view = view;
		this.model = model;
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private Tab createTab(Class<? extends Tab> type, Object tabModel) {
		if (type == StartTab.class) {
			StartTab tab = new StartTab();
			tab.setController(this);
			return tab;
		} else if (type == LicenseLogTab.class) {
			return new LicenseLogTab(model);
		} else if (type == ChangeDatabaseTab.class) {
			ChangeDatabaseTab tab = new ChangeDatabaseTab(model, this);
			view.addTabNavigationListener(tab);
			return tab;
		} else if (type == SchemaBuilderTab.class) {
			if (!(tabModel instanceof DatabaseSchema)) {
				throw new IllegalArgumentException("tabModel");
			}
			SchemaBuilderTab tab = new SchemaBuilderTab(this, model);
			tab.setSchema((DatabaseSchema) tabModel);
			return tab;
		} else if (type == SubjectTab.class) {
			if (!(tabModel instanceof NSubject)) {
				throw new IllegalArgumentException("tabModel");
			}
			DefaultBiometricModel biometricModel = new DefaultBiometricModel((NSubject) tabModel, model);
			DefaultSubjectPresentationModel subjectPresentationModel = new DefaultSubjectPresentationModel(biometricModel, model, this);
			biometricModel.addPropertyChangeListener(subjectPresentationModel);
			SubjectTree subjectTree = new SubjectTree();
			subjectTree.setSubject(biometricModel.getSubject());
			SubjectTab tab = new SubjectTab(subjectTree, subjectPresentationModel);
			subjectTree.addPropertyChangeListener(subjectPresentationModel);
			subjectPresentationModel.addPresentationListener(tab);
			view.addTabNavigationListener(tab);
			return tab;
		} else if (type == SettingsTab.class) {
			SettingsTab tab = new SettingsTab(model.getClient(), this);
			view.addTabNavigationListener(tab);
			return tab;
		} else if (type == DatabaseOperationTab.class) {
			if (!(tabModel instanceof BiometricModel)) {
				throw new IllegalArgumentException("tabModel");
			}
			DatabaseOperationTab tab = new DatabaseOperationTab(this, (BiometricModel) tabModel);
			view.addTabNavigationListener(tab);
			return tab;
		} else if (type == MatchingResultTab.class) {
			if (!(tabModel instanceof NSubject[])) {
				throw new IllegalArgumentException("tabModel");
			}
			NSubject[] subjects = (NSubject[]) tabModel;
			CaptureBiometricModel<NBiometric> matchingResultModel = new CaptureBiometricModel<NBiometric>(subjects[1], subjects[0], model) {
				@Override
				public NObjectCollection<NBiometric> getRelevantBiometricCollection() {
					throw new UnsupportedOperationException();
				}

			};
			MatchingResultTab tab = new MatchingResultTab(matchingResultModel);
			view.addTabNavigationListener(tab);
			return tab;
		} else {
			throw new UnsupportedOperationException("Unsupported page type: " + type);
		}
	}

	private void closeTab(int index) {
		Component component = view.getTabbedPane().getComponentAt(index);
		if (component instanceof Tab) {
			closeTab((Tab) component);
		} else {
			throw new IllegalArgumentException(String.format("Component at %d is not a Tab.", index));
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public Tab showTab(Class<? extends Tab> type, boolean alwaysCreateNew, boolean closeable, Object tabModel) {
		if (!alwaysCreateNew) {
			for (Tab tab : view.getTabs()) {
				if (type.isInstance(tab)) {
					view.setSelectedTab(tab);
					return tab;
				}
			}
		}
		Tab tab = createTab(type, tabModel);
		if (closeable) {
			view.addCloseableTab(tab.getTitle(), tab);
		} else {

			// Insert after all other uncloseable tabs.
			int index = 0;
			boolean inserted = false;
			for (Tab t : view.getTabs()) {
				if (t.isCloseable()) {
					view.insertTab(tab.getTitle(), tab, index);
					inserted = true;
					break;
				}
				index++;
			}
			if (!inserted) {
				view.addTab(tab.getTitle(), tab);
			}
		}
		view.setSelectedTab(tab);
		return tab;
	}

	@Override
	public void closeTab(Tab tab) {
		view.removeTab(tab);
		if (tab instanceof TabNavigationListener) {
			view.removeTabNavigationListener((TabNavigationListener) tab);
		}
	}

	@Override
	public void start() {
		if (model.getClient() == null) {
			dispose();
		} else {
			view.setBusy(false);
			showTab(StartTab.class, false, false, null);
		}
	}

	@Override
	public void createNewSubject() {
		showTab(SubjectTab.class, true, true, new NSubject());
	}

	@Override
	public void openSubject() {
		final OpenSubjectDialog dialog = new OpenSubjectDialog();
		dialog.setLocationRelativeTo(null);
		dialog.setModal(true);
		dialog.setVisible(true);
		if (dialog.getAction() == DialogAction.OK) {
			final ProgressMonitor progressMonitor = new ProgressMonitor(view, "Opening subject", "", 0, 100);
			progressMonitor.setMillisToDecideToPopup(0);
			progressMonitor.setProgress(0);

			SwingWorker<Object, Integer> worker = new SwingWorker<Object, Integer>() {

				@Override
				protected Object doInBackground() throws Exception {
					setProgress(1);
					NSubject subject = NSubject.fromFile(dialog.getFileName(), (short) dialog.getFormatOwner(), (short) dialog.getFormatType());
					setProgress(50);

					// TODO: Initialize subject properly.
					subject.getFingers().size();

					NBiometricStatus status = model.getClient().createTemplate(subject);
					if ((status == NBiometricStatus.NONE) || (status == NBiometricStatus.OK)) {
						return subject;
					} else {
						return status;
					}
				}

				@Override
				protected void done() {
					super.done();
					setProgress(100);
					Object result;
					try {
						result = get();
					} catch (InterruptedException e) {
						e.printStackTrace();
						return;
					} catch (ExecutionException e) {
						MessageUtils.showError(view, e.getCause());
						return;
					}
					if (result instanceof NSubject) {
						showTab(SubjectTab.class, true, true, result);
					} else if (result instanceof NBiometricStatus) {
						MessageUtils.showError(view, "Open template failed", "Failed to open template. Status: " + result);
					}
				}
			};

			worker.addPropertyChangeListener(new PropertyChangeListener() {

				@Override
				public void propertyChange(PropertyChangeEvent ev) {
					if ("progress".equals(ev.getPropertyName())) {
						progressMonitor.setProgress((Integer) ev.getNewValue());
					}
				}
			});

			worker.execute();
		}
	}

	@Override
	public void getSubject() {
		final GetSubjectDialog dialog = new GetSubjectDialog();
		dialog.setLocationRelativeTo(null);
		dialog.setModal(true);
		dialog.setVisible(true);
		if (dialog.getAction() == DialogAction.OK) {
			final ProgressMonitor progressMonitor = new ProgressMonitor(view, "Getting subject", "", 0, 100);
			progressMonitor.setMillisToDecideToPopup(0);
			progressMonitor.setProgress(0);

			SwingWorker<Object, Integer> worker = new SwingWorker<Object, Integer>() {

				@Override
				protected Object doInBackground() throws Exception {
					setProgress(1);

					NSubject subject = new NSubject();
					subject.setId(dialog.getSubjectId());
					setProgress(20);

					NBiometricStatus status = model.getClient().get(subject);
					setProgress(70);
					if (status != NBiometricStatus.OK) {
						return status;
					}

					// TODO: Initialize subject properly.
					subject.getFingers().size();

					return model.recreateSubject(subject);
				}

				@Override
				protected void done() {
					super.done();
					setProgress(100);
					Object result;
					try {
						result = get();
					} catch (InterruptedException e) {
						e.printStackTrace();
						Thread.currentThread().interrupt();
						return;
					} catch (ExecutionException e) {
						MessageUtils.showError(view, e.getCause());
						return;
					}
					if (result instanceof NSubject) {
						showTab(SubjectTab.class, true, true, result);
					} else if (result instanceof NBiometricStatus) {
						MessageUtils.showError(view, "Get Subject failed", "Failed to get subject. Status: " + result);
					}
				}
			};

			worker.addPropertyChangeListener(new PropertyChangeListener() {

				@Override
				public void propertyChange(PropertyChangeEvent ev) {
					if ("progress".equals(ev.getPropertyName())) {
						progressMonitor.setProgress((Integer) ev.getNewValue());
					}
				}
			});

			worker.execute();
		}
	}

	@Override
	public void settings() {
		showTab(SettingsTab.class, false, true, null);
	}

	@Override
	public void changeDatabase() {
		int count = view.getTabs().size();
		if (count > 1) {
			if (!MessageUtils.showQuestion(view, "Warning", "Changing database will close all currently opened tabs. Do you want to continue?")) {
				return;
			}
			for (int i = 1; i < count; i++)	{
				closeTab(1); // Each time close the second (index == 1) tab. This closes all tabs except the first (index == 0).
			}
		}
		view.setBusy(true);
		showTab(ChangeDatabaseTab.class, true, false, null);
	}

	@Override
	public void editSchema(DatabaseSchema schema) {
		view.setBusy(true);
		showTab(SchemaBuilderTab.class, true, false, schema);
	}

	@Override
	public CompletionHandler<NBiometricTask, Void> databaseOperation(String title, String message, NSubject subject) {
		DatabaseOperationTab tab = (DatabaseOperationTab) showTab(DatabaseOperationTab.class, true, true, new DefaultBiometricModel(subject, model));
		tab.setTitle(title);
		tab.showProgress(-1, -1);
		return tab;
	}

	@Override
	public void about() {
		AboutBox.show();
	}

	@Override
	public void dispose() {
		view.close();
		if (model.getClient() != null) {
			model.getClient().cancel();
			for (NDevice device : model.getClient().getDeviceManager().getDevices()) {
				if (device.isDisconnectable()) {
					model.getClient().getDeviceManager().disconnectFromDevice(device);
				}
			}
		}
		LicenseManager.getInstance().releaseAll();
		NCore.shutdown();
	}

}
