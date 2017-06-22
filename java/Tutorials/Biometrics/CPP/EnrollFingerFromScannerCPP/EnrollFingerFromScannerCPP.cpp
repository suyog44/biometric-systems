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

const NChar title[] = N_T("EnrollFingerFromScanner");
const NChar description[] = N_T("Demonstrates fingerprint feature extraction from fingerprint scanning device.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

static int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image] [template]" << endl << endl;
	cout << "\t[image]    - image filename to store scanned image." << endl;
	cout << "\t[template] - filename to store finger template." << endl << endl << endl;
	cout << "example:" << endl;
	cout << "\t" << title << "image.jpg template.dat" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Biometrics.FingerExtraction,Devices.FingerScanners");
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
		biometricClient.SetFingersTemplateSize(ntsLarge);
		biometricClient.SetUseDeviceManager(true);
		biometricClient.SetBiometricTypes(nbtFinger);
		biometricClient.Initialize();

		NDeviceManager deviceManager = biometricClient.GetDeviceManager();
		int deviceCount = deviceManager.GetDevices().GetCount();
		if (deviceCount > 0) cout << "Found " << deviceCount << " finger scanners." << endl;
		else
		{
			cout << "No finger scanners found, exiting ..." << endl;
			return -1;
		}
		if (deviceCount > 1) cout << "Please select finger scanner from the list: " << endl;
		for (int i = 0; i < deviceCount; i++)
		{
			NDevice device = deviceManager.GetDevices().Get(i);
			cout << i + 1 << device.GetDisplayName() << endl;
		}

		int selection = 0;
		if (deviceCount > 1)
		{
			cout << "Please enter finger scanner index: " << endl;
			cin >> selection;
			if (selection > deviceCount || selection < 1)
			{
				cout << "Incorrect index provided ..." << endl;
				return -1;
			}
		}
		biometricClient.SetFingerScanner(NObjectDynamicCast<NFScanner>(deviceManager.GetDevices().Get(selection)));

		NSubject subject;
		NFinger finger;
		subject.GetFingers().Add(finger);
		NBiometricStatus status = biometricClient.Capture(subject);
		if (status != nbsOk)
		{
			cout << "Failed to capture: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}

		status = biometricClient.CreateTemplate(subject);
		if (status == nbsOk)
		{
			cout << "Template extracted" << endl;
			finger.GetImage().Save(argv[1]);
			cout << "Image saved successfully" << endl;
			NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
			cout << "Template saved successfully" << endl;
		}
		else
		{
			cout << "Extraction failed! Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
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
