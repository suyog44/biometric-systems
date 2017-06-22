Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class AddFaceImageForm
	Inherits Form
	Public Sub New()
		InitializeComponent()

		Dim items() As String = System.Enum.GetNames(GetType(FcrFaceImageType))
		cbFaceImageType.Items.AddRange(items)
		cbImageDataType.Items.AddRange(System.Enum.GetNames(GetType(FcrImageDataType)))
		cbFaceImageType.SelectedIndex = 0
		cbImageDataType.SelectedIndex = 0
	End Sub

	Public Property FaceImageType() As FcrFaceImageType
		Get
			Return CType(System.Enum.Parse(GetType(FcrFaceImageType), cbFaceImageType.SelectedItem.ToString()), FcrFaceImageType)
		End Get
		Set(ByVal value As FcrFaceImageType)
			cbFaceImageType.SelectedItem = System.Enum.GetName(GetType(FcrFaceImage), value)
		End Set
	End Property

	Public Property ImageDataType() As FcrImageDataType
		Get
			Return CType(System.Enum.Parse(GetType(FcrImageDataType), cbImageDataType.SelectedItem.ToString()), FcrImageDataType)
		End Get
		Set(ByVal value As FcrImageDataType)
			cbImageDataType.SelectedItem = System.Enum.GetName(GetType(FcrImageDataType), value)
		End Set
	End Property
End Class
