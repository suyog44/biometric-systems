package com.neurotec.samples.biometrics;

public enum RecordType {

	// ==============================================
	// Enum constants
	// ==============================================

	TT_AN_TEMPLATE(0, "ANSI/NIST-ITL 1-2000 (ANTemplate)", "ANTemplate files", "Open ANTemplate", "Save ANTemplate"),
	TT_ANSI_FM_RECORD(1, "ANSI INCITS 378-2004 (FMRecord)", "FMRecord files", "Open FMRecord (ANSI)", "Save FMRecord"),
	TT_ISO_FM_RECORD(2, "ISO/IEC 19794-2:2005 (FMRecord)", "FMRecord files", "Open FMRecord (ISO)", "Save FMRecord"),
	TT_NF_RECORD(3, "Neurotechnology Finger Record (NFRecord)", "NFRecord files", "Open NFRecord(s)", "Save NFRecord(s)"),
	TT_NF_TEMPLATE(4, "Neurotechnology Fingers Template (NFTemplate)", "NFTemplate files", "Open NFTemplate", "Save NFTemplate"),
	TT_NL_RECORD(5, "Neurotechnology Face Record (NLRecord)", "NLRecord files", "Open NLRecord(s)", "Save NLRecord(s)"),
	TT_NL_TEMPLATE(6, "Neurotechnology Faces Template (NLTemplate)", "NLTemplate files", "Open NLTemplate", "Save NLTemplate"),
	TT_N_TEMPLATE(7, "Neurotechnology Template (NTemplate)", "NTemplate files", "Open NTemplate", "Save NTemplate");

	// ==============================================
	// Private fields
	// ==============================================

	private int value;

	private final String templateName;
	private final String fileType;
	private final String openDialogTitle;
	private final String saveDialogTitle;

	// ==============================================
	// Constructor
	// ==============================================

	RecordType(int value, String templateName, String fileType, String openDialogTitle, String saveDialogTitle) {
		this.value = value;
		this.templateName = templateName;
		this.fileType = fileType;
		this.openDialogTitle = openDialogTitle;
		this.saveDialogTitle = saveDialogTitle;
	}

	// ==============================================
	// Public methods
	// ==============================================

	public int getValue() {
		return value;
	}

	public String getTemplateName() {
		return templateName;
	}

	public String getSaveDialogTitle() {
		return saveDialogTitle;
	}

	public String getFileType() {
		return fileType;
	}

	public String getOpenDialogTitle() {
		return openDialogTitle;
	}

}
