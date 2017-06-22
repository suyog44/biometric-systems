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
using namespace Neurotec::Devices;

using namespace Neurotec::Images;
const NChar title[] = N_T("IrisesEnrollFromScammer");
const NChar description[] = N_T("Demonstrates enrollment from iris scanner.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image1] [image2] [template]" << endl << endl;
	cout << "\t[image1]    - image filename to store scanned left eye image." << endl;
	cout << "\t[image2]    - image filename to store scanned right eye image." << endl;
	cout << "\t[template] - filename for template." << endl;
	cout << "\texmaple: " << title << " left.jpg right.jpg template.dat" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.IrisExtraction,Devices.IrisScanners");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 4)
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
		biometricClient.SetIrisesTemplateSize(ntsLarge);
		biometricClient.SetUseDeviceManager(true);
		biometricClient.SetBiometricTypes(nbtIris);
		biometricClient.Initialize();

		NDeviceManager deviceManager = biometricClient.GetDeviceManager();
		int deviceCount = deviceManager.GetDevices().GetCount();
		if (deviceCount > 0)
		{
			cout << "Found " << deviceCount << " iris scanners" << endl;
		}
		else
		{
			cout << "No iris scanners found, exiting... " << endl;
			return -1;
		}
		if (deviceCount > 1)
		{
			cout<< "Please select iris scanners from the list: " << endl;
			for (int i = 0; i < deviceCount; i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				cout<< i + 1 << " " << device.GetDisplayName() << endl;
			}
		}

		int selection = 0;
		if (deviceCount > 1)
		{
			cout << "Please enter iris scanner index: " << endl;
			cin >> selection;
			if (selection > deviceCount || selection < 1)
			{
				cout << "Incorrect index provided ..." << endl;
				return -1;
			}
		}
		biometricClient.SetIrisScanner(NObjectDynamicCast<NIrisScanner>(deviceManager.GetDevices().Get(selection)));

		NSubject subject;
		NIris leftIris;
		leftIris.SetPosition(nepLeft);
		subject.GetIrises().Add(leftIris);
		NBiometricStatus status = biometricClient.Capture(subject);
		if (status == nbsOk)
		{
			cout << "Captured" << endl;
		}
		else
			cout << "Capturing failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;

		NIris rightIris;
		rightIris.SetPosition(nepRight);
		subject.GetIrises().Add(rightIris);
		status = biometricClient.Capture(subject);
		if (status == nbsOk)
		{
			cout << "Captured" << endl;
		}
		else
			cout << "Capturing failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;

		status = biometricClient.CreateTemplate(subject);
		if (status == nbsOk)
		{
			cout << "Template extracted" << endl;
		}
		else
			cout << "Extraction failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;

		leftIris.GetImage().Save(argv[1]);
		cout << "Image saved successfully" << endl;
		rightIris.GetImage().Save(argv[2]);
		cout << "Image saved successfully" << endl;
		NFile::WriteAllBytes(argv[3], subject.GetTemplateBuffer());
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
