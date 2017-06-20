Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class DeduplicationPanel
	Inherits ControlBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _taskSender As TaskSender
	Private _resultsFile As String

#End Region

#Region "Public methods"

	Public Overrides Sub Cancel()
		If (Not IsBusy) Then
			Return
		End If

		_taskSender.Cancel()
		btnCancel.Enabled = False
		rtbStatus.AppendText(Constants.vbCrLf & "Canceling, please wait ..." & Constants.vbCrLf)
	End Sub

	Public Overrides ReadOnly Property IsBusy() As Boolean
		Get
			If _taskSender IsNot Nothing Then
				Return _taskSender.IsBusy
			End If
			Return False
		End Get
	End Property

	Public Overrides Function GetTitle() As String
		Return "Deduplication"
	End Function

#End Region

#Region "Private methods"

	Private Sub EnableControls(ByVal isIdle As Boolean)
		btnStart.Enabled = isIdle
		btnCancel.Enabled = Not isIdle
		gbProperties.Enabled = isIdle
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

	Private Sub WriteLogHeader()
		Dim line As String = "TemplateId, MatchedWith, Score, FingersScore, FingersScores, IrisesScore, IrisesScores"
		If Accelerator IsNot Nothing Then
			line &= ", FacesScore, FacesScores, VoicesScore, VoicesScores, PalmsScore, PalmsScores"
		End If
		line &= Constants.vbLf
		File.WriteAllText(_resultsFile, line)
	End Sub

	Private Sub MatchingTasksCompleted(ByVal tasks() As NBiometricTask)
		If tasks IsNot Nothing Then
			Try
				Dim text = New StringBuilder()
				For Each task As NBiometricTask In tasks
					For Each subject As NSubject In task.Subjects
						If subject.MatchingResults IsNot Nothing AndAlso subject.MatchingResults.Count > 0 Then
							For Each item As NMatchingResult In subject.MatchingResults
								Dim line = New StringBuilder()
								line.Append(String.Format("{0},{1},{2}", subject.Id, item.Id, item.Score))
								Using details = New NMatchingDetails(item.MatchingDetailsBuffer)
									line.Append(String.Format(",{0},", details.FingersScore))
									For Each t As NFMatchingDetails In details.Fingers
										line.Append(String.Format("{0};", t.Score))
									Next t

									line.Append(String.Format(",{0},", details.IrisesScore))
									For Each t As NEMatchingDetails In details.Irises
										line.Append(String.Format("{0};", t.Score))
									Next t

									line.Append(String.Format(",{0},", details.FacesScore))
									For Each t As NLMatchingDetails In details.Faces
										line.Append(String.Format("{0};", t.Score))
									Next t

									line.Append(String.Format(",{0},", details.VoicesScore))
									For Each t As NSMatchingDetails In details.Voices
										line.Append(String.Format("{0};", t.Score))
									Next t

									line.Append(String.Format(",{0},", details.PalmsScore))
									For Each t As NFMatchingDetails In details.Palms
										line.Append(String.Format("{0};", t.Score))
									Next t
								End Using
								text.AppendLine(line.ToString())
							Next item
						Else
							text.AppendLine(String.Format("{0},NoMatches", subject.Id))
						End If
					Next subject
					task.Dispose()
				Next task

				File.AppendAllText(_resultsFile, text.ToString())
			Catch ex As Exception
				AppendStatus(String.Format("{0}" & Constants.vbCrLf, ex), Color.DarkRed)
			End Try
		End If
	End Sub

	Private Sub TaskSenderExceptionOccured(ByVal ex As Exception)
		AppendStatus(String.Format("{0}" & Constants.vbCrLf, ex), Color.DarkRed)
	End Sub

	Private Sub TaskSenderFinished()
		EnableControls(True)

		lblRemaining.Text = String.Empty
		pbarProgress.Value = pbarProgress.Maximum

		If _taskSender.Successful Then
			rtbStatus.AppendText("Deduplication completed without errors")
			pbStatus.Image = My.Resources.ok
			pbStatus.Image = pbStatus.Image
		Else
			rtbStatus.AppendText(If(_taskSender.Canceled, "Deduplication canceled.", "There were errors during deduplication"))
			pbStatus.Image = My.Resources._error
			pbStatus.Image = pbStatus.Image
		End If
	End Sub

	Private Sub TaskSenderProgressChanged()
		Dim numberOfTasksCompleted = _taskSender.PerformedTaskCount
		Dim elapsed As TimeSpan = _taskSender.ElapsedTime
		Dim remaining As TimeSpan = TimeSpan.FromTicks(elapsed.Ticks / numberOfTasksCompleted * (pbarProgress.Maximum - numberOfTasksCompleted))
		If remaining.TotalSeconds < 0 Then
			remaining = TimeSpan.Zero
		End If
		lblRemaining.Text = String.Format("Estimated time remaining: {0:00}.{1:00}:{2:00}:{3:00}", remaining.Days, remaining.Hours, remaining.Minutes, remaining.Seconds)

		pbarProgress.Value = If(numberOfTasksCompleted > pbarProgress.Maximum, pbarProgress.Maximum, numberOfTasksCompleted)
		lblProgress.Text = String.Format("{0} / {1}", numberOfTasksCompleted, pbarProgress.Maximum)
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		Try
			If IsBusy Then
				Return
			End If

			rtbStatus.Text = String.Empty
			pbStatus.Image = Nothing
			lblProgress.Text = String.Empty
			lblRemaining.Text = String.Empty

			_resultsFile = tbLogFile.Text
			WriteLogHeader()

			pbarProgress.Value = 0
			pbarProgress.Maximum = GetTemplateCount()

			_taskSender.TemplateLoader = TemplateLoader
			_taskSender.IsAccelerator = Accelerator IsNot Nothing
			BiometricClient.MatchingWithDetails = True
			_taskSender.BiometricClient = BiometricClient
			_taskSender.Start()
			EnableControls(False)
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try
	End Sub

	Private Sub DeduplicationPanelLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
			_taskSender = New TaskSender(BiometricClient, TemplateLoader, NBiometricOperations.Identify)
			AddHandler _taskSender.ProgressChanged, AddressOf TaskSenderProgressChanged
			AddHandler _taskSender.Finished, AddressOf TaskSenderFinished
			AddHandler _taskSender.ExceptionOccured, AddressOf TaskSenderExceptionOccured
			AddHandler _taskSender.MatchingTasksCompleted, AddressOf MatchingTasksCompleted

			lblProgress.Text = String.Empty
			lblRemaining.Text = String.Empty
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try
	End Sub

	Private Sub BtnCancelClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
		Cancel()
	End Sub

	Private Sub BtnBrowseClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowse.Click
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			tbLogFile.Text = openFileDialog.FileName
		End If
	End Sub

#End Region
End Class
