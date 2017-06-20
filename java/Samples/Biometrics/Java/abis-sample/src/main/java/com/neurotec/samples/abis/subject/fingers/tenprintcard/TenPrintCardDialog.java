package com.neurotec.samples.abis.subject.fingers.tenprintcard;

import com.neurotec.biometrics.NFImpressionType;
import com.neurotec.biometrics.NFPosition;
import com.neurotec.biometrics.NFinger;
import com.neurotec.images.NImage;
import com.neurotec.samples.abis.subject.fingers.tenprintcard.form.FormDefinition;
import com.neurotec.samples.abis.util.MessageUtils;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFileChooser;
import javax.swing.JPanel;
import javax.swing.JToolBar;

import org.xml.sax.SAXException;

public final class TenPrintCardDialog extends JDialog implements ActionListener {

	// ===========================================================
	// Private fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private FramePainter painter;
	private JButton btnOpen;
	private JButton btnOK;
	private JPanel panelSouth;
	private JPanel panelButtons;
	private JButton btnCancel;

	private File lastFile;
	private List<NFinger> fingers;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public TenPrintCardDialog() throws IOException {
		initGUI();
		initFrameDefinition();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		setDefaultCloseOperation(JDialog.DISPOSE_ON_CLOSE);
		Dimension size = new Dimension(800, 600);
		setSize(size);
		setMinimumSize(size);
		setPreferredSize(size);
		getContentPane().setLayout(new BorderLayout());

		JToolBar toolBar = new JToolBar();
		getContentPane().add(toolBar, BorderLayout.NORTH);

		btnOpen = new JButton("Open");
		toolBar.add(btnOpen);
		btnOpen.addActionListener(this);

		painter = new FramePainter();
		getContentPane().add(painter, BorderLayout.CENTER);
		painter.setPreferredSize(new Dimension(800, 500));
		painter.setSize(new Dimension(800, 500));
		painter.setVisible(false);

		panelSouth = new JPanel();
		getContentPane().add(panelSouth, BorderLayout.SOUTH);
		panelSouth.setLayout(new BoxLayout(panelSouth, BoxLayout.Y_AXIS));
		{
			panelButtons = new JPanel(new FlowLayout(FlowLayout.RIGHT));
			panelSouth.add(panelButtons);

			btnOK = new JButton("OK");
			panelButtons.add(btnOK);
			btnOK.setEnabled(false);
			btnOK.addActionListener(this);

			btnCancel = new JButton("Cancel");
			panelButtons.add(btnCancel);
			btnCancel.addActionListener(this);
		}
	}

	private void initFrameDefinition() throws IOException {
		painter.setFrameDimensions(1.53, 50, 50, 600);
		try {
			painter.setForm(FormDefinition.fromXml(getClass().getResourceAsStream("/TenPrintCard.xml")));
		} catch (SAXException e) {
			throw new IOException("Can't parse frame definition");
		}
	}

	private void generateFingers() {
		Map<Integer, NImage> srcimg = painter.getFramedFingerprints();
		fingers = new ArrayList<NFinger>();
		NFinger finger;
		NFPosition[] positions = new NFPosition[] {
			NFPosition.RIGHT_THUMB, NFPosition.RIGHT_INDEX_FINGER, NFPosition.RIGHT_MIDDLE_FINGER, NFPosition.RIGHT_RING_FINGER, NFPosition.RIGHT_LITTLE_FINGER,
			NFPosition.LEFT_THUMB, NFPosition.LEFT_INDEX_FINGER, NFPosition.LEFT_MIDDLE_FINGER, NFPosition.LEFT_RING_FINGER, NFPosition.LEFT_LITTLE_FINGER};

		for (int i = 0; i < positions.length; i++) {
			finger = new NFinger();
			finger.setPosition(positions[i]);
			finger.setImpressionType(NFImpressionType.NON_LIVE_SCAN_ROLLED);
			finger.setImage(srcimg.get(i + 1));
			fingers.add(finger);
		}

		finger = new NFinger();
		finger.setPosition(NFPosition.PLAIN_LEFT_FOUR_FINGERS);
		finger.setImpressionType(NFImpressionType.NON_LIVE_SCAN_PLAIN);
		finger.setImage(srcimg.get(11));
		fingers.add(finger);

		finger = new NFinger();
		finger.setPosition(NFPosition.LEFT_THUMB);
		finger.setImpressionType(NFImpressionType.NON_LIVE_SCAN_PLAIN);
		finger.setImage(srcimg.get(12));
		fingers.add(finger);

		finger = new NFinger();
		finger.setPosition(NFPosition.RIGHT_THUMB);
		finger.setImpressionType(NFImpressionType.NON_LIVE_SCAN_PLAIN);
		finger.setImage(srcimg.get(13));
		fingers.add(finger);

		finger = new NFinger();
		finger.setPosition(NFPosition.PLAIN_RIGHT_FOUR_FINGERS);
		finger.setImpressionType(NFImpressionType.NON_LIVE_SCAN_PLAIN);
		finger.setImage(srcimg.get(14));
		fingers.add(finger);

		for (NImage image : srcimg.values()) {
			image.dispose();
		}
	}

	private void setImage(NImage image) {
		painter.setVisible(true);
		painter.setImage(image);
		btnOK.setEnabled(image != null);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public List<NFinger> getFingers() {
		return fingers;
	}

	@Override
	public void actionPerformed(ActionEvent e) {
		if (e.getSource() == btnOpen) {
			JFileChooser fc = new JFileChooser();
			fc.setMultiSelectionEnabled(false);
			fc.setCurrentDirectory(lastFile);
			if (fc.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
				lastFile = fc.getSelectedFile();
				try {
					setImage(NImage.fromFile(fc.getSelectedFile().getAbsolutePath()));
				} catch (IOException ex) {
					MessageUtils.showError(getParent(), ex);
				}
			}
		} else if (e.getSource() == btnOK) {
			generateFingers();
			dispose();
		} else if (e.getSource() == btnCancel) {
			dispose();
		}
	}

}
