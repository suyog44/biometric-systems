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

const NChar title[] = N_T("SegmentIris");
const NChar description[] = N_T("Demonstrates iris segmenter.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [input image] [output image]" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar *components = N_T("Biometrics.IrisExtraction,Biometrics.IrisSegmentation");
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

		NIris iris;
		iris.SetFileName(argv[1]);
		iris.SetImageType(neitCroppedAndMasked);
		NSubject subject;
		subject.GetIrises().Add(iris);

		NBiometricClient biometricClient;
		NBiometricTask task = biometricClient.CreateTask(nboSegment, subject);
		biometricClient.PerformTask(task);
		if (task.GetStatus() == nbsOk)
		{
			for (int i = 0; i < iris.GetObjects().GetCount(); i++)
			{
				NEAttributes attributes = iris.GetObjects().Get(i);
				cout << "Overall quality\t" << attributes.GetQuality() << endl;
				cout << "GrayScaleUtilisation\t" << attributes.GetGrayScaleUtilisation() << endl;
				cout << "Interlace\t" << attributes.GetInterlace() << endl;
				cout << "IrisPupilConcentricity\t" << attributes.GetIrisPupilConcentricity() << endl;
				cout << "IrisPupilContrast\t" << attributes.GetIrisPupilContrast() << endl;
				cout << "IrisRadius\t" << attributes.GetIrisRadius() << endl;
				cout << "IrisScleraContrast\t" << attributes.GetIrisScleraContrast() << endl;
				cout << "MarginAdequacy\t" << attributes.GetMarginAdequacy() << endl;
				cout << "PupilBoundaryCircularity\t" << attributes.GetPupilBoundaryCircularity() << endl;
				cout << "PupilToIrisRatio\t" << attributes.GetPupilToIrisRatio() << endl;
				cout << "Sharpness\t" << attributes.GetSharpness() << endl;
				cout << "UsableIrisArea\t" << attributes.GetUsableIrisArea() << endl;
			}
			subject.GetIrises().Get(1).GetImage().Save(argv[2]);
		}
		else
		{
			cout << "Segmentation failed. Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), task.GetStatus()) << endl;
			if (task.GetError() != NULL)
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
