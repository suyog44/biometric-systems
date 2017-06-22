using System;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			try
			{

				NLicManDongle dongle = NLicenseManager.FindFirstDongle();
				if (dongle == null)
				{
					Console.WriteLine("no dongles found");
					return -1;
				}

				do
				{
					Console.WriteLine("=== Dongle Id: {0} ===\n", dongle.DistributorId);
					NLicenseProductInfo[] licenses = dongle.GetLicenses();
					foreach (NLicenseProductInfo license in licenses)
					{
						Console.WriteLine("{0} OS: {1}, Count: {2}", NLicenseManager.GetShortProductName(license.Id, license.LicenseType), license.OSFamily, license.LicenseCount);
					}

					dongle = NLicenseManager.FindNextDongle();
					if (dongle == null)
					{
						Console.WriteLine("no more dongles found");
					}
				} while (dongle != null);

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}
	}
}
