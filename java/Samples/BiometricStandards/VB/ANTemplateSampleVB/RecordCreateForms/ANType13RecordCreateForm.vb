Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO
Imports Neurotec.Images

Namespace RecordCreateForms

	Partial Public Class ANType13RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()
			For Each value As Object In System.Enum.GetValues(GetType(BdifFPImpressionType))
				cbFpImpressionType.Items.Add(value)
			Next value
			cbFpImpressionType.SelectedIndex = 0
		End Sub

#End Region

#Region "Private properties"

		Private ReadOnly Property ImpressionType() As BdifFPImpressionType
			Get
				Return CType(cbFpImpressionType.SelectedItem, BdifFPImpressionType)
			End Get
		End Property

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType13Record
			If imageLoader.CreateFromImage Then
				record = New ANType13Record(ANTemplate.VersionCurrent, Idc, ImpressionType, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, imageLoader.Image)
			ElseIf imageLoader.CreateFromData Then
				Using image As NImage = NImage.FromMemory(imageLoader.ImageData)
					record = New ANType13Record(ANTemplate.VersionCurrent, Idc, ImpressionType, imageLoader.Src, imageLoader.ScaleUnits, imageLoader.CompressionAlgorithm, image) With {.HorzLineLength = imageLoader.Hll, .VertLineLength = imageLoader.Vll, .HorzPixelScale = imageLoader.Hps, .VertPixelScale = imageLoader.Vps, .BitsPerPixel = imageLoader.Bpx}
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
