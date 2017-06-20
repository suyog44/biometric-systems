Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images

Namespace RecordCreateForms

	Partial Public Class ANImageBinaryRecordCreateForm
		Inherits ANRecordCreateForm
#Region "Private fields"

		Private _isLowResolution As Boolean = False

#End Region

#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			Dim compressionFormatsType As Type = GetCompressionFormatsType()
			If compressionFormatsType IsNot Nothing Then
				For Each value As Object In System.Enum.GetValues(compressionFormatsType)
					cbCompressionAlgorithm.Items.Add(value)
				Next value
				If cbCompressionAlgorithm.Items.Count > 0 Then
					cbCompressionAlgorithm.SelectedIndex = 0
				End If
			Else
				cbCompressionAlgorithm.Enabled = False
			End If

			panelFromImage.Enabled = rbFromImage.Checked
			panelFromData.Enabled = rbFromData.Checked
		End Sub

#End Region

#Region "Public properties"

		Public Property IsLowResolution() As Boolean
			Get
				Return _isLowResolution
			End Get
			Set(ByVal value As Boolean)
				_isLowResolution = value

				IsrValue = ANType1Record.MinScanningResolution
				If value Then
					Ir = IsrValue \ 2UI
				Else
					Ir = IsrValue \ 1UI
				End If

			End Set
		End Property

		Public ReadOnly Property IsrFlag() As Boolean
			Get
				Return chbIsrFlag.Checked
			End Get
		End Property

		Public Property IsrValue() As UInteger
			Get
				Return CUInt(Math.Round(isrResolutionEditBox.PpmValue))
			End Get
			Set(ByVal value As UInteger)
				isrResolutionEditBox.PpmValue = value
			End Set
		End Property

		Public Property IsrValuePpi() As Double
			Get
				Return Math.Round(isrResolutionEditBox.PpiValue)
			End Get
			Set(ByVal value As Double)
				isrResolutionEditBox.PpiValue = value
			End Set
		End Property

		Public Property CompressionAlgorithm() As Object
			Get
				Return cbCompressionAlgorithm.SelectedItem
			End Get
			Set(ByVal value As Object)
				cbCompressionAlgorithm.SelectedItem = value
			End Set
		End Property

		Public ReadOnly Property CreateFromImage() As Boolean
			Get
				Return rbFromImage.Checked
			End Get
		End Property

		Public ReadOnly Property CreateFromData() As Boolean
			Get
				Return rbFromData.Checked
			End Get
		End Property

		Public ReadOnly Property Image() As NImage
			Get
				Return NImage.FromFile(tbImagePath.Text)
			End Get
		End Property

		Public ReadOnly Property Hll() As UShort
			Get
				Return UShort.Parse(tbHll.Text)
			End Get
		End Property

		Public ReadOnly Property Vll() As UShort
			Get
				Return UShort.Parse(tbVll.Text)
			End Get
		End Property

		Public Property Ir() As UInteger
			Get
				Return CUInt(Math.Round(irResolutionEditBox.PpmValue))
			End Get
			Set(ByVal value As UInteger)
				irResolutionEditBox.PpmValue = value
			End Set
		End Property

		Public Property IrValuePpi() As Double
			Get
				Return Math.Round(irResolutionEditBox.PpiValue)
			End Get
			Set(ByVal value As Double)
				irResolutionEditBox.PpiValue = value
			End Set
		End Property

		Public ReadOnly Property VendorCA() As Byte
			Get
				Return Byte.Parse(tbVendorCa.Text)
			End Get
		End Property

		Public ReadOnly Property ImageData() As Byte()
			Get
				Return File.ReadAllBytes(tbImageDataPath.Text)
			End Get
		End Property

#End Region

#Region "Protected methods"

		Protected Overridable Function GetCompressionFormatsType() As Type
			Return Nothing
		End Function

#End Region

#Region "Private form events"

		Private Sub RbFromImageCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbFromImage.CheckedChanged
			panelFromImage.Enabled = rbFromImage.Checked
		End Sub

		Private Sub RbFromDataCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbFromData.CheckedChanged
			panelFromData.Enabled = rbFromData.Checked
		End Sub

		Private Sub BtnBrowseImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseImage.Click
			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
			If imageOpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
				tbImagePath.Text = imageOpenFileDialog.FileName
				Try
					Using image As NImage = Me.Image
						IsrValuePpi = image.HorzResolution * (CInt(IIf(IsLowResolution, 2, 1)))
						IrValuePpi = image.HorzResolution
					End Using
				Catch
					MessageBox.Show("Could not load image from specified file.")
				End Try
			End If
		End Sub

		Private Sub BrowseImageDataClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseImageData.Click
			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
			If imageOpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
				tbImageDataPath.Text = imageOpenFileDialog.FileName
				Try
					Using image As NImage = NImage.FromFile(tbImageDataPath.Text)
						IsrValuePpi = image.HorzResolution * (CInt(IIf(IsLowResolution, 2, 1)))
						IrValuePpi = image.HorzResolution
					End Using
				Catch
				End Try
			End If
		End Sub

		Private Sub ImageScanningResolutionEditBoxValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles isrResolutionEditBox.Validating
			Dim value As UInteger
			Try
				value = IsrValue
			Catch
				errorProvider.SetError(isrResolutionEditBox, "Image scanning resolution value is invalid.")
				e.Cancel = True
				Return
			End Try

			errorProvider.SetError(isrResolutionEditBox, Nothing)
		End Sub

		Private Sub TbHllValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbHll.Validating
			Try
				Dim value As UShort = Hll
				errorProvider.SetError(tbHll, Nothing)
			Catch
				errorProvider.SetError(tbHll, "Entered value is invalid.")
				e.Cancel = True
			End Try
		End Sub

		Private Sub TbVllValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbVll.Validating
			Try
				Dim value As UShort = Vll
				errorProvider.SetError(tbVll, Nothing)
			Catch
				errorProvider.SetError(tbVll, "Entered value is invalid.")
				e.Cancel = True
			End Try
		End Sub

		Private Sub NativeScanningResolutionEditBoxValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles irResolutionEditBox.Validating
			Try
				Dim value As UInteger = Ir
				errorProvider.SetError(irResolutionEditBox, Nothing)
			Catch
				errorProvider.SetError(irResolutionEditBox, "Entered value is invalid.")
				e.Cancel = True
			End Try
		End Sub

		Private Sub TbVendorCaValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbVendorCa.Validating
			Try
				Dim value As Byte = VendorCA
				errorProvider.SetError(tbVendorCa, Nothing)
			Catch
				errorProvider.SetError(tbVendorCa, "Entered value is invalid.")
				e.Cancel = True
			End Try
		End Sub

		Private Sub ANImageBinaryRecordCreateFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
			If DialogResult <> Windows.Forms.DialogResult.OK Then
				Return
			End If

			Try
				If CreateFromImage Then
					Dim image As NImage = Me.Image
					image.Dispose()
				End If
				errorProvider.SetError(tbImagePath, Nothing)
			Catch
				errorProvider.SetError(tbImagePath, "Could not load image from specified file.")
			End Try

			Try
				If CreateFromData Then
					Dim data() As Byte = ImageData
				End If
				errorProvider.SetError(tbImageDataPath, Nothing)
			Catch
				errorProvider.SetError(tbImageDataPath, "Could not load image data from specified file.")
			End Try
		End Sub

		Private Sub CbCompressionAlgorithmSelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbCompressionAlgorithm.SelectedIndexChanged
			tbVendorCa.Enabled = CompressionAlgorithm.Equals(ANImageCompressionAlgorithm.Vendor)
			vendorCaLabel.Enabled = tbVendorCa.Enabled
		End Sub

#End Region
	End Class
End Namespace
