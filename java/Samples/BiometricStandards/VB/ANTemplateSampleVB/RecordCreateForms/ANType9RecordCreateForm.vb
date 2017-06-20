Imports System
Imports System.IO
Imports Neurotec.Biometrics
Imports Neurotec.Biometrics.Standards

Namespace RecordCreateForms

	Partial Public Class ANType9RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			fromNFRecordPanel.Enabled = rbFromNFRecord.Checked
			createEmptyPanel.Enabled = rbCreateEmpty.Checked
		End Sub

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim record As ANType9Record
			If rbFromNFRecord.Checked Then
				Dim nfRecordData() As Byte
				Try
					nfRecordData = File.ReadAllBytes(tbNFRecordPath.Text)
				Catch
					Throw New Exception(String.Format("Could not load NFRecord from {0}", tbNFRecordPath.Text))
				End Try

				Dim nfrecord As New NFRecord(nfRecordData)

				record = New ANType9Record(ANTemplate.VersionCurrent, Idc, chbFmtFlag.Checked, nfrecord)
				template.Records.Add(record)
				Return record
			Else
				record = New ANType9Record(ANTemplate.VersionCurrent, Idc) With {.ImpressionType = CType(cbImpressionType.SelectedItem, BdifFPImpressionType), .MinutiaeFormat = chbFmtFlag.Checked, .HasMinutiae = chbHasMinutiae.Checked}
				record.SetHasMinutiaeRidgeCounts(chbContainsRidgeCounts.Checked, chbHasRidgeCountsIndicator.Checked)
			End If

			template.Records.Add(record)
			Return record
		End Function

#End Region

#Region "Private methods"

		Private Sub ANType9RecordCreateFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			For Each value As Object In System.Enum.GetValues(GetType(BdifFPImpressionType))
				cbImpressionType.Items.Add(value)
			Next value
			cbImpressionType.SelectedIndex = 0
		End Sub

		Private Sub RbFromNFRecordCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbFromNFRecord.CheckedChanged
			fromNFRecordPanel.Enabled = rbFromNFRecord.Checked
		End Sub

		Private Sub RbCreateEmptyCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbCreateEmpty.CheckedChanged
			createEmptyPanel.Enabled = rbCreateEmpty.Checked
		End Sub

		Private Sub BtnBrowseNFRecordClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseNFRecord.Click
			If nfRecordOpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
				tbNFRecordPath.Text = nfRecordOpenFileDialog.FileName
			End If
		End Sub

#End Region
	End Class
End Namespace
