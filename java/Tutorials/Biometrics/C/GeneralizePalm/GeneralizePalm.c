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

const NChar title[] = N_T("GeneralizePalm");
const NChar description[] = N_T("Demonstrates template creation and generalization of multiple palm prints.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2014-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [output template] [3 to 10 palm images]\n"), title);
	printf(N_T("\texample %s template image1.png image2.png image3.png\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNSubject hSubject = NULL;
	HNPalm hPalm = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBuffer hBuffer = NULL;
	HNString hString = NULL;

	NResult result = N_OK;
	const NChar * components = { N_T("Biometrics.PalmExtraction") };
	NBool available = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	const NInt sessionId = 1;
	NInt i;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 5)
	{
		OnExit();
		return usage();
	}

	// check the license first
	result = NLicenseObtainComponents(N_T("/local"), N_T("5000"), components, &available);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (!available)
	{
		printf(N_T("Licenses for %s not available\n"), components);
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}

	// create subject
	result = NSubjectCreate(&hSubject);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NSubjectCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 2; i < argc; i++)
	{
		// create palm for subject
		result = NPalmCreate(&hPalm);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NFignerCreate() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// set palm image filename
		result = NBiometricSetFileName(hPalm, argv[i]);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricSetFileName() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// mark for generalization by setting session id
		result = NBiometricSetSessionId(hPalm, sessionId);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NBiometricSetSessionId() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// add palm to subject
		result = NSubjectAddPalm(hSubject, hPalm, NULL);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectAddPalm() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// release palm reference
		result = NObjectSet(NULL, &hPalm);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// create biometric client
	result = NBiometricClientCreate(&hBiometricClient);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricClientCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create and generalize
	result = NBiometricEngineCreateTemplate(hBiometricClient, hSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus == nbsOk)
	{
		printf(N_T("generalization completed successfully\n"));

		// retrieve generalized template from subject
		result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NFileWriteAllBytesCN(argv[1], hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to write template to file (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("template saved successfully\n"));
	}
	else
	{
		// Retrieve biometric status
		result = NEnumToStringP(N_TYPE_OF(NBiometricStatus), biometricStatus, NULL, &hString);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NEnumToStringP() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NStringGetBuffer(hString, NULL, &szBiometricStatus);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NStringGetBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("template creation or generalization failed!\n"));
		printf(N_T("biometric status = %s.\n\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hPalm);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hString);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result2 = %d\n"), result2);
	}

	OnExit();
	return result;
}
