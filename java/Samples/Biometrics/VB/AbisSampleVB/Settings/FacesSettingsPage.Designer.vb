Imports Microsoft.VisualBasic
Imports System
Partial Public Class FacesSettingsPage
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
		Me.chbRecognizeEmotion = New System.Windows.Forms.CheckBox
		Me.nudLivenessThreshold = New System.Windows.Forms.NumericUpDown
		Me.chbRecognizeExpression = New System.Windows.Forms.CheckBox
		Me.cbMatchingSpeed = New System.Windows.Forms.ComboBox
		Me.chbDetectProperties = New System.Windows.Forms.CheckBox
		Me.label7 = New System.Windows.Forms.Label
		Me.chbDetermineGender = New System.Windows.Forms.CheckBox
		Me.label5 = New System.Windows.Forms.Label
		Me.chbDetectBaseDeaturePoints = New System.Windows.Forms.CheckBox
		Me.chbDetectAllFeaturePoints = New System.Windows.Forms.CheckBox
		Me.label8 = New System.Windows.Forms.Label
		Me.cbTemplateSize = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.nudMinIOD = New System.Windows.Forms.NumericUpDown
		Me.label2 = New System.Windows.Forms.Label
		Me.nudConfidenceThreshold = New System.Windows.Forms.NumericUpDown
		Me.label3 = New System.Windows.Forms.Label
		Me.nudMaxRoll = New System.Windows.Forms.NumericUpDown
		Me.label4 = New System.Windows.Forms.Label
		Me.nudMaximalYaw = New System.Windows.Forms.NumericUpDown
		Me.label9 = New System.Windows.Forms.Label
		Me.label10 = New System.Windows.Forms.Label
		Me.cbCamera = New System.Windows.Forms.ComboBox
		Me.cbFormat = New System.Windows.Forms.ComboBox
		Me.chbCreateThumbnail = New System.Windows.Forms.CheckBox
		Me.nudThumbnailWidth = New System.Windows.Forms.NumericUpDown
		Me.label11 = New System.Windows.Forms.Label
		Me.nudQuality = New System.Windows.Forms.NumericUpDown
		Me.nudGeneralizationRecordCount = New System.Windows.Forms.NumericUpDown
		Me.label6 = New System.Windows.Forms.Label
		Me.label12 = New System.Windows.Forms.Label
		Me.btnConnect = New System.Windows.Forms.Button
		Me.btnDisconnect = New System.Windows.Forms.Button
		Me.cbLivenessMode = New System.Windows.Forms.ComboBox
		Me.Label13 = New System.Windows.Forms.Label
		Me.chbDetermineAge = New System.Windows.Forms.CheckBox
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.nudLivenessThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudMinIOD, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudConfidenceThreshold, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudMaxRoll, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudMaximalYaw, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudThumbnailWidth, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudQuality, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudGeneralizationRecordCount, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 4
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.chbRecognizeEmotion, 1, 18)
		Me.tableLayoutPanel1.Controls.Add(Me.nudLivenessThreshold, 1, 11)
		Me.tableLayoutPanel1.Controls.Add(Me.chbRecognizeExpression, 1, 17)
		Me.tableLayoutPanel1.Controls.Add(Me.cbMatchingSpeed, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.chbDetectProperties, 1, 16)
		Me.tableLayoutPanel1.Controls.Add(Me.label7, 0, 11)
		Me.tableLayoutPanel1.Controls.Add(Me.chbDetermineGender, 1, 14)
		Me.tableLayoutPanel1.Controls.Add(Me.label5, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.chbDetectBaseDeaturePoints, 1, 13)
		Me.tableLayoutPanel1.Controls.Add(Me.chbDetectAllFeaturePoints, 1, 12)
		Me.tableLayoutPanel1.Controls.Add(Me.label8, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.cbTemplateSize, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.nudMinIOD, 1, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.nudConfidenceThreshold, 1, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.label3, 0, 6)
		Me.tableLayoutPanel1.Controls.Add(Me.nudMaxRoll, 1, 6)
		Me.tableLayoutPanel1.Controls.Add(Me.label4, 0, 7)
		Me.tableLayoutPanel1.Controls.Add(Me.nudMaximalYaw, 1, 7)
		Me.tableLayoutPanel1.Controls.Add(Me.label9, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label10, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.cbCamera, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.cbFormat, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.chbCreateThumbnail, 0, 19)
		Me.tableLayoutPanel1.Controls.Add(Me.nudThumbnailWidth, 1, 20)
		Me.tableLayoutPanel1.Controls.Add(Me.label11, 0, 20)
		Me.tableLayoutPanel1.Controls.Add(Me.nudQuality, 1, 8)
		Me.tableLayoutPanel1.Controls.Add(Me.nudGeneralizationRecordCount, 1, 9)
		Me.tableLayoutPanel1.Controls.Add(Me.label6, 0, 8)
		Me.tableLayoutPanel1.Controls.Add(Me.label12, 0, 9)
		Me.tableLayoutPanel1.Controls.Add(Me.btnConnect, 2, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnDisconnect, 3, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.cbLivenessMode, 1, 10)
		Me.tableLayoutPanel1.Controls.Add(Me.Label13, 0, 10)
		Me.tableLayoutPanel1.Controls.Add(Me.chbDetermineAge, 1, 15)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 21
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
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(597, 530)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'chbRecognizeEmotion
		'
		Me.chbRecognizeEmotion.AutoSize = True
		Me.chbRecognizeEmotion.Location = New System.Drawing.Point(152, 460)
		Me.chbRecognizeEmotion.Name = "chbRecognizeEmotion"
		Me.chbRecognizeEmotion.Size = New System.Drawing.Size(117, 17)
		Me.chbRecognizeEmotion.TabIndex = 14
		Me.chbRecognizeEmotion.Text = "Recognize emotion"
		Me.chbRecognizeEmotion.UseVisualStyleBackColor = True
		'
		'nudLivenessThreshold
		'
		Me.nudLivenessThreshold.Enabled = False
		Me.nudLivenessThreshold.Location = New System.Drawing.Point(152, 296)
		Me.nudLivenessThreshold.Name = "nudLivenessThreshold"
		Me.nudLivenessThreshold.Size = New System.Drawing.Size(120, 20)
		Me.nudLivenessThreshold.TabIndex = 21
		Me.nudLivenessThreshold.Value = New Decimal(New Integer() {50, 0, 0, 0})
		'
		'chbRecognizeExpression
		'
		Me.chbRecognizeExpression.AutoSize = True
		Me.chbRecognizeExpression.Location = New System.Drawing.Point(152, 437)
		Me.chbRecognizeExpression.Name = "chbRecognizeExpression"
		Me.chbRecognizeExpression.Size = New System.Drawing.Size(130, 17)
		Me.chbRecognizeExpression.TabIndex = 13
		Me.chbRecognizeExpression.Text = "Recognize expression"
		Me.chbRecognizeExpression.UseVisualStyleBackColor = True
		'
		'cbMatchingSpeed
		'
		Me.cbMatchingSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbMatchingSpeed.FormattingEnabled = True
		Me.cbMatchingSpeed.Location = New System.Drawing.Point(152, 86)
		Me.cbMatchingSpeed.Name = "cbMatchingSpeed"
		Me.cbMatchingSpeed.Size = New System.Drawing.Size(121, 21)
		Me.cbMatchingSpeed.TabIndex = 23
		'
		'chbDetectProperties
		'
		Me.chbDetectProperties.AutoSize = True
		Me.chbDetectProperties.Location = New System.Drawing.Point(152, 414)
		Me.chbDetectProperties.Name = "chbDetectProperties"
		Me.chbDetectProperties.Size = New System.Drawing.Size(107, 17)
		Me.chbDetectProperties.TabIndex = 12
		Me.chbDetectProperties.Text = "Detect properties"
		Me.chbDetectProperties.UseVisualStyleBackColor = True
		'
		'label7
		'
		Me.label7.AutoSize = True
		Me.label7.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label7.Location = New System.Drawing.Point(3, 293)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(143, 26)
		Me.label7.TabIndex = 20
		Me.label7.Text = "Liveness threshold:"
		Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'chbDetermineGender
		'
		Me.chbDetermineGender.AutoSize = True
		Me.chbDetermineGender.Location = New System.Drawing.Point(152, 368)
		Me.chbDetermineGender.Name = "chbDetermineGender"
		Me.chbDetermineGender.Size = New System.Drawing.Size(110, 17)
		Me.chbDetermineGender.TabIndex = 11
		Me.chbDetermineGender.Text = "Determine gender"
		Me.chbDetermineGender.UseVisualStyleBackColor = True
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label5.Location = New System.Drawing.Point(3, 56)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(143, 27)
		Me.label5.TabIndex = 15
		Me.label5.Text = "Template size:"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'chbDetectBaseDeaturePoints
		'
		Me.chbDetectBaseDeaturePoints.AutoSize = True
		Me.chbDetectBaseDeaturePoints.Location = New System.Drawing.Point(152, 345)
		Me.chbDetectBaseDeaturePoints.Name = "chbDetectBaseDeaturePoints"
		Me.chbDetectBaseDeaturePoints.Size = New System.Drawing.Size(151, 17)
		Me.chbDetectBaseDeaturePoints.TabIndex = 10
		Me.chbDetectBaseDeaturePoints.Text = "Detect base feature points"
		Me.chbDetectBaseDeaturePoints.UseVisualStyleBackColor = True
		'
		'chbDetectAllFeaturePoints
		'
		Me.chbDetectAllFeaturePoints.AutoSize = True
		Me.chbDetectAllFeaturePoints.Location = New System.Drawing.Point(152, 322)
		Me.chbDetectAllFeaturePoints.Name = "chbDetectAllFeaturePoints"
		Me.chbDetectAllFeaturePoints.Size = New System.Drawing.Size(138, 17)
		Me.chbDetectAllFeaturePoints.TabIndex = 9
		Me.chbDetectAllFeaturePoints.Text = "Detect all feature points"
		Me.chbDetectAllFeaturePoints.UseVisualStyleBackColor = True
		'
		'label8
		'
		Me.label8.AutoSize = True
		Me.label8.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label8.Location = New System.Drawing.Point(3, 83)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(143, 27)
		Me.label8.TabIndex = 22
		Me.label8.Text = "Matching speed:"
		Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbTemplateSize
		'
		Me.cbTemplateSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbTemplateSize.FormattingEnabled = True
		Me.cbTemplateSize.Location = New System.Drawing.Point(152, 59)
		Me.cbTemplateSize.Name = "cbTemplateSize"
		Me.cbTemplateSize.Size = New System.Drawing.Size(121, 21)
		Me.cbTemplateSize.TabIndex = 16
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 110)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(143, 26)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Minimal inter ocular distance:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'nudMinIOD
		'
		Me.nudMinIOD.Location = New System.Drawing.Point(152, 113)
		Me.nudMinIOD.Maximum = New Decimal(New Integer() {16384, 0, 0, 0})
		Me.nudMinIOD.Minimum = New Decimal(New Integer() {8, 0, 0, 0})
		Me.nudMinIOD.Name = "nudMinIOD"
		Me.nudMinIOD.Size = New System.Drawing.Size(120, 20)
		Me.nudMinIOD.TabIndex = 2
		Me.nudMinIOD.Value = New Decimal(New Integer() {40, 0, 0, 0})
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(3, 136)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(143, 26)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Confidence threshold:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'nudConfidenceThreshold
		'
		Me.nudConfidenceThreshold.Location = New System.Drawing.Point(152, 139)
		Me.nudConfidenceThreshold.Name = "nudConfidenceThreshold"
		Me.nudConfidenceThreshold.Size = New System.Drawing.Size(120, 20)
		Me.nudConfidenceThreshold.TabIndex = 4
		Me.nudConfidenceThreshold.Value = New Decimal(New Integer() {50, 0, 0, 0})
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(3, 162)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(143, 26)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Maximal roll:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'nudMaxRoll
		'
		Me.nudMaxRoll.Location = New System.Drawing.Point(152, 165)
		Me.nudMaxRoll.Maximum = New Decimal(New Integer() {180, 0, 0, 0})
		Me.nudMaxRoll.Name = "nudMaxRoll"
		Me.nudMaxRoll.Size = New System.Drawing.Size(120, 20)
		Me.nudMaxRoll.TabIndex = 6
		Me.nudMaxRoll.Value = New Decimal(New Integer() {15, 0, 0, 0})
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label4.Location = New System.Drawing.Point(3, 188)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(143, 26)
		Me.label4.TabIndex = 7
		Me.label4.Text = "Maximal yaw:"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'nudMaximalYaw
		'
		Me.nudMaximalYaw.Location = New System.Drawing.Point(152, 191)
		Me.nudMaximalYaw.Maximum = New Decimal(New Integer() {90, 0, 0, 0})
		Me.nudMaximalYaw.Name = "nudMaximalYaw"
		Me.nudMaximalYaw.Size = New System.Drawing.Size(120, 20)
		Me.nudMaximalYaw.TabIndex = 8
		Me.nudMaximalYaw.Value = New Decimal(New Integer() {15, 0, 0, 0})
		'
		'label9
		'
		Me.label9.AutoSize = True
		Me.label9.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label9.Location = New System.Drawing.Point(3, 0)
		Me.label9.Name = "label9"
		Me.label9.Size = New System.Drawing.Size(143, 29)
		Me.label9.TabIndex = 24
		Me.label9.Text = "Camera:"
		Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label10
		'
		Me.label10.AutoSize = True
		Me.label10.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label10.Location = New System.Drawing.Point(3, 29)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(143, 27)
		Me.label10.TabIndex = 25
		Me.label10.Text = "Format:"
		Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbCamera
		'
		Me.cbCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbCamera.FormattingEnabled = True
		Me.cbCamera.Location = New System.Drawing.Point(152, 3)
		Me.cbCamera.Name = "cbCamera"
		Me.cbCamera.Size = New System.Drawing.Size(275, 21)
		Me.cbCamera.TabIndex = 26
		'
		'cbFormat
		'
		Me.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFormat.FormattingEnabled = True
		Me.cbFormat.Location = New System.Drawing.Point(152, 32)
		Me.cbFormat.Name = "cbFormat"
		Me.cbFormat.Size = New System.Drawing.Size(275, 21)
		Me.cbFormat.TabIndex = 27
		'
		'chbCreateThumbnail
		'
		Me.chbCreateThumbnail.AutoSize = True
		Me.chbCreateThumbnail.Location = New System.Drawing.Point(3, 483)
		Me.chbCreateThumbnail.Name = "chbCreateThumbnail"
		Me.chbCreateThumbnail.Size = New System.Drawing.Size(136, 17)
		Me.chbCreateThumbnail.TabIndex = 28
		Me.chbCreateThumbnail.Text = "Create thumbnail image"
		Me.chbCreateThumbnail.UseVisualStyleBackColor = True
		'
		'nudThumbnailWidth
		'
		Me.nudThumbnailWidth.Enabled = False
		Me.nudThumbnailWidth.Location = New System.Drawing.Point(152, 506)
		Me.nudThumbnailWidth.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
		Me.nudThumbnailWidth.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
		Me.nudThumbnailWidth.Name = "nudThumbnailWidth"
		Me.nudThumbnailWidth.Size = New System.Drawing.Size(120, 20)
		Me.nudThumbnailWidth.TabIndex = 29
		Me.nudThumbnailWidth.Value = New Decimal(New Integer() {90, 0, 0, 0})
		'
		'label11
		'
		Me.label11.AutoSize = True
		Me.label11.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label11.Location = New System.Drawing.Point(3, 503)
		Me.label11.Name = "label11"
		Me.label11.Size = New System.Drawing.Size(143, 27)
		Me.label11.TabIndex = 30
		Me.label11.Text = "Width:"
		Me.label11.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'nudQuality
		'
		Me.nudQuality.Location = New System.Drawing.Point(152, 217)
		Me.nudQuality.Name = "nudQuality"
		Me.nudQuality.Size = New System.Drawing.Size(120, 20)
		Me.nudQuality.TabIndex = 18
		Me.nudQuality.Value = New Decimal(New Integer() {50, 0, 0, 0})
		'
		'nudGeneralizationRecordCount
		'
		Me.nudGeneralizationRecordCount.Location = New System.Drawing.Point(152, 243)
		Me.nudGeneralizationRecordCount.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
		Me.nudGeneralizationRecordCount.Minimum = New Decimal(New Integer() {3, 0, 0, 0})
		Me.nudGeneralizationRecordCount.Name = "nudGeneralizationRecordCount"
		Me.nudGeneralizationRecordCount.Size = New System.Drawing.Size(120, 20)
		Me.nudGeneralizationRecordCount.TabIndex = 31
		Me.nudGeneralizationRecordCount.Value = New Decimal(New Integer() {3, 0, 0, 0})
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label6.Location = New System.Drawing.Point(3, 214)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(143, 26)
		Me.label6.TabIndex = 17
		Me.label6.Text = "Quality threshold:"
		Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label12
		'
		Me.label12.AutoSize = True
		Me.label12.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label12.Location = New System.Drawing.Point(3, 240)
		Me.label12.Name = "label12"
		Me.label12.Size = New System.Drawing.Size(143, 26)
		Me.label12.TabIndex = 32
		Me.label12.Text = "Generalization record count:"
		Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'btnConnect
		'
		Me.btnConnect.Location = New System.Drawing.Point(433, 3)
		Me.btnConnect.Name = "btnConnect"
		Me.btnConnect.Size = New System.Drawing.Size(75, 23)
		Me.btnConnect.TabIndex = 33
		Me.btnConnect.Text = "Connect"
		Me.btnConnect.UseVisualStyleBackColor = True
		'
		'btnDisconnect
		'
		Me.btnDisconnect.Location = New System.Drawing.Point(516, 3)
		Me.btnDisconnect.Name = "btnDisconnect"
		Me.btnDisconnect.Size = New System.Drawing.Size(75, 23)
		Me.btnDisconnect.TabIndex = 34
		Me.btnDisconnect.Text = "Disconnect"
		Me.btnDisconnect.UseVisualStyleBackColor = True
		'
		'cbLivenessMode
		'
		Me.cbLivenessMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbLivenessMode.FormattingEnabled = True
		Me.cbLivenessMode.Location = New System.Drawing.Point(152, 269)
		Me.cbLivenessMode.Name = "cbLivenessMode"
		Me.cbLivenessMode.Size = New System.Drawing.Size(121, 21)
		Me.cbLivenessMode.TabIndex = 35
		'
		'Label13
		'
		Me.Label13.AutoSize = True
		Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Label13.Location = New System.Drawing.Point(3, 266)
		Me.Label13.Name = "Label13"
		Me.Label13.Size = New System.Drawing.Size(143, 27)
		Me.Label13.TabIndex = 36
		Me.Label13.Text = "Liveness mode:"
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'chbDetermineAge
		'
		Me.chbDetermineAge.AutoSize = True
		Me.chbDetermineAge.Location = New System.Drawing.Point(152, 391)
		Me.chbDetermineAge.Name = "chbDetermineAge"
		Me.chbDetermineAge.Size = New System.Drawing.Size(95, 17)
		Me.chbDetermineAge.TabIndex = 38
		Me.chbDetermineAge.Text = "Determine age"
		Me.chbDetermineAge.UseVisualStyleBackColor = True
		'
		'FacesSettingsPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "FacesSettingsPage"
		Me.Size = New System.Drawing.Size(597, 530)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.nudLivenessThreshold, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudMinIOD, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudConfidenceThreshold, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudMaxRoll, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudMaximalYaw, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudThumbnailWidth, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudQuality, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudGeneralizationRecordCount, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private WithEvents nudMinIOD As System.Windows.Forms.NumericUpDown
	Private label2 As System.Windows.Forms.Label
	Private WithEvents nudConfidenceThreshold As System.Windows.Forms.NumericUpDown
	Private label3 As System.Windows.Forms.Label
	Private WithEvents nudMaxRoll As System.Windows.Forms.NumericUpDown
	Private WithEvents nudMaximalYaw As System.Windows.Forms.NumericUpDown
	Private label4 As System.Windows.Forms.Label
	Private WithEvents chbDetectAllFeaturePoints As System.Windows.Forms.CheckBox
	Private WithEvents chbDetectBaseDeaturePoints As System.Windows.Forms.CheckBox
	Private WithEvents chbDetermineGender As System.Windows.Forms.CheckBox
	Private WithEvents chbDetectProperties As System.Windows.Forms.CheckBox
	Private WithEvents chbRecognizeExpression As System.Windows.Forms.CheckBox
	Private WithEvents chbRecognizeEmotion As System.Windows.Forms.CheckBox
	Private WithEvents cbMatchingSpeed As System.Windows.Forms.ComboBox
	Private label5 As System.Windows.Forms.Label
	Private label8 As System.Windows.Forms.Label
	Private WithEvents cbTemplateSize As System.Windows.Forms.ComboBox
	Private label6 As System.Windows.Forms.Label
	Private WithEvents nudQuality As System.Windows.Forms.NumericUpDown
	Private WithEvents nudLivenessThreshold As System.Windows.Forms.NumericUpDown
	Private label7 As System.Windows.Forms.Label
	Private label9 As System.Windows.Forms.Label
	Private label10 As System.Windows.Forms.Label
	Private WithEvents cbCamera As System.Windows.Forms.ComboBox
	Private WithEvents cbFormat As System.Windows.Forms.ComboBox
	Private WithEvents chbCreateThumbnail As System.Windows.Forms.CheckBox
	Private WithEvents nudThumbnailWidth As System.Windows.Forms.NumericUpDown
	Private label11 As System.Windows.Forms.Label
	Private nudGeneralizationRecordCount As System.Windows.Forms.NumericUpDown
	Private label12 As System.Windows.Forms.Label
	Private WithEvents btnConnect As System.Windows.Forms.Button
	Private WithEvents btnDisconnect As System.Windows.Forms.Button
	Friend WithEvents cbLivenessMode As System.Windows.Forms.ComboBox
	Friend WithEvents Label13 As System.Windows.Forms.Label
	Private WithEvents chbDetermineAge As System.Windows.Forms.CheckBox
End Class
