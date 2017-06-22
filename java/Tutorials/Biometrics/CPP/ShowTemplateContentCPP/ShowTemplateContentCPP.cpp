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
using namespace Neurotec::Text;

const NChar title[] = N_T("ShowTemplateContent");
const NChar description[] = N_T("Demonstrates methods and functions to access internal template information.");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << " [NTemplate] ..." << endl << endl;
	cout << "\t[NTemplate]  - NTemplate filename." << endl;
	return 1;
}

static int RotationToDegrees(int rotation)
{
	return (2 * rotation * 360 + 256) / (2 * 256);
}

static void PrintNSRecord(NSRecord nsRec)
{
	cout << "\tphrase id: " << nsRec.GetPhraseId() << endl;
	cout << "\tsize: " << nsRec.GetSize() << endl;
}

static void PrintNERecord(NERecord neRec)
{
	cout << "\tposition: " << neRec.GetPosition() << endl;
	cout << "\tsize: " << neRec.GetSize() << endl;
}

static void PrintNLRecord(NLRecord nlRec)
{
	cout << "\tquality: " << nlRec.GetQuality() << endl;
	cout << "\tsize: " << nlRec.GetSize() << endl;
}

static void PrintNFRecord(NFRecord nfRec)
{
	cout << "\tg: " << nfRec.GetG() << endl;
	cout << "\timpression type: " << NEnum::ToString(NBiometricTypes::NFImpressionTypeNativeTypeOf(), nfRec.GetImpressionType()) << endl;
	cout << "\tpattern class: " << NEnum::ToString(NBiometricTypes::NFPatternClassNativeTypeOf(), nfRec.GetPatternClass()) << endl;
	cout << "\tcbeff product type: " << nfRec.GetCbeffProductType() << endl;
	cout << "\tposition: " << NEnum::ToString(NBiometricTypes::NFPositionNativeTypeOf(), nfRec.GetPosition()) << endl;
	cout << "\tridge counts type: " << NEnum::ToString(NBiometricTypes::NFRidgeCountsTypeNativeTypeOf(), nfRec.GetRidgeCountsType()) << endl;
	cout << "\twidth: " << nfRec.GetWidth() << endl;
	cout << "\theight: " << nfRec.GetHeight() << endl;
	cout << "\thorizontal resolution: " << nfRec.GetHorzResolution() << endl;
	cout << "\tvertical resolution: " << nfRec.GetVertResolution() << endl;
	cout << "\tquality: " << nfRec.GetQuality() << endl;
	cout << "\tsize: " << nfRec.GetSize() << endl;
	cout << "\tminutia count " << nfRec.GetMinutiae().GetCount() << endl;

	NFMinutiaFormat minutiaFormat = nfRec.GetMinutiaFormat();
	int minutiaeCount = nfRec.GetMinutiae().GetCount();
	for (int i = 0; i < minutiaeCount; i++)
	{
		NFMinutia minutia = nfRec.GetMinutiae().Get(i);
		cout << "\tminutia " << i + 1 << " of " << minutiaeCount << endl;
		cout << "\tx: " << minutia.X << endl;
		cout << "\ty: " << minutia.Y << endl;
		cout << "\tangle: " << RotationToDegrees(minutia.Angle) << endl;
		if (minutiaFormat & nfmfHasQuality)
		{
			cout << "\tquality: " << minutia.Quality << endl;
		}
		if (minutiaFormat & nfmfHasG)
		{
			//not displaying correctly
			cout << "\tg: " << minutia.G << endl;
		}
		if (minutiaFormat & nfmfHasCurvature)
		{
			//not displaying correctly
			cout << "\tcurvature: " << minutia.Curvature << endl;
		}
	}

	int deltasCount = nfRec.GetDeltas().GetCount();
	for (int i = 0; i < deltasCount; i++)
	{
		NFDelta delta = nfRec.GetDeltas().Get(i);
		cout << "\tdelta " << i + 1 << " of " << deltasCount << endl;
		cout << "\tx: " << delta.X << endl;
		cout << "\ty: " << delta.Y << endl;
		cout << "\tangle1: " << RotationToDegrees(delta.Angle1) << endl;
		cout << "\tangle2: " << RotationToDegrees(delta.Angle2) << endl;
		cout << "\tangle3: " << RotationToDegrees(delta.Angle3) << endl;
	}

	int coresCount = nfRec.GetCores().GetCount();
	for (int i = 0; i < coresCount; i++)
	{
		NFCore core = nfRec.GetCores().Get(i);
		cout << "\tcore " << i + 1 << " of " << coresCount << endl;
		cout << "\tx: " << core.X << endl;
		cout << "\ty: " << core.Y << endl;
		cout << "\tangle " << RotationToDegrees(core.Angle) << endl;
	}

	int dcoresCount = nfRec.GetDoubleCores().GetCount();
	for (int i = 0; i < dcoresCount; i++)
	{
		NFDoubleCore doubleCore = nfRec.GetDoubleCores().Get(i);
		cout << "\tdoubleCore " << i + 1 << " of " << dcoresCount << endl;
		cout << "\tx: " << doubleCore.X << endl;
		cout << "\ty: " << doubleCore.Y << endl;
	}
}

int main(int argc, NChar **argv)
{
	OnStart(title, description, version, copyright, argc, argv);

	if(argc < 2)
	{
		OnExit();
		return usage();
	}

	try
	{
		NTemplate ntemplate(NFile::ReadAllBytes(argv[1]));
		cout << "Template" << argv[1] << "contains " << endl;
		if (ntemplate.GetFingers() != NULL)
		{
			int fingerCount = ntemplate.GetFingers().GetRecords().GetCount();
			cout << fingerCount << " fingers" << endl;
			for (int i = 0; i < fingerCount; i++)
			{
				NFRecord nfRec = ntemplate.GetFingers().GetRecords().Get(i);
				PrintNFRecord(nfRec);
			}
		}
		else
			cout << "0 fingers" << endl;

		if (ntemplate.GetFaces() != NULL)
		{
			int faceCount = ntemplate.GetFaces().GetRecords().GetCount();
			cout << faceCount << " faces" << endl;
			for (int i = 0; i < faceCount ; i++)
			{
				NLRecord nlRec = ntemplate.GetFaces().GetRecords().Get(i);
				PrintNLRecord(nlRec);
			}
		}
		else
			cout << "0 faces" << endl;

		if (ntemplate.GetIrises() !=NULL)
		{
			int irisCount = ntemplate.GetIrises().GetRecords().GetCount();
			cout << irisCount << " irises" << endl;
			for (int i = 0; i < irisCount; i++)
			{
				NERecord neRec = ntemplate.GetIrises().GetRecords().Get(i);
				PrintNERecord(neRec);
			}
		}
		else
			cout << "0 irises " << endl;

		if (ntemplate.GetVoices() != NULL)
		{
			int voiceCount = ntemplate.GetVoices().GetRecords().GetCount();
			cout << voiceCount << " voices" << endl;
			for (int i = 0; i < voiceCount; i++)
			{
				NSRecord nsRec = ntemplate.GetVoices().GetRecords().Get(i);
				PrintNSRecord(nsRec);
			}
		}
		else
			cout << "0 voices" << endl;

		if (ntemplate.GetPalms() != NULL)
		{
			int palmCount = ntemplate.GetPalms().GetRecords().GetCount();
			cout << palmCount << " palms" << palmCount << endl;
			for (int i = 0; i < palmCount; i++)
			{
				NFRecord nfRec = ntemplate.GetPalms().GetRecords().Get(i);
				PrintNFRecord(nfRec);
			}
		}
		else
			cout << "0 palms" << endl;
	}

	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
