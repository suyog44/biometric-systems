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

#include <vector>

using namespace std;
using namespace Neurotec;
using namespace Neurotec::IO;
using namespace Neurotec::Images;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Standards;

enum RecordType {
	rtANTemplate,
	rtFCRecord,
	rtFIRecord,
	rtFMRecord,
	rtIIRecord
};

struct RecordInfo {
	NChar * recordFile;
	RecordType recordType;
	BdifStandard standard;
	NUInt patronFormat;
};

const NChar title[] = N_T("ComplexCbeffRecord");
const NChar description[] = N_T("Creating a complex CbeffRecord");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

#define MAX_COMPONENTS 5

static const NChar * Components[MAX_COMPONENTS] =
{
	N_T("Biometrics.Standards.Base"),
	N_T("Biometrics.Standards.Irises"),
	N_T("Biometrics.Standards.Faces"),
	N_T("Biometrics.Standards.Fingers"),
	N_T("Biometrics.Standards.Palms")
};

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [ComplexCbeffRecord] [PatronFormat] [[Record] [RecordType] [RecordStandard] [PatronFormat]] ..." << endl << endl;
	cout << "\t[ComplexCbeffRecord] - filename of CbeffRecord which will be created" << endl;
	cout << "\t[PatronFormat] - hex number identifying root record patron format (all supported values can be found in CbeffRecord class documentation)" << endl;
	cout << "\t[[Record] [RecordType] [RecordStandard] [PatronFormat]] - record information. Block can be specified more than once" << endl;
	cout << "\t\t[Record] - filename containing the record." << endl;
	cout << "\t\t[RecordType] - number indicating record type(0 - ANTemplate, 1 - FCRecord, 2 - FIRecord, 3 - FMRecord, 4 - IIRecord)" << endl;
	cout << "\t\t[RecordStandard] - number indicating record standard value(0 - Iso, 1 - Ansi or -1 - Unspecified if ANTemplate type is used)" << endl;
	cout << "\t\t[PatronFormat] - hex number identifying patron format" << endl;
	return 1;
}

static std::vector<RecordInfo> ParseArgs(NChar ** argv, int argc)
{
	std::vector<RecordInfo> infoList;
	for (int i = 3; i < argc; i += 4)
	{
		RecordInfo recInfo;
		recInfo.recordFile = argv[i];
		recInfo.recordType = (RecordType)atoi(argv[i + 1]);
		recInfo.standard = (BdifStandard)atoi(argv[i + 2]);
		recInfo.patronFormat = NTypes::UInt32Parse(argv[i + 3], N_T("X"));
		infoList.push_back(recInfo);
	}
	return infoList;
}

int main(int argc, NChar ** argv)
{
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 6 || (argc - 3) % 4 != 0)
	{
		OnExit();
		return usage();
	}

	std::vector<NString> obtainedComponents;
	try
	{
		for (int i = 0; i < MAX_COMPONENTS; i++)
		{
			const NChar * szComponent = Components[i];
			if (NLicense::ObtainComponents(N_T("/local"), N_T("5000"), szComponent))
			{
				cout << "Obtained license for component: " << szComponent << endl;
				obtainedComponents.push_back(szComponent);
			}
		}
		if (obtainedComponents.empty())
		{
			NThrowNotActivatedException("Could not obtain any matching license");
		}

		std::vector<RecordInfo> recordInfo = ParseArgs(argv, argc);
		NUInt patronFormat = NTypes::UInt32Parse(argv[2], N_T("X"));
		CbeffRecord rootRecord(patronFormat);

		for (std::vector<RecordInfo>::iterator it = recordInfo.begin(); it != recordInfo.end(); it++)
		{
			CbeffRecord cbeffRecord = NULL;
			NBuffer buffer = NFile::ReadAllBytes(it->recordFile);

			switch (it->recordType)
			{
				case rtANTemplate:
					{
						ANTemplate anTemplate(buffer, anvlStandard);
						cbeffRecord = CbeffRecord(anTemplate, it->patronFormat);
						break;
					}
				case rtFCRecord:
					{
						FCRecord fcRecord(buffer, it->standard);
						cbeffRecord = CbeffRecord(fcRecord, it->patronFormat);
						break;
					}
				case rtFIRecord:
					{
						FIRecord fiRecord(buffer, it->standard);
						cbeffRecord = CbeffRecord(fiRecord, it->patronFormat);
						break;
					}
				case rtFMRecord:
					{
						FMRecord fmRecord(buffer, it->standard);
						cbeffRecord = CbeffRecord(fmRecord, it->patronFormat);
						break;
					}
				case rtIIRecord:
					{
						IIRecord iiRecord(buffer, it->standard);
						cbeffRecord = CbeffRecord(iiRecord, it->patronFormat);
					}
				break;
			}
			rootRecord.GetRecords().Add(cbeffRecord);
		}

		NFile::WriteAllBytes(argv[1], rootRecord.Save());
		cout << "Record sucssfully saved" << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	for (std::vector<NString>::iterator it = obtainedComponents.begin(); it != obtainedComponents.end(); it++)
	{
		NLicense::ReleaseComponents(*it);
	}
	OnExit();
	return 0;
}
