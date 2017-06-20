Imports Microsoft.VisualBasic
Imports System
Partial Public Class VoicesSettingsPage
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VoicesSettingsPage))
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.label4 = New System.Windows.Forms.Label
		Me.cbFormats = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.chbUniquePhrases = New System.Windows.Forms.CheckBox
		Me.chbTextIndependant = New System.Windows.Forms.CheckBox
		Me.chbTextDependent = New System.Windows.Forms.CheckBox
		Me.label2 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.cbMicrophones = New System.Windows.Forms.ComboBox
		Me.nudMaxFileSize = New System.Windows.Forms.NumericUpDown
		Me.tableLayoutPanel1.SuspendLayout()
		CType(Me.nudMaxFileSize, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.AutoScroll = True
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.label4, 0, 6)
		Me.tableLayoutPanel1.Controls.Add(Me.cbFormats, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.chbUniquePhrases, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.chbTextIndependant, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.chbTextDependent, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label3, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.cbMicrophones, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.nudMaxFileSize, 1, 6)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 7
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(485, 202)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label4.Location = New System.Drawing.Point(3, 175)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(80, 27)
		Me.label4.TabIndex = 8
		Me.label4.Text = "Maximal loaded" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "file size (MB):"
		Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbFormats
		'
		Me.cbFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbFormats.FormattingEnabled = True
		Me.cbFormats.Location = New System.Drawing.Point(89, 30)
		Me.cbFormats.Name = "cbFormats"
		Me.cbFormats.Size = New System.Drawing.Size(269, 21)
		Me.cbFormats.TabIndex = 6
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.label1, 2)
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(16, 77)
		Me.label1.Margin = New System.Windows.Forms.Padding(16, 0, 3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(466, 52)
		Me.label1.TabIndex = 2
		Me.label1.Text = resources.GetString("label1.Text")
		'
		'chbUniquePhrases
		'
		Me.chbUniquePhrases.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.chbUniquePhrases, 2)
		Me.chbUniquePhrases.Location = New System.Drawing.Point(3, 57)
		Me.chbUniquePhrases.Name = "chbUniquePhrases"
		Me.chbUniquePhrases.Size = New System.Drawing.Size(122, 17)
		Me.chbUniquePhrases.TabIndex = 0
		Me.chbUniquePhrases.Text = "Unique phrases only"
		Me.chbUniquePhrases.UseVisualStyleBackColor = True
		'
		'chbTextIndependant
		'
		Me.chbTextIndependant.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.chbTextIndependant, 2)
		Me.chbTextIndependant.Location = New System.Drawing.Point(3, 155)
		Me.chbTextIndependant.Name = "chbTextIndependant"
		Me.chbTextIndependant.Size = New System.Drawing.Size(182, 17)
		Me.chbTextIndependant.TabIndex = 2
		Me.chbTextIndependant.Text = "Extract text independent features"
		Me.chbTextIndependant.UseVisualStyleBackColor = True
		'
		'chbTextDependent
		'
		Me.chbTextDependent.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.chbTextDependent, 2)
		Me.chbTextDependent.Location = New System.Drawing.Point(3, 132)
		Me.chbTextDependent.Name = "chbTextDependent"
		Me.chbTextDependent.Size = New System.Drawing.Size(174, 17)
		Me.chbTextDependent.TabIndex = 1
		Me.chbTextDependent.Text = "Extract text dependent features"
		Me.chbTextDependent.UseVisualStyleBackColor = True
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(3, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(80, 27)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Microphone:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(3, 27)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(80, 27)
		Me.label3.TabIndex = 4
		Me.label3.Text = "Format:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbMicrophones
		'
		Me.cbMicrophones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbMicrophones.FormattingEnabled = True
		Me.cbMicrophones.Location = New System.Drawing.Point(89, 3)
		Me.cbMicrophones.Name = "cbMicrophones"
		Me.cbMicrophones.Size = New System.Drawing.Size(269, 21)
		Me.cbMicrophones.TabIndex = 5
		'
		'nudMaxFileSize
		'
		Me.nudMaxFileSize.DecimalPlaces = 3
		Me.nudMaxFileSize.Location = New System.Drawing.Point(89, 178)
		Me.nudMaxFileSize.Maximum = New Decimal(New Integer() {1024, 0, 0, 0})
		Me.nudMaxFileSize.Name = "nudMaxFileSize"
		Me.nudMaxFileSize.Size = New System.Drawing.Size(128, 20)
		Me.nudMaxFileSize.TabIndex = 9
		'
		'VoicesSettingsPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "VoicesSettingsPage"
		Me.Size = New System.Drawing.Size(485, 202)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		CType(Me.nudMaxFileSize, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents chbTextIndependant As System.Windows.Forms.CheckBox
	Private WithEvents chbTextDependent As System.Windows.Forms.CheckBox
	Private WithEvents chbUniquePhrases As System.Windows.Forms.CheckBox
	Private label1 As System.Windows.Forms.Label
	Private WithEvents cbFormats As System.Windows.Forms.ComboBox
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private WithEvents cbMicrophones As System.Windows.Forms.ComboBox
	Private WithEvents label4 As System.Windows.Forms.Label
	Private WithEvents nudMaxFileSize As System.Windows.Forms.NumericUpDown
End Class
