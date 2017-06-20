#include <TutorialUtils.h>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.h>
	#include <NBiometricClient/NBiometricClient.h>
	#include <NBiometrics/NBiometrics.h>
	#include <NMedia/NMedia.h>
	#include <NLicensing/NLicensing.h>
	#include <IO/NFileEnumerator.h>
#else
	#include <NCore.h>
	#include <NBiometricClient.h>
	#include <NBiometrics.h>
	#include <NMedia.h>
	#include <NLicensing.h>
	#include <IO/NFileEnumerator.h>

#endif

const NChar title[] = N_T("DetectFacialFeaturesFromImageStream");
const NChar description[] = N_T("Demonstrates facial features detection from image stream.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2015-2017 Neurotechnology");

static int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [-u url]\n"), title);
	printf(N_T("\t%s [-f filename]\n"), title);
	printf(N_T("\t%s [-d directory]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[-u url] - url to RTSP stream.\n"));
	printf(N_T("\t[-f filename] -  video file containing a face.\n"));
	printf(N_T("\t[-d directory] - directory containing face images.\n"));

	return 1;
}

void PrintNLFeaturePoint(const NChar * szName, NLFeaturePoint * pPoint)
{
	if ((pPoint->Confidence == 0) || (pPoint->Confidence == 254) || (pPoint->Confidence == 255))
	{
		printf(N_T("\t\t%s (feature code %d) feature unavailable.\tConfidence: 0\n"), szName, pPoint->Code);
		return;
	}
	printf(N_T("\t\t%s (feature code %d) feature found. X: %d, Y: %d, confidence: %d\n"), szName, pPoint->Code, pPoint->X, pPoint->Y, pPoint->Confidence);
}

void PrintFaceFeaturePoint(NLFeaturePoint * pPoint)
{
	if (pPoint->Confidence == 0)
	{
		printf(N_T("\t\tface feature point (feature code %d) unavailable. Confidence: 0\n"), pPoint->Code);
		return;
	}
	printf(N_T("\t\tface feature point (feature code %d) found. X: %d, Y: %d, confidence: %d\n"), pPoint->Code, pPoint->X, pPoint->Y, pPoint->Confidence);
}

int main(int argc, NChar **argv)
{
	const NInt MAX_FRAMES = 10;

	const NChar * additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	NChar * components = N_T("Biometrics.FaceDetection");

	HNBiometricClient hBiometricClient = NULL;
	HNSubject hSubject = NULL;
	HNFace hFace = NULL;
	NResult result = N_OK;
	HNMediaReader hReader = NULL;
	HNMediaSource hSource = NULL;
	HNFileEnumerator hEnumerator = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNImage hImage = NULL;
	NBiometricStatus biometricStatus = nbsNone;
	HNString hBiometricStatus = NULL;

	HNString hFileName = NULL;
	HNString hPath = NULL;

	HNLAttributes hLAttributes = NULL;
	NFileAttributes attributes = nfatNone;
	NRect boundingRect;
	NLFeaturePoint noseTipPoint;
	NLFeaturePoint mouthCenterPoint;
	NLFeaturePoint leftEyePoint;
	NLFeaturePoint rightEyePoint;

	NBool additionalObtained = NFalse;
	NBool available = NFalse;
	NBool isReaderUsed = NFalse;
	NBool next = NTrue;

	NInt frameCount = 0;

	const NChar * szBiometricStatus = NULL;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 2)
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
		NTemplateSize templateSize = ntsMedium;
		NBoolean parameter = NTrue;
		NBool hasEx = NFalse;

		// set template size to medium
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
			// set detect base facial features
			result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectBaseFeaturePoints"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}
	}

	if(strcmp(N_T("-f"), argv[1]) == 0)
	{
		result = NMediaSourceCreateFromFile(argv[2], 0, &hSource);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to create media source (result = %d)!"), result);
			goto FINALLY;
		}
	}
	else if(strcmp(N_T("-u"), argv[1])== 0)
	{
		result = NMediaSourceCreateFromUrl(argv[2], 0, &hSource);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to create media source (result = %d)!"), result);
			goto FINALLY;
		}
	}
	else if(strcmp(N_T("-d"), argv[1])== 0)
	{
		result = NFileEnumeratorCreate(argv[2], &hEnumerator);
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
		result = NMediaReaderCreate(hSource, nmtVideo, NTrue, 0, &hReader);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NMediaReaderCreate() failed (result = %d)!"), result);
			goto FINALLY;
		}
		isReaderUsed = NTrue;
	}

	// create detection task
	result = NBiometricEngineCreateTask(hBiometricClient, nboDetectSegments, hSubject, NULL, &hBiometricTask);
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

	while(next)
	{
		frameCount++;
		if(isReaderUsed)
		{
			NLong timespan = -1, duration = 0;
			result = NMediaReaderReadVideoSample(hReader, &timespan, &duration, &hImage);

			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NMediaReaderReadVideoSample() failed (result = %d)!"), result);
				goto FINALLY;
			}

			if (frameCount >= MAX_FRAMES)
			{
				next= NFalse;
			}
		}
		else
		{
			result = NFileEnumeratorGetFileAttributes(hEnumerator, &attributes);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorGetFileAttributes() failed (result = %d)!"), result);
				goto FINALLY;
			}
			if (attributes == nfatDirectory)
			{
				result = NFileEnumeratorMoveNext(hEnumerator, &next);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorMoveNext() failed (result = %d)!"), result);
					goto FINALLY;
				}
				continue;
			}

			result = NFileEnumeratorGetFileName(hEnumerator, &hFileName);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFileEnumeratorGetFileName() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NPathCombineP2N(argv[2], hFileName, &hPath);
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

		result = NBiometricTaskGetStatus(hBiometricTask, &biometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetStatus() failed (result = %d)!"), result);
			goto FINALLY;
		}

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

		printf(N_T(" Biometric status status: %s. Current frame - %d.\n"), szBiometricStatus, frameCount);

		result = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (biometricStatus == nbsOk)
		{
			// retrieve attributes array and the number of faces detected
			result = NFaceGetObject(hFace, 0, &hLAttributes);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NSubjectGetFace() failed (result = %d)!"), result);
				goto FINALLY;
			}

			{
				NBool hasEx = NFalse;

				// retrieve face boundingRect information of the face from attributes array
				result = NLAttributesGetBoundingRect(hLAttributes, &boundingRect);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NLAttributesGetBoundingRect() failed (result = %d)!"), result);
					goto FINALLY;
				}

				printf(N_T("\tface location = (%d, %d), width = %d, height = %d\n"),
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

				PrintNLFeaturePoint(N_T("left eye center"), &leftEyePoint);
				PrintNLFeaturePoint(N_T("right eye center"), &rightEyePoint);

				result = NLicenseIsComponentActivated(additionalComponents, &hasEx);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NLicenseIsComponentActivated() failed (result = %d)!"), result);
					goto FINALLY;
				}

				// retrieve mouth center of the face from attributes array
				result = NLAttributesGetMouthCenter(hLAttributes, &mouthCenterPoint);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NLAttributesGetMouthCenter() failed (result = %d)!"), result);
					goto FINALLY;
				}

				// retrieve nose tip center of the face from attributes array
				result = NLAttributesGetNoseTip(hLAttributes, &noseTipPoint);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NLAttributesGetNoseTip() failed (result = %d)!"), result);
					goto FINALLY;
				}

				PrintNLFeaturePoint(N_T("mouth center"), &mouthCenterPoint);
				PrintNLFeaturePoint(N_T("nose tip"), &noseTipPoint);

				result = NObjectSet(NULL, &hLAttributes);
				if (NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
					goto FINALLY;
				}
			}
		}
		else if (biometricStatus != nbsObjectNotFound)
		{
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
						goto FINALLY;
					}
				}
			}
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

	// reset HasMoreSamples value since we finished loading images
	result = NBiometricSetHasMoreSamples(hFace, NFalse);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetHasMoreSamples() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = N_OK;

FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hLAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hEnumerator);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hReader);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSource);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hPath);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hFileName);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(additionalComponents);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result2 = %d\n"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();

	return result;
}
