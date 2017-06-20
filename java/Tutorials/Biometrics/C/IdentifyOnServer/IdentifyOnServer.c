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

const NChar title[] = N_T("IdentifyOnServer");
const NChar description[] = N_T("Demonstrates template identification on server.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2014-2017 Neurotechnology");

const NChar * defaultServerIp = N_T("127.0.0.1");
const NInt defaultAdminPort = 24932;
const NInt defaultPort = 25452;

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [template] [server] [port]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[template] - template to be sent for enrollment (required)\n"));
	printf(N_T("\t[server]   - matching server address (optional parameter, default address is %s)\n"), defaultServerIp);
	printf(N_T("\t[port]     - matching server port (optional parameter, default port is %d)\n"), defaultAdminPort);

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

	// set id for the subject
	result = NSubjectSetId(hSubject, fileName);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectSetId() failed (result = %d)!"), result);
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
	HNClusterBiometricConnection hClusterBiometricConnection = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNString hBiometricStatus = NULL;
	HNString hHost = NULL;
	HNString hSubjectId = NULL;
	HNMatchingResult * hMatchingResults = NULL;

	NResult result = N_OK;
	int i;
	NInt resultsCount = 0;
	NInt score = 0;
	NClusterAddress clusterAddress;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	const NChar * szSubjectId = NULL;
	const NChar * serverIp = defaultServerIp;
	NInt adminPort = defaultAdminPort;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 2)
	{
		OnExit();
		return usage();
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

	// create connection to server
	result = NClusterBiometricConnectionCreate(&hClusterBiometricConnection);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NClusterBiometricConnectionCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (argc == 3)
		serverIp = argv[2];

	if (argc == 4)
	{
		serverIp = argv[2];
		adminPort = atoi(argv[3]);
	}

	result = NStringCreate(serverIp, &hHost);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NStringCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = NClusterAddressCreateN(hHost, defaultPort, adminPort, &clusterAddress);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NClusterAddressCreateN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = NClusterBiometricConnectionAddAddress(hClusterBiometricConnection, &clusterAddress, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NClusterBiometricConnectionAddAddress() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// add connection to client
	result = NBiometricClientAddRemoteConnection(hBiometricClient, hClusterBiometricConnection, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientAddRemoteConnection() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create enrollment task
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

	// get the status of the task
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

		printf(N_T("identification on server failed!\n"));
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
		result2 = NObjectSet(NULL, &hClusterBiometricConnection);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hSubjectId);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NObjectUnrefArray(hMatchingResults, resultsCount);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefArray() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hHost);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NClusterAddressDispose(&clusterAddress);
		if (NFailed(result2)) PrintErrorMsg(N_T("NClusterAddressDispose() failed (result = %d)!"), result2);
	}

	OnExit();
	return result;
}
