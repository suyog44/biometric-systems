Imports Microsoft.VisualBasic
Imports System
Partial Public Class OpenSubjectDialog
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
		Me.tbFileName = New System.Windows.Forms.TextBox
		Me.btnBrowse = New System.Windows.Forms.Button
		Me.label2 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.cbType = New System.Windows.Forms.ComboBox
		Me.cbOwner = New System.Windows.Forms.ComboBox
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.label1 = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.tableLayoutPanel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'tbFileName
		'
		Me.tbFileName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.tbFileName, 2)
		Me.tbFileName.Location = New System.Drawing.Point(83, 3)
		Me.tbFileName.Name = "tbFileName"
		Me.tbFileName.ReadOnly = True
		Me.tbFileName.Size = New System.Drawing.Size(326, 20)
		Me.tbFileName.TabIndex = 1
		'
		'btnBrowse
		'
		Me.btnBrowse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnBrowse.Location = New System.Drawing.Point(415, 3)
		Me.btnBrowse.Name = "btnBrowse"
		Me.btnBrowse.Size = New System.Drawing.Size(45, 23)
		Me.btnBrowse.TabIndex = 2
		Me.btnBrowse.Text = "..."
		Me.btnBrowse.UseVisualStyleBackColor = True
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(3, 29)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(74, 27)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Format owner:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(3, 56)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(74, 31)
		Me.label3.TabIndex = 4
		Me.label3.Text = "Format type:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(304, 90)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 5
		Me.btnOk.Text = "&OK"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.btnCancel, 2)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(385, 90)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 6
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'cbType
		'
		Me.tableLayoutPanel2.SetColumnSpan(Me.cbType, 3)
		Me.cbType.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbType.FormattingEnabled = True
		Me.cbType.Location = New System.Drawing.Point(83, 59)
		Me.cbType.Name = "cbType"
		Me.cbType.Size = New System.Drawing.Size(377, 21)
		Me.cbType.TabIndex = 7
		'
		'cbOwner
		'
		Me.tableLayoutPanel2.SetColumnSpan(Me.cbOwner, 3)
		Me.cbOwner.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbOwner.FormattingEnabled = True
		Me.cbOwner.Location = New System.Drawing.Point(83, 32)
		Me.cbOwner.Name = "cbOwner"
		Me.cbOwner.Size = New System.Drawing.Size(377, 21)
		Me.cbOwner.TabIndex = 8
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.ColumnCount = 4
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.Controls.Add(Me.btnOk, 1, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.btnCancel, 2, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.cbType, 1, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.tbFileName, 1, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.cbOwner, 1, 1)
		Me.tableLayoutPanel2.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.label3, 0, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.btnBrowse, 3, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.label2, 0, 1)
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 4
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(463, 116)
		Me.tableLayoutPanel2.TabIndex = 1
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(74, 29)
		Me.label1.TabIndex = 1
		Me.label1.Text = "File name:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'openFileDialog
		'
		Me.openFileDialog.Title = "Open subject template"
		'
		'OpenSubjectDialog
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(463, 116)
		Me.Controls.Add(Me.tableLayoutPanel2)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "OpenSubjectDialog"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Open subject"
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tbFileName As System.Windows.Forms.TextBox
	Private WithEvents btnBrowse As System.Windows.Forms.Button
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private btnOk As System.Windows.Forms.Button
	Private cbType As System.Windows.Forms.ComboBox
	Private WithEvents cbOwner As System.Windows.Forms.ComboBox
	Private btnCancel As System.Windows.Forms.Button
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
End Class