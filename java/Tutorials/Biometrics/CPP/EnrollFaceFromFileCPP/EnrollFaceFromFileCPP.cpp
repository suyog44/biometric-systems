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

const NChar title[] = N_T("EnrollFaceFromFile");
const NChar description[] = N_T("Demonstrates enrollment from file - image or video file.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [input file] [output template] [still image or video file]" << endl << endl;
	cout << "\t[input file]                - image or video filename with face." << endl;
	cout << "\t[output template]           - filename to store face template." << endl;
	cout << "\t[still image or video file] - specifies that passed source parameter is image (value: 0) or video (value: 1)." << endl << endl;
	cout << "example: " << title << " image.jpg template.dat 0" << endl;
	cout << "example: " << title << " video.avi template.dat 1" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	NString components = N_T("Biometrics.FaceExtraction");
	const NString additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
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
			NThrowException(NString::Format(N_T("Could not obtain licenses for components: {S}"), components.GetBuffer()));
		}
		if (NLicense::ObtainComponents(N_T("/local"), N_T("5000"), additionalComponents))
		{
			components = components + "," + additionalComponents;
		}

		bool isAdditionalComponentActivated = NLicense::IsComponentActivated(additionalComponents);
		NBiometricClient biometricClient;
		biometricClient.SetFacesDetectAllFeaturePoints(isAdditionalComponentActivated);
		biometricClient.SetFacesTemplateSize(ntsLarge);

		NFace face;
		face.SetFileName(argv[1]);
		NSubject subject;
		subject.GetFaces().Add(face);
		NBiometricStatus status = biometricClient.CreateTemplate(subject);
		if (status == nbsOk)
		{
			cout << "Template extracted" << endl;
			NFile::WriteAllBytes(argv[2], subject.GetTemplateBuffer());
			cout << "Template saved successfully" << endl;
		}
		else
		{
			cout << "Extraction failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
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
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
