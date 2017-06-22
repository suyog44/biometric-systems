Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports System.Linq

Partial Public Class PalmsSettingsPage
	Inherits Neurotec.Samples.SettingsPageBase
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
		AddHandler chbReturnBinarized.CheckedChanged, AddressOf ChbReturnBinarizedCheckedChanged
	End Sub

#End Region

#Region "Private methods"

	Private Sub ListDevices()
		Try
			Dim current = Client.PalmScanner
			Dim selected = cbScanners.SelectedItem
			cbScanners.BeginUpdate()
			cbScanners.Items.Clear()
			For Each item As NDevice In Client.DeviceManager.Devices
				If (item.DeviceType And NDeviceType.FScanner) = NDeviceType.FScanner Then
					Dim scanner As NFScanner = CType(item, NFScanner)
					If Array.Exists(scanner.GetSupportedImpressionTypes(), Function(x) NBiometricTypes.IsImpressionTypePalm(x)) Then
						cbScanners.Items.Add(item)
					End If
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
	End Sub

	Public Overrides Sub LoadSettings()
		ListDevices()

		cbScanners.SelectedItem = Client.PalmScanner
		cbTemplateSize.SelectedItem = Client.PalmsTemplateSize
		cbMatchingSpeed.SelectedItem = Client.PalmsMatchingSpeed
		nudMaximalRotation.Value = Convert.ToDecimal(Client.PalmsMaximalRotation)
		nudQuality.Value = Client.PalmsQualityThreshold
		chbReturnBinarized.Checked = Client.PalmsReturnBinarizedImage
		nudRecordCount.Value = SettingsManager.PalmsGeneralizationRecordCount

		MyBase.LoadSettings()

		btnDisconnect.Enabled = Client.FingerScanner IsNot Nothing AndAlso Client.FingerScanner.IsDisconnectable
	End Sub

	Public Overrides Sub DefaultSettings()
		If cbScanners.Items.Count > 0 Then
			cbScanners.SelectedIndex = 0
		End If

		Client.ResetProperty("Palms.TemplateSize")
		Client.ResetProperty("Palms.MatchingSpeed")
		Client.ResetProperty("Palms.MaximalRotation")
		Client.ResetProperty("Palms.QualityThreshold")
		Client.PalmsReturnBinarizedImage = True
		SettingsManager.PalmsGeneralizationRecordCount = 3

		MyBase.DefaultSettings()
	End Sub

	Public Overrides Sub SaveSettings()
		SettingsManager.PalmsGeneralizationRecordCount = Convert.ToInt32(nudRecordCount.Value)
		MyBase.SaveSettings()
	End Sub

#End Region

#Region "Private events"

	Private Sub DevicesCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		BeginInvoke(New MethodInvoker(AddressOf ListDevices))
	End Sub

	Private Sub CbScannersSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.PalmScanner = TryCast(cbScanners.SelectedItem, NFScanner)
		btnDisconnect.Enabled = Client.FingerScanner IsNot Nothing AndAlso Client.FingerScanner.IsDisconnectable
	End Sub

	Private Sub CbTemplateSizeSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.PalmsTemplateSize = CType(cbTemplateSize.SelectedItem, NTemplateSize)
	End Sub

	Private Sub CbMatchingSpeedSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.PalmsMatchingSpeed = CType(cbMatchingSpeed.SelectedItem, NMatchingSpeed)
	End Sub

	Private Sub NudMaximalRotationValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.PalmsMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value)
	End Sub

	Private Sub NudQualityValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.PalmsQualityThreshold = Convert.ToByte(nudQuality.Value)
	End Sub

	Private Sub ChbReturnBinarizedCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.PalmsReturnBinarizedImage = chbReturnBinarized.Checked
	End Sub

	Private Sub btnConnectClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
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

	Private Sub btnDisconnectClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisconnect.Click
		Dim device As NDevice = CType(cbScanners.SelectedItem, NDevice)
		If device IsNot Nothing Then
			Try
				Client.DeviceManager.DisconnectFromDevice(device)
			Catch ex As Exception
				Utilities.ShowError(ex)
			End Try
		End If
	End Sub

#End Region
End Class
