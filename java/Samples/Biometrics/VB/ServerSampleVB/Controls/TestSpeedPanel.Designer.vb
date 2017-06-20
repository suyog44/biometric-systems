Imports Microsoft.VisualBasic
Imports System

Partial Public Class TestSpeedPanel
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
		Me.gbProperties = New System.Windows.Forms.GroupBox
		Me.label9 = New System.Windows.Forms.Label
		Me.label8 = New System.Windows.Forms.Label
		Me.label7 = New System.Windows.Forms.Label
		Me.nudMaxCount = New System.Windows.Forms.NumericUpDown
		Me.lblCount = New System.Windows.Forms.Label
		Me.gbResults = New System.Windows.Forms.GroupBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.label1 = New System.Windows.Forms.Label
		Me.tbDBSize = New System.Windows.Forms.TextBox
		Me.rtbStatus = New System.Windows.Forms.RichTextBox
		Me.label5 = New System.Windows.Forms.Label
		Me.lblTemplatesOnAcc = New System.Windows.Forms.Label
		Me.label6 = New System.Windows.Forms.Label
		Me.lblTemplateInfo = New System.Windows.Forms.Label
		Me.pbStatus = New System.Windows.Forms.PictureBox
		Me.tbTime = New System.Windows.Forms.TextBox
		Me.tbSpeed = New System.Windows.Forms.TextBox
		Me.tbTaskCount = New System.Windows.Forms.TextBox
		Me.lblRemaining = New System.Windows.Forms.Label
		Me.gbProperties.SuspendLayout()
		CType(Me.nudMaxCount, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbResults.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'pbarProgress
		'
		Me.pbarProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pbarProgress.Location = New System.Drawing.Point(6, 91)
		Me.pbarProgress.Name = "pbarProgress"
		Me.pbarProgress.Size = New System.Drawing.Size(604, 23)
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
		'gbProperties
		'
		Me.gbProperties.Controls.Add(Me.label9)
		Me.gbProperties.Controls.Add(Me.label8)
		Me.gbProperties.Controls.Add(Me.label7)
		Me.gbProperties.Controls.Add(Me.nudMaxCount)
		Me.gbProperties.Location = New System.Drawing.Point(84, 3)
		Me.gbProperties.Name = "gbProperties"
		Me.gbProperties.Size = New System.Drawing.Size(287, 69)
		Me.gbProperties.TabIndex = 17
		Me.gbProperties.TabStop = False
		Me.gbProperties.Text = "Properites"
		'
		'label9
		'
		Me.label9.AutoSize = True
		Me.label9.Location = New System.Drawing.Point(3, 48)
		Me.label9.Name = "label9"
		Me.label9.Size = New System.Drawing.Size(271, 13)
		Me.label9.TabIndex = 22
		Me.label9.Text = "* - all templates should be able to fit into memory at once"
		'
		'label8
		'
		Me.label8.AutoSize = True
		Me.label8.Location = New System.Drawing.Point(258, 24)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(11, 13)
		Me.label8.TabIndex = 21
		Me.label8.Text = "*"
		'
		'label7
		'
		Me.label7.AutoSize = True
		Me.label7.Location = New System.Drawing.Point(3, 24)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(146, 13)
		Me.label7.TabIndex = 20
		Me.label7.Text = "Maximum templates to match:"
		'
		'nudMaxCount
		'
		Me.nudMaxCount.Location = New System.Drawing.Point(158, 19)
		Me.nudMaxCount.Maximum = New Decimal(New Integer() {1316134911, 2328, 0, 0})
		Me.nudMaxCount.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
		Me.nudMaxCount.Name = "nudMaxCount"
		Me.nudMaxCount.Size = New System.Drawing.Size(94, 20)
		Me.nudMaxCount.TabIndex = 20
		Me.nudMaxCount.Value = New Decimal(New Integer() {1000, 0, 0, 0})
		'
		'lblCount
		'
		Me.lblCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblCount.Location = New System.Drawing.Point(377, 75)
		Me.lblCount.Name = "lblCount"
		Me.lblCount.Size = New System.Drawing.Size(230, 13)
		Me.lblCount.TabIndex = 19
		Me.lblCount.Text = "progress label"
		Me.lblCount.TextAlign = System.Drawing.ContentAlignment.TopRight
		'
		'gbResults
		'
		Me.gbResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbResults.Controls.Add(Me.tableLayoutPanel1)
		Me.gbResults.Location = New System.Drawing.Point(6, 120)
		Me.gbResults.Name = "gbResults"
		Me.gbResults.Size = New System.Drawing.Size(604, 198)
		Me.gbResults.TabIndex = 20
		Me.gbResults.TabStop = False
		Me.gbResults.Text = "Results"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 5
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.tbDBSize, 3, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.rtbStatus, 1, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.label5, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.lblTemplatesOnAcc, 2, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.label6, 2, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.lblTemplateInfo, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.pbStatus, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.tbTime, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.tbSpeed, 3, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.tbTaskCount, 1, 2)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 6
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(598, 179)
		Me.tableLayoutPanel1.TabIndex = 17
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 31)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(103, 21)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Templates matched:"
		'
		'tbDBSize
		'
		Me.tbDBSize.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbDBSize.Location = New System.Drawing.Point(363, 3)
		Me.tbDBSize.Name = "tbDBSize"
		Me.tbDBSize.ReadOnly = True
		Me.tbDBSize.Size = New System.Drawing.Size(261, 20)
		Me.tbDBSize.TabIndex = 3
		Me.tbDBSize.Text = "N/A"
		'
		'rtbStatus
		'
		Me.tableLayoutPanel1.SetColumnSpan(Me.rtbStatus, 3)
		Me.rtbStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rtbStatus.Location = New System.Drawing.Point(112, 68)
		Me.rtbStatus.Name = "rtbStatus"
		Me.rtbStatus.ReadOnly = True
		Me.rtbStatus.Size = New System.Drawing.Size(512, 108)
		Me.rtbStatus.TabIndex = 8
		Me.rtbStatus.Text = ""
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label5.Location = New System.Drawing.Point(3, 5)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(103, 21)
		Me.label5.TabIndex = 4
		Me.label5.Text = "Time elapsed:"
		'
		'lblTemplatesOnAcc
		'
		Me.lblTemplatesOnAcc.AutoSize = True
		Me.lblTemplatesOnAcc.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblTemplatesOnAcc.Location = New System.Drawing.Point(227, 5)
		Me.lblTemplatesOnAcc.Name = "lblTemplatesOnAcc"
		Me.lblTemplatesOnAcc.Size = New System.Drawing.Size(130, 21)
		Me.lblTemplatesOnAcc.TabIndex = 2
		Me.lblTemplatesOnAcc.Text = "Templates on accelerator:"
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label6.Location = New System.Drawing.Point(227, 31)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(130, 21)
		Me.label6.TabIndex = 6
		Me.label6.Text = "Speed:"
		'
		'lblTemplateInfo
		'
		Me.lblTemplateInfo.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.lblTemplateInfo, 4)
		Me.lblTemplateInfo.Location = New System.Drawing.Point(3, 52)
		Me.lblTemplateInfo.Name = "lblTemplateInfo"
		Me.lblTemplateInfo.Size = New System.Drawing.Size(336, 13)
		Me.lblTemplateInfo.TabIndex = 16
		Me.lblTemplateInfo.Text = "* - server template count is assumed to be equal to DB template count"
		'
		'pbStatus
		'
		Me.pbStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.pbStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.pbStatus.Location = New System.Drawing.Point(54, 68)
		Me.pbStatus.Name = "pbStatus"
		Me.pbStatus.Size = New System.Drawing.Size(52, 50)
		Me.pbStatus.TabIndex = 15
		Me.pbStatus.TabStop = False
		'
		'tbTime
		'
		Me.tbTime.Location = New System.Drawing.Point(112, 3)
		Me.tbTime.Name = "tbTime"
		Me.tbTime.ReadOnly = True
		Me.tableLayoutPanel1.SetRowSpan(Me.tbTime, 2)
		Me.tbTime.Size = New System.Drawing.Size(109, 20)
		Me.tbTime.TabIndex = 5
		Me.tbTime.Text = "N/A"
		'
		'tbSpeed
		'
		Me.tbSpeed.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbSpeed.Location = New System.Drawing.Point(363, 29)
		Me.tbSpeed.Name = "tbSpeed"
		Me.tbSpeed.ReadOnly = True
		Me.tbSpeed.Size = New System.Drawing.Size(261, 20)
		Me.tbSpeed.TabIndex = 7
		Me.tbSpeed.Text = "N/A"
		'
		'tbTaskCount
		'
		Me.tbTaskCount.Location = New System.Drawing.Point(112, 29)
		Me.tbTaskCount.Name = "tbTaskCount"
		Me.tbTaskCount.ReadOnly = True
		Me.tableLayoutPanel1.SetRowSpan(Me.tbTaskCount, 2)
		Me.tbTaskCount.Size = New System.Drawing.Size(109, 20)
		Me.tbTaskCount.TabIndex = 1
		Me.tbTaskCount.Text = "N/A"
		'
		'lblRemaining
		'
		Me.lblRemaining.AutoSize = True
		Me.lblRemaining.Location = New System.Drawing.Point(6, 75)
		Me.lblRemaining.Name = "lblRemaining"
		Me.lblRemaining.Size = New System.Drawing.Size(126, 13)
		Me.lblRemaining.TabIndex = 21
		Me.lblRemaining.Text = "Estimated time remaining:"
		'
		'TestSpeedPanel
		'
		Me.AccessibleDescription = ""
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.lblRemaining)
		Me.Controls.Add(Me.gbResults)
		Me.Controls.Add(Me.lblCount)
		Me.Controls.Add(Me.gbProperties)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnStart)
		Me.Controls.Add(Me.pbarProgress)
		Me.Name = "TestSpeedPanel"
		Me.Size = New System.Drawing.Size(610, 325)
		Me.gbProperties.ResumeLayout(False)
		Me.gbProperties.PerformLayout()
		CType(Me.nudMaxCount, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbResults.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.pbStatus, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private pbarProgress As System.Windows.Forms.ProgressBar
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private gbProperties As System.Windows.Forms.GroupBox
	Private lblCount As System.Windows.Forms.Label
	Private label9 As System.Windows.Forms.Label
	Private label8 As System.Windows.Forms.Label
	Private label7 As System.Windows.Forms.Label
	Private nudMaxCount As System.Windows.Forms.NumericUpDown
	Private WithEvents gbResults As System.Windows.Forms.GroupBox
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents label1 As System.Windows.Forms.Label
	Private WithEvents tbDBSize As System.Windows.Forms.TextBox
	Private WithEvents rtbStatus As System.Windows.Forms.RichTextBox
	Private WithEvents label5 As System.Windows.Forms.Label
	Private WithEvents lblTemplatesOnAcc As System.Windows.Forms.Label
	Private WithEvents label6 As System.Windows.Forms.Label
	Private WithEvents lblTemplateInfo As System.Windows.Forms.Label
	Private WithEvents pbStatus As System.Windows.Forms.PictureBox
	Private WithEvents tbTime As System.Windows.Forms.TextBox
	Private WithEvents tbSpeed As System.Windows.Forms.TextBox
	Private WithEvents tbTaskCount As System.Windows.Forms.TextBox
	Private WithEvents lblRemaining As System.Windows.Forms.Label
End Class
