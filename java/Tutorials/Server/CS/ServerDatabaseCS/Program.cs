using System;
using System.Threading;
using System.IO;

using Neurotec.Cluster;

namespace Neurotec.Tutorials
{
	public class Program
	{
		private enum TaskType
		{
			Insert = 0,
			Delete = 1
		}

		private const string DefaultAddress = "127.0.0.1";
		private const int DefaultPort = 24932;

		private static int Usage()
		{
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} -s [server:port] -c [command] -i [template id] -t [template] -y [template type]", TutorialUtils.GetAssemblyName());
			Console.WriteLine("");
			Console.WriteLine("\t-s server:port   - matching server address (optional parameter, if address specified - port is optional)");
			Console.WriteLine("\t-c command       - command to be performed (either insert or delete) (required)");
			Console.WriteLine("\t-i template id   - id of template to be deleted or inserted (required)");
			Console.WriteLine("\t-t template      - template to be inserted (required only for insert)");
			Console.WriteLine("\t-y template type - type of template to be inserted (ansi or iso) (valid for insert to MegaMatcher Accelerator only) (optional)");
			Console.WriteLine("examples:");
			Console.WriteLine("\t{0} -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp ", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp -y ansi", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\t{0} -s 127.0.0.1:24932 -c delete -i testId", TutorialUtils.GetAssemblyName());

			return 1;
		}

		private static void ParseArgs(string[] args, out string serverIp, out int serverPort, out string id, out TaskType taskType, out string templateFile, out bool isStandardTemplate, out ClusterStandardTemplateType templateType)
		{
			serverIp = DefaultAddress;
			serverPort = DefaultPort;

			id = string.Empty;
			taskType = TaskType.Insert;
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
					case 'c':
						i++;
						taskType = (TaskType)Enum.Parse(typeof (TaskType), optarg, true);
						break;
					case 'i':
						i++;
						id = optarg;
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

			Console.WriteLine("selecting task type: {0}", taskType);

			if (id == string.Empty)
				throw new Exception("id - required parameter - not specified");
			if (taskType == TaskType.Insert)
			{
				if (templateFile == string.Empty)
					throw new Exception("template - required parameter - not specified");
			}
		}

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);
			if (args.Length < 2)
			{
				return Usage();
			}

			string server;
			int port;
			TaskType type;
			string templateFile;
			string id;
			bool isStandardTemplate;
			ClusterStandardTemplateType templateType;

			try
			{
				ParseArgs(args, out server, out port, out id, out type, out templateFile, out isStandardTemplate, out templateType);
			}
			catch (Exception ex)
			{
				Console.WriteLine("error: {0}", ex);
				return Usage();
			}

			AdminPacket request = null;
			Communication comm = null;

			try
			{
				
				switch (type)
				{
					case TaskType.Insert:
						{
							var ids = new [] { id };
							var templates = new [] { File.ReadAllBytes(templateFile) };
							if (!isStandardTemplate)
							{
								request = AdminPacket.CreatePacket_InsertTemplates(ids, templates);
							}
							else
							{
								var templateTypes = new [] { templateType };
								request = AdminPacket.CreatePacket_InsertTemplates(ids, templates, templateTypes);
							}
						}
						break;
					case TaskType.Delete:
						{
							string[] ids = { id };

							request = AdminPacket.CreatePacket_DeleteTemplates(ids);
							type = TaskType.Delete;
						}
						break;
					default:
						return Usage();
				}

				comm = new Communication(server, port);
				int taskId;
				SendRequest(comm, request, type, out taskId);
				WaitForResult(comm, taskId, type);

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
			finally
			{
				if (request != null)
				{
					request.DestroyPacket();
				}
				if (comm != null)
				{
					comm.Close();
				}
			}
		}

		private static void SendRequest(Communication com, AdminPacket packet, TaskType type, out int taskId)
		{
			AdminPacketReceived received = null;

			Console.WriteLine("sending request ...");

			try
			{
				ClusterStatusCode res = com.SendReceivePacket(packet, out received);
				if (res != ClusterStatusCode.OK)
					throw new Exception(string.Format("command failed. Result: {0}", res));

				switch (type)
				{
					case TaskType.Insert:
						res = received.GetInsertTaskId(out taskId);
						break;
					case TaskType.Delete:
						res = received.GetDeleteTaskId(out taskId);
						break;
					default:
						throw new Exception("invalid task type");
				}

				if (res != ClusterStatusCode.OK)
					throw new Exception(string.Format("command failed. result: {0}", res));
			}
			finally
			{
				if (received != null)
				{
					received.DestroyPacket();
				}
			}

			Console.WriteLine("... task with Id {0} started", taskId);
		}

		private static void WaitForResult(Communication com, int taskId, TaskType type)
		{
			Console.WriteLine("waiting for results ...");
			var status = ClusterInsertDeleteResult.Waiting;
			AdminPacket progressRequest = null;
			AdminPacketReceived progressRequestReceived = null;
			ClusterStatusCode res;

			do
			{
				try
				{
					switch (type)
					{
						case TaskType.Insert:
							progressRequest = AdminPacket.CreatePacket_InsertRequest(taskId);
							res = com.SendReceivePacket(progressRequest, out progressRequestReceived);
							if (res != ClusterStatusCode.OK)
								throw new Exception(string.Format("command failed. result: {0}", res));
							progressRequestReceived.GetInsertTaskResult(out status);
							if (res != ClusterStatusCode.OK)
								throw new Exception(string.Format("command failed. result: {0}", res));
							break;
						case TaskType.Delete:
							progressRequest = AdminPacket.CreatePacket_DeleteRequest(taskId);
							res = com.SendReceivePacket(progressRequest, out progressRequestReceived);
							if (res != ClusterStatusCode.OK)
								throw new Exception(string.Format("command failed. result: {0}", res));
							progressRequestReceived.GetDeleteTaskResult(out status);
							if (res != ClusterStatusCode.OK)
								throw new Exception(string.Format("command failed. result: {0}", res));
							break;
						default:
							throw new Exception("invalid task type");
					}
				}
				finally
				{
					if (progressRequest != null)
						progressRequest.DestroyPacket();
					if (status == ClusterInsertDeleteResult.Waiting)
					{
						if (progressRequestReceived != null)
							progressRequestReceived.DestroyPacket();
					}
				}

				if (status == ClusterInsertDeleteResult.Waiting)
				{
					Console.WriteLine("waiting for \"{0}\" task result ...", type);
					Thread.Sleep(100);
				}

			} while (status == ClusterInsertDeleteResult.Waiting);

			try
			{
				switch (status)
				{
					case ClusterInsertDeleteResult.Succeeded:
						Console.WriteLine("{0} task succeeded", type);
						break;
					case ClusterInsertDeleteResult.Failed:
					case ClusterInsertDeleteResult.ServerNotReady:
					case ClusterInsertDeleteResult.PartiallySucceeded:
						if (status == ClusterInsertDeleteResult.PartiallySucceeded)
						{
							Console.WriteLine("{0} task partially succeded", type);
						}
						else if (status == ClusterInsertDeleteResult.ServerNotReady)
						{
							Console.WriteLine("{0} task failed - server is not yet ready", type);
						}
						else if (status == ClusterInsertDeleteResult.Failed)
						{
							Console.WriteLine("{0} task failed", type);
						}
						int batchSize;
						ClusterInsertDeleteStatus stat;
						if (type == TaskType.Insert)
						{
							res = progressRequestReceived.GetInsertTaskBatchSize(out batchSize);
							if (res != ClusterStatusCode.OK)
								throw new Exception(string.Format("command failed. result: {0}", res));
							for (int i = 0; i < batchSize; i++)
							{
								res = progressRequestReceived.GetInsertTaskStatus(i, out stat);
								if (res != ClusterStatusCode.OK)
									throw new Exception(string.Format("command failed. result: {0}", res));
								Console.WriteLine("template {0} status: {1}", i, stat);
								if (stat == ClusterInsertDeleteStatus.OK) continue;
								string error;
								res = progressRequestReceived.GetInsertTaskError(i, out error);
								if (res != ClusterStatusCode.OK)
									throw new Exception(string.Format("command failed. result: {0}", res));
								if (error != string.Empty)
								{
									Console.WriteLine("error description: {0}", error);
								}
								else
								{
									Console.WriteLine("error description is empty");
								}
							}
						}
						else
						{
							res = progressRequestReceived.GetDeleteTaskBatchSize(out batchSize);
							if (res != ClusterStatusCode.OK)
								throw new Exception(string.Format("command failed. result: {0}", res));
							for (int i = 0; i < batchSize; i++)
							{
								res = progressRequestReceived.GetDeleteTaskStatus(i, out stat);
								if (res != ClusterStatusCode.OK)
									throw new Exception(string.Format("command failed. result: {0}", res));
								Console.WriteLine("template {0} status: {1}", i, stat);
								if (stat == ClusterInsertDeleteStatus.OK) continue;
								string error;
								res = progressRequestReceived.GetDeleteTaskError(i, out error);
								if (res != ClusterStatusCode.OK)
									throw new Exception(string.Format("command failed. result: {0}", res));
								if (error != string.Empty)
								{
									Console.WriteLine("error description: {0}", error);
								}
								else
								{
									Console.WriteLine("error description is empty");
								}
							}
						}

						

						break;
					default:
						Console.WriteLine("Unknown result: {0}", status);
						break;
				}
			}
			finally
			{
				progressRequestReceived.DestroyPacket();
			}
		}
	}
}
