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

const NChar title[] = N_T("NTemplateToANTemplate");
const NChar description[] = N_T("Demonstrates creation of ANTemplate from NTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2009-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [NTemplate] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tNTemplate   - filename with NTemplate\n"));
	printf(N_T("\tANTemplate  - filename for NTemplate\n"));
	printf(N_T("\tTot - specifies type of transaction\n"));
	printf(N_T("\tDai - specifies destination agency identifier\n"));
	printf(N_T("\tOri - specifies originating agency identifier\n"));
	printf(N_T("\tTcn - specifies transaction control number\n"));

	return 1;
}

NResult NTemplateToANTemplateFile(
	HNBuffer hTemplateBuffer, // buffer that contains packed NTemplate
	NChar * szFileName, // pointer to string that specifies filename to store ANTemplate
	const NChar * szTot, // pointer to tring that specifies type of transaction
	const NChar * szDai, // pointer to string that specifies destination agency identifier
	const NChar * szOri, // pointer to string that specifies originating agency identifier
	const NChar * szTcn // pointer to string that specifies transaction control number
)
{
	HNTemplate hNTemplate = NULL; // handle to NTemplate object
	HANTemplate hANTemplate = NULL; // handle to ANTemplate object
	NResult result;

	// Create NTemplate object from packed NTemplate
	result = NTemplateCreateFromMemoryN(hTemplateBuffer, 0, NULL, &hNTemplate);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NTemplateCreateFromMemory, error code: %d\n"), result);
		goto FINALLY;
	}

	// Create ANTemplate object from NFRecord object
	result = ANTemplateCreateFromNTemplateEx(AN_TEMPLATE_VERSION_CURRENT, szTot, szDai, szOri, szTcn, NTrue, hNTemplate, N_OBJECT_REF_RET, &hANTemplate);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateCreateFromNTemplate, error code: %d\n"), result);
		goto FINALLY;
	}

	// Store ANTemplate object in file
	result = ANTemplateSaveToFile(hANTemplate, szFileName, 0);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in ANTemplateSaveToFile, error code: %d\n"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hNTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hANTemplate);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	return result;
}

int main(int argc, NChar **argv)
{
	NResult result = N_OK;
	HNBuffer hBuffer = NULL;
	// Depending on NTemplate contents choose the licenses: if you will have only finger templates in NTemplate - leave finger templates license only.
	const NChar * components = N_T("Biometrics.Standards.FingerTemplates,Biometrics.Standards.PalmTemplates");
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

	result = NFileReadAllBytesCN(argv[1], &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileReadAllBytesN() failed, result = %d\n"), result);
		goto FINALLY;
	}
	
	if ((strlen(argv[3]) < 3) || (strlen(argv[3]) > 4))
	{
		printf(N_T("tot parameter should be 3 or 4 characters length\n"));
		result = N_E_FAILED;
		goto FINALLY;
	}

	result = NTemplateToANTemplateFile(hBuffer, argv[2], argv[3], argv[4], argv[5], argv[6]);
	if(NFailed(result))
	{
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBuffer);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
