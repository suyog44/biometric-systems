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
			Console.WriteLine(@"	{0} -s [server:port] -v [voice record] -t [output template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine();
			Console.WriteLine("\t-s [server:port]   - matching server address (optional parameter, if address specified - port is optional)");
			Console.WriteLine("\t-v [voice record]   -filename of voice record.");
			Console.WriteLine("\t-t [output template]   - filename to store voice template.");
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
			string voiceFile;
			try
			{
				ParseArgs(args, out server, out adminPort, out voiceFile, out templateFile);
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
				using (var voice = new NVoice())
				{
					// perform all biometric operations on remote server only
					biometricClient.LocalOperations = NBiometricOperations.None;
					var connection = new NClusterBiometricConnection { Host = server, AdminPort = adminPort };
					biometricClient.RemoteConnections.Add(connection);

					voice.SampleBuffer = new NBuffer(File.ReadAllBytes(voiceFile));
					subject.Voices.Add(voice);

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

		private static void ParseArgs(string[] args, out string serverIp, out int adminPort, out string voiceFile, out string templateFile)
		{
			serverIp = DefaultAddress;
			adminPort = DefaultAdminPort;

			voiceFile = string.Empty;
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
					case 'v':
						i++;
						voiceFile = optarg;
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

			if (voiceFile == string.Empty)
				throw new Exception("Voice Record - required parameter - not specified");
		}
	}
}
