Imports Microsoft.VisualBasic
Imports System

Partial Public Class IdentifyVoice
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IdentifyVoice))
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.lblTemplatesCount = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.btnOpenTemplates = New System.Windows.Forms.Button
		Me.matchingGroupBox = New System.Windows.Forms.GroupBox
		Me.chbUniquePhrases = New System.Windows.Forms.CheckBox
		Me.Label3 = New System.Windows.Forms.Label
		Me.btnDefault = New System.Windows.Forms.Button
		Me.cbMatchingFar = New System.Windows.Forms.ComboBox
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.lblFileForIdentification = New System.Windows.Forms.Label
		Me.btnOpenAudio = New System.Windows.Forms.Button
		Me.listView = New System.Windows.Forms.ListView
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.label2 = New System.Windows.Forms.Label
		Me.groupBox3 = New System.Windows.Forms.GroupBox
		Me.btnIdentify = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.groupBox1.SuspendLayout()
		Me.matchingGroupBox.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		Me.groupBox3.SuspendLayout()
		Me.SuspendLayout()
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.lblTemplatesCount)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.btnOpenTemplates)
		Me.groupBox1.Location = New System.Drawing.Point(3, 51)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(472, 57)
		Me.groupBox1.TabIndex = 15
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Templates loading"
		'
		'lblTemplatesCount
		'
		Me.lblTemplatesCount.AutoSize = True
		Me.lblTemplatesCount.Location = New System.Drawing.Point(142, 30)
		Me.lblTemplatesCount.Name = "lblTemplatesCount"
		Me.lblTemplatesCount.Size = New System.Drawing.Size(82, 13)
		Me.lblTemplatesCount.TabIndex = 7
		Me.lblTemplatesCount.Text = "templates count"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(42, 30)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(94, 13)
		Me.label1.TabIndex = 6
		Me.label1.Text = "Templates loaded:"
		'
		'btnOpenTemplates
		'
		Me.btnOpenTemplates.Image = CType(resources.GetObject("btnOpenTemplates.Image"), System.Drawing.Image)
		Me.btnOpenTemplates.Location = New System.Drawing.Point(12, 24)
		Me.btnOpenTemplates.Name = "btnOpenTemplates"
		Me.btnOpenTemplates.Size = New System.Drawing.Size(30, 23)
		Me.btnOpenTemplates.TabIndex = 5
		Me.btnOpenTemplates.UseVisualStyleBackColor = True
		'
		'matchingGroupBox
		'
		Me.matchingGroupBox.Controls.Add(Me.chbUniquePhrases)
		Me.matchingGroupBox.Controls.Add(Me.Label3)
		Me.matchingGroupBox.Controls.Add(Me.btnDefault)
		Me.matchingGroupBox.Controls.Add(Me.cbMatchingFar)
		Me.matchingGroupBox.Location = New System.Drawing.Point(203, 14)
		Me.matchingGroupBox.Name = "matchingGroupBox"
		Me.matchingGroupBox.Size = New System.Drawing.Size(260, 68)
		Me.matchingGroupBox.TabIndex = 21
		Me.matchingGroupBox.TabStop = False
		Me.matchingGroupBox.Text = "Matcher properties"
		'
		'chbUniquePhrases
		'
		Me.chbUniquePhrases.AutoSize = True
		Me.chbUniquePhrases.Checked = True
		Me.chbUniquePhrases.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbUniquePhrases.Location = New System.Drawing.Point(91, 46)
		Me.chbUniquePhrases.Name = "chbUniquePhrases"
		Me.chbUniquePhrases.Size = New System.Drawing.Size(122, 17)
		Me.chbUniquePhrases.TabIndex = 23
		Me.chbUniquePhrases.Text = "Unique phrases only"
		Me.chbUniquePhrases.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(6, 22)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(78, 13)
		Me.Label3.TabIndex = 22
		Me.Label3.Text = "Matching FAR:"
		'
		'btnDefault
		'
		Me.btnDefault.Location = New System.Drawing.Point(191, 17)
		Me.btnDefault.Name = "btnDefault"
		Me.btnDefault.Size = New System.Drawing.Size(63, 23)
		Me.btnDefault.TabIndex = 20
		Me.btnDefault.Text = "Default"
		Me.btnDefault.UseVisualStyleBackColor = True
		'
		'cbMatchingFar
		'
		Me.cbMatchingFar.FormattingEnabled = True
		Me.cbMatchingFar.Location = New System.Drawing.Point(91, 19)
		Me.cbMatchingFar.Name = "cbMatchingFar"
		Me.cbMatchingFar.Size = New System.Drawing.Size(86, 21)
		Me.cbMatchingFar.TabIndex = 19
		'
		'groupBox2
		'
		Me.groupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox2.Controls.Add(Me.lblFileForIdentification)
		Me.groupBox2.Controls.Add(Me.btnOpenAudio)
		Me.groupBox2.Location = New System.Drawing.Point(6, 114)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(469, 53)
		Me.groupBox2.TabIndex = 16
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Voice file / template for identification"
		'
		'lblFileForIdentification
		'
		Me.lblFileForIdentification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblFileForIdentification.Location = New System.Drawing.Point(125, 29)
		Me.lblFileForIdentification.Name = "lblFileForIdentification"
		Me.lblFileForIdentification.Size = New System.Drawing.Size(338, 13)
		Me.lblFileForIdentification.TabIndex = 10
		Me.lblFileForIdentification.Text = "file for identification"
		'
		'btnOpenAudio
		'
		Me.btnOpenAudio.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnOpenAudio.Image = CType(resources.GetObject("btnOpenAudio.Image"), System.Drawing.Image)
		Me.btnOpenAudio.Location = New System.Drawing.Point(9, 24)
		Me.btnOpenAudio.Name = "btnOpenAudio"
		Me.btnOpenAudio.Size = New System.Drawing.Size(110, 23)
		Me.btnOpenAudio.TabIndex = 8
		Me.btnOpenAudio.Text = "Open"
		Me.btnOpenAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnOpenAudio.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpenAudio.UseVisualStyleBackColor = True
		'
		'listView
		'
		Me.listView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.listView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
		Me.listView.Location = New System.Drawing.Point(9, 88)
		Me.listView.Name = "listView"
		Me.listView.Size = New System.Drawing.Size(454, 91)
		Me.listView.TabIndex = 2
		Me.listView.UseCompatibleStateImageBehavior = False
		Me.listView.View = System.Windows.Forms.View.Details
		'
		'columnHeader1
		'
		Me.columnHeader1.Text = "ID"
		Me.columnHeader1.Width = 300
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Score"
		Me.columnHeader2.Width = 100
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(12, 56)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(0, 13)
		Me.label2.TabIndex = 1
		'
		'groupBox3
		'
		Me.groupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox3.Controls.Add(Me.matchingGroupBox)
		Me.groupBox3.Controls.Add(Me.listView)
		Me.groupBox3.Controls.Add(Me.label2)
		Me.groupBox3.Controls.Add(Me.btnIdentify)
		Me.groupBox3.Location = New System.Drawing.Point(6, 173)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(469, 185)
		Me.groupBox3.TabIndex = 17
		Me.groupBox3.TabStop = False
		Me.groupBox3.Text = "Identification"
		'
		'btnIdentify
		'
		Me.btnIdentify.Enabled = False
		Me.btnIdentify.Location = New System.Drawing.Point(9, 19)
		Me.btnIdentify.Name = "btnIdentify"
		Me.btnIdentify.Size = New System.Drawing.Size(110, 23)
		Me.btnIdentify.TabIndex = 0
		Me.btnIdentify.Text = "&Identify"
		Me.btnIdentify.UseVisualStyleBackColor = True
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(3, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.VoiceMatching,Biometrics.VoiceExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(472, 45)
		Me.licensePanel.TabIndex = 26
		'
		'IdentifyVoice
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox3)
		Me.Name = "IdentifyVoice"
		Me.Size = New System.Drawing.Size(478, 361)
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.matchingGroupBox.ResumeLayout(False)
		Me.matchingGroupBox.PerformLayout()
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox3.ResumeLayout(False)
		Me.groupBox3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private groupBox1 As System.Windows.Forms.GroupBox
	Private lblTemplatesCount As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private WithEvents btnOpenTemplates As System.Windows.Forms.Button
	Private matchingGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents btnDefault As System.Windows.Forms.Button
	Private WithEvents cbMatchingFar As System.Windows.Forms.ComboBox
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private lblFileForIdentification As System.Windows.Forms.Label
	Private WithEvents btnOpenAudio As System.Windows.Forms.Button
	Private listView As System.Windows.Forms.ListView
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private label2 As System.Windows.Forms.Label
	Private groupBox3 As System.Windows.Forms.GroupBox
	Private WithEvents btnIdentify As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private licensePanel As LicensePanel
	Friend WithEvents chbUniquePhrases As System.Windows.Forms.CheckBox
	Friend WithEvents Label3 As System.Windows.Forms.Label
End Class

