Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class OpenSubjectDialog
	Inherits Form
	#Region "Private types"

	Private Structure ListItem
		Private privateValue As UShort
		Public Property Value() As UShort
			Get
				Return privateValue
			End Get
			Set(ByVal value As UShort)
				privateValue = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Structure

	#End Region

	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub ListTypes()
		cbType.BeginUpdate()
		Try
			cbType.Items.Clear()

			Dim selected As ListItem = CType(cbOwner.SelectedItem, ListItem)
			Dim type = GetType(CbeffBdbFormatIdentifiers)
			Dim fields = type.GetFields(BindingFlags.Static Or BindingFlags.Public).Where(Function(x) x.Name.StartsWith(selected.Name))
			Dim ownerNameLength As Integer = selected.Name.Length
			For Each item As FieldInfo In fields
				Dim li As ListItem = New ListItem With {.Name = item.Name.Substring(ownerNameLength), .Value = CUShort(item.GetValue(Nothing))}
				cbType.Items.Add(li)
			Next item

			Dim count As Integer = cbType.Items.Count
			If count > 0 Then
				cbType.SelectedIndex = 0
			End If
			cbType.Enabled = count > 0
		Finally
			cbType.EndUpdate()
		End Try
	End Sub

	Private Sub ListOwners()
		cbOwner.BeginUpdate()
		Try
			cbOwner.Items.Clear()
			Dim items = New Object() { New ListItem With {.Name = "Auto detect", .Value = CbeffBiometricOrganizations.NotForUse}, New ListItem With {.Name = "Neurotechnologija", .Value = CbeffBiometricOrganizations.Neurotechnologija}, New ListItem With {.Name = "IncitsTCM1Biometrics", .Value = CbeffBiometricOrganizations.IncitsTCM1Biometrics}, New ListItem With {.Name = "IsoIecJtc1SC37Biometrics", .Value = CbeffBiometricOrganizations.IsoIecJtc1SC37Biometrics} }

			cbOwner.Items.AddRange(items)
		Finally
			cbOwner.EndUpdate()
		End Try
	End Sub

	#End Region

	#Region "Private events"

	Private Sub BtnBrowseClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowse.Click
		If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
			tbFileName.Text = openFileDialog.FileName
		End If
	End Sub

	Private Sub CbOwnerSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbOwner.SelectedIndexChanged
		ListTypes()
	End Sub

	Private Sub OpenSubjectDialogLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		ListOwners()
		cbOwner.SelectedIndex = 0
	End Sub

	Private Sub OpenSubjectDialogShown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
		Dim result = openFileDialog.ShowDialog()
		If result = System.Windows.Forms.DialogResult.OK Then
			tbFileName.Text = openFileDialog.FileName
			btnOk.Focus()
		Else
			DialogResult = result
		End If
	End Sub

	#End Region

	#Region "Public properties"

	Public ReadOnly Property FileName() As String
		Get
			Return tbFileName.Text
		End Get
	End Property

	Public ReadOnly Property FormatOwner() As UShort
		Get
			Dim item As ListItem = CType(cbOwner.SelectedItem, ListItem)
			Return item.Value
		End Get
	End Property

	Public ReadOnly Property FormatType() As UShort
		Get
			If cbType.SelectedIndex <> -1 Then
				Dim item As ListItem = CType(cbType.SelectedItem, ListItem)
				Return item.Value
			Else
				Return 0
			End If
		End Get
	End Property

	#End Region
End Class
