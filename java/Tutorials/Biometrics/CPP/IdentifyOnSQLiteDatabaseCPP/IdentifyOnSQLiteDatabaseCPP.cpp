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
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::IO;
using namespace Neurotec::Text;

const NChar title[] = N_T("IdentifyOnSQLiteDatabase");
const NChar description[] = N_T("Demonstrates template identification using SQLite database.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

#define MAX_MATCHING_COMPONENTS 5

static const NChar * MatchingComponents[MAX_MATCHING_COMPONENTS] =
{
	N_T("Biometrics.FingerMatching"),
	N_T("Biometrics.FaceMatching"),
	N_T("Biometrics.IrisMatching"),
	N_T("Biometrics.PalmMatching"),
	N_T("Biometrics.VoiceMatching"),
};

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [template] [path to database file]" << endl << endl;
	cout << "\t[template]              - template for identification" << endl;
	cout << "\t[path to database file] - path to SQLite database file" << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& fileName, const NStringWrapper& subjectId)
{
	NSubject subject;
	subject.SetTemplateBuffer(NFile::ReadAllBytes(fileName));
	subject.SetId(subjectId);
	return subject;
}

int main(int argc, NChar * * argv)
{
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	std::vector<NString> obtainedComponents;
	try
	{
		for (int i = 0; i < MAX_MATCHING_COMPONENTS; i++)
		{
			const NChar * szComponent = MatchingComponents[i];
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

		NBiometricClient biometricClient;
		biometricClient.SetDatabaseConnectionToSQLite(argv[2]);
		NSubject subject = CreateSubject(argv[1],argv[1]);
		NBiometricTask task = biometricClient.CreateTask(nboIdentify, subject);
		biometricClient.PerformTask(task);
		if (task.GetStatus() != nbsOk)
		{
			cout << "Identification was unsuccessful. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), task.GetStatus()) << endl;
			if (task.GetError() != NULL)
				throw task.GetError();
			return -1;
		}
		for (int i = 0; i < subject.GetMatchingResults().GetCount(); i++)
		{
			NMatchingResult matchingResult = subject.GetMatchingResults().Get(i);
			cout << "Matched with ID: " << matchingResult.GetId() << " with score " << matchingResult.GetScore() << endl;
		}
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
