using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;
using Neurotec.Licensing;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [output template] [3 to 10 palm images]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\texample {0} template image1.png image2.png image3.png", TutorialUtils.GetAssemblyName());
			Console.WriteLine();

			return 1;
		}

		static int Main(string[] args)
		{
			const string Components = "Biometrics.PalmExtraction";

			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 4 || args.Length > 11)
			{
				return Usage();
			}

			try
			{
				// Obtain license
				if (!NLicense.ObtainComponents("/local", 5000, Components))
				{
					throw new ApplicationException(string.Format("Could not obtain licenses for components: {0}", Components));
				}

				using (NBiometricClient biometricClient = new NBiometricClient())
				using (NSubject subject = new NSubject())
				{
					for (int i = 1; i < args.Length; i++)
					{
						NPalm palm = new NPalm { SessionId = 1, FileName = args[i] };
						subject.Palms.Add(palm);
					}

					NBiometricStatus status = biometricClient.CreateTemplate(subject);
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Failed to create or generalize templates. Status: {0}.", status);
						return -1;
					}

					Console.WriteLine("Generalization completed successfully");
					Console.Write("Saving template to '{0}' ... ", args[0]);
					using (NBuffer buffer = subject.GetTemplateBuffer())
					{
						File.WriteAllBytes(args[0], buffer.ToArray());
					}
					Console.WriteLine("done");
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
