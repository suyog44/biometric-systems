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

const NChar title[] = N_T("IIRecordFromNImage");
const NChar description[] = N_T("Demonstrates creation of IIRecord from image");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage: %s [IIRecord] [Standard] [Version] {[image...]}\n"), title);
	printf(N_T("\tIIRecord - output IIRecord\n"));
	printf(N_T("\t[Standard] - standard for the record (ISO or ANSI)\n"));
	printf(N_T("\t[Version] - version for the record\n"));
	printf(N_T("\t\t 1 - ANSI/INCITS 379-2004\n"));
	printf(N_T("\t\t 1 - ISO/IEC 19794-6:2005n"));
	printf(N_T("\t\t 2 - ISO/IEC 19794-6:2011\n"));
	printf(N_T("\timage    - one or more images\n"));
	return 1;
}

int main(int argc, NChar *argv[])
{
	HNImage hImage = NULL;
	HNImage hGrayscaleImage = NULL;
	HIIRecord hIIRecord = NULL;
	HIirIrisImage hIrisImage = NULL;
	HNBuffer hBuffer = NULL;
	NInt i;
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.Standards.Irises");
	NBool available = NFalse;
	NUInt width;
	NUInt height;
	BdifStandard standard = bsIso;
	NVersion standardVersion = IIR_VERSION_ISO_2_0;
	NBool isFirstVersion = NFalse;

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
		result = PrintErrorMsgWithLastError(N_T("NLicenseObtainComponents() failed, result = %d\n"), result);
		goto FINALLY;
	}

	if (!available)
	{
		printf(N_T("Licenses for %s not available\n"), components);
		result = N_E_NOT_ACTIVATED;
		goto FINALLY;
	}

	if (!strcmp(argv[2], N_T("ANSI")))
	{
		standard = bsAnsi;
	}
	else if (!strcmp(argv[2], N_T("ISO")))
	{
		standard = bsIso;
	}
	else
	{
		printf(N_T("wrong standard!"));
		result = N_E_FAILED;
		goto FINALLY;
	}

	if (!strcmp(argv[3], N_T("1")))
	{
		standardVersion = standard == bsAnsi ? IIR_VERSION_ANSI_1_0 : IIR_VERSION_ISO_1_0;
		isFirstVersion = NTrue;
	}
	else if (!strcmp(argv[3], N_T("2")))
	{
		if (standard != bsIso)
		{
			printf(N_T("standard and version is incompatible!"));
			result = N_E_FAILED;
			goto FINALLY;
		}
		standardVersion = IIR_VERSION_ISO_2_0;
	}
	else
	{
		printf(N_T("wrong version!"));
		result = N_E_FAILED;
		goto FINALLY;
	}

	for (i = 4; i < argc; i++)
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

		result = NImageGetWidth(hGrayscaleImage, &width);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NImageGetWidth, error code: %d\n"), result);
			goto FINALLY;
		}

		result = NImageGetHeight(hGrayscaleImage, &height);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NImageGetHeight, error code: %d\n"), result);
			goto FINALLY;
		}

		if (hIIRecord == NULL)
		{
			result = IIRecordCreateEx2(standard, standardVersion, 0, &hIIRecord);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in IIRecordCreateEx, error code: %d\n"), result);
				goto FINALLY;
			}

			if (isFirstVersion)
			{
				result = IIRecordSetRawImageWidth(hIIRecord, (NUShort)width);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetImageWidth, error code: %d\n"), result);
					goto FINALLY;
				}

				result = IIRecordSetRawImageHeight(hIIRecord, (NUShort)height);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetImageHeight, error code: %d\n"), result);
					goto FINALLY;
				}

				result = IIRecordSetIntensityDepth(hIIRecord, 8);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetIntensityDepth, error code: %d\n"), result);
					goto FINALLY;
				}
			}
		}

		result = IirIrisImageCreateEx(standard, standardVersion, &hIrisImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageCreate, error code: %d\n"), result);
			goto FINALLY;
		}

		if (!isFirstVersion)
		{
			result = IirIrisImageSetImageWidth(hIrisImage, (NUShort)width);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetImageWidth, error code: %d\n"), result);
				goto FINALLY;
			}

			result = IirIrisImageSetImageHeight(hIrisImage, (NUShort)height);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetImageHeight, error code: %d\n"), result);
				goto FINALLY;
			}

			result = IirIrisImageSetIntensityDepth(hIrisImage, 8);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetIntensityDepth, error code: %d\n"), result);
				goto FINALLY;
			}
		}

		result = IirIrisImageSetImage(hIrisImage, 0, hGrayscaleImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in IirIrisImageSetImage, error code: %d\n"), result);
			goto FINALLY;
		}

		result = IIRecordAddIrisImageEx(hIIRecord, hIrisImage, NULL);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in IIRecordAddIrisImage, error code: %d\n"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hImage);
		if NFailed((result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hGrayscaleImage);
		if NFailed((result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hIrisImage);
		if NFailed((result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	result = NObjectSaveToMemoryN(hIIRecord, 0, &hBuffer);
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

	printf(N_T("IIRecord successfully saved to file %s\n"), argv[1]);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hGrayscaleImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hIIRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hIrisImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
