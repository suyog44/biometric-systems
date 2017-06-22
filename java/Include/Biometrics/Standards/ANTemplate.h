#ifndef AN_TEMPLATE_H_INCLUDED
#define AN_TEMPLATE_H_INCLUDED

#include <Biometrics/Standards/ANType1Record.h>
#include <Biometrics/Standards/ANType2Record.h>
#include <Biometrics/Standards/ANType3Record.h>
#include <Biometrics/Standards/ANType4Record.h>
#include <Biometrics/Standards/ANType5Record.h>
#include <Biometrics/Standards/ANType6Record.h>
#include <Biometrics/Standards/ANType7Record.h>
#include <Biometrics/Standards/ANType8Record.h>
#include <Biometrics/Standards/ANType9Record.h>
#include <Biometrics/Standards/ANType10Record.h>
#include <Biometrics/Standards/ANType13Record.h>
#include <Biometrics/Standards/ANType14Record.h>
#include <Biometrics/Standards/ANType15Record.h>
#include <Biometrics/Standards/ANType16Record.h>
#include <Biometrics/Standards/ANType17Record.h>
#include <Biometrics/Standards/ANType99Record.h>
#include <Images/NImage.h>
#include <Biometrics/NTemplate.h>

#ifdef N_CPP
extern "C"
{
#endif

N_DECLARE_OBJECT_TYPE(ANTemplate, NObject)

#define AN_TEMPLATE_VERSION_2_0 0x0200
#define AN_TEMPLATE_VERSION_2_1 0x0201
#define AN_TEMPLATE_VERSION_3_0 0x0300
#define AN_TEMPLATE_VERSION_4_0 0x0400

#define AN_TEMPLATE_VERSION_CURRENT AN_TEMPLATE_VERSION_4_0


NResult N_API ANTemplateGetVersionsEx(NVersion_ * arValue, NInt valueLength);
NResult N_API ANTemplateIsVersionSupported(NVersion_ version, NBool * pValue);

NResult N_API ANTemplateGetVersionNameN(NVersion_ version, HNString * phValue);

#define ANT_USE_NIST_MINUTIA_NEIGHBORS        0x00010000
#define ANT_LEAVE_INVALID_RECORDS_UNVALIDATED 0x00020000
#define ANT_USE_TWO_DIGIT_IDC                 0x00040000
#define ANT_USE_TWO_DIGIT_FIELD_NUMBER        0x00080000
#define ANT_USE_TWO_DIGIT_FIELD_NUMBER_TYPE_1 0x00100000

NResult N_API ANTemplateCreateEx(NVersion_ version, ANValidationLevel validationLevel, NUInt flags, HANTemplate * phTemplate);

NResult N_API ANTemplateCreateWithTransactionInformationN(NVersion_ version, HNString hTot, HNString hDai, HNString hOri, HNString hTcn, NUInt flags, HANTemplate * phTemplate);
#ifndef N_NO_ANSI_FUNC
NResult N_API ANTemplateCreateWithTransactionInformationA(NVersion_ version, const NAChar * szTot, const NAChar * szDai, const NAChar * szOri, const NAChar * szTcn, NUInt flags, HANTemplate * phTemplate);
#endif
#ifndef N_NO_UNICODE
NResult N_API ANTemplateCreateWithTransactionInformationW(NVersion_ version, const NWChar * szTot, const NWChar * szDai, const NWChar * szOri, const NWChar * szTcn, NUInt flags, HANTemplate * phTemplate);
#endif
#ifdef N_DOCUMENTATION
NResult N_API ANTemplateCreateWithTransactionInformation(NVersion_ version, const NChar * szTot, const NChar * szDai, const NChar * szOri, const NChar * szTcn, NUInt flags, HANTemplate * phTemplate);
#endif
#define ANTemplateCreateWithTransactionInformation N_FUNC_AW(ANTemplateCreateWithTransactionInformation)

NResult N_API ANTemplateCreateFromFileN(HNString hFileName, ANValidationLevel validationLevel, NUInt flags, HANTemplate * phTemplate);
#ifndef N_NO_ANSI_FUNC
NResult N_API ANTemplateCreateFromFileA(const NAChar * szFileName, ANValidationLevel validationLevel, NUInt flags, HANTemplate * phTemplate);
#endif
#ifndef N_NO_UNICODE
NResult N_API ANTemplateCreateFromFileW(const NWChar * szFileName, ANValidationLevel validationLevel, NUInt flags, HANTemplate * phTemplate);
#endif
#ifdef N_DOCUMENTATION
NResult N_API ANTemplateCreateFromFile(const NChar * szFileName, ANValidationLevel validationLevel, NUInt flags, HANTemplate * phTemplate);
#endif
#define ANTemplateCreateFromFile N_FUNC_AW(ANTemplateCreateFromFile)

NResult N_API ANTemplateCreateFromMemoryN(HNBuffer hBuffer, ANValidationLevel validationLevel, NUInt flags, NSizeType * pSize, HANTemplate * phTemplate);
NResult N_API ANTemplateCreateFromMemoryEx(const void * pBuffer, NSizeType bufferSize, ANValidationLevel validationLevel, NUInt flags, NSizeType * pSize, HANTemplate * phTemplate);
NResult N_API ANTemplateCreateFromStream(HNStream hStream, ANValidationLevel validationLevel, NUInt flags, HANTemplate * phTemplate);

NResult N_API ANTemplateCreateFromANTemplate(HANTemplate hSrcTemplate, NVersion_ version, NUInt flags, HANTemplate * phTemplate);


NResult N_API ANTemplateCreateFromNTemplateExN(NVersion_ version, HNString hTot, HNString hDai, HNString hOri, HNString hTcn,
	NBool type9RecordFmt, HNTemplate hNTemplate, NUInt flags, HANTemplate * phTemplate);
#ifndef N_NO_ANSI_FUNC
NResult N_API ANTemplateCreateFromNTemplateExA(NVersion_ version, const NAChar * szTot, const NAChar * szDai, const NAChar * szOri, const NAChar * szTcn,
	NBool type9RecordFmt, HNTemplate hNTemplate, NUInt flags, HANTemplate * phTemplate);
#endif
#ifndef N_NO_UNICODE
NResult N_API ANTemplateCreateFromNTemplateExW(NVersion_ version, const NWChar * szTot, const NWChar * szDai, const NWChar * szOri, const NWChar * szTcn,
	NBool type9RecordFmt, HNTemplate hNTemplate, NUInt flags, HANTemplate * phTemplate);
#endif
#ifdef N_DOCUMENTATION
NResult N_API ANTemplateCreateFromNTemplateEx(NVersion_ version, const NChar * szTot, const NChar * szDai, const NChar * szOri, const NChar * szTcn,
	NBool type9RecordFmt, HNTemplate hNTemplate, NUInt flags, HANTemplate * phTemplate);
#endif
#define ANTemplateCreateFromNTemplateEx N_FUNC_AW(ANTemplateCreateFromNTemplateEx)

NResult N_API ANTemplateValidate(HANTemplate hTemplate);

NResult N_API ANTemplateGetRecordCount(HANTemplate hTemplate, NInt * pValue);
NResult N_API ANTemplateGetRecordEx(HANTemplate hTemplate, NInt index, HANRecord * phValue);
NResult N_API ANTemplateGetRecordCapacity(HANTemplate hTemplate, NInt * pValue);
NResult N_API ANTemplateSetRecordCapacity(HANTemplate hTemplate, NInt value);
NResult N_API ANTemplateAddRecordEx(HANTemplate hTemplate, HANRecord hRecord, NInt * pIndex);

NResult N_API ANTemplateRemoveRecordAt(HANTemplate hTemplate, NInt index);
NResult N_API ANTemplateClearRecords(HANTemplate hTemplate);

NResult N_API ANTemplateSaveToFileN(HANTemplate hTemplate, HNString hFileName, NUInt flags);
#ifndef N_NO_ANSI_FUNC
NResult N_API ANTemplateSaveToFileA(HANTemplate hTemplate, const NAChar * szFileName, NUInt flags);
#endif
#ifndef N_NO_UNICODE
NResult N_API ANTemplateSaveToFileW(HANTemplate hTemplate, const NWChar * szFileName, NUInt flags);
#endif
#ifdef N_DOCUMENTATION
NResult N_API ANTemplateSaveToFile(HANTemplate hTemplate, const NChar * szFileName, NUInt flags);
#endif
#define ANTemplateSaveToFile N_FUNC_AW(ANTemplateSaveToFile)

NResult N_API ANTemplateToNTemplate(HANTemplate hTemplate, NUInt flags, HNTemplate * phNTemplate);

NResult N_API ANTemplateGetValidationLevel(HANTemplate hTemplate, ANValidationLevel * pValue);
NResult N_API ANTemplateGetVersion(HANTemplate hTemplate, NVersion_ * pValue);
NResult N_API ANTemplateSetVersion(HANTemplate hTemplate, NVersion_ value);

#ifdef N_CPP
}
#endif

#endif // !AN_TEMPLATE_H_INCLUDED
