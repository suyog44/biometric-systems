Imports Neurotec.Biometrics
Imports System.Data.Odbc
Imports System.Collections.Generic
Imports System.Data
Imports System
Imports Neurotec.IO

Public Class DatabaseConnection
	Implements ITemplateLoader
#Region "Protected fields"

	Private ReadOnly _lockObject As Object = New Object()

	Private _connection As OdbcConnection
	Private _dataReader As OdbcDataReader

	Private _isStarted As Boolean

#End Region

#Region "Public properties"

	Private privateIdColumn As String
	Public Property IdColumn() As String
		Get
			Return privateIdColumn
		End Get
		Set(ByVal value As String)
			privateIdColumn = value
		End Set
	End Property

	Private privateTemplateColumn As String
	Public Property TemplateColumn() As String
		Get
			Return privateTemplateColumn
		End Get
		Set(ByVal value As String)
			privateTemplateColumn = value
		End Set
	End Property

	Private privateServer As String
	Public Property Server() As String
		Get
			Return privateServer
		End Get
		Set(ByVal value As String)
			privateServer = value
		End Set
	End Property

	Private privateTable As String
	Public Property Table() As String
		Get
			Return privateTable
		End Get
		Set(ByVal value As String)
			privateTable = value
		End Set
	End Property

	Private privateUser As String
	Public Property User() As String
		Get
			Return privateUser
		End Get
		Set(ByVal value As String)
			privateUser = value
		End Set
	End Property

	Private privatePassword As String
	Public Property Password() As String
		Get
			Return privatePassword
		End Get
		Set(ByVal value As String)
			privatePassword = value
		End Set
	End Property

#End Region

#Region "Protected methods"
	Protected Sub Connect()
		Dim connectionString As String = String.Empty
		If (Not String.IsNullOrEmpty(Server)) Then
			connectionString &= String.Format("DSN={0};", Server)
		End If
		If (Not String.IsNullOrEmpty(User)) Then
			connectionString &= String.Format("UID={0};", User)
		End If
		If (Not String.IsNullOrEmpty(Password)) Then
			connectionString &= String.Format("PWD={0};", Password)
		End If

		Dim connection = New OdbcConnection(connectionString)
		connection.Open()
		_connection = connection
	End Sub

	Protected Sub CloseConnection()
		If _connection IsNot Nothing Then
			_connection.Close()
			_connection = Nothing
		End If
	End Sub
#End Region

#Region "Public methods"

	Public Sub CheckConnection()
		If _isStarted Then
			Return
		End If
		Connect()
		CloseConnection()
	End Sub

	Public Function GetTables() As String()
		Dim results As New List(Of String)()

		Connect()
		Try
			Dim tables As DataTable = _connection.GetSchema("Tables")
			For Each table As DataRow In tables.Rows
				results.Add(table("TABLE_NAME").ToString())
			Next table
		Catch e1 As OdbcException
			Return Nothing
		Finally
			CloseConnection()
		End Try

		Return results.ToArray()
	End Function

	Public Function GetColumns(ByVal table As String) As String()
		Dim results As New List(Of String)()

		Connect()
		Try
			Dim restrictions(3) As String
			restrictions(2) = table
			Dim columns As DataTable = _connection.GetSchema("Columns", restrictions)
			For Each column As DataRow In columns.Rows
				results.Add(column("COLUMN_NAME").ToString())
			Next column
		Catch e1 As OdbcException
			Return Nothing
		Finally
			CloseConnection()
		End Try

		Return results.ToArray()
	End Function

	Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
		If Not (TypeOf obj Is DatabaseConnection) Then
			Return False
		End If
		Dim target = TryCast(obj, DatabaseConnection)
		Return IdColumn = target.IdColumn AndAlso Password = target.Password AndAlso Server = target.Server AndAlso Table = target.Table AndAlso TemplateColumn = target.TemplateColumn AndAlso User = target.User
	End Function

	Public Overrides Function GetHashCode() As Integer
		Return MyBase.GetHashCode()
	End Function

	Public Overrides Function ToString() As String
		Return String.Format("ODBC connection at {0}; table: {1};", Server, Table)
	End Function

	Public Function GetConfigValue(ByVal key As String) As String
		Return Nothing
	End Function

#End Region

#Region "ITemplateLoader members"

	Public Sub BeginLoad() Implements ITemplateLoader.BeginLoad
		SyncLock _lockObject
			If _isStarted Then
				Throw New InvalidOperationException()
			End If
			_isStarted = True
			Connect()
			Dim cmd As OdbcCommand = _connection.CreateCommand()
			cmd.CommandTimeout = 0
			cmd.CommandText = String.Format("SELECT {0}, {1} FROM {2}", IdColumn, TemplateColumn, Table)
			_dataReader = cmd.ExecuteReader()
		End SyncLock
	End Sub

	Public Sub EndLoad() Implements ITemplateLoader.EndLoad
		SyncLock _lockObject
			If _dataReader IsNot Nothing Then
				_dataReader.Close()
			End If
			_dataReader = Nothing
			_isStarted = False
			CloseConnection()
		End SyncLock
	End Sub

	Public Function LoadNext(ByRef subjects() As NSubject, ByVal count As Integer) As Boolean Implements ITemplateLoader.LoadNext
		SyncLock _lockObject
			If (Not _isStarted) Then
				Throw New InvalidOperationException()
			End If
			Dim subjectList = New List(Of NSubject)()
			Do While subjectList.Count < count AndAlso _dataReader.Read()
				Dim tmpSubject = New NSubject()
				If (Not _dataReader.IsDBNull(1)) Then
					Dim tmpl = TryCast(_dataReader.GetValue(1), Byte())
					tmpSubject.SetTemplateBuffer(New NBuffer(tmpl))
				End If
				tmpSubject.Id = _dataReader.GetValue(0).ToString()
				subjectList.Add(tmpSubject)
			Loop
			subjects = subjectList.ToArray()
			Return subjects.Length > 0
		End SyncLock
	End Function

	Public ReadOnly Property TemplateCount() As Integer Implements ITemplateLoader.TemplateCount
		Get
			If _isStarted Then
				Throw New InvalidOperationException("Can not get count while loading started")
			End If
			Connect()
			Try
				Using cmd = New OdbcCommand(String.Format("SELECT COUNT(*) FROM {0}", Table), _connection)
					cmd.CommandTimeout = 0
					Dim result As Object = cmd.ExecuteScalar()
					Return Int32.Parse(result.ToString())
				End Using
			Finally
				CloseConnection()
			End Try
		End Get
	End Property

#End Region

#Region "IDisposable members"

	Public Overridable Sub Dispose() Implements ITemplateLoader.Dispose
		CloseConnection()
	End Sub

#End Region
End Class
