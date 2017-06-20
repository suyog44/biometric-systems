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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.tabControl1 = New System.Windows.Forms.TabControl
		Me.tabPageEnrollFromFile = New System.Windows.Forms.TabPage
		Me.enrollFromFilePanel = New Neurotec.Samples.EnrollFromFile
		Me.tabPageEnrollFromMicrophone = New System.Windows.Forms.TabPage
		Me.enrollFromMicrophonePanel = New Neurotec.Samples.EnrollFromMicrophone
		Me.tabPageVerify = New System.Windows.Forms.TabPage
		Me.verifyVoicePanel = New Neurotec.Samples.VerifyVoice
		Me.tabPageIdentify = New System.Windows.Forms.TabPage
		Me.identifyVoicePanel = New Neurotec.Samples.IdentifyVoice
		Me.tabControl1.SuspendLayout()
		Me.tabPageEnrollFromFile.SuspendLayout()
		Me.tabPageEnrollFromMicrophone.SuspendLayout()
		Me.tabPageVerify.SuspendLayout()
		Me.tabPageIdentify.SuspendLayout()
		Me.SuspendLayout()
		'
		'tabControl1
		'
		Me.tabControl1.Controls.Add(Me.tabPageEnrollFromFile)
		Me.tabControl1.Controls.Add(Me.tabPageEnrollFromMicrophone)
		Me.tabControl1.Controls.Add(Me.tabPageVerify)
		Me.tabControl1.Controls.Add(Me.tabPageIdentify)
		Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tabControl1.Location = New System.Drawing.Point(0, 0)
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = New System.Drawing.Size(551, 398)
		Me.tabControl1.TabIndex = 0
		'
		'tabPageEnrollFromFile
		'
		Me.tabPageEnrollFromFile.Controls.Add(Me.enrollFromFilePanel)
		Me.tabPageEnrollFromFile.Location = New System.Drawing.Point(4, 22)
		Me.tabPageEnrollFromFile.Name = "tabPageEnrollFromFile"
		Me.tabPageEnrollFromFile.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPageEnrollFromFile.Size = New System.Drawing.Size(543, 372)
		Me.tabPageEnrollFromFile.TabIndex = 0
		Me.tabPageEnrollFromFile.Text = "Enroll From File"
		Me.tabPageEnrollFromFile.UseVisualStyleBackColor = True
		'
		'enrollFromFilePanel
		'
		Me.enrollFromFilePanel.BiometricClient = Nothing
		Me.enrollFromFilePanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.enrollFromFilePanel.Location = New System.Drawing.Point(3, 3)
		Me.enrollFromFilePanel.Name = "enrollFromFilePanel"
		Me.enrollFromFilePanel.Size = New System.Drawing.Size(537, 366)
		Me.enrollFromFilePanel.TabIndex = 0
		'
		'tabPageEnrollFromMicrophone
		'
		Me.tabPageEnrollFromMicrophone.Controls.Add(Me.enrollFromMicrophonePanel)
		Me.tabPageEnrollFromMicrophone.Location = New System.Drawing.Point(4, 22)
		Me.tabPageEnrollFromMicrophone.Name = "tabPageEnrollFromMicrophone"
		Me.tabPageEnrollFromMicrophone.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPageEnrollFromMicrophone.Size = New System.Drawing.Size(543, 372)
		Me.tabPageEnrollFromMicrophone.TabIndex = 1
		Me.tabPageEnrollFromMicrophone.Text = "Enroll From Microphone"
		Me.tabPageEnrollFromMicrophone.UseVisualStyleBackColor = True
		'
		'enrollFromMicrophonePanel
		'
		Me.enrollFromMicrophonePanel.BiometricClient = Nothing
		Me.enrollFromMicrophonePanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.enrollFromMicrophonePanel.Location = New System.Drawing.Point(3, 3)
		Me.enrollFromMicrophonePanel.Name = "enrollFromMicrophonePanel"
		Me.enrollFromMicrophonePanel.Size = New System.Drawing.Size(537, 366)
		Me.enrollFromMicrophonePanel.TabIndex = 0
		'
		'tabPageVerify
		'
		Me.tabPageVerify.Controls.Add(Me.verifyVoicePanel)
		Me.tabPageVerify.Location = New System.Drawing.Point(4, 22)
		Me.tabPageVerify.Name = "tabPageVerify"
		Me.tabPageVerify.Size = New System.Drawing.Size(543, 372)
		Me.tabPageVerify.TabIndex = 2
		Me.tabPageVerify.Text = "Verify Voice"
		Me.tabPageVerify.UseVisualStyleBackColor = True
		'
		'verifyVoicePanel
		'
		Me.verifyVoicePanel.BiometricClient = Nothing
		Me.verifyVoicePanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.verifyVoicePanel.Location = New System.Drawing.Point(0, 0)
		Me.verifyVoicePanel.Name = "verifyVoicePanel"
		Me.verifyVoicePanel.Size = New System.Drawing.Size(543, 372)
		Me.verifyVoicePanel.TabIndex = 0
		'
		'tabPageIdentify
		'
		Me.tabPageIdentify.Controls.Add(Me.identifyVoicePanel)
		Me.tabPageIdentify.Location = New System.Drawing.Point(4, 22)
		Me.tabPageIdentify.Name = "tabPageIdentify"
		Me.tabPageIdentify.Size = New System.Drawing.Size(543, 372)
		Me.tabPageIdentify.TabIndex = 3
		Me.tabPageIdentify.Text = "Identify Voice"
		Me.tabPageIdentify.UseVisualStyleBackColor = True
		'
		'identifyVoicePanel
		'
		Me.identifyVoicePanel.BiometricClient = Nothing
		Me.identifyVoicePanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.identifyVoicePanel.Location = New System.Drawing.Point(0, 0)
		Me.identifyVoicePanel.Name = "identifyVoicePanel"
		Me.identifyVoicePanel.Size = New System.Drawing.Size(543, 372)
		Me.identifyVoicePanel.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(551, 398)
		Me.Controls.Add(Me.tabControl1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Simple Voices Sample"
		Me.tabControl1.ResumeLayout(False)
		Me.tabPageEnrollFromFile.ResumeLayout(False)
		Me.tabPageEnrollFromMicrophone.ResumeLayout(False)
		Me.tabPageVerify.ResumeLayout(False)
		Me.tabPageIdentify.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents tabControl1 As System.Windows.Forms.TabControl
	Private tabPageEnrollFromFile As System.Windows.Forms.TabPage
	Private tabPageEnrollFromMicrophone As System.Windows.Forms.TabPage
	Private tabPageVerify As System.Windows.Forms.TabPage
	Private tabPageIdentify As System.Windows.Forms.TabPage
	Private enrollFromFilePanel As EnrollFromFile
	Private enrollFromMicrophonePanel As EnrollFromMicrophone
	Private verifyVoicePanel As VerifyVoice
	Private identifyVoicePanel As IdentifyVoice
End Class


