Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images
Imports Neurotec.Licensing

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
	Private _isSegmentationActivated As Boolean?

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

	Private Sub SetBiometricClientParams()
		If Not _isSegmentationActivated.HasValue Then
			_isSegmentationActivated = NLicense.IsComponentActivated("Biometrics.FaceSegmentsDetection")
		End If

		_biometricClient.FacesDetectAllFeaturePoints = _isSegmentationActivated.Value
		_biometricClient.FacesDetectBaseFeaturePoints = _isSegmentationActivated.Value
		_biometricClient.FacesDetermineGender = _isSegmentationActivated.Value
		_biometricClient.FacesDetermineAge = _isSegmentationActivated.Value
		_biometricClient.FacesDetectProperties = _isSegmentationActivated.Value
		_biometricClient.FacesRecognizeEmotion = _isSegmentationActivated.Value
		_biometricClient.FacesRecognizeExpression = _isSegmentationActivated.Value
	End Sub

	Private Sub ExtractTemplate()
		If _subject Is Nothing Then
			Return
		End If
		SetBiometricClientParams()

		' Extract template
		_biometricClient.BeginCreateTemplate(_subject, AddressOf OnExtractDone, Nothing)
	End Sub

	Private Sub OnExtractDone(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnExtractDone), r)
		Else
			Try
				Dim status As NBiometricStatus = _biometricClient.EndCreateTemplate(r)
				If status = NBiometricStatus.Ok Then
					lblStatus.Text = "Template extracted"
					btnSaveTemplate.Enabled = True
				Else
					lblStatus.Text = String.Format("Extraction failed: {0}", status)
					btnSaveTemplate.Enabled = False
				End If
			Catch ex As Exception
				Utils.ShowException(ex)
				lblStatus.Text = "Extraction failed!"
				btnSaveTemplate.Enabled = False
			End Try
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub EnrollFromImageLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
			lblStatus.Text = String.Empty
			Dim item As Single = _biometricClient.FacesMaximalRoll
			Dim items As New List(Of Single)()
			For i As Single = 15 To 180 Step 15
				items.Add((i))
			Next i
			If (Not items.Contains(item)) Then
				items.Add(item)
			End If
			items.Sort()

			Dim index As Integer = items.IndexOf(item)
			Dim j As Integer = 0
			Do While j <> items.Count
				cbRollAngle.Items.Add(items(j))
				j += 1
			Loop
			cbRollAngle.SelectedIndex = index

			item = _biometricClient.FacesMaximalYaw
			items.Clear()
			For i As Single = 15 To 90 Step 15
				items.Add((i))
			Next i
			If (Not items.Contains(item)) Then
				items.Add(item)
			End If
			items.Sort()

			index = items.IndexOf(item)
			j = 0
			Do While j <> items.Count
				cbYawAngle.Items.Add(items(j))
				j += 1
			Loop
			cbYawAngle.SelectedIndex = index

			openFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
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

	Private Sub TsbExtractClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbExtract.Click
		Try
			ExtractTemplate()
		Catch ex As Exception
			Utils.ShowException(ex)
			btnSaveTemplate.Enabled = False
		End Try
	End Sub

	Private Sub TsbOpenImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenImage.Click
		Try
			openFileDialog.FileName = Nothing
			If openFileDialog.ShowDialog() = DialogResult.OK Then
				' Create new subject
				_subject = New NSubject()
				Dim face As New NFace() With {.FileName = openFileDialog.FileName}
				_subject.Faces.Add(face)
				facesView.Face = face
				ExtractTemplate()
			End If
		Catch ex As Exception
			Utils.ShowException(ex)
			btnSaveTemplate.Enabled = False
		End Try
	End Sub

	Private Sub CbYawAngleSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbYawAngle.SelectedIndexChanged
		_biometricClient.FacesMaximalYaw = CSng(cbYawAngle.SelectedItem)
	End Sub

	Private Sub CbRollAngleSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbRollAngle.SelectedIndexChanged
		_biometricClient.FacesMaximalRoll = CSng(cbRollAngle.SelectedItem)
	End Sub

#End Region
End Class
