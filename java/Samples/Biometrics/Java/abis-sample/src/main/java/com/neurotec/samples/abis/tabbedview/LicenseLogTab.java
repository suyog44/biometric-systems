package com.neurotec.samples.abis.tabbedview;

import com.neurotec.samples.abis.AbisModel;
import com.neurotec.samples.abis.LogView;
import com.neurotec.samples.util.LicenseManager;

import java.awt.Color;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;

import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.SwingConstants;
import javax.swing.text.BadLocationException;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

public final class LicenseLogTab extends ProgressTab implements LogView, PropertyChangeListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final StyledDocument document;

	private JScrollPane spInfo;
	private JTextPane tpInfo;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public LicenseLogTab(AbisModel model) {
		super();
		initGUI();
		document = tpInfo.getStyledDocument();
		setTitle("Obtaining licenses ...");
		showProgress(0, model.getLicenses().size());
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		spInfo = new JScrollPane();
		tpInfo = new JTextPane();
		spInfo.setViewportView(tpInfo);
		add(spInfo, SwingConstants.CENTER);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void appendText(String text, Color color) {
		SimpleAttributeSet attributes = new SimpleAttributeSet();
		StyleConstants.setForeground(attributes, color);
		try {
			document.insertString(document.getLength(), text, attributes);
		} catch (BadLocationException e) {
			throw new AssertionError("Can't happen - inserting at the end of the document.");
		}
	}

	@Override
	public void propertyChange(PropertyChangeEvent ev) {
		if (LicenseManager.PROGRESS_CHANGED_PROPERTY.equals(ev.getPropertyName())) {
			int progress = (Integer) ev.getNewValue();
			String message = String.format("# of analyzed licenses: %d%n", progress);
			setProgress(message, progress);
		} else if (LicenseManager.LAST_STATUS_MESSAGE_PROPERTY.equals(ev.getPropertyName())) {
			appendText((String) ev.getNewValue(), Color.BLACK);
		}
	}

}
