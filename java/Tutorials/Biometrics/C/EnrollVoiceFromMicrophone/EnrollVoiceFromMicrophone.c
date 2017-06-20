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

const NChar title[] = N_T("EnrollVoiceFromMicrophone");
const NChar description[] = N_T("Demonstrates voice feature extraction from microphone.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2011-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [template] [audio]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[template] - filename to store voice template.\n"));
	printf(N_T("\t[audio] - filename to store voice audio.\n"));
	printf(N_T("\n\nexample:\n"));
	printf(N_T("\t%s template.dat audio.wav\n"), title);
	
	return 1;
}

int main(int argc, NChar **argv)
{
	HNSubject hSubject = NULL;
	HNVoice hVoice = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNBuffer hBuffer = NULL;
	HNSoundBuffer hSoundBuffer = NULL;
	HNDeviceManager hDeviceManager = NULL;
	HNDevice hDevice = NULL;
	HNString hBiometricStatus = NULL;
	HNString hDeviceName = NULL;

	NResult result;
	const NChar * components = N_T("Devices.Microphones,Biometrics.VoiceExtraction");
	NBool available = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;

	const NChar * szDeviceName = NULL;
	NInt deviceCount;
	NInt i;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 3)
	{
		OnExit();
		return usage();
	}

	// check the license first
	result = NLicenseObtainComponents(N_T("/local"), N_T("5000"), components, &available);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (!available)
	{
		printf(N_T("Licenses for %s not available\n"), components);
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}

	printf(N_T("loading microphones ...\n"));

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set biometric client's type to nbtVoice
	result = NBiometricClientSetBiometricTypes(hBiometricClient, nbtVoice);
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

	// retrieve the number of microphones found
	result = NDeviceManagerGetDeviceCount(hDeviceManager, &deviceCount);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NDeviceManagerGetDeviceCount() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (deviceCount > 0)
		printf(N_T("found %d microphone(s)\n"), deviceCount);
	else
	{
		printf(N_T("no microphones found, exiting ...\n"));
		result = N_E_FAILED;
		goto FINALLY;
	}

	if (deviceCount > 1)
		printf(N_T("please select microphone from the list: \n"));

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
		printf(N_T("please enter microphone index: "));
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
	result = NBiometricClientSetVoiceCaptureDevice(hBiometricClient, hDevice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientSetVoiceCaptureDevice() failed (result = %d)!"), result);
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

	// create subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create voice for the subject
	result = NVoiceCreate(&hVoice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NVoiceCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set capture options to nbcoStream
	result = NBiometricSetCaptureOptions(hVoice, nbcoStream);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetCaptureOptions() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// add the voice to the subject
	result = NSubjectAddVoice(hSubject, hVoice, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddVoice() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// free unneeded hVoice
	result = NObjectSet(NULL, &hVoice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create biometric task to capture the voice
	result = NBiometricEngineCreateTask(hBiometricClient, nboCapture | nboSegment, hSubject, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("recording from %s microphone.\n"), szDeviceName);

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

	// retrieve template from subject in byte buffer
	result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// save template to file
	result = NFileWriteAllBytesCN(argv[1], hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileWriteAllBytesCN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("template saved!\n"));

	// retrieve the sound buffer captured
	result = NSubjectGetVoice(hSubject, 1, &hVoice);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetVoice() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = NVoiceGetSoundBuffer(hVoice, &hSoundBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NVoiceGetSoundBuffer() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// save sound buffer to file
	result = NSoundBufferSaveToFile(hSoundBuffer, argv[2], NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSoundBufferSaveToFile() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hVoice);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSoundBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hDeviceManager);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hDevice);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hDeviceName);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
