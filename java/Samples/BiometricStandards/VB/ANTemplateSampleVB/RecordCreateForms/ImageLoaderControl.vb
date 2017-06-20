Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards
Imports Neurotec.Images

Namespace RecordCreateForms

	Partial Public Class ImageLoaderControl
		Inherits UserControl
#Region "Private members"

		Private _hasBpx As Boolean
		Private _hasColorspace As Boolean

#End Region

#Region "Public constructor"

		Public Sub New()
			InitializeComponent()

			For Each value As Object In System.Enum.GetValues(GetType(BdifScaleUnits))
				cbScaleUnits.Items.Add(value)
			Next value
			cbScaleUnits.SelectedIndex = 0

			For Each value As Object In System.Enum.GetValues(GetType(ANImageCompressionAlgorithm))
				cbCompressionAlgorithm.Items.Add(value)
			Next value
			cbCompressionAlgorithm.SelectedIndex = 0

			For Each value As Object In System.Enum.GetValues(GetType(ANImageColorSpace))
				cbColorSpace.Items.Add(value)
			Next value
			cbColorSpace.SelectedIndex = 0

			nudVll.Maximum = UShort.MaxValue
			nudHll.Maximum = nudVll.Maximum
			nudVps.Maximum = UShort.MaxValue
			nudHps.Maximum = nudVps.Maximum

			panelFromImage.Enabled = rbFromImage.Checked
			panelFromData.Enabled = rbFromData.Checked
		End Sub

#End Region

#Region "Public properties"

		Public Property Src() As String
			Get
				Return tbSrc.Text
			End Get
			Set(ByVal value As String)
				tbSrc.Text = value
			End Set
		End Property

		Public Property ScaleUnits() As BdifScaleUnits
			Get
				Return CType(cbScaleUnits.SelectedItem, BdifScaleUnits)
			End Get
			Set(ByVal value As BdifScaleUnits)
				cbScaleUnits.SelectedItem = value
			End Set
		End Property

		Public Property CompressionAlgorithm() As ANImageCompressionAlgorithm
			Get
				Return CType(cbCompressionAlgorithm.SelectedItem, ANImageCompressionAlgorithm)
			End Get
			Set(ByVal value As ANImageCompressionAlgorithm)
				cbCompressionAlgorithm.SelectedItem = value
			End Set
		End Property

		<Browsable(False)> _
		Public ReadOnly Property CreateFromImage() As Boolean
			Get
				Return rbFromImage.Checked
			End Get
		End Property

		<Browsable(False)> _
		Public ReadOnly Property CreateFromData() As Boolean
			Get
				Return rbFromData.Checked
			End Get
		End Property

		Public Property Hll() As UShort
			Get
				Return CUShort(nudHll.Value)
			End Get
			Set(ByVal value As UShort)
				nudHll.Value = value
			End Set
		End Property

		Public Property Vll() As UShort
			Get
				Return CUShort(nudVll.Value)
			End Get
			Set(ByVal value As UShort)
				nudVll.Value = value
			End Set
		End Property

		Public Property Hps() As UShort
			Get
				Return CUShort(nudHps.Value)
			End Get
			Set(ByVal value As UShort)
				nudHps.Value = value
			End Set
		End Property

		Public Property Vps() As UShort
			Get
				Return CUShort(nudVps.Value)
			End Get
			Set(ByVal value As UShort)
				nudVps.Value = value
			End Set
		End Property

		Public Property HasBpx() As Boolean
			Get
				Return _hasBpx
			End Get
			Set(ByVal value As Boolean)
				_hasBpx = value
			End Set
		End Property

		Public Property Bpx() As Byte
			Get
				Return CByte(nudBpx.Value)
			End Get
			Set(ByVal value As Byte)
				nudBpx.Value = value
			End Set
		End Property

		Public Property HasColorspace() As Boolean
			Get
				Return _hasColorspace
			End Get
			Set(ByVal value As Boolean)
				_hasColorspace = value
			End Set
		End Property

		Public Property Colorspace() As ANImageColorSpace
			Get
				Return CType(cbColorSpace.SelectedItem, ANImageColorSpace)
			End Get
			Set(ByVal value As ANImageColorSpace)
				cbColorSpace.SelectedItem = value
			End Set
		End Property

		<Browsable(False)> _
		Public ReadOnly Property Image() As NImage
			Get
				Return NImage.FromFile(tbImagePath.Text)
			End Get
		End Property

		<Browsable(False)> _
		Public ReadOnly Property ImageData() As Byte()
			Get
				Return File.ReadAllBytes(imageDataPathTextBox.Text)
			End Get
		End Property

#End Region

#Region "Private methods"

		Private Sub BtnBrowseImageClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseImage.Click
			imageOpenFileDialog.Filter = NImages.GetOpenFileFilterString(True, True)
			If imageOpenFileDialog.ShowDialog() = DialogResult.OK Then
				tbImagePath.Text = imageOpenFileDialog.FileName
			End If
		End Sub

		Private Sub BtnBrowseImageDataClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowseImageData.Click
			If imageDataOpenFileDialog.ShowDialog() = DialogResult.OK Then
				imageDataPathTextBox.Text = imageDataOpenFileDialog.FileName
			End If
		End Sub

		Private Sub RbFomImageCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbFromImage.CheckedChanged
			panelFromImage.Enabled = rbFromImage.Checked
		End Sub

		Private Sub RbFromDataCheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbFromData.CheckedChanged
			panelFromData.Enabled = rbFromData.Checked

			bpxLabel.Enabled = _hasBpx
			nudBpx.Enabled = _hasBpx
			colorspaceLabel.Enabled = _hasColorspace
			cbColorSpace.Enabled = _hasColorspace
		End Sub

		Private Sub TbSrcValidating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbSrc.Validating
			If tbSrc.Text.Length < ANImageAsciiBinaryRecord.MinSourceAgencyLength OrElse tbSrc.Text.Length > ANImageAsciiBinaryRecord.MaxSourceAgencyLengthV4 Then
				errorProvider1.SetError(tbSrc, String.Format("Source agency field length must be between {0} and {1} characters", ANImageAsciiBinaryRecord.MinSourceAgencyLength, ANImageAsciiBinaryRecord.MaxSourceAgencyLengthV4))
				e.Cancel = True
			Else
				errorProvider1.SetError(tbSrc, Nothing)
			End If
		End Sub

#End Region
	End Class
End Namespace
