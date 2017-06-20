Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Media
Imports Neurotec.Devices
Imports Neurotec.Gui

Partial Public Class CaptureDeviceForm
	Inherits CaptureForm
#Region "Private fields"

	Private _nextMediaFormat As NMediaFormat
	Private ReadOnly _nextMediaFormatLock As Object = New Object()

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		AutoCaptureStart = True
	End Sub

#End Region

#Region "Protected methods"

	Protected Overrides Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return MyBase.IsValidDeviceType(value) AndAlso GetType(NCaptureDevice).IsAssignableFrom(value)
	End Function

	Protected Overrides Sub OnCapture()
		Dim captureDevice As NCaptureDevice = CType(Device, NCaptureDevice)
		AddHandler captureDevice.IsCapturingChanged, AddressOf Device_IsCapturingChanged
		OnStartingCapture()
		Dim stoppedCapturing As Boolean = False
		Try
			captureDevice.StartCapturing()
			Dim sampleObtained As Boolean = True
			Try
				AddMediaFormats(captureDevice.GetFormats(), captureDevice.GetCurrentFormat())
				Do While sampleObtained AndAlso Not IsCancellationPending
					SyncLock _nextMediaFormatLock
						If _nextMediaFormat IsNot Nothing Then
							captureDevice.SetCurrentFormat(_nextMediaFormat)
							_nextMediaFormat = Nothing
						End If
					End SyncLock
					sampleObtained = OnObtainSample()
				Loop
			Finally
				If sampleObtained AndAlso captureDevice.IsAvailable Then
					captureDevice.StopCapturing()
					stoppedCapturing = True
				End If
			End Try
		Finally
			RemoveHandler captureDevice.IsCapturingChanged, AddressOf Device_IsCapturingChanged
			OnFinishingCapture()
			If (Not stoppedCapturing) Then
				OnCaptureFinished()
			End If
		End Try
	End Sub

	Protected Overrides Sub OnCancelCapture()
		MyBase.OnCancelCapture()
		If Device.IsAvailable Then
			NGui.InvokeAsync(New MethodInvoker(AddressOf (CType(Device, NCaptureDevice)).StopCapturing))
		End If
	End Sub

	Protected Overrides Sub OnMediaFormatChanged(ByVal mediaFormat As NMediaFormat)
		SyncLock _nextMediaFormatLock
			_nextMediaFormat = mediaFormat
		End SyncLock
	End Sub

	Protected Overridable Function OnObtainSample() As Boolean
		Throw New NotImplementedException()
	End Function

	Protected Overridable Sub OnFinishingCapture()
	End Sub

	Protected Overridable Sub OnStartingCapture()
	End Sub

#End Region

#Region "Private form events"

	Private Sub Device_IsCapturingChanged(ByVal sender As Object, ByVal e As EventArgs)
		If Device.IsAvailable AndAlso (CType(Device, NCaptureDevice)).IsCapturing Then
			OnCaptureStarted()
		Else
			OnCaptureFinished()
		End If
	End Sub

#End Region
End Class
