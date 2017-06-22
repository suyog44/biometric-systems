Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO
Imports Neurotec.Images

Namespace RecordCreateForms

	Partial Public Class ANType17RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
		End Sub

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType17Record
			If imageLoader.CreateFromImage Then
				record = New ANType17Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, imageLoader.Image)
			ElseIf imageLoader.CreateFromData Then
				Using image As NImage = NImage.FromMemory(imageLoader.ImageData)
					record = New ANType17Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, image) With {.HorzLineLength = imageLoader.Hll, .VertLineLength = imageLoader.Vll, .HorzPixelScale = imageLoader.Hps, .VertPixelScale = imageLoader.Vps, .BitsPerPixel = imageLoader.Bpx, .ColorSpace = imageLoader.Colorspace}
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
