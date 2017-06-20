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

const NChar title[] = N_T("FIRecordFromImage");
const NChar description[] = N_T("Demonstrates creation of FIRecord from image");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << "[FIRecord] [Standard] [Version] {[image]}" << endl << endl;
	cout << "\tFIRecord - output FIRecord" << endl;
	cout << "\t[Standard] - standard for the record (ISO or ANSI)" << endl;
	cout << "\t[Version] - version for the record" << endl;
	cout << "\t\t 1 - ANSI/INCITS 381-2004" << endl;
	cout << "\t\t 1 - ISO/IEC 19794-4:2005" << endl;
	cout << "\t\t 2 - ISO/IEC 19794-4:2011" << endl;
	cout << "\timage    - one or more images" << endl;
	return 1;
}

int main(int argc, NChar *argv[])
{
	const NChar * components = N_T("Biometrics.Standards.Fingers");
	OnStart(title, description, version, copyright, argc, argv);

	if (argc < 5)
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
		
		BdifStandard standard = bsIso;
		NVersion version;
		bool isFirstVersion = false;
		if (!strcmp(argv[2], N_T("ANSI")))
			standard = bsAnsi;
		else if (!strcmp(argv[2], N_T("ISO")))
			standard = bsIso;
		else
			NThrowException("Wrong standard!");

		if (!strcmp(argv[3], N_T("1")))
		{
			if (standard == bsAnsi)
				version = FIR_VERSION_ANSI_1_0;
			else
				version = FIR_VERSION_ISO_1_0;
			isFirstVersion = true;
		}
		else if (!strcmp(argv[3], N_T("2")))
		{
			if (standard != bsIso)
				NThrowException("Standard and version is incompatible!");
			version = FIR_VERSION_ISO_2_0;
		}
		else if (!strcmp(argv[3], N_T("2.5")))
		{
			if (standard !=bsAnsi)
				NThrowException("Standard and version is incompatible!");
			version = FIR_VERSION_ANSI_2_5;
		}
		else
			NThrowException("Wrong version");

		FIRecord fiRecord = NULL;
		for (int i = 4; i < argc; i++)
		{
			NImage imageFromFile = NULL;
			NImage grayscaleImage = NULL;
			imageFromFile = NImage::FromFile(argv[i]);
			grayscaleImage = NImage::FromImage(NPF_GRAYSCALE_8U, 0, imageFromFile);
			if (grayscaleImage.GetResolutionIsAspectRatio() || grayscaleImage.GetHorzResolution() < 250 || grayscaleImage.GetVertResolution() < 250)
			{
				grayscaleImage.SetHorzResolution(500);
				grayscaleImage.SetVertResolution(500);
				grayscaleImage.SetResolutionIsAspectRatio(false);
			}

			if (fiRecord.IsNull())
			{
				fiRecord = FIRecord(standard, version);
				if (isFirstVersion)
				{
					fiRecord.SetPixelDepth(8);
					fiRecord.SetHorzImageResolution((NUShort)grayscaleImage.GetHorzResolution());
					fiRecord.SetHorzScanResolution((NUShort)grayscaleImage.GetHorzResolution());
					fiRecord.SetVertImageResolution((NUShort)grayscaleImage.GetVertResolution());
					fiRecord.SetVertScanResolution((NUShort)grayscaleImage.GetVertResolution());
				}
			}
			FirFingerView fingerView(standard, version);

			if (!isFirstVersion)
			{
				fingerView.SetPixelDepth(8);
				fingerView.SetHorzImageResolution((NUShort)grayscaleImage.GetHorzResolution());
				fingerView.SetHorzScanResolution((NUShort)grayscaleImage.GetHorzResolution());
				fingerView.SetVertImageResolution((NUShort)grayscaleImage.GetVertResolution());
				fingerView.SetVertScanResolution((NUShort)grayscaleImage.GetVertResolution());
			}

			if (!fingerView.IsNull())
			{
				cout << "fingerView is not NULL" << endl;
			}
			fiRecord.GetFingerViews().Add(fingerView);
			fingerView.SetImage(grayscaleImage);
		}

		if (!fiRecord.IsNull())
		{
			NFile::WriteAllBytes(argv[1], fiRecord.Save());
			cout << "FIRecord saved to " << argv[1] << endl;
		}
		else
			cout << "No images were added to FIRecord" << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
