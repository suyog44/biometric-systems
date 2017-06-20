package com.neurotec.samples.server.util;

import java.awt.GridBagConstraints;
import java.awt.Insets;

import javax.swing.JComponent;
import javax.swing.JPanel;

public final class GridBagUtils {

	// ==============================================
	// Private fields
	// ==============================================

	private GridBagConstraints gridBagConstraints;

	// ==============================================
	// Public methods
	// ==============================================

	public GridBagUtils(int fill) {
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.fill = fill;
	}

	public GridBagUtils(int fill, Insets insets) {
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.fill = fill;
		gridBagConstraints.insets = insets;
	}

	public void setInsets(Insets insets) {
		gridBagConstraints.insets = insets;
	}

	public void addToGridBagLayout(int x, int y, JPanel parent, JComponent component) {
		gridBagConstraints.gridx = x;
		gridBagConstraints.gridy = y;
		parent.add(component, gridBagConstraints);
	}

	public void addToGridBagLayout(int x, int y, int width, int height, JPanel parent, JComponent component) {
		gridBagConstraints.gridx = x;
		gridBagConstraints.gridy = y;
		gridBagConstraints.gridwidth = width;
		gridBagConstraints.gridheight = height;
		parent.add(component, gridBagConstraints);
	}

	public void addToGridBagLayout(int x, int y, int width, int height, int weightX, int weightY, JPanel parent, JComponent component) {
		gridBagConstraints.gridx = x;
		gridBagConstraints.gridy = y;
		gridBagConstraints.gridwidth = width;
		gridBagConstraints.gridheight = height;
		gridBagConstraints.weightx = weightX;
		gridBagConstraints.weighty = weightY;
		parent.add(component, gridBagConstraints);
	}

	public void clearGridBagConstraints() {
		gridBagConstraints.gridwidth = 1;
		gridBagConstraints.gridheight = 1;
		gridBagConstraints.weightx = 0;
		gridBagConstraints.weighty = 0;
	}

}
