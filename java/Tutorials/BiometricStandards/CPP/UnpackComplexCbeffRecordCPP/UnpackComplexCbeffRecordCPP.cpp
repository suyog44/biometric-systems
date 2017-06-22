#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NBiometricClient/NBiometricClient.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NBiometricClient.hpp>
	#include <NBiometrics.hpp>
	#include <NMedia.hpp>
	#include <NLicensing.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::IO;
using namespace Neurotec::Images;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Standards;

const NChar title[] = N_T("UnpackComplexCbeffRecord");
const NChar description[] = N_T("Unpack Complex CbeffRecord");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

enum BdbFormat {
	bdbfANTemplate = 0x001B8019,
	bdbfFCRecordAnsi = 0x001B0501,
	bdbfFCRecordIso = 0x01010008,
	bdbfFIRecordAnsi = 0x001B0401,
	bdbfFIRecordIso = 0x01010007,
	bdbfFMRecordAnsi = 0x001B0202,
	bdbfFMRecordIso = 0x01010002,
	bdbfIIRecordAnsiPolar = 0x001B0602,
	bdbfIIRecordIsoPolar = 0x0101000B,
	bdbfIIRecordAnsiRectilinear = 0x001B0601,
	bdbfIIRecordIsoRectilinear = 0x01010009
};

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title <<" [CbeffRecord] [PatronFormat]" << endl << endl;
	cout << "\t[CbeffRecord] - filename for CbeffRecord" << endl;
	cout << "\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation)" << endl;
	return 1;
}

static void RecordToFile(const CbeffRecord & record, int recordNumber)
{
	NUInt bdbFormat = record.GetBdbFormat();
	NString recordType;
	switch(bdbFormat)
	{
		case bdbfANTemplate:
			{
				recordType = N_T("ANTemplate");
				break;
			}
		case bdbfFCRecordAnsi:
			{
				recordType = N_T("FCRecordAnsi");
				break;
			}
		case bdbfFCRecordIso:
			{
				recordType = N_T("FCRecordIso");
				break;
			}
		case bdbfFIRecordAnsi:
			{
				recordType = N_T("FIRecordAnsi");
				break;
			}
		case bdbfFIRecordIso:
			{
				recordType = N_T("FIRecordIso");
				break;
			}
		case bdbfFMRecordAnsi:
			{
				recordType = N_T("FMRecordAnsi");
				break;
			}
		case bdbfFMRecordIso:
			{
				recordType = N_T("FMRecordIso");
				break;
			}
		case bdbfIIRecordAnsiPolar:
			{
				recordType = N_T("IIRecordAnsiPolar");
				break;
			}
		case bdbfIIRecordIsoPolar:
			{
				recordType = N_T("IIRecordIsoPolar");
				break;
			}
		case bdbfIIRecordAnsiRectilinear:
			{
				recordType = N_T("IIRecordAnsiRectilinear");
				break;
			}
		case bdbfIIRecordIsoRectilinear:
			{
				recordType = N_T("IIRecordIsoRectilinear");
				break;
			}
		default:
			{
				recordType = N_T("UnknownFormat");
				break;
			}
	}

	NString fileName = NString::Format(N_T("Record{I}_{S}.dat"), recordNumber, recordType.GetBuffer());
	NFile::WriteAllBytes(fileName, record.GetBdbBuffer());
}

static void UnpackRecords(const CbeffRecord & cbeffRecord, int recordNumber)
{
	int recordCount = cbeffRecord.GetRecords().GetCount();
	if (recordCount == 0)
	{
		RecordToFile(cbeffRecord, recordNumber);
	}
	else
	{
		for (int i = 0; i < recordCount; i++)
		{
			UnpackRecords(cbeffRecord.GetRecords().Get(i), recordNumber);
			recordNumber++;
		}
	}
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.Standards.Base");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc != 3)
	{
		OnExit();
		return usage();
	}

	try
	{
		if (!NLicense::ObtainComponents(N_T("/local"), N_T("5000"), components))
		{
			NThrowException(NString::Format(N_T("Could not obtain licenses for components: {S}"), components));
		}

		NBuffer packedCbeffRecord = NFile::ReadAllBytes(argv[1]);
		NUInt patronFormat = NTypes::UInt32Parse(argv[2], N_T("X"));
		CbeffRecord cbeffRecord(packedCbeffRecord, patronFormat);
		int recordNumber = 0;
		UnpackRecords(cbeffRecord, recordNumber);
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
