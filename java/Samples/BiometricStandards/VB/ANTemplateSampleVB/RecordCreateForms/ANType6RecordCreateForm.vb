Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images
Imports Neurotec.IO

Namespace RecordCreateForms

	Partial Public Class ANType6RecordCreateForm
		Inherits ANImageBinaryRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			IsLowResolution = False
		End Sub

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType6Record
			If CreateFromImage Then
				Using image As NImage = Me.Image
					record = New ANType6Record(ANTemplate.VersionCurrent, Idc, IsrFlag, CType(CompressionAlgorithm, ANBinaryImageCompressionAlgorithm), image)
					template.Records.Add(record)
					Return record
				End Using
			ElseIf CreateFromData Then
				Using image As NImage = NImage.FromMemory(ImageData)
					record = New ANType6Record(ANTemplate.VersionCurrent, Idc, IsrFlag, CType(CompressionAlgorithm, ANBinaryImageCompressionAlgorithm), image) With {.HorzLineLength = Hll, .VertLineLength = Vll}
				End Using
			Else
				Throw New NotSupportedException()
			End If

			template.Records.Add(record)
			Return record
		End Function

		Protected Overrides Function GetCompressionFormatsType() As Type
			Return GetType(ANBinaryImageCompressionAlgorithm)
		End Function

#End Region
	End Class
End Namespace
