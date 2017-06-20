package com.neurotec.samples.abis.settings;

import java.awt.BorderLayout;
import java.awt.FlowLayout;
import java.awt.SystemColor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.DefaultListModel;
import javax.swing.JButton;
import javax.swing.JList;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JSplitPane;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.tabbedview.Tab;
import com.neurotec.samples.abis.tabbedview.TabbedAbisController;
import com.neurotec.samples.util.LicenseManager;

public class SettingsTab extends Tab implements PageNavigationController, TabNavigationListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final NBiometricClient client;
	private final TabbedAbisController abisController;

	private final List<Page> pages = new ArrayList<Page>();
	private Page currentPage;
	private JSplitPane splitPane;
	private JList listViewPages;
	private JPanel panelPage;
	private JPanel panelButtons;
	private JButton btnDefault;
	private JButton btnOK;
	private JButton btnCancel;
	private JScrollPane scrollPane;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SettingsTab(NBiometricClient client, TabbedAbisController abisController) {
		super("Settings");
		this.client = client;
		this.abisController = abisController;
		initGUI();
		DefaultListModel model = (DefaultListModel) listViewPages.getModel();
		GeneralSettingsPage general = new GeneralSettingsPage(client, this);
		model.addElement(general);
		pages.add(general);
		if (LicenseManager.getInstance().isActivated("Biometrics.FingerExtraction", true) || !this.client.getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			FingersSettingsPage fingers = new FingersSettingsPage(client, this);
			model.addElement(fingers);
			pages.add(fingers);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.FaceExtraction", true) || !this.client.getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			FacesSettingsPage faces = new FacesSettingsPage(client, this);
			model.addElement(faces);
			pages.add(faces);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.IrisExtraction", true) || !this.client.getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			IrisesSettingsPage irises = new IrisesSettingsPage(client, this);
			model.addElement(irises);
			pages.add(irises);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.PalmExtraction", true) || !this.client.getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			PalmsSettingsPage palms = new PalmsSettingsPage(client, this);
			model.addElement(palms);
			pages.add(palms);
		}
		if (LicenseManager.getInstance().isActivated("Biometrics.VoiceExtraction", true) || !this.client.getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE)) {
			VoicesSettingsPage voices = new VoicesSettingsPage(client, this);
			model.addElement(voices);
			pages.add(voices);
		}
		navigateToPage(general);
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private void initGUI() {
		SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
		StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
		StyleConstants.setSpaceAbove(styleAttributes, 1);
		StyleConstants.setSpaceBelow(styleAttributes, 1);
		StyleConstants.setLeftIndent(styleAttributes, 3);
		StyleConstants.setRightIndent(styleAttributes, 3);
		StyleConstants.setFontSize(styleAttributes, 13);
		StyleConstants.setBold(styleAttributes, true);
		StyleConstants.setForeground(styleAttributes, SystemColor.menu);

		setLayout(new BorderLayout());

		splitPane = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT);
		add(splitPane, BorderLayout.CENTER);
		splitPane.setDividerLocation(120);
		listViewPages = new JList(new DefaultListModel());
		listViewPages.getSelectionModel().addListSelectionListener(new ListSelectionListener() {
			@Override
			public void valueChanged(ListSelectionEvent e) {
				navigateToPage(listViewPages.getSelectedValue());
			}
		});
		splitPane.add(listViewPages);

		scrollPane = new JScrollPane();
		splitPane.add(scrollPane);

		panelPage = new JPanel();
		scrollPane.setViewportView(panelPage);

		panelButtons = new JPanel();
		FlowLayout flPanelButtons = (FlowLayout) panelButtons.getLayout();
		flPanelButtons.setAlignment(FlowLayout.RIGHT);
		add(panelButtons, BorderLayout.SOUTH);

		btnDefault = new JButton("Default");
		btnDefault.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				if (currentPage != null) {
					getController(currentPage).defaultSettings();
				}
			}
		});
		panelButtons.add(btnDefault);

		btnOK = new JButton("OK");
		btnOK.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				for (Page page : pages) {
					getController(page).saveSettings();
				}
				SettingsManager.saveSettings(client);
				abisController.closeTab(SettingsTab.this);
			}
		});
		panelButtons.add(btnOK);

		btnCancel = new JButton("Cancel");
		btnCancel.addActionListener(new ActionListener() {
			@Override
			public void actionPerformed(ActionEvent e) {
				abisController.closeTab(SettingsTab.this);
			}
		});
		panelButtons.add(btnCancel);
	}

	private SettingsController getController(Page settingsPage) {
		if (settingsPage instanceof SettingsController) {
			return (SettingsController) settingsPage;
		} else {
			return null;
		}
	}

	// ===========================================================
	// Public fields
	// ===========================================================

	@Override
	public void navigateToPage(Object pageObject) {
		if (!(pageObject instanceof Page)) {
			throw new IllegalArgumentException("pageObject");
		}
		if (pageObject.equals(currentPage)) {
			return;
		}
		if ((currentPage != null) && isShown()) {
			getController(currentPage).navigatingFrom(null);
			currentPage = null;
			panelPage.removeAll();
		}
		Page page = (Page) pageObject;
		if (!pages.contains(page)) {
			pages.add(page);
		}
		currentPage = page;
		panelPage.add(currentPage);
		listViewPages.setSelectedValue(page, true);
		if (isShown()) {
			getController(page).navigatedTo(new NavigationEvent<SettingsTab, Page>(this, page));
		}
		repaint();
		revalidate();
	}

	@Override
	public void navigateToStartPage() {
		//TODO implement
	}

	@Override
	public void tabAdded(NavigationEvent<? extends Object, Tab> ev) {
		for (Page page : pages) {
			getController(page).loadSettings();
		}
	}

	@Override
	public void tabEnter(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			if (currentPage instanceof Page) {
				getController(currentPage).navigatedTo(new NavigationEvent<SettingsTab, Page>(this, currentPage));
			}
		}
	}

	@Override
	public void tabLeave(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			if (currentPage instanceof Page) {
				getController(currentPage).navigatingFrom(new NavigationEvent<SettingsTab, Page>(this, currentPage));
			}
		}
	}

	@Override
	public void tabClose(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			remove(currentPage);
			pages.clear();
		}
	}

}
