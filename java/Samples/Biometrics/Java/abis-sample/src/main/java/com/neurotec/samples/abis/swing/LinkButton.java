package com.neurotec.samples.abis.swing;

import java.awt.Component;
import java.awt.Cursor;
import java.awt.Font;
import java.awt.Graphics;

import javax.accessibility.Accessible;
import javax.swing.AbstractButton;
import javax.swing.DefaultButtonModel;
import javax.swing.Icon;
import javax.swing.UIManager;
import javax.swing.plaf.ButtonUI;

public class LinkButton extends AbstractButton implements Accessible {

	private static final long serialVersionUID = 1L;

	private static final String uiClassID = "RadioButtonUI";

	public LinkButton() {
		super();
		setModel(new DefaultButtonModel());
		init("", null);
		setBorderPainted(false);
		setHorizontalAlignment(LEADING);
		setFont(new Font("Tahoma", 0, 14));
		setCursor(new Cursor(Cursor.HAND_CURSOR));

		setIcon(new Icon() {

			@Override
			public void paintIcon(Component c, Graphics g, int x, int y) {
				// Do nothing.
			}

			@Override
			public int getIconWidth() {
				return 0;
			}

			@Override
			public int getIconHeight() {
				return 0;
			}

		});
	}

	@Override
	public void updateUI() {
		setUI((ButtonUI) UIManager.getUI(this));
	}

	@Override
	public String getUIClassID() {
		return uiClassID;
	}

}
