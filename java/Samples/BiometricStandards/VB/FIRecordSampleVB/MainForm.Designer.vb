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
		Me.treeView = New System.Windows.Forms.TreeView
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid
		Me.mainMenuStrip = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveFingerAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.openToolStripMenuItemCbeff = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.edToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFingerViewFromImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.removeFingerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.saveIngerAsImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.convertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fiRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.fiRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.cbeffRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.cbeffRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.imageOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.imageSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.fiView = New Neurotec.Biometrics.Standards.Gui.FIView
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.splitContainer2 = New System.Windows.Forms.SplitContainer
		Me.mainMenuStrip.SuspendLayout()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.splitContainer2.Panel1.SuspendLayout()
		Me.splitContainer2.Panel2.SuspendLayout()
		Me.splitContainer2.SuspendLayout()
		Me.SuspendLayout()
		'
		'treeView
		'
		Me.treeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.treeView.Location = New System.Drawing.Point(0, 0)
		Me.treeView.Name = "treeView"
		Me.treeView.Size = New System.Drawing.Size(268, 205)
		Me.treeView.TabIndex = 0
		'
		'propertyGrid
		'
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.Location = New System.Drawing.Point(0, 0)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.Size = New System.Drawing.Size(268, 309)
		Me.propertyGrid.TabIndex = 2
		'
		'mainMenuStrip
		'
		Me.mainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.edToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.mainMenuStrip.Location = New System.Drawing.Point(0, 0)
		Me.mainMenuStrip.Name = "mainMenuStrip"
		Me.mainMenuStrip.Size = New System.Drawing.Size(944, 24)
		Me.mainMenuStrip.TabIndex = 3
		Me.mainMenuStrip.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.saveFingerAsToolStripMenuItem, Me.toolStripSeparator3, Me.openToolStripMenuItemCbeff, Me.toolStripSeparator4, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'newToolStripMenuItem
		'
		Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
		Me.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + N"
		Me.newToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.newToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.newToolStripMenuItem.Text = "&New ..."
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + O"
		Me.openToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.openToolStripMenuItem.Text = "&Open FIRecord ..."
		'
		'saveFingerAsToolStripMenuItem
		'
		Me.saveFingerAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
		Me.saveFingerAsToolStripMenuItem.Enabled = False
		Me.saveFingerAsToolStripMenuItem.Name = "saveFingerAsToolStripMenuItem"
		Me.saveFingerAsToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl + S"
		Me.saveFingerAsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
		Me.saveFingerAsToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.saveFingerAsToolStripMenuItem.Text = "&Save FIRecord ..."
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(210, 6)
		'
		'openToolStripMenuItemCbeff
		'
		Me.openToolStripMenuItemCbeff.Name = "openToolStripMenuItemCbeff"
		Me.openToolStripMenuItemCbeff.Size = New System.Drawing.Size(213, 22)
		Me.openToolStripMenuItemCbeff.Text = "&Open CbeffRecord ..."
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(210, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(213, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'edToolStripMenuItem
		'
		Me.edToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addFingerViewFromImageToolStripMenuItem, Me.removeFingerToolStripMenuItem, Me.toolStripSeparator2, Me.saveIngerAsImageToolStripMenuItem, Me.toolStripSeparator1, Me.convertToolStripMenuItem})
		Me.edToolStripMenuItem.Name = "edToolStripMenuItem"
		Me.edToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.edToolStripMenuItem.Text = "&Edit"
		'
		'addFingerViewFromImageToolStripMenuItem
		'
		Me.addFingerViewFromImageToolStripMenuItem.Enabled = False
		Me.addFingerViewFromImageToolStripMenuItem.Name = "addFingerViewFromImageToolStripMenuItem"
		Me.addFingerViewFromImageToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
		Me.addFingerViewFromImageToolStripMenuItem.Text = "Add finger view ..."
		'
		'removeFingerToolStripMenuItem
		'
		Me.removeFingerToolStripMenuItem.Enabled = False
		Me.removeFingerToolStripMenuItem.Name = "removeFingerToolStripMenuItem"
		Me.removeFingerToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
		Me.removeFingerToolStripMenuItem.Text = "&Remove finger view"
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(216, 6)
		'
		'saveIngerAsImageToolStripMenuItem
		'
		Me.saveIngerAsImageToolStripMenuItem.Enabled = False
		Me.saveIngerAsImageToolStripMenuItem.Name = "saveIngerAsImageToolStripMenuItem"
		Me.saveIngerAsImageToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
		Me.saveIngerAsImageToolStripMenuItem.Text = "&Save fingerView as image ..."
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(216, 6)
		'
		'convertToolStripMenuItem
		'
		Me.convertToolStripMenuItem.Enabled = False
		Me.convertToolStripMenuItem.Name = "convertToolStripMenuItem"
		Me.convertToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
		Me.convertToolStripMenuItem.Text = "&Convert ..."
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
		'fiRecordOpenFileDialog
		'
		Me.fiRecordOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FIRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		Me.fiRecordOpenFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*"
		'
		'fiRecordSaveFileDialog
		'
		Me.fiRecordSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		Me.fiRecordSaveFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*"
		'
		'cbeffRecordOpenFileDialog
		'
		Me.cbeffRecordOpenFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'cbeffRecordSaveFileDialog
		'
		Me.cbeffRecordSaveFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'imageOpenFileDialog
		'
		Me.imageOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'imageSaveFileDialog
		'
		Me.imageSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'fiView
		'
		Me.fiView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fiView.FeatureColor = System.Drawing.Color.Red
		Me.fiView.Location = New System.Drawing.Point(0, 0)
		Me.fiView.Name = "fiView"
		Me.fiView.Record = Nothing
		Me.fiView.Size = New System.Drawing.Size(672, 518)
		Me.fiView.TabIndex = 4
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(0, 24)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.splitContainer2)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.fiView)
		Me.splitContainer1.Size = New System.Drawing.Size(944, 518)
		Me.splitContainer1.SplitterDistance = 268
		Me.splitContainer1.TabIndex = 3
		'
		'splitContainer2
		'
		Me.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer2.Location = New System.Drawing.Point(0, 0)
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
		Me.splitContainer2.Size = New System.Drawing.Size(268, 518)
		Me.splitContainer2.SplitterDistance = 205
		Me.splitContainer2.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(944, 542)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.mainMenuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "FIRecord Editor"
		Me.mainMenuStrip.ResumeLayout(False)
		Me.mainMenuStrip.PerformLayout()
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.splitContainer2.Panel1.ResumeLayout(False)
		Me.splitContainer2.Panel2.ResumeLayout(False)
		Me.splitContainer2.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private WithEvents treeView As System.Windows.Forms.TreeView
	Private propertyGrid As System.Windows.Forms.PropertyGrid
	Private Shadows mainMenuStrip As System.Windows.Forms.MenuStrip
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private edToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents convertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fiRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fiRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private imageOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private imageSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private fiView As Neurotec.Biometrics.Standards.Gui.FIView
	Private WithEvents saveFingerAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveIngerAsImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents removeFingerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFingerViewFromImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private splitContainer2 As System.Windows.Forms.SplitContainer
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents openToolStripMenuItemCbeff As System.Windows.Forms.ToolStripMenuItem
	Private cbeffRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private cbeffRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class