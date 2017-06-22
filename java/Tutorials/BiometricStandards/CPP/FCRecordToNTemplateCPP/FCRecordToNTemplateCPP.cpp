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
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::Biometrics::Standards;

const NChar title[] = N_T("FCRecordToNTemplate");
const NChar description[] = N_T("Demonstrates creation of NTemplate from FCRecord");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [FCRecord] [NTemplate]" << endl << endl;
	cout << "\tFCRecord  - input FCRecord" << endl;
	cout << "\tNTemplate - output NTemplate" << endl;
	return 1;
}

int main(int argc, NChar *argv[])
{
	const NChar * components = N_T("Biometrics.FaceExtraction,Biometrics.Standards.Faces");
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
		NSubject subject;
		NBuffer fcRecordData = NFile::ReadAllBytes(argv[1]);
		FCRecord fcRecord(fcRecordData, bsIso);
		for (int i = 0; i < fcRecord.GetFaceImages().GetCount(); i++)
		{
			FcrFaceImage fv = fcRecord.GetFaceImages().Get(i);
			NFace face;
			face.SetImage(fv.ToNImage());
			subject.GetFaces().Add(face);
		}
		biometricClient.SetFacesTemplateSize(ntsLarge);
		NBiometricStatus status = biometricClient.CreateTemplate(subject);
		if (status == nbsOk)
		{
			cout << "Template extracted" << endl;
			NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
			cout << "Template saved to file" << argv[2];
		}
		else
			cout << "Template extraction failed. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
