Partial Public Class MainForm
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.mainMenuStrip = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveSelectedStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.edToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addCbeffRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
		Me.InsertBeforeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.InsertAfterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.removeBranchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
		Me.addFCRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFIRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFMRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addIIRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fmRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.fmRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.cbeffRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.cbeffRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.splitContainer2 = New System.Windows.Forms.SplitContainer
		Me.treeView = New System.Windows.Forms.TreeView
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid
		Me.fcRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.fiRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.iiRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.fcRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.iiRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.fiRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.mainMenuStrip.SuspendLayout()
		Me.splitContainer2.Panel1.SuspendLayout()
		Me.splitContainer2.Panel2.SuspendLayout()
		Me.splitContainer2.SuspendLayout()
		Me.SuspendLayout()
		'
		'mainMenuStrip
		'
		Me.mainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.edToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.mainMenuStrip.Location = New System.Drawing.Point(0, 0)
		Me.mainMenuStrip.Name = "mainMenuStrip"
		Me.mainMenuStrip.Size = New System.Drawing.Size(394, 24)
		Me.mainMenuStrip.TabIndex = 3
		Me.mainMenuStrip.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.toolStripSeparator4, Me.openToolStripMenuItem, Me.toolStripSeparator2, Me.saveAsToolStripMenuItem, Me.saveSelectedStripMenuItem, Me.toolStripSeparator1, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'newToolStripMenuItem
		'
		Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
		Me.newToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.newToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
		Me.newToolStripMenuItem.Text = "&New"
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(143, 6)
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
		Me.openToolStripMenuItem.Text = "&Open"
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(143, 6)
		'
		'saveAsToolStripMenuItem
		'
		Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
		Me.saveAsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
		Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
		Me.saveAsToolStripMenuItem.Text = "&Save"
		'
		'saveSelectedStripMenuItem
		'
		Me.saveSelectedStripMenuItem.Name = "saveSelectedStripMenuItem"
		Me.saveSelectedStripMenuItem.Size = New System.Drawing.Size(146, 22)
		Me.saveSelectedStripMenuItem.Text = "&Save Selected"
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(143, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'edToolStripMenuItem
		'
		Me.edToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addCbeffRecordToolStripMenuItem, Me.addFromFileToolStripMenuItem, Me.toolStripSeparator5, Me.InsertBeforeToolStripMenuItem, Me.InsertAfterToolStripMenuItem, Me.toolStripSeparator3, Me.removeBranchToolStripMenuItem, Me.toolStripSeparator6, Me.addFCRecordToolStripMenuItem, Me.addFIRecordToolStripMenuItem, Me.addFMRecordToolStripMenuItem, Me.addIIRecordToolStripMenuItem})
		Me.edToolStripMenuItem.Name = "edToolStripMenuItem"
		Me.edToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.edToolStripMenuItem.Text = "&Edit"
		'
		'addCbeffRecordToolStripMenuItem
		'
		Me.addCbeffRecordToolStripMenuItem.Enabled = False
		Me.addCbeffRecordToolStripMenuItem.Name = "addCbeffRecordToolStripMenuItem"
		Me.addCbeffRecordToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.addCbeffRecordToolStripMenuItem.Text = "&Add CbeffRecord"
		'
		'addFromFileToolStripMenuItem
		'
		Me.addFromFileToolStripMenuItem.Enabled = False
		Me.addFromFileToolStripMenuItem.Name = "addFromFileToolStripMenuItem"
		Me.addFromFileToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.addFromFileToolStripMenuItem.Text = "&Add CbeffRecord from file"
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(210, 6)
		'
		'InsertBeforeToolStripMenuItem
		'
		Me.InsertBeforeToolStripMenuItem.Enabled = False
		Me.InsertBeforeToolStripMenuItem.Name = "InsertBeforeToolStripMenuItem"
		Me.InsertBeforeToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.InsertBeforeToolStripMenuItem.Text = "&Insert CbeffRecord before"
		Me.InsertBeforeToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'InsertAfterToolStripMenuItem
		'
		Me.InsertAfterToolStripMenuItem.Enabled = False
		Me.InsertAfterToolStripMenuItem.Name = "InsertAfterToolStripMenuItem"
		Me.InsertAfterToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.InsertAfterToolStripMenuItem.Text = "&Insert CbeffRecord after"
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(210, 6)
		'
		'removeBranchToolStripMenuItem
		'
		Me.removeBranchToolStripMenuItem.Enabled = False
		Me.removeBranchToolStripMenuItem.Name = "removeBranchToolStripMenuItem"
		Me.removeBranchToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.removeBranchToolStripMenuItem.Text = "&Remove Item"
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		Me.toolStripSeparator6.Size = New System.Drawing.Size(210, 6)
		'
		'addFCRecordToolStripMenuItem
		'
		Me.addFCRecordToolStripMenuItem.Enabled = False
		Me.addFCRecordToolStripMenuItem.Name = "addFCRecordToolStripMenuItem"
		Me.addFCRecordToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.addFCRecordToolStripMenuItem.Text = "&Add FCRecord from file"
		'
		'addFIRecordToolStripMenuItem
		'
		Me.addFIRecordToolStripMenuItem.Enabled = False
		Me.addFIRecordToolStripMenuItem.Name = "addFIRecordToolStripMenuItem"
		Me.addFIRecordToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.addFIRecordToolStripMenuItem.Text = "&Add FIRecord from file"
		'
		'addFMRecordToolStripMenuItem
		'
		Me.addFMRecordToolStripMenuItem.Enabled = False
		Me.addFMRecordToolStripMenuItem.Name = "addFMRecordToolStripMenuItem"
		Me.addFMRecordToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.addFMRecordToolStripMenuItem.Text = "&Add FMRecord from file"
		'
		'addIIRecordToolStripMenuItem
		'
		Me.addIIRecordToolStripMenuItem.Enabled = False
		Me.addIIRecordToolStripMenuItem.Name = "addIIRecordToolStripMenuItem"
		Me.addIIRecordToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.addIIRecordToolStripMenuItem.Text = "&Add IIRecord from file"
		'
		'helpToolStripMenuItem
		'
		Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.aboutToolStripMenuItem})
		Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
		Me.helpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.helpToolStripMenuItem.Text = "&Help"
		'
		'aboutToolStripMenuItem
		'
		Me.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem"
		Me.aboutToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
		Me.aboutToolStripMenuItem.Text = "&About"
		'
		'fmRecordOpenFileDialog
		'
		Me.fmRecordOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FIRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		Me.fmRecordOpenFileDialog.Filter = "FMRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'fmRecordSaveFileDialog
		'
		Me.fmRecordSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		Me.fmRecordSaveFileDialog.Filter = "FMRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'cbeffRecordOpenFileDialog
		'
		Me.cbeffRecordOpenFileDialog.Filter = "CbeffRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'cbeffRecordSaveFileDialog
		'
		Me.cbeffRecordSaveFileDialog.Filter = "CbeffRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'splitContainer2
		'
		Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer2.Location = New System.Drawing.Point(0, 24)
		Me.splitContainer2.Name = "splitContainer2"
		Me.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'splitContainer2.Panel1
		'
		Me.splitContainer2.Panel1.Controls.Add(Me.treeView)
		'
		'splitContainer2.Panel2
		'
		Me.splitContainer2.Panel2.Controls.Add(Me.propertyGrid)
		Me.splitContainer2.Size = New System.Drawing.Size(394, 617)
		Me.splitContainer2.SplitterDistance = 322
		Me.splitContainer2.TabIndex = 4
		'
		'treeView
		'
		Me.treeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.treeView.Location = New System.Drawing.Point(0, 0)
		Me.treeView.Name = "treeView"
		Me.treeView.Size = New System.Drawing.Size(394, 322)
		Me.treeView.TabIndex = 0
		'
		'propertyGrid
		'
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.Location = New System.Drawing.Point(0, 0)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.Size = New System.Drawing.Size(394, 291)
		Me.propertyGrid.TabIndex = 2
		'
		'fcRecordOpenFileDialog
		'
		Me.fcRecordOpenFileDialog.Filter = "FCRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'fiRecordOpenFileDialog
		'
		Me.fiRecordOpenFileDialog.Filter = "FIRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'iiRecordOpenFileDialog
		'
		Me.iiRecordOpenFileDialog.Filter = "IIRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'fcRecordSaveFileDialog
		'
		Me.fcRecordSaveFileDialog.Filter = "FCRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'iiRecordSaveFileDialog
		'
		Me.iiRecordSaveFileDialog.Filter = "IIRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'fiRecordSaveFileDialog
		'
		Me.fiRecordSaveFileDialog.Filter = "FIRecord |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(394, 641)
		Me.Controls.Add(Me.splitContainer2)
		Me.Controls.Add(Me.mainMenuStrip)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "CbeffRecordSampleCS"
		Me.mainMenuStrip.ResumeLayout(False)
		Me.mainMenuStrip.PerformLayout()
		Me.splitContainer2.Panel1.ResumeLayout(False)
		Me.splitContainer2.Panel2.ResumeLayout(False)
		Me.splitContainer2.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private Shadows mainMenuStrip As System.Windows.Forms.MenuStrip
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private edToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fmRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fmRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private cbeffRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private cbeffRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private splitContainer2 As System.Windows.Forms.SplitContainer
	Private WithEvents treeView As System.Windows.Forms.TreeView
	Private propertyGrid As System.Windows.Forms.PropertyGrid
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents removeBranchToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addCbeffRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveSelectedStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents InsertBeforeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents InsertAfterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFCRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFIRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFMRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addIIRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fcRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fiRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private iiRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fcRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private iiRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private fiRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
End Class