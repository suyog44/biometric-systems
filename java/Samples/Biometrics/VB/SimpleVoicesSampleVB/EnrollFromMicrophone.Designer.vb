Imports Microsoft.VisualBasic
Imports System

Partial Public Class EnrollFromMicrophone
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnrollFromMicrophone))
		Me.gbMicrophones = New System.Windows.Forms.GroupBox
		Me.btnStop = New System.Windows.Forms.Button
		Me.btnRefresh = New System.Windows.Forms.Button
		Me.btnStart = New System.Windows.Forms.Button
		Me.lbMicrophones = New System.Windows.Forms.ListBox
		Me.gbOptions = New System.Windows.Forms.GroupBox
		Me.chbExtractTextIndependent = New System.Windows.Forms.CheckBox
		Me.chbExtractTextDependent = New System.Windows.Forms.CheckBox
		Me.label1 = New System.Windows.Forms.Label
		Me.nudPhraseId = New System.Windows.Forms.NumericUpDown
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.label5 = New System.Windows.Forms.Label
		Me.backgroundWorker = New System.ComponentModel.BackgroundWorker
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.btnSaveVoice = New System.Windows.Forms.Button
		Me.saveVoiceFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.voiceView = New Neurotec.Biometrics.Gui.NVoiceView
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.chbCaptureAutomatically = New System.Windows.Forms.CheckBox
		Me.btnForce = New System.Windows.Forms.Button
		Me.gbMicrophones.SuspendLayout()
		Me.gbOptions.SuspendLayout()
		CType(Me.nudPhraseId, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'gbMicrophones
		'
		Me.gbMicrophones.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbMicrophones.Controls.Add(Me.btnForce)
		Me.gbMicrophones.Controls.Add(Me.btnStop)
		Me.gbMicrophones.Controls.Add(Me.btnRefresh)
		Me.gbMicrophones.Controls.Add(Me.btnStart)
		Me.gbMicrophones.Controls.Add(Me.lbMicrophones)
		Me.gbMicrophones.Location = New System.Drawing.Point(0, 76)
		Me.gbMicrophones.Name = "gbMicrophones"
		Me.gbMicrophones.Size = New System.Drawing.Size(331, 111)
		Me.gbMicrophones.TabIndex = 13
		Me.gbMicrophones.TabStop = False
		Me.gbMicrophones.Text = "Microphones list"
		'
		'btnStop
		'
		Me.btnStop.Enabled = False
		Me.btnStop.Location = New System.Drawing.Point(168, 80)
		Me.btnStop.Name = "btnStop"
		Me.btnStop.Size = New System.Drawing.Size(75, 23)
		Me.btnStop.TabIndex = 11
		Me.btnStop.Text = "Stop"
		Me.btnStop.UseVisualStyleBackColor = True
		'
		'btnRefresh
		'
		Me.btnRefresh.Location = New System.Drawing.Point(6, 80)
		Me.btnRefresh.Name = "btnRefresh"
		Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
		Me.btnRefresh.TabIndex = 10
		Me.btnRefresh.Text = "Refresh list"
		Me.btnRefresh.UseVisualStyleBackColor = True
		'
		'btnStart
		'
		Me.btnStart.Location = New System.Drawing.Point(87, 80)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(75, 23)
		Me.btnStart.TabIndex = 9
		Me.btnStart.Text = "Start"
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'lbMicrophones
		'
		Me.lbMicrophones.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbMicrophones.Location = New System.Drawing.Point(4, 19)
		Me.lbMicrophones.Name = "lbMicrophones"
		Me.lbMicrophones.Size = New System.Drawing.Size(321, 56)
		Me.lbMicrophones.TabIndex = 6
		'
		'gbOptions
		'
		Me.gbOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbOptions.Controls.Add(Me.chbCaptureAutomatically)
		Me.gbOptions.Controls.Add(Me.chbExtractTextIndependent)
		Me.gbOptions.Controls.Add(Me.chbExtractTextDependent)
		Me.gbOptions.Controls.Add(Me.label1)
		Me.gbOptions.Controls.Add(Me.nudPhraseId)
		Me.gbOptions.Location = New System.Drawing.Point(334, 76)
		Me.gbOptions.Name = "gbOptions"
		Me.gbOptions.Size = New System.Drawing.Size(194, 111)
		Me.gbOptions.TabIndex = 20
		Me.gbOptions.TabStop = False
		Me.gbOptions.Text = "Options"
		'
		'chbExtractTextIndependent
		'
		Me.chbExtractTextIndependent.AutoSize = True
		Me.chbExtractTextIndependent.Checked = True
		Me.chbExtractTextIndependent.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbExtractTextIndependent.Location = New System.Drawing.Point(6, 67)
		Me.chbExtractTextIndependent.Name = "chbExtractTextIndependent"
		Me.chbExtractTextIndependent.Size = New System.Drawing.Size(182, 17)
		Me.chbExtractTextIndependent.TabIndex = 25
		Me.chbExtractTextIndependent.Text = "Extract text independent features"
		Me.chbExtractTextIndependent.UseVisualStyleBackColor = True
		'
		'chbExtractTextDependent
		'
		Me.chbExtractTextDependent.AutoSize = True
		Me.chbExtractTextDependent.Checked = True
		Me.chbExtractTextDependent.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbExtractTextDependent.Location = New System.Drawing.Point(6, 44)
		Me.chbExtractTextDependent.Name = "chbExtractTextDependent"
		Me.chbExtractTextDependent.Size = New System.Drawing.Size(174, 17)
		Me.chbExtractTextDependent.TabIndex = 24
		Me.chbExtractTextDependent.Text = "Extract text dependent features"
		Me.chbExtractTextDependent.UseVisualStyleBackColor = True
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(3, 22)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(55, 13)
		Me.label1.TabIndex = 19
		Me.label1.Text = "Phrase Id:"
		'
		'nudPhraseId
		'
		Me.nudPhraseId.Location = New System.Drawing.Point(64, 20)
		Me.nudPhraseId.Maximum = New Decimal(New Integer() {2147483647, 0, 0, 0})
		Me.nudPhraseId.Name = "nudPhraseId"
		Me.nudPhraseId.Size = New System.Drawing.Size(93, 20)
		Me.nudPhraseId.TabIndex = 18
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Image = CType(resources.GetObject("btnSaveTemplate.Image"), System.Drawing.Image)
		Me.btnSaveTemplate.Location = New System.Drawing.Point(6, 252)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(110, 23)
		Me.btnSaveTemplate.TabIndex = 17
		Me.btnSaveTemplate.Text = "Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(-3, 54)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(224, 13)
		Me.label5.TabIndex = 22
		Me.label5.Text = "Select microphone, press start and say phrase"
		'
		'btnSaveVoice
		'
		Me.btnSaveVoice.Enabled = False
		Me.btnSaveVoice.Image = CType(resources.GetObject("btnSaveVoice.Image"), System.Drawing.Image)
		Me.btnSaveVoice.Location = New System.Drawing.Point(122, 252)
		Me.btnSaveVoice.Name = "btnSaveVoice"
		Me.btnSaveVoice.Size = New System.Drawing.Size(121, 23)
		Me.btnSaveVoice.TabIndex = 26
		Me.btnSaveVoice.Text = "Save Voice Audio"
		Me.btnSaveVoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveVoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveVoice.UseVisualStyleBackColor = True
		'
		'saveVoiceFileDialog
		'
		Me.saveVoiceFileDialog.Filter = "Wave audio files (*.wav;*.wave)|*.wav;*.wave"
		'
		'voiceView
		'
		Me.voiceView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.voiceView.BackColor = System.Drawing.Color.Transparent
		Me.voiceView.Location = New System.Drawing.Point(0, 192)
		Me.voiceView.Name = "voiceView"
		Me.voiceView.Size = New System.Drawing.Size(536, 54)
		Me.voiceView.TabIndex = 27
		Me.voiceView.Text = "voiceView"
		Me.voiceView.Voice = Nothing
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Devices.Microphones,Biometrics.VoiceExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(535, 45)
		Me.licensePanel.TabIndex = 25
		'
		'chbCaptureAutomatically
		'
		Me.chbCaptureAutomatically.AutoSize = True
		Me.chbCaptureAutomatically.Checked = True
		Me.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbCaptureAutomatically.Location = New System.Drawing.Point(6, 86)
		Me.chbCaptureAutomatically.Name = "chbCaptureAutomatically"
		Me.chbCaptureAutomatically.Size = New System.Drawing.Size(127, 17)
		Me.chbCaptureAutomatically.TabIndex = 26
		Me.chbCaptureAutomatically.Text = "Capture automatically"
		Me.chbCaptureAutomatically.UseVisualStyleBackColor = True
		'
		'btnForce
		'
		Me.btnForce.Location = New System.Drawing.Point(249, 80)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(75, 23)
		Me.btnForce.TabIndex = 12
		Me.btnForce.Text = "Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'EnrollFromMicrophone
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.voiceView)
		Me.Controls.Add(Me.btnSaveVoice)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.label5)
		Me.Controls.Add(Me.gbOptions)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Controls.Add(Me.gbMicrophones)
		Me.Name = "EnrollFromMicrophone"
		Me.Size = New System.Drawing.Size(535, 314)
		Me.gbMicrophones.ResumeLayout(False)
		Me.gbOptions.ResumeLayout(False)
		Me.gbOptions.PerformLayout()
		CType(Me.nudPhraseId, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private gbMicrophones As System.Windows.Forms.GroupBox
	Private WithEvents btnStop As System.Windows.Forms.Button
	Private WithEvents btnRefresh As System.Windows.Forms.Button
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private WithEvents lbMicrophones As System.Windows.Forms.ListBox
	Private gbOptions As System.Windows.Forms.GroupBox
	Private label1 As System.Windows.Forms.Label
	Private nudPhraseId As System.Windows.Forms.NumericUpDown
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private label5 As System.Windows.Forms.Label
	Private WithEvents backgroundWorker As System.ComponentModel.BackgroundWorker
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private licensePanel As LicensePanel
	Private WithEvents btnSaveVoice As System.Windows.Forms.Button
	Private WithEvents saveVoiceFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents chbExtractTextIndependent As System.Windows.Forms.CheckBox
	Private WithEvents chbExtractTextDependent As System.Windows.Forms.CheckBox
	Private WithEvents voiceView As Neurotec.Biometrics.Gui.NVoiceView
	Friend WithEvents chbCaptureAutomatically As System.Windows.Forms.CheckBox
	Friend WithEvents btnForce As System.Windows.Forms.Button
End Class

