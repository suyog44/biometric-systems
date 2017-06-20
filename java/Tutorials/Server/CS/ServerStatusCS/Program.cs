using System;
using Neurotec.Cluster;

namespace Neurotec.Tutorials
{
	public class Program
	{
		#region Console interface

		private const int DefaultPort = 24932;

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [server:port]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\tserver:port - matching server address (port is optional)");
			Console.WriteLine();

			return 1;
		}

		private static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 1)
			{
				return Usage();
			}

			try
			{
				string server;
				int port;

				try
				{
					if (args[0].Contains(":"))
					{
						string[] splitAddress = args[0].Split(':');
						server = splitAddress[0];
						port = int.Parse(splitAddress[1]);
					}
					else
					{
						server = args[0];
						port = DefaultPort;
						Console.WriteLine("port not specified; using default: {0}", DefaultPort);
						Console.WriteLine();
					}
				}
				catch
				{
					Console.WriteLine("server address in wrong format.");
					return -1;
				}

				var serverStatus = new Program(server, port);
				serverStatus.PrintServerStatus();
				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		#endregion Console interface

		#region Server status info

		private readonly string _server;
		private readonly int _port;

		public Program(string server, int port)
		{
			_server = server;
			_port = port;
		}

		public void PrintServerStatus()
		{
			try
			{
				Console.WriteLine("asking info from {0}: {1} ...", _server, _port);
				Console.WriteLine();

				Console.WriteLine("requesting info about server ...");
							
				AdminPacket adminPacket = AdminPacket.CreatePacket_ServerInfoRequest();
				AdminPacketReceived adminPacketReceived = SendReceivePacket(adminPacket);
				ClusterServerStatus serverStatus;
				if (adminPacketReceived.GetServerInfo(out serverStatus) == ClusterStatusCode.OK)
				{
					Console.WriteLine("server status: {0}", serverStatus);
				}
				else
				{
					Console.WriteLine("unable to determine server status");
				}
				Console.WriteLine();

				Console.WriteLine("requesting info about nodes ...");

				adminPacket = AdminPacket.CreatePacket_NodesInfoRequest();
				adminPacketReceived = SendReceivePacket(adminPacket);

				if (adminPacketReceived != null)
				{
					NodeInfo[] nodeInfos;
					adminPacketReceived.GetNodesInfo(out nodeInfos);
					Console.WriteLine("{0} node(s) running:", nodeInfos.Length);
					foreach (NodeInfo nodeInfo in nodeInfos)
					{
						Console.WriteLine("{0} ({1})", nodeInfo.id, nodeInfo.state);
					}
					Console.WriteLine();

					adminPacketReceived.DestroyPacket();
				}
				else
				{
					Console.WriteLine("failed to receive tasks info");
				}

				adminPacket.DestroyPacket();

				Console.WriteLine("requesting info about tasks ...");

				adminPacket = AdminPacket.CreatePacket_TasksInfoRequest();
				adminPacketReceived = SendReceivePacket(adminPacket);

				if (adminPacketReceived != null)
				{
					TaskInfo[] taskInfos;
					adminPacketReceived.GetTasksInfo(out taskInfos);

					Console.WriteLine("{0} task(s):", taskInfos.Length);
					foreach (TaskInfo taskInfo in taskInfos)
					{
						Console.WriteLine("\tid: {0}", taskInfo.taskId);
						Console.WriteLine("\tprogress: {0}", taskInfo.taskProgress);
						Console.WriteLine("\tnodes completed: {0}", taskInfo.nodesCompleted);
						Console.WriteLine("\tworking nodes: {0}", taskInfo.workingNodes);
						for (int i = 0; i < taskInfo.workingNodes; i++)
						{
							Console.WriteLine("\t\tnode ID: {0}", taskInfo.workingNodeInfo[i].nodeId);
							Console.WriteLine("\t\tnode progress: {0}", taskInfo.workingNodeInfo[i].progress);
						}
					}
					Console.WriteLine();

					adminPacketReceived.DestroyPacket();
				}
				else
				{
					Console.WriteLine("failed to receive tasks info");
				}

				adminPacket.DestroyPacket();

				Console.WriteLine("requesting info about results ...");

				adminPacket = AdminPacket.CreatePacket_ResultsInfoRequest();
				adminPacketReceived = SendReceivePacket(adminPacket);

				if (adminPacketReceived != null)
				{
					int[] results;
					adminPacketReceived.GetResults(out results);

					Console.WriteLine("{0} completed task(s):", results.Length);
					foreach (int result in results)
					{
						Console.WriteLine(result);
					}
					Console.WriteLine();

					adminPacketReceived.DestroyPacket();
				}
				else
				{
					Console.WriteLine("failed to receive results info");
				}

				adminPacket.DestroyPacket();
			}
			catch (Exception ex)
			{
				Console.WriteLine("an error has occured");
				Console.WriteLine(ex);
			}
		}

		private AdminPacketReceived SendReceivePacket(AdminPacket packet)
		{
			Communication comm = null;

			try
			{
				comm = new Communication(_server, _port);
				AdminPacketReceived receivedPacket;
				ClusterStatusCode communicationResult = comm.SendReceivePacket(packet, out receivedPacket);
				if (communicationResult == ClusterStatusCode.OK)
				{
					return receivedPacket;
				}
				else
				{
					Console.WriteLine("command failed. Result: {0}", communicationResult);
				}
			}
			catch //(Exception ex)
			{
				Console.WriteLine("failed to get node info");
			}
			finally
			{
				if (comm != null)
				{
					comm.Close();
				}
			}
			return null;
		}

		#endregion Server status info
	}
}
