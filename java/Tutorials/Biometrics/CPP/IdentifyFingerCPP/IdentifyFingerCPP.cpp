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
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;

const NChar title[] = N_T("IdentifyFinger");
const NChar description[] = N_T("Demonstrates fingerprint identification.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [probe image] [one or more gallery gallery images] ... " << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& fileName, const NStringWrapper& subjectId)
{
	NSubject subject;
	subject.SetId(subjectId);
	NFinger finger;
	finger.SetFileName(fileName);
	subject.GetFingers().Add(finger);
	return subject;
}

int main(int argc, NChar * * argv)
{
	const NChar * components = { N_T("Biometrics.FingerExtraction,Biometrics.FingerMatching") };
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
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

		NBiometricClient biometricClient;
		biometricClient.SetMatchingThreshold(48);
		biometricClient.SetFacesMatchingSpeed(nmsLow);

		NSubject probeSubject = CreateSubject(argv[1], "ProbeSubject");
		NBiometricTask enrollTask = biometricClient.CreateTask(nboEnroll,NULL);
		for (int i = 1; i< argc - 1; i++)
		{
			enrollTask.GetSubjects().Add(CreateSubject(argv[i+1], NString::Format ("GallerySubject {I32}", i)));
		}
		biometricClient.PerformTask(enrollTask);
		if (enrollTask.GetStatus() != nbsOk)
		{
			cout << "Enroll unsuccessful" << endl;
			if (enrollTask.GetError() != NULL)
				throw enrollTask.GetError();
			return -1;
		}

		NBiometricStatus status = biometricClient.Identify(probeSubject);
		NSubject::MatchingResultCollection matchingResult =  probeSubject.GetMatchingResults();
		if (status == nbsOk)
		{
			for (int i = 0; i < matchingResult.GetCount(); i++)
			{
				NMatchingResult r = matchingResult.Get(i);
				cout << "Matched with ID: " << r.GetId() << " with score " << r.GetScore() << endl;
			}
		}
		else if (status == nbsMatchNotFound)
		{
			cout << "Match not found" << endl;
		}
		else 
		{
			cout << "Matching failed! Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
