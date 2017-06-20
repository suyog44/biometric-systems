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

const NChar title[] = N_T("FCRecordFromNImage");
const NChar description[] = N_T("Demonstrates creation of FCRecord from image");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage: %s [FCRecord] {[image]}\n"), title);
	printf(N_T("\tFCRecord - output FCRecord\n"));
	printf(N_T("\timage    - one or more images\n"));

	return 1;
}

int main(int argc, NChar *argv[])
{
	const BdifStandard standard = bsIso;
	const NVersion standardVersion = FCR_VERSION_ISO_3_0;

	int i;
	HNImage hImage = NULL;
	HNImage hGrayscaleImage = NULL;
	HFCRecord hFCRecord = NULL;
	HFcrFaceImage new_face = NULL;
	HNBuffer hBuffer = NULL;
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.Standards.Faces");
	NBool available = NFalse;

	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
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

	for (i = 2; i < argc; i++)
	{
		result = NImageCreateFromFileEx(argv[i], NULL, 0, NULL, &hImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NImageCreateFromFile, error code: %d\n"), result);
			goto FINALLY;
		}

		result = NImagesRecolorImage(hImage, NPF_GRAYSCALE_8U, NULL, NULL, &hGrayscaleImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NImagesRecolorImage, error code: %d"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		if (!hFCRecord)
		{
			result = FCRecordCreateEx(standard, standardVersion, 20, &hFCRecord);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FCRecordCreate, error code: %d\n"), result);
				goto FINALLY;
			}
		}

		result = FcrFaceImageCreate(standard, standardVersion, &new_face);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FcrFaceImageCreate, error code: %d\n"), result);
			goto FINALLY;
		}

		result = FcrFaceImageSetFaceImageType(new_face, fcrfitBasic);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FcrFaceImageSetFaceImageType, error code: %d\n"), result);
			goto FINALLY;
		}

		result = FcrFaceImageSetImageDataType(new_face, fcridtJpeg);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FcrFaceImageSetImageDataType, error code: %d\n"), result);
			goto FINALLY;
		}

		result = FcrFaceImageSetImage(new_face, 0, hGrayscaleImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FcrFaceImageSetImage, error code: %d\n"), result);
			goto FINALLY;
		}

		result = FCRecordAddFaceImageEx(hFCRecord, new_face, NULL);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FCRecordAddFaceImageEx, error code: %d\n"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hGrayscaleImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &new_face);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	result = NObjectSaveToMemoryN(hFCRecord, 0, &hBuffer);
	if(NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NObjectSaveToMemoryN, error code: %d\n"), result);
		goto FINALLY;
	}

	result = NFileWriteAllBytesCN(argv[1], hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NFileWriteAllBytesCN, error code: %d\n"), result);
		goto FINALLY;
	}

	printf(N_T("FCRecord successfully saved to file %s\n"), argv[1]);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hGrayscaleImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFCRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &new_face);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
