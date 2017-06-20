package com.neurotec.samples.abis.swing;

import com.neurotec.biometrics.NBiometricTypes;
import com.neurotec.biometrics.NSAttributes;
import com.neurotec.biometrics.NVoice;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.voices.Phrase;

import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.util.concurrent.TimeUnit;

import javax.swing.BorderFactory;
import javax.swing.JLabel;
import javax.swing.JPanel;

public class VoiceView extends JPanel {

	private static final long serialVersionUID = 1L;

	private NVoice voice;

	private JLabel jLabel1;
	private JLabel jLabel2;
	private JLabel jLabel3;
	private JLabel jLabel4;
	private JLabel jLabel9;
	private JLabel lblPhrase;
	private JLabel lblPhraseID;
	private JLabel lblQuality;
	private JLabel lblVoiceDuration;
	private JLabel lblVoiceStart;
	private JPanel mainPanel;

	public VoiceView() {
		super();
		initGUI();
	}

	private void initGUI() {
		GridBagConstraints gridBagConstraints;

		mainPanel = new JPanel();
		jLabel1 = new JLabel();
		jLabel2 = new JLabel();
		jLabel3 = new JLabel();
		jLabel4 = new JLabel();
		lblPhrase = new JLabel();
		lblQuality = new JLabel();
		lblVoiceStart = new JLabel();
		lblVoiceDuration = new JLabel();
		jLabel9 = new JLabel();
		lblPhraseID = new JLabel();

		setLayout(new FlowLayout(FlowLayout.LEFT));

		mainPanel.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		GridBagLayout mainPanelLayout = new GridBagLayout();
		mainPanelLayout.columnWidths = new int[] {0, 10, 0};
		mainPanelLayout.rowHeights = new int[] {0, 5, 0, 5, 0, 5, 0, 5, 0};
		mainPanel.setLayout(mainPanelLayout);

		jLabel1.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		jLabel1.setText("Phrase:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		mainPanel.add(jLabel1, gridBagConstraints);

		jLabel2.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		jLabel2.setText("Quality:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 4;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		mainPanel.add(jLabel2, gridBagConstraints);

		jLabel3.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		jLabel3.setText("Voice start:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 6;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		mainPanel.add(jLabel3, gridBagConstraints);

		jLabel4.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		jLabel4.setText("Voice duration:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 8;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		mainPanel.add(jLabel4, gridBagConstraints);

		lblPhrase.setFont(new Font("Tahoma", 1, 14)); // NOI18N
		lblPhrase.setText("jLabel5");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 2;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		mainPanel.add(lblPhrase, gridBagConstraints);

		lblQuality.setFont(new Font("Tahoma", 1, 14)); // NOI18N
		lblQuality.setText("jLabel6");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 4;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		mainPanel.add(lblQuality, gridBagConstraints);

		lblVoiceStart.setFont(new Font("Tahoma", 1, 14)); // NOI18N
		lblVoiceStart.setText("jLabel7");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 6;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		mainPanel.add(lblVoiceStart, gridBagConstraints);

		lblVoiceDuration.setFont(new Font("Tahoma", 1, 14)); // NOI18N
		lblVoiceDuration.setText("jLabel8");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 8;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		mainPanel.add(lblVoiceDuration, gridBagConstraints);

		jLabel9.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		jLabel9.setText("Phrase ID:");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 0;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_END;
		mainPanel.add(jLabel9, gridBagConstraints);

		lblPhraseID.setFont(new Font("Tahoma", 1, 14)); // NOI18N
		lblPhraseID.setText("jLabel10");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.gridx = 2;
		gridBagConstraints.gridy = 0;
		gridBagConstraints.anchor = GridBagConstraints.LINE_START;
		mainPanel.add(lblPhraseID, gridBagConstraints);

		add(mainPanel);
	}

	private String formatTime(long millis) {
		return String.format("%02d:%02d:%02d:%03d",
							TimeUnit.MILLISECONDS.toHours(millis),
							TimeUnit.MILLISECONDS.toMinutes(millis) - TimeUnit.HOURS.toMinutes(TimeUnit.MILLISECONDS.toHours(millis)),
							TimeUnit.MILLISECONDS.toSeconds(millis) - TimeUnit.MINUTES.toSeconds(TimeUnit.MILLISECONDS.toMinutes(millis)),
							millis - TimeUnit.SECONDS.toMillis(TimeUnit.MILLISECONDS.toSeconds(millis)));
	}

	public NVoice getVoice() {
		return voice;
	}

	public void setVoice(NVoice voice) {
		this.voice = voice;

		if (voice == null) {
			lblPhrase.setText("N/A");
			lblQuality.setText("N/A");
			lblVoiceStart.setText("N/A");
			lblVoiceDuration.setText("N/A");
			lblPhraseID.setText("-1");
		} else {
			int phraseId = voice.getPhraseID();
			lblPhraseID.setText(String.valueOf(phraseId));
			String sPhrase = null;
			for (Phrase phrase : SettingsManager.getPhrases()) {
				if (phraseId == phrase.getID()) {
					sPhrase = phrase.getPhrase();
				}
			}
			if (sPhrase == null) {
				lblPhrase.setText("N/A");
			} else {
				lblPhrase.setText(sPhrase);
			}

			if (voice.getObjects().isEmpty()) {
				lblQuality.setText("N/A");
				lblVoiceStart.setText("N/A");
				lblVoiceDuration.setText("N/A");
			} else {
				NSAttributes attributes = voice.getObjects().get(0);
				byte quality = attributes.getQuality();
				if (quality == NBiometricTypes.QUALITY_UNKNOWN) {
					lblQuality.setText("N/A");
				} else if (quality == NBiometricTypes.QUALITY_FAILED) {
					lblQuality.setText("Failed to determine quality");
				} else {
					lblQuality.setText(String.valueOf(quality));
				}
				if ((attributes.getVoiceStart() == 0) && (attributes.getVoiceDuration() == 0)) { // Doesn't have timespan.
					lblVoiceStart.setText("N/A");
					lblVoiceDuration.setText("N/A");
				} else {
					lblVoiceStart.setText(formatTime(attributes.getVoiceStart()));
					lblVoiceDuration.setText(formatTime(attributes.getVoiceDuration()));
				}
			}
		}
	}

}
