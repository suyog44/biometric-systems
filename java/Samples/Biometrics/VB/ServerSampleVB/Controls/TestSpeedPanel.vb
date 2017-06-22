Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Globalization
Imports Neurotec.Biometrics

Partial Public Class TestSpeedPanel
	Inherits ControlBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _taskSender As TaskSender

#End Region

#Region "Public overriden properties"

	Public Overrides Property Accelerator() As AcceleratorConnection
		Get
			Return MyBase.Accelerator
		End Get
		Set(ByVal value As AcceleratorConnection)
			MyBase.Accelerator = value
			If Visible Then
				Dim isAccelerator = value IsNot Nothing
				Dim supportsGetCount = (BiometricClient.RemoteConnections(0).Operations And NBiometricOperations.GetCount) = NBiometricOperations.GetCount
				lblTemplateInfo.Visible = Not (isAccelerator Or supportsGetCount)
				lblTemplatesOnAcc.Text = String.Format("Templates on {0}:", If(isAccelerator, "accelerator", If(supportsGetCount, "server", "server*")))
			End If
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Overrides Function GetTitle() As String
		Return "Test matching speed"
	End Function

	Public Overrides ReadOnly Property IsBusy() As Boolean
		Get
			If _taskSender IsNot Nothing Then
				Return _taskSender.IsBusy
			End If
			Return False
		End Get
	End Property

	Public Overrides Sub Cancel()
		If (Not IsBusy) Then
			Return
		End If

		_taskSender.Cancel()
		rtbStatus.Text = "Canceling ..." & Constants.vbCrLf

		btnCancel.Enabled = False
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		Dim isAccelerator = Accelerator IsNot Nothing
		Dim supportsGetCount = BiometricClient.RemoteConnections(0).Operations & NBiometricOperations.GetCount = NBiometricOperations.GetCount
		Try
			If IsBusy Then
				Return
			End If

			EnableControls(False)
			tbSpeed.Text = "N/A"
			tbTime.Text = "N/A"
			tbTaskCount.Text = "N/A"
			rtbStatus.Text = "Preparing ..."
			lblCount.Text = String.Empty
			pbarProgress.Value = 0
			Dim maxCount As Integer = Convert.ToInt32(nudMaxCount.Value)
			Dim loaderTemplateCount = GetTemplateCount()
			pbarProgress.Maximum = If(maxCount > loaderTemplateCount, loaderTemplateCount, maxCount)
			pbStatus.Image = Nothing

			' if speed is counted not on MegaMatcher Accelerator and server does not support get count operation 
			' servers DB size is assumed to be equal to probe template databases size
			Dim templateCount As Integer = If(isAccelerator, Accelerator.GetDbSize(), If(supportsGetCount, BiometricClient.GetCount(), loaderTemplateCount))
			tbDBSize.Text = templateCount.ToString(CultureInfo.InvariantCulture)

			_taskSender.BunchSize = maxCount
			_taskSender.SendOneBunchOnly = True
			_taskSender.TemplateLoader = TemplateLoader
			_taskSender.IsAccelerator = isAccelerator
			BiometricClient.MatchingWithDetails = False
			_taskSender.BiometricClient = BiometricClient
			_taskSender.TemplateLoader = TemplateLoader
			_taskSender.Start()
		Catch ex As Exception
			Utilities.ShowError(ex)
			rtbStatus.Text = ex.ToString()
			pbStatus.Image = My.Resources._error
			EnableControls(True)
		End Try
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		Cancel()
	End Sub

	Private Sub TestSpeedPanelLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
			_taskSender = New TaskSender(BiometricClient, TemplateLoader, NBiometricOperations.Identify)
			AddHandler _taskSender.ProgressChanged, AddressOf TaskSenderProgressChanged
			AddHandler _taskSender.Finished, AddressOf TaskSenderFinished
			AddHandler _taskSender.ExceptionOccured, AddressOf TaskSenderExceptionOccured
			lblCount.Text = String.Empty
			lblRemaining.Text = String.Empty
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try
	End Sub

#End Region

#Region "Private methods"

	Private Sub EnableControls(ByVal isIdle As Boolean)
		btnStart.Enabled = isIdle
		gbProperties.Enabled = isIdle
		btnCancel.Enabled = Not isIdle
	End Sub

	Private Sub TaskSenderProgressChanged()
		Dim templatesMatched = _taskSender.PerformedTaskCount
		If templatesMatched = 1 Then
			rtbStatus.Text = "Matching templates ..."
		End If

		Dim formatInfo = CType(NumberFormatInfo.CurrentInfo.Clone(), NumberFormatInfo)
		formatInfo.NumberGroupSeparator = " "
		tbTaskCount.Text = templatesMatched.ToString(CultureInfo.InvariantCulture)

		Dim dbSize As Integer = Convert.ToInt32(tbDBSize.Text)
		Dim timeElapsed As Double = _taskSender.ElapsedTime.TotalSeconds
		Dim speed As Double = dbSize * templatesMatched / timeElapsed

		tbSpeed.Text = speed.ToString("N2", formatInfo)
		tbTime.Text = timeElapsed.ToString("#.## s")

		Dim remaining As TimeSpan = TimeSpan.FromSeconds(timeElapsed / templatesMatched * (pbarProgress.Maximum - templatesMatched))
		If remaining.TotalSeconds < 0 Then
			remaining = TimeSpan.Zero
		End If
		lblRemaining.Text = String.Format("Estimated time remaining: {0:00}.{1:00}:{2:00}:{3:00}", remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds)

		pbarProgress.Value = templatesMatched
		lblCount.Text = String.Format("{0} / {1}", templatesMatched, pbarProgress.Maximum)
	End Sub

	Private Sub TaskSenderFinished()
		EnableControls(True)
		If _taskSender.Canceled Then
			rtbStatus.AppendText("Speed test canceled" & Constants.vbCrLf)
			pbStatus.Image = Nothing
			Return
		End If

		If _taskSender.Successful Then
			rtbStatus.Text = String.Format("Speed: {0} templates per second." & Constants.vbLf & "Total of {1} templates were sent and matched against {2} templates per {3} seconds.", tbSpeed.Text, tbTaskCount.Text, tbDBSize.Text, tbTime.Text)
			pbStatus.Image = My.Resources.ok
			Return
		End If

		If (Not _taskSender.Successful) Then
			rtbStatus.AppendText(Constants.vbCrLf & "Operation completed with errors" & Constants.vbCrLf)
			pbStatus.Image = My.Resources._error
		End If
		pbarProgress.Value = pbarProgress.Maximum
	End Sub

	Private Sub TaskSenderExceptionOccured(ByVal ex As Exception)
		AppendStatus(String.Format("{0}" & Constants.vbCrLf, ex), Color.DarkRed)
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
