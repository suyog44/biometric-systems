Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Devices

Partial Public Class FingersSettingsPage
	Inherits Neurotec.Samples.SettingsPageBase
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

		AddHandler cbScanners.SelectedIndexChanged, AddressOf CbScannersSelectedIndexChanged
		AddHandler cbTemplateSize.SelectedIndexChanged, AddressOf CbTemplateSizeSelectedIndexChanged
		AddHandler cbMatchingSpeed.SelectedIndexChanged, AddressOf CbMatchingSpeedSelectedIndexChanged
		AddHandler nudMaximalRotation.ValueChanged, AddressOf NudMaximalRotationValueChanged
		AddHandler nudQuality.ValueChanged, AddressOf NudQualityValueChanged
		AddHandler chbFastExtraction.CheckedChanged, AddressOf ChbFastExtractionCheckedChanged
		AddHandler chbReturnBinarized.CheckedChanged, AddressOf ChbReturnBinarizedCheckedChanged
	End Sub

#End Region

#Region "Private methods"

	Private Sub ListDevices()
		Try
			Dim current = Client.FingerScanner
			Dim selected = cbScanners.SelectedItem
			cbScanners.BeginUpdate()
			cbScanners.Items.Clear()
			For Each item As NDevice In Client.DeviceManager.Devices
				If (item.DeviceType And NDeviceType.FScanner) = NDeviceType.FScanner Then
					cbScanners.Items.Add(item)
				End If
			Next item

			cbScanners.SelectedItem = current
			If cbScanners.SelectedIndex = -1 AndAlso cbScanners.Items.Count > 0 Then
				cbScanners.SelectedItem = selected
				If cbScanners.SelectedIndex = -1 AndAlso cbScanners.Items.Count > 0 Then
					cbScanners.SelectedIndex = 0
				End If
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			cbScanners.EndUpdate()
		End Try
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
			chbCalculateNfiq.Enabled = LicensingTools.CanAssessFingerQuality(Client.LocalOperations)
			If (Not chbCalculateNfiq.Enabled) Then
				chbCalculateNfiq.Text &= " (Not activated)"
			End If
			chbDeterminePatternClass.Enabled = LicensingTools.CanDetectFingerSegments(Client.LocalOperations)
			If (Not chbDeterminePatternClass.Enabled) Then
				chbDeterminePatternClass.Text &= " (Not activated)"
			End If
			Dim remoteConnection = Client.RemoteConnections.FirstOrDefault()
			Dim remoteOperations As NBiometricOperations = If(remoteConnection IsNot Nothing, remoteConnection.Operations, NBiometricOperations.None)
			chbCheckForDuplicates.Enabled = LicensingTools.CanFingerBeMatched(remoteOperations)
			If (Not chbCheckForDuplicates.Enabled) Then
				chbCheckForDuplicates.Text &= " (Not activated)"
			End If
		End If
	End Sub

	Public Overrides Sub LoadSettings()
		ListDevices()

		cbScanners.SelectedItem = Client.FingerScanner
		cbTemplateSize.SelectedItem = Client.FingersTemplateSize
		cbMatchingSpeed.SelectedItem = Client.FingersMatchingSpeed
		nudMaximalRotation.Value = Convert.ToDecimal(Client.FingersMaximalRotation)
		nudQuality.Value = Client.FingersQualityThreshold
		chbFastExtraction.Checked = Client.FingersFastExtraction
		chbReturnBinarized.Checked = Client.FingersReturnBinarizedImage
		chbDeterminePatternClass.Checked = Client.FingersDeterminePatternClass
		chbCalculateNfiq.Checked = Client.FingersCalculateNfiq
		chbCheckForDuplicates.Checked = Client.FingersCheckForDuplicatesWhenCapturing

		nudGeneralizationRecordCount.Value = SettingsManager.FingersGeneralizationRecordCount

		MyBase.LoadSettings()
	End Sub

	Public Overrides Sub DefaultSettings()
		If cbScanners.Items.Count > 0 Then
			cbScanners.SelectedIndex = 0
		End If
		Client.ResetProperty("Fingers.TemplateSize")
		Client.ResetProperty("Fingers.MatchingSpeed")
		Client.ResetProperty("Fingers.MaximalRotation")
		Client.ResetProperty("Fingers.QualityThreshold")
		Client.ResetProperty("Fingers.FastExtraction")
		Client.FingersReturnBinarizedImage = True
		Client.FingersCalculateNfiq = chbCalculateNfiq.Enabled
		Client.FingersDeterminePatternClass = chbDeterminePatternClass.Enabled
		Client.FingersCheckForDuplicatesWhenCapturing = chbCheckForDuplicates.Enabled
		SettingsManager.FingersGeneralizationRecordCount = 3

		MyBase.DefaultSettings()
	End Sub

	Public Overrides Sub SaveSettings()
		SettingsManager.FingersGeneralizationRecordCount = Convert.ToInt32(nudGeneralizationRecordCount.Value)

		MyBase.SaveSettings()
	End Sub

#End Region

#Region "Private events"

	Private Sub DevicesCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		BeginInvoke(New MethodInvoker(AddressOf ListDevices))
	End Sub

	Private Sub CbScannersSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingerScanner = TryCast(cbScanners.SelectedItem, NFScanner)
		btnDisconnect.Enabled = Client.FingerScanner IsNot Nothing AndAlso Client.FingerScanner.IsDisconnectable
	End Sub

	Private Sub CbTemplateSizeSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingersTemplateSize = CType(cbTemplateSize.SelectedItem, NTemplateSize)
	End Sub

	Private Sub CbMatchingSpeedSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingersMatchingSpeed = CType(cbMatchingSpeed.SelectedItem, NMatchingSpeed)
	End Sub

	Private Sub NudMaximalRotationValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingersMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value)
	End Sub

	Private Sub NudQualityValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingersQualityThreshold = Convert.ToByte(nudQuality.Value)
	End Sub

	Private Sub ChbFastExtractionCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingersFastExtraction = chbFastExtraction.Checked
	End Sub

	Private Sub ChbReturnBinarizedCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.FingersReturnBinarizedImage = chbReturnBinarized.Checked
	End Sub

	Private Sub ChbDeterminePatternClassCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbDeterminePatternClass.CheckedChanged
		Client.FingersDeterminePatternClass = chbDeterminePatternClass.Checked
	End Sub

	Private Sub ChbCalculateNfiqCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCalculateNfiq.CheckedChanged
		Client.FingersCalculateNfiq = chbCalculateNfiq.Checked
	End Sub

	Private Sub BtnConnectClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
		Dim newDevice As NDevice = Nothing
		Try
			Using form = New ConnectToDeviceForm()
				If form.ShowDialog() = DialogResult.OK Then
					newDevice = Client.DeviceManager.ConnectToDevice(form.SelectedPlugin, form.Parameters)
					ListDevices()
					cbScanners.SelectedItem = newDevice

					If CType(cbScanners.SelectedItem, NDevice) IsNot newDevice Then
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
		Dim device As NDevice = CType(cbScanners.SelectedItem, NDevice)
		If device IsNot Nothing Then
			Try
				Client.DeviceManager.DisconnectFromDevice(device)
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

	Private Sub ChbCheckForDuplicatesCheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chbCheckForDuplicates.CheckedChanged
		Client.FingersCheckForDuplicatesWhenCapturing = True
	End Sub

#End Region

End Class
