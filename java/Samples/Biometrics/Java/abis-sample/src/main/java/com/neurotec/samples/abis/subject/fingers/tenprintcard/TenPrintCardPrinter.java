package com.neurotec.samples.abis.subject.fingers.tenprintcard;

import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Image;
import java.awt.Rectangle;
import java.awt.print.PageFormat;
import java.awt.print.Paper;
import java.awt.print.Printable;
import java.awt.print.PrinterException;
import java.io.IOException;
import java.util.Map;

import javax.print.attribute.HashPrintRequestAttributeSet;
import javax.print.attribute.PrintRequestAttributeSet;
import javax.print.attribute.standard.PrinterResolution;

import org.xml.sax.SAXException;

import com.neurotec.images.NImage;
import com.neurotec.samples.abis.subject.BiometricController.TenPrintCardType;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.form.FormDefinition;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.form.FormPainter;
import com.neurotec.samples.abis.swing.PrintPreview;

public class TenPrintCardPrinter extends PrintPreview implements Printable {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final TenPrintCard card;
	private FormDefinition form;
	private FormPainter formPainter;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public TenPrintCardPrinter(TenPrintCard card) {
		this.card = card;
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void printCard(FormDefinition cardForm) {
		this.form = cardForm;
		this.formPainter = new FormPainter(cardForm);
		setPrintable(this);

		PageFormat pageFormat = getPageFormat();
		Paper paper = pageFormat.getPaper();
		paper.setImageableArea(9, paper.getImageableY(), paper.getWidth() - 18, paper.getImageableHeight());
		pageFormat.setPaper(paper);
		setPageFormat(pageFormat);

		PrintRequestAttributeSet attributes = new HashPrintRequestAttributeSet();
		PrinterResolution pr = new PrinterResolution(100, 100, PrinterResolution.DPI);
		attributes.add(pr);
		setPrintAttributes(attributes);

		setVisible(true);
	}

	private Rectangle fitImage(Rectangle frameCell, Image fingerImage) {
		double widthRatio = 1.0 * frameCell.width / fingerImage.getWidth(null);
		double heightRatio = 1.0 * frameCell.height / fingerImage.getHeight(null);

		double ratio = widthRatio < heightRatio ? widthRatio : heightRatio;
		int w = (int) (fingerImage.getWidth(null) * ratio);
		int h = (int) (fingerImage.getHeight(null) * ratio);

		Rectangle rectangle = new Rectangle(frameCell.x, frameCell.y, w, h);
		int centerWidth = frameCell.width - w;
		if (centerWidth > 0) {
			rectangle.x += centerWidth / 2;
		}

		int centerHeight = frameCell.height - h;
		if (centerHeight > 0) {
			rectangle.y += centerHeight / 2;
		}

		return rectangle;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void printCard(TenPrintCardType type) throws IOException {
		FormDefinition cardForm;
		try {
			switch (type) {
			case APPLICANT:
				cardForm = FormDefinition.fromXml(getClass().getResourceAsStream("/ApplicantCard.xml"));
				break;
			case CRIMINAL:
				cardForm = FormDefinition.fromXml(getClass().getResourceAsStream("/CriminalCard.xml"));
				break;
			case FINGER_FRAME:
				cardForm = FormDefinition.fromXml(getClass().getResourceAsStream("/TenPrintCard.xml"));
				break;
			default:
				throw new IllegalArgumentException("type");
			}
		} catch (SAXException e) {
			throw new IOException("Error parsing XML file: " + e.getMessage());
		}
		printCard(cardForm);
	}

	@Override
	public int print(Graphics graphics, PageFormat pageFormat, int pageIndex) throws PrinterException {
		if (pageIndex != 0) {
			return NO_SUCH_PAGE;
		}

		Graphics2D g2d = (Graphics2D) graphics;
		g2d.translate((int) pageFormat.getImageableX(), (int) pageFormat.getImageableY());
		g2d.scale(0.72, 0.72);

		Map<Integer, Rectangle> frameSplitting = form.getFingerRectangles(new Rectangle(), 0, 0);
		Map<Integer, NImage> fingerImages = card.getImages();
		for (int i = 1; i <= 14; i++) {
			if (fingerImages.containsKey(i)) {
				Rectangle currrentRect = frameSplitting.get(i);
				Image fingerImage = fingerImages.get(i).toImage();
				Rectangle tmpRect = new Rectangle(currrentRect.x + 2, currrentRect.y + 2, currrentRect.width - 4, currrentRect.height - 4);

				Rectangle imagePosition = fitImage(tmpRect, fingerImage);
				g2d.drawImage(fingerImage, imagePosition.x, imagePosition.y, imagePosition.width, imagePosition.height, null);
				g2d.drawImage(fingerImage,
								imagePosition.x, imagePosition.y, imagePosition.x + imagePosition.width, imagePosition.y + imagePosition.height,
								0, 0, fingerImage.getWidth(null), fingerImage.getHeight(null),
								null);
			}
		}
		formPainter.paintForm(g2d);
		return PAGE_EXISTS;
	}

}
