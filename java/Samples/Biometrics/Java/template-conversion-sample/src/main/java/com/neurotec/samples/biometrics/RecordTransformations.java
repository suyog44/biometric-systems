package com.neurotec.samples.biometrics;

import com.neurotec.biometrics.NFRecord;
import com.neurotec.biometrics.NFTemplate;
import com.neurotec.biometrics.NLRecord;
import com.neurotec.biometrics.NLTemplate;
import com.neurotec.biometrics.NTemplate;
import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANValidationLevel;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.FMRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.lang.NObject;
import com.neurotec.util.NVersion;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import javax.swing.JOptionPane;

public final class RecordTransformations {

	// ==============================================
	// Private static fields
	// ==============================================

	private static RecordTransformations defaultInstance;

	// ==============================================
	// Public static methods
	// ==============================================

	public static RecordTransformations getInstance() {
		synchronized (RecordTransformations.class) {
			if (defaultInstance == null) {
				defaultInstance = new RecordTransformations();
			}
			return defaultInstance;
		}
	}

	// ==============================================
	// Private fields
	// ==============================================

	/**.
	 * source		ANT		FMR(ANSI) FMR(ISO) NFR		NFT		NLR		NLT		NT
	 * dest
	 * ANT			-		1			1		1		1		0		0		1
	 * FMR(ANSI)	1		-			1		1		1		0		0		1
	 * FMR(ISO)		1		1			-		1		1		0		0		1
	 * NFR			1		1			1		-		1		0		0		1
	 * NFT			1		1			1		1		-		0		0		1
	 * NLR			0		0			0		0		0		-		1		1
	 * NLT			0		0			0		0		0		1		-		1
	 * NT			1		1			1		1		1		1		1		-
	 */
	private final int[][] possibleTransformations = new int[][] {
		{0, 1, 1, 1, 1, 0, 0, 1},
		{1, 0, 1, 1, 1, 0, 0, 1},
		{1, 1, 0, 1, 1, 0, 0, 1},
		{1, 1, 1, 0, 1, 0, 0, 1},
		{1, 1, 1, 1, 0, 0, 0, 1},
		{0, 0, 0, 0, 0, 0, 1, 1},
		{0, 0, 0, 0, 0, 1, 0, 1},
		{1, 1, 1, 1, 1, 1, 1, 0}
	};

	private ANTemplate currentANTemplate;
	private NFRecord[] currentNFRecords;
	private NFTemplate currentNFTemplate;
	private NTemplate currentNTemplate;
	private FMRecord currentFMRecord;
	private NLTemplate currentNLTemplate;
	private NLRecord[] currentNLRecords;
	private final int[] disableArray = new int[8];

	// ==============================================
	// Private constructor
	// ==============================================

	private RecordTransformations() {
	}

	// ==============================================
	// Private methods
	// ==============================================

	// ==============================================
	// Open templates
	// ==============================================

	private String openANTemplate(List<String> fileNames) throws IOException {
		if (currentANTemplate != null) {
			currentANTemplate.dispose();
		}
		currentANTemplate = new ANTemplate(fileNames.get(0), ANValidationLevel.STANDARD);
		return String.format("Filename: %s%n Type: ANTemplate%n", fileNames.get(0));
	}

	private String openNFRecord(List<String> fileNames) throws IOException {
		if (currentNFRecords != null) {
			NObject.disposeArray(currentNFRecords);
		}

		currentNFRecords = new NFRecord[fileNames.size()];
		StringBuilder openedFileNamesBuilder = new StringBuilder(128);
		for (int i = 0; i < currentNFRecords.length; i++) {
			NBuffer bufferTmp = NFile.readAllBytes(fileNames.get(i));
			currentNFRecords[i] = new NFRecord(bufferTmp);
			openedFileNamesBuilder.append(String.format("Filename: %s%n Type: NFRecord%n", fileNames.get(i)));
			openedFileNamesBuilder.append(String.format("Cores: %d, Double cores: %d, Deltas: %d, Minutia: %d%nG: %d%n%n", currentNFRecords[i].getCores().size(), currentNFRecords[i].getDoubleCores().size(), currentNFRecords[i].getDeltas().size(), currentNFRecords[i].getMinutiae().size(), currentNFRecords[i].getG()));
		}
		return openedFileNamesBuilder.toString();
	}

	private String openNFTemplate(List<String> fileNames) throws IOException {
		StringBuilder returnTextBuilder = new StringBuilder(64);
		if (currentNFTemplate != null) {
			currentNFTemplate.dispose();
		}

		NBuffer arrayN = NFile.readAllBytes(fileNames.get(0));
		currentNFTemplate = new NFTemplate(arrayN);
		returnTextBuilder.append(String.format("Filename: %s%n Type: NFTemplate%n", fileNames.get(0)));

		int recordCount = currentNFTemplate.getRecords().size();
		returnTextBuilder.append(String.format("Fingerprint record count: %d%n", recordCount));
		if (recordCount == 0) {
			Arrays.fill(disableArray, 1);
		} else {
			Arrays.fill(disableArray, 0);
		}
		return returnTextBuilder.toString();
	}

	private String openNTemplate(List<String> fileNames) throws IOException {
		StringBuilder returnTextBuilder = new StringBuilder(128);
		Arrays.fill(disableArray, 0);

		if (currentNTemplate != null) {
			currentNTemplate.dispose();
		}

		NBuffer arrayN = NFile.readAllBytes(fileNames.get(0));
		currentNTemplate = new NTemplate(arrayN);

		returnTextBuilder.append(String.format("Filename: %s%n Type: NTemplate%n", fileNames.get(0)));

		NFTemplate fingers = currentNTemplate.getFingers();

		if ((fingers != null) && (fingers.getRecords().size() > 0)) {
			returnTextBuilder.append(String.format("Fingerprint record count: %d%n", fingers.getRecords().size()));
		} else {
			disableArray[1] = 1;
			disableArray[2] = 1;
			disableArray[3] = 1;
			disableArray[4] = 1;
		}

		NLTemplate faces = currentNTemplate.getFaces();
		if ((faces != null) && (faces.getRecords().size() > 0)) {
			returnTextBuilder.append(String.format("Face record count: %d%n", faces.getRecords().size()));
		} else {
			disableArray[0] = 1;
			disableArray[5] = 1;
			disableArray[6] = 1;
		}
		return returnTextBuilder.toString();
	}

	private String openANSIFMRecord(List<String> fileNames) throws IOException {
		if (currentFMRecord != null) {
			currentFMRecord.dispose();
		}
		NBuffer arrayN = NFile.readAllBytes(fileNames.get(0));
		currentFMRecord = new FMRecord(arrayN, BDIFStandard.ANSI);
		return String.format("Filename: %s%n Type: FMRecord ANSI%n", fileNames.get(0));
	}

	private String openISOFMRecord(List<String> fileNames) throws IOException {
		if (currentFMRecord != null) {
			currentFMRecord.dispose();
		}
		NBuffer arrayN = NFile.readAllBytes(fileNames.get(0));
		currentFMRecord = new FMRecord(arrayN, BDIFStandard.ISO);
		return String.format("Filename: %s%n Type: FMRecord ISO%n", fileNames.get(0));
	}

	private String openNLTemplate(List<String> fileNames) throws IOException {
		StringBuilder returnTextBuilder = new StringBuilder(64);
		Arrays.fill(disableArray, 0);

		if (currentNLTemplate != null) {
			currentNLTemplate.dispose();
		}

		NBuffer arrayN = NFile.readAllBytes(fileNames.get(0));
		currentNLTemplate = new NLTemplate(arrayN);

		returnTextBuilder.append(String.format("Filename: %s%n Type: NLTemplate%n", fileNames.get(0)));

		int recordCount = currentNLTemplate.getRecords().size();
		returnTextBuilder.append(String.format("Face record count: %d%n", recordCount));
		if (recordCount == 0) {
			disableArray[5] = 1;
			disableArray[7] = 1;
		}
		return returnTextBuilder.toString();
	}

	private String openNLRecord(List<String> fileNames) throws IOException {
		StringBuilder openedFileNamesBuilder = new StringBuilder(64);
		if (currentNLRecords != null) {
			NObject.disposeArray(currentNLRecords);
		}

		currentNLRecords = new NLRecord[fileNames.size()];
		for (int i = 0; i < currentNLRecords.length; i++) {
			NBuffer bufferTmp = NFile.readAllBytes(fileNames.get(i));
			currentNLRecords[i] = new NLRecord(bufferTmp);
			openedFileNamesBuilder.append(String.format("Filename: %s%n Type: NLRecord%n", fileNames.get(i)));
			openedFileNamesBuilder.append(String.format("Quality: %d%n%n", currentNLRecords[i].getQuality()));
		}
		return openedFileNamesBuilder.toString();
	}

	// ==============================================
	// Convert templates
	// ==============================================

	private ANTemplate fromNTemplateToANTemplate(NTemplate ntemplate) throws Exception {
		String tot = "NEUR"; // type of transaction
		String dai = "NeurotecDest"; // destination agency identifier
		String ori = "NeurotecOrig"; // originating agency identifier
		String tcn = "00001"; // transaction control number

		// Creating ANTemplate object from NTemplate object.
		try {
			return new ANTemplate(ANTemplate.VERSION_CURRENT, tot, dai, ori, tcn, true, ntemplate, 0);
		} catch (Exception e) {
			throw new Exception("Error converting NTemplate to ANTemplate.", e);
		}
	}

	private NTemplate fromANTemplateToNTemplate(ANTemplate antemplate) throws Exception {
		try {
			return antemplate.toNTemplate();
		} catch (Exception e) {
			throw new Exception("Error converting ANTemplate to NTemplate.", e);
		}
	}

	private NFTemplate fromANTemplateToNFTemplate(ANTemplate antemplate) throws Exception {
		try {
			return antemplate.toNTemplate().getFingers();
		} catch (Exception e) {
			throw new Exception("Error converting ANTemplate to NFTemplate.", e);
		}
	}

	private FMRecord fromNFTemplateToFMRecord(NFTemplate nftemplate, BDIFStandard standard) throws Exception {
		try {
			return new FMRecord(nftemplate, standard, new NVersion(2, 0));
		} catch (Exception e) {
			throw new Exception("Error converting NFTemplate to FMRecord.", e);
		}
	}

	private NFTemplate fromFMRecordToNFTemplate(FMRecord fmrecord) throws Exception {
		try {
			return fmrecord.toNFTemplate();
		} catch (Exception e) {
			throw new Exception("Error converting FMRecord to NFTemplate.", e);
		}
	}

	private NTemplate fromFMRecordToNTemplate(FMRecord fmrecord) throws Exception {
		try {
			return fmrecord.toNTemplate();
		} catch (Exception e) {
			throw new Exception("Error converting FMRecord to NTemplate.", e);
		}
	}

	private String convertToNFRecord(RecordType fromType, File file) throws Exception {
		String returnText = "";
		switch (fromType) {
		case TT_N_TEMPLATE:
			if (validateCurrentNTemplateFingerCount()) {
				returnText = saveNFTemplateAsNFRecord(currentNTemplate.getFingers(), file);
			}
			break;

		case TT_NF_TEMPLATE:
			if (validateCurrentNFTemplate()) {
				returnText = saveNFTemplateAsNFRecord(currentNFTemplate, file);
			}
			break;

		case TT_AN_TEMPLATE:
			if ((currentANTemplate != null)) {
				NFTemplate nfTemplate = fromANTemplateToNFTemplate(currentANTemplate);
				returnText = saveNFTemplateAsNFRecord(nfTemplate, file);
				nfTemplate.dispose();
				break;
			}
			printInvalidStatement();
			break;

		case TT_ANSI_FM_RECORD:
		case TT_ISO_FM_RECORD:
			if ((currentFMRecord != null)) {
				NFTemplate nfTemplate = fromFMRecordToNFTemplate(currentFMRecord);
				returnText = saveNFTemplateAsNFRecord(nfTemplate, file);
				nfTemplate.dispose();
				break;
			}
			printInvalidStatement();
			break;

		case TT_NF_RECORD:
		case TT_NL_RECORD:
		case TT_NL_TEMPLATE:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return returnText;

	}

	private String convertToNFTemplate(RecordType fromType, File file) throws Exception {

		switch (fromType) {
		case TT_N_TEMPLATE:
			if (validateCurrentNTemplateFingerCount()) {
				return saveNFTemplate(currentNTemplate.getFingers(), file);
			}
			break;

		case TT_NF_RECORD:
			if (validateCurrentNFRecords()) {
				NFTemplate nfTemplate = new NFTemplate();
				for (int i = 0; i < currentNFRecords.length; i++) {
					try {
						nfTemplate.getRecords().add((NFRecord) currentNFRecords[i].clone());
					} catch (CloneNotSupportedException e) {
						throw new AssertionError("Can't happen");
					}
				}

				return saveNFTemplate(nfTemplate, file);
			}
			break;

		case TT_AN_TEMPLATE:
			if ((currentANTemplate != null)) {
				NFTemplate nfTemplate = fromANTemplateToNFTemplate(currentANTemplate);
				return saveNFTemplate(nfTemplate, file);
			}
			printInvalidStatement();
			break;

		case TT_ANSI_FM_RECORD:
		case TT_ISO_FM_RECORD:
			if ((currentFMRecord != null)) {
				NFTemplate nfTemplate = fromFMRecordToNFTemplate(currentFMRecord);
				return saveNFTemplate(nfTemplate, file);
			}
			printInvalidStatement();
			break;

		case TT_NF_TEMPLATE:
		case TT_NL_RECORD:
		case TT_NL_TEMPLATE:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return "";
	}

	private String convertToNLRecord(RecordType fromType, File file) throws IOException {
		switch (fromType) {
		case TT_N_TEMPLATE:
			if (validateCurrentNTemplateFaceCount()) {
				return saveNLTemplateAsNLRecord(currentNTemplate.getFaces(), file);
			}
			break;

		case TT_NL_TEMPLATE:
			if (validateCurrentNLTemplate()) {
				return saveNLTemplateAsNLRecord(currentNLTemplate, file);
			}
			break;

		case TT_ANSI_FM_RECORD:
		case TT_AN_TEMPLATE:
		case TT_ISO_FM_RECORD:
		case TT_NF_RECORD:
		case TT_NF_TEMPLATE:
		case TT_NL_RECORD:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return "";

	}

	private String convertToNLTemplate(RecordType fromType, File file) throws IOException {
		switch (fromType) {
		case TT_NL_RECORD:
			if ((currentNLRecords == null) || (currentNLRecords.length == 0)) {
				printInvalidStatement();
			} else {
				NLTemplate nlTemplate = new NLTemplate();
				for (int i = 0; i < currentNLRecords.length; i++) {
					try {
						nlTemplate.getRecords().add((NLRecord) currentNLRecords[i].clone());
					} catch (CloneNotSupportedException e) {
						throw new AssertionError("Can't happen");
					}
				}
				return saveNLTemplate(nlTemplate, file);
			}
			break;

		case TT_N_TEMPLATE:
			if (validateCurrentNTemplateFaces()) {
				return saveNLTemplate(currentNTemplate.getFaces(), file);
			}
			break;

		case TT_ANSI_FM_RECORD:
		case TT_AN_TEMPLATE:
		case TT_ISO_FM_RECORD:
		case TT_NF_RECORD:
		case TT_NF_TEMPLATE:
		case TT_NL_TEMPLATE:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return "";

	}

	private String convertToNTemplate(RecordType fromType, File file) throws Exception {

		String returnText = "";
		switch (fromType) {
		case TT_AN_TEMPLATE:
			if ((currentANTemplate != null)) {
				NTemplate nTemplate = fromANTemplateToNTemplate(currentANTemplate);
				returnText = saveNTemplate(nTemplate, file);
				break;
			}
			printInvalidStatement();
			break;

		case TT_NF_TEMPLATE:
			if (validateCurrentNFTemplate()) {
				NTemplate nTemplate = new NTemplate();
				try {
					nTemplate.setFingers((NFTemplate) currentNFTemplate.clone());
				} catch (CloneNotSupportedException e) {
					throw new AssertionError("Can't happen");
				}
				returnText = saveNTemplate(nTemplate, file);
			}
			break;

		case TT_NF_RECORD:
			if (validateCurrentNFRecords()) {
				NFTemplate nfTemplate = new NFTemplate();
				for (int i = 0; i < currentNFRecords.length; i++) {
					try {
						nfTemplate.getRecords().add((NFRecord) currentNFRecords[i].clone());
					} catch (CloneNotSupportedException e) {
						throw new AssertionError("Can't happen");
					}
				}
				NTemplate nTemplate = new NTemplate();
				try {
					nTemplate.setFingers((NFTemplate) nfTemplate.clone());
				} catch (CloneNotSupportedException e) {
					throw new AssertionError("Can't happen");
				}

				returnText = saveNTemplate(nTemplate, file);
				nfTemplate.dispose();
			}
			break;

		case TT_ANSI_FM_RECORD:
		case TT_ISO_FM_RECORD:
			if ((currentFMRecord != null)) {
				NTemplate nTemplate = fromFMRecordToNTemplate(currentFMRecord);
				returnText = saveNTemplate(nTemplate, file);
				break;
			}
			printInvalidStatement();
			break;

		case TT_NL_TEMPLATE:
			if (validateCurrentNLTemplate()) {
				NTemplate nTemplate = new NTemplate();
				try {
					nTemplate.setFaces((NLTemplate) currentNLTemplate.clone());
				} catch (CloneNotSupportedException e) {
					throw new AssertionError("Can't happen");
				}
				returnText = saveNTemplate(nTemplate, file);
			}
			break;

		case TT_NL_RECORD:
			if (validateCurrentNLRecords()) {
				NLTemplate nlTemplate = new NLTemplate();
				for (int i = 0; i < currentNLRecords.length; i++) {
					try {
						nlTemplate.getRecords().add((NLRecord) currentNLRecords[i].clone());
					} catch (CloneNotSupportedException e) {
						throw new AssertionError("Can't happen");
					}
				}
				NTemplate tmpl2 = new NTemplate();
				try {
					tmpl2.setFaces((NLTemplate) nlTemplate.clone());
				} catch (CloneNotSupportedException e) {
					throw new AssertionError("Can't happen");
				}

				returnText = saveNTemplate(tmpl2, file);
				nlTemplate.dispose();
			}
			break;

		case TT_N_TEMPLATE:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return returnText;
	}

	private String convertToANTemplate(RecordType fromType, File file) throws Exception {

		String returnText = "";
		switch (fromType) {
		case TT_N_TEMPLATE:
			if (validateCurrentNTemplateFaces()) {
				ANTemplate anTemplate = fromNTemplateToANTemplate(currentNTemplate);
				returnText = saveANTemplate(anTemplate, file);
			}
			break;

		case TT_NF_TEMPLATE:
			if (validateCurrentNFTemplate()) {
				NTemplate nTemplate = new NTemplate();
				try {
					nTemplate.setFingers((NFTemplate) currentNFTemplate.clone());
				} catch (CloneNotSupportedException e) {
					throw new AssertionError("Cant Happen");
				}
				ANTemplate anTemplate = fromNTemplateToANTemplate(nTemplate);
				returnText = saveANTemplate(anTemplate, file);
				nTemplate.dispose();
			}
			break;

		case TT_NF_RECORD:
			if (validateCurrentNFRecords()) {
				NFTemplate nfTemplate = new NFTemplate();
				for (int i = 0; i < currentNFRecords.length; i++) {
					try {
						nfTemplate.getRecords().add((NFRecord) currentNFRecords[i].clone());
					} catch (CloneNotSupportedException e) {
						throw new AssertionError("Cant Happen");
					}
				}
				NTemplate nTemplate = new NTemplate();
				try {
					nTemplate.setFingers((NFTemplate) nfTemplate.clone());
				} catch (CloneNotSupportedException e) {
					throw new AssertionError("Cant Happen");
				}

				ANTemplate anTemplate = fromNTemplateToANTemplate(nTemplate);
				returnText = saveANTemplate(anTemplate, file);
				nTemplate.dispose();
				nfTemplate.dispose();
			}
			break;

		case TT_ANSI_FM_RECORD:
		case TT_ISO_FM_RECORD:
			if ((currentFMRecord != null)) {
				NTemplate nTemplate = fromFMRecordToNTemplate(currentFMRecord);
				ANTemplate anTemplate = fromNTemplateToANTemplate(nTemplate);
				returnText = saveANTemplate(anTemplate, file);
				nTemplate.dispose();
				break;
			}
			printInvalidStatement();
			break;

		case TT_AN_TEMPLATE:
		case TT_NL_RECORD:
		case TT_NL_TEMPLATE:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return returnText;
	}

	private String convertToFMRecord(RecordType fromType, File file, boolean toISO) throws Exception {

		String returnText = "";
		switch (fromType) {
		case TT_N_TEMPLATE:
			if (validateCurrentNTemplateFingers()) {
				FMRecord fmRecord = toISO ? fromNFTemplateToFMRecord(currentNTemplate.getFingers(), BDIFStandard.ISO) : fromNFTemplateToFMRecord(currentNTemplate.getFingers(), BDIFStandard.ANSI);
				returnText = saveFMRecord(fmRecord, file);
			}
			break;

		case TT_NF_TEMPLATE:
			if (validateCurrentNFTemplate()) {
				FMRecord fmRecord = toISO ? fromNFTemplateToFMRecord(currentNFTemplate, BDIFStandard.ISO) : fromNFTemplateToFMRecord(currentNFTemplate, BDIFStandard.ANSI);
				returnText = saveFMRecord(fmRecord, file);
			}
			break;

		case TT_NF_RECORD:
			if (validateCurrentNFRecords()) {
				NFTemplate nfTemplate = new NFTemplate();
				for (int i = 0; i < currentNFRecords.length; i++) {
					try {
						nfTemplate.getRecords().add((NFRecord) currentNFRecords[i].clone());
					} catch (CloneNotSupportedException e) {
						throw new AssertionError("Can't happen");
					}
				}

				FMRecord fmRecord = toISO ? fromNFTemplateToFMRecord(nfTemplate, BDIFStandard.ISO) : fromNFTemplateToFMRecord(nfTemplate, BDIFStandard.ANSI);
				returnText = saveFMRecord(fmRecord, file);
				nfTemplate.dispose();
			}
			break;

		case TT_AN_TEMPLATE:
			if ((currentANTemplate != null)) {
				NFTemplate nfTemplate = fromANTemplateToNFTemplate(currentANTemplate);
				FMRecord record = toISO ? fromNFTemplateToFMRecord(nfTemplate, BDIFStandard.ISO) : fromNFTemplateToFMRecord(nfTemplate, BDIFStandard.ANSI);
				returnText = saveFMRecord(record, file);
				nfTemplate.dispose();
				break;
			}
			printInvalidStatement();
			break;

		case TT_ANSI_FM_RECORD:
		case TT_ISO_FM_RECORD:
			if (currentFMRecord != null) {
				NFTemplate nfTemplate = fromFMRecordToNFTemplate(currentFMRecord);
				FMRecord fmRecord = toISO ? fromNFTemplateToFMRecord(nfTemplate, BDIFStandard.ISO) : fromNFTemplateToFMRecord(nfTemplate, BDIFStandard.ANSI);
				returnText = saveFMRecord(fmRecord, file);
				nfTemplate.dispose();
				break;
			}
			printInvalidStatement();
			break;

		case TT_NL_RECORD:
		case TT_NL_TEMPLATE:
			throw new AssertionError("Unsupported transformation");
		default:
			throw new AssertionError("Invalid source type");
		}
		return returnText;
	}

	// ===========================================================
	// Save templates
	// ===========================================================

	private String saveFMRecord(FMRecord fmRecord, File file) throws IOException {
		if (file != null) {
			NFile.writeAllBytes(file.getAbsolutePath(), fmRecord.save());
			fmRecord.dispose();
			return "Saved record (FMRecord): " + file.getPath() + "\r\n";
		}
		return "";
	}

	private String saveNFTemplate(NFTemplate nfTemplate, File file) throws IOException {
		if (file != null) {
			NFile.writeAllBytes(file.getAbsolutePath(), nfTemplate.save());
			nfTemplate.dispose();
			return "Saved record (NFTemplate): " + file.getPath() + "\r\n";
		}
		return "";
	}

	private String saveNLTemplate(NLTemplate nlTemplate, File file) throws IOException {
		if (file != null) {
			NFile.writeAllBytes(file.getAbsolutePath(), nlTemplate.save());
			nlTemplate.dispose();
			return "Saved record (NLTemplate): " + file.getPath() + "\r\n";
		}
		return "";
	}

	private String saveNTemplate(NTemplate nTemplate, File file) throws IOException {
		if (file != null) {
			NFile.writeAllBytes(file.getAbsolutePath(), nTemplate.save());
			nTemplate.dispose();
			return "Saved record (NTemplate): " + file.getPath() + "\r\n";
		}
		return "";
	}

	private String saveANTemplate(ANTemplate anTemplate, File file) throws IOException {
		if (file != null) {
			anTemplate.save(file.getPath());
			anTemplate.dispose();
			return "Saved record (ANTemplate): " + file.getPath() + "\r\n";
		}
		return "";
	}

	private String saveNLTemplateAsNLRecord(NLTemplate nlTemplate, File file) throws IOException {

		if (file != null) {
			StringBuilder returnTextBuilder = new StringBuilder(64);
			String filePath = file.getAbsolutePath();
			int index = filePath.lastIndexOf('.');
			String extension = "";
			String name;
			if (index > 0) {
				extension = filePath.substring(index + 1);
				name = filePath.substring(0, index);
			} else {
				name = "";
			}

			for (int i = 0; i < nlTemplate.getRecords().size(); i++) {
				String tmp = String.format("%s%d.%s", name, i, extension);
				NFile.writeAllBytes(tmp, nlTemplate.getRecords().get(i).save());
				returnTextBuilder.append("Saved record (NLRecord): ").append(tmp).append("\r\n");
			}
			return returnTextBuilder.toString();
		}
		return "";
	}

	private String saveNFTemplateAsNFRecord(NFTemplate nfTemplate, File file) throws IOException {
		if (file != null) {
			StringBuilder returnTextBuilder = new StringBuilder(64);
			String filePath = file.getAbsolutePath();
			int index = filePath.lastIndexOf('.');
			String extension = "";
			String name;
			if (index > 0) {
				extension = filePath.substring(index + 1);
				name = filePath.substring(0, index);
			} else {
				name = "";
			}

			for (int i = 0; i < nfTemplate.getRecords().size(); i++) {
				String tmp = String.format("%s%d.%s", name, i, extension);
				NFile.writeAllBytes(tmp, nfTemplate.getRecords().get(i).save());
				returnTextBuilder.append("Saved record (NFRecord): ").append(tmp).append("\r\n");
			}
			return returnTextBuilder.toString();
		}
		return "";
	}

	// ===========================================================
	// Validate templates
	// ===========================================================

	private boolean validateCurrentNFTemplate() {
		if ((currentNFTemplate == null) || (currentNFTemplate.getRecords().size() == 0)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNLTemplate() {
		if ((currentNLTemplate == null) || (currentNLTemplate.getRecords().size() == 0)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNTemplateFingers() {
		if ((currentNTemplate == null) || (currentNTemplate.getFingers() == null)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNTemplateFingerCount() {
		if ((currentNTemplate == null) || (currentNTemplate.getFingers() == null) || (currentNTemplate.getFingers().getRecords().size() == 0)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNTemplateFaces() {
		if ((currentNTemplate == null) || (currentNTemplate.getFaces() == null)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNTemplateFaceCount() {
		if ((currentNTemplate == null) || (currentNTemplate.getFaces() == null) || (currentNTemplate.getFaces().getRecords().size() == 0)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNFRecords() {
		if ((currentNFRecords == null) || (currentNFRecords.length == 0)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private boolean validateCurrentNLRecords() {
		if ((currentNLRecords == null) || (currentNLRecords.length == 0)) {
			printInvalidStatement();
			return false;
		}
		return true;
	}

	private void printInvalidStatement() {
		JOptionPane.showMessageDialog(null, "No templates opened for conversion, or data is invalid.", "Template Convertion: Information", JOptionPane.INFORMATION_MESSAGE);
	}

	// ==============================================
	// Public methods
	// ==============================================

	public int[] getPossibleTransformations(int type) {
		return possibleTransformations[type].clone();
	}

	public void clearCurrentRecords() {
		currentANTemplate = null;
		currentNFRecords = null;
		currentNFTemplate = null;
		currentNTemplate = null;
		currentFMRecord = null;
		currentNLRecords = null;
		currentNLTemplate = null;
	}

	public String openTemplate(RecordType fromType, List<String> fileNames) throws IOException {
		switch (fromType) {
		case TT_AN_TEMPLATE:
			return openANTemplate(fileNames);
		case TT_ANSI_FM_RECORD:
			return openANSIFMRecord(fileNames);
		case TT_ISO_FM_RECORD:
			return openISOFMRecord(fileNames);
		case TT_NF_RECORD:
			return openNFRecord(fileNames);
		case TT_NF_TEMPLATE:
			return openNFTemplate(fileNames);
		case TT_NL_RECORD:
			return openNLRecord(fileNames);
		case TT_NL_TEMPLATE:
			return openNLTemplate(fileNames);
		case TT_N_TEMPLATE:
			return openNTemplate(fileNames);
		default:
			throw new AssertionError("Can not happen");
		}
	}

	public int[] getDisableArray() {
		return disableArray.clone();
	}

	public String convertAndSave(RecordType fromType, RecordType toType, File file) throws Exception {
		switch (toType) {
		case TT_AN_TEMPLATE:
			return convertToANTemplate(fromType, file);
		case TT_ANSI_FM_RECORD:
			return convertToFMRecord(fromType, file, false);
		case TT_ISO_FM_RECORD:
			return convertToFMRecord(fromType, file, true);
		case TT_NF_RECORD:
			return convertToNFRecord(fromType, file);
		case TT_NF_TEMPLATE:
			return convertToNFTemplate(fromType, file);
		case TT_NL_RECORD:
			return convertToNLRecord(fromType, file);
		case TT_NL_TEMPLATE:
			return convertToNLTemplate(fromType, file);
		case TT_N_TEMPLATE:
			return convertToNTemplate(fromType, file);
		default:
			throw new AssertionError("Can not happen");
		}
	}

	public boolean templateLoaded() {
		return currentANTemplate != null || currentNFRecords != null || currentNFTemplate != null || currentNTemplate != null || currentFMRecord != null || currentNLTemplate != null || currentNLRecords != null;
	}
}
