using System;
using System.IO;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.IO;

namespace Neurotec.Tutorials
{
	class Program
	{
		private const string DefaultAddress = "127.0.0.1";
		private const int DefaultPort = 25452;

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} -s [server:port] -t [template]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)");
			Console.WriteLine("\t-t template      - template to be sent for identification (required)");

			return 1;
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			string templateFile;
			string server;
			int port;

			try
			{
				ParseArgs(args, out server, out port, out templateFile);
			}
			catch (Exception ex)
			{
				Console.WriteLine("error: {0}", ex);
				Usage();
				return -1;
			}

			try
			{
				using (var biometricClient = new NBiometricClient())
				// Read template
				using (NSubject subject = CreateSubject(templateFile, templateFile))
				{
					// Create connection to server
					var connection = new NClusterBiometricConnection {Host = server, Port = port};

					biometricClient.RemoteConnections.Add(connection);

					// Create identification task
					NBiometricTask identifyTask = biometricClient.CreateTask(NBiometricOperations.Identify, subject);
					biometricClient.PerformTask(identifyTask);
					NBiometricStatus status = identifyTask.Status;
					if (status != NBiometricStatus.Ok)
					{
						Console.WriteLine("Identification was unsuccessful. Status: {0}.", status);
						if (identifyTask.Error != null) throw identifyTask.Error;
						return -1;
					}
					foreach (var matchingResult in subject.MatchingResults)
					{
						Console.WriteLine("Matched with ID: '{0}' with score {1}", matchingResult.Id, matchingResult.Score);
					}
				}

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		private static NSubject CreateSubject(string fileName, string subjectId)
		{
			var subject = new NSubject();
			subject.SetTemplateBuffer(new NBuffer(File.ReadAllBytes(fileName)));
			subject.Id = subjectId;

			return subject;
		}

		private static void ParseArgs(string[] args, out string serverIp, out int serverAdminPort, out string templateFile)
		{
			serverIp = DefaultAddress;
			serverAdminPort = DefaultPort;

			templateFile = string.Empty;

			for (int i = 0; i < args.Length; i++)
			{
				string optarg = string.Empty;

				if (args[i].Length != 2 || args[i][0] != '-')
				{
					throw new ApplicationException("parameter parse error");
				}

				if (args.Length > i + 1 && args[i + 1][0] != '-')
				{
					optarg = args[i + 1]; // we have a parameter for given flag
				}

				if (optarg == string.Empty)
				{
					throw new ApplicationException("parameter parse error");
				}

				switch (args[i][1])
				{
					case 's':
						i++;
						if (optarg.Contains(":"))
						{
							string[] splitAddress = optarg.Split(':');
							serverIp = splitAddress[0];
							serverAdminPort = int.Parse(splitAddress[1]);
						}
						else
						{
							serverIp = optarg;
							serverAdminPort = DefaultPort;
						}
						break;
					case 't':
						i++;
						templateFile = optarg;
						break;
					default:
						throw new ApplicationException("wrong parameter found!");
				}
			}

			if (templateFile == string.Empty)
				throw new ApplicationException("template - required parameter - not specified");
		}
	}
}
