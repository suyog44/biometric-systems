Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class FieldNumberForm
	Inherits Form
#Region "Public static methods"

	Public Shared Function IsFieldStandard(ByVal recordType As ANRecordType, ByVal version As NVersion, ByVal fieldNumber As Integer, ByVal validationLevel As ANValidationLevel) As Boolean
		If fieldNumber = ANRecord.FieldLen Then
			Return True
		End If
		If recordType Is ANRecordType.Type1 AndAlso fieldNumber = ANType1Record.FieldVer Then
			Return True
		End If
		If recordType IsNot ANRecordType.Type1 AndAlso fieldNumber = ANRecord.FieldIdc Then
			Return True
		End If
		If recordType.DataType <> ANRecordDataType.Ascii AndAlso fieldNumber = ANRecord.FieldData Then
			Return True
		End If
		If validationLevel <> ANValidationLevel.Minimal Then
			Return recordType.IsFieldStandard(version, fieldNumber)
		End If
		If recordType Is ANRecordType.Type1 Then
			If fieldNumber = ANType1Record.FieldCnt Then
				Return True
			End If
		End If
		Return False
	End Function

#End Region

#Region "Private fields"

	Private _version As NVersion = CType(0, NVersion)
	Private _recordType As ANRecordType = Nothing
	Private _useSelectMode As Boolean = True
	Private _validationLevel As ANValidationLevel = ANValidationLevel.Standard
	Private _useUserDefinedFieldNumber As Boolean = True
	Private _maxFieldNumber As Integer
	Private _standardFieldNumbers() As Integer
	Private _userDefinedFieldNumbers() As NRange

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		OnUseSelectModeChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Sub AddNumbers(ByVal sb As StringBuilder, ByVal numbers() As NRange)
		Dim i As Integer = 0
		For Each range As NRange In numbers
			If i <> 0 Then
				sb.Append(", ")
			End If
			sb.AppendFormat("({0}.{1:000} - {0}.{2:000})", _recordType.Number, range.From, range.To)
			i += 1
		Next range
	End Sub

	Private Sub UpdateFields()
		Dim version As NVersion
		If _useSelectMode Then
			version = _version
		Else
			version = ANTemplate.VersionCurrent
		End If
		Dim versions As List(Of NVersion) = Nothing
		If (Not _useSelectMode) AndAlso _recordType IsNot Nothing Then
			Dim vers() As NVersion = ANTemplate.GetVersions()
			versions = New List(Of NVersion)(vers.Length)
			For Each v As NVersion In vers
				If v >= _recordType.Version Then
					versions.Add(v)
				End If
			Next v
		End If
		If _recordType IsNot Nothing Then
			_maxFieldNumber = _recordType.GetMaxFieldNumber(version)
			_standardFieldNumbers = _recordType.GetStandardFieldNumbers(version)
			Dim sb As New StringBuilder()
			If _useSelectMode Then
				If _validationLevel = ANValidationLevel.Minimal Then
					sb.AppendFormat("({0}.001 - {0}.{1:000})", _recordType.Number, _maxFieldNumber)
					sb.AppendLine()
					sb.Append("UDF: ")
				End If
				_userDefinedFieldNumbers = _recordType.GetUserDefinedFieldNumbers(version)
				If _userDefinedFieldNumbers.Length = 0 Then
					sb.Append("None")
				Else
					AddNumbers(sb, _userDefinedFieldNumbers)
				End If
			Else
				_userDefinedFieldNumbers = Nothing
				For Each v As NVersion In versions
					sb.AppendFormat("{0}: ", v)
					Dim udfNumbers() As NRange = _recordType.GetUserDefinedFieldNumbers(v)
					If udfNumbers.Length = 0 Then
						sb.Append("None")
					Else
						AddNumbers(sb, udfNumbers)
					End If
					sb.AppendLine()
				Next v
			End If
			userDefinedFieldNumbersLabel.Text = sb.ToString()
			userDefinedFieldNumbersLabel.Enabled = True
			rbUserDefinedField.Enabled = userDefinedFieldNumbersLabel.Enabled
		Else
			_maxFieldNumber = 0
			_standardFieldNumbers = Nothing
			_userDefinedFieldNumbers = Nothing
			userDefinedFieldNumbersLabel.Text = String.Empty
			userDefinedFieldNumbersLabel.Enabled = False
			rbUserDefinedField.Enabled = userDefinedFieldNumbersLabel.Enabled
		End If
		lvStandardField.BeginUpdate()
		lvStandardField.Items.Clear()
		If _standardFieldNumbers IsNot Nothing Then
			For Each fieldNumber As Integer In _standardFieldNumbers
				Dim isReadOnly As Boolean = IsFieldStandard(_recordType, version, fieldNumber, _validationLevel)
				If (Not _useSelectMode) OrElse (Not isReadOnly) Then
					Dim fieldItem As New ListViewItem(String.Format("{0}.{1:000}", _recordType.Number, fieldNumber))
					fieldItem.Tag = fieldNumber
					Dim id As String = _recordType.GetFieldId(version, fieldNumber)
					Dim name As String = _recordType.GetFieldName(version, fieldNumber)
					Dim isMandatory As Boolean = _recordType.IsFieldMandatory(version, fieldNumber)
					If id <> String.Empty Then
						name = String.Format("{0} ({1})", name, id)
					End If
					fieldItem.SubItems.Add(name)
					If (Not _useSelectMode) Then
						Dim knownVer As NVersion = version
						For Each v As NVersion In versions
							If _recordType.IsFieldKnown(v, fieldNumber) AndAlso _recordType.IsFieldStandard(v, fieldNumber) Then
								knownVer = v
								Exit For
							End If
						Next v
						fieldItem.SubItems.Add(knownVer.ToString())
					End If
					If (Not _useSelectMode) OrElse _validationLevel = ANValidationLevel.Minimal Then
						If isMandatory Then
							fieldItem.SubItems.Add("Yes")
						Else
							fieldItem.SubItems.Add("No")
						End If
					End If
					lvStandardField.Items.Add(fieldItem)
				End If
			Next fieldNumber
		End If
		lvStandardField.EndUpdate()
	End Sub

	Private Sub OnVersionChanged()
		UpdateFields()
	End Sub

	Private Sub OnRecordTypeChanged()
		UpdateFields()
	End Sub

	Private Sub UpdateGui()
		rbStandardField.Enabled = _validationLevel = ANValidationLevel.Minimal
		If _validationLevel = ANValidationLevel.Minimal Then
			rbUserDefinedField.Text = "Other field:"
		Else
			rbUserDefinedField.Text = "User-defined field:"
		End If
		Dim index As Integer = lvStandardField.Columns.IndexOf(fieldVersionColumnHeader)
		If index <> -1 Then
			lvStandardField.Columns.RemoveAt(index)
		End If
		index = lvStandardField.Columns.IndexOf(isFieldMantadoryColumnHeader)
		If index <> -1 Then
			lvStandardField.Columns.RemoveAt(index)
		End If
		If (Not _useSelectMode) Then
			index = lvStandardField.Columns.IndexOf(fieldVersionColumnHeader)
			If index = -1 Then
				lvStandardField.Columns.Add(fieldVersionColumnHeader)
			End If
		End If
		index = lvStandardField.Columns.IndexOf(isFieldMantadoryColumnHeader)
		If index = -1 Then
			lvStandardField.Columns.Add(isFieldMantadoryColumnHeader)
		End If
		If _useSelectMode Then
			ClientSize = New Size(440, ClientSize.Height)
		Else
			ClientSize = New Size(500, ClientSize.Height)
		End If
	End Sub

	Private Sub OnUseSelectModeChanged()
		UpdateFields()
		UpdateGui()
		If (Not _useSelectMode) Then
			SetUseUserDefinedFieldNumber(False)
		End If
		rbStandardField.Visible = _useSelectMode
		rbUserDefinedField.Visible = _useSelectMode
		tbUserDefinedField.Visible = _useSelectMode
		btnOk.Visible = _useSelectMode
		userDefinedFieldsLabel.Visible = Not _useSelectMode
		standardFieldsLabel.Visible = Not _useSelectMode
		If _useSelectMode Then
			btnCancel.Text = "Cancel"
		Else
			btnCancel.Text = "Close"
		End If
		OnUseUserDefinedFieldNumberChanged()
	End Sub

	Private Sub OnValidationLevelChanged()
		UpdateFields()
		UpdateGui()
		If _useSelectMode AndAlso _validationLevel <> ANValidationLevel.Minimal Then
			SetUseUserDefinedFieldNumber(True)
		End If
	End Sub

	Private Sub OnUseUserDefinedFieldNumberChanged()
		rbStandardField.Checked = _useSelectMode AndAlso Not _useUserDefinedFieldNumber
		lvStandardField.Enabled = (Not _useSelectMode) OrElse Not _useUserDefinedFieldNumber
		rbUserDefinedField.Checked = _useSelectMode AndAlso _useUserDefinedFieldNumber
		tbUserDefinedField.Enabled = (Not _useSelectMode) OrElse _useUserDefinedFieldNumber
		OnSelectedFieldNumberChanged()
	End Sub

	Private Sub SetUseUserDefinedFieldNumber(ByVal value As Boolean)
		If _useUserDefinedFieldNumber <> value Then
			_useUserDefinedFieldNumber = value
			OnUseUserDefinedFieldNumberChanged()
		End If
	End Sub

	Private Sub OnSelectedFieldNumberChanged()
		btnOk.Enabled = _useSelectMode AndAlso (_useUserDefinedFieldNumber OrElse lvStandardField.SelectedIndices.Count <> 0)
	End Sub

	Private Function IsUdfNumber(ByVal fieldNumber As Integer) As Boolean
		For Each range As NRange In _userDefinedFieldNumbers
			If fieldNumber >= range.From AndAlso fieldNumber <= range.To Then
				Return True
			End If
		Next range
		Return False
	End Function

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

	Public Property RecordType() As ANRecordType
		Get
			Return _recordType
		End Get
		Set(ByVal value As ANRecordType)
			If _recordType IsNot value Then
				_recordType = value
				OnRecordTypeChanged()
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

	Public Property ValidationLevel() As ANValidationLevel
		Get
			Return _validationLevel
		End Get
		Set(ByVal value As ANValidationLevel)
			If _validationLevel <> value Then
				_validationLevel = value
				OnValidationLevelChanged()
			End If
		End Set
	End Property

	Public Property FieldNumber() As Integer
		Get
			If _useUserDefinedFieldNumber Then
				Dim value As Integer
				Dim text As String = tbUserDefinedField.Text
				If (Not String.IsNullOrEmpty(text)) AndAlso text.Contains(".") Then
					text = text.Trim()
					Dim prefix As String = String.Format("{0}.", _recordType.Number)
					If text.StartsWith(prefix) Then
						text = text.Substring(prefix.Length)
					End If
				End If
				If Integer.TryParse(text, value) Then
					Return value
				Else
					Return -1
				End If
			End If
			If lvStandardField.SelectedItems.Count = 0 Then
				Return -1
			Else
				Return CInt(Fix(lvStandardField.SelectedItems(0).Tag))
			End If
		End Get
		Set(ByVal value As Integer)
			If _useUserDefinedFieldNumber Then
				If value = -1 Then
					tbUserDefinedField.Text = String.Empty
				Else
					tbUserDefinedField.Text = value.ToString()
				End If
			Else
				If value = -1 Then
					lvStandardField.SelectedItems.Clear()
				Else
					lvStandardField.Items(Array.IndexOf(Of Integer)(_standardFieldNumbers, value)).Selected = True
				End If
			End If
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub LvStandardFieldSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lvStandardField.SelectedIndexChanged
		OnSelectedFieldNumberChanged()
	End Sub

	Private Sub LvStandardFieldDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lvStandardField.DoubleClick
		If _useSelectMode AndAlso FieldNumber <> -1 Then
			DialogResult = System.Windows.Forms.DialogResult.OK
		End If
	End Sub

	Private Sub RbStandardFieldClick(ByVal sender As Object, ByVal e As EventArgs) Handles rbStandardField.Click
		SetUseUserDefinedFieldNumber(False)
	End Sub

	Private Sub RbUserDefinedFieldClick(ByVal sender As Object, ByVal e As EventArgs) Handles rbUserDefinedField.Click
		SetUseUserDefinedFieldNumber(True)
	End Sub

	Private Sub FieldNumberFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If DialogResult = System.Windows.Forms.DialogResult.OK Then
			If _useUserDefinedFieldNumber Then
				Dim fieldNumber As Integer = Me.FieldNumber
				Dim errorMessage As String
				If fieldNumber = -1 Then
					errorMessage = "User defined field number is invalid"
				Else
					If fieldNumber < 1 Then
						errorMessage = "User defined field number is less than one"
					Else
						If fieldNumber > _maxFieldNumber Then
							errorMessage = "User defined field number is greater than maximal allowed value"
						Else
							If _validationLevel <> ANValidationLevel.Minimal AndAlso (Not IsUdfNumber(fieldNumber)) Then
								errorMessage = "User defined field number is not in user defined field range"
							Else
								errorMessage = Nothing
							End If
						End If
					End If
				End If
				If errorMessage IsNot Nothing Then
					e.Cancel = True
					tbUserDefinedField.Focus()
					MessageBox.Show(errorMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
			End If
		End If
	End Sub

#End Region
End Class
