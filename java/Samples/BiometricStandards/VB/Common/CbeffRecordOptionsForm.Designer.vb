Partial Public Class CbeffRecordOptionsForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CbeffRecordOptionsForm))
		Me.gbCommon = New System.Windows.Forms.GroupBox
		Me.rbOwnerType = New System.Windows.Forms.RadioButton
		Me.rbUseFormat = New System.Windows.Forms.RadioButton
		Me.txtBoxFormat = New System.Windows.Forms.TextBox
		Me.lbFormat = New System.Windows.Forms.Label
		Me.lbOwner = New System.Windows.Forms.Label
		Me.lbType = New System.Windows.Forms.Label
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnClose = New System.Windows.Forms.Button
		Me.fbOwnerType = New System.Windows.Forms.GroupBox
		Me.cbTypes = New System.Windows.Forms.ComboBox
		Me.cbOwners = New System.Windows.Forms.ComboBox
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.gbCommon.SuspendLayout()
		Me.fbOwnerType.SuspendLayout()
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'gbCommon
		'
		Me.gbCommon.Controls.Add(Me.rbOwnerType)
		Me.gbCommon.Controls.Add(Me.rbUseFormat)
		Me.gbCommon.Location = New System.Drawing.Point(12, 12)
		Me.gbCommon.Name = "gbCommon"
		Me.gbCommon.Size = New System.Drawing.Size(213, 47)
		Me.gbCommon.TabIndex = 1
		Me.gbCommon.TabStop = False
		Me.gbCommon.Text = "Common"
		'
		'rbOwnerType
		'
		Me.rbOwnerType.AutoSize = True
		Me.rbOwnerType.Checked = True
		Me.rbOwnerType.Location = New System.Drawing.Point(9, 19)
		Me.rbOwnerType.Name = "rbOwnerType"
		Me.rbOwnerType.Size = New System.Drawing.Size(104, 17)
		Me.rbOwnerType.TabIndex = 10
		Me.rbOwnerType.TabStop = True
		Me.rbOwnerType.Text = "Owner and Type"
		Me.rbOwnerType.UseVisualStyleBackColor = True
		'
		'rbUseFormat
		'
		Me.rbUseFormat.AutoSize = True
		Me.rbUseFormat.Location = New System.Drawing.Point(119, 19)
		Me.rbUseFormat.Name = "rbUseFormat"
		Me.rbUseFormat.Size = New System.Drawing.Size(91, 17)
		Me.rbUseFormat.TabIndex = 8
		Me.rbUseFormat.Text = "Patron Format"
		Me.rbUseFormat.UseVisualStyleBackColor = True
		'
		'txtBoxFormat
		'
		Me.txtBoxFormat.Enabled = False
		Me.txtBoxFormat.Location = New System.Drawing.Point(54, 19)
		Me.txtBoxFormat.Name = "txtBoxFormat"
		Me.txtBoxFormat.Size = New System.Drawing.Size(150, 20)
		Me.txtBoxFormat.TabIndex = 9
		'
		'lbFormat
		'
		Me.lbFormat.AutoSize = True
		Me.lbFormat.Location = New System.Drawing.Point(6, 22)
		Me.lbFormat.Name = "lbFormat"
		Me.lbFormat.Size = New System.Drawing.Size(42, 13)
		Me.lbFormat.TabIndex = 6
		Me.lbFormat.Text = "Format:"
		'
		'lbOwner
		'
		Me.lbOwner.AutoSize = True
		Me.lbOwner.Location = New System.Drawing.Point(6, 22)
		Me.lbOwner.Name = "lbOwner"
		Me.lbOwner.Size = New System.Drawing.Size(41, 13)
		Me.lbOwner.TabIndex = 3
		Me.lbOwner.Text = "Owner:"
		'
		'lbType
		'
		Me.lbType.AutoSize = True
		Me.lbType.Location = New System.Drawing.Point(6, 48)
		Me.lbType.Name = "lbType"
		Me.lbType.Size = New System.Drawing.Size(34, 13)
		Me.lbType.TabIndex = 2
		Me.lbType.Text = "Type:"
		'
		'btnOk
		'
		Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnOk.Location = New System.Drawing.Point(285, 145)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 2
		Me.btnOk.Text = "&Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnClose
		'
		Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnClose.Location = New System.Drawing.Point(366, 145)
		Me.btnClose.Name = "btnClose"
		Me.btnClose.Size = New System.Drawing.Size(75, 23)
		Me.btnClose.TabIndex = 3
		Me.btnClose.Text = "&Close"
		Me.btnClose.UseVisualStyleBackColor = True
		'
		'fbOwnerType
		'
		Me.fbOwnerType.Controls.Add(Me.cbTypes)
		Me.fbOwnerType.Controls.Add(Me.cbOwners)
		Me.fbOwnerType.Controls.Add(Me.lbOwner)
		Me.fbOwnerType.Controls.Add(Me.lbType)
		Me.fbOwnerType.Location = New System.Drawing.Point(12, 65)
		Me.fbOwnerType.Name = "fbOwnerType"
		Me.fbOwnerType.Size = New System.Drawing.Size(429, 74)
		Me.fbOwnerType.TabIndex = 10
		Me.fbOwnerType.TabStop = False
		Me.fbOwnerType.Text = "Owner and Type"
		'
		'cbTypes
		'
		Me.cbTypes.FormattingEnabled = True
		Me.cbTypes.Location = New System.Drawing.Point(53, 45)
		Me.cbTypes.Name = "cbTypes"
		Me.cbTypes.Size = New System.Drawing.Size(370, 21)
		Me.cbTypes.TabIndex = 5
		'
		'cbOwners
		'
		Me.cbOwners.FormattingEnabled = True
		Me.cbOwners.Location = New System.Drawing.Point(53, 19)
		Me.cbOwners.Name = "cbOwners"
		Me.cbOwners.Size = New System.Drawing.Size(370, 21)
		Me.cbOwners.TabIndex = 4
		'
		'groupBox1
		'
		Me.groupBox1.Controls.Add(Me.lbFormat)
		Me.groupBox1.Controls.Add(Me.txtBoxFormat)
		Me.groupBox1.Location = New System.Drawing.Point(231, 12)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(210, 47)
		Me.groupBox1.TabIndex = 11
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Patron Format"
		'
		'CbeffRecordOptionsForm
		'
		Me.AcceptButton = Me.btnOk
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnClose
		Me.ClientSize = New System.Drawing.Size(451, 175)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.fbOwnerType)
		Me.Controls.Add(Me.btnClose)
		Me.Controls.Add(Me.btnOk)
		Me.Controls.Add(Me.gbCommon)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "CbeffRecordOptionsForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "CbeffRecord Options"
		Me.gbCommon.ResumeLayout(False)
		Me.gbCommon.PerformLayout()
		Me.fbOwnerType.ResumeLayout(False)
		Me.fbOwnerType.PerformLayout()
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private lbType As System.Windows.Forms.Label
	Private lbOwner As System.Windows.Forms.Label
	Private btnOk As System.Windows.Forms.Button
	Private btnClose As System.Windows.Forms.Button
	Private WithEvents rbUseFormat As System.Windows.Forms.RadioButton
	Private lbFormat As System.Windows.Forms.Label
	Private WithEvents rbOwnerType As System.Windows.Forms.RadioButton
	Private fbOwnerType As System.Windows.Forms.GroupBox
	Private cbTypes As System.Windows.Forms.ComboBox
	Private cbOwners As System.Windows.Forms.ComboBox
	Protected WithEvents txtBoxFormat As System.Windows.Forms.TextBox
	Protected WithEvents groupBox1 As System.Windows.Forms.GroupBox
	Protected WithEvents gbCommon As System.Windows.Forms.GroupBox
End Class