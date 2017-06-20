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

const NChar title[] = N_T("CreateMinexCompliantTemplate");
const NChar description[] = N_T("Demonstrates creation of Minex compliant fingerprint record (FMRecord) from image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [image] [template] [format]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[image]    - image filename to extract.\n"));
	printf(N_T("\t[template] - FMRecord to store extracted features.\n"));
	printf(N_T("\t[DoNotUseNeurotecCbeffProductId] - 1 if cbeffProductId should be not used (set as 0); otherwise, cbeffProductId is set as Neurotechnology's product Id.\n"));
	printf(N_T("\t\tIf not specified, cbeffProductId is set as Neurotechnology's product Id.\n"));
	printf(N_T("\n\nexamples:\n"));
	printf(N_T("\t%s image.jpg fmrecord.FMRecord 0\n"), title);

	return 1;
}

int main(int argc, NChar **argv)
{
	HNSubject hSubject = NULL;
	HNFinger hFinger = NULL;
	HNFAttributes hFingerAttributes = NULL;
	HNBiometricClient hBiometricClient = NULL;
	HNBiometricTask hBiometricTask = NULL;
	HFMRecord hRecord = NULL;
	HFMRecord hFingerView = NULL;
	HNBuffer hBuffer = NULL;
	HNString hBiometricStatus = NULL;

	NResult result = N_OK;
	const NChar * components = { N_T("Biometrics.FingerExtraction,Biometrics.FingerQualityAssessmentBase,Biometrics.Standards.FingerTemplates") };
	NBool available = NFalse;
	NBiometricStatus biometricStatus = nbsNone;
	const NChar * szBiometricStatus = NULL;
	NfiqQuality nfiq;
	NByte quality;
	NBool doNotUseNeurotecCbeffProduct = NFalse;
	NUInt flags = 0;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	if (argc > 3)
	{
		if (!strcmp(argv[3], N_T("1")))
		{
			doNotUseNeurotecCbeffProduct = NTrue;
			flags = BDIF_DO_NOT_CHECK_CBEFF_PRODUCT_ID;
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

	{
		NBool parameter = NTrue;

		// set parameter to calculate NFIQ value
		result = NObjectSetPropertyP(hBiometricClient, N_T("Fingers.CalculateNfiq"), N_TYPE_OF(NBoolean), naNone, &parameter, sizeof(parameter), 1, NTrue);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSetProperty() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	// create biometric task
	result = NBiometricEngineCreateTask(hBiometricClient, nboCreateTemplate | nboAssessQuality, hSubject, NULL, &hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEngineCreateTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// perform the biometric task
	result = NBiometricEnginePerformTask(hBiometricClient, hBiometricTask);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricEnginePerformTask() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// retrieve the status of the biometric task
	result = NBiometricTaskGetStatus(hBiometricTask, &biometricStatus);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetStatus() failed (result = %d)!"), result);
		goto FINALLY;
	}

	if (biometricStatus == nbsOk)
	{
		printf(N_T("ANSI template extracted\n"));

		// retrieve the NFIQ quality value
		{
			// free unneeded hFinger
			result = NObjectSet(NULL, &hFinger);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// retrieve hFinger at index 0
			result = NSubjectGetFinger(hSubject, 0, &hFinger);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NSubjectGetFinger() failed (result = %d)!"), result);
				goto FINALLY;
			}
			// retrieve finger attributes
			result = NFrictionRidgeGetObject(hFinger, 0, &hFingerAttributes);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFrictionRidgeGetObject() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// retrieve NFIQ
			result = NFAttributesGetNfiqQuality(hFingerAttributes, &nfiq);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFAttributesGetNfiqQuality() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}

		// retrieve the template from subject
		result = NSubjectToFMRecordEx(hSubject, bsAnsi, FMR_VERSION_ANSI_2_0, flags, &hRecord);	
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NSubjectToFMRecordEx() failed (result = %d)!"), result);
			goto FINALLY;
		}
		// do not use Cbeff product Id
		if (doNotUseNeurotecCbeffProduct)
		{
			result = FMRecordSetCbeffProductId(hRecord, 0);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("FMRecordSetCbeffProductId() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}

		// retrieve hFingerView at index 0
		result = FMRecordGetFingerView(hRecord, 0, &hFingerView);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("FMRecordGetFingerView() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// Calculates the quality (6 - NFIQ) * 20 of a given fingerprint image
		quality = (NByte)((6 - nfiq) * 20);

		// set quality value to fingerview
		result = FmrFingerViewSetFingerQuality(hFingerView, quality);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("FmrFingerViewSetFingerQuality() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// save FMRecord
		result = NObjectSaveToMemoryN(hRecord, 0, &hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSaveToMemoryN() failed (result = %d)!"), result);
			goto FINALLY;
		}

		{
			NSizeType length;
			result = NBufferGetSize(hBuffer, &length);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NBufferGetSize() failed (result = %d)!"), result);
				goto FINALLY;
			}
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

		// retrieve the error message
		{
			HNError hError = NULL;
			result = NBiometricTaskGetError(hBiometricTask, &hError);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NBiometricTaskGetError() failed (result = %d)!"), result);
				goto FINALLY;
			}
			result = N_E_FAILED;
			if (hError)
			{
				result = PrintErrorMsgWithError(N_T("task error:\n"), hError);
				{
					NResult result2 = NObjectSet(NULL, &hError);
					if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
				}
			}
		}
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hSubject);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFinger);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFingerAttributes);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hRecord);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFingerView);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricClient);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBiometricTask);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NStringSet(NULL, &hBiometricStatus);
		if (NFailed(result2)) PrintErrorMsg(N_T("NStringSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result2 = %d\n"), result2);
	}

	OnExit();
	return result;
}
