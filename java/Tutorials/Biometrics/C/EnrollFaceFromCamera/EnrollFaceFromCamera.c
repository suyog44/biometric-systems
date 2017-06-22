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

const NChar title[] = N_T("EnrollFaceFromCamera");
const NChar description[] = N_T("Demonstrates face feature extraction from camera.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2011-2017 Neurotechnology");

static int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [image] [template]\n"), title);
	printf(N_T("\t%s [image] [template] [-u url](optional)\n"), title);
	printf(N_T("\t%s [image] [template] [-f filename](optional)\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[image]    - image filename to store face image.\n"));
	printf(N_T("\t[template] - filename to store face template.\n"));
	printf(N_T("\t[-u url]      - (optional) url to RTSP stream.\n"));
	printf(N_T("\t[-f filename]      - (optional) video file to be connected as camera.\n"));
	printf(N_T("\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used"));
	printf(N_T("\n\nexamples:\n"));
	printf(N_T("\t%s image.jpg template.dat\n"), title);
	printf(N_T("\t%s image.jpg template.dat -u rtsp://172.16.0.161/axis-media/media.amp\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNDevice hDevice = NULL;
	HNImage hImage = NULL;
	HNBuffer hBuffer = NULL;
	HNLAttributes hLAttributes = NULL;
	HNSubject hSubject = NULL;
	HNFace hFace = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNDeviceManager hDeviceManager = NULL;
	HNString hDeviceName = NULL;
	HNString hBiometricStatus = NULL;
	HNPluginManager hPluginManager = NULL;
	HNPlugin hPlugin = NULL;
	HNParameterDescriptor * hParameterDescriptor = NULL;
	HNParameterBag hParameterBag = NULL;
	HNPropertyBag hPropertyBag = NULL;
	HNValue hValue = NULL;
	HNString hUrl = NULL;

	const NChar * components = N_T("Biometrics.FaceExtraction,Devices.Cameras");
	NBool available = NFalse;
	const NChar * szDeviceName = NULL;
	const NChar * szBiometricStatus = NULL;
	NResult result = N_OK;
	NInt deviceCount = 0;
	NBiometricStatus biometricStatus = nbsNone;
	NRect boundingRect;
	NLFeaturePoint leftEyePoint;
	NLFeaturePoint rightEyePoint;
	NPluginState pluginState = npsNone;
	NBool isConnectSupported = NFalse;
	NInt parametersCount = 0;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 3 && argc != 5)
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
	if (argc == 5)
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
			NBool isUrl = strcmp(N_T("-u"), argv[3]) == 0;
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
			result = NStringFormat(&hUrl, N_T("{S}"), argv[4]);
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

	// capture the face
	result = NBiometricClientCapture(hBiometricClient, hSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCapture() failed (result = %d)!"), result);
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

		printf(N_T("template extraction failed!"));
		printf(N_T("biometric status: %s\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	// retrieve the image from the face captured
	result = NFaceGetImage(hFace, &hImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFaceGetImage() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve template from subject in byte buffer
	result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("saving image to file %s\n"), argv[1]);
	// save image to file
	result = NImageSaveToFileEx(hImage, argv[1], NULL, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve the attributes from face
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

	// retrieve the left eye center
	result = NLAttributesGetLeftEyeCenter(hLAttributes, &leftEyePoint);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLAttributesGetLeftEyeCenter() failed (result = %d)!"), result);
		goto FINALLY;
	}

	//retrieve the right eye center
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

	// save template to file
	result = NFileWriteAllBytesCN(argv[2], hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileWriteAllBytesCN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("template saved successfully\n"));

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hDevice);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hLAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFace);
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
