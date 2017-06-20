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

const NChar title[] = N_T("DetectFacialFeaturesFromCamera");
const NChar description[] = N_T("Demonstrates face feature extraction from camera.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

static int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [template] [image] [tokenImage]" << endl;
	cout << "\t" << title << " [template] [image] [tokenImage] [-u url](optional)" << endl;
	cout << "\t" << title << " [template] [image] [tokenImage] [-f filename](optional)" << endl << endl;
	cout << "\t[template] - filename to store face template." << endl;
	cout << "\t[image]    - image filename to store face image." << endl;
	cout << "\t[tokenImage]    - image filename to store token face image." << endl;
	cout << "\t[-u url]      - (optional) url to RTSP stream." << endl;
	cout << "\t[-f filename]      - (optional) video file to be connected as camera." << endl;
	cout << "\tIf url(-u) or filename(-f) attribute is not specified first attached camera will be used" << endl << endl;
	cout << "examples:" << endl;
	cout << "\t" << title << " image.jpg template.dat" << endl;
	cout << "\t" << title << " image.jpg template.dat -u rtsp://172.16.0.161/axis-media/media.amp" << endl;
	return 1;
}

static void PrintNleFeaturePoint(const NStringWrapper& name, NLFeaturePoint point)
{
	if (point.Confidence == 0)
		cout << "\t\t " << name.GetString() << " (feature code) " << point.Code << " feature unavailable. \tConfidence: 0" << endl;
	else
		cout << "\t\t" << name.GetString() << " feature found. X: " << point.X << " Y: " << point.Y << " ,confidence: " << point.Confidence << endl;
}

static void PrintFaceFeaturePoint(NLFeaturePoint point)
{
	if (point.Confidence == 0)
		cout << "\t\tface feature point (feature code " << point.Code << ") unavailable. \tConfidence: 0" << endl;
	else
		cout << "\t\tface feature point (feature code" << point.Code << ") found. X: " << point.X << " Y: " << point.Y << " ,confidence " << point.Confidence << endl;
}

static NDevice connectDevice(NDeviceManager& deviceManager, const NStringWrapper& url, const NStringWrapper& urlTrue)
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

static void PrintFaceAttributes(const NFace & face, NBoolean additionalComponents)
{
	for (int i = 0; i < face.GetObjects().GetCount(); i++)
	{
		NLAttributes attributes = face.GetObjects().Get(i);
		cout << "location = " << attributes.GetBoundingRect().X << " " << attributes.GetBoundingRect().Y << " ,width = " << attributes.GetBoundingRect().Width << " ,height = " << attributes.GetBoundingRect().Height << endl;
		PrintNleFeaturePoint("LeftEyeCentre", attributes.GetLeftEyeCenter());
		PrintNleFeaturePoint("RightEyeCenter", attributes.GetRightEyeCenter());
		if (additionalComponents)
		{
			PrintNleFeaturePoint("MouthCenter", attributes.GetMouthCenter());
			PrintNleFeaturePoint("NoseTip", attributes.GetNoseTip());
			for (int j = 0; j < attributes.GetFeaturePoints().GetCount(); j++)
			{
				NLFeaturePoint featurePoint = attributes.GetFeaturePoints().Get(j);
				PrintFaceFeaturePoint(featurePoint);
			}
			if (attributes.GetAge() == 254) cout << "\t\tAge not detected" << endl;
			else cout << "\t\tAge: " << attributes.GetAge() << endl;
			if (attributes.GetGenderConfidence() == 255) cout << "\t\tGender not detected" << endl;
			else cout << "\t\tGender " << NEnum::ToString(NBiometricTypes::NGenderNativeTypeOf(), attributes.GetGender()) << ", Confidence: " << attributes.GetGenderConfidence() << endl;
			if (attributes.GetExpressionConfidence() == 255) cout << "\t\tExpression not detected" << endl;
			else cout << "\t\tExpression: " << NEnum::ToString(NBiometricTypes::NLExpressionNativeTypeOf(), attributes.GetExpression()) << ", Confidence: " << attributes.GetExpressionConfidence() << endl;
			if (attributes.GetBlinkConfidence() == 255) cout << "\t\tBlink not detected" << endl;
			else cout << "\t\tBlink: " << (attributes.GetProperties() == nlpBlink ? ("True") : ("False")) << ", Confidence: " << attributes.GetBlinkConfidence() << endl;
			if (attributes.GetMouthOpenConfidence() == 255) cout << "\t\tMouth open is not detected" << endl;
			else cout << "\t\tMouth open: " << (attributes.GetProperties() == nlpMouthOpen ? N_T("True") : N_T("False")) << ", Confidence: " << attributes.GetMouthOpenConfidence() << endl;
			if (attributes.GetGlassesConfidence() == 255) cout << "\t\tGlasses are not detected" << endl;
			else cout << "\t\tGlasses: " << (attributes.GetProperties() == nlpGlasses ?  N_T("True") : N_T("False")) << ", Confidence: " << attributes.GetGlassesConfidence() << endl;
			if (attributes.GetDarkGlassesConfidence() == 255) cout << "\t\tDark glasses are not detected" << endl;
			else cout << "\t\tDark glasses: " << (attributes.GetProperties() == nlpDarkGlasses ? N_T("True") : N_T("False")) << ", Confidence " << attributes.GetDarkGlassesConfidence() << endl;
		}
	}
}

int main(int argc, NChar **argv)
{
	NString components = N_T("Biometrics.FaceDetection,Biometrics.FaceExtraction");
	const NString additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 4 && argc != 6)
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
		if (!NLicense::ObtainComponents(N_T("/local"), N_T("5000"), additionalComponents))
		{
			components = components + "," + additionalComponents;
		}

		bool isAdditionalComponentActivated = NLicense::IsComponentActivated(additionalComponents);
		NBiometricClient biometricClient;
		biometricClient.SetUseDeviceManager(true);
		biometricClient.SetBiometricTypes(nbtFace);
		biometricClient.SetFacesTemplateSize(ntsLarge);
		if (isAdditionalComponentActivated)
		{
			biometricClient.SetFacesDetectAllFeaturePoints(true);
			biometricClient.SetFacesDetectBaseFeaturePoints(true);
			biometricClient.SetFacesRecognizeExpression(true);
			biometricClient.SetFacesDetectProperties(true);
			biometricClient.SetFacesDetermineGender(true);
			biometricClient.SetFacesDetermineAge(true);
		}
		biometricClient.Initialize();

		NCamera camera = NULL;
		NDeviceManager deviceManager = biometricClient.GetDeviceManager();
		if (argc == 6)
		{
			camera = NObjectDynamicCast<NCamera>(connectDevice(deviceManager, argv[5], argv[4]));
		}
		else
		{
			int count = deviceManager.GetDevices().GetCount();
			if (count == 0)
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

		NBiometricTask task = biometricClient.CreateTask((NBiometricOperations)(nboCapture | nboDetectSegments | nboSegment | nboAssessQuality), subject);
		cout << "Starting to capture. Please look into the camera..." << endl;
		biometricClient.PerformTask(task);
		cout << "Done" << endl;
		if (task.GetStatus() == nbsOk)
		{
			PrintFaceAttributes(face, isAdditionalComponentActivated);
			NFile::WriteAllBytes(argv[1], subject.GetTemplateBuffer());
			face.GetImage().Save(argv[2]);
			subject.GetFaces().Get(1).GetImage().Save(argv[3]);
		}
		else
		{
			cout << "Capturing failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(),task.GetStatus()) << endl;
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
