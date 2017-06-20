Imports System
Imports System.Collections.Generic
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
		_biometricClient = New NBiometricClient With {.UseDeviceManager = True, .BiometricTypes = NBiometricType.Finger}
		_biometricClient.Initialize()

		Dim page = New TabPage("Enroll From Image")
		Dim enrollFromImage = New EnrollFromImage With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(enrollFromImage)
		tabControl.TabPages.Add(page)

		page = New TabPage("Enroll From Scanner")
		Dim enrollFromScanner = New EnrollFromScanner With {.BiometricClient = _biometricClient, .Dock = DockStyle.Fill}
		page.Controls.Add(enrollFromScanner)
		tabControl.TabPages.Add(page)

		page = New TabPage("Identify Finger")
		Dim identifyFinger = New IdentifyFinger With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(identifyFinger)
		tabControl.TabPages.Add(page)

		page = New TabPage("Verify Finger")
		Dim verifyFinger = New VerifyFinger With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(verifyFinger)
		tabControl.TabPages.Add(page)

		page = New TabPage("Segment Fingers")
		Dim segmentFingers = New SegmentFingerprints With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(segmentFingers)
		tabControl.TabPages.Add(page)

		page = New TabPage("Generalize Finger")
		Dim generalizeFingers = New GeneralizeFinger With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(generalizeFingers)
		tabControl.TabPages.Add(page)

	End Sub

	Private Sub TabControlSelecting(ByVal sender As Object, ByVal e As TabControlCancelEventArgs) Handles tabControl.Selecting
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
			_biometricClient.Reset()
		End If
	End Sub

	Private Sub MainFormFormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
		End If
	End Sub

#End Region

End Class
