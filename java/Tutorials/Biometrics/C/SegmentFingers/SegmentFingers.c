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

const NChar title[] = N_T("SegmentFingers");
const NChar description[] = N_T("Demonstrates fingerprint image segmentation.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2012-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [image] [position] <optional: missing positions> ... \n"), title);
	printf(N_T("\t[image]             - image containing fingerprints\n"));
	printf(N_T("\t[position]          - fingerprints position in provided image\n"));
	printf(N_T("\t[missing positions] - one or more NFPosition value of missing fingers\n\n"));
	printf(N_T("\tvalid positions:\n"));
	printf(N_T("\t\tPlainRightFourFingers = 13, PlainLeftFourFingers = 14, PlainThumbs = 15\n"));
	printf(N_T("\t\tRightThumb = 1, RightIndex = 2, RightMiddle = 3, RightRing = 4, RightLittle = 5\n"));
	printf(N_T("\t\tLeftThumb = 6, LeftIndex = 7, LeftMiddle = 8, LeftRing = 9, LeftLittle = 10\n\n"));
	printf(N_T("\texample: %s image.png 15\n"), title);
	printf(N_T("\texample: %s image.png 13 2 3\n"), title);
	printf(N_T("\n"));

	return 1;
}

int main(int argc, NChar * * argv)
{
	HNBiometricClient hBiometricClient = NULL;
	HNSubject hSubject = NULL;
	HNFinger hFinger = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNImage hImage = NULL;
	HNString hBiometricStatus = NULL;
	HNString hPosition = NULL;
	HNString hFileName = NULL;

	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.FingerSegmentation");
	NBool available = NFalse;
	NBool wrongHand = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	const NChar * szPosition = NULL;
	NFPosition position;
	NInt count = 0;
	NInt i;

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
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed, result = %d\n"), result);
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

	// create the subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create the finger
	result = NFingerCreate(&hFinger);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFingerCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// read and set the image for the finger
	result = NBiometricSetFileName(hFinger, argv[1]);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileNameN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set finger position
	position = (NFPosition)atoi(argv[2]);
	result = NFrictionRidgeSetPosition(hFinger, position);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFrictionRidgeSetPosition() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set the finger for the subject
	result = NSubjectAddFinger(hSubject, hFinger, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddFinger() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set missing finger positions
	for (i = 0; i < argc - 3; i++)
	{
		position = (NFPosition)atoi(argv[i + 3]);
		result = NSubjectAddMissingFinger(hSubject, position, NULL);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectAddMissingFinger() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// create biometric task
	result = NBiometricEngineCreateTask(hBiometricClient, nboSegment | nboCreateTemplate, hSubject, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// perform the biometric task
	result = NBiometricEnginePerformTask(hBiometricClient, hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEnginePerformTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve the status of the biometric task
	result = NBiometricTaskGetStatus(hBiometricTask, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetStatus() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// check if wrong hand is detected
	result = NFingerGetWrongHandWarning(hFinger, &wrongHand);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFingerGetWrongHandWarning() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if(wrongHand == NTrue)
	{
		printf(N_T("warning: possibly wrong hand\n"));
	}

	// free unneeded hFinger
	result = NObjectSet(NULL, &hFinger);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
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

		printf(N_T("failed to segment!\n"));
		printf(N_T("biometric status: %s\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}
	else
	{
		// get finger count
		result = NSubjectGetFingerCount(hSubject, &count);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectGetFingerCount() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("found %d segments\n"), (count - 1));

		for (i = 1; i < count; i++)
		{
			// retrieve the finger
			result = NSubjectGetFinger(hSubject, i, &hFinger);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NSubjectGetFinger() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// retrieve the status of finger
			result = NBiometricGetStatus(hFinger, &biometricStatus);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NBiometricGetStatus() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// retrieve finger position
			result = NFrictionRidgeGetPosition(hFinger, &position);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFrictionRidgeGetPosition() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NEnumToStringP(N_TYPE_OF(NFPosition), position, NULL, &hPosition);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NEnumToStringP() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NStringGetBuffer(hPosition, NULL, &szPosition);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
				goto FINALLY;
			}

			if (biometricStatus == nbsOk)
			{
				printf(N_T("\t%s: "), szPosition);

				// retrieve image
				result = NFrictionRidgeGetImage(hFinger, &hImage);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NFrictionRidgeGetImage() failed (result = %d)!"), result);
					goto FINALLY;
				}

				result = NStringFormat(&hFileName, N_T("{S}.png"), szPosition);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NFrictionRidgeGetImage() failed (result = %d)!"), result);
					goto FINALLY;
				}

				// save image to file
				result = NImageSaveToFileExN(hImage, hFileName, NULL, NULL, 0);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileExN() failed (result = %d)!"), result);
					goto FINALLY;
				}

				// free unneeded hImage
				result = NObjectSet(NULL, &hImage);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
					goto FINALLY;
				}

				// free unneeded hFileName
				result = NStringSet(NULL, &hFileName);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
					goto FINALLY;
				}

				printf(N_T("\timage saved\n"));
			}
			else
			{
				printf(N_T("\t %s: %s\n"), szPosition, szBiometricStatus);
			}

			// free unneeded hFinger
			result = NObjectSet(NULL, &hFinger);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// free unneeded hPosition
			result = NStringSet(NULL, &hPosition);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFinger);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hPosition);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hFileName);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}

