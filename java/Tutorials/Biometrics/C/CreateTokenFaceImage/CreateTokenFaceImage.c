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

const NChar title[] = N_T("CreateTokenFaceImage");
const NChar description[] = N_T("Demonstrates creation of token face image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [face_image] [token_face_image]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[face_image]       - an image containing frontal face.\n"));
	printf(N_T("\t[token_face_image] - filename of created token face image.\n"));

	return 1;
}

int main(int argc, NChar *argv[])
{
	HNFace hFace = NULL;
	HNFace hOtherFace = NULL;
	HNSubject hSubject = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNLAttributes hFacesAttributes = NULL;
	HNImage hTokenImage = NULL;
	HNString hBiometricStatus = NULL;

	NResult result = N_OK;
	NBool available = NFalse;
	const NChar * components = N_T("Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssessment");
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	NByte quality;
	NByte sharpness;
	NByte backgroundUniformity;
	NByte grayscaleDensity;

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

	// create subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
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
	result = NBiometricSetFileName(hFace, argv[1]);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileName() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set the face for the subject
	result = NSubjectAddFace(hSubject, hFace, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddFace() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create biometric task to create token face image
	result = NBiometricEngineCreateTask(hBiometricClient, nboSegment | nboAssessQuality, hSubject, NULL, &hBiometricTask);
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

		printf(N_T("failed to execute task!\n"));
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

	// retrieve the token face from hSubject
	result = NSubjectGetFace(hSubject, 1, &hOtherFace);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetFace() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve token face image
	result = NFaceGetImage(hOtherFace, &hTokenImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFaceGetImage() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// save token face image to file
	result = NImageSaveToFileEx(hTokenImage, argv[2], NULL, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFile() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("token face image successfully saved to \"%s\".\n"), argv[2]);

	// retrieve attributes array from hOtherFace
	result = NFaceGetObject(hOtherFace, 0, &hFacesAttributes);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetFaces() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (hFacesAttributes == NULL)
	{
		printf(N_T("token face image's attributes not found\n"));
		goto FINALLY;
	}

	// retrieve quality
	result = NBiometricAttributesGetQuality(hFacesAttributes, &quality);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricAttributesGetQuality() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("global token face image quality score = %d.\n"), quality);

	printf(N_T("attributes:\n"));
	// retrieve sharpness score
	result = NLAttributesGetSharpness(hFacesAttributes, &sharpness);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLAttributesGetSharpness() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("\tsharpness score = %d\n"), sharpness);

	// retrieve background uniformity score
	result = NLAttributesGetBackgroundUniformity(hFacesAttributes, &backgroundUniformity);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLAttributesGetBackgroundUniformity() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("\tbackground uniformity score = %d\n"), backgroundUniformity);

	// retrieve grayscale density score
	result = NLAttributesGetGrayscaleDensity(hFacesAttributes, &grayscaleDensity);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLAttributesGetGrayscaleDensity() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("\tgrayscale density score = %d\n"), grayscaleDensity);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hOtherFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFacesAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hTokenImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();

	return result;
}
