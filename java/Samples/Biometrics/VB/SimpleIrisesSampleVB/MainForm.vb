Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Client
Imports Neurotec.Devices

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
		_biometricClient = New NBiometricClient() With {.BiometricTypes = Biometrics.NBiometricType.Iris, .UseDeviceManager = True}
		_biometricClient.Initialize()

		Dim page = New TabPage("Enroll from image")
		Dim enrollFromImage = New EnrollFromImage With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(enrollFromImage)
		TabControl.TabPages.Add(page)

		page = New TabPage("Enroll from scanner")
		Dim enrollFromScanner = New EnrollFromScanner With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(enrollFromScanner)
		TabControl.TabPages.Add(page)

		page = New TabPage("Verify iris")
		Dim verifyIris = New VerifyIris With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(verifyIris)
		TabControl.TabPages.Add(page)

		page = New TabPage("Identify iris")
		Dim identifyIris = New IdentifyIris With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(identifyIris)
		TabControl.TabPages.Add(page)

		page = New TabPage("Segment iris")
		Dim segmentIris = New SegmentIris With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(segmentIris)
		TabControl.TabPages.Add(page)
	End Sub

	Private Sub MainFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
		End If
	End Sub

	Private Sub tabControlSelecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs)
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
			_biometricClient.Reset()
		End If
	End Sub

#End Region

End Class
