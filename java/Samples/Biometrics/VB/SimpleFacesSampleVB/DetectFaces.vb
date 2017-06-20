Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Client
Imports Neurotec.Images
Imports Neurotec.Licensing

Partial Public Class DetectFaces
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _image As NImage
	Private _biometricClient As NBiometricClient
	Private _isSegmentationActivated? As Boolean

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
		_biometricClient.FacesMaximalRoll = CSng(cbRollAngle.SelectedItem)
		_biometricClient.FacesMaximalYaw = CSng(cbYawAngle.SelectedItem)

		If (Not _isSegmentationActivated.HasValue) Then
			_isSegmentationActivated = NLicense.IsComponentActivated("Biometrics.FaceSegmentsDetection")
		End If

		_biometricClient.FacesDetectAllFeaturePoints = _isSegmentationActivated.Value
		_biometricClient.FacesDetectBaseFeaturePoints = _isSegmentationActivated.Value
	End Sub

	Private Sub DetectFace(ByVal image As NImage)
		If image Is Nothing Then
			Return
		End If

		SetBiometricClientParams()
		' Detect asynchroniously all faces that are suitable for face recognition in the image
		_biometricClient.BeginDetectFaces(image, AddressOf OnDetectDone, Nothing)
	End Sub

	Private Sub OnDetectDone(ByVal r As IAsyncResult)
		If InvokeRequired Then
			BeginInvoke(New AsyncCallback(AddressOf OnDetectDone), r)
		Else
			Try
				facesView.Face = _biometricClient.EndDetectFaces(r)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub DetectFacesLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Try
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
		Catch ex As Exception
			Utils.ShowException(ex)
		End Try
	End Sub

	Private Sub TsbOpenImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbOpenImage.Click
		openFaceImageDlg.Filter = NImages.GetOpenFileFilterString(True, True)
		If openFaceImageDlg.ShowDialog() = DialogResult.OK Then
			If _image IsNot Nothing Then
				_image.Dispose()
			End If
			_image = Nothing

			Try
				' Read image
				_image = NImage.FromFile(openFaceImageDlg.FileName)
				DetectFace(_image)
			Catch ex As Exception
				Utils.ShowException(ex)
			End Try
		End If
	End Sub

	Private Sub TsbDetectFacialFeaturesClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsbDetectFacialFeatures.Click
		Try
			DetectFace(_image)
		Catch ex As Exception
			Utils.ShowException(ex)
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
