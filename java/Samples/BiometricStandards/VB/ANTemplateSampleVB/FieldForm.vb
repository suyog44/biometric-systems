Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class FieldForm
	Inherits Form
	#Region "Public static methods"

	Public Shared Sub GetFieldValue(ByVal field As ANField, ByVal value As StringBuilder)
		value.Length = 0
		Dim manySubFields As Boolean = field.SubFields.Count > 1
		Dim sfi As Integer = 0
		For Each subField As ANSubField In field.SubFields
			If sfi <> 0 Then
				value.Append(","c)
			End If
			If manySubFields Then
				value.Append("{"c)
			End If
			Dim ii As Integer = 0
			For Each item As String In subField.Items
				If ii <> 0 Then
					value.Append("|"c)
				End If
				value.Append(item)
				ii += 1
			Next item
			If manySubFields Then
				value.Append("}"c)
			End If
			sfi += 1
		Next subField
	End Sub

	#End Region

	#Region "Private fields"

	Private _field As ANField
	Private _isReadOnly As Boolean
	Private _isModified As Boolean

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		OnFieldChanged()
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub UpdateField()
		If _field IsNot Nothing Then
			Dim value As New StringBuilder()
			GetFieldValue(_field, value)
			fieldValueLabel.Text = value.ToString()
		Else
			fieldValueLabel.Text = String.Empty
		End If
		editFieldButton.Enabled = _field IsNot Nothing AndAlso _field.SubFields.Count = 1 AndAlso _field.SubFields(0).Items.Count = 1
		_isModified = True
	End Sub

	Private Sub OnFieldChanged()
		subFieldListBox.BeginUpdate()
		subFieldListBox.Items.Clear()
		If _field IsNot Nothing Then
			Dim value As New StringBuilder()
			For Each subField As ANSubField In _field.SubFields
				GetSubFieldValue(subField, value)
				subFieldListBox.Items.Add(value.ToString())
			Next subField
			subFieldListBox.SelectedIndex = 0
		End If
		subFieldListBox.EndUpdate()
		UpdateField()
		_isModified = False
	End Sub

	Private Sub EditField()
		Dim form As New ItemForm()
		form.Value = _field.Value
		form.Text = "Edit Field"
		form.IsReadOnly = _isReadOnly
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			_field.Value = form.Value
			UpdateSelectedSubField() ' couse we need to update subfield also
		End If
	End Sub

	Private Sub OnIsReadOnlyChanged()
		UpdateSubFieldControls()
		UpdateItemControls()
	End Sub

	Private Function GetSelectedSubField() As ANSubField
		If subFieldListBox.SelectedIndices.Count = 1 Then
			Return _field.SubFields(subFieldListBox.SelectedIndex)
		Else
			Return Nothing
		End If
	End Function

	Private Sub GetSubFieldValue(ByVal subField As ANSubField, ByVal value As StringBuilder)
		value.Length = 0
		Dim ii As Integer = 0
		For Each item As String In subField.Items
			If ii <> 0 Then
				value.Append("|"c)
			End If
			value.Append(item)
			ii += 1
		Next item
	End Sub

	Private Sub OnSelectedSubFieldChanged()
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		itemListBox.BeginUpdate()
		itemListBox.Items.Clear()
		If selectedSubField IsNot Nothing Then
			For Each item As String In selectedSubField.Items
				itemListBox.Items.Add(item)
			Next item
			itemListBox.SelectedIndex = 0
		End If
		itemListBox.EndUpdate()
		UpdateSubFieldControls()
	End Sub

	Private Sub UpdateSubFieldControls()
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		addSubFieldButton.Enabled = _field IsNot Nothing AndAlso Not _isReadOnly
		insertSubFieldButton.Enabled = (Not _isReadOnly) AndAlso selectedSubField IsNot Nothing
		Dim sc As Integer = subFieldListBox.SelectedIndices.Count
		removeSubFieldButton.Enabled = (Not _isReadOnly) AndAlso sc <> 0 AndAlso sc <> _field.SubFields.Count
		editSubFieldButton.Enabled = selectedSubField IsNot Nothing AndAlso selectedSubField.Items.Count = 1
	End Sub

	Private Sub UpdateSubField(ByVal index As Integer)
		Dim subFieldValue As New StringBuilder()
		GetSubFieldValue(_field.SubFields(index), subFieldValue)
		subFieldListBox.Items(index) = subFieldValue.ToString()
		UpdateField()
	End Sub

	Private Sub UpdateSelectedSubField()
		UpdateSubField(subFieldListBox.SelectedIndex)
	End Sub

	Private Sub EditSubField()
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		Dim form As New ItemForm()
		form.Value = selectedSubField.Value
		form.Text = "Edit Subfield"
		form.IsReadOnly = _isReadOnly
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			selectedSubField.Value = form.Value
			UpdateSelectedSubField()
		End If
	End Sub

	Private Function GetSelectedItemIndex() As Integer

		Return CInt(IIf(itemListBox.SelectedIndices.Count = 1, itemListBox.SelectedIndex, -1))
	End Function

	Private Sub OnSelectedItemChanged()
		UpdateItemControls()
	End Sub

	Private Sub UpdateItemControls()
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		Dim selectedItemIndex As Integer = GetSelectedItemIndex()
		addItemButton.Enabled = selectedSubField IsNot Nothing AndAlso Not _isReadOnly
		insertItemButton.Enabled = (Not _isReadOnly) AndAlso selectedItemIndex <> -1
		Dim sc As Integer = itemListBox.SelectedIndices.Count
		removeItemButton.Enabled = (Not _isReadOnly) AndAlso sc <> 0 AndAlso sc <> selectedSubField.Items.Count
		editItemButton.Enabled = selectedItemIndex <> -1
	End Sub

	Private Sub EditItem()
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		Dim selectedItemIndex As Integer = GetSelectedItemIndex()
		Dim form As New ItemForm()
		form.Value = selectedSubField.Items(selectedItemIndex)
		form.IsReadOnly = _isReadOnly
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			selectedSubField.Items(selectedItemIndex) = form.Value
			UpdateSelectedSubField()
			itemListBox.SelectedIndex = -1
			itemListBox.SelectedIndex = selectedItemIndex
		End If
	End Sub

	#End Region

	#Region "Public properties"

	Public Property Field() As ANField
		Get
			Return _field
		End Get
		Set(ByVal value As ANField)
			If _field IsNot value Then
				_field = value
				OnFieldChanged()
			End If
		End Set
	End Property

	Public ReadOnly Property IsModified() As Boolean
		Get
			Return _isModified
		End Get
	End Property

	Public Property IsReadOnly() As Boolean
		Get
			Return _isReadOnly
		End Get
		Set(ByVal value As Boolean)
			If _isReadOnly <> value Then
				_isReadOnly = value
				OnIsReadOnlyChanged()
			End If
		End Set
	End Property

	#End Region

	#Region "Private form events"

	Private Sub FieldValueLabelDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles fieldValueLabel.DoubleClick
		If _field IsNot Nothing AndAlso _field.SubFields.Count = 1 AndAlso _field.SubFields(0).Items.Count = 1 Then
			EditField()
		End If
	End Sub

	Private Sub EditFieldButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles editFieldButton.Click
		EditField()
	End Sub

	Private Sub SubFieldListBoxSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles subFieldListBox.SelectedIndexChanged
		OnSelectedSubFieldChanged()
	End Sub

	Private Sub SubFieldListBoxDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles subFieldListBox.DoubleClick
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		If selectedSubField IsNot Nothing AndAlso selectedSubField.Items.Count = 1 Then
			EditSubField()
		End If
	End Sub

	Private Sub AddSubFieldButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles addSubFieldButton.Click
		Dim form As New ItemForm()
		form.Text = "Add Subfield"
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			_field.SubFields.Add(form.Value)
			subFieldListBox.Items.Add(String.Empty)
			Dim index As Integer = _field.SubFields.Count - 1
			UpdateSubField(index)
			subFieldListBox.SelectedIndex = -1
			subFieldListBox.SelectedIndex = index
		End If
	End Sub

	Private Sub InsertSubFieldButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles insertSubFieldButton.Click
		Dim index As Integer = subFieldListBox.SelectedIndex
		Dim form As New ItemForm()
		form.Text = "Insert Subfield"
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			_field.SubFields.Insert(index, form.Value)
			subFieldListBox.Items.Insert(index, String.Empty)
			UpdateSubField(index)
			subFieldListBox.SelectedIndex = -1
			subFieldListBox.SelectedIndex = index
		End If
	End Sub

	Private Sub RemoveSubFieldButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles removeSubFieldButton.Click
		subFieldListBox.BeginUpdate()
		Dim selCount As Integer = subFieldListBox.SelectedIndices.Count
		Dim selIndices(selCount - 1) As Integer
		subFieldListBox.SelectedIndices.CopyTo(selIndices, 0)
		Array.Sort(Of Integer)(selIndices)
		For i As Integer = selCount - 1 To 0 Step -1
			Dim index As Integer = selIndices(i)
			_field.SubFields.RemoveAt(index)
			subFieldListBox.Items.RemoveAt(index)
		Next i
		UpdateField()
		subFieldListBox.SelectedIndex = -1
		If selIndices(0) = _field.SubFields.Count Then
			subFieldListBox.SelectedIndex = _field.SubFields.Count - 1
		Else
			subFieldListBox.SelectedIndex = selIndices(0)
		End If
		subFieldListBox.EndUpdate()
	End Sub

	Private Sub EditSubFieldButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles editSubFieldButton.Click
		EditSubField()
	End Sub

	Private Sub ItemListBoxSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles itemListBox.SelectedIndexChanged
		OnSelectedItemChanged()
	End Sub

	Private Sub ItemListBoxDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles itemListBox.DoubleClick
		If GetSelectedItemIndex() <> -1 Then
			EditItem()
		End If
	End Sub

	Private Sub AddItemButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles addItemButton.Click
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		Dim form As New ItemForm()
		form.Text = "Add Item"
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Dim index As Integer = selectedSubField.Items.Add(form.Value)
			UpdateSelectedSubField()
			itemListBox.SelectedIndex = -1
			itemListBox.SelectedIndex = index
		End If
	End Sub

	Private Sub InsertItemButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles insertItemButton.Click
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		Dim form As New ItemForm()
		form.Text = "Insert Item"
		If form.ShowDialog() = Windows.Forms.DialogResult.OK Then
			Dim index As Integer = GetSelectedItemIndex()
			selectedSubField.Items.Insert(index, form.Value)
			UpdateSelectedSubField()
			itemListBox.SelectedIndex = -1
			itemListBox.SelectedIndex = index
		End If
	End Sub

	Private Sub RemoveItemButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles removeItemButton.Click
		itemListBox.BeginUpdate()
		Dim selectedSubField As ANSubField = GetSelectedSubField()
		Dim selCount As Integer = itemListBox.SelectedIndices.Count
		Dim selIndices(selCount - 1) As Integer
		itemListBox.SelectedIndices.CopyTo(selIndices, 0)
		Array.Sort(Of Integer)(selIndices)
		For i As Integer = selCount - 1 To 0 Step -1
			Dim index As Integer = selIndices(i)
			selectedSubField.Items.RemoveAt(index)
		Next i
		UpdateSelectedSubField()
		itemListBox.SelectedIndex = -1
		If selIndices(0) = selectedSubField.Items.Count Then
			itemListBox.SelectedIndex = selectedSubField.Items.Count - 1
		Else
			itemListBox.SelectedIndex = selIndices(0)
		End If
		itemListBox.EndUpdate()
	End Sub

	Private Sub EditItemButtonClick(ByVal sender As Object, ByVal e As EventArgs) Handles editItemButton.Click
		EditItem()
	End Sub

	Private Sub FieldFormShown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		If _field IsNot Nothing AndAlso _field.SubFields.Count = 1 AndAlso _field.SubFields(0).Items.Count = 1 AndAlso (Not IsReadOnly) Then
			EditField()
		End If
	End Sub

	#End Region
End Class
