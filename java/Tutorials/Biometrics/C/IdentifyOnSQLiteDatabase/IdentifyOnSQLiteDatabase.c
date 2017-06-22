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

const NChar title[] = N_T("IdentifyOnSQLiteDatabase");
const NChar description[] = N_T("Demonstrates template identification using SQLite database.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2014-2017 Neurotechnology");

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
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [template] [path to database file]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[template]              - template for identification\n"));
	printf(N_T("\t[path to database file] - path to SQLite database file\n"));

	return 1;
}

NResult CreateSubject(HNSubject hSubject, const NChar * fileName)
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
	result = NSubjectSetId(hSubject, fileName);
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

int main(int argc, NChar * * argv)
{
	HNBiometricClient hBiometricClient = NULL;
	HNSubject hSubject = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNString hSubjectId = NULL;
	HNString hBiometricStatus = NULL;
	HNSQLiteBiometricConnection hSQLiteBiometricConnection = NULL;
	HNMatchingResult * hMatchingResults = NULL;

	const NChar * obtainedLicenses[MAX_MATCHING_COMPONENTS];
	NInt obtainedLicensesCount = 0;

	NResult result = N_OK;
	int i;
	NInt resultsCount = 0;
	NInt score = 0;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	const NChar * szSubjectId = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

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

	// create subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set template for subject
	result = CreateSubject(hSubject, argv[1]);
	if (NFailed(result))
	{
		PrintErrorMsg(N_T("CreateSubject() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set SQLite database
	result = NBiometricClientSetDatabaseConnectionToSQLite(hBiometricClient, argv[2], &hSQLiteBiometricConnection);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientSetDatabaseConnectionToSQLite() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create identification task
	result = NBiometricEngineCreateTask(hBiometricClient, nboIdentify, hSubject, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
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

	// retrieve matching results
	result = NSubjectGetMatchingResults(hSubject, &hMatchingResults, &resultsCount);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetMatchingResults() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 0; i < resultsCount; i++)
	{
		// free hSubjectId
		result = NStringSet(NULL, &hSubjectId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve id
		result = NMatchingResultGetId(hMatchingResults[i], &hSubjectId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMatchingResultGetId() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NStringGetBuffer(hSubjectId, NULL, &szSubjectId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve matching score
		result = NMatchingResultGetScore(hMatchingResults[i], &score);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMatchingResultGetScore() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("matched with ID: '%s' with score %d\n"), szSubjectId, score);
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectUnrefArray(hMatchingResults, resultsCount);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefArray() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSQLiteBiometricConnection);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hSubjectId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
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
