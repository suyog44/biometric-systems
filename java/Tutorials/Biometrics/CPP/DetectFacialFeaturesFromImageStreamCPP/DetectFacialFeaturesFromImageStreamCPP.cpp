#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NBiometricClient/NBiometricClient.hpp>
	#include <NBiometrics/NBiometrics.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NLicensing/NLicensing.hpp>
	#include <IO/NFileEnumerator.hpp>
#else
	#include <NCore.hpp>
	#include <NBiometricClient.hpp>
	#include <NBiometrics.hpp>
	#include <NMedia.hpp>
	#include <NLicensing.hpp>
	#include <IO/NFileEnumerator.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Client;
using namespace Neurotec::IO;
using namespace Neurotec::Media;
using namespace Neurotec::Images;

const NChar title[] = N_T("DetectFacialFeaturesFromImageStream");
const NChar description[] = N_T("Demonstrates facial features detection from image stream.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

static int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [-u url]" << endl;
	cout << "\t" << title << " [-f filename]" << endl;
	cout << "\t" << title << " [-d directory]" << endl << endl;
	cout << "\t[-u url] - url to RTSP stream." << endl;
	cout << "\t[-f filename] -  video file containing a face." << endl;
	cout << "\t[-d directory] - directory containing face images." << endl;
	return 1;
}

static void PrintNleFeaturePoint(const NStringWrapper& name, NLFeaturePoint point)
{
	if (point.Confidence == 0)
		cout << "\t\t " << name.GetString() << " (feature code) " << point.Code << " feature unavailable. \tConfidence: 0" << endl;
	else
		cout << "\t\t" << name.GetString() << " feature found. X: " << point.X << " Y: " << point.Y << " ,confidence: " << point.Confidence << endl;
}

static void PrintFaceAttributes(const NFace & face, bool additionalComponents)
{
	for (int i = 0; i < face.GetObjects().GetCount(); i++)
	{
		NLAttributes attributes = face.GetObjects().Get(i);
		cout << "Location = " << attributes.GetBoundingRect().X << " " << attributes.GetBoundingRect().Y << ", width = " << attributes.GetBoundingRect().Width << ", height = " << attributes.GetBoundingRect().Height << endl;
		PrintNleFeaturePoint("LeftEyeCentre", attributes.GetLeftEyeCenter());
		PrintNleFeaturePoint("RightEyeCenter", attributes.GetRightEyeCenter());
		if (additionalComponents)
		{
			PrintNleFeaturePoint("MouthCenter", attributes.GetMouthCenter());
			PrintNleFeaturePoint("NoseTip", attributes.GetNoseTip());
		}
	}
}

int main(int argc, NChar **argv)
{
	const int MAX_FRAMES = 100;
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
		biometricClient.SetFacesTemplateSize(ntsMedium);
		bool isAdditionalComponentActivated = NLicense::IsComponentActivated(additionalComponents);
		if (isAdditionalComponentActivated)
		{
			biometricClient.SetFacesDetectBaseFeaturePoints(true);
		}

		NFace face;
		face.SetHasMoreSamples(true);
		NSubject subject;
		subject.GetFaces().Add(face);
		NMediaReader reader = NULL;
		NFileEnumerator enumerator = NULL;
		bool next = true;

		if (strcmp(N_T("-f"), argv[1]) == 0)
		{
			reader = NMediaReader(NMediaSource::FromFile(argv[2]),nmtVideo, true);
		}
		else if (strcmp(N_T("-u"), argv[1]) == 0)
		{
			reader = NMediaReader(NMediaSource::FromUrl(argv[2]),nmtVideo, true);
		}
		else if (strcmp(N_T("-d"), argv[1]) == 0)
		{
			enumerator = NFileEnumerator(argv[2]);
			next = enumerator.MoveNext();
		}
		else NThrowException("Unknown input options specified!");

		NImage image = NULL;
		int frameCount = 0;
		bool isReaderUsed = !reader.IsNull();
		NBiometricTask task = biometricClient.CreateTask(nboDetectSegments, subject);
		if (isReaderUsed)
			reader.Start();

		while (next)
		{
			frameCount++;
			if (isReaderUsed)
			{
				if(frameCount > MAX_FRAMES)
				{
					break;
				}
				image = reader.ReadVideoSample();
			}
			else
			{
				image = NULL;
				while(next)
				{
					if (enumerator.GetFileAttributes() == nfatDirectory)
					{
						next = enumerator.MoveNext();
					}
					else
					{
						NString fileName = NPath::Combine(argv[2], enumerator.GetFileName());
						image = NImage::FromFile(fileName);
						next = enumerator.MoveNext();
						break;
					}
				}
			}

			if (image.IsNull())
				break;

			face.SetImage(image);
			biometricClient.PerformTask(task);
			cout << frameCount << " detection status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), task.GetStatus()) << endl;
			if (task.GetStatus() == nbsOk)
			{
				PrintFaceAttributes(face, isAdditionalComponentActivated);
			}
			else if (task.GetStatus() != nbsObjectNotFound)
			{
				cout << "Detection failed! Status: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), task.GetStatus()) << endl;
				if (!task.GetError().IsNull())
					throw task.GetError();
				return -1;
			}
		}

		if (isReaderUsed)
			reader.Stop();
		face.SetHasMoreSamples(false);
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
