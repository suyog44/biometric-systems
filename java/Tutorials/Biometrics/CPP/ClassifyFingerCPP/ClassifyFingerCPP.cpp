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

const NChar title[] = N_T("ClassifyFinger");
const NChar description[] = N_T("Demonstrates fingerprint classification.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image]" << endl << endl;
	cout << "\timage - image of fingerprint to be classified." << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& fileName, NFPosition fingerPosition)
{
	NFinger finger;
	finger.SetFileName(fileName);
	finger.SetPosition(fingerPosition);
	NSubject subject;
	subject.SetId(fileName);
	subject.GetFingers().Add(finger);
	return subject;
}

int main(int argc, NChar * argv[])
{
	const NChar * components = N_T("Biometrics.FingerSegmentsDetection");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
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
		biometricClient.SetFingersDeterminePatternClass(true);

		NSubject subject = CreateSubject(argv[1], nfpUnknown);
		NBiometricTask task = biometricClient.CreateTask(nboDetectSegments, subject);
		biometricClient.PerformTask(task);
		NBiometricStatus status = task.GetStatus();
		if (status == nbsOk)
		{
			NFinger finger = subject.GetFingers().Get(0);
			cout << "Fingerprint pattern class is " << NEnum::ToString(NBiometricTypes::NFPatternClassNativeTypeOf(), finger.GetObjects().Get(0).GetPatternClass()) << ", confidence " << finger.GetObjects().Get(0).GetPatternClassConfidence() << endl;
		}
		else
		{
			cout << "Classification failed. Status " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			if (task.GetError() != NULL)
				throw task.GetError();
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
