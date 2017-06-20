package com.neurotec.samples.abis.subject;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;
import java.io.IOException;
import java.util.List;

import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComponent;
import javax.swing.JFileChooser;
import javax.swing.JPanel;
import javax.swing.JScrollPane;

import com.neurotec.biometrics.NBiometric;
import com.neurotec.biometrics.NBiometricType;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NFrictionRidge;
import com.neurotec.biometrics.NIris;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NVoice;
import com.neurotec.biometrics.swing.NFaceView;
import com.neurotec.biometrics.swing.NFingerView;
import com.neurotec.biometrics.swing.NFingerViewBase.ShownImage;
import com.neurotec.biometrics.swing.NIrisView;
import com.neurotec.images.NImage;
import com.neurotec.images.NImages;
import com.neurotec.lang.NDisposable;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.subject.faces.IcaoWarningsPanel;
import com.neurotec.samples.abis.swing.GeneralizeProgressView;
import com.neurotec.samples.abis.swing.Node;
import com.neurotec.samples.abis.swing.VoiceView;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.CollectionUtils;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.util.Utils;
import com.neurotec.swing.NView;
import com.neurotec.swing.NViewZoomSlider;

public class BiometricPreviewPage extends Page implements ActionListener, PageNavigationListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JComponent view;
	private NBiometric biometric;
	private Node node;
	private JFileChooser fc;
	private NViewZoomSlider horizontalZoomSlider;

	private IcaoWarningsPanel icaoWarningsView;	
	private JButton btnFinish;
	private JButton btnSave;
	private JCheckBox cbShowBinarizedImage;
	private JPanel panelBottom;
	private JPanel panelButtons;
	private JPanel panelZoom;
	private JScrollPane spView;
	private JPanel panelGeneralization;
	private GeneralizeProgressView generalizeProgressView;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public BiometricPreviewPage(PageNavigationController pageController) {
		super("Biometric preview", pageController);
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		spView = new JScrollPane();
		panelBottom = new JPanel();
		panelButtons = new JPanel();
		btnSave = new JButton();
		btnFinish = new JButton();
		panelZoom = new JPanel();
		horizontalZoomSlider = new NViewZoomSlider();
		cbShowBinarizedImage = new JCheckBox();

		setLayout(new BorderLayout());
		add(spView, BorderLayout.CENTER);

		JPanel panelSide = new JPanel();
		panelSide.setLayout(new BorderLayout());
		add(panelSide, BorderLayout.WEST);
		{
			icaoWarningsView = new IcaoWarningsPanel();
			panelSide.add(icaoWarningsView, BorderLayout.NORTH);
		}

		panelBottom.setLayout(new BorderLayout());

		panelGeneralization = new JPanel();
		panelBottom.add(panelGeneralization, BorderLayout.NORTH);
		panelGeneralization.setLayout(new BorderLayout(0, 0));

		generalizeProgressView = new GeneralizeProgressView();
		panelGeneralization.add(generalizeProgressView, BorderLayout.CENTER);


		panelButtons.setLayout(new FlowLayout(FlowLayout.RIGHT));

		btnSave.setText("Save");
		panelButtons.add(btnSave);

		btnFinish.setText("Finish");
		btnFinish.setPreferredSize(new Dimension(65, 23));
		panelButtons.add(btnFinish);

		panelBottom.add(panelButtons, BorderLayout.EAST);

		panelZoom.add(horizontalZoomSlider);

		cbShowBinarizedImage.setText("Show binarized image");
		panelZoom.add(cbShowBinarizedImage);

		panelBottom.add(panelZoom, BorderLayout.WEST);

		add(panelBottom, BorderLayout.SOUTH);

		fc = new JFileChooser();

		btnFinish.addActionListener(this);
		btnSave.addActionListener(this);
		cbShowBinarizedImage.addActionListener(this);
	}

	private void save() {
		if (biometric.getBiometricType().contains(NBiometricType.VOICE)) {
			fc.setFileFilter(null);
		} else {
			fc.setFileFilter(new Utils.ImageFileFilter(NImages.getSaveFileFilter()));
		}
		if (fc.showSaveDialog(this) == JFileChooser.APPROVE_OPTION) {
			try {
				if (biometric.getBiometricType().contains(NBiometricType.VOICE)) {
					((NVoice) biometric).getSoundBuffer().save(fc.getSelectedFile().getAbsolutePath());
				} else {
					NImage image;
					if (biometric.getBiometricType().contains(NBiometricType.FINGER) || biometric.getBiometricType().contains(NBiometricType.PALM)) {
						if (cbShowBinarizedImage.isSelected()) {
							image = ((NFrictionRidge) biometric).getBinarizedImage();
						} else {
							image = ((NFrictionRidge) biometric).getImage();
						}
					} else if (biometric.getBiometricType().contains(NBiometricType.FACE)) {
						image = ((NFace) biometric).getImage();
					} else if (biometric.getBiometricType().contains(NBiometricType.IRIS)) {
						image = ((NIris) biometric).getImage();
					} else {
						throw new AssertionError("Unknown biometric: " + biometric);
					}
					image.save(fc.getSelectedFile().getAbsolutePath());
				}
			} catch (IOException e) {
				MessageUtils.showError(this, e);
			}
		}
	}

	private void updateView() {
		if (node == null) {
			return;
		}
		icaoWarningsView.setVisible(false);
		generalizeProgressView.setVisible(node.isGeneralizedNode());
		biometric = CollectionUtils.getFirst(node.getItems());

		boolean canSave;
		boolean canShowBinarizedImage = false;
		if (node.getBiometricType().contains(NBiometricType.FINGER) || node.getBiometricType().contains(NBiometricType.PALM)) {
			NFrictionRidge first = (NFrictionRidge) biometric;
			NFingerView fingerView = new NFingerView();
			fingerView.setAutofit(true);
			fingerView.setFinger(first);
			spView.setViewportView(fingerView);
			if (view instanceof NView) {
				((NDisposable) view).dispose();
			}
			view = fingerView;
			horizontalZoomSlider.setView(fingerView);
			canSave = first.getImage() != null;
			if (generalizeProgressView.isVisible()) {
				List<NBiometric> generalized = node.getAllGeneralized();
				generalizeProgressView.setView(fingerView);
				generalizeProgressView.setBiometrics(node.getItems());
				generalizeProgressView.setGeneralized(generalized);
				NBiometric selected = CollectionUtils.getFirst(generalized);
				generalizeProgressView.setSelected(selected != null ? selected : first);
			}
			canShowBinarizedImage = first.getBinarizedImage() != null;
			updateShownImage();
		} else if (biometric.getBiometricType().contains(NBiometricType.FACE)) {
			NFace first = (NFace) biometric;
			NFaceView faceView = new NFaceView();
			faceView.setAutofit(true);
			faceView.setShowIcaoArrows(false);
			faceView.setFace(first);
			if (wasIcaoCheckPerformed(first)) {
				icaoWarningsView.setVisible(true);
				icaoWarningsView.setFace(first);
			}
			if (view instanceof NView) {
				((NDisposable) view).dispose();
			}
			view = faceView;
			horizontalZoomSlider.setView(faceView);
			spView.setViewportView(faceView);
			canSave = first.getImage() != null;
			if (generalizeProgressView.isVisible()) {
				List<NBiometric> generalized = node.getAllGeneralized();
				generalizeProgressView.setView(faceView);
				generalizeProgressView.setBiometrics(node.getItems());
				generalizeProgressView.setGeneralized(generalized);
				NBiometric selected = CollectionUtils.getFirst(generalized);
				generalizeProgressView.setSelected(selected != null ? selected : first);
			}
		} else if (biometric.getBiometricType().contains(NBiometricType.IRIS)) {
			NIris iris = (NIris) biometric;
			NIrisView irisView = new NIrisView();
			irisView.setAutofit(true);
			irisView.setIris(iris);
			if (view instanceof NView) {
				((NDisposable) view).dispose();
			}
			view = irisView;
			spView.setViewportView(irisView);
			horizontalZoomSlider.setView(irisView);
			canSave = iris.getImage() != null;
		} else if (biometric.getBiometricType().contains(NBiometricType.VOICE)) {
			view = new VoiceView();
			spView.setViewportView(view);
			NVoice voice = (NVoice) biometric;
			((VoiceView) view).setVoice(voice);
			canSave = voice.getSoundBuffer() != null;
		} else {
			throw new IllegalArgumentException("Cannot preview biometric type " + biometric.getBiometricType());
		}
		btnSave.setVisible(canSave);
		if (biometric.getBiometricType().contains(NBiometricType.VOICE)) {
			btnSave.setText("Save audio file");
			panelZoom.setVisible(false);
		} else {
			btnSave.setText("Save image");
			panelZoom.setVisible(true);
		}
		if (!canShowBinarizedImage) {
			cbShowBinarizedImage.setSelected(false);
		}
		cbShowBinarizedImage.setVisible(canShowBinarizedImage);

		revalidate();
		repaint();
	}

	private void updateShownImage() {
		if (cbShowBinarizedImage.isSelected()) {
			((NFingerView) view).setShownImage(ShownImage.RESULT);
		} else {
			((NFingerView) view).setShownImage(ShownImage.ORIGINAL);
		}
	}

	private PropertyChangeListener generalizeProgressViewPropertyChanged = new PropertyChangeListener() {

		@Override
		public void propertyChange(PropertyChangeEvent evt) {
			if ("Selected".equals(evt.getPropertyName())) {
				if (node.getBiometricType().contains(NBiometricType.FINGER) || node.getBiometricType().contains(NBiometricType.PALM)) {
					NFingerView fingerView = (NFingerView)view;
					NFrictionRidge finger = fingerView.getFinger();
					biometric = finger;
					cbShowBinarizedImage.setVisible(finger.getBinarizedImage() != null);
					if (!cbShowBinarizedImage.isVisible() && cbShowBinarizedImage.isSelected()) {
						cbShowBinarizedImage.setSelected(false);
					}
				} else if (node.getBiometricType().contains(NBiometricType.FACE)) {
					NFaceView faceView = (NFaceView)view;
					biometric = faceView.getFace();
				}
			}

		}
	};

	private boolean wasIcaoCheckPerformed(NFace face) {
		NLAttributes attributes = face.getObjects().get(0);
		if (attributes != null && !attributes.getTokenImageRect().isEmpty()) {
			return true;
		} else {
			NLAttributes parentObject = (NLAttributes) face.getParentObject();
			return parentObject != null && !parentObject.getTokenImageRect().isEmpty();
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public NBiometric getBiometric() {
		return biometric;
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {
			updateView();
		}
		generalizeProgressView.addPropertyChangeListener(generalizeProgressViewPropertyChanged);
	}

	@Override
	public void navigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		if (view != null) {
			horizontalZoomSlider.setView(null);
		}
		generalizeProgressView.removePropertyChangeListener(generalizeProgressViewPropertyChanged);
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		if (ev.getSource().equals(btnFinish)) {
			getPageController().navigateToStartPage();
		} else if (ev.getSource().equals(btnSave)) {
			save();
		} else if (ev.getSource().equals(cbShowBinarizedImage)) {
			if (view instanceof NFingerView) {
				updateShownImage();
			}
		}
	}

	public void setNode(Node node) {
		if (node == null) throw new NullPointerException("node");
		if (!node.isBiometricNode()) throw new IllegalArgumentException("node is not biometric");
		this.node = node;
		updateView();
	}

}
