Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class GeneralizeFaces
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _subject As NSubject = Nothing

#End Region

#Region "Public constructor"

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
					faceView.Face = _subject.Faces.Last()
				Else
					lblStatus.Text = String.Format("Status: {0}", status)
					lblStatus.ForeColor = Color.Red
				End If
			Catch ex As Exception
				lblStatus.Text = "Status: error occurred"
				lblStatus.ForeColor = Color.Red
				Utils.ShowException(ex)
			End Try
			btnOpenImages.Enabled = True
		End If
	End Sub

	Private Sub BtnOpenImagesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenImages.Click
		_subject = Nothing
		faceView.Face = Nothing
		btnSaveTemplate.Enabled = False
		lblImageCount.Text = "0"
		lblStatus.Visible = False

		If OpenFileDialog.ShowDialog() = DialogResult.OK Then
			_subject = New NSubject()
			For Each fileName In openFileDialog.FileNames
				Dim face As New NFace() With {.FileName = fileName, .SessionId = 1}
				_subject.Faces.Add(face)
			Next fileName
			lblImageCount.Text = OpenFileDialog.FileNames.Length.ToString()
			lblStatus.Text = "Status: performing extraction and generalization"
			lblStatus.Visible = True
			lblStatus.ForeColor = Color.Black
			BiometricClient.BeginCreateTemplate(_subject, AddressOf OnCreateTemplateCompleted, Nothing)
			btnOpenImages.Enabled = False
		End If
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If _subject IsNot Nothing Then
			If saveTemplateDialog.ShowDialog() = DialogResult.OK Then
				Try
					Using templateBuffer = _subject.GetTemplateBuffer()
						File.WriteAllBytes(saveTemplateDialog.FileName, templateBuffer.ToArray())
					End Using
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub GeneralizeFacesLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If (Not DesignMode) Then
			OpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		End If
	End Sub

#End Region
End Class
