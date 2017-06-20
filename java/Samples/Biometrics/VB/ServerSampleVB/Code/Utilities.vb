Imports Microsoft.VisualBasic
Imports System
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Windows.Forms

Public Class Utilities
	Private Const ERROR_TITLE As String = "Error"
	Private Const INFORMATION_TITLE As String = "Information"
	Private Const QUESTION_TITLE As String = "Question"

	Public Shared Function GetCurrentApplicationLocation() As String
		Return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar
	End Function

	Public Shared Function GetCurrentApplicationName() As String
		Return Assembly.GetEntryAssembly().GetName().Name
	End Function

	Public Shared Sub ShowError(ByVal message As String)
		MessageBox.Show(message, GetCurrentApplicationName() & ": " & ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error)
	End Sub

	Public Shared Sub ShowError(ByVal ex As Exception)
		ShowError(ex.ToString())
	End Sub

	Public Shared Sub ShowError(ByVal format As String, ByVal ParamArray args() As Object)
		Dim str As String = String.Format(format, args)
		ShowError(str)
	End Sub

	Public Shared Sub ShowInformation(ByVal message As String)
		MessageBox.Show(message, GetCurrentApplicationName() & ": " & INFORMATION_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information)
	End Sub

	Public Shared Sub ShowInformation(ByVal format As String, ByVal ParamArray args() As Object)
		Dim str As String = String.Format(format, args)
		ShowInformation(str)
	End Sub

	Public Shared Function ShowQuestion(ByVal message As String) As Boolean
		If DialogResult.Yes = MessageBox.Show(message, GetCurrentApplicationName() & ": " & QUESTION_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question) Then
			Return True
		End If
		Return False
	End Function

	Public Shared Function ShowQuestion(ByVal format As String, ByVal ParamArray args() As Object) As Boolean
		Dim str As String = String.Format(format, args)
		Return ShowQuestion(str)
	End Function

	Public Shared Function MatchingThresholdToString(ByVal value As Integer) As String
		Dim p As Double = -value / 12.0
		Return String.Format(String.Format("{{0:P{0}}}", Math.Max(0, CInt(Fix(Math.Ceiling(-p))) - 2)), Math.Pow(10, p))
	End Function

	Public Shared Function MatchingThresholdFromString(ByVal value As String) As Integer
		Dim p As Double = Math.Log10(Math.Max(Double.Epsilon, Math.Min(1, Double.Parse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "")) / 100)))
		Return Math.Max(0, CInt(Fix(Math.Round(-12 * p))))
	End Function

	Public Shared Function MaximalRotationToDegrees(ByVal value As Byte) As Integer
		Return (2 * value * 360 + 256) / (2 * 256)
	End Function

	Public Shared Function MaximalRotationFromDegrees(ByVal value As Integer) As Byte
		Return CByte((2 * value * 256 + 360) / (2 * 360))
	End Function
End Class
