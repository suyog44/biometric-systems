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

const NChar title[] = N_T("VerifyPalm");
const NChar description[] = N_T("Demonstrates palmprint verification.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [reference image] [candidate image]" << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& fileName, const NStringWrapper& subjectId)
{
	NSubject subject;
	NPalm palm;
	palm.SetFileName(fileName);
	subject.SetId(subjectId);
	subject.GetPalms().Add(palm);
	return subject;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.PalmExtraction,Biometrics.PalmMatching");
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
		biometricClient.SetPalmsMatchingSpeed(nmsLow);

		NSubject referenceSubject = CreateSubject(argv[1],argv[1]);
		NSubject candidateSubject = CreateSubject(argv[2],argv[2]);
		NBiometricStatus status = biometricClient.Verify(referenceSubject, candidateSubject);
		if (status == nbsOk || status == nbsMatchNotFound)
		{
			cout << "Image scored " << referenceSubject.GetMatchingResults().Get(0).GetScore() << ", verification..";
			if (status == nbsOk)
			{
				cout << "Succeeded" << endl;
			}
			else
			{
				cout << "Failed" << endl;
			}
		}
		else
		{
			cout << "Verification failed. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
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
