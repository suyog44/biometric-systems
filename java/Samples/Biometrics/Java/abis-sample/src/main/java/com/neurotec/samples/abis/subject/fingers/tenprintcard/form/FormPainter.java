package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

import java.awt.BasicStroke;
import java.awt.Font;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Point;
import java.awt.Rectangle;
import java.util.EnumSet;

public class FormPainter {

	// ===========================================================
	// Private fields
	// ===========================================================

	private final FormDefinition form;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public FormPainter(FormDefinition frame) {
		this.form = frame;
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void drawBlockBorders(Graphics2D g2d, Block block) {
		for (BorderInfo border : block.getBorders()) {
			g2d.setStroke(new BasicStroke(border.getSize()));
			if (border.getAlignment().contains(Alignment.BOTTOM)) {
				g2d.drawLine(block.getX(), block.getY() + block.getHeight(), block.getX() + block.getWidth(), block.getY() + block.getHeight());
			}
			if (border.getAlignment().contains(Alignment.TOP)) {
				g2d.drawLine(block.getX(), block.getY(), block.getX() + block.getWidth(), block.getY());
			}
			if (border.getAlignment().contains(Alignment.LEFT)) {
				g2d.drawLine(block.getX(), block.getY(), block.getX(), block.getY() + block.getHeight());
			}
			if (border.getAlignment().contains(Alignment.RIGHT)) {
				g2d.drawLine(block.getX() + block.getWidth(), block.getY(), block.getX() + block.getWidth(), block.getY() + block.getHeight());
			}
		}
	}

	private void drawCheckBox(Graphics g, Rectangle rect, Point startPoint, Point endPoint) {
		int squareWidth = 16;
		int textCenter = startPoint.x - rect.x + (endPoint.x - startPoint.x) / 2;
		int cbX = rect.x + textCenter - squareWidth / 2;
		int cbY = rect.y + 6;
		g.drawRect(cbX, cbY, squareWidth, squareWidth);
	}

	private void drawString(Graphics g, String text, int startX, int startY) {
		for (String line : text.split("\n")) {
			g.drawString(line, startX, startY);
			startY += g.getFontMetrics().getHeight();
		}
	}

	private Point getAlignmentPoint(Graphics g, Rectangle rect, Text text, Font font, Point endPoint) {
		FontMetrics metrics = g.getFontMetrics(font);
		int stringWidth = metrics.stringWidth(text.getValue());
		int stringHeight = metrics.getHeight();
		Point point = new Point();

		boolean centerIncluded = text.getAlignment().contains(Alignment.CENTER);
		boolean topBottomIncluded = text.getAlignment().contains(Alignment.TOP) || text.getAlignment().contains(Alignment.BOTTOM);
		boolean leftRightIncluded = text.getAlignment().contains(Alignment.LEFT) || text.getAlignment().contains(Alignment.RIGHT);

		// Vertical
		if (text.getAlignment().contains(Alignment.TOP)) {
			if ((endPoint.y == 0) || !centerIncluded) {
				point.y = rect.y + 2 + stringHeight;
			} else {
				point.y = endPoint.y;
			}
			endPoint.y = point.y + stringHeight;
		} else if (text.getAlignment().contains(Alignment.BOTTOM)) {
			if (endPoint.y == 0) {
				endPoint.y = rect.y + rect.height - 4;
			} else {
				endPoint.y--;
			}
			point.y = endPoint.y;
		} else if (centerIncluded) {
			if ((endPoint.y == 0) || leftRightIncluded) {
				point.y = rect.y + (rect.height - stringHeight) / 2 + stringHeight;
			} else {
				point.y = endPoint.y + 1;
			}
			endPoint.y = point.y + stringHeight;
		}

		// Horizontal
		if (text.getAlignment().contains(Alignment.LEFT)) {
			if (endPoint.x == 0) {
				point.x = rect.x + 4;
			} else {
				point.x = endPoint.x + 1;
			}
			endPoint.x = point.x + stringWidth;
		} else if (text.getAlignment().contains(Alignment.RIGHT)) {
			if (endPoint.x == 0) {
				endPoint.x = rect.x + form.getWidth() - 2;
			} else {
				endPoint.x--;
			}
			point.x = endPoint.x - stringWidth;
		} else if (centerIncluded) {
			if ((endPoint.x == 0) || topBottomIncluded) {
				point.x = rect.x + (rect.width - stringWidth) / 2;
			} else {
				point.x = endPoint.x + 1;
			}
			endPoint.x = point.x + stringWidth;
		}

		return point;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void paintForm(Graphics2D g2d) {
		BasicStroke regularStroke = new BasicStroke(1);
		g2d.setColor(form.getColor());
		for (Block block : form.getBlocks()) {
			int currentWidth = 0;
			int currentHeight = 0;
			for (Cell cell : block.getCells()) {
				g2d.setStroke(regularStroke);
				Rectangle cellRect = new Rectangle(block.getX() + currentWidth, block.getY() + currentHeight, cell.getWidth(), cell.getHeight());
				if (cell.isDrawRect()) {
					g2d.drawRect(cellRect.x, cellRect.y, cellRect.width, cellRect.height);
				}

				Point endPoint = new Point();
				EnumSet<Alignment> tmpAlignment = EnumSet.noneOf(Alignment.class);
				for (Text t : cell.getTextLines()) {
					if (!t.getAlignment().equals(tmpAlignment)) {
						endPoint = new Point();
					}
					Font font;
					if (t.isBold()) {
						font = new Font("Arial", Font.BOLD, (int) Math.round(t.getSize() / 0.72));
					} else {
						font = new Font("Arial", Font.PLAIN, (int) Math.round(t.getSize() / 0.72));
					}
					g2d.setFont(font);
					Point point = getAlignmentPoint(g2d, cellRect, t, font, endPoint);
					drawString(g2d, t.getValue(), point.x, point.y);
					tmpAlignment = t.getAlignment();

					if (t.isDrawCheckbox()) {
						drawCheckBox(g2d, cellRect, point, endPoint);
					}
				}

				// Do not increment height if line contains more than one cell
				int h;
				if (cell.getWidth() + currentWidth == block.getWidth()) {
					h = cell.getHeight();
				} else {
					h = 0;
				}
				if (cell.getHeight() + currentHeight <= block.getHeight()) {
					currentHeight += h;
				} else {
					currentHeight = 0;
				}
				if (cell.getWidth() + currentWidth < block.getWidth()) {
					currentWidth += cell.getWidth();
				} else {
					currentWidth = 0;
				}
			}
			drawBlockBorders(g2d, block);
		}
	}

}
