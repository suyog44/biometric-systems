Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms

Public Class Utilities
	Private Const ErrorTitle As String = "Error"
	Private Const InformationTitle As String = "Information"
	Private Const QuestionTitle As String = "Question"
	Private Const WarningTitle As String = "Warning"

	''' <summary>
	''' Gets location for current applicaiton folder.
	''' </summary>
	''' <returns></returns>
	Public Shared Function GetCurrentApplicationLocation() As String
		Return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar
	End Function

	''' <summary>
	''' Returns the current application name.
	''' </summary>
	''' <returns></returns>
	Public Shared Function GetCurrentApplicationName() As String
		Return System.Reflection.Assembly.GetEntryAssembly().GetName().Name
	End Function

	''' <summary>
	''' Returns the current application version.
	''' </summary>
	''' <returns></returns>
	Public Shared Function GetCurrentApplicationVersion() As Version
		Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
	End Function

	''' <summary>
	''' Shows the error message with the exclamation mark.
	''' </summary>
	''' <param name="message">Message to be displayed.</param>
	Public Shared Sub ShowError(ByVal message As String)
		MessageBox.Show(message, GetCurrentApplicationName() & ": " & ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
	End Sub

	''' <summary>
	''' Shows the error message with the exclamation mark.
	''' </summary>
	''' <param name="ex">Exception.</param>
	Public Shared Sub ShowError(ByVal ex As Exception)
		ShowError(ex.ToString())
	End Sub

	''' <summary>
	''' Shows the error message with the exclamation mark.
	''' </summary>
	Public Shared Sub ShowError(ByVal format As String, ParamArray ByVal args() As Object)
		Dim str As String = String.Format(format, args)
		ShowError(str)
	End Sub

	''' <summary>
	''' Shows the information message with the exclamation mark.
	''' </summary>
	''' <param name="message">Message to be displayed.</param>
	Public Shared Sub ShowInformation(ByVal message As String)
		MessageBox.Show(message, GetCurrentApplicationName() & ": " & InformationTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	''' <summary>
	''' Shows the information message with the exclamation mark.
	''' </summary>
	Public Shared Sub ShowInformation(ByVal format As String, ParamArray ByVal args() As Object)
		Dim str As String = String.Format(format, args)
		ShowInformation(str)
	End Sub

	''' <summary>
	''' Shows the question message with the question mark.
	''' </summary>
	''' <param name="message">Message to be displayed.</param>
	''' <returns>Returns the user response.</returns>
	Public Shared Function ShowQuestion(ByVal message As String) As Boolean
		Return ShowQuestion(CType(Nothing, IWin32Window), message)
	End Function

	''' <summary>
	''' Shows the question message with the question mark.
	''' </summary>
	''' <returns>Returns the user response.</returns>
	Public Shared Function ShowQuestion(ByVal format As String, ParamArray ByVal args() As Object) As Boolean
		Return ShowQuestion(CType(Nothing, IWin32Window), format, args)
	End Function

	Public Shared Function ShowQuestion(ByVal owner As IWin32Window, ByVal message As String, ParamArray ByVal args() As Object) As Boolean
		Dim str As String = String.Format(message, args)
		Return ShowQuestion(owner, str)
	End Function

	Public Shared Function ShowQuestion(ByVal owner As IWin32Window, ByVal message As String) As Boolean
		If DialogResult.Yes = MessageBox.Show(owner, message, GetCurrentApplicationName() & ": " & QuestionTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
			Return True
		End If
		Return False
	End Function

	Public Shared Sub ShowWarning(ByVal format As String, ParamArray ByVal args() As Object)
		ShowWarning(CType(Nothing, IWin32Window), format, args)
	End Sub

	Public Shared Sub ShowWarning(ByVal owner As IWin32Window, ByVal message As String, ParamArray ByVal args() As Object)
		ShowWarning(owner, String.Format(message, args))
	End Sub

	Public Shared Sub ShowWarning(ByVal owner As IWin32Window, ByVal message As String)
		MessageBox.Show(owner, message, GetCurrentApplicationName() & ": " & QuestionTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
	End Sub
End Class
