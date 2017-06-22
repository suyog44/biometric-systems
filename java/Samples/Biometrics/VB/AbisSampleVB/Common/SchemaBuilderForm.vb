Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms
Imports Neurotec.Biometrics

Partial Public Class SchemaBuilderForm
	Inherits Form
#Region "Private types"

	Private Enum ColumnType
		Unknown = 0
		BiographicDataString
		BiographicDataInteger
		Gender
		Thumbnail
		EnrollData
		CustomDataString
		CustomDataInteger
		CustomDataBlob
	End Enum

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		For Each item In CType(System.Enum.GetValues(GetType(ColumnType)), ColumnType())
			If item <> ColumnType.Unknown Then
				cbType.Items.Add(item)
			End If
		Next item
		cbType.SelectedIndex = 0
	End Sub

#End Region

#Region "Private fields"

	Private _schema As SampleDbSchema

#End Region

#Region "Public properties"

	Private privateSchema As SampleDbSchema
	Public Property Schema() As SampleDbSchema
		Get
			Return privateSchema
		End Get
		Set(ByVal value As SampleDbSchema)
			privateSchema = value
		End Set
	End Property

#End Region

#Region "Private methods"

	Private Function GetColumnType(ByVal element As NBiographicDataElement) As ColumnType
		If element.Name = _schema.GenderDataName Then
			Return ColumnType.Gender
		ElseIf element.Name = _schema.EnrollDataName Then
			Return ColumnType.EnrollData
		ElseIf element.Name = _schema.ThumbnailDataName Then
			Return ColumnType.Thumbnail
		Else
			Dim isCustom As Boolean = _schema.CustomData.Elements.Contains(element)
			If element.DbType = NDBType.String Then
				Return If(isCustom, ColumnType.CustomDataString, ColumnType.BiographicDataString)
			End If
			If element.DbType = NDBType.Integer Then
				Return If(isCustom, ColumnType.CustomDataInteger, ColumnType.BiographicDataInteger)
			End If
			If element.DbType = NDBType.Blob AndAlso isCustom Then
				Return ColumnType.CustomDataBlob
			End If
		End If
		Return ColumnType.Unknown
	End Function

	Private Function GetGroup(ByVal type As ColumnType) As ListViewGroup
		Select Case type
			Case ColumnType.BiographicDataString, ColumnType.BiographicDataInteger
				Return listView.Groups("lvgBiographicData")
			Case ColumnType.Gender, ColumnType.Thumbnail, ColumnType.EnrollData
				Return listView.Groups("lvgSampleData")
			Case ColumnType.CustomDataString, ColumnType.CustomDataInteger, ColumnType.CustomDataBlob
				Return listView.Groups("lvgCustomData")
			Case Else
				Return Nothing
		End Select
	End Function

	Private Sub AddElement(ByVal element As NBiographicDataElement)
		Dim type As ColumnType = GetColumnType(element)
		Dim typeString As String = type.ToString()
		If type = ColumnType.Gender Then
			typeString = typeString + " (String)"
		ElseIf type = ColumnType.Thumbnail Or type = ColumnType.EnrollData Then
			typeString = typeString + " (Blob)"
		End If
		Dim lvi As New ListViewItem(type.ToString())
		lvi.SubItems.Add(element.Name)
		lvi.SubItems.Add(element.DbColumn)
		lvi.Group = GetGroup(type)
		lvi.Tag = element
		listView.Items.Add(lvi)
	End Sub

	Private Function CheckNameDoesNotConflict(ByVal name As String) As Boolean
		Dim subjectType = GetType(NSubject)
		Dim properties = subjectType.GetProperties()
		Return properties.FirstOrDefault(Function(p) p.Name = name) Is Nothing
	End Function

#End Region

#Region "Private form events"

	Private Sub SchemaBuilderFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		If Schema Is Nothing OrElse Schema.IsEmpty Then
			Throw New ArgumentNullException("Schema")
		End If

		_schema = New SampleDbSchema()
		_schema.SchemaName = Schema.SchemaName
		_schema.EnrollDataName = Schema.EnrollDataName
		_schema.ThumbnailDataName = Schema.ThumbnailDataName
		_schema.GenderDataName = Schema.GenderDataName
		_schema.BiographicData = NBiographicDataSchema.Parse(Schema.BiographicData.ToString())
		_schema.CustomData = NBiographicDataSchema.Parse((If(Schema.CustomData, New NBiographicDataSchema())).ToString())

		For Each element In _schema.BiographicData.Elements
			AddElement(element)
		Next element
		For Each element In _schema.CustomData.Elements
			AddElement(element)
		Next element
	End Sub

	Private Sub BtnAddClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
		Dim name As String = tbName.Text.Trim()
		Dim dbColumn As String = tbDbColumn.Text.Trim()
		If name = String.Empty Then
			Utilities.ShowInformation("Name field can not be empty")
			tbName.Focus()
		ElseIf Not CheckNameDoesNotConflict(name) Then
			Utilities.ShowInformation("Name can not be same as NSubject property name")
			tbName.Focus()
		Else
			Dim type As ColumnType = CType(cbType.SelectedItem, ColumnType)
			Dim isCustom As Boolean = type <> ColumnType.BiographicDataInteger AndAlso type <> ColumnType.BiographicDataString AndAlso type <> ColumnType.Gender
			Dim element As New NBiographicDataElement()
			element.Name = name
			element.DbColumn = dbColumn
			If type = ColumnType.CustomDataBlob OrElse type = ColumnType.EnrollData OrElse type = ColumnType.Thumbnail Then
				element.DbType = NDBType.Blob
			ElseIf type = ColumnType.BiographicDataInteger OrElse type = ColumnType.CustomDataInteger Then
				element.DbType = NDBType.Integer
			Else
				element.DbType = NDBType.String
			End If

			Dim elements = Enumerable.Union(_schema.BiographicData.Elements, _schema.CustomData.Elements).ToList()
			If elements.Exists(Function(x) String.Compare(x.Name, element.Name, True) = 0 Or String.Compare(x.DbColumn, element.Name, True) = 0) Then
				Utilities.ShowInformation("Item with same name or db column name already exists")
				tbName.Focus()
				Return
			ElseIf (Not String.IsNullOrEmpty(element.DbColumn)) Then
				If (elements.Exists(Function(x) String.Compare(x.DbColumn, element.DbColumn, True) = 0)) Or (elements.Exists(Function(y) String.Compare(y.Name, element.DbColumn, True) = 0)) Then
					Utilities.ShowInformation("Item with same name or column name already exists")
					tbDbColumn.Focus()
					Return
				End If
			Else
				If type = ColumnType.Gender AndAlso _schema.GenderDataName IsNot Nothing Then
					Utilities.ShowInformation("Gender field already exists")
					Return
				ElseIf type = ColumnType.EnrollData AndAlso _schema.EnrollDataName IsNot Nothing Then
					Utilities.ShowInformation("Enroll data field already exists")
					Return
				ElseIf type = ColumnType.Thumbnail AndAlso _schema.ThumbnailDataName IsNot Nothing Then
					Utilities.ShowInformation("Thumbnail data field already exists")
					Return
				End If
				End If

				If isCustom Then
					_schema.CustomData.Elements.Add(element)
				Else
					_schema.BiographicData.Elements.Add(element)
				End If

				If type = ColumnType.Gender Then
					_schema.GenderDataName = element.Name
				ElseIf type = ColumnType.Thumbnail Then
					_schema.ThumbnailDataName = element.Name
				ElseIf type = ColumnType.EnrollData Then
					_schema.EnrollDataName = element.Name
				End If

				AddElement(element)

				tbDbColumn.Text = String.Empty
				tbName.Text = String.Empty
		End If
	End Sub

	Private Sub BtnOkClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnOk.Click
		Schema = _schema
		DialogResult = System.Windows.Forms.DialogResult.OK
	End Sub

	Private Sub ListViewSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles listView.SelectedIndexChanged
		btnDelete.Enabled = listView.SelectedItems.Count > 0
	End Sub

	Private Sub BtnDeleteClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
		Dim selected As ListViewItem = listView.SelectedItems(0)
		Dim element As NBiographicDataElement = CType(selected.Tag, NBiographicDataElement)
		_schema.BiographicData.Elements.Remove(element)
		_schema.CustomData.Elements.Remove(element)
		If element.Name = _schema.GenderDataName Then
			_schema.GenderDataName = Nothing
		ElseIf element.Name = _schema.ThumbnailDataName Then
			_schema.ThumbnailDataName = Nothing
		ElseIf element.Name = _schema.EnrollDataName Then
			_schema.EnrollDataName = Nothing
		End If
		selected.Remove()
	End Sub

#End Region
End Class
