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

const NChar title[] = N_T("MatchMultipleFaces");
const NChar description[] = N_T("Demonstrates matching a face from reference image to multiple faces from other image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [reference_face_image] [multiple_faces_image]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[reference_face_image]  - filename of image with a single (reference) face.\n"));
	printf(N_T("\t[multiple_faces_image]  - filename of image with multiple faces.\n"));

	return 1;
}

NResult CreateSubject(HNSubject hSubject, const NChar * fileName, NBool isMultiple)
{
	HNFace hFace = NULL;
	NResult result = N_OK;

	// set multiple subjects
	result = NSubjectSetMultipleSubjects(hSubject, isMultiple);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectSetMultipleSubjects() failed (result = %d)!"), result);
		goto FINALLY;
	}

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
	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hFace);
		if (NFailed(result)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	return result;
}

int main(int argc, NChar **argv)
{
	HNBiometricClient hBiometricClient = NULL;
	HNSubject hProbeSubject = NULL;
	HNSubject hGallerySubject = NULL;
	HNSubject hCurrentSubject = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNMatchingResult * hMatchingResults = NULL;
	HNString hMatchId = NULL;
	HNString hSubjectId = NULL;
	HNString hBiometricStatus = NULL;

	NBiometricStatus biometricStatus = nbsNone;
	NInt i;
	const NChar * components = { N_T("Biometrics.FaceExtraction,Biometrics.FaceMatching") };
	NBool available = NFalse;
	NResult result = N_OK;
	NInt relatedSubjectsCount = 0;
	NInt resultsCount = 0;
	const NChar * szMatchId = NULL;
	NInt matchScore = 0;
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

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create subject for probe image
	result = NSubjectCreate(&hProbeSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = CreateSubject(hProbeSubject, argv[1], NFalse);
	if (NFailed(result))
	{
		PrintErrorMsg(N_T("CreateSubject() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create subject for gallery image
	result = NSubjectCreate(&hGallerySubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = CreateSubject(hGallerySubject, argv[2], NTrue);
	if (NFailed(result))
	{
		PrintErrorMsg(N_T("CreateSubject() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = NBiometricEngineCreateTemplate(hBiometricClient, hProbeSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
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

		printf(N_T("template creation failed!\n"));
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

	result = NBiometricEngineCreateTemplate(hBiometricClient, hGallerySubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
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

		printf(N_T("template creation failed!\n"));
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

	result = NBiometricEngineCreateTask(hBiometricClient, nboEnroll, NULL, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set id for galler subject
	result = NSubjectSetId(hGallerySubject, N_T("GallerySubject_0"));
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectSetId() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// add gallery subject to biometric task
	result = NBiometricTaskAddSubject(hBiometricTask, hGallerySubject, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricTaskAddSubject() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve related subjects' count
	result = NSubjectGetRelatedSubjectCount(hGallerySubject, &relatedSubjectsCount);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetRelatedSubjectCount() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 0; i < relatedSubjectsCount; i++)
	{
		// retrieve related subject from galler subjects at index i
		result = NSubjectGetRelatedSubject(hGallerySubject, i, &hCurrentSubject);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectGetRelatedSubject() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// create gallery subject id
		result = NStringFormat(&hSubjectId, N_T("GallerySubject_{I32}"), (i + 1));
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringFormat() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set id for gallery subject
		result = NSubjectSetIdN(hCurrentSubject, hSubjectId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectSetIdN() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// add gallery subject to biometric task
		result = NBiometricTaskAddSubject(hBiometricTask, hCurrentSubject, NULL);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricTaskAddSubject() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hCurrentSubject
		result = NObjectSet(NULL, &hCurrentSubject);
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

		printf(N_T("failed to perform biometric task!\n"));
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
		
		// set matching threshold to 48
		result = NObjectSetPropertyP(hBiometricClient, N_T("Matching.Threshold"), N_TYPE_OF(NInt32), naNone, &matchingThreshold, sizeof(matchingThreshold), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetPropertyP() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set matching speed to low
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

		printf(N_T("identification failed!\n"));
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

	// retrieve matching results' array
	result = NSubjectGetMatchingResults(hProbeSubject, &hMatchingResults, &resultsCount);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetMatchingResults() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 0; i < resultsCount; i++)
	{
		// retrieve subject id
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

		// retrieve matching score
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

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hProbeSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hGallerySubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hCurrentSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectUnrefArray(hMatchingResults, resultsCount);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefArray() failed, result = %d\n"), result2);
		result2 = NStringSet(NULL, &hMatchId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed, result = %d\n"), result2);
		result2 = NStringSet(NULL, &hSubjectId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed, result = %d\n"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed, result = %d\n"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
