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
using namespace Neurotec::IO;
using namespace Neurotec::Images;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Standards;

const NChar title[] = N_T("FCRecordFromNImage");
const NChar description[] = N_T("Demonstrates creation of FCRecord from image");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [FCRecord] {[image]}" << endl << endl;
	cout << "\tFCRecord - output FCRecord" << endl;
	cout << "\timage    - one or more images" << endl;
	return 1;
}

int main(int argc, NChar *argv[])
{
	const NChar * components = N_T("Biometrics.Standards.Faces");
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

		if (argc > 2)
		{
			FCRecord fcRecord = FCRecord(bsIso, FCR_VERSION_ISO_3_0);
			for (int i = 2; i < argc; i++)
			{
				NImage imageFromFile = NULL;
				NImage grayscaleImage = NULL;
				imageFromFile = NImage::FromFile(argv[i]);
				grayscaleImage = NImage::FromImage(NPF_GRAYSCALE_8U, 0, imageFromFile);
				FcrFaceImage fcrImage(bsIso, FCR_VERSION_ISO_3_0);
				fcrImage.SetFaceImageType(fcrfitBasic);
				fcrImage.SetImageDataType(fcridtJpeg);
				fcrImage.SetImage(grayscaleImage);
				fcRecord.GetFaceImages().Add(fcrImage);
			}

			NFile::WriteAllBytes(argv[1], fcRecord.Save());
			cout << "FCRecord saved to " << argv[1];
		}
		else
			cout << "No images were added to FCRecord";
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
