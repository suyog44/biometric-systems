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

const NChar title[] = N_T("CreateTokenFaceImage");
const NChar description[] = N_T("Demonstrates creation of token face image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [face_image] [token_face_image]" << endl << endl;
	cout << "\t[face_image]       - an image containing frontal face." << endl;
	cout << "\t[token_face_image] - filename of created token face image." << endl;
	return 1;
}

int main(int argc, NChar *argv[])
{
	const NChar * components = N_T("Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment");
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

		NBiometricClient biometricCLient;
		NSubject subject;
		NFace face;
		face.SetFileName(argv[1]);
		subject.GetFaces().Add(face);
		NBiometricTask task = biometricCLient.CreateTask((NBiometricOperations)(nboSegment | nboAssessQuality), subject);
		biometricCLient.PerformTask(task);
		NBiometricStatus status = task.GetStatus();
		if (status == nbsOk)
		{
			NLAttributes attributes = (subject.GetFaces().Get(1)).GetObjects().Get(0);
			cout << "Global token face image quality score = " << attributes.GetQuality() << " Tested attributes details:" << endl;
			cout << "Sharpness score = " << attributes.GetSharpness() << endl;
			cout << "Background uniformity score = " << attributes.GetBackgroundUniformity() << endl;
			cout << "GreyScale density score = " << attributes.GetGrayscaleDensity() << endl;
			(subject.GetFaces().Get(1)).GetImage().Save(argv[2]);
		}
		else
		{
			cout << "Token face Image creation failed! Status = " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), task.GetStatus()) << endl;
			if (task.GetError() != NULL)
				throw task.GetError();
			return -1;
		}
	}

	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
