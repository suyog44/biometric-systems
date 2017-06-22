Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms

Public Class Utilities
	Private Const ERROR_TITLE As String = "Error"
	Private Const INFORMATION_TITLE As String = "Information"
	Private Const QUESTION_TITLE As String = "Question"

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
		MessageBox.Show(message, GetCurrentApplicationName() & ": " & ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
		MessageBox.Show(message, GetCurrentApplicationName() & ": " & INFORMATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information)
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
		If DialogResult.Yes = MessageBox.Show(owner, message, GetCurrentApplicationName() & ": " & QUESTION_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
			Return True
		End If
		Return False
	End Function

	''' <summary>
	''' Gets all filenames for the provided path and pattern.
	''' </summary>
	''' <param name="baseDir"></param>
	''' <param name="pattern"></param>
	''' <returns></returns>
	Public Shared Function GetAllFileNames(ByVal baseDir As String, ByVal pattern As String) As String()
		Dim fileResults As New List(Of String)()

		Dim directoryStack As New Stack(Of String)()
		directoryStack.Push(baseDir)

		Do While directoryStack.Count > 0
			Dim currentDir As String = directoryStack.Pop()

			For Each fileName As String In Directory.GetFiles(currentDir, pattern)
				fileResults.Add(fileName)
			Next fileName
		Loop

		Return fileResults.ToArray()
	End Function

	Public Shared Function MatchingThresholdEqual(ByVal thresholdValue As Integer, ByVal percentString As String) As Boolean
		If percentString Is Nothing Then
			Return False
		End If
		Dim intVal As Integer = MatchingThresholdFromString(percentString)
		Return (intVal = thresholdValue)
	End Function

	Public Shared Function MatchingThresholdToString(ByVal value As Integer) As String
		Dim p As Double = -value / 12.0
		Return String.Format(String.Format("{{0:P{0}}}", Math.Max(0, CInt(Fix(Math.Ceiling(-p))) - 2)), Math.Pow(10, p))
	End Function

	Public Shared Function MatchingThresholdFromString(ByVal value As String) As Integer
		Dim p As Double = Math.Log10(Math.Max(Double.Epsilon, Math.Min(1, Double.Parse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "")) / 100)))
		Return Math.Max(0, CInt(Fix(Math.Round(-12 * p))))
	End Function

	Public Shared Function GetUserLocalDataDir(ByVal productName As String) As String
		Dim localDataDir As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
		localDataDir = Path.Combine(localDataDir, "Neurotechnology")
		If (Not Directory.Exists(localDataDir)) Then
			Directory.CreateDirectory(localDataDir)
		End If
		localDataDir = Path.Combine(localDataDir, productName)
		If (Not Directory.Exists(localDataDir)) Then
			Directory.CreateDirectory(localDataDir)
		End If

		Return localDataDir
	End Function
End Class
