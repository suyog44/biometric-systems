Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports Neurotec.Images

Partial Public Class IrisScannerForm
	Inherits BiometricDeviceForm
#Region "Private fields"

	Private _position As NEPosition
	Private _missingPositions() As NEPosition

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		OnDeviceChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Overloads Function OnImage(ByVal biometric As NIris, ByVal isFinal As Boolean) As Boolean
		Dim sb As New StringBuilder()
		sb.Append(biometric.Status)
		For Each obj In biometric.Objects
			sb.AppendLine()
			sb.AppendFormat(vbTab & "{0}: {1} (Position: {2})", obj.Position, obj.Status, obj.BoundingRect)
		Next obj
		Return OnImage(biometric.Image, sb.ToString(), (If(biometric.Status <> NBiometricStatus.None, biometric.Status, NBiometricStatus.Ok)).ToString(), isFinal)
	End Function

#End Region

#Region "Protected methods"

	Protected Overrides Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return MyBase.IsValidDeviceType(value) AndAlso GetType(NIrisScanner).IsAssignableFrom(value)
	End Function

	Protected Overrides Sub OnCapture()
		Dim irisScanner As NIrisScanner = CType(Device, NIrisScanner)
		AddHandler irisScanner.CapturePreview, AddressOf DeviceCapturePreview
		Try
			Using subject = New NSubject()
				If _missingPositions IsNot Nothing Then
					For Each missingPosition In _missingPositions
						subject.MissingEyes.Add(missingPosition)
					Next missingPosition
				End If
				Using iris = New NIris()
					iris.Position = _position
					If (Not Automatic) Then
						iris.CaptureOptions = NBiometricCaptureOptions.Manual
					End If
					irisScanner.Capture(iris, Timeout)
					OnImage(iris, True)
				End Using
			End Using
		Finally
			RemoveHandler irisScanner.CapturePreview, AddressOf DeviceCapturePreview
		End Try
	End Sub

	Protected Overrides Sub OnWriteScanParameters(ByVal writer As System.Xml.XmlWriter)
		MyBase.OnWriteScanParameters(writer)
		WriteParameter(writer, "Position", Position)
		If MissingPositions IsNot Nothing Then
			For Each position As NEPosition In MissingPositions
				WriteParameter(writer, "Missing", position)
			Next position
		End If
	End Sub

#End Region

#Region "Public properties"

	Public Property Position() As NEPosition
		Get
			Return _position
		End Get
		Set(ByVal value As NEPosition)
			If _position <> value Then
				CheckIsBusy()
				_position = value
			End If
		End Set
	End Property

	Public Property MissingPositions() As NEPosition()
		Get
			Return _missingPositions
		End Get
		Set(ByVal value As NEPosition())
			If _missingPositions IsNot value Then
				CheckIsBusy()
				_missingPositions = value
			End If
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub DeviceCapturePreview(ByVal sender As Object, ByVal e As NBiometricDeviceCapturePreviewEventArgs)
		Dim force As Boolean = OnImage(CType(e.Biometric, NIris), False)
		Dim status As NBiometricStatus = e.Biometric.Status
		If (Not Automatic) Then
			If status = NBiometricStatus.Ok Or Not NBiometricTypes.IsBiometricStatusFinal(status) Then
				e.Biometric.Status = If(force, NBiometricStatus.Ok, NBiometricStatus.BadObject)
			End If
		End If
	End Sub

#End Region
End Class
