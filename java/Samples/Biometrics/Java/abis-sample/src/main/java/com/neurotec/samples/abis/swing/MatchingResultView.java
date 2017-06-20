package com.neurotec.samples.abis.swing;

import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NEMatchingDetails;
import com.neurotec.biometrics.NFMatchingDetails;
import com.neurotec.biometrics.NLMatchingDetails;
import com.neurotec.biometrics.NMatchingDetails;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NSMatchingDetails;

import java.awt.Color;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class MatchingResultView extends JPanel implements ActionListener {

	private static final long serialVersionUID = 1L;

	private final NMatchingResult matchingResult;
	private final int matchingThreshold;
	private final List<ActionListener> actionListeners;

	private boolean linkEnabled = true;
	private JLabel lblDetails;
	private LinkButton btnLink;

	public MatchingResultView(NMatchingResult matchingResult, int matchingThreshold) {
		this.matchingResult = matchingResult;
		this.matchingThreshold = matchingThreshold;
		this.actionListeners = new ArrayList<ActionListener>();
		initGUI();
		btnLink.setText(String.format("<html><color=\"black\"><a href=\"\">Matched with %s, score = %d</a></color></html>", this.matchingResult.getId(), this.matchingResult.getScore()));
		lblDetails.setText(matchingResultToString(this.matchingResult));
	}

	private void initGUI() {
		btnLink = new LinkButton();
		lblDetails = new JLabel();

		setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));

		btnLink.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
		add(btnLink);

		lblDetails.setBorder(BorderFactory.createEmptyBorder(3, 10, 3, 3));
		add(lblDetails);

		btnLink.addActionListener(this);
	}

	private String matchingResultToString(NMatchingResult result) {
		NMatchingDetails details = result.getMatchingDetails();
		StringBuilder sb = new StringBuilder(256);
		sb.append("<html>");
		if (details != null) {
			String strBelowThreshold = " (Below matching threshold)";
			if (details.getBiometricType().contains(NBiometricType.FACE)) {
				sb.append(String.format("Face match details: score = %d<br/>", details.getFacesScore()));
				int index = 0;
				for (NLMatchingDetails faceDetails : details.getFaces()) {
					if (faceDetails.getMatchedIndex() == -1) {
						sb.append(String.format("Face index %d: doesn't match<br/>", index++));
					} else {
						sb.append(String.format("Face index %d: matched with index %d, score = %d%s;<br/>", index++, faceDetails.getMatchedIndex(), faceDetails.getScore(), faceDetails.getScore() < matchingThreshold ? strBelowThreshold : ""));
					}
				}
			}
			if (details.getBiometricType().contains(NBiometricType.FINGER)) {
				sb.append(String.format("Finger match details: score = %d<br/>", details.getFingersScore()));
				int index = 0;
				for (NFMatchingDetails fingerDetails : details.getFingers()) {
					if (fingerDetails.getMatchedIndex() == -1) {
						sb.append(String.format("Finger index %d: doesn't match<br/>", index++));
					} else {
						sb.append(String.format("Finger index %d: matched with index %d, score = %d%s;<br/>", index++, fingerDetails.getMatchedIndex(), fingerDetails.getScore(), fingerDetails.getScore() < matchingThreshold ? strBelowThreshold : ""));
					}
				}
			}
			if (details.getBiometricType().contains(NBiometricType.IRIS)) {
				sb.append(String.format("Iris match details: score = %d<br/>", details.getIrisesScore()));
				int index = 0;
				for (NEMatchingDetails irisDetails : details.getIrises()) {
					if (irisDetails.getMatchedIndex() == -1) {
						sb.append(String.format("Iris index %d: doesn't match<br/>", index++));
					} else {
						sb.append(String.format("Iris index %d: matched with index %d, score = %d%s;<br/>", index++, irisDetails.getMatchedIndex(), irisDetails.getScore(), irisDetails.getScore() < matchingThreshold ? strBelowThreshold : ""));
					}
				}
			}
			if (details.getBiometricType().contains(NBiometricType.PALM)) {
				sb.append(String.format("Palm match details: score = %d<br/>", details.getPalmsScore()));
				int index = 0;
				for (NFMatchingDetails palmDetails : details.getPalms()) {
					if (palmDetails.getMatchedIndex() == -1) {
						sb.append(String.format("Palm index %d: doesn't match<br/>", index++));
					} else {
						sb.append(String.format("Palm index %d: matched with index %d, score = %d%s;<br/>", index++, palmDetails.getMatchedIndex(), palmDetails.getScore(), palmDetails.getScore() < matchingThreshold ? strBelowThreshold : ""));
					}
				}
			}
			if (details.getBiometricType().contains(NBiometricType.VOICE)) {
				sb.append(String.format("Voice match details: score = %d<br/>", details.getVoicesScore()));
				int index = 0;
				for (NSMatchingDetails voiceDetails : details.getVoices()) {
					if (voiceDetails.getMatchedIndex() == -1) {
						sb.append(String.format("Voice index %d: doesn't match<br/>", index++));
					} else {
						sb.append(String.format("Voice index %d: matched with index %d, score = %d%s;<br/>", index++, voiceDetails.getMatchedIndex(), voiceDetails.getScore(), voiceDetails.getScore() < matchingThreshold ? strBelowThreshold : ""));
					}
				}
			}
			if (!details.getBiometricType().contains(NBiometricType.FACE)
				&& !details.getBiometricType().contains(NBiometricType.FINGER)
				&& !details.getBiometricType().contains(NBiometricType.IRIS)
				&& !details.getBiometricType().contains(NBiometricType.PALM)
				&& !details.getBiometricType().contains(NBiometricType.VOICE)) {
				sb.append("Score = ").append(details.getScore());
			}
		}
		return sb.append("</html>").toString();
	}

	public void addActionListener(ActionListener l) {
		actionListeners.add(l);
	}

	public void removeActionListener(ActionListener l) {
		actionListeners.remove(l);
	}

	public boolean isLinkEnabled() {
		return linkEnabled;
	}

	public void setLinkEnabled(boolean value) {
		this.linkEnabled = value;
		btnLink.setEnabled(value);
		btnLink.setForeground(value ? Color.GREEN : Color.BLACK);
	}

	public NMatchingResult getMatchingResult() {
		return matchingResult;
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource().equals(btnLink)) {
			for (ActionListener l : actionListeners) {
				l.actionPerformed(new ActionEvent(this, ActionEvent.ACTION_PERFORMED, "LinkClicked"));
			}
		}
	}

}
