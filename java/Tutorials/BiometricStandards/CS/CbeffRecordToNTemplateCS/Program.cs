using System;
using System.Collections.Generic;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Standards;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static readonly string[] Components =
		{
			"Biometrics.Standards.Base",
			"Biometrics.Standards.Irises",
			"Biometrics.Standards.Fingers",
			"Biometrics.Standards.Faces",
			"Biometrics.Standards.Palms",
			"Biometrics.IrisExtraction",
			"Biometrics.FingerExtraction",
			"Biometrics.FaceExtraction",
			"Biometrics.PalmExtraction"
		};

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [CbeffRecord] [PatronFormat] [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t[CbeffRecord] - filename of CbeffRecord.");
			Console.WriteLine("\t[PatronFormat] - hex number identifying patron format (all supported values can be found in CbeffRecord class documentation).");
			Console.WriteLine("\t[NTemplate] - filename of NTemplate.");
			Console.WriteLine("");

			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length != 3)
			{
				return Usage();
			}

			var obtainedLicenses = new List<string>();
			try
			{
				// Obtain licenses.
				foreach (string component in Components)
				{
					if (NLicense.ObtainComponents("/local", 5000, component))
					{
						Console.WriteLine("Obtained license for component: {0}", component);
						obtainedLicenses.Add(component);
					}
				}
				if (obtainedLicenses.Count == 0)
				{
					throw new NotActivatedException("Could not obtain any matching license");
				}

				// Read CbeffRecord buffer
				var packedCbeffRecord = new NBuffer(File.ReadAllBytes(args[0]));

				// Get CbeffRecord patron format
				// all supported patron formats can be found in CbeffRecord class documentation
				uint patronFormat = uint.Parse(args[1], System.Globalization.NumberStyles.HexNumber);

				// Creating CbeffRecord object from NBuffer object
				using (var cbeffRecord = new CbeffRecord(packedCbeffRecord, patronFormat))
				using (var subject = new NSubject())
				using (var engine = new NBiometricEngine())
				{
					// Setting CbeffRecord
					subject.SetTemplate(cbeffRecord);

					// Extracting template details from specified CbeffRecord data
					engine.CreateTemplate(subject);

					if (subject.Status == NBiometricStatus.Ok)
					{
						File.WriteAllBytes(args[2], subject.GetTemplateBuffer().ToArray());
						Console.WriteLine("Template successfully saved");
					}
					else
					{
						Console.WriteLine("Template creation failed! Status: {0}", subject.Status);
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
				foreach (string component in obtainedLicenses)
				{
					NLicense.ReleaseComponents(component);
				}
			}
		}
	}
}
