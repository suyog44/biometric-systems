<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TenPrintCardPrintForm
	Inherits System.Windows.Forms.PrintPreviewDialog

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.PrintDialog1 = New System.Windows.Forms.PrintDialog
		Me.SuspendLayout()
		'
		'PrintDialog1
		'
		Me.PrintDialog1.UseEXDialog = True
		'
		'TenPrintCardPrintForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1132, 533)
		Me.Name = "TenPrintCardPrintForm"
		Me.Text = "TenPrintCardPrintForm"
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
End Class
