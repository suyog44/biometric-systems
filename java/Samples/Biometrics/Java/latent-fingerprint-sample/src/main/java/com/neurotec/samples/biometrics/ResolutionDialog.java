package com.neurotec.samples.biometrics;

import com.neurotec.samples.util.Utils;

import java.awt.BasicStroke;
import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.Frame;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.Insets;
import java.awt.Point;
import java.awt.RenderingHints;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.image.BufferedImage;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JSpinner;
import javax.swing.SpinnerNumberModel;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

public final class ResolutionDialog extends JDialog implements ActionListener {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private Point startPt;
	private Point endPt;
	private boolean dragging;
	private static final int END_MARKER_SIZE = 4;

	private SpinnerNumberModel hResModel;
	private SpinnerNumberModel vResModel;

	private final MainFrameEventListener listener;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JSpinner spinnerHRez;
	private JSpinner spinnerVRez;
	private JSpinner spinnerLength;

	private JLabel lblImage;

	private JRadioButton radioInches;
	private JRadioButton radioCentemeters;

	private JButton btnOk;
	private JButton btnCancel;

	// ==============================================
	// Public constructor
	// ==============================================

	public ResolutionDialog(Frame owner, MainFrameEventListener listener) {
		super(owner, "Resolution", true);
		this.listener = listener;
		setPreferredSize(new Dimension(580, 410));
		setMinimumSize(new Dimension(575, 400));
		initializeComponents();
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		getContentPane().setLayout(new BorderLayout());
		JPanel mainPanel = new JPanel();
		GridBagLayout mainPanelLayot = new GridBagLayout();
		mainPanel.setLayout(mainPanelLayot);

		JPanel imagePanel = new JPanel();
		lblImage = new ImageLabel();
		imagePanel.add(lblImage);

		JScrollPane imageScrollPane = new JScrollPane(imagePanel, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
		imageScrollPane.setPreferredSize(new Dimension(540, 220));

		JPanel toolPanel = new JPanel();
		toolPanel.setPreferredSize(new Dimension(275, 80));
		toolPanel.setMinimumSize(new Dimension(275, 80));
		toolPanel.setBorder(BorderFactory.createTitledBorder("Measure resolution tool"));
		GridBagLayout toolPanelLayout = new GridBagLayout();
		toolPanel.setLayout(toolPanelLayout);

		JLabel lblIcon = new JLabel(Utils.createIcon("images/MeasureDpiTool.png"));
		spinnerLength = new JSpinner(new SpinnerNumberModel(1, 1, 100, 1));
		radioInches = new JRadioButton("Inch(es)");
		radioCentemeters = new JRadioButton("Centimeter(s)");

		ButtonGroup unitsGroup = new ButtonGroup();
		unitsGroup.add(radioInches);
		unitsGroup.add(radioCentemeters);

		radioInches.setSelected(true);

		GridBagConstraints c = new GridBagConstraints();
		c.fill = GridBagConstraints.HORIZONTAL;
		c.insets = new Insets(1, 2, 1, 2);

		c.gridx = 0;
		c.gridy = 0;
		c.gridwidth = 3;
		toolPanel.add(new JLabel("Draw a line on the image which represents:"), c);

		c.gridx = 0;
		c.gridy = 1;
		c.gridwidth = 1;
		c.gridheight = 2;
		toolPanel.add(lblIcon, c);

		c.gridx = 1;
		c.gridy = 1;
		c.gridheight = 1;
		toolPanel.add(spinnerLength, c);

		c.gridx = 2;
		c.gridy = 1;
		toolPanel.add(radioInches, c);

		c.gridx = 2;
		c.gridy = 2;
		toolPanel.add(radioCentemeters, c);

		JPanel resolutionPanel = new JPanel(new GridLayout(2, 2, 4, 4));
		resolutionPanel.setPreferredSize(new Dimension(260, 80));
		resolutionPanel.setMinimumSize(new Dimension(260, 80));
		resolutionPanel.setBorder(BorderFactory.createTitledBorder("Resolution"));

		hResModel = new SpinnerNumberModel(500d, 50d, 3000d, 1);
		spinnerHRez = new JSpinner(hResModel);
		spinnerHRez.addChangeListener(new SpinnerChangeListener(spinnerHRez));

		vResModel = new SpinnerNumberModel(500d, 50d, 3000d, 1);
		spinnerVRez = new JSpinner(vResModel);
		spinnerVRez.addChangeListener(new SpinnerChangeListener(spinnerVRez));

		resolutionPanel.add(new JLabel("Horizontal resolution:"));
		resolutionPanel.add(spinnerHRez);
		resolutionPanel.add(new JLabel("Vertical resolution:"));
		resolutionPanel.add(spinnerVRez);

		JPanel buttonPanel = new JPanel();
		buttonPanel.setLayout(new BoxLayout(buttonPanel, BoxLayout.X_AXIS));

		btnOk = new JButton("OK");
		btnOk.setPreferredSize(new Dimension(75, 25));
		btnOk.addActionListener(this);

		btnCancel = new JButton("Cancel");
		btnCancel.setPreferredSize(new Dimension(75, 25));
		btnCancel.addActionListener(this);

		buttonPanel.add(Box.createHorizontalGlue());
		buttonPanel.add(btnOk);
		buttonPanel.add(Box.createHorizontalStrut(3));
		buttonPanel.add(btnCancel);

		c.insets = new Insets(4, 4, 4, 4);

		c.gridx = 0;
		c.gridy = 0;
		c.gridwidth = 2;
		mainPanel.add(imageScrollPane, c);

		c.gridx = 0;
		c.gridy = 1;
		c.gridwidth = 1;
		mainPanel.add(toolPanel, c);

		c.gridx = 1;
		c.gridy = 1;
		mainPanel.add(resolutionPanel, c);

		c.gridx = 0;
		c.gridy = 2;
		c.gridwidth = 2;
		mainPanel.add(buttonPanel, c);

		getContentPane().add(mainPanel, BorderLayout.CENTER);
		pack();

	}

	private double calculateDistance() {
		return Math.sqrt((double) (startPt.x - endPt.x) * (double) (startPt.x - endPt.x) + (double) (startPt.y - endPt.y) * (double) (startPt.y - endPt.y));
	}

	private void drawSelectionLine(Graphics2D g, Color color, int width) {

		// Draw pointer markers.
		g.setColor(color);
		g.setStroke(new BasicStroke(width));
		g.drawLine(startPt.x - END_MARKER_SIZE, startPt.y, startPt.x + END_MARKER_SIZE, startPt.y);
		g.drawLine(startPt.x, startPt.y - END_MARKER_SIZE, startPt.x, startPt.y + END_MARKER_SIZE);
		g.drawLine(endPt.x - END_MARKER_SIZE, endPt.y, endPt.x + END_MARKER_SIZE, endPt.y);
		g.drawLine(endPt.x, endPt.y - END_MARKER_SIZE, endPt.x, endPt.y + END_MARKER_SIZE);

		// Draw line between starting and ending points.
		g.drawLine(startPt.x, startPt.y, endPt.x, endPt.y);
	}

	private void paintImageLabel(Graphics grapics) {
		if (!(startPt == null) && !(endPt == null)) {
			Graphics2D g = (Graphics2D) grapics;
			RenderingHints hints = g.getRenderingHints();
			g.setRenderingHint(RenderingHints.KEY_RENDERING, RenderingHints.VALUE_RENDER_QUALITY);

			drawSelectionLine(g, Color.WHITE, 3);

			drawSelectionLine(g, Color.GREEN.darker(), 1);
			g.setRenderingHints(hints);

		}
	}

	void onMouseDragged(Point point) {
		if (dragging) {
			endPt = point;
			lblImage.repaint();
		}
	}

	void onMousePressedLeft(Point point) {
		startPt = point;
		endPt = null;
		lblImage.setCursor(new Cursor(Cursor.CROSSHAIR_CURSOR));
		dragging = true;
	}

	void onMouseReleasedLeft(Point point) {
		if (dragging) {
			endPt = point;
			setCursor(new Cursor(Cursor.DEFAULT_CURSOR));
			dragging = false;

			double scaleToInch = 1.0d;
			if (radioCentemeters.isSelected()) {
				scaleToInch = 1.0f / 2.54f;
			}

			double distance = calculateDistance();
			if (distance > 50.0) {
				spinnerHRez.setValue(distance / ((Integer) spinnerLength.getValue() * scaleToInch));
				spinnerVRez.setValue(spinnerHRez.getValue());
			} else {
				JOptionPane.showMessageDialog(ResolutionDialog.this, "Please draw a longer line segment.");
			}
			lblImage.repaint();

			startPt = null;
			endPt = null;
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	public double getHorzResolution() {
		return (Double) spinnerHRez.getValue();
	}

	public void setHorzResolution(double value) {
		if (value < (Double) hResModel.getMinimum()) {
			value = (Double) hResModel.getMinimum();
		} else if (value > (Double) hResModel.getMaximum()) {
			value = (Double) hResModel.getMaximum();
		}
		spinnerHRez.setValue(value);
	}

	public double getVertResolution() {
		return (Double) spinnerVRez.getValue();
	}

	public void setVertResolution(double value) {
		if (value < (Double) vResModel.getMinimum()) {
			value = (Double) vResModel.getMinimum();
		} else if (value > (Double) vResModel.getMaximum()) {
			value = (Double) vResModel.getMaximum();
		}
		spinnerVRez.setValue(value);
	}

	public void setFingerImage(BufferedImage value) {
		lblImage.setIcon(new ImageIcon(value));
	}

	// ==============================================
	// Event handling
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnOk) {
			listener.resolutionCheckCompleted(true, getHorzResolution(), getVertResolution());
			dispose();
		} else if (source == btnCancel) {
			listener.resolutionCheckCompleted(false, getHorzResolution(), getVertResolution());
			dispose();
		}
	}

	// =========================================================
	// Private class handling mouse events on image label
	// =========================================================

	private final class ImageLabelMouseListener extends MouseAdapter {

		@Override
		public void mouseDragged(MouseEvent e) {
			onMouseDragged(e.getPoint());
		}

		@Override
		public void mouseMoved(MouseEvent e) {
			// Do nothing.
		}

		@Override
		public void mousePressed(MouseEvent e) {
			if (e.getButton() == MouseEvent.BUTTON1) {
				onMousePressedLeft(e.getPoint());
			}
		}

		@Override
		public void mouseReleased(MouseEvent e) {
			if (e.getButton() == MouseEvent.BUTTON1) {
				onMouseReleasedLeft(e.getPoint());
			}
		}

	}

	// =================================================
	// Private class handling spinner change events
	// =================================================

	private static final class SpinnerChangeListener implements ChangeListener {

		// ==============================================
		// Private fields
		// ==============================================

		private final JSpinner spinner;

		// ==============================================
		// Private constructor
		// ==============================================

		SpinnerChangeListener(JSpinner spinner) {
			super();
			this.spinner = spinner;
		}

		// ==============================================
		// Public methods
		// ==============================================

		public void stateChanged(ChangeEvent e) {
			if ((Double) spinner.getValue() < 250) {
				((JSpinner.DefaultEditor) spinner.getEditor()).getTextField().setBackground(Color.RED);
				spinner.setToolTipText("Current resolution is lower than recommended minimum.");
			} else {
				((JSpinner.DefaultEditor) spinner.getEditor()).getTextField().setBackground(Color.WHITE);
				spinner.setToolTipText("");
			}
		}
	}

	// ===================================================================
	// Private class extending JLabel to display finger print image
	// ===================================================================

	private final class ImageLabel extends JLabel {

		// ==============================================
		// Private static fields
		// ==============================================

		private static final long serialVersionUID = 1L;

		// ==============================================
		// Private constructor
		// ==============================================

		ImageLabel() {
			super();
			ImageLabelMouseListener mouseListener = new ImageLabelMouseListener();
			addMouseListener(mouseListener);
			addMouseMotionListener(mouseListener);
		}

		// ==============================================
		// Overridden methods
		// ==============================================

		@Override
		protected void paintComponent(Graphics g) {
			super.paintComponent(g);
			paintImageLabel(g);
		}

	}

}
