using System;
using System.IO;

using Neurotec.Licensing;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Tutorials
{
	static class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [input image] [output template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.PalmExtraction";

			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var palm = new NPalm())
				{
					//Read palm image from file and add it to NPalm object
					palm.FileName = args[0];

					//Read palm image from file and add it NSubject
					subject.Palms.Add(palm);

					//Set palm template size (recommended, for enroll to database, is large) (optional)
					biometricClient.PalmsTemplateSize = NTemplateSize.Large;

					//Create template from added finger image
					var status = biometricClient.CreateTemplate(subject);
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Template extracted");

						// save compressed template to file
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("template saved successfully");
					}
					else
					{
						Console.WriteLine("Extraction failed: {0}", status);
						return -1;
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
