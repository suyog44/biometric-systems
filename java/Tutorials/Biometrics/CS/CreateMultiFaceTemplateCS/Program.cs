using System;
using System.Collections.Generic;
using System.IO;

using Neurotec.Biometrics;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [one or more templates] [output NLTemplate]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t[one or more NLTemplates]  - one or more files containing face templates.");
			Console.WriteLine("\t[output NTemplate]         - output NTemplate file.");
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
				// Read all input NLTemplates
				var inputTemplates = new List<NTemplate>();
				for (int i = 0; i < args.Length - 1; i++)
				{
					byte[] templateData = File.ReadAllBytes(args[i]);
					Console.WriteLine("reading {0}", args[i]);

					inputTemplates.Add(new NTemplate(templateData));
				}

				// Create and fill output faces template
				var outputTemplate = new NLTemplate();
				foreach (NTemplate inputTemplate in inputTemplates)
				{
					if (inputTemplate.Faces == null) continue;

					foreach (NLRecord inputRecord in inputTemplate.Faces.Records)
					{
						outputTemplate.Records.Add((NLRecord)inputRecord.Clone());
					}
				}

				if (outputTemplate.Records.Count == 0)
				{
					Console.WriteLine("not writing template file because no records found.");
					return -1;
				}

				Console.WriteLine("{0} face record(s) found.", outputTemplate.Records.Count);

				// Write output file
				byte[] packetTemplate = outputTemplate.Save().ToArray();
				File.WriteAllBytes(args[args.Length - 1], packetTemplate);
				Console.WriteLine("writing {0}", args[args.Length - 1]);
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}
	}
}
