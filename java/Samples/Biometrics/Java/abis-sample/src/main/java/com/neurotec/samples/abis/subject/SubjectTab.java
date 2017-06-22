package com.neurotec.samples.abis.subject;

import java.awt.BorderLayout;
import java.awt.Color;
import java.beans.PropertyChangeListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.JComponent;

import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.NodeChangeListener;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.swing.SubjectTree;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.Tab;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;

public final class SubjectTab extends Tab implements SubjectPresentationListener, TabNavigationListener {

	// ===========================================================
	// Private fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private final SubjectPresentationModel model;
	private JComponent shownPage;
	private final SubjectTree subjectTree;
	private final List<PageNavigationListener> pageNavigationListeners;
	private boolean updating;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SubjectTab(SubjectTree subjectTree, SubjectPresentationModel model) {
		super("Subject");
		this.subjectTree = subjectTree;
		this.model = model;
		this.pageNavigationListeners = new ArrayList<PageNavigationListener>();
		initGUI();
		subjectPresentationChanged();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		setLayout(new BorderLayout());
		subjectTree.setBorder(BorderFactory.createLineBorder(new Color(0, 0, 0)));
		add(subjectTree, BorderLayout.WEST);
	}

	private void setShownPage(JComponent page) {
		if (shownPage != null) {
			if ((shownPage instanceof Page) && isShown()) {
				fireNavigatingFrom(new NavigationEvent<SubjectTab, Page>(this, (Page) shownPage));
			}
			remove(shownPage);
			if (shownPage instanceof PageNavigationListener) {
				removePageNavigationListener((PageNavigationListener) shownPage);
			}
		}
		shownPage = page;
		if (shownPage instanceof PageNavigationListener) {
			addPageNavigationListener((PageNavigationListener) shownPage);
		}
		if (shownPage != null) {
			add(shownPage, BorderLayout.CENTER);
		}
		if ((shownPage instanceof Page) && isShown()) {
			fireNavigatedTo(new NavigationEvent<SubjectTab, Page>(this, (Page) shownPage));
		}
		revalidate();
		repaint();
	}

	private void fireNavigatedTo(NavigationEvent<? extends Object, Page> ev) {
		for (PageNavigationListener l : pageNavigationListeners) {
			l.navigatedTo(ev);
		}
	}

	private void fireNavigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		for (PageNavigationListener l : pageNavigationListeners) {
			l.navigatingFrom(ev);
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void subjectPresentationChanged() {
		if (!updating) {
			updating = true;
			try {
				if (!model.getPresentationTitle().equals(getTitle())) {
					setTitle(model.getPresentationTitle());
				}
				if (!subjectTree.getAllowedNewTypes().equals(model.getAllowedNewTypes())) {
					if (model.getAllowedNewTypes().isEmpty()) {
						MessageUtils.showError(null, "Error", "No required licenses are activated.");
					}
					subjectTree.setAllowedNewTypes(model.getAllowedNewTypes());
				}
				if ((subjectTree.getSelectedItem() == null) || !subjectTree.getSelectedItem().equals(model.getSelectedSubjectElement())) {
					subjectTree.setSelectedItem(subjectTree.getNodeFor(model.getSelectedSubjectElement()));
				}
				Page page = model.getShownPage();
				if ((shownPage == null) || !shownPage.equals(page)) {
					setShownPage(page);
				}
			} finally {
				updating = false;
			}
		}
	}

	@Override
	public void tabAdded(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void tabEnter(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			if (shownPage instanceof Page) {
				fireNavigatedTo(new NavigationEvent<SubjectTab, Page>(this, (Page) shownPage));
			}
		}
	}

	@Override
	public void tabLeave(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			if (shownPage instanceof Page) {
				fireNavigatingFrom(new NavigationEvent<SubjectTab, Page>(this, (Page) shownPage));
			}
		}
	}

	@Override
	public void tabClose(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			for (PropertyChangeListener l : subjectTree.getPropertyChangeListeners()) {
				subjectTree.removePropertyChangeListener(l);
			}
			pageNavigationListeners.clear();
			SwingUtils.runOnEDT(new Runnable() {

				@Override
				public void run() {
					subjectTree.setSubject(null);
					if (shownPage != null) {
						remove(shownPage);
					}
				}
			});
			model.dispose();
		}
	}

	public void addPageNavigationListener(PageNavigationListener l) {
		if (!pageNavigationListeners.contains(l)) {
			pageNavigationListeners.add(l);
		}
	}

	public void removePageNavigationListener(PageNavigationListener l) {
		pageNavigationListeners.remove(l);
	}

	public void addNodeChangeListener(NodeChangeListener l) {
		subjectTree.addNodeChangeListener(l);
	}

	public void removeNodeChangeListener(NodeChangeListener l) {
		subjectTree.removeNodeChangeListener(l);
	}

}
