Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images

Partial Public Class EnrollFromImage
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _iris As NIris

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

	Private Sub OnExtractionCompleted(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnExtractionCompleted), r)
		Else
			Dim status As NBiometricStatus = _biometricClient.EndCreateTemplate(r)
			If status = NBiometricStatus.Ok Then
				lblQuality.Text = String.Format("Quality: {0}", _iris.Objects.First().Quality)
				btnSaveTemplate.Enabled = True
			Else
				_subject = Nothing
				_iris = Nothing
				lblQuality.Text = String.Empty
				MessageBox.Show("Iris image quality is too low.", Text, MessageBoxButtons.OK)
			End If
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnOpenClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
		openFileDialog.FileName = Nothing
		openFileDialog.Filter = NImages.GetOpenFileFilterString(True, False)

		If openFileDialog.ShowDialog() = DialogResult.OK Then
			lblQuality.Text = String.Empty
			btnSaveTemplate.Enabled = False

			' Create subject with selected iris image
			_iris = New NIris() With {.FileName = openFileDialog.FileName}
			irisView.Iris = _iris
			_subject = New NSubject()
			_subject.Irises.Add(_iris)
			' Start extraction
			_biometricClient.BeginCreateTemplate(_subject, AddressOf OnExtractionCompleted, Nothing)
		End If
	End Sub

	Private Sub BtnSaveTemplateClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTemplate.Click
		If _subject IsNot Nothing Then
			saveFileDialog.FileName = String.Empty
			saveFileDialog.Filter = String.Empty
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					File.WriteAllBytes(saveFileDialog.FileName, _subject.GetTemplateBuffer().ToArray())
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

#End Region
End Class
