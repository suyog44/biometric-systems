Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(CaptureForm))
		Me.pictureBox = New System.Windows.Forms.PictureBox()
		Me.statusTextBox = New System.Windows.Forms.TextBox()
		Me.forceButton = New System.Windows.Forms.Button()
		Me.closeButton = New System.Windows.Forms.Button()
		Me.backgroundWorker = New System.ComponentModel.BackgroundWorker()
		Me.formatsComboBox = New System.Windows.Forms.ComboBox()
		Me.customizeFormatButton = New System.Windows.Forms.Button()
		CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' pictureBox
		' 
		Me.pictureBox.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.pictureBox.Location = New System.Drawing.Point(12, 12)
		Me.pictureBox.Name = "pictureBox"
		Me.pictureBox.Size = New System.Drawing.Size(640, 371)
		Me.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pictureBox.TabIndex = 1
		Me.pictureBox.TabStop = False
		' 
		' statusTextBox
		' 
		Me.statusTextBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.statusTextBox.Location = New System.Drawing.Point(12, 416)
		Me.statusTextBox.Multiline = True
		Me.statusTextBox.Name = "statusTextBox"
		Me.statusTextBox.ReadOnly = True
		Me.statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.statusTextBox.Size = New System.Drawing.Size(556, 116)
		Me.statusTextBox.TabIndex = 3
		' 
		' forceButton
		' 
		Me.forceButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.forceButton.Location = New System.Drawing.Point(577, 387)
		Me.forceButton.Name = "forceButton"
		Me.forceButton.Size = New System.Drawing.Size(75, 23)
		Me.forceButton.TabIndex = 9
		Me.forceButton.Text = "&Force"
		Me.forceButton.UseVisualStyleBackColor = True
		'			Me.forceButton.Click += New System.EventHandler(Me.forceButton_Click);
		' 
		' closeButton
		' 
		Me.closeButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.closeButton.Location = New System.Drawing.Point(577, 509)
		Me.closeButton.Name = "closeButton"
		Me.closeButton.Size = New System.Drawing.Size(75, 23)
		Me.closeButton.TabIndex = 8
		Me.closeButton.Text = "Close"
		Me.closeButton.UseVisualStyleBackColor = True
		'			Me.closeButton.Click += New System.EventHandler(Me.closeButton_Click);
		' 
		' backgroundWorker
		' 
		Me.backgroundWorker.WorkerReportsProgress = True
		Me.backgroundWorker.WorkerSupportsCancellation = True
		'			Me.backgroundWorker.DoWork += New System.ComponentModel.DoWorkEventHandler(Me.backgroundWorker_DoWork);
		'			Me.backgroundWorker.RunWorkerCompleted += New System.ComponentModel.RunWorkerCompletedEventHandler(Me.backgroundWorker_RunWorkerCompleted);
		' 
		' formatsComboBox
		' 
		Me.formatsComboBox.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.formatsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.formatsComboBox.FormattingEnabled = True
		Me.formatsComboBox.Location = New System.Drawing.Point(12, 389)
		Me.formatsComboBox.Name = "formatsComboBox"
		Me.formatsComboBox.Size = New System.Drawing.Size(478, 21)
		Me.formatsComboBox.TabIndex = 10
		'			Me.formatsComboBox.SelectedIndexChanged += New System.EventHandler(Me.formatsComboBox_SelectedIndexChanged);
		' 
		' customizeFormatButton
		' 
		Me.customizeFormatButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.customizeFormatButton.Enabled = False
		Me.customizeFormatButton.Location = New System.Drawing.Point(496, 387)
		Me.customizeFormatButton.Name = "customizeFormatButton"
		Me.customizeFormatButton.Size = New System.Drawing.Size(75, 23)
		Me.customizeFormatButton.TabIndex = 11
		Me.customizeFormatButton.Text = "Customize..."
		Me.customizeFormatButton.UseVisualStyleBackColor = True
		'			Me.customizeFormatButton.Click += New System.EventHandler(Me.customizeFormatButton_Click);
		' 
		' CaptureForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.closeButton
		Me.ClientSize = New System.Drawing.Size(664, 544)
		Me.Controls.Add(Me.customizeFormatButton)
		Me.Controls.Add(Me.formatsComboBox)
		Me.Controls.Add(Me.forceButton)
		Me.Controls.Add(Me.closeButton)
		Me.Controls.Add(Me.statusTextBox)
		Me.Controls.Add(Me.pictureBox)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "CaptureForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "CaptureForm"
		'			Me.Shown += New System.EventHandler(Me.CaptureForm_Shown);
		'			Me.FormClosing += New System.Windows.Forms.FormClosingEventHandler(Me.CaptureForm_FormClosing);
		CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private statusTextBox As System.Windows.Forms.TextBox
	Private WithEvents closeButton As System.Windows.Forms.Button
	Private WithEvents backgroundWorker As System.ComponentModel.BackgroundWorker
	Private WithEvents formatsComboBox As System.Windows.Forms.ComboBox
	Protected WithEvents pictureBox As System.Windows.Forms.PictureBox
	Protected WithEvents forceButton As System.Windows.Forms.Button
	Protected WithEvents customizeFormatButton As System.Windows.Forms.Button
End Class