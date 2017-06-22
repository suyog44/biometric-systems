Imports System
Imports Neurotec.Devices
Imports Neurotec.Gui

Partial Public Class BiometricDeviceForm
	Inherits CaptureForm
#Region "Private fields"

	Private _automatic As Boolean = True
	Private _timeout As Integer = -1

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Protected methods"

	Protected Overrides Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return MyBase.IsValidDeviceType(value) AndAlso GetType(NBiometricDevice).IsAssignableFrom(value)
	End Function

	Protected Overrides Sub OnCancelCapture()
		MyBase.OnCancelCapture()
		NGui.InvokeAsync(AddressOf (CType(Device, NBiometricDevice)).Cancel)
	End Sub

	Protected Overrides Sub OnWriteScanParameters(ByVal writer As System.Xml.XmlWriter)
		MyBase.OnWriteScanParameters(writer)
		WriteParameter(writer, "Modality", (CType(Device, NBiometricDevice)).BiometricType)
	End Sub

#End Region

#Region "Public properties"

	Public Property Automatic() As Boolean
		Get
			Return _automatic
		End Get
		Set(ByVal value As Boolean)
			If _automatic <> value Then
				CheckIsBusy()
				_automatic = value
			End If
		End Set
	End Property

	Public Property Timeout() As Integer
		Get
			Return _timeout
		End Get
		Set(ByVal value As Integer)
			If _timeout <> value Then
				CheckIsBusy()
				_timeout = value
			End If
		End Set
	End Property

#End Region
End Class
