Imports Microsoft.VisualBasic
Imports System
Partial Public Class MatchingResultView
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
		Me.linkLabel = New System.Windows.Forms.LinkLabel()
		Me.lblDetails = New System.Windows.Forms.Label()
		Me.SuspendLayout()
		' 
		' linkLabel
		' 
		Me.linkLabel.AutoSize = True
		Me.linkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
		Me.linkLabel.LinkColor = System.Drawing.Color.Black
		Me.linkLabel.Location = New System.Drawing.Point(3, 0)
		Me.linkLabel.Name = "linkLabel"
		Me.linkLabel.Size = New System.Drawing.Size(105, 16)
		Me.linkLabel.TabIndex = 0
		Me.linkLabel.TabStop = True
		Me.linkLabel.Text = "Matched with {0}"
'		Me.linkLabel.LinkClicked += New System.Windows.Forms.LinkLabelLinkClickedEventHandler(Me.LinkLabelLinkClicked);
		' 
		' lblDetails
		' 
		Me.lblDetails.AutoSize = True
		Me.lblDetails.Location = New System.Drawing.Point(10, 25)
		Me.lblDetails.Name = "lblDetails"
		Me.lblDetails.Size = New System.Drawing.Size(84, 13)
		Me.lblDetails.TabIndex = 1
		Me.lblDetails.Text = "Matching details"
		' 
		' MatchingResultView
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.Controls.Add(Me.lblDetails)
		Me.Controls.Add(Me.linkLabel)
		Me.Name = "MatchingResultView"
		Me.Size = New System.Drawing.Size(188, 45)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private WithEvents linkLabel As System.Windows.Forms.LinkLabel
	Private lblDetails As System.Windows.Forms.Label

End Class
