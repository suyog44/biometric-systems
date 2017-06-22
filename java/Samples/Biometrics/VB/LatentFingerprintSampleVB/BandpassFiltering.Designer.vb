Imports Microsoft.VisualBasic
Imports System
Partial Public Class BandpassFilteringForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(BandpassFilteringForm))
		Me.panel1 = New System.Windows.Forms.Panel()
		Me.panel5 = New System.Windows.Forms.Panel()
		Me.button1 = New System.Windows.Forms.Button()
		Me.button2 = New System.Windows.Forms.Button()
		Me.panel2 = New System.Windows.Forms.Panel()
		Me.penSize = New System.Windows.Forms.TrackBar()
		Me.label5 = New System.Windows.Forms.Label()
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.radioRect = New System.Windows.Forms.RadioButton()
		Me.radioCircle = New System.Windows.Forms.RadioButton()
		Me.label4 = New System.Windows.Forms.Label()
		Me.panel6 = New System.Windows.Forms.Panel()
		Me.button6 = New System.Windows.Forms.Button()
		Me.button4 = New System.Windows.Forms.Button()
		Me.bRealtime = New System.Windows.Forms.CheckBox()
		Me.button3 = New System.Windows.Forms.Button()
		Me.button5 = New System.Windows.Forms.Button()
		Me.label3 = New System.Windows.Forms.Label()
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
		Me.panel3 = New System.Windows.Forms.Panel()
		Me.viewFourierMask = New System.Windows.Forms.PictureBox()
		Me.label1 = New System.Windows.Forms.Label()
		Me.panel4 = New System.Windows.Forms.Panel()
		Me.viewResult = New System.Windows.Forms.PictureBox()
		Me.label2 = New System.Windows.Forms.Label()
		Me.panel1.SuspendLayout()
		Me.panel5.SuspendLayout()
		Me.panel2.SuspendLayout()
		CType(Me.penSize, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.panel6.SuspendLayout()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.panel3.SuspendLayout()
		CType(Me.viewFourierMask, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.panel4.SuspendLayout()
		CType(Me.viewResult, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' panel1
		' 
		Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel1.Controls.Add(Me.panel5)
		Me.panel1.Controls.Add(Me.panel2)
		Me.panel1.Controls.Add(Me.label3)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Left
		Me.panel1.Location = New System.Drawing.Point(0, 0)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(86, 419)
		Me.panel1.TabIndex = 0
		' 
		' panel5
		' 
		Me.panel5.Controls.Add(Me.button1)
		Me.panel5.Controls.Add(Me.button2)
		Me.panel5.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel5.Location = New System.Drawing.Point(0, 347)
		Me.panel5.Name = "panel5"
		Me.panel5.Size = New System.Drawing.Size(82, 68)
		Me.panel5.TabIndex = 7
		' 
		' button1
		' 
		Me.button1.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.button1.Location = New System.Drawing.Point(3, 6)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(76, 26)
		Me.button1.TabIndex = 5
		Me.button1.Text = "Accept"
		Me.button1.UseVisualStyleBackColor = True
'		Me.button1.Click += New System.EventHandler(Me.button1_Click);
		' 
		' button2
		' 
		Me.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.button2.Location = New System.Drawing.Point(3, 38)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(76, 26)
		Me.button2.TabIndex = 6
		Me.button2.Text = "Dismiss"
		Me.button2.UseVisualStyleBackColor = True
'		Me.button2.Click += New System.EventHandler(Me.button2_Click);
		' 
		' panel2
		' 
		Me.panel2.Controls.Add(Me.penSize)
		Me.panel2.Controls.Add(Me.label5)
		Me.panel2.Controls.Add(Me.tableLayoutPanel1)
		Me.panel2.Controls.Add(Me.label4)
		Me.panel2.Controls.Add(Me.panel6)
		Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel2.Location = New System.Drawing.Point(0, 13)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(82, 334)
		Me.panel2.TabIndex = 8
		' 
		' penSize
		' 
		Me.penSize.Location = New System.Drawing.Point(3, 236)
		Me.penSize.Maximum = 150
		Me.penSize.Minimum = 1
		Me.penSize.Name = "penSize"
		Me.penSize.Size = New System.Drawing.Size(76, 45)
		Me.penSize.TabIndex = 14
		Me.penSize.TickFrequency = 10
		Me.penSize.Value = 20
		' 
		' label5
		' 
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(3, 220)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(30, 13)
		Me.label5.TabIndex = 12
		Me.label5.Text = "Size:"
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F))
		Me.tableLayoutPanel1.Controls.Add(Me.radioRect, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.radioCircle, 0, 0)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 173)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 1
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(82, 44)
		Me.tableLayoutPanel1.TabIndex = 11
		' 
		' radioRect
		' 
		Me.radioRect.Appearance = System.Windows.Forms.Appearance.Button
		Me.radioRect.AutoSize = True
		Me.radioRect.Image = My.Resources.ToolRect
		Me.radioRect.Location = New System.Drawing.Point(44, 3)
		Me.radioRect.Name = "radioRect"
		Me.radioRect.Size = New System.Drawing.Size(35, 38)
		Me.radioRect.TabIndex = 1
		Me.radioRect.TabStop = True
		Me.radioRect.UseVisualStyleBackColor = True
'		Me.radioRect.CheckedChanged += New System.EventHandler(Me.radioRect_CheckedChanged);
		' 
		' radioCircle
		' 
		Me.radioCircle.Appearance = System.Windows.Forms.Appearance.Button
		Me.radioCircle.AutoSize = True
		Me.radioCircle.Image = My.Resources.ToolCircle
		Me.radioCircle.Location = New System.Drawing.Point(3, 3)
		Me.radioCircle.Name = "radioCircle"
		Me.radioCircle.Size = New System.Drawing.Size(35, 38)
		Me.radioCircle.TabIndex = 0
		Me.radioCircle.TabStop = True
		Me.radioCircle.UseVisualStyleBackColor = True
'		Me.radioCircle.CheckedChanged += New System.EventHandler(Me.radioCircle_CheckedChanged);
		' 
		' label4
		' 
		Me.label4.BackColor = System.Drawing.SystemColors.ControlDark
		Me.label4.Dock = System.Windows.Forms.DockStyle.Top
		Me.label4.ForeColor = System.Drawing.SystemColors.ControlLight
		Me.label4.Location = New System.Drawing.Point(0, 160)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(82, 13)
		Me.label4.TabIndex = 9
		Me.label4.Text = "Tools"
		' 
		' panel6
		' 
		Me.panel6.Controls.Add(Me.button6)
		Me.panel6.Controls.Add(Me.button4)
		Me.panel6.Controls.Add(Me.bRealtime)
		Me.panel6.Controls.Add(Me.button3)
		Me.panel6.Controls.Add(Me.button5)
		Me.panel6.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel6.Location = New System.Drawing.Point(0, 0)
		Me.panel6.Name = "panel6"
		Me.panel6.Size = New System.Drawing.Size(82, 160)
		Me.panel6.TabIndex = 10
		' 
		' button6
		' 
		Me.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.button6.Location = New System.Drawing.Point(3, 98)
		Me.button6.Name = "button6"
		Me.button6.Size = New System.Drawing.Size(76, 26)
		Me.button6.TabIndex = 9
		Me.button6.Text = "Refresh"
		Me.button6.UseVisualStyleBackColor = True
'		Me.button6.Click += New System.EventHandler(Me.button6_Click);
		' 
		' button4
		' 
		Me.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.button4.Location = New System.Drawing.Point(3, 2)
		Me.button4.Name = "button4"
		Me.button4.Size = New System.Drawing.Size(76, 26)
		Me.button4.TabIndex = 5
		Me.button4.Text = "Set all"
		Me.button4.UseVisualStyleBackColor = True
'		Me.button4.Click += New System.EventHandler(Me.button4_Click);
		' 
		' bRealtime
		' 
		Me.bRealtime.Checked = True
		Me.bRealtime.CheckState = System.Windows.Forms.CheckState.Checked
		Me.bRealtime.Location = New System.Drawing.Point(3, 121)
		Me.bRealtime.Name = "bRealtime"
		Me.bRealtime.Size = New System.Drawing.Size(75, 36)
		Me.bRealtime.TabIndex = 8
		Me.bRealtime.Text = "Auto refresh"
		Me.bRealtime.UseVisualStyleBackColor = True
		' 
		' button3
		' 
		Me.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.button3.Location = New System.Drawing.Point(3, 34)
		Me.button3.Name = "button3"
		Me.button3.Size = New System.Drawing.Size(76, 26)
		Me.button3.TabIndex = 6
		Me.button3.Text = "Reset all"
		Me.button3.UseVisualStyleBackColor = True
'		Me.button3.Click += New System.EventHandler(Me.button3_Click);
		' 
		' button5
		' 
		Me.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
		Me.button5.Location = New System.Drawing.Point(3, 66)
		Me.button5.Name = "button5"
		Me.button5.Size = New System.Drawing.Size(76, 26)
		Me.button5.TabIndex = 7
		Me.button5.Text = "Invert"
		Me.button5.UseVisualStyleBackColor = True
'		Me.button5.Click += New System.EventHandler(Me.button5_Click);
		' 
		' label3
		' 
		Me.label3.BackColor = System.Drawing.SystemColors.ControlDark
		Me.label3.Dock = System.Windows.Forms.DockStyle.Top
		Me.label3.ForeColor = System.Drawing.SystemColors.ControlLight
		Me.label3.Location = New System.Drawing.Point(0, 0)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(82, 13)
		Me.label3.TabIndex = 1
		Me.label3.Text = "Operations"
		' 
		' splitContainer1
		' 
		Me.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(86, 0)
		Me.splitContainer1.Name = "splitContainer1"
		' 
		' splitContainer1.Panel1
		' 
		Me.splitContainer1.Panel1.Controls.Add(Me.panel3)
		Me.splitContainer1.Panel1.Controls.Add(Me.label1)
		' 
		' splitContainer1.Panel2
		' 
		Me.splitContainer1.Panel2.Controls.Add(Me.panel4)
		Me.splitContainer1.Panel2.Controls.Add(Me.label2)
		Me.splitContainer1.Size = New System.Drawing.Size(740, 419)
		Me.splitContainer1.SplitterDistance = 365
		Me.splitContainer1.TabIndex = 1
		' 
		' panel3
		' 
		Me.panel3.AutoScroll = True
		Me.panel3.Controls.Add(Me.viewFourierMask)
		Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel3.Location = New System.Drawing.Point(0, 13)
		Me.panel3.Name = "panel3"
		Me.panel3.Size = New System.Drawing.Size(361, 402)
		Me.panel3.TabIndex = 1
		' 
		' viewFourierMask
		' 
		Me.viewFourierMask.Location = New System.Drawing.Point(3, 3)
		Me.viewFourierMask.Name = "viewFourierMask"
		Me.viewFourierMask.Size = New System.Drawing.Size(54, 49)
		Me.viewFourierMask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.viewFourierMask.TabIndex = 0
		Me.viewFourierMask.TabStop = False
'		Me.viewFourierMask.MouseMove += New System.Windows.Forms.MouseEventHandler(Me.viewFourier_MouseMove);
'		Me.viewFourierMask.MouseDown += New System.Windows.Forms.MouseEventHandler(Me.viewFourier_MouseDown);
'		Me.viewFourierMask.MouseUp += New System.Windows.Forms.MouseEventHandler(Me.viewFourier_MouseUp);
		' 
		' label1
		' 
		Me.label1.BackColor = System.Drawing.SystemColors.ControlDark
		Me.label1.Dock = System.Windows.Forms.DockStyle.Top
		Me.label1.ForeColor = System.Drawing.SystemColors.ControlLight
		Me.label1.Location = New System.Drawing.Point(0, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(361, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Fourier image and mask"
		' 
		' panel4
		' 
		Me.panel4.AutoScroll = True
		Me.panel4.Controls.Add(Me.viewResult)
		Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel4.Location = New System.Drawing.Point(0, 13)
		Me.panel4.Name = "panel4"
		Me.panel4.Size = New System.Drawing.Size(367, 402)
		Me.panel4.TabIndex = 2
		' 
		' viewResult
		' 
		Me.viewResult.Location = New System.Drawing.Point(3, 3)
		Me.viewResult.Name = "viewResult"
		Me.viewResult.Size = New System.Drawing.Size(54, 49)
		Me.viewResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
		Me.viewResult.TabIndex = 1
		Me.viewResult.TabStop = False
		' 
		' label2
		' 
		Me.label2.BackColor = System.Drawing.SystemColors.ControlDark
		Me.label2.Dock = System.Windows.Forms.DockStyle.Top
		Me.label2.ForeColor = System.Drawing.SystemColors.ControlLight
		Me.label2.Location = New System.Drawing.Point(0, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(367, 13)
		Me.label2.TabIndex = 1
		Me.label2.Text = "Filtered image"
		' 
		' BandpassFiltering
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(826, 419)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.panel1)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.MinimumSize = New System.Drawing.Size(8, 286)
		Me.Name = "BandpassFiltering"
		Me.ShowInTaskbar = False
		Me.Text = "Bandpass Filtering"
		Me.panel1.ResumeLayout(False)
		Me.panel5.ResumeLayout(False)
		Me.panel2.ResumeLayout(False)
		Me.panel2.PerformLayout()
		CType(Me.penSize, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.panel6.ResumeLayout(False)
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.panel3.ResumeLayout(False)
		Me.panel3.PerformLayout()
		CType(Me.viewFourierMask, System.ComponentModel.ISupportInitialize).EndInit()
		Me.panel4.ResumeLayout(False)
		Me.panel4.PerformLayout()
		CType(Me.viewResult, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private panel1 As System.Windows.Forms.Panel
	Private label3 As System.Windows.Forms.Label
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private panel3 As System.Windows.Forms.Panel
	Private panel4 As System.Windows.Forms.Panel
	Private WithEvents button2 As System.Windows.Forms.Button
	Private WithEvents button1 As System.Windows.Forms.Button
	Private panel5 As System.Windows.Forms.Panel
	Private panel2 As System.Windows.Forms.Panel
	Private WithEvents button5 As System.Windows.Forms.Button
	Private WithEvents button3 As System.Windows.Forms.Button
	Private WithEvents button4 As System.Windows.Forms.Button
	Private WithEvents button6 As System.Windows.Forms.Button
	Private bRealtime As System.Windows.Forms.CheckBox
	Private WithEvents viewFourierMask As System.Windows.Forms.PictureBox
	Private viewResult As System.Windows.Forms.PictureBox
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents radioRect As System.Windows.Forms.RadioButton
	Private WithEvents radioCircle As System.Windows.Forms.RadioButton
	Private label4 As System.Windows.Forms.Label
	Private panel6 As System.Windows.Forms.Panel
	Private label5 As System.Windows.Forms.Label
	Private penSize As System.Windows.Forms.TrackBar

End Class