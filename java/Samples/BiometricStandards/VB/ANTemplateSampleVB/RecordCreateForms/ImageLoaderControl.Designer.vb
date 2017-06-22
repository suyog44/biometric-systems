Imports Microsoft.VisualBasic
Imports System

Namespace RecordCreateForms
	Partial Public Class ImageLoaderControl
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
			Me.components = New System.ComponentModel.Container()
			Me.rbFromData = New System.Windows.Forms.RadioButton()
			Me.rbFromImage = New System.Windows.Forms.RadioButton()
			Me.panelFromImage = New System.Windows.Forms.Panel()
			Me.tbImagePath = New System.Windows.Forms.TextBox()
			Me.btnBrowseImage = New System.Windows.Forms.Button()
			Me.imageDataOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
			Me.imageOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
			Me.panelFromData = New System.Windows.Forms.Panel()
			Me.colorspaceLabel = New System.Windows.Forms.Label()
			Me.cbColorSpace = New System.Windows.Forms.ComboBox()
			Me.nudBpx = New System.Windows.Forms.NumericUpDown()
			Me.bpxLabel = New System.Windows.Forms.Label()
			Me.nudVps = New System.Windows.Forms.NumericUpDown()
			Me.nudHps = New System.Windows.Forms.NumericUpDown()
			Me.label3 = New System.Windows.Forms.Label()
			Me.label4 = New System.Windows.Forms.Label()
			Me.nudVll = New System.Windows.Forms.NumericUpDown()
			Me.nudHll = New System.Windows.Forms.NumericUpDown()
			Me.label5 = New System.Windows.Forms.Label()
			Me.label6 = New System.Windows.Forms.Label()
			Me.btnBrowseImageData = New System.Windows.Forms.Button()
			Me.label8 = New System.Windows.Forms.Label()
			Me.imageDataPathTextBox = New System.Windows.Forms.TextBox()
			Me.label1 = New System.Windows.Forms.Label()
			Me.label2 = New System.Windows.Forms.Label()
			Me.cbScaleUnits = New System.Windows.Forms.ComboBox()
			Me.cbCompressionAlgorithm = New System.Windows.Forms.ComboBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.tbSrc = New System.Windows.Forms.TextBox()
			Me.errorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
			Me.panelFromImage.SuspendLayout()
			Me.panelFromData.SuspendLayout()
			CType(Me.nudBpx, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudVps, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudHps, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudVll, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.nudHll, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' rbFromData
			' 
			Me.rbFromData.AutoSize = True
			Me.rbFromData.Location = New System.Drawing.Point(6, 153)
			Me.rbFromData.Name = "rbFromData"
			Me.rbFromData.Size = New System.Drawing.Size(75, 17)
			Me.rbFromData.TabIndex = 8
			Me.rbFromData.Text = "From data:"
			Me.rbFromData.UseVisualStyleBackColor = True
			'			Me.rbFromData.CheckedChanged += New System.EventHandler(Me.RbFromDataCheckedChanged);
			' 
			' rbFromImage
			' 
			Me.rbFromImage.AutoSize = True
			Me.rbFromImage.Checked = True
			Me.rbFromImage.Location = New System.Drawing.Point(6, 87)
			Me.rbFromImage.Name = "rbFromImage"
			Me.rbFromImage.Size = New System.Drawing.Size(82, 17)
			Me.rbFromImage.TabIndex = 6
			Me.rbFromImage.TabStop = True
			Me.rbFromImage.Text = "From image:"
			Me.rbFromImage.UseVisualStyleBackColor = True
			'			Me.rbFromImage.CheckedChanged += New System.EventHandler(Me.RbFomImageCheckedChanged);
			' 
			' panelFromImage
			' 
			Me.panelFromImage.Controls.Add(Me.tbImagePath)
			Me.panelFromImage.Controls.Add(Me.btnBrowseImage)
			Me.panelFromImage.Location = New System.Drawing.Point(6, 110)
			Me.panelFromImage.Name = "panelFromImage"
			Me.panelFromImage.Size = New System.Drawing.Size(298, 37)
			Me.panelFromImage.TabIndex = 7
			' 
			' tbImagePath
			' 
			Me.tbImagePath.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.tbImagePath.Location = New System.Drawing.Point(5, 3)
			Me.tbImagePath.Name = "tbImagePath"
			Me.tbImagePath.Size = New System.Drawing.Size(209, 20)
			Me.tbImagePath.TabIndex = 0
			' 
			' btnBrowseImage
			' 
			Me.btnBrowseImage.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnBrowseImage.Location = New System.Drawing.Point(220, 1)
			Me.btnBrowseImage.Name = "btnBrowseImage"
			Me.btnBrowseImage.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseImage.TabIndex = 1
			Me.btnBrowseImage.Text = "Browse..."
			Me.btnBrowseImage.UseVisualStyleBackColor = True
			'			Me.btnBrowseImage.Click += New System.EventHandler(Me.BtnBrowseImageClick);
			' 
			' imageDataOpenFileDialog
			' 
			Me.imageDataOpenFileDialog.Filter = "All Files (*.*)|*.*"
			' 
			' imageOpenFileDialog
			' 
			Me.imageOpenFileDialog.Filter = "All Files (*.*)|*.*"
			Me.imageOpenFileDialog.Title = "Open Image"
			' 
			' panelFromData
			' 
			Me.panelFromData.Controls.Add(Me.colorspaceLabel)
			Me.panelFromData.Controls.Add(Me.cbColorSpace)
			Me.panelFromData.Controls.Add(Me.nudBpx)
			Me.panelFromData.Controls.Add(Me.bpxLabel)
			Me.panelFromData.Controls.Add(Me.nudVps)
			Me.panelFromData.Controls.Add(Me.nudHps)
			Me.panelFromData.Controls.Add(Me.label3)
			Me.panelFromData.Controls.Add(Me.label4)
			Me.panelFromData.Controls.Add(Me.nudVll)
			Me.panelFromData.Controls.Add(Me.nudHll)
			Me.panelFromData.Controls.Add(Me.label5)
			Me.panelFromData.Controls.Add(Me.label6)
			Me.panelFromData.Controls.Add(Me.btnBrowseImageData)
			Me.panelFromData.Controls.Add(Me.label8)
			Me.panelFromData.Controls.Add(Me.imageDataPathTextBox)
			Me.panelFromData.Location = New System.Drawing.Point(6, 176)
			Me.panelFromData.Name = "panelFromData"
			Me.panelFromData.Size = New System.Drawing.Size(298, 206)
			Me.panelFromData.TabIndex = 9
			' 
			' colorspaceLabel
			' 
			Me.colorspaceLabel.AutoSize = True
			Me.colorspaceLabel.Location = New System.Drawing.Point(5, 138)
			Me.colorspaceLabel.Name = "colorspaceLabel"
			Me.colorspaceLabel.Size = New System.Drawing.Size(63, 13)
			Me.colorspaceLabel.TabIndex = 10
			Me.colorspaceLabel.Text = "Colorspace:"
			' 
			' cbColorSpace
			' 
			Me.cbColorSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbColorSpace.FormattingEnabled = True
			Me.cbColorSpace.Location = New System.Drawing.Point(126, 135)
			Me.cbColorSpace.Name = "cbColorSpace"
			Me.cbColorSpace.Size = New System.Drawing.Size(169, 21)
			Me.cbColorSpace.TabIndex = 11
			' 
			' nudBpx
			' 
			Me.nudBpx.Location = New System.Drawing.Point(126, 109)
			Me.nudBpx.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
			Me.nudBpx.Name = "nudBpx"
			Me.nudBpx.Size = New System.Drawing.Size(100, 20)
			Me.nudBpx.TabIndex = 9
			' 
			' bpxLabel
			' 
			Me.bpxLabel.AutoSize = True
			Me.bpxLabel.Location = New System.Drawing.Point(5, 112)
			Me.bpxLabel.Name = "bpxLabel"
			Me.bpxLabel.Size = New System.Drawing.Size(69, 13)
			Me.bpxLabel.TabIndex = 8
			Me.bpxLabel.Text = "Bits per pixel:"
			' 
			' nudVps
			' 
			Me.nudVps.Location = New System.Drawing.Point(126, 83)
			Me.nudVps.Name = "nudVps"
			Me.nudVps.Size = New System.Drawing.Size(100, 20)
			Me.nudVps.TabIndex = 7
			' 
			' nudHps
			' 
			Me.nudHps.Location = New System.Drawing.Point(126, 57)
			Me.nudHps.Name = "nudHps"
			Me.nudHps.Size = New System.Drawing.Size(100, 20)
			Me.nudHps.TabIndex = 5
			' 
			' label3
			' 
			Me.label3.AutoSize = True
			Me.label3.Location = New System.Drawing.Point(5, 60)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(109, 13)
			Me.label3.TabIndex = 4
			Me.label3.Text = "Horizontal pixel scale:"
			' 
			' label4
			' 
			Me.label4.AutoSize = True
			Me.label4.Location = New System.Drawing.Point(5, 86)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(97, 13)
			Me.label4.TabIndex = 6
			Me.label4.Text = "Vertical pixel scale:"
			' 
			' nudVll
			' 
			Me.nudVll.Location = New System.Drawing.Point(126, 32)
			Me.nudVll.Name = "nudVll"
			Me.nudVll.Size = New System.Drawing.Size(100, 20)
			Me.nudVll.TabIndex = 3
			' 
			' nudHll
			' 
			Me.nudHll.Location = New System.Drawing.Point(126, 6)
			Me.nudHll.Name = "nudHll"
			Me.nudHll.Size = New System.Drawing.Size(100, 20)
			Me.nudHll.TabIndex = 1
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(5, 9)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(108, 13)
			Me.label5.TabIndex = 0
			Me.label5.Text = "Horizontal line length:"
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(5, 35)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(96, 13)
			Me.label6.TabIndex = 2
			Me.label6.Text = "Vertical line length:"
			' 
			' btnBrowseImageData
			' 
			Me.btnBrowseImageData.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnBrowseImageData.Location = New System.Drawing.Point(220, 178)
			Me.btnBrowseImageData.Name = "btnBrowseImageData"
			Me.btnBrowseImageData.Size = New System.Drawing.Size(75, 23)
			Me.btnBrowseImageData.TabIndex = 14
			Me.btnBrowseImageData.Text = "Browse..."
			Me.btnBrowseImageData.UseVisualStyleBackColor = True
			'			Me.btnBrowseImageData.Click += New System.EventHandler(Me.BtnBrowseImageDataClick);
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Location = New System.Drawing.Point(5, 164)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(79, 13)
			Me.label8.TabIndex = 12
			Me.label8.Text = "Image data file:"
			' 
			' imageDataPathTextBox
			' 
			Me.imageDataPathTextBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.imageDataPathTextBox.Location = New System.Drawing.Point(8, 180)
			Me.imageDataPathTextBox.Name = "imageDataPathTextBox"
			Me.imageDataPathTextBox.Size = New System.Drawing.Size(206, 20)
			Me.imageDataPathTextBox.TabIndex = 13
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(8, 30)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(62, 13)
			Me.label1.TabIndex = 2
			Me.label1.Text = "Scale units:"
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(8, 57)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(115, 13)
			Me.label2.TabIndex = 4
			Me.label2.Text = "Compression algorithm:"
			' 
			' cbScaleUnits
			' 
			Me.cbScaleUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbScaleUnits.FormattingEnabled = True
			Me.cbScaleUnits.Location = New System.Drawing.Point(129, 27)
			Me.cbScaleUnits.Name = "cbScaleUnits"
			Me.cbScaleUnits.Size = New System.Drawing.Size(172, 21)
			Me.cbScaleUnits.TabIndex = 3
			' 
			' cbCompressionAlgorithm
			' 
			Me.cbCompressionAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbCompressionAlgorithm.FormattingEnabled = True
			Me.cbCompressionAlgorithm.Location = New System.Drawing.Point(129, 54)
			Me.cbCompressionAlgorithm.Name = "cbCompressionAlgorithm"
			Me.cbCompressionAlgorithm.Size = New System.Drawing.Size(172, 21)
			Me.cbCompressionAlgorithm.TabIndex = 5
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(8, 4)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(82, 13)
			Me.label10.TabIndex = 0
			Me.label10.Text = "Source agency:"
			' 
			' tbSrc
			' 
			Me.tbSrc.Location = New System.Drawing.Point(129, 1)
			Me.tbSrc.Name = "tbSrc"
			Me.tbSrc.Size = New System.Drawing.Size(172, 20)
			Me.tbSrc.TabIndex = 1
			'			Me.tbSrc.Validating += New System.ComponentModel.CancelEventHandler(Me.TbSrcValidating);
			' 
			' errorProvider1
			' 
			Me.errorProvider1.ContainerControl = Me
			' 
			' ImageLoaderControl
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.label10)
			Me.Controls.Add(Me.tbSrc)
			Me.Controls.Add(Me.cbCompressionAlgorithm)
			Me.Controls.Add(Me.cbScaleUnits)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.panelFromData)
			Me.Controls.Add(Me.panelFromImage)
			Me.Controls.Add(Me.rbFromImage)
			Me.Controls.Add(Me.rbFromData)
			Me.Name = "ImageLoaderControl"
			Me.Size = New System.Drawing.Size(311, 387)
			Me.panelFromImage.ResumeLayout(False)
			Me.panelFromImage.PerformLayout()
			Me.panelFromData.ResumeLayout(False)
			Me.panelFromData.PerformLayout()
			CType(Me.nudBpx, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudVps, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudHps, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudVll, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.nudHll, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private WithEvents rbFromData As System.Windows.Forms.RadioButton
		Private WithEvents rbFromImage As System.Windows.Forms.RadioButton
		Private panelFromImage As System.Windows.Forms.Panel
		Private tbImagePath As System.Windows.Forms.TextBox
		Private WithEvents btnBrowseImage As System.Windows.Forms.Button
		Private imageDataOpenFileDialog As System.Windows.Forms.OpenFileDialog
		Private imageOpenFileDialog As System.Windows.Forms.OpenFileDialog
		Private panelFromData As System.Windows.Forms.Panel
		Private label5 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private WithEvents btnBrowseImageData As System.Windows.Forms.Button
		Private label8 As System.Windows.Forms.Label
		Private imageDataPathTextBox As System.Windows.Forms.TextBox
		Private label1 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private cbScaleUnits As System.Windows.Forms.ComboBox
		Private cbCompressionAlgorithm As System.Windows.Forms.ComboBox
		Private nudVps As System.Windows.Forms.NumericUpDown
		Private nudHps As System.Windows.Forms.NumericUpDown
		Private label3 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private nudVll As System.Windows.Forms.NumericUpDown
		Private nudHll As System.Windows.Forms.NumericUpDown
		Protected colorspaceLabel As System.Windows.Forms.Label
		Protected cbColorSpace As System.Windows.Forms.ComboBox
		Protected nudBpx As System.Windows.Forms.NumericUpDown
		Protected bpxLabel As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private WithEvents tbSrc As System.Windows.Forms.TextBox
		Private errorProvider1 As System.Windows.Forms.ErrorProvider
	End Class
End Namespace