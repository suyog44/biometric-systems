package com.neurotec.samples.abis.tabbedview;

import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.samples.util.Utils;

import java.awt.Dimension;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.lang.reflect.InvocationTargetException;

import javax.swing.JFrame;

public final class AbisApplicationPanel extends AbisTabbedPanel {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JFrame mainFrame;

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void close() {
		if (mainFrame == null) {
			throw new IllegalStateException("Application has not been launched yet");
		}
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {

				@Override
				public void run() {
					getTabbedPane().removeAll();
					mainFrame.dispose();
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(mainFrame, e);
		}
	}

	@Override
	public void launch() {
		try {
			SwingUtils.runOnEDTAndWait(new Runnable() {
				@Override
				public void run() {
					mainFrame = new JFrame();
					Dimension d = new Dimension(1050, 700);
					mainFrame.setSize(d);
					mainFrame.setMinimumSize(d);
					mainFrame.setPreferredSize(d);
					mainFrame.setResizable(true);
					mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
					mainFrame.setTitle("Multibiometric Sample");
					mainFrame.setLocationRelativeTo(null);
					mainFrame.setIconImage(Utils.createIconImage("images/Logo16x16.png"));
					mainFrame.add(AbisApplicationPanel.this);
					mainFrame.addWindowListener(new WindowAdapter() {
						@Override
						public void windowClosing(WindowEvent e) {
							getController().dispose();
						}
					});
					mainFrame.setVisible(true);
				}
			});
		} catch (InterruptedException e) {
			e.printStackTrace();
			Thread.currentThread().interrupt();
		} catch (InvocationTargetException e) {
			MessageUtils.showError(mainFrame, e);
		}
	}

}
