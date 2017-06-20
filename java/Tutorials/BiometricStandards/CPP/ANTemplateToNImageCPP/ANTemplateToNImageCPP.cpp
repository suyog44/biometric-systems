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
using namespace Neurotec::Images;
using namespace Neurotec::Licensing;
using namespace Neurotec::Biometrics;
using namespace Neurotec::Biometrics::Standards;

const NChar title[] = N_T("ANTemplateToNImage");
const NChar description[] = N_T("Demonstrates how to save images stored in ANTemplate");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [ANTemplate]" << endl << endl;
	cout << "\tANTemplate - filename of ANTemplate" << endl;
	return 1;
}

int main(int argc, NChar ** argv)
{
	const NChar * components = N_T("Biometrics.Standards.Base,Biometrics.Standards.PalmTemplates,Biometrics.Standards.Irises,Biometrics.Standards.Faces");
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
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

		ANTemplate ntemplate(argv[1], anvlStandard);
		for (int i = 0; i < ntemplate.GetRecords().GetCount(); i++)
		{
			ANRecord record = ntemplate.GetRecords().Get(i);
			NImage image = NULL;
			int number = record.GetRecordType().GetNumber();
			if (number >= 3 && number <=8 && number !=7)
				image = NObjectDynamicCast<ANImageBinaryRecord>(record).ToNImage();
			else if (number >= 10 && number <= 17)
				image = NObjectDynamicCast<ANImageAsciiBinaryRecord>(record).ToNImage();
			if (!image.IsNull())
			{
				NString fileName = NString::Format("record{I}_type{I}.jpg", i + 1, number);
				image.Save(fileName);
				cout << "Image saved to " << fileName << endl;
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
