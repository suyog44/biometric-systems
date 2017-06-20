Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports Neurotec.Media

Partial Public Class FacesSettingsPage
	Inherits SettingsPageBase
#Region "Private fields"

	Private _areComponentsChecked As Boolean

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		cbMatchingSpeed.Items.Add(NMatchingSpeed.High)
		cbMatchingSpeed.Items.Add(NMatchingSpeed.Medium)
		cbMatchingSpeed.Items.Add(NMatchingSpeed.Low)

		cbTemplateSize.Items.Add(NTemplateSize.Large)
		cbTemplateSize.Items.Add(NTemplateSize.Medium)
		cbTemplateSize.Items.Add(NTemplateSize.Small)

		For Each mode In System.Enum.GetValues(GetType(NLivenessMode))
			cbLivenessMode.Items.Add(mode)
		Next mode

		AddHandler cbCamera.SelectedIndexChanged, AddressOf CbCameraSelectedIndexChanged
		AddHandler cbFormat.SelectedIndexChanged, AddressOf CbFormatSelectedIndexChanged
		AddHandler cbTemplateSize.SelectedIndexChanged, AddressOf CbTemplateSizeSelectedIndexChanged
		AddHandler cbMatchingSpeed.SelectedIndexChanged, AddressOf CbMatchingSpeedSelectedIndexChanged
		AddHandler nudMinIOD.ValueChanged, AddressOf NudMinIODValueChanged
		AddHandler nudConfidenceThreshold.ValueChanged, AddressOf NudConfidenceThresholdValueChanged
		AddHandler nudMaxRoll.ValueChanged, AddressOf NudMaxRollValueChanged
		AddHandler nudMaximalYaw.ValueChanged, AddressOf NudMaximalYawValueChanged
		AddHandler nudQuality.ValueChanged, AddressOf NudQualityValueChanged
		AddHandler cbLivenessMode.SelectedIndexChanged, AddressOf CbLivenessModeSelectedIndexChanged
		AddHandler nudLivenessThreshold.ValueChanged, AddressOf NudLivenessThresholdValueChanged
		AddHandler chbDetectAllFeaturePoints.CheckedChanged, AddressOf ChbDetectAllFeaturePointsCheckedChanged
		AddHandler chbDetectBaseDeaturePoints.CheckedChanged, AddressOf ChbDetectBaseDeaturePointsCheckedChanged
		AddHandler chbDetermineGender.CheckedChanged, AddressOf ChbDetermineGenderCheckedChanged
		AddHandler chbDetermineAge.CheckedChanged, AddressOf ChbDetermineAgeCheckedChanged
		AddHandler chbDetectProperties.CheckedChanged, AddressOf ChbDetectPropertiesCheckedChanged
		AddHandler chbRecognizeExpression.CheckedChanged, AddressOf ChbRecognizeExpressionCheckedChanged
		AddHandler chbRecognizeEmotion.CheckedChanged, AddressOf ChbRecognizeEmotionCheckedChanged
		AddHandler chbCreateThumbnail.CheckedChanged, AddressOf ChbCreateThumbnailCheckedChanged
		AddHandler nudThumbnailWidth.ValueChanged, AddressOf NudThumbnailWidthValueChanged
	End Sub

#End Region

#Region "Public methods"

	Public Overrides Sub OnNavigatingFrom()
		RemoveHandler Client.DeviceManager.Devices.CollectionChanged, AddressOf DevicesCollectionChanged

		MyBase.OnNavigatingFrom()
	End Sub

	Public Overrides Sub OnNavigatedTo(ByVal ParamArray args() As Object)
		MyBase.OnNavigatedTo(args)

		AddHandler Client.DeviceManager.Devices.CollectionChanged, AddressOf DevicesCollectionChanged

		If _areComponentsChecked = False Then
			_areComponentsChecked = True
			Dim isActivated As Boolean = LicensingTools.CanDetectFaceSegments(Client.LocalOperations)
			If (Not isActivated) Then
				chbDetectAllFeaturePoints.Enabled = False
				chbDetectBaseDeaturePoints.Enabled = False
				chbDetermineGender.Enabled = False
				chbRecognizeEmotion.Enabled = False
				chbDetectProperties.Enabled = False
				chbRecognizeExpression.Enabled = False
				chbDetermineAge.Enabled = False

				chbDetectAllFeaturePoints.Text &= " (Not activated)"
				chbDetectBaseDeaturePoints.Text &= " (Not activated)"
				chbDetermineGender.Text &= " (Not activated)"
				chbRecognizeEmotion.Text &= " (Not activated)"
				chbDetectProperties.Text &= " (Not activated)"
				chbRecognizeExpression.Text &= " (Not activated)"
				chbDetermineAge.Text &= " (Not activated)"
			End If
		End If
	End Sub

	Public Overrides Sub LoadSettings()
		ListDevices()

		cbCamera.SelectedItem = Client.FaceCaptureDevice
		cbTemplateSize.SelectedItem = Client.FacesTemplateSize
		cbMatchingSpeed.SelectedItem = Client.FacesMatchingSpeed
		nudMinIOD.Value = Client.FacesMinimalInterOcularDistance
		nudConfidenceThreshold.Value = Client.FacesConfidenceThreshold
		nudMaxRoll.Value = CDec(Client.FacesMaximalRoll)
		nudMaximalYaw.Value = CDec(Client.FacesMaximalYaw)
		nudQuality.Value = Client.FacesQualityThreshold
		cbLivenessMode.SelectedItem = Client.FacesLivenessMode
		nudLivenessThreshold.Value = Client.FacesLivenessThreshold
		chbDetectAllFeaturePoints.Checked = Client.FacesDetectAllFeaturePoints AndAlso chbDetectAllFeaturePoints.Enabled
		chbDetectBaseDeaturePoints.Checked = Client.FacesDetectBaseFeaturePoints AndAlso chbDetectBaseDeaturePoints.Enabled
		chbDetermineGender.Checked = Client.FacesDetermineGender AndAlso chbDetermineGender.Enabled
		chbDetectProperties.Checked = Client.FacesDetectProperties AndAlso chbDetectProperties.Enabled
		chbRecognizeExpression.Checked = Client.FacesRecognizeExpression AndAlso chbRecognizeExpression.Enabled
		chbRecognizeEmotion.Checked = Client.FacesRecognizeEmotion AndAlso chbRecognizeEmotion.Enabled
		chbCreateThumbnail.Checked = Client.FacesCreateThumbnailImage
		nudThumbnailWidth.Value = Client.FacesThumbnailImageWidth
		nudGeneralizationRecordCount.Value = SettingsManager.FacesGeneralizationRecordCount
		chbDetermineAge.Checked = Client.FacesDetermineAge

		MyBase.LoadSettings()
	End Sub

	Public Overrides Sub DefaultSettings()
		If cbCamera.Items.Count > 0 Then
			cbCamera.SelectedIndex = 0
		End If

		Client.ResetProperty("Faces.TemplateSize")
		Client.ResetProperty("Faces.MatchingSpeed")
		Client.ResetProperty("Faces.MinimalInterOcularDistance")
		Client.ResetProperty("Faces.ConfidenceThreshold")
		Client.ResetProperty("Faces.MaximalRoll")
		Client.ResetProperty("Faces.MaximalYaw")
		Client.ResetProperty("Faces.QualityThreshold")
		Client.ResetProperty("Faces.LivenessMode")
		Client.ResetProperty("Faces.LivenessThreshold")
		Client.FacesDetectAllFeaturePoints = chbDetectAllFeaturePoints.Enabled
		Client.ResetProperty("Faces.DetectBaseFeaturePoints")
		Client.FacesDetermineGender = chbDetermineGender.Enabled
		Client.FacesDetermineAge = chbDetermineAge.Enabled
		Client.FacesDetectProperties = chbDetectProperties.Enabled
		Client.FacesRecognizeExpression = chbRecognizeExpression.Enabled
		Client.FacesRecognizeEmotion = chbRecognizeEmotion.Enabled
		Client.FacesCreateThumbnailImage = True
		Client.FacesThumbnailImageWidth = 90
		SettingsManager.FacesGeneralizationRecordCount = 3

		MyBase.DefaultSettings()
	End Sub

	Public Overrides Sub SaveSettings()
		SettingsManager.FacesGeneralizationRecordCount = Convert.ToInt32(nudGeneralizationRecordCount.Value)
		MyBase.SaveSettings()
	End Sub

#End Region

#Region "Private methods"

	Private Sub ListDevices()
		Try
			Dim current = Client.FaceCaptureDevice
			Dim selected = cbCamera.SelectedItem
			cbCamera.BeginUpdate()
			cbCamera.Items.Clear()
			For Each item As NDevice In Client.DeviceManager.Devices
				If (item.DeviceType And NDeviceType.Camera) = NDeviceType.Camera Then
					cbCamera.Items.Add(item)
				End If
			Next item

			cbCamera.SelectedItem = current
			If cbCamera.SelectedIndex = -1 AndAlso cbCamera.Items.Count > 0 Then
				cbCamera.SelectedItem = selected
				If cbCamera.SelectedIndex = -1 AndAlso cbCamera.Items.Count > 0 Then
					cbCamera.SelectedIndex = 0
				End If
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			cbCamera.EndUpdate()
		End Try
	End Sub

	Private Sub ListVideoFormats()
		cbFormat.BeginUpdate()
		Try
			cbFormat.Items.Clear()
			Dim device As NCaptureDevice = TryCast(cbCamera.SelectedItem, NCaptureDevice)
			If device IsNot Nothing Then
				For Each item In device.GetFormats()
					cbFormat.Items.Add(item)
				Next item
				Dim current As NMediaFormat = device.GetCurrentFormat()
				If current IsNot Nothing Then
					Dim index As Integer = cbFormat.Items.IndexOf(current)
					If index <> -1 Then
						cbFormat.SelectedIndex = index
					Else
						cbFormat.Items.Insert(0, current)
						cbFormat.SelectedIndex = 0
					End If
				End If
			End If
		Finally
			cbFormat.EndUpdate()
		End Try
	End Sub

#End Region

#Region "Private events"

	Private Sub CbCameraSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FaceCaptureDevice = TryCast(cbCamera.SelectedItem, NCamera)
		ListVideoFormats()
		btnDisconnect.Enabled = Client.FaceCaptureDevice IsNot Nothing AndAlso Client.FaceCaptureDevice.IsDisconnectable
	End Sub

	Private Sub CbFormatSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Dim format As NMediaFormat = TryCast(cbFormat.SelectedItem, NMediaFormat)
		If format IsNot Nothing Then
			Client.FaceCaptureDevice.SetCurrentFormat(format)
		End If
	End Sub

	Private Sub DevicesCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		BeginInvoke(New MethodInvoker(AddressOf ListDevices))
	End Sub

	Private Sub CbTemplateSizeSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesTemplateSize = CType(cbTemplateSize.SelectedItem, NTemplateSize)
	End Sub

	Private Sub CbMatchingSpeedSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesMatchingSpeed = CType(cbMatchingSpeed.SelectedItem, NMatchingSpeed)
	End Sub

	Private Sub NudMinIODValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesMinimalInterOcularDistance = Convert.ToInt32(nudMinIOD.Value)
	End Sub

	Private Sub NudConfidenceThresholdValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesConfidenceThreshold = Convert.ToByte(nudConfidenceThreshold.Value)
	End Sub

	Private Sub NudMaxRollValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesMaximalRoll = Convert.ToSingle(nudMaxRoll.Value)
	End Sub

	Private Sub NudMaximalYawValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesMaximalYaw = Convert.ToSingle(nudMaximalYaw.Value)
	End Sub

	Private Sub NudQualityValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesQualityThreshold = Convert.ToByte(nudQuality.Value)
	End Sub

	Private Sub NudLivenessThresholdValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesLivenessThreshold = Convert.ToByte(nudLivenessThreshold.Value)
	End Sub

	Private Sub ChbDetectAllFeaturePointsCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesDetectAllFeaturePoints = chbDetectAllFeaturePoints.Checked
	End Sub

	Private Sub ChbDetectBaseDeaturePointsCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesDetectBaseFeaturePoints = chbDetectBaseDeaturePoints.Checked
	End Sub

	Private Sub ChbDetermineGenderCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesDetermineGender = chbDetermineGender.Checked
	End Sub

	Private Sub ChbDetermineAgeCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesDetermineAge = chbDetermineAge.Checked
	End Sub

	Private Sub ChbDetectPropertiesCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesDetectProperties = chbDetectProperties.Checked
	End Sub

	Private Sub ChbRecognizeExpressionCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesRecognizeExpression = chbRecognizeExpression.Checked
	End Sub

	Private Sub ChbRecognizeEmotionCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesRecognizeEmotion = chbRecognizeEmotion.Checked
	End Sub

	Private Sub ChbCreateThumbnailCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesCreateThumbnailImage = chbCreateThumbnail.Checked
		nudThumbnailWidth.Enabled = chbCreateThumbnail.Checked
	End Sub

	Private Sub NudThumbnailWidthValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FacesThumbnailImageWidth = Convert.ToInt32(nudThumbnailWidth.Value)
	End Sub

	Private Sub BtnConnectClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
		Dim newDevice As NDevice = Nothing
		Try
			Using form = New ConnectToDeviceForm()
				If form.ShowDialog() = DialogResult.OK Then
					newDevice = Client.DeviceManager.ConnectToDevice(form.SelectedPlugin, form.Parameters)
					ListDevices()
					cbCamera.SelectedItem = newDevice

					If CType(cbCamera.SelectedItem, NDevice) IsNot newDevice Then
						If newDevice IsNot Nothing Then
							Client.DeviceManager.DisconnectFromDevice(newDevice)
						End If

						Utilities.ShowError("Failed to create connection to device using specified connection details")
					End If
				End If
			End Using
		Catch ex As Exception
			If newDevice IsNot Nothing Then
				Client.DeviceManager.DisconnectFromDevice(newDevice)
			End If

			Utilities.ShowError(ex)
		End Try
	End Sub

	Private Sub BtnDisconnectClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
		Dim device As NDevice = CType(cbCamera.SelectedItem, NDevice)
		If device IsNot Nothing Then
			Try
				Client.DeviceManager.DisconnectFromDevice(device)
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

	Private Sub CbLivenessModeSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Client.FacesLivenessMode = CType(cbLivenessMode.SelectedItem, NLivenessAction)
		nudLivenessThreshold.Enabled = Client.FacesLivenessMode <> NLivenessAction.None
	End Sub

#End Region
End Class
