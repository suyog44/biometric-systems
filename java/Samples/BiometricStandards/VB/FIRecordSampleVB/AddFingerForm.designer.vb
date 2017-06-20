Imports Microsoft.VisualBasic
Imports System
Partial Public Class AddFingerForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddFingerForm))
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.label1 = New System.Windows.Forms.Label
		Me.cbFingerPosition = New System.Windows.Forms.ComboBox
		Me.SuspendLayout()
		'
		'btnOk
		'
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(96, 33)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 0
		Me.btnOk.Text = "&Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(177, 33)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 1
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(0, 9)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(78, 13)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Finger position:"
		'
		'cbFingerPosition
		'
		Me.cbFingerPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFingerPosition.Location = New System.Drawing.Point(96, 6)
		Me.cbFingerPosition.Name = "cbFingerPosition"
		Me.cbFingerPosition.Size = New System.Drawing.Size(156, 21)
		Me.cbFingerPosition.TabIndex = 3
		'
		'AddFingerForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(259, 59)
		Me.Controls.Add(Me.cbFingerPosition)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "AddFingerForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Add finger view"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private label1 As System.Windows.Forms.Label
	Protected btnOk As System.Windows.Forms.Button
	Protected btnCancel As System.Windows.Forms.Button
	Private cbFingerPosition As System.Windows.Forms.ComboBox
End Class
