Imports Microsoft.VisualBasic
Imports System
Partial Public Class DeduplicationPanel
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
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnStart = New System.Windows.Forms.Button
		Me.pbarProgress = New System.Windows.Forms.ProgressBar
		Me.lblProgress = New System.Windows.Forms.Label
		Me.gbProperties = New System.Windows.Forms.GroupBox
		Me.btnBrowse = New System.Windows.Forms.Button
		Me.label5 = New System.Windows.Forms.Label
		Me.tbLogFile = New System.Windows.Forms.TextBox
		Me.rtbStatus = New System.Windows.Forms.RichTextBox
		Me.pbStatus = New System.Windows.Forms.PictureBox
		Me.lblRemaining = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.gbProperties.SuspendLayout()
		CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btnCancel
		'
		Me.btnCancel.Enabled = False
		Me.btnCancel.Location = New System.Drawing.Point(6, 32)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 25
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnStart
		'
		Me.btnStart.Location = New System.Drawing.Point(6, 3)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(75, 23)
		Me.btnStart.TabIndex = 24
		Me.btnStart.Text = "Start"
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'pbarProgress
		'
		Me.pbarProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pbarProgress.Location = New System.Drawing.Point(3, 85)
		Me.pbarProgress.Name = "pbarProgress"
		Me.pbarProgress.Size = New System.Drawing.Size(758, 23)
		Me.pbarProgress.TabIndex = 23
		'
		'lblProgress
		'
		Me.lblProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblProgress.Location = New System.Drawing.Point(387, 69)
		Me.lblProgress.Name = "lblProgress"
		Me.lblProgress.Size = New System.Drawing.Size(371, 13)
		Me.lblProgress.TabIndex = 28
		Me.lblProgress.Text = "progress label"
		Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'gbProperties
		'
		Me.gbProperties.Controls.Add(Me.btnBrowse)
		Me.gbProperties.Controls.Add(Me.label5)
		Me.gbProperties.Controls.Add(Me.tbLogFile)
		Me.gbProperties.Location = New System.Drawing.Point(87, 3)
		Me.gbProperties.Name = "gbProperties"
		Me.gbProperties.Size = New System.Drawing.Size(527, 52)
		Me.gbProperties.TabIndex = 30
		Me.gbProperties.TabStop = False
		Me.gbProperties.Text = "Properites"
		'
		'btnBrowse
		'
		Me.btnBrowse.Location = New System.Drawing.Point(482, 17)
		Me.btnBrowse.Name = "btnBrowse"
		Me.btnBrowse.Size = New System.Drawing.Size(35, 23)
		Me.btnBrowse.TabIndex = 34
		Me.btnBrowse.Text = "..."
		Me.btnBrowse.UseVisualStyleBackColor = True
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(6, 22)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(124, 13)
		Me.label5.TabIndex = 33
		Me.label5.Text = "Deduplication results file:"
		'
		'tbLogFile
		'
		Me.tbLogFile.Location = New System.Drawing.Point(136, 19)
		Me.tbLogFile.Name = "tbLogFile"
		Me.tbLogFile.Size = New System.Drawing.Size(340, 20)
		Me.tbLogFile.TabIndex = 32
		Me.tbLogFile.Text = "results.csv"
		'
		'rtbStatus
		'
		Me.rtbStatus.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.rtbStatus.Location = New System.Drawing.Point(64, 114)
		Me.rtbStatus.Name = "rtbStatus"
		Me.rtbStatus.ReadOnly = True
		Me.rtbStatus.Size = New System.Drawing.Size(697, 237)
		Me.rtbStatus.TabIndex = 32
		Me.rtbStatus.Text = ""
		'
		'pbStatus
		'
		Me.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.pbStatus.Location = New System.Drawing.Point(6, 114)
		Me.pbStatus.Name = "pbStatus"
		Me.pbStatus.Size = New System.Drawing.Size(52, 50)
		Me.pbStatus.TabIndex = 31
		Me.pbStatus.TabStop = False
		'
		'lblRemaining
		'
		Me.lblRemaining.AutoSize = True
		Me.lblRemaining.Location = New System.Drawing.Point(0, 69)
		Me.lblRemaining.Name = "lblRemaining"
		Me.lblRemaining.Size = New System.Drawing.Size(126, 13)
		Me.lblRemaining.TabIndex = 33
		Me.lblRemaining.Text = "Estimated time remaining:"
		'
		'openFileDialog
		'
		Me.openFileDialog.CheckFileExists = False
		Me.openFileDialog.FileName = "deduplication results.csv"
		'
		'DeduplicationPanel
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.lblRemaining)
		Me.Controls.Add(Me.rtbStatus)
		Me.Controls.Add(Me.pbStatus)
		Me.Controls.Add(Me.gbProperties)
		Me.Controls.Add(Me.lblProgress)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnStart)
		Me.Controls.Add(Me.pbarProgress)
		Me.Name = "DeduplicationPanel"
		Me.Size = New System.Drawing.Size(764, 354)
		Me.gbProperties.ResumeLayout(False)
		Me.gbProperties.PerformLayout()
		CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private pbarProgress As System.Windows.Forms.ProgressBar
	Private lblProgress As System.Windows.Forms.Label
	Private gbProperties As System.Windows.Forms.GroupBox
	Private rtbStatus As System.Windows.Forms.RichTextBox
	Private pbStatus As System.Windows.Forms.PictureBox
	Private lblRemaining As System.Windows.Forms.Label
	Private tbLogFile As System.Windows.Forms.TextBox
	Private WithEvents btnBrowse As System.Windows.Forms.Button
	Private label5 As System.Windows.Forms.Label
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
End Class
