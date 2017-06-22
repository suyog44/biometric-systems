Imports System
Imports System.IO
Imports System.Net
Imports Neurotec.Cluster

Public Class AcceleratorConnection
#Region "Public constructor"

	Public Sub New(ByVal url As String, ByVal username As String, ByVal password As String)
		Server = url
		privateUserName = username
		privatePassword = password
	End Sub

#End Region

#Region "Protected fields"

#End Region

#Region "Private methods"

	Private Shared Sub CheckClusterStatusCode(ByVal clusterStatusCode As ClusterStatusCode)
		If clusterStatusCode <> clusterStatusCode.OK Then
			Throw New Exception(String.Format("Cluster error: {0}", clusterStatusCode.ToString()))
		End If
	End Sub

	Private Shared Function SendReceivePacket(ByVal server As String, ByVal adminPort As Integer, ByVal packet As AdminPacket) As AdminPacketReceived
		Dim comm As Communication = Nothing
		Try
			comm = New Communication(server, adminPort)
			Dim receivedPacket As AdminPacketReceived = Nothing
			Dim communicationResult As ClusterStatusCode = comm.SendReceivePacket(packet, receivedPacket)
			CheckClusterStatusCode(communicationResult)
			If receivedPacket Is Nothing Then
				Throw New Exception("Failed to receive result from server.")
			End If
			Return receivedPacket
		Finally
			If comm IsNot Nothing Then
				comm.Close()
			End If
		End Try
	End Function

#End Region

#Region "Public properties"

	Private privateServer As String
	Public Property Server() As String
		Get
			Return privateServer
		End Get
		Set(ByVal value As String)
			privateServer = value
		End Set
	End Property

	Private privateUserName As String
	Public Property UserName() As String
		Get
			Return privateUserName
		End Get
		Set(ByVal value As String)
			privateUserName = value
		End Set
	End Property

	Private privatePassword As String
	Public Property Password() As String
		Get
			Return privatePassword
		End Get
		Set(ByVal value As String)
			privatePassword = value
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Function GetDbSize() As Integer
		Dim serverAddress As String = Server
		If (Not serverAddress.StartsWith("http://")) Then
			serverAddress = "http://" & serverAddress
		End If
		If serverAddress.EndsWith("/") Then
			serverAddress = serverAddress.Substring(0, serverAddress.Length - 1)
		End If
		Dim uriString As String = Uri.EscapeUriString(String.Format("{0}:{1}/rcontrol.php?a=getDatabaseSize", serverAddress, 80))
		Dim request As WebRequest = WebRequest.Create(uriString)
		request.Credentials = If(String.IsNullOrEmpty(UserName), CredentialCache.DefaultCredentials, New NetworkCredential(UserName, Password))
		request.Method = "POST"
		request.ContentType = "application/x-www-form-urlencoded"
		request.Timeout = 1000 * 60 * 180
		Dim resp As WebResponse = request.GetResponse()
		Dim stream As Stream = resp.GetResponseStream()
		Using reader = New StreamReader(stream)
			Dim value As String = reader.ReadLine()
			Return Integer.Parse(value)
		End Using
	End Function

#End Region

#Region "Public static methods"

	Public Shared Function CheckConnection(ByVal serverAddress As String, ByVal adminPort As Integer) As Boolean
		Dim packet As AdminPacket = Nothing
		Dim received As AdminPacketReceived = Nothing

		Try
			packet = AdminPacket.CreatePacket_NodesInfoRequest()
			received = SendReceivePacket(serverAddress, adminPort, packet)
			If received IsNot Nothing Then
				Dim info() As NodeInfo = Nothing
				Dim res As ClusterStatusCode = received.GetNodesInfo(info)
				If res = ClusterStatusCode.OK Then
					Return True
				End If
			End If
		Catch
			Return False
		Finally
			If received IsNot Nothing Then
				received.DestroyPacket()
			End If

			If packet IsNot Nothing Then
				packet.DestroyPacket()
			End If
		End Try

		Return False
	End Function

#End Region
End Class
