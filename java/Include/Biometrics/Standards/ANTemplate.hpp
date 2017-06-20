#include <Biometrics/Standards/ANRecord.hpp>

#ifndef AN_TEMPLATE_HPP_INCLUDED
#define AN_TEMPLATE_HPP_INCLUDED

#include <Biometrics/Standards/ANRecordType.hpp>
#include <Biometrics/Standards/ANType1Record.hpp>
#include <Biometrics/Standards/ANType2Record.hpp>
#include <Biometrics/Standards/ANType3Record.hpp>
#include <Biometrics/Standards/ANType4Record.hpp>
#include <Biometrics/Standards/ANType5Record.hpp>
#include <Biometrics/Standards/ANType6Record.hpp>
#include <Biometrics/Standards/ANType7Record.hpp>
#include <Biometrics/Standards/ANType8Record.hpp>
#include <Biometrics/Standards/ANType9Record.hpp>
#include <Biometrics/Standards/ANType10Record.hpp>
#include <Biometrics/Standards/ANType13Record.hpp>
#include <Biometrics/Standards/ANType14Record.hpp>
#include <Biometrics/Standards/ANType15Record.hpp>
#include <Biometrics/Standards/ANType16Record.hpp>
#include <Biometrics/Standards/ANType17Record.hpp>
#include <Biometrics/Standards/ANType99Record.hpp>
#include <Images/NImage.hpp>
#include <Biometrics/NTemplate.hpp>
namespace Neurotec { namespace Biometrics { namespace Standards
{
using ::Neurotec::Images::HNImage;
#include <Biometrics/Standards/ANTemplate.h>
}}}

namespace Neurotec { namespace Biometrics { namespace Standards
{
#include <Core/NNoDeprecate.h>

#undef AN_TEMPLATE_VERSION_2_0
#undef AN_TEMPLATE_VERSION_2_1
#undef AN_TEMPLATE_VERSION_3_0
#undef AN_TEMPLATE_VERSION_4_0

#undef AN_TEMPLATE_VERSION_CURRENT

#undef ANT_USE_NIST_MINUTIA_NEIGHBORS
#undef ANT_LEAVE_INVALID_RECORDS_UNVALIDATED
#undef ANT_USE_TWO_DIGIT_IDC
#undef ANT_USE_TWO_DIGIT_FIELD_NUMBER
#undef ANT_USE_TWO_DIGIT_FIELD_NUMBER_TYPE_1

const NVersion AN_TEMPLATE_VERSION_2_0(0x0200);
const NVersion AN_TEMPLATE_VERSION_2_1(0x0201);
const NVersion AN_TEMPLATE_VERSION_3_0(0x0300);
const NVersion AN_TEMPLATE_VERSION_4_0(0x0400);

const NVersion AN_TEMPLATE_VERSION_CURRENT(AN_TEMPLATE_VERSION_4_0);

const NUInt ANT_USE_NIST_MINUTIA_NEIGHBORS = 0x00010000;
const NUInt ANT_LEAVE_INVALID_RECORDS_UNVALIDATED = 0x00020000;
const NUInt ANT_USE_TWO_DIGIT_IDC = 0x00040000;
const NUInt ANT_USE_TWO_DIGIT_FIELD_NUMBER = 0x00080000;
const NUInt ANT_USE_TWO_DIGIT_FIELD_NUMBER_TYPE_1 = 0x00100000;

class ANTemplate : public NObject
{
	N_DECLARE_OBJECT_CLASS(ANTemplate, NObject)

public:
	class RecordCollection : public ::Neurotec::Collections::NCollectionBase<ANRecord, ANTemplate,
		ANTemplateGetRecordCount, ANTemplateGetRecordEx>
	{
		RecordCollection(const ANTemplate & owner)
		{
			SetOwner(owner);
		}

		friend class ANTemplate;
	public:
		NInt GetCapacity() const
		{
			NInt value;
			NCheck(ANTemplateGetRecordCapacity(this->GetOwnerHandle(), &value));
			return value;
		}

		void SetCapacity(NInt value)
		{
			NCheck(ANTemplateSetRecordCapacity(this->GetOwnerHandle(), value));
		}

		void RemoveAt(NInt index)
		{
			NCheck(ANTemplateRemoveRecordAt(this->GetOwnerHandle(), index));
		}

		void Clear()
		{
			NCheck(ANTemplateClearRecords(this->GetOwnerHandle()));
		}

		NInt Add(const ANRecord & value)
		{
			NInt index;
			NCheck(ANTemplateAddRecordEx(this->GetOwnerHandle(), value.GetHandle(), &index));
			return index;
		}
	};

private:
	static HANTemplate Create(NVersion version, ANValidationLevel validationLevel,  NUInt flags)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateEx(version.GetValue(), validationLevel, flags, &handle));
		return handle;
	}

	static HANTemplate Create(const NStringWrapper & tot, const NStringWrapper & dai, const NStringWrapper & ori, const NStringWrapper & tcn, NUInt flags)
	{
		return Create(AN_TEMPLATE_VERSION_4_0, tot, dai, ori, tcn, flags);
	}

	static HANTemplate Create(NVersion version, const NStringWrapper & tot, const NStringWrapper & dai, const NStringWrapper & ori, const NStringWrapper & tcn, NUInt flags)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateWithTransactionInformationN(version.GetValue(), tot.GetHandle(), dai.GetHandle(), ori.GetHandle(), tcn.GetHandle(), flags, &handle));
		return handle;
	}

	static HANTemplate Create(const NStringWrapper & fileName, ANValidationLevel validationLevel, NUInt flags)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateFromFileN(fileName.GetHandle(), validationLevel, flags, &handle));
		return handle;
	}

	static HANTemplate Create(const ::Neurotec::IO::NBuffer & buffer, ANValidationLevel validationLevel, NUInt flags, NSizeType * pSize)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateFromMemoryN(buffer.GetHandle(), validationLevel, flags, pSize, &handle));
		return handle;
	}

	static HANTemplate Create(const void * pBuffer, NSizeType bufferSize, ANValidationLevel validationLevel, NUInt flags, NSizeType * pSize)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateFromMemoryEx(pBuffer, bufferSize, validationLevel, flags, pSize, &handle));
		return handle;
	}

	static HANTemplate Create(const ::Neurotec::IO::NStream & stream, ANValidationLevel validationLevel, NUInt flags)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateFromStream(stream.GetHandle(), validationLevel, flags, &handle));
		return handle;
	}

	static HANTemplate Create(NVersion version, const NStringWrapper & tot, const NStringWrapper & dai, const NStringWrapper & ori, const NStringWrapper & tcn,
		bool type9RecordFmt, const NTemplate & nTemplate, NUInt flags)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateFromNTemplateExN(version.GetValue(), tot.GetHandle(), dai.GetHandle(), ori.GetHandle(), tcn.GetHandle(), type9RecordFmt ? NTrue : NFalse, nTemplate.GetHandle(), flags, &handle));
		return handle;
	}

	static HANTemplate Create(const ANTemplate & srcANTemplate, NVersion version, NUInt flags)
	{
		HANTemplate handle;
		NCheck(ANTemplateCreateFromANTemplate(srcANTemplate.GetHandle(), version.GetValue(), flags, &handle));
		return handle;
	}
public:
	static NType ANValidationLevelNativeTypeOf()
	{
		return NObject::GetObject<NType>(N_TYPE_OF(ANValidationLevel), true);
	}

	static NInt GetVersions(NVersion * arValue, NInt valueLength)
	{
		return NCheck(ANTemplateGetVersionsEx(reinterpret_cast<NVersion_ *>(arValue), valueLength));
	}

	static NArrayWrapper<NVersion> GetVersions()
	{
		NInt count = GetVersions(NULL, 0);
		NArrayWrapper<NVersion> values(count);
		count = GetVersions(values.GetPtr(), count);
		values.SetCount(count);
		return values;
	}

	static bool IsVersionSupported(const NVersion & version)
	{
		NBool value;
		NCheck(ANTemplateIsVersionSupported(version.GetValue(), &value));
		return value != 0;
	}

	static NString GetVersionName(const NVersion & version)
	{
		HNString hValue;
		NCheck(ANTemplateGetVersionNameN(version.GetValue(), &hValue));
		return NString(hValue, true);
	}

	explicit ANTemplate(NVersion version, ANValidationLevel validationLevel, NUInt flags = 0)
		: NObject(Create(version, validationLevel, flags), true)
	{
	}

	ANTemplate(NVersion version, const NStringWrapper & tot, const NStringWrapper & dai, const NStringWrapper & ori, const NStringWrapper & tcn, NUInt flags = 0)
		: NObject(Create(version, tot, dai, ori, tcn, flags), true)
	{
	}

	ANTemplate(const NStringWrapper & fileName, ANValidationLevel validationLevel, NUInt flags = 0)
		: NObject(Create(fileName, validationLevel, flags), true)
	{
	}

	ANTemplate(const ::Neurotec::IO::NBuffer & buffer, ANValidationLevel validationLevel, NUInt flags = 0, NSizeType * pSize = NULL)
		: NObject(Create(buffer, validationLevel, flags, pSize), true)
	{
	}

	ANTemplate(const void * pBuffer, NSizeType bufferSize, ANValidationLevel validationLevel, NUInt flags = 0, NSizeType * pSize = NULL)
		: NObject(Create(pBuffer, bufferSize, validationLevel, flags, pSize), true)
	{
	}

	ANTemplate(const ::Neurotec::IO::NStream & stream, ANValidationLevel validationLevel, NUInt flags = 0)
		: NObject(Create(stream, validationLevel, flags), true)
	{
	}

	ANTemplate(const ANTemplate & srcANTemplate, NVersion version, NUInt flags = 0)
		: NObject(Create(srcANTemplate, version, flags), true)
	{
	}

	ANTemplate(NVersion version, const NStringWrapper & tot, const NStringWrapper & dai, const NStringWrapper & ori, const NStringWrapper & tcn,
		bool type9RecordFmt, const NTemplate & nTemplate, NUInt flags = 0)
		: NObject(Create(version, tot, dai, ori, tcn, type9RecordFmt, nTemplate, flags), true)
	{
	}

	void Save(const NStringWrapper & fileName, NUInt flags = 0) const
	{
		NCheck(ANTemplateSaveToFileN(GetHandle(), fileName.GetHandle(), flags));
	}

	NTemplate ToNTemplate(NUInt flags = 0) const
	{
		HNTemplate hNTemplate;
		NCheck(ANTemplateToNTemplate(GetHandle(), flags, &hNTemplate));
		return FromHandle<NTemplate>(hNTemplate);
	}

	ANValidationLevel GetValidationLevel() const
	{
		ANValidationLevel value;
		NCheck(ANTemplateGetValidationLevel(GetHandle(), &value));
		return value;
	}

	NVersion GetVersion() const
	{
		NVersion_ value;
		NCheck(ANTemplateGetVersion(GetHandle(), &value));
		return NVersion(value);
	}

	void SetVersion(const NVersion & value)
	{
		NCheck(ANTemplateSetVersion(GetHandle(), value.GetValue()));
	}

	RecordCollection GetRecords()
	{
		return RecordCollection(*this);
	}

	const RecordCollection GetRecords() const
	{
		return RecordCollection(*this);
	}
};
#include <Core/NReDeprecate.h>
}}}

#endif // !AN_TEMPLATE_HPP_INCLUDED
