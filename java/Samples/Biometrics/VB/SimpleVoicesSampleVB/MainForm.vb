Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Client
Imports Neurotec.Devices
Imports Neurotec.Biometrics

Partial Public Class MainForm
	Inherits Form
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Private _biometricClient As NBiometricClient

#End Region

#Region "Private form events"

	Private Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		_biometricClient = New NBiometricClient With {.UseDeviceManager = True, .BiometricTypes = NBiometricType.Voice}
		_biometricClient.Initialize()

		enrollFromFilePanel.BiometricClient = _biometricClient
		enrollFromMicrophonePanel.BiometricClient = _biometricClient
		verifyVoicePanel.BiometricClient = _biometricClient
		identifyVoicePanel.BiometricClient = _biometricClient
	End Sub

	Private Sub MainFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
		End If
	End Sub

	Private Sub TabControlSelecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tabControl1.Selecting
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
			_biometricClient.Reset()
		End If
	End Sub

#End Region

End Class
