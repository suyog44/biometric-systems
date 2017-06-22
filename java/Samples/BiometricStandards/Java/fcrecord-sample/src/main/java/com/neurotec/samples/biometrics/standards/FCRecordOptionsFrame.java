package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FCRFaceImage;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.util.NVersion;

import java.awt.Dimension;
import java.awt.Frame;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JCheckBox;
import javax.swing.JPanel;

public final class FCRecordOptionsFrame extends BDIFOptionsFrame {

	public final class FCRecordOptions {
		private BDIFStandard standard;
		private NVersion version;
		private int flags;

		FCRecordOptions(BDIFStandard standard, NVersion version, int flags) {
			this.standard = standard;
			this.version = version;
			this.flags = flags;
		}

		public BDIFStandard getStandard() {
			return standard;
		}

		public NVersion getVersion() {
			return version;
		}

		public int getFlags() {
			return flags;
		}
	}

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 1L;

	// ==============================================
	// Private fields
	// ==============================================

	private JCheckBox chkProcessFirstImageOnly;
	private JCheckBox chkSkipFeaturePoints;
	private List<StandardVersion> versions;

	// ==============================================
	// Public constructor
	// ==============================================

	public FCRecordOptionsFrame(Frame owner) {
		super(owner);
		this.setTitle("FCRecordOptionsFrame");
		this.setPreferredSize(new Dimension(340, 300));
		initializeComponents();
		versions = new ArrayList<StandardVersion>();
		versions.add(new StandardVersion(BDIFStandard.ANSI, FCRecord.VERSION_ANSI_10, "ANSI/INCITS 385-2004"));
		versions.add(new StandardVersion(BDIFStandard.ISO, FCRecord.VERSION_ISO_10, "ISO/IEC 19794-5:2005"));
		versions.add(new StandardVersion(BDIFStandard.ISO, FCRecord.VERSION_ISO_30, "ISO/IEC 19794-5:2011"));
		setStandardVersions(versions);
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		chkProcessFirstImageOnly = new JCheckBox("Process first image only");
		chkSkipFeaturePoints = new JCheckBox("Skip feature points");

		JPanel fcRecordPanel = new JPanel();
		fcRecordPanel.setPreferredSize(new Dimension(300, 130));
		fcRecordPanel.setBorder(BorderFactory.createTitledBorder("FCRecord"));
		fcRecordPanel.setLayout(new BoxLayout(fcRecordPanel, BoxLayout.Y_AXIS));
		fcRecordPanel.add(chkProcessFirstImageOnly);
		fcRecordPanel.add(chkSkipFeaturePoints);

		this.getContentPane().add(fcRecordPanel);
		fcRecordPanel.setBounds(15, 150, 300, 75);
		getButtonPanel().setBounds(15, 230, 300, 25);
		this.pack();
	}

	// ==============================================
	// Overridden methods
	// ==============================================

	@Override
	public int getFlags() {
		int flags = super.getFlags();
		if (chkProcessFirstImageOnly.isSelected()) {
			flags |= FCRecord.FLAG_PROCESS_FIRST_FACE_IMAGE_ONLY;
		}
		if (chkSkipFeaturePoints.isSelected()) {
			flags |= FCRFaceImage.FLAG_SKIP_FEATURE_POINTS;
		}
		return flags;
	}

	@Override
	public void setFlags(int flags) {
		if ((flags & FCRecord.FLAG_PROCESS_FIRST_FACE_IMAGE_ONLY) == FCRecord.FLAG_PROCESS_FIRST_FACE_IMAGE_ONLY) {
			chkProcessFirstImageOnly.setSelected(true);
		}
		if ((flags & FCRFaceImage.FLAG_SKIP_FEATURE_POINTS) == FCRFaceImage.FLAG_SKIP_FEATURE_POINTS) {
			chkSkipFeaturePoints.setSelected(true);
		}
		super.setFlags(flags);
	}

	public FCRecordOptions getRecordOptions() {
		if (isOk) {
			return new FCRecordOptions(getStandard(), getVersion(), getFlags());
		}
		return null;
	}

}
