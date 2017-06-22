Imports System
Imports System.IO
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO

Namespace RecordCreateForms

	Partial Public Class ANType7RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public properties"

		Public Sub New()
			InitializeComponent()

			isrResolutionEditBox.PpmValue = ANType1Record.MinScanningResolution
			irResolutionEditBox.PpmValue = ANType1Record.MinScanningResolution
		End Sub

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim data() As Byte = File.ReadAllBytes(tbImageDatePath.Text)
			Dim buffer As New NBuffer(data)

			Dim record As New ANType7Record(ANTemplate.VersionCurrent, Idc) With {.Data = buffer}

			template.Records.Add(record)
			Return record
		End Function

#End Region

#Region "Private methods"

		Private Sub BtnImageDataClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseImageData.Click
			If imageDataOpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
				tbImageDatePath.Text = imageDataOpenFileDialog.FileName
			End If
		End Sub

#End Region
	End Class
End Namespace
