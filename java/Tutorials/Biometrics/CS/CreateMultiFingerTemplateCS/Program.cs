using System;
using System.IO;

using Neurotec.Biometrics;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [templates ...] [NTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[templates] - one or more files containing fingerprint templates.");
			Console.WriteLine("\t[NTemplate] - filename of output file where NTemplate is saved.");
			Console.WriteLine();
			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			try
			{
				var nfTemplate = new NFTemplate();
				for (int i = 0; i < args.Length - 1; i++)
				{
					var template = new NTemplate(File.ReadAllBytes(args[i]));
					if (template.Fingers != null)
					{
						foreach (NFRecord record in template.Fingers.Records)
						{
							nfTemplate.Records.Add((NFRecord)record.Clone());
						}
					}
				}

				File.WriteAllBytes(args[args.Length - 1], nfTemplate.Save().ToArray());
				Console.WriteLine("Template successfully writen to {0}", args[args.Length - 1]);

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}
	}
}
