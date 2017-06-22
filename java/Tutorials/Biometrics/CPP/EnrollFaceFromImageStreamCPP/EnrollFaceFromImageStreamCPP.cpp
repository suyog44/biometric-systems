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
using namespace Neurotec::Media;
using namespace Neurotec::Images;

const NChar title[] = N_T("EnrollFaceFromImageStream");
const NChar description[] = N_T("Demonstrates enrollment from image stream");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [output template] [-u url]" << endl;
	cout << "\t" << title << " [output template] [-f filename]" << endl;
	cout << "\t" << title << " [output template] [-d directory]" << endl << endl;
	cout << "\t[-u url] - url to RTSP stream." << endl;
	cout << "\t[-f filename] -  video file containing a face." << endl;
	cout << "\t[-d directory] - directory containing face images." << endl << endl;
	cout << "example: " << title << " template.dat -f video.avi" << endl;
	cout << "example: " << title << " template.dat -u rtsp://camera_url" << endl;
	cout << "example: " << title << " template.dat -d C:\templates" << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	
	NString components = N_T("Biometrics.FaceExtraction");
	const NString additionalComponents = N_T("Biometrics.FaceSegmentsDetection");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 4)
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

		NBiometricClient biometricClient;
		biometricClient.SetFacesTemplateSize(ntsLarge);
		bool isAdditionalComponentActivated = NLicense::IsComponentActivated(additionalComponents);
		biometricClient.SetFacesDetectAllFeaturePoints(isAdditionalComponentActivated);

		NSubject subject;
		NFace face;
		face.SetHasMoreSamples(true);
		subject.GetFaces().Add(face);

		NMediaReader reader = NULL;
		NFileEnumerator enumerator = NULL;
		bool next = false;

		if (strcmp(N_T("-f"), argv[2]) == 0)
		{
			reader = NMediaReader(NMediaSource::FromFile(argv[3]),nmtVideo, true);
		}
		else if (strcmp(N_T("-u"), argv[2]) == 0)
		{
			reader = NMediaReader(NMediaSource::FromUrl(argv[3]),nmtVideo, true);
		}
		else if (strcmp(N_T("-d"), argv[2]) == 0)
		{
			enumerator = NFileEnumerator(argv[3]);
			next = enumerator.MoveNext();
		}
		else NThrowException("Unknown input options specified!");

		bool isReaderUsed = !reader.IsNull();
		NImage image = NULL;
		NBiometricStatus status = nbsNone;
		NBiometricTask task = biometricClient.CreateTask(nboCreateTemplate, subject);
		if (isReaderUsed)
			reader.Start();

		while (status == nbsNone)
		{
			if (isReaderUsed)
			{
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
						NString fileName = NPath::Combine(argv[3], enumerator.GetFileName());
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
			if (!task.GetError().IsNull())
			{
				throw task.GetError();
			}
			status = task.GetStatus();
		}

		if (isReaderUsed)
			reader.Stop();

		face.SetHasMoreSamples(false);
		if (image.IsNull())
		{
			biometricClient.PerformTask(task);
			status = task.GetStatus();
		}

		if (status == nbsOk)
		{
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
			cout << "Template extracted" << endl;
			NFile::WriteAllBytes(argv[1], subject.GetTemplateBuffer());
			cout << "Template saved successfully" << endl;
		}
		else
		{
			cout << "Extraction failed: " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), status) << endl;
			if (!task.GetError().IsNull()) throw task.GetError();
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
