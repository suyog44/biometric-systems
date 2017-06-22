package com.neurotec.samples.abis.subject.voices;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.SystemColor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.File;
import java.util.EnumSet;

import javax.swing.BorderFactory;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.ButtonGroup;
import javax.swing.DefaultComboBoxModel;
import javax.swing.GroupLayout;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JRadioButton;
import javax.swing.JTextPane;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

import com.neurotec.biometrics.NBiometricCaptureOption;
import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NSubject.VoiceCollection;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.swing.NVoiceView;
import com.neurotec.devices.NMicrophone;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.settings.Settings;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.CaptureBiometricController;
import com.neurotec.samples.abis.subject.Source;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.LockingTask;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.util.event.NCollectionChangeEvent;
import com.neurotec.util.event.NCollectionChangeListener;

public final class CaptureVoicePage extends Page implements CaptureVoiceView, PageNavigationListener, NCollectionChangeListener, PropertyChangeListener, ActionListener {

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	private static final Color COLOR_OK = new Color(0, 128, 0);
	private static final Color COLOR_ORANGE = new Color(255, 153, 0);
	private static final Color COLOR_ERROR = new Color(255, 0, 0);

	// ===========================================================
	// Private fields
	// ===========================================================

	private CaptureVoiceModel biometricModel;
	private CaptureBiometricController biometricController;
	private NVoice currentBiometric;

	private final LockingTask cancelTask = new LockingTask() {

		@Override
		public void performTask() {
			biometricController.cancel();
		}
	};

	private ButtonGroup bgSources;
	private JButton btnEditPhrase;
	private JButton btnStop;
	private JButton btnStart;
	private JButton btnOpenFile;
	private JButton btnFinish;
	private JComboBox phrasesComboBox;
	private JFileChooser fcOpenFile;
	private JLabel lblFileName;
	private JLabel lblPhraseId;
	private JPanel capturePanel;
	private JPanel phrasePanel;
	private JPanel sourcePanel;
	private JPanel panelBottom;
	private JPanel panelStatus;
	private JRadioButton rbFile;
	private JRadioButton rbDevices;
	private JTextPane tpStatus;
	private File lastFile;
	private NVoiceView voiceView;
	private JPanel panelSouth;
	private JPanel panelFinish;
	private JPanel panelControls;
	private JPanel panelAutomatic;
	private JPanel panelControlButtons;
	private JCheckBox cbAutomatic;
	private JButton btnForce;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public CaptureVoicePage(PageNavigationController pageController) {
		super("New...", pageController);
		initGUI();
		lastFile = new File(Settings.getInstance().getLastVoiceFilePath());
		updatePhrasesList();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		setLayout(new BorderLayout());
		JPanel center = new JPanel();
		GroupLayout centerLayout = new GroupLayout(center);
		center.setLayout(centerLayout);
		centerLayout.setAutoCreateGaps(true);
		centerLayout.setAutoCreateContainerGaps(true);
		add(center);
		{
			phrasePanel = new JPanel();
			phrasePanel.setBorder(BorderFactory.createTitledBorder("Secret phrase (Please answer the question)"));
			GroupLayout phraseLayout = new GroupLayout(phrasePanel);
			phrasePanel.setLayout(phraseLayout);
			phraseLayout.setAutoCreateGaps(true);
			phraseLayout.setAutoCreateContainerGaps(true);
			{
				phrasesComboBox = new JComboBox(new DefaultComboBoxModel());
				phrasesComboBox.addItemListener(new ItemListener() {
					@Override
					public void itemStateChanged(ItemEvent e) {
						lblPhraseId.setText(phrasesComboBox.getSelectedIndex() != -1 ? String.valueOf(((Phrase) phrasesComboBox.getSelectedItem()).getID()) : "");
					}
				});
			}
			{
				btnEditPhrase = new JButton("Edit");
				btnEditPhrase.addActionListener(this);
			}
			{
				lblPhraseId = new JLabel();
				lblPhraseId.setText("");
			}
			JLabel lblSelectedPhrase = new JLabel("Selected phrase:");
			JLabel lblPhraseIdLabel = new JLabel("Phrase Id:");
			JLabel lblPhraseDescription = new JLabel("<html>Phrase should be secret answer to the selected question.<br>Phrase duration should be at least about 6 seconds or 4 words.</html>");
			lblPhraseDescription.setFont(new Font("Tahoma", Font.BOLD, 12));
			phraseLayout.setHorizontalGroup(
				phraseLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
					.addGroup(phraseLayout.createSequentialGroup()
						.addGroup(phraseLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
							.addComponent(lblSelectedPhrase)
							.addComponent(lblPhraseIdLabel))
						.addGroup(phraseLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
							.addComponent(phrasesComboBox)
							.addComponent(lblPhraseId))
						.addComponent(btnEditPhrase))
					.addComponent(lblPhraseDescription)
			);
			phraseLayout.setVerticalGroup(
				phraseLayout.createSequentialGroup()
					.addGroup(phraseLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
						.addComponent(lblSelectedPhrase)
						.addComponent(phrasesComboBox)
						.addComponent(btnEditPhrase))
					.addGroup(phraseLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
						.addComponent(lblPhraseIdLabel)
						.addComponent(lblPhraseId))
					.addComponent(lblPhraseDescription)
			);
		}
		{
			sourcePanel = new JPanel();
			sourcePanel.setBorder(BorderFactory.createTitledBorder("Source"));
			GroupLayout sourceLayout = new GroupLayout(sourcePanel);
			sourcePanel.setLayout(sourceLayout);
			sourceLayout.setAutoCreateGaps(true);
			sourceLayout.setAutoCreateContainerGaps(true);
			bgSources = new ButtonGroup();
			{
				rbDevices = new JRadioButton("Microphone:", true);
				bgSources.add(rbDevices);
				rbDevices.addActionListener(this);
			}
			{
				rbFile = new JRadioButton("Sound file:");
				bgSources.add(rbFile);
				rbFile.addActionListener(this);
			}
			{
				fcOpenFile = new JFileChooser();
				fcOpenFile.setMultiSelectionEnabled(false);
				btnOpenFile = new JButton("Open");
				btnOpenFile.addActionListener(this);
			}
			{
				lblFileName = new JLabel();
			}
			Component glue = Box.createHorizontalGlue();
			sourceLayout.setHorizontalGroup(
				sourceLayout.createParallelGroup(GroupLayout.Alignment.LEADING)
					.addComponent(rbDevices)
					.addGroup(sourceLayout.createSequentialGroup()
						.addComponent(rbFile)
						.addComponent(lblFileName)
						.addComponent(glue)
						.addComponent(btnOpenFile))
			);
			sourceLayout.setVerticalGroup(
				sourceLayout.createSequentialGroup()
					.addComponent(rbDevices)
					.addGroup(sourceLayout.createParallelGroup(GroupLayout.Alignment.BASELINE)
						.addComponent(rbFile)
						.addComponent(lblFileName)
						.addComponent(glue)
						.addComponent(btnOpenFile))
			);
		}
		{
			capturePanel = new JPanel(new FlowLayout(FlowLayout.LEADING));
			capturePanel.setBorder(BorderFactory.createTitledBorder("Capture"));

			panelControls = new JPanel();
			panelControls.setLayout(new BoxLayout(panelControls, BoxLayout.Y_AXIS));

			panelAutomatic = new JPanel(new FlowLayout(FlowLayout.LEADING));
			cbAutomatic = new JCheckBox("Capture automatically");
			cbAutomatic.setHorizontalAlignment(0);
			cbAutomatic.setSelected(true);
			panelAutomatic.add(cbAutomatic);
			panelControls.add(panelAutomatic);

			panelControlButtons = new JPanel(new FlowLayout(FlowLayout.LEADING));

			btnStart = new JButton("Start");
			btnStart.setPreferredSize(new Dimension(70, 40));
			panelControlButtons.add(btnStart);
			btnStart.addActionListener(this);

			btnStop = new JButton("Stop");
			btnStop.setPreferredSize(new Dimension(70, 40));
			panelControlButtons.add(btnStop);
			btnStop.setEnabled(false);
			btnStop.addActionListener(this);

			btnForce = new JButton("Force");
			btnForce.setPreferredSize(new Dimension(70, 40));
			panelControlButtons.add(btnForce);
			btnForce.setEnabled(false);
			btnForce.addActionListener(this);

			panelControls.add(panelControlButtons);

			capturePanel.add(panelControls, BorderLayout.WEST);

			voiceView = new NVoiceView();
			capturePanel.add(voiceView, BorderLayout.CENTER);
		}
		{
			Dimension min = new Dimension(0, 1);
			Dimension max = new Dimension(0, Short.MAX_VALUE);
			Component glue = new Box.Filler(min, max, max);
			JLabel lblPhraseSelect = new JLabel("1. Please select secret phrase ID from the list:");
			JLabel lblSoundSourceSelect = new JLabel("2. Please select sound source:");
			centerLayout.setHorizontalGroup(
				centerLayout.createParallelGroup()
					.addComponent(lblPhraseSelect)
					.addComponent(phrasePanel)
					.addComponent(lblSoundSourceSelect)
					.addComponent(sourcePanel)
					.addComponent(capturePanel)
					.addComponent(glue)
			);
			centerLayout.setVerticalGroup(
				centerLayout.createSequentialGroup()
					.addComponent(lblPhraseSelect)
					.addComponent(phrasePanel)
					.addComponent(lblSoundSourceSelect)
					.addComponent(sourcePanel)
					.addComponent(capturePanel)
					.addComponent(glue)
			);
		}
		{
			panelBottom = new JPanel();
			add(panelBottom, BorderLayout.SOUTH);
			panelBottom.setLayout(new BoxLayout(panelBottom, BoxLayout.Y_AXIS));
			{
				panelStatus = new JPanel();
				panelBottom.add(panelStatus);
				panelStatus.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
				panelStatus.setLayout(new GridLayout(1, 0));
			}

			{
				tpStatus = new JTextPane();
				StyledDocument doc = tpStatus.getStyledDocument();
				SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
				StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
				StyleConstants.setLeftIndent(styleAttributes, 3);
				StyleConstants.setRightIndent(styleAttributes, 3);
				StyleConstants.setFontSize(styleAttributes, 16);
				StyleConstants.setForeground(styleAttributes, SystemColor.menu);
				doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);
			}
			tpStatus.setEditable(false);
			panelStatus.add(tpStatus);
			{
				panelSouth = new JPanel();
				panelBottom.add(panelSouth);
				panelSouth.setLayout(new BorderLayout(0, 0));
				{
					panelFinish = new JPanel();
					panelSouth.add(panelFinish, BorderLayout.EAST);
					{
						btnFinish = new JButton("Finish");
						panelFinish.add(btnFinish);
						btnFinish.addActionListener(this);
					}
				}
			}
		}
	}

	private void updateControls() {
		boolean idle = !cancelTask.isBusy();
		phrasesComboBox.setEnabled(idle && phrasesComboBox.getItemCount() > 0);
		btnEditPhrase.setEnabled(idle);
		rbDevices.setEnabled(idle
							 && (biometricModel.getClient().getVoiceCaptureDevice() != null)
							 && biometricModel.getClient().getVoiceCaptureDevice().isAvailable()
							 && (biometricModel.getClient().getLocalOperations().contains(NBiometricOperation.CREATE_TEMPLATE) || biometricModel.getClient().getLocalOperations().contains(NBiometricOperation.SEGMENT)));
		rbFile.setEnabled(idle);
		btnOpenFile.setEnabled(idle);
		btnStart.setEnabled(rbDevices.isSelected() && idle);
		btnStop.setEnabled(!idle);
		btnFinish.setEnabled(idle);
		btnForce.setEnabled(!idle && !cbAutomatic.isSelected());
		cbAutomatic.setEnabled(idle);

		if (rbFile.isSelected()) {
			btnOpenFile.setVisible(true);
			cbAutomatic.setVisible(false);
			btnForce.setVisible(false);
		} else if (rbDevices.isSelected()) {
			btnOpenFile.setVisible(false);
			cbAutomatic.setVisible(true);
			btnForce.setVisible(true);
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
	}

	private void editPhrase() {
		EditPhraseDialog dialog = new EditPhraseDialog(null);
		dialog.setPhrases(SettingsManager.getPhrases());
		dialog.setModal(true);
		dialog.setVisible(true);
		updatePhrasesList();
	}

	private void updatePhrasesList() {
		DefaultComboBoxModel model = (DefaultComboBoxModel) phrasesComboBox.getModel();
		model.removeAllElements();
		for (Phrase phrase : SettingsManager.getPhrases()) {
			model.addElement(phrase);
		}
	}

	private void setStatus(String msg, Color color) {
		tpStatus.setText(msg);
		tpStatus.setBackground(color);
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setBiometricModel(CaptureVoiceModel biometricModel) {
		if (biometricModel == null) {
			throw new NullPointerException("biometricModel");
		}
		this.biometricModel = biometricModel;
	}

	public void setBiometricController(CaptureBiometricController biometricController) {
		if (biometricController == null) {
			throw new NullPointerException("biometricController");
		}
		this.biometricController = biometricController;
	}

	@Override
	public Source getSource() {
		if (rbFile.isSelected()) {
			return Source.FILE;
		} else if (rbDevices.isSelected()) {
			return Source.DEVICE;
		} else {
			throw new AssertionError("A source radiobutton must be selected.");
		}
	}

	@Override
	public EnumSet<NBiometricCaptureOption> getCaptureOptions() {
		if (cbAutomatic.isSelected()) {
			return EnumSet.noneOf(NBiometricCaptureOption.class);
		} else {
			return EnumSet.of(NBiometricCaptureOption.MANUAL);
		}
	}

	@Override
	public File getFile() {
		fcOpenFile.setCurrentDirectory(lastFile);
		if (fcOpenFile.showOpenDialog(this) == JFileChooser.APPROVE_OPTION) {
			lastFile = fcOpenFile.getSelectedFile();
			if (!Settings.getInstance().getLastVoiceFilePath().equals(lastFile.getAbsolutePath())) {
				Settings.getInstance().setLastVoiceFilePath(lastFile.getAbsolutePath());
				Settings.getInstance().save();
			}
			lblFileName.setText(lastFile.getName());
			return lastFile;
		} else {
			return null;
		}
	}

	@Override
	public Phrase getCurrentPhrase() {
		return (Phrase) phrasesComboBox.getSelectedItem();
	}

	@Override
	public void captureCompleted(final NBiometricStatus status) {
		cancelTask.setBusy(false);
		SwingUtils.runOnEDT(new Runnable() {

			@Override
			public void run() {
				tpStatus.setVisible(true);
				if (status == NBiometricStatus.OK) {
					setStatus("Extraction completed successfully", COLOR_OK);
				} else {
					setStatus("Extraction failed: " + status, COLOR_ERROR);
				}
				updateControls();
			}
		});
	}

	@Override
	public void captureFailed(Throwable th, final String msg) {
		cancelTask.setBusy(false);
		if (th != null) {
			if ((th.getMessage() != null) && th.getMessage().contains("OutOfMemoryError")) {
				MessageUtils.showError(this, "Not enough memory", "Not enough memory to load this image. Please select a smaller one.");
			} else {
				MessageUtils.showError(this, th);
			}
		}
		SwingUtils.runOnEDT(new Runnable() {
			@Override
			public void run() {
				panelStatus.setVisible(true);
				updateControls();
				setStatus("Extraction failed: " + msg, COLOR_ERROR);
			}
		});
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			biometricModel.getClient().addPropertyChangeListener(this);
			biometricModel.getLocalSubject().getVoices().addCollectionChangeListener(this);
			rbDevices.setSelected(true);
			voiceCaptureDeviceChanged();
			tpStatus.setVisible(false);
			updateControls();
		}
	}

	@Override
	public void navigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			try {
				cancelTask.startAndWait();
			} catch (InterruptedException e) {
				Thread.currentThread().interrupt();
				e.printStackTrace();
				return;
			}
			voiceView.setVoice(null);
			biometricModel.getClient().removePropertyChangeListener(this);
			biometricModel.getLocalSubject().getVoices().removeCollectionChangeListener(this);
			biometricController.finish();
		}
	}

	public void voiceCaptureDeviceChanged() {
		NMicrophone device = biometricModel.getClient().getVoiceCaptureDevice();
		if ((device == null) || !device.isAvailable()) {
			if (rbDevices.isSelected()) {
				rbFile.setSelected(true);
			}
			rbDevices.setText("Microphone (Not connected)");
		} else {
			rbDevices.setText(String.format("Microphone (%s)", device.getDisplayName()));
		}
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == rbDevices) {
				updateControls();
			} else if (ev.getSource() == rbFile) {
				updateControls();
			} else if (ev.getSource() == btnEditPhrase) {
				editPhrase();
				updateControls();
			} else if (ev.getSource() == btnOpenFile) {
				panelStatus.setVisible(true);
				setStatus("Extracting template...", COLOR_ORANGE);
				cancelTask.setBusy(true);
				updateControls();
				biometricController.capture();
			} else if (ev.getSource() == btnStart) {
				panelStatus.setVisible(true);
				setStatus("Extracting record. Please say phrase ...", COLOR_ORANGE);
				cancelTask.setBusy(true);
				updateControls();
				biometricController.capture();
			} else if (ev.getSource() == btnStop) {
				biometricController.force();
			} else if (btnForce.equals(ev.getSource())) {
				biometricController.forceStart();
				btnForce.setEnabled(false);
			} else if (ev.getSource().equals(btnFinish)) {
				getPageController().navigateToStartPage();
			}

		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void collectionChanged(final NCollectionChangeEvent ev) {
		final VoiceCollection voices = biometricModel.getLocalSubject().getVoices();
		if (ev.getSource().equals(voices)) {
			if (currentBiometric != null) {
				currentBiometric.removePropertyChangeListener(this);
			}
			SwingUtils.runOnEDT(new Runnable() {

				@Override
				public void run() {
					if (voices.isEmpty()) {
						voiceView.setVoice(null);
						currentBiometric = null;
					} else {
						currentBiometric = voices.get(voices.size() - 1);
						voiceView.setVoice(currentBiometric);
						currentBiometric.addPropertyChangeListener(CaptureVoicePage.this);
					}
				}
			});
		}
	}

	@Override
	public void propertyChange(PropertyChangeEvent ev) {
		if (ev.getSource().equals(biometricModel.getClient()) && "VoiceCaptureDevice".equals(ev.getPropertyName())) {
			SwingUtils.runOnEDT(new Runnable() {
				@Override
				public void run() {
					voiceCaptureDeviceChanged();
					updateControls();
				}
			});
		}
	}

}
