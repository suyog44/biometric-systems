package com.neurotec.samples.abis.tabbedview;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.SystemColor;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.util.ArrayList;
import java.util.List;
import java.util.zip.DataFormatException;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.ImageIcon;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricStatus;
import com.neurotec.biometrics.NEMatchingDetails;
import com.neurotec.biometrics.NFMatchingDetails;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NFinger;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NGender;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NLMatchingDetails;
import com.neurotec.biometrics.NMatchingDetails;
import com.neurotec.biometrics.NMatchingResult;
import com.neurotec.biometrics.NPalm;
import com.neurotec.biometrics.NSMatchingDetails;
import com.neurotec.biometrics.NSubject;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.NXMatchingDetails;
import com.neurotec.biometrics.client.NBiometricClient;
import com.neurotec.biometrics.swing.MinutiaSelectionEvent;
import com.neurotec.biometrics.swing.MinutiaSelectionListener;
import com.neurotec.biometrics.swing.NFaceView;
import com.neurotec.biometrics.swing.NFingerView;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.images.NImage;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.TabNavigationListener;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.subject.CaptureBiometricModel;
import com.neurotec.samples.abis.subject.EnrollDataSerializer;
import com.neurotec.samples.abis.swing.SubjectSchemaGrid;
import com.neurotec.samples.abis.swing.VoiceView;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.abis.util.SwingUtils;
import com.neurotec.util.NIndexPair;
import com.neurotec.util.NPropertyBag;
import com.neurotec.util.concurrent.CompletionHandler;

public class MatchingResultTab extends Tab implements CompletionHandler<NBiometricStatus, Object>, TabNavigationListener, ItemListener {

	// ===========================================================
	// Nested classes
	// ===========================================================

	private static class MatchedPair<T extends NBiometric, U extends NXMatchingDetails> {

		private final T first;
		private final T second;
		private final U details;

		MatchedPair(T first, T second, U details) {
			this.first = first;
			this.second = second;
			this.details = details;
		}

		public T getFirst() {
			return first;
		}

		public T getSecond() {
			return second;
		}

		public U getDetails() {
			return details;
		}

	}

	private static final class MatchedFinger extends MatchedPair<NFinger, NFMatchingDetails> {

		MatchedFinger(NFinger first, NFinger second, NFMatchingDetails details) {
			super(first, second, details);
		}

		@Override
		public String toString() {
			return String.format("Probe finger(%s) matched with gallery finger(%s). Score = %d.", getFirst().getPosition(), getSecond().getPosition(), getDetails().getScore());
		}

	}

	private static final class MatchedFace extends MatchedPair<NFace, NLMatchingDetails> {

		MatchedFace(NFace first, NFace second, NLMatchingDetails details) {
			super(first, second, details);
		}

		@Override
		public String toString() {
			return String.format("Faces matched. Score = %d.", getDetails().getScore());
		}

	}

	private static final class MatchedIris extends MatchedPair<NIris, NEMatchingDetails> {

		MatchedIris(NIris first, NIris second, NEMatchingDetails details) {
			super(first, second, details);
		}

		@Override
		public String toString() {
			return String.format("Probe iris(%s) matched with gallery iris(%s). Score = %d.", getFirst().getPosition(), getSecond().getPosition(), getDetails().getScore());
		}

	}

	private static final class MatchedVoice extends MatchedPair<NVoice, NSMatchingDetails> {

		MatchedVoice(NVoice first, NVoice second, NSMatchingDetails details) {
			super(first, second, details);
		}

		@Override
		public String toString() {
			return String.format("Probe voice(PhraseID=%s) matched with gallery voice(PhraseID=%s). Score = %d.", getFirst().getPhraseID(), getSecond().getPhraseID(), getDetails().getScore());
		}

	}

	private static final class MatchedPalm extends MatchedPair<NPalm, NFMatchingDetails> {

		MatchedPalm(NPalm first, NPalm second, NFMatchingDetails details) {
			super(first, second, details);
		}

		@Override
		public String toString() {
			return String.format("Probe palm(%s) matched with gallery palm(%s). Score = %d.", getFirst().getPosition(), getSecond().getPosition(), getDetails().getScore());
		}

	}

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private final NSubject probe;
	private NSubject gallery;
	private final NBiometricClient client;
	private final DatabaseSchema schema;
	private NMatchingResult matchingResult;
	private SubjectSchemaGrid propertyGrid;

	private JComboBox comboBoxMatchedBiometrics;
	private JLabel lblGalleryInfo;
	private JLabel lblGalleryTitle;
	private JLabel lblMatchedBiometrics;
	private JLabel lblProbeInfo;
	private JLabel lblProbeTitle;
	private JLabel lblScore;
	private JLabel lblSubject;
	private JLabel lblThumbnailImage;
	private JPanel panelGallery;
	private JPanel panelGalleryInfo;
	private JPanel panelGalleryTitle;
	private JPanel panelID;
	private JPanel panelMatchedBiometrics;
	private JPanel panelProbe;
	private JPanel panelProbeInfo;
	private JPanel panelProbeTitle;
	private JPanel panelStatus;
	private JPanel panelThumbnailIcon;
	private JPanel panelTop;
	private JPanel panelTopLeft;
	private JPanel panelViews;
	private JScrollPane spGalleryView;
	private JScrollPane spProbeView;
	private JScrollPane spProperties;
	private JScrollPane spThumbnail;
	private JTextPane tpStatus;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public MatchingResultTab(CaptureBiometricModel<?> model) {
		if (model == null) {
			throw new NullPointerException("model");
		}
		probe = model.getLocalSubject();
		gallery = model.getSubject();
		client = model.getClient();
		schema = model.getDatabaseSchema();
		initGUI();
		panelStatus.setVisible(false);

		for (NMatchingResult result : probe.getMatchingResults()) {
			if (result.getId().equals(gallery.getId())) {
				matchingResult = result;
				break;
			}
		}
		setTitle("Matching result: " + gallery.getId());
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		GridBagConstraints gridBagConstraints;

		panelTop = new JPanel();
		panelStatus = new JPanel();
		tpStatus = new JTextPane();
		StyledDocument doc = tpStatus.getStyledDocument();
		SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
		StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
		StyleConstants.setSpaceAbove(styleAttributes, 1);
		StyleConstants.setSpaceBelow(styleAttributes, 1);
		StyleConstants.setLeftIndent(styleAttributes, 3);
		StyleConstants.setRightIndent(styleAttributes, 3);
		StyleConstants.setFontSize(styleAttributes, 13);
		StyleConstants.setBold(styleAttributes, true);
		StyleConstants.setForeground(styleAttributes, SystemColor.menu);
		doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);
		panelTopLeft = new JPanel();
		spThumbnail = new JScrollPane();
		panelThumbnailIcon = new JPanel();
		lblThumbnailImage = new JLabel();
		panelID = new JPanel();
		lblSubject = new JLabel();
		lblScore = new JLabel();
		spProperties = new JScrollPane();
		panelViews = new JPanel();
		panelProbe = new JPanel();
		panelProbeTitle = new JPanel();
		lblProbeTitle = new JLabel();
		spProbeView = new JScrollPane();
		panelProbeInfo = new JPanel();
		lblProbeInfo = new JLabel();
		panelGallery = new JPanel();
		panelGalleryTitle = new JPanel();
		lblGalleryTitle = new JLabel();
		spGalleryView = new JScrollPane();
		panelGalleryInfo = new JPanel();
		lblGalleryInfo = new JLabel();
		panelMatchedBiometrics = new JPanel();
		lblMatchedBiometrics = new JLabel();
		comboBoxMatchedBiometrics = new JComboBox();

		setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		setLayout(new BorderLayout());

		panelTop.setLayout(new BorderLayout());

		panelStatus.setBorder(BorderFactory.createEmptyBorder(5, 5, 5, 5));
		panelStatus.setLayout(new GridLayout(1, 0));

		tpStatus.setEditable(false);
		panelStatus.add(tpStatus);

		panelTop.add(panelStatus, BorderLayout.NORTH);

		panelTopLeft.setLayout(new FlowLayout(FlowLayout.LEADING));

		panelThumbnailIcon.setLayout(new GridBagLayout());
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.fill = GridBagConstraints.BOTH;
		panelThumbnailIcon.add(lblThumbnailImage, gridBagConstraints);

		spThumbnail.setViewportView(panelThumbnailIcon);

		panelTopLeft.add(spThumbnail);

		panelID.setBorder(BorderFactory.createEmptyBorder(5, 15, 5, 5));
		panelID.setLayout(new BoxLayout(panelID, BoxLayout.Y_AXIS));

		lblSubject.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		lblSubject.setText("Subject:");
		panelID.add(lblSubject);

		lblScore.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		lblScore.setText("Score:");
		panelID.add(lblScore);

		panelTopLeft.add(panelID);

		panelTop.add(panelTopLeft, BorderLayout.WEST);

		spProperties.setPreferredSize(new Dimension(2, 200));
		panelTop.add(spProperties, BorderLayout.CENTER);

		add(panelTop, BorderLayout.NORTH);

		panelViews.setLayout(new GridLayout(1, 2));

		panelProbe.setLayout(new BorderLayout());

		lblProbeTitle.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		lblProbeTitle.setText("Probe subject");
		panelProbeTitle.add(lblProbeTitle);

		panelProbe.add(panelProbeTitle, BorderLayout.NORTH);
		panelProbe.add(spProbeView, BorderLayout.CENTER);

		panelProbeInfo.setLayout(new FlowLayout(FlowLayout.LEADING));

		lblProbeInfo.setText("Probe info");
		panelProbeInfo.add(lblProbeInfo);

		panelProbe.add(panelProbeInfo, BorderLayout.SOUTH);

		panelViews.add(panelProbe);

		panelGallery.setLayout(new BorderLayout());

		lblGalleryTitle.setFont(new Font("Tahoma", 0, 14)); // NOI18N
		lblGalleryTitle.setText("Gallery subject");
		panelGalleryTitle.add(lblGalleryTitle);

		panelGallery.add(panelGalleryTitle, BorderLayout.NORTH);
		panelGallery.add(spGalleryView, BorderLayout.CENTER);

		panelGalleryInfo.setLayout(new FlowLayout(FlowLayout.LEADING));

		lblGalleryInfo.setText("Gallery info");
		panelGalleryInfo.add(lblGalleryInfo);

		panelGallery.add(panelGalleryInfo, BorderLayout.SOUTH);

		panelViews.add(panelGallery);

		add(panelViews, BorderLayout.CENTER);

		panelMatchedBiometrics.setLayout(new BorderLayout());

		lblMatchedBiometrics.setText("Matched biometrics ");
		panelMatchedBiometrics.add(lblMatchedBiometrics, BorderLayout.WEST);

		panelMatchedBiometrics.add(comboBoxMatchedBiometrics, BorderLayout.CENTER);

		add(panelMatchedBiometrics, BorderLayout.SOUTH);

		propertyGrid = new SubjectSchemaGrid();
		propertyGrid.setEditable(false);
		spProperties.setViewportView(propertyGrid);

		comboBoxMatchedBiometrics.addItemListener(this);
	}

	private NFingerView showFrictionRidge(NFrictionRidge target, JScrollPane pane, JLabel lblInfo) {
		NFingerView view = new NFingerView();
		view.setFinger(target);
		pane.setViewportView(view);

		NFrictionRidge.ObjectCollection objects = target.getObjects();
		if (objects.isEmpty()) {
			lblInfo.setText("Position = " + target.getPosition());
		} else {
			lblInfo.setText("Position = " + target.getPosition() + ", Quality = " + objects.get(0).getQuality());
		}

		return view;
	}

	private NFaceView showFace(NFace target, JScrollPane pane, JLabel lblInfo) {
		NFaceView view = new NFaceView();
		view.setFace(target);
		pane.setViewportView(view);

		NFace.ObjectCollection objects = target.getObjects();
		if (objects.isEmpty()) {
			lblInfo.setText("");
		} else {
			lblInfo.setText("Quality = " + objects.get(0).getQuality());
		}

		return view;
	}

	private NIrisView showIris(NIris target, JScrollPane pane, JLabel lblInfo) {
		NIrisView view = new NIrisView();
		view.setIris(target);
		pane.setViewportView(view);

		NIris.ObjectCollection objects = target.getObjects();
		if (objects.isEmpty()) {
			lblInfo.setText("Position = " + target.getPosition());
		} else {
			lblInfo.setText("Position = " + target.getPosition() + ", Quality = " + objects.get(0).getQuality());
		}

		return view;
	}

	private VoiceView showVoice(NVoice target, JScrollPane pane, JLabel lblInfo) {
		VoiceView view = new VoiceView();
		view.setVoice(target);
		pane.setViewportView(view);

		NVoice.ObjectCollection objects = target.getObjects();
		if (objects.isEmpty()) {
			lblInfo.setText("");
		} else {
			lblInfo.setText("Quality = " + objects.get(0).getQuality());
		}

		return view;
	}

	private int recordIndexToFaceIndex(int index, List<Integer> recordCounts) {
		if (recordCounts != null) {
			int sum = 0;
			int faceIndex = 0;
			for (int item : recordCounts) {
				if (index >= sum && index < sum + item) {
					return faceIndex;
				}
				sum += item;
				faceIndex++;
			}
		}
		return 0;
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	@Override
	public void tabAdded(NavigationEvent<? extends Object, Tab> ev) {
		if (ev.getDestination().equals(this)) {
			lblSubject.setText("Subject: " + probe.getId());
			lblScore.setText("Score: " + matchingResult.getScore());
			client.get(gallery, null, this);
		}
	}

	@Override
	public void tabEnter(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void tabLeave(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void tabClose(NavigationEvent<? extends Object, Tab> ev) {
		// Do nothing.
	}

	@Override
	public void itemStateChanged(ItemEvent ev) {
		if (ev.getSource().equals(comboBoxMatchedBiometrics)) {
			if (ev.getStateChange() == ItemEvent.SELECTED) {
				Object selected = ev.getItem();
				if (selected instanceof MatchedFinger) {
					MatchedFinger mf = (MatchedFinger) selected;
					final NFingerView view1 = showFrictionRidge(mf.getFirst(), spProbeView, lblProbeInfo);
					final NFingerView view2 = showFrictionRidge(mf.getSecond(), spGalleryView, lblGalleryInfo);
					NIndexPair[] matchedPairs = mf.getDetails().getMatedMinutiae();
					view1.setMatedMinutiae(matchedPairs);
					view2.setMatedMinutiae(matchedPairs);
					view1.prepareTree();
					view2.setTree(view1.getTree());
					view1.addMinutiaSelectionListener(new MinutiaSelectionListener() {

						@Override
						public void selectedMinutiaIndexChanged(MinutiaSelectionEvent e) {
							view2.setSelectedMinutiaIndex(e.getSelelctedIndex());
						}
					});
					view2.addMinutiaSelectionListener(new MinutiaSelectionListener() {

						@Override
						public void selectedMinutiaIndexChanged(MinutiaSelectionEvent e) {
							view1.setSelectedMinutiaIndex(e.getSelelctedIndex());
						}
					});
				} else if (selected instanceof MatchedFace) {
					MatchedFace mf = (MatchedFace) selected;
					showFace(mf.getFirst(), spProbeView, lblProbeInfo);
					showFace(mf.getSecond(), spGalleryView, lblGalleryInfo);
				} else if (selected instanceof MatchedIris) {
					MatchedIris mi = (MatchedIris) selected;
					showIris(mi.getFirst(), spProbeView, lblProbeInfo);
					showIris(mi.getSecond(), spGalleryView, lblGalleryInfo);
				} else if (selected instanceof MatchedVoice) {
					MatchedVoice mv = (MatchedVoice) selected;
					showVoice(mv.getFirst(), spProbeView, lblProbeInfo);
					showVoice(mv.getSecond(), spGalleryView, lblGalleryInfo);
				} else if (selected instanceof MatchedPalm) {
					MatchedPalm mp = (MatchedPalm) selected;
					final NFingerView view1 = showFrictionRidge(mp.getFirst(), spProbeView, lblProbeInfo);
					final NFingerView view2 = showFrictionRidge(mp.getSecond(), spGalleryView, lblGalleryInfo);
					NIndexPair[] matchedPairs = mp.getDetails().getMatedMinutiae();
					view1.setMatedMinutiae(matchedPairs);
					view2.setMatedMinutiae(matchedPairs);
					view1.prepareTree();
					view2.setTree(view1.getTree());
					view1.addMinutiaSelectionListener(new MinutiaSelectionListener() {

						@Override
						public void selectedMinutiaIndexChanged(MinutiaSelectionEvent e) {
							view2.setSelectedMinutiaIndex(e.getSelelctedIndex());
						}
					});
					view2.addMinutiaSelectionListener(new MinutiaSelectionListener() {

						@Override
						public void selectedMinutiaIndexChanged(MinutiaSelectionEvent e) {
							view1.setSelectedMinutiaIndex(e.getSelelctedIndex());
						}
					});
				} else {
					throw new AssertionError("Unknown matched pair: " + selected);
				}
			}
		}
	}

	@Override
	public void completed(NBiometricStatus result, Object attachment) {
		SwingUtils.runOnEDT(new Runnable() {

			@Override
			public void run() {
				if (!schema.isEmpty() && !schema.getThumbnailDataName().isEmpty() && gallery.getProperties().containsKey(schema.getThumbnailDataName())) {
					NBuffer nb = gallery.getProperty("Thumbnail", NBuffer.class);
					ImageIcon icon = new ImageIcon(NImage.fromMemory(nb).toImage());
					lblThumbnailImage.setIcon(icon);
				} else {
					spThumbnail.setVisible(false);
				}

				List<Integer> galeryRecordCounts = new ArrayList<Integer>();
				if (!schema.isEmpty()) {
					NPropertyBag bag = new NPropertyBag();
					gallery.captureProperties(bag);

					if (!schema.getEnrollDataName().isEmpty() && bag.containsKey(schema.getEnrollDataName())) {
						NBuffer templateBuffer = gallery.getTemplateBuffer();
						NBuffer enrollData = (NBuffer) bag.get(schema.getEnrollDataName());
						Exception ex = null;
						try {
							gallery = EnrollDataSerializer.getInstance().deserialize(templateBuffer, enrollData, galeryRecordCounts);
						} catch (DataFormatException e) {
							e.printStackTrace();
							ex = e;
						}
						if (ex != null) {
							final Exception finalEx = ex;
							SwingUtils.runOnEDT(new Runnable() {

								@Override
								public void run() {
									MessageUtils.showError(MatchingResultTab.this, finalEx);
								}
							});
						}
					}
					if ((!schema.getGenderDataName().isEmpty()) && bag.containsKey(schema.getGenderDataName())) {
						String genderString = (String) bag.get(schema.getGenderDataName());
						bag.set(schema.getGenderDataName(), NGender.valueOf(genderString.toUpperCase()));
					}

					propertyGrid.setSource(schema);
					propertyGrid.setValues(bag);
				} else {
					spProperties.setVisible(false);
				}

				comboBoxMatchedBiometrics.removeAllItems();
				NMatchingDetails details = matchingResult.getMatchingDetails();
				if (details == null) {
					tpStatus.setText("Enable 'Return matching details' in settings to see more details in this tab");
					tpStatus.setBackground(new Color(255, 153, 0));
					panelViews.setVisible(false);
					panelMatchedBiometrics.setVisible(false);
				} else {

					int matchingThreshold = client.getMatchingThreshold();

					List<NFinger> flattenedFingers = new ArrayList<NFinger>();
					for (NFinger finger : probe.getFingers()) {
						if (finger.getObjects().size() == 1) {
							flattenedFingers.add(finger);
						}
					}
					for (int i = 0; i < details.getFingers().size(); i++) {
						NFMatchingDetails fDetails = details.getFingers().get(i);
						if ((fDetails.getMatchedIndex() != -1) && (fDetails.getScore() >= matchingThreshold)) {
							comboBoxMatchedBiometrics.addItem(new MatchedFinger(flattenedFingers.get(i), gallery.getFingers().get(fDetails.getMatchedIndex()), fDetails));
						}
					}

					List<NFace> faces = new ArrayList<NFace>(probe.getFaces());
					List<Integer> recordCounts = new ArrayList<Integer>();
					for (NFace face : faces) {
						recordCounts.add(face.getObjects().get(0).getTemplate().getRecords().size());
					}
					for (int i = 0; i < details.getFaces().size(); i++) {
						NLMatchingDetails lDetails = details.getFaces().get(i);
						if ((lDetails.getMatchedIndex() != -1) && (lDetails.getScore() >= matchingThreshold)) {
							comboBoxMatchedBiometrics.addItem(new MatchedFace(faces.get(recordIndexToFaceIndex(i, recordCounts)), gallery.getFaces().get(0), lDetails));
						}
					}

					List<NIris> flattenedIrises = new ArrayList<NIris>();
					for (NIris iris : probe.getIrises()) {
						if (iris.getObjects().size() == 1) {
							flattenedIrises.add(iris);
						}
					}
					for (int i = 0; i < details.getIrises().size(); i++) {
						NEMatchingDetails eDetails = details.getIrises().get(i);
						if ((eDetails.getMatchedIndex() != -1) && (eDetails.getScore() >= matchingThreshold)) {
							comboBoxMatchedBiometrics.addItem(new MatchedIris(flattenedIrises.get(i), gallery.getIrises().get(eDetails.getMatchedIndex()), eDetails));
						}
					}

					List<NPalm> palms = new ArrayList<NPalm>(probe.getPalms());
					for (int i = 0; i < details.getPalms().size(); i++) {
						NFMatchingDetails fDetails = details.getPalms().get(i);
						if ((fDetails.getMatchedIndex() != -1) && (fDetails.getScore() >= matchingThreshold)) {
							comboBoxMatchedBiometrics.addItem(new MatchedPalm(palms.get(i), gallery.getPalms().get(fDetails.getMatchedIndex()), fDetails));
						}
					}

					List<NVoice> flattenedVoices = new ArrayList<NVoice>();
					for (NVoice voice : probe.getVoices()) {
						if (voice.getObjects().size() == 1) {
							flattenedVoices.add(voice);
						}
					}
					for (int i = 0; i < details.getVoices().size(); i++) {
						NSMatchingDetails sDetails = details.getVoices().get(i);
						if ((sDetails.getMatchedIndex() != -1) && (sDetails.getScore() >= matchingThreshold)) {
							comboBoxMatchedBiometrics.addItem(new MatchedVoice(flattenedVoices.get(i), gallery.getVoices().get(sDetails.getMatchedIndex()), sDetails));
						}
					}

					comboBoxMatchedBiometrics.setSelectedIndex(0);
				}
			}
		});
	}

	@Override
	public void failed(Throwable th, Object attachment) {
		MessageUtils.showError(this, th);
	}

}
