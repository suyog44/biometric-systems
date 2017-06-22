Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class CharsetForm
	Inherits Form
#Region "Private fields"

	Private _version As NVersion = CType(0, NVersion)
	Private _useSelectMode As Boolean = True
	Private _useUserDefinedCharsetIndex As Boolean = False
	Private _standardCharsetIndicies() As Integer

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		lblUserDefinedCharsetIndicies.Text = String.Format("({0:000} - {1:000})", ANType1Record.CharsetUserDefinedFrom, ANType1Record.CharsetUserDefinedTo)
		OnUseSelectModeChanged()
	End Sub

#End Region

#Region "Private methods"

	Private Sub UpdateCharsets()
		Dim version As NVersion
		If _useSelectMode Then
			version = _version
		Else
			Version = ANTemplate.VersionCurrent
		End If
		Dim versions() As NVersion = ANTemplate.GetVersions()
		_standardCharsetIndicies = ANType1Record.GetStandardCharsetIndexes(version)
		lbStandardCharsets.BeginUpdate()
		lbStandardCharsets.Items.Clear()
		For Each charsetIndex As Integer In _standardCharsetIndicies
			Dim charsetItem As New ListViewItem(String.Format("{0:000}", charsetIndex))
			charsetItem.Tag = charsetIndex
			charsetItem.SubItems.Add(String.Format("{0} ({1})", ANType1Record.GetStandardCharsetName(version, charsetIndex), ANType1Record.GetStandardCharsetDescription(version, charsetIndex)))
			If (Not _useSelectMode) Then
				Dim knownVer As NVersion = version
				For Each v As NVersion In versions
					If ANType1Record.IsCharsetKnown(v, charsetIndex) Then
						knownVer = v
						Exit For
					End If
				Next v
				charsetItem.SubItems.Add(knownVer.ToString())
			End If
			lbStandardCharsets.Items.Add(charsetItem)
		Next charsetIndex
		lbStandardCharsets.EndUpdate()
	End Sub

	Private Sub OnVersionChanged()
		UpdateCharsets()
	End Sub

	Private Sub OnUseSelectModeChanged()
		UpdateCharsets()
		If _useSelectMode Then
			Dim index As Integer
			index = lbStandardCharsets.Columns.IndexOf(charsetVersionColumnHeader)
			If index <> -1 Then
				lbStandardCharsets.Columns.RemoveAt(index)
			End If
		Else
			SetUseUserDefinedCharsetIndex(False)
			Dim index As Integer
			index = lbStandardCharsets.Columns.IndexOf(charsetVersionColumnHeader)
			If index = -1 Then
				lbStandardCharsets.Columns.Add(charsetVersionColumnHeader)
			End If
		End If
		Dim sz As Size = New Size(0, ClientSize.Height)
		If _useSelectMode Then
			sz.Width = 370
		Else
			sz.Width = 430
		End If
		ClientSize = sz
		rbStandardCharset.Visible = _useSelectMode
		rbUserDefinedCharset.Visible = rbStandardCharset.Visible
		tbCharsetVersion.Visible = rbUserDefinedCharset.Visible
		charsetVersionLabel.Visible = tbCharsetVersion.Visible
		tbUserDefinedCharsetName.Visible = charsetVersionLabel.Visible
		userDefinedCharsetNameLabel.Visible = tbUserDefinedCharsetName.Visible
		tbUserDefinedCharsetIndex.Visible = userDefinedCharsetNameLabel.Visible
		userDefinedCharsetIndexLabel.Visible = tbUserDefinedCharsetIndex.Visible
		btnOk.Visible = userDefinedCharsetIndexLabel.Visible
		userDefinedCharsetsLabel.Visible = Not _useSelectMode
		standardCharsetsLabel.Visible = userDefinedCharsetsLabel.Visible
		btnCancel.Text = CType(IIf(_useSelectMode, "Cancel", "Close"), String)
		OnUseUserDefinedCharsetIndexChanged()
	End Sub

	Private Sub OnUseUserDefinedCharsetIndexChanged()
		rbStandardCharset.Checked = _useSelectMode AndAlso Not _useUserDefinedCharsetIndex
		lbStandardCharsets.Enabled = (Not _useSelectMode) OrElse Not _useUserDefinedCharsetIndex
		rbUserDefinedCharset.Checked = _useSelectMode AndAlso _useUserDefinedCharsetIndex
		tbUserDefinedCharsetName.Enabled = (Not _useSelectMode) OrElse _useUserDefinedCharsetIndex
		userDefinedCharsetNameLabel.Enabled = tbUserDefinedCharsetName.Enabled
		tbUserDefinedCharsetIndex.Enabled = userDefinedCharsetNameLabel.Enabled
		userDefinedCharsetIndexLabel.Enabled = tbUserDefinedCharsetIndex.Enabled
		OnSelectedCharsetIndexChanged()
	End Sub

	Private Sub SetUseUserDefinedCharsetIndex(ByVal value As Boolean)
		If _useUserDefinedCharsetIndex <> value Then
			_useUserDefinedCharsetIndex = value
			OnUseUserDefinedCharsetIndexChanged()
		End If
	End Sub

	Private Sub OnSelectedCharsetIndexChanged()
		btnOk.Enabled = _useSelectMode AndAlso (_useUserDefinedCharsetIndex OrElse lbStandardCharsets.SelectedIndices.Count <> 0)
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

	Public Property CharsetIndex() As Integer
		Get
			If _useUserDefinedCharsetIndex Then
				Dim value As Integer
				Return CInt(IIf(Integer.TryParse(tbUserDefinedCharsetIndex.Text, value), value, -1))
			ElseIf lbStandardCharsets.SelectedItems.Count = 0 Then
				Return -1
			Else
				Return CInt(Fix(lbStandardCharsets.SelectedItems(0).Tag))
			End If
		End Get
		Set(ByVal value As Integer)
			If _useUserDefinedCharsetIndex Then
				tbUserDefinedCharsetIndex.Text = CStr(IIf(value = -1, String.Empty, value.ToString()))
			Else
				If value = -1 Then
					lbStandardCharsets.SelectedItems.Clear()
				Else
					lbStandardCharsets.Items(Array.IndexOf(Of Integer)(_standardCharsetIndicies, value)).Selected = True
				End If
			End If
		End Set
	End Property

	Public Property CharsetName() As String
		Get
			If _useUserDefinedCharsetIndex Then
				Return tbUserDefinedCharsetName.Text
			ElseIf lbStandardCharsets.SelectedItems.Count = 0 Then
				Return Nothing
			Else
				Return ANType1Record.GetStandardCharsetName(CType(IIf(_useSelectMode, _version, ANTemplate.VersionCurrent), NVersion), CInt(Fix(lbStandardCharsets.SelectedItems(0).Tag)))
			End If
		End Get
		Set(ByVal value As String)
			If _useUserDefinedCharsetIndex Then
				tbUserDefinedCharsetName.Text = value
			End If
		End Set
	End Property

	Public Property CharsetVersion() As String
		Get
			Return tbCharsetVersion.Text
		End Get
		Set(ByVal value As String)
			tbCharsetVersion.Text = value
		End Set
	End Property

#End Region

#Region "Private form events"

	Private Sub LvStandardCharsetSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbStandardCharsets.SelectedIndexChanged
		OnSelectedCharsetIndexChanged()
	End Sub

	Private Sub LvStandardCharsetDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lbStandardCharsets.DoubleClick
		If _useSelectMode AndAlso CharsetIndex <> -1 Then
			DialogResult = System.Windows.Forms.DialogResult.OK
		End If
	End Sub

	Private Sub RbStandardCharsetClick(ByVal sender As Object, ByVal e As EventArgs) Handles rbStandardCharset.Click
		SetUseUserDefinedCharsetIndex(False)
	End Sub

	Private Sub RbUserDefinedCharsetClick(ByVal sender As Object, ByVal e As EventArgs) Handles rbUserDefinedCharset.Click
		SetUseUserDefinedCharsetIndex(True)
	End Sub

	Private Sub CharsetFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
		If DialogResult = System.Windows.Forms.DialogResult.OK Then
			If _useUserDefinedCharsetIndex Then
				Dim charsetIndex As Integer = Me.CharsetIndex
				Dim errorMessage As String = Nothing
				If charsetIndex = -1 Then
					errorMessage = "User defined charset index is invalid"
				ElseIf charsetIndex < 0 Then
					errorMessage = "User defined charset index is less than zero"
				ElseIf charsetIndex > ANType1Record.CharsetUserDefinedTo Then
					errorMessage = "User defined charset index is greater than maximal allowed value"
				ElseIf charsetIndex < ANType1Record.CharsetUserDefinedFrom OrElse charsetIndex > ANType1Record.CharsetUserDefinedTo Then
					errorMessage = "User defined charset index is not in user defined charset range"
				End If
				If errorMessage IsNot Nothing Then
					e.Cancel = True
					tbUserDefinedCharsetIndex.Focus()
					MessageBox.Show(errorMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
				End If
			End If
		End If
	End Sub

#End Region
End Class
