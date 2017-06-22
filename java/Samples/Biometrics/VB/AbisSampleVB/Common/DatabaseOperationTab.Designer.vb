Imports Microsoft.VisualBasic
Imports System
Partial Public Class DababaseOperationTab
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
		Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
		Me.label1 = New System.Windows.Forms.Label()
		Me.progressBar1 = New System.Windows.Forms.ProgressBar()
		Me.lblStatus = New System.Windows.Forms.Label()
		Me.rtbError = New System.Windows.Forms.RichTextBox()
		Me.flowLayoutPanel = New System.Windows.Forms.FlowLayoutPanel()
		Me.tableLayoutPanel.SuspendLayout()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel
		' 
		Me.tableLayoutPanel.AutoScroll = True
		Me.tableLayoutPanel.AutoSize = True
		Me.tableLayoutPanel.ColumnCount = 2
		Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F))
		Me.tableLayoutPanel.Controls.Add(Me.label1, 0, 3)
		Me.tableLayoutPanel.Controls.Add(Me.progressBar1, 1, 3)
		Me.tableLayoutPanel.Controls.Add(Me.lblStatus, 0, 0)
		Me.tableLayoutPanel.Controls.Add(Me.rtbError, 0, 1)
		Me.tableLayoutPanel.Controls.Add(Me.flowLayoutPanel, 0, 2)
		Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel.Name = "tableLayoutPanel"
		Me.tableLayoutPanel.RowCount = 4
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F))
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F))
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tableLayoutPanel.Size = New System.Drawing.Size(455, 312)
		Me.tableLayoutPanel.TabIndex = 1
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(3, 289)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(120, 13)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Operation is in progress:"
		' 
		' progressBar1
		' 
		Me.progressBar1.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.progressBar1.Location = New System.Drawing.Point(129, 292)
		Me.progressBar1.Name = "progressBar1"
		Me.progressBar1.Size = New System.Drawing.Size(323, 17)
		Me.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
		Me.progressBar1.TabIndex = 2
		' 
		' lblStatus
		' 
		Me.lblStatus.AutoSize = True
		Me.lblStatus.BackColor = System.Drawing.Color.Orange
		Me.tableLayoutPanel.SetColumnSpan(Me.lblStatus, 2)
		Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.lblStatus.ForeColor = System.Drawing.Color.White
		Me.lblStatus.Location = New System.Drawing.Point(3, 0)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
		Me.lblStatus.Size = New System.Drawing.Size(449, 22)
		Me.lblStatus.TabIndex = 4
		Me.lblStatus.Text = "Operation in progress. Please wait ..."
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		' 
		' rtbError
		' 
		Me.rtbError.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.tableLayoutPanel.SetColumnSpan(Me.rtbError, 2)
		Me.rtbError.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rtbError.ForeColor = System.Drawing.Color.Red
		Me.rtbError.Location = New System.Drawing.Point(3, 25)
		Me.rtbError.Name = "rtbError"
		Me.rtbError.ReadOnly = True
		Me.rtbError.Size = New System.Drawing.Size(449, 172)
		Me.rtbError.TabIndex = 6
		Me.rtbError.Text = ""
		' 
		' flowLayoutPanel
		' 
		Me.flowLayoutPanel.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.flowLayoutPanel.AutoScroll = True
		Me.tableLayoutPanel.SetColumnSpan(Me.flowLayoutPanel, 2)
		Me.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
		Me.flowLayoutPanel.Location = New System.Drawing.Point(3, 203)
		Me.flowLayoutPanel.Name = "flowLayoutPanel"
		Me.flowLayoutPanel.Size = New System.Drawing.Size(449, 83)
		Me.flowLayoutPanel.TabIndex = 7
		Me.flowLayoutPanel.WrapContents = False
		' 
		' DababaseOperationTab
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.Controls.Add(Me.tableLayoutPanel)
		Me.Name = "DababaseOperationTab"
		Me.Size = New System.Drawing.Size(455, 312)
		Me.tableLayoutPanel.ResumeLayout(False)
		Me.tableLayoutPanel.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private progressBar1 As System.Windows.Forms.ProgressBar
	Private lblStatus As System.Windows.Forms.Label
	Private rtbError As System.Windows.Forms.RichTextBox
	Private flowLayoutPanel As System.Windows.Forms.FlowLayoutPanel
End Class
