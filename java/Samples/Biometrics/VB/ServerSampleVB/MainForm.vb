Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Client

Partial Public Class MainForm
	Inherits Form
	Private Enum Task
		Enroll = 0
		Deduplication = 1
		SpeedTest = 2
		Settings = 3
	End Enum

#Region "Private fields"

	Private Const SampleTitle As String = "Server Sample"
	Private ReadOnly _selectedButtonColor As Color = SystemColors.Highlight
	Private ReadOnly _notSelectedButtonColor As Color = SystemColors.InactiveCaption
	Private ReadOnly _panels As New List(Of ControlBase)()

	Private _activePanel As ControlBase
	Private _templateLoader As ITemplateLoader
	Private _acceleratorConnection As AcceleratorConnection
	Private _biometricClient As NBiometricClient
	Private _biometricConnection As NClusterBiometricConnection

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		ToolTip.SetToolTip(btnConnection, "Change connection settings to Server and/or database containing templates")
		ToolTip.SetToolTip(btnTestSpeed, "Test Megamatcher Accelerator matching speed")
		toolTip.SetToolTip(btnEnroll, "Enroll templates to Megamatcher Accelerator")
		toolTip.SetToolTip(btnDeduplication, "Perform template deduplication on Megamatcher Accelerator")
	End Sub

#End Region

#Region "Private form events"

	Private Sub MainFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Dim panel As ControlBase = New EnrollPanel()
		panel.Dock = DockStyle.Fill
		_panels.Add(panel)
		panel.Focus()

		panel = New DeduplicationPanel With {.Dock = DockStyle.Fill}
		_panels.Add(panel)

		panel = New TestSpeedPanel With {.Dock = DockStyle.Fill}
		_panels.Add(panel)

		panel = New SettingsPanel With {.Dock = DockStyle.Fill}
		_panels.Add(panel)

		If (Not ShowConnectionSettings()) Then
			Close()
		End If
		For Each tmpPanel In _panels
			tmpPanel.BiometricClient = _biometricClient
		Next tmpPanel
	End Sub

	Private Sub BtnConnectionClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnection.Click
		If _activePanel.IsBusy Then
			If Utilities.ShowQuestion("Action in progress. Stop current action?") Then
				_activePanel.Cancel()
			Else
				Return
			End If
		End If
		If ShowConnectionSettings() Then
			_activePanel.TemplateLoader = _templateLoader
			_activePanel.Accelerator = _acceleratorConnection
			_activePanel.BiometricClient = _biometricClient
		End If
	End Sub

	Private Sub BtnTestSpeedClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnTestSpeed.Click
		ShowPanel(Task.SpeedTest)
	End Sub

	Private Sub BtnEnrollClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnEnroll.Click
		ShowPanel(Task.Enroll)
	End Sub

	Private Sub MainFormVisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
		ShowPanel(Task.Deduplication)
	End Sub

	Private Sub BtnDeduplicationClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDeduplication.Click
		ShowPanel(Task.Deduplication)
	End Sub

	Private Sub BtnSettingsClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSettings.Click
		ShowPanel(Task.Settings)
	End Sub

	Private Sub MainFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If _activePanel IsNot Nothing AndAlso _activePanel.IsBusy Then
			_activePanel.Cancel()
			Text = String.Format("{0}: Closing, please wait ...", SampleTitle)
			splitContainer.Panel1.Enabled = False
			Do While _activePanel.IsBusy
				Application.DoEvents()
			Loop
		End If
	End Sub

#End Region

#Region "Private methods"

	Private Function ShowConnectionSettings() As Boolean
		Dim dialog = New ConnectionForm With {.DB = TryCast(_templateLoader, DatabaseConnection)}
		If dialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
			_biometricClient = New NBiometricClient()
			_biometricConnection = New NClusterBiometricConnection(dialog.Server, dialog.ClientPort, dialog.AdminPort)
			_biometricClient.RemoteConnections.Add(_biometricConnection)
			If dialog.IsAccelerator Then
				_acceleratorConnection = New AcceleratorConnection(dialog.Server, dialog.UserName, dialog.Password)
			Else
				_acceleratorConnection = Nothing
			End If

			If (Not dialog.UseDB) Then
				_templateLoader = New DirectoryEnumerator(dialog.TemplateDir)
			Else
				_templateLoader = dialog.DB
			End If
			Return True
		End If
		Return False
	End Function

	Private Sub ShowPanel(ByVal task As Task, ByVal force As Boolean)
		Dim index = CInt(Fix(task))
		If _activePanel Is _panels(index) Then
			Return
		End If
		If (Not force) AndAlso _activePanel IsNot Nothing AndAlso _activePanel.IsBusy Then
			If Utilities.ShowQuestion("Action in progress. Stop current action?") Then
				_activePanel.Cancel()
			Else
				Return
			End If
		End If

		_activePanel = _panels(index)

		btnEnroll.BackColor = If(index = 0, _selectedButtonColor, _notSelectedButtonColor)
		btnDeduplication.BackColor = If(index = 1, _selectedButtonColor, _notSelectedButtonColor)
		btnTestSpeed.BackColor = If(index = 2, _selectedButtonColor, _notSelectedButtonColor)
		btnSettings.BackColor = If(index = 3, _selectedButtonColor, _notSelectedButtonColor)

		_activePanel.TemplateLoader = _templateLoader
		_activePanel.Accelerator = _acceleratorConnection
		_activePanel.BiometricClient = _biometricClient
		SplitContainer.Panel2.Controls.Clear()
		SplitContainer.Panel2.Controls.Add(_activePanel)

		Dim title As String = _activePanel.GetTitle()
		Text = If(String.IsNullOrEmpty(title), SampleTitle, String.Format("{0}: {1}", SampleTitle, title))
	End Sub

	Private Sub ShowPanel(ByVal task As Task)
		ShowPanel(task, False)
	End Sub

#End Region
End Class
