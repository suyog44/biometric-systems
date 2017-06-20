Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Neurotec.Biometrics
Imports Neurotec.Devices
Imports Neurotec.Images

Partial Public Class FScannerForm
	Inherits BiometricDeviceForm
#Region "Private fields"

	Private _impressionType As NFImpressionType
	Private _position As NFPosition
	Private _missingPositions() As NFPosition

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		OnDeviceChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Overloads Function OnImage(ByVal biometric As NFrictionRidge, ByVal isFinal As Boolean) As Boolean
		Dim sb As New StringBuilder()
		sb.Append(biometric.Status)
		For Each obj In biometric.Objects
			sb.AppendLine()
			sb.AppendFormat(vbTab & "{0}: {1}", obj.Position, obj.Status)
		Next obj
		Return OnImage(biometric.Image, sb.ToString(), (If(biometric.Status <> NBiometricStatus.None, biometric.Status, NBiometricStatus.Ok)).ToString(), isFinal)
	End Function

#End Region

#Region "Protected methods"

	Protected Overrides Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return MyBase.IsValidDeviceType(value) AndAlso GetType(NFScanner).IsAssignableFrom(value)
	End Function

	Protected Overrides Sub OnCapture()
		Dim fScanner As NFScanner = CType(Device, NFScanner)
		AddHandler fScanner.CapturePreview, AddressOf Device_CapturePreview
		Try
			Using subject = New NSubject()
				If _missingPositions IsNot Nothing Then
					For Each missingPosition In _missingPositions
						subject.MissingFingers.Add(missingPosition)
					Next missingPosition
				End If

				Dim biometric = NFrictionRidge.FromPosition(_position)
				If TypeOf biometric Is NFinger Then
					subject.Fingers.Add(CType(biometric, NFinger))
				Else
					subject.Palms.Add(CType(biometric, NPalm))
				End If
				biometric.ImpressionType = _impressionType
				biometric.Position = _position
				If (Not Automatic) Then
					biometric.CaptureOptions = NBiometricCaptureOptions.Manual
				End If
				fScanner.Capture(biometric, Timeout)
				OnImage(biometric, True)
			End Using
		Finally
			RemoveHandler fScanner.CapturePreview, AddressOf Device_CapturePreview
		End Try
	End Sub

	Protected Overrides Sub OnWriteScanParameters(ByVal writer As System.Xml.XmlWriter)
		MyBase.OnWriteScanParameters(writer)
		WriteParameter(writer, "ImpressionType", ImpressionType)
		WriteParameter(writer, "Position", Position)
		If MissingPositions IsNot Nothing Then
			For Each position As NFPosition In MissingPositions
				WriteParameter(writer, "Missing", position)
			Next position
		End If
	End Sub

#End Region

#Region "Public properties"

	Public Property ImpressionType() As NFImpressionType
		Get
			Return _impressionType
		End Get
		Set(ByVal value As NFImpressionType)
			If _impressionType <> value Then
				CheckIsBusy()
				_impressionType = value
			End If
		End Set
	End Property

	Public Property Position() As NFPosition
		Get
			Return _position
		End Get
		Set(ByVal value As NFPosition)
			If _position <> value Then
				CheckIsBusy()
				_position = value
			End If
		End Set
	End Property

	Public Property MissingPositions() As NFPosition()
		Get
			Return _missingPositions
		End Get
		Set(ByVal value As NFPosition())
			If _missingPositions IsNot value Then
				CheckIsBusy()
				_missingPositions = value
			End If
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub Device_CapturePreview(ByVal sender As Object, ByVal e As NBiometricDeviceCapturePreviewEventArgs)
		Dim force As Boolean = OnImage(CType(e.Biometric, NFrictionRidge), False)
		Dim status As NBiometricStatus = e.Biometric.Status
		If (Not Automatic) Then
			If status = NBiometricStatus.Ok Or Not NBiometricTypes.IsBiometricStatusFinal(status) Then
				e.Biometric.Status = If(force, NBiometricStatus.Ok, NBiometricStatus.BadObject)
			End If
		End If
	End Sub

#End Region
End Class

