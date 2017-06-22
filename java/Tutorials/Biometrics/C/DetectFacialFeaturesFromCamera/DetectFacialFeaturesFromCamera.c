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

const NChar title[] = N_T("DetectFacialFeaturesFromCamera");
const NChar description[] = N_T("Demonstrates face feature extraction from camera.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2014-2017 Neurotechnology");

static int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [template] [image] [tokenImage]\n"), title);
	printf(N_T("\t%s [template] [image] [tokenImage] [-u url](optional)\n"), title);
	printf(N_T("\t%s [template] [image] [tokenImage] [-f filename](optional)\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[template] - filename to store face template.\n"));
	printf(N_T("\t[image]    - image filename to store face image.\n"));
	printf(N_T("\t[tokenImage]    - image filename to store token face image.\n"));
	printf(N_T("\t[-u url]      - (optional) url to RTSP stream.\n"));
	printf(N_T("\t[-f filename]      - (optional) video file to be connected as camera.\n"));
	printf(N_T("\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used"));
	printf(N_T("\n\nexamples:\n"));
	printf(N_T("\t%s image.jpg template.dat\n"), title);
	printf(N_T("\t%s image.jpg template.dat -u rtsp://172.16.0.161/axis-media/media.amp\n"), title);

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
	const NChar * additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	const NChar * szDeviceName = NULL;
	const NChar * szBiometricStatus = NULL;
	NChar * components = N_T("Biometrics.FaceDetection,Biometrics.FaceExtraction,Devices.Cameras,Biometrics.FaceSegmentsDetection,Biometrics.FaceQualityAssessment");
	HNDevice hDevice = NULL;
	HNImage hImage = NULL;
	HNImage hTokenImage = NULL;
	HNBuffer hBuffer = NULL;
	HNSubject hSubject = NULL;
	HNFace hFace = NULL;
	HNFace hTokenFace = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNDeviceManager hDeviceManager = NULL;
	HNString hDeviceName = NULL;
	HNString hBiometricStatus = NULL;
	HNString hUrl = NULL;
	HNPluginManager hPluginManager = NULL;
	HNPlugin hPlugin = NULL;
	NPluginState pluginState = npsNone;
	HNParameterDescriptor * hParameterDescriptor = NULL;
	HNParameterBag hParameterBag = NULL;
	HNPropertyBag hPropertyBag = NULL;
	HNValue hValue = NULL;
	NBool parameter = NTrue;
	NBool available = NFalse;
	NBool additionalObtained = NFalse;
	NBool isConnectSupported = NFalse;
	NResult result = N_OK;
	NInt deviceCount = 0;
	NInt facesDetected = 0;
	NInt i = 0, j = 0;
	NInt featurePointsCount = 0;
	NInt parametersCount = 0;
	NBiometricStatus biometricStatus = nbsNone;
	NRect boundingRect;
	HNBiometricTask hBiometricTask = NULL;
	HNLAttributes hLAttributes = NULL;
	HNLAttributes * hFacesAttributesArray = NULL;
	NLFeaturePoint * featurePoints = NULL;
	NLFeaturePoint noseTipPoint;
	NLFeaturePoint mouthCenterPoint;
	NLFeaturePoint leftEyePoint;
	NLFeaturePoint rightEyePoint;
	NByte age = 0;
	NByte confidence = 0;
	NByte blinkConfidence = 0;
	NByte mouthOpenConfidence = 0;
	NByte glassesConfidence = 0;
	NByte darkGlassesConfidence = 0;
	NGender gender = ngUnspecified;
	NLExpression expression = nleUnspecified;
	NLProperties properties;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 4 && argc != 6)
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

	printf(N_T("loading cameras ...\n"));

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set biometric client's type to nbtFaces
	result = NBiometricClientSetBiometricTypes(hBiometricClient, nbtFace);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientSetBiometricTypes() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// specify biometric client to use device manager
	result = NBiometricClientSetUseDeviceManager(hBiometricClient, NTrue);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientSetUseDeviceManager() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// initialize biometric client
	result = NBiometricEngineInitialize(hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineInitialize() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve device manager from biometric client
	result = NBiometricClientGetDeviceManager(hBiometricClient, &hDeviceManager);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientGetDeviceManager() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// check if url to RTSP stream is provided
	if (argc == 6)
	{
		// get plugin manager
		result = NDeviceManagerGetPluginManager(&hPluginManager);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetPluginManager() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve "Media" plugin
		result = NPluginManagerGetPluginByName(hPluginManager, N_T("Media"), &hPlugin);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NPluginManagerGetPluginByName() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve plugin state
		result = NPluginGetState(hPlugin, &pluginState);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NPluginGetState() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve is connect to device supported
		result = NDeviceManagerIsConnectToDeviceSupported(hPlugin, &isConnectSupported);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NDeviceManagerIsConnectToDeviceSupported() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (pluginState && isConnectSupported)
		{
			NBool isUrl = strcmp(N_T("-u"), argv[4]) == 0;
			// retrieve parameters
			result = NDeviceManagerGetConnectToDeviceParameters(hPlugin, &hParameterDescriptor, &parametersCount);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetConnectToDeviceParameters() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// create parameter bag
			result = NParameterBagCreate(hParameterDescriptor, parametersCount, &hParameterBag);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetConnectToDeviceParameters() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// convert parameter bag to property bag
			result = NParameterBagToPropertyBag(hParameterBag, &hPropertyBag);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NParameterBagToPropertyBag() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// create the handle of the name
			result = NValueCreateFromString(isUrl ? N_T("IP Camera") : N_T("Video file"), naNone, &hValue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NValueCreateFromString() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// add DisplayName property to property bag
			result = NPropertyBagAdd(hPropertyBag, N_T("DisplayName"), hValue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NPropertyBagAdd() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// free unneeded hValue
			result = NObjectSet(NULL, &hValue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// create handle to url stream
			result = NStringFormat(&hUrl, N_T("{S}"), argv[5]);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NStringFormat() failed (result = %d)!"), result);
				goto FINALLY;
			}

			result = NValueCreateFromStringN(hUrl, naNone, &hValue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NValueCreateFromStringN() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// add Url or FileName property to property bag
			result = NPropertyBagAdd(hPropertyBag, isUrl ? N_T("Url") : N_T("FileName"), hValue);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NPropertyBagAdd() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// connect hPlugin with hPropertyBag to hDeviceManager
			result = NDeviceManagerConnectToDevice(hDeviceManager, hPlugin, hPropertyBag, &hDevice);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NDeviceManagerConnectToDevice() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}
		else
		{
			result = PrintErrorMsgWithLastError(N_T("Wrong plugin state or connection not supported!"), result);
			goto FINALLY;
		}
	}
	else
	{
		// retrieve the number of cameras found
		result = NDeviceManagerGetDeviceCount(hDeviceManager, &deviceCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetDeviceCount() failed (result = %d)!"), result);
			goto FINALLY;
		}
		if (deviceCount == 0)
		{
			printf(N_T("no cameras found, exiting ...\n"));
			result = N_E_FAILED;
			goto FINALLY;
		}

		// get the first camera from hDeviceManager
		result = NDeviceManagerGetDevice(hDeviceManager, 0, &hDevice);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetDevice() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}
	
	// specify the biometric client to use the device selected
	result = NBiometricClientSetFaceCaptureDevice(hBiometricClient, hDevice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientSetFaceCaptureDevice() failed (result = %d)!"), result);
		goto FINALLY;
	}

	{
		NTemplateSize templateSize = ntsLarge;
		
		// set template size to large
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.TemplateSize"), N_TYPE_OF(NTemplateSize), naNone, &templateSize, sizeof(templateSize), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// set detect base features
	result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectBaseFeaturePoints"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (additionalObtained)
	{
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectAllFeaturePoints"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set detect age
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetermineAge"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set detect geneder
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetermineGender"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set recognize expression
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.RecognizeExpression"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set detect blink
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectBlink"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set detect mouth open
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectMouthOpen"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set detect glasses
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectGlasses"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set detect dark glasses
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.DetectDarkGlasses"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set recognize emotion
		result = NObjectSetPropertyP(hBiometricClient, N_T("Faces.RecognizeEmotion"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// get display name
	result = NDeviceGetDisplayNameN(hDevice, &hDeviceName);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NDeviceGetDisplayNameN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = NStringGetBuffer(hDeviceName, NULL, &szDeviceName);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("capturing from %s camera.\n"), szDeviceName);
	printf(N_T("please turn camera to face.\n"));

	// create the subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create the face
	result = NFaceCreate(&hFace);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFaceCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set capture options to nbcoStream
	result = NBiometricSetCaptureOptions(hFace, nbcoStream);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetCaptureOptions() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// add the face to the subject
	result = NSubjectAddFace(hSubject, hFace, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddFace() failed (result = %d)!"), result);
		goto FINALLY;
	}
	// create biometric task to detect faces
	result = NBiometricEngineCreateTask(hBiometricClient, nboCapture | nboDetectSegments | nboSegment | nboAssessQuality, hSubject, NULL, &hBiometricTask);
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

	// retrieve attributes array and the number of faces detected
	result = NFaceGetObjects(hFace, &hFacesAttributesArray, &facesDetected);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetFaces() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (facesDetected < 1) {
		printf(N_T("no faces found\n"));
		goto FINALLY;
	}

	printf(N_T("%d faces found.\n"), facesDetected);

	for (i = 0; i < facesDetected; ++i)
	{
		NBool hasEx = NFalse;

		// retrieve face boundingRect information of the face from attributes array
		result = NLAttributesGetBoundingRect(hFacesAttributesArray[i], &boundingRect);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetBoundingRect() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("\tface location = (%d, %d), width = %d, height = %d\n"),
			boundingRect.X, boundingRect.Y,
			boundingRect.Width, boundingRect.Height);

		printf(N_T("\nface %d:\n"), (i + 1));

		// retrieve left eye center of the face from attributes array
		result = NLAttributesGetLeftEyeCenter(hFacesAttributesArray[i], &leftEyePoint);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetLeftEyeCenter() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve right eye center of the face from attributes array
		result = NLAttributesGetRightEyeCenter(hFacesAttributesArray[i], &rightEyePoint);
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
		result = NLAttributesGetMouthCenter(hFacesAttributesArray[i], &mouthCenterPoint);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetMouthCenter() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve nose tip center of the face from attributes array
		result = NLAttributesGetNoseTip(hFacesAttributesArray[i], &noseTipPoint);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetNoseTip() failed (result = %d)!"), result);
			goto FINALLY;
		}

		
		PrintNLFeaturePoint(N_T("mouth center"), &mouthCenterPoint);
		PrintNLFeaturePoint(N_T("nose tip"), &noseTipPoint);

		// retrieve feature points from from attributes array
		result = NLAttributesGetFeaturePoints(hFacesAttributesArray[i], &featurePoints, &featurePointsCount); 
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetFeaturePoints() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("\n"));
		for(j = 0; j < featurePointsCount; j++)
		{
			PrintFaceFeaturePoint(&featurePoints[j]);
		}

		// retrieve age from attributes array
		result = NLAttributesGetAge(hFacesAttributesArray[i], &age);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetGender() failed (result = %d)!"), result);
			goto FINALLY;
		}
		if (age == 254)
			printf(N_T("\t\tage not detected\n"));
		else
			printf(N_T("\t\tage: %d\n"), age);

		// retrieve gender confidence from attributes array
		result = NLAttributesGetGenderConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetGenderConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve gender from attributes array
		result = NLAttributesGetGender(hFacesAttributesArray[i], &gender);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetGender() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (confidence == 255)
			printf(N_T("\t\tgender not detected\n"));
		else if (confidence == 0)
			printf(N_T("\t\tgender not specified\n"));
		else
			printf(N_T("\t\tgender: %s, confidence: %d\n"), (gender == ngMale ? N_T("male") : N_T("female")), confidence);

		// retrieve expression confidence from attributes array
		result = NLAttributesGetExpressionConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetExpressionConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve expression from attributes array
		result = NLAttributesGetExpression(hFacesAttributesArray[i], &expression);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetExpression() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (confidence == 255)
			printf(N_T("\t\texpression: not detected\n"));
		else if (confidence == 254)
			printf(N_T("\t\texpression: not calculated\n"));
		else if (confidence == 0)
			printf(N_T("\t\texpression: not specified\n"));
		else
		{
			if (expression == nleSmile) {
				printf(N_T("\t\texpression: smile, confidence: %d\n"), confidence);
			}
		}

		// retrieve properties from attributes array
		result = NLAttributesGetProperties(hFacesAttributesArray[i], &properties);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetProperties() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve blink confidence from atributes array
		result = NLAttributesGetBlinkConfidence(hFacesAttributesArray[i], &blinkConfidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetBlinkConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve mouth open confidence from atributes array
		result = NLAttributesGetMouthOpenConfidence(hFacesAttributesArray[i], &mouthOpenConfidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetMouthOpenConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve glasses confidence from atributes array
		result = NLAttributesGetGlassesConfidence(hFacesAttributesArray[i], &glassesConfidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetGlassesConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve dark glasses confidence from atributes array
		result = NLAttributesGetDarkGlassesConfidence(hFacesAttributesArray[i], &darkGlassesConfidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetDarkGlassesConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (blinkConfidence == 255) 
			printf(N_T("\t\tblink is not detected\n"));
		else if (blinkConfidence == 254)
			printf(N_T("\t\tblink is not calculated\n"));
		else 
			printf(N_T("\t\tblink: %s, confidence: %d\n"), (properties & nlpBlink) == nlpBlink ? N_T("yes") : N_T("no"), blinkConfidence);
		
		if (mouthOpenConfidence == 255) 
			printf(N_T("\t\tmouth open is not detected\n"));
		else if (mouthOpenConfidence == 254) 
			printf(N_T("\t\tmouth open is not calculated\n"));
		else 
			printf(N_T("\t\tmouth open: %s, confidence: %d\n"), (properties & nlpMouthOpen) == nlpMouthOpen ? N_T("yes") : N_T("no"), mouthOpenConfidence);
		
		if (glassesConfidence == 255) 
			printf(N_T("\t\tglasses are not detected\n"));
		if (glassesConfidence == 254) 
			printf(N_T("\t\tglasses are not calculated\n"));
		else 
			printf(N_T("\t\tglasses: %s, confidence: %d\n"), (properties & nlpGlasses) == nlpGlasses ? N_T("yes") : N_T("no"), glassesConfidence);
		
		if (darkGlassesConfidence == 255) 
			printf(N_T("\t\tdark glasses are not detected\n"));
		if (darkGlassesConfidence == 254) 
			printf(N_T("\t\tdark glasses are not calculated\n"));
		else 
			printf(N_T("\t\tdark glasses: %s, confidence: %d\n"), (properties & nlpDarkGlasses) == nlpDarkGlasses ? N_T("yes") : N_T("no"), darkGlassesConfidence);

		// retrieve anger confidence
		result = NLAttributesGetEmotionAngerConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionAngerConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\tanger confidence: %d\n"), confidence);

		// retrieve disgust confidence
		result = NLAttributesGetEmotionDisgustConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionDisgustConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\tdisgust confidence: %d\n"), confidence);

		// retrieve fear confidence
		result = NLAttributesGetEmotionFearConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionFearConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\tfear confidence: %d\n"), confidence);

		// retrieve happiness confidence
		result = NLAttributesGetEmotionHappinessConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionHappinessConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\thappiness confidence: %d\n"), confidence);

		// retrieve neutral confidence
		result = NLAttributesGetEmotionNeutralConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionNeutralConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\tneutral confidence: %d\n"), confidence);

		// retrieve sadness confidence
		result = NLAttributesGetEmotionSadnessConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionSadnessConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\tsadness confidence: %d\n"), confidence);

		// retrieve surprise confidence
		result = NLAttributesGetEmotionSurpriseConfidence(hFacesAttributesArray[i], &confidence);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLAttributesGetEmotionSurpriseConfidence() failed (result = %d)!"), result);
			goto FINALLY;
		}
		printf(N_T("\t\tsurprise confidence: %d\n"), confidence);

		if (featurePoints)
		{
			NFree(featurePoints);
			featurePoints = NULL;
		}
	}

	// retrieve template from subject in byte buffer
	result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve the image from the face captured
	result = NFaceGetImage(hFace, &hImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFaceGetImage() failed (result = %d)!"), result);
		goto FINALLY;
	}

		// retrieve the token face from hSubject
	result = NSubjectGetFace(hSubject, 1, &hTokenFace);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetFace() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve token face image
	result = NFaceGetImage(hTokenFace, &hTokenImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFaceGetImage() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// save template to file
	result = NFileWriteAllBytesCN(argv[1], hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileWriteAllBytesCN() failed (result = %d)!"), result);
		goto FINALLY;
	}
	printf(N_T("saved template to file %s\n"), argv[1]);

	// save image to file
	result = NImageSaveToFileEx(hImage, argv[2], NULL, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileEx() failed (result = %d)!"), result);
		goto FINALLY;
	}
	printf(N_T("saved face image to file %s\n"), argv[2]);

	// save token face image to file
	result = NImageSaveToFileEx(hTokenImage, argv[3], NULL, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFile() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("saved token face image to file %s\n"), argv[3]);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hDevice);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hTokenImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hTokenFace);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hDeviceManager);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hPluginManager);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hPlugin);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectUnrefArray(hParameterDescriptor, parametersCount);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefArray() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hParameterBag);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hPropertyBag);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hValue);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
		result2 = NObjectSet(NULL, &hLAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectUnrefArray(hFacesAttributesArray, facesDetected);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectUnrefElements() failed (result = %d)!"), result2);
		if (featurePoints) NFree(featurePoints);
		result = NStringSet(NULL, &hUrl);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result);
		result = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result);
		result = NStringSet(NULL, &hDeviceName);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();

	return result;
}
