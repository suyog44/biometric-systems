Imports System
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Biometrics.Gui
Imports Neurotec.Images

Partial Public Class EnrollFromImage
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		OpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
		SaveFileDialog.Filter = NImages.GetSaveFileFilterString()
	End Sub

#End Region

#Region "Private fields"

	Private _image As NImage
	Private _biometricClient As NBiometricClient
	Private _subject As NSubject
	Private _subjectFinger As NFinger

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
			Dim status As NBiometricStatus = NBiometricStatus.None
			Try
				status = _biometricClient.EndCreateTemplate(r)
				If status <> NBiometricStatus.Ok Then
					MessageBox.Show(String.Format("Extraction failed. Status: {0}", status), Text, MessageBoxButtons.OK)
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
			Finally
				If status = NBiometricStatus.Ok Then
					lblQuality.Text = String.Format("Quality: {0}", _subjectFinger.Objects.First().Quality)
					saveImageButton.Enabled = True
					saveTemplateButton.Enabled = True
					chbShowBinarizedImage.Enabled = True
				Else
					lblQuality.Text = String.Empty
					fingerView2.Finger = Nothing
					_subject = Nothing
					_subjectFinger = Nothing
				End If
			End Try
		End If
	End Sub

	Private Sub SetShownImage()
		fingerView2.ShownImage = If(chbShowBinarizedImage.Checked, ShownImage.Result, ShownImage.Original)
	End Sub

	Private Sub ExtractFeatures()
		If _image Is Nothing Then
			Return
		End If
		saveImageButton.Enabled = False
		saveTemplateButton.Enabled = False
		chbShowBinarizedImage.Enabled = False

		' Create a finger subject and begin extracting
		_subjectFinger = New NFinger With {.Image = _image}
		_subject = New NSubject()
		_subject.Fingers.Add(_subjectFinger)
		fingerView2.Finger = _subjectFinger
		SetShownImage()

		_biometricClient.BeginCreateTemplate(_subject, AddressOf OnExtractionCompleted, Nothing)
	End Sub

#End Region

#Region "Private form events"

	Private Sub OpenButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles openButton.Click
		openFileDialog.FileName = Nothing
		If openFileDialog.ShowDialog() = DialogResult.OK Then
			Try
				If _image IsNot Nothing Then
					_image.Dispose()
					_image = Nothing
				End If
				lblQuality.Text = String.Empty
				fingerView1.Finger = Nothing
				fingerView2.Finger = Nothing

				extractFeaturesButton.Enabled = False
				saveImageButton.Enabled = False
				saveTemplateButton.Enabled = False

				_image = NImage.FromFile(openFileDialog.FileName)
				Dim finger = New NFinger With {.Image = _image}
				fingerView1.Finger = finger
				extractFeaturesButton.Enabled = True
				ExtractFeatures()
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub ExtractFeaturesButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles extractFeaturesButton.Click
		ExtractFeatures()
	End Sub

	Private Sub DefaultButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles defaultButton.Click
		thresholdNumericUpDown.Value = 39
	End Sub

	Private Sub SaveImageButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveImageButton.Click
		If _subjectFinger IsNot Nothing Then
			saveFileDialog.FileName = String.Empty
			saveFileDialog.Filter = NImages.GetSaveFileFilterString()
			If saveFileDialog.ShowDialog() = DialogResult.OK Then
				Try
					If chbShowBinarizedImage.Checked Then
						_subjectFinger.BinarizedImage.Save(saveFileDialog.FileName)
					Else
						_subjectFinger.Image.Save(saveFileDialog.FileName)
					End If
				Catch ex As Exception
					Utils.ShowException(ex)
				End Try
			End If
		End If
	End Sub

	Private Sub ThresholdNumericUpDownValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles thresholdNumericUpDown.ValueChanged
		If _biometricClient IsNot Nothing Then
			_biometricClient.FingersQualityThreshold = CByte(thresholdNumericUpDown.Value)
			defaultButton.Enabled = True
		End If
	End Sub

	Private Sub SaveTemplateButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles saveTemplateButton.Click
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

	Private Sub EnrollFromImageVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		If Visible AndAlso _biometricClient IsNot Nothing Then
			thresholdNumericUpDown.Value = 39
			thresholdNumericUpDown.Enabled = True
			defaultButton.Enabled = True
			_biometricClient.FingersReturnBinarizedImage = True
		End If
	End Sub

	Private Sub ChbShowBinarizedImageCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbShowBinarizedImage.CheckedChanged
		SetShownImage()
	End Sub

	Private Sub FingerView2MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles fingerView2.MouseClick
		If e.Button = MouseButtons.Right AndAlso chbShowBinarizedImage.Enabled Then
			chbShowBinarizedImage.Checked = Not chbShowBinarizedImage.Checked
		End If
	End Sub

#End Region

End Class
