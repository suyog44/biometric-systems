Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.Odbc
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Client

Partial Public Class ChangeDatabaseDialog
	Inherits Form
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Public properties"

	Private privateBiometricClient As NBiometricClient
	Public Property BiometricClient() As NBiometricClient
		Get
			Return privateBiometricClient
		End Get
		Set(ByVal value As NBiometricClient)
			privateBiometricClient = value
		End Set
	End Property
	Public ReadOnly Property ClearDatabase() As Boolean
		Get
			Return chbClear.Checked AndAlso Not rbRemoteServer.Checked
		End Get
	End Property

#End Region

#Region "Private methods"

	Private Sub ApplyValues()
		Dim type As ConnectionType = GetSelected()
		BiometricClient.DatabaseConnection = Nothing
		BiometricClient.RemoteConnections.Clear()
		Select Case type
			Case ConnectionType.SQLiteDatabase
				Dim dbPath As String = Path.Combine(Utilities.GetUserLocalDataDir("NeurotechnologySample"), "BiometricsV50.db")
				BiometricClient.SetDatabaseConnectionToSQLite(dbPath)
				Exit Select
			Case ConnectionType.OdbcDatabase
				BiometricClient.SetDatabaseConnectionToOdbc(tbConnectionString.Text, cbTableName.Text)
			Case ConnectionType.RemoteMatchingServer
				Dim port As Integer = Convert.ToInt32(nudClientPort.Value)
				Dim adminPort As Integer = Convert.ToInt32(nudAdminPort.Value)
				Dim host As String = tbHostName.Text
				BiometricClient.RemoteConnections.AddToCluster(host, port, adminPort)
				Exit Select
			Case Else
		End Select

		Dim selected As SampleDbSchema = CType(cbSchema.SelectedItem, SampleDbSchema)
		If (Not selected.IsEmpty) Then
			BiometricClient.BiographicDataSchema = selected.BiographicData
			If selected.CustomData IsNot Nothing Then
				BiometricClient.CustomDataSchema = selected.CustomData
			End If
		End If
	End Sub

	Private Function GetSelected() As ConnectionType
		Return If(rbSQLite.Checked, ConnectionType.SQLiteDatabase, (If(rbOdbc.Checked, ConnectionType.OdbcDatabase, ConnectionType.RemoteMatchingServer)))
	End Function

	Private Sub TogleRadioButtons()
		Dim selected As ConnectionType = GetSelected()
		tbConnectionString.Enabled = selected = ConnectionType.OdbcDatabase
		cbTableName.Enabled = tbConnectionString.Enabled
		btnListTables.Enabled = cbTableName.Enabled
		nudAdminPort.Enabled = selected = ConnectionType.RemoteMatchingServer
		nudClientPort.Enabled = nudAdminPort.Enabled
		tbHostName.Enabled = nudClientPort.Enabled
		chbClear.Enabled = selected = ConnectionType.OdbcDatabase OrElse selected = ConnectionType.SQLiteDatabase
		chbClear.Checked = chbClear.Checked AndAlso chbClear.Enabled
		cbLocalOperations.Enabled = (selected = ConnectionType.RemoteMatchingServer)
	End Sub

#End Region

#Region "Private events"

	Private Sub InitializeBiometricClient(ByVal biometricClient As NBiometricClient, ByVal isRemoteServerChecked As Boolean, ByVal operationsIndex As Integer)
		biometricClient.UseDeviceManager = True
		biometricClient.Initialize()

		If isRemoteServerChecked Then
			Dim localOperations As NBiometricOperations = NBiometricOperations.None
			Dim operations() As NBiometricOperations = {NBiometricOperations.None, NBiometricOperations.Detect, NBiometricOperations.DetectSegments, NBiometricOperations.Segment, NBiometricOperations.AssessQuality, NBiometricOperations.CreateTemplate}
			For i As Integer = 0 To operationsIndex
				localOperations = localOperations Or operations(i)
			Next i
			biometricClient.LocalOperations = localOperations
		End If

		SettingsManager.LoadSettings(biometricClient)
		SettingsManager.LoadPreferedDevices(biometricClient)
		If ClearDatabase Then
			Dim status As NBiometricStatus = biometricClient.Clear()
			If status <> NBiometricStatus.Ok Then
				Utilities.ShowInformation("Failed to clear database: {0}", status)
			End If
		End If
	End Sub

	Private Sub BtnOKClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOK.Click
		Dim schema As SampleDbSchema = CType(cbSchema.SelectedItem, SampleDbSchema)
		If rbRemoteServer.Checked Then
			If schema.HasCustomData Then
				Dim sb = New StringBuilder()
				Dim count = schema.CustomData.Elements.Count

				For i As Integer = 0 To count - 1
					sb.Append(schema.CustomData.Elements(i).Name)
					If i + 1 <> count Then
						sb.Append(", ")
					End If
				Next i

				Dim msg = "Current schema contains custom data (Columns: {0}). Only biographic data is supported with remote matching server. Please select different schema or edit current one."
				Utilities.ShowInformation(String.Format(msg, sb))
				Return
			End If
		End If

		If (Not rbSQLite.Checked) AndAlso SettingsManager.WarnHasSchema AndAlso Not schema.IsEmpty Then
			Dim msg As String = "Please note that for biometric client will not automaticly create columns specified in database schema for odbc connection or matching server." & " User must ensure that columns specified in schema exists. Continue anyway?"
			If Utilities.ShowQuestion(Me, msg) Then
				SettingsManager.WarnHasSchema = False
			Else
				Return
			End If
		End If

		Try
			BiometricClient = New NBiometricClient()
			ApplyValues()
			Dim isRemoteServerChecked As Boolean = rbRemoteServer.Checked
			Dim operationsIndex As Integer = cbLocalOperations.SelectedIndex

			LongActionDialog.ShowDialog(Me, "Initializing biometric client ... ", New Action(Of NBiometricClient, Boolean, Integer)(AddressOf InitializeBiometricClient), BiometricClient, isRemoteServerChecked, operationsIndex)
		Catch ex As Exception
			Utilities.ShowError(ex)
			BiometricClient.Dispose()
			BiometricClient = Nothing
			Return
		End Try

		SettingsManager.RemoteServerHostName = tbHostName.Text
		SettingsManager.RemoteServerAdminPort = Convert.ToInt32(nudAdminPort.Value)
		SettingsManager.RemoteServerPort = Convert.ToInt32(nudClientPort.Value)
		SettingsManager.TableName = cbTableName.Text
		SettingsManager.OdbcConnectionString = tbConnectionString.Text
		SettingsManager.ConnectionType = GetSelected()

		Dim index As Integer = cbSchema.SelectedIndex
		SettingsManager.CurrentSchemaIndex = If(index + 1 <> cbSchema.Items.Count, index, -1)
		SettingsManager.LocalOperationsIndex = cbLocalOperations.SelectedIndex

		DialogResult = System.Windows.Forms.DialogResult.OK
	End Sub

	Private Sub ChangeDatabaseDialogLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		For Each item In SettingsManager.Schemas
			cbSchema.Items.Add(item)
		Next item
		cbSchema.Items.Add(SampleDbSchema.Empty)
		cbSchema.SelectedIndex = SettingsManager.CurrentSchemaIndex
		If cbSchema.SelectedIndex = -1 Then
			cbSchema.SelectedItem = SampleDbSchema.Empty
		End If

		Select Case SettingsManager.ConnectionType
			Case ConnectionType.SQLiteDatabase
				rbSQLite.Checked = True
			Case ConnectionType.OdbcDatabase
				rbOdbc.Checked = True
			Case ConnectionType.RemoteMatchingServer
				rbRemoteServer.Checked = True
			Case Else
		End Select
		tbConnectionString.Text = SettingsManager.OdbcConnectionString
		cbTableName.Text = SettingsManager.TableName
		tbHostName.Text = SettingsManager.RemoteServerHostName
		nudClientPort.Value = SettingsManager.RemoteServerPort
		nudAdminPort.Value = SettingsManager.RemoteServerAdminPort
		cbLocalOperations.SelectedIndex = SettingsManager.LocalOperationsIndex
	End Sub

	Private Sub ChangeDatabaseDialogFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If DialogResult <> System.Windows.Forms.DialogResult.OK Then
			If BiometricClient IsNot Nothing Then
				BiometricClient.Dispose()
			End If
			BiometricClient = Nothing
		End If
	End Sub

	Private Sub BtnListTablesClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnListTables.Click
		Dim sqlConnection As OdbcConnection = Nothing
		Try
			sqlConnection = New OdbcConnection(tbConnectionString.Text)
			sqlConnection.Open()
			Using table As DataTable = sqlConnection.GetSchema(OdbcMetaDataCollectionNames.Tables)
				Dim names(table.Rows.Count - 1) As String
				For i As Integer = 0 To names.Length - 1
					Dim row As DataRow = table.Rows(i)
					names(i) = row("TABLE_NAME").ToString()
				Next i

				cbTableName.Items.Clear()
				For Each item In names
					cbTableName.Items.Add(item)
				Next item
				If cbTableName.Items.Count > 0 Then
					cbTableName.SelectedIndex = 0
				End If
			End Using
		Catch ex As Exception
			Utilities.ShowError(ex)
		Finally
			If sqlConnection IsNot Nothing Then
				sqlConnection.Close()
			End If
		End Try
	End Sub

	Private Sub RadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbRemoteServer.CheckedChanged, rbSQLite.CheckedChanged, rbOdbc.CheckedChanged
		Dim rb As RadioButton = TryCast(sender, RadioButton)
		If rb IsNot Nothing AndAlso rb.Checked Then
			TogleRadioButtons()
			If rb IsNot rbSQLite AndAlso Not (CType(cbSchema.SelectedItem, SampleDbSchema)).IsEmpty Then
				toolTip.Show("Please make sure database schema is correct for current database or remote matching server", lblDbSchema)
			End If
		End If
	End Sub

	Private Sub BtnEditClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
		Dim builderForm As New SchemaBuilderForm()
		Dim schemas() As SampleDbSchema = SettingsManager.Schemas.ToArray()
		Dim current As SampleDbSchema = CType(cbSchema.SelectedItem, SampleDbSchema)
		builderForm.Schema = current
		If current.IsEmpty Then
			Return
		End If
		If builderForm.ShowDialog() = DialogResult.OK Then
			current = builderForm.Schema
			cbSchema.Items(cbSchema.SelectedIndex) = current
			schemas(cbSchema.SelectedIndex) = current
			SettingsManager.Schemas = schemas
		End If
	End Sub

	Private Sub CbSchemaSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSchema.SelectedIndexChanged
		btnEdit.Enabled = Not CType(cbSchema.SelectedItem, SampleDbSchema).IsEmpty
	End Sub

#End Region
End Class
