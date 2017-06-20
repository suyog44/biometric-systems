Imports Microsoft.VisualBasic
Imports System
Partial Public Class MediaPlayerControl
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing


#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
		Me.btnPlay = New System.Windows.Forms.Button()
		Me.btnStop = New System.Windows.Forms.Button()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' tableLayoutPanel1
		' 
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel1.Controls.Add(Me.btnPlay, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnStop, 1, 0)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 1
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0F))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(157, 52)
		Me.tableLayoutPanel1.TabIndex = 0
		' 
		' btnPlay
		' 
		Me.btnPlay.Enabled = False
		Me.btnPlay.Image = Global.Neurotec.Samples.My.Resources.play
		Me.btnPlay.Location = New System.Drawing.Point(3, 3)
		Me.btnPlay.Name = "btnPlay"
		Me.btnPlay.Size = New System.Drawing.Size(72, 44)
		Me.btnPlay.TabIndex = 0
		Me.btnPlay.Text = "Play"
		Me.btnPlay.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnPlay.UseVisualStyleBackColor = True
		'			Me.btnPlay.Click += New System.EventHandler(Me.BtnPlayClick);
		' 
		' btnStop
		' 
		Me.btnStop.Enabled = False
		Me.btnStop.Image = Global.Neurotec.Samples.My.Resources._stop
		Me.btnStop.Location = New System.Drawing.Point(81, 3)
		Me.btnStop.Name = "btnStop"
		Me.btnStop.Size = New System.Drawing.Size(72, 44)
		Me.btnStop.TabIndex = 1
		Me.btnStop.Text = "Stop"
		Me.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnStop.UseVisualStyleBackColor = True
		'			Me.btnStop.Click += New System.EventHandler(Me.BtnStopClick);
		' 
		' MediaPlayerControl
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "MediaPlayerControl"
		Me.Size = New System.Drawing.Size(157, 52)
		'			Me.VisibleChanged += New System.EventHandler(Me.MediaPlayerControlVisibleChanged);
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnPlay As System.Windows.Forms.Button
	Private WithEvents btnStop As System.Windows.Forms.Button
End Class
