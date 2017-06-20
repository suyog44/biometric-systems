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
			Console.WriteLine("usage: {0} [FCRecord] [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[FCRecord]  - input FCRecord");
			Console.WriteLine("\t[NTemplate] - output NTemplate");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FaceExtraction,Biometrics.Standards.Faces";

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
					// Read FCRecord from file
					byte[] fcRecordData = File.ReadAllBytes(args[0]);

					// Create FCRecord
					var fcRec = new FCRecord(fcRecordData, BdifStandard.Iso);

					// Read all images from FCRecord
					foreach (FcrFaceImage fv in fcRec.FaceImages)
					{
						var face = new NFace { Image = fv.ToNImage() };
						subject.Faces.Add(face);
					}

					// Set face template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FacesTemplateSize = NTemplateSize.Large;

					// Create template from added face image(s)
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
