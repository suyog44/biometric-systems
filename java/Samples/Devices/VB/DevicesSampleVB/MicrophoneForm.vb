Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Devices
Imports Neurotec.Sound
Imports Neurotec.Media
Imports Neurotec.Sound.Processing
Imports System.Windows.Forms

Partial Public Class MicrophoneForm
	Inherits CaptureDeviceForm
#Region "Private fields"

	Private _soundLevel As Double

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		OnDeviceChanged()
		EnableForcedCapture = False
	End Sub

#End Region

#Region "Private methods"

	Private Sub OnSoundSample(ByVal soundBuffer As NSoundBuffer)
		SyncLock _statusLock
			_soundLevel = NSoundProc.GetSoundLevel(soundBuffer)
		End SyncLock
		BeginInvoke(New MethodInvoker(AddressOf OnStatusChanged))
	End Sub

#End Region

#Region "Protected methods"

	Protected Overrides Sub OnStatusChanged()
		SyncLock _statusLock
			Dim level As Integer = CInt(Fix(_soundLevel * 100.0))
			soundLevelProgressBar.Value = level
			soundLevelProgressBar.Visible = IsCapturing
		End SyncLock
		MyBase.OnStatusChanged()
	End Sub

	Protected Overrides Function IsValidDeviceType(ByVal value As Type) As Boolean
		Return MyBase.IsValidDeviceType(value) AndAlso GetType(NMicrophone).IsAssignableFrom(value)
	End Function

	Protected Overrides Function OnObtainSample() As Boolean
		Dim microphone As NMicrophone = CType(Device, NMicrophone)
		Using soundSample As NSoundBuffer = microphone.GetSoundSample()
			If soundSample IsNot Nothing Then
				OnSoundSample(soundSample)
				Return True
			Else
				Return False
			End If
		End Using
	End Function

#End Region
End Class
