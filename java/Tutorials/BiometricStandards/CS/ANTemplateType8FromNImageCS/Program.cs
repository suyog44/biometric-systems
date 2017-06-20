using System;
using Neurotec.Biometrics.Standards;
using Neurotec.Images;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [Signature] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[Signature]  - filename with signature image.");
			Console.WriteLine("\t[ANTemplate] - filename for ANTemplate.");
			Console.WriteLine("\t[Tot] - specifies type of transaction.");
			Console.WriteLine("\t[Dai] - specifies destination agency identifier.");
			Console.WriteLine("\t[Ori] - specifies originating agency identifier.");
			Console.WriteLine("\t[Tcn] - specifies transaction control number.");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Other";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 6)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				string tot = args[2]; // type of transaction
				string dai = args[3]; // destination agency identifier
				string ori = args[4]; // originating agency identifier
				string tcn = args[5]; // transaction control number

				if ((tot.Length < 3) || (tot.Length > 4))
				{
					Console.WriteLine("Tot parameter should be 3 or 4 characters length.");
					return -1;
				}

				// Create empty ANTemplate object with only type 1 record in it
				using (var template = new ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, 0))
				using (NImage lrBinImage = NImage.FromFile(args[0]))
				{
					lrBinImage.HorzResolution = 500;
					lrBinImage.VertResolution = 500;
					lrBinImage.ResolutionIsAspectRatio = false;

					// Create Type 8 record
					var record = new ANType8Record(ANTemplate.VersionCurrent, 0, ANSignatureType.Official, ANSignatureRepresentationType.ScannedUncompressed, true, lrBinImage);
					// Add Type 8 record to ANTemplate object
					template.Records.Add(record);

					// Store ANTemplate object with type 8 record in file
					template.Save(args[1]);
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
