
Partial Public Class IIRecordOptionsForm
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
		Me.gbIIRecord = New System.Windows.Forms.GroupBox()
		Me.cbProcessFirstIrisImageOnly = New System.Windows.Forms.CheckBox()
		Me.gbIIRecord.SuspendLayout()
		Me.SuspendLayout()
		' 
		' btnCancel
		' 
		Me.btnCancel.Location = New System.Drawing.Point(243, 188)
		' 
		' btnOk
		' 
		Me.btnOk.Location = New System.Drawing.Point(162, 188)
		' 
		' gbIIRecord
		' 
		Me.gbIIRecord.Controls.Add(Me.cbProcessFirstIrisImageOnly)
		Me.gbIIRecord.Location = New System.Drawing.Point(12, 136)
		Me.gbIIRecord.Name = "gbIIRecord"
		Me.gbIIRecord.Size = New System.Drawing.Size(311, 46)
		Me.gbIIRecord.TabIndex = 3
		Me.gbIIRecord.TabStop = False
		Me.gbIIRecord.Text = "IIRecord"
		' 
		' cbProcessFirstIrisImageOnly
		' 
		Me.cbProcessFirstIrisImageOnly.AutoSize = True
		Me.cbProcessFirstIrisImageOnly.Location = New System.Drawing.Point(9, 19)
		Me.cbProcessFirstIrisImageOnly.Name = "cbProcessFirstIrisImageOnly"
		Me.cbProcessFirstIrisImageOnly.Size = New System.Drawing.Size(151, 17)
		Me.cbProcessFirstIrisImageOnly.TabIndex = 0
		Me.cbProcessFirstIrisImageOnly.Text = "Process first iris image only"
		Me.cbProcessFirstIrisImageOnly.UseVisualStyleBackColor = True
		' 
		' IIRecordOptionsForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(330, 221)
		Me.Controls.Add(Me.gbIIRecord)
		Me.Name = "IIRecordOptionsForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "IIRecordOptionsForm"
		Me.Controls.SetChildIndex(Me.gbIIRecord, 0)
		Me.Controls.SetChildIndex(Me.btnCancel, 0)
		Me.Controls.SetChildIndex(Me.btnOk, 0)
		Me.gbIIRecord.ResumeLayout(False)
		Me.gbIIRecord.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private gbIIRecord As System.Windows.Forms.GroupBox
	Private cbProcessFirstIrisImageOnly As System.Windows.Forms.CheckBox
End Class