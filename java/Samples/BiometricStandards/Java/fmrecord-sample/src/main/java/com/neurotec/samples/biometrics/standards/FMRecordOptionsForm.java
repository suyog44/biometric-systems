package com.neurotec.samples.biometrics.standards;

import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FMRecord;

import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Toolkit;
import java.util.ArrayList;
import java.util.List;

import javax.swing.BorderFactory;
import javax.swing.JCheckBox;
import javax.swing.JPanel;

public final class FMRecordOptionsForm extends BDIFOptionsFrame {

	// ==============================================
	// Private static fields
	// ==============================================

	private static final long serialVersionUID = 3075030762294399842L;
	private static final int FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY = 4352;

	// ==============================================
	// Private GUI controls
	// ==============================================

	private JPanel gbFMRecord;
	private JCheckBox cbProcessFirstFingerViewOnly;
	private JCheckBox cbProcessFirstFingerOnly;

	// ==============================================
	// Public constructor
	// ==============================================

	public FMRecordOptionsForm(MainFrame owner, String title) {
		super(owner, title);
		initializeComponents();

		List<StandardVersion> versions = new ArrayList<StandardVersion>();
		versions.add(new StandardVersion(BDIFStandard.ANSI, FMRecord.VERSION_ANSI_20, "ANSI/INCITS 378-2004"));
		versions.add(new StandardVersion(BDIFStandard.ANSI, FMRecord.VERSION_ANSI_35, "ANSI/INCITS 378-2009"));
		versions.add(new StandardVersion(BDIFStandard.ISO, FMRecord.VERSION_ISO_20, "ISO/IEC 19794-2:2005"));
		versions.add(new StandardVersion(BDIFStandard.ISO, FMRecord.VERSION_ISO_30, "ISO/IEC 19794-2:2011"));
		setVersions(versions);

		Dimension dim = Toolkit.getDefaultToolkit().getScreenSize();
		setLocation(dim.width / 2 - getSize().width / 2, dim.height / 2	- getSize().height / 2);
	}

	public FMRecordOptionsForm(MainFrame owner, String title, int flag) {
		this(owner, title);
		setFlags(flag);
	}

	// ==============================================
	// Private methods
	// ==============================================

	private void initializeComponents() {

		// cbProcessFirstFingersOnly
		cbProcessFirstFingerOnly = new JCheckBox();
		cbProcessFirstFingerOnly.setName("cbProcessFirstFingerOnly");
		cbProcessFirstFingerOnly.setSize(134, 17);
		cbProcessFirstFingerOnly.setText("Process first finger only");

		// cbProcessFirstFingerViewOnly
		cbProcessFirstFingerViewOnly = new JCheckBox();
		cbProcessFirstFingerViewOnly.setName("cbProcessFirstFingerViewOnly");
		cbProcessFirstFingerViewOnly.setSize(159, 17);
		cbProcessFirstFingerViewOnly.setText("Process first finger view only");

		// gbFMRecord
		gbFMRecord = new JPanel();
		gbFMRecord.setPreferredSize(new Dimension(315, 110));
		gbFMRecord.setBorder(BorderFactory.createTitledBorder("FIRecord"));

		GridBagLayout gbFirRecordLayout = new GridBagLayout();
		gbFirRecordLayout.columnWidths = new int[] {90};
		gbFirRecordLayout.rowHeights = new int[] {25, 25};
		gbFMRecord.setLayout(gbFirRecordLayout);
		GridBagConstraints c = new GridBagConstraints();
		c.fill = GridBagConstraints.HORIZONTAL;
		c.gridx = 0;
		c.gridy = 0;
		gbFMRecord.add(cbProcessFirstFingerViewOnly, c);
		c.gridx = 0;
		c.gridy = 1;
		gbFMRecord.add(cbProcessFirstFingerOnly, c);
		gbFMRecord.setLocation(12, 162);
		gbFMRecord.setName("gbFMRecord");
		gbFMRecord.setSize(313, 75);
		gbFMRecord.setBorder(BorderFactory.createTitledBorder("FMRecord"));

		getButtonPanel().setBounds(0, 240, 315, 25);
		setSize(350, 300);
		add(gbFMRecord);
		setName("FMRecordOptionsForm");
	}

	// ==============================================
	// Protected methods
	// ==============================================

	@Override
	protected  void buttonOKActionPerformed() {
		getMainFrame().setCurrentStandard(getStandard());
		getMainFrame().setCurrentVersion(getVersion());
		getMainFrame().setCurrentFlags(getFlags());
	}

	@Override
	protected void onModeChanged() {
		super.onModeChanged();

		switch (getMode()) {
		case NEW:
			cbProcessFirstFingerOnly.setEnabled(false);
			break;
		case SAVE:
		case OPEN:
		case CONVERT:
			break;
		default:
			throw new AssertionError("UNKNOWN");
		}
	}

	// ==============================================
	// Public getters and setters
	// ==============================================

	@Override
	public  int getFlags() {
		int flags = super.getFlags();
		if (cbProcessFirstFingerOnly.isSelected()) {
			flags |= FMRecord.FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY;
		}
		if (cbProcessFirstFingerViewOnly.isSelected()) {
			flags |= FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY;
		}
		return flags;
	}

	@Override
	public  void setFlags(int flags) {
		if ((flags & FMRecord.FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY) == FMRecord.FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY) {
			cbProcessFirstFingerOnly.setSelected(true);
		}

		if ((flags & FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY) == FLAG_PROCESS_FIRST_FINGER_VIEW_ONLY) {
			cbProcessFirstFingerViewOnly.setSelected(true);
		}
		super.setFlags(flags);
	}

}
