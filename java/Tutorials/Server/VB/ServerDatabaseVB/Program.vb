Imports Microsoft.VisualBasic
Imports System
Imports System.Threading
Imports System.IO

Imports Neurotec.Cluster

Public Class Program
	Private Enum TaskType As Integer
		Insert = 0
		Delete = 1
	End Enum

	Private Const DefaultAddress As String = "127.0.0.1"
	Private Const DefaultPort As Integer = 24932

	Private Shared Function Usage() As Integer
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} -s [server:port] -c [command] -i [template id] -t [template] -y [template type]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(vbTab & "-s server:port   - matching server address (optional parameter, if address specified - port is optional)")
		Console.WriteLine(vbTab & "-c command       - command to be performed (either insert or delete) (required)")
		Console.WriteLine(vbTab & "-i template id   - id of template to be deleted or inserted (required)")
		Console.WriteLine(vbTab & "-t template      - template to be inserted (required only for insert)")
		Console.WriteLine(vbTab & "-y template type - type of template to be inserted (ansi or iso) (valid for insert to MegaMatcher Accelerator only) (optional)")
		Console.WriteLine("examples:")
		Console.WriteLine(vbTab & "{0} -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp ", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "{0} -s 127.0.0.1:24932 -c insert -i testId -t testTemplate.tmp -y ansi", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "{0} -s 127.0.0.1:24932 -c delete -i testId", TutorialUtils.GetAssemblyName())

		Return 1
	End Function

	Private Shared Sub ParseArgs(ByVal args() As String, <System.Runtime.InteropServices.Out()> ByRef serverIp As String, <System.Runtime.InteropServices.Out()> ByRef serverPort As Integer, <System.Runtime.InteropServices.Out()> ByRef id As String, <System.Runtime.InteropServices.Out()> ByRef taskType As TaskType, <System.Runtime.InteropServices.Out()> ByRef templateFile As String, <System.Runtime.InteropServices.Out()> ByRef isStandardTemplate As Boolean, <System.Runtime.InteropServices.Out()> ByRef templateType As ClusterStandardTemplateType)
		serverIp = DefaultAddress
		serverPort = DefaultPort

		id = String.Empty
		taskType = taskType.Insert
		templateFile = String.Empty

		isStandardTemplate = False
		templateType = ClusterStandardTemplateType.Ansi

		For i As Integer = 0 To args.Length - 1
			Dim optarg As String = String.Empty

			If args(i).Length <> 2 OrElse args(i).Chars(0) <> "-"c Then
				Throw New Exception("parameter parse error")
			End If

			If args.Length > i + 1 AndAlso args(i + 1).Chars(0) <> "-"c Then
				optarg = args(i + 1) ' we have a parameter for given flag
			End If

			If optarg = String.Empty Then
				Throw New Exception("parameter parse error")
			End If

			Select Case args(i).Chars(1)
				Case "s"c
					i += 1
					If optarg.Contains(":") Then
						Dim splitAddress() As String = optarg.Split(":"c)
						serverIp = splitAddress(0)
						serverPort = Integer.Parse(splitAddress(1))
					Else
						serverIp = optarg
						serverPort = DefaultPort
					End If
				Case "t"c
					i += 1
					templateFile = optarg
				Case "c"c
					i += 1
					taskType = CType(System.Enum.Parse(GetType(TaskType), optarg, True), TaskType)
				Case "i"c
					i += 1
					id = optarg

				Case "y"c
					i += 1
					If optarg.Equals("ansi", StringComparison.InvariantCultureIgnoreCase) Then
						templateType = ClusterStandardTemplateType.Ansi
						isStandardTemplate = True
					ElseIf optarg.Equals("iso", StringComparison.InvariantCultureIgnoreCase) Then
						templateType = ClusterStandardTemplateType.Iso
						isStandardTemplate = True
					Else
						Throw New Exception("wrong standard (should be iso or ansi)!")
					End If
				Case Else
					Throw New Exception("wrong parameter found!")
			End Select
		Next i

		Console.WriteLine("selecting task type: {0}", taskType)

		If id = String.Empty Then
			Throw New Exception("id - required parameter - not specified")
		End If
		If taskType = taskType.Insert Then
			If templateFile = String.Empty Then
				Throw New Exception("template - required parameter - not specified")
			End If
		End If
	End Sub

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 2 Then
			Return Usage()
		End If

		Dim server As String = String.Empty
		Dim port As Integer
		Dim type As TaskType
		Dim templateFile As String = String.Empty
		Dim id As String = String.Empty
		Dim isStandardTemplate As Boolean
		Dim templateType As ClusterStandardTemplateType

		Try
			ParseArgs(args, server, port, id, type, templateFile, isStandardTemplate, templateType)
		Catch ex As Exception
			Console.WriteLine("error: {0}", ex.ToString())
			Return Usage()
		End Try

		Dim request As AdminPacket = Nothing
		Dim comm As Communication = Nothing

		Try

			Select Case type
				Case TaskType.Insert
					Dim ids() As String = {id}
					Dim templates()() As Byte = {File.ReadAllBytes(templateFile)}
					If (Not isStandardTemplate) Then
						request = AdminPacket.CreatePacket_InsertTemplates(ids, templates)
					Else
						Dim templateTypes() As ClusterStandardTemplateType = {templateType}
						request = AdminPacket.CreatePacket_InsertTemplates(ids, templates, templateTypes)
					End If
				Case TaskType.Delete
					Dim ids() As String = {id}

					request = AdminPacket.CreatePacket_DeleteTemplates(ids)
					type = TaskType.Delete
				Case Else
					Return Usage()
			End Select

			comm = New Communication(server, port)
			Dim taskId As Integer
			SendRequest(comm, request, type, taskId)
			WaitForResult(comm, taskId, type)
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			If request IsNot Nothing Then
				request.DestroyPacket()
			End If
			If comm IsNot Nothing Then
				comm.Close()
			End If
		End Try
	End Function

	Private Shared Sub SendRequest(ByVal com As Communication, ByVal packet As AdminPacket, ByVal type As TaskType, ByRef taskId As Integer)
		Dim received As AdminPacketReceived = Nothing

		Console.WriteLine("sending request ...")

		Try
			Dim res As ClusterStatusCode = com.SendReceivePacket(packet, received)
			If res <> ClusterStatusCode.OK Then
				Throw New Exception(String.Format("command failed. Result: {0}", res))
			End If

			Select Case type
				Case TaskType.Insert
					res = received.GetInsertTaskId(taskId)
				Case TaskType.Delete
					res = received.GetDeleteTaskId(taskId)
				Case Else
					Throw New Exception("invalid task type")
			End Select

			If res <> ClusterStatusCode.OK Then
				Throw New Exception(String.Format("command failed. result: {0}", res))
			End If
		Finally
			If received IsNot Nothing Then
				received.DestroyPacket()
			End If
		End Try

		Console.WriteLine("... task with Id {0} started", taskId)
	End Sub

	Private Shared Sub WaitForResult(ByVal com As Communication, ByVal taskId As Integer, ByVal type As TaskType)
		Console.WriteLine("waiting for results ...")
		Dim status As ClusterInsertDeleteResult = ClusterInsertDeleteResult.Waiting
		Dim progressRequest As AdminPacket = Nothing
		Dim progressRequestReceived As AdminPacketReceived = Nothing
		Dim res As ClusterStatusCode

		Do
			Try
				Select Case type
					Case TaskType.Insert
						progressRequest = AdminPacket.CreatePacket_InsertRequest(taskId)
						res = com.SendReceivePacket(progressRequest, progressRequestReceived)
						If res <> ClusterStatusCode.OK Then
							Throw New Exception(String.Format("command failed. result: {0}", res))
						End If
						progressRequestReceived.GetInsertTaskResult(status)
						If res <> ClusterStatusCode.OK Then
							Throw New Exception(String.Format("command failed. result: {0}", res))
						End If
					Case TaskType.Delete
						progressRequest = AdminPacket.CreatePacket_DeleteRequest(taskId)
						res = com.SendReceivePacket(progressRequest, progressRequestReceived)
						If res <> ClusterStatusCode.OK Then
							Throw New Exception(String.Format("command failed. result: {0}", res))
						End If
						progressRequestReceived.GetDeleteTaskResult(status)
						If res <> ClusterStatusCode.OK Then
							Throw New Exception(String.Format("command failed. result: {0}", res))
						End If
					Case Else
						Throw New Exception("invalid task type")
				End Select
			Finally
				If progressRequest IsNot Nothing Then
					progressRequest.DestroyPacket()
				End If
				If status = ClusterInsertDeleteResult.Waiting Then
					If progressRequestReceived IsNot Nothing Then
						progressRequestReceived.DestroyPacket()
					End If
				End If
			End Try

			If status = ClusterInsertDeleteResult.Waiting Then
				Console.WriteLine("waiting for ""{0}"" task result ...", type)
				Thread.Sleep(100)
			End If

		Loop While status = ClusterInsertDeleteResult.Waiting

		Try
			Select Case status
				Case ClusterInsertDeleteResult.Succeeded
					Console.WriteLine("{0} task succeeded", type)
				Case ClusterInsertDeleteResult.Failed, ClusterInsertDeleteResult.ServerNotReady, ClusterInsertDeleteResult.PartiallySucceeded
					If status = ClusterInsertDeleteResult.PartiallySucceeded Then
						Console.WriteLine("{0} task partially succeded", type)
					ElseIf status = ClusterInsertDeleteResult.ServerNotReady Then
						Console.WriteLine("{0} task failed - server is not yet ready", type)
					ElseIf status = ClusterInsertDeleteResult.Failed Then
						Console.WriteLine("{0} task failed", type)
					End If
					Dim batchSize As Integer = 0
					Dim stat As ClusterInsertDeleteStatus = ClusterInsertDeleteStatus.OK
					If type = TaskType.Insert Then
						res = progressRequestReceived.GetInsertTaskBatchSize(batchSize)
						If res <> ClusterStatusCode.OK Then
							Throw New Exception(String.Format("command failed. result: {0}", res))
						End If
						For i As Integer = 0 To batchSize - 1
							res = progressRequestReceived.GetInsertTaskStatus(i, stat)
							If res <> ClusterStatusCode.OK Then
								Throw New Exception(String.Format("command failed. result: {0}", res))
							End If
							Console.WriteLine("template {0} status: {1}", i, stat)
							If stat <> ClusterInsertDeleteStatus.OK Then
								Dim [error] As String = String.Empty
								res = progressRequestReceived.GetInsertTaskError(i, [error])
								If res <> ClusterStatusCode.OK Then
									Throw New Exception(String.Format("command failed. Result: {0}", res))
								End If
								If [error] <> String.Empty Then
									Console.WriteLine("error description: {0}", [error])
								Else
									Console.WriteLine("error description is empty")
								End If
							End If
						Next i
					Else
						res = progressRequestReceived.GetDeleteTaskBatchSize(batchSize)
						If res <> ClusterStatusCode.OK Then
							Throw New Exception(String.Format("command failed. result: {0}", res))
						End If
						For i As Integer = 0 To batchSize - 1
							res = progressRequestReceived.GetDeleteTaskStatus(i, stat)
							If res <> ClusterStatusCode.OK Then
								Throw New Exception(String.Format("command failed. result: {0}", res))
							End If
							Console.WriteLine("template {0} status: {1}", i, stat)
							If stat <> ClusterInsertDeleteStatus.OK Then
								Dim [error] As String = String.Empty
								res = progressRequestReceived.GetDeleteTaskError(i, [error])
								If res <> ClusterStatusCode.OK Then
									Throw New Exception(String.Format("command failed. Result: {0}", res))
								End If
								If [error] <> String.Empty Then
									Console.WriteLine("error description: {0}", [error])
								Else
									Console.WriteLine("error description is empty")
								End If
							End If
						Next i
					End If

				Case Else
					Console.WriteLine("Unknown result: {0}", status)
			End Select
		Finally
			If progressRequestReceived IsNot Nothing Then
				progressRequestReceived.DestroyPacket()
			End If
		End Try
	End Sub
End Class
