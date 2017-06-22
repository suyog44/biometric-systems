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
			Console.WriteLine("\t{0} [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn] [Src]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[NImage]     - filename with image file.");
			Console.WriteLine("\t[ANTemplate] - filename for ANTemplate.");
			Console.WriteLine("\t[Tot] - specifies type of transaction.");
			Console.WriteLine("\t[Dai] - specifies destination agency identifier.");
			Console.WriteLine("\t[Ori] - specifies originating agency identifier.");
			Console.WriteLine("\t[Tcn] - specifies transaction control number.");
			Console.WriteLine("\t[Src] - specifies source agency number.");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Irises";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 7)
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
				string src = args[6]; // source agency number

				if ((tot.Length < 3) || (tot.Length > 4))
				{
					Console.WriteLine("Tot parameter should be 3 or 4 characters length.");
					return -1;
				}

				// Create empty ANTemplate object with only type 1 record in it
				using (var template = new ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, 0))
				using(NImage image = NImage.FromFile(args[0]))
				// Convert to rgb image
				using (NImage rgbImage = NImage.FromImage(NPixelFormat.Rgb8U, 0, image))
				{
					rgbImage.ResolutionIsAspectRatio = true;

					// Create Type 17 record
					var record = new ANType17Record(ANTemplate.VersionCurrent, 0, src, BdifScaleUnits.None, ANImageCompressionAlgorithm.None, rgbImage);

					// Add Type 17 record to ANTemplate object
					template.Records.Add(record);

					// Store ANTemplate object with type 17 record in file
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
