Imports Microsoft.VisualBasic
Imports System

Partial Public Class NewNFRecordForm
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
		Me.nudWidth = New System.Windows.Forms.NumericUpDown()
		Me.label1 = New System.Windows.Forms.Label()
		Me.label2 = New System.Windows.Forms.Label()
		Me.nudHeight = New System.Windows.Forms.NumericUpDown()
		Me.label3 = New System.Windows.Forms.Label()
		Me.nudHorzResolution = New System.Windows.Forms.NumericUpDown()
		Me.nudVertResolution = New System.Windows.Forms.NumericUpDown()
		Me.label4 = New System.Windows.Forms.Label()
		Me.btnOk = New System.Windows.Forms.Button()
		Me.btnCancel = New System.Windows.Forms.Button()
		CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudHeight, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudHorzResolution, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudVertResolution, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		' 
		' nudWidth
		' 
		Me.nudWidth.Location = New System.Drawing.Point(132, 12)
		Me.nudWidth.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
		Me.nudWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.nudWidth.Name = "nudWidth"
		Me.nudWidth.Size = New System.Drawing.Size(120, 20)
		Me.nudWidth.TabIndex = 0
		Me.nudWidth.Value = New Decimal(New Integer() {500, 0, 0, 0})
		' 
		' label1
		' 
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(88, 14)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(38, 13)
		Me.label1.TabIndex = 1
		Me.label1.Text = "Width:"
		' 
		' label2
		' 
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(85, 40)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(41, 13)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Height:"
		' 
		' nudHeight
		' 
		Me.nudHeight.Location = New System.Drawing.Point(132, 38)
		Me.nudHeight.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
		Me.nudHeight.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.nudHeight.Name = "nudHeight"
		Me.nudHeight.Size = New System.Drawing.Size(120, 20)
		Me.nudHeight.TabIndex = 2
		Me.nudHeight.Value = New Decimal(New Integer() {500, 0, 0, 0})
		' 
		' label3
		' 
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(21, 64)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(105, 13)
		Me.label3.TabIndex = 5
		Me.label3.Text = "Horizontal resolution:"
		' 
		' nudHorzResolution
		' 
		Me.nudHorzResolution.Location = New System.Drawing.Point(132, 62)
		Me.nudHorzResolution.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
		Me.nudHorzResolution.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.nudHorzResolution.Name = "nudHorzResolution"
		Me.nudHorzResolution.Size = New System.Drawing.Size(120, 20)
		Me.nudHorzResolution.TabIndex = 4
		Me.nudHorzResolution.Value = New Decimal(New Integer() {500, 0, 0, 0})
		' 
		' nudVertResolution
		' 
		Me.nudVertResolution.Location = New System.Drawing.Point(132, 88)
		Me.nudVertResolution.Maximum = New Decimal(New Integer() {65535, 0, 0, 0})
		Me.nudVertResolution.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
		Me.nudVertResolution.Name = "nudVertResolution"
		Me.nudVertResolution.Size = New System.Drawing.Size(120, 20)
		Me.nudVertResolution.TabIndex = 6
		Me.nudVertResolution.Value = New Decimal(New Integer() {500, 0, 0, 0})
		' 
		' label4
		' 
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(33, 90)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(93, 13)
		Me.label4.TabIndex = 7
		Me.label4.Text = "Vertical resolution:"
		' 
		' btnOk
		' 
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(98, 151)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 8
		Me.btnOk.Text = "OK"
		Me.btnOk.UseVisualStyleBackColor = True
		' 
		' btnCancel
		' 
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(179, 151)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 9
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		' 
		' NewNFRecordForm
		' 
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(266, 186)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.label4)
		Me.Controls.Add(Me.nudVertResolution)
		Me.Controls.Add(Me.label3)
		Me.Controls.Add(Me.nudHorzResolution)
		Me.Controls.Add(Me.label2)
		Me.Controls.Add(Me.nudHeight)
		Me.Controls.Add(Me.label1)
		Me.Controls.Add(Me.nudWidth)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "NewNFRecordForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.Text = "Add NFRecord"
		CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudHeight, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudHorzResolution, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudVertResolution, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private nudWidth As System.Windows.Forms.NumericUpDown
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private nudHeight As System.Windows.Forms.NumericUpDown
	Private label3 As System.Windows.Forms.Label
	Private nudHorzResolution As System.Windows.Forms.NumericUpDown
	Private nudVertResolution As System.Windows.Forms.NumericUpDown
	Private label4 As System.Windows.Forms.Label
	Private btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
End Class
