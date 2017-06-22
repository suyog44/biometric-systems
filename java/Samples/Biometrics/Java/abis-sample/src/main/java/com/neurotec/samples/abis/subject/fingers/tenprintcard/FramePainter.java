package com.neurotec.samples.abis.subject.fingers.tenprintcard;

import com.neurotec.images.NImage;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.form.FormDefinition;
import com.neurotec.samples.abis.util.MessageUtils;

import java.awt.BasicStroke;
import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Cursor;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.Point;
import java.awt.Rectangle;
import java.awt.RenderingHints;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.awt.event.MouseEvent;
import java.awt.geom.AffineTransform;
import java.awt.image.BufferedImage;
import java.util.HashMap;
import java.util.Map;

import javax.swing.event.MouseInputListener;

public final class FramePainter extends javax.swing.JComponent implements MouseInputListener {

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private inner classes
	// ===========================================================

	private enum MouseCapturing {

		NOT_CAPTURING,
		MOVING,
		RESIZING;

	};

	// ===========================================================
	// Private fields
	// ===========================================================

	private NImage image;
	private FormDefinition form;

	private int x;
	private int y;
	private int width;
	private int height;
	private double aspect;
	private Image drawnImage;
	private Image smallImage;
	private Rectangle imageRect;
	private double currScale = 1;

	private int mouseDownX;
	private int mouseDownY;
	private MouseCapturing mouseCapturing = MouseCapturing.NOT_CAPTURING;

	// ===========================================================
	// FramePainter properties
	// ===========================================================

	/** Identifies a change on autofit property. */
	public static final String AUTOFIT_CHANGED_PROPERTY = "autofit";
	/** Identifies a change on scale property. */
	public static final String SCALE_CHANGED_PROPERTY = "scale";

	// ===========================================================
	// Public constructor
	// ===========================================================

	public FramePainter() {
		this.setDoubleBuffered(true);
		this.setBackground(Color.WHITE);
		this.setLayout(new BorderLayout());
		this.setOpaque(false);
		this.addComponentListener(new ComponentAdapter() {
			@Override
			public void componentResized(ComponentEvent e) {
				onSizeChanged(e);
			}
		});

		this.addMouseListener(this);
		this.addMouseMotionListener(this);
		this.imageRect = new Rectangle();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private boolean inFrame(Point p) {
		return (p.x < this.width + this.x) && (p.y < this.height + this.y) && (p.x > this.x) && (p.y > this.y);
	}

	private boolean inSizeFrame(Point p) {
		return (p.x < this.width + this.x + 5) && (p.y < this.height + this.y + 5) && (p.x > this.width + this.x - 5) && (p.y > this.height + this.y - 5);
	}

	private void changeScale(double newScale) {
		this.x = (int) Math.round(this.x * newScale);
		this.y = (int) Math.round(this.y * newScale);
		this.width = (int) Math.round(this.width * newScale);
		this.height = (int) Math.round(this.height * newScale);
	}

	private void onSizeChanged(ComponentEvent e) {
		if ((getWidth() == 0) || (getHeight() == 0) || (this.image == null)) {
			return;
		}

		int w;
		int h;

		if (this.image.getHeight() > this.image.getWidth())	{
			h = this.getHeight();
			w = (h * this.image.getWidth()) / this.image.getHeight();
		} else {
			w = this.getWidth();
			h = (w * this.image.getHeight()) / this.image.getWidth();
		}

		if (w <= 0 || h <= 0) {
			this.drawnImage = null;
			refresh();
			return;
		}

		this.x -= this.imageRect.getX();
		this.y -= this.imageRect.getY();

		this.imageRect = new Rectangle(this.getWidth() / 2 - w / 2, this.getHeight() / 2 - h / 2, w, h);

		if (this.drawnImage != null) {
			this.drawnImage = null;
		}

		this.drawnImage = createImage(w, h);
		Graphics2D g = (Graphics2D) this.drawnImage.getGraphics();
		g.setRenderingHint(RenderingHints.KEY_INTERPOLATION, RenderingHints.VALUE_INTERPOLATION_BICUBIC);

		g.drawImage(smallImage, 0, 0, w, h, this);

		changeScale(1 / this.currScale);
		this.currScale = (double) w / this.image.getWidth();
		changeScale(this.currScale);

		this.x += this.imageRect.x;
		this.y += this.imageRect.y;

		refresh();
	}

	public Map<Integer, Rectangle> getFrameSplitting() {
		double scale = 1 / this.currScale;
		int frameX = (int) Math.round((this.x - this.imageRect.x) * scale);
		int frameY = (int) Math.round((this.y - this.imageRect.y) * scale);
		int frameWidth = (int) Math.round(this.width * scale);
		int frameHeight = (int) Math.round(this.height * scale);

		if (image == null) {
			return form.getFingerRectangles(new Rectangle(frameX, frameY, frameWidth, frameHeight), 0, 0);
		} else {
			return form.getFingerRectangles(new Rectangle(frameX, frameY, frameWidth, frameHeight), image.getWidth(), image.getHeight());
		}
	}

	private void refresh() {
		revalidate();
		repaint();
	}

	private void drawImage(Graphics g) {
		if (this.drawnImage != null) {
			g.drawImage(this.drawnImage, this.imageRect.x, this.imageRect.y, this.imageRect.width, this.imageRect.height, this);
		}
	}

	private void drawFrame(Graphics g) {
		if ((this.width <= 0) || (this.height <= 0)) {
			return;
		}

		Graphics2D g2d = (Graphics2D) g;
		g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
		g2d.setColor(form.getColor());
		g2d.setStroke(new BasicStroke(2));
		AffineTransform at = AffineTransform.getTranslateInstance(x, y);
		g2d.transform(at);

		g.drawRect(0, 0, this.width, this.height);
		g.fillRect(this.width - 3, this.height - 3, 5, 5);
		g.drawRect(this.width - 3, this.height - 3, 5, 5);

		Map<Integer, Rectangle> fingerRectangles;
		if (image == null) {
			fingerRectangles = form.getFingerRectangles(new Rectangle(0, 0, width, height), 0, 0);
		} else {
			fingerRectangles = form.getFingerRectangles(new Rectangle(0, 0, width, height), image.getWidth(), image.getHeight());
		}

		for (int i = 1; i <= 14; i++) {
			Rectangle rect = fingerRectangles.get(i);
			g.drawRect(rect.x, rect.y, rect.width, rect.height);
		}
	}

	private static BufferedImage resizeImage(BufferedImage originalImage) {
		int w = originalImage.getWidth();
		int h;
		if (w > 500) {
			w = 500;
			h = originalImage.getHeight() * 500 / originalImage.getWidth();
		} else {
			return originalImage;
		}
		int type = originalImage.getType() == 0 ? BufferedImage.TYPE_INT_ARGB : originalImage.getType();
		BufferedImage resizedImage = new BufferedImage(w, h, type);
		Graphics2D g = resizedImage.createGraphics();
		g.drawImage(originalImage, 0, 0, w, h, null);
		g.dispose();

		return resizedImage;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setImage(NImage image) {
		if (this.image != null)	{
			this.image.dispose();
			this.image = null;
		}

		this.drawnImage = null;

		this.image = image;
		try {
			smallImage = resizeImage((BufferedImage) this.image.toImage());
		} catch (OutOfMemoryError er) {
			this.image.dispose();
			this.image = null;
			MessageUtils.showError(this, "Error", "Not enough memory to open image of this size. Try a smaller one.");
			return;
		}
		onSizeChanged(null);
		setDefaultFramePosition();
		refresh();
	}

	public void setForm(FormDefinition form) {
		this.form = form;
	}

	public void setDefaultFramePosition() {
		if (this.image == null) {
			return;
		}

		this.x = this.imageRect.x;
		this.y = this.imageRect.y;
		this.width = this.imageRect.width;
		this.height = this.imageRect.height;
		if ((double) this.width / this.height > this.aspect) {
			this.width = (int) Math.round(this.height * this.aspect);
			this.x += (this.imageRect.width - this.width) / 2;
		} else {
			this.height = (int) Math.round(this.width / this.aspect);
			this.y += (this.imageRect.height - this.height) / 2;
		}
	}

	public void setFrameDimensions(double aspect, int x, int y, int w) {
		this.aspect = aspect;
		this.x = x;
		this.y = y;
		this.width = w;
		this.height = (int) Math.round(w / aspect);
	}

	public Map<Integer, NImage> getFramedFingerprints() {
		Map<Integer, Rectangle> splitting = getFrameSplitting();
		Map<Integer, NImage> images = new HashMap<Integer, NImage>();

		for (Integer key : splitting.keySet()) {
			Rectangle rect = splitting.get(key);
			NImage img = this.image.crop(rect.x, rect.y, rect.width, rect.height);
			images.put(key, img);
		}
		return images;
	}

	@Override
	public void paintComponent(Graphics g) {
		super.paintComponent(g);
		if (this.image == null) {
			return;
		}
		drawImage(g);
		drawFrame(g);
	}

	@Override
	public void mousePressed(MouseEvent e) {
		if (inSizeFrame(e.getPoint())) {
			mouseDownX = e.getX() - this.width;
			mouseDownY = e.getY() - this.height;
			mouseCapturing = MouseCapturing.RESIZING;
		} else {
			if (inFrame(e.getPoint())) {
				mouseDownX = e.getX() - this.x;
				mouseDownY = e.getY() - this.y;
				mouseCapturing = MouseCapturing.MOVING;
			}
		}
	}

	@Override
	public void mouseReleased(MouseEvent e) {
		if (!mouseCapturing.equals(MouseCapturing.NOT_CAPTURING)) {
			mouseCapturing = MouseCapturing.NOT_CAPTURING;
		}
	}

	@Override
	public void mouseDragged(MouseEvent e) {
		if (mouseCapturing.equals(MouseCapturing.RESIZING)) {
			if (e.getX() - mouseDownX > 250) {
				this.width = e.getX() - mouseDownX;
				this.height = (int) Math.round(this.width / this.aspect);
			}
			refresh();
		} else {
			if (mouseCapturing.equals(MouseCapturing.MOVING)) {
				this.x = e.getX() - mouseDownX;
				this.y = e.getY() - mouseDownY;
				refresh();
			}
		}
	}

	@Override
	public void mouseMoved(MouseEvent e) {
		if (inSizeFrame(e.getPoint()) || mouseCapturing.equals(MouseCapturing.RESIZING)) {
			setCursor(Cursor.getPredefinedCursor(Cursor.NW_RESIZE_CURSOR));
		} else {
			if (inFrame(e.getPoint()) || mouseCapturing.equals(MouseCapturing.MOVING)) {
				setCursor(Cursor.getPredefinedCursor(Cursor.MOVE_CURSOR));
			} else {
				setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
			}
		}
	}

	@Override
	public void mouseClicked(MouseEvent e) {}
	@Override
	public void mouseEntered(MouseEvent e) {}
	@Override
	public void mouseExited(MouseEvent e) {}

}
