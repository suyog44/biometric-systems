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

const NChar title[] = N_T("EnrollFingerFromScanner");
const NChar description[] = N_T("Demonstrates fingerprint feature extraction from fingerprint scanning device.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

static int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [image] [template]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[image]    - image filename to store scanned image.\n"));
	printf(N_T("\t[template] - filename to store finger template.\n"));
	printf(N_T("\n\nexample:\n"));
	printf(N_T("\t%s image.jpg template.dat\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNDevice hDevice = NULL;
	HNImage hImage = NULL;
	HNBuffer hBuffer = NULL;
	HNSubject hSubject = NULL;
	HNFinger hFinger = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNDeviceManager hDeviceManager = NULL;
	HNString hDeviceName = NULL;
	HNString hBiometricStatus = NULL;

	const NChar * components = N_T("Biometrics.FingerExtraction,Devices.FingerScanners");
	NBool available = NFalse;
	const NChar * szDeviceName = NULL;
	const NChar * szBiometricStatus = NULL;
	NResult result = N_OK;
	NInt deviceCount = 0;
	NInt i = 0;
	NBiometricStatus biometricStatus = nbsNone;

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

	printf(N_T("loading scanners ...\n"));

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set biometric client's type to nbtFinger
	result = NBiometricClientSetBiometricTypes(hBiometricClient, nbtFinger);
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

	// retrieve the number of scanners found
	result = NDeviceManagerGetDeviceCount(hDeviceManager, &deviceCount);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetDeviceCount() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (deviceCount > 0)
		printf(N_T("found %d scanner(s)\n"), deviceCount);
	else
	{
		printf(N_T("no scanners found, exiting ...\n"));
		result = N_E_FAILED;
		goto FINALLY;
	}

	if (deviceCount > 1)
		printf(N_T("please select scanner from the list: \n"));

	for (i = 0; i < deviceCount; i++)
	{
		// get the hDevice from hDeviceManager at i index
		result = NDeviceManagerGetDevice(hDeviceManager, i, &hDevice);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetDevice() failed (result = %d)!"), result);
			goto FINALLY;
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

		printf(N_T("%d) %s\n"), i + 1, szDeviceName);

		// free unneeded hDevice
		result = NObjectSet(NULL, &hDevice);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hDeviceName
		result = NStringSet(NULL, &hDeviceName);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	if (deviceCount > 1)
	{
		printf(N_T("please enter scanner index: "));
		scanf(N_T("%d"), &i);
		if (i > deviceCount || i < 1)
		{
			printf(N_T("incorrect index provided ..."));
			result = N_E_FAILED;
			goto FINALLY;
		}
	}
	i -= 1;

	// get the device selected 
	result = NDeviceManagerGetDevice(hDeviceManager, i, &hDevice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetDevice() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// specify the biometric client to use the device selected
	result = NBiometricClientSetFingerScanner(hBiometricClient, hDevice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientSetFingerScanner() failed (result = %d)!"), result);
		goto FINALLY;
	}

	{
		NTemplateSize templateSize = ntsLarge;
		
		// set template size to large
		result = NObjectSetPropertyP(hBiometricClient, N_T("Fingers.TemplateSize"), N_TYPE_OF(NTemplateSize), naNone, &templateSize, sizeof(templateSize), 1, NTrue);
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

	printf(N_T("capturing from %s scanner.\n"), szDeviceName);
	printf(N_T("please put the finger on the scanner.\n"));

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

	// add the finger to the subject
	result = NSubjectAddFinger(hSubject, hFinger, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddFinger() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// scan the finger
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

		printf(N_T("fingerprint capture failed!\n"));
		printf(N_T("biometric status: %s\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	// retrieve the image from the finger scanned
	result = NFrictionRidgeGetImage(hFinger, &hImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFrictionRidgeGetImage() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("\nsaving image to file %s\n"), argv[1]);
	// save image to file
	result = NImageSaveToFileEx(hImage, argv[1], NULL, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create template
	result = NBiometricEngineCreateTemplate(hBiometricClient, hSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
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

		printf(N_T("template extraction failed!\n"));
		printf(N_T("biometric status: %s\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	// retrieve template from subject in byte buffer
	result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
		goto FINALLY;
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
		result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFinger);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hDeviceManager);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result);
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
