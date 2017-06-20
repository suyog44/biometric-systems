Imports Microsoft.VisualBasic
Imports System
Imports System.Windows.Forms
Imports Neurotec.Media

Partial Public Class CustomizeFormatForm
	Inherits Form
	Public Sub New()
		InitializeComponent()
	End Sub

	Public Shared Function CustomizeFormat(ByVal mediaFormat As NMediaFormat) As NMediaFormat
		If mediaFormat Is Nothing Then
			Throw New ArgumentNullException("mediaFormat")
		End If
		Dim frm As New CustomizeFormatForm()
		Dim clone As NMediaFormat = CType(mediaFormat.Clone(), NMediaFormat)
		frm.formatsPropertyGrid.SelectedObject = clone
		If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK AndAlso clone <> mediaFormat Then
			Return clone
		End If

		clone.Dispose()
		Return Nothing
	End Function
End Class
