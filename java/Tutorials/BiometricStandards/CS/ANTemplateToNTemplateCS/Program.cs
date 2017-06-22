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
			Console.WriteLine("\t{0} [ANTemplate] [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[ATemplate] - filename of ANTemplate.");
			Console.WriteLine("\t[NTemplate] - filename of NTemplate.");
			Console.WriteLine("");
			Console.WriteLine("examples:");
			Console.WriteLine("\t{0} antemplate.data nTemplate.data", TutorialUtils.GetAssemblyName());

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.FingerTemplates";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				string aNTemplateFileName = args[0];

				// Creating ANTemplate object from file
				using(var anTemplate = new ANTemplate(aNTemplateFileName, ANValidationLevel.Standard))
				// Converting ANTemplate object to NTemplate object
				using (NTemplate nTemplate = anTemplate.ToNTemplate())
				{
					// Packing NTemplate object
					byte[] packedNTemplate = nTemplate.Save().ToArray();

					// Storing NTemplate object in file
					File.WriteAllBytes(args[1], packedNTemplate);
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
