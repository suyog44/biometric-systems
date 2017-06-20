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

const NChar title[] = N_T("EnrollFaceFromFile");
const NChar description[] = N_T("Demonstrates enrollment from file - image or video file.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2006-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [input file] [output template] [still image or video file]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[input file]                - image or video filename with face.\n"));
	printf(N_T("\t[output template]           - filename to store face template.\n"));
	printf(N_T("\t[still image or video file] - specifies that passed source parameter is image (value: 0) or video (value: 1).\n"));
	printf(N_T("\n"));
	printf(N_T("example: %s image.jpg template.dat 0\n"), title);
	printf(N_T("example: %s video.avi template.dat 1\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNSubject hSubject = NULL;
	HNFace hFace = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBuffer hBuffer = NULL;
	HNLAttributes hLAttributes = NULL;
	HNString hBiometricStatus = NULL;

	NResult result = N_OK;
	const NChar * components = { N_T("Biometrics.FaceExtraction") };
	const NChar * additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	NBool additionalObtained = NFalse;
	NBool available = NFalse;
	NInt facesDetected = 0;
	NBiometricStatus biometricStatus = nbsNone;
	NRect boundingRect;
	NLFeaturePoint leftEyePoint;
	NLFeaturePoint rightEyePoint;
	NLFeaturePoint noseTipPoint;
	NLFeaturePoint mouthCenterPoint;
	NBool isVideo = NFalse;
	const NChar * szBiometricStatus = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 4)
	{
		OnExit();
		return usage();
	}

	// check the license first
	result = NLicenseObtainComponents(N_T("/local"), N_T("5000"), components, &available);
	if (NFailed(result))
	{
		goto FINALLY;
	}
	if (!available)
	{
		printf(N_T("Licenses for %s not available\n"), components);
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}
	result = NLicenseObtainComponents(N_T("/local"), N_T("5000"), additionalComponents, &additionalObtained);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed, result = %d\n"), result);
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
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileNameN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	isVideo = atoi(argv[3]);

	// set capture option to nbcoStream or nbcoNone
	if (isVideo)
	{
		result = NBiometricSetCaptureOptions(hFace, nbcoStream);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricSetCaptureOptions() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}
	else {
		result = NBiometricSetCaptureOptions(hFace, nbcoNone);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricSetCaptureOptions() failed (result = %d)!"), result);
			goto FINALLY;
		}
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

	{
		NTemplateSize templateSize = ntsLarge;
		NBoolean parameter = NTrue;
		NBool hasEx = NFalse;

		// set template size to large
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.TemplateSize"), N_TYPE_OF(NTemplateSize), naNone, &templateSize, sizeof(templateSize), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NLicenseIsComponentActivated(additionalComponents, &hasEx);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLicenseIsComponentActivated() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (hasEx)
		{
			// set detect all facial features
			result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectAllFeaturePoints"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}
	}

	// create the template
	result = NBiometricEngineCreateTemplate(hBiometricClient, hSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus == nbsOk)
	{
		printf(N_T("template extracted\n"));

		// retrieve the template from subject
		result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NFileWriteAllBytesCN(argv[2], hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to write template to file (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("template saved successfully\n"));
	}
	else
	{
		// Retrieve biometric status
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

		printf(N_T("template extraction failed!\n"));
		printf(N_T("biometric status = %s.\n\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	// retrieve the number of faces detected
	result = NSubjectGetFaceCount(hSubject, &facesDetected);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetFaceCount() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (facesDetected > 0) {
		// retrieve attributes array from hFace
		result = NFaceGetObject(hFace, 0, &hLAttributes);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NFaceGetObject() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve face boundingRect information of the face from attributes array
		result = NLAttributesGetBoundingRect(hLAttributes, &boundingRect);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetBoundingRect() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("found face:\n"));
		printf(N_T("\tlocation = (%d, %d), width = %d, height = %d\n"),
			boundingRect.X, boundingRect.Y,
			boundingRect.Width, boundingRect.Height);

		// retrieve left eye center of the face from attributes array
		result = NLAttributesGetLeftEyeCenter(hLAttributes, &leftEyePoint);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetLeftEyeCenter() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve right eye center of the face from attributes array
		result = NLAttributesGetRightEyeCenter(hLAttributes, &rightEyePoint);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetRightEyeCenter() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (leftEyePoint.Confidence > 0 || rightEyePoint.Confidence > 0)
		{
			printf(N_T("\tfound eyes:\n"));
			if(rightEyePoint.Confidence > 0)
			{
				printf(N_T("\t\tright: location = (%d, %d), confidence = %d\n"),
					rightEyePoint.X, rightEyePoint.Y,
					rightEyePoint.Confidence);
			}
			if(leftEyePoint.Confidence > 0)
			{
				printf(N_T("\t\tleft: location = (%d, %d), confidence = %d\n"),
					leftEyePoint.X, leftEyePoint.Y,
					leftEyePoint.Confidence);
			}
		}

		{
			NBool hasEx = NFalse;
			result = NLicenseIsComponentActivated(additionalComponents, &hasEx);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NLicenseIsComponentActivated() failed (result = %d)!"), result);
				goto FINALLY;
			}
			if (hasEx)
			{
				// retrieve nose tip center of the face from attributes array
				result = NLAttributesGetNoseTip(hLAttributes, &noseTipPoint);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NLAttributesGetNoseTip() failed (result = %d)!"), result);
					goto FINALLY;
				}

				if(noseTipPoint.Confidence > 0)
				{
					printf(N_T("\tfound nose:\n"));
					printf(N_T("\t\tlocation = (%d, %d), confidence = %d\n"),
						noseTipPoint.X, noseTipPoint.Y,
						noseTipPoint.Confidence);
				}

				// retrieve mouth center of the face from attributes array
				result = NLAttributesGetMouthCenter(hLAttributes, &mouthCenterPoint);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NLAttributesGetMouthCenter() failed (result = %d)!"), result);
					goto FINALLY;
				}

				if(mouthCenterPoint.Confidence > 0)
				{
					printf(N_T("\tfound mouth:\n"));
					printf(N_T("\t\tlocation = (%d, %d), confidence = %d\n"),
						mouthCenterPoint.X, mouthCenterPoint.Y,
						mouthCenterPoint.Confidence);
				}
			}
		}
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hLAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(additionalComponents);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result2 = %d\n"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result2 = %d\n"), result2);
	}

	OnExit();
	return result;
}
