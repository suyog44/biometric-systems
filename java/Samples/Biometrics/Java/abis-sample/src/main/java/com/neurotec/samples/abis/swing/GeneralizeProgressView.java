package com.neurotec.samples.abis.swing;

import java.awt.Color;
import java.awt.Component;
import java.awt.Cursor;
import java.awt.Dimension;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Point;
import java.awt.RenderingHints;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionAdapter;
import java.awt.geom.AffineTransform;
import java.awt.geom.Ellipse2D;
import java.awt.geom.Rectangle2D;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BoxLayout;
import javax.swing.JComponent;
import javax.swing.JLabel;
import javax.swing.JPanel;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.swing.NFaceView;
import com.neurotec.biometrics.swing.NFingerView;
import com.neurotec.swing.NView;

public class GeneralizeProgressView extends JPanel {

	private static final long serialVersionUID = 1L;
	private static final Color COLOR_OK = new Color(0, 128, 0);
	private static final Color COLOR_TEXT = Color.BLACK;

	// ===========================================================
	// Private types
	// ===========================================================

	private class ItemStatus {
		private String text;
		private boolean fill;
		private Color color;
		private NBiometric biometric;
		private Rectangle2D.Double hitBox;
		private boolean selected;

		public boolean isHitTest(Point p) {
			if (hitBox != null) {
				return hitBox.getX() <= p.getX() && p.getX() <= hitBox.getX() + hitBox.getWidth()
					&& hitBox.getY() <= p.getY() && p.getY() <= hitBox.getY() + hitBox.getHeight();
			}
			return false;
		}

		public String getText() {
			return text;
		}

		public void setText(String text) {
			this.text = text;
		}

		public boolean isFill() {
			return fill;
		}

		public void setFill(boolean fill) {
			this.fill = fill;
		}

		public Color getColor() {
			return color;
		}

		public void setColor(Color color) {
			this.color = color;
		}

		public NBiometric getBiometric() {
			return biometric;
		}

		public void setBiometric(NBiometric biometric) {
			this.biometric = biometric;
		}

		public Rectangle2D.Double getHitBox() {
			return hitBox;
		}

		public void setHitBox(Rectangle2D.Double hitBox) {
			this.hitBox = hitBox;
		}

		public boolean isSelected() {
			return selected;
		}

		public void setSelected(boolean selected) {
			this.selected = selected;
		}
	};

	private class GeneralizeProgressComponent extends JComponent {

		private static final long serialVersionUID = 1L;

		public GeneralizeProgressComponent() {
			setDoubleBuffered(true);
			addMouseMotionListener(new MouseMotionAdapter() {
				@Override
				public void mouseMoved(MouseEvent e) {
					boolean hit = false;
					if (isEnableMouseSelection()) {
						for (ItemStatus item : drawings) {
							if (item.isHitTest(e.getPoint())) {
								hit = true;
							}
						}
						if (hit) {
							setCursor(Cursor.getPredefinedCursor(Cursor.HAND_CURSOR));
						}
					}
					if (!hit) {
						setCursor(Cursor.getDefaultCursor());
					}
				}
			});
			addMouseListener(new MouseAdapter() {
				@Override
				public void mouseClicked(MouseEvent e) {
					if (isEnableMouseSelection()) {
						for (ItemStatus item : drawings) {
							if (item.isHitTest(e.getPoint())) {
								setSelected(item.getBiometric());
								break;
							}
						}
					}
				}
			});
		}

		@Override
		protected void paintComponent(Graphics g) {
			Graphics2D g2d = (Graphics2D) g;
			int margin = 2;
			Dimension sz = getSize();
			sz.width = sz.width - margin * 2;
			sz.height = sz.height - margin * 2;

			g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);

			if (drawings.size() > 0) {
				Dimension defaultTextSize = measureString("Az", g2d, getFont());
				Dimension textSize = defaultTextSize;

				float bubleDiameter = textSize.height - margin * 2;
				float totalWidth = 0;
				for (ItemStatus item : drawings) {
					totalWidth += 2 * margin + measureString(item.getText(), g2d, getFont()).width + margin + bubleDiameter + 2 * margin;
				}
				float offsetX = (sz.width - totalWidth) / 2;
				float offsetY = (sz.height - textSize.height) / 2;

				AffineTransform m = g2d.getTransform();
				m.translate(offsetX, offsetY);
				g2d.setTransform(m);

				float offset = 2 * margin;
				for (ItemStatus item : drawings) {
					Rectangle2D.Double hitBox = new Rectangle2D.Double(offsetX + offset, offsetY, 0, 0);
					if (item.getText() != null && !item.getText().equals("")) {
						textSize = measureString(item.getText(), g2d, getFont());
						g2d.setColor(COLOR_TEXT);
						g2d.drawString(item.getText(), offset, 12);
					} else {
						textSize = new Dimension(0, defaultTextSize.height);
					}
					hitBox.width = textSize.width + margin;
					offset += hitBox.width;
					if (item.isFill()) {
						g2d.setColor(item.getColor());
						g2d.fill(new Ellipse2D.Double(offset, margin, bubleDiameter, bubleDiameter));
					} else {
						g2d.setColor(item.getColor());
						g2d.draw(new Ellipse2D.Double(offset, margin, bubleDiameter, bubleDiameter));
					}
					if (item.isSelected()) {
						Color color = g2d.getColor();
						g2d.setColor(Color.BLUE);
						g2d.draw(new Ellipse2D.Double(offset, margin, bubleDiameter, bubleDiameter));
						g2d.setColor(color);
					}
					offset += bubleDiameter + 2 * margin;
					hitBox.width += bubleDiameter;
					hitBox.height = textSize.height;
					item.setHitBox(hitBox);
				}
			}
			super.paintComponent(g);
			if (redraw) {
				redraw = false;
			}
		}

		@Override
		public void paint(Graphics g) {
			Graphics2D g2d = (Graphics2D) g.create();
			super.paint(g2d);
			g2d.dispose();
		}
	}

	// ===========================================================
	// Private static methods
	// ===========================================================

	private static Dimension measureString(String text, Graphics2D graphics, Font font) {
		FontMetrics metrics = graphics.getFontMetrics(font);
		int hgt = metrics.getHeight();
		int adv = metrics.stringWidth(text);
		return new Dimension(adv + 2, hgt + 2);
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private JLabel lblStatus;
	private GeneralizeProgressComponent panelPaint;
	private  NView view = null;
	private List<? extends NBiometric> biometrics = null;
	private NBiometric selected = null;
	private List<NBiometric> generalized = null;
	private boolean enableMouseSelection = true;
	private List<ItemStatus> drawings = new ArrayList<ItemStatus>();
	private boolean redraw = false;
	private PropertyChangeListener biometricPropertyChanged = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent evt) {
			if (evt.getPropertyName().equals("Status"))
				updateBiometricsStatus();
		}
	};

	// ===========================================================
	// Public constructor
	// ===========================================================

	public GeneralizeProgressView() {
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		setSize(new Dimension(362, 47));
		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		{
			panelPaint = new GeneralizeProgressComponent();
			panelPaint.setSize(350, 22);
			add(panelPaint);
		}
		{
			lblStatus = new JLabel("");
			lblStatus.setAlignmentX(Component.CENTER_ALIGNMENT);
			add(lblStatus);
		}
	}

	private void onDataChanged() {
		int i;
		drawings.clear();
		if (biometrics != null) {
			i = 1;
			for (NBiometric item : biometrics) {
				ItemStatus itemStatus = new ItemStatus();
				itemStatus.setText(String.valueOf(i++));
				itemStatus.setBiometric(item);
				itemStatus.setColor(Color.ORANGE);
				itemStatus.setFill(false);
				drawings.add(itemStatus);
			}
		}
		if (generalized != null) {
			i = 0;
			for (NBiometric item : generalized) {
				ItemStatus itemStatus = new ItemStatus();
				itemStatus.setText(i++ == 0 ? "Generalized:" : "");
				itemStatus.setBiometric(item);
				itemStatus.setColor(Color.ORANGE);
				itemStatus.setFill(false);
				drawings.add(itemStatus);
			}
		}
		updateBiometricsStatus();
		redraw = true;
	}

	private void updateBiometricsStatus() {
		for (ItemStatus item : drawings) {
			NBiometric biometric = item.getBiometric();
			item.setColor(Color.ORANGE);
			item.setFill(false);
			if (biometric != null) {
				switch (biometric.getStatus()) {
				case OK:
					item.setColor(COLOR_OK);
					item.setFill(true);
					break;
				case NONE:
					item.setColor(Color.ORANGE);
					item.setFill(false);
					break;
				default:
					item.setColor(Color.RED);
					item.setFill(true);
					break;
				};
			}
		}
		invalidate();
		revalidate();
		repaint();
	}

	private void onPropertyChanged(String propertyName) {
		firePropertyChange(propertyName, null, null);
	}

	// ===========================================================
	// Protected methods
	// ===========================================================

	protected void setBiometricToView(NView view, NBiometric biometric) {
		if (view != null) {
			if (view instanceof NFaceView) {
				((NFaceView)view).setFace((NFace) biometric);
			} else if (view instanceof NFingerView) {
				((NFingerView)view).setFinger((NFrictionRidge)biometric);
			}
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public NView getView() {
		return view;
	}

	public void setView(NView view) {
		this.view = view;
		onPropertyChanged("View");
	}

	public NBiometric getSelected() {
		return selected;
	}

	public void setSelected(NBiometric value) {
		boolean newValue = selected != value;
		selected = value;
		setBiometricToView(view, value);
		for (ItemStatus status : drawings) {
			status.setSelected(status.getBiometric().equals(value));
		}
		invalidate();
		revalidate();
		repaint();
		if (newValue)
			onPropertyChanged("Selected");
	}

	public List<? extends NBiometric> getBiometrics() {
		return biometrics;
	}

	public void setBiometrics(List<? extends NBiometric> value) {
		if (biometrics != value) {
			if (biometrics != null) {
				for (NBiometric item : biometrics) {
					item.removePropertyChangeListener(biometricPropertyChanged);
				}
			}
			biometrics = value;
			if (value != null) {
				for (NBiometric item : biometrics) {
					item.addPropertyChangeListener(biometricPropertyChanged);
				}
			}
			onDataChanged();
			onPropertyChanged("Biometrics");
		}
	}

	public List<NBiometric> getGeneralized() {
		return generalized;
	}
	public void setGeneralized(List<NBiometric> value) {
		if (generalized != value) {
			if (generalized != null) {
				for (NBiometric item : generalized) {
					item.removePropertyChangeListener(biometricPropertyChanged);
				}
			}
			generalized = value;
			if (generalized != null) {
				for (NBiometric item : generalized) {
					item.addPropertyChangeListener(biometricPropertyChanged);
				}
			}
			onDataChanged();
			onPropertyChanged("Generalized");
		}
	}

	public String getStatusText() {
		return lblStatus.getText();
	}

	public void setStatusText(String value) {
		if (!lblStatus.getText().equals(value)) {
			lblStatus.setText(value);
			onPropertyChanged("StatusText");
		}
	}

	public boolean isEnableMouseSelection() {
		return enableMouseSelection;
	}

	public void setEnableMouseSelection(boolean value) {
		enableMouseSelection = value;
	}

	public void clear() {
		setBiometrics(null);
		setGeneralized(null);
		setSelected(null);
		setStatusText("");
		drawings.clear();
	}

	@Override
	public Dimension getPreferredSize() {
		Dimension sz = lblStatus.getSize();
		if (drawings.size() > 0) {
			double maxHeight = 0;
			for (ItemStatus item : drawings) {
				if (item.getHitBox() != null) {
					double height = item.getHitBox().getHeight();
					if (height > maxHeight) {
						maxHeight = height;
					}
				}
			}
			sz.height += maxHeight + 30;
			if (drawings.get(drawings.size() - 1).getHitBox() != null) {
				sz.width = Math.max((int)drawings.get(drawings.size() - 1).getHitBox().getMaxX(), (int)sz.getWidth());
			}
		} else {
			sz.width = Math.max(50, sz.width);
			sz.height = 35;
		}
		return sz;
	}
}
