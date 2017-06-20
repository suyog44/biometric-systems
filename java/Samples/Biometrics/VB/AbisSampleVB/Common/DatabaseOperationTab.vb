Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports System.Linq

Partial Public Class DababaseOperationTab
	Inherits Neurotec.Samples.TabPageContentBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		DoubleBuffered = True
		TabName = "DatabaseOperationTab"
	End Sub

#End Region

#Region "Public methods"

	Public Overrides Sub SetParams(ByVal ParamArray parameters() As Object)
		If parameters Is Nothing OrElse parameters.Length <> 2 Then
			Throw New ArgumentException("parameters")
		End If

		_subject = CType(parameters(0), NSubject)
		_operation = CType(parameters(1), NBiometricOperations)
		_biometricClient = TabController.Client

		MyBase.SetParams(parameters)
	End Sub

	Public Overrides Sub OnTabAdded()
		Select Case _operation
			Case NBiometricOperations.Enroll, NBiometricOperations.Verify, NBiometricOperations.Update
				TabName = String.Format("{0}: {1}", _operation, _subject.Id)
			Case NBiometricOperations.EnrollWithDuplicateCheck
				TabName = String.Format("Enroll: {0}", _subject.Id)
			Case NBiometricOperations.Identify
				TabName = "Identify"
			Case Else
				Throw New ArgumentException("parameters")
		End Select

		If TabName.Length > 30 Then
			TabName = TabName.Substring(0, 30) & "..."
		End If

		ShowError(Nothing)

		_biometricClient = TabController.Client
		Dim task As NBiometricTask = _biometricClient.CreateTask(_operation, _subject)
		_biometricClient.BeginPerformTask(task, AddressOf OnTaskCompleted, Nothing)

		MyBase.OnTabAdded()
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject
	Private _operation As NBiometricOperations
	Private _biometricClient As NBiometricClient

#End Region

#Region "Private methods"

	Private Sub OnTaskCompleted(ByVal result As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnTaskCompleted), result)
		Else
			Try
				HideProgressbar()

				Dim task As NBiometricTask = _biometricClient.EndPerformTask(result)
				Dim status As NBiometricStatus = task.Status
				lblStatus.Text = String.Format("{0}: {1}", _operation, status)
				lblStatus.BackColor = If(status = NBiometricStatus.Ok, Color.Green, Color.Red)

				If task.Error IsNot Nothing Then
					ShowError(task.Error)
				Else
					If _operation <> NBiometricOperations.Enroll And _operation <> NBiometricOperations.Update And (status = NBiometricStatus.Ok Or status = NBiometricStatus.DuplicateFound) Then
						If status = NBiometricStatus.Ok Or status = NBiometricStatus.DuplicateFound Then
							Dim details = _subject.MatchingResults.ToArray().Reverse()
							If details.Count() > 0 Then
								Dim showLink As Boolean = SettingsManager.ConnectionType <> ConnectionType.RemoteMatchingServer
								For Each item In details
									Dim view = New MatchingResultView() With {.MatchingThreshold = _biometricClient.MatchingThreshold, .LinkEnabled = showLink, .Result = item}
									AddHandler view.LinkActivated, AddressOf MatchingResultViewLinkActivated
									flowLayoutPanel.Controls.Add(view)
								Next item
							End If
						End If
					End If
				End If
			Catch ex As Exception
				lblStatus.Text = String.Format("{0}: {1}", _operation, "Error")
				lblStatus.BackColor = Color.Red
				ShowError(ex)
			End Try
		End If

	End Sub

	Private Sub ShowError(ByVal ex As Exception)
		Dim errorIndex As Integer = 1
		Dim whiteSpaceIndex As Integer = tableLayoutPanel.RowCount - 2
		If ex IsNot Nothing Then
			rtbError.Text = ex.ToString()
			tableLayoutPanel.RowStyles(errorIndex).SizeType = SizeType.Percent
			tableLayoutPanel.RowStyles(errorIndex).Height = 100
			tableLayoutPanel.RowStyles(whiteSpaceIndex).SizeType = SizeType.Absolute
			tableLayoutPanel.RowStyles(whiteSpaceIndex).Height = 0
		Else
			tableLayoutPanel.RowStyles(errorIndex).SizeType = SizeType.Absolute
			tableLayoutPanel.RowStyles(errorIndex).Height = 0
			tableLayoutPanel.RowStyles(whiteSpaceIndex).SizeType = SizeType.Percent
			tableLayoutPanel.RowStyles(whiteSpaceIndex).Height = 100
		End If
	End Sub

	Private Sub HideProgressbar()
		Dim index As Integer = tableLayoutPanel.RowCount - 1
		tableLayoutPanel.RowStyles(index).SizeType = SizeType.Absolute
		tableLayoutPanel.RowStyles(index).Height = 0
	End Sub

	Private Sub MatchingResultViewLinkActivated(ByVal sender As Object, ByVal e As EventArgs)
		Dim view As MatchingResultView = CType(sender, MatchingResultView)
		TabController.ShowTab(GetType(MatchingResultTab), True, True, _subject, New NSubject With {.Id = view.Result.Id})
	End Sub

#End Region
End Class
