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

const NChar title[] = N_T("IIRecordFromNImage");
const NChar description[] = N_T("Demonstrates creation of IIRecord from image");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [IIRecord] [Standard] [Version] {[image...]}" << endl << endl;
	cout << "\tIIRecord - output IIRecord" << endl;
	cout << "\t[Standard] - standard for the record (ISO or ANSI)" << endl;
	cout << "\t[Version] - version for the record" << endl;
	cout << "\t\t 1 - ANSI/INCITS 379-2004" << endl;
	cout << "\t\t 1 - ISO/IEC 19794-6:2005" << endl;
	cout << "\t\t 2 - ISO/IEC 19794-6:2011" << endl;
	cout << "\timage    - one or more images" << endl;
	return 1;
}

int main(int argc, NChar *argv[])
{
	const NChar * components = N_T("Biometrics.Standards.Irises");
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

		BdifStandard standard;
		if (!strcmp(argv[2], N_T("ANSI")))
			standard = bsAnsi;
		else if (!strcmp(argv[2], N_T("ISO")))
			standard = bsIso;
		else
			NThrowException("Wrong standard");

		NVersion version;
		bool isFirstVersion = false;
		if (!strcmp(argv[3], N_T("1")))
		{
			version = standard == bsAnsi ? IIR_VERSION_ANSI_1_0 : IIR_VERSION_ISO_1_0;
			isFirstVersion = true;
		}
		else if (!strcmp(argv[3], N_T("2")))
		{
			if (standard != bsIso)
				NThrowException("Standard and version is incompatible!");
			version = IIR_VERSION_ISO_2_0;
		}
		else
			NThrowException("Wrong version!");

		IIRecord iiRec = NULL;
		for (int i = 4; i < argc; i++)
		{
			NImage imageFromFile = NImage::FromFile(argv[i]);
			NImage image = NImage::FromImage(NPF_GRAYSCALE_8U, 0, imageFromFile);

			if (iiRec.IsNull())
			{
				iiRec = IIRecord(standard, version);
				if (isFirstVersion)
				{
					iiRec.SetRawImageHeight((NUShort)image.GetHeight());
					iiRec.SetRawImageWidth((NUShort)image.GetWidth());
					iiRec.SetIntensityDepth(8);
				}
			}

			IirIrisImage iirIrisImage(standard, version);
			if (!isFirstVersion)
			{
				iirIrisImage.SetImageHeight((NUShort)image.GetHeight());
				iirIrisImage.SetImageWidth((NUShort)image.GetWidth());
				iirIrisImage.SetIntensityDepth(8);
			}
			iirIrisImage.SetImage(image);
			iiRec.GetIrisImages().Add(iirIrisImage);
		}

		if (!iiRec.IsNull())
		{
			NFile::WriteAllBytes(argv[1], iiRec.Save());
			cout << "IIrecord saved to " << argv[1];
		}
		else
		{
			cout << "No images were added to IIRecord" << endl;;
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
