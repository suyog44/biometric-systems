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

const NChar title[] = N_T("ANTemplateToNImage");
const NChar description[] = N_T("Demonstrates how to save images stored in ANTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2009-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [ANTemplate]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tANTemplate - filename of ANTemplate\n"));

	return 1;
}

int main(int argc, NChar **argv)
{
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.Standards.Base,Biometrics.Standards.PalmTemplates,Biometrics.Standards.Irises,Biometrics.Standards.Faces");
	NBool available = NFalse;
	HANTemplate hTemplate = NULL;
	HANRecord hRecord = NULL;
	HANRecordType hRecordType = NULL;
	HNImage hImage = NULL;
	NInt recordNumber;
	NInt count;
	NInt i;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
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

	result = ANTemplateCreateFromFile(argv[1], anvlStandard, 0, &hTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("ANTemplateCreateFromFile() failed, result = %d\n"), result);
		goto FINALLY;
	}

	result = ANTemplateGetRecordCount(hTemplate, &count);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("ANTemplateGetRecordCount() failed, result = %d\n"), result);
		goto FINALLY;
	}

	for (i = 0; i < count; i++)
	{
		NChar szFileName[1024];

		result = ANTemplateGetRecordEx(hTemplate, i, &hRecord);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("ANTemplateGetRecord() failed, result = %d\n"), result);
			goto FINALLY;
		}

		result = ANRecordGetRecordTypeEx(hRecord, &hRecordType);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("ANRecordGetRecordTypeEx() failed, result = %d\n"), result);
			goto FINALLY;
		}

		result = ANRecordTypeGetNumber(hRecordType, &recordNumber);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("ANRecordTypeGetNumber() failed, result = %d\n"), result);
			goto FINALLY;
		}

		if (recordNumber >= 3 && recordNumber <= 8 && recordNumber != 7)
		{
			result = ANImageBinaryRecordToNImage((HANImageBinaryRecord)hRecord, 0, &hImage);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("ANImageBinaryRecordToNImage() failed, result = %d\n"), result);
				goto FINALLY;
			}
		}
		else if (recordNumber >= 10 && recordNumber <= 17)
		{
			result = ANImageAsciiBinaryRecordToNImage((HANImageAsciiBinaryRecord)hRecord, 0, &hImage);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("ANImageAsciiBinaryRecordToNImage() failed, result = %d\n"), result);
				goto FINALLY;
			}
		}

		if (hImage)
		{
			sprintf(szFileName, N_T("record%d_type%d.jpg"), i + 1, recordNumber);
			result = NImageSaveToFileEx(hImage, szFileName, NULL, NULL, 0);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileEx() failed, result = %d\n"), result);
				goto FINALLY;
			}
			printf(N_T("image saved to %s\n"), szFileName);
		}

		// free hRecord
		result = NObjectSet(NULL, &hRecord);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free hRecordType
		result = NObjectSet(NULL, &hRecordType);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free hImage
		result = NObjectSet(NULL, &hImage);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hRecordType);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
