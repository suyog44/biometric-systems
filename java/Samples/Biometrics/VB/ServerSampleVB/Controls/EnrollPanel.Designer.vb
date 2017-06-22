Imports Microsoft.VisualBasic
Imports System
Partial Public Class EnrollPanel
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
		Me.pbarProgress = New System.Windows.Forms.ProgressBar
		Me.btnStart = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.gbResults = New System.Windows.Forms.GroupBox
		Me.rtbStatus = New System.Windows.Forms.RichTextBox
		Me.tbTaskCount = New System.Windows.Forms.TextBox
		Me.label1 = New System.Windows.Forms.Label
		Me.pbStatus = New System.Windows.Forms.PictureBox
		Me.label5 = New System.Windows.Forms.Label
		Me.tbTime = New System.Windows.Forms.TextBox
		Me.lblProgress = New System.Windows.Forms.Label
		Me.gbProperties = New System.Windows.Forms.GroupBox
		Me.nudBunchSize = New System.Windows.Forms.NumericUpDown
		Me.label4 = New System.Windows.Forms.Label
		Me.lblRemaining = New System.Windows.Forms.Label
		Me.gbResults.SuspendLayout()
		CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbProperties.SuspendLayout()
		CType(Me.nudBunchSize, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'pbarProgress
		'
		Me.pbarProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pbarProgress.Location = New System.Drawing.Point(3, 76)
		Me.pbarProgress.Name = "pbarProgress"
		Me.pbarProgress.Size = New System.Drawing.Size(707, 23)
		Me.pbarProgress.TabIndex = 0
		'
		'btnStart
		'
		Me.btnStart.Location = New System.Drawing.Point(3, 3)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(75, 23)
		Me.btnStart.TabIndex = 1
		Me.btnStart.Text = "Start"
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Enabled = False
		Me.btnCancel.Location = New System.Drawing.Point(3, 32)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 2
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'gbResults
		'
		Me.gbResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbResults.Controls.Add(Me.rtbStatus)
		Me.gbResults.Controls.Add(Me.tbTaskCount)
		Me.gbResults.Controls.Add(Me.label1)
		Me.gbResults.Controls.Add(Me.pbStatus)
		Me.gbResults.Controls.Add(Me.label5)
		Me.gbResults.Controls.Add(Me.tbTime)
		Me.gbResults.Location = New System.Drawing.Point(3, 105)
		Me.gbResults.Name = "gbResults"
		Me.gbResults.Size = New System.Drawing.Size(707, 211)
		Me.gbResults.TabIndex = 19
		Me.gbResults.TabStop = False
		Me.gbResults.Text = "Results"
		'
		'rtbStatus
		'
		Me.rtbStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.rtbStatus.Location = New System.Drawing.Point(68, 51)
		Me.rtbStatus.Name = "rtbStatus"
		Me.rtbStatus.ReadOnly = True
		Me.rtbStatus.Size = New System.Drawing.Size(631, 154)
		Me.rtbStatus.TabIndex = 16
		Me.rtbStatus.Text = ""
		'
		'tbTaskCount
		'
		Me.tbTaskCount.Location = New System.Drawing.Point(111, 19)
		Me.tbTaskCount.Name = "tbTaskCount"
		Me.tbTaskCount.ReadOnly = True
		Me.tbTaskCount.Size = New System.Drawing.Size(109, 20)
		Me.tbTaskCount.TabIndex = 3
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(7, 22)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(99, 13)
		Me.label1.TabIndex = 5
		Me.label1.Text = "Templates to enroll:"
		'
		'pbStatus
		'
		Me.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.pbStatus.Location = New System.Drawing.Point(10, 51)
		Me.pbStatus.Name = "pbStatus"
		Me.pbStatus.Size = New System.Drawing.Size(52, 50)
		Me.pbStatus.TabIndex = 15
		Me.pbStatus.TabStop = False
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(237, 22)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(73, 13)
		Me.label5.TabIndex = 11
		Me.label5.Text = "Time elapsed:"
		'
		'tbTime
		'
		Me.tbTime.Location = New System.Drawing.Point(316, 19)
		Me.tbTime.Name = "tbTime"
		Me.tbTime.ReadOnly = True
		Me.tbTime.Size = New System.Drawing.Size(136, 20)
		Me.tbTime.TabIndex = 12
		Me.tbTime.Text = "N/A"
		'
		'lblProgress
		'
		Me.lblProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblProgress.Location = New System.Drawing.Point(343, 60)
		Me.lblProgress.Name = "lblProgress"
		Me.lblProgress.Size = New System.Drawing.Size(367, 13)
		Me.lblProgress.TabIndex = 20
		Me.lblProgress.Text = "progress"
		Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'gbProperties
		'
		Me.gbProperties.Controls.Add(Me.nudBunchSize)
		Me.gbProperties.Controls.Add(Me.label4)
		Me.gbProperties.Location = New System.Drawing.Point(84, 3)
		Me.gbProperties.Name = "gbProperties"
		Me.gbProperties.Size = New System.Drawing.Size(371, 41)
		Me.gbProperties.TabIndex = 24
		Me.gbProperties.TabStop = False
		Me.gbProperties.Text = "Properties"
		'
		'nudBunchSize
		'
		Me.nudBunchSize.Location = New System.Drawing.Point(101, 14)
		Me.nudBunchSize.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
		Me.nudBunchSize.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.nudBunchSize.Name = "nudBunchSize"
		Me.nudBunchSize.Size = New System.Drawing.Size(128, 20)
		Me.nudBunchSize.TabIndex = 27
		Me.nudBunchSize.Value = New Decimal(New Integer() {350, 0, 0, 0})
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(13, 16)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(62, 13)
		Me.label4.TabIndex = 26
		Me.label4.Text = "Bunch size:"
		'
		'lblRemaining
		'
		Me.lblRemaining.AutoSize = True
		Me.lblRemaining.Location = New System.Drawing.Point(3, 60)
		Me.lblRemaining.Name = "lblRemaining"
		Me.lblRemaining.Size = New System.Drawing.Size(126, 13)
		Me.lblRemaining.TabIndex = 25
		Me.lblRemaining.Text = "Estimated time remaining:"
		'
		'EnrollPanel
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.lblRemaining)
		Me.Controls.Add(Me.gbProperties)
		Me.Controls.Add(Me.lblProgress)
		Me.Controls.Add(Me.gbResults)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnStart)
		Me.Controls.Add(Me.pbarProgress)
		Me.Name = "EnrollPanel"
		Me.Size = New System.Drawing.Size(713, 319)
		Me.gbResults.ResumeLayout(False)
		Me.gbResults.PerformLayout()
		CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbProperties.ResumeLayout(False)
		Me.gbProperties.PerformLayout()
		CType(Me.nudBunchSize, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private pbarProgress As System.Windows.Forms.ProgressBar
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private gbResults As System.Windows.Forms.GroupBox
	Private rtbStatus As System.Windows.Forms.RichTextBox
	Private tbTaskCount As System.Windows.Forms.TextBox
	Private label1 As System.Windows.Forms.Label
	Private pbStatus As System.Windows.Forms.PictureBox
	Private label5 As System.Windows.Forms.Label
	Private tbTime As System.Windows.Forms.TextBox
	Private lblProgress As System.Windows.Forms.Label
	Private gbProperties As System.Windows.Forms.GroupBox
	Private lblRemaining As System.Windows.Forms.Label
	Private nudBunchSize As System.Windows.Forms.NumericUpDown
	Private label4 As System.Windows.Forms.Label
End Class
