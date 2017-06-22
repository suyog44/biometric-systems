package com.neurotec.samples.abis.subject;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.GridLayout;
import java.awt.Image;
import java.awt.Insets;
import java.awt.SystemColor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;
import java.util.Arrays;
import java.util.EnumSet;
import java.util.concurrent.ExecutionException;
import java.util.Map;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.Box.Filler;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFileChooser;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.SwingConstants;
import javax.swing.SwingWorker;
import javax.swing.UIManager;
import javax.swing.event.CaretEvent;
import javax.swing.event.CaretListener;
import javax.swing.text.SimpleAttributeSet;
import javax.swing.text.StyleConstants;
import javax.swing.text.StyledDocument;

import com.neurotec.biometrics.NBiometricOperation;
import com.neurotec.biometrics.NFace;
import com.neurotec.biometrics.NGender;
import com.neurotec.biometrics.NLAttributes;
import com.neurotec.biometrics.NSubject;
import com.neurotec.images.NImage;
import com.neurotec.io.NBuffer;
import com.neurotec.samples.abis.event.NavigationEvent;
import com.neurotec.samples.abis.event.PageNavigationListener;
import com.neurotec.samples.abis.schema.DatabaseSchema;
import com.neurotec.samples.abis.settings.SettingsManager;
import com.neurotec.samples.abis.subject.BiometricController.TenPrintCardType;
import com.neurotec.samples.abis.swing.AutoCompleteTextField;
import com.neurotec.samples.abis.swing.SubjectSchemaGrid;
import com.neurotec.samples.abis.tabbedview.Page;
import com.neurotec.samples.abis.tabbedview.PageNavigationController;
import com.neurotec.samples.abis.util.CollectionUtils;
import com.neurotec.samples.abis.util.MessageUtils;
import com.neurotec.samples.swing.ImageThumbnailFileChooser;
import com.neurotec.samples.util.Utils;
import com.neurotec.util.NPropertyBag;

public final class SubjectOverviewPage extends Page implements PageNavigationListener, ActionListener, CaretListener {

	// ===========================================================
	// Static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	public static final Color COLOR_OK = new Color(0, 128, 0);
	public static final Color COLOR_WARNING = new Color(255, 153, 0);
	public static final Color COLOR_ERROR = new Color(255, 0, 0);

	// ===========================================================
	// Private fields
	// ===========================================================

	private BiometricModel biometricModel;
	private BiometricController biometricController;

	private SubjectSchemaGrid propertyGrid;
	private NImage thumbnail;
	private ImageThumbnailFileChooser fc;
	private File lastFile;
	private boolean busy;

	private JButton btnEnroll;
	private JButton btnEnrollDC;
	private JButton btnIdentify;
	private JButton btnOpenThumbnail;
	private JButton btnPrintApplicant;
	private JButton btnPrintCriminal;
	private JButton btnSaveTemplate;
	private JButton btnUpdate;
	private JButton btnVerify;
	private Filler filler1;
	private JLabel lblInfoIcon;
	private JLabel lblProgress;
	private JLabel lblSubjectID;
	private JLabel lblThumbnailImage;
	private JPanel panelButtons;
	private JPanel panelCenter;
	private JPanel panelEnrollData;
	private JPanel panelHint;
	private JPanel panelLeft;
	private JPanel panelProgress;
	private JPanel panelQuery;
	private JPanel panelTable;
	private JPanel panelThumbnail;
	private JPanel panelThumbnailControls;
	private JPanel panelThumbnailIcon;
	private JPanel panelTop;
	private JProgressBar progressBar;
	private JScrollPane spPropertyGrid;
	private JScrollPane spThumbnail;
	private AutoCompleteTextField tfQuery;
	private AutoCompleteTextField tfSubjectID;
	private JTextPane tpHint;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public SubjectOverviewPage(PageNavigationController pageController) {
		super("Subject", pageController);
		initGUI();
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGUI() {
		GridBagConstraints gridBagConstraints;

		panelTop = new JPanel();
		lblSubjectID = new JLabel();
		tfSubjectID = new AutoCompleteTextField();
		panelLeft = new JPanel();
		panelButtons = new JPanel();
		btnIdentify = new JButton();
		btnVerify = new JButton();
		btnEnroll = new JButton();
		btnEnrollDC = new JButton();
		btnUpdate = new JButton();
		btnPrintCriminal = new JButton();
		btnPrintApplicant = new JButton();
		btnSaveTemplate = new JButton();
		panelProgress = new JPanel();
		progressBar = new JProgressBar();
		lblProgress = new JLabel();
		panelCenter = new JPanel();
		panelQuery = new JPanel();
		lblInfoIcon = new JLabel();
		tfQuery = new AutoCompleteTextField();
		panelEnrollData = new JPanel();
		panelThumbnail = new JPanel();
		panelThumbnailControls = new JPanel();
		btnOpenThumbnail = new JButton();
		spThumbnail = new JScrollPane();
		panelThumbnailIcon = new JPanel();
		lblThumbnailImage = new JLabel();
		panelTable = new JPanel();
		spPropertyGrid = new JScrollPane();
		filler1 = new Filler(new Dimension(0, 5), new Dimension(0, 5), new Dimension(32767, 5));
		panelHint = new JPanel();
		tpHint = new JTextPane();
		StyledDocument doc = tpHint.getStyledDocument();
		SimpleAttributeSet styleAttributes = new SimpleAttributeSet();
		StyleConstants.setAlignment(styleAttributes, StyleConstants.ALIGN_CENTER);
		StyleConstants.setSpaceAbove(styleAttributes, 3);
		StyleConstants.setSpaceBelow(styleAttributes, 3);
		StyleConstants.setLeftIndent(styleAttributes, 5);
		StyleConstants.setRightIndent(styleAttributes, 5);
		StyleConstants.setFontSize(styleAttributes, 16);
		StyleConstants.setBold(styleAttributes, true);
		StyleConstants.setForeground(styleAttributes, SystemColor.menu);
		doc.setParagraphAttributes(0, doc.getLength(), styleAttributes, false);

		setLayout(new BorderLayout());

		panelTop.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
		panelTop.setLayout(new FlowLayout(FlowLayout.LEADING));

		lblSubjectID.setText("Subject ID");
		panelTop.add(lblSubjectID);

		tfSubjectID.setPreferredSize(new Dimension(400, 20));
		panelTop.add(tfSubjectID);

		add(panelTop, BorderLayout.NORTH);

		panelLeft.setBorder(BorderFactory.createEmptyBorder(10, 10, 10, 10));
		panelLeft.setLayout(new BorderLayout());

		panelButtons.setLayout(new GridLayout(9, 1, 2, 2));

		btnIdentify.setText("Identify");
		panelButtons.add(btnIdentify);

		btnVerify.setText("Verify");
		panelButtons.add(btnVerify);

		btnEnroll.setText("Enroll");
		panelButtons.add(btnEnroll);

		btnEnrollDC.setText("<html><center>Enroll with duplicate<br/>check</center></html>");
		panelButtons.add(btnEnrollDC);

		btnUpdate.setText("Update");
		panelButtons.add(btnUpdate);

		btnPrintCriminal.setText("Print criminal card");
		panelButtons.add(btnPrintCriminal);

		btnPrintApplicant.setText("Print applicant card");
		panelButtons.add(btnPrintApplicant);

		btnSaveTemplate.setText("Save template");
		panelButtons.add(btnSaveTemplate);

		panelProgress.setLayout(new BorderLayout());

		progressBar.setIndeterminate(true);
		panelProgress.add(progressBar, BorderLayout.SOUTH);

		lblProgress.setHorizontalAlignment(SwingConstants.CENTER);
		lblProgress.setText("Preparing preview...");
		panelProgress.add(lblProgress, BorderLayout.CENTER);

		panelButtons.add(panelProgress);

		panelLeft.add(panelButtons, BorderLayout.NORTH);

		add(panelLeft, BorderLayout.WEST);

		panelCenter.setBorder(BorderFactory.createEtchedBorder());
		panelCenter.setLayout(new BorderLayout());

		panelQuery.setLayout(new GridBagLayout());

		lblInfoIcon.setIcon(UIManager.getIcon("OptionPane.questionIcon"));
		lblInfoIcon.setText("Query");
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.insets = new Insets(5, 5, 5, 5);
		panelQuery.add(lblInfoIcon, gridBagConstraints);
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.fill = GridBagConstraints.HORIZONTAL;
		gridBagConstraints.weightx = 1.0;
		gridBagConstraints.insets = new Insets(5, 5, 5, 5);
		panelQuery.add(tfQuery, gridBagConstraints);

		panelCenter.add(panelQuery, BorderLayout.NORTH);

		panelEnrollData.setBorder(BorderFactory.createTitledBorder("Enroll Data"));
		panelEnrollData.setLayout(new BorderLayout());

		panelThumbnail.setBorder(BorderFactory.createTitledBorder("Thumbnail"));
		panelThumbnail.setPreferredSize(new Dimension(300, 0));
		panelThumbnail.setLayout(new BorderLayout());

		panelThumbnailControls.setLayout(new FlowLayout(FlowLayout.LEFT));

		btnOpenThumbnail.setText("Open image");
		panelThumbnailControls.add(btnOpenThumbnail);

		panelThumbnail.add(panelThumbnailControls, BorderLayout.NORTH);

		panelThumbnailIcon.setLayout(new GridBagLayout());
		gridBagConstraints = new GridBagConstraints();
		gridBagConstraints.fill = GridBagConstraints.BOTH;
		panelThumbnailIcon.add(lblThumbnailImage, gridBagConstraints);

		spThumbnail.setViewportView(panelThumbnailIcon);

		panelThumbnail.add(spThumbnail, BorderLayout.CENTER);

		panelEnrollData.add(panelThumbnail, BorderLayout.WEST);

		panelTable.setLayout(new GridLayout(1, 1));
		panelTable.add(spPropertyGrid);

		panelEnrollData.add(panelTable, BorderLayout.CENTER);
		panelEnrollData.add(filler1, BorderLayout.PAGE_START);

		panelCenter.add(panelEnrollData, BorderLayout.CENTER);

		add(panelCenter, BorderLayout.CENTER);

		panelHint.setBorder(BorderFactory.createEmptyBorder(3, 3, 3, 3));
		panelHint.setLayout(new GridLayout(1, 0));

		tpHint.setEditable(false);
		panelHint.add(tpHint);

		add(panelHint, BorderLayout.SOUTH);

		if (lblInfoIcon.getIcon() instanceof ImageIcon) {
			ImageIcon icon = (ImageIcon) lblInfoIcon.getIcon();
			Image newimg = icon.getImage().getScaledInstance(20, 20, java.awt.Image.SCALE_SMOOTH);
			lblInfoIcon.setIcon(new ImageIcon(newimg));
		} else {
			lblInfoIcon.setIcon(null);
			System.out.println("Warning: icon is not ImageIcon.");
		}

		lblInfoIcon.setToolTipText("<html>Identification operation can use query so that only subjects with matching biographic data would be used for biometric identification.<br/>"
								   + "Example: Country='Germany' AND NOT (City='Berlin' OR City='München')</html>");
		propertyGrid = new SubjectSchemaGrid();
		spPropertyGrid.setViewportView(propertyGrid);

		fc = new ImageThumbnailFileChooser();
		fc.setIcon(Utils.createIconImage("images/Logo16x16.png"));
		fc.setMultiSelectionEnabled(false);

		btnEnroll.addActionListener(this);
		btnEnrollDC.addActionListener(this);
		btnIdentify.addActionListener(this);
		btnVerify.addActionListener(this);
		btnUpdate.addActionListener(this);
		btnPrintCriminal.addActionListener(this);
		btnPrintApplicant.addActionListener(this);
		btnSaveTemplate.addActionListener(this);
		btnOpenThumbnail.addActionListener(this);
		tfSubjectID.addCaretListener(this);
	}

	private void trySetThumbnail() {
		DatabaseSchema schema = biometricModel.getDatabaseSchema();
		if (!schema.isEmpty() && !schema.getThumbnailDataName().isEmpty()) {
			thumbnail = getSubjectThumbnail();
			if (thumbnail != null) {
				setThumbnailImage(thumbnail);
			}
		}
	}

	private void tryFillProperties() {
		DatabaseSchema schema = biometricModel.getDatabaseSchema();
		if (!schema.isEmpty() && !biometricModel.getSubject().getProperties().isEmpty()) {
			NPropertyBag bag = biometricModel.getSubject().getProperties();
			List<String> allowedProperties = schema.getAllowedProperties();
			for (Map.Entry<String, Object> entry : bag.toArray()) {
				if (allowedProperties.contains(entry.getKey())) {
					bag.remove(entry.getKey());
					propertyGrid.setValue(entry.getKey(), entry.getValue());
				}
			}
		}
	}

	private void tryFillGenderField() {
		DatabaseSchema schema = biometricModel.getDatabaseSchema();
		if (!schema.isEmpty() && !schema.getGenderDataName().isEmpty()) {
			NGender gender = (NGender) propertyGrid.getValue(schema.getGenderDataName());
			if (gender == NGender.UNSPECIFIED) {
				for (NFace face : biometricModel.getSubject().getFaces()) {
					NGender g = face.getObjects().get(0).getGender();
					if ((g == NGender.MALE) || (g == NGender.FEMALE)) {
						propertyGrid.setValue(schema.getGenderDataName(), g);
						break;
					}
				}
			}
		}
	}

	private void updateControls() {
		boolean empty = isSubjectEmpty();
		if (empty) {
			setHint("Subject is empty. Click on wanted modality in tree view to create a new template", COLOR_WARNING);
		} else {
			setHint("Subject is ready for action. Click on buttons above to perform action", COLOR_OK);
		}
		EnumSet<NBiometricOperation> operations = EnumSet.noneOf(NBiometricOperation.class);
		operations.addAll(biometricModel.getClient().getLocalOperations());
		if (!biometricModel.getClient().getRemoteConnections().isEmpty()) {
			operations.addAll(biometricModel.getClient().getRemoteConnections().get(0).getOperations());
		}
		btnEnroll.setEnabled(!empty && !busy && operations.contains(NBiometricOperation.ENROLL));
		btnEnrollDC.setEnabled(!empty && !busy && operations.contains(NBiometricOperation.ENROLL_WITH_DUPLICATE_CHECK));
		btnIdentify.setEnabled(!empty && !busy);
		btnVerify.setEnabled(!empty && !busy && operations.contains(NBiometricOperation.VERIFY));
		btnUpdate.setEnabled(!empty && !busy && operations.contains(NBiometricOperation.UPDATE));
		btnSaveTemplate.setEnabled(!empty && !busy);
		btnPrintCriminal.setEnabled(!biometricModel.getSubject().getFingers().isEmpty() && !busy);
		btnPrintApplicant.setEnabled(!biometricModel.getSubject().getFingers().isEmpty() && !busy);
		btnOpenThumbnail.setEnabled(!busy);
		panelProgress.setVisible(busy);

		DatabaseSchema schema = biometricModel.getDatabaseSchema();
		if (schema.isEmpty()) {
			panelEnrollData.setVisible(false);
		} else {
			panelThumbnail.setVisible(!schema.getThumbnailDataName().isEmpty());
		}
	}

	private File getFile(boolean open) {
		fc.setCurrentDirectory(lastFile);
		int result;
		if (open) {
			result = fc.showOpenDialog(this);
		} else {
			result = fc.showSaveDialog(this);
		}
		if (result == JFileChooser.APPROVE_OPTION) {
			lastFile = fc.getSelectedFile();
			return lastFile;
		} else {
			return null;
		}
	}

	private void enroll(final boolean checkDuplicates) {
		if (Utils.isNullOrEmpty(biometricModel.getSubject().getId())) {
			errorFocusOnID();
			return;
		}

		new SwingWorker<Void, Object>() {

			@Override
			protected Void doInBackground() throws Exception {
				DatabaseSchema schema = biometricModel.getDatabaseSchema();
				if (!biometricModel.getDatabaseSchema().isEmpty()) {
					biometricController.resetProperties(propertyGrid.getProperties());
					if (!schema.getThumbnailDataName().isEmpty()) {
						biometricController.setThumbnail(thumbnail);
					}
					if (!schema.getEnrollDataName().isEmpty()) {
						biometricController.prepareEnrollData();
					}
				}
				biometricController.enroll(checkDuplicates);
				return null;
			}

			@Override
			protected void done() {
				super.done();
				busy = false;
				updateControls();
				try {
					get();
				} catch (InterruptedException e) {
					e.printStackTrace();
					Thread.currentThread().interrupt();
				} catch (ExecutionException e) {
					MessageUtils.showError(SubjectOverviewPage.this, e);
				}
			}

		}.execute();
	}

	private boolean isSubjectEmpty() {
		NSubject subject = biometricModel.getSubject();
		return subject.getFingers().isEmpty()
			&& subject.getFaces().isEmpty()
			&& subject.getIrises().isEmpty()
			&& subject.getVoices().isEmpty()
			&& subject.getPalms().isEmpty();
	}

	private NImage getSubjectThumbnail() {
		String thumbnailDataName = biometricModel.getDatabaseSchema().getThumbnailDataName();

		if (biometricModel.getSubject().getProperties().containsKey(thumbnailDataName)) {
			NBuffer imageBuffer = biometricModel.getSubject().getProperty(thumbnailDataName, NBuffer.class);
			if (imageBuffer.size() > 0) {
				return NImage.fromMemory(biometricModel.getSubject().getProperty(thumbnailDataName, NBuffer.class));
			}
		}

		for (NFace face : biometricModel.getSubject().getFaces()) {
			NLAttributes attr = CollectionUtils.getFirst(face.getObjects());
			if (attr != null) {
				NImage image = attr.getThumbnail();
				if (image != null) {
					return image;
				}
			}
		}

		return null;
	}

	private void setThumbnailImage(NImage img) {
		ImageIcon icon = new ImageIcon(img.writeTo(null));
		lblThumbnailImage.setIcon(icon);
	}

	private void setHint(String msg, Color color) {
		tpHint.setText(msg);
		tpHint.setBackground(color);
	}

	private void errorFocusOnID() {
		tfSubjectID.requestFocus();
		tfSubjectID.setBackground(Color.RED);
	}

	private void printCardButtonPressed(final TenPrintCardType type) {

		new SwingWorker<Void, Object>() {

			@Override
			protected Void doInBackground() throws Exception {
				biometricController.printTenPrintCard(type);
				return null;
			}

			@Override
			protected void done() {
				super.done();
				busy = false;
				updateControls();
				try {
					get();
				} catch (InterruptedException e) {
					e.printStackTrace();
					Thread.currentThread().interrupt();
				} catch (ExecutionException e) {
					MessageUtils.showError(SubjectOverviewPage.this, e);
				}
			}
		}.execute();
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public void setBiometricModel(BiometricModel biometricModel) {
		if (biometricModel == null) {
			throw new NullPointerException("biometricModel");
		}
		this.biometricModel = biometricModel;
	}

	public void setBiometricController(BiometricController biometricController) {
		if (biometricController == null) {
			throw new NullPointerException("biometricController");
		}
		this.biometricController = biometricController;
	}

	@Override
	public void navigatedTo(NavigationEvent<? extends Object, Page> ev) {
		if (equals(ev.getDestination())) {

			EnumSet<NBiometricOperation> remoteOperations = EnumSet.noneOf(NBiometricOperation.class);
			if (!biometricModel.getClient().getRemoteConnections().isEmpty()) {
				remoteOperations = biometricModel.getClient().getRemoteConnections().get(0).getOperations();
			}
			if (biometricModel.getClient().getLocalOperations().contains(NBiometricOperation.LIST_IDS) || remoteOperations.contains(NBiometricOperation.LIST_IDS)) {
				tfSubjectID.clearAutocomplete();
				tfSubjectID.addAutocomplete(Arrays.asList(biometricModel.getClient().listIds()));
			}

			biometricController.setID(tfSubjectID.getText());
			tfQuery.clearAutocomplete();
			tfQuery.addAutocomplete(SettingsManager.getQueryAutoComplete());

			if (propertyGrid.isEmpty()) {
				propertyGrid.setSource(biometricModel.getDatabaseSchema());
			}

			trySetThumbnail();
			tryFillProperties();
			tryFillGenderField();
			updateControls();
		}
	}

	@Override
	public void navigatingFrom(NavigationEvent<? extends Object, Page> ev) {
		// Do nothing.
	}

	@Override
	public void actionPerformed(ActionEvent ev) {
		try {
			if (ev.getSource() == btnEnroll) {
				enroll(false);
			} else if (ev.getSource() == btnEnrollDC) {
				enroll(true);
			} else if (ev.getSource() == btnIdentify) {
				String query = tfQuery.getText();
				if (!query.isEmpty() && !tfQuery.containsAutocomplete(query)) {
					tfQuery.addAutocomplete(query);
					SettingsManager.setQueryAutoComplete(tfQuery.getAutocomplete());
				}
				biometricController.identify(query.isEmpty() ? null : query);
			} else if (ev.getSource() == btnVerify) {
				if (Utils.isNullOrEmpty(biometricModel.getSubject().getId())) {
					errorFocusOnID();
					return;
				}
				biometricController.verify();
			} else if (ev.getSource() == btnUpdate) {
				if (Utils.isNullOrEmpty(biometricModel.getSubject().getId())) {
					errorFocusOnID();
					return;
				}
				biometricController.update();
			} else if (ev.getSource() == btnSaveTemplate) {
				File file = getFile(false);
				if (file != null) {
					biometricController.saveTemplate(file);
				}
			} else if (ev.getSource() == btnPrintCriminal) {
				busy = true;
				updateControls();
				printCardButtonPressed(TenPrintCardType.CRIMINAL);
			} else if (ev.getSource() == btnPrintApplicant) {
				busy = true;
				updateControls();
				printCardButtonPressed(TenPrintCardType.APPLICANT);
			} else if (ev.getSource() == btnOpenThumbnail) {
				File file = getFile(true);
				if (file != null) {
					thumbnail = NImage.fromFile(file.getAbsolutePath());
					setThumbnailImage(thumbnail);
				}
			}
		} catch (Exception e) {
			MessageUtils.showError(this, e);
		}
	}

	@Override
	public void caretUpdate(CaretEvent e) {
		if (e.getSource().equals(tfSubjectID)) {
			if (tfSubjectID.getBackground().equals(Color.RED)) {
				tfSubjectID.setBackground(Color.WHITE);
			}
			biometricController.setID(tfSubjectID.getText());
		}
	}

}
