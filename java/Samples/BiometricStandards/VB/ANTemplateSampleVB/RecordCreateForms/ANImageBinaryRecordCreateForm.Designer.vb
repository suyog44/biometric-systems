Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms

	Partial Public Class ANImageBinaryRecordCreateForm
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
			Me.chbIsrFlag = New System.Windows.Forms.CheckBox
			Me.irResolutionEditBox = New Neurotec.Samples.RecordCreateForms.ResolutionEditBox
			Me.label3 = New System.Windows.Forms.Label
			Me.isrResolutionEditBox = New Neurotec.Samples.RecordCreateForms.ResolutionEditBox
			Me.label2 = New System.Windows.Forms.Label
			Me.label4 = New System.Windows.Forms.Label
			Me.cbCompressionAlgorithm = New System.Windows.Forms.ComboBox
			Me.rbFromImage = New System.Windows.Forms.RadioButton
			Me.tbImagePath = New System.Windows.Forms.TextBox
			Me.btnBrowseImage = New System.Windows.Forms.Button
			Me.rbFromData = New System.Windows.Forms.RadioButton
			Me.label5 = New System.Windows.Forms.Label
			Me.label6 = New System.Windows.Forms.Label
			Me.tbHll = New System.Windows.Forms.TextBox
			Me.tbVll = New System.Windows.Forms.TextBox
			Me.tbVendorCa = New System.Windows.Forms.TextBox
			Me.vendorCaLabel = New System.Windows.Forms.Label
			Me.tbImageDataPath = New System.Windows.Forms.TextBox
			Me.btnBrowseImageData = New System.Windows.Forms.Button
			Me.label8 = New System.Windows.Forms.Label
			Me.panelFromImage = New System.Windows.Forms.Panel
			Me.panelFromData = New System.Windows.Forms.Panel
			Me.imageOpenFileDialog = New System.Windows.Forms.OpenFileDialog
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panelFromImage.SuspendLayout()
			Me.panelFromData.SuspendLayout()
			Me.SuspendLayout()
			'
			'btnOk
			'
			Me.btnOk.Location = New System.Drawing.Point(159, 436)
			Me.btnOk.TabIndex = 13
			'
			'btnCancel
			'
			Me.btnCancel.Location = New System.Drawing.Point(240, 436)
			Me.btnCancel.TabIndex = 14
			'
			'chbIsrFlag
			'
			Me.chbIsrFlag.AutoSize = True
			Me.chbIsrFlag.Location = New System.Drawing.Point(12, 64)
			Me.chbIsrFlag.Name = "chbIsrFlag"
			Me.chbIsrFlag.Size = New System.Drawing.Size(169, 17)
			Me.chbIsrFlag.TabIndex = 4
			Me.chbIsrFlag.Text = "Image scanning resolution flag"
			Me.chbIsrFlag.UseVisualStyleBackColor = True
			'
			'irResolutionEditBox
			'
			Me.irResolutionEditBox.Location = New System.Drawing.Point(127, 56)
			Me.irResolutionEditBox.Name = "irResolutionEditBox"
			Me.irResolutionEditBox.PpcmValue = 0
			Me.irResolutionEditBox.PpiValue = 0
			Me.irResolutionEditBox.PpmmValue = 0
			Me.irResolutionEditBox.PpmValue = 0
			Me.irResolutionEditBox.Size = New System.Drawing.Size(148, 30)
			Me.irResolutionEditBox.TabIndex = 5
			'
			'label3
			'
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(5, 62)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(87, 13)
			Me.label3.TabIndex = 4
			Me.label3.Text = "Image resolution:"
			'
			'isrResolutionEditBox
			'
			Me.isrResolutionEditBox.Location = New System.Drawing.Point(15, 104)
			Me.isrResolutionEditBox.Name = "isrResolutionEditBox"
			Me.isrResolutionEditBox.PpcmValue = 0
			Me.isrResolutionEditBox.PpiValue = 0
			Me.isrResolutionEditBox.PpmmValue = 0
			Me.isrResolutionEditBox.PpmValue = 0
			Me.isrResolutionEditBox.Size = New System.Drawing.Size(145, 30)
			Me.isrResolutionEditBox.TabIndex = 6
			'
			'label2
			'
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(12, 88)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(133, 13)
			Me.label2.TabIndex = 5
			Me.label2.Text = "Image scanning resolution:"
			'
			'label4
			'
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(12, 137)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(115, 13)
			Me.label4.TabIndex = 7
			Me.label4.Text = "Compression algorithm:"
			'
			'cbCompressionAlgorithm
			'
			Me.cbCompressionAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbCompressionAlgorithm.FormattingEnabled = True
			Me.cbCompressionAlgorithm.Location = New System.Drawing.Point(138, 134)
			Me.cbCompressionAlgorithm.Name = "cbCompressionAlgorithm"
			Me.cbCompressionAlgorithm.Size = New System.Drawing.Size(172, 21)
			Me.cbCompressionAlgorithm.TabIndex = 8
			'
			'rbFromImage
			'
			Me.rbFromImage.AutoSize = True
			Me.rbFromImage.Checked = True
			Me.rbFromImage.Location = New System.Drawing.Point(12, 162)
			Me.rbFromImage.Name = "rbFromImage"
			Me.rbFromImage.Size = New System.Drawing.Size(82, 17)
			Me.rbFromImage.TabIndex = 9
			Me.rbFromImage.TabStop = True
			Me.rbFromImage.Text = "From image:"
			Me.rbFromImage.UseVisualStyleBackColor = True
			'
			'tbImagePath
			'
			Me.tbImagePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.tbImagePath.Location = New System.Drawing.Point(5, 3)
			Me.tbImagePath.Name = "tbImagePath"
			Me.tbImagePath.Size = New System.Drawing.Size(212, 20)
			Me.tbImagePath.TabIndex = 0
			'
			'btnBrowseImage
			'
			Me.btnBrowseImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.btnBrowseImage.Location = New System.Drawing.Point(223, 0)
			Me.btnBrowseImage.Name = "btnBrowseImage"
			Me.btnBrowseImage.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseImage.TabIndex = 1
			Me.btnBrowseImage.Text = "Browse..."
			Me.btnBrowseImage.UseVisualStyleBackColor = True
			'
			'rbFromData
			'
			Me.rbFromData.AutoSize = True
			Me.rbFromData.Location = New System.Drawing.Point(12, 228)
			Me.rbFromData.Name = "rbFromData"
			Me.rbFromData.Size = New System.Drawing.Size(75, 17)
			Me.rbFromData.TabIndex = 11
			Me.rbFromData.Text = "From data:"
			Me.rbFromData.UseVisualStyleBackColor = True
			'
			'label5
			'
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(5, 9)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(108, 13)
			Me.label5.TabIndex = 0
			Me.label5.Text = "Horizontal line length:"
			'
			'label6
			'
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(5, 35)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(96, 13)
			Me.label6.TabIndex = 2
			Me.label6.Text = "Vertical line length:"
			'
			'tbHll
			'
			Me.tbHll.Location = New System.Drawing.Point(126, 6)
			Me.tbHll.Name = "tbHll"
			Me.tbHll.Size = New System.Drawing.Size(100, 20)
			Me.tbHll.TabIndex = 1
			Me.tbHll.Text = "0"
			'
			'tbVll
			'
			Me.tbVll.Location = New System.Drawing.Point(126, 32)
			Me.tbVll.Name = "tbVll"
			Me.tbVll.Size = New System.Drawing.Size(100, 20)
			Me.tbVll.TabIndex = 3
			Me.tbVll.Text = "0"
			'
			'tbVendorCa
			'
			Me.tbVendorCa.Location = New System.Drawing.Point(126, 86)
			Me.tbVendorCa.Name = "tbVendorCa"
			Me.tbVendorCa.Size = New System.Drawing.Size(100, 20)
			Me.tbVendorCa.TabIndex = 7
			Me.tbVendorCa.Text = "0"
			'
			'vendorCaLabel
			'
			Me.vendorCaLabel.AutoSize = True
			Me.vendorCaLabel.Location = New System.Drawing.Point(5, 89)
			Me.vendorCaLabel.Name = "vendorCaLabel"
			Me.vendorCaLabel.Size = New System.Drawing.Size(61, 13)
			Me.vendorCaLabel.TabIndex = 6
			Me.vendorCaLabel.Text = "Vendor CA:"
			'
			'tbImageDataPath
			'
			Me.tbImageDataPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.tbImageDataPath.Location = New System.Drawing.Point(8, 128)
			Me.tbImageDataPath.Name = "tbImageDataPath"
			Me.tbImageDataPath.Size = New System.Drawing.Size(209, 20)
			Me.tbImageDataPath.TabIndex = 9
			'
			'btnBrowseImageData
			'
			Me.btnBrowseImageData.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.btnBrowseImageData.Location = New System.Drawing.Point(223, 128)
			Me.btnBrowseImageData.Name = "btnBrowseImageData"
			Me.btnBrowseImageData.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseImageData.TabIndex = 10
			Me.btnBrowseImageData.Text = "Browse..."
			Me.btnBrowseImageData.UseVisualStyleBackColor = True
			'
			'label8
			'
			Me.label8.AutoSize = True
			Me.label8.Location = New System.Drawing.Point(5, 112)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(79, 13)
			Me.label8.TabIndex = 8
			Me.label8.Text = "Image data file:"
			'
			'panelFromImage
			'
			Me.panelFromImage.Controls.Add(Me.tbImagePath)
			Me.panelFromImage.Controls.Add(Me.btnBrowseImage)
			Me.panelFromImage.Location = New System.Drawing.Point(12, 185)
			Me.panelFromImage.Name = "panelFromImage"
			Me.panelFromImage.Size = New System.Drawing.Size(301, 37)
			Me.panelFromImage.TabIndex = 10
			'
			'panelFromData
			'
			Me.panelFromData.Controls.Add(Me.label5)
			Me.panelFromData.Controls.Add(Me.label3)
			Me.panelFromData.Controls.Add(Me.irResolutionEditBox)
			Me.panelFromData.Controls.Add(Me.label6)
			Me.panelFromData.Controls.Add(Me.btnBrowseImageData)
			Me.panelFromData.Controls.Add(Me.tbHll)
			Me.panelFromData.Controls.Add(Me.label8)
			Me.panelFromData.Controls.Add(Me.tbVll)
			Me.panelFromData.Controls.Add(Me.tbImageDataPath)
			Me.panelFromData.Controls.Add(Me.vendorCaLabel)
			Me.panelFromData.Controls.Add(Me.tbVendorCa)
			Me.panelFromData.Location = New System.Drawing.Point(12, 251)
			Me.panelFromData.Name = "panelFromData"
			Me.panelFromData.Size = New System.Drawing.Size(301, 167)
			Me.panelFromData.TabIndex = 12
			'
			'imageOpenFileDialog
			'
			Me.imageOpenFileDialog.Filter = "All Files (*.*)|*.*"
			Me.imageOpenFileDialog.Title = "Open Image"
			'
			'ANImageBinaryRecordCreateForm
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(325, 469)
			Me.Controls.Add(Me.panelFromImage)
			Me.Controls.Add(Me.rbFromImage)
			Me.Controls.Add(Me.label4)
			Me.Controls.Add(Me.panelFromData)
			Me.Controls.Add(Me.isrResolutionEditBox)
			Me.Controls.Add(Me.rbFromData)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.chbIsrFlag)
			Me.Controls.Add(Me.cbCompressionAlgorithm)
			Me.Name = "ANImageBinaryRecordCreateForm"
			Me.Text = "ANImageBinaryRecordCreateForm"
			Me.Controls.SetChildIndex(Me.label1, 0)
			Me.Controls.SetChildIndex(Me.cbCompressionAlgorithm, 0)
			Me.Controls.SetChildIndex(Me.chbIsrFlag, 0)
			Me.Controls.SetChildIndex(Me.label2, 0)
			Me.Controls.SetChildIndex(Me.rbFromData, 0)
			Me.Controls.SetChildIndex(Me.isrResolutionEditBox, 0)
			Me.Controls.SetChildIndex(Me.panelFromData, 0)
			Me.Controls.SetChildIndex(Me.label4, 0)
			Me.Controls.SetChildIndex(Me.rbFromImage, 0)
			Me.Controls.SetChildIndex(Me.panelFromImage, 0)
			Me.Controls.SetChildIndex(Me.nudIdc, 0)
			Me.Controls.SetChildIndex(Me.btnOk, 0)
			Me.Controls.SetChildIndex(Me.btnCancel, 0)
			CType(Me.errorProvider, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudIdc, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panelFromImage.ResumeLayout(False)
			Me.panelFromImage.PerformLayout()
			Me.panelFromData.ResumeLayout(False)
			Me.panelFromData.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private chbIsrFlag As System.Windows.Forms.CheckBox
		Private WithEvents irResolutionEditBox As ResolutionEditBox
		Private label3 As System.Windows.Forms.Label
		Private WithEvents isrResolutionEditBox As ResolutionEditBox
		Private label2 As System.Windows.Forms.Label
		Private tbImagePath As System.Windows.Forms.TextBox
		Private WithEvents btnBrowseImage As System.Windows.Forms.Button
		Private label5 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private WithEvents tbHll As System.Windows.Forms.TextBox
		Private WithEvents tbVll As System.Windows.Forms.TextBox
		Private tbImageDataPath As System.Windows.Forms.TextBox
		Private WithEvents btnBrowseImageData As System.Windows.Forms.Button
		Private label8 As System.Windows.Forms.Label
		Private imageOpenFileDialog As System.Windows.Forms.OpenFileDialog
		Protected WithEvents tbVendorCa As System.Windows.Forms.TextBox
		Protected vendorCaLabel As System.Windows.Forms.Label
		Protected label4 As System.Windows.Forms.Label
		Protected WithEvents rbFromImage As System.Windows.Forms.RadioButton
		Protected WithEvents rbFromData As System.Windows.Forms.RadioButton
		Protected WithEvents panelFromImage As System.Windows.Forms.Panel
		Protected WithEvents panelFromData As System.Windows.Forms.Panel
		Private WithEvents cbCompressionAlgorithm As System.Windows.Forms.ComboBox
	End Class
End Namespace