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

const NChar title[] = N_T("ANTemplateToNTemplate");
const NChar description[] = N_T("Demonstrates ANTemplate conversion to NTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2009-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [ANTemplate] [NTemplate]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tANTemplate - filename with ANTemplate\n"));
	printf(N_T("\tNTemplate  - filename for NTemplate\n"));
	printf(N_T("example:\n"));
	printf(N_T("\t%s anTemplate.ANTemplate template.NTemplate\n"), title);

	return 1;
}

NResult ANTemplateFileToPackedNTemplate(
	NChar * szFileName, // pointer to string that specifies name of file were ANTemplate is stored
	HNBuffer * hBuffer // packed NTemplate buffer
)
{
	HANTemplate hANTemplate = NULL; // handle to ANTemplate object
	HNTemplate hNTemplate = NULL; // handle to NTemplate object
	NResult result;

	// Create ANTemplate object from file
	result = ANTemplateCreateFromFile(szFileName, anvlStandard, 0, &hANTemplate);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateCreateFromFile, error code: %d\n"), result);
		goto FINALLY;
	}

	// Convert ANTemplate object to NTemplate object
	result = ANTemplateToNTemplate(hANTemplate, 0, &hNTemplate);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateToNTemplate, error code: %d\n"), result);
		goto FINALLY;
	}

	// Pack NTemplate object
	result = NObjectSaveToMemoryN(hNTemplate, 0, hBuffer);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NObjectSaveToMemoryN, error code: %d\n"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hANTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hNTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
	}

	return result;
}

int main(int argc, NChar **argv)
{
	HNBuffer hBuffer = NULL;
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.Standards.FingerTemplates");
	NBool available = NFalse;

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
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed, result = %d\n"), result);
		goto FINALLY;
	}

	if (!available)
	{
		printf(N_T("Licenses for %s not available\n"), components);
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}

	result = ANTemplateFileToPackedNTemplate(argv[1], &hBuffer);
	if (NFailed(result))
	{
		goto FINALLY;
	}

	result = NFileWriteAllBytesCN(argv[2], hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileWriteAllBytesCN() failed, result = %d\n"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
