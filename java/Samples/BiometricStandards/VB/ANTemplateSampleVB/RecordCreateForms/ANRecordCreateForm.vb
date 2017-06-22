Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Namespace RecordCreateForms

	Partial Public Class ANRecordCreateForm
		Inherits Form
#Region "Private fields"

		Private _template As ANTemplate
		Private _createdRecord As ANRecord

#End Region

#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
		End Sub

#End Region

#Region "Public properties"

		Public Property Template() As ANTemplate
			Get
				Return _template
			End Get
			Set(ByVal value As ANTemplate)
				_template = value
			End Set
		End Property

		Public Property CreatedRecord() As ANRecord
			Get
				Return _createdRecord
			End Get
			Protected Set(ByVal value As ANRecord)
				_createdRecord = value
			End Set
		End Property

		Public Property Idc() As Integer
			Get
				Return Convert.ToInt32(nudIdc.Value)
			End Get
			Set(ByVal value As Integer)
				nudIdc.Value = value
			End Set
		End Property

#End Region

#Region "Protected methods"

		Protected Overridable Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Return Nothing
		End Function

#End Region

#Region "Private methods"

		Private Sub ANRecordCreateFormFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			If DialogResult <> Windows.Forms.DialogResult.OK Then
				Return
			End If

			If (Not ValidateChildren()) Then
				e.Cancel = True
				Return
			End If

			Try
				CreatedRecord = OnCreateRecord(Template)
			Catch ex As Exception
				MessageBox.Show(ex.ToString())
				e.Cancel = True
			End Try
		End Sub

#End Region
	End Class
End Namespace
