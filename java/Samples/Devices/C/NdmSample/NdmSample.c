////////////////////////////////////////////////////////////////////////////////////////////////////
// This is example of plugin for Neurotechnology NDeviceManager.
// The plugin provides 4 devices (microphone, camera, fingerprint and iris scanner). To make this
// example short and hardware independent, the plugin doesn't use physical devices. Instead the
// device input is read from the files.
//
// The sample and the NDeviceManager plugin interface is documented in developers guide (please see
// the "Samples->Devices->NdmSample" section).
//
// The source code exploration can be started from following functions:
//   * NdmSampleModuleCreate: registers plugin in the framework and is called by the code at the end of the file
//   * NdmSampleGetSupportedDeviceTypes: lists the device types that plugin will provide
//   * NdmSamplePlug/NdmSampleUnplug: activates/deactivates the plugin
////////////////////////////////////////////////////////////////////////////////////////////////////

#include <Plugins/NPluginModuleInternal.h>
#include <Devices/NdmInterface.h>
#include <Threading/NMonitor.h>
#include <Threading/NSyncEvent.h>
#include <Threading/NThread.h>
#include <IO/NPath.h>
#include <Media/NMediaReader.h>
#include <Media/NMedia.h>
#include <Media/Processing/NMediaProc.h>
#include <NBiometrics.h>
#include <NDevices.h>
#include <stdlib.h>
#include <string.h>

NResult N_API NdmSampleModuleOf(HNModule * phValue);

/*
 * Plugin specific device information (descriptor).
 * Any needed bookkeeping information. Can contain handles, and other resources needed to work with device and plugin synchronisation.
 */
typedef struct NdmSampleDevice_
{
	// General stuff
	NDeviceType deviceType;
	const NChar * szId;
	const NChar * szDisplayName;
	HNString hFileName1;
	HNString hFileName2;
	NMonitor monitor;
	NBool monitorInitialized;
	NBool isCapturing;
	void * pParam;
	// Capture device specific stuff
	HNMediaReader hMediaReader;
	NdmIsCaptureDeviceCapturingChangedProc pIsCaptureDeviceCapturingChanged;
	// Biometric device specific stuff
	NBool cancelCapture;
	HNSyncEvent hCaptureFinishedEvent;
} NdmSampleDevice;

/*
 * Auxiliary function used by NdmSampleStopCaptureDeviceCapturing.
 * It is also called for ndtCaptureDevice during removal (from NdmSampleDeviceFree).
 * The function notifies the framework of capture finish, to make sure that API user is also notified.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to StartCaptureDeviceCapturing, otherwise the device usages through different device managers will interfere. 
 */
static NResult NdmSampleDeviceStopCapturing(NdmSampleDevice * pDevice, void * pParam)
{
N_TRY
	NBool wasCapturing = NFalse;

	if (!pDevice) N_ERROR_ARGUMENT_NULL_P(pDevice);

	if (pDevice->isCapturing && pDevice->pParam == pParam)
	{
		N_CHECK(NMediaReaderStop(pDevice->hMediaReader));
	}
	wasCapturing = pDevice->isCapturing;
	pDevice->isCapturing = NFalse;
N_FINALLY
	if (wasCapturing)
	{
		N_CHECK(pDevice->pIsCaptureDeviceCapturingChanged((NHandle)pDevice, pDevice->pParam));
		pDevice->pIsCaptureDeviceCapturingChanged = NULL;
		pDevice->pParam = NULL;
	}
N_TRY_END
}

/*
 * Auxiliary function used by NdmSampleCancelBiometricDevice.
 * It is also called for ndtBiometricDevice during removal (from NdmSampleDeviceFree).
 * The function waits until the capturing is actually stopped.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to CaptureFScanner/CaptureIrisScanner, otherwise the device usages through different device managers will interfere. 
 */
static NResult NdmSampleDeviceCancel(NdmSampleDevice * pDevice, void * pParam)
{
N_TRY
	if (!pDevice) N_ERROR_ARGUMENT_NULL_P(pDevice);

	if (pDevice->isCapturing && pDevice->pParam == pParam)
	{
		pDevice->cancelCapture = NTrue;
		N_CHECK(NWaitObjectWaitFor(pDevice->hCaptureFinishedEvent));
	}
N_TRY_E
}

/*
 * Auxiliary function used to release resources allocated by device. 
 * The function also makes sure that device's operations in progress are stopped.
 */
static NResult NdmSampleDeviceFree(NdmSampleDevice * pDevice)
{
N_TRY
	NBool exitMonitor = NFalse;

	if (!pDevice) N_ERROR_ARGUMENT_NULL_P(pDevice);

	if (pDevice->monitorInitialized)
	{
		if (pDevice->deviceType == ndtMicrophone || pDevice->deviceType == ndtCamera)
		{
			NMonitorEnter(&pDevice->monitor); exitMonitor = NTrue;
			N_CHECK(NdmSampleDeviceStopCapturing(pDevice, pDevice->pParam));
			NMonitorExit(&pDevice->monitor); exitMonitor = NFalse;
		}
		else
		{
			N_CHECK(NdmSampleDeviceCancel(pDevice, pDevice->pParam));
		}
		NMonitorDispose(&pDevice->monitor);
	}
	if (pDevice->hCaptureFinishedEvent) N_CHECK(NObjectSet(NULL, &pDevice->hCaptureFinishedEvent));
	if (pDevice->hMediaReader) N_CHECK(NObjectSet(NULL, &pDevice->hMediaReader));
	NStringFree(pDevice->hFileName2);
	NStringFree(pDevice->hFileName1);
	NFree(pDevice); pDevice = NULL;
N_FINALLY
	if (exitMonitor && pDevice) NMonitorExit(&pDevice->monitor);
N_TRY_END
}

/*
 * Auxiliary function.
 * Creates the device descriptor according to given parameters.
 */
static NResult NdmSampleDeviceCreate(NDeviceType deviceType, const NChar * szId, const NChar * szDisplayName,
	const NChar * szDirectory, const NChar * szFileName1, const NChar * szFileName2, NdmSampleDevice * * ppDevice)
{
N_TRY
	NdmSampleDevice * pDevice = NULL;
	if (!ppDevice) N_ERROR_ARGUMENT_NULL_P(ppDevice);
	N_CHECK(NCAlloc(sizeof(NdmSampleDevice), (void **)&pDevice));
	pDevice->deviceType = deviceType;
	pDevice->szId = szId;
	pDevice->szDisplayName = szDisplayName;
	if (szFileName1)
	{
		N_CHECK(NPathCombine(szDirectory, szFileName1, &pDevice->hFileName1));
	}
	if (szFileName2)
	{
		N_CHECK(NPathCombine(szDirectory, szFileName2, &pDevice->hFileName2));
	}
	NMonitorInit(&pDevice->monitor); pDevice->monitorInitialized = NTrue;
	N_CHECK(NSyncEventCreate(NTrue, NTrue, &pDevice->hCaptureFinishedEvent));
	*ppDevice = pDevice;
N_FINALLY
	if (N_WAS_ERROR)
	{
		if (*ppDevice) *ppDevice = NULL;
		if (pDevice) NdmSampleDeviceFree(pDevice);
	}
N_TRY_END
}

/*
 * Auxiliary function.
 * Initializes fake ndtCaptureDevice for capturing data from media file.
 */
static NResult NdmSampleDeviceInitReader(NdmSampleDevice * pDevice)
{
N_TRY
	if (!pDevice) N_ERROR_ARGUMENT_NULL_P(pDevice);
	if (!pDevice->hMediaReader)
	{
		N_CHECK(NMediaReaderCreateFromFileN(pDevice->hFileName1, pDevice->deviceType == ndtMicrophone ? nmtAudio : nmtVideo, NTrue, 0, &pDevice->hMediaReader));
	}
N_TRY_E
}

static NMonitor ndmSampleMonitor;
static NBool ndmSampleMonitorInitialized = NFalse;
static NBool ndmSampleIsPlugged = NFalse;
static NVersion ndmSampleInterfaceVersion = 0;
static HNPlugin hNdmSamplePlugin = NULL;
static const NdmHostInterfaceV1 * pNdmSampleHostInterfaceV1 = NULL;
static NdmSampleDevice * * arpNdmSampleDevices = NULL;
static NInt ndmSampleDeviceCount = 0;

/*
 * Implementation of interfaceV1.GetSupportedDeviceTypes function.
 * The function is used by the framework to determine what type of the devices can be supported by plugin, and hence affect the subset of the interface function that will be required.
 */
static NResult N_API NdmSampleGetSupportedDeviceTypes(NDeviceType * pValue)
{
N_TRY
	if (!pValue) N_ERROR_ARGUMENT_NULL_P(pValue);
	*pValue = ndtMicrophone | ndtCamera | ndtFingerScanner | ndtIrisScanner;
N_TRY_E
}

/*
 * Implementation of interfaceV1.UpdateDeviceList function.
 * The function will be invoked periodically by device managers asking plugin to recheck the list of connected devices.
 * The function can be omitted if plugin knows better when to trigger device update cycle.
 * The function should register new and remove disconected devices from the framework (see the TODO: comments below).
 * The function should be called from NdmSamplePlug to perform initial device enumeration.
 */
static NResult N_API NdmSampleUpdateDeviceList(void)
{
N_TRY
	NBool exitMonitor = NFalse;
	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	if (!ndmSampleIsPlugged) N_ERROR_INVALID_OPERATION_M(N_T("NdmSample is not plugged"));
	// TODO: Add device reenumeration code here, create devices that are added, add them to arpNdmSampleDevices
	// TODO: Call pNdmSampleHostInterfaceV1->LoseDevice for each device that is removed and remove them from arpNdmSampleDevices
	// TODO: Call pNdmSampleHostInterfaceV1->RemoveDevice for each device that has been removed
	// TODO: Free devices that are removed
	// TODO: Call pNdmSampleHostInterfaceV1->AddDevice for each device that has been added
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Auxiliary function used by all plugin interface functions to check validity of passed device handle. 
 * This is crucial, because API users might keep obsolete handles to devices which have already been removed.
 * The handles will also become obsolete if devices are not removed, but module is unplugged/replugged.
 */
static NResult N_API NdmSampleCheckDeviceHandle(NHandle hDevice, NdmSampleDevice * * ppDevice)
{
N_TRY
	NdmSampleDevice * pDevice = (NdmSampleDevice *)hDevice;
	NBool handleFound = NFalse;
	NInt i;

	if (!hDevice) N_ERROR_ARGUMENT_NULL_P(hDevice);
	if (!ppDevice) N_ERROR_ARGUMENT_NULL_P(ppDevice);
	if (!ndmSampleIsPlugged) N_ERROR_INVALID_OPERATION_M(N_T("NdmSample is not plugged"));

	for (i = 0; i < ndmSampleDeviceCount; i++)
	{
		if (arpNdmSampleDevices[i] == pDevice)
		{
			handleFound = NTrue;
			break;
		}
	}
	if (!handleFound) N_ERROR_ARGUMENT_M(N_T("NdmSample device handle is invalid"));
	*ppDevice = pDevice;
N_FINALLY
	if (N_WAS_ERROR)
	{
		if (ppDevice) *ppDevice = NULL;
	}
N_TRY_END
}

/*
 * Implementation of interfaceV1.GetDeviceType function.
 * The function will be invoked when API function NDeviceGetDeviceType is called.
 */
static NResult N_API NdmSampleGetDeviceType(NHandle hDevice, NDeviceType * pValue)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;

	if (!pValue) N_ERROR_ARGUMENT_NULL_P(pValue);
	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));

	*pValue = pDevice->deviceType;
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.GetDeviceId function.
 * The function will be invoked when API function NDeviceGetId is called.
 */
static NResult N_API NdmSampleGetDeviceId(NHandle hDevice, HNString * phValue)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	N_CHECK(NStringCreate(pDevice->szId, phValue));
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.GetDeviceDisplayName function.
 * The function will be invoked when API function NDeviceGetDisplayName is called.
 */
static NResult N_API NdmSampleGetDeviceDisplayName(NHandle hDevice, HNString * phValue)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	N_CHECK(NStringCreate(pDevice->szDisplayName, phValue));
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}
/*
 * Implementation of interfaceV1.IsCaptureDeviceCapturing function.
 * The function will be invoked when API function NCaptureDeviceIsCapturing is called.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to StartCaptureDeviceCapturing, otherwise the device usages through different device managers will interfere. 
 */
static NResult N_API NdmSampleIsCaptureDeviceCapturing(NHandle hDevice, NBool * pValue, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;

	*pValue = pDevice->isCapturing && pDevice->pParam == pParam;
N_FINALLY
	if (N_WAS_ERROR)
	{
		if (pValue) *pValue = NFalse;
	}
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.StartCaptureDeviceCapturing function.
 * The function will be invoked when API function NCaptureDeviceStartCapturing is called.
 * The pIsCaptureDeviceCapturingChanged is callback to the framework which must be invoked when capturing is started/stopped. This must be performed not only if stop is requested by the API (call to interfaceV1.StopCaptureDeviceCapturing) but also if capturing is stopped due to other reason (like error, stream termination, device disconnection or plugin unplug).
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be remembered and checked in other capturing function, otherwise the device usages through different device managers will interfere. The pParam should also be passed to pIsCaptureDeviceCapturingChanged callback. 
 * Important: The plugin should take care that the internal buffers don't grow indefinitely. I.e. must drop the old data if the GetCameraFrame/GetMicrophoneSoundSample are not called fast enough.
 */
static NResult N_API NdmSampleStartCaptureDeviceCapturing(NHandle hDevice, NdmIsCaptureDeviceCapturingChangedProc pIsCapturingChanged, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;
	NBool startedCapture = NFalse;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;
	if (pDevice->isCapturing) N_ERROR_INVALID_OPERATION_M(N_T("Capture device is already capturing"));

	N_CHECK(NdmSampleDeviceInitReader(pDevice));
	N_CHECK(NMediaReaderStart(pDevice->hMediaReader));
	pDevice->isCapturing = NTrue;
	pDevice->pIsCaptureDeviceCapturingChanged = pIsCapturingChanged;
	pDevice->pParam = pParam;
	startedCapture = NTrue;
N_FINALLY
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (startedCapture)
	{
		N_CHECK(pDevice->pIsCaptureDeviceCapturingChanged(hDevice, pParam));
	}
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.StopCaptureDeviceCapturing function.
 * The function will be invoked when API function NCaptureDeviceStopCapturing is called.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to StartCaptureDeviceCapturing, otherwise the device usages through different device managers will interfere. 
 */
static NResult N_API NdmSampleStopCaptureDeviceCapturing(NHandle hDevice, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;

	N_CHECK(NdmSampleDeviceStopCapturing(pDevice, pParam));
N_FINALLY
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.GetCameraFrame function.
 * The function will be invoked when API function NCameraGetFrame is called.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to StartCaptureDeviceCapturing, otherwise the device usages through different device managers will interfere. 
 */
static NResult N_API NdmSampleGetCameraFrame(NHandle hDevice, HNImage * phImage, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;
	NTimeSpan timeStamp, duration;
	NMediaState mediaState;
	NBool stoppedCapture = NFalse;

	if (phImage) *phImage = NULL;
	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;
	NMonitorExit(&ndmSampleMonitor); exitMonitor = NFalse;

	if (pDevice->isCapturing && pDevice->pParam == pParam)
	{
		N_CHECK(NMediaReaderReadVideoSample(pDevice->hMediaReader, &timeStamp, &duration, phImage));
		N_CHECK(NMediaReaderGetState(pDevice->hMediaReader, &mediaState));
		if (mediaState == nmsStopped)
		{
			pDevice->isCapturing = NFalse;
			stoppedCapture = NTrue;
		}
	}
N_FINALLY
	if (stoppedCapture)
	{
		N_CHECK(pDevice->pIsCaptureDeviceCapturingChanged(hDevice, pDevice->pParam));
		pDevice->pIsCaptureDeviceCapturingChanged = NULL;
		pDevice->pParam = NULL;
	}
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.GetMicrophoneSoundSample function.
 * The function will be invoked when API function NMicrophoneGetSoundSample is called.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to StartCaptureDeviceCapturing, otherwise the device usages through different device managers will interfere. 
 */
static NResult N_API NdmSampleGetMicrophoneSoundSample(NHandle hDevice, HNSoundBuffer * phSoundBuffer, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;
	NTimeSpan timeStamp, duration;
	NMediaState mediaState;
	NBool stoppedCapture = NFalse;

	if (phSoundBuffer) *phSoundBuffer = NULL;
	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;
	NMonitorExit(&ndmSampleMonitor); exitMonitor = NFalse;

	if (pDevice->isCapturing && pDevice->pParam == pParam)
	{
		N_CHECK(NMediaReaderReadAudioSample(pDevice->hMediaReader, &timeStamp, &duration, phSoundBuffer));
		N_CHECK(NMediaReaderGetState(pDevice->hMediaReader, &mediaState));
		if (mediaState == nmsStopped)
		{
			pDevice->isCapturing = NFalse;
			stoppedCapture = NTrue;
		}
	}
N_FINALLY
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (stoppedCapture)
	{
		N_CHECK(pDevice->pIsCaptureDeviceCapturingChanged(hDevice, pDevice->pParam));
		pDevice->pIsCaptureDeviceCapturingChanged = NULL;
		pDevice->pParam = NULL;
	}
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.CancelBiometricDevice function.
 * The function will be invoked when API function NBiometricDeviceCancel is called.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be checked to be equal to pParam passed to CaptureIrisScanner/CaptureFScanner, otherwise the device usages through different device managers will interfere. 
 */
static NResult N_API NdmSampleCancelBiometricDevice(NHandle hDevice, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	N_CHECK(NdmSampleDeviceCancel(pDevice, pParam));
N_FINALLY
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Auxiliary function used by NdmSampleCaptureIrisScanner and NdmSampleCaptureFScanner.
 * Notifies NdmSampleCancelBiometricDevice function waiting for completion of canceled capturing.
 */
static NResult NdmSampleDeviceFinishCapture(NdmSampleDevice * pDevice)
{
N_TRY
	if (!pDevice) N_ERROR_ARGUMENT_NULL_P(pDevice);
	pDevice->isCapturing = NFalse;
	pDevice->pParam = NULL;
	pDevice->cancelCapture = NFalse;
	N_CHECK(NSyncEventSet(pDevice->hCaptureFinishedEvent));
N_TRY_E
}

/*
 * Implementation of interfaceV1.GetFScannerSupportedImpressionTypes function.
 * The function will be invoked when API function NFScannerGetSupportedImpressionTypes is called.
 */
static NResult N_API NdmSampleGetFScannerSupportedImpressionTypes(NHandle hDevice, NFImpressionType * arValue, NInt valueLength)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NInt count = 0;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	// TODO: Modify the following code according to scanner capabilities
	if (arValue)
	{
		if (valueLength < count + 1) N_ERROR_ARGUMENT_MP(N_T("valueLength is insufficient"), valueLength);
		arValue[count] = nfitLiveScanPlain;
	}
	count += 1;

	N_RESULT(count);
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.GetFScannerSupportedPositions function.
 * The function will be invoked when API function NFScannerGetSupportedPositions is called.
 */
static NResult N_API NdmSampleGetFScannerSupportedPositions(NHandle hDevice, NFPosition * arValue, NInt valueLength)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NInt count = 0;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	// TODO: Modify the following code according to scanner capabilities
	if (arValue)
	{
		if (valueLength < count + 1) N_ERROR_ARGUMENT_MP(N_T("valueLength is insufficient"), valueLength);
		arValue[count] = nfpUnknown;
	}
	count += 1;

	N_RESULT(count);
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.CaptureFScanner function.
 * The function will be invoked when API functions NFScannerCapture or NFScannerCaptureEx are called.
 * See the documentation for meaning of the arguments.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be remembered and checked in other capturing function, otherwise the device usages through different device managers will interfere. The pParam should also be passed to pPreview callback. 
 */
static NResult N_API NdmSampleCaptureFScanner(NHandle hDevice, HNObject hCaptureInfo, NFImpressionType impressionType, NFPosition position, const NFPosition * arMissingPositions, NInt missingPositionCount,
	NBool automatic, NInt timeoutMilliseconds, const HNFAttributes * arhObjects, NInt objectCount, HNImage * arhImages, NInt imagesLength, NInt * pImageCount, NBiometricStatus * pStatus,
	NdmPreviewFScannerProc pPreview, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice = NULL;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;
	HNImage hImage = NULL;
	NBool finishCapture = NFalse;
	NInt i = 0;

	if (!arhObjects) N_ERROR_ARGUMENT_NULL_P(arhObjects);
	if (objectCount != 1) N_ERROR_ARGUMENT_MP(N_T("objectCount is not one"), objectCount); // TODO: Remove/change this if slap capture is supported
	if (!arhImages) N_ERROR_ARGUMENT_NULL_P(arhImages);
	if (imagesLength < 1) N_ERROR_ARGUMENT_MP(N_T("imagesLength is insufficient"), imagesLength); // TODO: Remove/change this if multi-image capture is supported
	if (!pImageCount) N_ERROR_ARGUMENT_NULL_P(pImageCount);
	if (!pStatus) N_ERROR_ARGUMENT_NULL_P(pStatus);

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;
	NMonitorExit(&ndmSampleMonitor); exitMonitor = NFalse;
	if (pDevice->isCapturing) N_ERROR_INVALID_OPERATION_M(N_T("Scanner is already capturing"));

	N_CHECK(NImageCreateFromFileExN(pDevice->hFileName1, NULL, 0, NULL, &hImage));
	// TODO: Add scanner capture initialization code here
	pDevice->isCapturing = NTrue;
	pDevice->pParam = pParam;
	N_CHECK(NSyncEventReset(pDevice->hCaptureFinishedEvent));
	finishCapture = NTrue;
	NMonitorExit(&pDevice->monitor); exitDeviceMonitor = NFalse;

	for (; ; )
	{
		if (pDevice->cancelCapture)
		{
			*pStatus = nbsCanceled;
		}
		else
		{
			// TODO: Iterate capture loop, update hImage instead of the following dummy code
			if (i < 5)
			{
				*pStatus = nbsObjectNotFound;
			}
			else if (i < 10)
			{
				*pStatus = nbsBadObject;
			}
			else if (i < 15)
			{
				*pStatus = nbsNone;
			}
			else
			{
				*pStatus = nbsOk;
			}
			i++;
			NThreadSleep(100);
		}

		N_CHECK(pPreview(hDevice, hCaptureInfo, &hImage, 1, pStatus, pParam));
		if (NBiometricStatusIsFinal(*pStatus)) break;
	}

	arhImages[0] = hImage; hImage = NULL;

	N_UNREFERENCED_PARAMETER(timeoutMilliseconds);
	N_UNREFERENCED_PARAMETER(automatic);
	N_UNREFERENCED_PARAMETER(missingPositionCount);
	N_UNREFERENCED_PARAMETER(arMissingPositions);
	N_UNREFERENCED_PARAMETER(position);
	N_UNREFERENCED_PARAMETER(impressionType);
N_FINALLY
	if (hImage) N_CHECK(NObjectSet(NULL, &hImage));
	if (finishCapture)
	{
		N_CHECK(NdmSampleDeviceFinishCapture(pDevice));
	}
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implemenation of interfaceV1.GetIrisScannerSupportedPositions
 * The function will be invoked when API function NIrisScannerGetSupportedPositions is called.
 */
static NResult N_API NdmSampleGetIrisScannerSupportedPositions(NHandle hDevice, NEPosition * arValue, NInt valueLength)
{
N_TRY
	NdmSampleDevice * pDevice;
	NBool exitMonitor = NFalse;
	NInt count = 0;

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	// TODO: Modify the following code according to scanner capabilities
	if (arValue)
	{
		if (valueLength < count + 2) N_ERROR_ARGUMENT_MP(N_T("valueLength is insufficient"), valueLength);
		arValue[count] = nepRight;
		arValue[count + 1] = nepLeft;
	}
	count += 2;

	N_RESULT(count);
N_FINALLY
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Implementation of interfaceV1.CaptureIrisScanner function.
 * The function will be invoked when API functions NIrisScannerCapture or NIrisScannerCaptureEx are called.
 * See the documentation of API function NIrisScannerCaptureEx for meaning of the arguments.
 * The pParam is a context of a device. Different context correspond to devices accessed from different device managers. Consequently pParam should be remembered and checked in other capturing function, otherwise the device usages through different device managers will interfere. The pParam should also be passed to pPreview callback. 
 */
static NResult N_API NdmSampleCaptureIrisScanner(NHandle hDevice, HNObject hCaptureInfo, NEPosition position, const NEPosition * arMissingPositions, NInt missingPositionCount,
	NBool automatic, NInt timeoutMilliseconds, const HNEAttributes * arhObjects, NInt objectCount, HNImage * arhImages, NInt imagesLength, NInt * pImageCount, NBiometricStatus * pStatus,
	NdmPreviewIrisScannerProc pPreview, void * pParam)
{
N_TRY
	NdmSampleDevice * pDevice = NULL;
	NBool exitMonitor = NFalse;
	NBool exitDeviceMonitor = NFalse;
	HNImage hImage = NULL;
	NBool finishCapture = NFalse;
	NInt i = 0;

	if (!arhObjects) N_ERROR_ARGUMENT_NULL_P(arhObjects);
	if (objectCount != 1) N_ERROR_ARGUMENT_MP(N_T("objectCount is not one"), objectCount); // TODO: Remove/change this if slap capture is supported
	if (!arhImages) N_ERROR_ARGUMENT_NULL_P(arhObjects);
	if (imagesLength < 1) N_ERROR_ARGUMENT_MP(N_T("imagesLength is insufficient"), imagesLength); // TODO: Remove/change this if multi-image capture is supported
	if (!pImageCount) N_ERROR_ARGUMENT_NULL_P(arhObjects);
	if (!pStatus) N_ERROR_ARGUMENT_NULL_P(arhObjects);

	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	N_CHECK(NdmSampleCheckDeviceHandle(hDevice, &pDevice));
	NMonitorEnter(&pDevice->monitor); exitDeviceMonitor = NTrue;
	NMonitorExit(&ndmSampleMonitor); exitMonitor = NFalse;
	if (pDevice->isCapturing) N_ERROR_INVALID_OPERATION_M(N_T("Scanner is already capturing"));

	N_CHECK(NImageCreateFromFileExN(position == nepLeft ? pDevice->hFileName2 : pDevice->hFileName1, NULL, 0, NULL, &hImage));
	// TODO: Add scanner capture initialization code here
	pDevice->isCapturing = NTrue;
	pDevice->pParam = pParam;
	N_CHECK(NSyncEventReset(pDevice->hCaptureFinishedEvent)); finishCapture = NTrue;
	NMonitorExit(&pDevice->monitor); exitDeviceMonitor = NFalse;

	for (; ; )
	{
		if (pDevice->cancelCapture)
		{
			*pStatus = nbsCanceled;
		}
		else
		{
			// TODO: Iterate capture loop, update hImage instead of the following dummy code
			if (i < 5)
			{
				*pStatus = nbsObjectNotFound;
			}
			else if (i < 10)
			{
				*pStatus = nbsBadObject;
			}
			else if (i < 15)
			{
				*pStatus = nbsNone;
			}
			else
			{
				*pStatus = nbsOk;
			}
			i++;
			N_CHECK(NThreadSleep(100));
		}

		N_CHECK(pPreview(hDevice, hCaptureInfo, &hImage, 1, pStatus, pParam));
		if (NBiometricStatusIsFinal(*pStatus)) break;
	}

	arhImages[0] = hImage; hImage = NULL;

	N_UNREFERENCED_PARAMETER(timeoutMilliseconds);
	N_UNREFERENCED_PARAMETER(automatic);
	N_UNREFERENCED_PARAMETER(missingPositionCount);
	N_UNREFERENCED_PARAMETER(arMissingPositions);
N_FINALLY
	if (hImage) N_CHECK(NObjectSet(NULL, &hImage));
	if (finishCapture)
	{
		N_CHECK(NdmSampleDeviceFinishCapture(pDevice));
	}
	if (exitDeviceMonitor) NMonitorExit(&pDevice->monitor);
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Plugin activation.
 * Major plugin initialization should happen here. The function should prepare the module for work. It hould also check for availability of supported devices and register them in device manager.
 *
 * This function is registered by NPluginModuleSetPlug() from the NdmSampleModuleCreate() and will be called only if following conditions hold:
 *   * The plugin is not disabled and activated by an API user (NPluginPlug or NPluginManagerPlugAll/NPluginManagerRefresh functions). The activated plugin will have npsUnused state.
 *   * There is a device manager that enumerates the devices of the type provided by the plugin.
 *
 * Note: The function can be called for newly loaded plugins, as whell as previously unplugged ones. The plugin should be designed for multiple plug/unplug cycles.
 */
static NResult N_API NdmSamplePlug(HNString hDirectory, NVersion interfaceVersion, const void * pHostInterface, HNPlugin hPlugin)
{
N_TRY
	HNModule hModule = NULL;
	NBool exitMonitor = NFalse;
	NInt i;
	const NChar * szDirectory;

	N_CHECK(NdmSampleModuleOf(&hModule));
	N_CHECK(NModuleCheckInit(hModule, NFalse));
	N_CHECK(NStringGetBuffer(hDirectory, NULL, &szDirectory));
	NMonitorEnter(&ndmSampleMonitor); exitMonitor = NTrue;
	ndmSampleInterfaceVersion = interfaceVersion;
	hNdmSamplePlugin = hPlugin;
	ndmSampleIsPlugged = NTrue;
	if (NVersionGetMajor(interfaceVersion) == 1)
	{
		pNdmSampleHostInterfaceV1 = (NdmHostInterfaceV1 *)pHostInterface;
	}
	else
	{
		N_ERROR_NOT_IMPLEMENTED();
	}

	// TODO: Remove the following code up to NdmSampleUpdateDeviceList call and implement device enumeration in NdmSampleUpdateDeviceList
	N_CHECK(NCAlloc(sizeof(NdmSampleDevice *) * 4, (void * *)&arpNdmSampleDevices));

	N_CHECK(NdmSampleDeviceCreate(ndtMicrophone, N_T("Sample Microphone"), N_T("Sample Microphone"), szDirectory, N_T("NdmSampleMicrophone.wav"), NULL, &arpNdmSampleDevices[ndmSampleDeviceCount]));
	ndmSampleDeviceCount++;

	N_CHECK(NdmSampleDeviceCreate(ndtCamera, N_T("Sample Camera"), N_T("Sample Camera"), szDirectory, N_T("NdmSampleCamera.avi"), NULL, &arpNdmSampleDevices[ndmSampleDeviceCount]));
	ndmSampleDeviceCount++;

	N_CHECK(NdmSampleDeviceCreate(ndtFingerScanner, N_T("Sample Finger Scanner"), N_T("Sample Finger Scanner"), szDirectory, N_T("NdmSampleFingerScanner.bmp"), NULL, &arpNdmSampleDevices[ndmSampleDeviceCount]));
	ndmSampleDeviceCount++;

	N_CHECK(NdmSampleDeviceCreate(ndtIrisScanner, N_T("Sample Iris Scanner"), N_T("Sample Iris Scanner"), szDirectory, N_T("NdmSampleIrisScannerLeft.png"), N_T("NdmSampleIrisScannerRight.png"), &arpNdmSampleDevices[ndmSampleDeviceCount]));
	ndmSampleDeviceCount++;

	for (i = 0; i < ndmSampleDeviceCount; i++)
	{
		N_CHECK(pNdmSampleHostInterfaceV1->AddDevice(arpNdmSampleDevices[i], hPlugin, NULL));
	}
	N_CHECK(NdmSampleUpdateDeviceList());
N_FINALLY
	if (hModule) N_CHECK(NObjectSet(NULL, &hModule));
	if (exitMonitor) NMonitorExit(&ndmSampleMonitor);
N_TRY_END
}

/*
 * Plugin deactivation. Will be called when framework unplugs the plugin. Unplug can happend when the provided devices are no longer monitored by device managers or when unplug is explicitly requested by the API user (NPluginUnplug/NPluginManagerUnplugAll functions).
 *
 * The function should unregister devices that were provided to device manager and clean-up resources allocated in NdmSamplePlug() and further operation.
 * Note: Users can unplug the plugin at any time (for example the device is still in use). Special care must be taked to implement the proper synchronisation.
 */
static NResult N_API NdmSampleUnplug(void)
{
N_TRY
	NInt i;

	NMonitorEnter(&ndmSampleMonitor);
	for (i = 0; i < ndmSampleDeviceCount; i++)
	{
		N_CHECK(NdmSampleDeviceFree(arpNdmSampleDevices[i]));
	}
	NFree(arpNdmSampleDevices);
	arpNdmSampleDevices = NULL;
	ndmSampleDeviceCount = 0;
	pNdmSampleHostInterfaceV1 = NULL;
	ndmSampleInterfaceVersion = 0;
	hNdmSamplePlugin = NULL;
	ndmSampleIsPlugged = NFalse;
	NMonitorExit(&ndmSampleMonitor);
N_TRY_E
}

/*
 * Plugin initialization.
 * Will be called once (during module creation).
 * Should contain minimal plugin initialization. It is recommended to perform
 * resource allocation in NdmSamplePlug() function. For example device handles
 * and memory buffers should not be used in disabled plugin (the one not
 * plugged by the user).
 */
static NResult N_API NdmSampleInit(void)
{
N_TRY
	/* Setup lightweight mutex used to ensure consistency of internal data
	   because module will be used by multiple threads.
	   The NMutex or other critical section primitive can be used insted of NMonitor.
	 */
	NMonitorInit(&ndmSampleMonitor); ndmSampleMonitorInitialized = NTrue;

	N_RETURN;
N_TRY_E
}

/*
 * Plugin finalization.
 * Will be called once (during module unregistration).
 * Should contain plugin clean-up code.
 */
static NResult N_API NdmSampleUninit(NBool isProcessTermination)
{
N_TRY
	if (!isProcessTermination)
	{
		if (ndmSampleIsPlugged)
		{
			N_CHECK(NPluginUnplug(hNdmSamplePlugin));
		}
		if (ndmSampleMonitorInitialized)
		{
			NMonitorDispose(&ndmSampleMonitor);
			ndmSampleMonitorInitialized = NFalse;
		}
	}
N_TRY_E
}

/*
 * NModule Constructor.
 * The function registers the plugin in Neurotechnology framework and is called upon library load (from DllMain). 
 * The module info (name, version etc.) and plugin interface are described here, and will have to be customized by the writer of a plugin.
 */
static NResult N_API NdmSampleModuleCreate(HNModule * phModule)
{
N_TRY
	static NModuleOfProc arpDependences[] = { N_MODULE_OF(NCore), N_MODULE_OF(NMedia), N_MODULE_OF(NDevices), N_MODULE_OF(NMediaProc), N_MODULE_OF(NBiometrics) };
	HNPluginModule hModule = NULL;
	static NdmInterfaceV1 interfaceV1 = { 0 };
	static const NPluginInterfaceVersionInfo arInterfaceVersionInfos[] =
	{
		NPluginInterfaceVersionInfoConst(1, 0, 1, interfaceV1),
	};
	NModuleOptions moduleOptions = 
		#ifdef N_UNICODE
			nmoUnicode
		#else
			nmoNone
		#endif
		|
		#ifdef N_NO_ANSI_FUNC
			nmoNoAnsiFunc
		#else
			nmoNone
		#endif
		|
		#ifdef N_NO_UNICODE
			nmoNoUnicode
		#else
			nmoNone
		#endif
		|
		#ifdef N_DEBUG
			nmoDebug
		#else
			nmoNone
		#endif
		|
		#if defined(N_LIB)
			nmoLib
		#elif defined(N_EXE)
			nmoExe
		#else
			nmoNone
		#endif
			;

	if (!phModule) N_ERROR_ARGUMENT_NULL_P(phModule);

	N_CHECK(NPluginModuleCreate(&hModule));
	// This example plugin doesn't implement all NdmInterfaceV1 functions,
	// please check the NdmInterface.h for list of all the possible functions.
	// The file also contains comments showing which device types require particular function.
	interfaceV1.GetSupportedDeviceTypes = NdmSampleGetSupportedDeviceTypes;
	interfaceV1.UpdateDeviceList = NdmSampleUpdateDeviceList;
	interfaceV1.GetDeviceType = NdmSampleGetDeviceType;
	interfaceV1.GetDeviceId = NdmSampleGetDeviceId;
	interfaceV1.GetDeviceDisplayName = NdmSampleGetDeviceDisplayName;
	interfaceV1.StartCaptureDeviceCapturing = NdmSampleStartCaptureDeviceCapturing;
	interfaceV1.GetCameraFrame = NdmSampleGetCameraFrame;
	interfaceV1.StopCaptureDeviceCapturing = NdmSampleStopCaptureDeviceCapturing;
	interfaceV1.IsCaptureDeviceCapturing = NdmSampleIsCaptureDeviceCapturing;
	interfaceV1.GetMicrophoneSoundSample = NdmSampleGetMicrophoneSoundSample;
	interfaceV1.CancelBiometricDevice = NdmSampleCancelBiometricDevice;
	interfaceV1.GetFScannerSupportedImpressionTypes = NdmSampleGetFScannerSupportedImpressionTypes;
	interfaceV1.GetFScannerSupportedPositions = NdmSampleGetFScannerSupportedPositions;
	interfaceV1.CaptureFScanner = NdmSampleCaptureFScanner;
	interfaceV1.GetIrisScannerSupportedPositions = NdmSampleGetIrisScannerSupportedPositions;
	interfaceV1.CaptureIrisScanner = NdmSampleCaptureIrisScanner;
	// TODO: Add implemented interface functions here

	N_CHECK(NModuleSetOptions(hModule, moduleOptions));
	N_CHECK(NModuleSetDependences(hModule, arpDependences, sizeof(arpDependences) / sizeof(arpDependences[0])));
	N_CHECK(NModuleSetInit(hModule, NdmSampleInit));
	N_CHECK(NModuleSetUninit(hModule, NdmSampleUninit));
	/* When changing the plugin's name, make sure to also rename
	 *   * the NdmSampleModuleOf function and
	 *   * the name of the resulting DLL file (NdmSample.dll).
	 */
	N_CHECK(NModuleSetName(hModule, N_T("NdmSample")));
	N_CHECK(NModuleSetTitle(hModule, N_T("Neurotechnology Devices Sample Module")));
	N_CHECK(NModuleSetProduct(hModule, N_T("Neurotechnology Devices")));
	N_CHECK(NModuleSetCompany(hModule, N_T("Neurotechnology")));
	N_CHECK(NModuleSetCopyright(hModule, N_T("Copyright (C) 2011-2017 Neurotechnology")));
	N_CHECK(NModuleSetVersionMajor(hModule, 5));
	N_CHECK(NModuleSetVersionMinor(hModule, 1));
	N_CHECK(NModuleSetVersionBuild(hModule, 0));
	N_CHECK(NModuleSetVersionRevision(hModule, 0));
	N_CHECK(NPluginModuleSetPluginName(hModule, N_T("Sample")));
	N_CHECK(NPluginModuleSetInterfaceType(hModule, N_T("NDeviceManager")));
	N_CHECK(NPluginModuleSetPriority(hModule, 0));
	N_CHECK(NPluginModuleSetPlug(hModule, NdmSamplePlug));
	N_CHECK(NPluginModuleSetUnplug(hModule, NdmSampleUnplug));
	N_CHECK(NPluginModuleSetInterfaceVersions(hModule, arInterfaceVersionInfos, sizeof(arInterfaceVersionInfos) / sizeof(arInterfaceVersionInfos[0])));

	*phModule = hModule;
N_FINALLY
	if (N_WAS_ERROR)
	{
		if (phModule) *phModule = NULL;
		if (hModule) N_CHECK(NObjectSet(NULL, &hModule));
	}
N_TRY_END
}

static HNModule hNdmSampleModule = NULL;

/************************************************************************
 * Returns the handle of the plugin module.
 * This function is MANDATORY and should have the same basename as the plugin's DLL:
 * The plugin named <DeviceName> should have Ndm<DeviceName>ModuleOf function and be saved as Ndm<DeviceName>.dll file.
 ************************************************************************/
NResult N_API NdmSampleModuleOf(HNModule * phValue)
{
N_TRY
	if (!hNdmSampleModule)
	{
		/* The module's constructor haven't been called, so need to call it manually.
		 * This can only happen if module is linked statically to the application (rather than compiled as dynamic library used as plugin).
		 * In that case, the application must explicitly call this ModuleOf function before calling NPluginManagerAddPlugin.
		 */
		N_CHECK(NModuleRegister(NdmSampleModuleCreate, &hNdmSampleModule));
	}
	N_CHECK(NObjectGet(hNdmSampleModule, phValue));
N_FINALLY
	if (N_WAS_ERROR)
	{
		if (phValue) *phValue = NULL;
	}
N_TRY_END
}

/************************************************************************
 * Multiplatform code to automatically launch the module constructor
 * (the NdmSampleModuleCreate function) on library load.
 * Changes beyong this line are probably not needed.
 ************************************************************************/

#if !defined(N_LIB) && !defined(N_EXE)

#ifdef N_WINDOWS
	#define WIN32_LEAN_AND_MEAN
	#include <Interop/NWindows.h>
#else
	#include <stdlib.h>
	#include <signal.h>
	#include <unistd.h>
#endif // N_WINDOWS

#ifdef N_WINDOWS

static void DllMainProcessDetach(HANDLE hinstDLL, LPVOID lpvReserved)
{
	if (lpvReserved == NULL)
	{
		NModuleUnregister(&hNdmSampleModule);
	}
	N_UNREFERENCED_PARAMETER(hinstDLL);
}

static BOOL DllMainProcessAttach(HANDLE hinstDLL, LPVOID lpvReserved)
{
N_TRY
	if (!hNdmSampleModule)
	{
		N_CHECK(NModuleRegister(NdmSampleModuleCreate, &hNdmSampleModule));
	}
	N_UNREFERENCED_PARAMETER(hinstDLL);
	N_UNREFERENCED_PARAMETER(lpvReserved);
N_FINALLY
	if (N_WAS_ERROR)
	{
		N_CHECK(NErrorReport(N_THE_ERROR));
		// DllMainProcessDetach will be called automatically by Windows
	}
N_TRY_END_RAW
	return N_WAS_ERROR ? FALSE : TRUE;
}

BOOL WINAPI DllMain(HANDLE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
	switch (fdwReason)
	{
	case DLL_PROCESS_ATTACH:
		return DllMainProcessAttach(hinstDLL, lpvReserved);
	case DLL_THREAD_ATTACH:
		return TRUE;
	case DLL_THREAD_DETACH:
		return TRUE;
	case DLL_PROCESS_DETACH:
		DllMainProcessDetach(hinstDLL, lpvReserved);
		return TRUE;
	default:
		return TRUE;
	}
}

#else // !N_WINDOWS

#ifndef N_CPP
/* this attribute doesn't always work for C++ */
#define N_MODULE_CONSTRUCTOR __attribute__((constructor))
#define N_MODULE_DESTRUCTOR  __attribute__((destructor))
#else
#define N_MODULE_CONSTRUCTOR
#define N_MODULE_DESTRUCTOR
#endif

N_MODULE_DESTRUCTOR static void NModuleUninit(void)
{
	NModuleUnregister(&hNdmSampleModule);
}

N_MODULE_CONSTRUCTOR static void NModuleInit(void)
{
N_TRY
	N_CHECK(NModuleRegister(NdmSampleModuleCreate, &hNdmSampleModule));
N_FINALLY
	if (N_WAS_ERROR)
	{
		N_CHECK(NErrorReport(N_THE_ERROR));
		NModuleUninit();
		raise(SIGSYS);
	}
N_TRY_END_RAW
}

#undef N_MODULE_CONSTRUCTOR
#undef N_MODULE_DESTRUCTOR

#ifdef N_CPP

class NModuleInitializer
{
public:
	NModuleInitializer()
	{
		NModuleInit();
	}

	~NModuleInitializer()
	{
		NModuleUninit();
	}
};

static NModuleInitializer nModuleInitializer;

#endif // N_CPP

#endif // !N_WINDOWS

#endif // !defined(N_LIB) && !defined(N_EXE)
