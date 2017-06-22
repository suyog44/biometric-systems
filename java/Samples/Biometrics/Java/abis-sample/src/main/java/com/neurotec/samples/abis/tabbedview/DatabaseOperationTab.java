package com.neurotec.samples.abis.tabbedview;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NBiometricTask;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NSubject.MatchingResultCollection;
import com.neurotec.samples.abis.ConnectionType;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.BiometricModel;
import com.neurotec.samples.abis.swing.MatchingResultView;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.util.concurrent.CompletionHandler;

import java.awt.Color;
import java.awt.Component;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.EnumSet;

import javax.swing.BoxLayout;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.SwingConstants;

public class DatabaseOperationTab extends ProgressTab implements ActionListener, CompletionHandler<NBiometricTask, Void>, TabNavigationListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	public static final Color COLOR_OK = new Color(0, 128, 0);
	public static final Color COLOR_WARNING = new Color(255, 153, 0);
	public static final Color COLOR_ERROR = new Color(255, 0, 0);

	// ===========================================================
	// Private fields
	// ===========================================================

	private final TabController tabController;
	private final BiometricModel model;

	private JPanel panelMatchingResults;
	private JScrollPane spMatchingResults;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public DatabaseOperationTab(TabController tabController, BiometricModel model) {
		super();
		this.tabController = tabController;
		this.model = model;
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		spMatchingResults = new JScrollPane();
		panelMatchingResults = new JPanel();

		panelMatchingResults.setLayout(new BoxLayout(panelMatchingResults, BoxLayout.Y_AXIS));
		spMatchingResults.setViewportView(panelMatchingResults);
		add(spMatchingResults, SwingConstants.CENTER);
	}

	private void addMatchingResult(MatchingResultView result) {
		result.setAlignmentX(0);
		result.addActionListener(this);
		panelMatchingResults.add(result);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void tabAdded(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void tabEnter(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void tabLeave(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void tabClose(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			for (Component component : panelMatchingResults.getComponents()) {
				if (component instanceof MatchingResultView) {
					((MatchingResultView) component).removeActionListener(this);
				}
			}
		}
	}

	@Override
	public void completed(final NBiometricTask result, Void attachment) {
		Throwable error = result.getError();
		if (error != null) {
			failed(error, null);
			return;
		}
		SwingUtils.runOnEDT(new Runnable() {

			@Override
			public void run() {
				hideProgress();
				NBiometricStatus status = result.getStatus();
				Color color;
				if (status == NBiometricStatus.OK) {
					color = COLOR_OK;
				} else {
					color = COLOR_ERROR;
				}
				EnumSet<NBiometricOperation> operations = result.getOperations();
				setStatus(operations + ": " + status, color);

				if (!operations.contains(NBiometricOperation.ENROLL) && !operations.contains(NBiometricOperation.UPDATE) && ((status == NBiometricStatus.OK) || (status == NBiometricStatus.DUPLICATE_FOUND))) {
					MatchingResultCollection results = result.getSubjects().get(0).getMatchingResults();
					boolean showLink = SettingsManager.getConnectionType() != ConnectionType.REMOTE_MATCHING_SERVER;
					if (results.size() > 0) {
						for (NMatchingResult item : results) {
							MatchingResultView resultView = new MatchingResultView(item, model.getClient().getMatchingThreshold());
							resultView.setLinkEnabled(showLink);
							addMatchingResult(resultView);
						}
					}
				}
			}
		});
	}

	@Override
	public void failed(final Throwable th, Void attachment) {
		MessageUtils.showError(this, th);
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				hideProgress();
				setStatus("Error: " + th, COLOR_ERROR);
			}
		});
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource() instanceof MatchingResultView) {
			MatchingResultView view = (MatchingResultView) ev.getSource();
			NSubject second = new NSubject();
			second.setId(view.getMatchingResult().getId());
			tabController.showTab(MatchingResultTab.class, true, true, new NSubject[] {model.getSubject(), second});
		}
	}

}
