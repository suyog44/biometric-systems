Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Specialized
Imports System.Windows.Forms
Imports Neurotec.Devices
Imports Neurotec.Media

Partial Public Class VoicesSettingsPage
	Inherits Neurotec.Samples.SettingsPageBase
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		AddHandler cbMicrophones.SelectedIndexChanged, AddressOf CbMicrophonesSelectedIndexChanged
		AddHandler cbFormats.SelectedIndexChanged, AddressOf CbFormatsSelectedIndexChanged
		AddHandler chbUniquePhrases.CheckedChanged, AddressOf ChbUniquePhrasesCheckedChanged
		AddHandler chbTextDependent.CheckedChanged, AddressOf ChbTextDependentCheckedChanged
		AddHandler chbTextIndependant.CheckedChanged, AddressOf ChbTextIndependantCheckedChanged
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub ListDevices()
		Try
			Dim current = Client.VoiceCaptureDevice
			Dim selected = cbMicrophones.SelectedItem
			cbMicrophones.BeginUpdate()
			cbMicrophones.Items.Clear()
			For Each item As NDevice In Client.DeviceManager.Devices
				If (item.DeviceType And NDeviceType.Microphone) = NDeviceType.Microphone Then
					cbMicrophones.Items.Add(item)
				End If
			Next item

			cbMicrophones.SelectedItem = current
			If cbMicrophones.SelectedIndex = -1 AndAlso cbMicrophones.Items.Count > 0 Then
				cbMicrophones.SelectedItem = selected
				If cbMicrophones.SelectedIndex = -1 AndAlso cbMicrophones.Items.Count > 0 Then
					cbMicrophones.SelectedIndex = 0
				End If
			End If
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			cbMicrophones.EndUpdate()
		End Try
	End Sub

	Private Sub ListAudioFormats()
		cbFormats.BeginUpdate()
		Try
			cbFormats.Items.Clear()
			Dim device As NCaptureDevice = TryCast(cbMicrophones.SelectedItem, NCaptureDevice)
			If device IsNot Nothing Then
				For Each item In device.GetFormats()
					cbFormats.Items.Add(item)
				Next item
				Dim current As NMediaFormat = device.GetCurrentFormat()
				If current IsNot Nothing Then
					Dim index As Integer = cbFormats.Items.IndexOf(current)
					If index <> -1 Then
						cbFormats.SelectedIndex = index
					Else
						cbFormats.Items.Insert(0, current)
						cbFormats.SelectedIndex = 0
					End If
				End If
			End If
		Finally
			cbFormats.EndUpdate()
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

		cbMicrophones.SelectedItem = Client.VoiceCaptureDevice
		chbUniquePhrases.Checked = Client.VoicesUniquePhrasesOnly
		chbTextDependent.Checked = Client.VoicesExtractTextDependentFeatures
		chbTextIndependant.Checked = Client.VoicesExtractTextIndependentFeatures
		nudMaxFileSize.Value = CType(Client.VoicesMaximalLoadedFileSize / 1048576.0F, Decimal)

		MyBase.LoadSettings()
	End Sub

	Public Overrides Sub DefaultSettings()
		If cbMicrophones.Items.Count > 0 Then
			cbMicrophones.SelectedIndex = 0
		End If
		Client.ResetProperty("Voices.UniquePhrasesOnly")
		Client.ResetProperty("Voices.ExtractTextDependentFeatures")
		Client.ResetProperty("Voices.ExtractTextIndependentFeatures")
		Client.ResetProperty("Voices.MaximalLoadedFileSize")

		MyBase.DefaultSettings()
	End Sub

	#End Region

	#Region "Private events"

	Private Sub DevicesCollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
		BeginInvoke(New MethodInvoker(AddressOf ListDevices))
	End Sub

	Private Sub CbMicrophonesSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMicrophones.SelectedIndexChanged
		Client.VoiceCaptureDevice = TryCast(cbMicrophones.SelectedItem, NMicrophone)
		ListAudioFormats()
	End Sub

	Private Sub CbFormatsSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbFormats.SelectedIndexChanged
		Dim format As NMediaFormat = TryCast(cbFormats.SelectedItem, NMediaFormat)
		If format IsNot Nothing Then
			Client.VoiceCaptureDevice.SetCurrentFormat(format)
		End If
	End Sub

	Private Sub ChbUniquePhrasesCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbUniquePhrases.CheckedChanged
		Client.VoicesUniquePhrasesOnly = chbUniquePhrases.Checked
	End Sub

	Private Sub ChbTextDependentCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbTextDependent.CheckedChanged
		Client.VoicesExtractTextDependentFeatures = chbTextDependent.Checked
	End Sub

	Private Sub ChbTextIndependantCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbTextIndependant.CheckedChanged
		Client.VoicesExtractTextIndependentFeatures = chbTextIndependant.Checked
	End Sub

	Private Sub NudMaxFileSizeValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudMaxFileSize.ValueChanged
		Client.VoicesMaximalLoadedFileSize = CType(nudMaxFileSize.Value * 1048576, Long)
	End Sub

	#End Region
End Class
