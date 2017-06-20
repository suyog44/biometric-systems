Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports System.ComponentModel

Partial Public Class CaptureVoicePage
	Inherits Neurotec.Samples.PageBase
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		AddHandler rbFromFile.CheckedChanged, AddressOf RadioButtonCheckedChanged
		AddHandler rbMicrophone.CheckedChanged, AddressOf RadioButtonCheckedChanged
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _newSubject As NSubject
	Private _voice As NVoice
	Private _isIdle As New ManualResetEvent(True)
	Private _phrases As New List(Of Phrase)()

#End Region

#Region "Public methods"

	Public Overrides Sub OnNavigatedTo(ByVal ParamArray args() As Object)
		If args Is Nothing OrElse args.Length <> 2 Then
			Throw New ArgumentException("args")
		End If

		_subject = CType(args(0), NSubject)
		_biometricClient = CType(args(1), NBiometricClient)
		AddHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged

		_newSubject = New NSubject()
		_voice = New NVoice()
		_newSubject.Voices.Add(_voice)
		voiceView.Voice = _voice
		_phrases = New List(Of Phrase)(SettingsManager.Phrases)
		ListAllPhrases()
		lblHint.Visible = False

		OnVoiceCaptureDeviceChanged()
		EnableControls()

		MyBase.OnNavigatedTo(args)
	End Sub

	Public Overrides Sub OnNavigatingFrom()
		If IsBusy() Then
			LongActionDialog.ShowDialog(Me, "Finishing current action ...", AddressOf CancelAndWait)
		End If
		RemoveHandler _biometricClient.PropertyChanged, AddressOf OnBiometricClientPropertyChanged
		If _voice.Status = NBiometricStatus.Ok Then
			Dim voices = _newSubject.Voices.ToArray()
			_newSubject.Clear()
			For Each item In voices
				_subject.Voices.Add(item)
			Next item
		End If
		voiceView.Voice = Nothing
		_newSubject = Nothing
		_voice = Nothing
		_subject = Nothing
		_biometricClient = Nothing

		MyBase.OnNavigatingFrom()
	End Sub

#End Region

#Region "Private methods"

	Private Sub SetHint(ByVal backColor As Color, ByVal format As String, ByVal ParamArray args() As Object)
		lblHint.BackColor = backColor
		lblHint.Text = String.Format(format, args)
		lblHint.Visible = True
	End Sub

	Private Sub ListAllPhrases()
		Try
			cbPhrase.BeginUpdate()
			Dim selected As Object = cbPhrase.SelectedItem
			cbPhrase.Items.Clear()
			For Each item In _phrases
				cbPhrase.Items.Add(item)
			Next item
			cbPhrase.SelectedItem = selected
			If cbPhrase.Items.Count > 0 AndAlso cbPhrase.SelectedItem Is Nothing Then
				cbPhrase.SelectedIndex = 0
			End If
		Finally
			cbPhrase.EndUpdate()
		End Try
	End Sub

	Private Sub EnableControls()
		Dim isIdle As Boolean = Not IsBusy()
		Dim fromFile As Boolean = rbFromFile.Checked
		rbMicrophone.Enabled = _biometricClient.VoiceCaptureDevice IsNot Nothing AndAlso _biometricClient.VoiceCaptureDevice.IsAvailable AndAlso (_biometricClient.LocalOperations And NBiometricOperations.CreateTemplate) <> 0
		gbPhrase.Enabled = isIdle
		btnOpenFile.Enabled = fromFile AndAlso isIdle
		btnStop.Enabled = (Not fromFile) AndAlso Not isIdle
		btnStart.Enabled = (Not fromFile) AndAlso isIdle
		gbSource.Enabled = isIdle
		chnCaptureAutomatically.Visible = Not fromFile
		chnCaptureAutomatically.Enabled = isIdle
		btnForce.Visible = Not fromFile
		btnForce.Enabled = Not fromFile AndAlso Not chnCaptureAutomatically.Checked AndAlso Not isIdle

		Dim boldFinish As Boolean = isIdle AndAlso _voice IsNot Nothing AndAlso _voice.Status = NBiometricStatus.Ok
		btnFinish.Font = New Font(btnFinish.Font, If(boldFinish, FontStyle.Bold, FontStyle.Regular))

		busyIndicator.Visible = Not isIdle
	End Sub

	Private Sub SetIsBusy(ByVal value As Boolean)
		If value Then
			_isIdle.Reset()
		Else
			_isIdle.Set()
		End If
	End Sub

	Private Function IsBusy() As Boolean
		Return Not _isIdle.WaitOne(0)
	End Function

	Private Sub CancelAndWait()
		If IsBusy() Then
			_biometricClient.Cancel()
			_isIdle.WaitOne()
		End If
	End Sub

#End Region

#Region "Private events"

	Private Sub OnVoiceCaptureDeviceChanged()
		Dim device = _biometricClient.VoiceCaptureDevice
		If device Is Nothing OrElse Not device.IsAvailable Then
			rbFromFile.Checked = True
			rbMicrophone.Text = "Microphone (Not connected)"
		Else
			rbMicrophone.Text = String.Format("Microphone ({0})", device.DisplayName)
		End If
		EnableControls()
	End Sub

	Private Sub OnBiometricClientPropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
		If e.PropertyName = "VoiceCaptureDevice" Then
			BeginInvoke(New MethodInvoker(AddressOf InvokeOnVoiceCaptureDeviceChanged))
		End If
	End Sub

	Private Sub InvokeOnVoiceCaptureDeviceChanged()
		If IsPageShown Then
			OnVoiceCaptureDeviceChanged()
		End If
	End Sub

	Private Sub BtnEditClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
		Using form As New EditPhrasesForm()
			form.Phrases = _phrases
			form.ShowDialog(Me)
			SettingsManager.Phrases = _phrases
			ListAllPhrases()
		End Using
	End Sub

	Private Sub BtnOpenFileClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenFile.Click
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Dim phrase As Phrase = TryCast(cbPhrase.SelectedItem, Phrase)
			_voice.SoundBuffer = Nothing
			_voice.FileName = openFileDialog.FileName
			_voice.PhraseId = If(phrase IsNot Nothing, phrase.Id, 0)
			_voice.CaptureOptions = NBiometricCaptureOptions.None
			Dim biometricTask = _biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.CreateTemplate, _newSubject)
			SetIsBusy(True)
			_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCreateTemplateCompleted, Nothing)
			SetHint(Color.Orange, "Extracting record. Please wait ...")
			EnableControls()
		End If
	End Sub

	Private Sub OnCreateTemplateCompleted(ByVal r As IAsyncResult)
		Dim status As NBiometricStatus = NBiometricStatus.InternalError
		Try
			Dim biometricTask = _biometricClient.EndPerformTask(r)
			status = biometricTask.Status
			If biometricTask.Error IsNot Nothing Then
				Utilities.ShowError(biometricTask.Error)
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		End Try

		SetIsBusy(False)
		BeginInvoke(New Action(Of NBiometricStatus)(AddressOf UpdateStatus), status)

	End Sub

	Private Sub UpdateStatus(ByVal status As NBiometricStatus)
		If IsPageShown Then
			If status = NBiometricStatus.Ok Then
				SetHint(Color.Green, "Extraction successful")
			Else
				SetHint(Color.Red, "Extraction failed: {0}", status)
			End If
			EnableControls()
		End If
	End Sub

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		Dim phrase As Phrase = TryCast(cbPhrase.SelectedItem, Phrase)
		_voice.FileName = Nothing
		_voice.SoundBuffer = Nothing
		_voice.PhraseId = If(phrase IsNot Nothing, phrase.Id, 0)
		_voice.CaptureOptions = If(chnCaptureAutomatically.Checked, NBiometricCaptureOptions.None, NBiometricCaptureOptions.Manual)
		Dim biometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.Segment Or NBiometricOperations.CreateTemplate, _newSubject)
		SetIsBusy(True)
		_biometricClient.BeginPerformTask(biometricTask, AddressOf OnCreateTemplateCompleted, Nothing)
		SetHint(Color.Orange, "Extracting record. Please say phrase ...")
		EnableControls()
	End Sub

	Private Sub BtnStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
		btnStop.Enabled = False
		_biometricClient.Force()
	End Sub

	Private Sub RadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		If (CType(sender, RadioButton)).Checked Then
			ToggleRadioButtons()
		End If
	End Sub

	Private Sub ToggleRadioButtons()
		EnableControls()
	End Sub

	Private Sub BtnFinishClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
		PageController.NavigateToStartPage()
	End Sub

	Private Sub BtnForceClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
		_biometricClient.ForceStart()
		btnForce.Enabled = False
	End Sub

#End Region

End Class

Public Class Phrase
#Region "Public fields"

	Public ReadOnly Id As Integer
	Public ReadOnly [String] As String

#End Region

#Region "Public constructor"

	Public Sub New(ByVal id As Integer, ByVal phrase As String)
		Me.Id = id
		[String] = phrase
	End Sub

#End Region

#Region "public methods"

	Public Overrides Function ToString() As String
		Return [String]
	End Function

	Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
		Dim ph As Phrase = TryCast(obj, Phrase)
		If ph IsNot Nothing Then
			Return String.Equals([String], ph.String) AndAlso Id = ph.Id
		End If
		Return MyBase.Equals(obj)
	End Function

	Public Overrides Function GetHashCode() As Integer
		Return MyBase.GetHashCode()
	End Function

#End Region
End Class
