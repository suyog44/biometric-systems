using System;
using Neurotec.Licensing;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Biometrics.Standards;
using System.IO;

namespace Neurotec.Tutorials
{
	static class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [input image] [output template] [format]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t[input image]    - image filename to extract.");
			Console.WriteLine("\t[output template] - filename to store extracted features.");
			Console.WriteLine("\t[format]   - whether proprietary or standard template should be created.");
			Console.WriteLine("\t\tIf not specified, proprietary Neurotechnology template is created (recommended).");
			Console.WriteLine("\t\tANSI for ANSI/INCITS 378-2004");
			Console.WriteLine("\t\tISO for ISO/IEC 19794-2");
			Console.WriteLine();
			Console.WriteLine("\texample: {0} image.jpg template.dat", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample: {0} image.jpg isoTemplate.dat ISO", TutorialUtils.GetAssemblyName());

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.FingerExtraction";
			BdifStandard standard = BdifStandard.Unspecified;

			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 2)
			{
				return Usage();
			}
			if (args.Length > 2)
			{
				if (args[2] == "ANSI")
				{
					standard = BdifStandard.Ansi;
				}
				else if (args[2] == "ISO")
				{
					standard = BdifStandard.Iso;
				}
			}

			try
			{
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var finger = new NFinger())
				{
					//Read finger image from file and add it to NFinger object
					finger.FileName = args[0];

					//Read finger image from file and add it NSubject
					subject.Fingers.Add(finger);

					//Set finger template size (recommended, for enroll to database, is large) (optional)
					biometricClient.FingersTemplateSize = NTemplateSize.Large;

					//Create template from added finger image
					var status = biometricClient.CreateTemplate(subject);
					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("{0} template extracted.", standard == BdifStandard.Iso ? "ISO" : standard == BdifStandard.Ansi ? "ANSI" : "Proprietary");
					}
					else
					{
						Console.WriteLine("Extraction failed: {0}", status);
						return -1;
					}

					// save compressed template to file
					if (standard == BdifStandard.Iso)
					{
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer(CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics,
							CbeffBdbFormatIdentifiers.IsoIecJtc1SC37BiometricsFingerMinutiaeRecordFormat,
							FMRecord.VersionIsoCurrent).ToArray());
					}
					else if (standard == BdifStandard.Ansi)
					{
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer(CbeffBiometricOrganizations.IncitsTCM1Biometrics,
							CbeffBdbFormatIdentifiers.IncitsTCM1BiometricsFingerMinutiaeU,
							FMRecord.VersionAnsiCurrent).ToArray());
					}
					else
					{
						File.WriteAllBytes(args[1], subject.GetTemplateBuffer().ToArray());
					}
					Console.WriteLine("Template saved successfully");
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
