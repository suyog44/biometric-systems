using System;
using System.IO;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [NTemplate] [CbeffRecord] [PatronFormat]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[NTemplate] - filename of NTemplate.");
			Console.WriteLine("\t[CbeffRecord] - filename of CbeffRecord.");
			Console.WriteLine("\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation).");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Base";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 3)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				// Read NTemplate buffer
				var packedNTemplate = new NBuffer(File.ReadAllBytes(args[0]));

				// Combine NTemplate BDB format
				uint bdbFormat = BdifTypes.MakeFormat(CbeffBiometricOrganizations.Neurotechnologija, CbeffBdbFormatIdentifiers.NeurotechnologijaNTemplate);

				// Get CbeffRecord patron format
				// all supported patron formats can be found in CbeffRecord class documentation
				uint patronFormat = uint.Parse(args[2], System.Globalization.NumberStyles.HexNumber);

				// Create CbeffRecord from NTemplate buffer
				using (var cbeffRecord = new CbeffRecord(bdbFormat, packedNTemplate, patronFormat))
				{
					// Saving NTemplate
					File.WriteAllBytes(args[1], cbeffRecord.Save().ToArray());
					Console.WriteLine("Template successfully saved");
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				NLicense.ReleaseComponents(Components);
			}
		}
	}
}
