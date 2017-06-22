Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms

Partial Public Class LongTaskForm
	Inherits Form
	Private callback As DoWorkEventHandler
	Private argument As Object
	Private results As RunWorkerCompletedEventArgs
	Private [error] As Exception

	Private Sub New(ByVal title As String, ByVal callback As DoWorkEventHandler)
		InitializeComponent()

		lblTitle.Text = title
		Me.callback = callback
	End Sub

	Public Shared Function RunLongTask(ByVal title As String, ByVal callback As DoWorkEventHandler, ByVal arg As Object) As RunWorkerCompletedEventArgs
		Using frmLongTask As LongTaskForm = New LongTaskForm(title, callback)
			frmLongTask.argument = arg
			frmLongTask.ShowDialog()
			If frmLongTask.error IsNot Nothing Then
				Throw frmLongTask.error
			End If
			Return frmLongTask.results
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
		Else
			Using frmLongTask As LongTaskForm = New LongTaskForm(title, callback)
				frmLongTask.argument = arg
				frmLongTask.ShowDialog()
				If frmLongTask.error IsNot Nothing Then
					Return New RunWorkerCompletedEventArgs(frmLongTask.results, frmLongTask.error, False)

				End If
				Return frmLongTask.results
			End Using
		End If
	End Function

	Private Sub LongTaskForm_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		'bwLongTask_DoWork(sender, new DoWorkEventArgs(argument));
		bwLongTask.RunWorkerAsync(argument)
		'this.Close();
	End Sub

	Private Sub DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles bwLongTask.DoWork
		Try
			If callback IsNot Nothing Then
				callback(sender, e)
			End If
		Catch ex As Exception
			[error] = ex
		End Try
	End Sub

	Private Sub RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles bwLongTask.RunWorkerCompleted
		results = e
		Close()
	End Sub
End Class
