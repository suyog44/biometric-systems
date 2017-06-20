Imports System
Imports System.Media
Imports System.Windows.Forms

Partial Public Class NewFingerViewForm
	Inherits Form
#Region "Public constructor"

	Public Sub New()
		InitializeComponent()
	End Sub

#End Region

#Region "Private variables"

	Private _sizeX As UShort
	Private _sizeY As UShort
	Private _vertResolution As UShort
	Private _horzResolution As UShort

#End Region

#Region "Public properties"

	Public ReadOnly Property SizeX() As UShort
		Get
			Return _sizeX
		End Get
	End Property
	Public ReadOnly Property SizeY() As UShort
		Get
			Return _sizeY
		End Get
	End Property
	Public ReadOnly Property VertResolution() As UShort
		Get
			Return _vertResolution
		End Get
	End Property
	Public ReadOnly Property HorzResolution() As UShort
		Get
			Return _horzResolution
		End Get
	End Property

#End Region

#Region "Private events"

	Private Sub numberBox_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
		Dim isNumber As UShort = 0
		If Char.IsControl(e.KeyChar) Then
			e.Handled = False
			Return
		End If

		Dim tb As TextBox = TryCast(sender, TextBox)
		If (Not UShort.TryParse(tb.Text + e.KeyChar.ToString(), isNumber)) Then
			e.Handled = True
			SystemSounds.Beep.Play()
		End If
	End Sub

	Private Sub btnOk_Click(ByVal sender As Object, ByVal e As EventArgs)
		If (Not UShort.TryParse(tbSizeX.Text, _sizeX)) OrElse (Not UShort.TryParse(tbSizeY.Text, _sizeY)) OrElse (Not UShort.TryParse(tbVertRes.Text, _vertResolution)) OrElse (Not UShort.TryParse(tbHorRes.Text, _horzResolution)) Then
			MessageBox.Show("Parameters can't be parsed!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
			DialogResult = System.Windows.Forms.DialogResult.None
			Return
		End If
	End Sub

#End Region
End Class
