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

const NChar title[] = N_T("NTemplateToCbeffRecord");
const NChar description[] = N_T("Converting NTemplate to CbeffRecord");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2014-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [NTemplate] [CbeffRecord] [PatronFormat]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[NTemplate] - filename of NTemplate\n"));
	printf(N_T("\t[CbeffRecord] - filename for CbeffRecord\n"));
	printf(N_T("\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation)\n"));
	return 1;
}

int main(int argc, NChar **argv)
{
	NResult result = N_OK;
	HNBuffer hBuffer = NULL;
	HNBuffer hCbeffBuffer = NULL;
	HCbeffRecord hCbeffRecord = NULL; // handle to CbeffRecord object
	const NChar * components = N_T("Biometrics.Standards.Base");
	NBool available = NFalse;
	NUInt bdbformat = 0;
	NUInt patronFormat = 0;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc != 4)
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

	// combine NTemplate BDB format
	bdbformat = BdifMakeFormat(CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NTEMPLATE);

	// get CbeffRecord patron format
	result = NUInt32Parse(argv[3], N_T("X"), &patronFormat);
	if (NFailed(result))
	{ 
		result = PrintErrorMsgWithLastError(N_T("NUInt32Parse() failed, result = %d\n"), result);
		goto FINALLY;
	}

	// create CbeffRecord from NTemplate buffer
	result = CbeffRecordCreateFromDataN(bdbformat, hBuffer, patronFormat, &hCbeffRecord);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("CbeffRecordCreateFromDataN() failed, result = %d\n"), result);
		goto FINALLY;
	}
	
	// get CbeffRecord buffer
	result = NObjectSaveToMemoryN(hCbeffRecord, 0, &hCbeffBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSaveToMemoryN() failed, result = %d\n"), result);
		goto FINALLY;
	}

	// save CbeffRecord buffer to file
	result = NFileWriteAllBytesCN(argv[2], hCbeffBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFileWriteAllBytesCN() failed, result = %d\n"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBuffer);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hCbeffBuffer);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hCbeffRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
