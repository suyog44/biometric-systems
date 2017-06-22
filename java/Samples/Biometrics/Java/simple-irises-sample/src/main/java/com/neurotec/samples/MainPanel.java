package com.neurotec.samples;

import java.awt.Dimension;
import java.awt.GridLayout;
import java.io.IOException;

import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

public final class MainPanel extends JPanel implements ChangeListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JTabbedPane tabbedPane;
	private EnrollFromImage enrollFromImage;
	private EnrollFromScanner enrollFromScanner;
	private VerifyIris verifyIris;
	private IdentifyIris identifyIris;
	private SegmentIris segmentIris;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public MainPanel() {
		super(new GridLayout(1, 1));
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			e.printStackTrace();
		}
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		tabbedPane = new JTabbedPane();
		tabbedPane.addChangeListener(this);

		enrollFromImage = new EnrollFromImage();
		enrollFromImage.init();
		tabbedPane.addTab("Enroll from image", null, enrollFromImage);

		enrollFromScanner = new EnrollFromScanner();
		enrollFromScanner.init();
		tabbedPane.addTab("Enroll from scanner", null, enrollFromScanner);

		verifyIris = new VerifyIris();
		verifyIris.init();
		tabbedPane.addTab("Verify iris", null, verifyIris);

		identifyIris = new IdentifyIris();
		identifyIris.init();
		tabbedPane.addTab("Identify iris", null, identifyIris);

		segmentIris = new SegmentIris();
		segmentIris.init();
		tabbedPane.addTab("Segment iris", null, segmentIris);

		add(tabbedPane);
		setPreferredSize(new Dimension(630, 600));
		tabbedPane.setTabLayoutPolicy(JTabbedPane.SCROLL_TAB_LAYOUT);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void obtainLicenses(BasePanel panel) throws IOException {
		if (!panel.isObtained()) {
			boolean status = IrisesTools.getInstance().obtainLicenses(panel.getLicenses());
			panel.updateLicensing(status);
		}
	}

	// ===========================================================
	// Event handling
	// ===========================================================

	@Override
	public void stateChanged(ChangeEvent evt) {
		if (evt.getSource() == tabbedPane) {
			if ((enrollFromScanner != null) && (tabbedPane.getSelectedIndex() != 1)) {
				enrollFromScanner.cancelCapturing();
			}
			try {
				switch (tabbedPane.getSelectedIndex()) {
				case 0: {
					obtainLicenses(enrollFromImage);
					enrollFromImage.updateIrisesTools();
					break;
				}
				case 1: {
					obtainLicenses(enrollFromScanner);
					enrollFromScanner.updateIrisesTools();
					enrollFromScanner.updateScannerList();
					break;
				}
				case 2: {
					obtainLicenses(verifyIris);
					verifyIris.updateIrisesTools();
					break;
				}
				case 3: {
					obtainLicenses(identifyIris);
					identifyIris.updateIrisesTools();
					break;
				}
				case 4: {
					obtainLicenses(segmentIris);
					segmentIris.updateIrisesTools();
					break;
				}
				default: {
					throw new IndexOutOfBoundsException("unreachable");
				}
				}
			} catch (IOException e) {
				e.printStackTrace();
				JOptionPane.showMessageDialog(this, "Could not obtain licenses for components: " + e.toString(), "Error", JOptionPane.ERROR_MESSAGE);
			}
		}
	}

}
