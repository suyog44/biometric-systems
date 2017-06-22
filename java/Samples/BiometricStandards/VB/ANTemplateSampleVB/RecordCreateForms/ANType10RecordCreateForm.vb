Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO
Imports Neurotec.Images

Namespace RecordCreateForms

	Partial Public Class ANType10RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			For Each value As Object In System.Enum.GetValues(GetType(ANImageType))
				imageTypeComboBox.Items.Add(value)
			Next value
			imageTypeComboBox.SelectedIndex = 0
		End Sub

#End Region

#Region "Private properties"

		Private ReadOnly Property ImageType() As ANImageType
			Get
				Return CType(imageTypeComboBox.SelectedItem, ANImageType)
			End Get
		End Property

		Private ReadOnly Property Smt() As String
			Get
				Return smtTextBox.Text
			End Get
		End Property

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType10Record
			If imageLoader.CreateFromImage Then
				record = New ANType10Record(ANTemplate.VersionCurrent, Idc, ImageType, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, Smt, imageLoader.Image)
			ElseIf imageLoader.CreateFromData Then
				Using image As NImage = NImage.FromMemory(imageLoader.ImageData)
					record = New ANType10Record(ANTemplate.VersionCurrent, Idc, ImageType, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, Smt, image) With {.HorzLineLength = imageLoader.Hll, .VertLineLength = imageLoader.Vll, .HorzPixelScale = imageLoader.Hps, .VertPixelScale = imageLoader.Vps, .ColorSpace = imageLoader.Colorspace}
				End Using
			Else
				Throw New NotSupportedException()
			End If

			template.Records.Add(record)
			Return record
		End Function

#End Region
	End Class
End Namespace
