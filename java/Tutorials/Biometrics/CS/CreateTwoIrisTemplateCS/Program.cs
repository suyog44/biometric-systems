using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Licensing;
using Neurotec.IO;
using System.Collections;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [left eye] [right eye] [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[left eye]  - filename of the left eye file with template.");
			Console.WriteLine("\t[right eye] - filename of the right eye file with template.");
			Console.WriteLine("\t[template]  - filename for template.");

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.IrisExtraction";

			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 3)
			{
				return Usage();
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				var nTemplate = new NTemplate();

				// create NTemplate
				var outputTemplate = new NTemplate();
				// create NETemplate
				var outputIrisesTemplate = new NETemplate();
				// set NETemplate to NTemplate
				outputTemplate.Irises = outputIrisesTemplate;
				nTemplate.Irises = new NETemplate();

				for (int i = 0; i < (args.Length - 1); i++)
				{
					// read NTemplate/NETemplate/NERecord from input file
					var hBuffer = new NBuffer(File.ReadAllBytes(args[i]));
					var newTemplate = new NTemplate(hBuffer);
					var irisTemplate = newTemplate.Irises;

					// retrieve NETemplate from NTemplate
					var inputIrisesTemplate = nTemplate.Irises;

					// retrieve NERecords count
					int inputRecordCount = irisTemplate.Records.Count;
					Console.WriteLine("found {0} records in file {1}\n", inputRecordCount, args[i]);

					foreach (var record in irisTemplate.Records)
					{
						// add NERecord to output NETemplate
						outputIrisesTemplate.Records.Add(record);
					}
				}

				// save ouput template
				File.WriteAllBytes(args[2], outputIrisesTemplate.Save().ToArray());
				Console.WriteLine("Template successfully saved to file: {0}", args[2]);

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
