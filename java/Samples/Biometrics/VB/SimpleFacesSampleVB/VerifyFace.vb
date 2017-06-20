Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui

Partial Public Class VerifyFace
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

	Private Function OpenImageTemplate(ByVal faceView As NFaceView, <System.Runtime.InteropServices.Out()> ByRef subject As NSubject) As String
		subject = Nothing
		faceView.Face = Nothing
		msgLabel.Text = String.Empty
		Dim fileLocation As String = String.Empty

		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Title = "Open Template"
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then ' load template
			fileLocation = OpenFileDialog.FileName

			' Check if given file is a template
			Try
				subject = NSubject.FromFile(OpenFileDialog.FileName)
				EnableVerifyButton()
			Catch
			End Try

			' If file is not a template, try to load it as image
			If subject Is Nothing Then
				' Create a face object
				Dim face As New NFace() With {.FileName = fileLocation}
				faceView.Face = face
				subject = New NSubject()
				subject.Faces.Add(face)

				' Extract a template from the subject
				_biometricClient.BeginCreateTemplate(subject, AddressOf OnCreationCompleted, Nothing)
			End If
		End If
		Return fileLocation
	End Function

	Private Sub OnCreationCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnCreationCompleted), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndCreateTemplate(r)
				If status <> NBiometricStatus.Ok Then
					MessageBox.Show(String.Format("The template was not extracted: {0}.", status), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
				EnableVerifyButton()
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub EnableVerifyButton()
		verifyButton.Enabled = IsSubjectValid(_subject1) AndAlso IsSubjectValid(_subject2)
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
					msgLabel.Text = msg
					MessageBox.Show(String.Format("{0}{1}{2}", verificationStatus, Environment.NewLine, msg))
				Else
					MessageBox.Show(verificationStatus)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub OpenImageButton1Click(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton1.Click
		templateLeftLabel.Text = String.Empty
		templateLeftLabel.Text = OpenImageTemplate(faceView1, _subject1)
	End Sub

	Private Sub OpenImageButton2Click(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton2.Click
		templateRightLabel.Text = String.Empty
		templateRightLabel.Text = OpenImageTemplate(faceView2, _subject2)
	End Sub

	Private Sub VerifyFaceLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		msgLabel.Text = String.Empty
		templateLeftLabel.Text = String.Empty
		templateRightLabel.Text = String.Empty
		Try
			matchingFarComboBox.BeginUpdate()
			matchingFarComboBox.Items.Add(0.001.ToString("P1"))
			matchingFarComboBox.Items.Add(0.0001.ToString("P2"))
			matchingFarComboBox.Items.Add(0.00001.ToString("P3"))
		Finally
			matchingFarComboBox.EndUpdate()
		End Try

		matchingFarComboBox.SelectedIndex = 1
	End Sub

	Private Sub MatchingFarComboBoxEnter(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Enter
		defaultButton.Enabled = True
	End Sub

	Private Sub DefaultButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles defaultButton.Click
		matchingFarComboBox.SelectedIndex = 1
		defaultButton.Enabled = False
	End Sub

	Private Sub ClearImagesButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles clearImagesButton.Click
		_subject1 = Nothing
		_subject2 = Nothing
		verifyButton.Enabled = False

		faceView1.Face = Nothing
		faceView2.Face = Nothing

		msgLabel.Text = String.Empty
		templateLeftLabel.Text = String.Empty
		templateRightLabel.Text = String.Empty
	End Sub

	Private Sub VerifyButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles verifyButton.Click
		If _subject1 IsNot Nothing AndAlso _subject2 IsNot Nothing Then
			_biometricClient.BeginVerify(_subject1, _subject2, AddressOf OnVerifyCompleted, Nothing)
		End If
		verifyButton.Enabled = False
	End Sub

	Private Sub MatchingFarComboBoxLeave(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Leave
		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(matchingFarComboBox.Text)
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
			EnableVerifyButton()
		Catch
			matchingFarComboBox.Select()
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

#End Region
End Class
