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
using namespace Neurotec::IO;

const NChar title[] = N_T("CreateMultipleFaceTemplate");
const NChar description[] = N_T("Demonstrates creation of NTemplate containing multiple face templates.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [nlrecords/nltemplates/ntemplates] ... [NTemplate]" << endl << endl;
	cout << "\t[nlrecords/nltemplates/ntemplates] ...  - filenames of one or more files containing NLRecords, NLTemplates and/or NTempaltes (faces)." << endl;
	cout << "\t[NTemplate]                             - filename of output file where NTemplate is saved." << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	try
	{
		NLTemplate nlTemplate;
		for (int i = 1; i < argc - 1; i++)
		{
			cout << "Reading " << argv[i] << endl;
			NTemplate ntemplate(NFile::ReadAllBytes(argv[i]));
			if (ntemplate.GetFaces() != NULL)
			{
				for (int j = 0; j < ntemplate.GetFaces().GetRecords().GetCount(); j++)
				{
					NLRecord record = ntemplate.GetFaces().GetRecords().Get(j);
					nlTemplate.GetRecords().Add(record);
				}
			}
		}
		if (nlTemplate.GetRecords().GetCount() == 0)
		{
			cout << "Not writing template file because no records found" << endl;
			return -1;
		}

		cout << "Records found: " <<  nlTemplate.GetRecords().GetCount() << endl;
		NFile::WriteAllBytes(argv[argc - 1], nlTemplate.Save());
		cout << "Template successfully written to " << argv[argc -1] << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
