Imports Microsoft.VisualBasic
Imports System

Partial Public Class MainForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.tabControl1 = New System.Windows.Forms.TabControl()
		Me.tabPage1 = New System.Windows.Forms.TabPage()
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.rtbRight = New System.Windows.Forms.RichTextBox()
		Me.btnConvertAndSave = New System.Windows.Forms.Button()
		Me.gbOpenning = New System.Windows.Forms.GroupBox()
		Me.rbNLRecord = New System.Windows.Forms.RadioButton()
		Me.rbNLTemplate = New System.Windows.Forms.RadioButton()
		Me.rbNTemplate = New System.Windows.Forms.RadioButton()
		Me.rbNFTemplate = New System.Windows.Forms.RadioButton()
		Me.rbNFRecords = New System.Windows.Forms.RadioButton()
		Me.rbFMRecordISO = New System.Windows.Forms.RadioButton()
		Me.rbFMRecordANSI = New System.Windows.Forms.RadioButton()
		Me.rbANTemplate = New System.Windows.Forms.RadioButton()
		Me.label6 = New System.Windows.Forms.Label()
		Me.label1 = New System.Windows.Forms.Label()
		Me.groupBox2 = New System.Windows.Forms.GroupBox()
		Me.label7 = New System.Windows.Forms.Label()
		Me.rbNLRecordRight = New System.Windows.Forms.RadioButton()
		Me.rbNLTemplateRight = New System.Windows.Forms.RadioButton()
		Me.rbNTemplateRight = New System.Windows.Forms.RadioButton()
		Me.rbNFTemplateRight = New System.Windows.Forms.RadioButton()
		Me.rbNFRecordsRight = New System.Windows.Forms.RadioButton()
		Me.rbFMRecordISORight = New System.Windows.Forms.RadioButton()
		Me.rbFMRecordANSIRight = New System.Windows.Forms.RadioButton()
		Me.rbANTemplateRight = New System.Windows.Forms.RadioButton()
		Me.label5 = New System.Windows.Forms.Label()
		Me.openImageButton = New System.Windows.Forms.Button()
		Me.rtbLeft = New System.Windows.Forms.RichTextBox()
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog()
		Me.tabControl1.SuspendLayout()
		Me.tabPage1.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.gbOpenning.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		Me.SuspendLayout()
		' 
		' tabControl1
		' 
		Me.tabControl1.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.tabControl1.Controls.Add(Me.tabPage1)
		Me.tabControl1.Location = New System.Drawing.Point(12, 12)
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = New System.Drawing.Size(716, 552)
		Me.tabControl1.TabIndex = 0
		' 
		' tabPage1
		' 
		Me.tabPage1.Controls.Add(Me.tableLayoutPanel1)
		Me.tabPage1.Location = New System.Drawing.Point(4, 22)
		Me.tabPage1.Name = "tabPage1"
		Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPage1.Size = New System.Drawing.Size(708, 526)
		Me.tabPage1.TabIndex = 0
		Me.tabPage1.Text = "Template conversion"
		Me.tabPage1.UseVisualStyleBackColor = True
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
		Me.tableLayoutPanel1.Controls.Add(Me.rtbRight, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.btnConvertAndSave, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.gbOpenning, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.groupBox2, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.openImageButton, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.rtbLeft, 0, 2)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 3
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0F))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0F))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248.0F))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(702, 520)
		Me.tableLayoutPanel1.TabIndex = 0
		' 
		' rtbRight
		' 
		Me.rtbRight.BackColor = System.Drawing.SystemColors.Window
		Me.rtbRight.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rtbRight.Location = New System.Drawing.Point(354, 275)
		Me.rtbRight.Name = "rtbRight"
		Me.rtbRight.ReadOnly = True
		Me.rtbRight.Size = New System.Drawing.Size(345, 242)
		Me.rtbRight.TabIndex = 5
		Me.rtbRight.TabStop = False
		Me.rtbRight.Text = ""
		Me.rtbRight.WordWrap = False
		' 
		' btnConvertAndSave
		' 
		Me.btnConvertAndSave.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.btnConvertAndSave.Image = (CType(resources.GetObject("btnConvertAndSave.Image"), System.Drawing.Image))
		Me.btnConvertAndSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnConvertAndSave.Location = New System.Drawing.Point(436, 243)
		Me.btnConvertAndSave.Name = "btnConvertAndSave"
		Me.btnConvertAndSave.Size = New System.Drawing.Size(181, 23)
		Me.btnConvertAndSave.TabIndex = 4
		Me.btnConvertAndSave.Text = "Convert template and save"
		Me.btnConvertAndSave.UseVisualStyleBackColor = True
		'			Me.btnConvertAndSave.Click += New System.EventHandler(Me.btnConvertAndSave_Click);
		' 
		' gbOpenning
		' 
		Me.gbOpenning.Controls.Add(Me.rbNLRecord)
		Me.gbOpenning.Controls.Add(Me.rbNLTemplate)
		Me.gbOpenning.Controls.Add(Me.rbNTemplate)
		Me.gbOpenning.Controls.Add(Me.rbNFTemplate)
		Me.gbOpenning.Controls.Add(Me.rbNFRecords)
		Me.gbOpenning.Controls.Add(Me.rbFMRecordISO)
		Me.gbOpenning.Controls.Add(Me.rbFMRecordANSI)
		Me.gbOpenning.Controls.Add(Me.rbANTemplate)
		Me.gbOpenning.Controls.Add(Me.label6)
		Me.gbOpenning.Controls.Add(Me.label1)
		Me.gbOpenning.Dock = System.Windows.Forms.DockStyle.Fill
		Me.gbOpenning.Location = New System.Drawing.Point(3, 3)
		Me.gbOpenning.Name = "gbOpenning"
		Me.gbOpenning.Size = New System.Drawing.Size(345, 232)
		Me.gbOpenning.TabIndex = 0
		Me.gbOpenning.TabStop = False
		Me.gbOpenning.Text = "Template to convert from"
		' 
		' rbNLRecord
		' 
		Me.rbNLRecord.AutoSize = True
		Me.rbNLRecord.Location = New System.Drawing.Point(69, 158)
		Me.rbNLRecord.Name = "rbNLRecord"
		Me.rbNLRecord.Size = New System.Drawing.Size(234, 17)
		Me.rbNLRecord.TabIndex = 6
		Me.rbNLRecord.TabStop = True
		Me.rbNLRecord.Text = "Neurotechnology Face Record (NLRecord)"
		Me.rbNLRecord.UseVisualStyleBackColor = True
		'			Me.rbNLRecord.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbNLTemplate
		' 
		Me.rbNLTemplate.AutoSize = True
		Me.rbNLTemplate.Location = New System.Drawing.Point(69, 181)
		Me.rbNLTemplate.Name = "rbNLTemplate"
		Me.rbNLTemplate.Size = New System.Drawing.Size(257, 17)
		Me.rbNLTemplate.TabIndex = 7
		Me.rbNLTemplate.TabStop = True
		Me.rbNLTemplate.Text = "Neurotechnology Faces Template (NLTemplate)"
		Me.rbNLTemplate.UseVisualStyleBackColor = True
		'			Me.rbNLTemplate.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbNTemplate
		' 
		Me.rbNTemplate.AutoSize = True
		Me.rbNTemplate.Location = New System.Drawing.Point(69, 204)
		Me.rbNTemplate.Name = "rbNTemplate"
		Me.rbNTemplate.Size = New System.Drawing.Size(219, 17)
		Me.rbNTemplate.TabIndex = 8
		Me.rbNTemplate.TabStop = True
		Me.rbNTemplate.Text = "Neurotechnology Template (NTemplate)"
		Me.rbNTemplate.UseVisualStyleBackColor = True
		'			Me.rbNTemplate.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbNFTemplate
		' 
		Me.rbNFTemplate.AutoSize = True
		Me.rbNFTemplate.Location = New System.Drawing.Point(69, 135)
		Me.rbNFTemplate.Name = "rbNFTemplate"
		Me.rbNFTemplate.Size = New System.Drawing.Size(262, 17)
		Me.rbNFTemplate.TabIndex = 5
		Me.rbNFTemplate.TabStop = True
		Me.rbNFTemplate.Text = "Neurotechnology Fingers Template (NFTemplate)"
		Me.rbNFTemplate.UseVisualStyleBackColor = True
		'			Me.rbNFTemplate.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbNFRecords
		' 
		Me.rbNFRecords.AutoSize = True
		Me.rbNFRecords.Location = New System.Drawing.Point(69, 112)
		Me.rbNFRecords.Name = "rbNFRecords"
		Me.rbNFRecords.Size = New System.Drawing.Size(239, 17)
		Me.rbNFRecords.TabIndex = 4
		Me.rbNFRecords.TabStop = True
		Me.rbNFRecords.Text = "Neurotechnology Finger Record (NFRecord)"
		Me.rbNFRecords.UseVisualStyleBackColor = True
		'			Me.rbNFRecords.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbFMRecordISO
		' 
		Me.rbFMRecordISO.AutoSize = True
		Me.rbFMRecordISO.Location = New System.Drawing.Point(69, 89)
		Me.rbFMRecordISO.Name = "rbFMRecordISO"
		Me.rbFMRecordISO.Size = New System.Drawing.Size(193, 17)
		Me.rbFMRecordISO.TabIndex = 3
		Me.rbFMRecordISO.TabStop = True
		Me.rbFMRecordISO.Text = "ISO/IEC 19794-2:2005 (FMRecord)"
		Me.rbFMRecordISO.UseVisualStyleBackColor = True
		'			Me.rbFMRecordISO.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbFMRecordANSI
		' 
		Me.rbFMRecordANSI.AutoSize = True
		Me.rbFMRecordANSI.Location = New System.Drawing.Point(69, 66)
		Me.rbFMRecordANSI.Name = "rbFMRecordANSI"
		Me.rbFMRecordANSI.Size = New System.Drawing.Size(195, 17)
		Me.rbFMRecordANSI.TabIndex = 2
		Me.rbFMRecordANSI.TabStop = True
		Me.rbFMRecordANSI.Text = "ANSI INCITS 378-2004 (FMRecord)"
		Me.rbFMRecordANSI.UseVisualStyleBackColor = True
		'			Me.rbFMRecordANSI.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' rbANTemplate
		' 
		Me.rbANTemplate.AutoSize = True
		Me.rbANTemplate.Location = New System.Drawing.Point(69, 43)
		Me.rbANTemplate.Name = "rbANTemplate"
		Me.rbANTemplate.Size = New System.Drawing.Size(203, 17)
		Me.rbANTemplate.TabIndex = 1
		Me.rbANTemplate.TabStop = True
		Me.rbANTemplate.Text = "ANSI/NIST-ITL 1-2000 (ANTemplate)"
		Me.rbANTemplate.UseVisualStyleBackColor = True
		'			Me.rbANTemplate.CheckedChanged += New System.EventHandler(Me.Global_CheckedChanged);
		' 
		' label6
		' 
		Me.label6.AutoSize = True
		Me.label6.Location = New System.Drawing.Point(16, 27)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(54, 13)
		Me.label6.TabIndex = 0
		Me.label6.Text = "Template:"
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(16, 27)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(0, 13)
		Me.label1.TabIndex = 0
		' 
		' groupBox2
		' 
		Me.groupBox2.Controls.Add(Me.label7)
		Me.groupBox2.Controls.Add(Me.rbNLRecordRight)
		Me.groupBox2.Controls.Add(Me.rbNLTemplateRight)
		Me.groupBox2.Controls.Add(Me.rbNTemplateRight)
		Me.groupBox2.Controls.Add(Me.rbNFTemplateRight)
		Me.groupBox2.Controls.Add(Me.rbNFRecordsRight)
		Me.groupBox2.Controls.Add(Me.rbFMRecordISORight)
		Me.groupBox2.Controls.Add(Me.rbFMRecordANSIRight)
		Me.groupBox2.Controls.Add(Me.rbANTemplateRight)
		Me.groupBox2.Controls.Add(Me.label5)
		Me.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.groupBox2.Location = New System.Drawing.Point(354, 3)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(345, 232)
		Me.groupBox2.TabIndex = 3
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Template to convert to"
		' 
		' label7
		' 
		Me.label7.AutoSize = True
		Me.label7.Location = New System.Drawing.Point(6, 27)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(54, 13)
		Me.label7.TabIndex = 0
		Me.label7.Text = "Template:"
		' 
		' rbNLRecordRight
		' 
		Me.rbNLRecordRight.AutoSize = True
		Me.rbNLRecordRight.Location = New System.Drawing.Point(66, 158)
		Me.rbNLRecordRight.Name = "rbNLRecordRight"
		Me.rbNLRecordRight.Size = New System.Drawing.Size(234, 17)
		Me.rbNLRecordRight.TabIndex = 7
		Me.rbNLRecordRight.TabStop = True
		Me.rbNLRecordRight.Text = "Neurotechnology Face Record (NLRecord)"
		Me.rbNLRecordRight.UseVisualStyleBackColor = True
		' 
		' rbNLTemplateRight
		' 
		Me.rbNLTemplateRight.AutoSize = True
		Me.rbNLTemplateRight.Location = New System.Drawing.Point(66, 181)
		Me.rbNLTemplateRight.Name = "rbNLTemplateRight"
		Me.rbNLTemplateRight.Size = New System.Drawing.Size(252, 17)
		Me.rbNLTemplateRight.TabIndex = 8
		Me.rbNLTemplateRight.TabStop = True
		Me.rbNLTemplateRight.Text = "Neurotechnology Face Template (NLTemplate)"
		Me.rbNLTemplateRight.UseVisualStyleBackColor = True
		' 
		' rbNTemplateRight
		' 
		Me.rbNTemplateRight.AutoSize = True
		Me.rbNTemplateRight.Location = New System.Drawing.Point(66, 204)
		Me.rbNTemplateRight.Name = "rbNTemplateRight"
		Me.rbNTemplateRight.Size = New System.Drawing.Size(224, 17)
		Me.rbNTemplateRight.TabIndex = 9
		Me.rbNTemplateRight.TabStop = True
		Me.rbNTemplateRight.Text = "Neurotechnology Templates (NTemplate)"
		Me.rbNTemplateRight.UseVisualStyleBackColor = True
		' 
		' rbNFTemplateRight
		' 
		Me.rbNFTemplateRight.AutoSize = True
		Me.rbNFTemplateRight.Location = New System.Drawing.Point(66, 135)
		Me.rbNFTemplateRight.Name = "rbNFTemplateRight"
		Me.rbNFTemplateRight.Size = New System.Drawing.Size(262, 17)
		Me.rbNFTemplateRight.TabIndex = 6
		Me.rbNFTemplateRight.TabStop = True
		Me.rbNFTemplateRight.Text = "Neurotechnology Fingers Template (NFTemplate)"
		Me.rbNFTemplateRight.UseVisualStyleBackColor = True
		' 
		' rbNFRecordsRight
		' 
		Me.rbNFRecordsRight.AutoSize = True
		Me.rbNFRecordsRight.Location = New System.Drawing.Point(66, 112)
		Me.rbNFRecordsRight.Name = "rbNFRecordsRight"
		Me.rbNFRecordsRight.Size = New System.Drawing.Size(239, 17)
		Me.rbNFRecordsRight.TabIndex = 5
		Me.rbNFRecordsRight.TabStop = True
		Me.rbNFRecordsRight.Text = "Neurotechnology Finger Record (NFRecord)"
		Me.rbNFRecordsRight.UseVisualStyleBackColor = True
		' 
		' rbFMRecordISORight
		' 
		Me.rbFMRecordISORight.AutoSize = True
		Me.rbFMRecordISORight.Location = New System.Drawing.Point(66, 89)
		Me.rbFMRecordISORight.Name = "rbFMRecordISORight"
		Me.rbFMRecordISORight.Size = New System.Drawing.Size(193, 17)
		Me.rbFMRecordISORight.TabIndex = 4
		Me.rbFMRecordISORight.TabStop = True
		Me.rbFMRecordISORight.Text = "ISO/IEC 19794-2:2005 (FMRecord)"
		Me.rbFMRecordISORight.UseVisualStyleBackColor = True
		' 
		' rbFMRecordANSIRight
		' 
		Me.rbFMRecordANSIRight.AutoSize = True
		Me.rbFMRecordANSIRight.Location = New System.Drawing.Point(66, 66)
		Me.rbFMRecordANSIRight.Name = "rbFMRecordANSIRight"
		Me.rbFMRecordANSIRight.Size = New System.Drawing.Size(195, 17)
		Me.rbFMRecordANSIRight.TabIndex = 3
		Me.rbFMRecordANSIRight.TabStop = True
		Me.rbFMRecordANSIRight.Text = "ANSI INCITS 378-2004 (FMRecord)"
		Me.rbFMRecordANSIRight.UseVisualStyleBackColor = True
		' 
		' rbANTemplateRight
		' 
		Me.rbANTemplateRight.AutoSize = True
		Me.rbANTemplateRight.Location = New System.Drawing.Point(66, 43)
		Me.rbANTemplateRight.Name = "rbANTemplateRight"
		Me.rbANTemplateRight.Size = New System.Drawing.Size(203, 17)
		Me.rbANTemplateRight.TabIndex = 2
		Me.rbANTemplateRight.TabStop = True
		Me.rbANTemplateRight.Text = "ANSI/NIST-ITL 1-2000 (ANTemplate)"
		Me.rbANTemplateRight.UseVisualStyleBackColor = True
		' 
		' label5
		' 
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(16, 27)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(0, 13)
		Me.label5.TabIndex = 1
		' 
		' openImageButton
		' 
		Me.openImageButton.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.openImageButton.Image = (CType(resources.GetObject("openImageButton.Image"), System.Drawing.Image))
		Me.openImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.openImageButton.Location = New System.Drawing.Point(85, 243)
		Me.openImageButton.Name = "openImageButton"
		Me.openImageButton.Size = New System.Drawing.Size(181, 23)
		Me.openImageButton.TabIndex = 1
		Me.openImageButton.Text = "Open template"
		Me.openImageButton.UseVisualStyleBackColor = True
		'			Me.openImageButton.Click += New System.EventHandler(Me.openImageButton1_Click);
		' 
		' rtbLeft
		' 
		Me.rtbLeft.BackColor = System.Drawing.SystemColors.Window
		Me.rtbLeft.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rtbLeft.Location = New System.Drawing.Point(3, 275)
		Me.rtbLeft.Name = "rtbLeft"
		Me.rtbLeft.ReadOnly = True
		Me.rtbLeft.Size = New System.Drawing.Size(345, 242)
		Me.rtbLeft.TabIndex = 2
		Me.rtbLeft.TabStop = False
		Me.rtbLeft.Text = ""
		Me.rtbLeft.WordWrap = False
		' 
		' MainForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(732, 576)
		Me.Controls.Add(Me.tabControl1)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.Name = "MainForm"
		Me.Text = "Neurotechnology Template Conversion Sample"
		Me.tabControl1.ResumeLayout(False)
		Me.tabPage1.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.gbOpenning.ResumeLayout(False)
		Me.gbOpenning.PerformLayout()
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tabControl1 As System.Windows.Forms.TabControl
	Private tabPage1 As System.Windows.Forms.TabPage
	Private gbOpenning As System.Windows.Forms.GroupBox
	Private label1 As System.Windows.Forms.Label
	Private WithEvents openImageButton As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private label5 As System.Windows.Forms.Label
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents rbFMRecordISO As System.Windows.Forms.RadioButton
	Private WithEvents rbFMRecordANSI As System.Windows.Forms.RadioButton
	Private WithEvents rbANTemplate As System.Windows.Forms.RadioButton
	Private label6 As System.Windows.Forms.Label
	Private WithEvents rbNFTemplate As System.Windows.Forms.RadioButton
	Private WithEvents rbNFRecords As System.Windows.Forms.RadioButton
	Private WithEvents rbNLRecord As System.Windows.Forms.RadioButton
	Private WithEvents rbNLTemplate As System.Windows.Forms.RadioButton
	Private WithEvents rbNTemplate As System.Windows.Forms.RadioButton
	Private label7 As System.Windows.Forms.Label
	Private rbNLRecordRight As System.Windows.Forms.RadioButton
	Private rbNLTemplateRight As System.Windows.Forms.RadioButton
	Private rbNTemplateRight As System.Windows.Forms.RadioButton
	Private rbNFTemplateRight As System.Windows.Forms.RadioButton
	Private rbNFRecordsRight As System.Windows.Forms.RadioButton
	Private rbFMRecordISORight As System.Windows.Forms.RadioButton
	Private rbFMRecordANSIRight As System.Windows.Forms.RadioButton
	Private rbANTemplateRight As System.Windows.Forms.RadioButton
	Private WithEvents btnConvertAndSave As System.Windows.Forms.Button
	Private rtbRight As System.Windows.Forms.RichTextBox
	Private rtbLeft As System.Windows.Forms.RichTextBox
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
End Class


