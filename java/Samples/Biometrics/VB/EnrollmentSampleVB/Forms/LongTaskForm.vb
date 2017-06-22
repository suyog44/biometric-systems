Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Forms
	Partial Public Class LongTaskForm
		Inherits Form
		#Region "Private fields"

		Private ReadOnly _callback As DoWorkEventHandler
		Private _argument As Object
		Private _results As RunWorkerCompletedEventArgs
		Private _error As Exception

		#End Region

		#Region "Private constructor"

		Private Sub New(ByVal title As String, ByVal callback As DoWorkEventHandler)
			InitializeComponent()

			lblTitle.Text = title
			_callback = callback
		End Sub

		#End Region

		#Region "Public methods"

		Public Shared Function RunLongTask(ByVal title As String, ByVal callback As DoWorkEventHandler, ByVal arg As Object) As RunWorkerCompletedEventArgs
			Using frmLongTask As New LongTaskForm(title, callback)
				frmLongTask._argument = arg
				frmLongTask.ShowDialog()
				If frmLongTask._error IsNot Nothing Then
					Throw frmLongTask._error
				End If
				Return frmLongTask._results
			End Using
		End Function

		Private Delegate Function RunLongTaskDel(ByVal title As String, ByVal callback As DoWorkEventHandler, ByVal arg As Object, ByVal ctrl As Control) As RunWorkerCompletedEventArgs
		Public Shared Function RunLongTask(ByVal title As String, ByVal callback As DoWorkEventHandler, ByVal arg As Object, ByVal ctrl As Control) As RunWorkerCompletedEventArgs
			If ctrl.InvokeRequired Then
				Dim del As RunLongTaskDel = AddressOf RunLongTask
					Dim res As IAsyncResult = ctrl.BeginInvoke(del, title, callback, arg, ctrl)
					Dim args As RunWorkerCompletedEventArgs = CType(ctrl.EndInvoke(res), RunWorkerCompletedEventArgs)
					If args.Error IsNot Nothing Then
						Throw args.Error
					End If
					Return args
			End If
			Using frmLongTask As New LongTaskForm(title, callback)
				frmLongTask._argument = arg
				frmLongTask.ShowDialog()
				If frmLongTask._error IsNot Nothing Then
					Return New RunWorkerCompletedEventArgs(frmLongTask._results, frmLongTask._error, False)

				End If
				Return frmLongTask._results
			End Using
		End Function

		#End Region

		#Region "Private form events"

		Private Sub LongTaskFormShown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
			bwLongTask.RunWorkerAsync(_argument)
		End Sub

		Private Sub BwLongTaskDoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bwLongTask.DoWork
			Try
				If _callback IsNot Nothing Then
					_callback(sender, e)
				End If
			Catch ex As Exception
				_error = ex
			End Try
		End Sub

		Private Sub BwRefresherRunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bwLongTask.RunWorkerCompleted
			_results = e
			Close()
		End Sub

		#End Region
	End Class
End Namespace
