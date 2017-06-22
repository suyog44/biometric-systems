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
using namespace Neurotec::Licensing;

const NChar title[] = N_T("DongleInfo");
const NChar description[] = N_T("Demonstrates dongle information retrieval");
const NChar version[] = N_T("9.0.0.0");
const NChar copyright[] = N_T("Copyright (C) 2016-2017 Neurotechnology");

int main()
{
	OnStart(title, description, version, copyright, 0, NULL);

	try
	{
		NLicManDongle dongle = NLicenseManager::FindFirstDongle();
		if (dongle.IsNull())
		{
			cout << "No dongles found";
			return -1;
		}

		do
		{
			cout << "=== Dongle Id: " << dongle.GetDistributorId() << " ===";
			for (int i = 0; i < dongle.GetLicenses().GetCount(); i++)
			{
				NLicenseProductInfo licenseInfo = dongle.GetLicenses().Get(i);
				cout << NLicenseManager::GetShortProductName(licenseInfo.GetId(), licenseInfo.GetLicenseType()) << " OS: " << licenseInfo.GetOSFamily()
					 << ", Count: " << licenseInfo.GetLicenseCount() << endl;
			}

			dongle = NLicenseManager::FindNextDongle();
			if (dongle.IsNull())
			{
				cout << "No more dongles found";
			}
		} while (!dongle.IsNull());
	}
	catch (NError& ex)
	{
		return LastError(ex);
	}

	OnExit();
	return 0;
}
