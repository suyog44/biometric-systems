using System;
using System.IO;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Standards;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [NTemplate] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[NTemplate]     - filename of NTemplate.");
			Console.WriteLine("\t[ANTemplate]    - filename of ANTemplate.");
			Console.WriteLine("\t[Tot] - specifies type of transaction.");
			Console.WriteLine("\t[Dai] - specifies destination agency identifier.");
			Console.WriteLine("\t[Ori] - specifies originating agency identifier.");
			Console.WriteLine("\t[Tcn] - specifies transaction control number.");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			// Depending on NTemplate contents choose the licenses: if you will have only finger templates in NTemplate - leave finger templates license only.
			const string Components = "Biometrics.Standards.FingerTemplates,Biometrics.Standards.PalmTemplates";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 6)
			{
				return Usage();
			}

			if (args[0] == "/?" || args[0] == "help")
			{
				return Usage();
			}

			try
			{
				string nTemplateFileName = args[0];

				string tot = args[2]; // type of transaction
				string dai = args[3]; // destination agency identifier
				string ori = args[4]; // originating agency identifier
				string tcn = args[5]; // transaction control number

				if ((tot.Length < 3) || (tot.Length > 4))
				{
					Console.WriteLine("Tot parameter should be 3 or 4 characters length.");
					return -1;
				}

				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				byte[] packedNTemplate = File.ReadAllBytes(nTemplateFileName);

				// Creating NTemplate object from packed NTemplate
				using(var nTemplate = new NTemplate(packedNTemplate))
				// Creating ANTemplate object from NTemplate object
				using (var anTemplate = new ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, true, nTemplate))
				{
					// Storing ANTemplate object in file
					anTemplate.Save(args[1]);
					Console.WriteLine("Program produced file: " + args[1]);
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
