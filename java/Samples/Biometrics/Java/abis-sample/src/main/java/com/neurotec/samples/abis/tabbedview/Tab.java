package com.neurotec.samples.abis.tabbedview;

import com.neurotec.samples.abis.swing.CloseableTabHandle;

import javax.swing.JPanel;
import javax.swing.JTabbedPane;

public abstract class Tab extends JPanel {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private String title;
	private boolean shown;
	private CloseableTabHandle handle;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public Tab() {
		this("");
	}

	public Tab(String title) {
		this(title, null);
	}

	public Tab(CloseableTabHandle handle) {
		this(null, handle);
	}

	public Tab(String title, CloseableTabHandle handle) {
		super();
		this.title = title;
		this.handle = handle;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public boolean isShown() {
		return shown;
	}

	public void setShown(boolean shown) {
		this.shown = shown;
	}

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
		if (handle == null) {
			if (getParent() instanceof JTabbedPane) {
				JTabbedPane pane = (JTabbedPane) getParent();
				pane.setTitleAt(pane.indexOfComponent(this), title);
			}
		} else {
			handle.setTitle(title);
		}
	}

	public boolean isCloseable() {
		return handle != null;
	}

	public void setHandle(CloseableTabHandle handle) {
		this.handle = handle;
	}

}
