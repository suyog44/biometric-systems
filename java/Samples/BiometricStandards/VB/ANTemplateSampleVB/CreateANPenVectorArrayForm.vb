Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class CreateANPenVectorArrayForm
	Inherits Form
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private fields"

	Private _vectors() As ANPenVector

	#End Region

	#Region "Public constructors"

	Public Property Vectors() As ANPenVector()
		Get
			Return _vectors
		End Get
		Set(ByVal value As ANPenVector())
			_vectors = value
			dataGridView.Rows.Clear()
			For Each item As ANPenVector In Vectors
				dataGridView.Rows.Add(item.X, item.Y, item.Pressure)
			Next item
		End Set
	End Property

	#End Region

	#Region "Private form events"

	Private Sub DataGridViewCellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dataGridView.CellEndEdit
		Dim row As DataGridViewRow = dataGridView.Rows(e.RowIndex)
		If row.IsNewRow Then
			Return
		End If
		Dim cell As DataGridViewCell = row.Cells(e.ColumnIndex)
		Dim cellValue As Object = cell.FormattedValue
		Dim isValid As Boolean = cellValue IsNot Nothing
		If isValid Then
			Dim isPressure As Boolean = e.ColumnIndex = 2
			If isPressure Then
				Dim value As Byte
				isValid = Byte.TryParse(cellValue.ToString(), value)
			Else
				Dim value As UShort
				isValid = UShort.TryParse(cellValue.ToString(), value)
			End If
		End If

		cell.Style.BackColor = CType(IIf(isValid, Color.White, Color.Red), Color)
	End Sub

	Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
		Dim isOk As Boolean = True
		Dim list As New List(Of ANPenVector)()
		For Each row As DataGridViewRow In dataGridView.Rows
			If row.IsNewRow Then
				Continue For
			End If

			Dim x As UShort = 0, y As UShort = 0
			Dim pressure As Byte = 0
			If (Not GetValue(row.Cells(0).Value, x)) Then
				isOk = False
				row.Cells(0).Style.BackColor = Color.Red
			End If
			If (Not GetValue(row.Cells(1).Value, y)) Then
				isOk = False
				row.Cells(1).Style.BackColor = Color.Red
			End If
			If (Not GetValue(row.Cells(2).Value, pressure)) Then
				isOk = False
				row.Cells(2).Style.BackColor = Color.Red
			End If
			If (Not isOk) Then
				Exit For
			End If
			list.Add(New ANPenVector(x, y, pressure))
		Next row

		If isOk Then
			Vectors = list.ToArray()
			DialogResult = Windows.Forms.DialogResult.OK
		End If
	End Sub

	#End Region

	#Region "Private methods"

	Private Function GetValue(ByVal cellValue As Object, <System.Runtime.InteropServices.Out()> ByRef value As UShort) As Boolean
		value = 0
		If cellValue Is Nothing Then
			Return False
		End If
		Return UShort.TryParse(cellValue.ToString(), value)
	End Function

	Private Function GetValue(ByVal cellValue As Object, <System.Runtime.InteropServices.Out()> ByRef value As Byte) As Boolean
		value = 0
		If cellValue Is Nothing Then
			Return False
		End If
		Return Byte.TryParse(cellValue.ToString(), value)
	End Function

	#End Region
End Class
