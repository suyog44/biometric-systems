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
			Console.WriteLine("\t{0} [ANTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[ATemplate] - filename of ANTemplate.");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.Standards.Base,Biometrics.Standards.PalmTemplates,Biometrics.Standards.Irises,Biometrics.Standards.Faces";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 1)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new NotActivatedException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var template = new ANTemplate(args[0], ANValidationLevel.Standard))
				{
					for (int i = 0; i < template.Records.Count; i++)
					{
						ANRecord record = template.Records[i];
						NImage image = null;
						int number = record.RecordType.Number;
						if (number >= 3 && number <= 8 && number != 7)
						{
							image = ((ANImageBinaryRecord)record).ToNImage();
						}
						else if (number >= 10 && number <= 17)
						{
							image = ((ANImageAsciiBinaryRecord)record).ToNImage();
						}

						if (image != null)
						{
							string fileName = string.Format("record{0}_type{1}.jpg", i + 1, number);
							image.Save(fileName);
							image.Dispose();
							Console.WriteLine("Image saved to {0}", fileName);
						}
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
