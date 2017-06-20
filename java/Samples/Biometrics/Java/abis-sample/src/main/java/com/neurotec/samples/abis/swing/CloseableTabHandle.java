package com.neurotec.samples.abis.swing;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

import javax.swing.AbstractButton;
import javax.swing.BorderFactory;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.plaf.basic.BasicButtonUI;

import com.neurotec.samples.abis.tabbedview.Tab;
import com.neurotec.samples.abis.tabbedview.TabbedAbisController;

public final class CloseableTabHandle extends JPanel {

	private static final MouseListener BUTTON_MOUSE_LISTENER = new MouseAdapter() {

		@Override
		public void mouseEntered(MouseEvent e) {
			Component component = e.getComponent();
			if (component instanceof AbstractButton) {
				((AbstractButton) component).setBorderPainted(true);
			} else {
				throw new AssertionError("Listener " + this + " can only listen to AbstractButton events.");
			}
		}

		@Override
		public void mouseExited(MouseEvent e) {
			Component component = e.getComponent();
			if (component instanceof AbstractButton) {
				((AbstractButton) component).setBorderPainted(false);
			} else {
				throw new AssertionError("Listener " + this + " can only listen to AbstractButton events.");
			}
		}

	};

	private static final long serialVersionUID = 1L;

	private final TabbedAbisController controller;
	private final Tab tab;

	private JLabel lblTitle;
	private JButton btnClose;

	public CloseableTabHandle(TabbedAbisController controller, Tab tab, String title) {
		super();
		if (controller == null) {
			throw new NullPointerException("controller");
		}
		this.controller = controller;
		this.tab = tab;
		initGUI();
		setTitle(title);
	}

	private void initGUI() {
		btnClose = new TabButton();
		lblTitle = new JLabel();

		setLayout(new FlowLayout(FlowLayout.LEFT, 0, 0));

		add(lblTitle);
		lblTitle.setBorder(BorderFactory.createEmptyBorder(0, 0, 0, 5));
		add(btnClose);

		setOpaque(false);
		setBorder(BorderFactory.createEmptyBorder(2, 0, 0, 0));
	}

	public void setTitle(String title) {
		lblTitle.setText(title);
	}

	public Tab getTab() {
		return tab;
	}

	private class TabButton extends JButton implements ActionListener {

		private static final long serialVersionUID = 1L;

		TabButton() {
			super();
			int size = 17;
			setPreferredSize(new Dimension(size, size));
			setUI(new BasicButtonUI());
			setContentAreaFilled(false);
			setFocusable(false);
			setBorder(BorderFactory.createEtchedBorder());
			setBorderPainted(false);
			addMouseListener(BUTTON_MOUSE_LISTENER);
			setRolloverEnabled(true);
			addActionListener(this);
		}

		@Override
		public void actionPerformed(ActionEvent e) {
			controller.closeTab(tab);
		}

		@Override
		public void updateUI() {
			// Don't update UI.
		}

		@Override
		protected void paintComponent(Graphics g) {
			super.paintComponent(g);
			Graphics2D g2 = (Graphics2D) g.create();
			if (getModel().isPressed()) {
				g2.translate(1, 1);
			}
			g2.setStroke(new BasicStroke(2));
			g2.setColor(Color.BLACK);
			if (getModel().isRollover()) {
				g2.setColor(Color.MAGENTA);
			}
			int delta = 6;
			g2.drawLine(delta, delta, getWidth() - delta - 1, getHeight() - delta - 1);
			g2.drawLine(getWidth() - delta - 1, delta, delta, getHeight() - delta - 1);
			g2.dispose();
		}

	}

}
