Imports Microsoft.VisualBasic
Imports System
Imports System.Threading
Imports System.IO

Imports Neurotec.Cluster

Public Class Program
	Private Const DefaultAddress As String = "127.0.0.1"
	Private Const DefaultPort As Integer = 25452
	Private Const DefaultQuery As String = "SELECT node_id, dbid FROM node_tbl"

	Private Shared Function Usage() As Int32
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} -s [server:port] -t [template] -q [query] -y [template type]", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "{0} -s [server:port] -t [template] -q [query]", TutorialUtils.GetAssemblyName())
		Console.WriteLine("")
		Console.WriteLine(vbTab & "-s server:port   - matching server address (optional parameter, if address specified - port is optional)")
		Console.WriteLine(vbTab & "-t template      - template to be sent for matching (required)")
		Console.WriteLine(vbTab & "-q query         - database query to execute (optional)")
		Console.WriteLine(vbTab & "-y template type - template type (optional - specify parameter only if template is not NTemplate, but FMRecord ansi or iso). Parameter values: ansi or iso.")
		Console.WriteLine(vbTab & "Important! Template type parameter is available only for Accelerator product!")
		Console.WriteLine("default query (if not specified): {0}", DefaultQuery)
		Return 1
	End Function

	Private Shared Sub ParseArgs(ByVal args() As String, <System.Runtime.InteropServices.Out()> ByRef serverIp As String, <System.Runtime.InteropServices.Out()> ByRef serverPort As Integer, <System.Runtime.InteropServices.Out()> ByRef templateFile As String, <System.Runtime.InteropServices.Out()> ByRef query As String, <System.Runtime.InteropServices.Out()> ByRef isStandardTemplate As Boolean, <System.Runtime.InteropServices.Out()> ByRef templateType As ClusterStandardTemplateType)
		serverIp = DefaultAddress
		serverPort = DefaultPort
		query = DefaultQuery

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
				Case "q"c
					i += 1
					query = optarg
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

		If templateFile = String.Empty Then
			Throw New Exception("template - required parameter - not specified")
		End If
	End Sub

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)

		If args.Length < 2 Then
			Return Usage()
		End If

		Dim templateFile As String = String.Empty
		Dim template() As Byte
		Dim server As String = String.Empty
		Dim port As Integer
		Dim query As String = String.Empty
		Dim isStandardTemplate As Boolean
		Dim templateType As ClusterStandardTemplateType
		Dim comm As Communication = Nothing
		Try

			Try
				ParseArgs(args, server, port, templateFile, query, isStandardTemplate, templateType)
			Catch ex As Exception
				Console.WriteLine("error: {0}", ex.ToString())
				Return Usage()
			End Try

			Try
				template = File.ReadAllBytes(templateFile)
			Catch
				Console.WriteLine("could not load template from file {0}.", args(0))
				Return -1
			End Try

			comm = New Communication(server, port)

			Dim taskId As Integer
			If (Not isStandardTemplate) Then
				SendRequest(comm, template, query, taskId)
			Else
				SendRequest(comm, template, templateType, query, taskId)
			End If
			WaitForResult(comm, taskId)

			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		Finally
			If comm IsNot Nothing Then
				comm.Close()
			End If
		End Try
	End Function

	Private Shared Sub SendRequest(ByVal com As Communication, ByVal template() As Byte, ByVal query As String, <System.Runtime.InteropServices.Out()> ByRef taskId As Integer)
		'Const fingerprintMatchingFar As UInteger = 60
		'Const fingerprintMaximalRotation As Byte = 5

		Console.WriteLine("sending request ...")

		'Const part As UInteger = 0
		Dim parameters As New MatchingParameters()
		'parameters.AddParameter(part, NMatcher.ParameterFingersMaximalRotation, fingerprintMaximalRotation)
		'parameters.AddParameter(part, NMatcher.ParameterMatchingThreshold, fingerprintMatchingFar)

		Dim clientPacket As ClientPacket = clientPacket.CreateTask(ClusterTaskMode.Normal, template, query, parameters, 100)
		Dim received As ClientPacketReceived = Nothing
		Dim res As ClusterStatusCode = com.SendReceivePacket(clientPacket, received)
		If res <> ClusterStatusCode.OK Then
			Throw New Exception(String.Format("failed to create task. task was not added. error: {0}", res))
		End If
		res = received.GetTaskID(taskId)
		If res <> ClusterStatusCode.OK Then
			Throw New Exception(String.Format("failed to get task id. task was not added. error: {0}", res))
		End If
		If taskId = -1 Then
			Throw New Exception("failed to get task id. task was not added.")
		End If
		clientPacket.DestroyPacket()
		received.DestroyPacket()

		Console.WriteLine("... task with ID {0} started", taskId)
	End Sub

	Private Shared Sub SendRequest(ByVal com As Communication, ByVal template() As Byte, ByVal templateType As ClusterStandardTemplateType, ByVal query As String, <System.Runtime.InteropServices.Out()> ByRef taskId As Integer)
		'Const fingerprintMatchingFar As UInteger = 60
		'Const fingerprintMaximalRotation As Byte = 5

		Console.WriteLine("sending request ...")

		'Const part As UInteger = 0
		Dim parameters As New MatchingParameters()
		'parameters.AddParameter(part, NMatcher.ParameterFingersMaximalRotation, fingerprintMaximalRotation)
		'parameters.AddParameter(part, NMatcher.ParameterMatchingThreshold, fingerprintMatchingFar)

		Dim clientPacket As ClientPacket = clientPacket.CreateTask(ClusterTaskMode.Normal, template, templateType, query, parameters, 100)
		Dim received As ClientPacketReceived = Nothing
		Dim res As ClusterStatusCode = com.SendReceivePacket(clientPacket, received)
		If res <> ClusterStatusCode.OK Then
			Throw New Exception(String.Format("failed to create task. task was not added. error: {0}", res))
		End If
		res = received.GetTaskID(taskId)
		If res <> ClusterStatusCode.OK Then
			Throw New Exception(String.Format("failed to get task id. task was not added. error: {0}", res))
		End If
		If taskId = -1 Then
			Throw New Exception("failed to get task id. task was not added.")
		End If
		clientPacket.DestroyPacket()
		received.DestroyPacket()

		Console.WriteLine("... task with ID {0} started", taskId)
	End Sub

	Private Shared Sub WaitForResult(ByVal com As Communication, ByVal taskId As Integer)
		Console.WriteLine("waiting for results ...")
		Dim progress As Integer = 0
		Dim count As Integer = 0
		Do
			Dim progressRequest As ClientPacket = ClientPacket.CreateProgressRequest(taskId)
			Dim progressRequestReceived As ClientPacketReceived = Nothing
			com.SendReceivePacket(progressRequest, progressRequestReceived)

			progressRequestReceived.GetTaskProgress(progress, count)
			progressRequest.DestroyPacket()
			progressRequestReceived.DestroyPacket()

			If progress <> 100 Then
				Thread.Sleep(100)
			End If

			If progress < 0 Then
				Console.Write("task aborted on server side.")
				Return
			End If

		Loop While progress <> 100

		If count > 0 Then
			Dim resultRequest As ClientPacket = ClientPacket.CreateResultRequest(taskId, 1, count)
			Dim resultsReceived As ClientPacketReceived = Nothing
			com.SendReceivePacket(resultRequest, resultsReceived)
			Dim results() As ClusterResults = Nothing
			resultsReceived.GetResults(results)
			For Each clusterRes As ClusterResults In results
				Console.WriteLine("... matched with id: {0}, score: {1}", clusterRes.id, clusterRes.similarity)
			Next clusterRes
			resultsReceived.DestroyPacket()
			resultRequest.DestroyPacket()
		Else
			Console.WriteLine("... no matches")
		End If

		Dim deleteRequest As ClientPacket = ClientPacket.CreateResultDelete(taskId)
		com.SendPacket(deleteRequest)
		deleteRequest.DestroyPacket()
	End Sub
End Class
