#include <TutorialUtils.h>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.h>
	#include <NMedia/NMedia.h>
	#include <NLicensing/NLicensing.h>
#else
	#include <NCore.h>
	#include <NMedia.h>
	#include <NLicensing.h>
#endif

const NChar title[] = N_T("WsqToNImage");
const NChar description[] = N_T("Demonstrates WSQ to NImage conversion");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2013-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [srcImage] [dstImage]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tsrcImage - filename of source WSQ image.\n"));
	printf(N_T("\tdstImage - name of a file to save converted image to.\n"));
	printf(N_T("\n"));
	return 1;
}

int main(int argc, NChar **argv)
{
	NResult result = N_OK;
	const NChar * components = N_T("Images.WSQ");
	NBool available = NFalse;
	HNImage hImage = NULL;
	HNImageFormat hImageFormat = NULL;
	HNImageFormat hDstImageFormat = NULL;
	HNImageInfo hImageInfo = NULL;
	NFloat bitRate = 0;

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

	// get WSQ image format
	result = NImageFormatGetWsqEx(&hImageFormat);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageFormatGetWsqEx() failed, result = %d\n"), result);
		goto FINALLY;
	}

	// create an NImage from a WSQ image file
	result = NImageCreateFromFileEx(argv[1], hImageFormat, 0, NULL, &hImage);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageCreateFromFileEx() failed, result = %d\n"), result);
		goto FINALLY;
	}

	result = NImageGetInfo(hImage, &hImageInfo);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageGetInfo() failed, result = %d\n"), result);
		goto FINALLY;
	}

	result = WsqInfoGetBitRate((HWsqInfo)hImageInfo, &bitRate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("WsqInfoGetBitRate() failed, result = %d\n"), result);
		goto FINALLY;
	}
	printf(N_T("loaded wsq bitrate: %.2f\n"), bitRate);

	// pick a format to save in, e.g. JPEG
	result = NImageFormatGetJpegEx(&hDstImageFormat);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageFormatGetJpegEx() failed, result = %d\n"), result);
		goto FINALLY;
	}

	// save image to specified file
	result = NImageSaveToFileEx(hImage, argv[2], hDstImageFormat, NULL, 0);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NImageSaveToFileEx() failed, result = %d\n"), result);
		goto FINALLY;
	}

	result = N_OK;
FINALLY:
	{
		NResult result2;

		result2 = NObjectSet(NULL, &hImage);
		if (NFailed(result2))
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hImageFormat);
		if (NFailed(result2))
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hDstImageFormat);
		if (NFailed(result2))
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NObjectSet(NULL, &hImageInfo);
		if (NFailed(result2))
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed, result = %d\n"), result2);
		result2 = NLicenseReleaseComponents(components);
		if (NFailed(result2))
			result = PrintErrorMsgWithLastError(N_T("NLicenseReleaseComponents() failed, result = %d\n"), result2);
	}

	OnExit();

	return result;
}
