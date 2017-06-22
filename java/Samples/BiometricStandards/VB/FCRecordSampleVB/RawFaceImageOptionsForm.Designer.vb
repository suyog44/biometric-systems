Imports Microsoft.VisualBasic
Imports System

Partial Public Class RawFaceImageOptionsForm
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

#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Me.label1 = New System.Windows.Forms.Label
		Me.cbImageColorSpace = New System.Windows.Forms.ComboBox
		Me.label4 = New System.Windows.Forms.Label
		Me.label5 = New System.Windows.Forms.Label
		Me.label6 = New System.Windows.Forms.Label
		Me.tbWidth = New System.Windows.Forms.TextBox
		Me.tbHeight = New System.Windows.Forms.TextBox
		Me.tbVendorImageColorSpace = New System.Windows.Forms.TextBox
		Me.errorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.gbMain.SuspendLayout()
		CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btnOk
		'
		Me.btnOk.Location = New System.Drawing.Point(127, 211)
		'
		'btnCancel
		'
		Me.btnCancel.Location = New System.Drawing.Point(208, 211)
		'
		'gbMain
		'
		Me.gbMain.Controls.Add(Me.tbVendorImageColorSpace)
		Me.gbMain.Controls.Add(Me.tbHeight)
		Me.gbMain.Controls.Add(Me.tbWidth)
		Me.gbMain.Controls.Add(Me.label6)
		Me.gbMain.Controls.Add(Me.label5)
		Me.gbMain.Controls.Add(Me.label4)
		Me.gbMain.Controls.Add(Me.label1)
		Me.gbMain.Controls.Add(Me.cbImageColorSpace)
		Me.gbMain.Size = New System.Drawing.Size(268, 201)
		Me.gbMain.Controls.SetChildIndex(Me.cbFaceImageType, 0)
		Me.gbMain.Controls.SetChildIndex(Me.cbImageDataType, 0)
		Me.gbMain.Controls.SetChildIndex(Me.cbImageColorSpace, 0)
		Me.gbMain.Controls.SetChildIndex(Me.label1, 0)
		Me.gbMain.Controls.SetChildIndex(Me.label4, 0)
		Me.gbMain.Controls.SetChildIndex(Me.label5, 0)
		Me.gbMain.Controls.SetChildIndex(Me.label6, 0)
		Me.gbMain.Controls.SetChildIndex(Me.tbWidth, 0)
		Me.gbMain.Controls.SetChildIndex(Me.tbHeight, 0)
		Me.gbMain.Controls.SetChildIndex(Me.tbVendorImageColorSpace, 0)
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(6, 76)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(123, 13)
		Me.label1.TabIndex = 7
		Me.label1.Text = "Face image color space:"
		'
		'cbImageColorSpace
		'
		Me.cbImageColorSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbImageColorSpace.FormattingEnabled = True
		Me.cbImageColorSpace.Location = New System.Drawing.Point(152, 73)
		Me.cbImageColorSpace.Name = "cbImageColorSpace"
		Me.cbImageColorSpace.Size = New System.Drawing.Size(105, 21)
		Me.cbImageColorSpace.TabIndex = 6
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(6, 103)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(38, 13)
		Me.label4.TabIndex = 8
		Me.label4.Text = "Width:"
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(6, 125)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(41, 13)
		Me.label5.TabIndex = 9
		Me.label5.Text = "Height:"
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Location = New System.Drawing.Point(6, 148)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(133, 13)
		Me.label6.TabIndex = 10
		Me.label6.Text = "Vendor image color space:"
		'
		'tbWidth
		'
		Me.errorProvider.SetIconAlignment(Me.tbWidth, System.Windows.Forms.ErrorIconAlignment.MiddleLeft)
		Me.tbWidth.Location = New System.Drawing.Point(152, 100)
		Me.tbWidth.Name = "tbWidth"
		Me.tbWidth.Size = New System.Drawing.Size(105, 20)
		Me.tbWidth.TabIndex = 11
		Me.tbWidth.Text = "0"
		'
		'tbHeight
		'
		Me.errorProvider.SetIconAlignment(Me.tbHeight, System.Windows.Forms.ErrorIconAlignment.MiddleLeft)
		Me.tbHeight.Location = New System.Drawing.Point(152, 122)
		Me.tbHeight.Name = "tbHeight"
		Me.tbHeight.Size = New System.Drawing.Size(105, 20)
		Me.tbHeight.TabIndex = 12
		Me.tbHeight.Text = "0"
		'
		'tbVendorImageColorSpace
		'
		Me.errorProvider.SetIconAlignment(Me.tbVendorImageColorSpace, System.Windows.Forms.ErrorIconAlignment.MiddleLeft)
		Me.tbVendorImageColorSpace.Location = New System.Drawing.Point(152, 145)
		Me.tbVendorImageColorSpace.Name = "tbVendorImageColorSpace"
		Me.tbVendorImageColorSpace.Size = New System.Drawing.Size(105, 20)
		Me.tbVendorImageColorSpace.TabIndex = 13
		Me.tbVendorImageColorSpace.Text = "0"
		'
		'errorProvider
		'
		Me.errorProvider.ContainerControl = Me
		'
		'RawFaceImageOptionsForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(295, 246)
		Me.Name = "RawFaceImageOptionsForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "RawFaceImageOptionsForm"
		Me.gbMain.ResumeLayout(False)
		Me.gbMain.PerformLayout()
		CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private label1 As System.Windows.Forms.Label
	Private WithEvents cbImageColorSpace As System.Windows.Forms.ComboBox
	Private label6 As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private label4 As System.Windows.Forms.Label
	Private WithEvents tbVendorImageColorSpace As System.Windows.Forms.TextBox
	Private WithEvents tbHeight As System.Windows.Forms.TextBox
	Private WithEvents tbWidth As System.Windows.Forms.TextBox
	Private errorProvider As System.Windows.Forms.ErrorProvider
End Class
