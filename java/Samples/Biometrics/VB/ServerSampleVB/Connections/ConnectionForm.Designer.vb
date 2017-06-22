Imports Microsoft.VisualBasic
Imports System

Partial Public Class ConnectionForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConnectionForm))
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.rbDirectory = New System.Windows.Forms.RadioButton
		Me.rbDatabase = New System.Windows.Forms.RadioButton
		Me.tbPath = New System.Windows.Forms.TextBox
		Me.btnBrowse = New System.Windows.Forms.Button
		Me.gbDatabase = New System.Windows.Forms.GroupBox
		Me.btnReset = New System.Windows.Forms.Button
		Me.btnConnect = New System.Windows.Forms.Button
		Me.cbId = New System.Windows.Forms.ComboBox
		Me.cbTemplate = New System.Windows.Forms.ComboBox
		Me.label7 = New System.Windows.Forms.Label
		Me.label6 = New System.Windows.Forms.Label
		Me.cbTable = New System.Windows.Forms.ComboBox
		Me.tbDBPassword = New System.Windows.Forms.MaskedTextBox
		Me.tbDBUser = New System.Windows.Forms.TextBox
		Me.tbDBServer = New System.Windows.Forms.TextBox
		Me.label5 = New System.Windows.Forms.Label
		Me.label4 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
		Me.gbAccelerator = New System.Windows.Forms.GroupBox
		Me.btnCheckConnection = New System.Windows.Forms.Button
		Me.chbIsAccelerator = New System.Windows.Forms.CheckBox
		Me.nudAdminPort = New System.Windows.Forms.NumericUpDown
		Me.label12 = New System.Windows.Forms.Label
		Me.nudPort = New System.Windows.Forms.NumericUpDown
		Me.tbUsername = New System.Windows.Forms.TextBox
		Me.tbMMAServer = New System.Windows.Forms.TextBox
		Me.mtbPasword = New System.Windows.Forms.MaskedTextBox
		Me.label11 = New System.Windows.Forms.Label
		Me.label10 = New System.Windows.Forms.Label
		Me.label9 = New System.Windows.Forms.Label
		Me.label8 = New System.Windows.Forms.Label
		Me.gbTemplates = New System.Windows.Forms.GroupBox
		Me.gbDatabase.SuspendLayout()
		Me.gbAccelerator.SuspendLayout()
		CType(Me.nudAdminPort, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudPort, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.gbTemplates.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.Location = New System.Drawing.Point(147, 541)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 18
		Me.btnOk.Text = "Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(228, 541)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 19
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'rbDirectory
		'
		Me.rbDirectory.AutoSize = True
		Me.rbDirectory.Location = New System.Drawing.Point(6, 19)
		Me.rbDirectory.Name = "rbDirectory"
		Me.rbDirectory.Size = New System.Drawing.Size(163, 17)
		Me.rbDirectory.TabIndex = 4
		Me.rbDirectory.TabStop = True
		Me.rbDirectory.Text = "Load templates from directory"
		Me.rbDirectory.UseVisualStyleBackColor = True
		'
		'rbDatabase
		'
		Me.rbDatabase.AutoSize = True
		Me.rbDatabase.Location = New System.Drawing.Point(6, 75)
		Me.rbDatabase.Name = "rbDatabase"
		Me.rbDatabase.Size = New System.Drawing.Size(167, 17)
		Me.rbDatabase.TabIndex = 7
		Me.rbDatabase.TabStop = True
		Me.rbDatabase.Text = "Load templates from database"
		Me.rbDatabase.UseVisualStyleBackColor = True
		'
		'tbPath
		'
		Me.tbPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbPath.Location = New System.Drawing.Point(23, 42)
		Me.tbPath.Name = "tbPath"
		Me.tbPath.Size = New System.Drawing.Size(229, 20)
		Me.tbPath.TabIndex = 5
		Me.tbPath.Text = "c:\"
		'
		'btnBrowse
		'
		Me.btnBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnBrowse.Location = New System.Drawing.Point(256, 40)
		Me.btnBrowse.Name = "btnBrowse"
		Me.btnBrowse.Size = New System.Drawing.Size(38, 23)
		Me.btnBrowse.TabIndex = 6
		Me.btnBrowse.Text = "..."
		Me.btnBrowse.UseVisualStyleBackColor = True
		'
		'gbDatabase
		'
		Me.gbDatabase.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbDatabase.Controls.Add(Me.btnReset)
		Me.gbDatabase.Controls.Add(Me.btnConnect)
		Me.gbDatabase.Controls.Add(Me.cbId)
		Me.gbDatabase.Controls.Add(Me.cbTemplate)
		Me.gbDatabase.Controls.Add(Me.label7)
		Me.gbDatabase.Controls.Add(Me.label6)
		Me.gbDatabase.Controls.Add(Me.cbTable)
		Me.gbDatabase.Controls.Add(Me.tbDBPassword)
		Me.gbDatabase.Controls.Add(Me.tbDBUser)
		Me.gbDatabase.Controls.Add(Me.tbDBServer)
		Me.gbDatabase.Controls.Add(Me.label5)
		Me.gbDatabase.Controls.Add(Me.label4)
		Me.gbDatabase.Controls.Add(Me.label3)
		Me.gbDatabase.Controls.Add(Me.label1)
		Me.gbDatabase.Location = New System.Drawing.Point(6, 98)
		Me.gbDatabase.Name = "gbDatabase"
		Me.gbDatabase.Size = New System.Drawing.Size(288, 204)
		Me.gbDatabase.TabIndex = 6
		Me.gbDatabase.TabStop = False
		'
		'btnReset
		'
		Me.btnReset.Location = New System.Drawing.Point(187, 93)
		Me.btnReset.Name = "btnReset"
		Me.btnReset.Size = New System.Drawing.Size(93, 23)
		Me.btnReset.TabIndex = 14
		Me.btnReset.Text = "&Reset"
		Me.btnReset.UseVisualStyleBackColor = True
		'
		'btnConnect
		'
		Me.btnConnect.Location = New System.Drawing.Point(92, 93)
		Me.btnConnect.Name = "btnConnect"
		Me.btnConnect.Size = New System.Drawing.Size(89, 23)
		Me.btnConnect.TabIndex = 13
		Me.btnConnect.Text = "&Connect"
		Me.btnConnect.UseVisualStyleBackColor = True
		'
		'cbId
		'
		Me.cbId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbId.FormattingEnabled = True
		Me.cbId.Location = New System.Drawing.Point(92, 176)
		Me.cbId.Name = "cbId"
		Me.cbId.Size = New System.Drawing.Size(190, 21)
		Me.cbId.TabIndex = 17
		'
		'cbTemplate
		'
		Me.cbTemplate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbTemplate.FormattingEnabled = True
		Me.cbTemplate.Location = New System.Drawing.Point(92, 149)
		Me.cbTemplate.Name = "cbTemplate"
		Me.cbTemplate.Size = New System.Drawing.Size(189, 21)
		Me.cbTemplate.TabIndex = 16
		'
		'label7
		'
		Me.label7.AutoSize = True
		Me.label7.Location = New System.Drawing.Point(3, 179)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(60, 13)
		Me.label7.TabIndex = 13
		Me.label7.Text = "ID collumn:"
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Location = New System.Drawing.Point(3, 152)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(93, 13)
		Me.label6.TabIndex = 12
		Me.label6.Text = "Template collumn:"
		'
		'cbTable
		'
		Me.cbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbTable.FormattingEnabled = True
		Me.cbTable.Location = New System.Drawing.Point(92, 122)
		Me.cbTable.Name = "cbTable"
		Me.cbTable.Size = New System.Drawing.Size(188, 21)
		Me.cbTable.TabIndex = 15
		'
		'tbDBPassword
		'
		Me.tbDBPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbDBPassword.Location = New System.Drawing.Point(59, 64)
		Me.tbDBPassword.Name = "tbDBPassword"
		Me.tbDBPassword.Size = New System.Drawing.Size(223, 20)
		Me.tbDBPassword.TabIndex = 12
		Me.tbDBPassword.UseSystemPasswordChar = True
		'
		'tbDBUser
		'
		Me.tbDBUser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbDBUser.Location = New System.Drawing.Point(59, 39)
		Me.tbDBUser.Name = "tbDBUser"
		Me.tbDBUser.Size = New System.Drawing.Size(223, 20)
		Me.tbDBUser.TabIndex = 11
		'
		'tbDBServer
		'
		Me.tbDBServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbDBServer.Location = New System.Drawing.Point(59, 13)
		Me.tbDBServer.Name = "tbDBServer"
		Me.tbDBServer.Size = New System.Drawing.Size(223, 20)
		Me.tbDBServer.TabIndex = 9
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(3, 130)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(37, 13)
		Me.label5.TabIndex = 4
		Me.label5.Text = "Table:"
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(3, 67)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(36, 13)
		Me.label4.TabIndex = 3
		Me.label4.Text = "PWD:"
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(3, 42)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(29, 13)
		Me.label3.TabIndex = 2
		Me.label3.Text = "UID:"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(5, 16)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(33, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "DSN:"
		'
		'folderBrowserDialog
		'
		Me.folderBrowserDialog.Description = "Select directory with templates"
		'
		'gbAccelerator
		'
		Me.gbAccelerator.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbAccelerator.Controls.Add(Me.btnCheckConnection)
		Me.gbAccelerator.Controls.Add(Me.chbIsAccelerator)
		Me.gbAccelerator.Controls.Add(Me.nudAdminPort)
		Me.gbAccelerator.Controls.Add(Me.label12)
		Me.gbAccelerator.Controls.Add(Me.nudPort)
		Me.gbAccelerator.Controls.Add(Me.tbUsername)
		Me.gbAccelerator.Controls.Add(Me.tbMMAServer)
		Me.gbAccelerator.Controls.Add(Me.mtbPasword)
		Me.gbAccelerator.Controls.Add(Me.label11)
		Me.gbAccelerator.Controls.Add(Me.label10)
		Me.gbAccelerator.Controls.Add(Me.label9)
		Me.gbAccelerator.Controls.Add(Me.label8)
		Me.gbAccelerator.Location = New System.Drawing.Point(2, 3)
		Me.gbAccelerator.Name = "gbAccelerator"
		Me.gbAccelerator.Size = New System.Drawing.Size(301, 218)
		Me.gbAccelerator.TabIndex = 7
		Me.gbAccelerator.TabStop = False
		Me.gbAccelerator.Text = "Server connection"
		'
		'btnCheckConnection
		'
		Me.btnCheckConnection.Location = New System.Drawing.Point(90, 101)
		Me.btnCheckConnection.Name = "btnCheckConnection"
		Me.btnCheckConnection.Size = New System.Drawing.Size(110, 23)
		Me.btnCheckConnection.TabIndex = 14
		Me.btnCheckConnection.Text = "Check Connection"
		Me.btnCheckConnection.UseVisualStyleBackColor = True
		'
		'chbIsAccelerator
		'
		Me.chbIsAccelerator.AutoSize = True
		Me.chbIsAccelerator.Location = New System.Drawing.Point(6, 134)
		Me.chbIsAccelerator.Name = "chbIsAccelerator"
		Me.chbIsAccelerator.Size = New System.Drawing.Size(91, 17)
		Me.chbIsAccelerator.TabIndex = 6
		Me.chbIsAccelerator.Text = "Is Accelerator"
		Me.chbIsAccelerator.UseVisualStyleBackColor = True
		'
		'nudAdminPort
		'
		Me.nudAdminPort.Location = New System.Drawing.Point(90, 75)
		Me.nudAdminPort.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
		Me.nudAdminPort.Name = "nudAdminPort"
		Me.nudAdminPort.Size = New System.Drawing.Size(205, 20)
		Me.nudAdminPort.TabIndex = 4
		'
		'label12
		'
		Me.label12.AutoSize = True
		Me.label12.Location = New System.Drawing.Point(6, 76)
		Me.label12.Name = "label12"
		Me.label12.Size = New System.Drawing.Size(60, 13)
		Me.label12.TabIndex = 5
		Me.label12.Text = "Admin port:"
		'
		'nudPort
		'
		Me.nudPort.Location = New System.Drawing.Point(90, 45)
		Me.nudPort.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
		Me.nudPort.Name = "nudPort"
		Me.nudPort.Size = New System.Drawing.Size(205, 20)
		Me.nudPort.TabIndex = 1
		'
		'tbUsername
		'
		Me.tbUsername.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbUsername.Enabled = False
		Me.tbUsername.Location = New System.Drawing.Point(90, 157)
		Me.tbUsername.Name = "tbUsername"
		Me.tbUsername.Size = New System.Drawing.Size(204, 20)
		Me.tbUsername.TabIndex = 2
		Me.tbUsername.Text = "Admin"
		'
		'tbMMAServer
		'
		Me.tbMMAServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tbMMAServer.Location = New System.Drawing.Point(90, 18)
		Me.tbMMAServer.Name = "tbMMAServer"
		Me.tbMMAServer.Size = New System.Drawing.Size(205, 20)
		Me.tbMMAServer.TabIndex = 0
		Me.tbMMAServer.Text = "192.168.0.1"
		'
		'mtbPasword
		'
		Me.mtbPasword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.mtbPasword.Enabled = False
		Me.mtbPasword.Location = New System.Drawing.Point(90, 183)
		Me.mtbPasword.Name = "mtbPasword"
		Me.mtbPasword.Size = New System.Drawing.Size(204, 20)
		Me.mtbPasword.TabIndex = 3
		Me.mtbPasword.Text = "Admin"
		Me.mtbPasword.UseSystemPasswordChar = True
		'
		'label11
		'
		Me.label11.AutoSize = True
		Me.label11.Location = New System.Drawing.Point(6, 46)
		Me.label11.Name = "label11"
		Me.label11.Size = New System.Drawing.Size(57, 13)
		Me.label11.TabIndex = 3
		Me.label11.Text = "Client port:"
		'
		'label10
		'
		Me.label10.AutoSize = True
		Me.label10.Location = New System.Drawing.Point(20, 186)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(56, 13)
		Me.label10.TabIndex = 2
		Me.label10.Text = "Password:"
		'
		'label9
		'
		Me.label9.AutoSize = True
		Me.label9.Location = New System.Drawing.Point(20, 160)
		Me.label9.Name = "label9"
		Me.label9.Size = New System.Drawing.Size(61, 13)
		Me.label9.TabIndex = 1
		Me.label9.Text = "User name:"
		'
		'label8
		'
		Me.label8.AutoSize = True
		Me.label8.Location = New System.Drawing.Point(6, 21)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(81, 13)
		Me.label8.TabIndex = 0
		Me.label8.Text = "Server address:"
		'
		'gbTemplates
		'
		Me.gbTemplates.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbTemplates.Controls.Add(Me.rbDirectory)
		Me.gbTemplates.Controls.Add(Me.btnBrowse)
		Me.gbTemplates.Controls.Add(Me.gbDatabase)
		Me.gbTemplates.Controls.Add(Me.tbPath)
		Me.gbTemplates.Controls.Add(Me.rbDatabase)
		Me.gbTemplates.Location = New System.Drawing.Point(2, 227)
		Me.gbTemplates.Name = "gbTemplates"
		Me.gbTemplates.Size = New System.Drawing.Size(301, 308)
		Me.gbTemplates.TabIndex = 8
		Me.gbTemplates.TabStop = False
		Me.gbTemplates.Text = "Templates"
		'
		'ConnectionForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(304, 567)
		Me.Controls.Add(Me.gbTemplates)
		Me.Controls.Add(Me.gbAccelerator)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOk)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.MinimumSize = New System.Drawing.Size(280, 290)
		Me.Name = "ConnectionForm"
		Me.ShowInTaskbar = False
		Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
		Me.Text = "Connection settings"
		Me.gbDatabase.ResumeLayout(False)
		Me.gbDatabase.PerformLayout()
		Me.gbAccelerator.ResumeLayout(False)
		Me.gbAccelerator.PerformLayout()
		CType(Me.nudAdminPort, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudPort, System.ComponentModel.ISupportInitialize).EndInit()
		Me.gbTemplates.ResumeLayout(False)
		Me.gbTemplates.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private WithEvents rbDirectory As System.Windows.Forms.RadioButton
	Private rbDatabase As System.Windows.Forms.RadioButton
	Private tbPath As System.Windows.Forms.TextBox
	Private WithEvents btnBrowse As System.Windows.Forms.Button
	Private gbDatabase As System.Windows.Forms.GroupBox
	Private tbDBUser As System.Windows.Forms.TextBox
	Private tbDBServer As System.Windows.Forms.TextBox
	Private label5 As System.Windows.Forms.Label
	Private label4 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
	Private tbDBPassword As System.Windows.Forms.MaskedTextBox
	Private label7 As System.Windows.Forms.Label
	Private label6 As System.Windows.Forms.Label
	Private WithEvents cbTable As System.Windows.Forms.ComboBox
	Private WithEvents btnConnect As System.Windows.Forms.Button
	Private cbId As System.Windows.Forms.ComboBox
	Private cbTemplate As System.Windows.Forms.ComboBox
	Private gbAccelerator As System.Windows.Forms.GroupBox
	Private label10 As System.Windows.Forms.Label
	Private label9 As System.Windows.Forms.Label
	Private label8 As System.Windows.Forms.Label
	Private tbMMAServer As System.Windows.Forms.TextBox
	Private mtbPasword As System.Windows.Forms.MaskedTextBox
	Private label11 As System.Windows.Forms.Label
	Private tbUsername As System.Windows.Forms.TextBox
	Private gbTemplates As System.Windows.Forms.GroupBox
	Private nudPort As System.Windows.Forms.NumericUpDown
	Private WithEvents btnReset As System.Windows.Forms.Button
	Private nudAdminPort As System.Windows.Forms.NumericUpDown
	Private label12 As System.Windows.Forms.Label
	Private WithEvents chbIsAccelerator As System.Windows.Forms.CheckBox
	Private WithEvents btnCheckConnection As System.Windows.Forms.Button
End Class
