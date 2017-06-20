package com.neurotec.samples.biometrics;

import java.awt.BasicStroke;
import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Frame;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.GridLayout;
import java.awt.Rectangle;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.MouseMotionListener;
import java.awt.geom.Ellipse2D;
import java.awt.image.BufferedImage;
import java.awt.image.ColorModel;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.nio.ByteBuffer;

import javax.imageio.ImageIO;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JScrollPane;
import javax.swing.JSlider;
import javax.swing.JSplitPane;
import javax.swing.SwingUtilities;

import com.neurotec.images.NImage;
import com.neurotec.images.NPixelFormat;
import com.neurotec.images.processing.NGIP;
import com.neurotec.images.processing.NGIP.FFTResult;
import com.neurotec.samples.util.Utils;

public final class BandpassFilteringDialog extends JDialog implements ActionListener {

	// ==============================================
	// Static nested classes
	// ==============================================

	private static enum PenType {
		CIRCLE, RECTANGLE
	}

	private static final class FourierMaskView extends JLabel {

		private static final long serialVersionUID = 1L;
		private BufferedImage image;

		void init(BufferedImage img) {
			this.image = img;
			setIcon(new ImageIcon(image));
		}

		@Override
		protected void paintComponent(Graphics g) {
			super.paintComponent(g);
			g.drawImage(image, 0, 0, image.getWidth(), image.getHeight(), null);
		}

	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private NImage originalReal;
	private NImage originalImaginary;
	private NImage result;

	private Graphics graphics;
	private Graphics gr;

	private BufferedImage maskBitmap;
	private BufferedImage fftBitmap;

	private int imgWidth;
	private int imgHeight;
	private int lastX;
	private int lastY;
	private int originalWidth;
	private int originalHeight;

	private PenType penType;
	private boolean allowPainting;

	private final MainFrameEventListener listener;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JButton btnSetAll;
	private JButton btnResetAll;
	private JButton btnInvert;
	private JButton btnRefresh;
	private JCheckBox chkAutoRefresh;

	private JRadioButton radioCircle;
	private JRadioButton radioRect;

	private FourierMaskView fourierMaskView;
	private JLabel lblResult;

	private JSlider sizeSlider;

	private JButton btnAccept;
	private JButton btnDismiss;

	// ==============================================
	// Public constructor
	// ==============================================

	public BandpassFilteringDialog(Frame owner, MainFrameEventListener listener, NImage original) {
		super(owner, "Bandpass Filtering", true);
		this.listener = listener;
		setPreferredSize(new Dimension(840, 460));
		setMinimumSize(new Dimension(15, 285));

		initializeComponents();

		sizeSlider.setValue(20);
		penType = PenType.CIRCLE;
		radioCircle.setSelected(true);
		allowPainting = false;

		loadImages(original);
		fillMask(Color.BLACK);
		updateFFT(true);
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		getContentPane().setLayout(new BorderLayout());

		JSplitPane splitPane = new JSplitPane();

		splitPane.setLeftComponent(createLeftSplitPanel());
		splitPane.setRightComponent(createRightSplitPanel());
		splitPane.setDividerLocation(360);

		getContentPane().add(createLeftPanel(), BorderLayout.BEFORE_LINE_BEGINS);
		getContentPane().add(splitPane, BorderLayout.CENTER);
		pack();
	}

	private JPanel createLeftPanel() {

		JPanel leftPanel = new JPanel();
		leftPanel.setLayout(new BoxLayout(leftPanel, BoxLayout.Y_AXIS));
		leftPanel.setPreferredSize(new Dimension(95, 380));
		leftPanel.setMaximumSize(new Dimension(95, 380));

		btnAccept = new JButton("Accept");
		btnAccept.addActionListener(this);

		btnDismiss = new JButton("Dismiss");
		btnDismiss.addActionListener(this);

		JPanel operationsPanel = createOperationsPanel();
		JPanel toolsPanel = createToolsPanel();

		leftPanel.add(operationsPanel);
		leftPanel.add(toolsPanel);
		leftPanel.add(btnAccept);
		leftPanel.add(btnDismiss);

		operationsPanel.setAlignmentX(LEFT_ALIGNMENT);
		toolsPanel.setAlignmentX(LEFT_ALIGNMENT);
		btnAccept.setAlignmentX(LEFT_ALIGNMENT);
		btnDismiss.setAlignmentX(LEFT_ALIGNMENT);

		return leftPanel;

	}

	private JPanel createOperationsPanel() {
		JPanel operationsPanel = new JPanel();
		operationsPanel.setLayout(new GridLayout(6, 1, 2, 2));

		JPanel operationsTitlePanel = new JPanel();
		operationsTitlePanel.add(new JLabel("Operations"));
		operationsTitlePanel.setOpaque(true);
		operationsTitlePanel.setBackground(Color.GRAY);

		btnSetAll = new JButton("Set all");
		btnSetAll.addActionListener(this);

		btnResetAll = new JButton("Reset all");
		btnResetAll.addActionListener(this);

		btnInvert = new JButton("Invert");
		btnInvert.addActionListener(this);

		btnRefresh = new JButton("Refresh");
		btnRefresh.addActionListener(this);

		chkAutoRefresh = new JCheckBox("Auto refresh");
		chkAutoRefresh.setSelected(true);

		operationsPanel.add(operationsTitlePanel);
		operationsPanel.add(btnSetAll);
		operationsPanel.add(btnResetAll);
		operationsPanel.add(btnInvert);
		operationsPanel.add(btnRefresh);
		operationsPanel.add(chkAutoRefresh);

		operationsTitlePanel.setAlignmentX(LEFT_ALIGNMENT);
		btnSetAll.setAlignmentX(LEFT_ALIGNMENT);
		btnResetAll.setAlignmentX(LEFT_ALIGNMENT);
		btnInvert.setAlignmentX(LEFT_ALIGNMENT);
		btnRefresh.setAlignmentX(LEFT_ALIGNMENT);
		btnRefresh.setAlignmentX(LEFT_ALIGNMENT);
		return operationsPanel;
	}

	private JPanel createToolsPanel() {
		JPanel toolsPanel = new JPanel();
		toolsPanel.setLayout(new BoxLayout(toolsPanel, BoxLayout.Y_AXIS));

		JPanel toolsTitlePanel = new JPanel();
		toolsTitlePanel.add(new JLabel("Tools"));
		toolsTitlePanel.setOpaque(true);
		toolsTitlePanel.setBackground(Color.GRAY);
		toolsTitlePanel.setPreferredSize(new Dimension(95, 20));
		toolsTitlePanel.setMaximumSize(new Dimension(95, 20));

		JPanel radioPanel = new JPanel(new FlowLayout());
		radioCircle = new JRadioButton(Utils.createIcon("images/ToolCircle.png"));
		radioCircle.setSelectedIcon(Utils.createIcon("images/ToolCircleSelected.png"));
		radioCircle.setRolloverIcon(Utils.createIcon("images/ToolCircleSelected.png"));
		radioCircle.addActionListener(this);

		radioRect = new JRadioButton(Utils.createIcon("images/ToolRect.png"));
		radioRect.setSelectedIcon(Utils.createIcon("images/ToolRectSelected.png"));
		radioRect.setRolloverIcon(Utils.createIcon("images/ToolRectSelected.png"));
		radioRect.addActionListener(this);

		ButtonGroup toolsGroup = new ButtonGroup();
		toolsGroup.add(radioCircle);
		toolsGroup.add(radioRect);

		radioCircle.setSelected(true);

		radioPanel.add(radioCircle);
		radioPanel.add(radioRect);

		JLabel lblSize = new JLabel("Size:");

		sizeSlider = new JSlider(1, 150, 20);

		toolsPanel.add(toolsTitlePanel);
		toolsPanel.add(radioPanel);
		toolsPanel.add(lblSize);
		toolsPanel.add(sizeSlider);
		toolsPanel.add(Box.createGlue());
		return toolsPanel;
	}

	private JPanel createLeftSplitPanel() {
		JPanel leftSplitPanel = new JPanel(new BorderLayout());

		JPanel leftTitlePanel = new JPanel();
		leftTitlePanel.add(new JLabel("Fourier image and mask"));
		leftTitlePanel.setOpaque(true);
		leftTitlePanel.setBackground(Color.GRAY);

		JPanel leftImagePanel = new JPanel();
		fourierMaskView = new FourierMaskView();
		fourierMaskView.addMouseListener(new MouseAdapter() {

			@Override
			public void mouseDragged(MouseEvent e) {
				viewFourierMouseMove(e);
			}

			@Override
			public void mousePressed(MouseEvent e) {
				viewFourierMouseDown(e);
			}

			@Override
			public void mouseReleased(MouseEvent e) {
				viewFourierMouseUp(e);
			}
		});
		fourierMaskView.addMouseMotionListener(new MouseMotionListener() {

			public void mouseMoved(MouseEvent e) {
				// Do nothing.
			}

			public void mouseDragged(MouseEvent e) {
				viewFourierMouseMove(e);

			}
		});

		leftImagePanel.add(fourierMaskView);
		JScrollPane leftScrollPane = new JScrollPane(leftImagePanel, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);

		leftSplitPanel.add(leftTitlePanel, BorderLayout.BEFORE_FIRST_LINE);
		leftSplitPanel.add(leftScrollPane, BorderLayout.CENTER);
		return leftSplitPanel;
	}

	private JPanel createRightSplitPanel() {
		JPanel rightSplitPanel = new JPanel(new BorderLayout());

		JPanel rightTitlePanel = new JPanel();
		rightTitlePanel.add(new JLabel("Filtered image"));
		rightTitlePanel.setOpaque(true);
		rightTitlePanel.setBackground(Color.GRAY);

		JPanel rightImagePanel = new JPanel();
		lblResult = new JLabel();
		rightImagePanel.add(lblResult);

		JScrollPane rightScrollPane = new JScrollPane(rightImagePanel, JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);

		rightSplitPanel.add(rightTitlePanel, BorderLayout.BEFORE_FIRST_LINE);
		rightSplitPanel.add(rightScrollPane, BorderLayout.CENTER);
		return rightSplitPanel;
	}

	private void loadImages(NImage original) {
		Dimension d = NGIP.fftGetOptimalSize(original);

		imgWidth = d.width;
		imgHeight = d.height;
		originalWidth = original.getWidth();
		originalHeight = original.getHeight();

		BufferedImage image = new BufferedImage(d.width, d.height, BufferedImage.TYPE_INT_ARGB);
		fourierMaskView.init(image);
		gr = image.createGraphics();
		maskBitmap = new BufferedImage(d.width, d.height, BufferedImage.TYPE_INT_ARGB);
		graphics = maskBitmap.createGraphics();

		NImage originalImg = NImage.create(NPixelFormat.GRAYSCALE_8U, d.width, d.height, 0);
		originalImg.setHorzResolution(original.getHorzResolution());
		originalImg.setVertResolution(original.getVertResolution());
		originalImg.setResolutionIsAspectRatio(false);

		result = NImage.create(NPixelFormat.GRAYSCALE_8U, d.width, d.height, 0);
		result.setHorzResolution(originalImg.getHorzResolution());
		result.setVertResolution(originalImg.getVertResolution());
		result.setResolutionIsAspectRatio(false);

		NImage.copy(original, 0, 0, originalWidth, originalHeight, originalImg, d.width / 2 - originalWidth / 2, d.height / 2 - originalHeight / 2);

		FFTResult fftResult = NGIP.fft(originalImg);

		originalReal = fftResult.getReal();
		originalImaginary = fftResult.getImaginary();

		if (originalImaginary == null) {
			try {
				originalImaginary = (NImage) originalReal.clone();
			} catch (CloneNotSupportedException e) {
				throw new AssertionError("Can't happen");
			}
		}

		NImage fft = NGIP.createMagnitudeFromSpectrum(originalReal, originalImaginary);
		fftBitmap = (BufferedImage) NImage.fromImage(NPixelFormat.RGB_8U, 0, shiftFFT(fft)).toImage();
	}

	private NImage shiftFFT(NImage inp) {
		int cx, cy;
		NImage shifted = NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, inp);
		cx = inp.getWidth() / 2;
		cy = inp.getHeight() / 2;

		NImage.copy(inp, 0, 0, cx, cy, shifted, cx, cy);
		NImage.copy(inp, cx, cy, cx, cy, shifted, 0, 0);
		NImage.copy(inp, cx, 0, cx, cy, shifted, 0, cy);
		NImage.copy(inp, 0, cy, cx, cy, shifted, cx, 0);

		return shifted;
	}

	private void fillMask(Color color) {
		graphics.setColor(color);
		((Graphics2D) graphics).setStroke(new BasicStroke(1));
		Rectangle rect = new Rectangle(0, 0, imgWidth, imgHeight);
		((Graphics2D) graphics).fill(rect);
	}

	private void updateFFT(boolean ifft) {
		NImage mask = null;
		NImage resultReal = null;
		NImage resultImaginary = null;
		NImage bmpimg = null;
		NImage tmp = null;

		try {
			if (ifft) {
				ByteArrayOutputStream baos = new ByteArrayOutputStream();
				ImageIO.write(maskBitmap, "png", baos);

				byte[] imageInByte = baos.toByteArray();
				baos.flush();
				baos.close();
				bmpimg = NImage.fromMemory(ByteBuffer.wrap(imageInByte));
				mask = shiftFFT(NImage.fromImage(NPixelFormat.GRAYSCALE_8U, 0, bmpimg));

				resultReal = NImage.fromImage(originalReal.getPixelFormat(), 0, originalReal);
				resultImaginary = NImage.fromImage(originalImaginary.getPixelFormat(), 0, originalImaginary);

				NGIP.applyMaskToSpectrum(resultReal, resultImaginary, mask);

				tmp = NGIP.ifft(resultReal, resultImaginary);
				if (tmp != null) {
					result = tmp.crop((tmp.getWidth() - originalWidth) / 2, (tmp.getHeight() - originalHeight) / 2, originalWidth, originalHeight);
					lblResult.setIcon(new ImageIcon(result.toImage()));
					lblResult.updateUI();
				}
			}
			gr.drawImage(fftBitmap, 0, 0, imgWidth, imgHeight, null);
			int x;
			int y;
			for (x = 0; x < maskBitmap.getWidth(); x++) {
				for (y = 0; y < maskBitmap.getHeight(); y++) {
					ColorModel colorModel = maskBitmap.getColorModel();
					int pixel = maskBitmap.getRGB(x, y);
					Color myColor2 = new Color(colorModel.getRed(pixel), 0, 0, 64);
					maskBitmap.setRGB(x, y, myColor2.getRGB());
				}
			}
			gr.drawImage(maskBitmap, 0, 0, imgWidth, imgHeight, 0, 0, imgWidth, imgHeight, null);
		} catch (IOException e) {
			e.printStackTrace();
			JOptionPane.showMessageDialog(this, String.format("Error updating FFT image: %s", e.getMessage()), getTitle(), JOptionPane.ERROR_MESSAGE);
			return;
		} finally {
			if (tmp != null) {
				tmp.dispose();
			}

			if (bmpimg != null) {
				bmpimg.dispose();
			}

			if (resultImaginary != null) {
				resultImaginary.dispose();
			}

			if (resultReal != null) {
				resultReal.dispose();
			}

			if (mask != null) {
				mask.dispose();
			}
		}
		fourierMaskView.updateUI();

	}

	private void invert(BufferedImage b) {
		int x;
		int y;
		for (x = 0; x < b.getWidth(); x++) {
			for (y = 0; y < b.getHeight(); y++) {
				ColorModel colorModel = b.getColorModel();
				int pixel = b.getRGB(x, y);
				Color myColor2 = new Color(255 - colorModel.getRed(pixel), 255 - colorModel.getGreen(pixel), 255 - colorModel.getBlue(pixel),
						colorModel.getAlpha(pixel));
				b.setRGB(x, y, myColor2.getRGB());
			}
		}
	}

	private void draw(MouseEvent e) {
		Color color;
		if (SwingUtilities.isLeftMouseButton(e)) {
			color = Color.WHITE;
		} else if (SwingUtilities.isRightMouseButton(e)) {
			color = Color.BLACK;
		} else {
			return;
		}

		if (lastX == e.getX() && lastY == e.getY()) {
			graphics.setColor(color);
			((Graphics2D) graphics).setStroke(new BasicStroke(1));
			Rectangle rect = new Rectangle(e.getX() - sizeSlider.getValue() / 2, e.getY() - sizeSlider.getValue() / 2, sizeSlider.getValue(),
					sizeSlider.getValue());

			if (penType == PenType.CIRCLE) {
				Ellipse2D.Double ellipse = new Ellipse2D.Double(rect.x, rect.y, rect.width, rect.height);
				((Graphics2D) graphics).fill(ellipse);
			} else {
				((Graphics2D) graphics).fill(rect);
			}
		} else {
			Graphics2D g = (Graphics2D) graphics;
			g.setColor(color);
			if (penType == PenType.CIRCLE) {
				g.setStroke(new BasicStroke(sizeSlider.getValue(), BasicStroke.CAP_ROUND, BasicStroke.CAP_ROUND));
			} else {
				g.setStroke(new BasicStroke(sizeSlider.getValue(), BasicStroke.CAP_SQUARE, BasicStroke.CAP_SQUARE));
			}
			g.drawLine(lastX, lastY, e.getX(), e.getY());
		}
	}

	private void viewFourierMouseDown(MouseEvent e) {
		if (SwingUtilities.isLeftMouseButton(e) || SwingUtilities.isRightMouseButton(e)) {
			lastX = e.getX();
			lastY = e.getY();
			allowPainting = true;
		}
	}

	private void viewFourierMouseMove(MouseEvent e) {
		if (!allowPainting) {
			return;
		}
		if (SwingUtilities.isLeftMouseButton(e) || SwingUtilities.isRightMouseButton(e)) {
			draw(e);
			lastX = e.getX();
			lastY = e.getY();
			updateFFT(false);
		}
	}

	private void viewFourierMouseUp(MouseEvent e) {
		if (!allowPainting) {
			return;
		}
		if (SwingUtilities.isLeftMouseButton(e) || SwingUtilities.isRightMouseButton(e)) {
			draw(e);
			lastX = e.getX();
			lastY = e.getY();
			updateFFT(chkAutoRefresh.isSelected());
			allowPainting = false;
		}
	}

	private void setAll() {
		fillMask(Color.WHITE);
		updateFFT(chkAutoRefresh.isSelected());
	}

	private void resetAll() {
		fillMask(Color.BLACK);
		updateFFT(chkAutoRefresh.isSelected());
	}

	private void invertClicked() {
		invert(maskBitmap);
		updateFFT(chkAutoRefresh.isSelected());
	}

	private void accept() {
		listener.bandPassFilteringAccepted(getResultImage());
		dispose();
	}

	private void radioCircleCheckedChanged() {
		if (radioCircle.isSelected()) {
			penType = PenType.CIRCLE;
		}
	}

	private void radioRectCheckedChanged() {
		if (radioRect.isSelected()) {
			penType = PenType.RECTANGLE;
		}
	}

	// ==============================================
	// Public methods
	// ==============================================

	public NImage getResultImage() {
		return result;
	}

	// ==============================================
	// Event handling ActionListener
	// ==============================================

	public void actionPerformed(ActionEvent e) {
		Object source = e.getSource();
		if (source == btnSetAll) {
			setAll();
		} else if (source == btnResetAll) {
			resetAll();
		} else if (source == btnInvert) {
			invertClicked();
		} else if (source == btnRefresh) {
			updateFFT(true);
		} else if (source == btnAccept) {
			accept();
		} else if (source == btnDismiss) {
			dispose();
		} else if (source == radioCircle) {
			radioCircleCheckedChanged();
		} else if (source == radioRect) {
			radioRectCheckedChanged();
		}
	}

}
