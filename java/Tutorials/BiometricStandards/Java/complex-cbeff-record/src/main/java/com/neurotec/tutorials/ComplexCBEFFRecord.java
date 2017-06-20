package com.neurotec.tutorials;

import com.neurotec.biometrics.standards.ANTemplate;
import com.neurotec.biometrics.standards.ANValidationLevel;
import com.neurotec.biometrics.standards.BDIFStandard;
import com.neurotec.biometrics.standards.CBEFFRecord;
import com.neurotec.biometrics.standards.FCRecord;
import com.neurotec.biometrics.standards.FIRecord;
import com.neurotec.biometrics.standards.FMRecord;
import com.neurotec.biometrics.standards.IIRecord;
import com.neurotec.io.NBuffer;
import com.neurotec.io.NFile;
import com.neurotec.licensing.NLicense;
import com.neurotec.tutorials.util.LibraryManager;
import com.neurotec.tutorials.util.Utils;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public final class ComplexCBEFFRecord {

	private static final String DESCRIPTION = "Creating a complex CbeffRecord";
	private static final String NAME = "complex-cbeff-record";
	private static final String VERSION = "9.0.0.0";

	private static final List<String> LICENSES = Arrays.asList(
			"Biometrics.Standards.Base",
			"Biometrics.Standards.Irises",
			"Biometrics.Standards.Fingers",
			"Biometrics.Standards.Faces",
			"Biometrics.Standards.Palms"
			);

	private enum RecordType{
		ANTEMPLATE,
		FCRECORD,
		FIRECORD,
		FMRECORD,
		IIRECORD
	}

	private static class RecordInformation {
		public String recordFile;
		public BDIFStandard standard;
		public RecordType recordType;
		public int patronFormat;
	}

	private static void usage() {
		System.out.println("usage:");
		System.out.format("\t%s [ComplexCbeffRecord] [PatronFormat] [[Record] [RecordType] [RecordStandard] [PatronFormat]] ...", NAME);
		System.out.println();
		System.out.println("\t[ComplexCbeffRecord] - filename of CbeffRecord which will be created.");
		System.out.println("\t[PatronFormat] - number identifying root record patron format (all supported values can be found in CbeffRecord class documentation)."); ;
		System.out.println("\t[[Record] [RecordType] [RecordStandard] [PatronFormat]] - record information. Block can be specified more than once.");
		System.out.println("\t\t[Record] - filename containing the record.");
		System.out.println("\t\t[RecordType] - one of record type values(ANTEMPLATE, FCRECORD, FIRECORD, FMRECORD, IIRECORD)");
		System.out.println("\t\t[RecordStandard] - one of record standard values(ANSI, ISO or UNSPECIFIED if ANTemplate type is used).");
		System.out.println("\t\t[PatronFormat] - number identifying patron format.");
		System.out.println();
	}

	public static void main(String[] args) {
		LibraryManager.initLibraryPath();
		Utils.printTutorialHeader(DESCRIPTION, NAME, VERSION, args);

		if (args.length < 6 || (args.length - 2) % 4 != 0) {
			usage();
			System.exit(1);
		}

		RecordInformation[] recordInformation;
		List<String> obtainedLicenses = new ArrayList<String>();
		try {
			recordInformation = parseArguments(args);

			for (String license : LICENSES) {
				if (NLicense.obtainComponents("/local", 5000, license)) {
					obtainedLicenses.add(license);
				}
			}
			if (obtainedLicenses.size() == 0) {
				System.err.println("Could not obtain licenses");
				System.exit(-1);
			}

			// Create root CbeffRecord
			int patronFormat = Integer.parseInt(args[1], 16);
			CBEFFRecord rootRecord = new CBEFFRecord(patronFormat);

			for (RecordInformation info : recordInformation) {
				CBEFFRecord cbeffRecord;
				NBuffer buffer = NFile.readAllBytes(info.recordFile);

				// Create a record object according information specified in arguments
				switch (info.recordType) {
				case ANTEMPLATE:
					ANTemplate anTemplate = new ANTemplate(buffer, ANValidationLevel.STANDARD);
					cbeffRecord = new CBEFFRecord(anTemplate, info.patronFormat);
					break;
				case FCRECORD:
					FCRecord fcRecord = new FCRecord(buffer, info.standard);
					cbeffRecord = new CBEFFRecord(fcRecord, info.patronFormat);
					break;
				case FIRECORD:
					FIRecord fiRecord = new FIRecord(buffer, info.standard);
					cbeffRecord = new CBEFFRecord(fiRecord, info.patronFormat);
					break;
				case FMRECORD:
					FMRecord fmRecord = new FMRecord(buffer, info.standard);
					cbeffRecord = new CBEFFRecord(fmRecord, info.patronFormat);
					break;
				case IIRECORD:
					IIRecord iiRecord = new IIRecord(buffer, info.standard);
					cbeffRecord = new CBEFFRecord(iiRecord, info.patronFormat);
					break;
				default:
					throw new AssertionError("Not recognised record format");
				}

				rootRecord.getRecords().add(cbeffRecord);
			}

			NFile.writeAllBytes(args[0], rootRecord.save());
			System.out.println("Record sucessfully saved");
		} catch (Throwable th) {
			Utils.handleError(th);
		} finally {
			for(String license : obtainedLicenses) {
				try {
					NLicense.releaseComponents(license);
				} catch (IOException e) {
					Utils.handleError(e);
				}
			}
		}

	}

	private static RecordInformation[] parseArguments(String[] args) {
		List<RecordInformation> infoList = new ArrayList<RecordInformation>() ;

		for(int i = 2; i < args.length; i += 4) {
			RecordInformation recInfo = new RecordInformation();
			recInfo.recordFile = args[i];
			recInfo.recordType = RecordType.valueOf(args[i + 1]);
			recInfo.standard = BDIFStandard.valueOf(args[i+2]);
			recInfo.patronFormat = Integer.parseInt(args[i+3], 16);
			infoList.add(recInfo);
		}
		return infoList.toArray(new RecordInformation[infoList.size()]);
	}

}
