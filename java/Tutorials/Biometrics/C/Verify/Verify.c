#include <TutorialUtils.h>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.h>
	#include <NBiometricClient/NBiometricClient.h>
	#include <NBiometrics/NBiometrics.h>
	#include <NMedia/NMedia.h>
	#include <NLicensing/NLicensing.h>
#else
	#include <NCore.h>
	#include <NBiometricClient.h>
	#include <NBiometrics.h>
	#include <NMedia.h>
	#include <NLicensing.h>
#endif

const NChar title[] = N_T("Verify");
const NChar description[] = N_T("Demonstrates template verification.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2011-2017 Neurotechnology");

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
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [reference template] [candidate template]\n"), title);
	printf(N_T("\n"));

	return 1;
}

NResult CreateSubject(HNSubject hSubject, const NChar * fileName, HNString subjectId)
{
	HNBuffer hBuffer = NULL;
	NResult result = N_OK;

	// read template
	result = NFileReadAllBytesCN(fileName, &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileReadAllBytesCN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set template for subject
	result = NSubjectSetTemplateBuffer(hSubject, hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectSetTemplateBuffer() failed (result = %d)!"), result);
		goto FINALLY;
	}
	
	// set the id for the subject
	result = NSubjectSetIdN(hSubject, subjectId);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectSetIdN() failed (result = %d)!"), result);
		goto FINALLY;
	}

FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	return result;
}

static NResult PrintMatchingDetails(HNMatchingDetails hMatchingDetails)
{
	NResult nr;
	NBiometricType biometricType;
	NInt i, count, index, score;

	nr = NMatchingDetailsBaseGetBiometricType(hMatchingDetails, &biometricType);
	if (NFailed(nr)) goto FINALLY;

	if ((biometricType & nbtFinger) == nbtFinger)
	{
		HNFMatchingDetails hFingerMatchingDetails = NULL;

		nr = NMatchingDetailsGetFingersScore(hMatchingDetails, &score);
		if (NFailed(nr)) goto FINALLY;
		printf(N_T("    Fingerprint match details:"));
		printf(N_T(" score = %d\n"), score);

		nr = NMatchingDetailsGetFingerCount(hMatchingDetails, &count);
		if (NFailed(nr)) goto FINALLY;
		for (i = 0; i < count; i++)
		{
			nr = NMatchingDetailsGetFingerEx(hMatchingDetails, i, &hFingerMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
			nr = NXMatchingDetailsGetMatchedIndex(hFingerMatchingDetails, &index);
			if (NFailed(nr)) goto FINALLY;
			nr = NMatchingDetailsBaseGetScore(hFingerMatchingDetails, &score);
			if (NFailed(nr)) goto FINALLY;
			printf(N_T("    fingerprint index: %d; score: %d;\n"), index, score);

			nr = NObjectSet(NULL, &hFingerMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
		}
		if(NFailed(nr))
		{
			nr = NObjectSet(NULL, &hFingerMatchingDetails);
			nr = N_E_FAILED;
			goto FINALLY;
		}
	}
	if ((biometricType & nbtFace) == nbtFace)
	{
		HNLMatchingDetails hFaceMatchingDetails = NULL;

		nr = NMatchingDetailsGetFacesScore(hMatchingDetails, &score);
		if (NFailed(nr)) goto FINALLY;
		printf(N_T("    Faces match details:"));
		printf(N_T(" score = %d\n"), score);

		nr = NMatchingDetailsGetFaceCount(hMatchingDetails, &count);
		if (NFailed(nr)) goto FINALLY;
		for (i = 0; i < count; i++)
		{
			nr = NMatchingDetailsGetFaceEx(hMatchingDetails, i, &hFaceMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
			nr = NXMatchingDetailsGetMatchedIndex(hFaceMatchingDetails, &index);
			if (NFailed(nr)) goto FINALLY;
			nr = NMatchingDetailsBaseGetScore(hFaceMatchingDetails, &score);
			if (NFailed(nr)) goto FINALLY;
			printf(N_T("    face index: %d; score: %d;\n"), index, score);

			nr = NObjectSet(NULL, &hFaceMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
		}
		if(NFailed(nr))
		{
			nr = NObjectSet(NULL, &hFaceMatchingDetails);
			nr = N_E_FAILED;
			goto FINALLY;
		}
	}
	if ((biometricType & nbtIris) == nbtIris)
	{
		HNEMatchingDetails hIrisMatchingDetails = NULL;

		nr = NMatchingDetailsGetIrisesScore(hMatchingDetails, &score);
		if (NFailed(nr)) goto FINALLY;
		printf(N_T("    Irises match details:"));
		printf(N_T(" score = %d\n"), score);

		nr = NMatchingDetailsGetIrisCount(hMatchingDetails, &count);
		if (NFailed(nr)) goto FINALLY;
		for (i = 0; i < count; i++)
		{
			nr = NMatchingDetailsGetIrisEx(hMatchingDetails, i, &hIrisMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
			nr = NXMatchingDetailsGetMatchedIndex(hIrisMatchingDetails, &index);
			if (NFailed(nr)) goto FINALLY;
			nr = NMatchingDetailsBaseGetScore(hIrisMatchingDetails, &score);
			if (NFailed(nr)) goto FINALLY;
			printf(N_T("    iris index: %d; score: %d;\n"), index, score);

			nr = NObjectSet(NULL, &hIrisMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
		}
		if(NFailed(nr))
		{
			nr = NObjectSet(NULL, &hIrisMatchingDetails);
			nr = N_E_FAILED;
			goto FINALLY;
		}
	}
	if ((biometricType & nbtPalm) == nbtPalm)
	{
		HNFMatchingDetails hPalmMatchingDetails = NULL;

		nr = NMatchingDetailsGetPalmsScore(hMatchingDetails, &score);
		if (NFailed(nr)) goto FINALLY;
		printf(N_T("    Palms match details:"));
		printf(N_T(" score = %d\n"), score);

		nr = NMatchingDetailsGetPalmCount(hMatchingDetails, &count);
		if (NFailed(nr)) goto FINALLY;
		for (i = 0; i < count; i++)
		{
			nr = NMatchingDetailsGetPalmEx(hMatchingDetails, i, &hPalmMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
			nr = NXMatchingDetailsGetMatchedIndex(hPalmMatchingDetails, &index);
			if (NFailed(nr)) goto FINALLY;
			nr = NMatchingDetailsBaseGetScore(hPalmMatchingDetails, &score);
			if (NFailed(nr)) goto FINALLY;
			printf(N_T("    palm index: %d; score: %d;\n"), index, score);

			nr = NObjectSet(NULL, &hPalmMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
		}
		if(NFailed(nr))
		{
			nr = NObjectSet(NULL, &hPalmMatchingDetails);
			nr = N_E_FAILED;
			goto FINALLY;
		}
	}
	if ((biometricType & nbtVoice) == nbtVoice)
	{
		HNSMatchingDetails hVoiceMatchingDetails = NULL;

		nr = NMatchingDetailsGetVoicesScore(hMatchingDetails, &score);
		if (NFailed(nr)) goto FINALLY;
		printf(N_T("    Voices match details:"));
		printf(N_T(" score = %d\n"), score);

		nr = NMatchingDetailsGetVoiceCount(hMatchingDetails, &count);
		if (NFailed(nr)) goto FINALLY;
		for (i = 0; i < count; i++)
		{
			nr = NMatchingDetailsGetVoiceEx(hMatchingDetails, i, &hVoiceMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
			nr = NXMatchingDetailsGetMatchedIndex(hVoiceMatchingDetails, &index);
			if (NFailed(nr)) goto FINALLY;
			nr = NMatchingDetailsBaseGetScore(hVoiceMatchingDetails, &score);
			if (NFailed(nr)) goto FINALLY;
			printf(N_T("    voice index: %d; score: %d;\n"), index, score);

			nr = NObjectSet(NULL, &hVoiceMatchingDetails);
			if (NFailed(nr)) goto FINALLY;
		}
		if(NFailed(nr))
		{
			nr = NObjectSet(NULL, &hVoiceMatchingDetails);
			nr = N_E_FAILED;
			goto FINALLY;
		}
	}

	nr = N_OK;
FINALLY:
	return nr;
}

int main(int argc, NChar * * argv)
{
	HNSubject hProbeSubject = NULL;
	HNSubject hGallerySubject = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNString hBiometricStatus = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNMatchingResult * hMatchingResults = NULL;
	HNMatchingDetails hMatchingDetails = NULL;
	HNString hMatchId = NULL;
	HNString hSubjectId = NULL;

	NResult result = N_OK;
	NBiometricStatus biometricStatus = nbsNone;
	NInt matchScore = 0;
	NInt resultsCount = 0;
	const NChar * szMatchId = NULL;
	int i;
	const NChar * szBiometricStatus = NULL;
	const NChar * obtainedLicenses[MAX_MATCHING_COMPONENTS];
	NInt obtainedLicensesCount = 0;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	// check the license first
	for (i = 0; i < MAX_MATCHING_COMPONENTS; i++)
	{
		NBool available;
		result = NLicenseObtainComponents(N_T("/local"), N_T("5000"), MatchingComponents[i], &available);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed, result = %d\n"), result);
			goto FINALLY;
		}
		if (available)
		{
			obtainedLicenses[obtainedLicensesCount++] = MatchingComponents[i];
		}
	}

	if (obtainedLicensesCount == 0)
	{
		printf(N_T("Could not obtain any matching license\n"));
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create subject for probe template
	result = NSubjectCreate(&hProbeSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create probe subject id
	result = NStringCreate(N_T("ProbeSubject"), &hSubjectId);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NStringCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set template for probe subject
	result = CreateSubject(hProbeSubject, argv[1], hSubjectId);
	if (NFailed(result))
	{
		PrintErrorMsg(N_T("CreateSubject() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// free unneeded hSubjectId
	result = NStringSet(NULL, &hSubjectId);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create subject for gallery template
	result = NSubjectCreate(&hGallerySubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create gallery subject id
	result = NStringCreate(N_T("GallerySubject"), &hSubjectId);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NStringCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set template for gallery subject
	result = CreateSubject(hGallerySubject, argv[2], hSubjectId);
	if (NFailed(result))
	{
		PrintErrorMsg(N_T("CreateSubject() failed (result = %d)!"), result);
		goto FINALLY;
	}

	{
		NInt matchingThreshold = 48;
		NBool parameter = NTrue;

		// set matching threshold
		result = NObjectSetPropertyP(hBiometricClient, N_T("Matching.Threshold"), N_TYPE_OF(NInt32), naNone, &matchingThreshold, sizeof(matchingThreshold), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetPropertyP() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set matching speed
		result = NObjectSetPropertyP(hBiometricClient, N_T("Matching.WithDetails"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetPropertyP() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// perform verification
	result = NBiometricEngineVerifyOffline(hBiometricClient, hProbeSubject, hGallerySubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineVerifyOffline() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus != nbsOk)
	{
		// retrieve biometric status
		result = NEnumToStringP(N_TYPE_OF(NBiometricStatus), biometricStatus, NULL, &hBiometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEnumToStringP() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NStringGetBuffer(hBiometricStatus, NULL, &szBiometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("verification failed!\n"));
		printf(N_T("biometric status: %s.\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	// retrieve matching results array
	result = NSubjectGetMatchingResults(hProbeSubject, &hMatchingResults, &resultsCount);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetMatchingResults() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// print matching results
	for (i = 0; i < resultsCount; i++)
	{
		result = NMatchingResultGetId(hMatchingResults[i], &hMatchId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMatchingResultGetId() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NStringGetBuffer(hMatchId, NULL, &szMatchId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NMatchingResultGetScore(hMatchingResults[i], &matchScore);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMatchingResultGetScore() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("matched with ID '%s' with score '%d'\n"), szMatchId, matchScore);

		// free unneeded hMatchId
		result = NStringSet(NULL, &hMatchId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve matching details
		result = NMatchingResultGetMatchingDetails(hMatchingResults[i], &hMatchingDetails);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMatchingResultGetMatchingDetails() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = PrintMatchingDetails(hMatchingDetails);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("Error while printing matching details (result = %d)!"), result);
		}

		// free unneeded hMatchingDetails
		result = NObjectSet(NULL, &hMatchingDetails);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hProbeSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hGallerySubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hMatchingDetails);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectUnrefArray(hMatchingResults, resultsCount);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefArray() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hMatchId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hSubjectId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		for (i = 0; i < obtainedLicensesCount; i++)
		{
			result2 = NLicenseReleaseComponents(obtainedLicenses[i]);
			if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
		}
	}

	OnExit();
	return result;
}
