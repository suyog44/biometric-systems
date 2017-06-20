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

const NChar title[] = N_T("ANTemplateType17FromNImage");
const NChar description[] = N_T("Demonstrates creation of ANTemplate with type 17 record in it");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2009-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn] [Src]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tNImage     - filename with image file\n"));
	printf(N_T("\tANTemplate - filename for ANTemplate\n"));
	printf(N_T("\tTot - specifies type of transaction\n"));
	printf(N_T("\tDai - specifies destination agency identifier\n"));
	printf(N_T("\tOri - specifies originating agency identifier\n"));
	printf(N_T("\tTcn - specifies transaction control number\n"));
	printf(N_T("\tSrc - specifies source agency number\n"));

	return 1;
}

NResult ANTemplateAddRecordType17(
	NChar * szFileNameIn, // pointer to string that specifies filename of image file
	NChar * szFileNameOut, // pointer to string that specifies filename to store ANTemplate
	const NChar * szTot, // pointer to tring that specifies type of transaction
	const NChar * szDai, // pointer to string that specifies destination agency identifier
	const NChar * szOri, // pointer to string that specifies originating agency identifier
	const NChar * szTcn, // pointer to string that specifies transaction control number
	const NChar * szSrc	// pointer to string that specifies source agency number
	)
{
	HANTemplate hANTemplate = NULL; // handle to ANTemplate object
	HNImage hImage = NULL; // handle to NImage object
	HNImage hRGBImage = NULL; // handle to RGB object
	HANType17Record hRecord = NULL; // handle to ANRecord object
	NResult result;
	NInt idc = 0;
	ANImageCompressionAlgorithm cga = anicaNone; // image compression algortihm currently none
	BdifScaleUnits slc = bsuNone;

	// Create empty ANTemplate object with only type 1 record in it
	result = ANTemplateCreateWithTransactionInformation(AN_TEMPLATE_VERSION_CURRENT, szTot, szDai, szOri, szTcn, 0, &hANTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateCreateWithTransactionInformation, error code: %d"), result);
		goto FINALLY;
	}

	// Create NImage object from image file
	result = NImageCreateFromFileEx(szFileNameIn, NULL, 0, NULL, &hImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NImageCreateFromFile, error code: %d"), result);
		goto FINALLY;
	}

	result = NImageSetResolutionIsAspectRatio(hImage, NTrue);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NImageSetResolutionIsAspectRatio, error code: %d"), result);
		goto FINALLY;
	}

	// create RGB image from an image
	result = NImagesRecolorImage(hImage, NPF_RGB_8U, NULL, NULL, &hRGBImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NImagesRecolorImage, error code: %d"), result);
		goto FINALLY;
	}

	// Create Type 17 record
	result = ANType17RecordCreateFromNImage(AN_TEMPLATE_VERSION_CURRENT, idc, szSrc, slc, cga, hRGBImage, 0, &hRecord);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANType17RecordCreateFromNImage, error code: %d"), result);
		goto FINALLY;
	}

	// Add Type 17 record to ANTemplate object
	result = ANTemplateAddRecordEx(hANTemplate, hRecord, NULL);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateAddRecordEx, error code: %d"), result);
		goto FINALLY;
	}

	// Store ANTemplate object with type 17 record in file
	result = ANTemplateSaveToFile(hANTemplate, szFileNameOut, 0);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateSaveToFile, error code: %d"), result);
		goto FINALLY;
	}

	printf(N_T("template saved to %s"), szFileNameOut);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hANTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hRGBImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	return result;
}

int main(int argc, NChar **argv)
{
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.Standards.Irises");
	NBool available = NFalse;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 8)
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

	result = ANTemplateAddRecordType17(argv[1], argv[2], argv[3], argv[4], argv[5], argv[6], argv[7]);
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
