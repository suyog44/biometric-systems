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
using namespace Neurotec::IO;

const NChar title[] = N_T("EnrollPalmFromImage");
const NChar description[] = N_T("Demonstrates palmprint feature extraction from image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << " [image] [template]" << endl;
	cout << "\t[image]    - image filename to extract." << endl;
	cout << "\t[template] - filename to store extracted features." << endl << endl << endl;
	cout << "example: " << endl;
	cout << "\t" << title << "image.jpg template.dat" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.PalmExtraction");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 3)
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

		NBiometricClient biometricClient;
		biometricClient.SetPalmsTemplateSize(ntsLarge);

		NPalm palm;
		palm.SetFileName(argv[1]);
		NSubject subject;
		subject.GetPalms().Add(palm);

		NBiometricStatus status = biometricClient.CreateTemplate(subject);
		if (subject.GetPalms().GetCount() > 1)
		{
			cout << "Found: " << subject.GetPalms().GetCount() << " palms." << endl;
		}
		if (status == nbsOk)
		{
			cout << "Template extracted" << endl;
			NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
			cout << "Template saved successfully" << endl;
		}
		else
		{
			cout << "Extraction failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
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
