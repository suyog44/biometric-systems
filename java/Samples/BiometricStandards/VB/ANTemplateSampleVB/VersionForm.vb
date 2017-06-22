Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class VersionForm
	Inherits Form
	#Region "Private fields"

	Private _useSelectMode As Boolean = True
	Private ReadOnly _versions() As NVersion

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		_versions = ANTemplate.GetVersions()
		lbVersions.BeginUpdate()
		For Each version As NVersion In _versions
			Dim versionItem As New ListViewItem(version.ToString())
			versionItem.Tag = version
			versionItem.SubItems.Add(ANTemplate.GetVersionName(version))
			lbVersions.Items.Add(versionItem)
		Next version
		lbVersions.EndUpdate()
		OnUseSelectModeChanged()
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub OnUseSelectModeChanged()
		btnOk.Visible = _useSelectMode
		btnCancel.Text = CStr(IIf(_useSelectMode, "Cancel", "Close"))
		OnSelectedVersionChanged()
	End Sub

	Private Sub OnSelectedVersionChanged()
		btnOk.Enabled = _useSelectMode AndAlso lbVersions.SelectedIndices.Count <> 0
	End Sub

	#End Region

	#Region "Private form events"

	Private Sub LvVersionsSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles lbVersions.SelectedIndexChanged
		OnSelectedVersionChanged()
	End Sub

	Private Sub LvVersionsDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles lbVersions.DoubleClick
		If _useSelectMode AndAlso SelectedVersion <> CType(0, NVersion) Then
			DialogResult = Windows.Forms.DialogResult.OK
		End If
	End Sub

	#End Region

	#Region "Public properties"

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

	Public Property SelectedVersion() As NVersion
		Get
			If lbVersions.SelectedItems.Count = 0 Then
				Return CType(0, NVersion)
			Else
				Return CType(lbVersions.SelectedItems(0).Tag, NVersion)
			End If
		End Get
		Set(ByVal value As NVersion)
			If value = CType(0, NVersion) Then
				lbVersions.SelectedItems.Clear()
			Else
				lbVersions.Items(Array.IndexOf(Of NVersion)(_versions, value)).Selected = True
			End If
		End Set
	End Property

	#End Region
End Class
