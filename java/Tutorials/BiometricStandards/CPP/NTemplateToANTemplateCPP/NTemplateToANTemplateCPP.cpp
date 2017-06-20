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

const NChar title[] = N_T("NTemplateToANTemplate");
const NChar description[] = N_T("Demonstrates creation of ANTemplate from NTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [NTemplate] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]" << endl << endl;
	cout << "\tNTemplate     - filename with tNTemplate" << endl;
	cout << "\tANTemplate - filename for ANTemplate" << endl;
	cout << "\tTot - specifies type of transaction" << endl;
	cout << "\tDai - specifies destination agency identifier" << endl;
	cout << "\tOri - specifies originating agency identifier" << endl;
	cout << "\tTcn - specifies transaction control number" << endl;
	return 1;
}

int main(int argc, NChar ** argv)
{
	const NChar * components = N_T("Biometrics.Standards.FingerTemplates,Biometrics.Standards.PalmTemplates");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 7)
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

		NString tot = argv[3];
		NString dai = argv[4];
		NString ori = argv[5];
		NString tcn = argv[6];

		if (tot.GetLength() < 3 || tot.GetLength() > 4)
		{
			cout << "Tot parameter should be 3 or 4 characters length.";
			return -1;
		}

		NBuffer buffer = NFile::ReadAllBytes(argv[1]);
		NTemplate nTemplate(buffer);
		ANTemplate anTemplate(AN_TEMPLATE_VERSION_CURRENT, tot, dai, ori, tcn, true, nTemplate);
		anTemplate.Save(argv[2]);
		cout << "Program produced file: " << argv[2];
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
