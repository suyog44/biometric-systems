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

const NChar title[] = N_T("ANTemplateType16FromNImage");
const NChar description[] = N_T("Demonstrates creation of ANTemplate with type 16 record in it");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn] [Src] [Udi]" << endl << endl;
	cout << "\tNImage     - filename with image file" << endl;
	cout << "\tANTemplate - filename for ANTemplate" << endl;
	cout << "\tTot - specifies type of transaction" << endl;
	cout << "\tDai - specifies destination agency identifier" << endl;
	cout << "\tOri - specifies originating agency identifier" << endl;
	cout << "\tTcn - specifies transaction control number" << endl;
	cout << "\tSrc - specifies source agency number" << endl;
	cout << "\tUdi -\tUdi - specifies type of user-defined image" << endl;
	return 1;
}

int main(int argc, NChar ** argv)
{
	const NChar * components = N_T("Biometrics.Standards.Base");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 9)
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

		NString tot = argv[3];
		NString dai = argv[4];
		NString ori = argv[5];
		NString tcn = argv[6];
		NString src = argv[7];
		NString udi = argv[8];

		if (tot.GetLength() < 3 || tot.GetLength() > 4)
		{
			cout << "Tot parameter should be 3 or 4 characters length.";
			return -1;
		}

		ANTemplate antemplate(AN_TEMPLATE_VERSION_CURRENT, tot, dai, ori, tcn, 0);
		NImage image = NImage::FromFile(argv[1]);
		NImage rgbImage = NImage::FromImage(NPF_RGB_8U, 0, image);
		rgbImage.SetResolutionIsAspectRatio(true);
		ANType16Record record(AN_TEMPLATE_VERSION_CURRENT, 0, udi, src, bsuNone, anicaNone, rgbImage);
		antemplate.GetRecords().Add(record);
		antemplate.Save(argv[2]);
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	NLicense::ReleaseComponents(components);
	OnExit();
	return 0;
}
