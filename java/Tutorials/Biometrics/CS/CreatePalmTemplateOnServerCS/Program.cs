using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;

namespace Neurotec.Tutorials
{
	static class Program
	{
		private const string DefaultAddress = "127.0.0.1";
		private const int DefaultAdminPort = 24932;

		private static int Usage()
		{
			Console.WriteLine(@"usage:");
			Console.WriteLine(@"	{0} -s [server:port] -i [input image] -t [output template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t-s [server:port]   - matching server address (optional parameter, if address specified - port is optional)");
			Console.WriteLine("\t-i [image]   - image filename to store palm image.");
			Console.WriteLine("\t-t [output template]   - filename to store palm template.");
			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 3)
			{
				return Usage();
			}

			string server;
			int adminPort;
			string templateFile;
			string imageFile;
			try
			{
				ParseArgs(args, out server, out adminPort, out imageFile, out templateFile);
			}
			catch (Exception ex)
			{
				Console.WriteLine("error: {0}", ex);
				return Usage();
			}

			try
			{
				using (var biometricClient = new NBiometricClient())
				using (var subject = new NSubject())
				using (var palm = new NPalm())
				{
					// perform all biometric operations on remote server only
					biometricClient.LocalOperations = NBiometricOperations.None;
					var connection = new NClusterBiometricConnection { Host = server, AdminPort = adminPort };
					biometricClient.RemoteConnections.Add(connection);

					palm.SampleBuffer = new NBuffer(File.ReadAllBytes(imageFile));
					subject.Palms.Add(palm);
					biometricClient.PalmsTemplateSize = NTemplateSize.Large;

					var status = biometricClient.CreateTemplate(subject);

					if (status == NBiometricStatus.Ok)
					{
						Console.WriteLine("Template extracted");
					}
					else
					{
						Console.WriteLine("Extraction failed: {0}", status);
						return -1;
					}

					File.WriteAllBytes(templateFile, subject.GetTemplateBuffer().ToArray());
					Console.WriteLine("Template saved successfully");
				}
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		private static void ParseArgs(string[] args, out string serverIp, out int adminPort, out string imageFile, out string templateFile)
		{
			serverIp = DefaultAddress;
			adminPort = DefaultAdminPort;

			imageFile = string.Empty;
			templateFile = string.Empty;

			for (int i = 0; i < args.Length; i++)
			{
				string optarg = string.Empty;

				if (args[i].Length != 2 || args[i][0] != '-')
				{
					throw new Exception("parameter parse error");
				}

				if (args.Length > i + 1 && args[i + 1][0] != '-')
				{
					optarg = args[i + 1]; // we have a parameter for given flag
				}

				if (optarg == string.Empty)
				{
					throw new Exception("parameter parse error");
				}

				switch (args[i][1])
				{
					case 's':
						i++;
						if (optarg.Contains(":"))
						{
							string[] splitAddress = optarg.Split(':');
							serverIp = splitAddress[0];
							adminPort = int.Parse(splitAddress[1]);
						}
						else
						{
							serverIp = optarg;
							adminPort = DefaultAdminPort;
						}
						break;
					case 'i':
						i++;
						imageFile = optarg;
						break;
					case 't':
						i++;
						templateFile = optarg;
						break;
					default:
						throw new Exception("wrong parameter found!");
				}
			}

			if (templateFile == string.Empty)
				throw new Exception("template - required parameter - not specified");

			if (imageFile == string.Empty)
				throw new Exception("image - required parameter - not specified");
		}
	}
}
