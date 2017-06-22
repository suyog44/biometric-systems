Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Forms

Partial Public Class ConnectionForm
	Inherits Form
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		LoadSettings()
	End Sub

#End Region

#Region "Private fields"

	Private _db As DatabaseConnection

#End Region	' Private fields

#Region "Public properties"

	Public Property Server() As String
		Get
			Return tbMMAServer.Text
		End Get
		Set(ByVal value As String)
			tbMMAServer.Text = value
		End Set
	End Property

	Public Property ClientPort() As Integer
		Get
			Return CInt(Fix(nudPort.Value))
		End Get
		Set(ByVal value As Integer)
			nudPort.Value = value
		End Set
	End Property

	Public Property AdminPort() As Integer
		Get
			Return CInt(Fix(nudAdminPort.Value))
		End Get
		Set(ByVal value As Integer)
			nudAdminPort.Value = value
		End Set
	End Property

	Public Property Password() As String
		Get
			Return mtbPasword.Text
		End Get
		Set(ByVal value As String)
			mtbPasword.Text = value
		End Set
	End Property

	Public Property UserName() As String
		Get
			Return tbUsername.Text
		End Get
		Set(ByVal value As String)
			tbUsername.Text = value
		End Set
	End Property

	Public Property UseDb() As Boolean
		Get
			Return rbDatabase.Checked
		End Get
		Set(ByVal value As Boolean)
			If value Then
				rbDatabase.Checked = True
			Else
				rbDirectory.Checked = True
			End If
			ToggleRadioButtons()
		End Set
	End Property

	Public Property TemplateDir() As String
		Get
			Return tbPath.Text
		End Get
		Set(ByVal value As String)
			tbPath.Text = value
		End Set
	End Property

	Public Property DbServer() As String
		Get
			Return tbDBServer.Text
		End Get
		Set(ByVal value As String)
			tbDBServer.Text = value
		End Set
	End Property

	Public ReadOnly Property DbTable() As String
		Get
			Return cbTable.Text
		End Get
	End Property

	Public Property DbUser() As String
		Get
			Return tbDBUser.Text
		End Get
		Set(ByVal value As String)
			tbDBUser.Text = value
		End Set
	End Property

	Public Property DbPassword() As String
		Get
			Return tbDBPassword.Text
		End Get
		Set(ByVal value As String)
			tbDBPassword.Text = value
		End Set
	End Property

	Public Property IsAccelerator() As Boolean
		Get
			Return chbIsAccelerator.Checked
		End Get
		Set(ByVal value As Boolean)
			chbIsAccelerator.Checked = value
		End Set
	End Property

	Public Property Db() As DatabaseConnection
		Get
			Return _db
		End Get
		Set(ByVal value As DatabaseConnection)
			_db = value
			If value Is Nothing Then
				Return
			End If

			DbServer = value.Server
			DbUser = value.User
			DbPassword = value.Password
			cbTable.Items.Clear()
			cbTable.Items.AddRange(value.GetTables())
			cbTable.SelectedItem = value.Table
			ListCollumns()
			cbId.SelectedItem = value.IdColumn
			cbTemplate.SelectedItem = value.TemplateColumn
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub LoadSettings()
		UseDb = SettingsAccesor.UseDb
		DbServer = SettingsAccesor.DbServer
		DbUser = SettingsAccesor.DbUser
		DbPassword = SettingsAccesor.DbPassword
		Server = SettingsAccesor.Server
		ClientPort = SettingsAccesor.ClientPort
		AdminPort = SettingsAccesor.AdminPort
		UserName = SettingsAccesor.UserName
		Password = SettingsAccesor.Password
		TemplateDir = SettingsAccesor.TemplateDir
		IsAccelerator = SettingsAccesor.IsAccelerator
	End Sub

	Private Sub ToggleRadioButtons()
		gbDatabase.Enabled = rbDatabase.Checked
		tbPath.Enabled = rbDirectory.Checked
		btnBrowse.Enabled = tbPath.Enabled
	End Sub

	Private Sub SaveSettings()
		SettingsAccesor.UseDb = UseDb
		SettingsAccesor.DbServer = DbServer
		Dim value As String = DbTable
		If (Not String.IsNullOrEmpty(value)) Then
			SettingsAccesor.DbTable = value
		End If
		SettingsAccesor.DbUser = DbUser
		SettingsAccesor.DbPassword = DbPassword
		value = TryCast(cbId.SelectedItem, String)
		If (Not String.IsNullOrEmpty(value)) Then
			SettingsAccesor.IdColumn = value
		End If
		value = TryCast(cbTemplate.SelectedItem, String)
		If (Not String.IsNullOrEmpty(value)) Then
			SettingsAccesor.TemplateColumn = value
		End If
		SettingsAccesor.Password = Password
		SettingsAccesor.UserName = UserName
		SettingsAccesor.ClientPort = ClientPort
		SettingsAccesor.AdminPort = AdminPort
		SettingsAccesor.Server = Server
		SettingsAccesor.TemplateDir = TemplateDir
		SettingsAccesor.IsAccelerator = IsAccelerator
		SettingsAccesor.SaveSettings()
	End Sub

	Private Sub DatabaseSettingsReset()
		Db = Nothing
		tbDBServer.Text = "mysql_dsn"
		tbDBUser.Text = "user"
		tbDBPassword.Text = "pass"
		Try
			Dim conn = New DatabaseConnection()
			Dim hostValue As String = conn.GetConfigValue("Host")
			If hostValue IsNot Nothing Then
				tbDBServer.Text = hostValue
			End If
			Dim userValue As String = conn.GetConfigValue("User")
			If userValue IsNot Nothing Then
				tbDBUser.Text = userValue
			End If
			Dim passwordValue As String = conn.GetConfigValue("Password")
			If passwordValue IsNot Nothing Then
				tbDBPassword.Text = passwordValue
			End If
		Catch
		End Try
		cbTable.Items.Clear()
		cbTemplate.Items.Clear()
		cbId.Items.Clear()
	End Sub

	Private Sub ListCollumns()
		cbId.Items.Clear()
		cbTemplate.Items.Clear()
		If cbTable.SelectedItem IsNot Nothing Then
			Dim columns() As String = Db.GetColumns(DbTable)
			If columns IsNot Nothing Then
				cbId.Items.AddRange(columns)
				cbTemplate.Items.AddRange(columns)
			End If
		End If
	End Sub

#End Region

#Region "Private form events"

	Private Sub BtnConnectClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConnect.Click
		If Db IsNot Nothing Then
			If Db.Server = DbServer AndAlso Db.User = DbUser AndAlso Db.Password = DbPassword Then
				Return
			End If
		End If
		_db = New DatabaseConnection()

		Try
			If _db Is Nothing Then
				Throw New ApplicationException("Specified db is null")
			End If

			_db.Server = DbServer
			_db.User = DbUser
			_db.Password = DbPassword
			_db.CheckConnection()
			cbTable.Items.Clear()
			Dim tables() As String = _db.GetTables()
			If tables IsNot Nothing Then
				cbTable.Items.AddRange(tables)
				Dim table As String = SettingsAccesor.DbTable
				If cbTable.Items.Contains(table) Then
					cbTable.SelectedItem = table
				ElseIf tables.Length > 0 Then
					cbTable.SelectedIndex = 0
				End If
			End If
		Catch ex As Exception
			_db = Nothing
			Utilities.ShowError("Failed to connect to db. Reason: {0}", ex)
		End Try
	End Sub

	Private Sub CbTableSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbTable.SelectedIndexChanged
		If Db IsNot Nothing Then
			Db.Table = cbTable.Text
		End If
		ListCollumns()

		Dim collumn As String = SettingsAccesor.IdColumn
		If cbId.Items.Contains(collumn) Then
			cbId.SelectedItem = collumn
		ElseIf cbId.Items.Count > 0 Then
			cbId.SelectedIndex = 0
		End If

		collumn = SettingsAccesor.TemplateColumn
		If cbTemplate.Items.Contains(collumn) Then
			cbTemplate.SelectedItem = collumn
		ElseIf cbTemplate.Items.Count > 0 Then
			cbTemplate.SelectedIndex = 0
		End If
	End Sub

	Private Sub BtnResetClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReset.Click
		DatabaseSettingsReset()
	End Sub

	Private Sub ChbIsAcceleratorCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chbIsAccelerator.CheckedChanged
		tbUsername.Enabled = chbIsAccelerator.Checked
		mtbPasword.Enabled = chbIsAccelerator.Checked
	End Sub

	Private Sub BtnCheckConnectionClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheckConnection.Click
		If AcceleratorConnection.CheckConnection(tbMMAServer.Text, CInt(Fix(nudAdminPort.Value))) Then
			Utilities.ShowInformation("Connection test successful")
		Else
			Utilities.ShowError("Connection failed")
		End If
	End Sub

	Private Sub BtnBrowseClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowse.Click
		Dim path As String = TemplateDir
		If (Not String.IsNullOrEmpty(path)) AndAlso Directory.Exists(path) Then
			folderBrowserDialog.SelectedPath = path
		End If
		If folderBrowserDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
			TemplateDir = folderBrowserDialog.SelectedPath
		End If
	End Sub

	Private Sub RbDirectoryCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbDirectory.CheckedChanged
		ToggleRadioButtons()
	End Sub

	Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
		If UseDb Then
			If Db Is Nothing Then
				Utilities.ShowInformation("Connection with database must be established before proceeding")
				Return
			End If
			Db.Table = cbTable.Text
			Db.TemplateColumn = cbTemplate.Text
			Db.IdColumn = cbId.Text
		ElseIf String.IsNullOrEmpty(TemplateDir) OrElse (Not Directory.Exists(TemplateDir)) Then
			Utilities.ShowInformation("Specified directory doesn't exists")
			Return
		End If

		SaveSettings()
		DialogResult = System.Windows.Forms.DialogResult.OK
	End Sub

#End Region
End Class
