Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Devices

Partial Public Class DeviceManagerForm
	Inherits Form
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		anyCheckBox.Tag = NDeviceType.Any
		captureDeviceCheckBox.Tag = NDeviceType.CaptureDevice
		microphoneCheckBox.Tag = NDeviceType.Microphone
		cameraCheckBox.Tag = NDeviceType.Camera
		biometricDeviceCheckBox.Tag = NDeviceType.BiometricDevice
		fScannerCheckBox.Tag = NDeviceType.FScanner
		fingerScannerCheckBox.Tag = NDeviceType.FingerScanner
		palmScannerCheckBox.Tag = NDeviceType.PalmScanner
		irisScannerCheckBox.Tag = NDeviceType.IrisScanner
		DeviceTypes = NDeviceType.Any
	End Sub

#End Region

#Region "Public properties"

	Public Property DeviceTypes() As NDeviceType
		Get
			Dim value As NDeviceType = NDeviceType.None
			For Each checkBox As CheckBox In deviceTypesGroupBox.Controls
				If checkBox.Checked Then
					value = value Or CType(checkBox.Tag, NDeviceType)
				End If
			Next checkBox
			Return value
		End Get
		Set(ByVal value As NDeviceType)
			For Each checkBox As CheckBox In deviceTypesGroupBox.Controls
				checkBox.Checked = (value And CType(checkBox.Tag, NDeviceType)) <> 0
			Next checkBox
		End Set
	End Property

	Public Property AutoPlug() As Boolean
		Get
			Return cbAutoplug.Checked
		End Get
		Set(ByVal value As Boolean)
			cbAutoplug.Checked = value
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub deviceTypeCheckBox_Click(ByVal sender As Object, ByVal e As EventArgs) Handles captureDeviceCheckBox.Click, microphoneCheckBox.Click, anyCheckBox.Click, fScannerCheckBox.Click, cameraCheckBox.Click, irisScannerCheckBox.Click, palmScannerCheckBox.Click, fingerScannerCheckBox.Click, biometricDeviceCheckBox.Click
		Dim checkBox As CheckBox = CType(sender, CheckBox)
		Dim deviceType As NDeviceType = CType(checkBox.Tag, NDeviceType)
		DeviceTypes = If(checkBox.CheckState = CheckState.Unchecked, DeviceTypes Or deviceType, DeviceTypes And (Not deviceType))
	End Sub

	Private Sub btnAccept_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAccept.Click
		If DeviceTypes = NDeviceType.None Then
			DeviceTypes = NDeviceType.Any
		End If
	End Sub

#End Region
End Class
