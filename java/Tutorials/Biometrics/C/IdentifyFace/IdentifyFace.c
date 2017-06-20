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

const NChar title[] = N_T("IdentifyFace");
const NChar description[] = N_T("Demonstrates facial identification (matching of template extracted from image to gallery of serialized templates).");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2006-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [probe image] [gallery images]\n"), title);
	printf(N_T("\n"));
	
	return 1;
}

NResult CreateSubject(HNSubject hSubject, const NChar * fileName, HNString subjectId)
{
	HNFace hFace = NULL;
	NResult result = N_OK;

	// create face for the subject
	result = NFaceCreate(&hFace);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFaceCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// read and set the image for the face
	result = NBiometricSetFileName(hFace, fileName);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileNameN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set the face for the subject
	result = NSubjectAddFace(hSubject, hFace, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddFace() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set the id for the subject
	result = NSubjectSetIdN(hSubject, subjectId);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectSetIdN() failed (result = %d)!"), result);
		goto FINALLY;
	}
	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	return result;
}

int main(int argc, NChar **argv)
{
	HNSubject hProbeSubject = NULL;
	HNSubject hGallerySubject = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNString hBiometricStatus = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNMatchingResult * hMatchingResults = NULL;
	HNString hMatchId = NULL;
	HNString hSubjectId = NULL;

	const NChar * components = { N_T("Biometrics.FaceExtraction,Biometrics.FaceMatching") };
	NBool available = NFalse;
	NResult result = N_OK;
	NBiometricStatus biometricStatus = nbsNone;
	NInt matchScore = 0;
	NInt resultsCount = 0;
	const NChar * szMatchId = NULL;
	int i;
	const NChar * szBiometricStatus = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	// check the license first
	result = NLicenseObtainComponents(N_T("/local"), N_T("5000"), components, &available);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (!available)
	{
		printf(N_T("Licenses for %s not available\n"), components);
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}

	// create subject for probe image
	result = NSubjectCreate(&hProbeSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create probe subject id
	result = NStringCreate(N_T("ProbeSubject"), &hSubjectId);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NStringCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// extract template from the image for probe subject
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

	// create biometric task to enroll
	result = NBiometricEngineCreateTask(hBiometricClient, nboEnroll, NULL, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 0; i < (argc - 2); i++)
	{
		// create subject for gallery images
		result = NSubjectCreate(&hGallerySubject);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// create gallery subject id
		result = NStringFormat(&hSubjectId, N_T("GallerySubject_{I32}"), i);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringFormat() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// extract template from the image for gallery subject
		result = CreateSubject(hGallerySubject, argv[i + 2], hSubjectId);
		if (NFailed(result))
		{
			PrintErrorMsg(N_T("CreateSubject() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// add subject to biometric task
		result = NBiometricTaskAddSubject(hBiometricTask, hGallerySubject, NULL);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricTaskAddSubject() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hGallerySubject
		result = NObjectSet(NULL, &hGallerySubject);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hSubjectId
		result = NStringSet(NULL, &hSubjectId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// perform biometric task
	result = NBiometricEnginePerformTask(hBiometricClient, hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEnginePerformTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve biometric task's status
	result = NBiometricTaskGetStatus(hBiometricTask, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetStatus() failed (result = %d)!"), result);
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

		printf(N_T("enroll task failed!\n"));
		printf(N_T("biometric status: %s.\n"), szBiometricStatus);

		// retrieve the error message
		{
			HNError hError = NULL;
			result = NBiometricTaskGetError(hBiometricTask, &hError);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetError() failed (result = %d)!"), result);
				goto FINALLY;
			}
			result = N_E_FAILED;
			if (hError)
			{
				result = PrintErrorMsgWithError(N_T("task error:\n"), hError);
				{
					NResult result2 = NObjectSet(NULL, &hError);
					if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
				}
			}
		}
		goto FINALLY;
	}

	{
		NInt matchingThreshold = 48;
		NMatchingSpeed matchingSpeed = nmsLow;

		// set matching threshold
		result = NObjectSetPropertyP(hBiometricClient, N_T("Matching.Threshold"), N_TYPE_OF(NInt32), naNone, &matchingThreshold, sizeof(matchingThreshold), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetPropertyP() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set matching speed
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.MatchingSpeed"), N_TYPE_OF(NMatchingSpeed), naNone, &matchingSpeed, sizeof(matchingSpeed), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetPropertyP() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// perform identification
	result = NBiometricEngineIdentify(hBiometricClient, hProbeSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineIdentify() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus == nbsOk)
	{
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
		}
	}
	else
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

		printf(N_T("identification failed!\n"));
		printf(N_T("biometric status: %s.\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
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
		result2 = NObjectUnrefArray(hMatchingResults, resultsCount);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefArray() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hMatchId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hSubjectId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
