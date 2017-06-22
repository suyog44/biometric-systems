Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports Neurotec.Cluster

Public Class Program
#Region "Console interface"

	Private Const DefaultPort As Integer = 24932

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [server:port] [command]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "server:port - matching server address (port is optional)")
		Console.WriteLine()
		Console.WriteLine(vbTab & "commands:")
		Console.WriteLine(vbTab + vbTab & "start                - Start cluster")
		Console.WriteLine(vbTab + vbTab & "stop <id>            - Stop (wait until finished task in progress) server (id is 0) or node (id is above or equal 4)")
		Console.WriteLine(vbTab + vbTab & "kill <id>            - Instantly stop server (id is 0) or node (id is above or equal 4)")
		Console.WriteLine(vbTab + vbTab & "info <info type>     - Print info about cluster or nodes")
		Console.WriteLine(vbTab + vbTab + vbTab & "info type: tasks_short | tasks_complete | nodes | results")
		Console.WriteLine(vbTab + vbTab & "dbupdate             - DB update")
		Console.WriteLine(vbTab + vbTab & "dbchanged <id>      - Notify server of changed templates in DB")
		Console.WriteLine(vbTab + vbTab & "dbflush              - Flush the database")
		Console.WriteLine(vbTab + vbTab & "status               - Gets current server status")
		Console.WriteLine()

		Return 1
	End Function

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Try
			Dim server As String
			Dim port As Integer
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

			Dim cmd As New List(Of String)()
			For i As Integer = 1 To args.Length - 1
				cmd.Add(args(i))
			Next i

			Dim program As New Program(server, port)
			program.ExecuteCommand(cmd.ToArray())
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

	Private Sub ExecuteCommand(ByVal args() As String)
		Dim request As AdminPacket = Nothing
		Dim results As AdminPacketReceived = Nothing

		Try
			Dim nodeId As Integer
			Select Case args(0)
				Case "start"
					request = AdminPacket.CreatePacket_ServerStart()
					If sendPacket(request) Then
						Console.WriteLine("start command sent successfully")
					Else
						Console.WriteLine("failed to send start command")
					End If

				Case "stop"
					nodeId = getNodeId(args(1))
					If args.Length >= 2 AndAlso (nodeId <> -1) Then
						request = AdminPacket.CreatePacket_NodeStop(nodeId)
						If sendPacket(request) Then
							Console.WriteLine(String.Format("stop {0} command sent successfully", args(1)))
						Else
							Console.WriteLine("stop command sent failed")
						End If
					Else
						Console.WriteLine("missing parameter: id")
					End If

				Case "kill"
					nodeId = getNodeId(args(1))
					If args.Length >= 2 AndAlso (nodeId <> -1) Then
						If nodeId = 0 Then
							request = AdminPacket.CreatePacket_ServerKill()
						Else
							request = AdminPacket.CreatePacket_NodeKill(nodeId)
						End If

						If sendPacket(request) Then
							Console.WriteLine(String.Format("kill {0} command sent successfully", args(1)))
						Else
							Console.WriteLine("kill command sent failed")
						End If
					Else
						Console.WriteLine("missing parameter: id")
					End If

				Case "info"
					If args.Length >= 2 Then
						Select Case args(1)
							Case "tasks_short"
								request = AdminPacket.CreatePacket_TasksShortInfoRequest()
								results = SendReceivePacket(request)
								If results IsNot Nothing Then
									PrintShortRunningTasksInfo(results)
								End If

							Case "tasks_complete"
								request = AdminPacket.CreatePacket_TasksInfoRequest()
								results = SendReceivePacket(request)
								If results IsNot Nothing Then
									PrintCompleteRunningTasksInfo(results)
								End If

							Case "nodes"
								request = AdminPacket.CreatePacket_NodesInfoRequest()
								results = SendReceivePacket(request)
								If results IsNot Nothing Then
									PrintNodesInfo(results)
								End If

							Case "results"
								request = AdminPacket.CreatePacket_ResultsInfoRequest()
								results = SendReceivePacket(request)
								If results IsNot Nothing Then
									PrintTaskResultsInfo(results)
								End If

							Case Else
								Console.WriteLine("unknown info type: {0}", args(1))
						End Select
					Else
						Console.WriteLine("missing parameter: info type")
					End If

				Case "dbupdate"
					request = AdminPacket.CreatePacket_DatabaseUpdate()
					If sendPacket(request) Then
						Console.WriteLine("dbupdate command sent successfully")
					Else
						Console.WriteLine("failed to send dbupdate command")
					End If

				Case "dbchanged"
					If args.Length >= 2 Then
						Dim updateIDs(args.Length - 2) As String
						Array.Copy(args, 1, updateIDs, 0, args.Length - 1)
						request = AdminPacket.CreatePacket_UpdateDatabaseIDs(updateIDs)
						If sendPacket(request) Then
							Console.WriteLine("dbchanged command sent successfully")
						Else
							Console.WriteLine("failed to send dbchanged command")
						End If
					Else
						Console.WriteLine("missing parameters: Ids of records to update.")
					End If
				Case "dbflush"
					request = AdminPacket.CreatePacket_DatabaseFlush()
					If sendPacket(request) Then
						Console.WriteLine("dbflush command sent successfully")
					Else
						Console.WriteLine("failed to send dbflush command")
					End If
				Case "status"
					request = AdminPacket.CreatePacket_ServerInfoRequest()
					results = SendReceivePacket(request)
					If results IsNot Nothing Then
						PrintServerInfo(results)
					End If
				Case Else
					Console.WriteLine("command not recognized.")
			End Select
		Catch ex As Exception
			Console.WriteLine("an error has occured:")
			Console.WriteLine(ex)
		Finally
			If request IsNot Nothing Then
				request.DestroyPacket()
			End If
			If results IsNot Nothing Then
				results.DestroyPacket()
			End If
		End Try
	End Sub

	Private Shared Sub PrintShortRunningTasksInfo(ByVal results As AdminPacketReceived)
		Dim taskShortInfo() As TaskShortInfo = Nothing
		results.GetTasksShortInfo(taskShortInfo)
		Console.WriteLine("{0} node(s) running:", taskShortInfo.Length)
		For Each info As TaskShortInfo In taskShortInfo
			Console.WriteLine(vbTab & "id: {0}", info.taskId)
			Console.WriteLine(vbTab & "progress: {0}", info.taskProgress)
			Console.WriteLine(vbTab & "nodes completed: {0}", info.nodesCompleted)
			Console.WriteLine(vbTab & "working nodes: {0}", info.workingNodes)
		Next info
		Console.WriteLine()
	End Sub

	Private Shared Sub PrintCompleteRunningTasksInfo(ByVal results As AdminPacketReceived)
		Dim taskInfos() As TaskInfo = Nothing
		results.GetTasksInfo(taskInfos)

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
	End Sub

	Private Shared Sub PrintNodesInfo(ByVal results As AdminPacketReceived)
		Dim nodeInfos() As NodeInfo = Nothing
		results.GetNodesInfo(nodeInfos)
		Console.WriteLine("{0} node(s) running:", nodeInfos.Length)
		For Each nodeInfo As NodeInfo In nodeInfos
			Console.WriteLine("{0} ({1})", nodeInfo.id, nodeInfo.state)
		Next nodeInfo
		Console.WriteLine()
	End Sub

	Private Shared Sub PrintServerInfo(ByVal results As AdminPacketReceived)
		Dim status As ClusterServerStatus
		Dim code As ClusterStatusCode = results.GetServerInfo(status)
		If code <> ClusterStatusCode.OK Then
			Console.WriteLine("Error while getting server info")
		Else
			Console.WriteLine("Server status is: {0}", status)
		End If

		Console.WriteLine()
	End Sub

	Private Shared Sub PrintTaskResultsInfo(ByVal results As AdminPacketReceived)
		Dim resInfo() As Integer = Nothing
		results.GetResults(resInfo)

		Console.WriteLine("{0} completed task(s):", resInfo.Length)
		For Each result As Integer In resInfo
			Console.WriteLine(result)
		Next result
		Console.WriteLine()
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

	Private Function SendPacket(ByVal packet As AdminPacket) As Boolean
		Dim comm As Communication = Nothing

		Try
			comm = New Communication(_server, _port)
			Dim communicationResult As ClusterStatusCode = comm.SendPacket(packet)
			If communicationResult = ClusterStatusCode.OK Then
				Return True
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
		Return False
	End Function

	Private Shared Function GetNodeId(ByVal idString As String) As Integer
		If idString = "server" Then
			Return 0
		End If

		Try
			Return Integer.Parse(idString)
		Catch
			Return -1
		End Try
	End Function

#End Region	' Server status info
End Class
