#ifndef N_FACE_VERIFICATION_HPP_INCLUDED
#define N_FACE_VERIFICATION_HPP_INCLUDED

#include <Core/NTypes.hpp>
#include <Core/NModule.hpp>
#include <Core/NObject.hpp>
#include <Images/NImage.hpp>
#include <Geometry/NGeometry.hpp>

namespace Neurotec { namespace FaceVerification {
using Neurotec::Images::HNImage;
using Neurotec::Geometry::NRect_;
#include <FaceVerification/NFaceVerification.h>
}}

N_DEFINE_ENUM_TYPE_TRAITS(Neurotec::FaceVerification, NFaceVerificationStatus)
N_DEFINE_ENUM_TYPE_TRAITS(Neurotec::FaceVerification, NFaceVerificationLivenessAction)
N_DEFINE_ENUM_TYPE_TRAITS(Neurotec::FaceVerification, NFaceVerificationLivenessMode)

namespace Neurotec { namespace FaceVerification {
#undef N_FACE_VERIFICATION_MAX_USER_COUNT
const NInt N_FACE_VERIFICATION_MAX_USER_COUNT = 10;

class NFaceVerificationUser : public NObject
{
	N_DECLARE_OBJECT_CLASS(NFaceVerificationUser, NObject)

public:
	NString GetId() const
	{
		return GetString(NFaceVerificationUserGetId);
	}

	::Neurotec::NPropertyBag GetMetadata() const
	{
		return GetObject<HandleType, ::Neurotec::NPropertyBag>(NFaceVerificationUserGetMetadata);
	}
};

class NFaceVerificationEventInfo : public NObject
{
	N_DECLARE_OBJECT_CLASS(NFaceVerificationEventInfo, NObject)

public:
	::Neurotec::Images::NImage GetImage() const
	{
		return GetObject<HandleType, ::Neurotec::Images::NImage>(NFaceVerificationEventInfoGetImage);
	}

	NFaceVerificationStatus GetStatus() const
	{
		NFaceVerificationStatus value;
		NCheck(NFaceVerificationEventInfoGetStatus(GetHandle(), &value));
		return value;
	}

	NFaceVerificationLivenessAction GetLivenessAction() const
	{
		NFaceVerificationLivenessAction value;
		NCheck(NFaceVerificationEventInfoGetLivenessAction(GetHandle(), &value));
		return value;
	}

	NFloat GetLivenessTargetYaw() const
	{
		NFloat value;
		NCheck(NFaceVerificationEventInfoGetLivenessTargetYaw(GetHandle(), &value));
		return value;
	}

	NFloat GetYaw() const
	{
		NFloat value;
		NCheck(NFaceVerificationEventInfoGetYaw(GetHandle(), &value));
		return value;
	}

	NFloat GetRoll() const
	{
		NFloat value;
		NCheck(NFaceVerificationEventInfoGetRoll(GetHandle(), &value));
		return value;
	}

	NFloat GetPitch() const
	{
		NFloat value;
		NCheck(NFaceVerificationEventInfoGetPitch(GetHandle(), &value));
		return value;
	}

	NByte GetQuality() const
	{
		NByte value;
		NCheck(NFaceVerificationEventInfoGetQuality(GetHandle(), &value));
		return value;
	}

	::Neurotec::Geometry::NRect GetBoundingRect() const
	{
		::Neurotec::Geometry::NRect value;
		NCheck(NFaceVerificationEventInfoGetBoundingRect(GetHandle(), &value));
		return value;
	}

	NByte GetLivenessScore() const
	{
		NByte value;
		NCheck(NFaceVerificationEventInfoGetLivenessScore(GetHandle(), &value));
		return value;
	}

	NFaceVerificationStatus GetLastExtractionStatus() const
	{
		NFaceVerificationStatus value;
		NCheck(NFaceVerificationEventInfoGetLastExtractionStatus(GetHandle(), &value));
		return value;
	}

	NBool IsExtracting() const
	{
		NBool value;
		NCheck(NFaceVerificationEventInfoIsExtracting(GetHandle(), &value));
		return value;
	}

	void MarkForExtraction() const
	{
		NCheck(NFaceVerificationEventInfoMarkForExtraction(GetHandle()));
	}

	void FinishCapturing() const
	{
		NCheck(NFaceVerificationEventInfoFinishCapturing(GetHandle()));
	}
};

class NFaceVerification
{
	N_DECLARE_STATIC_OBJECT_CLASS(NFaceVerification)
	N_DECLARE_MODULE_CLASS(NFaceVerification)

public:
	class UserCollection : public ::Neurotec::Collections::NStaticCollectionBase<NFaceVerificationUser,
		NFaceVerificationGetUserCount, NFaceVerificationGetUser>
	{
		UserCollection()
		{
		}

		friend class NFaceVerification;
	public:
		void RemoveAt(NInt index)
		{
			NCheck(NFaceVerificationRemoveUserAt(index));
		}

		void Clear()
		{
			NCheck(NFaceVerificationClearUsers());
		}

		NInt IndexOf(const NStringWrapper & id) const
		{
			NInt index;
			NCheck(NFaceVerificationGetUserIndexByIdN(id.GetHandle(), &index));
			return index;
		}

		bool Contains(const NStringWrapper & id) const
		{
			return IndexOf(id) != -1;
		}

		NFaceVerificationUser GetById(const NStringWrapper & id) const
		{
			HNFaceVerificationUser hValue;
			NCheck(NFaceVerificationGetUserByIdN(id.GetHandle(), &hValue));
			return NObject::FromHandle<NFaceVerificationUser>(hValue);
		}
	};

	class CapturePreviewEventArgs : public EventArgs
	{
	private:
		HNFaceVerificationEventInfo hEventInfo;

	public:
		CapturePreviewEventArgs(HNFaceVerificationEventInfo hEventInfo, void * pParam)
			: EventArgs(NULL, pParam), hEventInfo(hEventInfo)
		{
		}

		NFaceVerificationEventInfo GetEventInfo() const
		{
			return NObject::FromHandle<NFaceVerificationEventInfo>(hEventInfo, false);
		}
	};

	template<typename F>
	class CapturePreviewEventHandler : public EventHandlerBase<F>
	{
	public:
		CapturePreviewEventHandler(F f)
			: EventHandlerBase<F>(f)
		{
		}

		static NResult N_API NativeCallback(HNFaceVerificationEventInfo hEventInfo, void * pParam)
		{
			NResult result = N_OK;
			try
			{
				CapturePreviewEventHandler<F> * pHandler = static_cast<CapturePreviewEventHandler *>(pParam);
				CapturePreviewEventArgs e(hEventInfo, pHandler->pParam);
				pHandler->callback(e);
			}
			N_EXCEPTION_CATCH_AND_SET_LAST(result);
			return result;
		}
	};

public:
	static NType NFaceVerificationStatusNativeTypeOf()
	{
		return NObject::GetObject<NType>(N_TYPE_OF(NFaceVerificationStatus));
	}

	static NType NFaceVerificationLivenessActionNativeTypeOf()
	{
		return NObject::GetObject<NType>(N_TYPE_OF(NFaceVerificationLivenessAction));
	}

	static NType NFaceVerificationLivenessModeNativeTypeOf()
	{
		return NObject::GetObject<NType>(N_TYPE_OF(NFaceVerificationLivenessMode));
	}

	NFaceVerification(const NStringWrapper & dbName, const NStringWrapper & password)
	{
		NCheck(NFaceVerificationInitializeN(dbName.GetHandle(), password.GetHandle()));
	}

	~NFaceVerification()
	{
	N_TRY_NR
		N_CHECK(NFaceVerificationUninitialize());
	N_TRY_ENR
	}

	NFaceVerificationStatus Enroll(const NStringWrapper & id, NInt timeout, const NPropertyBag & metadata)
	{
		NFaceVerificationStatus status;
		NCheck(NFaceVerificationEnrollN(id.GetHandle(), timeout, metadata.IsNull() ? NULL : metadata.GetHandle(), &status));
		return status;
	}

	NFaceVerificationStatus Verify(const NStringWrapper & id, NInt timeout)
	{
		NFaceVerificationStatus status;
		NCheck(NFaceVerificationVerifyN(id.GetHandle(), timeout, &status));
		return status;
	}

	void Cancel()
	{
		NCheck(NFaceVerificationCancel());
	}

	NByte GetQualityThreshold() const
	{
		NByte value;
		NCheck(NFaceVerificationGetQualityThreshold(&value));
		return value;
	}

	void SetQualityThreshold(NByte value)
	{
		NCheck(NFaceVerificationSetQualityThreshold(value));
	}

	NInt GetMatchingThreshold() const
	{
		NInt value;
		NCheck(NFaceVerificationGetMatchingThreshold(&value));
		return value;
	}

	void SetMatchingThreshold(NInt value)
	{
		NCheck(NFaceVerificationSetMatchingThreshold(value));
	}

	NByte GetLivenessThreshold() const
	{
		NByte value;
		NCheck(NFaceVerificationGetLivenessThreshold(&value));
		return value;
	}

	void SetLivenessThreshold(NByte value)
	{
		NCheck(NFaceVerificationSetLivenessThreshold(value));
	}

	NFaceVerificationLivenessMode GetLivenessMode() const
	{
		NFaceVerificationLivenessMode value;
		NCheck(NFaceVerificationGetLivenessMode(&value));
		return value;
	}

	void SetLivenessMode(NFaceVerificationLivenessMode value)
	{
		NCheck(NFaceVerificationSetLivenessMode(value));
	}

	NBool GetUseManualExtraction() const
	{
		NBool value;
		NCheck(NFaceVerificationGetUseManualExtraction(&value));
		return value;
	}

	void SetUseManualExtraction(NBool value)
	{
		NCheck(NFaceVerificationSetUseManualExtraction(value));
	}

	NInt GetLivenessBlinkTimeout() const
	{
		NInt value;
		NCheck(NFaceVerificationGetLivenessBlinkTimeout(&value));
		return value;
	}

	void SetLivenessBlinkTimeout(NInt value)
	{
		NCheck(NFaceVerificationSetLivenessBlinkTimeout(value));
	}

	NArrayWrapper<NString> GetAvailableCameraNames() const
	{
		HNString * arhNames;
		NInt nameCount;
		NCheck(NFaceVerificationGetAvailableCameraNamesN(&arhNames, &nameCount));
		return NArrayWrapper<NString>(arhNames, nameCount);
	}

	NString GetCamera() const
	{
		return NObject::GetString(NFaceVerificationGetCameraN);
	}

	void SetCamera(const NStringWrapper & value)
	{
		NObject::SetString(NFaceVerificationSetCameraN, value);
	}

	void AddCapturePreviewCallback(const NCallback & callback)
	{
		NCheck(NFaceVerificationAddCapturePreview(callback.GetHandle()));
	}

	template<typename F>
	NCallback AddCapturePreviewCallback(const F & callback, void * pParam = NULL)
	{
		NCallback cb = NTypes::CreateCallback<CapturePreviewEventHandler<F> >(callback, pParam);
		AddCapturePreviewCallback(cb);
		return cb;
	}

	void RemoveCapturePreviewCallback(const NCallback & callback)
	{
		NCheck(NFaceVerificationRemoveCapturePreview(callback.GetHandle()));
	}

	template<typename F>
	NCallback RemoveCapturePreviewCallback(const F & callback, void * pParam = NULL)
	{
		NCallback cb = NTypes::CreateCallback<CapturePreviewEventHandler<F> >(callback, pParam);
		RemoveCapturePreviewCallback(cb);
		return cb;
	}

	UserCollection GetUsers()
	{
		return UserCollection();
	}
};
}}

#endif // !N_FACE_VERIFICATION_HPP_INCLUDED
