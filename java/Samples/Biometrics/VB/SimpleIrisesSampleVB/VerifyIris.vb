Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui

Partial Public Class VerifyIris
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

	Private Function OpenImageTemplate(ByVal irisView As NIrisView, <System.Runtime.InteropServices.Out()> ByRef subject As NSubject) As String
		subject = Nothing
		irisView.Iris = Nothing
		lblMsg.Text = String.Empty
		Dim fileName As String = String.Empty

		OpenFileDialog.FileName = Nothing
		OpenFileDialog.Title = "Open Template"
		If OpenFileDialog.ShowDialog() = DialogResult.OK Then ' load template
			fileName = OpenFileDialog.FileName

			' Check if given file is a template
			Try
				subject = NSubject.FromFile(OpenFileDialog.FileName)
				EnableVerifyButton()
			Catch
			End Try

			' If file is not a template, try to load it as image
			If subject Is Nothing Then
				' Create an iris object
				Dim iris As New NIris() With {.FileName = fileName}
				irisView.Iris = iris
				subject = New NSubject()
				subject.Irises.Add(iris)

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
					MessageBox.Show(verificationStatus)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub SetFar()
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

	Private Sub BtnOpenImage1Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImage1.Click
		lblTemplateLeft.Text = String.Empty
		lblTemplateLeft.Text = OpenImageTemplate(irisView1, _subject1)
	End Sub

	Private Sub BtnDefaultClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDefault.Click
		SetDefaultFar()
	End Sub

	Private Sub BtnOpenImage2Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImage2.Click
		lblTemplateRight.Text = String.Empty
		lblTemplateRight.Text = OpenImageTemplate(irisView2, _subject2)
	End Sub

	Private Sub BtnClearClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
		_subject1 = Nothing
		_subject2 = Nothing
		btnVerify.Enabled = False

		irisView1.Iris = Nothing
		irisView2.Iris = Nothing

		lblMsg.Text = String.Empty
		lblTemplateLeft.Text = String.Empty
		lblTemplateRight.Text = String.Empty
	End Sub

	Private Sub BtnVerifyClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnVerify.Click
		If _subject1 IsNot Nothing AndAlso _subject2 IsNot Nothing Then
			_biometricClient.BeginVerify(_subject1, _subject2, AddressOf OnVerifyCompleted, Nothing)
		End If
		btnVerify.Enabled = False
	End Sub

	Private Sub VerifyIrisLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		lblMsg.Text = String.Empty
		lblTemplateLeft.Text = String.Empty
		lblTemplateRight.Text = String.Empty
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

	Private Sub CbMatchingFAREnter(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFAR.Enter
		btnDefault.Enabled = True
	End Sub

	Private Sub CbMatchingFARLeave(ByVal sender As Object, ByVal e As EventArgs) Handles cbMatchingFAR.Leave
		SetFar()
	End Sub

	Private Sub VerifyIrisVisibleChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Visible AndAlso _biometricClient IsNot Nothing Then
			SetDefaultFar()
		End If
	End Sub

#End Region
End Class
