#include <TutorialUtils.hpp>

#ifdef N_MAC_OSX_FRAMEWORKS
	#include <NCore/NCore.hpp>
	#include <NLicensing/NLicensing.hpp>
#else
	#include <NCore.hpp>
	#include <NLicensing.hpp>
#endif

using namespace std;
using namespace Neurotec;
using namespace Neurotec::IO;
using namespace Neurotec::Licensing;

const NChar title[] = N_T("IdInfo");
const NChar description[] = N_T("Demonstrates id information retrieval from id file");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

static int usage()
{
	cout << "usage:" << endl;
	cout << "\t" << title << "[id file name]" << endl;
	return 1;
}

int main(int argc, NChar *argv[])
{
	OnStart(title, description, version, copyright, argc, argv);

	if (argc != 2)
	{
		return usage();
	}

	try
	{
		NString id = NFile::ReadAllText(argv[1]);
		NInt sequenceNumber, distributorId;
		NUInt productId;
		NLicenseManager::GetLicenseData(id, &sequenceNumber, &productId, &distributorId);

		cout << "Sequence number: " << sequenceNumber << endl;
		cout << "Distributor id: " << distributorId << endl;
		cout << "Product: " << NLicenseManager::GetShortProductName(productId, nltSingleComputer);
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
