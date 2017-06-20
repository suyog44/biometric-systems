Imports Microsoft.VisualBasic
Imports System
Partial Public Class MicrophoneForm
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
		Me.soundLevelProgressBar = New System.Windows.Forms.ProgressBar()
		Me.SuspendLayout()
		' 
		' soundLevelProgressBar
		' 
		Me.soundLevelProgressBar.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.soundLevelProgressBar.Location = New System.Drawing.Point(146, 140)
		Me.soundLevelProgressBar.Name = "soundLevelProgressBar"
		Me.soundLevelProgressBar.Size = New System.Drawing.Size(363, 20)
		Me.soundLevelProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
		Me.soundLevelProgressBar.TabIndex = 11
		' 
		' MicrophoneForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(664, 544)
		Me.Controls.Add(Me.soundLevelProgressBar)
		Me.Name = "MicrophoneForm"
		Me.Text = "MicrophoneForm"
		Me.Controls.SetChildIndex(Me.soundLevelProgressBar, 0)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private soundLevelProgressBar As System.Windows.Forms.ProgressBar
End Class
