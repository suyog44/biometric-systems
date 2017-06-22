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

const NChar title[] = N_T("ANTemplateType9FromNImage");
const NChar description[] = N_T("Demonstrates creation of ANTemplate with type 9 record in it");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2009-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tNImage     - filename with image file\n"));
	printf(N_T("\tANTemplate - filename for ANTemplate\n"));
	printf(N_T("\tTot - specifies type of transaction\n"));
	printf(N_T("\tDai - specifies destination agency identifier\n"));
	printf(N_T("\tOri - specifies originating agency identifier\n"));
	printf(N_T("\tTcn - specifies transaction control number\n"));

	return 1;
}

NResult ANTemplateAddRecordType9(
	NChar * szFileNameIn, // pointer to string that specifies filename of image file
	NChar * szFileNameOut, // pointer to string that specifies filename to store ANTemplate
	const NChar * szTot, // pointer to tring that specifies type of transaction
	const NChar * szDai, // pointer to string that specifies destination agency identifier
	const NChar * szOri, // pointer to string that specifies originating agency identifier
	const NChar * szTcn // pointer to string that specifies transaction control number
	)
{
	HANTemplate hANTemplate = NULL; // handle to ANTemplate object
	HANType9Record hRecord = NULL; // handle to ANRecord object
	HNSubject hSubject = NULL;
	HNFinger hFinger = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HNError hError = NULL;
	HNString hErrorString = NULL;
	HNString hBiometricStatus = NULL;
	HNTemplate hTemplate = NULL;
	HNFTemplate hNFTemplate = NULL;
	HNFRecord hNFRecord = NULL; //hande to NFRecord object

	NResult result;
	NInt idc = 0;
	NBool fmt = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	const NChar * szError = NULL;

	// create subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create finger for the subject
	result = NFingerCreate(&hFinger);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFingerCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// read and set the image for the finger
	result = NBiometricSetFileName(hFinger, szFileNameIn);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileNameN() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set the finger for the subject
	result = NSubjectAddFinger(hSubject, hFinger, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectAddFinger() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create the template
	result = NBiometricEngineCreateTemplate(hBiometricClient, hSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus != nbsOk)
	{
		// retrieve the error message
		result = NBiometricTaskGetError(hBiometricTask, &hError);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetError() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NObjectToStringN(hError, NULL, &hErrorString);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectToStringN() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NStringGetBuffer(hErrorString, NULL, &szError);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

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
		printf(N_T("biometric status: %s.\n"), szBiometricStatus);
		printf(N_T("error message: \n%s.\n"), szError);

		result = N_E_FAILED;
		goto FINALLY;
	}

	// retrieve template
	result = NSubjectGetTemplate(hSubject, &hTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	result = NTemplateGetFingersEx(hTemplate, &hNFTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NTemplateGetFingersEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve reocrd
	result = NFTemplateGetRecordEx(hNFTemplate, 0, &hNFRecord);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFTemplateGetRecordEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// Create empty ANTemplate object with only type 1 record in it
	result = ANTemplateCreateWithTransactionInformation(AN_TEMPLATE_VERSION_CURRENT, szTot, szDai, szOri, szTcn, 0, &hANTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateCreate, error code: %d"), result);
		goto FINALLY;
	}

	// Create Type 9 record
	result = ANType9RecordCreateFromNFRecord(AN_TEMPLATE_VERSION_CURRENT, idc, fmt, hNFRecord, 0, &hRecord);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANType9RecordCreateFromNFRecord, error code: %d"), result);
		goto FINALLY;
	}

	// Add Type 9 record to ANTemplate object
	result = ANTemplateAddRecordEx(hANTemplate, hRecord, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateAddRecordEx, error code: %d"), result);
		goto FINALLY;
	}

	// Store ANTemplate object with type 9 record in file
	result = ANTemplateSaveToFile(hANTemplate, szFileNameOut, 0);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateSaveToFile, error code: %d"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hANTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hSubject);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFinger);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hError);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hErrorString);
		if NFailed((result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if NFailed((result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hNFTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hNFRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	return result;
}

int main(int argc, NChar **argv)
{
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.FingerExtraction,Biometrics.Standards.FingerTemplates");
	NBool available = NFalse;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 7)
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

	result = ANTemplateAddRecordType9(argv[1], argv[2], argv[3], argv[4], argv[5], argv[6]);
	if (NFailed(result))
	{
		goto FINALLY;
	}

	result = N_OK;

FINALLY:
	{
		NResult result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
