Imports System
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images
Imports Neurotec.IO

Namespace RecordCreateForms

	Partial Public Class ANType8RecordCreateForm
		Inherits ANImageBinaryRecordCreateForm
#Region "Private fields"

		Private _vectors(-1) As ANPenVector

#End Region

#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			IsLowResolution = False
			FromVectorsIr = IsrValue

			fromVectorsPanel.Enabled = rbFromVectors.Checked

			For Each value As Object In System.Enum.GetValues(GetType(ANSignatureType))
				cbSignature.Items.Add(value)
			Next value
			If cbSignature.Items.Count > 0 Then
				cbSignature.SelectedIndex = 0
			Else
				cbSignature.Enabled = False
			End If
		End Sub

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType8Record
			If CreateFromImage Then
				Using image As NImage = Me.Image
					record = New ANType8Record(ANTemplate.VersionCurrent, Idc, SignatureType, CType(CompressionAlgorithm, ANSignatureRepresentationType), IsrFlag, image)
				End Using
			ElseIf CreateFromVector Then
				record = New ANType8Record(ANTemplate.VersionCurrent, Idc, SignatureType, Vectors)
			ElseIf CreateFromData Then
				Using image As NImage = NImage.FromMemory(ImageData)
					record = New ANType8Record(ANTemplate.VersionCurrent, Idc, SignatureType, CType(CompressionAlgorithm, ANSignatureRepresentationType), IsrFlag, image) With {.HorzLineLength = Hll, .VertLineLength = Vll}
				End Using
			Else
				Throw New NotSupportedException()
			End If

			template.Records.Add(record)
			Return record
		End Function

		Protected Overrides Function GetCompressionFormatsType() As Type
			Return GetType(ANSignatureRepresentationType)
		End Function

#End Region

#Region "Protected properties"

		Protected ReadOnly Property CreateFromVector() As Boolean
			Get
				Return rbFromVectors.Checked
			End Get
		End Property

		Protected Property FromVectorsIr() As UInteger
			Get
				Return CUInt(Math.Round(resolutioEditBox.PpmValue))
			End Get
			Set(ByVal value As UInteger)
				resolutioEditBox.PpmValue = value
			End Set
		End Property

		Protected Property Vectors() As ANPenVector()
			Get
				Return _vectors
			End Get
			Set(ByVal value As ANPenVector())
				_vectors = value
			End Set
		End Property

#End Region

#Region "Public properties"

		Public Property SignatureType() As ANSignatureType
			Get
				Return CType(cbSignature.SelectedItem, ANSignatureType)
			End Get
			Set(ByVal value As ANSignatureType)
				cbSignature.SelectedItem = value
			End Set
		End Property

#End Region

#Region "Private form events"

		Private Sub FromVectorsRadioButtonCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbFromVectors.CheckedChanged
			fromVectorsPanel.Enabled = rbFromVectors.Checked
		End Sub

		Private Sub BtnEditVectorsClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditVectors.Click
			Using dialog As New CreateANPenVectorArrayForm()
				dialog.Vectors = Vectors
				If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
					Vectors = dialog.Vectors
				End If
			End Using
		End Sub

#End Region
	End Class
End Namespace
