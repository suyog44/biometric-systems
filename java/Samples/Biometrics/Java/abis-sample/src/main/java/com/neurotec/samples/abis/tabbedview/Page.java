package com.neurotec.samples.abis.tabbedview;

import javax.swing.JPanel;

public abstract class Page extends JPanel {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private String title;
	private PageNavigationController pageController;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public Page(String title, PageNavigationController pageController) {
		super();
		this.title = title;
		this.pageController = pageController;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public PageNavigationController getPageController() {
		return pageController;
	}

	@Override
	public String toString() {
		return getTitle();
	}

}
