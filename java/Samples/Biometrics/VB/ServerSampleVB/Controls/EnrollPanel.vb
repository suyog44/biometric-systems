Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Globalization
Imports Neurotec.Biometrics

Partial Public Class EnrollPanel
	Inherits ControlBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _taskSender As TaskSender

#End Region

#Region "Public methods"

	Public Overrides Function GetTitle() As String
		Return "Enroll Templates"
	End Function

	Public Overrides Sub Cancel()
		If (Not IsBusy) Then
			Return
		End If

		_taskSender.Cancel()
		btnCancel.Enabled = False
		AppendStatus(Constants.vbCrLf & "Canceling, please wait ..." & Constants.vbCrLf)
	End Sub

	Public Overrides ReadOnly Property IsBusy() As Boolean
		Get
			If _taskSender IsNot Nothing Then
				Return _taskSender.IsBusy
			End If
			Return False
		End Get
	End Property

#End Region

#Region "Private form events"
	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		Try
			If IsBusy Then
				Return
			End If

			pbarProgress.Value = 0
			Dim count As Integer = GetTemplateCount()
			pbarProgress.Maximum = count
			tbTaskCount.Text = count.ToString(CultureInfo.InvariantCulture)
			rtbStatus.Text = String.Empty
			pbStatus.Image = Nothing
			tbTime.Text = "N/A"

			_taskSender.TemplateLoader = TemplateLoader
			_taskSender.IsAccelerator = Accelerator IsNot Nothing
			_taskSender.BiometricClient = BiometricClient
			_taskSender.BunchSize = CInt(Fix(nudBunchSize.Value))
			_taskSender.Start()

			AppendStatus(String.Format("Enrolling from: {0}" & Constants.vbCrLf, TemplateLoader))
			EnableControls(False)
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try
	End Sub

	Private Sub EnrollPanelLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		_taskSender = New TaskSender(BiometricClient, TemplateLoader, NBiometricOperations.Enroll)
		AddHandler _taskSender.ProgressChanged, AddressOf TaskSenderProgressChanged
		AddHandler _taskSender.Finished, AddressOf TaskSenderFinished
		AddHandler _taskSender.ExceptionOccured, AddressOf TaskSenderExceptionOccured
		lblProgress.Text = String.Empty
		lblRemaining.Text = String.Empty
	End Sub

	Private Sub TaskSenderProgressChanged()
		Dim templatesEnrolled = _taskSender.PerformedTaskCount
		If templatesEnrolled = 1 Then
			rtbStatus.Text = String.Format("Enrolling from: {0}" & Constants.vbCrLf, TemplateLoader)
		End If
		pbarProgress.Value = templatesEnrolled

		Dim elapsed As TimeSpan = _taskSender.ElapsedTime
		lblProgress.Text = String.Format("{0} / {1}", templatesEnrolled, tbTaskCount.Text)
		tbTime.Text = elapsed.TotalSeconds.ToString("#.## s")

		Dim remaining As TimeSpan = TimeSpan.FromTicks(elapsed.Ticks / templatesEnrolled * (pbarProgress.Maximum - templatesEnrolled))
		If remaining.TotalSeconds < 0 Then
			remaining = TimeSpan.Zero
		End If
		lblRemaining.Text = String.Format("Estimated time remaining: {0:00}.{1:00}:{2:00}:{3:00}", remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds)

	End Sub

	Private Sub TaskSenderFinished()
		EnableControls(True)
		tbTime.Text = String.Format("{0:00}:{1:00}:{2:00}", _taskSender.ElapsedTime.Hours, _taskSender.ElapsedTime.Minutes, _taskSender.ElapsedTime.Seconds)
		lblRemaining.Text = String.Empty
		lblProgress.Text = String.Empty
		If _taskSender.Canceled Then
			AppendStatus(Constants.vbCrLf & "Enrollment canceled")
			pbStatus.Image = My.Resources._error
			Return
		End If

		If _taskSender.Successful Then
			AppendStatus(Constants.vbCrLf & "All templates enrolled successfully")
			pbStatus.Image = My.Resources.ok
		Else
			AppendStatus(Constants.vbCrLf & "There were errors during enrollment", Color.DarkRed)
			pbStatus.Image = My.Resources._error
		End If
	End Sub

	Private Sub TaskSenderExceptionOccured(ByVal ex As Exception)
		AppendStatus(String.Format("{0}" & Constants.vbCrLf, ex), Color.DarkRed)
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		Cancel()
	End Sub

#End Region

#Region "Private methods"

	Private Sub EnableControls(ByVal isIdle As Boolean)
		btnStart.Enabled = isIdle
		btnCancel.Enabled = Not isIdle
		nudBunchSize.Enabled = isIdle
	End Sub

	Private Sub AppendStatus(ByVal msg As String)
		AppendStatus(msg, Color.Black)
	End Sub

	Private Delegate Sub AppendStatusDelegate(ByVal msg As String, ByVal color As Color)
	Private Sub AppendStatus(ByVal msg As String, ByVal color As Color)
		If InvokeRequired Then
			Dim del As AppendStatusDelegate = AddressOf AppendStatus
			BeginInvoke(del, msg, color)
			Return
		End If

		rtbStatus.SelectionStart = rtbStatus.TextLength
		rtbStatus.SelectionColor = color
		rtbStatus.AppendText(msg)
		rtbStatus.SelectionColor = rtbStatus.ForeColor
	End Sub

#End Region
End Class
