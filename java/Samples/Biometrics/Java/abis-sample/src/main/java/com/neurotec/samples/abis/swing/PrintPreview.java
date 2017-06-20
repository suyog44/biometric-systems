package com.neurotec.samples.abis.swing;

import com.neurotec.samples.abis.util.MessageUtils;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Graphics2D;
import java.awt.GraphicsConfiguration;
import java.awt.GraphicsDevice;
import java.awt.GraphicsEnvironment;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.awt.image.BufferedImage;
import java.awt.print.PageFormat;
import java.awt.print.Printable;
import java.awt.print.PrinterException;
import java.awt.print.PrinterJob;

import javax.print.attribute.HashPrintRequestAttributeSet;
import javax.print.attribute.PrintRequestAttributeSet;
import javax.swing.Box.Filler;
import javax.swing.ButtonGroup;
import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JToggleButton;

public class PrintPreview extends JFrame implements ActionListener, ItemListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final PrinterJob printerJob;
	private PageFormat pageFormat;
	private PrintRequestAttributeSet printAttributes;
	private Printable printable;
	private Page page;
	private double scale;

	private JButton btnClose;
	private JToggleButton btnLandscape;
	private JButton btnPageSetup;
	private JToggleButton btnPortrait;
	private JButton btnPrint;
	private ButtonGroup buttonGroupOrientation;
	private JComboBox comboBoxScale;
	private Filler filler1;
	private Filler filler2;
	private Filler filler3;
	private JLabel lblScale;
	private JPanel panelTop;
	private JScrollPane spPage;

	// ===========================================================
	// Public constructors
	// ===========================================================

	public PrintPreview() {
		super("Print Preview");
		this.printerJob = PrinterJob.getPrinterJob();
		this.pageFormat = printerJob.defaultPage();
		this.printAttributes = new HashPrintRequestAttributeSet();
		this.scale = 1.0;

		initGUI();
		updatePreview();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		buttonGroupOrientation = new ButtonGroup();
		panelTop = new JPanel();
		btnPrint = new JButton();
		btnPageSetup = new JButton();
		filler1 = new Filler(new Dimension(5, 0), new Dimension(5, 0), new Dimension(5, 32767));
		lblScale = new JLabel();
		comboBoxScale = new JComboBox();
		filler2 = new Filler(new Dimension(5, 0), new Dimension(5, 0), new Dimension(5, 32767));
		btnPortrait = new JToggleButton();
		btnLandscape = new JToggleButton();
		filler3 = new Filler(new Dimension(5, 0), new Dimension(5, 0), new Dimension(5, 32767));
		btnClose = new JButton();
		spPage = new JScrollPane();

		setLayout(new BorderLayout());

		panelTop.setLayout(new FlowLayout(FlowLayout.LEADING));

		btnPrint.setText("Print...");
		btnPrint.setPreferredSize(new Dimension(99, 23));
		panelTop.add(btnPrint);

		btnPageSetup.setText("Page Setup...");
		panelTop.add(btnPageSetup);
		panelTop.add(filler1);

		lblScale.setText("Scale:");
		panelTop.add(lblScale);

		comboBoxScale.setModel(new DefaultComboBoxModel(new String[] {"30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%", "125%", "150%", "175%", "200%", "300%", "500%"}));
		comboBoxScale.setSelectedItem(comboBoxScale.getItemAt(7));
		panelTop.add(comboBoxScale);
		panelTop.add(filler2);

		buttonGroupOrientation.add(btnPortrait);
		btnPortrait.setSelected(true);
		btnPortrait.setText("Portrait");
		btnPortrait.setPreferredSize(new Dimension(83, 23));
		panelTop.add(btnPortrait);

		buttonGroupOrientation.add(btnLandscape);
		btnLandscape.setText("Landscape");
		panelTop.add(btnLandscape);
		panelTop.add(filler3);

		btnClose.setText("Close");
		panelTop.add(btnClose);

		add(panelTop, BorderLayout.NORTH);
		add(spPage, BorderLayout.CENTER);

		btnPrint.setEnabled(false);

		btnPrint.addActionListener(this);
		btnPageSetup.addActionListener(this);
		btnPortrait.addActionListener(this);
		btnLandscape.addActionListener(this);
		btnClose.addActionListener(this);
		comboBoxScale.addItemListener(this);

		this.setSize(1200, 800);
		this.setLocationRelativeTo(null);
	}

	private void updatePreview() {
		Dimension size = new Dimension((int) pageFormat.getPaper().getWidth(), (int) pageFormat.getPaper().getHeight());
		if (pageFormat.getOrientation() == PageFormat.PORTRAIT) {
			btnPortrait.setSelected(true);
		} else {
			btnLandscape.setSelected(true);
			size = new Dimension(size.height, size.width);
		}
		if (printable != null) {
			page = new Page(size);
			spPage.setViewportView(page);
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setPrintable(Printable printable) {
		if (printable == null) {
			throw new NullPointerException("printable");
		}
		this.printable = printable;
		btnPrint.setEnabled(true);
		updatePreview();
	}

	public void setPageFormat(PageFormat pageFormat) {
		if (pageFormat == null) {
			throw new NullPointerException("pageFormat");
		}
		this.pageFormat = pageFormat;
		updatePreview();
	}

	public PageFormat getPageFormat() {
		return pageFormat;
	}

	public void setPrintAttributes(PrintRequestAttributeSet printAttributes) {
		if (pageFormat == null) {
			throw new NullPointerException("printAttributes");
		}
		this.printAttributes = printAttributes;
		updatePreview();
	}

	@Override
	public void itemStateChanged(ItemEvent ev) {
		if (ev.getSource().equals(comboBoxScale) && (ev.getStateChange() == ItemEvent.SELECTED)) {
			String selected = (String) comboBoxScale.getSelectedItem();
			scale = Double.parseDouble(selected.trim().replace("%", "")) / 100;

			if (printable != null) {
				page.refresh();
				invalidate();
				validate();
			}
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource().equals(btnPrint)) {
				printerJob.setPrintable(printable, pageFormat);
				if (printerJob.printDialog(printAttributes)) {
					printerJob.print();
				}
			} else if (ev.getSource().equals(btnPageSetup)) {
				pageFormat = printerJob.pageDialog(pageFormat);
				updatePreview();
			} else if (ev.getSource().equals(btnLandscape)) {
				pageFormat.setOrientation(PageFormat.LANDSCAPE);
				updatePreview();
			} else if (ev.getSource().equals(btnPortrait)) {
				pageFormat.setOrientation(PageFormat.PORTRAIT);
				updatePreview();
			} else if (ev.getSource().equals(btnClose)) {
				dispose();
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	// ===========================================================
	// Inner classes
	// ===========================================================

	class Page extends JLabel {

		private static final long serialVersionUID = 1L;

		private final Dimension size;
		private BufferedImage image;

		private final GraphicsConfiguration config;

		Page(Dimension size) {
			this.size = size;

			GraphicsEnvironment env = GraphicsEnvironment.getLocalGraphicsEnvironment();
			GraphicsDevice device = env.getDefaultScreenDevice();
			this.config = device.getDefaultConfiguration();

			refresh();
		}

		@Override
		public Dimension getPreferredSize() {
			double s = scale;
			int w = (int) (s * size.width);
			int h = (int) (s * size.height);
			return new Dimension(w, h);
		}

		public final void refresh() {
			image = config.createCompatibleImage((int) (size.getWidth() * scale), (int) (size.getHeight() * scale));
			Graphics2D g2d = image.createGraphics();
			try {
				g2d.scale(scale, scale);
				Color c = g2d.getColor();
				g2d.setColor(Color.WHITE);
				g2d.fillRect(0, 0, (int) (pageFormat.getWidth()), (int) (pageFormat.getHeight()));
				g2d.setColor(c);
				g2d.clipRect(0, 0, (int) (pageFormat.getWidth()), (int) (pageFormat.getHeight()));
				printable.print(g2d, pageFormat, 0);
			} catch (PrinterException e) {
				e.printStackTrace();
			} finally {
				g2d.dispose();
			}
			this.setIcon(new ImageIcon(image));
			repaint();
		}

	}

}
