Imports Microsoft.VisualBasic
Imports System
Partial Public Class AddFaceImageForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddFaceImageForm))
		Me.gbMain = New System.Windows.Forms.GroupBox
		Me.label3 = New System.Windows.Forms.Label
		Me.cbImageDataType = New System.Windows.Forms.ComboBox
		Me.cbFaceImageType = New System.Windows.Forms.ComboBox
		Me.label2 = New System.Windows.Forms.Label
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.gbMain.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbMain
		'
		Me.gbMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbMain.Controls.Add(Me.label3)
		Me.gbMain.Controls.Add(Me.cbImageDataType)
		Me.gbMain.Controls.Add(Me.cbFaceImageType)
		Me.gbMain.Controls.Add(Me.label2)
		Me.gbMain.Location = New System.Drawing.Point(12, 4)
		Me.gbMain.Name = "gbMain"
		Me.gbMain.Size = New System.Drawing.Size(263, 101)
		Me.gbMain.TabIndex = 0
		Me.gbMain.TabStop = False
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(6, 52)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(112, 13)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Face image data type:"
		'
		'cbImageDataType
		'
		Me.cbImageDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbImageDataType.FormattingEnabled = True
		Me.cbImageDataType.Location = New System.Drawing.Point(152, 49)
		Me.cbImageDataType.Name = "cbImageDataType"
		Me.cbImageDataType.Size = New System.Drawing.Size(105, 21)
		Me.cbImageDataType.TabIndex = 4
		'
		'cbFaceImageType
		'
		Me.cbFaceImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFaceImageType.FormattingEnabled = True
		Me.cbFaceImageType.Location = New System.Drawing.Point(152, 25)
		Me.cbFaceImageType.Name = "cbFaceImageType"
		Me.cbFaceImageType.Size = New System.Drawing.Size(105, 21)
		Me.cbFaceImageType.TabIndex = 4
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(6, 28)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(88, 13)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Face image type:"
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(119, 114)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 4
		Me.btnOk.Text = "&OK"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(200, 114)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 3
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'AddFaceImageForm
		'
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(287, 149)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.gbMain)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "AddFaceImageForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Add face from image"
		Me.gbMain.ResumeLayout(False)
		Me.gbMain.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Protected btnOk As System.Windows.Forms.Button
	Protected btnCancel As System.Windows.Forms.Button
	Private label3 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Protected gbMain As System.Windows.Forms.GroupBox
	Protected cbFaceImageType As System.Windows.Forms.ComboBox
	Protected cbImageDataType As System.Windows.Forms.ComboBox
End Class
