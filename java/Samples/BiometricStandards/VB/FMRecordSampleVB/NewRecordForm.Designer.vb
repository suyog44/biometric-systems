Partial Public Class NewFingerViewForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(NewFingerViewForm))
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.label3 = New System.Windows.Forms.Label()
		Me.label4 = New System.Windows.Forms.Label()
		Me.tbSizeY = New System.Windows.Forms.TextBox()
		Me.tbSizeX = New System.Windows.Forms.TextBox()
		Me.tbVertRes = New System.Windows.Forms.TextBox()
		Me.tbHorRes = New System.Windows.Forms.TextBox()
		Me.groupBox1 = New System.Windows.Forms.GroupBox()
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' btnOk
		' 
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(198, 95)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(59, 23)
		Me.btnOk.TabIndex = 0
		Me.btnOk.Text = "Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		AddHandler btnOk.Click, AddressOf btnOk_Click
		' 
		' btnCancel
		' 
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(263, 94)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(60, 24)
		Me.btnCancel.TabIndex = 1
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(10, 27)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(63, 13)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Vertical size"
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(160, 24)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(75, 13)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Horizontal size"
		' 
		' label3
		' 
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(10, 57)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(90, 13)
		Me.label3.TabIndex = 4
		Me.label3.Text = "Vertical resolution"
		' 
		' label4
		' 
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(160, 60)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(102, 13)
		Me.label4.TabIndex = 5
		Me.label4.Text = "Horizontal resolution"
		' 
		' tbSizeY
		' 
		Me.tbSizeY.Location = New System.Drawing.Point(113, 21)
		Me.tbSizeY.Name = "tbSizeY"
		Me.tbSizeY.Size = New System.Drawing.Size(41, 20)
		Me.tbSizeY.TabIndex = 6
		Me.tbSizeY.Text = "400"
		AddHandler tbSizeY.KeyPress, AddressOf numberBox_KeyPress
		' 
		' tbSizeX
		' 
		Me.tbSizeX.Location = New System.Drawing.Point(266, 20)
		Me.tbSizeX.Name = "tbSizeX"
		Me.tbSizeX.Size = New System.Drawing.Size(41, 20)
		Me.tbSizeX.TabIndex = 7
		Me.tbSizeX.Text = "400"
		AddHandler tbSizeX.KeyPress, AddressOf numberBox_KeyPress
		' 
		' tbVertRes
		' 
		Me.tbVertRes.Location = New System.Drawing.Point(115, 57)
		Me.tbVertRes.Name = "tbVertRes"
		Me.tbVertRes.Size = New System.Drawing.Size(39, 20)
		Me.tbVertRes.TabIndex = 8
		Me.tbVertRes.Text = "500"
		AddHandler tbVertRes.KeyPress, AddressOf numberBox_KeyPress
		' 
		' tbHorRes
		' 
		Me.tbHorRes.Location = New System.Drawing.Point(266, 54)
		Me.tbHorRes.Name = "tbHorRes"
		Me.tbHorRes.Size = New System.Drawing.Size(41, 20)
		Me.tbHorRes.TabIndex = 9
		Me.tbHorRes.Text = "500"
		AddHandler tbHorRes.KeyPress, AddressOf numberBox_KeyPress
		' 
		' groupBox1
		' 
		Me.groupBox1.Controls.Add(Me.label4)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.label2)
		Me.groupBox1.Controls.Add(Me.tbHorRes)
		Me.groupBox1.Controls.Add(Me.label3)
		Me.groupBox1.Controls.Add(Me.tbVertRes)
		Me.groupBox1.Controls.Add(Me.tbSizeY)
		Me.groupBox1.Controls.Add(Me.tbSizeX)
		Me.groupBox1.Location = New System.Drawing.Point(6, 0)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(316, 88)
		Me.groupBox1.TabIndex = 12
		Me.groupBox1.TabStop = False
		' 
		' NewFingerViewForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(326, 120)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.Name = "NewFingerViewForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "New finger view"
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private label4 As System.Windows.Forms.Label
	Friend WithEvents tbSizeY As System.Windows.Forms.TextBox
	Friend WithEvents tbSizeX As System.Windows.Forms.TextBox
	Friend WithEvents tbVertRes As System.Windows.Forms.TextBox
	Friend WithEvents tbHorRes As System.Windows.Forms.TextBox
	Private groupBox1 As System.Windows.Forms.GroupBox
End Class