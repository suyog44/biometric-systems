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

const NChar title[] = N_T("GeneralizeFace");
const NChar description[] = N_T("Demonstrates template creation and generalization of multiple faces.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [output template] [multiple face images]" << endl;
	cout << "\texample " << title << " template image1.png image2.png image3.png" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = { N_T("Biometrics.FaceExtraction") };
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

		NSubject subject;
		for (int i = 2; i < argc; i++)
		{
			NFace face;
			face.SetFileName(argv[i]);
			face.SetSessionId(1);
			subject.GetFaces().Add(face);
		}

		NBiometricClient biometricClient;
		NBiometricStatus status = biometricClient.CreateTemplate(subject);
		if (status != nbsOk)
		{
			cout << "Failed to create or generalize templates. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}
		cout << "Generalazition completed successfully" << endl;
		cout << "Saving template to " << argv[1] << endl;
		NFile::WriteAllBytes(argv[1], subject.GetTemplateBuffer());
		cout << "Done" << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
