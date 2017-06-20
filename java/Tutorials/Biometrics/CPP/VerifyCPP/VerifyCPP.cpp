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

const NChar title[] = N_T("Verify");
const NChar description[] = N_T("Demonstrates template verification.");
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

static int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [reference template] [candidate template]" << endl;
	return 1;
}

static NSubject CreateSubject(const NStringWrapper& fileName, const NStringWrapper& subjectId)
{
	NSubject subject;
	subject.SetTemplateBuffer(NFile::ReadAllBytes(fileName));
	subject.SetId(subjectId);
	return subject;
}

static NString MatchingDetailsToString(NMatchingDetails details) 
{
	NStringBuilder builder;
	NBiometricType biometricType = details.GetBiometricType();
	if (biometricType == nbtFinger)
	{
		builder.Append(N_T("\nFingerprint match details:"));
		builder.AppendFormat(N_T("score = {I}\n"), details.GetFingersScore());
		for (int i = 0; i < details.GetFingers().GetCount(); i++)
		{
			NFMatchingDetails fingerDetails = details.GetFingers().Get(i);
			builder.AppendFormat(N_T("Fingerprint: {I} and score: {I}\n"), fingerDetails.GetMatchedIndex(), fingerDetails.GetScore());
		}
	}

	if (biometricType == nbtFace)
	{
		builder.Append(N_T("\nFace match details:"));
		builder.AppendFormat(N_T("score = {I}\n"), details.GetFacesScore());
		for (int i = 0; i < details.GetFaces().GetCount(); i++)
		{
			NLMatchingDetails faceDetails = details.GetFaces().Get(i);
			builder.AppendFormat(N_T("Face index: {I} and score: {I}\n"), faceDetails.GetMatchedIndex(), faceDetails.GetScore());
		}
	}

	if (biometricType == nbtIris)
	{
		builder.Append(N_T("\nIrises match details:"));
		builder.AppendFormat(N_T("score = {I}\n"), details.GetIrisesScore());
		for (int i = 0; i < details.GetIrises().GetCount(); i++)
		{
			NEMatchingDetails irisesDetails = details.GetIrises().Get(i);
			builder.AppendFormat(N_T("Irises index: {I} and score: {I}\n"), irisesDetails.GetMatchedIndex(), irisesDetails.GetScore());
		}
	}

	if (biometricType == nbtPalm)
	{
		builder.Append(N_T("\nPalmprint match details:"));
		builder.AppendFormat(N_T("score = {I}\n"), details.GetPalmsScore());
		for (int i = 0; i < details.GetPalms().GetCount(); i++)
		{
			NFMatchingDetails palmDetails = details.GetPalms().Get(i);
			builder.AppendFormat(N_T("Palmprint index: {I} and score: {I}\n"), palmDetails.GetMatchedIndex(), palmDetails.GetScore());
		}
	}

	if (biometricType == nbtVoice)
	{
		builder.Append(N_T("\nVoice match details: "));
		builder.AppendFormat(N_T("score = {I}\n"), details.GetVoicesScore());
		for (int i = 0; i < details.GetVoices().GetCount(); i++)
		{
			NSMatchingDetails voiceDetails = details.GetVoices().Get(i);
			builder.AppendFormat(N_T("Voices index: {I} and score: {I}\n"), voiceDetails.GetMatchedIndex(), voiceDetails.GetScore());
		}
	}
	return builder.ToString();
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
			NThrowNotActivatedException("\nCould not obtain any matching license");
		}

		NBiometricClient biometricClient;
		biometricClient.SetMatchingThreshold(48);
		biometricClient.SetMatchingWithDetails(true);
		NSubject probeSubject = CreateSubject(argv[1], argv[1]);
		NSubject candidateSubject = CreateSubject(argv[2], argv[2]);
		NBiometricStatus status = biometricClient.Verify(probeSubject, candidateSubject);
		if (status == nbsOk)
		{
			NSubject::MatchingResultCollection matchingResult = probeSubject.GetMatchingResults();
			for (int i = 0; i < matchingResult.GetCount(); i++)
			{
				NMatchingResult r = matchingResult.Get(i);
				cout << "Matched with ID: " << r.GetId() << " with score " << r.GetScore() << endl;
				cout << MatchingDetailsToString(r.GetMatchingDetails()) << endl;
			}
		}
		else if (status == nbsMatchNotFound)
		{
			cout << "MatchNotFound" << endl;
		}
		else
		{
			cout << "Verification failed. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
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
