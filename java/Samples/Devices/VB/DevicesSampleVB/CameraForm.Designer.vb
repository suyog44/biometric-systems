Imports Microsoft.VisualBasic
Imports System
Partial Public Class CameraForm
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
		Me.focusButton = New System.Windows.Forms.Button()
		Me.resetFocusButton = New System.Windows.Forms.Button()
		Me.clickToFocusCheckBox = New System.Windows.Forms.CheckBox()
		Me.cameraStatusLabel = New System.Windows.Forms.Label()
		CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' pictureBox
		' 
		'			Me.pictureBox.MouseClick += New System.Windows.Forms.MouseEventHandler(Me.pictureBox_MouseClick);
		'			Me.pictureBox.Paint += New System.Windows.Forms.PaintEventHandler(Me.pictureBox_Paint);
		' 
		' forceButton
		' 
		Me.forceButton.Text = "&Capture"
		'			Me.forceButton.Click += New System.EventHandler(Me.forceButton_Click);
		' 
		' focusButton
		' 
		Me.focusButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.focusButton.Location = New System.Drawing.Point(577, 416)
		Me.focusButton.Name = "focusButton"
		Me.focusButton.Size = New System.Drawing.Size(75, 23)
		Me.focusButton.TabIndex = 12
		Me.focusButton.Text = "&Focus"
		Me.focusButton.UseVisualStyleBackColor = True
		'			Me.focusButton.Click += New System.EventHandler(Me.focusButton_Click);
		' 
		' resetFocusButton
		' 
		Me.resetFocusButton.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.resetFocusButton.Location = New System.Drawing.Point(577, 445)
		Me.resetFocusButton.Name = "resetFocusButton"
		Me.resetFocusButton.Size = New System.Drawing.Size(75, 23)
		Me.resetFocusButton.TabIndex = 13
		Me.resetFocusButton.Text = "&Reset"
		Me.resetFocusButton.UseVisualStyleBackColor = True
		'			Me.resetFocusButton.Click += New System.EventHandler(Me.resetFocusButton_Click);
		' 
		' clickToFocusCheckBox
		' 
		Me.clickToFocusCheckBox.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.clickToFocusCheckBox.AutoSize = True
		Me.clickToFocusCheckBox.Checked = True
		Me.clickToFocusCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
		Me.clickToFocusCheckBox.Location = New System.Drawing.Point(577, 474)
		Me.clickToFocusCheckBox.Name = "clickToFocusCheckBox"
		Me.clickToFocusCheckBox.Size = New System.Drawing.Size(55, 17)
		Me.clickToFocusCheckBox.TabIndex = 14
		Me.clickToFocusCheckBox.Text = "Focus"
		Me.clickToFocusCheckBox.UseVisualStyleBackColor = True
		' 
		' cameraStatusLabel
		' 
		Me.cameraStatusLabel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.cameraStatusLabel.AutoSize = True
		Me.cameraStatusLabel.Location = New System.Drawing.Point(574, 493)
		Me.cameraStatusLabel.Name = "cameraStatusLabel"
		Me.cameraStatusLabel.Size = New System.Drawing.Size(98, 13)
		Me.cameraStatusLabel.TabIndex = 15
		Me.cameraStatusLabel.Text = "cameraStatusLabel"
		' 
		' CameraForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(664, 544)
		Me.Controls.Add(Me.focusButton)
		Me.Controls.Add(Me.clickToFocusCheckBox)
		Me.Controls.Add(Me.cameraStatusLabel)
		Me.Controls.Add(Me.resetFocusButton)
		Me.Name = "CameraForm"
		Me.Text = "CameraForm"
		Me.Controls.SetChildIndex(Me.resetFocusButton, 0)
		Me.Controls.SetChildIndex(Me.cameraStatusLabel, 0)
		Me.Controls.SetChildIndex(Me.clickToFocusCheckBox, 0)
		Me.Controls.SetChildIndex(Me.focusButton, 0)
		Me.Controls.SetChildIndex(Me.pictureBox, 0)
		Me.Controls.SetChildIndex(Me.forceButton, 0)
		CType(Me.pictureBox, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents focusButton As System.Windows.Forms.Button
	Private WithEvents resetFocusButton As System.Windows.Forms.Button
	Private clickToFocusCheckBox As System.Windows.Forms.CheckBox
	Private cameraStatusLabel As System.Windows.Forms.Label
End Class
