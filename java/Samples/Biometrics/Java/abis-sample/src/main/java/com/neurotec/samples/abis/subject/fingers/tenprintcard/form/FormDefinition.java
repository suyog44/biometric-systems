package com.neurotec.samples.abis.subject.fingers.tenprintcard.form;

import java.awt.Color;
import java.awt.Rectangle;
import java.io.IOException;
import java.io.InputStream;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;

import org.xml.sax.SAXException;

public final class FormDefinition {

	// ===========================================================
	// Private static methods
	// ===========================================================

	private static Block readBlock(Element blockElement) {
		String sX = blockElement.getAttribute("x");
		String sY = blockElement.getAttribute("y");
		String sWidth = blockElement.getAttribute("width");
		String sHeight = blockElement.getAttribute("height");
		String sFingerBlock = blockElement.getAttribute("isFingerBlock");

		int x;
		if (sX.isEmpty()) {
			x = 0;
		} else {
			x = Integer.parseInt(sX);
		}
		int y;
		if (sY.isEmpty()) {
			y = 0;
		} else {
			y = Integer.parseInt(sY);
		}
		int width;
		if (sWidth.isEmpty()) {
			width = 0;
		} else {
			width = Integer.parseInt(sWidth);
		}
		int height;
		if (sHeight.isEmpty()) {
			height = 0;
		} else {
			height = Integer.parseInt(sHeight);
		}
		boolean fingerBlock;
		if (sFingerBlock.isEmpty()) {
			fingerBlock = false;
		} else {
			fingerBlock = Boolean.parseBoolean(sFingerBlock);
		}

		Block block = new Block(x, y, width, height, fingerBlock);

		List<Cell> cells = block.getCells();
		NodeList cellElements = blockElement.getElementsByTagName("cell");
		for (int i = 0; i < cellElements.getLength(); i++) {
			cells.add(readCell((Element) cellElements.item(i), block));
		}

		List<BorderInfo> borders = block.getBorders();
		NodeList borderElements = blockElement.getElementsByTagName("frame");
		for (int i = 0; i < borderElements.getLength(); i++) {
			borders.add(readBorderInfo((Element) borderElements.item(i)));
		}

		return block;
	}

	private static Cell readCell(Element cellElement, Block parent) {
		String sWidth = cellElement.getAttribute("width");
		String sHeight = cellElement.getAttribute("height");
		String sDrawRect = cellElement.getAttribute("drawRect");
		String sFingerNumber = cellElement.getAttribute("fingerNo");

		int width;
		if (sWidth.isEmpty()) {
			width = parent.getWidth();
		} else {
			width = Integer.parseInt(sWidth);
		}
		int height;
		if (sHeight.isEmpty()) {
			height = parent.getHeight();
		} else {
			height = Integer.parseInt(sHeight);
		}
		boolean drawRect;
		if (sDrawRect.isEmpty()) {
			drawRect = true;
		} else {
			drawRect = Boolean.parseBoolean(sDrawRect);
		}
		int fingerNumber;
		if (sFingerNumber.isEmpty()) {
			fingerNumber = -1;
		} else {
			fingerNumber = Integer.parseInt(sFingerNumber);
		}

		Cell cell = new Cell(width, height, drawRect, fingerNumber);

		List<Text> lines = cell.getTextLines();
		NodeList textElements = cellElement.getElementsByTagName("text");
		for (int i = 0; i < textElements.getLength(); i++) {
			lines.add(readText((Element) textElements.item(i)));
		}

		return cell;
	}

	private static Text readText(Element textElement) {
		String sValue = textElement.getAttribute("value");
		String sSize = textElement.getAttribute("size");
		String sAlignment = textElement.getAttribute("alignment");
		String sDrawCheckbox = textElement.getAttribute("square");
		String sBold = textElement.getAttribute("bold");

		int size = Integer.parseInt(sSize);
		String value = sValue.replace("\\n", "\n");
		EnumSet<Alignment> alignment = parseAlignment(sAlignment);
		boolean drawCheckbox;
		if (sDrawCheckbox.isEmpty()) {
			drawCheckbox = false;
		} else {
			drawCheckbox = Boolean.parseBoolean(sDrawCheckbox);
		}
		boolean bold;
		if (sBold.isEmpty()) {
			bold = false;
		} else {
			bold = Boolean.parseBoolean(sBold);
		}

		return new Text(value, size, alignment, drawCheckbox, bold);
	}

	private static BorderInfo readBorderInfo(Element borderElement) {
		String sSize = borderElement.getAttribute("size");
		String sAlignment = borderElement.getAttribute("positions");

		int size = Integer.parseInt(sSize);
		EnumSet<Alignment> alignment = parseAlignment(sAlignment);

		return new BorderInfo(size, alignment);
	}

	private static EnumSet<Alignment> parseAlignment(String str) {
		EnumSet<Alignment> alignment = EnumSet.noneOf(Alignment.class);
		if (!str.isEmpty()) {
			String[] values = str.split(",");
			for (String value : values) {
				alignment.add(Alignment.fromString(value));
			}
		}
		return alignment;
	}

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static FormDefinition fromXml(InputStream xml) throws IOException, SAXException {
		try {
			DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
			DocumentBuilder db = dbf.newDocumentBuilder();
			Document doc = db.parse(xml);

			String sColor = doc.getDocumentElement().getAttribute("color");
			Color color;
			Field field;
			try {
				field = Class.forName("java.awt.Color").getField(sColor.toUpperCase());
				color = (Color) field.get(null);
			} catch (ClassNotFoundException e) {
				throw new AssertionError("can't happen");
			} catch (NoSuchFieldException e) {
				throw new SAXException("Unknown color: " + sColor, e);
			} catch (SecurityException e) {
				e.printStackTrace();
				color = Color.RED;
			} catch (IllegalAccessException e) {
				e.printStackTrace();
				color = Color.RED;
			}

			FormDefinition form = new FormDefinition(color);

			NodeList blockElements = doc.getElementsByTagName("block");
			for (int i = 0; i < blockElements.getLength(); i++) {
				Block block = readBlock((Element) blockElements.item(i));
				if (form.width < (block.getX() + block.getWidth())) {
					form.width = block.getX() + block.getWidth();
				}
				if (form.height < (block.getY() + block.getHeight())) {
					form.height = block.getY() + block.getHeight();
				}
				form.blocks.add(block);
			}

			return form;
		} catch (ParserConfigurationException e) {
			throw new AssertionError("can't happen");
		}
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private final Color color;
	private final List<Block> blocks;
	private int width;
	private int height;

	// ===========================================================
	// Private constructor
	// ===========================================================

	FormDefinition(Color color) {
		this.color = color;
		this.blocks = new ArrayList<Block>();
	}

	// ===========================================================
	// Package private methods
	// ===========================================================

	List<Block> getBlocks() {
		return blocks;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public int getWidth() {
		return width;
	}

	public int getHeight() {
		return height;
	}

	public Color getColor() {
		return color;
	}

	public Map<Integer, Rectangle> getFingerRectangles(Rectangle outerFrame, int areaWidth, int areaHeight) {
		Map<Integer, Rectangle> areaMap = new HashMap<Integer, Rectangle>();

		Block block = null;
		for (Block b : getBlocks()) {
			if (b.isFingerBlock()) {
				block = b;
				break;
			}
		}
		if (block == null) {
			return areaMap;
		}
		double widthRatio;
		if (outerFrame.width == 0) {
			widthRatio = 1;
		} else {
			widthRatio = outerFrame.width / (double) block.getWidth();
		}
		double heightRatio;
		if (outerFrame.height == 0) {
			heightRatio = 1;
		} else {
			heightRatio = outerFrame.height / (double) block.getHeight();
		}

		int currentWidth = 0;
		int currentHeight = 0;
		for (Cell cell : block.getCells()) {
			int cellX = (int) ((block.getX() + currentWidth) * widthRatio) + outerFrame.x;
			int cellY = (int) ((block.getY() + currentHeight) * heightRatio) + outerFrame.y;
			int cellW = (int) (widthRatio * cell.getWidth());
			int cellH = (int) (heightRatio * cell.getHeight());
			if (cellX < 0) {
				cellX = 0;
			}
			if (cellY < 0) {
				cellY = 0;
			}
			if ((areaWidth > 0) && (areaHeight > 0)) {
				if (cellX + cellW > areaWidth) {
					cellW = areaWidth - cellX;
				}
				if (cellY + cellH > areaHeight) {
					cellH = areaHeight - cellY;
				}
			}
			areaMap.put(cell.getFingerNumber(), new Rectangle(cellX, cellY, cellW, cellH));

			// Do not increment height if line contains more than one cell.
			int tmpHeight;
			if ((cell.getWidth() + currentWidth) == block.getWidth()) {
				tmpHeight = cell.getHeight();
			} else {
				tmpHeight = 0;
			}
			if ((cell.getHeight() + currentHeight) <= block.getHeight()) {
				currentHeight += tmpHeight;
			} else {
				currentHeight = 0;
			}
			if ((cell.getWidth() + currentWidth) < block.getWidth()) {
				currentWidth += cell.getWidth();
			} else {
				currentWidth = 0;
			}
		}

		return areaMap;
	}

}
