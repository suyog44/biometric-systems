Imports Microsoft.VisualBasic
Imports System

Partial Public Class EnrollFromFile
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnrollFromFile))
		Me.btnOpen = New System.Windows.Forms.Button
		Me.lblSoundFile = New System.Windows.Forms.Label
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.btnExtract = New System.Windows.Forms.Button
		Me.nudPhraseId = New System.Windows.Forms.NumericUpDown
		Me.label1 = New System.Windows.Forms.Label
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.chbExtractTextIndependent = New System.Windows.Forms.CheckBox
		Me.chbExtractTextDependent = New System.Windows.Forms.CheckBox
		Me.lblStatus = New System.Windows.Forms.Label
		Me.label5 = New System.Windows.Forms.Label
		Me.btnSaveVoice = New System.Windows.Forms.Button
		Me.saveVoiceFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		CType(Me.nudPhraseId, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnOpen
		'
		Me.btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), System.Drawing.Image)
		Me.btnOpen.Location = New System.Drawing.Point(3, 54)
		Me.btnOpen.Name = "btnOpen"
		Me.btnOpen.Size = New System.Drawing.Size(110, 23)
		Me.btnOpen.TabIndex = 4
		Me.btnOpen.Tag = "Open"
		Me.btnOpen.Text = "Open file"
		Me.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpen.UseVisualStyleBackColor = True
		'
		'lblSoundFile
		'
		Me.lblSoundFile.AutoSize = True
		Me.lblSoundFile.Location = New System.Drawing.Point(177, 59)
		Me.lblSoundFile.Name = "lblSoundFile"
		Me.lblSoundFile.Size = New System.Drawing.Size(94, 13)
		Me.lblSoundFile.TabIndex = 5
		Me.lblSoundFile.Text = "Sound file location"
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Image = CType(resources.GetObject("btnSaveTemplate.Image"), System.Drawing.Image)
		Me.btnSaveTemplate.Location = New System.Drawing.Point(3, 197)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(110, 23)
		Me.btnSaveTemplate.TabIndex = 7
		Me.btnSaveTemplate.Text = "Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'btnExtract
		'
		Me.btnExtract.Location = New System.Drawing.Point(3, 152)
		Me.btnExtract.Name = "btnExtract"
		Me.btnExtract.Size = New System.Drawing.Size(110, 23)
		Me.btnExtract.TabIndex = 8
		Me.btnExtract.Text = "&Extract"
		Me.btnExtract.UseVisualStyleBackColor = True
		'
		'nudPhraseId
		'
		Me.nudPhraseId.Location = New System.Drawing.Point(184, 155)
		Me.nudPhraseId.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
		Me.nudPhraseId.Name = "nudPhraseId"
		Me.nudPhraseId.Size = New System.Drawing.Size(93, 20)
		Me.nudPhraseId.TabIndex = 9
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(123, 157)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(55, 13)
		Me.label1.TabIndex = 10
		Me.label1.Text = "Phrase Id:"
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.chbExtractTextIndependent)
		Me.groupBox1.Controls.Add(Me.chbExtractTextDependent)
		Me.groupBox1.Location = New System.Drawing.Point(3, 83)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(264, 64)
		Me.groupBox1.TabIndex = 15
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Options"
		'
		'chbExtractTextIndependent
		'
		Me.chbExtractTextIndependent.AutoSize = True
		Me.chbExtractTextIndependent.Checked = True
		Me.chbExtractTextIndependent.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbExtractTextIndependent.Location = New System.Drawing.Point(6, 42)
		Me.chbExtractTextIndependent.Name = "chbExtractTextIndependent"
		Me.chbExtractTextIndependent.Size = New System.Drawing.Size(182, 17)
		Me.chbExtractTextIndependent.TabIndex = 23
		Me.chbExtractTextIndependent.Text = "Extract text independent features"
		Me.chbExtractTextIndependent.UseVisualStyleBackColor = True
		'
		'chbExtractTextDependent
		'
		Me.chbExtractTextDependent.AutoSize = True
		Me.chbExtractTextDependent.Checked = True
		Me.chbExtractTextDependent.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbExtractTextDependent.Location = New System.Drawing.Point(6, 19)
		Me.chbExtractTextDependent.Name = "chbExtractTextDependent"
		Me.chbExtractTextDependent.Size = New System.Drawing.Size(174, 17)
		Me.chbExtractTextDependent.TabIndex = 22
		Me.chbExtractTextDependent.Text = "Extract text dependent features"
		Me.chbExtractTextDependent.UseVisualStyleBackColor = True
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
		Me.lblStatus.Location = New System.Drawing.Point(0, 178)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(104, 13)
		Me.lblStatus.TabIndex = 16
		Me.lblStatus.Text = "Extraction Status"
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(115, 59)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(60, 13)
		Me.label5.TabIndex = 17
		Me.label5.Text = "Sound File:"
		'
		'btnSaveVoice
		'
		Me.btnSaveVoice.Image = CType(resources.GetObject("btnSaveVoice.Image"), System.Drawing.Image)
		Me.btnSaveVoice.Location = New System.Drawing.Point(118, 197)
		Me.btnSaveVoice.Name = "btnSaveVoice"
		Me.btnSaveVoice.Size = New System.Drawing.Size(121, 23)
		Me.btnSaveVoice.TabIndex = 19
		Me.btnSaveVoice.Text = "Save Voice Audio"
		Me.btnSaveVoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveVoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveVoice.UseVisualStyleBackColor = True
		'
		'saveVoiceFileDialog
		'
		Me.saveVoiceFileDialog.Filter = "Wave audio files (*.wav;*.wave)|*.wav;*.wave"
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(3, 3)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Media,Biometrics.VoiceExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(377, 45)
		Me.licensePanel.TabIndex = 18
		'
		'EnrollFromFile
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.btnSaveVoice)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.label5)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.nudPhraseId)
		Me.Controls.Add(Me.btnExtract)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Controls.Add(Me.lblSoundFile)
		Me.Controls.Add(Me.btnOpen)
		Me.Name = "EnrollFromFile"
		Me.Size = New System.Drawing.Size(380, 294)
		CType(Me.nudPhraseId, System.ComponentModel.ISupportInitialize).EndInit()
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents btnOpen As System.Windows.Forms.Button
	Private lblSoundFile As System.Windows.Forms.Label
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private WithEvents btnExtract As System.Windows.Forms.Button
	Private nudPhraseId As System.Windows.Forms.NumericUpDown
	Private label1 As System.Windows.Forms.Label
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private lblStatus As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private licensePanel As LicensePanel
	Private WithEvents btnSaveVoice As System.Windows.Forms.Button
	Private WithEvents saveVoiceFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents chbExtractTextIndependent As System.Windows.Forms.CheckBox
	Private WithEvents chbExtractTextDependent As System.Windows.Forms.CheckBox
End Class

