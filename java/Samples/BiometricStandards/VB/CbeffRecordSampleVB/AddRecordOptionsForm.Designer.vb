Partial Public Class AddRecordOptionsForm
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
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.cbStandard = New System.Windows.Forms.ComboBox
		Me.groupBox1.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtBoxFormat
		'
		Me.txtBoxFormat.Size = New System.Drawing.Size(79, 20)
		'
		'groupBox1
		'
		Me.groupBox1.Location = New System.Drawing.Point(302, 12)
		Me.groupBox1.Size = New System.Drawing.Size(139, 47)
		'
		'gbCommon
		'
		Me.gbCommon.Location = New System.Drawing.Point(83, 12)
		'
		'groupBox2
		'
		Me.groupBox2.Controls.Add(Me.cbStandard)
		Me.groupBox2.Location = New System.Drawing.Point(12, 12)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(65, 47)
		Me.groupBox2.TabIndex = 12
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Standard"
		'
		'cbStandard
		'
		Me.cbStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbStandard.FormattingEnabled = True
		Me.cbStandard.Location = New System.Drawing.Point(7, 19)
		Me.cbStandard.Name = "cbStandard"
		Me.cbStandard.Size = New System.Drawing.Size(52, 21)
		Me.cbStandard.TabIndex = 0
		'
		'AddRecordOptionsForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.ClientSize = New System.Drawing.Size(449, 175)
		Me.Controls.Add(Me.groupBox2)
		Me.Name = "AddRecordOptionsForm"
		Me.Controls.SetChildIndex(Me.gbCommon, 0)
		Me.Controls.SetChildIndex(Me.groupBox1, 0)
		Me.Controls.SetChildIndex(Me.groupBox2, 0)
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.groupBox2.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private groupBox2 As System.Windows.Forms.GroupBox
	Private cbStandard As System.Windows.Forms.ComboBox

End Class