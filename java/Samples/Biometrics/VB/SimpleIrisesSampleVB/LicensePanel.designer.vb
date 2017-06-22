Imports Microsoft.VisualBasic
Imports System

Partial Public Class LicensePanel
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary> 
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso (components IsNot Nothing) Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.label1 = New System.Windows.Forms.Label
		Me.lblStatus = New System.Windows.Forms.Label
		Me.rtbComponents = New System.Windows.Forms.RichTextBox
		Me.SuspendLayout()
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
		Me.label1.Location = New System.Drawing.Point(5, 3)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(178, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Required component licenses:"
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.ForeColor = System.Drawing.Color.Red
		Me.lblStatus.Location = New System.Drawing.Point(5, 23)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(164, 13)
		Me.lblStatus.TabIndex = 3
		Me.lblStatus.Text = "Component licenses not obtained"
		'
		'rtbComponents
		'
		Me.rtbComponents.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.rtbComponents.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.rtbComponents.Enabled = False
		Me.rtbComponents.Location = New System.Drawing.Point(189, 3)
		Me.rtbComponents.Multiline = False
		Me.rtbComponents.Name = "rtbComponents"
		Me.rtbComponents.ReadOnly = True
		Me.rtbComponents.Size = New System.Drawing.Size(231, 22)
		Me.rtbComponents.TabIndex = 4
		Me.rtbComponents.Text = "Components"
		'
		'LicensePanel
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.rtbComponents)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.label1)
		Me.Name = "LicensePanel"
		Me.Size = New System.Drawing.Size(420, 45)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private label1 As System.Windows.Forms.Label
	Private lblStatus As System.Windows.Forms.Label
	Friend WithEvents rtbComponents As System.Windows.Forms.RichTextBox
End Class

