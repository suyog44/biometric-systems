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

const NChar title[] = N_T("SegmentFingers");
const NChar description[] = N_T("Demonstrates fingerprint image segmentation.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [image] [position] <optional: missing positions> ... " << endl;
	cout << "\t[image]             - image containing fingerprints" << endl;
	cout << "\t[position]          - fingerprints position in provided image" << endl;
	cout << "\t[missing positions] - one or more NFPosition value of missing fingers" << endl << endl;
	cout << "\tvalid positions:" << endl;
	cout << "\t\tPlainRightFourFingers = 13, PlainLeftFourFingers = 14, PlainThumbs = 15" << endl;
	cout << "\t\tRightThumb = 1, RightIndex = 2, RightMiddle = 3, RightRing = 4, RightLittle = 5" << endl;
	cout << "\t\tLeftThumb = 6, LeftIndex = 7, LeftMiddle = 8, LeftRing = 9, LeftLittle = 10" << endl << endl;
	cout << "\texample: " << title << " image.png 15" << endl;
	cout << "\texample: " << title << " image.png 13 2 3" << endl;
	return 1;
}

int main(int argc, NChar * * argv)
{
	const NChar * components = N_T("Biometrics.FingerSegmentation,Biometrics.FingerExtraction");
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

		NFinger finger;
		finger.SetFileName(argv[1]);
		finger.SetPosition(NFPosition(NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), argv[2])));
		NSubject subject;
		subject.GetFingers().Add(finger);
		for (int i = 3; i < argc ; i++)
		{
			subject.GetMissingFingers().Add(NFPosition(NEnum::Parse(NBiometricTypes::NFPositionNativeTypeOf(), argv[i])));
		}

		NBiometricClient biometricClient;
		NBiometricTask task = biometricClient.CreateTask((NBiometricOperations)(nboSegment | nboCreateTemplate), subject);
		biometricClient.PerformTask(task);
		NBiometricStatus status = task.GetStatus();
		if (status == nbsOk)
		{
			if (finger.GetWrongHandWarning())
			{
				cout << "Warning: possibly wrong hand." << endl;
			}
			int segmentCount = subject.GetFingers().GetCount();
			cout << "Found " << segmentCount - 1 << " segments:" << endl;
			for (int i = 1; i < segmentCount; i++)
			{
				NFinger segmentedFinger = subject.GetFingers().Get(i);
				if (segmentedFinger.GetStatus() == nbsOk)
				{
					cout << "\t" << NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), segmentedFinger.GetPosition()) << endl;
					segmentedFinger.GetImage().Save((NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), segmentedFinger.GetPosition())) + ".png");
					cout << "Saving image..." << endl;
				}
				else cout << "\t" << NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), segmentedFinger.GetPosition()) << ": " << NEnum::ToString(NBiometricTypes::NBiometricStatusNativeTypeOf(), segmentedFinger.GetStatus()) << endl;
			}
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
