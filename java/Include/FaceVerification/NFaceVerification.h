#ifndef N_FACE_VERIFICATION_H_INCLUDED
#define N_FACE_VERIFICATION_H_INCLUDED

#include <Core/NTypes.h>
#include <Core/NModule.h>
#include <Core/NObject.h>
#include <Images/NImage.h>
#include <Geometry/NGeometry.h>

#ifdef N_CPP
extern "C"
{
#endif

#define N_FACE_VERIFICATION_MAX_USER_COUNT 10

typedef enum NFaceVerificationStatus_
{
	nlesNone = 0,
	nlesSuccess = 1,
	nlesTimeout = 2,
	nlesCanceled = 3,
	nlesBadQuality = 4,
	nlesUserExists = 5,
	nlesMatchNotFound = 6,
	nlesCameraNotFound = 7,
	nlesOperationNotActivated = 8,
	nlesRequiresLivenessAction = 9,
	nlesUserNotFound = 10,
	nlesFaceNotFound = 11,
	nlesLivenessCheckFailed = 12
} NFaceVerificationStatus;

N_DECLARE_TYPE(NFaceVerificationStatus)

typedef enum NFaceVerificationLivenessAction_
{
	nllaNone = 0,
	nllaKeepStill = 0x000001,
	nllaBlink = 0x000002,
	nllaRotateYaw = 0x000004,
	nllaKeepRotatingYaw = 0x000008
} NFaceVerificationLivenessAction;
N_DECLARE_TYPE(NFaceVerificationLivenessAction)

typedef enum NFaceVerificationLivenessMode_
{
	nllmNone = 0,
	nllmPassive = 1,
	nllmActive = 2,
	nllmPassiveAndActive = 3,
	nllmSimple = 4,
} NFaceVerificationLivenessMode;
N_DECLARE_TYPE(NFaceVerificationLivenessMode)

N_DECLARE_MODULE(NFaceVerification)
N_DECLARE_OBJECT_TYPE(NFaceVerification, NObject)
N_DECLARE_OBJECT_TYPE(NFaceVerificationUser, NObject)
N_DECLARE_OBJECT_TYPE(NFaceVerificationEventInfo, NObject)

typedef NResult (N_CALLBACK NFaceVerificationCapturePreviewCallback)(HNFaceVerificationEventInfo hEventInfo, void * pParam);
N_DECLARE_TYPE(NFaceVerificationCapturePreviewCallback)

NResult N_API NFaceVerificationInitializeN(HNString hDbName, HNString hPassword);
#ifndef N_NO_ANSI_FUNC
NResult N_API NFaceVerificationInitializeA(const NAChar * szDbName, const NAChar * szPassword);
#endif
#ifndef N_NO_UNICODE
NResult N_API NFaceVerificationInitializeW(const NWChar * szDbName, const NWChar * szPassword);
#endif
#define NFaceVerificationInitialize N_FUNC_AW(NFaceVerificationInitialize)
#ifdef N_DOCUMENTATION
NResult N_API NFaceVerificationInitialize(const NChar * szDbName, const NChar * szPassword);
#endif

NResult N_API NFaceVerificationUninitialize(void);

NResult N_API NFaceVerificationGetQualityThreshold(NByte * pValue);
NResult N_API NFaceVerificationSetQualityThreshold(NByte value);
NResult N_API NFaceVerificationGetMatchingThreshold(NInt * pValue);
NResult N_API NFaceVerificationSetMatchingThreshold(NInt value);
NResult N_API NFaceVerificationGetLivenessThreshold(NByte * pValue);
NResult N_API NFaceVerificationSetLivenessThreshold(NByte value);
NResult N_API NFaceVerificationGetLivenessMode(NFaceVerificationLivenessMode * pValue);
NResult N_API NFaceVerificationSetLivenessMode(NFaceVerificationLivenessMode value);
NResult N_API NFaceVerificationSetUseManualExtraction(NBool value);
NResult N_API NFaceVerificationGetUseManualExtraction(NBool * pValue);
NResult N_API NFaceVerificationSetLivenessBlinkTimeout(NInt value);
NResult N_API NFaceVerificationGetLivenessBlinkTimeout(NInt * pValue);

NResult N_API NFaceVerificationGetAvailableCameraNamesN(HNString ** parhNames, NInt * pNameCount);
NResult N_API NFaceVerificationGetCameraN(HNString * phCameraName);
NResult N_API NFaceVerificationSetCameraN(HNString hCameraName);

NResult N_API NFaceVerificationEnrollN(HNString hId, NInt timeout, HNPropertyBag hMetadata, NFaceVerificationStatus * pStatus);
#ifndef N_NO_ANSI_FUNC
NResult N_API NFaceVerificationEnrollA(const NAChar * szId, NInt timeout, HNPropertyBag hMetadata, NFaceVerificationStatus * pStatus);
#endif
#ifndef N_NO_UNICODE
NResult N_API NFaceVerificationEnrollW(const NWChar * szId, NInt timeout, HNPropertyBag hMetadata, NFaceVerificationStatus * pStatus);
#endif
#define NFaceVerificationEnroll N_FUNC_AW(NFaceVerificationEnroll)
#ifdef N_DOCUMENTATION
NResult N_API NFaceVerificationEnroll(const NChar * szId, NInt timeout, HNPropertyBag hMetadata, NFaceVerificationStatus * pStatus);
#endif

NResult N_API NFaceVerificationVerifyN(HNString hId, NInt timeout, NFaceVerificationStatus * pStatus);
#ifndef N_NO_ANSI_FUNC
NResult N_API NFaceVerificationVerifyA(const NAChar * szId, NInt timeout, NFaceVerificationStatus * pStatus);
#endif
#ifndef N_NO_UNICODE
NResult N_API NFaceVerificationVerifyW(const NWChar * szId, NInt timeout, NFaceVerificationStatus * pStatus);
#endif
#define NFaceVerificationVerify N_FUNC_AW(NFaceVerificationVerify)
#ifdef N_DOCUMENTATION
NResult N_API NFaceVerificationVerify(const NChar * szId, NInt timeout, NFaceVerificationStatus * pStatus);
#endif

NResult N_API NFaceVerificationCancel();

NResult N_API NFaceVerificationGetUserCount(NInt * pValue);
NResult N_API NFaceVerificationGetUser(NInt index, HNFaceVerificationUser * phValue);
NResult N_API NFaceVerificationRemoveUserAt(NInt index);
NResult N_API NFaceVerificationClearUsers(void);

NResult N_API NFaceVerificationGetUserByIdN(HNString hId, HNFaceVerificationUser * phValue);
#ifndef N_NO_ANSI_FUNC
NResult N_API NFaceVerificationGetUserByIdA(const NAChar * szId, HNFaceVerificationUser * phValue);
#endif
#ifndef N_NO_UNICODE
NResult N_API NFaceVerificationGetUserByIdW(const NWChar * szId, HNFaceVerificationUser * phValue);
#endif
#define NFaceVerificationGetUserById N_FUNC_AW(NFaceVerificationGetUserById)
#ifdef N_DOCUMENTATION
NResult N_API NFaceVerificationGetUserById(const NChar * szId, HNFaceVerificationUser * phValue);
#endif

NResult N_API NFaceVerificationGetUserIndexByIdN(HNString hId, NInt * pValue);
#ifndef N_NO_ANSI_FUNC
NResult N_API NFaceVerificationGetUserIndexByIdA(const NAChar * szId, NInt * pValue);
#endif
#ifndef N_NO_UNICODE
NResult N_API NFaceVerificationGetUserIndexByIdW(const NWChar * szId, NInt * pValue);
#endif
#define NFaceVerificationGetUserIndexById N_FUNC_AW(NFaceVerificationGetUserIndexById)
#ifdef N_DOCUMENTATION
NResult N_API NFaceVerificationGetUserIndexById(const NChar * szId, NInt * pValue);
#endif

NResult N_API NFaceVerificationAddCapturePreview(HNCallback hCallback);
NResult N_API NFaceVerificationRemoveCapturePreview(HNCallback hCallback);
NResult N_API NFaceVerificationAddCapturePreviewCallback(NFaceVerificationCapturePreviewCallback pCallback, void * pParam);
NResult N_API NFaceVerificationRemoveCapturePreviewCallback(NFaceVerificationCapturePreviewCallback pCallback, void * pParam);

NResult N_API NFaceVerificationUserGetId(HNFaceVerificationUser hUser, HNString * phValue);
NResult N_API NFaceVerificationUserGetMetadata(HNFaceVerificationUser hUser, HNPropertyBag * phValue);

NResult N_API NFaceVerificationEventInfoMarkForExtraction(HNFaceVerificationEventInfo hEventInfo);
NResult N_API NFaceVerificationEventInfoGetImage(HNFaceVerificationEventInfo hEventInfo, HNImage * phValue);
NResult N_API NFaceVerificationEventInfoGetStatus(HNFaceVerificationEventInfo hEventInfo, NFaceVerificationStatus * pValue);
NResult N_API NFaceVerificationEventInfoGetYaw(HNFaceVerificationEventInfo hEventInfo, NFloat * pValue);
NResult N_API NFaceVerificationEventInfoGetRoll(HNFaceVerificationEventInfo hEventInfo, NFloat * pValue);
NResult N_API NFaceVerificationEventInfoGetPitch(HNFaceVerificationEventInfo hEventInfo, NFloat * pValue);
NResult N_API NFaceVerificationEventInfoGetQuality(HNFaceVerificationEventInfo hEventInfo, NByte * pValue);
NResult N_API NFaceVerificationEventInfoGetBoundingRect(HNFaceVerificationEventInfo hEventInfo, struct NRect_ * pValue);
NResult N_API NFaceVerificationEventInfoGetLivenessAction(HNFaceVerificationEventInfo hEventInfo, NFaceVerificationLivenessAction * pValue);
NResult N_API NFaceVerificationEventInfoGetLivenessTargetYaw(HNFaceVerificationEventInfo hEventInfo, NFloat * pValue);
NResult N_API NFaceVerificationEventInfoGetLivenessScore(HNFaceVerificationEventInfo hEventInfo, NByte * pValue);
NResult N_API NFaceVerificationEventInfoGetLastExtractionStatus(HNFaceVerificationEventInfo hEventInfo, NFaceVerificationStatus * pValue);
NResult N_API NFaceVerificationEventInfoFinishCapturing(HNFaceVerificationEventInfo hEventInfo);
NResult N_API NFaceVerificationEventInfoIsExtracting(HNFaceVerificationEventInfo hEventInfo, NBool * pValue);

#ifdef N_CPP
}
#endif

#endif // !N_FACE_VERIFICATION_H_INCLUDED
