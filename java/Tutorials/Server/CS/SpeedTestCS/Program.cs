using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Neurotec.Cluster;

namespace Neurotec.Tutorials
{
	class Program
	{
		private static int Usage()
		{
			Console.WriteLine("Application indented for MegaMatcher Accelerator");
			Console.WriteLine();
			Console.WriteLine("usage:");
			Console.WriteLine("\t{0} [server] [client port] [directory] [user name] [password] ", TutorialUtils.GetAssemblyName());
			Console.WriteLine("\tserver      - matching server address");
			Console.WriteLine("\tclient port - matching server port");
			Console.WriteLine("\tdirectory   - directory containing templates to match");
			Console.WriteLine();
			Console.WriteLine("example: 192.168.0.1 25452 c:\\templates Admin Admin");
			Console.WriteLine();

			return 1;
		}

		private const int SendThreadCount = 24;
		private const int RecieveThreadCount = 12;

		static int Main(string[] args)
		{
			TutorialUtils.PrintTutorialHeader(args);

			if (args.Length < 5)
			{
				return Usage();
			}

			try
			{
				string serverAddress = args[0];
				int clientPort = int.Parse(args[1]);
				string directory = args[2];
				string userName = args[3];
				string password = args[4];

				int dbSize = GetDbSize(serverAddress, 80, userName, password);
				IEnumerable<byte[]> templates = GetTemplates(directory);

				var tasks = new MatchingTaskCollection();
				foreach (byte[] t in templates)
				{
					tasks.Enqueue(new MatchingTask(t));
				}
				int taskCount = tasks.Count;

				var stopWatch = new Stopwatch();
				stopWatch.Start();
				SendRecieveTasks(serverAddress, clientPort, tasks, SendThreadCount, RecieveThreadCount);
				stopWatch.Stop();

				double speed = dbSize * taskCount / stopWatch.Elapsed.TotalSeconds;

				var formatInfo = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
				formatInfo.NumberGroupSeparator = " ";
				Console.WriteLine("Speed: \t\t{0} templates per second", speed.ToString("N0", formatInfo));
				Console.WriteLine("Elapsed time:\t {0:f2} seconds", stopWatch.Elapsed.TotalSeconds);
				Console.WriteLine("DBSize: \t{0}", dbSize);
				Console.WriteLine("Sent template count: {0}", taskCount);

				return 0;
			}
			catch (Exception ex)
			{
				return TutorialUtils.PrintException(ex);
			}
		}

		private static void SendRecieveTasks(string serverAddress, int clientPort, MatchingTaskCollection inputMatchingTasks, int sendThreadCount, int recieveThreadCount)
		{
			var exceptions = new List<Exception>();
			var sentTasks = new MatchingTaskCollection();
			int producersWorking = sendThreadCount;

			var workerThreads = new List<Thread>();

			for (int i = 0; i < sendThreadCount; i++)
			{
				var workerThread = new Thread(delegate()
				{
					Communication comm = null;
					try
					{
						comm = new Communication(serverAddress, clientPort);
						while (true)
						{
							MatchingTask task = inputMatchingTasks.Dequeue();
							if (task == null) return;
							int taskId = SendMatchingTask(comm, task, ClusterTaskMode.Normal);
							task.TaskId = taskId;
							if (taskId != -1)
							{
								sentTasks.Enqueue(task);
							}
						}
					}
					catch (Exception ex)
					{
						lock (exceptions)
						{
							exceptions.Add(ex);
						}
					}
					finally
					{
						Interlocked.Decrement(ref producersWorking);
						if (comm != null) comm.Close();
					}
				});
				workerThread.Start();
				workerThreads.Add(workerThread);
			}

			for (int i = 0; i < recieveThreadCount; i++)
			{
				var workerThread = new Thread(() =>
				{
					Communication comm = null;
					try
					{
						comm = new Communication(serverAddress, clientPort);
						while (true)
						{
							MatchingTask task = sentTasks.Dequeue();
							if (task != null)
							{
								WaitForMatchingTask(comm, task.TaskId);
							}
							else
							{
								if (producersWorking == 0) return;
								Thread.Sleep(100);
							}
						}
					}
					catch (Exception ex)
					{
						lock (exceptions)
						{
							exceptions.Add(ex);
						}
					}
					finally
					{
						if (comm != null) comm.Close();
					}
				});
				workerThread.Start();
				workerThreads.Add(workerThread);
			}

			foreach (Thread workerThread in workerThreads)
			{
				workerThread.Join();
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions.ToArray());
			}
		}

		private static int SendMatchingTask(Communication comm, MatchingTask task, ClusterTaskMode clusterTaskMode)
		{
			ClientPacketReceived received = null;
			ClientPacket clientPacket = null;
			try
			{
				clientPacket = ClientPacket.CreateTask(clusterTaskMode, task.Template, " ", null, 0);
				ClusterStatusCode res = comm.SendReceivePacket(clientPacket, out received);
				CheckClusterStatusCode(res);
				int taskId;
				received.GetTaskID(out taskId);
				task.TaskId = taskId;
				return taskId;
			}
			finally
			{
				if (clientPacket != null) clientPacket.DestroyPacket();
				if (received != null) received.DestroyPacket();
			}
		}

		private static void WaitForMatchingTask(Communication comm, int taskId)
		{
			int progress;
			do
			{
				ClientPacket progressRequest = ClientPacket.CreateProgressRequest(taskId);
				ClientPacketReceived progressRequestReceived;
				ClusterStatusCode status = comm.SendReceivePacket(progressRequest, out progressRequestReceived);
				CheckClusterStatusCode(status);
				int count;
				status = progressRequestReceived.GetTaskProgress(out progress, out count);
				CheckClusterStatusCode(status);

				progressRequestReceived.DestroyPacket();
				progressRequest.DestroyPacket();
				if (progress != 100) Thread.Sleep(100);
			} while (progress != 100);

			ClientPacket deleteRequest = ClientPacket.CreateResultDelete(taskId);
			comm.SendPacket(deleteRequest);
			deleteRequest.DestroyPacket();
		}

		private static IEnumerable<byte[]> GetTemplates(string directory)
		{
			if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
				throw new ArgumentException("Directory doesn't exists");

			string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
			var templates = new byte[files.Length][];
			for (int i = 0; i < files.Length; i++)
			{
				templates[i] = File.ReadAllBytes(files[i]);
			}
			return templates;
		}

		private static void CheckClusterStatusCode(ClusterStatusCode clusterStatusCode)
		{
			if (clusterStatusCode != ClusterStatusCode.OK)
			{
				throw new Exception(string.Format("Cluster error: {0}", clusterStatusCode));
			}
		}

		private static int GetDbSize(string serverAddress, int port, string username, string password)
		{
			if (!serverAddress.StartsWith(@"http://"))
				serverAddress = @"http://" + serverAddress;
			if (serverAddress.EndsWith(@"/"))
				serverAddress = serverAddress.Substring(0, serverAddress.Length - 1);
			string uriString = Uri.EscapeUriString(string.Format("{0}:{1}/rcontrol.php?a=getDatabaseSize", serverAddress, port));
			WebRequest request = WebRequest.Create(uriString);
			request.Credentials = string.IsNullOrEmpty(username) ? CredentialCache.DefaultCredentials : new NetworkCredential(username, password);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.Timeout = 1000 * 60 * 180;
			WebResponse resp = request.GetResponse();
			{
				Stream stream = resp.GetResponseStream();
				if (stream != null)
					using (var reader = new StreamReader(stream))
					{
						string value = reader.ReadLine();
						if (value != null) return int.Parse(value);
					}
			}
			throw new Exception("Failed to get Accelerator database size");
		}

		private class AggregateException : Exception
		{
			public AggregateException(params Exception[] exceptions)
			{
				Exceptions = exceptions;
			}

			public Exception[] Exceptions
			{
				get; private set;
			}

			public override string ToString()
			{
				if (Exceptions == null || Exceptions.Length > 0) return base.ToString();
				var result = new StringBuilder(string.Format("{0} exception(s) occurred:\n", Exceptions.Length));
				foreach (Exception ex in Exceptions)
				{
					result.Append(string.Format("{0}{1}{1}", ex.Message, Environment.NewLine));
				}
				return result.ToString();
			}
		}

		private class MatchingTask
		{
			public MatchingTask(byte[] template)
			{
				Template = template;
				TaskId = -1;
			}

			public int TaskId { get; set; }

			public byte[] Template { get; private set; }
		}

		private class MatchingTaskCollection : Queue<MatchingTask>
		{
			public new MatchingTask Dequeue()
			{
				lock (this)
				{
					if (Count == 0) return null;
					return base.Dequeue();
				}
			}

			public new void Enqueue(MatchingTask task)
			{
				lock (this)
				{
					base.Enqueue(task);
				}
			}
		}
	}
}
