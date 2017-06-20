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

const NChar title[] = N_T("FIRecordFromImage");
const NChar description[] = N_T("Demonstrates creation of FIRecord from image");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage: %s [FIRecord] [Standard] [Version] {[image]}\n"), title);
	printf(N_T("\tFIRecord - output FIRecord\n"));
	printf(N_T("\t[Standard] - standard for the record (ISO or ANSI)\n"));
	printf(N_T("\t[Version] - version for the record\n"));
	printf(N_T("\t\t 1 - ANSI/INCITS 381-2004\n"));
	printf(N_T("\t\t 1 - ISO/IEC 19794-4:2005n"));
	printf(N_T("\t\t 2 - ISO/IEC 19794-4:2011\n"));
	printf(N_T("\timage    - one or more images\n"));

	return 1;
}

int main(int argc, NChar *argv[])
{
	int i;
	HNImage hImage = NULL;
	HNImage hGrayscaleImage = NULL;
	HFIRecord hFIRecord = NULL;
	HFirFingerView hFirFingerView = NULL;
	HNBuffer hBuffer = NULL;
	NFloat vert_resolution;
	NFloat horz_resolution;
	NResult result = N_OK;
	const NChar * components = N_T("Biometrics.Standards.Fingers");
	NBool available = NFalse;
	BdifStandard standard = bsIso;
	NVersion standardVersion = FIR_VERSION_ISO_2_0;
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
		standardVersion = standard == bsAnsi ? FIR_VERSION_ANSI_1_0 : FIR_VERSION_ISO_1_0;
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
		standardVersion = FIR_VERSION_ISO_2_0;
	}
	else if (!strcmp(argv[3], N_T("2.5")))
	{
		if (standard != bsAnsi)
		{
			printf(N_T("standard and version is incompatible!"));
			result = N_E_FAILED;
			goto FINALLY;
		}
		standardVersion = FIR_VERSION_ANSI_2_5;
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

		result = NImageGetHorzResolution(hImage, &horz_resolution);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NImageGetHorzResolution, error code: %d\n"), result);
			goto FINALLY;
		}

		result = NImageGetVertResolution(hImage, &vert_resolution);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NImageGetVertResolution, error code: %d\n"), result);
			goto FINALLY;
		}

		if (horz_resolution < 250.0f)
		{
			horz_resolution = 500.0f;
		}
		if (vert_resolution < 250.0f)
		{
			vert_resolution = 500.0f;
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

		result = NImageSetHorzResolution(hGrayscaleImage, horz_resolution);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to set horizontal resolution, error %d\n"), result);
			goto FINALLY;
		}

		result = NImageSetVertResolution(hGrayscaleImage, vert_resolution);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to set vertical resolution, error %d\n"), result);
			goto FINALLY;
		}

		if (!hFIRecord)
		{
			result = FIRecordCreateEx(standard, standardVersion, 0, &hFIRecord);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FIRecordCreateEx, error code: %d\n"), result);
				goto FINALLY;
			}

			if (isFirstVersion)
			{
				result = FIRecordSetPixelDepth(hFIRecord, 8);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in FIRecordSetPixelDepth, error code: %d\n"), result);
					goto FINALLY;
				}

				result = FIRecordSetHorzImageResolution(hFIRecord, (NUShort)horz_resolution);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in FIRecordSetHorzImageResolution, error code: %d\n"), result);
					goto FINALLY;
				}

				result = FIRecordSetHorzScanResolution(hFIRecord, (NUShort)horz_resolution);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in FIRecordSetHorzScanResolution, error code: %d\n"), result);
					goto FINALLY;
				}

				result = FIRecordSetVertImageResolution(hFIRecord, (NUShort)vert_resolution);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in FIRecordSetVertImageResolution, error code: %d\n"), result);
					goto FINALLY;
				}

				result = FIRecordSetVertScanResolution(hFIRecord, (NUShort)vert_resolution);
				if(NFailed(result))
				{
					result = PrintErrorMsgWithLastError(N_T("error in FIRecordSetVertScanResolution, error code: %d\n"), result);
					goto FINALLY;
				}
			}
		}

		result = FirFingerViewCreate(standard, standardVersion, &hFirFingerView);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FirFingerViewCreate, error code: %d\n"), result);
			goto FINALLY;
		}

		if (!isFirstVersion)
		{
			result = FirFingerViewSetPixelDepth(hFirFingerView, 8);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FirFingerViewSetPixelDepth, error code: %d\n"), result);
				goto FINALLY;
			}

			result = FirFingerViewSetHorzScanResolution(hFirFingerView, (NUShort)horz_resolution);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FirFingerViewSetHorzScanResolution, error code: %d\n"), result);
				goto FINALLY;
			}

			result = FirFingerViewSetHorzImageResolution(hFirFingerView, (NUShort)horz_resolution);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FirFingerViewSetHorzImageResolution, error code: %d\n"), result);
				goto FINALLY;
			}

			result = FirFingerViewSetVertScanResolution(hFirFingerView, (NUShort)vert_resolution);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FirFingerViewSetVertScanResolution, error code: %d\n"), result);
				goto FINALLY;
			}

			result = FirFingerViewSetVertImageResolution(hFirFingerView, (NUShort)vert_resolution);
			if(NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in FirFingerViewSetVertImageResolution, error code: %d\n"), result);
				goto FINALLY;
			}
		}

		result = FIRecordAddFingerView(hFIRecord, hFirFingerView, NULL);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FIRecordAddFingerView, error code: %d\n"), result);
			goto FINALLY;
		}

		result = FirFingerViewSetImage(hFirFingerView, 0, hGrayscaleImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in FirFingerSetImage, error code: %d\n"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hGrayscaleImage);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		result = NObjectSet(NULL, &hFirFingerView);
		if(NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	result = NObjectSaveToMemoryN(hFIRecord, 0, &hBuffer);
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

	printf(N_T("FIRecord successfully saved to file %s\n"), argv[1]);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hGrayscaleImage);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFIRecord);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hFirFingerView);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if NFailed((result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2)) PrintErrorMsg(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();
	return result;
}
