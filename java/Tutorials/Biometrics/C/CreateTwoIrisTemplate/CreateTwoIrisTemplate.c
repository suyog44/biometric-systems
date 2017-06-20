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

const NChar title[] = N_T("CreateTwoIrisTemplate");
const NChar description[] = N_T("Demonstrates how to create two eye NTemplate.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2008-2017 Neurotechnology");

int usage()
{
	printf(N_T("usage:\n"));
	printf(N_T("\t%s [left eye] [right eye] [template]\n"), title);
	printf(N_T("\n"));
	printf(N_T("\tleft eye  - filename of the left eye file with template.\n"));
	printf(N_T("\tright eye - filename of the right eye file with template.\n"));
	printf(N_T("\ttemplate  - filename for template.\n"));

	return 1;
}

int main(int argc, NChar **argv)
{
	HNTemplate hNTemplate = NULL;
	HNETemplate outputIrisesTemplate = NULL;
	HNTemplate outputTemplate = NULL;
	HNETemplate inputIrisesTemplate = NULL;
	HNFRecord inputIrisRecord = NULL;
	HNBuffer hBuffer = NULL;

	NInt i, j;
	NResult result = N_OK;
	NInt index;
	NInt inputRecordCount;

	OnStart(title, description, version, copyright, argc, argv);
	
	if (argc < 4)
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

	// create NETemplate
	result = NETemplateCreateEx(NFalse, &outputIrisesTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NETemplateCreateEx() failed (result = %d)!"), result);
		goto FINALLY;
	}

	// set NETemplate to NTemplate
	result = NTemplateSetIrises(outputTemplate, outputIrisesTemplate);
	if (NFailed(result))
	{
		result = PrintErrorMsgWithLastError(N_T("NTemplateSetIrises() failed (result = %d)!"), result);
		goto FINALLY;
	}

	for (i = 0; i < (argc - 2); i++)
	{
		// read NTemplate/NETemplate/NERecord from input file
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

		// retrieve NETemplate from NTemplate
		result = NTemplateGetIrisesEx(hNTemplate, &inputIrisesTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NTemplateGetIrisesEx() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// free unneeded hNTemplate
		result = NObjectSet(NULL, &hNTemplate);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
			goto FINALLY;
		}

		// retrieve NERecords count
		result = NETemplateGetRecordCount(inputIrisesTemplate, &inputRecordCount);
		if (NFailed(result))
		{
			result = PrintErrorMsgWithLastError(N_T("NETemplateGetRecordCount() failed (result = %d)!"), result);
			goto FINALLY;
		}

		printf(N_T("found %d records in file %s\n"), inputRecordCount, argv[1 + i]);

		for (j = 0; j < inputRecordCount; j++)
		{
			// retrieve NERecord from NETemplate at index j
			result = NETemplateGetRecordEx(inputIrisesTemplate, j, &inputIrisRecord);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NETemplateGetRecordEx() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// add NERecord to output NETemplate
			result = NETemplateAddRecordEx(outputIrisesTemplate, inputIrisRecord, &index);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NFTemplateAddRecordEx() failed (result = %d)!"), result);
				goto FINALLY;
			}

			// free unneeded inputIrisRecord
			result = NObjectSet(NULL, &inputIrisRecord);
			if (NFailed(result))
			{
				result = PrintErrorMsgWithLastError(N_T("NObjectSet() failed (result = %d)!"), result);
				goto FINALLY;
			}
		}

		// free unneeded inputIrisesTemplate
		result = NObjectSet(NULL, &inputIrisesTemplate);
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
		result2 = NObjectSet(NULL, &outputIrisesTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &outputTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &inputIrisesTemplate);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &inputIrisRecord);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
		result2 = NObjectSet(NULL, &hBuffer);
		if (NFailed(result2)) PrintErrorMsg(N_T("NObjectSet() failed (result = %d)!"), result2);
	}

	OnExit();

	return result;
}
