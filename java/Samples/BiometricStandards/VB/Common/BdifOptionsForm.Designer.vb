Partial Public Class BdifOptionsForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BdifOptionsForm))
		Me.gbMain = New System.Windows.Forms.GroupBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.cbVersion = New System.Windows.Forms.ComboBox
		Me.cbBiometricStandard = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.lbVersion = New System.Windows.Forms.Label
		Me.cbDoNotCheckCbeffProductId = New System.Windows.Forms.CheckBox
		Me.cbNoStrictRead = New System.Windows.Forms.CheckBox
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnOk = New System.Windows.Forms.Button
		Me.gbMain.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbMain
		'
		Me.gbMain.Controls.Add(Me.tableLayoutPanel1)
		Me.gbMain.Location = New System.Drawing.Point(12, 12)
		Me.gbMain.Name = "gbMain"
		Me.gbMain.Size = New System.Drawing.Size(311, 121)
		Me.gbMain.TabIndex = 0
		Me.gbMain.TabStop = False
		Me.gbMain.Text = "Common"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.cbVersion, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.cbBiometricStandard, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.lbVersion, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.cbDoNotCheckCbeffProductId, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.cbNoStrictRead, 1, 3)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 4
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(305, 102)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'cbVersion
		'
		Me.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbVersion.FormattingEnabled = True
		Me.cbVersion.Location = New System.Drawing.Point(106, 30)
		Me.cbVersion.Name = "cbVersion"
		Me.cbVersion.Size = New System.Drawing.Size(195, 21)
		Me.cbVersion.TabIndex = 7
		'
		'cbBiometricStandard
		'
		Me.cbBiometricStandard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbBiometricStandard.FormattingEnabled = True
		Me.cbBiometricStandard.Location = New System.Drawing.Point(106, 3)
		Me.cbBiometricStandard.Name = "cbBiometricStandard"
		Me.cbBiometricStandard.Size = New System.Drawing.Size(88, 21)
		Me.cbBiometricStandard.TabIndex = 1
		'
		'label1
		'
		Me.label1.Anchor = System.Windows.Forms.AnchorStyles.Right
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(3, 7)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(97, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Biometric standard:"
		'
		'lbVersion
		'
		Me.lbVersion.Anchor = System.Windows.Forms.AnchorStyles.Right
		Me.lbVersion.AutoSize = True
		Me.lbVersion.Location = New System.Drawing.Point(55, 34)
		Me.lbVersion.Name = "lbVersion"
		Me.lbVersion.Size = New System.Drawing.Size(45, 13)
		Me.lbVersion.TabIndex = 6
		Me.lbVersion.Text = "Version:"
		'
		'cbDoNotCheckCbeffProductId
		'
		Me.cbDoNotCheckCbeffProductId.AutoSize = True
		Me.cbDoNotCheckCbeffProductId.Location = New System.Drawing.Point(106, 57)
		Me.cbDoNotCheckCbeffProductId.Name = "cbDoNotCheckCbeffProductId"
		Me.cbDoNotCheckCbeffProductId.Size = New System.Drawing.Size(177, 17)
		Me.cbDoNotCheckCbeffProductId.TabIndex = 4
		Me.cbDoNotCheckCbeffProductId.Text = "Do not check CBEFF product id"
		Me.cbDoNotCheckCbeffProductId.UseVisualStyleBackColor = True
		'
		'cbNoStrictRead
		'
		Me.cbNoStrictRead.AutoSize = True
		Me.cbNoStrictRead.Location = New System.Drawing.Point(106, 80)
		Me.cbNoStrictRead.Name = "cbNoStrictRead"
		Me.cbNoStrictRead.Size = New System.Drawing.Size(95, 17)
		Me.cbNoStrictRead.TabIndex = 5
		Me.cbNoStrictRead.Text = "Non-strict read"
		Me.cbNoStrictRead.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(245, 149)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 1
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(164, 149)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 2
		Me.btnOk.Text = "&OK"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'BdifOptionsForm
		'
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.ClientSize = New System.Drawing.Size(332, 182)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.gbMain)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "BdifOptionsForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "BdifOptionsForm"
		Me.gbMain.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private gbMain As System.Windows.Forms.GroupBox
	Private WithEvents cbBiometricStandard As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private cbDoNotCheckCbeffProductId As System.Windows.Forms.CheckBox
	Private cbNoStrictRead As System.Windows.Forms.CheckBox
	Protected btnCancel As System.Windows.Forms.Button
	Protected btnOk As System.Windows.Forms.Button
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private cbVersion As System.Windows.Forms.ComboBox
	Private lbVersion As System.Windows.Forms.Label
End Class