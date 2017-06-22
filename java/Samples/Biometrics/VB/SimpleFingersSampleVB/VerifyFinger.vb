Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui

Partial Public Class VerifyFinger
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

	Private Function OpenImageTemplate(ByVal fingerView As NFingerView, <System.Runtime.InteropServices.Out()> ByRef subject As NSubject) As String
		subject = Nothing
		fingerView.Finger = Nothing
		msgLabel.Text = String.Empty
		ResetMatedMinutiaeOnViews()
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

			' If file is a template - return, otherwise assume that the file is an image and try to extract it
			If subject IsNot Nothing AndAlso subject.Fingers.Count > 0 Then
				fingerView.Finger = subject.Fingers(0)

				Return fileLocation
			End If

			' Create a finger object
			Dim finger = New NFinger With {.FileName = fileLocation}
			subject = New NSubject()
			subject.Fingers.Add(finger)

			fingerView.Finger = finger

			' Extract a template from the subject
			_biometricClient.FingersReturnBinarizedImage = True
			_biometricClient.BeginCreateTemplate(subject, AddressOf OnExtractCompleted, Nothing)
		End If
		Return fileLocation
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
				EnableCheckBoxes()
				SetShownImage()
				EnableVerifyButton()
			End Try
		End If
	End Sub

	Private Sub EnableVerifyButton()
		verifyButton.Enabled = IsSubjectValid(_subject1) AndAlso IsSubjectValid(_subject2)
	End Sub

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

					Dim matedMinutiae = _subject1.MatchingResults(0).MatchingDetails.Fingers(0).GetMatedMinutiae()

					fingerView1.MatedMinutiaIndex = 0
					fingerView1.MatedMinutiae = matedMinutiae

					fingerView2.MatedMinutiaIndex = 1
					fingerView2.MatedMinutiae = matedMinutiae

					fingerView1.PrepareTree()
					fingerView2.Tree = fingerView1.Tree
				Else
					MessageBox.Show(verificationStatus)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Function IsSubjectValid(ByVal subject As NSubject) As Boolean
		Return subject IsNot Nothing AndAlso (subject.Status = NBiometricStatus.Ok OrElse subject.Status = NBiometricStatus.MatchNotFound OrElse subject.Status = NBiometricStatus.None AndAlso subject.GetTemplateBuffer() IsNot Nothing)
	End Function

	Private Sub SetFar()
		Try
			_biometricClient.MatchingThreshold = Utils.MatchingThresholdFromString(matchingFarComboBox.Text)
			matchingFarComboBox.Text = Utils.MatchingThresholdToString(_biometricClient.MatchingThreshold)
			EnableVerifyButton()
		Catch
			matchingFarComboBox.Select()
			MessageBox.Show("FAR is not valid", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try
	End Sub

	Private Sub SetDefaultFar()
		matchingFarComboBox.SelectedIndex = 1
		defaultButton.Enabled = False
		SetFar()
	End Sub

	Private Sub ResetMatedMinutiaeOnViews()
		fingerView2.MatedMinutiae = Nothing
		fingerView1.MatedMinutiae = fingerView2.MatedMinutiae
		fingerView2.Tree = fingerView1.MatedMinutiae
		fingerView1.Tree = fingerView2.Tree
	End Sub

	Private Sub SetShownImage()
		fingerView1.ShownImage = If(chbShowBinarizedImage1.Checked, ShownImage.Result, ShownImage.Original)
		fingerView2.ShownImage = If(chbShowBinarizedImage2.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

	Private Sub EnableCheckBoxes()
		chbShowBinarizedImage1.Enabled = _subject1 IsNot Nothing AndAlso _subject1.Status = NBiometricStatus.Ok
		chbShowBinarizedImage2.Enabled = _subject2 IsNot Nothing AndAlso _subject2.Status = NBiometricStatus.Ok
	End Sub

#End Region

#Region "Private form events"

	Private Sub OpenImageButton1Click(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton1.Click
		chbShowBinarizedImage1.Enabled = False
		templateLeftLabel.Text = String.Empty
		templateLeftLabel.Text = OpenImageTemplate(fingerView1, _subject1)
	End Sub

	Private Sub OpenImageButton2Click(ByVal sender As Object, ByVal e As EventArgs) Handles openImageButton2.Click
		chbShowBinarizedImage2.Enabled = False
		templateRightLabel.Text = String.Empty
		templateRightLabel.Text = OpenImageTemplate(fingerView2, _subject2)
	End Sub

	Private Sub MatchingFarComboBoxEnter(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Enter
		defaultButton.Enabled = True
	End Sub

	Private Sub DefaultButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles defaultButton.Click
		SetDefaultFar()
	End Sub

	Private Sub ClearImagesButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles clearImagesButton.Click
		_subject1 = Nothing
		_subject2 = Nothing
		verifyButton.Enabled = False

		fingerView1.Finger = Nothing
		fingerView2.Finger = Nothing

		msgLabel.Text = String.Empty
		templateLeftLabel.Text = String.Empty
		templateRightLabel.Text = String.Empty

		chbShowBinarizedImage1.Enabled = False
		chbShowBinarizedImage2.Enabled = False
	End Sub

	Private Sub VerifyFingerLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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

	Private Sub VerifyButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles verifyButton.Click
		If _subject1 IsNot Nothing AndAlso _subject2 IsNot Nothing Then
			_biometricClient.MatchingWithDetails = True
			fingerView1.MatedMinutiae = Nothing
			fingerView2.MatedMinutiae = Nothing
			_biometricClient.BeginVerify(_subject1, _subject2, AddressOf OnVerifyCompleted, Nothing)
		End If
		verifyButton.Enabled = False
	End Sub

	Private Sub MatchingFarComboBoxLeave(ByVal sender As Object, ByVal e As EventArgs) Handles matchingFarComboBox.Leave
		SetFar()
	End Sub

	Private Sub FingerView1SelectedTreeMinutiaIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles fingerView1.MatedMinutiaIndexChanged, fingerView1.SelectedTreeMinutiaIndexChanged
		Dim args = TryCast(e, TreeMinutiaEventArgs)
		If fingerView2 IsNot Nothing AndAlso args IsNot Nothing Then
			fingerView2.SelectedMinutiaIndex = args.Index
		End If
	End Sub

	Private Sub FingerView2SelectedTreeMinutiaIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles fingerView2.SelectedTreeMinutiaIndexChanged
		Dim args = TryCast(e, TreeMinutiaEventArgs)
		If fingerView1 IsNot Nothing AndAlso args IsNot Nothing Then
			fingerView1.SelectedMinutiaIndex = args.Index
		End If
	End Sub

	Private Sub VerifyFingerVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			SetDefaultFar()
		End If
	End Sub

	Private Sub ChbShowBinarizedImage1CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarizedImage1.CheckedChanged
		SetShownImage()
	End Sub

	Private Sub ChbShowBinarizedImage2CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarizedImage2.CheckedChanged
		SetShownImage()
	End Sub

	Private Sub FingerView1MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fingerView1.MouseClick
		If e.Button = MouseButtons.Right AndAlso chbShowBinarizedImage1.Enabled Then
			chbShowBinarizedImage1.Checked = Not chbShowBinarizedImage1.Checked
		End If
	End Sub

	Private Sub FingerView2MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fingerView2.MouseClick
		If e.Button = MouseButtons.Right AndAlso chbShowBinarizedImage2.Enabled Then
			chbShowBinarizedImage2.Checked = Not chbShowBinarizedImage2.Checked
		End If
	End Sub

#End Region

End Class
