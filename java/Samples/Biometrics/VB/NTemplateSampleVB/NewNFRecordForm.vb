Imports Microsoft.VisualBasic
Imports System.Windows.Forms

Partial Public Class NewNFRecordForm
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

	Public Property HorzResolution() As UShort
		Get
			Return CUShort(nudHorzResolution.Value)
		End Get
		Set(ByVal value As UShort)
			nudHorzResolution.Value = value
		End Set
	End Property

	Public Property VertResolution() As UShort
		Get
			Return CUShort(nudVertResolution.Value)
		End Get
		Set(ByVal value As UShort)
			nudVertResolution.Value = value
		End Set
	End Property
End Class
