package com.neurotec.samples.abis.subject.faces;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Font;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.util.EnumSet;
import java.util.HashSet;
import java.util.Set;

import javax.swing.BoxLayout;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingUtilities;
import javax.swing.border.EmptyBorder;

import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NICAOWarning;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.util.NCollectionChangedAction;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

public class IcaoWarningsPanel extends JPanel {

	private static final long serialVersionUID = 1L;

	private static final Color COLOR_GOOD = new Color(0xFF008000);
	private static final Color COLOR_BAD = new Color(0xFFFF0000);
	private static final Color COLOR_IDETERMINATE = new Color(0xFFFFA500);

	private final NCollectionChangeListener objectsCollectionChanged = new NCollectionChangeListener() {
		@Override
		public void collectionChanged(NCollectionChangeEvent event) {
			if (event.getAction() == NCollectionChangedAction.ADD) {
				if (event.getSource().equals(face.getObjects())) {
					if (attributes != null) {
						attributes.removePropertyChangeListener(attributesPropertyChange);
					}
					attributes = (NLAttributes) event.getNewItems().get(0);
					attributes.addPropertyChangeListener(attributesPropertyChange);
				}
			} else if ((event.getAction() == NCollectionChangedAction.REMOVE) || (event.getAction() == NCollectionChangedAction.RESET)) {
				if (event.getSource().equals(face.getObjects())) {
					if (attributes != null) {
						attributes.removePropertyChangeListener(attributesPropertyChange);
					}
				}
			}
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					warningsChanged();
				}
			});
		}
	};

	private final PropertyChangeListener attributesPropertyChange = new PropertyChangeListener() {
		@Override
		public void propertyChange(PropertyChangeEvent evt) {
			SwingUtilities.invokeLater(new Runnable() {

				@Override
				public void run() {
					warningsChanged();
				}
			});
		}
	};

	private NFace face;
	private NLAttributes attributes;

	private Set<JLabel> labels;

	private JLabel lblBackgroundUniformity;
	private JLabel lblBlink;
	private JLabel lblDarkGlasses;
	private JLabel lblExpression;
	private JLabel lblFaceDetected;
	private JLabel lblGrayscaleDensity;
	private JLabel lblMouthOpen;
	private JLabel lblPitch;
	private JLabel lblRoll;
	private JLabel lblSaturation;
	private JLabel lblSharpness;
	private JLabel lblTooClose;
	private JLabel lblTooEast;
	private JLabel lblTooFar;
	private JLabel lblTooNorth;
	private JLabel lblTooSouth;
	private JLabel lblTooWest;
	private JLabel lblYaw;
	private JPanel panelWarnings;
	private JPanel panelIcao;

	public IcaoWarningsPanel() {
		super();
		initGui();
	}

	private void initGui() {
		panelIcao = new JPanel();
		panelWarnings = new JPanel();
		lblFaceDetected = new JLabel();
		lblExpression = new JLabel();
		lblDarkGlasses = new JLabel();
		lblBlink = new JLabel();
		lblMouthOpen = new JLabel();
		lblRoll = new JLabel();
		lblYaw = new JLabel();
		lblPitch = new JLabel();
		lblTooClose = new JLabel();
		lblTooFar = new JLabel();
		lblTooNorth = new JLabel();
		lblTooSouth = new JLabel();
		lblTooWest = new JLabel();
		lblTooEast = new JLabel();
		lblSharpness = new JLabel();
		lblGrayscaleDensity = new JLabel();
		lblSaturation = new JLabel();
		lblBackgroundUniformity = new JLabel();

		setLayout(new BorderLayout());

		panelIcao.setLayout(new BoxLayout(panelIcao, BoxLayout.Y_AXIS));
		panelIcao.setPreferredSize(new Dimension(190, 290));
		panelWarnings.setLayout(new BoxLayout(panelWarnings, BoxLayout.Y_AXIS));
		panelWarnings.setBorder(new EmptyBorder(0, 20, 0, 10));

		JLabel lblIcaoStatus = new JLabel("ICAO Status:");
		lblIcaoStatus.setFont(new Font("Tahoma", Font.PLAIN, 20));
		lblIcaoStatus.setBorder(new EmptyBorder(0, 10, 10, 0));

		lblFaceDetected.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblFaceDetected.setText("Face detected");
		panelWarnings.add(lblFaceDetected);

		lblExpression.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblExpression.setText("Expression");
		panelWarnings.add(lblExpression);

		lblDarkGlasses.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblDarkGlasses.setText("Dark Glasses");
		panelWarnings.add(lblDarkGlasses);

		lblBlink.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblBlink.setText("Blink");
		panelWarnings.add(lblBlink);

		lblMouthOpen.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblMouthOpen.setText("Mouth Open");
		panelWarnings.add(lblMouthOpen);

		lblRoll.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblRoll.setText("Roll");
		panelWarnings.add(lblRoll);

		lblYaw.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblYaw.setText("Yaw");
		panelWarnings.add(lblYaw);

		lblPitch.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblPitch.setText("Pitch");
		panelWarnings.add(lblPitch);

		lblTooClose.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblTooClose.setText("Too Close");
		panelWarnings.add(lblTooClose);

		lblTooFar.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblTooFar.setText("Too Far");
		panelWarnings.add(lblTooFar);

		lblTooNorth.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblTooNorth.setText("Too North");
		panelWarnings.add(lblTooNorth);

		lblTooSouth.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblTooSouth.setText("Too South");
		panelWarnings.add(lblTooSouth);

		lblTooWest.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblTooWest.setText("Too West");
		panelWarnings.add(lblTooWest);

		lblTooEast.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblTooEast.setText("Too East");
		panelWarnings.add(lblTooEast);

		lblSharpness.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblSharpness.setText("Sharpness");
		panelWarnings.add(lblSharpness);

		lblGrayscaleDensity.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblGrayscaleDensity.setText("Grayscale Density");
		panelWarnings.add(lblGrayscaleDensity);

		lblSaturation.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblSaturation.setText("Saturation");
		panelWarnings.add(lblSaturation);

		lblBackgroundUniformity.setFont(new Font("Tahoma", 1, 11)); // NOI18N
		lblBackgroundUniformity.setText("Background Uniformity");
		panelWarnings.add(lblBackgroundUniformity);

		panelIcao.add(lblIcaoStatus);
		panelIcao.add(panelWarnings);
		add(panelIcao, BorderLayout.WEST);

		labels = new HashSet<JLabel>();
		labels.add(lblFaceDetected);
		labels.add(lblExpression);
		labels.add(lblDarkGlasses);
		labels.add(lblBlink);
		labels.add(lblMouthOpen);
		labels.add(lblRoll);
		labels.add(lblYaw);
		labels.add(lblPitch);
		labels.add(lblTooClose);
		labels.add(lblTooFar);
		labels.add(lblTooNorth);
		labels.add(lblTooSouth);
		labels.add(lblTooWest);
		labels.add(lblTooEast);
		labels.add(lblSharpness);
		labels.add(lblGrayscaleDensity);
		labels.add(lblSaturation);
		labels.add(lblBackgroundUniformity);
	}

	private void subscribeToFaceEvents() {
		if (face != null) {
			face.getObjects().addCollectionChangeListener(objectsCollectionChanged);
			if (face.getObjects().isEmpty()) {
				attributes = null;
			} else {
				attributes = face.getObjects().get(0);
				attributes.addPropertyChangeListener(attributesPropertyChange);
			}
		}
	}

	private void unsubscribeFromFaceEvents() {
		if (face != null) {
			face.getObjects().removeCollectionChangeListener(objectsCollectionChanged);
		}
		if (attributes != null) {
			attributes.removePropertyChangeListener(attributesPropertyChange);
		}
	}

	private Color getConfidenceColor(EnumSet<NICAOWarning> set, NICAOWarning warning, int confidence) {
		if (set.contains(warning)) {
			if (confidence <= 100) {
				return COLOR_BAD;
			} else {
				return COLOR_IDETERMINATE;
			}
		} else {
			return COLOR_GOOD;
		}
	}

	private Color getColor(EnumSet<NICAOWarning> set, NICAOWarning... warnings) {
		for (NICAOWarning w : warnings) {
			if (set.contains(w)) {
				return COLOR_BAD;
			}
		}
		return COLOR_GOOD;
	}

	private String getConfidenceString(String name, int value) {
		return String.format("%s: %s", name, (value <= 100) ? value : "N/A");
	}

	private void warningsChanged() {
		if (attributes == null) {
			updateAllLabels(COLOR_IDETERMINATE);
		} else {
			EnumSet<NICAOWarning> warnings = attributes.getIcaoWarnings();
			if (warnings.contains(NICAOWarning.FACE_NOT_DETECTED)) {
				updateAllLabels(COLOR_IDETERMINATE);
				updateLabel(lblFaceDetected, COLOR_BAD);
			} else {
				updateLabel(lblFaceDetected, COLOR_GOOD);
				updateLabel(lblExpression, getConfidenceColor(warnings, NICAOWarning.EXPRESSION, attributes.getExpressionConfidence() & 0xFF));
				updateLabel(lblDarkGlasses, getConfidenceColor(warnings, NICAOWarning.DARK_GLASSES, attributes.getDarkGlassesConfidence() & 0xFF));
				updateLabel(lblBlink, getConfidenceColor(warnings, NICAOWarning.BLINK, attributes.getBlinkConfidence() & 0xFF));
				updateLabel(lblMouthOpen, getConfidenceColor(warnings, NICAOWarning.MOUTH_OPEN, attributes.getMouthOpenConfidence() & 0xFF));
				updateLabel(lblRoll, getColor(warnings, NICAOWarning.ROLL_LEFT, NICAOWarning.ROLL_RIGHT));
				updateLabel(lblYaw, getColor(warnings, NICAOWarning.YAW_LEFT, NICAOWarning.YAW_RIGHT));
				updateLabel(lblPitch, getColor(warnings, NICAOWarning.PITCH_UP, NICAOWarning.PITCH_DOWN));
				updateLabel(lblTooClose, getColor(warnings, NICAOWarning.TOO_NEAR));
				updateLabel(lblTooFar, getColor(warnings, NICAOWarning.TOO_FAR));
				updateLabel(lblTooNorth, getColor(warnings, NICAOWarning.TOO_NORTH));
				updateLabel(lblTooSouth, getColor(warnings, NICAOWarning.TOO_SOUTH));
				updateLabel(lblTooWest, getColor(warnings, NICAOWarning.TOO_WEST));
				updateLabel(lblTooEast, getColor(warnings, NICAOWarning.TOO_EAST));
				updateLabel(lblSharpness, getColor(warnings, NICAOWarning.SHARPNESS), getConfidenceString("Sharpness", attributes.getSharpness() & 0xFF));
				updateLabel(lblSaturation, getColor(warnings, NICAOWarning.SATURATION), getConfidenceString("Saturation", attributes.getSaturation() & 0xFF));
				updateLabel(lblGrayscaleDensity, getColor(warnings, NICAOWarning.GRAYSCALE_DENSITY), getConfidenceString("Grayscale Density", attributes.getGrayscaleDensity() & 0xFF));
				updateLabel(lblBackgroundUniformity, getColor(warnings, NICAOWarning.BACKGROUND_UNIFORMITY), getConfidenceString("Background Uniformity", attributes.getBackgroundUniformity() & 0xFF));
			}
		}
		repaint();
	}

	private void updateAllLabels(Color color) {
		for (JLabel lbl : labels) {
			lbl.setForeground(color);
		}
	}

	private void updateLabel(JLabel label, Color color, String text) {
		label.setForeground(color);
		label.setText(text);
	}

	private void updateLabel(JLabel label, Color color) {
		label.setForeground(color);
	}

	public NFace getFace() {
		return face;
	}

	public void setFace(NFace face) {
		if ((face == null) || !face.equals(this.face)) {
			unsubscribeFromFaceEvents();
			this.face = face;
			subscribeToFaceEvents();
			warningsChanged();
		}
	}

}
