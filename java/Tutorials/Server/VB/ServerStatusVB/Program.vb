Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Cluster

Public Class Program
#Region "Console interface"

	Private Const DefaultPort As Integer = 24932

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [server:port]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "server:port - matching server address (port is optional)")
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 1 Then
			Return Usage()
		End If

		Dim server As String
		Dim port As Integer
		Try
			Try
				If args(0).Contains(":") Then
					Dim splitAddress() As String = args(0).Split(":"c)
					server = splitAddress(0)
					port = Integer.Parse(splitAddress(1))
				Else
					server = args(0)
					port = DefaultPort
					Console.WriteLine("port not specified; using default: {0}", DefaultPort)
					Console.WriteLine()
				End If
			Catch
				Console.WriteLine("server address in wrong format.")
				Return -1
			End Try

			Dim program As New Program(server, port)
			program.PrintServerStatus()
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		End Try
	End Function

#End Region	' Console interface

#Region "Server status info"

	Private ReadOnly _server As String
	Private ReadOnly _port As Integer

	Public Sub New(ByVal server As String, ByVal port As Integer)
		_server = server
		_port = port
	End Sub

	Public Sub PrintServerStatus()
		Try
			Console.WriteLine("asking info from {0}: {1} ...", _server, _port)
			Console.WriteLine()

			Console.WriteLine("requesting info about server ...")

			Dim adminPacket As AdminPacket = adminPacket.CreatePacket_ServerInfoRequest()
			Dim adminPacketReceived As AdminPacketReceived = SendReceivePacket(adminPacket)
			Dim serverStatus As ClusterServerStatus
			If adminPacketReceived.GetServerInfo(serverStatus) = ClusterStatusCode.OK Then
				Console.WriteLine("server status: {0}", serverStatus)
			Else
				Console.WriteLine("unable to determine server status")
			End If
			Console.WriteLine()

			Console.WriteLine("requesting info about nodes ...")

			adminPacket = adminPacket.CreatePacket_NodesInfoRequest()
			adminPacketReceived = SendReceivePacket(adminPacket)

			If adminPacketReceived IsNot Nothing Then
				Dim nodeInfos() As NodeInfo = Nothing
				adminPacketReceived.GetNodesInfo(nodeInfos)
				Console.WriteLine("{0} node(s) running:", nodeInfos.Length)
				For Each nodeInfo As NodeInfo In nodeInfos
					Console.WriteLine("{0} ({1})", nodeInfo.id, nodeInfo.state)
				Next nodeInfo
				Console.WriteLine()

				adminPacketReceived.DestroyPacket()
			Else
				Console.WriteLine("failed to receive tasks info")
			End If

			adminPacket.DestroyPacket()

			Console.WriteLine("requesting info about tasks ...")

			adminPacket = adminPacket.CreatePacket_TasksInfoRequest()
			adminPacketReceived = SendReceivePacket(adminPacket)

			If adminPacketReceived IsNot Nothing Then
				Dim taskInfos() As TaskInfo = Nothing
				adminPacketReceived.GetTasksInfo(taskInfos)

				Console.WriteLine("{0} task(s):", taskInfos.Length)
				For Each taskInfo As TaskInfo In taskInfos
					Console.WriteLine(vbTab & "id: {0}", taskInfo.taskId)
					Console.WriteLine(vbTab & "progress: {0}", taskInfo.taskProgress)
					Console.WriteLine(vbTab & "nodes completed: {0}", taskInfo.nodesCompleted)
					Console.WriteLine(vbTab & "working nodes: {0}", taskInfo.workingNodes)
					For i As Integer = 0 To taskInfo.workingNodes - 1
						Console.WriteLine(vbTab + vbTab & "node ID: {0}", taskInfo.workingNodeInfo(i).nodeId)
						Console.WriteLine(vbTab + vbTab & "node progress: {0}", taskInfo.workingNodeInfo(i).progress)
					Next i
				Next taskInfo
				Console.WriteLine()

				adminPacketReceived.DestroyPacket()
			Else
				Console.WriteLine("failed to receive tasks info")
			End If

			adminPacket.DestroyPacket()

			Console.WriteLine("requesting info about results ...")

			adminPacket = adminPacket.CreatePacket_ResultsInfoRequest()
			adminPacketReceived = SendReceivePacket(adminPacket)

			If adminPacketReceived IsNot Nothing Then
				Dim results() As Integer = Nothing
				adminPacketReceived.GetResults(results)

				Console.WriteLine("{0} completed task(s):", results.Length)
				For Each result As Integer In results
					Console.WriteLine(result)
				Next result
				Console.WriteLine()

				adminPacketReceived.DestroyPacket()
			Else
				Console.WriteLine("failed to receive results info")
			End If

			adminPacket.DestroyPacket()
		Catch ex As Exception
			Console.WriteLine("an error has occured")
			Console.WriteLine(ex)
		End Try
	End Sub

	Private Function SendReceivePacket(ByVal packet As AdminPacket) As AdminPacketReceived
		Dim comm As Communication = Nothing

		Try
			comm = New Communication(_server, _port)
			Dim receivedPacket As AdminPacketReceived = Nothing
			Dim communicationResult As ClusterStatusCode = comm.SendReceivePacket(packet, receivedPacket)
			If communicationResult = ClusterStatusCode.OK Then
				Return receivedPacket
			Else
				Console.WriteLine("command failed. Result: {0}", communicationResult)
			End If
		Catch '(Exception ex)
			Console.WriteLine("failed to get node info")
		Finally
			If comm IsNot Nothing Then
				comm.Close()
			End If
		End Try
		Return Nothing
	End Function

#End Region	' Server status info
End Class
