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

const NChar title[] = N_T("MatchMultipleFaces");
const NChar description[] = N_T("Demonstrates matching a face from reference image to multiple faces from other image.");
const NChar version[] = N_T("6.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [reference_face_image] [multiple_faces_image]" << endl << endl;
	cout << "\t[reference_face_image]  - filename of image with a single (reference) face." << endl;
	cout << "\t[multiple_faces_image]  - filename of image with multiple faces." << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& fileName, const NStringWrapper& subjectId, bool isMultiple)
{
	NSubject subject;
	subject.SetMultipleSubjects(isMultiple);
	subject.SetId(subjectId);
	NFace face;
	face.SetFileName(fileName);
	subject.GetFaces().Add(face);
	return subject;
}

int main(int argc, NChar **argv)
{
	const NChar * components = { N_T("Biometrics.FaceExtraction,Biometrics.FaceMatching") };
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

		NSubject referenceSubject = CreateSubject(argv[1], argv[1], false);
		NSubject candidateSubject = CreateSubject(argv[2], argv[2], true);
		NBiometricClient biometricClient;
		NBiometricStatus status = biometricClient.CreateTemplate(referenceSubject);
		if (status != nbsOk)
		{
			cout << "Template creation was unsuccessful. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}
		status = biometricClient.CreateTemplate(candidateSubject);
		if (status != nbsOk)
		{
			cout << "Template creation was unsuccessful. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}

		NBiometricTask enrollTask = biometricClient.CreateTask(nboEnroll, NULL);
		candidateSubject.SetId(NString::Format(N_T("GallerySubject 0")));
		enrollTask.GetSubjects().Add(candidateSubject);
		NSubject relatedSubject;
		for (int i=0 ; i < candidateSubject.GetRelatedSubjects().GetCount(); i++)
		{
			relatedSubject = candidateSubject.GetRelatedSubjects().Get(i);
			relatedSubject.SetId(NString::Format(N_T("GallerySubject {I}"), i + 1));
			enrollTask.GetSubjects().Add(relatedSubject);
		}
		biometricClient.PerformTask(enrollTask);
		status = enrollTask.GetStatus();
		if (status != nbsOk)
		{
			cout << "Enrollment was unsuccessful. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}
		biometricClient.SetMatchingThreshold(48);
		biometricClient.SetFacesMatchingSpeed(nmsLow);
		status = biometricClient.Identify(referenceSubject);
		if (status == nbsOk)
		{
			NSubject::MatchingResultCollection matchingResult = referenceSubject.GetMatchingResults();
			for (int i=0; i < matchingResult.GetCount(); i++)
			{
				NMatchingResult r(matchingResult.Get(i));
				cout << "Matched with ID: " << r.GetId() << " with score: " << r.GetScore() << endl;
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
