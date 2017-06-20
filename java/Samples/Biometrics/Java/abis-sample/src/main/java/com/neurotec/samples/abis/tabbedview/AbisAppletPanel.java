package com.neurotec.samples.abis.tabbedview;

import java.awt.BorderLayout;

import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;

import java.awt.Dimension;
import java.awt.Font;
import java.lang.reflect.InvocationTargetException;

import javax.swing.JApplet;
import javax.swing.JLabel;
import javax.swing.SwingConstants;

public final class AbisAppletPanel extends AbisTabbedPanel {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final JApplet applet;
	private final JLabel lblClose;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public AbisAppletPanel(JApplet applet) {
		super();
		this.applet = applet;
		lblClose = new JLabel();
		lblClose.setFont(new Font("Tahoma", 0, 18));
		lblClose.setHorizontalAlignment(SwingConstants.CENTER);
		lblClose.setText("Thank you for using Abis sample.");
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void close() {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					getTabbedPane().removeAll();
					removeAll();
					add(lblClose, BorderLayout.CENTER);
					revalidate();
					repaint();
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(applet, e);
		}
	}

	@Override
	public void launch() {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					Dimension d = new Dimension(1050, 700);
					applet.setSize(d);
					applet.add(AbisAppletPanel.this);
					applet.setVisible(true);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(applet, e);
		}
	}

}
