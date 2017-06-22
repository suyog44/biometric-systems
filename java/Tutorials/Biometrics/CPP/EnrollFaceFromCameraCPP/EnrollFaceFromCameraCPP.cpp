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
using namespace Neurotec::Text;
using namespace Neurotec::Devices;
using namespace Neurotec::Plugins;
using namespace Neurotec::ComponentModel;

const NChar title[] = N_T("EnrollFaceFromCamera");
const NChar description[] = N_T("Demonstrates face feature extraction from camera.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

static int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image] [template]" << endl;
	cout << "\t" << title << " [image] [template] [-u url](optional)" << endl;
	cout << "\t" << title << " [image] [template] [-f filename](optional)" << endl << endl;
	cout << "\t[template] - filename to store face template." << endl;
	cout << "\t[image]    - image filename to store face image." << endl;
	cout << "\t[-u url]      - (optional) url to RTSP stream." << endl;
	cout << "\t[-f filename]      - (optional) video file to be connected as camera." << endl;
	cout << "\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used" << endl << endl;
	cout << "examples:" << endl;
	cout << "\t" << title << " image.jpg template.dat" << endl;
	cout << "\t" << title << " image.jpg template.dat -u rtsp://172.16.0.161/axis-media/media.amp" << endl;
	return 1;
}

static NDevice ConnectDevice(NDeviceManager& deviceManager, const NStringWrapper& url, const NStringWrapper& urlTrue)
{
	bool isUrl = strcmp(N_T("-u"), urlTrue.GetString().GetBuffer()) == 0;
	NPlugin plugin = NDeviceManager::GetPluginManager().GetPlugins().Get(N_T("Media"));
	if ((plugin.GetState() == npsPlugged) && (NDeviceManager::IsConnectToDeviceSupported(plugin)))
	{
		NArrayWrapper<NParameterDescriptor> parameters = NDeviceManager::GetConnectToDeviceParameters(plugin);
		NParameterBag bag(parameters.begin(), parameters.end());
		if (isUrl)
		{
			bag.SetProperty(N_T("DisplayName"), NValue::FromString(N_T("IP Camera")));
			bag.SetProperty(N_T("Url"), NValue::FromString(url));
		}
		else
		{
			bag.SetProperty(N_T("DisplayName"), NValue::FromString(N_T("Video file")));
			bag.SetProperty(N_T("FileName"), NValue::FromString(url));
		}
		return deviceManager.ConnectToDevice(plugin, bag.ToPropertyBag());
	}
	NThrowException("Failed to connect device!");
}

int main(int argc, NChar **argv)
{
	NString components = N_T("Biometrics.FaceDetection,Biometrics.FaceExtraction");
	const NString additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 3 && argc != 5)
	{
		OnExit();
		return usage();
	}

	try
	{
		if (!NLicense::ObtainComponents(N_T("/local"), N_T("5000"), components))
		{
			NThrowException(NString::Format(N_T("Could not obtain licenses for components: {S}"), components.GetBuffer())); 
		}
		if (NLicense::ObtainComponents(N_T("/local"), N_T("5000"), additionalComponents))
		{
			components = components + "," + additionalComponents;
		}

		bool isAdditionalComponentActivated = NLicense::IsComponentActivated(additionalComponents);
		NBiometricClient biometricClient;
		biometricClient.SetUseDeviceManager(true);
		biometricClient.SetBiometricTypes(nbtFace);
		biometricClient.SetFacesTemplateSize(ntsLarge);
		biometricClient.SetFacesDetectAllFeaturePoints(isAdditionalComponentActivated);
		biometricClient.Initialize();

		NDeviceManager deviceManager = biometricClient.GetDeviceManager();
		NCamera camera(NULL);
		if (argc == 5)
		{
			camera = NObjectDynamicCast<NCamera>(ConnectDevice(deviceManager, argv[4], argv[3]));
		}
		else
		{
			if (deviceManager.GetDevices().GetCount() == 0)
			{
				cout << "No cameras found, exiting..." << endl;
				return -1;
			}
			camera = NObjectDynamicCast<NCamera>(deviceManager.GetDevices().Get(0));
		}
		biometricClient.SetFaceCaptureDevice(camera);

		NSubject subject;
		NFace face;
		face.SetCaptureOptions(nbcoStream);
		subject.GetFaces().Add(face);

		cout << "Capturing from " << biometricClient.GetFaceCaptureDevice().GetDisplayName() << ". Please turn camera to face." << endl;
		NBiometricStatus status = biometricClient.Capture(subject);
		if (status == nbsOk)
		{
			cout << "Capturing succeeded" << endl;
		}
		else
		{
			cout << "Capturing failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			return -1;
		}

		for (int i = 0; i < subject.GetFaces().GetCount(); i++)
		{
			NFace face = subject.GetFaces().Get(i);
			for (int j = 0; j < face.GetObjects().GetCount(); j++)
			{
				NLAttributes attributes = face.GetObjects().Get(j);
				cout << "Face: " << "\tlocation = " << attributes.GetBoundingRect().X << " " << attributes.GetBoundingRect().Y << ", width = " << attributes.GetBoundingRect().Width << ", height = " << attributes.GetBoundingRect().Height << endl;
				if (attributes.GetRightEyeCenter().Confidence > 0 || attributes.GetLeftEyeCenter().Confidence > 0)
				{
					cout << "\tFound eyes: " << endl;
					if (attributes.GetRightEyeCenter().Confidence > 0)
					{
						cout << "\t\tRight location = " << attributes.GetRightEyeCenter().X << " " << attributes.GetLeftEyeCenter().Y << ", confidence = " << attributes.GetRightEyeCenter().Confidence << endl;
					}
					if (attributes.GetLeftEyeCenter().Confidence > 0)
					{
						cout << "\t\tLeft location = " << attributes.GetLeftEyeCenter().X << " " << attributes.GetLeftEyeCenter().Y << ", confidence = " << attributes.GetLeftEyeCenter().Confidence << endl;
					}
				}
				if (isAdditionalComponentActivated && attributes.GetNoseTip().Confidence > 0)
				{
					cout << "\tFound nose: " << endl << "\t\tLocation = " << attributes.GetNoseTip().X << " " << attributes.GetNoseTip().Y << ", confidence " << attributes.GetNoseTip().Confidence << endl;
				}
				if (isAdditionalComponentActivated && attributes.GetMouthCenter().Confidence > 0)
				{
					cout << "\tFound mouth: " << endl << "\t\tLocation = " << attributes.GetMouthCenter().X << " " << attributes.GetMouthCenter().Y << ", confidence " << attributes.GetMouthCenter().Confidence << endl;
				}
			}
		}

		face.GetImage().Save(argv[1]);
		cout << "Image saved successfully" << endl;
		NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
		cout << "Template saved successfully" << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
