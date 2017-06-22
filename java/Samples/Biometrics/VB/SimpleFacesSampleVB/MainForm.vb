Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

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
		_biometricClient = New NBiometricClient() With {.BiometricTypes = NBiometricType.Face, .UseDeviceManager = True}
		_biometricClient.Initialize()

		Dim page As New TabPage("Detect faces")
		Dim detectFaces As New DetectFaces()
		detectFaces.Dock = DockStyle.Fill
		detectFaces.BiometricClient = _biometricClient
		page.Controls.Add(detectFaces)
		tabControl.TabPages.Add(page)

		page = New TabPage("Enroll from image")
		Dim enrollFromImage As New EnrollFromImage() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(enrollFromImage)
		tabControl.TabPages.Add(page)

		page = New TabPage("Enroll from camera")
		Dim enrollFromCamera As New EnrollFromCamera() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(enrollFromCamera)
		tabControl.TabPages.Add(page)

		page = New TabPage("Identify face")
		Dim identifyFace As New IdentifyFace() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(identifyFace)
		tabControl.TabPages.Add(page)

		page = New TabPage("Verify face")
		Dim verifyFace As New VerifyFace() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(verifyFace)
		tabControl.TabPages.Add(page)

		page = New TabPage("Match multiple faces")
		Dim matchMultipleFaces As New MatchMultipleFaces() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(matchMultipleFaces)
		tabControl.TabPages.Add(page)

		page = New TabPage("Create token face image")
		Dim createTokenImage As New CreateTokenFaceImage() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(createTokenImage)
		tabControl.TabPages.Add(page)

		page = New TabPage("Generalize faces")
		Dim generalizeFaces As New GeneralizeFaces() With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(generalizeFaces)
		tabControl.TabPages.Add(page)

		page = New TabPage("Capture ICAO image")
		Dim icaoPage = New CaptureIcaoCompliantImage With {.Dock = DockStyle.Fill, .BiometricClient = _biometricClient}
		page.Controls.Add(icaoPage)
		tabControl.TabPages.Add(page)
	End Sub

	Private Sub TabControlSelecting(ByVal sender As Object, ByVal e As TabControlCancelEventArgs) Handles tabControl.Selecting
		If _biometricClient IsNot Nothing Then
			_biometricClient.Reset()
			_biometricClient.Cancel()
		End If
	End Sub

	Private Sub MainFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If _biometricClient IsNot Nothing Then
			_biometricClient.Cancel()
		End If
	End Sub

#End Region
End Class
