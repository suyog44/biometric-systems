Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO
Imports Neurotec.Images

Namespace RecordCreateForms

	Partial Public Class ANType14RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
		End Sub

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType14Record
			If imageLoader.CreateFromImage Then
				record = New ANType14Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, imageLoader.Image)
			ElseIf imageLoader.CreateFromData Then
				Using image As NImage = NImage.FromMemory(imageLoader.ImageData)
					record = New ANType14Record(ANTemplate.VersionCurrent, Idc, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, image) With {.HorzLineLength = imageLoader.Hll, .VertLineLength = imageLoader.Vll, .HorzPixelScale = imageLoader.Hps, .VertPixelScale = imageLoader.Vps, .BitsPerPixel = imageLoader.Bpx}
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
