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

const NChar title[] = N_T("CreateMultipleFaceTemplate");
const NChar description[] = N_T("Demonstrates creation of NTemplate containing multiple face templates.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [nlrecords/nltemplates/ntemplates] ... [NTemplate]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\t[nlrecords/nltemplates/ntemplates] ...  - filenames of one or more files containing NLRecords, NLTemplates and/or NTempaltes (faces).\n"));
	printf(N_T("\t[NTemplate]                             - filename of output file where NTemplate is saved.\n"));

	return 1;
}

int main(int argc, NChar **argv)
{
	HNTemplate hNTemplate = NULL;
	HNLTemplate outputFacesTemplate = NULL;
	HNTemplate outputTemplate = NULL;
	HNLTemplate inputFacesTemplate = NULL;
	HNLRecord inputFaceRecord = NULL;
	HNBuffer hBuffer = NULL;

	NInt i, j;
	NResult result = N_OK;
	NInt index;
	NInt inputRecordCount;

	OnStart(title, description, version, copyright, argc, argv);
	
	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	// create NTemplate
	result = NTemplateCreateEx(0, &outputTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NTemplateCreate() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// create NLTemplate
	result = NLTemplateCreateEx(0, &outputFacesTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NLTemplateCreateEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set NLTemplate to NTemplate
	result = NTemplateSetFaces(outputTemplate, outputFacesTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NTemplateSetFaces() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 0; i < (argc - 2); i++)
	{
		// read NTemplate/NLTemplate/NLRecord from input file
		result = NFileReadAllBytesCN(argv[i + 1], &hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("failed to read template from file (error = %d)!"), result);
			goto FINALLY;
		}

		result = NTemplateCreateFromMemoryN(hBuffer, 0, NULL, &hNTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NTemplateCreateFromMemoryN() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hBuffer
		result = NObjectSet(NULL, &hBuffer);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve NLTemplate from NTemplate
		result = NTemplateGetFacesEx(hNTemplate, &inputFacesTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NTemplateGetFacesEx() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hNTemplate
		result = NObjectSet(NULL, &hNTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve NLRecords count
		result = NLTemplateGetRecordCount(inputFacesTemplate, &inputRecordCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NLTemplateGetRecordCount() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("found %d records in file %s\n"), inputRecordCount, argv[1 + i]);

		for (j = 0; j < inputRecordCount; j++)
		{
			// retrieve NLRecord from NLTemplate at index j
			result = NLTemplateGetRecordEx(inputFacesTemplate, j, &inputFaceRecord);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NLTemplateGetRecord() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// add NLRecord to output NLTemplate
			result = NLTemplateAddRecordEx(outputFacesTemplate, inputFaceRecord, &index);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NLTemplateAddRecordEx() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// free unneeded inputFaceRecord
			result = NObjectSet(NULL, &inputFaceRecord);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}

		// free unneeded inputFacesTemplate
		result = NObjectSet(NULL, &inputFacesTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}
	}

	result = NObjectSaveToMemoryN(outputTemplate, 0, &hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NObjectSaveToMemory() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// save ouput template
	result = NFileWriteAllBytesCN(argv[argc - 1], hBuffer);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("failed to write template to file (result = %d)!"), result);
		goto FINALLY;
	}

	printf(N_T("NTemplate successfully saved to file %s\n"), argv[argc - 1]);

	result = N_OK;
FINALLY:
	{
		NResult result2 = NObjectSet(NULL, &hNTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &outputFacesTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &outputTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &inputFacesTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &inputFaceRecord);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	OnExit();

	return result;
}
