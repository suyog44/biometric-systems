Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.IO

Public Class DirectoryEnumerator
	Implements ITemplateLoader
#Region "Private fields"

	Private ReadOnly _lockObject As Object = New Object()
	Private ReadOnly _files() As String

	Private _index As Integer = -1

#End Region

#Region "Public constructor"

	Public Sub New(ByVal directory As String)
		If directory Is Nothing OrElse (Not System.IO.Directory.Exists(directory)) Then
			Throw New ArgumentException("Directory doesn't exist")
		End If

		Dir = directory
		_files = System.IO.Directory.GetFiles(Dir)
	End Sub

#End Region

#Region "Public properties"

	Private privateDir As String
	Public Property Dir() As String
		Get
			Return privateDir
		End Get
		Private Set(ByVal value As String)
			privateDir = value
		End Set
	End Property

#End Region

#Region "ITemplateLoader members"

	Public Sub BeginLoad() Implements ITemplateLoader.BeginLoad
		SyncLock _lockObject
			If _index <> -1 Then
				Throw New InvalidOperationException()
			End If
			_index = 0
		End SyncLock
	End Sub

	Public Sub EndLoad() Implements ITemplateLoader.EndLoad
		SyncLock _lockObject
			_index = -1
		End SyncLock
	End Sub

	Public Function LoadNext(<System.Runtime.InteropServices.Out()> ByRef subjects() As NSubject, ByVal n As Integer) As Boolean Implements ITemplateLoader.LoadNext
		SyncLock _lockObject
			If _index = -1 Then
				Throw New InvalidOperationException()
			End If
			subjects = Nothing
			If _files.Length = 0 OrElse _files.Length <= _index Then
				Return False
			End If

			Dim count As Integer = _files.Length - _index
			count = If(count > n, n, count)
			subjects = New NSubject(count - 1) {}
			For i As Integer = 0 To count - 1
				Dim file As String = _files(_index)
				_index += 1
				subjects(i) = New NSubject()
				subjects(i).SetTemplateBuffer(New NBuffer(System.IO.File.ReadAllBytes(file)))
				subjects(i).Id = Path.GetFileNameWithoutExtension(file)
			Next i
			Return True
		End SyncLock
	End Function

	Public Sub Dispose() Implements ITemplateLoader.Dispose
	End Sub

	Public ReadOnly Property TemplateCount() As Integer Implements ITemplateLoader.TemplateCount
		Get
			Return _files.Length
		End Get
	End Property

#End Region

#Region "Public methods"

	Public Overrides Function ToString() As String
		Return Dir
	End Function

#End Region
End Class
