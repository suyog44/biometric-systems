package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.util.NVersion;

import java.awt.Dimension;
import java.awt.Frame;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.BoxLayout;
import javax.swing.JCheckBox;
import javax.swing.JPanel;

public final class FIRecordOptionsFrame extends BDIFOptionsFrame {

	public final class FIRecordOptions {
		private BDIFStandard standard;
		private NVersion version;
		private int flags;

		FIRecordOptions(BDIFStandard standard, NVersion version, int flags) {
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

	private JCheckBox chkProcessFirstFingerOnly;
	private JCheckBox chkProcessFirstFingerViewOnly;
	private List<StandardVersion> versions;

	// ==============================================
	// Public constructor
	// ==============================================

	public FIRecordOptionsFrame(Frame owner) {
		super(owner);
		this.setTitle("FIRecordOptionsFrame");
		this.setPreferredSize(new Dimension(340, 300));
		initializeComponents();
		versions = new ArrayList<StandardVersion>();
		versions.add(new StandardVersion(BDIFStandard.ANSI, FIRecord.VERSION_ANSI_10, "ANSI/INCITS 381-2004"));
		versions.add(new StandardVersion(BDIFStandard.ANSI, FIRecord.VERSION_ANSI_25, "ANSI/INCITS 381-2009"));
		versions.add(new StandardVersion(BDIFStandard.ISO, FIRecord.VERSION_ISO_10, "ISO/IEC 19794-4:2005"));
		versions.add(new StandardVersion(BDIFStandard.ISO, FIRecord.VERSION_ISO_20, "ISO/IEC 19794-4:2011"));
		setStandardVersions(versions);
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {
		chkProcessFirstFingerOnly = new JCheckBox("Process first finger only");
		chkProcessFirstFingerViewOnly = new JCheckBox("Process first fingerView only");

		JPanel fiRecordPanel = new JPanel();
		fiRecordPanel.setPreferredSize(new Dimension(300, 130));
		fiRecordPanel.setBorder(BorderFactory.createTitledBorder("FIRecord"));
		fiRecordPanel.setLayout(new BoxLayout(fiRecordPanel, BoxLayout.Y_AXIS));
		fiRecordPanel.add(chkProcessFirstFingerOnly);
		fiRecordPanel.add(chkProcessFirstFingerViewOnly);

		this.getContentPane().add(fiRecordPanel);
		fiRecordPanel.setBounds(15, 150, 300, 75);
		getButtonPanel().setBounds(15, 230, 300, 25);
		this.pack();
	}

	// ==============================================
	// Overridden methods
	// ==============================================

	@Override
	protected void onModeChanged() {
		super.onModeChanged();
		switch (getMode()) {
		case NEW:
			chkProcessFirstFingerOnly.setEnabled(false);
			break;
		default:
		}
	}

	@Override
	public int getFlags() {
		int flags = super.getFlags();
		if (chkProcessFirstFingerOnly.isSelected()) {
			flags |= FIRecord.FLAG_PROCESS_FIRST_FINGER_ONLY;
		}
		if (chkProcessFirstFingerViewOnly.isSelected()) {
			flags |= FIRecord.FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY;
		}
		return flags;
	}

	@Override
	public void setFlags(int flags) {
		if ((flags & FIRecord.FLAG_PROCESS_FIRST_FINGER_ONLY) == FIRecord.FLAG_PROCESS_FIRST_FINGER_ONLY) {
			chkProcessFirstFingerOnly.setSelected(true);
		}
		if ((flags & FIRecord.FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY) == FIRecord.FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY) {
			chkProcessFirstFingerViewOnly.setSelected(true);
		}
		super.setFlags(flags);
	}

	public FIRecordOptions getFiRecordOptions() {
		if(isOk) {
			return new FIRecordOptions(getStandard(), getVersion(), getFlags());
		} else {
			return null;
		}
	}

}
