Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Neurotec.Biometrics.Standards

Partial Public Class RawFaceImageOptionsForm
	Inherits AddFaceImageForm
	Public Enum RawFaceImageOptionsFormMode
		Load = 1
		Save = 2
	End Enum

	Public Sub New()
		InitializeComponent()

		cbImageColorSpace.Items.AddRange(System.Enum.GetNames(GetType(FcrImageColorSpace)))
		cbImageColorSpace.SelectedIndex = 0
	End Sub

	Private _mode As RawFaceImageOptionsFormMode = RawFaceImageOptionsFormMode.Load
	Public Property Mode() As RawFaceImageOptionsFormMode
		Get
			Return _mode
		End Get
		Set(ByVal value As RawFaceImageOptionsFormMode)
			_mode = value
			OnModeChanged()
		End Set
	End Property

	Public Property ImageColorSpace() As FcrImageColorSpace
		Get
			Return CType(System.Enum.Parse(GetType(FcrImageColorSpace), cbImageColorSpace.SelectedItem.ToString()), FcrImageColorSpace)
		End Get
		Set(ByVal value As FcrImageColorSpace)
			cbImageColorSpace.SelectedItem = System.Enum.GetName(GetType(FcrImageColorSpace), value)
		End Set
	End Property

	Public Property ImageWidth() As UShort
		Get
			Return UShort.Parse(tbWidth.Text)
		End Get
		Set(ByVal value As UShort)
			tbWidth.Text = value.ToString()
		End Set
	End Property

	Public Property ImageHeight() As UShort
		Get
			Return UShort.Parse(tbHeight.Text)
		End Get
		Set(ByVal value As UShort)
			tbHeight.Text = value.ToString()
		End Set
	End Property

	Public ReadOnly Property VendorColorSpace() As Byte
		Get
			Return Byte.Parse(tbVendorImageColorSpace.Text)
		End Get
	End Property

	Private Sub cbImageColorSpace_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbImageColorSpace.SelectedIndexChanged
		If CType(System.Enum.Parse(GetType(FcrImageColorSpace), cbImageColorSpace.SelectedItem.ToString()), FcrImageColorSpace) = FcrImageColorSpace.Vendor Then
			If (Not tbVendorImageColorSpace.Enabled) Then
				tbVendorImageColorSpace.Enabled = True
			End If
		Else
			tbVendorImageColorSpace.Enabled = False
			tbVendorImageColorSpace.Text = "0"
		End If
	End Sub

	Private Sub tbWidthHeight_Validating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbWidth.Validating, tbHeight.Validating
		Dim s As Control = TryCast(sender, Control)
		If s Is Nothing Then
			Return
		End If

		Dim result As UShort
		If UShort.TryParse(s.Text, result) Then
			errorProvider.SetError(s, String.Empty)
		Else
			errorProvider.SetError(s, "Incorrect data entered. Data should be in range 0 to 65535.")
			e.Cancel = True
		End If
	End Sub

	Private Sub tbVendorImageColorSpace_Validating(ByVal sender As Object, ByVal e As CancelEventArgs) Handles tbVendorImageColorSpace.Validating
		Dim s As Control = TryCast(sender, Control)
		If s Is Nothing Then
			Return
		End If

		Dim result As Byte
		If Byte.TryParse(s.Text, result) Then
			errorProvider.SetError(s, String.Empty)
		Else
			errorProvider.SetError(s, "Incorrect data entered. Data should be in range 0 to 255.")
			e.Cancel = True
		End If
	End Sub

	Private Sub OnModeChanged()
		Select Case Mode
			Case RawFaceImageOptionsFormMode.Load
				Text = "Add face from data"
			Case RawFaceImageOptionsFormMode.Save
				Text = "Save face as data"
		End Select
	End Sub
End Class
