using System;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Standards;
using Neurotec.Licensing;
using System.Linq;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [NImage] [ANTemplate] [Tot] [Dai] [Ori] [Tcn]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[NImage]     - filename with image file.");
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
			const string Components = "Biometrics.FingerExtraction,Biometrics.Standards.FingerTemplates";

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

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var finger = new NFinger())
				// Create empty ANTemplate object with only type 1 record in it
				using (var template = new ANTemplate(ANTemplate.VersionCurrent, tot, dai, ori, tcn, 0))
				{
					//Read finger image from file and add it to NFinger object
					finger.FileName = args[0];

					//Read finger image from file and add it NSubject
					subject.Fingers.Add(finger);

					//Create template from added finger image
					var status = biometricClient.CreateTemplate(subject);
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Template extracted");

						// Create Type 9 record
						var record = new ANType9Record(ANTemplate.VersionCurrent, 0, true, subject.GetTemplate().Fingers.Records.First());
						// Add Type 9 record to ANTemplate object
						template.Records.Add(record);

						// Store ANTemplate object with type 9 record in file
						template.Save(args[1]);
					}
					else
					{
						Console.WriteLine("Extraction failed: {0}", status);
					}
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
