#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NBiometricClient/NBiometricClient.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NBiometricClient.hpp>
	#include <NBiometrics.hpp>
	#include <NMedia.hpp>
	#include <NLicensing.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::IO;

const NChar title[] = N_T("CreateTwoIrisTemplate");
const NChar description[] = N_T("Demonstrates how to create two eye NTemplate.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title <<" [left eye] [right eye] [template]" << endl << endl;
	cout << "\tleft eye  - filename of the left eye file with template." << endl;
	cout << "\tright eye - filename of the right eye file with template." << endl;
	cout << "\ttemplate  - filename for template." << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	OnStart(title, description, version, copyright, argc, argv);
	
	if (argc < 4)
	{
		OnExit();
		return usage();
	}

	try
	{
		NTemplate nTemplate;
		// create NTemplate
		NTemplate outputTemplate;
		// create NETemplate
		NETemplate outputIrisesTemplate;
		// set NETemplate to NTemplate
		outputTemplate.SetIrises(outputIrisesTemplate);
		//NETemplate nTemplate.Irises;

		for (int i = 1; i < argc - 1; i++){
			// read NTemplate/NETemplate/NERecord from input file
			NTemplate newTemplate(NFile::ReadAllBytes(argv[i]));
			NETemplate irisTemplate(newTemplate.GetIrises());

			// retrieve NETemplate from NTemplate
			NETemplate inputIrisesTemplate(nTemplate.GetIrises());

			// retrieve NERecords count
			int inputRecordCount = irisTemplate.GetRecords().GetCount();
			cout << "found " << inputRecordCount << " records in file " << argv[i] << ".\n";

			for (int j = 0; j < inputRecordCount; j++){
				// add NERecord to output NETemplate
				outputIrisesTemplate.GetRecords().Add(irisTemplate.GetRecords().Get(j));
			}
		}

		cout << "Template successfully saved to file: " << argv[3];
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
