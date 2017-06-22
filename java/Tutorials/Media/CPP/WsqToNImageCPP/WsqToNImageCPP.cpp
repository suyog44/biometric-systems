#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NMedia/NMedia.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NMedia.hpp>
	#include <NLicensing.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::Licensing;
using namespace Neurotec::Images;

const NChar title[] = N_T("WsqToNImage");
const NChar description[] = N_T("Demonstrates WSQ to NImage conversion");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [srcImage] [dstImage]" << endl << endl;
	cout << "\tsrcImage - filename of source WSQ image." << endl;
	cout << "\tdstImage - name of a file to save converted image to." << endl;
	return 1;
}

int main(int argc, NChar **argv)
{
	const NChar * components = N_T("Images.WSQ");
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

		NImage image = NImage::FromFile(argv[1], NImageFormat::GetWsq());
		cout << "Loaded WSQ bitrate: " << NObjectDynamicCast<WsqInfo>(image.GetInfo()).GetBitRate() << endl;
		NImageFormat dstFormat = NImageFormat::GetJpeg();
		image.Save(argv[2], dstFormat);
		cout << dstFormat.GetName() << " image was saved to " << argv[2] << endl;
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
