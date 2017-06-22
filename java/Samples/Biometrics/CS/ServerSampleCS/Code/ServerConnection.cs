using System;
using System.IO;
using System.Net;
using Neurotec.Cluster;

namespace Neurotec.Samples.Code
{
	public class AcceleratorConnection
	{
		#region Public constructor

		public AcceleratorConnection(string url, string username, string password)
		{
			Server = url;
			UserName = username;
			Password = password;
		}

		#endregion

		#region Protected fields

		#endregion

		#region Private methods

		private static void CheckClusterStatusCode(ClusterStatusCode clusterStatusCode)
		{
			if (clusterStatusCode != ClusterStatusCode.OK)
			{
				throw new Exception(string.Format("Cluster error: {0}", clusterStatusCode.ToString()));
			}
		}

		private static AdminPacketReceived SendReceivePacket(string server, int adminPort, AdminPacket packet)
		{
			Communication comm = null;
			try
			{
				comm = new Communication(server, adminPort);
				AdminPacketReceived receivedPacket;
				ClusterStatusCode communicationResult = comm.SendReceivePacket(packet, out receivedPacket);
				CheckClusterStatusCode(communicationResult);
				if (receivedPacket == null)
				{
					throw new Exception("Failed to receive result from server.");
				}
				return receivedPacket;
			}
			finally
			{
				if (comm != null) comm.Close();
			}
		}

		#endregion

		#region Public properties

		public string Server { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		#endregion

		#region Public methods

		public int GetDbSize()
		{
			string serverAddress = Server;
			if (!serverAddress.StartsWith(@"http://"))
				serverAddress = @"http://" + serverAddress;
			if (serverAddress.EndsWith(@"/"))
				serverAddress = serverAddress.Substring(0, serverAddress.Length - 1);
			string uriString = Uri.EscapeUriString(string.Format("{0}:{1}/rcontrol.php?a=getDatabaseSize", serverAddress, 80));
			WebRequest request = WebRequest.Create(uriString);
			request.Credentials = string.IsNullOrEmpty(UserName) ? CredentialCache.DefaultCredentials : new NetworkCredential(UserName, Password);
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.Timeout = 1000 * 60 * 180;
			WebResponse resp = request.GetResponse();
			Stream stream = resp.GetResponseStream();
			using (var reader = new StreamReader(stream))
			{
				string value = reader.ReadLine();
				return int.Parse(value);
			}
		}

		#endregion

		#region Public static methods

		public static bool CheckConnection(string serverAddress, int adminPort)
		{
			AdminPacket packet = null;
			AdminPacketReceived received = null;

			try
			{
				packet = AdminPacket.CreatePacket_NodesInfoRequest();
				received = SendReceivePacket(serverAddress, adminPort, packet);
				if (received != null)
				{
					NodeInfo[] info;
					ClusterStatusCode res = received.GetNodesInfo(out info);
					if (res == ClusterStatusCode.OK)
					{
						return true;
					}
				}
			}
			catch
			{
				return false;
			}
			finally
			{
				if (received != null)
				{
					received.DestroyPacket();
				}

				if (packet != null)
				{
					packet.DestroyPacket();
				}
			}

			return false;
		}

		#endregion
	}

}
