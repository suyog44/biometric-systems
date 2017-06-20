Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms

Namespace Forms
	Partial Public Class EditInfoForm
		Inherits Form
		#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			_enrollToServerColumn = New DataGridViewCheckBoxColumn()
			_enrollToServerColumn.HeaderText = "Enroll to server"
			_enrollToServerColumn.SortMode = DataGridViewColumnSortMode.NotSortable
		End Sub

		#End Region

		#Region "Private fields"

		Private _suspendEvents As Boolean
		Private ReadOnly _enrollToServerColumn As DataGridViewCheckBoxColumn
		Private _information() As InfoField

		#End Region

		#Region "Public properties"

		Public Property Information() As InfoField()
			Get
				Return _information
			End Get
			Set(ByVal value As InfoField())
				_information = value
			End Set
		End Property

		#End Region

		#Region "Private form events"

		Private Sub EditInfoFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			_suspendEvents = True

			dataGridView.Rows.Clear()
			For Each item As InfoField In Information
				dataGridView.Rows.Add(item.Key, item.IsEditable)
			Next item

			UpdateComboxes()
			cbThumbnailField.SelectedItem = My.Settings.InformationThumbnailField
			_suspendEvents = False
		End Sub

		Private Sub BtnUpClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnUp.Click
			Dim rows As DataGridViewSelectedRowCollection = dataGridView.SelectedRows
			If rows.Count > 0 Then
				Dim row As DataGridViewRow = rows(0)
				If row.Index <> 0 Then
					_suspendEvents = True

					Dim index As Integer = row.Index
					dataGridView.Rows.RemoveAt(index)
					index -= 1
					dataGridView.Rows.Insert(index, row)
					dataGridView.ClearSelection()
					dataGridView.Rows(index).Selected = True

					_suspendEvents = False
				End If
			End If
		End Sub

		Private Sub BtnDownClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDown.Click
			Dim rows As DataGridViewSelectedRowCollection = dataGridView.SelectedRows
			If rows.Count > 0 Then
				Dim row As DataGridViewRow = rows(0)
				If row.Index <> dataGridView.RowCount - 2 Then
					_suspendEvents = True

					Dim index As Integer = row.Index
					dataGridView.Rows.RemoveAt(index)
					index += 1
					dataGridView.Rows.Insert(index, row)
					dataGridView.ClearSelection()
					dataGridView.Rows(index).Selected = True

					_suspendEvents = False
				End If
			End If
		End Sub

		Private Sub DataGridViewRowsAdded(ByVal sender As Object, ByVal e As DataGridViewRowsAddedEventArgs) Handles dataGridView.RowsAdded
			If _suspendEvents Then
				Return
			End If
			UpdateComboxes()
		End Sub

		Private Sub DataGridViewRowsRemoved(ByVal sender As Object, ByVal e As DataGridViewRowsRemovedEventArgs) Handles dataGridView.RowsRemoved
			If _suspendEvents Then
				Return
			End If
			UpdateComboxes()
		End Sub

		Private Sub DataGridViewCellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dataGridView.CellEndEdit
			If e.ColumnIndex = 0 Then
				Dim thumbnail As Integer = cbThumbnailField.SelectedIndex
				UpdateComboxes()
				If thumbnail - 1 = e.RowIndex Then
					cbThumbnailField.SelectedIndex = thumbnail
				End If
			End If
		End Sub

		Private Sub BtnDeleteClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
			Dim rows As DataGridViewSelectedRowCollection = dataGridView.SelectedRows
			If rows.Count > 0 Then
				dataGridView.Rows.RemoveAt(rows(0).Index)
			End If
		End Sub

		Private Sub DataGridViewCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles dataGridView.CellValueChanged
			If _suspendEvents Then
				Return
			End If

			If e.ColumnIndex = 0 AndAlso e.RowIndex >= 0 Then
				Dim thumbnail As Integer = cbThumbnailField.SelectedIndex
				UpdateComboxes()
				If thumbnail - 1 = e.RowIndex Then
					cbThumbnailField.SelectedIndex = thumbnail
				End If
			End If
		End Sub

		Private Sub ComboBoxSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim combo As ComboBox = CType(sender, ComboBox)
			If dataGridView.ColumnCount > 1 Then
				dataGridView.Rows(combo.SelectedIndex).Cells(1).Value = True
			End If
		End Sub

		Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
			For Each row As DataGridViewRow In dataGridView.Rows
				If (Not row.IsNewRow) AndAlso String.IsNullOrEmpty(TryCast(row.Cells(0).Value, String)) Then
					Utilities.ShowError("Key value is invalid")
					dataGridView.ClearSelection()
					row.Selected = True
					Return
				End If
			Next row

			If dataGridView.Rows.Count <= 1 Then
				Utilities.ShowError("Create at least one row of information description")
				Return
			End If

			Dim hash As String = Nothing
			Dim template As String = Nothing
			Dim thumbnail As String = TryCast(cbThumbnailField.SelectedItem, String)
			My.Settings.InformationThumbnailField = thumbnail

			Dim fields As New List(Of InfoField)()
			Dim builder As New StringBuilder()
			For Each row As DataGridViewRow In dataGridView.Rows
				If row.IsNewRow Then
					Continue For
				End If

				Dim inf As New InfoField()
				inf.Key = TryCast(row.Cells(0).Value, String)
				inf.ShowAsThumbnail = thumbnail = inf.Key
				inf.IsEditable = (Not inf.ShowAsThumbnail) AndAlso Not (inf.Key = hash OrElse inf.Key = template)
				If dataGridView.ColumnCount > 1 AndAlso row.Cells(1).Value IsNot Nothing Then
					inf.EnrollToServer = CBool(row.Cells(1).Value)
				End If
				If inf.Key IsNot Nothing Then
					inf.Key = inf.Key.Trim()
				End If

				fields.Add(inf)
				builder.AppendFormat("{0};", inf)
			Next row

			My.Settings.Information = builder.ToString()
			My.Settings.Save()

			_information = fields.ToArray()

			DialogResult = System.Windows.Forms.DialogResult.OK
		End Sub

		#End Region

		#Region "Private methods"

		Private Sub UpdateComboxes()
			UpdateComboBox(cbThumbnailField, True)
		End Sub

		Private Sub UpdateComboBox(ByVal combo As ComboBox, ByVal allowEmpty As Boolean)
			combo.BeginUpdate()
			Try
				Dim selected As String = TryCast(combo.SelectedItem, String)
				combo.Items.Clear()
				If allowEmpty Then
					combo.Items.Add(String.Empty)
				End If
				For Each row As DataGridViewRow In dataGridView.Rows
					If row.Cells(0).Value IsNot Nothing Then
						combo.Items.Add(row.Cells(0).Value)
					End If
				Next row
				combo.SelectedItem = selected
			Finally
				combo.EndUpdate()
			End Try
		End Sub

		#End Region
	End Class
End Namespace
