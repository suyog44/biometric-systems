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

const NChar title[] = N_T("DetectFacialFeatures");
const NChar description[] = N_T("Demonstrates detection of face and facial features in the image.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image]" << endl << endl;
	cout << "\t[image] - filename of image." << endl;
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

int main(int argc, NChar **argv)
{
	NString components = N_T("Biometrics.FaceDetection,Biometrics.FaceExtraction");
	const NString additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 2)
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

		NBiometricClient biometricClient;
		bool isAdditionalComponentActivated = NLicense::IsComponentActivated(additionalComponents);
		if (isAdditionalComponentActivated)
		{
			biometricClient.SetFacesDetectAllFeaturePoints(true);
			biometricClient.SetFacesDetectBaseFeaturePoints(true);
			biometricClient.SetFacesRecognizeExpression(true);
			biometricClient.SetFacesDetectProperties(true);
			biometricClient.SetFacesDetermineGender(true);
			biometricClient.SetFacesDetermineAge(true);
		}
		biometricClient.SetFacesTemplateSize(ntsMedium);

		NSubject subject;
		NFace face;
		face.SetFileName(argv[1]);
		subject.GetFaces().Add(face);
		subject.SetMultipleSubjects(true);

		NBiometricTask task = biometricClient.CreateTask(nboDetectSegments, subject);
		biometricClient.PerformTask(task);
		NBiometricStatus status = task.GetStatus();
		if (status == nbsOk)
		{
			for (int i = 0; i < face.GetObjects().GetCount(); i++)
			{
				NLAttributes attributes = face.GetObjects().Get(i);
				cout << "location = " << attributes.GetBoundingRect().X << " " << attributes.GetBoundingRect().Y << " ,width = " << attributes.GetBoundingRect().Width << " ,height = " << attributes.GetBoundingRect().Height << endl;
				PrintNleFeaturePoint("LeftEyeCentre", attributes.GetLeftEyeCenter());
				PrintNleFeaturePoint("RightEyeCenter", attributes.GetRightEyeCenter());
				if (isAdditionalComponentActivated)
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
		else
		{
			cout << "Face detection failed! Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), task.GetStatus()) << endl;
			if (task.GetError() != NULL )
				throw task.GetError();
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
