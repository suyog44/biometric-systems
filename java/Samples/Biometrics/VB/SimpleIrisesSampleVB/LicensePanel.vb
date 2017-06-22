Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Licensing
Imports System.Text

Partial Public Class LicensePanel
	Inherits UserControl
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private fields"

	Public Const Port As Integer = 5000
	Public Const Address As String = "/local"

	Private _requiredComponents As String = String.Empty
	Private _optionalComponents As String = String.Empty

#End Region

#Region "Public properties"

	Public Property RequiredComponents() As String
		Get
			Return _requiredComponents
		End Get
		Set(ByVal value As String)
			_requiredComponents = value
			rtbComponents.SelectionColor = Color.Black
			rtbComponents.Text = GetRequiredComponentsString()
			Dim [optional] As String = GetOptionalComponentsString()
			If (Not String.IsNullOrEmpty([optional])) Then
				rtbComponents.AppendText(", " & [optional])
			End If
		End Set
	End Property

	Public Property OptionalComponents() As String
		Get
			Return _optionalComponents
		End Get
		Set(ByVal value As String)
			_optionalComponents = value
			rtbComponents.SelectionColor = Color.Black
			rtbComponents.Text = GetRequiredComponentsString()
			Dim [optional] As String = GetOptionalComponentsString()
			If (Not String.IsNullOrEmpty([optional])) Then
				rtbComponents.AppendText(", " & [optional])
			End If
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Sub LicensePanelLoad(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
		If (Not DesignMode) Then
			RefreshComponentsStatus()
		End If
	End Sub

	Private Function GetRequiredComponentsString() As String
		Return If(_requiredComponents IsNot Nothing, _requiredComponents.Replace(",", ", "), String.Empty)
	End Function

	Private Function GetOptionalComponentsString() As String
		If _optionalComponents Is Nothing Then
			Return String.Empty
		End If

		Dim comps() As String = _optionalComponents.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
		If comps.Length = 0 Then
			Return String.Empty
		End If
		Dim result = New StringBuilder()
		For i As Integer = 0 To comps.Length - 1
			result.Append(comps(i))
			result.Append("(optional)")
			If i <> comps.Length - 1 Then
				result.Append(", ")
			End If
		Next i
		Return result.ToString()
	End Function

	Private Sub RefreshRequired()
		Dim text As String = rtbComponents.Text
		Try
			rtbComponents.Text = String.Empty
			Dim obtainedCount As Integer = 0
			Dim requiredComponents() As String = Me.RequiredComponents.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
			For i As Integer = 0 To requiredComponents.Length - 1
				Dim item As String = requiredComponents(i)
				rtbComponents.SelectionStart = rtbComponents.TextLength
				If NLicense.IsComponentActivated(item) Then
					rtbComponents.SelectionColor = Color.Green
					rtbComponents.AppendText(item)
					obtainedCount += 1
				Else
					rtbComponents.SelectionColor = Color.Red
					rtbComponents.AppendText(item)
				End If
				If i <> requiredComponents.Length - 1 Then
					rtbComponents.SelectionColor = Color.Black
					rtbComponents.AppendText(", ")
				End If
			Next i

			If obtainedCount = requiredComponents.Length Then
				lblStatus.Text = "Component licenses obtained"
				lblStatus.ForeColor = Color.Green
			Else
				lblStatus.Text = "Not all required licenses obtained"
				lblStatus.ForeColor = Color.Red
			End If
		Catch
			rtbComponents.SelectionColor = Color.Black
			rtbComponents.Text = text
			Throw
		End Try
	End Sub

	Private Sub RefreshOptional()
		Dim text As String = rtbComponents.Text
		Try
			rtbComponents.SelectionColor = Color.Black
			rtbComponents.AppendText(", ")
			Dim comps() As String = OptionalComponents.Split(New Char() {","c}, StringSplitOptions.RemoveEmptyEntries)
			For i As Integer = 0 To comps.Length - 1
				Dim item As String = comps(i)
				rtbComponents.SelectionStart = rtbComponents.TextLength
				rtbComponents.SelectionColor = If(NLicense.IsComponentActivated(item), Color.Green, Color.Red)
				rtbComponents.AppendText(String.Format("{0} (optional)", item))

				If i <> comps.Length - 1 Then
					rtbComponents.SelectionColor = Color.Black
					rtbComponents.AppendText(", ")
				End If
			Next i
		Catch
			rtbComponents.SelectionColor = Color.Black
			rtbComponents.Text = text
			Throw
		End Try
	End Sub

#End Region

#Region "Public methods"

	Public Sub RefreshComponentsStatus()
		Try
			RefreshRequired()
			RefreshOptional()
		Catch ex As Exception
			lblStatus.Text = String.Format("Failed to check components activation status. Error message: {0}", ex.Message)
			lblStatus.ForeColor = Color.Red
		End Try
	End Sub

#End Region
End Class
