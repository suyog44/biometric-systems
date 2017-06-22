Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class RecordTypeForm
	Inherits Form
	#Region "Private fields"

	Private _version As NVersion = CType(0, NVersion)
	Private _useSelectMode As Boolean = True

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		OnUseSelectModeChanged()
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub UpdateRecords()
		lvRecordType.BeginUpdate()
		lvRecordType.Items.Clear()
		For Each recordType As ANRecordType In ANRecordType.Types
			Dim recordVersion As NVersion = recordType.Version
			If (Not _useSelectMode) OrElse (recordType.Number <> 1 AndAlso recordVersion >= _version) Then
				Dim recordTypeItem As New ListViewItem(recordType.Number.ToString())
				recordTypeItem.Tag = recordType
				recordTypeItem.SubItems.Add(recordType.Name)
				If (Not _useSelectMode) Then
					recordTypeItem.SubItems.Add(recordType.DataType.ToString())
					recordTypeItem.SubItems.Add(recordVersion.ToString())
				End If
				lvRecordType.Items.Add(recordTypeItem)
			End If
		Next recordType
		lvRecordType.EndUpdate()
	End Sub

	Private Sub OnVersionChanged()
		UpdateRecords()
	End Sub

	Private Sub OnUseSelectModeChanged()
		UpdateRecords()
		If _useSelectMode Then
			Dim index As Integer
			index = lvRecordType.Columns.IndexOf(recordTypeDataTypeColumnHeader)
			If index <> -1 Then
				lvRecordType.Columns.RemoveAt(index)
			End If
			index = lvRecordType.Columns.IndexOf(recordTypeVersionColumnHeader)
			If index <> -1 Then
				lvRecordType.Columns.RemoveAt(index)
			End If
		Else
			Dim index As Integer
			index = lvRecordType.Columns.IndexOf(recordTypeDataTypeColumnHeader)
			If index = -1 Then
				lvRecordType.Columns.Add(recordTypeDataTypeColumnHeader)
			End If
			index = lvRecordType.Columns.IndexOf(recordTypeVersionColumnHeader)
			If index = -1 Then
				lvRecordType.Columns.Add(recordTypeVersionColumnHeader)
			End If
		End If
		ClientSize = New Size(CInt(IIf(_useSelectMode, 380, 530)), ClientSize.Height)
		btnShowFields.Visible = Not _useSelectMode
		btnOk.Visible = _useSelectMode
		btnCancel.Text = CStr(IIf(_useSelectMode, "Cancel", "Close"))
		OnSelectedRecordTypeChanged()
	End Sub

	Private Sub OnSelectedRecordTypeChanged()
		Dim selectedRecordType As ANRecordType = RecordType
		btnShowFields.Enabled = (Not _useSelectMode) AndAlso selectedRecordType IsNot Nothing
		btnOk.Enabled = _useSelectMode AndAlso selectedRecordType IsNot Nothing
	End Sub

	Private Sub ShowFields()
		Dim form As New FieldNumberForm()
		form.Text = "Fields"
		form.UseSelectMode = False
		form.RecordType = RecordType
		form.ShowDialog()
	End Sub

	#End Region

	#Region "Public properties"

	Public Property Version() As NVersion
		Get
			Return _version
		End Get
		Set(ByVal value As NVersion)
			If _version <> value Then
				_version = value
				OnVersionChanged()
			End If
		End Set
	End Property

	Public Property UseSelectMode() As Boolean
		Get
			Return _useSelectMode
		End Get
		Set(ByVal value As Boolean)
			If _useSelectMode <> value Then
				_useSelectMode = value
				OnUseSelectModeChanged()
			End If
		End Set
	End Property

	Public Property RecordType() As ANRecordType
		Get
			If lvRecordType.SelectedItems.Count = 0 Then
				Return Nothing
			Else
				Return CType(lvRecordType.SelectedItems(0).Tag, ANRecordType)
			End If
		End Get
		Set(ByVal value As ANRecordType)
			If value Is Nothing Then
				lvRecordType.SelectedItems.Clear()
			Else
				lvRecordType.Items(ANRecordType.Types.IndexOf(value)).Selected = True
			End If
		End Set
	End Property

	#End Region

	#Region "private form events"

	Private Sub LvRecordTypeSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvRecordType.SelectedIndexChanged
		OnSelectedRecordTypeChanged()
	End Sub

	Private Sub LvRecordTypeDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvRecordType.DoubleClick
		If RecordType IsNot Nothing Then
			If _useSelectMode Then
				DialogResult = Windows.Forms.DialogResult.OK
			Else
				ShowFields()
			End If
		End If
	End Sub

	Private Sub BtnShowFieldsClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnShowFields.Click
		ShowFields()
	End Sub

	#End Region
End Class
