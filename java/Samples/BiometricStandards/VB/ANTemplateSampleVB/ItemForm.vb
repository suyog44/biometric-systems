Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Partial Public Class ItemForm
	Inherits Form
	#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
		OnIsReadOnlyChanged()
	End Sub

	#End Region

	#Region "Private methods"

	Private Sub OnIsReadOnlyChanged()
		btnOk.Visible = Not tbValue.ReadOnly
		btnCancel.Text = CStr(IIf((Not tbValue.ReadOnly), "Cancel", "Close"))
	End Sub

	#End Region

	#Region "Public properties"

	Public Property Value() As String
		Get
			Return tbValue.Text
		End Get
		Set(ByVal value As String)
			tbValue.Text = value
		End Set
	End Property

	Public Property IsReadOnly() As Boolean
		Get
			Return tbValue.ReadOnly
		End Get
		Set(ByVal value As Boolean)
			If tbValue.ReadOnly <> value Then
				tbValue.ReadOnly = value
				OnIsReadOnlyChanged()
			End If
		End Set
	End Property

	#End Region
End Class
