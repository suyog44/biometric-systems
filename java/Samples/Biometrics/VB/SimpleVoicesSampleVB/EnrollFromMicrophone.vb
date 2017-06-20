Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Devices

Partial Public Class EnrollFromMicrophone
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _deviceManager As NDeviceManager
	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _voice As NVoice

#End Region

#Region "Public properties"

	Public Property BiometricClient() As NBiometricClient
		Get
			Return _biometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			_biometricClient = value
		End Set
	End Property

#End Region

#Region "Public methods"

	Public Sub StopCapturing()
		_biometricClient.Cancel()
	End Sub

#End Region

#Region "Private methods"

	Private Sub UpdateDeviceList()
		lbMicrophones.BeginUpdate()
		Try
			lbMicrophones.Items.Clear()
			For Each item As NDevice In _deviceManager.Devices
				lbMicrophones.Items.Add(item)
			Next item
		Finally
			lbMicrophones.EndUpdate()
		End Try
	End Sub

	Private Sub EnableControls(ByVal capturing As Boolean)
		Dim hasTemplate = (Not capturing) AndAlso _subject IsNot Nothing AndAlso _subject.Status = NBiometricStatus.Ok
		btnSaveTemplate.Enabled = hasTemplate
		btnSaveVoice.Enabled = hasTemplate
		btnStart.Enabled = Not capturing
		btnStop.Enabled = capturing
		btnRefresh.Enabled = Not capturing
		gbOptions.Enabled = Not capturing
		lbMicrophones.Enabled = Not capturing
		chbCaptureAutomatically.Enabled = Not capturing
		btnForce.Enabled = Not chbCaptureAutomatically.Checked AndAlso capturing
	End Sub

	Private Sub OnCapturingCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnCapturingCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			Dim status As NBiometricStatus = task.Status
			' If Stop button was pushed
			If status = NBiometricStatus.Canceled Then
				Return
			End If

			If status <> NBiometricStatus.Ok AndAlso status <> NBiometricStatus.SourceError AndAlso status <> NBiometricStatus.TooFewSamples Then
				' Since capture failed start capturing again
				_voice.SoundBuffer = Nothing
				_biometricClient.BeginPerformTask(task, AddressOf OnCapturingCompleted, Nothing)
			Else
				EnableControls(False)
			End If
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub EnrollFromMicrophoneLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If DesignMode Then
			Return
		End If
		voiceView.Voice = Nothing
		_deviceManager = _biometricClient.DeviceManager
		UpdateDeviceList()
	End Sub

	Private Sub BtnStartClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStart.Click
		If _biometricClient.VoiceCaptureDevice Is Nothing Then
			MessageBox.Show("Please select a microphone")
			Return
		End If

		If (Not chbExtractTextDependent.Checked) AndAlso (Not chbExtractTextIndependent.Checked) Then
			MessageBox.Show("No features configured to extract", "Invalid options", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		' Set voice capture from stream
		_voice = New NVoice With {.CaptureOptions = NBiometricCaptureOptions.Stream}
		If Not chbCaptureAutomatically.Checked Then
			_voice.CaptureOptions = _voice.CaptureOptions Or NBiometricCaptureOptions.Manual
		End If
		_subject = New NSubject()
		_subject.Voices.Add(_voice)
		voiceView.Voice = _voice

		Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Capture Or NBiometricOperations.Segment, _subject)
		_biometricClient.BeginPerformTask(task, AddressOf OnCapturingCompleted, Nothing)
		EnableControls(True)
	End Sub

	Private Sub BtnStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
		StopCapturing()
		EnableControls(False)
	End Sub

	Private Sub BtnRefreshClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefresh.Click
		UpdateDeviceList()
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If saveFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				File.WriteAllBytes(saveFileDialog.FileName, _subject.GetTemplateBuffer().ToArray())
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub BtnSaveVoiceClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveVoice.Click
		If saveVoiceFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				' Voice buffer is saved in Child attribute after segmentation
				Dim voice = TryCast(_voice.Objects(0).Child, NVoice)
				If voice Is Nothing Then
					Return
				End If
				File.WriteAllBytes(saveVoiceFileDialog.FileName, voice.SoundBuffer.Save().ToArray())
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub ChbExtractTextDependentCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbExtractTextDependent.CheckedChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.VoicesExtractTextDependentFeatures = chbExtractTextDependent.Checked
		End If
	End Sub

	Private Sub ChbExtractTextIndependentCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbExtractTextIndependent.CheckedChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.VoicesExtractTextIndependentFeatures = chbExtractTextIndependent.Checked
		End If
	End Sub

	Private Sub LbMicrophonesSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbMicrophones.SelectedIndexChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.VoiceCaptureDevice = TryCast(lbMicrophones.SelectedItem, NMicrophone)
		End If
	End Sub

	Private Sub EnrollFromMicrophoneVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			chbExtractTextIndependent.Checked = True
			chbExtractTextDependent.Checked = True
			nudPhraseId.Value = 0
			EnableControls(False)
		End If
	End Sub

	Private Sub BtnForceClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForce.Click
		_biometricClient.ForceStart()
		btnForce.Enabled = False
	End Sub

#End Region

End Class
