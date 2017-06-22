using System;
using System.Collections.Generic;
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
			Console.WriteLine("\t{0} [server:port] [command]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\tserver:port - matching server address (port is optional)");
			Console.WriteLine();
			Console.WriteLine("\tcommands:");
			Console.WriteLine("\t\tstart                - Start cluster");
			Console.WriteLine("\t\tstop <id>            - Stop (wait until finished task in progress) server (id is 0) or node (id is above or equal 4)");
			Console.WriteLine("\t\tkill <id>            - Instantly stop server (id is 0) or node (id is above or equal 4)");
			Console.WriteLine("\t\tinfo <info type>     - Print info about cluster or nodes");
			Console.WriteLine("\t\t\tinfo type: tasks_short | tasks_complete | nodes | results");
			Console.WriteLine("\t\tdbupdate             - DB update");
			Console.WriteLine("\t\tdbchanged <id>      - Notify server of changed templates in DB");
			Console.WriteLine("\t\tdbflush              - Flush the database");
			Console.WriteLine("\t\tstatus               - Gets current server status");
			Console.WriteLine();

			return 1;
		}

		private static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 2)
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

				var cmd = new List<string>();
				for (int i = 1; i < args.Length; i++)
				{
					cmd.Add(args[i]);
				}

				var serverAdmin = new Program(server, port);
				serverAdmin.ExecuteCommand(cmd.ToArray());

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

		private void ExecuteCommand(string[] args)
		{
			AdminPacket request = null;
			AdminPacketReceived results = null;

			try
			{
				int nodeId;
				switch (args[0])
				{
					case "start":
						request = AdminPacket.CreatePacket_ServerStart();
						Console.WriteLine(SendPacket(request) ? "start command sent successfully" : "failed to send start command");
						break;

					case "stop":
						if (args.Length >= 2
							&& ((nodeId = GetNodeId(args[1])) != -1))
						{
							request = AdminPacket.CreatePacket_NodeStop(nodeId);
							Console.WriteLine(SendPacket(request)
							                  	? string.Format("stop {0} command sent successfully", args[1])
							                  	: "stop command sent failed");
						}
						else
						{
							Console.WriteLine("missing parameter: id");
						}
						break;

					case "kill":
						if (args.Length >= 2
							&& ((nodeId = GetNodeId(args[1])) != -1))
						{
							request = nodeId == 0 ? AdminPacket.CreatePacket_ServerKill() : AdminPacket.CreatePacket_NodeKill(nodeId);

							Console.WriteLine(SendPacket(request)
							                  	? string.Format("kill {0} command sent successfully", args[1])
							                  	: "kill command sent failed");
						}
						else
						{
							Console.WriteLine("missing parameter: id");
						}
						break;

					case "info":
						if (args.Length >= 2)
						{
							switch (args[1])
							{
								case "tasks_short":
									request = AdminPacket.CreatePacket_TasksShortInfoRequest();
									results = SendReceivePacket(request);
									if (results != null)
									{
										PrintShortRunningTasksInfo(results);
									}
									break;

								case "tasks_complete":
									request = AdminPacket.CreatePacket_TasksInfoRequest();
									results = SendReceivePacket(request);
									if (results != null)
									{
										PrintCompleteRunningTasksInfo(results);
									}
									break;

								case "nodes":
									request = AdminPacket.CreatePacket_NodesInfoRequest();
									results = SendReceivePacket(request);
									if (results != null)
									{
										PrintNodesInfo(results);
									}
									break;

								case "results":
									request = AdminPacket.CreatePacket_ResultsInfoRequest();
									results = SendReceivePacket(request);
									if (results != null)
									{
										PrintTaskResultsInfo(results);
									}
									break;

								default:
									Console.WriteLine("unknown info type: {0}", args[1]);
									break;
							}
						}
						else
						{
							Console.WriteLine("missing parameter: info type");
						}
						break;

					case "dbupdate":
						request = AdminPacket.CreatePacket_DatabaseUpdate();
						Console.WriteLine(SendPacket(request) ? "dbupdate command sent successfully" : "failed to send dbupdate command");
						break;

					case "dbchanged":
						if (args.Length >= 2)
						{
							var updateIDs = new string[args.Length - 1];
							Array.Copy(args, 1, updateIDs, 0, args.Length - 1);
							request = AdminPacket.CreatePacket_UpdateDatabaseIDs(updateIDs);
							Console.WriteLine(SendPacket(request)
							                  	? "dbchanged command sent successfully"
							                  	: "failed to send dbchanged command");
						}
						else
						{
							Console.WriteLine("missing parameters: Ids of records to update.");
						}
						break;
					case "dbflush":
						request = AdminPacket.CreatePacket_DatabaseFlush();
						Console.WriteLine(SendPacket(request) ? "dbflush command sent successfully" : "failed to send dbflush command");
						break;
					case "status":
						request = AdminPacket.CreatePacket_ServerInfoRequest();
						results = SendReceivePacket(request);
						if (results != null)
						{
							PrintServerInfo(results);
						}
						break;
					default:
						Console.WriteLine("command not recognized.");
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("an error has occured:");
				Console.WriteLine(ex);
			}
			finally
			{
				if (request != null)
				{
					request.DestroyPacket();
				}
				if (results != null)
				{
					results.DestroyPacket();
				}
			}
		}

		private static void PrintShortRunningTasksInfo(AdminPacketReceived results)
		{
			TaskShortInfo[] taskShortInfo;
			results.GetTasksShortInfo(out taskShortInfo);
			Console.WriteLine("{0} node(s) running:", taskShortInfo.Length);
			foreach (TaskShortInfo info in taskShortInfo)
			{
				Console.WriteLine("\tid: {0}", info.taskId);
				Console.WriteLine("\tprogress: {0}", info.taskProgress);
				Console.WriteLine("\tnodes completed: {0}", info.nodesCompleted);
				Console.WriteLine("\tworking nodes: {0}", info.workingNodes);
			}
			Console.WriteLine();
		}

		private static void PrintCompleteRunningTasksInfo(AdminPacketReceived results)
		{
			TaskInfo[] taskInfos;
			results.GetTasksInfo(out taskInfos);

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
		}

		private static void PrintNodesInfo(AdminPacketReceived results)
		{
			NodeInfo[] nodeInfos;
			results.GetNodesInfo(out nodeInfos);
			Console.WriteLine("{0} node(s) running:", nodeInfos.Length);
			foreach (NodeInfo nodeInfo in nodeInfos)
			{
				Console.WriteLine("{0} ({1})", nodeInfo.id, nodeInfo.state);
			}
			Console.WriteLine();
		}

		private static void PrintServerInfo(AdminPacketReceived results)
		{
			ClusterServerStatus status;
			ClusterStatusCode code = results.GetServerInfo(out status);
			if (code != ClusterStatusCode.OK)
			{
				Console.WriteLine("Error while getting server info");
			}
			else
			{
				Console.WriteLine("Server status is: {0}", status);
			}

			Console.WriteLine();
		}

		private static void PrintTaskResultsInfo(AdminPacketReceived results)
		{
			int[] resInfo;
			results.GetResults(out resInfo);

			Console.WriteLine("{0} completed task(s):", resInfo.Length);
			foreach (int result in resInfo)
			{
				Console.WriteLine(result);
			}
			Console.WriteLine();
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

		private bool SendPacket(AdminPacket packet)
		{
			Communication comm = null;

			try
			{
				comm = new Communication(_server, _port);
				ClusterStatusCode communicationResult = comm.SendPacket(packet);
				if (communicationResult == ClusterStatusCode.OK)
				{
					return true;
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
			return false;
		}

		private static int GetNodeId(string idString)
		{
			if (idString == "server")
			{
				return 0;
			}

			try
			{
				return int.Parse(idString);
			}
			catch
			{
				return -1;
			}
		}

		#endregion Server status info
	}
}
