package com.neurotec.samples.abis.tabbedview;

import java.awt.BorderLayout;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ContainerEvent;
import java.awt.event.ContainerListener;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.List;

import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.JToolBar;
import javax.swing.SwingConstants;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.client.NBiometricClient.RemoteConnectionCollection;
import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.AbisView;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.swing.CloseableTabHandle;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.samples.util.Utils;

public abstract class AbisTabbedPanel extends JPanel implements AbisView, ActionListener, ChangeListener, ContainerListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JButton btnAbout;
	private JButton btnNewSubject;
	private JButton btnOpenSubject;
	private JButton btnGetSubject;
	private JButton btnSettings;
	private JButton btnChangeDatabase;
	private JButton btnClose;
	private JTabbedPane tabbedPane;
	private JToolBar mainToolBar;

	private TabbedAbisController controller;
	private AbisModel model;
	private Tab currentTab;
	private final List<TabNavigationListener> tabNavigationListeners;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public AbisTabbedPanel() {
		this.tabNavigationListeners = new ArrayList<TabNavigationListener>();
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		mainToolBar = new JToolBar();
		btnNewSubject = new JButton();
		btnOpenSubject = new JButton();
		btnGetSubject = new JButton();
		btnSettings = new JButton();
		btnChangeDatabase = new JButton();
		btnAbout = new JButton();
		btnClose = new JButton();
		tabbedPane = new JTabbedPane();

		setLayout(new BorderLayout());

		mainToolBar.setFloatable(false);
		mainToolBar.setRollover(true);
		mainToolBar.setLayout(new FlowLayout(FlowLayout.LEFT));

		Dimension buttonSize = new Dimension(140, 45);

		btnNewSubject.setText("New Subject");
		btnNewSubject.setIcon(Utils.createIcon("images/NewDocument.png"));
		btnNewSubject.setPreferredSize(buttonSize);
		btnNewSubject.setFocusable(false);
		btnNewSubject.setHorizontalTextPosition(SwingConstants.CENTER);
		btnNewSubject.setVerticalTextPosition(SwingConstants.BOTTOM);
		mainToolBar.add(btnNewSubject);

		btnOpenSubject.setText("Open Subject");
		btnOpenSubject.setIcon(Utils.createIcon("images/OpenFolder.png"));
		btnOpenSubject.setPreferredSize(buttonSize);
		btnOpenSubject.setFocusable(false);
		btnOpenSubject.setHorizontalTextPosition(SwingConstants.CENTER);
		btnOpenSubject.setVerticalTextPosition(SwingConstants.BOTTOM);
		mainToolBar.add(btnOpenSubject);

		btnGetSubject.setText("Get Subject");
		btnGetSubject.setIcon(Utils.createIcon("images/Get.png"));
		btnGetSubject.setPreferredSize(buttonSize);
		btnGetSubject.setFocusable(false);
		btnGetSubject.setHorizontalTextPosition(SwingConstants.CENTER);
		btnGetSubject.setVerticalTextPosition(SwingConstants.BOTTOM);
		mainToolBar.add(btnGetSubject);

		btnSettings.setText("Settings");
		btnSettings.setIcon(Utils.createIcon("images/Settings.png"));
		btnSettings.setPreferredSize(buttonSize);
		btnSettings.setFocusable(false);
		btnSettings.setHorizontalTextPosition(SwingConstants.CENTER);
		btnSettings.setVerticalTextPosition(SwingConstants.BOTTOM);
		mainToolBar.add(btnSettings);

		btnChangeDatabase.setText("Change Database");
		btnChangeDatabase.setIcon(Utils.createIcon("images/OpenFolder.png"));
		btnChangeDatabase.setPreferredSize(buttonSize);
		btnChangeDatabase.setFocusable(false);
		btnChangeDatabase.setHorizontalTextPosition(SwingConstants.CENTER);
		btnChangeDatabase.setVerticalTextPosition(SwingConstants.BOTTOM);
		mainToolBar.add(btnChangeDatabase);

		btnAbout.setText("About");
		btnAbout.setIcon(Utils.createIcon("images/Help.png"));
		btnAbout.setPreferredSize(buttonSize);
		btnAbout.setFocusable(false);
		btnAbout.setHorizontalTextPosition(SwingConstants.CENTER);
		btnAbout.setVerticalTextPosition(SwingConstants.BOTTOM);
		mainToolBar.add(btnAbout);

		if (this instanceof AbisAppletPanel) {
			btnClose.setText("Close");
			btnClose.setIcon(Utils.createIcon("images/Delete.png"));
			btnClose.setPreferredSize(buttonSize);
			btnClose.setFocusable(false);
			btnClose.setHorizontalTextPosition(SwingConstants.CENTER);
			btnClose.setVerticalTextPosition(SwingConstants.BOTTOM);
			mainToolBar.add(btnClose);
		}

		add(mainToolBar, BorderLayout.NORTH);
		add(tabbedPane, BorderLayout.CENTER);

		btnNewSubject.addActionListener(this);
		btnOpenSubject.addActionListener(this);
		btnGetSubject.addActionListener(this);
		btnSettings.addActionListener(this);
		btnChangeDatabase.addActionListener(this);
		btnAbout.addActionListener(this);
		btnClose.addActionListener(this);
		tabbedPane.addContainerListener(this);
		tabbedPane.addChangeListener(this);
	}

	private void fireTabAdded(NavigationEvent<? extends Object, Tab> ev) {
		for (TabNavigationListener l : tabNavigationListeners) {
			l.tabAdded(ev);
		}
	}

	private void fireTabEnter(NavigationEvent<? extends Object, Tab> ev) {
		for (TabNavigationListener l : tabNavigationListeners) {
			l.tabEnter(ev);
		}
	}


	private void fireTabLeave(NavigationEvent<? extends Object, Tab> ev) {
		for (TabNavigationListener l : tabNavigationListeners) {
			l.tabLeave(ev);
		}
	}

	private void fireTabClose(NavigationEvent<? extends Object, Tab> ev) {
		for (TabNavigationListener l : tabNavigationListeners) {
			l.tabClose(ev);
		}
	}

	void removeTab(final Tab tab) {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					tabbedPane.remove(tab);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(this, e);
		}
	}

	protected JTabbedPane getTabbedPane() {
		return tabbedPane;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void addTab(String title, Tab tab) {
		insertTab(title, tab, tabbedPane.getTabCount());
	}

	public void insertTab(final String title, final Tab tab, final int index) {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					tabbedPane.insertTab(title, null, tab, null, index);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(this, e);
		}
	}

	public void addCloseableTab(String title, Tab tab) {
		insertCloseableTab(title, tab, tabbedPane.getTabCount());
	}

	public void insertCloseableTab(final String title, final Tab tab, final int index) {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					CloseableTabHandle handle = new CloseableTabHandle(controller, tab, title);
					tab.setHandle(handle);
					tabbedPane.insertTab(title, null, tab, null, index);
					tabbedPane.setTabComponentAt(index, handle);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(this, e);
		}
	}

	public Tab getSelectedTab() {
		Component component = tabbedPane.getSelectedComponent();
		if (component instanceof Tab) {
			return (Tab) tabbedPane.getSelectedComponent();
		} else {
			return null;
		}
	}

	public void setSelectedTab(final Tab tab) {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					tabbedPane.setSelectedComponent(tab);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(this, e);
		}
	}

	public void setSelectedTab(final int index) {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					tabbedPane.setSelectedIndex(index);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(this, e);
		}
	}

	public List<Tab> getTabs() {
		List<Tab> tabs = new ArrayList<Tab>();
		for (int i = 0; i < tabbedPane.getTabCount(); i++) {
			tabs.add((Tab) tabbedPane.getComponentAt(i));
		}
		return tabs;
	}

	@Override
	public void setBusy(final boolean busy) {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					boolean canGetSubject = false;
					if (model.getClient() != null) {
						RemoteConnectionCollection connections = model.getClient().getRemoteConnections();
						canGetSubject = (!connections.isEmpty() && connections.get(0).getOperations().contains(NBiometricOperation.GET)) || (model.getClient().getLocalOperations().contains(NBiometricOperation.GET));
					}
					btnNewSubject.setEnabled(!busy);
					btnOpenSubject.setEnabled(!busy);
					btnGetSubject.setEnabled(!busy && canGetSubject);
					btnSettings.setEnabled(!busy);
					btnChangeDatabase.setEnabled(!busy);
					btnAbout.setEnabled(!busy);
					btnClose.setEnabled(!busy);
					tabbedPane.setEnabled(!busy);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(this, e);
		}
	}

	public void setController(TabbedAbisController controller) {
		this.controller = controller;
	}

	public TabbedAbisController getController() {
		return controller;
	}

	public void setModel(AbisModel model) {
		this.model = model;
	}

	public void addTabNavigationListener(TabNavigationListener l) {
		tabNavigationListeners.add(l);
	}

	public void removeTabNavigationListener(TabNavigationListener l) {
		tabNavigationListeners.remove(l);
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource() == btnNewSubject) {
			controller.createNewSubject();
		} else if (ev.getSource() == btnOpenSubject) {
			controller.openSubject();
		} else if (ev.getSource() == btnGetSubject) {
			controller.getSubject();
		} else if (ev.getSource() == btnSettings) {
			controller.settings();
		} else if (ev.getSource() == btnChangeDatabase) {
			controller.changeDatabase();
		} else if (ev.getSource() == btnAbout) {
			controller.about();
		} else if (ev.getSource() == btnClose) {
			controller.dispose();
		} else {
			throw new AssertionError("Unknown event source: " + ev);
		}
	}

	@Override
	public void stateChanged(ChangeEvent ev) {
		Object source = ev.getSource();
		if (source instanceof JTabbedPane) {
			if (currentTab != null) {
				currentTab.setShown(false);
				fireTabLeave(new NavigationEvent<AbisTabbedPanel, Tab>(AbisTabbedPanel.this, currentTab));
			}
			Component component = ((JTabbedPane) source).getSelectedComponent();
			if (component instanceof Tab) {
				currentTab = (Tab) component;
				currentTab.setShown(true);
				fireTabEnter(new NavigationEvent<AbisTabbedPanel, Tab>(AbisTabbedPanel.this, currentTab));
			} else {
				currentTab = null;
			}
		}
	}

	@Override
	public void componentAdded(ContainerEvent ev) {
		Object source = ev.getSource();
		if (source instanceof JTabbedPane) {
			Component component = ev.getChild();
			if (component instanceof Tab) {
				fireTabAdded(new NavigationEvent<AbisTabbedPanel, Tab>(AbisTabbedPanel.this, (Tab) component));
			}
		}
	}

	@Override
	public void componentRemoved(ContainerEvent ev) {
		Object source = ev.getSource();
		if (source instanceof JTabbedPane) {
			Component component = ev.getChild();
			if (component instanceof Tab) {
				fireTabClose(new NavigationEvent<AbisTabbedPanel, Tab>(AbisTabbedPanel.this, (Tab) component));
			}
		}
	}

}
