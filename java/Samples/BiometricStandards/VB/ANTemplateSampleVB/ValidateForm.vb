Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class ValidateForm
	Inherits Form
	#Region "Private fields"

	Private _path As String
	Private _filter As String
	Private _validationLevel As ANValidationLevel = ANValidationLevel.Standard
	Private _flags As UInteger
	Private _currentFileName As String

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Public properties"

	Public Property Path() As String
		Get
			Return _path
		End Get
		Set(ByVal value As String)
			_path = value
		End Set
	End Property

	Public Property Filter() As String
		Get
			Return _filter
		End Get
		Set(ByVal value As String)
			_filter = value
		End Set
	End Property

	Public Property ValidationLevel() As ANValidationLevel
		Get
			Return _validationLevel
		End Get
		Set(ByVal value As ANValidationLevel)
			_validationLevel = value
		End Set
	End Property

	Public Property Flags() As UInteger
		Get
			Return _flags
		End Get
		Set(ByVal value As UInteger)
			_flags = value
		End Set
	End Property

	#End Region

	#Region "Private methods"

	Private Sub ExamineDir(ByVal path As String, ByVal filters() As String, ByVal fileNames As List(Of String))
		If backgroundWorker.CancellationPending Then
			Return
		End If
		backgroundWorker.ReportProgress(-1, path)
		For Each filter As String In filters
			If backgroundWorker.CancellationPending Then
				Exit For
			End If
			For Each fileName As String In Directory.GetFiles(path, filter)
				If backgroundWorker.CancellationPending Then
					Exit For
				End If
				fileNames.Add(fileName)
			Next fileName
		Next filter
		If backgroundWorker.CancellationPending Then
			Return
		End If
		For Each dirName As String In Directory.GetDirectories(path)
			If backgroundWorker.CancellationPending Then
				Exit For
			End If
			ExamineDir(dirName, filters, fileNames)
		Next dirName
	End Sub

	#End Region

	#Region "Private form events"

	Private Sub WorkerDoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles backgroundWorker.DoWork
		Dim cc As Integer = 0
		Dim fileNames As New List(Of String)()
		ExamineDir(_path, _filter.Split(";"c), fileNames)
		If (Not backgroundWorker.CancellationPending) Then
			Dim i As Integer = 0, c As Integer = fileNames.Count, twoC As Integer = c * 2
			For Each fileName As String In fileNames
				backgroundWorker.ReportProgress((i + c) \ twoC, fileName)
				If backgroundWorker.CancellationPending Then
					Exit For
				End If
				Try
					Using template As New ANTemplate(fileName, _validationLevel, _flags)
					End Using
				Catch ex As Exception
					backgroundWorker.ReportProgress(-2, ex)
				End Try
				i += 200
				cc += 1
			Next fileName
		End If
		e.Result = IIf(backgroundWorker.CancellationPending, -cc - 1, cc + 1)
	End Sub

	Private Sub WorkerProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) Handles backgroundWorker.ProgressChanged
		If e.ProgressPercentage = -1 Then
			progressLabel.Text = String.Format("Examing directory: {0}", e.UserState)
		ElseIf e.ProgressPercentage = -2 Then
			lbError.Items.Add(New ValidateErrorInfo(_currentFileName, CType(e.UserState, Exception)))
		Else
			_currentFileName = CStr(e.UserState)
			progressLabel.Text = String.Format("Examing file: {0}", e.UserState)
			progressBar.Value = e.ProgressPercentage
		End If
	End Sub

	Private Sub RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
		progressLabel.Text = String.Format("{0}: {1} error(s) in {2} file(s)", IIf(CInt(Fix(e.Result)) < 0, "Stopped", "Complete"), lbError.Items.Count, Math.Abs(CInt(Fix(e.Result))) - 1)
		progressBar.Visible = False
		btnStop.Enabled = False
		btnClose.Enabled = True
		If e.Error IsNot Nothing Then
			MessageBox.Show(e.Error.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End If
	End Sub

	Private Sub BtnStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
		backgroundWorker.CancelAsync()
	End Sub

	Private Sub LbErrorSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbError.SelectedIndexChanged
		tbError.Text = Nothing
		If lbError.SelectedItem IsNot Nothing Then
			Dim ei As ValidateErrorInfo = CType(lbError.SelectedItem, ValidateErrorInfo)
			tbError.AppendText(ei.Error.ToString())
			tbError.SelectionStart = 0
			tbError.ScrollToCaret()
		End If
	End Sub

	Private Sub ValidateFormShown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		backgroundWorker.RunWorkerAsync()
	End Sub

	#End Region
End Class

Friend Structure ValidateErrorInfo
	#Region "Private fields"

	Private _fileName As String
	Private _error As Exception

	#End Region

	#Region "Public constructor"

	Public Sub New(ByVal fileName As String, ByVal [error] As Exception)
		Me._fileName = fileName
		Me._error = [error]
	End Sub

	#End Region

	#Region "Public properties"

	Public ReadOnly Property FileName() As String
		Get
			Return _fileName
		End Get
	End Property

	Public ReadOnly Property [Error]() As Exception
		Get
			Return _error
		End Get
	End Property

	#End Region
End Structure
