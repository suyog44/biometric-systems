Imports System
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.IO

Partial Public Class VerifyVoice
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject1 As NSubject
	Private _subject2 As NSubject

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

	Private Function OpenTemplateOrFile(<System.Runtime.InteropServices.Out()> ByRef subject As NSubject) As String
		subject = Nothing
		lblMsg.Text = String.Empty
		Dim fileName As String = String.Empty

		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Title = "Open voice template or audio file"
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then ' load template
			fileName = OpenFileDialog.FileName

			' Check if given file is a template
			Dim fileData = New NBuffer(File.ReadAllBytes(OpenFileDialog.FileName))
			Try
				NTemplate.Check(fileData)
				subject = New NSubject()
				subject.SetTemplateBuffer(fileData)
				EnableVerifyButton()
			Catch
			End Try

			' If file is not a template, try to load it as audio file
			If subject Is Nothing Then
				' Create voice object
				Dim voice = New NVoice With {.FileName = fileName}
				subject = New NSubject()
				subject.Voices.Add(voice)

				' Extract a template from the subject
				_biometricClient.BeginCreateTemplate(subject, AddressOf OnExtractCompleted, subject)
			End If
		End If
		Return fileName
	End Function

	Private Sub OnExtractCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnExtractCompleted), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndCreateTemplate(r)
				If status <> NBiometricStatus.Ok Then
					MessageBox.Show(String.Format("The template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			Finally
				EnableVerifyButton()
			End Try
		End If
	End Sub

	Private Sub EnableVerifyButton()
		btnVerify.Enabled = IsSubjectValid(_subject1) AndAlso IsSubjectValid(_subject2)
	End Sub

	Private Function IsSubjectValid(ByVal subject As NSubject) As Boolean
		Return subject IsNot Nothing AndAlso (subject.Status = NBiometricStatus.Ok OrElse subject.Status = NBiometricStatus.MatchNotFound OrElse subject.Status = NBiometricStatus.None AndAlso subject.GetTemplateBuffer() IsNot Nothing)
	End Function

	Private Sub OnVerifyCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnVerifyCompleted), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndVerify(r)
				Dim verificationStatus = String.Format("Verification status: {0}", status)
				If status = NBiometricStatus.Ok Then
					'get matching score
					Dim score As Integer = _subject1.MatchingResults(0).Score
					Dim msg As String = String.Format("Score of matched templates: {0}", score)
					lblMsg.Text = msg
					MessageBox.Show(String.Format("{0}{1}{2}", verificationStatus, Environment.NewLine, msg))
				Else
					lblMsg.Text = verificationStatus
					MessageBox.Show(verificationStatus)
				End If

			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub SetFar()
		_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked

		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(cbMatchingFAR.Text)
			cbMatchingFAR.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
			EnableVerifyButton()
		Catch
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
			cbMatchingFAR.Select()
		End Try
	End Sub

	Private Sub SetDefaultFar()
		cbMatchingFAR.SelectedIndex = 1
		btnDefault.Enabled = False
		SetFar()
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnVerifyClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
		If _subject1 IsNot Nothing AndAlso _subject2 IsNot Nothing Then
			_biometricClient.BeginVerify(_subject1, _subject2, AddressOf OnVerifyCompleted, Nothing)
		End If
		btnVerify.Enabled = False
	End Sub

	Private Sub BtnDefaultClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
		SetDefaultFar()
	End Sub

	Private Sub BtnOpen1Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen1.Click
		lblFirstTemplate.Text = OpenTemplateOrFile(_subject1)
	End Sub

	Private Sub BtnOpen2Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen2.Click
		lblSecondTemplate.Text = OpenTemplateOrFile(_subject2)
	End Sub

	Private Sub CbMatchingFAREnter(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFAR.Enter
		btnDefault.Enabled = True
	End Sub

	Private Sub VerifyVoiceLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		lblMsg.Text = String.Empty
		lblFirstTemplate.Text = String.Empty
		lblSecondTemplate.Text = String.Empty
		Try
			cbMatchingFAR.BeginUpdate()
			cbMatchingFAR.Items.Add(0.001.ToString("P1"))
			cbMatchingFAR.Items.Add(0.0001.ToString("P2"))
			cbMatchingFAR.Items.Add(0.00001.ToString("P3"))
		Finally
			cbMatchingFAR.EndUpdate()
		End Try
		cbMatchingFAR.SelectedIndex = 1
	End Sub

	Private Sub CbMatchingFARLeave(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFAR.Leave
		SetFar()
	End Sub

	Private Sub VerifyVoiceVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			chbUniquePhrases.Checked = True
			SetDefaultFar()
		End If
	End Sub

	Private Sub chbUniquePhrasesCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbUniquePhrases.CheckedChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked
		End If
	End Sub

#End Region
End Class
