Imports Microsoft.VisualBasic
Imports System
Partial Public Class IrisesSettingsPage
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
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.label1 = New System.Windows.Forms.Label()
		Me.cbScanners = New System.Windows.Forms.ComboBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.cbTemplateSize = New System.Windows.Forms.ComboBox()
		Me.nudMaximalRotation = New System.Windows.Forms.NumericUpDown()
		Me.cbMatchingSpeed = New System.Windows.Forms.ComboBox()
		Me.label4 = New System.Windows.Forms.Label()
		Me.label5 = New System.Windows.Forms.Label()
		Me.label3 = New System.Windows.Forms.Label()
		Me.nudQuality = New System.Windows.Forms.NumericUpDown()
		Me.chbFastExtraction = New System.Windows.Forms.CheckBox()
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.nudMaximalRotation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudQuality, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.AutoScroll = True
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F))
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.cbScanners, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.cbTemplateSize, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nudMaximalRotation, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.cbMatchingSpeed, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label4, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.label5, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label3, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.nudQuality, 1, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.chbFastExtraction, 1, 5)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 7
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(294, 187)
		Me.tableLayoutPanel1.TabIndex = 1
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(88, 27)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Iris scanner:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' cbScanners
		' 
		Me.cbScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbScanners.FormattingEnabled = True
		Me.cbScanners.Location = New System.Drawing.Point(97, 3)
		Me.cbScanners.Name = "cbScanners"
		Me.cbScanners.Size = New System.Drawing.Size(191, 21)
		Me.cbScanners.TabIndex = 1
'		Me.cbScanners.SelectedIndexChanged += New System.EventHandler(Me.CbScannersSelectedIndexChanged);
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(3, 27)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(88, 27)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Template size:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' cbTemplateSize
		' 
		Me.cbTemplateSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbTemplateSize.FormattingEnabled = True
		Me.cbTemplateSize.Location = New System.Drawing.Point(97, 30)
		Me.cbTemplateSize.Name = "cbTemplateSize"
		Me.cbTemplateSize.Size = New System.Drawing.Size(191, 21)
		Me.cbTemplateSize.TabIndex = 4
'		Me.cbTemplateSize.SelectedIndexChanged += New System.EventHandler(Me.CbTemplateSizeSelectedIndexChanged);
		' 
		' nudMaximalRotation
		' 
		Me.nudMaximalRotation.Location = New System.Drawing.Point(97, 84)
		Me.nudMaximalRotation.Maximum = New Decimal(New Integer() { 180, 0, 0, 0})
		Me.nudMaximalRotation.Name = "nudMaximalRotation"
		Me.nudMaximalRotation.Size = New System.Drawing.Size(71, 20)
		Me.nudMaximalRotation.TabIndex = 9
		Me.nudMaximalRotation.Value = New Decimal(New Integer() { 15, 0, 0, 0})
'		Me.nudMaximalRotation.ValueChanged += New System.EventHandler(Me.NudMaximalRotationValueChanged);
		' 
		' cbMatchingSpeed
		' 
		Me.cbMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbMatchingSpeed.FormattingEnabled = True
		Me.cbMatchingSpeed.Location = New System.Drawing.Point(97, 57)
		Me.cbMatchingSpeed.Name = "cbMatchingSpeed"
		Me.cbMatchingSpeed.Size = New System.Drawing.Size(191, 21)
		Me.cbMatchingSpeed.TabIndex = 11
'		Me.cbMatchingSpeed.SelectedIndexChanged += New System.EventHandler(Me.CbMatchingSpeedSelectedIndexChanged);
		' 
		' label4
		' 
		Me.label4.AutoSize = True
		Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label4.Location = New System.Drawing.Point(3, 81)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(88, 26)
		Me.label4.TabIndex = 8
		Me.label4.Text = "Maximal rotation:"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' label5
		' 
		Me.label5.AutoSize = True
		Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label5.Location = New System.Drawing.Point(3, 54)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(88, 27)
		Me.label5.TabIndex = 10
		Me.label5.Text = "Matching speed:"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' label3
		' 
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(3, 107)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(88, 26)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Quality threshold:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		' 
		' nudQuality
		' 
		Me.nudQuality.Location = New System.Drawing.Point(97, 110)
		Me.nudQuality.Name = "nudQuality"
		Me.nudQuality.Size = New System.Drawing.Size(71, 20)
		Me.nudQuality.TabIndex = 6
		Me.nudQuality.Value = New Decimal(New Integer() { 5, 0, 0, 0})
'		Me.nudQuality.ValueChanged += New System.EventHandler(Me.NudQualityValueChanged);
		' 
		' chbFastExtraction
		' 
		Me.chbFastExtraction.AutoSize = True
		Me.chbFastExtraction.Location = New System.Drawing.Point(97, 136)
		Me.chbFastExtraction.Name = "chbFastExtraction"
		Me.chbFastExtraction.Size = New System.Drawing.Size(95, 17)
		Me.chbFastExtraction.TabIndex = 2
		Me.chbFastExtraction.Text = "Fast extraction"
		Me.chbFastExtraction.UseVisualStyleBackColor = True
'		Me.chbFastExtraction.CheckedChanged += New System.EventHandler(Me.ChbFastExtractionCheckedChanged);
		' 
		' IrisesSettingsPage
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "IrisesSettingsPage"
		Me.Size = New System.Drawing.Size(294, 187)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.nudMaximalRotation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudQuality, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private WithEvents cbScanners As System.Windows.Forms.ComboBox
	Private label2 As System.Windows.Forms.Label
	Private WithEvents cbTemplateSize As System.Windows.Forms.ComboBox
	Private WithEvents nudMaximalRotation As System.Windows.Forms.NumericUpDown
	Private WithEvents cbMatchingSpeed As System.Windows.Forms.ComboBox
	Private label4 As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private WithEvents nudQuality As System.Windows.Forms.NumericUpDown
	Private WithEvents chbFastExtraction As System.Windows.Forms.CheckBox
End Class
