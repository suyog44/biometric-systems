Imports System.Windows.Forms

Partial Public Class NewNERecordForm
	Inherits Form
	Public Sub New()
		InitializeComponent()
	End Sub

	Public Property RecordWidth() As UShort
		Get
			Return CUShort(nudWidth.Value)
		End Get
		Set(ByVal value As UShort)
			nudWidth.Value = value
		End Set
	End Property

	Public Property RecordHeight() As UShort
		Get
			Return CUShort(nudHeight.Value)
		End Get
		Set(ByVal value As UShort)
			nudHeight.Value = value
		End Set
	End Property
End Class
