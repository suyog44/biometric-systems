Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Reflection
Imports Neurotec.Biometrics.Standards

Partial Public Class CbeffRecordOptionsForm
	Inherits Form
#Region "Private fields"

	Private _owners As Dictionary(Of String, Integer)
	Private _types As Dictionary(Of String, Integer)

#End Region

#Region "Public properties"

	Public ReadOnly Property PatronFormat() As UInteger
		Get

			Try
				If rbOwnerType.Checked Then
					Dim selectedOwner As String = CStr(cbOwners.SelectedItem)
					Dim selectedType As String = CStr(cbTypes.SelectedItem)
					Return BdifTypes.MakeFormat(Convert.ToUInt16(_owners(selectedOwner)), Convert.ToUInt16(_types(selectedType)))
                Else
                    Return Convert.ToUInt32(txtBoxFormat.Text, 16)

				End If
			Catch e1 As Exception
				Return 0
			End Try
		End Get
	End Property

#End Region

#Region "Public constructor"

	Public Sub New()
		InitializeComponent()

		InitializeOwners()
		InitializeTypes()

		Me.FormBorderStyle = FormBorderStyle.FixedSingle
	End Sub

#End Region

#Region "Private methods"

	Private Sub InitializeOwners()
		_owners = New Dictionary(Of String, Integer)()

		Dim infos() As FieldInfo = GetType(CbeffBiometricOrganizations).GetFields()
		Dim sortedInfos As List(Of FieldInfo) = infos.ToList()
		sortedInfos.Sort(Function(i1, i2) i1.Name.CompareTo(i2.Name))

		For Each info As FieldInfo In sortedInfos
			_owners.Add(info.Name, CUShort(info.GetValue(Nothing)))
		Next info

		For Each item As String In _owners.Keys
			cbOwners.Items.Add(item)
		Next item
	End Sub

	Private Sub InitializeTypes()
		_types = New Dictionary(Of String, Integer)()

		Dim infos() As FieldInfo = GetType(CbeffPatronFormatIdentifiers).GetFields()
		Dim sortedInfos As List(Of FieldInfo) = infos.ToList()
		sortedInfos.Sort(Function(i1, i2) i1.Name.CompareTo(i2.Name))

		For Each info As FieldInfo In sortedInfos
			_types.Add(info.Name, CUShort(info.GetValue(Nothing)))
		Next info

		For Each item As String In _types.Keys
			cbTypes.Items.Add(item)
		Next item
	End Sub

#End Region

#Region "Private form methods"

	Private Sub rbUseFormat_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbUseFormat.CheckedChanged
		rbOwnerType.Checked = Not rbUseFormat.Checked
		txtBoxFormat.Enabled = rbUseFormat.Checked
	End Sub

	Private Sub rbOwnerType_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbOwnerType.CheckedChanged
		rbUseFormat.Checked = Not rbOwnerType.Checked
		cbOwners.Enabled = rbOwnerType.Checked
		cbTypes.Enabled = rbOwnerType.Checked
	End Sub

#End Region
End Class
