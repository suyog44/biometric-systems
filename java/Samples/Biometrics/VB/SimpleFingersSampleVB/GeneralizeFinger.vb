Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images
Imports Neurotec.Biometrics.Gui

Partial Public Class GeneralizeFinger
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject = Nothing

#End Region

#Region "Public properties"

	Private privateBiometricClient As NBiometricClient
	Public Property BiometricClient() As NBiometricClient
		Get
			Return privateBiometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateBiometricClient = value
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub OnCreateTemplateCompleted(ByVal result As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnCreateTemplateCompleted), result)
		Else
			Try
				Dim status As NBiometricStatus = BiometricClient.EndCreateTemplate(result)
				If status = NBiometricStatus.Ok Then
					lblStatus.Text = "Status: Ok"
					lblStatus.ForeColor = Color.Green
					btnSaveTemplate.Enabled = True
					fingerView.Finger = _subject.Fingers.Last()
				Else
					lblStatus.Text = String.Format("Status: {0}", status)
					lblStatus.ForeColor = Color.Red
				End If
			Catch ex As Exception
				lblStatus.Text = "Status: error occured"
				lblStatus.ForeColor = Color.Red
				Utils.ShowException(ex)
			End Try
			btnOpenImages.Enabled = True
		End If
	End Sub

	Private Sub BtnOpenImagesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImages.Click
		_subject = Nothing
		fingerView.Finger = Nothing
		btnSaveTemplate.Enabled = False
		lblImageCount.Text = "0"
		lblStatus.Visible = False

		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Dim files = openFileDialog.FileNames
			If files.Length < 3 OrElse files.Length > 10 Then
				Dim msg As String = String.Format("{0} images selected. Please select at least 3 and no more than 10 images", If(files.Length > 10, "Too many", "Too few"))
				MessageBox.Show(msg, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
			Else
				_subject = New NSubject()
				For Each fileName In files
					Dim finger As New NFinger() With {.FileName = fileName, .SessionId = 1}
					_subject.Fingers.Add(finger)
				Next fileName
				lblImageCount.Text = openFileDialog.FileNames.Length.ToString()
				lblStatus.Text = "Status: performing extraction and generalizarion"
				lblStatus.Visible = True
				lblStatus.ForeColor = Color.Black
				BiometricClient.BeginCreateTemplate(_subject, AddressOf OnCreateTemplateCompleted, Nothing)
				btnOpenImages.Enabled = False
			End If
		End If
	End Sub

	Private Sub GeneralizeFingerLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		End If
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If _subject IsNot Nothing Then
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					Using templateBuffer = _subject.GetTemplateBuffer()
						File.WriteAllBytes(saveFileDialog.FileName, templateBuffer.ToArray())
					End Using
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub GeneralizeFingerVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso BiometricClient IsNot Nothing Then
			BiometricClient.FingersReturnBinarizedImage = True
		End If
	End Sub

	Private Sub ChbShowBinarizedImageCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarizedImage.CheckedChanged
		fingerView.ShownImage = If(chbShowBinarizedImage.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

#End Region
End Class
