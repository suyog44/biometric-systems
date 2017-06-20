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

const NChar title[] = N_T("EnrollFaceFromImageStream");
const NChar description[] = N_T("Demonstrates enrollment from image stream");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2014-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [output template] [-u url]\n"), title);
	printf(N_T("\t%s [output template] [-f filename]\n"), title);
	printf(N_T("\t%s [output template] [-d directory]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[-u url] - url to RTSP stream.\n"));
	printf(N_T("\t[-f filename] -  video file containing a face.\n"));
	printf(N_T("\t[-d directory] - directory containing face images.\n"));
	printf(N_T("\n"));
	printf(N_T("example: %s template.dat -f video.avi\n"), title);
	printf(N_T("example: %s template.dat -u rtsp://camera_url\n"), title);
	printf(N_T("example: %s template.dat -d C:\templates\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNSubject hSubject = NULL;
	HNFace hFace = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBuffer hBuffer = NULL;
	HNLAttributes hLAttributes = NULL;
	HNMediaReader hReader = NULL;
	HNMediaSource hSource = NULL;
	HNFileEnumerator hEnumerator = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNImage hImage = NULL;
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

	HNString hFileName = NULL;
	HNString hPath = NULL;

	NBool next = NTrue;
	NBool isReaderUsed = NFalse;
	NBool wasImageNull = NFalse;
	const NChar * szBiometricStatus = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 4)
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
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileName() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set that face will be captured from image stream
	result = NBiometricSetHasMoreSamples(hFace, NTrue);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetHasMoreSamples() failed (result = %d)!"), result);
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

	if(strcmp(N_T("-f"), argv[2]) == 0)
	{
		result = NMediaSourceCreateFromFile(argv[3], 0, &hSource);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to create media source (result = %d)!"), result);
			goto FINALLY;
		}
	}
	else if(strcmp(N_T("-u"), argv[2])== 0)
	{
		result = NMediaSourceCreateFromUrl(argv[3], 0, &hSource);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to create media source (result = %d)!"), result);
			goto FINALLY;
		}
	}
	else if(strcmp(N_T("-d"), argv[2])== 0)
	{
		result = NFileEnumeratorCreate(argv[3], &hEnumerator);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorCreate() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NFileEnumeratorMoveNext(hEnumerator, &next);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorMoveNext() failed (result = %d)!"), result);
			goto FINALLY;
		}

	}
	else
	{
		result = N_E_ARGUMENT;
		result = PrintErrorMsgWithLastError(N_T("Argument extraction failed (result = %d)!"), result);
		goto FINALLY;
	}

	if(hSource)
	{
		// create media reader
		result = NMediaReaderCreate(hSource, nmtVideo, NFalse, 0, &hReader);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMediaReaderCreate() failed (result = %d)!"), result);
			goto FINALLY;
		}
		isReaderUsed = NTrue;
	}

	// create template creation task
	result = NBiometricEngineCreateTask(hBiometricClient, nboCreateTemplate, hSubject, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if(isReaderUsed)
	{
		// start media reader
		result = NMediaReaderStart(hReader);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMediaReaderStart() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	while (biometricStatus == nbsNone)
	{
		if(isReaderUsed)
		{
			NLong timespan = -1, duration = 0;
			result = NMediaReaderReadVideoSample(hReader, &timespan, &duration, &hImage);

			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NMediaReaderReadVideoSample() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}
		else
		{
			if (!next)
			{
				wasImageNull = NTrue;
				break;
			}

			result = NFileEnumeratorGetFileName(hEnumerator, &hFileName);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorGetFileName() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NPathCombineP2N(argv[3], hFileName, &hPath);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NPathCombineP2N() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NImageCreateFromFileExN(hPath, NULL, 0, NULL, &hImage);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NImageCreateFromFileExN() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NStringSet(NULL, &hFileName);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NStringSet(NULL, &hPath);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
				goto FINALLY;
			}
			
			result = NFileEnumeratorMoveNext(hEnumerator, &next);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorMoveNext() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}

		if (!hImage)
		{
			wasImageNull = NTrue;
			break;
		}

		result = NFaceSetImage(hFace, hImage);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NFaceSetImage() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NBiometricEnginePerformTask(hBiometricClient, hBiometricTask);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricEnginePerformTask() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve the error message
		{
			HNError hError = NULL;
			result = NBiometricTaskGetError(hBiometricTask, &hError);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetError() failed (result = %d)!"), result);
				goto FINALLY;
			}
			if (hError)
			{
				result = PrintErrorMsgWithError(N_T("task error:\n"), hError);
				{
					NResult result2 = NObjectSet(NULL, &hError);
					if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
				}
			}
		}

		result = NBiometricTaskGetStatus(hBiometricTask, &biometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetStatus() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hImage);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	if(isReaderUsed)
	{
		// stop reader
		result = NMediaReaderStop(hReader);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMediaReaderStop() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// reset HasMoreSamples value since we finished extracting images
	result = NBiometricSetHasMoreSamples(hFace, NFalse);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetHasMoreSamples() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// if loading was finished because MeadiaReaded had no more images we have to
	// finalize extraction by performing task after setting HasMoreSamples to false
	if (wasImageNull)
	{
		result = NBiometricEnginePerformTask(hBiometricClient, hBiometricTask);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricEnginePerformTask() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NBiometricTaskGetStatus(hBiometricTask, &biometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetStatus() failed (result = %d)!"), result);
			goto FINALLY;
		}
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

		result = NFileWriteAllBytesCN(argv[1], hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to write template to file (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("template saved successfully\n"));
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

		printf(N_T("template extraction failed!\n"));
		printf(N_T("biometric status = %s.\n\n"), szBiometricStatus);

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

	// retrieve the number of faces detected
	result = NSubjectGetFaceCount(hSubject, &facesDetected);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetFaceCount() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (facesDetected > 0)
	{
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
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hLAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hReader);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSource);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hEnumerator);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hPath);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hFileName);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
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
