package com.neurotec.samples;

import java.awt.Component;
import java.awt.Dimension;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.swing.JFrame;
import javax.swing.JOptionPane;
import javax.swing.JTabbedPane;
import javax.swing.SwingUtilities;
import javax.swing.event.ChangeEvent;
import javax.swing.event.ChangeListener;

import com.neurotec.lang.NCore;
import com.neurotec.samples.util.LibraryManager;
import com.neurotec.samples.util.Utils;

public final class SimpleFacesApplication implements ChangeListener {

	// ===========================================================
	// Static constructor
	// ===========================================================

	static {
		try {
			javax.swing.UIManager.setLookAndFeel(javax.swing.UIManager.getSystemLookAndFeelClassName());
		} catch (Exception e) {
			Logger.getLogger(Logger.getLogger("global").getName()).log(Level.FINE, e.getMessage(), e);
		}
	}

	// ===========================================================
	// Public static methods
	// ===========================================================

	public static void main(String[] args) {
		new SimpleFacesApplication();
	}

	// ===========================================================
	// Private fields
	// ===========================================================

	private JTabbedPane tabbedPane;
	private BasePanel panelDetectFaces;
	private BasePanel panelEnrollFromImage;
	private BasePanel panelEnrollFromCamera;
	private BasePanel panelIdentifyFace;
	private BasePanel panelVerifyFace;
	private BasePanel panelMatchMultipleFaces;
	private BasePanel panelCreateTokenFaceImage;
	private BasePanel panelGeneralizeFace;
	private BasePanel panelcaptureIcaoCompliantImage;

	// ===========================================================
	// Private constructor
	// ===========================================================

	private SimpleFacesApplication() {

		LibraryManager.initLibraryPath();

		SwingUtilities.invokeLater(new Runnable() {
			@Override
			public void run() {
				JFrame frame = new JFrame();
				try {
					Dimension d = new Dimension(800, 600);

					frame.setSize(d);
					frame.setMinimumSize(d);
					frame.setPreferredSize(d);

					frame.setResizable(true);
					frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
					frame.setTitle("Simple faces sample");
					frame.setIconImage(Utils.createIconImage("images/Logo16x16.png"));

					tabbedPane = new JTabbedPane();
					frame.add(tabbedPane);

					addTabs(tabbedPane);

					frame.addWindowListener(new WindowAdapter() {
						@Override
						public void windowClosing(WindowEvent e) {
							dispose();
						}
					});
					frame.setLocationRelativeTo(null);
					frame.setVisible(true);

				} catch (Throwable e) {
					showError(null, e);
				}
			}
		});
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void addTabs(JTabbedPane tabbedPane) {
		tabbedPane.addChangeListener(this);

		panelDetectFaces = new DetectFaces();
		panelDetectFaces.init();
		tabbedPane.addTab(panelDetectFaces.getName(), panelDetectFaces);

		panelEnrollFromImage = new EnrollFromImage();
		panelEnrollFromImage.init();
		tabbedPane.addTab(panelEnrollFromImage.getName(), panelEnrollFromImage);

		panelEnrollFromCamera = new EnrollFromCamera();
		panelEnrollFromCamera.init();
		tabbedPane.addTab(panelEnrollFromCamera.getName(), panelEnrollFromCamera);

		panelIdentifyFace = new IdentifyFace();
		panelIdentifyFace.init();
		tabbedPane.addTab(panelIdentifyFace.getName(), panelIdentifyFace);

		panelVerifyFace = new VerifyFace();
		panelVerifyFace.init();
		tabbedPane.addTab(panelVerifyFace.getName(), panelVerifyFace);

		panelMatchMultipleFaces = new MatchMultipleFaces();
		panelMatchMultipleFaces.init();
		tabbedPane.addTab(panelMatchMultipleFaces.getName(), panelMatchMultipleFaces);

		panelCreateTokenFaceImage = new CreateTokenFaceImage();
		panelCreateTokenFaceImage.init();
		tabbedPane.addTab(panelCreateTokenFaceImage.getName(), panelCreateTokenFaceImage);

		panelGeneralizeFace = new GeneralizeFace();
		panelGeneralizeFace.init();
		tabbedPane.addTab(panelGeneralizeFace.getName(), panelGeneralizeFace);

		panelcaptureIcaoCompliantImage = new CaptureIcaoCompliantImage();
		panelcaptureIcaoCompliantImage.init();
		tabbedPane.addTab(panelcaptureIcaoCompliantImage.getName(), panelcaptureIcaoCompliantImage);
	}

	private void showError(Component parentComponent, Throwable e) {
		e.printStackTrace();
		String message;
		if (e.getMessage() != null) {
			message = e.getMessage();
		} else if (e.getCause() != null) {
			message = e.getCause().getMessage();
		} else {
			message = "An error occurred. Please see log for more details.";
		}
		JOptionPane.showMessageDialog(parentComponent, message, "Error", JOptionPane.ERROR_MESSAGE);
	}

	private void dispose() {
		for (Component component : tabbedPane.getComponents()) {
			if (component instanceof BasePanel) {
				((BasePanel) component).onDestroy();
			}
		}
		FaceTools.getInstance().releaseLicenses();
		NCore.shutdown();
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void stateChanged(ChangeEvent ev) {
		if (ev.getSource() == tabbedPane) {

			if (panelEnrollFromCamera != null) {
				panelEnrollFromCamera.onClose();
			}
			try {
				switch (tabbedPane.getSelectedIndex()) {
				case 0:
					obtainLicenses(panelDetectFaces);
					FaceTools.getInstance().resetParameters();
					break;
				case 1:
					obtainLicenses(panelEnrollFromImage);
					FaceTools.getInstance().resetParameters();
					break;
				case 2:
					obtainLicenses(panelEnrollFromCamera);
					FaceTools.getInstance().resetParameters();
					break;
				case 3:
					obtainLicenses(panelIdentifyFace);
					FaceTools.getInstance().resetParameters();
					break;
				case 4:
					obtainLicenses(panelVerifyFace);
					FaceTools.getInstance().resetParameters();
					break;
				case 5:
					obtainLicenses(panelMatchMultipleFaces);
					FaceTools.getInstance().resetParameters();
					break;
				case 6:
					obtainLicenses(panelCreateTokenFaceImage);
					FaceTools.getInstance().resetParameters();
					break;
				case 7:
					obtainLicenses(panelGeneralizeFace);
					FaceTools.getInstance().resetParameters();
					break;
				case 8:
					obtainLicenses(panelcaptureIcaoCompliantImage);
					FaceTools.getInstance().resetParameters();
					break;
				default:
					throw new IndexOutOfBoundsException("unreachable");
				}
			} catch (Exception e) {
				showError(null, e);
			}
		}
	}

	public void obtainLicenses(BasePanel panel) {
		try {
			if (!panel.isObtained()) {
				boolean status = FaceTools.getInstance().obtainLicenses(panel.getRequiredLicenses());
				FaceTools.getInstance().obtainLicenses(panel.getOptionalLicenses());
				panel.getLicensing().setRequiredComponents(panel.getRequiredLicenses());
				panel.getLicensing().setOptionalComponents(panel.getOptionalLicenses());
				panel.updateLicensing(status);
			}
		} catch (Exception e) {
			showError(null, e);
		}
	}

}
