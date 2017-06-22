Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Devices

Partial Public Class IrisesSettingsPage
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
		AddHandler chbFastExtraction.CheckedChanged, AddressOf ChbFastExtractionCheckedChanged
	End Sub

#End Region

#Region "Private methods"

	Private Sub ListDevices()
		Try
			Dim current = Client.IrisScanner
			Dim selected = cbScanners.SelectedItem
			cbScanners.BeginUpdate()
			cbScanners.Items.Clear()
			For Each item As NDevice In Client.DeviceManager.Devices
				If (item.DeviceType And NDeviceType.IrisScanner) = NDeviceType.IrisScanner Then
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
	End Sub

	Public Overrides Sub LoadSettings()
		ListDevices()

		cbScanners.SelectedItem = Client.IrisScanner
		cbTemplateSize.SelectedItem = Client.IrisesTemplateSize
		cbMatchingSpeed.SelectedItem = Client.IrisesMatchingSpeed
		nudMaximalRotation.Value = Convert.ToDecimal(Client.IrisesMaximalRotation)
		nudQuality.Value = Client.IrisesQualityThreshold
		chbFastExtraction.Checked = Client.IrisesFastExtraction

		MyBase.LoadSettings()
	End Sub

	Public Overrides Sub DefaultSettings()
		If cbScanners.Items.Count > 0 Then
			cbScanners.SelectedIndex = 0
		End If
		Client.ResetProperty("Irises.TemplateSize")
		Client.ResetProperty("Irises.MatchingSpeed")
		Client.ResetProperty("Irises.MaximalRotation")
		Client.ResetProperty("Irises.QualityThreshold")
		Client.ResetProperty("Irises.FastExtraction")

		MyBase.DefaultSettings()
	End Sub

#End Region

#Region "Private events"

	Private Sub DevicesCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		BeginInvoke(New MethodInvoker(AddressOf ListDevices))
	End Sub

	Private Sub CbScannersSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.IrisScanner = TryCast(cbScanners.SelectedItem, NIrisScanner)
	End Sub

	Private Sub CbTemplateSizeSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.IrisesTemplateSize = CType(cbTemplateSize.SelectedItem, NTemplateSize)
	End Sub

	Private Sub CbMatchingSpeedSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.IrisesMatchingSpeed = CType(cbMatchingSpeed.SelectedItem, NMatchingSpeed)
	End Sub

	Private Sub NudMaximalRotationValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.IrisesMaximalRotation = Convert.ToSingle(nudMaximalRotation.Value)
	End Sub

	Private Sub NudQualityValueChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.IrisesQualityThreshold = Convert.ToByte(nudQuality.Value)
	End Sub

	Private Sub ChbFastExtractionCheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
		Client.IrisesFastExtraction = chbFastExtraction.Checked
	End Sub

#End Region
End Class
