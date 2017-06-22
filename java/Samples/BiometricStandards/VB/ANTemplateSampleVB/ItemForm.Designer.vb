Imports Microsoft.VisualBasic
Imports System
Partial Public Class ItemForm
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
		Me.valueLabel = New System.Windows.Forms.Label()
		Me.tbValue = New System.Windows.Forms.TextBox()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' valueLabel
		' 
		Me.valueLabel.AutoSize = True
		Me.valueLabel.Location = New System.Drawing.Point(12, 9)
		Me.valueLabel.Name = "valueLabel"
		Me.valueLabel.Size = New System.Drawing.Size(37, 13)
		Me.valueLabel.TabIndex = 0
		Me.valueLabel.Text = "Value:"
		' 
		' tbValue
		' 
		Me.tbValue.AcceptsReturn = True
		Me.tbValue.AcceptsTab = True
		Me.tbValue.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.tbValue.Location = New System.Drawing.Point(12, 25)
		Me.tbValue.Multiline = True
		Me.tbValue.Name = "tbValue"
		Me.tbValue.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.tbValue.Size = New System.Drawing.Size(268, 92)
		Me.tbValue.TabIndex = 1
		Me.tbValue.WordWrap = False
		' 
		' btnOk
		' 
		Me.btnOk.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(124, 131)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 2
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.Anchor = (CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(205, 131)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' ItemForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(292, 166)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.tbValue)
		Me.Controls.Add(Me.valueLabel)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(300, 200)
		Me.Name = "ItemForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Edit Item"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private valueLabel As System.Windows.Forms.Label
	Private tbValue As System.Windows.Forms.TextBox
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
End Class