Imports Microsoft.VisualBasic
Imports System
Partial Public Class ValidateForm
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
		Me.progressTitleLabel = New System.Windows.Forms.Label()
		Me.progressLabel = New System.Windows.Forms.Label()
		Me.progressBar = New System.Windows.Forms.ProgressBar()
		Me.errorsLabel = New System.Windows.Forms.Label()
		Me.errorsSplitContainer = New System.Windows.Forms.SplitContainer()
		Me.lbError = New System.Windows.Forms.ListBox()
		Me.tbError = New System.Windows.Forms.TextBox()
		Me.btnStop = New System.Windows.Forms.Button()
		Me.btnClose = New System.Windows.Forms.Button()
		Me.backgroundWorker = New System.ComponentModel.BackgroundWorker()
		Me.errorsSplitContainer.Panel1.SuspendLayout()
		Me.errorsSplitContainer.Panel2.SuspendLayout()
		Me.errorsSplitContainer.SuspendLayout()
		Me.SuspendLayout()
		' 
		' progressTitleLabel
		' 
		Me.progressTitleLabel.AutoSize = True
		Me.progressTitleLabel.Location = New System.Drawing.Point(12, 9)
		Me.progressTitleLabel.Name = "progressTitleLabel"
		Me.progressTitleLabel.Size = New System.Drawing.Size(51, 13)
		Me.progressTitleLabel.TabIndex = 0
		Me.progressTitleLabel.Text = "Progress:"
		' 
		' progressLabel
		' 
		Me.progressLabel.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.progressLabel.AutoEllipsis = True
		Me.progressLabel.Location = New System.Drawing.Point(69, 9)
		Me.progressLabel.Name = "progressLabel"
		Me.progressLabel.Size = New System.Drawing.Size(518, 15)
		Me.progressLabel.TabIndex = 1
		' 
		' progressBar
		' 
		Me.progressBar.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.progressBar.Location = New System.Drawing.Point(12, 27)
		Me.progressBar.Name = "progressBar"
		Me.progressBar.Size = New System.Drawing.Size(575, 19)
		Me.progressBar.TabIndex = 2
		' 
		' errorsLabel
		' 
		Me.errorsLabel.AutoSize = True
		Me.errorsLabel.Location = New System.Drawing.Point(12, 59)
		Me.errorsLabel.Name = "errorsLabel"
		Me.errorsLabel.Size = New System.Drawing.Size(37, 13)
		Me.errorsLabel.TabIndex = 3
		Me.errorsLabel.Text = "Errors:"
		' 
		' errorsSplitContainer
		' 
		Me.errorsSplitContainer.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.errorsSplitContainer.Location = New System.Drawing.Point(15, 75)
		Me.errorsSplitContainer.Name = "errorsSplitContainer"
		' 
		' errorsSplitContainer.Panel1
		' 
		Me.errorsSplitContainer.Panel1.Controls.Add(Me.lbError)
		' 
		' errorsSplitContainer.Panel2
		' 
		Me.errorsSplitContainer.Panel2.Controls.Add(Me.tbError)
		Me.errorsSplitContainer.Size = New System.Drawing.Size(572, 191)
		Me.errorsSplitContainer.SplitterDistance = 285
		Me.errorsSplitContainer.TabIndex = 4
		' 
		' lbError
		' 
		Me.lbError.DisplayMember = "FileName"
		Me.lbError.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lbError.FormattingEnabled = True
		Me.lbError.HorizontalScrollbar = True
		Me.lbError.Location = New System.Drawing.Point(0, 0)
		Me.lbError.Name = "lbError"
		Me.lbError.Size = New System.Drawing.Size(285, 186)
		Me.lbError.TabIndex = 0
'		Me.lbError.SelectedIndexChanged += New System.EventHandler(Me.LbErrorSelectedIndexChanged);
		' 
		' tbError
		' 
		Me.tbError.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbError.Location = New System.Drawing.Point(0, 0)
		Me.tbError.Multiline = True
		Me.tbError.Name = "tbError"
		Me.tbError.ReadOnly = True
		Me.tbError.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.tbError.Size = New System.Drawing.Size(283, 191)
		Me.tbError.TabIndex = 0
		Me.tbError.WordWrap = False
		' 
		' btnStop
		' 
		Me.btnStop.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnStop.Location = New System.Drawing.Point(431, 286)
		Me.btnStop.Name = "btnStop"
		Me.btnStop.Size = New System.Drawing.Size(75, 23)
		Me.btnStop.TabIndex = 5
		Me.btnStop.Text = "Stop"
		Me.btnStop.UseVisualStyleBackColor = True
'		Me.btnStop.Click += New System.EventHandler(Me.BtnStopClick);
		' 
		' btnClose
		' 
		Me.btnClose.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnClose.Enabled = False
		Me.btnClose.Location = New System.Drawing.Point(512, 286)
		Me.btnClose.Name = "btnClose"
		Me.btnClose.Size = New System.Drawing.Size(75, 23)
		Me.btnClose.TabIndex = 6
		Me.btnClose.Text = "Close"
		Me.btnClose.UseVisualStyleBackColor = True
		' 
		' backgroundWorker
		' 
		Me.backgroundWorker.WorkerReportsProgress = True
		Me.backgroundWorker.WorkerSupportsCancellation = True
'		Me.backgroundWorker.DoWork += New System.ComponentModel.DoWorkEventHandler(Me.WorkerDoWork);
'		Me.backgroundWorker.RunWorkerCompleted += New System.ComponentModel.RunWorkerCompletedEventHandler(Me.RunWorkerCompleted);
'		Me.backgroundWorker.ProgressChanged += New System.ComponentModel.ProgressChangedEventHandler(Me.WorkerProgressChanged);
		' 
		' ValidateForm
		' 
		Me.AcceptButton = Me.btnClose
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnClose
		Me.ClientSize = New System.Drawing.Size(599, 321)
		Me.Controls.Add(Me.btnClose)
		Me.Controls.Add(Me.btnStop)
		Me.Controls.Add(Me.errorsSplitContainer)
		Me.Controls.Add(Me.errorsLabel)
		Me.Controls.Add(Me.progressBar)
		Me.Controls.Add(Me.progressLabel)
		Me.Controls.Add(Me.progressTitleLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(300, 300)
		Me.Name = "ValidateForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Validate"
'		Me.Shown += New System.EventHandler(Me.ValidateFormShown);
		Me.errorsSplitContainer.Panel1.ResumeLayout(False)
		Me.errorsSplitContainer.Panel2.ResumeLayout(False)
		Me.errorsSplitContainer.Panel2.PerformLayout()
		Me.errorsSplitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private progressTitleLabel As System.Windows.Forms.Label
	Private progressLabel As System.Windows.Forms.Label
	Private progressBar As System.Windows.Forms.ProgressBar
	Private errorsLabel As System.Windows.Forms.Label
	Private errorsSplitContainer As System.Windows.Forms.SplitContainer
	Private WithEvents lbError As System.Windows.Forms.ListBox
	Private tbError As System.Windows.Forms.TextBox
	Private WithEvents btnStop As System.Windows.Forms.Button
	Private btnClose As System.Windows.Forms.Button
	Private WithEvents backgroundWorker As System.ComponentModel.BackgroundWorker
End Class