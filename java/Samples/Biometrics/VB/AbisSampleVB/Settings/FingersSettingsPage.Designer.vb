Imports Microsoft.VisualBasic
Imports System
Partial Public Class FingersSettingsPage
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
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.label1 = New System.Windows.Forms.Label
		Me.cbScanners = New System.Windows.Forms.ComboBox
		Me.label2 = New System.Windows.Forms.Label
		Me.cbTemplateSize = New System.Windows.Forms.ComboBox
		Me.nudMaximalRotation = New System.Windows.Forms.NumericUpDown
		Me.cbMatchingSpeed = New System.Windows.Forms.ComboBox
		Me.label4 = New System.Windows.Forms.Label
		Me.label5 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.nudQuality = New System.Windows.Forms.NumericUpDown
		Me.chbFastExtraction = New System.Windows.Forms.CheckBox
		Me.chbReturnBinarized = New System.Windows.Forms.CheckBox
		Me.chbDeterminePatternClass = New System.Windows.Forms.CheckBox
		Me.chbCalculateNfiq = New System.Windows.Forms.CheckBox
		Me.label6 = New System.Windows.Forms.Label
		Me.nudGeneralizationRecordCount = New System.Windows.Forms.NumericUpDown
		Me.btnConnect = New System.Windows.Forms.Button
		Me.btnDisconnect = New System.Windows.Forms.Button
		Me.chbCheckForDuplicates = New System.Windows.Forms.CheckBox
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.nudMaximalRotation, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudQuality, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudGeneralizationRecordCount, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.AutoScroll = True
		Me.tableLayoutPanel1.ColumnCount = 4
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92.0!))
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
		Me.tableLayoutPanel1.Controls.Add(Me.chbFastExtraction, 1, 6)
		Me.tableLayoutPanel1.Controls.Add(Me.chbReturnBinarized, 1, 7)
		Me.tableLayoutPanel1.Controls.Add(Me.chbDeterminePatternClass, 1, 8)
		Me.tableLayoutPanel1.Controls.Add(Me.chbCalculateNfiq, 1, 9)
		Me.tableLayoutPanel1.Controls.Add(Me.label6, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.nudGeneralizationRecordCount, 1, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.btnConnect, 2, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnDisconnect, 3, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.chbCheckForDuplicates, 1, 10)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 11
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(526, 286)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(140, 29)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Finger scanner:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbScanners
		'
		Me.cbScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbScanners.FormattingEnabled = True
		Me.cbScanners.Location = New System.Drawing.Point(149, 3)
		Me.cbScanners.Name = "cbScanners"
		Me.cbScanners.Size = New System.Drawing.Size(191, 21)
		Me.cbScanners.TabIndex = 1
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(3, 29)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(140, 27)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Template size:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbTemplateSize
		'
		Me.cbTemplateSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbTemplateSize.FormattingEnabled = True
		Me.cbTemplateSize.Location = New System.Drawing.Point(149, 32)
		Me.cbTemplateSize.Name = "cbTemplateSize"
		Me.cbTemplateSize.Size = New System.Drawing.Size(191, 21)
		Me.cbTemplateSize.TabIndex = 4
		'
		'nudMaximalRotation
		'
		Me.nudMaximalRotation.Location = New System.Drawing.Point(149, 86)
		Me.nudMaximalRotation.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
		Me.nudMaximalRotation.Name = "nudMaximalRotation"
		Me.nudMaximalRotation.Size = New System.Drawing.Size(71, 20)
		Me.nudMaximalRotation.TabIndex = 9
		Me.nudMaximalRotation.Value = New Decimal(New Integer() {180, 0, 0, 0})
		'
		'cbMatchingSpeed
		'
		Me.cbMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbMatchingSpeed.FormattingEnabled = True
		Me.cbMatchingSpeed.Location = New System.Drawing.Point(149, 59)
		Me.cbMatchingSpeed.Name = "cbMatchingSpeed"
		Me.cbMatchingSpeed.Size = New System.Drawing.Size(191, 21)
		Me.cbMatchingSpeed.TabIndex = 11
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label4.Location = New System.Drawing.Point(3, 83)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(140, 26)
		Me.label4.TabIndex = 8
		Me.label4.Text = "Maximal rotation:"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label5.Location = New System.Drawing.Point(3, 56)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(140, 27)
		Me.label5.TabIndex = 10
		Me.label5.Text = "Matching speed:"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(3, 109)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(140, 26)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Quality threshold:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'nudQuality
		'
		Me.nudQuality.Location = New System.Drawing.Point(149, 112)
		Me.nudQuality.Name = "nudQuality"
		Me.nudQuality.Size = New System.Drawing.Size(71, 20)
		Me.nudQuality.TabIndex = 6
		Me.nudQuality.Value = New Decimal(New Integer() {39, 0, 0, 0})
		'
		'chbFastExtraction
		'
		Me.chbFastExtraction.AutoSize = True
		Me.chbFastExtraction.Location = New System.Drawing.Point(149, 164)
		Me.chbFastExtraction.Name = "chbFastExtraction"
		Me.chbFastExtraction.Size = New System.Drawing.Size(95, 17)
		Me.chbFastExtraction.TabIndex = 2
		Me.chbFastExtraction.Text = "Fast extraction"
		Me.chbFastExtraction.UseVisualStyleBackColor = True
		'
		'chbReturnBinarized
		'
		Me.chbReturnBinarized.AutoSize = True
		Me.chbReturnBinarized.Location = New System.Drawing.Point(149, 187)
		Me.chbReturnBinarized.Name = "chbReturnBinarized"
		Me.chbReturnBinarized.Size = New System.Drawing.Size(134, 17)
		Me.chbReturnBinarized.TabIndex = 7
		Me.chbReturnBinarized.Text = "Return binarized image"
		Me.chbReturnBinarized.UseVisualStyleBackColor = True
		'
		'chbDeterminePatternClass
		'
		Me.chbDeterminePatternClass.AutoSize = True
		Me.chbDeterminePatternClass.Location = New System.Drawing.Point(149, 210)
		Me.chbDeterminePatternClass.Name = "chbDeterminePatternClass"
		Me.chbDeterminePatternClass.Size = New System.Drawing.Size(137, 17)
		Me.chbDeterminePatternClass.TabIndex = 12
		Me.chbDeterminePatternClass.Text = "Determine pattern class"
		Me.chbDeterminePatternClass.UseVisualStyleBackColor = True
		'
		'chbCalculateNfiq
		'
		Me.chbCalculateNfiq.AutoSize = True
		Me.chbCalculateNfiq.Location = New System.Drawing.Point(149, 233)
		Me.chbCalculateNfiq.Name = "chbCalculateNfiq"
		Me.chbCalculateNfiq.Size = New System.Drawing.Size(92, 17)
		Me.chbCalculateNfiq.TabIndex = 13
		Me.chbCalculateNfiq.Text = "Calculate Nfiq"
		Me.chbCalculateNfiq.UseVisualStyleBackColor = True
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label6.Location = New System.Drawing.Point(3, 135)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(140, 26)
		Me.label6.TabIndex = 14
		Me.label6.Text = "Generalization record count:"
		Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'nudGeneralizationRecordCount
		'
		Me.nudGeneralizationRecordCount.Location = New System.Drawing.Point(149, 138)
		Me.nudGeneralizationRecordCount.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
		Me.nudGeneralizationRecordCount.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
		Me.nudGeneralizationRecordCount.Name = "nudGeneralizationRecordCount"
		Me.nudGeneralizationRecordCount.Size = New System.Drawing.Size(71, 20)
		Me.nudGeneralizationRecordCount.TabIndex = 15
		Me.nudGeneralizationRecordCount.Value = New Decimal(New Integer() {3, 0, 0, 0})
		'
		'btnConnect
		'
		Me.btnConnect.Location = New System.Drawing.Point(354, 3)
		Me.btnConnect.Name = "btnConnect"
		Me.btnConnect.Size = New System.Drawing.Size(75, 23)
		Me.btnConnect.TabIndex = 32
		Me.btnConnect.Text = "Connect"
		Me.btnConnect.UseVisualStyleBackColor = True
		'
		'btnDisconnect
		'
		Me.btnDisconnect.Location = New System.Drawing.Point(437, 3)
		Me.btnDisconnect.Name = "btnDisconnect"
		Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
		Me.btnDisconnect.TabIndex = 33
		Me.btnDisconnect.Text = "Disconnect"
		Me.btnDisconnect.UseVisualStyleBackColor = True
		'
		'chbCheckForDuplicates
		'
		Me.chbCheckForDuplicates.AutoSize = True
		Me.chbCheckForDuplicates.Location = New System.Drawing.Point(149, 256)
		Me.chbCheckForDuplicates.Name = "chbCheckForDuplicates"
		Me.chbCheckForDuplicates.Size = New System.Drawing.Size(199, 17)
		Me.chbCheckForDuplicates.TabIndex = 35
		Me.chbCheckForDuplicates.Text = "Check for duplicates when capturing"
		Me.chbCheckForDuplicates.UseVisualStyleBackColor = True
		'
		'FingersSettingsPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "FingersSettingsPage"
		Me.PageName = "Fingers settings"
		Me.Size = New System.Drawing.Size(526, 286)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.nudMaximalRotation, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudQuality, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudGeneralizationRecordCount, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private WithEvents cbScanners As System.Windows.Forms.ComboBox
	Private WithEvents chbReturnBinarized As System.Windows.Forms.CheckBox
	Private label2 As System.Windows.Forms.Label
	Private WithEvents cbTemplateSize As System.Windows.Forms.ComboBox
	Private label3 As System.Windows.Forms.Label
	Private WithEvents nudQuality As System.Windows.Forms.NumericUpDown
	Private WithEvents chbFastExtraction As System.Windows.Forms.CheckBox
	Private WithEvents nudMaximalRotation As System.Windows.Forms.NumericUpDown
	Private WithEvents cbMatchingSpeed As System.Windows.Forms.ComboBox
	Private label4 As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private WithEvents chbDeterminePatternClass As System.Windows.Forms.CheckBox
	Private WithEvents chbCalculateNfiq As System.Windows.Forms.CheckBox
	Private label6 As System.Windows.Forms.Label
	Private nudGeneralizationRecordCount As System.Windows.Forms.NumericUpDown
	Private WithEvents btnConnect As System.Windows.Forms.Button
	Private WithEvents btnDisconnect As System.Windows.Forms.Button
	Private WithEvents chbCheckForDuplicates As System.Windows.Forms.CheckBox

End Class
