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

const NChar title[] = N_T("SegmentIris");
const NChar description[] = N_T("Demonstrates iris segmenter.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [input image] [output image]\n"), title);
	printf(N_T("\n"));

	return 1;
}

int main(int argc, NChar **argv)
{
	HNBiometricClient hBiometricClient = NULL;
	HNSubject hSubject = NULL;
	HNIris hIris = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNImage hImage = NULL;
	HNEAttributes * hIrisesAttributesArray = NULL;
	HNString hBiometricStatus = NULL;
	HNString hPosition = NULL;
	HNString hFileName = NULL;

	NResult result = N_OK;
	const NChar *components = N_T("Biometrics.IrisExtraction,Biometrics.IrisSegmentation");
	NBool available = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	NInt irisesDetected = 0;
	NInt i;
	NByte value = 0;

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

	// create the iris
	result = NIrisCreate(&hIris);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NIrisCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// read and set the image for the iris
	result = NBiometricSetFileName(hIris, argv[1]);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileNameN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set image type
	result = NIrisSetImageType(hIris, neitCroppedAndMasked);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NIrisSetImageType() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set the iris for the subject
	result = NSubjectAddIris(hSubject, hIris, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddIris() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// free unneeded hIris
	result = NObjectSet(NULL, &hIris);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create biometric task
	result = NBiometricEngineCreateTask(hBiometricClient, nboSegment, hSubject, NULL, &hBiometricTask);
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

	// retrieve iris at index 0
	result = NSubjectGetIris(hSubject, 0, &hIris);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetIris() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve attributes array and the number of irises detected
	result = NIrisGetObjects(hIris, &hIrisesAttributesArray, &irisesDetected);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NIrisGetObjects() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// print iris attributes
	for (i = 0; i < irisesDetected; i++)
	{
		result = NBiometricAttributesGetQuality(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricAttributesGetQuality() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("overall quality\t\t\t%d\n"), value);

		result = NEAttributesGetGrayScaleUtilisation(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetGrayScaleUtilisation() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("GrayScaleUtilisation\t\t%d\n"), value);

		result = NEAttributesGetInterlace(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetInterlace() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("Interlace\t\t\t%d\n"), value);

		result = NEAttributesGetIrisPupilConcentricity(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetIrisPupilConcentricity() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("IrisPupilConcentricity\t\t%d\n"), value);

		result = NEAttributesGetIrisPupilContrast(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetIrisPupilContrast() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("IrisPupilContrast\t\t%d\n"), value);

		result = NEAttributesGetIrisRadius(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetIrisRadius() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("IrisRadius\t\t\t%d\n"), value);

		result = NEAttributesGetIrisScleraContrast(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetIrisScleraContrast() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("IrisScleraContrast\t\t%d\n"), value);

		result = NEAttributesGetMarginAdequacy(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetMarginAdequacy() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("MarginAdequacy\t\t\t%d\n"), value);

		result = NEAttributesGetPupilBoundaryCircularity(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetPupilBoundaryCircularity() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("PupilBoundaryCircularity\t%d\n"), value);

		result = NEAttributesGetPupilToIrisRatio(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetPupilToIrisRatio() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("PupilToIrisRatio\t\t%d\n"), value);

		result = NEAttributesGetSharpness(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetSharpness() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("Sharpness\t\t\t%d\n"), value);

		result = NEAttributesGetUsableIrisArea(hIrisesAttributesArray[i], &value);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEAttributesGetUsableIrisArea() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("UsableIrisArea\t\t\t%d\n"), value);
	}

	// free unneeded hIris
	result = NObjectSet(NULL, &hIris);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve iris at index 1
	result = NSubjectGetIris(hSubject, 1, &hIris);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetIris() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve iris image
	result = NIrisGetImage(hIris, &hImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NIrisGetImage() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("\nsaving image to file %s\n"), argv[2]);
	// save image to file
	result = NImageSaveToFileEx(hImage, argv[2], NULL, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hIris);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectUnrefArray(hIrisesAttributesArray, irisesDetected);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefElements() failed (result = %d)!"), result2);
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
