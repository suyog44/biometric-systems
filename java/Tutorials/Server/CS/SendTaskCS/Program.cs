using System;
using System.Threading;
using System.IO;

using Neurotec.Cluster;

namespace Neurotec.Tutorials
{
	public class Program
	{
		private const string DefaultAddress = "127.0.0.1";
		private const int DefaultPort = 25452;
		private const string DefaultQuery = "SELECT node_id, dbid FROM node_tbl";

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} -s [server:port] -t [template] -q [query] -y [template type]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} -s [server:port] -t [template] -q [query]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)");
			Console.WriteLine("\t-t template      - template to be sent for matching (required)");
			Console.WriteLine("\t-q query         - database query to execute (optional)");
			Console.WriteLine("\t-y template type - template type (optional - specify parameter only if template is not NTemplate, but FMRecord ansi or iso). Parameter values: ansi or iso.");
			Console.WriteLine("Important! Template type parameter is available only for Accelerator product!");
			Console.WriteLine("default query (if not specified): {0}", DefaultQuery);

			return 1;
		}

		private static void ParseArgs(string[] args, out string serverIp, out int serverPort, out string templateFile, out string query, out bool isStandardTemplate, out ClusterStandardTemplateType templateType)
		{
			serverIp = DefaultAddress;
			serverPort = DefaultPort;
			query = DefaultQuery;

			templateFile = string.Empty;

			isStandardTemplate = false;
			templateType = ClusterStandardTemplateType.Ansi;

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
							serverPort = int.Parse(splitAddress[1]);
						}
						else
						{
							serverIp = optarg;
							serverPort = DefaultPort;
						}
						break;
					case 't':
						i++;
						templateFile = optarg;
						break;
					case 'q':
						i++;
						query = optarg;
						break;
					case 'y':
						i++;
						if (optarg.Equals("ansi", StringComparison.InvariantCultureIgnoreCase))
						{
							templateType = ClusterStandardTemplateType.Ansi;
							isStandardTemplate = true;
						}
						else if (optarg.Equals("iso", StringComparison.InvariantCultureIgnoreCase))
						{
							templateType = ClusterStandardTemplateType.Iso;
							isStandardTemplate = true;
						}
						else
						{
							throw new Exception("wrong standard (should be iso or ansi)!");
						}
						break;
					default:
						throw new Exception("wrong parameter found!");
				}
			}

			if (templateFile == string.Empty)
				throw new Exception("template - required parameter - not specified");
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
			{
				return Usage();
			}

			string templateFile;
			byte[] template;
			string server;
			int port;
			string query;
			bool isStandardTemplate;
			ClusterStandardTemplateType templateType;
			try
			{
				ParseArgs(args, out server, out port, out templateFile, out query, out isStandardTemplate, out templateType);
			}
			catch (Exception ex)
			{
				Console.WriteLine("error: {0}", ex);
				return Usage();
			}
			
			try
			{
				template = File.ReadAllBytes(templateFile);
			}
			catch
			{
				Console.WriteLine("could not load template from file {0}.", args[0]);
				return -1;
			}

			Communication comm = null;

			try
			{
				comm = new Communication(server, port);

				int taskId;
				if (!isStandardTemplate)
				{
					SendRequest(comm, template, query, out taskId);
				}
				else
				{
					SendRequest(comm, template, templateType, query, out taskId);
				}

				WaitForResult(comm, taskId);

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				if (comm != null)
				{
					comm.Close();
				}
			}
		}

		private static void SendRequest(Communication com, byte[] template, string query, out int taskId)
		{
			Console.WriteLine("sending request ...");

			var parameters = new MatchingParameters();

			ClientPacket clientPacket = ClientPacket.CreateTask(ClusterTaskMode.Normal, template, query, parameters, 100);
			ClientPacketReceived received;
			ClusterStatusCode res = com.SendReceivePacket(clientPacket, out received);
			if (res != ClusterStatusCode.OK)
				throw new Exception(string.Format("failed to create task. task was not added. error: {0}", res));
			res = received.GetTaskID(out taskId);
			if (res != ClusterStatusCode.OK)
				throw new Exception(string.Format("failed to get task id. task was not added. error: {0}", res));
			if (taskId == -1)
				throw new Exception("failed to get task id. task was not added.");
			clientPacket.DestroyPacket();
			received.DestroyPacket();

			Console.WriteLine("... task with ID {0} started", taskId);
		}

		private static void SendRequest(Communication com, byte[] template, ClusterStandardTemplateType templateType, string query, out int taskId)
		{
			Console.WriteLine("sending request ...");

			var parameters = new MatchingParameters();

			ClientPacket clientPacket = ClientPacket.CreateTask(ClusterTaskMode.Normal, template, templateType, query, parameters, 100);
			ClientPacketReceived received;
			ClusterStatusCode res = com.SendReceivePacket(clientPacket, out received);
			if (res != ClusterStatusCode.OK)
				throw new Exception(string.Format("failed to create task. task was not added. error: {0}", res));
			res = received.GetTaskID(out taskId);
			if (res != ClusterStatusCode.OK)
				throw new Exception(string.Format("failed to get task id. task was not added. error: {0}", res));
			if (taskId == -1)
				throw new Exception("failed to get task id. task was not added.");
			clientPacket.DestroyPacket();
			received.DestroyPacket();

			Console.WriteLine("... task with ID {0} started", taskId);
		}

		private static void WaitForResult(Communication com, int taskId)
		{
			Console.WriteLine("waiting for results ...");
			int progress;
			int count;
			do
			{
				ClientPacket progressRequest = ClientPacket.CreateProgressRequest(taskId);
				ClientPacketReceived progressRequestReceived;
				com.SendReceivePacket(progressRequest, out progressRequestReceived);

				progressRequestReceived.GetTaskProgress(out progress, out count);
				progressRequest.DestroyPacket();
				progressRequestReceived.DestroyPacket();

				if (progress != 100)
					Thread.Sleep(100);

				if (progress < 0)
				{
					Console.Write("task aborted on server side.");
					return;
				}

			} while (progress != 100);

			if (count > 0)
			{
				ClientPacket resultRequest = ClientPacket.CreateResultRequest(taskId, 1, count);
				ClientPacketReceived resultsReceived;
				com.SendReceivePacket(resultRequest, out resultsReceived);
				ClusterResults[] results;
				resultsReceived.GetResults(out results);
				foreach (ClusterResults clusterRes in results)
				{
					Console.WriteLine("... matched with id: {0}, score: {1}", clusterRes.id, clusterRes.similarity);
				}
				resultsReceived.DestroyPacket();
				resultRequest.DestroyPacket();
			}
			else
			{
				Console.WriteLine("... no matches");
			}

			ClientPacket deleteRequest = ClientPacket.CreateResultDelete(taskId);
			com.SendPacket(deleteRequest);
			deleteRequest.DestroyPacket();
		}
	}
}
