package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.util.NVersion;

import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.Frame;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.JCheckBox;
import javax.swing.JPanel;

public final class IIRecordOptionsFrame extends BDIFOptionsFrame {

	public class IIRecordOptions {

		private BDIFStandard standard;
		private NVersion version;
		private int flags;

		public IIRecordOptions(BDIFStandard standard, NVersion version, int flags) {
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

	// ===========================================================
	// Private static fields
	// ===========================================================

	private static final long serialVersionUID = 1L;

	// ===========================================================
	// Private fields
	// ===========================================================

	private JCheckBox chkProcessFirstImageOnly;
	private List<StandardVersion> versions;

	// ===========================================================
	// Public constructor
	// ===========================================================

	public IIRecordOptionsFrame(Frame owner) {
		super(owner);
		this.setTitle("IIRecordOptionsFrame");
		this.setPreferredSize(new Dimension(340, 270));
		initGui();
		versions = new ArrayList<StandardVersion>();
		versions.add(new StandardVersion(BDIFStandard.ANSI, IIRecord.VERSION_ANSI_10, "ANSI/INCITS 379-2004"));
		versions.add(new StandardVersion(BDIFStandard.ISO, IIRecord.VERSION_ISO_10, "ISO/IEC 19794-6:2005"));
		versions.add(new StandardVersion(BDIFStandard.ISO, IIRecord.VERSION_ISO_20, "ISO/IEC 19794-6:2011"));
		setStandardVersions(versions);
	}

	// ===========================================================
	// Private methods
	// ===========================================================

	private void initGui() {
		chkProcessFirstImageOnly = new JCheckBox("Process first iris image only");

		JPanel fcRecordPanel = new JPanel(new BorderLayout());
		fcRecordPanel.setBorder(BorderFactory.createTitledBorder("IIRecord"));
		fcRecordPanel.add(chkProcessFirstImageOnly, BorderLayout.CENTER);

		getContentPane().add(fcRecordPanel);
		fcRecordPanel.setBounds(15, 150, 300, 50);
		getButtonPanel().setBounds(15, 210, 300, 25);
		this.pack();
	}

	// ===========================================================
	// Protected methods
	// ===========================================================

	@Override
	protected void onModeChanged() {
		super.onModeChanged();

		switch (getMode()) {
		case NEW:
			chkProcessFirstImageOnly.setEnabled(false);
			break;
		default:
		}
	}

	// ===========================================================
	// Public methods
	// ===========================================================

	public IIRecordOptions getIIRecordOptions() {
		if (isOk) {
			return new IIRecordOptions(getStandard(), getVersion(), getFlags());
		} else {
			return null;
		}
	}

	@Override
	public int getFlags() {
		int flags = super.getFlags();
		if (chkProcessFirstImageOnly.isSelected()) {
			flags |= IIRecord.FLAG_PROCESS_IRIS_FIRST_IRIS_IMAGE_ONLY;
		}
		return flags;
	}

	@Override
	public void setFlags(int flags) {
		if ((flags & IIRecord.FLAG_PROCESS_IRIS_FIRST_IRIS_IMAGE_ONLY) == IIRecord.FLAG_PROCESS_IRIS_FIRST_IRIS_IMAGE_ONLY) {
			chkProcessFirstImageOnly.setSelected(true);
		}
		super.setFlags(flags);
	}

}
