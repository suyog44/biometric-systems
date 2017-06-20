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

const NChar title[] = N_T("EnrollFingerFromImage");
const NChar description[] = N_T("Demonstrates fingerprint feature extraction from image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [image] [template] [format]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[image]    - image filename to extract.\n"));
	printf(N_T("\t[template] - filename to store extracted features.\n"));
	printf(N_T("\t[format]   - whether proprietary or standard template should be created.\n"));
	printf(N_T("\t\tIf not specified, proprietary Neurotechnology template is created (recommended).\n"));
	printf(N_T("\t\tANSI for ANSI/INCITS 378-2004\n"));
	printf(N_T("\t\tISO for ISO/IEC 19794-2\n"));
	printf(N_T("\n\nexamples:\n"));
	printf(N_T("\t%s image.jpg template.dat\n"), title);
	printf(N_T("\t%s image.jpg isoTemplate.dat ISO\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNSubject hSubject = NULL;
	HNFinger hFinger = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBuffer hBuffer = NULL;
	HNString hBiometricStatus = NULL;

	NResult result = N_OK;
	const NChar * components = { N_T("Biometrics.FingerExtraction") };
	NBool available = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	BdifStandard standard = bsUnspecified;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	if (argc > 3)
	{
		if (!strcmp(argv[3], N_T("ANSI")))
		{
			standard = bsAnsi;
		}
		else if (!strcmp(argv[3], N_T("ISO")))
		{
			standard = bsIso;
		}
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

	// create finger for the subject
	result = NFingerCreate(&hFinger);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NFingerCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// read and set the image for the finger
	result = NBiometricSetFileName(hFinger, argv[1]);
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

	{
		NTemplateSize templateSize = ntsLarge;

		// set template size to large
		result = NObjectSetPropertyP(hBiometricClient, N_T("Fingers.TemplateSize"), N_TYPE_OF(NTemplateSize), naNone, &templateSize, sizeof(templateSize), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// create the template
	result = NBiometricEngineCreateTemplate(hBiometricClient, hSubject, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTemplate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus == nbsOk)
	{
		printf(N_T("%s template extracted\n"), standard == bsIso ? N_T("ISO") : standard == bsAnsi ? N_T("ANSI") : N_T("Proprietary"));

		// retrieve the template from subject
		if (standard == bsIso)
			result = NSubjectGetTemplateBufferWithFormatEx(hSubject,
			CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS,
			CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_RECORD_FORMAT,
			FMR_VERSION_ISO_CURRENT,
			&hBuffer);
		else if (standard == bsAnsi)
			result = NSubjectGetTemplateBufferWithFormatEx(hSubject,
			CBEFF_BO_INCITS_TC_M1_BIOMETRICS,
			CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_MINUTIAE_U,
			FMR_VERSION_ANSI_CURRENT,
			&hBuffer);
		else
			result = NSubjectGetTemplateBuffer(hSubject, &hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectGetTemplateBuffer() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NFileWriteAllBytesCN(argv[2], hBuffer);
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
		printf(N_T("biometric status = %s.\n\n"), szBiometricStatus);

		result = N_E_FAILED;
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFinger);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result2 = %d\n"), result2);
	}

	OnExit();
	return result;
}
