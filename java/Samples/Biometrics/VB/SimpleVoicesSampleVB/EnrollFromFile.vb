Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Partial Public Class EnrollFromFile
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

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

#Region "Private methods"

	Private Sub DisableControls()
		lblSoundFile.Text = String.Empty
		lblStatus.Text = String.Empty
		btnExtract.Enabled = False
		btnSaveTemplate.Enabled = False
		btnSaveVoice.Enabled = False
	End Sub

	Private Sub OnExtractionCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnExtractionCompleted), r)
		Else
			Dim task As NBiometricTask = _biometricClient.EndPerformTask(r)
			Dim status As NBiometricStatus = task.Status
			If status = NBiometricStatus.Ok Then
				btnSaveTemplate.Enabled = True
				btnSaveVoice.Enabled = True
				lblStatus.Text = "Template extracted"
			Else
				lblStatus.Text = String.Format("Extraction failed: {0}.", status)
			End If
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub EnrollFromFileLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		DisableControls()
	End Sub

	Private Sub BtnOpenClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
		_subject = Nothing
		_voice = Nothing

		DisableControls()

		If openFileDialog.ShowDialog() = DialogResult.OK Then
			' Create a subject with voice record
			_voice = New NVoice With {.FileName = openFileDialog.FileName}
			_subject = New NSubject()
			_subject.Voices.Add(_voice)

			lblSoundFile.Text = openFileDialog.FileName
			btnExtract.Enabled = True
		End If
	End Sub

	Private Sub BtnExtractClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnExtract.Click
		btnSaveVoice.Enabled = False
		btnSaveTemplate.Enabled = False
		lblStatus.Text = String.Empty

		If (Not chbExtractTextDependent.Checked) AndAlso (Not chbExtractTextIndependent.Checked) Then
			MessageBox.Show("No features configured to extract", "Invalid options", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Return
		End If

		Try
			_voice.PhraseId = Convert.ToInt32(nudPhraseId.Value)
			' Do voice extraction and segment voice from audio
			Dim task As NBiometricTask = _biometricClient.CreateTask(NBiometricOperations.Segment Or NBiometricOperations.CreateTemplate, _subject)
			_biometricClient.BeginPerformTask(task, AddressOf OnExtractionCompleted, Nothing)
		Catch ex As Exception
			Utils.ShowException(ex)
		End Try
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
				Dim voice As NVoice = _subject.Voices.Last()
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

	Private Sub EnrollFromFileVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			chbExtractTextIndependent.Checked = True
			chbExtractTextDependent.Checked = True
			nudPhraseId.Value = 0
		End If
	End Sub

#End Region
End Class
