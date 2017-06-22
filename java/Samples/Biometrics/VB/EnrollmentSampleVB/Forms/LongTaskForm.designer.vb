Imports Microsoft.VisualBasic
Imports System
Namespace Forms
	Partial Public Class LongTaskForm
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
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(LongTaskForm))
			Me.progressBar1 = New System.Windows.Forms.ProgressBar()
			Me.lblTitle = New System.Windows.Forms.Label()
			Me.bwLongTask = New System.ComponentModel.BackgroundWorker()
			Me.SuspendLayout()
			' 
			' progressBar1
			' 
			Me.progressBar1.Location = New System.Drawing.Point(12, 29)
			Me.progressBar1.Name = "progressBar1"
			Me.progressBar1.Size = New System.Drawing.Size(343, 23)
			Me.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
			Me.progressBar1.TabIndex = 1
			' 
			' lblTitle
			' 
			Me.lblTitle.AutoSize = True
			Me.lblTitle.Location = New System.Drawing.Point(12, 13)
			Me.lblTitle.Name = "lblTitle"
			Me.lblTitle.Size = New System.Drawing.Size(56, 13)
			Me.lblTitle.TabIndex = 0
			Me.lblTitle.Text = "Working..."
			' 
			' bwLongTask
			' 
'			Me.bwLongTask.DoWork += New System.ComponentModel.DoWorkEventHandler(Me.BwLongTaskDoWork);
'			Me.bwLongTask.RunWorkerCompleted += New System.ComponentModel.RunWorkerCompletedEventHandler(Me.BwRefresherRunWorkerCompleted);
			' 
			' LongTaskForm
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(367, 69)
			Me.ControlBox = False
			Me.Controls.Add(Me.lblTitle)
			Me.Controls.Add(Me.progressBar1)
			Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
			Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
			Me.Name = "LongTaskForm"
			Me.ShowInTaskbar = False
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Working"
'			Me.Shown += New System.EventHandler(Me.LongTaskFormShown);
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private progressBar1 As System.Windows.Forms.ProgressBar
		Private lblTitle As System.Windows.Forms.Label
		Private WithEvents bwLongTask As System.ComponentModel.BackgroundWorker
	End Class
End Namespace
