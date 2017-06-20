Imports Microsoft.VisualBasic
Imports System
Namespace Forms
	Partial Public Class PictureCaptureForm
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
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(PictureCaptureForm))
			Me.label1 = New System.Windows.Forms.Label()
			Me.cbCameras = New System.Windows.Forms.ComboBox()
			Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
			Me.cbFormats = New System.Windows.Forms.ComboBox()
			Me.panel1 = New System.Windows.Forms.Panel()
			Me.btnOk = New System.Windows.Forms.Button()
			Me.btnCancel = New System.Windows.Forms.Button()
			Me.btnCapture = New System.Windows.Forms.Button()
			Me.backgroundWorker = New System.ComponentModel.BackgroundWorker()
			Me.pictureBox = New System.Windows.Forms.PictureBox()
			Me.tableLayoutPanel1.SuspendLayout()
			Me.panel1.SuspendLayout()
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label1.Location = New System.Drawing.Point(3, 0)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(51, 29)
			Me.label1.TabIndex = 0
			Me.label1.Text = "Cameras:"
			Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			' 
			' cbCameras
			' 
			Me.cbCameras.Dock = System.Windows.Forms.DockStyle.Fill
			Me.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbCameras.FormattingEnabled = True
			Me.cbCameras.Location = New System.Drawing.Point(60, 3)
			Me.cbCameras.Name = "cbCameras"
			Me.cbCameras.Size = New System.Drawing.Size(294, 21)
			Me.cbCameras.TabIndex = 1
'			Me.cbCameras.SelectedIndexChanged += New System.EventHandler(Me.CbCamerasSelectedIndexChanged);
			' 
			' tableLayoutPanel1
			' 
			Me.tableLayoutPanel1.ColumnCount = 4
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F))
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F))
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
			Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.cbCameras, 1, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.cbFormats, 2, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.panel1, 2, 2)
			Me.tableLayoutPanel1.Controls.Add(Me.btnCapture, 3, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.pictureBox, 0, 1)
			Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
			Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
			Me.tableLayoutPanel1.RowCount = 3
			Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
			Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F))
			Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
			Me.tableLayoutPanel1.Size = New System.Drawing.Size(597, 496)
			Me.tableLayoutPanel1.TabIndex = 6
			' 
			' cbFormats
			' 
			Me.cbFormats.Dock = System.Windows.Forms.DockStyle.Fill
			Me.cbFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbFormats.FormattingEnabled = True
			Me.cbFormats.Location = New System.Drawing.Point(360, 3)
			Me.cbFormats.Name = "cbFormats"
			Me.cbFormats.Size = New System.Drawing.Size(144, 21)
			Me.cbFormats.TabIndex = 11
'			Me.cbFormats.SelectedIndexChanged += New System.EventHandler(Me.CbFormatsSelectedIndexChanged);
			' 
			' panel1
			' 
			Me.tableLayoutPanel1.SetColumnSpan(Me.panel1, 2)
			Me.panel1.Controls.Add(Me.btnOk)
			Me.panel1.Controls.Add(Me.btnCancel)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel1.Location = New System.Drawing.Point(360, 465)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(234, 28)
			Me.panel1.TabIndex = 12
			' 
			' btnOk
			' 
			Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnOk.Location = New System.Drawing.Point(58, 3)
			Me.btnOk.Name = "btnOk"
			Me.btnOk.Size = New System.Drawing.Size(84, 23)
			Me.btnOk.TabIndex = 7
			Me.btnOk.Text = "&OK"
			Me.btnOk.UseVisualStyleBackColor = True
'			Me.btnOk.Click += New System.EventHandler(Me.BtnOkClick);
			' 
			' btnCancel
			' 
			Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(148, 3)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(84, 23)
			Me.btnCancel.TabIndex = 8
			Me.btnCancel.Text = "&Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			' 
			' btnCapture
			' 
			Me.btnCapture.Location = New System.Drawing.Point(510, 3)
			Me.btnCapture.Name = "btnCapture"
			Me.btnCapture.Size = New System.Drawing.Size(84, 23)
			Me.btnCapture.TabIndex = 10
			Me.btnCapture.Text = "Cap&ture"
			Me.btnCapture.UseVisualStyleBackColor = True
'			Me.btnCapture.Click += New System.EventHandler(Me.BtnCaptureClick);
			' 
			' backgroundWorker
			' 
			Me.backgroundWorker.WorkerSupportsCancellation = True
'			Me.backgroundWorker.DoWork += New System.ComponentModel.DoWorkEventHandler(Me.BackgroundWorkerDoWork);
			' 
			' pictureBox
			' 
			Me.tableLayoutPanel1.SetColumnSpan(Me.pictureBox, 4)
			Me.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill
			Me.pictureBox.Location = New System.Drawing.Point(3, 32)
			Me.pictureBox.Name = "pictureBox"
			Me.pictureBox.Size = New System.Drawing.Size(591, 427)
			Me.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
			Me.pictureBox.TabIndex = 13
			Me.pictureBox.TabStop = False
			' 
			' PictureCaptureForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(597, 496)
			Me.Controls.Add(Me.tableLayoutPanel1)
			Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
			Me.MinimizeBox = False
			Me.Name = "PictureCaptureForm"
			Me.Text = "Capture Picture"
'			Me.Load += New System.EventHandler(Me.PictureCaptureFormLoad);
'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.PictureCaptureFormFormClosing);
			Me.tableLayoutPanel1.ResumeLayout(False)
			Me.tableLayoutPanel1.PerformLayout()
			Me.panel1.ResumeLayout(False)
			CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private label1 As System.Windows.Forms.Label
		Private WithEvents cbCameras As System.Windows.Forms.ComboBox
		Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
		Private WithEvents btnCapture As System.Windows.Forms.Button
		Private WithEvents btnOk As System.Windows.Forms.Button
		Private btnCancel As System.Windows.Forms.Button
		Private WithEvents backgroundWorker As System.ComponentModel.BackgroundWorker
		Private WithEvents cbFormats As System.Windows.Forms.ComboBox
		Private panel1 As System.Windows.Forms.Panel
		Private pictureBox As System.Windows.Forms.PictureBox
	End Class
End Namespace
