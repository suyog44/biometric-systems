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

const NChar title[] = N_T("ANTemplateToNTemplate");
const NChar description[] = N_T("Demonstrates ANTemplate conversion to NTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << "[ANTemplate] [NTemplate]" << endl << endl;
	cout << "\tANTemplate - filename with ANTemplate" << endl;
	cout << "\tNTemplate  - filename for NTemplate" << endl;
	cout << "example:" << endl;
	cout << "\t" << title << "anTemplate.ANTemplate template.NTemplate" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.Standards.FingerTemplates");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 3)
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
		
		ANTemplate anTemplate(argv[1], anvlStandard);
		NTemplate nTemplate(anTemplate.ToNTemplate());
		NFile::WriteAllBytes(argv[2], nTemplate.Save());
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
