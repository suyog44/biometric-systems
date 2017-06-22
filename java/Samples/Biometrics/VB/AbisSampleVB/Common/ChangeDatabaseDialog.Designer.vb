Imports Microsoft.VisualBasic
Imports System
Partial Public Class ChangeDatabaseDialog
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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangeDatabaseDialog))
		Me.tbConnectionString = New System.Windows.Forms.TextBox
		Me.nudClientPort = New System.Windows.Forms.NumericUpDown
		Me.nudAdminPort = New System.Windows.Forms.NumericUpDown
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnOK = New System.Windows.Forms.Button
		Me.chbClear = New System.Windows.Forms.CheckBox
		Me.lblClientPort = New System.Windows.Forms.Label
		Me.lblAdminPort = New System.Windows.Forms.Label
		Me.cbTableName = New System.Windows.Forms.ComboBox
		Me.btnListTables = New System.Windows.Forms.Button
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.tbHostName = New System.Windows.Forms.TextBox
		Me.rbRemoteServer = New System.Windows.Forms.RadioButton
		Me.rbSQLite = New System.Windows.Forms.RadioButton
		Me.rbOdbc = New System.Windows.Forms.RadioButton
		Me.label1 = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.label3 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.lblDbSchema = New System.Windows.Forms.Label
		Me.cbSchema = New System.Windows.Forms.ComboBox
		Me.btnEdit = New System.Windows.Forms.Button
		Me.label5 = New System.Windows.Forms.Label
		Me.cbLocalOperations = New System.Windows.Forms.ComboBox
		Me.label6 = New System.Windows.Forms.Label
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		CType(Me.nudClientPort, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudAdminPort, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'tbConnectionString
		'
		Me.tbConnectionString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.tbConnectionString, 2)
		Me.tbConnectionString.Location = New System.Drawing.Point(121, 59)
		Me.tbConnectionString.Name = "tbConnectionString"
		Me.tbConnectionString.Size = New System.Drawing.Size(263, 20)
		Me.tbConnectionString.TabIndex = 3
		'
		'nudClientPort
		'
		Me.nudClientPort.Location = New System.Drawing.Point(121, 181)
		Me.nudClientPort.Maximum = New Decimal(New Integer() {32000, 0, 0, 0})
		Me.nudClientPort.Name = "nudClientPort"
		Me.nudClientPort.Size = New System.Drawing.Size(111, 20)
		Me.nudClientPort.TabIndex = 6
		Me.nudClientPort.Value = New Decimal(New Integer() {25452, 0, 0, 0})
		'
		'nudAdminPort
		'
		Me.nudAdminPort.Location = New System.Drawing.Point(121, 207)
		Me.nudAdminPort.Maximum = New Decimal(New Integer() {32000, 0, 0, 0})
		Me.nudAdminPort.Name = "nudAdminPort"
		Me.nudAdminPort.Size = New System.Drawing.Size(111, 20)
		Me.nudAdminPort.TabIndex = 7
		Me.nudAdminPort.Value = New Decimal(New Integer() {24932, 0, 0, 0})
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(309, 322)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 21)
		Me.btnCancel.TabIndex = 8
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnOK
		'
		Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOK.Location = New System.Drawing.Point(230, 322)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(73, 21)
		Me.btnOK.TabIndex = 9
		Me.btnOK.Text = "&OK"
		Me.btnOK.UseVisualStyleBackColor = True
		'
		'chbClear
		'
		Me.chbClear.AutoSize = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.chbClear, 4)
		Me.chbClear.Location = New System.Drawing.Point(3, 266)
		Me.chbClear.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
		Me.chbClear.Name = "chbClear"
		Me.chbClear.Size = New System.Drawing.Size(87, 17)
		Me.chbClear.TabIndex = 10
		Me.chbClear.Text = "Clear all data"
		Me.chbClear.UseVisualStyleBackColor = True
		'
		'lblClientPort
		'
		Me.lblClientPort.AutoSize = True
		Me.lblClientPort.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblClientPort.Location = New System.Drawing.Point(23, 178)
		Me.lblClientPort.Name = "lblClientPort"
		Me.lblClientPort.Size = New System.Drawing.Size(92, 26)
		Me.lblClientPort.TabIndex = 13
		Me.lblClientPort.Text = "Client port:"
		Me.lblClientPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAdminPort
		'
		Me.lblAdminPort.AutoSize = True
		Me.lblAdminPort.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblAdminPort.Location = New System.Drawing.Point(23, 204)
		Me.lblAdminPort.Name = "lblAdminPort"
		Me.lblAdminPort.Size = New System.Drawing.Size(92, 26)
		Me.lblAdminPort.TabIndex = 14
		Me.lblAdminPort.Text = "Admin port:"
		Me.lblAdminPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbTableName
		'
		Me.cbTableName.FormattingEnabled = True
		Me.cbTableName.Location = New System.Drawing.Point(121, 98)
		Me.cbTableName.Name = "cbTableName"
		Me.cbTableName.Size = New System.Drawing.Size(118, 21)
		Me.cbTableName.TabIndex = 16
		'
		'btnListTables
		'
		Me.btnListTables.Location = New System.Drawing.Point(309, 98)
		Me.btnListTables.Name = "btnListTables"
		Me.btnListTables.Size = New System.Drawing.Size(75, 23)
		Me.btnListTables.TabIndex = 17
		Me.btnListTables.Text = "List"
		Me.btnListTables.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.ColumnCount = 4
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.Controls.Add(Me.tbHostName, 2, 6)
		Me.tableLayoutPanel2.Controls.Add(Me.rbRemoteServer, 0, 5)
		Me.tableLayoutPanel2.Controls.Add(Me.rbSQLite, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.tbConnectionString, 2, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.btnListTables, 3, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.cbTableName, 2, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.nudAdminPort, 2, 8)
		Me.tableLayoutPanel2.Controls.Add(Me.nudClientPort, 2, 7)
		Me.tableLayoutPanel2.Controls.Add(Me.rbOdbc, 0, 1)
		Me.tableLayoutPanel2.Controls.Add(Me.label1, 1, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.btnCancel, 3, 12)
		Me.tableLayoutPanel2.Controls.Add(Me.label2, 1, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.btnOK, 2, 12)
		Me.tableLayoutPanel2.Controls.Add(Me.lblClientPort, 1, 7)
		Me.tableLayoutPanel2.Controls.Add(Me.chbClear, 0, 10)
		Me.tableLayoutPanel2.Controls.Add(Me.lblAdminPort, 1, 8)
		Me.tableLayoutPanel2.Controls.Add(Me.label3, 1, 6)
		Me.tableLayoutPanel2.Controls.Add(Me.Label4, 1, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.lblDbSchema, 0, 11)
		Me.tableLayoutPanel2.Controls.Add(Me.cbSchema, 2, 11)
		Me.tableLayoutPanel2.Controls.Add(Me.btnEdit, 3, 11)
		Me.tableLayoutPanel2.Controls.Add(Me.label5, 1, 9)
		Me.tableLayoutPanel2.Controls.Add(Me.cbLocalOperations, 2, 9)
		Me.tableLayoutPanel2.Controls.Add(Me.label6, 3, 9)
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 13
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28.0!))
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(387, 346)
		Me.tableLayoutPanel2.TabIndex = 5
		'
		'tbHostName
		'
		Me.tbHostName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.tbHostName, 2)
		Me.tbHostName.Location = New System.Drawing.Point(121, 155)
		Me.tbHostName.Name = "tbHostName"
		Me.tbHostName.Size = New System.Drawing.Size(263, 20)
		Me.tbHostName.TabIndex = 19
		'
		'rbRemoteServer
		'
		Me.rbRemoteServer.AutoSize = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.rbRemoteServer, 4)
		Me.rbRemoteServer.Location = New System.Drawing.Point(3, 132)
		Me.rbRemoteServer.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
		Me.rbRemoteServer.Name = "rbRemoteServer"
		Me.rbRemoteServer.Size = New System.Drawing.Size(140, 17)
		Me.rbRemoteServer.TabIndex = 4
		Me.rbRemoteServer.TabStop = True
		Me.rbRemoteServer.Text = "Remote matching server"
		Me.rbRemoteServer.UseVisualStyleBackColor = True
		'
		'rbSQLite
		'
		Me.rbSQLite.AutoSize = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.rbSQLite, 4)
		Me.rbSQLite.Location = New System.Drawing.Point(3, 8)
		Me.rbSQLite.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
		Me.rbSQLite.Name = "rbSQLite"
		Me.rbSQLite.Size = New System.Drawing.Size(160, 17)
		Me.rbSQLite.TabIndex = 0
		Me.rbSQLite.TabStop = True
		Me.rbSQLite.Text = "SQLite database connection"
		Me.rbSQLite.UseVisualStyleBackColor = True
		'
		'rbOdbc
		'
		Me.rbOdbc.AutoSize = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.rbOdbc, 4)
		Me.rbOdbc.Location = New System.Drawing.Point(3, 36)
		Me.rbOdbc.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
		Me.rbOdbc.Name = "rbOdbc"
		Me.rbOdbc.Size = New System.Drawing.Size(154, 17)
		Me.rbOdbc.TabIndex = 1
		Me.rbOdbc.TabStop = True
		Me.rbOdbc.Text = "Odbc database connection"
		Me.rbOdbc.UseVisualStyleBackColor = True
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(23, 56)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(92, 26)
		Me.label1.TabIndex = 2
		Me.label1.Text = "Connection string:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(23, 95)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(92, 29)
		Me.label2.TabIndex = 3
		Me.label2.Text = "Table name:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(23, 152)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(92, 26)
		Me.label3.TabIndex = 18
		Me.label3.Text = "Server address:"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.Label4, 3)
		Me.Label4.Location = New System.Drawing.Point(23, 82)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(303, 13)
		Me.Label4.TabIndex = 20
		Me.Label4.Text = "                Example: Dsn=mysql_dsn;UID=user;PWD=password"
		'
		'lblDbSchema
		'
		Me.lblDbSchema.AutoSize = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.lblDbSchema, 2)
		Me.lblDbSchema.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblDbSchema.Location = New System.Drawing.Point(3, 286)
		Me.lblDbSchema.Name = "lblDbSchema"
		Me.lblDbSchema.Size = New System.Drawing.Size(112, 29)
		Me.lblDbSchema.TabIndex = 21
		Me.lblDbSchema.Text = "Database schema:"
		Me.lblDbSchema.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbSchema
		'
		Me.cbSchema.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cbSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbSchema.FormattingEnabled = True
		Me.cbSchema.Location = New System.Drawing.Point(121, 289)
		Me.cbSchema.Name = "cbSchema"
		Me.cbSchema.Size = New System.Drawing.Size(182, 21)
		Me.cbSchema.TabIndex = 22
		'
		'btnEdit
		'
		Me.btnEdit.Location = New System.Drawing.Point(309, 289)
		Me.btnEdit.Name = "btnEdit"
		Me.btnEdit.Size = New System.Drawing.Size(75, 23)
		Me.btnEdit.TabIndex = 23
		Me.btnEdit.Text = "Edit"
		Me.btnEdit.UseVisualStyleBackColor = True
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label5.Location = New System.Drawing.Point(23, 230)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(92, 28)
		Me.label5.TabIndex = 26
		Me.label5.Text = "Local operations:"
		Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'cbLocalOperations
		'
		Me.cbLocalOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbLocalOperations.FormattingEnabled = True
		Me.cbLocalOperations.Items.AddRange(New Object() {"None", "Detect", "Detect - DetectSegments", "Detect - Segment", "Detect - AssessQuality", "All"})
		Me.cbLocalOperations.Location = New System.Drawing.Point(121, 233)
		Me.cbLocalOperations.Name = "cbLocalOperations"
		Me.cbLocalOperations.Size = New System.Drawing.Size(182, 21)
		Me.cbLocalOperations.TabIndex = 27
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label6.Image = Global.Neurotec.Samples.My.Resources.Resources.Help
		Me.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.label6.Location = New System.Drawing.Point(309, 230)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(75, 28)
		Me.label6.TabIndex = 28
		Me.label6.Text = "    "
		Me.toolTip.SetToolTip(Me.label6, resources.GetString("label6.ToolTip"))
		'
		'toolTip
		'
		Me.toolTip.AutoPopDelay = 24000
		Me.toolTip.InitialDelay = 500
		Me.toolTip.ReshowDelay = 100
		'
		'ChangeDatabaseDialog
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.AutoSize = True
		Me.ClientSize = New System.Drawing.Size(387, 346)
		Me.Controls.Add(Me.tableLayoutPanel2)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "ChangeDatabaseDialog"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Connection Settings"
		CType(Me.nudClientPort, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudAdminPort, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private tbConnectionString As System.Windows.Forms.TextBox
	Private nudClientPort As System.Windows.Forms.NumericUpDown
	Private nudAdminPort As System.Windows.Forms.NumericUpDown
	Private btnCancel As System.Windows.Forms.Button
	Private WithEvents btnOK As System.Windows.Forms.Button
	Private chbClear As System.Windows.Forms.CheckBox
	Private lblClientPort As System.Windows.Forms.Label
	Private lblAdminPort As System.Windows.Forms.Label
	Private cbTableName As System.Windows.Forms.ComboBox
	Private WithEvents btnListTables As System.Windows.Forms.Button
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents rbSQLite As System.Windows.Forms.RadioButton
	Private WithEvents rbOdbc As System.Windows.Forms.RadioButton
	Private WithEvents rbRemoteServer As System.Windows.Forms.RadioButton
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private label3 As System.Windows.Forms.Label
	Private tbHostName As System.Windows.Forms.TextBox
	Friend WithEvents Label4 As System.Windows.Forms.Label
	Friend WithEvents lblDbSchema As System.Windows.Forms.Label
	Friend WithEvents cbSchema As System.Windows.Forms.ComboBox
	Friend WithEvents btnEdit As System.Windows.Forms.Button
	Friend WithEvents toolTip As System.Windows.Forms.ToolTip
	Private WithEvents label5 As System.Windows.Forms.Label
	Private WithEvents cbLocalOperations As System.Windows.Forms.ComboBox
	Private WithEvents label6 As System.Windows.Forms.Label
End Class