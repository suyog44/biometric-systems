Imports System
Imports System.Windows.Forms

Partial Public Class LongActionDialog
	Inherits Form
#Region "Private constructor"

	Private Sub New(ByVal title As String)
		InitializeComponent()
		lblTitle.Text = title
	End Sub

#End Region

#Region "Private fields"

	Private _canClose As Boolean = False
	Private _target As System.Delegate
	Private _args() As Object
	Private _targetInvoke As Action
	Private _asyncResult As IAsyncResult
	Private _targetResult As Object

#End Region

#Region "Public static methods"

	Public Overloads Shared Function ShowDialog(ByVal owner As IWin32Window, ByVal title As String, ByVal target As System.Delegate, ByVal ParamArray args() As Object) As Object
		Dim dialog As New LongActionDialog(title) With {._target = target, ._args = args}
		dialog.ShowDialog()
		Return dialog.CompleteAction()
	End Function

	Public Overloads Shared Function ShowDialog(ByVal owner As IWin32Window, ByVal title As String, ByVal target As Action) As Object
		Return ShowDialog(owner, title, target, Nothing)
	End Function

#End Region

#Region "Private methods"

	Private Function CompleteAction() As Object
		_targetInvoke.EndInvoke(_asyncResult)
		Return _targetResult
	End Function

	Private Sub ActionDynamicInvoke()
		_targetResult = _target.DynamicInvoke(_args)
	End Sub

	Private Sub StartAction()
		_targetInvoke = AddressOf ActionDynamicInvoke
		_targetInvoke.BeginInvoke(AddressOf OnActionCompleted, Nothing)
	End Sub

	Private Sub OnActionCompleted(ByVal r As IAsyncResult)
		BeginInvoke(New AsyncCallback(AddressOf InvokeCompleted), r)
	End Sub
	Private Sub InvokeCompleted(ByVal result As Object)
		_asyncResult = result
		_canClose = True
		Close()
	End Sub

	Private Sub LongTaskFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
		e.Cancel = Not _canClose
	End Sub

	Private Sub LongActionDialogShown(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Shown
		StartAction()
	End Sub

#End Region
End Class
