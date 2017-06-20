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

const NChar title[] = N_T("EnrollVoiceFromMicrophone");
const NChar description[] = N_T("Demonstrates voice feature extraction from microphone.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [template] [audio]" << endl << endl;
	cout << "\t[template] - filename to store voice template." << endl;
	cout << "\t[audio] - filename to store voice audio." << endl << endl << endl;
	cout << "\t" << title << " template.dat audio.wav" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Devices.Microphones,Biometrics.VoiceExtraction");
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

		NBiometricClient biometricClient;
		biometricClient.SetUseDeviceManager(true);
		biometricClient.SetBiometricTypes(nbtVoice);
		biometricClient.Initialize();

		NDeviceManager deviceManager = biometricClient.GetDeviceManager();
		int deviceCount = deviceManager.GetDevices().GetCount();
		if (deviceCount > 0)
		{
			cout << "Found: " << deviceCount << " microphones." << endl;
		}
		else
		{
			cout << "No microphones found, exiting..." << endl;
			return -1;
		}
		if (deviceCount > 1)
		{
			cout << "Please select microphone from the list: " << endl;
			for (int i = 0; i < deviceCount; i++)
			{
				NDevice device = deviceManager.GetDevices().Get(i);
				cout << i + 1 << " " << device.GetDisplayName() << endl;
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
		biometricClient.SetVoiceCaptureDevice(NObjectDynamicCast<NMicrophone>(deviceManager.GetDevices().Get(selection)));

		NSubject subject;
		NVoice voice;
		subject.GetVoices().Add(voice);
		NBiometricTask task = biometricClient.CreateTask((NBiometricOperations)(nboCapture | nboSegment), subject);
		cout << "Capturing" << endl;
		biometricClient.PerformTask(task);
		NBiometricStatus status = task.GetStatus();
		if (status == nbsOk)
		{
			cout << "Template extracted" << endl;
			subject.GetVoices().Get(1).GetSoundBuffer().Save(argv[1]);
			cout << "Voice saved successfully" << endl;
			NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
			cout << "Template saved successfully" << endl;
		}
		else
		{
			cout << "Failed to capture " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			if (task.GetError() != NULL)
			{
				throw task.GetError();
			}
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
