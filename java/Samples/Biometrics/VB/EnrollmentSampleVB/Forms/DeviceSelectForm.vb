Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Devices

Namespace Forms
	Partial Public Class DeviceSelectForm
		Inherits Form
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
		End Sub

		#End Region

		#Region "Public properties"

		Private privateDeviceManager As NDeviceManager
		Public Property DeviceManager() As NDeviceManager
			Get
				Return privateDeviceManager
			End Get
			Set(ByVal value As NDeviceManager)
				privateDeviceManager = value
			End Set
		End Property

		Private privateSelectedDevice As NFScanner
		Public Property SelectedDevice() As NFScanner
			Get
				Return privateSelectedDevice
			End Get
			Set(ByVal value As NFScanner)
				privateSelectedDevice = value
			End Set
		End Property

		#End Region

		#Region "Private form methods"

		Private Sub ScannerFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			cbScanner.BeginUpdate()
			Try
				cbScanner.Items.Clear()
				If DeviceManager IsNot Nothing Then
					For Each item As NDevice In DeviceManager.Devices
						If TypeOf item Is NFScanner Then
							cbScanner.Items.Add(item)
						End If
					Next item

					cbScanner.SelectedItem = SelectedDevice
					If cbScanner.SelectedItem Is Nothing AndAlso cbScanner.Items.Count > 0 Then
						cbScanner.SelectedIndex = 0
					End If
				End If
			Finally
				cbScanner.EndUpdate()
			End Try
		End Sub

		Private Sub CbScannerSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbScanner.SelectedIndexChanged
			SelectedDevice = TryCast(cbScanner.SelectedItem, NFScanner)
			If SelectedDevice IsNot Nothing Then
				Dim canCaptureSlaps As Boolean = False
				Dim canCaptureRolled As Boolean = False

				For Each item As NFPosition In SelectedDevice.GetSupportedPositions()
					If (Not NBiometricTypes.IsPositionFourFingers(item)) Then
						Continue For
					End If
					canCaptureSlaps = True
					Exit For
				Next item

				For Each item As NFImpressionType In SelectedDevice.GetSupportedImpressionTypes()
					If (Not NBiometricTypes.IsImpressionTypeRolled(item)) Then
						Continue For
					End If
					canCaptureRolled = True
					Exit For
				Next item

				If canCaptureRolled Then
					lblCanCaptureRolled.Image = Global.Neurotec.Samples.My.Resources.Ok
				Else
					lblCanCaptureRolled.Image = Global.Neurotec.Samples.My.Resources.Bad
				End If

				If canCaptureSlaps Then
					lblCanCaptureSlaps.Image = Global.Neurotec.Samples.My.Resources.Ok
				Else
					lblCanCaptureSlaps.Image = Global.Neurotec.Samples.My.Resources.Bad
				End If
			Else
				lblCanCaptureRolled.Visible = False
				lblCanCaptureSlaps.Visible = False
			End If
		End Sub

		Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
			If SelectedDevice IsNot Nothing Then
				DialogResult = System.Windows.Forms.DialogResult.OK
			Else
				Utilities.ShowWarning("Scanner not selected")
			End If
		End Sub

		#End Region
	End Class
End Namespace
