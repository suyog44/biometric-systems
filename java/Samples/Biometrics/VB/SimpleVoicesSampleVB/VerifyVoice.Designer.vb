Imports Microsoft.VisualBasic
Imports System

Partial Public Class VerifyVoice
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerifyVoice))
		Me.btnDefault = New System.Windows.Forms.Button
		Me.matchingFarLabel = New System.Windows.Forms.Label
		Me.lblSecondTemplate = New System.Windows.Forms.Label
		Me.lblFirstTemplate = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.btnVerify = New System.Windows.Forms.Button
		Me.label1 = New System.Windows.Forms.Label
		Me.btnOpen1 = New System.Windows.Forms.Button
		Me.btnOpen2 = New System.Windows.Forms.Button
		Me.gbMatching = New System.Windows.Forms.GroupBox
		Me.chbUniquePhrases = New System.Windows.Forms.CheckBox
		Me.cbMatchingFAR = New System.Windows.Forms.ComboBox
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.lblMsg = New System.Windows.Forms.Label
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.gbMatching.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnDefault
		'
		Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnDefault.Location = New System.Drawing.Point(162, 11)
		Me.btnDefault.Name = "btnDefault"
		Me.btnDefault.Size = New System.Drawing.Size(63, 23)
		Me.btnDefault.TabIndex = 20
		Me.btnDefault.Text = "Default"
		Me.btnDefault.UseVisualStyleBackColor = True
		'
		'matchingFarLabel
		'
		Me.matchingFarLabel.AutoSize = True
		Me.matchingFarLabel.Location = New System.Drawing.Point(9, 16)
		Me.matchingFarLabel.Name = "matchingFarLabel"
		Me.matchingFarLabel.Size = New System.Drawing.Size(78, 13)
		Me.matchingFarLabel.TabIndex = 18
		Me.matchingFarLabel.Text = "Matching &FAR:"
		'
		'lblSecondTemplate
		'
		Me.lblSecondTemplate.AutoSize = True
		Me.lblSecondTemplate.Location = New System.Drawing.Point(204, 159)
		Me.lblSecondTemplate.Name = "lblSecondTemplate"
		Me.lblSecondTemplate.Size = New System.Drawing.Size(85, 13)
		Me.lblSecondTemplate.TabIndex = 50
		Me.lblSecondTemplate.Text = "second template"
		'
		'lblFirstTemplate
		'
		Me.lblFirstTemplate.AutoSize = True
		Me.lblFirstTemplate.Location = New System.Drawing.Point(204, 130)
		Me.lblFirstTemplate.Name = "lblFirstTemplate"
		Me.lblFirstTemplate.Size = New System.Drawing.Size(66, 13)
		Me.lblFirstTemplate.TabIndex = 49
		Me.lblFirstTemplate.Text = "first template"
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(51, 159)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(147, 13)
		Me.label3.TabIndex = 48
		Me.label3.Text = "Second template or audio file:"
		'
		'btnVerify
		'
		Me.btnVerify.Enabled = False
		Me.btnVerify.Location = New System.Drawing.Point(15, 183)
		Me.btnVerify.Name = "btnVerify"
		Me.btnVerify.Size = New System.Drawing.Size(121, 23)
		Me.btnVerify.TabIndex = 46
		Me.btnVerify.Text = "Verify"
		Me.btnVerify.UseVisualStyleBackColor = True
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(51, 130)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(129, 13)
		Me.label1.TabIndex = 47
		Me.label1.Text = "First template or audio file:"
		'
		'btnOpen1
		'
		Me.btnOpen1.Image = CType(resources.GetObject("btnOpen1.Image"), System.Drawing.Image)
		Me.btnOpen1.Location = New System.Drawing.Point(15, 125)
		Me.btnOpen1.Name = "btnOpen1"
		Me.btnOpen1.Size = New System.Drawing.Size(30, 23)
		Me.btnOpen1.TabIndex = 21
		Me.btnOpen1.UseVisualStyleBackColor = True
		'
		'btnOpen2
		'
		Me.btnOpen2.Image = CType(resources.GetObject("btnOpen2.Image"), System.Drawing.Image)
		Me.btnOpen2.Location = New System.Drawing.Point(15, 154)
		Me.btnOpen2.Name = "btnOpen2"
		Me.btnOpen2.Size = New System.Drawing.Size(30, 23)
		Me.btnOpen2.TabIndex = 22
		Me.btnOpen2.UseVisualStyleBackColor = True
		'
		'gbMatching
		'
		Me.gbMatching.Controls.Add(Me.chbUniquePhrases)
		Me.gbMatching.Controls.Add(Me.btnDefault)
		Me.gbMatching.Controls.Add(Me.matchingFarLabel)
		Me.gbMatching.Controls.Add(Me.cbMatchingFAR)
		Me.gbMatching.Location = New System.Drawing.Point(3, 52)
		Me.gbMatching.MaximumSize = New System.Drawing.Size(600, 200)
		Me.gbMatching.Name = "gbMatching"
		Me.gbMatching.Size = New System.Drawing.Size(242, 67)
		Me.gbMatching.TabIndex = 32
		Me.gbMatching.TabStop = False
		Me.gbMatching.Text = "Matcher properties"
		'
		'chbUniquePhrases
		'
		Me.chbUniquePhrases.AutoSize = True
		Me.chbUniquePhrases.Checked = True
		Me.chbUniquePhrases.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbUniquePhrases.Location = New System.Drawing.Point(93, 40)
		Me.chbUniquePhrases.Name = "chbUniquePhrases"
		Me.chbUniquePhrases.Size = New System.Drawing.Size(122, 17)
		Me.chbUniquePhrases.TabIndex = 22
		Me.chbUniquePhrases.Text = "Unique phrases only"
		Me.chbUniquePhrases.UseVisualStyleBackColor = True
		'
		'cbMatchingFAR
		'
		Me.cbMatchingFAR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbMatchingFAR.FormattingEnabled = True
		Me.cbMatchingFAR.Location = New System.Drawing.Point(93, 13)
		Me.cbMatchingFAR.Name = "cbMatchingFAR"
		Me.cbMatchingFAR.Size = New System.Drawing.Size(63, 21)
		Me.cbMatchingFAR.TabIndex = 19
		'
		'lblMsg
		'
		Me.lblMsg.AutoSize = True
		Me.lblMsg.Location = New System.Drawing.Point(12, 219)
		Me.lblMsg.Name = "lblMsg"
		Me.lblMsg.Size = New System.Drawing.Size(33, 13)
		Me.lblMsg.TabIndex = 45
		Me.lblMsg.Text = "score"
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
		Me.licensePanel.Size = New System.Drawing.Size(412, 45)
		Me.licensePanel.TabIndex = 51
		'
		'VerifyVoice
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.gbMatching)
		Me.Controls.Add(Me.btnOpen2)
		Me.Controls.Add(Me.btnOpen1)
		Me.Controls.Add(Me.lblSecondTemplate)
		Me.Controls.Add(Me.lblFirstTemplate)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.btnVerify)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.lblMsg)
		Me.Name = "VerifyVoice"
		Me.Size = New System.Drawing.Size(418, 244)
		Me.gbMatching.ResumeLayout(False)
		Me.gbMatching.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents btnDefault As System.Windows.Forms.Button
	Private matchingFarLabel As System.Windows.Forms.Label
	Private lblSecondTemplate As System.Windows.Forms.Label
	Private lblFirstTemplate As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private WithEvents btnVerify As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private WithEvents btnOpen1 As System.Windows.Forms.Button
	Private WithEvents btnOpen2 As System.Windows.Forms.Button
	Private gbMatching As System.Windows.Forms.GroupBox
	Private WithEvents cbMatchingFAR As System.Windows.Forms.ComboBox
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private lblMsg As System.Windows.Forms.Label
	Private licensePanel As LicensePanel
	Friend WithEvents chbUniquePhrases As System.Windows.Forms.CheckBox
End Class

