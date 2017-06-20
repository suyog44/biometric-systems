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
using namespace Neurotec::IO;
using namespace Neurotec::Images;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Standards;

const NChar title[] = N_T("NTemplateToCbeffRecord");
const NChar description[] = N_T("Converting NTemplate to CbeffRecord");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << " [NTemplate] [CbeffRecord] [PatronFormat]" << endl << endl;
	cout << "\t[NTemplate] - filename of NTemplate" << endl;
	cout << "\t[CbeffRecord] - filename for CbeffRecord" << endl;
	cout << "\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation)" << endl;
	return 1;
}

int main(int argc, NChar ** argv)
{
	const NChar * components = N_T("Biometrics.Standards.Base");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc != 4)
	{
		OnExit();
		return usage();
	}
	
	try
	{
		if (!NLicense::ObtainComponents(N_T("/local"), N_T("5000"), components))
		{
			NThrowException(NString::Format(N_T("Could not obtain licenses for components: {S}"), components));
		}

		NBuffer packedTemplate = NFile::ReadAllBytes(argv[1]);
		NUInt bdbFormat = BdifTypes::MakeFormat(CBEFF_BO_NEUROTECHNOLOGIJA, CBEFF_BDBFI_NEUROTECHNOLOGIJA_NTEMPLATE);
		NUInt patronFormat = NTypes::UInt32Parse(argv[2], N_T("X"));
		CbeffRecord cbeffRecord(bdbFormat, packedTemplate, patronFormat);
		NFile::WriteAllBytes(argv[2], cbeffRecord.Save());
		cout << "Template successfully saved";
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
