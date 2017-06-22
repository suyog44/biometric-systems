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
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Standards;

const NChar title[] = N_T("CbeffRecordToNTemplate");
const NChar description[] = N_T("Converting CbeffRecord to NTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

#define MAX_COMPONENTS 8

static const NChar * Components[MAX_COMPONENTS] =
{
	N_T("Biometrics.Standards.Base"),
	N_T("Biometrics.Standards.Irises"),
	N_T("Biometrics.Standards.Fingers"),
	N_T("Biometrics.Standards.Palms"),
	N_T("Biometrics.IrisExtraction"),
	N_T("Biometrics.FingerExtraction"),
	N_T("Biometrics.FaceExtraction"),
	N_T("Biometrics.PalmExtraction")
};

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [CbeffRecord] [PatronFormat] [NTemplate]" << endl << endl;
	cout << "\t[NTemplate] - filename of NTemplate" << endl;
	cout << "\t[CbeffRecord] - filename for CbeffRecord" << endl;
	cout << "\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation)" << endl;
	return 1;
}

int main(int argc, NChar ** argv)
{
	OnStart(title, description, version, copyright, argc, argv);

	if(argc != 4)
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

		NBuffer packedCbeffRecord = NFile::ReadAllBytes(argv[1]);
		NUInt patronFormat = NTypes::UInt32Parse(argv[2], N_T("X"));
		CbeffRecord cbeffRecord(packedCbeffRecord, patronFormat);
		NSubject subject;
		NBiometricClient client;
		subject.SetTemplate(cbeffRecord);
		NBiometricStatus status = client.CreateTemplate(subject);
		if (status == nbsOk)
		{
			NFile::WriteAllBytes(argv[3], subject.GetTemplate().Save());
			cout << "Template successfully saved";
		}
		else
			cout << "Template creation failed! Satus: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status);

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
