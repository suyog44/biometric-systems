Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards
Imports Neurotec.IO

Namespace RecordCreateForms

	Partial Public Class ANType99RecordCreateForm
		Inherits ANRecordCreateForm
#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			cbVersion.Items.Add(ANType99Record.HeaderVersion10)
			cbVersion.Items.Add(ANType99Record.HeaderVersion11)
			cbVersion.SelectedIndex = cbVersion.Items.Count - 1

			nudBft.Maximum = UShort.MaxValue
			nudBfo.Maximum = nudBft.Maximum

			For Each value As Object In System.Enum.GetValues(GetType(ANBiometricType))
				chlbBiometricType.Items.Add(value)
			Next value
			chlbBiometricType.SetItemChecked(0, True)
		End Sub

#End Region

#Region "Private fields"

		Private _isUpdating As Boolean

#End Region

#Region "Protected methods"

		Protected Overrides Function OnCreateRecord(ByVal template As ANTemplate) As ANRecord
			Dim data() As Byte = File.ReadAllBytes(tbDataPath.Text)
			Dim buffer As New NBuffer(data)

			Dim record As New ANType99Record(ANTemplate.VersionCurrent, Idc) With {.BiometricType = GetBiometricType(), .BdbFormatOwner = CUShort(nudBfo.Value), .BdbFormatType = CUShort(nudBft.Value), .Data = buffer}

			template.Records.Add(record)
			Return record
		End Function

		Protected Function GetBiometricType() As ANBiometricType
			Dim value As ANBiometricType = ANBiometricType.NoInformationGiven
			Dim indices As CheckedListBox.CheckedIndexCollection = chlbBiometricType.CheckedIndices
			For i As Integer = 0 To indices.Count - 1
				Dim index As Integer = indices(i)
				value = value Or CType(chlbBiometricType.Items(index), ANBiometricType)
			Next i
			Return value
		End Function

#End Region

#Region "Private form events"

		Private Sub BtnBrowseDataClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseData.Click
			If dataOpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
				tbDataPath.Text = dataOpenFileDialog.FileName
			End If
		End Sub

		Private Sub TbSrcValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbSrc.Validating
			If tbSrc.Text.Length < ANType99Record.MinSourceAgencyLength OrElse tbSrc.Text.Length > ANType99Record.MaxSourceAgencyLengthV4 Then
				errorProvider.SetError(tbSrc, String.Format("Source agency field length must be between {0} and {1} characters.", ANType99Record.MinSourceAgencyLength, ANType99Record.MaxSourceAgencyLengthV4))
				e.Cancel = True
			Else
				errorProvider.SetError(tbSrc, Nothing)
			End If
		End Sub

		Private Sub ChlbBiometricTypeImteCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs) Handles chlbBiometricType.ItemCheck
			If _isUpdating Then
				Return
			End If

			_isUpdating = True
			If e.Index = 0 Then
				If e.NewValue = CheckState.Checked Then
					For i As Integer = 1 To chlbBiometricType.Items.Count - 1
						chlbBiometricType.SetItemChecked(i, False)
					Next i
				End If
			Else
				chlbBiometricType.SetItemChecked(0, False)
			End If
			_isUpdating = False

		End Sub

#End Region
	End Class
End Namespace
