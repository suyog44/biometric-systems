using System;
using System.IO;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Standards;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage: {0} [FIRecord] [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[FIRecord]  - input FIRecord");
			Console.WriteLine("\t[NTemplate] - output NTemplate");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerExtraction,Biometrics.Standards.Fingers";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				{
					// Read FIRecord from file
					byte[] fiRecordData = File.ReadAllBytes(args[0]);

					// Create FIRecord
					var fiRec = new FIRecord(fiRecordData, BdifStandard.Iso);

					// Read all images from FIRecord
					foreach (FirFingerView fv in fiRec.FingerViews)
					{
						var finger = new NFinger { Image = fv.ToNImage() };
						subject.Fingers.Add(finger);
					}

					// Set finger template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FingersTemplateSize = NTemplateSize.Large;

					// Create template from added finger image(s)
					var status = biometricClient.CreateTemplate(subject);
					Console.WriteLine(status == NBiometricStatus.Ok
						? "Template extracted"
						: String.Format("Extraction failed: {0}", status));

					// Save template to file
					if (status == NBiometricStatus.Ok)
					{
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("template saved successfully");
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
