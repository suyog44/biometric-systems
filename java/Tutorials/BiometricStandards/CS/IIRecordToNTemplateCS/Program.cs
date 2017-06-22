using System;
using System.IO;

using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Standards;
using Neurotec.Licensing;
using Neurotec.Biometrics;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage: {0} [IIRecord] [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[IIRecord]  - input IIRecord");
			Console.WriteLine("\t[NTemplate] - output NTemplate");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.IrisExtraction,Biometrics.Standards.Irises";

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
					// Read IIRecord from file
					byte[] iiRecordData = File.ReadAllBytes(args[0]);

					// Create IIRecord
					var iiRec = new IIRecord(iiRecordData, BdifStandard.Iso);

					// Read all images from IIRecord
					foreach (IirIrisImage irisImage in iiRec.IrisImages)
					{
						var iris = new NIris { Image = irisImage.ToNImage() };
						subject.Irises.Add(iris);
					}

					// Set iris template size (recommended, for enroll to database, is large) (optional)
					biometricClient.IrisesTemplateSize = NTemplateSize.Large;

					// Create template from added iris image(s)
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
