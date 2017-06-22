Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Threading
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Public Delegate Sub Finished()
Public Delegate Sub ErrorOccured(ByVal ex As Exception)
Public Delegate Sub ProgressChanged()
Public Delegate Sub MatchingTasksCompleted(ByVal tasks() As NBiometricTask)

Public Class TaskSender
	Inherits UserControl
#Region "Public events"

	Public Event Finished As Finished
	Public Event ProgressChanged As ProgressChanged
	Public Event ExceptionOccured As ErrorOccured
	Public Event MatchingTasksCompleted As MatchingTasksCompleted

#End Region

#Region "Public constructor"

	Public Sub New(ByVal biometricClient As NBiometricClient, ByVal templateLoader As ITemplateLoader, ByVal operation As NBiometricOperations)
		BunchSize = 100
		If biometricClient Is Nothing Then
			Throw New ArgumentNullException("biometricClient")
		End If
		If templateLoader Is Nothing Then
			Throw New ArgumentNullException("templateLoader")
		End If

		Me.TemplateLoader = templateLoader
		biometricClient = biometricClient
		_operation = operation
		_worker = New BackgroundWorker With {.WorkerReportsProgress = True, .WorkerSupportsCancellation = True}
		AddHandler _worker.DoWork, AddressOf WorkerDoWork
		AddHandler _worker.ProgressChanged, AddressOf WorkerProgressChanged
		AddHandler _worker.RunWorkerCompleted, AddressOf RunWorkerCompleted
	End Sub

#End Region

#Region "Private fields"

	Private ReadOnly _worker As BackgroundWorker
	Private ReadOnly _operation As NBiometricOperations
	Private ReadOnly _exceptionLock As Object = New Object()
	Private _completedTasks As Queue(Of NBiometricTask)

	Private Const TasksAccumulate As Integer = 10

	Private _tasksReceived As Integer
	Private _maxActiveTaskCount As Integer = 1000
	Private _activeTasks As Long
	Private _stop As Boolean
	Private _templateLoader As ITemplateLoader
	Private _stopWatch As Stopwatch
	Dim _pendingTasks = New TaskQueue()
	Dim _loaderWorking As Long = 1

#End Region

#Region "Public properties"

	Private privateBunchSize As Integer
	Public Property BunchSize() As Integer
		Get
			Return privateBunchSize
		End Get
		Set(ByVal value As Integer)
			privateBunchSize = value
		End Set
	End Property

	Public Property TemplateLoader() As ITemplateLoader
		Get
			Return _templateLoader
		End Get
		Set(ByVal value As ITemplateLoader)
			If IsBusy Then
				Throw New InvalidOperationException("Task sender is busy")
			End If
			_templateLoader = value
		End Set
	End Property

	Private privateSuccessful As Boolean
	Public Property Successful() As Boolean
		Get
			Return privateSuccessful
		End Get
		Private Set(ByVal value As Boolean)
			privateSuccessful = value
		End Set
	End Property

	Private privateCanceled As Boolean
	Public Property Canceled() As Boolean
		Get
			Return privateCanceled
		End Get
		Private Set(ByVal value As Boolean)
			privateCanceled = value
		End Set
	End Property

	Public ReadOnly Property IsBusy() As Boolean
		Get
			If _worker Is Nothing Then
				Return False
			End If
			Return _worker.IsBusy
		End Get
	End Property

	Private privateSendOneBunchOnly As Boolean
	Public Property SendOneBunchOnly() As Boolean
		Get
			Return privateSendOneBunchOnly
		End Get
		Set(ByVal value As Boolean)
			privateSendOneBunchOnly = value
		End Set
	End Property

	Public ReadOnly Property PerformedTaskCount() As Integer
		Get
			Return _tasksReceived
		End Get
	End Property

	Public ReadOnly Property ElapsedTime() As TimeSpan
		Get
			If _stopWatch Is Nothing Then
				Return TimeSpan.Zero
			End If
			Return _stopWatch.Elapsed
		End Get
	End Property

	Private privateBiometricClient As NBiometricClient
	Public Property BiometricClient() As NBiometricClient
		Get
			Return privateBiometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateBiometricClient = value
		End Set
	End Property

	Private privateIsAccelerator As Boolean
	Public Property IsAccelerator() As Boolean
		Get
			Return privateIsAccelerator
		End Get
		Set(ByVal value As Boolean)
			privateIsAccelerator = value
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Sub Start()
		If IsBusy Then
			Throw New InvalidOperationException("Already started")
		End If
		_stop = False
		_tasksReceived = 0
		Successful = True
		Canceled = False
		_stopWatch = New Stopwatch()
		_maxActiveTaskCount = If(IsAccelerator, 1000, 100)
		SettingsAccesor.SetMatchingParameters(BiometricClient)
		_worker.RunWorkerAsync()
	End Sub

	Public Sub Cancel()
		Canceled = True
		BiometricClient.Cancel()
		_worker.CancelAsync()
		_stop = True
	End Sub

#End Region

#Region "Private methods"

	Private Sub RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
		RaiseEvent Finished()
	End Sub

	Private Sub WorkerProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
		RaiseEvent ProgressChanged()
	End Sub

	Private Sub OnOperationCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnOperationCompleted), r)
		Else
			Dim task As NBiometricTask
			Try
				task = BiometricClient.EndPerformTask(r)
			Catch ex As Exception
				FireExceptionOccuredEvent(ex)
				Successful = False
				Interlocked.Decrement(_activeTasks)
				Return
			End Try
			Dim status As NBiometricStatus = task.Status
			If task.Error IsNot Nothing Then
				SyncLock _exceptionLock
					FireExceptionOccuredEvent(task.Error)
				End SyncLock
			End If
			If status <> NBiometricStatus.Ok AndAlso status <> NBiometricStatus.MatchNotFound Then
				SyncLock _exceptionLock
					FireExceptionOccuredEvent(New Exception(status.ToString()))
				End SyncLock
			End If

			Interlocked.Add(_tasksReceived, task.Subjects.Count)
			_worker.ReportProgress(_tasksReceived)

			If MatchingTasksCompletedEvent IsNot Nothing Then
				SyncLock _completedTasks
					_completedTasks.Enqueue(task)
					If _completedTasks.Count > TasksAccumulate Then
						RaiseEvent MatchingTasksCompleted(_completedTasks.ToArray())
						_completedTasks.Clear()
					End If
				End SyncLock
			Else
				task.Dispose()
			End If
			Interlocked.Decrement(_activeTasks)
		End If
	End Sub

	Private Sub WorkerDoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
		_pendingTasks = New TaskQueue()
		_loaderWorking = 1
		_completedTasks = New Queue(Of NBiometricTask)()

		Try
			TemplateLoader.BeginLoad()
		Catch ex As Exception
			FireExceptionOccuredEvent(ex)
			Return
		End Try

		Dim loaderThread = New Thread(AddressOf TaskLoaderThread)

		loaderThread.Start()

		If SendOneBunchOnly Then
			loaderThread.Join()
		End If
		_stopWatch.Start()

		Try
			Do While Not _stop
				If _activeTasks > _maxActiveTaskCount Then
					Thread.Sleep(500)
					Continue Do
				End If

				Dim task As NBiometricTask
				task = _pendingTasks.Dequeue()
				If task Is Nothing Then
					If Interlocked.Read(_loaderWorking) = 0 Then
						Exit Do
					End If
					Thread.Sleep(100)
					Continue Do
				End If
				BiometricClient.BeginPerformTask(task, AddressOf OnOperationCompleted, Nothing)
				task.Dispose()
				Interlocked.Increment(_activeTasks)
			Loop
		Catch ex As Exception
			SyncLock _exceptionLock
				FireExceptionOccuredEvent(ex)
			End SyncLock
		End Try

		Do While Interlocked.Read(_activeTasks) > 0
			Thread.Sleep(100)
		Loop
		_stopWatch.Stop()

		If MatchingTasksCompletedEvent IsNot Nothing AndAlso _completedTasks.Count > 0 Then
			RaiseEvent MatchingTasksCompleted(_completedTasks.ToArray())
		End If

		Try
			TemplateLoader.EndLoad()
		Catch ex As Exception
			FireExceptionOccuredEvent(ex)
		End Try

		If (Not _worker.CancellationPending) Then
			Return
		End If

		Successful = False
	End Sub

	Private Sub TaskLoaderThread()
		Try
			Do While Not _stop
				If _pendingTasks.Count < 200 Then
					Dim subjects() As NSubject = Nothing
					If TemplateLoader.LoadNext(subjects, BunchSize) Then
						If _operation = NBiometricOperations.Identify Then
							For Each subject As NSubject In subjects
								Dim task = BiometricClient.CreateTask(_operation, subject)
								_pendingTasks.Enqueue(task)
								subject.Dispose()
							Next subject
						Else
							Dim task = BiometricClient.CreateTask(_operation, Nothing)
							For Each subject As NSubject In subjects
								task.Subjects.Add(subject)
								subject.Dispose()
							Next subject
							_pendingTasks.Enqueue(task)
						End If
					Else
						Exit Do
					End If
				Else
					If _stop Then
						Return
					End If
					Thread.Sleep(250)
				End If
				If SendOneBunchOnly Then
					Return
				End If
			Loop
		Catch ex As Exception
			SyncLock _exceptionLock
				FireExceptionOccuredEvent(ex)
			End SyncLock
		Finally
			Interlocked.Decrement(_loaderWorking)
		End Try
	End Sub

	Private Sub FireExceptionOccuredEvent(ByVal ex As Exception)
		Successful = False
		If ExceptionOccuredEvent IsNot Nothing Then
			ExceptionOccuredEvent.BeginInvoke(ex, Nothing, Nothing)
		End If
	End Sub

#End Region

End Class

Public Class TaskQueue
	Inherits Queue(Of NBiometricTask)
#Region "Private fields"

	Private ReadOnly _lockObject As Object = New Object()

#End Region

#Region "Public constructors"

	Public Sub New()
	End Sub

	Public Sub New(ByVal tasks As IEnumerable(Of NBiometricTask))
		MyBase.New(tasks)
	End Sub

#End Region

#Region "Public methods"

	Public Shadows Function Dequeue() As NBiometricTask
		SyncLock _lockObject
			Return If(Count = 0, Nothing, MyBase.Dequeue())
		End SyncLock
	End Function

	Public Shadows Sub Enqueue(ByVal task As NBiometricTask)
		SyncLock _lockObject
			MyBase.Enqueue(task)
		End SyncLock
	End Sub

#End Region
End Class
