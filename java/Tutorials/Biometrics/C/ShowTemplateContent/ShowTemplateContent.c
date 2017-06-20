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

const NChar title[] = N_T("ShowTemplateContent");
const NChar description[] = N_T("Demonstrates methods and functions to access internal template information.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [NTemplate] ...\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[NTemplate]  - NTemplate filename.\n"));
	return 1;
}

NInt RotationToDegrees(NInt rotation)
{
	return (2 * rotation * 360 + 256) / (2 * 256);
}

const NChar* NFImpressionTypeToString(NFImpressionType impressionType)
{
	switch (impressionType)
	{
		case nfitLatentImpression:
			return N_T("Latent impression fingerprint.");
		case nfitLatentLift:
			return N_T("Latent lift fingerprint.");
		case nfitLatentPhoto:
			return N_T("Latent photo fingerprint.");
		case nfitLatentTracing:
			return N_T("Latent tracing fingerprint.");
		case nfitLiveScanContactless:
			return N_T("Live-scanned fingerprint using contactless device.");
		case nfitLiveScanPlain:
			return N_T("Live-scanned plain fingerprint.");
		case nfitLiveScanRolled:
			return N_T("Live-scanned rolled fingerprint.");
		case nfitNonliveScanPlain:
			return N_T("Nonlive-scanned (from paper) plain fingerprint.");
		case nfitNonliveScanRolled:
			return N_T("Nonlive-scanned (from paper) rolled fingerprint.");
		case nfitSwipe:
			return N_T("Live-scanned fingerprint by sliding the finger across a \"swipe\" sensor.");
		default:
			return N_T("n/a");
	}
}

const NChar* NFPatternClassToString(NFPatternClass patternClass)
{
	switch (patternClass)
	{
	case nfpcAccidentalWhorl:
		return N_T("Accidental whorl pattern class.");
	case nfpcAmputation:
		return N_T("Amputation. Pattern class is not available.");
	case nfpcCentralPocketLoop:
		return N_T("Central pocket loop pattern class.");
	case nfpcDoubleLoop:
		return N_T("Double loop pattern class.");
	case nfpcLeftSlantLoop:
		return N_T("Left slant loop pattern class.");
	case nfpcPlainArch:
		return N_T("Plain arch pattern class.");
	case nfpcPlainWhorl:
		return N_T("Plain whorl pattern class.");
	case nfpcRadialLoop:
		return N_T("Radial loop pattern class.");
	case nfpcRightSlantLoop:
		return N_T("Right slant loop pattern class.");
	case nfpcScar:
		return N_T("Scar. Pattern class is not available.");
	case nfpcTentedArch:
		return N_T("Tented arch pattern class.");
	case nfpcUlnarLoop:
		return N_T("Ulnar loop pattern class.");
	case nfpcUnknown:
		return N_T("Unknown pattern class.");
	case nfpcWhorl:
		return N_T("Whorl pattern class.");
	default:
		return N_T("n/a");
	}
}

const NChar* NFPositionToString(NFPosition position)
{
	switch (position)
	{
	case nfpLeftIndex:
		return N_T("Index finger of the left hand.");
	case nfpLeftLittle:
		return N_T("Little finger of the left hand.");
	case nfpLeftMiddle:
		return N_T("Middle finger of the left hand.");
	case nfpLeftRing:
		return N_T("Ring finger of the left hand.");
	case nfpLeftThumb:
		return N_T("Thumb of the left hand.");
	case nfpRightIndex:
		return N_T("Index finger of the right hand.");
	case nfpRightLittle:
		return N_T("Little finger of the right hand.");
	case nfpRightMiddle:
		return N_T("Middle finger of the right hand.");
	case nfpRightRing:
		return N_T("Ring finger of the right hand.");
	case nfpRightThumb:
		return N_T("Thumb of the right hand.");
	case nfpUnknown:
		return N_T("Unknown finger.");
	default:
		return N_T("n/a");
	}
}

const NChar* NFRidgeCountsTypeToString(NFRidgeCountsType ridgeCountsType)
{
	switch (ridgeCountsType)
	{
	case nfrctEightNeighbors:
		return N_T("The NFRecord contains ridge counts to closest minutia in each of the eight sectors of each minutia. First sector starts at minutia angle.");
	case nfrctEightNeighborsWithIndexes:
		return N_T("The NFRecord contains ridge counts to eight neighbors of each minutia.");
	case nfrctFourNeighbors:
		return N_T("The NFRecord contains ridge counts to closest minutia in each of the four sectors of each minutia. First sector starts at minutia angle.");
	case nfrctFourNeighborsWithIndexes:
		return N_T("The NFRecord contains ridge counts to four neighbors of each minutia.");
	case nfrctNone:
		return N_T("The NFRecord does not contain ridge counts.");
	case nfrctUnspecified:
		return N_T("For internal use.");
	default:
		return N_T("n/a");
	}
}

const NChar* NFMinutiaTypeToString(NFMinutiaType minutiaType)
{
	switch (minutiaType)
	{
	case nfmtBifurcation:
		return N_T("The minutia that is a bifurcation of a ridge.");
	case nfmtEnd:
		return N_T("The minutia that is an end of a ridge.");
	case nfmtUnknown:
		return N_T("The type of the minutia is unknown.");
	default:
		return N_T("n/a");
	}
}

void PrintNFRecord(HNFRecord hnfRec)
{
	NResult result;
	NUShort ushortVal;
	NByte byteVal;
	NFImpressionType impressionType;
	NFPatternClass patternClass;
	NFPosition position;
	NFMinutiaFormat minutiaFormat;
	NFRidgeCountsType ridgeCountsType;
	NInt featureCount;
	NInt i;
	NSizeType size;

	result = NFRecordGetG(hnfRec, &byteVal);
	if (NSucceeded(result))
	{
		printf(N_T("\tG: %d\n"), byteVal);
	}

	result = NFRecordGetImpressionType(hnfRec, &impressionType);
	if (NSucceeded(result))
	{
		printf(N_T("\timpression type: %s\n"), NFImpressionTypeToString(impressionType));
	}

	result = NFRecordGetPatternClass(hnfRec, &patternClass);
	if (NSucceeded(result))
	{
		printf(N_T("\tpattern class: %s\n"), NFPatternClassToString(patternClass));
	}

	result = NFRecordGetCbeffProductType(hnfRec, &ushortVal);
	if (NSucceeded(result))
	{
		printf(N_T("\tcbeff product type: %d\n"), ushortVal);
	}

	result = NFRecordGetPosition(hnfRec, &position);
	if (NSucceeded(result))
	{
		printf(N_T("\tposition: %s\n"), NFPositionToString(position));
	}

	result = NFRecordGetRidgeCountsType(hnfRec, &ridgeCountsType);
	if (NSucceeded(result))
	{
		printf(N_T("\tridge counts type: %s\n"), NFRidgeCountsTypeToString(ridgeCountsType));
	}

	result = NFRecordGetWidth(hnfRec, &ushortVal);
	if (NSucceeded(result))
	{
		printf(N_T("\twidth: %d\n"), ushortVal);
	}

	result = NFRecordGetHeight(hnfRec, &ushortVal);
	if (NSucceeded(result))
	{
		printf(N_T("\theight: %d\n"), ushortVal);
	}

	result = NFRecordGetHorzResolution(hnfRec, &ushortVal);
	if (NSucceeded(result))
	{
		printf(N_T("\thorizontal resolution: %d\n"), ushortVal);
	}

	result = NFRecordGetVertResolution(hnfRec, &ushortVal);
	if (NSucceeded(result))
	{
		printf(N_T("\tvertical resolution: %d\n"), ushortVal);
	}

	result = NFRecordGetQuality(hnfRec, &byteVal);
	if (NSucceeded(result))
	{
		printf(N_T("\tquality: %d\n"), byteVal);
	}

	result = NObjectGetSize(hnfRec, 0, &size);
	if (NSucceeded(result))
	{
		printf(N_T("\tsize: %ld\n"), (unsigned long)size);
	}

	result = NFRecordGetMinutiaFormat(hnfRec, &minutiaFormat);

	/* minutiae */
	result = NFRecordGetMinutiaCount(hnfRec, &featureCount);
	if (NSucceeded(result))
	{
		NFMinutia minutia;

		printf(N_T("\tminutia count: %d\n"), featureCount);

		for (i = 0; i < featureCount; i++)
		{
			result = NFRecordGetMinutia(hnfRec, i, &minutia);
			if (NSucceeded(result))
			{
				printf(N_T("\t\tminutia %d of %d:\n"), i+1, featureCount);
				printf(N_T("\t\tx: %d\n"), minutia.X);
				printf(N_T("\t\ty: %d\n"), minutia.Y);
				printf(N_T("\t\tangle: %d\n"), RotationToDegrees(minutia.Angle));
				printf(N_T("\t\ttype: %s\n"), NFMinutiaTypeToString(minutia.Type));
				if (minutiaFormat & nfmfHasQuality)
				{
					printf(N_T("\t\tquality: %d\n"), minutia.Quality);
				}
				if (minutiaFormat & nfmfHasG)
				{
					printf(N_T("\t\tg: %d\n"), minutia.G);
				}
				if (minutiaFormat & nfmfHasCurvature)
				{
					printf(N_T("\t\tcurvature: %d\n"), minutia.Curvature);
				}
				printf(N_T("\n"));
			}
		}
	}

	/* deltas */
	result = NFRecordGetDeltaCount(hnfRec, &featureCount);
	if (NSucceeded(result))
	{
		NFDelta delta;

		printf(N_T("\tdelta count: %d\n"), featureCount);

		for (i = 0; i < featureCount; i++)
		{
			result = NFRecordGetDelta(hnfRec, i, &delta);
			if (NSucceeded(result))
			{
				printf(N_T("\t\tdelta %d of %d:\n"), i+1, featureCount);
				printf(N_T("\t\tx: %d\n"), delta.X);
				printf(N_T("\t\ty: %d\n"), delta.Y);
				printf(N_T("\t\tangle1: %d\n"), RotationToDegrees(delta.Angle1));
				printf(N_T("\t\tangle2: %d\n"), RotationToDegrees(delta.Angle2));
				printf(N_T("\t\tangle3: %d\n"), RotationToDegrees(delta.Angle3));
			}
		}
	}

	/* cores */
	result = NFRecordGetCoreCount(hnfRec, &featureCount);
	if (NSucceeded(result))
	{
		NFCore core;

		printf(N_T("\tcore count: %d\n"), featureCount);

		for (i = 0; i < featureCount; i++)
		{
			result = NFRecordGetCore(hnfRec, i, &core);
			if (NSucceeded(result))
			{
				printf(N_T("\t\tcore %d of %d:\n"), i+1, featureCount);
				printf(N_T("\t\tx: %d\n"), core.X);
				printf(N_T("\t\ty: %d\n"), core.Y);
				printf(N_T("\t\tangle: %d\n"), RotationToDegrees(core.Angle));
			}
		}
	}

	/* double cores */
	result = NFRecordGetDoubleCoreCount(hnfRec, &featureCount);
	if (NSucceeded(result))
	{
		NFDoubleCore doubleCore;

		printf(N_T("\tdouble core count: %d\n"), featureCount);

		for (i = 0; i < featureCount; i++)
		{
			result = NFRecordGetDoubleCore(hnfRec, i, &doubleCore);
			if (NSucceeded(result))
			{
				printf(N_T("\t\tdouble core %d of %d:\n"), i+1, featureCount);
				printf(N_T("\t\tx: %d\n"), doubleCore.X);
				printf(N_T("\t\ty: %d\n"), doubleCore.Y);
			}
		}
	}
}

NResult PrintNFTemplate(HNTemplate hnTemplate, NBool isPalm)
{
	NResult result = N_OK;
	HNFTemplate hnfTemplate = NULL;
	NInt i;
	NInt recordCount;

	if (!isPalm)
	{
		result = NTemplateGetFingersEx(hnTemplate, &hnfTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NTemplateGetFingers, error code: %d"), result);
			return result;
		}
	}
	else
	{
		result = NTemplateGetPalmsEx(hnTemplate, &hnfTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NTemplateGetFingers, error code: %d"), result);
			return result;
		}
	}

	if (hnfTemplate)
	{
		HNFRecord hnfRec;

		result = NFTemplateGetRecordCount(hnfTemplate, &recordCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NFTemplateGetRecordCount, error code: %d"), result);
			result = NObjectSet(NULL, &hnfTemplate);
			result = N_E_FAILED;
			return result;
		}
		if (!isPalm) printf(N_T("%d finger records\n"), recordCount);
		else printf(N_T("%d palm records\n"), recordCount);

		for (i = 0; i < recordCount; i++)
		{
			result = NFTemplateGetRecordEx(hnfTemplate, i, &hnfRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in NFTemplateGetRecord, error code: %d"), result);
				result = NObjectSet(NULL, &hnfTemplate);
				result = N_E_FAILED;
				return result;
			}

			PrintNFRecord(hnfRec);

			result = NObjectSet(NULL, &hnfRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				return result;
			}
		}
		result = NObjectSet(NULL, &hnfTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			return result;
		}
	}
	else
	{
		if (!isPalm) printf(N_T("0 finger records\n"));
		else printf(N_T("0 palm records\n"));
	}

	return result;
}

const NChar* NEPositionToString(NEPosition position)
{
	switch (position)
	{
	case nepLeft:
		return N_T("Left eye.");
	case nepRight:
		return N_T("Right eye.");
	case nepUnknown:
		return N_T("Unknown eye.");
	default:
		return N_T("n/a");
	}
}

void PrintNERecord(HNERecord hnlRec)
{
	NResult result;
	NEPosition position;
	NSizeType size;

	result = NERecordGetPosition(hnlRec, &position);
	if (NSucceeded(result))
	{
		printf(N_T("\tposition: %s\n"), NEPositionToString(position));
	}

	result = NObjectGetSize(hnlRec, 0, &size);
	if (NSucceeded(result))
	{
		printf(N_T("\tsize: %d\n"), (int)size);
	}
}

NResult PrintNETemplate(HNTemplate hnTemplate)
{
	NResult result = N_OK;
	HNETemplate hneTemplate = NULL;
	NInt i;
	NInt recordCount;

	result = NTemplateGetIrisesEx(hnTemplate, &hneTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NTemplateGetIrises, error code: %d"), result);
		return result;
	}

	if (hneTemplate)
	{
		HNERecord hneRec = NULL;

		result = NETemplateGetRecordCount(hneTemplate, &recordCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NETemplateGetRecordCount, error code: %d"), result);
			result = NObjectSet(NULL, &hneTemplate);
			result = N_E_FAILED;
			return result;
		}
		printf(N_T("%d iris records\n"), recordCount);

		for (i = 0; i < recordCount; i++)
		{
			result = NETemplateGetRecordEx(hneTemplate, i, &hneRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in NLTemplateGetRecord, error code: %d"), result);
				result = NObjectSet(NULL, &hneTemplate);
				result = N_E_FAILED;
				return result;
			}

			PrintNERecord(hneRec);
			result = NObjectSet(NULL, &hneRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				return result;
			}
		}
		result = NObjectSet(NULL, &hneTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			return result;
		}
	}
	else
	{
		printf(N_T("0 iris records\n"));
	}

	return result;
}

void PrintNLRecord(HNLRecord hnlRec)
{
	NResult result;
	NByte byteVal;
	NSizeType size;

	result = NLRecordGetQuality(hnlRec, &byteVal);
	if (NSucceeded(result))
	{
		printf(N_T("\tquality: %d\n"), byteVal);
	}

	result = NObjectGetSize(hnlRec, 0, &size);
	if (NSucceeded(result))
	{
		printf(N_T("\tsize: %d\n"), (int)size);
	}
}

NResult PrintNLTemplate(HNTemplate hnTemplate)
{
	NResult result = N_OK;
	HNLTemplate hnlTemplate = NULL;
	NInt i;
	NInt recordCount;

	result = NTemplateGetFacesEx(hnTemplate, &hnlTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NTemplateGetFaces, error code: %d"), result);
		return result;
	}

	if (hnlTemplate)
	{
		HNLRecord hnlRec = NULL;

		result = NLTemplateGetRecordCount(hnlTemplate, &recordCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NLTemplateGetRecordCount, error code: %d"), result);
			result = NObjectSet(NULL, &hnlTemplate);
			result = N_E_FAILED;
			return result;
		}
		printf(N_T("%d face records\n"), recordCount);

		for (i = 0; i < recordCount; i++)
		{
			result = NLTemplateGetRecordEx(hnlTemplate, i, &hnlRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in NLTemplateGetRecord, error code: %d"), result);
				result = NObjectSet(NULL, &hnlTemplate);
				result = N_E_FAILED;
				return result;
			}

			PrintNLRecord(hnlRec);
			result = NObjectSet(NULL, &hnlRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				return result;
			}
		}
		result = NObjectSet(NULL, &hnlTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			return result;
		}
	}
	else
	{
		printf(N_T("0 face records\n"));
	}

	return result;
}

void PrintNSRecord(HNSRecord hnsRec)
{
	NResult result;
	NInt intVal;
	NSizeType size;

	result = NSRecordGetPhraseId(hnsRec, &intVal);
	if (NSucceeded(result))
	{
		printf(N_T("\tphrase id: %d\n"), intVal);
	}

	result = NObjectGetSize(hnsRec, 0, &size);
	if (NSucceeded(result))
	{
		printf(N_T("\tsize: %d\n"), (int)size);
	}
}

NResult PrintNSTemplate(HNTemplate hnTemplate)
{
	NResult result = N_OK;
	HNSTemplate hnsTemplate = NULL;
	NInt i;
	NInt recordCount;

	result = NTemplateGetVoicesEx(hnTemplate, &hnsTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NTemplateGetVoices, error code: %d"), result);
		return result;
	}

	if (hnsTemplate)
	{
		HNSRecord hnsRec = NULL;

		result = NSTemplateGetRecordCount(hnsTemplate, &recordCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("error in NSTemplateGetRecordCount, error code: %d"), result);
			result = NObjectSet(NULL, &hnsTemplate);
			result = N_E_FAILED;
			return result;
		}
		printf(N_T("%d voice records\n"), recordCount);

		for (i = 0; i < recordCount; i++)
		{
			result = NSTemplateGetRecordEx(hnsTemplate, i, &hnsRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("error in NSTemplateGetRecord, error code: %d"), result);
				result = NObjectSet(NULL, &hnsTemplate);
				result = N_E_FAILED;
				return result;
			}

			PrintNSRecord(hnsRec);
			result = NObjectSet(NULL, &hnsRec);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				return result;
			}
		}
		result = NObjectSet(NULL, &hnsTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			return result;
		}
	}
	else
	{
		printf(N_T("0 voice records\n"));
	}

	return result;
}

int main(int argc, NChar **argv)
{
	HNBuffer hBuffer = NULL;
	HNTemplate hTemplate = NULL;

	NResult result = N_OK;

	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
	{
		OnExit();
		return usage();
	}

	result = NFileReadAllBytesCN(argv[1], &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NFileReadAllBytesCN, error code: %d"), result);
		goto FINALLY;
	}

	result = NTemplateCreateFromMemoryN(hBuffer, 0, NULL, &hTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("error in NTemplateCreateFromMemory, error code: %d"), result);
		goto FINALLY;
	}

	printf(N_T("template contains:\n"));

	result = PrintNFTemplate(hTemplate, NFalse);
	if (NFailed(result))
		goto FINALLY;

	result = PrintNLTemplate(hTemplate);
	if (NFailed(result))
		goto FINALLY;

	result = PrintNETemplate(hTemplate);
	if (NFailed(result))
		goto FINALLY;

	result = PrintNSTemplate(hTemplate);
	if (NFailed(result))
		goto FINALLY;

	result = PrintNFTemplate(hTemplate, NTrue);
	if (NFailed(result))
		goto FINALLY;

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	OnExit();

	return result;
}
