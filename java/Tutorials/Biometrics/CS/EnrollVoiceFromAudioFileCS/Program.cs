using System;
using System.IO;

using Neurotec.Licensing;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [source] [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[source] - filename of audio file to extract.");
			Console.WriteLine("\t[template] - filename to store sound template.");
			Console.WriteLine();
			Console.WriteLine("\texample: {0} audio.wav template.dat", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Media,Biometrics.VoiceExtraction";

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
				using (var voice = new NVoice())
				{
					//Read voice image from file and add it to NFinger object
					voice.FileName = args[0];

					//Read voice from file and add it NSubject
					subject.Voices.Add(voice);

					//Create template from added voice file
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
			catch(Exception ex)
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
