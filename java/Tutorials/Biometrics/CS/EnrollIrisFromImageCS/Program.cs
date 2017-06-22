using System;
using System.IO;

using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [image] [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[image]    - filename of image.");
			Console.WriteLine("\t[template] - filename for template.");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.IrisExtraction";

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
				using (var iris = new NIris())
				{
					// Read iris image from file and add it to NIris object
					iris.FileName = args[0];

					// Read iris image from file and add it NSubject
					subject.Irises.Add(iris);

					// Set iris template size (recommended, for enroll to database, is large) (optional)
					biometricClient.IrisesTemplateSize = NTemplateSize.Large;

					// Create template from added iris image
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
						Console.WriteLine("Extraction failed! Status = {0}", status);
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
