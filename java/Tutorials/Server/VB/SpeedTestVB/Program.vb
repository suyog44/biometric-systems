Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading
Imports Neurotec.Cluster

Friend Class Program
	Private Shared Function Usage() As Integer
		Console.WriteLine("Application indented for MegaMatcher Accelerator")
		Console.WriteLine()
		Console.WriteLine("usage:")
		Console.WriteLine(vbTab & "{0} [server] [client port] [directory] [user name] [password] ", TutorialUtils.GetAssemblyName())
		Console.WriteLine(vbTab & "server      - matching server address")
		Console.WriteLine(vbTab & "client port - matching server port")
		Console.WriteLine(vbTab & "directory   - directory containing templates to match")
		Console.WriteLine()
		Console.WriteLine("example: 192.168.0.1 25452 c:\templates Admin Admin")
		Console.WriteLine()
		Return 1
	End Function

	Private Const SendThreadCount As Integer = 24
	Private Const RecieveThreadCount As Integer = 12

	Shared Function Main(ByVal args() As String) As Integer
		TutorialUtils.PrintTutorialHeader(args)
		If args.Length < 5 Then
			Return Usage()
		End If

		Try
			Dim serverAddress As String = args(0)
			Dim clientPort As Integer = Integer.Parse(args(1))
			Dim directory As String = args(2)
			Dim userName As String = args(3)
			Dim password As String = args(4)

			Dim dbSize As Integer = GetDbSize(serverAddress, 80, userName, password)
			Dim templates()() As Byte = GetTemplates(directory)

			Dim tasks As New MatchingTaskCollection()
			For i As Integer = 0 To templates.Length - 1
				tasks.Enqueue(New MatchingTask(templates(i)))
			Next i
			Dim taskCount As Integer = tasks.Count

			Dim stopWatch As New Stopwatch()
			stopWatch.Start()
			SendRecieveTasks(serverAddress, clientPort, tasks, SendThreadCount, RecieveThreadCount)
			stopWatch.Stop()

			Dim speed As Double = dbSize * taskCount / stopWatch.Elapsed.TotalSeconds

			Dim formatInfo As NumberFormatInfo = CType(NumberFormatInfo.CurrentInfo.Clone(), NumberFormatInfo)
			formatInfo.NumberGroupSeparator = " "
			Console.WriteLine("Speed: " & vbTab + vbTab & "{0} templates per second", speed.ToString("N0", formatInfo))
			Console.WriteLine("Elapsed time:" & vbTab & " {0:f2} seconds", stopWatch.Elapsed.TotalSeconds)
			Console.WriteLine("DBSize: " & vbTab & "{0}", dbSize)
			Console.WriteLine("Sent template count: {0}", taskCount)
			Return 0
		Catch ex As Exception
			Return TutorialUtils.PrintException(ex)
		End Try
	End Function

	Private Shared Sub DoSendWork()
		Dim comm As Communication = Nothing
		Try
			comm = New Communication(_serverAddress, _clientPort)
			Do
				Dim task As MatchingTask = _inputMatchingTasks.Dequeue()
				If task Is Nothing Then
					Return
				End If
				Dim taskId As Integer = SendMatchingTask(comm, task, ClusterTaskMode.Normal)
				task.TaskId = taskId
				If taskId <> -1 Then
					_sentTasks.Enqueue(task)
				End If
			Loop
		Catch ex As Exception
			SyncLock _exceptions
				_exceptions.Add(ex)
			End SyncLock
		Finally
			Interlocked.Decrement(_producersWorking)
			If comm IsNot Nothing Then
				comm.Close()
			End If
		End Try
	End Sub

	Private Shared Sub DoReceiveWork()
		Dim comm As Communication = Nothing
		Try
			comm = New Communication(_serverAddress, _clientPort)
			Do
				Dim task As MatchingTask = _sentTasks.Dequeue()
				If task IsNot Nothing Then
					WaitForMatchingTask(comm, task.TaskId)
				Else
					If _producersWorking = 0 Then
						Return
					End If
					Thread.Sleep(100)
				End If
			Loop
		Catch ex As Exception
			SyncLock _exceptions
				_exceptions.Add(ex)
			End SyncLock
		Finally
			If comm IsNot Nothing Then
				comm.Close()
			End If
		End Try
	End Sub

	Shared _exceptions As New List(Of Exception)
	Shared _sentTasks As New MatchingTaskCollection()
	Shared _producersWorking As Integer
	Shared _serverAddress As String
	Shared _clientPort As Integer
	Shared _inputMatchingTasks As MatchingTaskCollection

	Private Shared Sub SendRecieveTasks(ByVal serverAddress As String, ByVal clientPort As Integer, ByVal inputMatchingTasks As MatchingTaskCollection, ByVal sendThreadCount As Integer, ByVal recieveThreadCount As Integer)
		_exceptions = New List(Of Exception)()
		_sentTasks = New MatchingTaskCollection()
		_producersWorking = sendThreadCount
		_serverAddress = serverAddress
		_clientPort = clientPort
		_inputMatchingTasks = _inputMatchingTasks

		Dim workerThreads As New List(Of Thread)()

		For i As Integer = 0 To sendThreadCount - 1
			Dim workerThread As New Thread(New ThreadStart(AddressOf DoSendWork))
			workerThread.Start()
			workerThreads.Add(workerThread)
		Next i

		For i As Integer = 0 To recieveThreadCount - 1
			Dim workerThread As New Thread(New ThreadStart(AddressOf DoReceiveWork))
			workerThread.Start()
			workerThreads.Add(workerThread)
		Next i

		For Each workerThread As Thread In workerThreads
			workerThread.Join()
		Next workerThread

		If _exceptions.Count > 0 Then
			Throw New AggregateException(_exceptions.ToArray())
		End If
	End Sub

	Private Shared Function SendMatchingTask(ByVal comm As Communication, ByVal task As MatchingTask, ByVal clusterTaskMode As ClusterTaskMode) As Integer
		Dim received As ClientPacketReceived = Nothing
		Dim clientPacket As ClientPacket = Nothing
		Try
			clientPacket = clientPacket.CreateTask(clusterTaskMode, task.Template, " ", Nothing, 0)
			Dim res As ClusterStatusCode = comm.SendReceivePacket(clientPacket, received)
			CheckClusterStatusCode(res)
			Dim taskId As Integer
			received.GetTaskID(taskId)
			task.TaskId = taskId
			Return taskId
		Finally
			If clientPacket IsNot Nothing Then
				clientPacket.DestroyPacket()
			End If
			If received IsNot Nothing Then
				received.DestroyPacket()
			End If
		End Try
	End Function

	Private Shared Sub WaitForMatchingTask(ByVal comm As Communication, ByVal taskId As Integer)
		Dim progress As Integer
		Do
			Dim progressRequest As ClientPacket = ClientPacket.CreateProgressRequest(taskId)
			Dim progressRequestReceived As ClientPacketReceived = Nothing
			Dim status As ClusterStatusCode = comm.SendReceivePacket(progressRequest, progressRequestReceived)
			CheckClusterStatusCode(status)
			Dim count As Integer
			status = progressRequestReceived.GetTaskProgress(progress, count)
			CheckClusterStatusCode(status)

			progressRequestReceived.DestroyPacket()
			progressRequest.DestroyPacket()
			If progress <> 100 Then
				Thread.Sleep(100)
			End If
		Loop While progress <> 100

		Dim deleteRequest As ClientPacket = ClientPacket.CreateResultDelete(taskId)
		comm.SendPacket(deleteRequest)
		deleteRequest.DestroyPacket()
	End Sub

	Private Shared Function GetTemplates(ByVal directory As String) As Byte()()
		If String.IsNullOrEmpty(directory) OrElse (Not System.IO.Directory.Exists(directory)) Then
			Throw New ArgumentException("Directory doesn't exists")
		End If

		Dim files() As String = System.IO.Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
		Dim templates(files.Length - 1)() As Byte
		For i As Integer = 0 To files.Length - 1
			templates(i) = File.ReadAllBytes(files(i))
		Next i
		Return templates
	End Function

	Private Shared Sub CheckClusterStatusCode(ByVal statusCode As ClusterStatusCode)
		If statusCode <> ClusterStatusCode.OK Then
			Throw New Exception(String.Format("Cluster error: {0}", statusCode))
		End If
	End Sub

	Private Shared Function GetDbSize(ByVal serverAddress As String, ByVal port As Integer, ByVal username As String, ByVal password As String) As Integer
		If (Not serverAddress.StartsWith("http://")) Then
			serverAddress = "http://" & serverAddress
		End If
		If serverAddress.EndsWith("/") Then
			serverAddress = serverAddress.Substring(0, serverAddress.Length - 1)
		End If
		Dim uriString As String = Uri.EscapeUriString(String.Format("{0}:{1}/rcontrol.php?a=getDatabaseSize", serverAddress, port))
		Dim request As WebRequest = WebRequest.Create(uriString)
		If String.IsNullOrEmpty(username) Then
			request.Credentials = CredentialCache.DefaultCredentials
		Else
			request.Credentials = New NetworkCredential(username, password)
		End If
		request.Method = "POST"
		request.ContentType = "application/x-www-form-urlencoded"
		request.Timeout = 1000 * 60 * 180
		Dim resp As WebResponse = request.GetResponse()
		If resp IsNot Nothing Then
			Dim stream As Stream = resp.GetResponseStream()
			If stream IsNot Nothing Then
				Using reader As New StreamReader(stream)
					Dim value As String = reader.ReadLine()
					If value IsNot Nothing Then
						Return Integer.Parse(value)
					End If
				End Using
			End If
		End If
		Throw New Exception("Failed to get Accelerator database size")
	End Function

	Private Class AggregateException
		Inherits Exception
		Public Sub New(ByVal ParamArray exceptions() As Exception)
			_exceptions = exceptions
		End Sub

		Private ReadOnly _exceptions() As Exception
		Public ReadOnly Property Exceptions() As Exception()
			Get
				Return _exceptions
			End Get
		End Property

		Public Overrides Function ToString() As String
			If Exceptions Is Nothing OrElse Exceptions.Length > 0 Then
				Return MyBase.ToString()
			End If
			Dim result As New StringBuilder(String.Format("{0} exception(s) occurred:" & vbLf, Exceptions.Length))
			For Each ex As Exception In Exceptions
				result.Append(String.Format("{0}{1}{1}", ex.Message, Environment.NewLine))
			Next ex
			Return result.ToString()
		End Function
	End Class

	Private Class MatchingTask
		Private _template() As Byte
		Private _taskId As Integer

		Public Sub New(ByVal template() As Byte)
			Me.Template = template
			TaskId = -1
		End Sub

		Public Property TaskId() As Integer
			Get
				Return _taskId
			End Get
			Set(ByVal value As Integer)
				_taskId = value
			End Set
		End Property

		Public Property Template() As Byte()
			Get
				Return _template
			End Get
			Private Set(ByVal value As Byte())
				_template = value
			End Set
		End Property
	End Class

	Private Class MatchingTaskCollection
		Inherits Queue(Of MatchingTask)
		Public Shadows Function Dequeue() As MatchingTask
			SyncLock Me
				If Count = 0 Then
					Return Nothing
				End If
				Return MyBase.Dequeue()
			End SyncLock
		End Function

		Public Shadows Sub Enqueue(ByVal task As MatchingTask)
			SyncLock Me
				MyBase.Enqueue(task)
			End SyncLock
		End Sub
	End Class
End Class
