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

const NChar title[] = N_T("NTemplateToFMRecord");
const NChar description[] = N_T("Demonstrates creation of FMRecord from NTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [NTemplate] [FMRecord] [Standard&Version] [FlagUseNeurotecFields]" << endl << endl;
	cout << "\tNTemplate - filename with NTemplate" << endl;
	cout << "\tFMRecord  - filename with FMRecord" << endl;
	cout << "\tStandard & Version - FMRecord standard & version" << endl;
	cout << "\t\t2 - ANSI/INCITS 378-2004" << endl;
	cout << "\t\t3.5 - ANSI/INCITS 378-2009" << endl;
	cout << "\t\t2 - ISO/IEC 19794-2:2005" << endl;
	cout << "\t\t3 - ISO/IEC 19794-2:2011" << endl;
	cout << "\t\tMINEX - Minex compliant record (ANSI/INCITS 378-2004 without extended data)" << endl;
	cout << "\tFlagUseNeurotecFields - 1 if FMRFV_USE_NEUROTEC_FIELDS flag is used; otherwise, 0 flag was not used. For Minex compliant record must be 0." << endl;
	cout << "example:" << endl;
	cout << "\t" << title << " template.NTemplate fmrecord.FMRecord ISO3 1" << endl;
	cout << "\t" << title << " template.NTemplate fmrecord.FMRecord MINEX 0" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.Standards.FingerTemplates");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 5)
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

		BdifStandard standard;
		NVersion version;
		int flags = 0;
		if (!strcmp(argv[3], N_T("ANSI2")))
		{
			standard = bsAnsi;
			version = FMR_VERSION_ANSI_2_0;
		}
		else if (!strcmp(argv[3], N_T("ISO2")))
		{
			standard = bsIso;
			version = FMR_VERSION_ISO_2_0;
		}
		else if (!strcmp(argv[3], N_T("ISO3")))
		{
			standard = bsIso;
			version = FMR_VERSION_ISO_3_0;
		}
		else if (!strcmp(argv[3], N_T("ANSI3.5")))
		{
			standard = bsAnsi;
			version = FMR_VERSION_ANSI_3_5;
		}
		else if (!strcmp(argv[3], N_T("MINEX")))
		{
			if (!strcmp(argv[4], N_T("1"))) // check if Neurotec flags are used
				NThrowException("MINEX and FlagUseNeurotecFields is incompatible");

			standard = bsAnsi;
			version = FMR_VERSION_ANSI_3_5;
			flags = FMRFV_SKIP_RIDGE_COUNTS | FMRFV_SKIP_SINGULAR_POINTS | FMRFV_SKIP_NEUROTEC_FIELDS;
		}
		else
			NThrowException("Wrong version");

		flags |= !strcmp(argv[4], N_T("1")) ? 1 : 0;
		NBuffer packedNTemplate = NFile::ReadAllBytes(argv[1]);
		NTemplate nTemplate(packedNTemplate);
		NFTemplate nfTemplate = nTemplate.GetFingers();
		FMRecord fmRecord = NULL;
		if (!nfTemplate.IsNull())
		{
			fmRecord = FMRecord(nfTemplate, standard, version);
			NBuffer storedFmRecord = NULL;
			storedFmRecord = fmRecord.Save(flags);
			NFile::WriteAllBytes(argv[2], storedFmRecord);
		}
		else
			cout << "There are no NFRecords in NTemplate" << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
