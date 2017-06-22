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
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Standards;
using namespace Neurotec::IO;

const NChar title[] = N_T("EnrollFingerFromImage");
const NChar description[] = N_T("Demonstrates fingerprint feature extraction from image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image] [template] [format]" << endl << endl;
	cout << "\t[image]    - image filename to extract." << endl;
	cout << "\t[template] - filename to store extracted features." << endl;
	cout << "\t[format]   - whether proprietary or standard template should be created." << endl;
	cout << "\t\tIf not specified, proprietary Neurotechnology template is created (recommended)." << endl;
	cout << "\t\tANSI for ANSI/INCITS 378-2004" << endl;
	cout << "\t\tISO for ISO/IEC 19794-2" << endl << endl << endl;
	cout << "examples:" << endl;
	cout << "\t" << title << " image.jpg template.dat" << endl;
	cout << "\t" << title << " image.jpg isoTemplate.dat ISO" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = { N_T("Biometrics.FingerExtraction") };
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
	{
		OnExit();
		return usage();
	}

	BdifStandard standard = bsUnspecified;
	if (argc > 3)
	{
		if (!strcmp(argv[3], N_T("ANSI")))
		{
			standard = bsAnsi;
		}
		else if (!strcmp(argv[3], N_T("ISO")))
		{
			standard = bsIso;
		}
	}

	try
	{
		if (!NLicense::ObtainComponents(N_T("/local"), N_T("5000"), components))
		{
			NThrowException(NString::Format(N_T("Could not obtain licenses for components: {S}"), components));
		}

		NBiometricClient biometricClient;
		biometricClient.SetFingersTemplateSize(ntsLarge);

		NSubject subject;
		NFinger finger;
		finger.SetFileName(argv[1]);
		subject.GetFingers().Add(finger);
		NBiometricStatus status = biometricClient.CreateTemplate(subject);
		if (status == nbsOk)
		{
			cout << "Template extracted: " << endl;
			if (standard == bsIso)
			{
				cout << "ISO" << endl;
				NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer(CBEFF_BO_ISO_IEC_JTC_1_SC_37_BIOMETRICS, CBEFF_BDBFI_ISO_IEC_JTC_1_SC_37_BIOMETRICS_FINGER_MINUTIAE_RECORD_FORMAT, FMR_VERSION_ISO_CURRENT));
			}
			else if (standard == bsAnsi)
			{
				cout << "ANSI" << endl;
				NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer(CBEFF_BO_INCITS_TC_M1_BIOMETRICS, CBEFF_BDBFI_INCITS_TC_M1_BIOMETRICS_FINGER_MINUTIAE_U, FMR_VERSION_ANSI_CURRENT));
			}
			else
			{
				cout << "Proprietary" << endl;
				NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
			}
			cout << "Template saved successfully" << endl;
		}
		else
		{
			cout << "Extraction failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
		}
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
