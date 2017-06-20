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
		Me.mainSplitContainer = New System.Windows.Forms.SplitContainer
		Me.leftSplitContainer = New System.Windows.Forms.SplitContainer
		Me.treeView = New System.Windows.Forms.TreeView
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid
		Me.fcView = New Neurotec.Biometrics.Standards.Gui.FCView
		Me.mainMenuStrip = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.openToolStripMenuItemCbeff = New System.Windows.Forms.ToolStripMenuItem
		Me.fileToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFaceFromImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFaceFromRawToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.removeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.saveFaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveFaceAsDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.convertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fcRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.fcRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.imageOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.rawImageOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.saveImageFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.saveRawFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.cbeffRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.cbeffRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.mainSplitContainer.Panel1.SuspendLayout()
		Me.mainSplitContainer.Panel2.SuspendLayout()
		Me.mainSplitContainer.SuspendLayout()
		Me.leftSplitContainer.Panel1.SuspendLayout()
		Me.leftSplitContainer.Panel2.SuspendLayout()
		Me.leftSplitContainer.SuspendLayout()
		Me.mainMenuStrip.SuspendLayout()
		Me.SuspendLayout()
		'
		'mainSplitContainer
		'
		Me.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.mainSplitContainer.Location = New System.Drawing.Point(0, 24)
		Me.mainSplitContainer.Name = "mainSplitContainer"
		'
		'mainSplitContainer.Panel1
		'
		Me.mainSplitContainer.Panel1.Controls.Add(Me.leftSplitContainer)
		'
		'mainSplitContainer.Panel2
		'
		Me.mainSplitContainer.Panel2.Controls.Add(Me.fcView)
		Me.mainSplitContainer.Size = New System.Drawing.Size(729, 489)
		Me.mainSplitContainer.SplitterDistance = 243
		Me.mainSplitContainer.TabIndex = 0
		'
		'leftSplitContainer
		'
		Me.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.leftSplitContainer.Location = New System.Drawing.Point(0, 0)
		Me.leftSplitContainer.Name = "leftSplitContainer"
		Me.leftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'leftSplitContainer.Panel1
		'
		Me.leftSplitContainer.Panel1.Controls.Add(Me.treeView)
		'
		'leftSplitContainer.Panel2
		'
		Me.leftSplitContainer.Panel2.Controls.Add(Me.propertyGrid)
		Me.leftSplitContainer.Size = New System.Drawing.Size(243, 489)
		Me.leftSplitContainer.SplitterDistance = 184
		Me.leftSplitContainer.TabIndex = 0
		'
		'treeView
		'
		Me.treeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.treeView.Location = New System.Drawing.Point(0, 0)
		Me.treeView.Name = "treeView"
		Me.treeView.Size = New System.Drawing.Size(243, 184)
		Me.treeView.TabIndex = 0
		'
		'propertyGrid
		'
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.Location = New System.Drawing.Point(0, 0)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.Size = New System.Drawing.Size(243, 301)
		Me.propertyGrid.TabIndex = 0
		'
		'fcView
		'
		Me.fcView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.fcView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.fcView.FeatureColor = System.Drawing.Color.Red
		Me.fcView.Location = New System.Drawing.Point(0, 0)
		Me.fcView.Name = "fcView"
		Me.fcView.Record = Nothing
		Me.fcView.Size = New System.Drawing.Size(482, 489)
		Me.fcView.TabIndex = 0
		'
		'mainMenuStrip
		'
		Me.mainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.mainMenuStrip.Location = New System.Drawing.Point(0, 0)
		Me.mainMenuStrip.Name = "mainMenuStrip"
		Me.mainMenuStrip.Size = New System.Drawing.Size(729, 24)
		Me.mainMenuStrip.TabIndex = 1
		Me.mainMenuStrip.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.saveToolStripMenuItem, Me.toolStripSeparator4, Me.openToolStripMenuItemCbeff, Me.fileToolStripSeparator1, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'newToolStripMenuItem
		'
		Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
		Me.newToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.newToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
		Me.newToolStripMenuItem.Text = "&New ..."
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
		Me.openToolStripMenuItem.Text = "&Open ..."
		'
		'saveToolStripMenuItem
		'
		Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
		Me.saveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
		Me.saveToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
		Me.saveToolStripMenuItem.Text = "&Save ..."
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(181, 6)
		'
		'openToolStripMenuItemCbeff
		'
		Me.openToolStripMenuItemCbeff.Name = "openToolStripMenuItemCbeff"
		Me.openToolStripMenuItemCbeff.Size = New System.Drawing.Size(184, 22)
		Me.openToolStripMenuItemCbeff.Text = "&Open CbeffRecord ..."
		'
		'fileToolStripSeparator1
		'
		Me.fileToolStripSeparator1.Name = "fileToolStripSeparator1"
		Me.fileToolStripSeparator1.Size = New System.Drawing.Size(181, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'editToolStripMenuItem
		'
		Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addFaceFromImageToolStripMenuItem, Me.addFaceFromRawToolStripMenuItem, Me.toolStripSeparator1, Me.removeToolStripMenuItem, Me.toolStripSeparator2, Me.saveFaceToolStripMenuItem, Me.saveFaceAsDataToolStripMenuItem, Me.toolStripSeparator3, Me.convertToolStripMenuItem})
		Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
		Me.editToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.editToolStripMenuItem.Text = "&Edit"
		'
		'addFaceFromImageToolStripMenuItem
		'
		Me.addFaceFromImageToolStripMenuItem.Enabled = False
		Me.addFaceFromImageToolStripMenuItem.Name = "addFaceFromImageToolStripMenuItem"
		Me.addFaceFromImageToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
		Me.addFaceFromImageToolStripMenuItem.Text = "&Add face from image ..."
		'
		'addFaceFromRawToolStripMenuItem
		'
		Me.addFaceFromRawToolStripMenuItem.Enabled = False
		Me.addFaceFromRawToolStripMenuItem.Name = "addFaceFromRawToolStripMenuItem"
		Me.addFaceFromRawToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
		Me.addFaceFromRawToolStripMenuItem.Text = "A&dd face from data ..."
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(195, 6)
		'
		'removeToolStripMenuItem
		'
		Me.removeToolStripMenuItem.Enabled = False
		Me.removeToolStripMenuItem.Name = "removeToolStripMenuItem"
		Me.removeToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
		Me.removeToolStripMenuItem.Text = "&Remove face ..."
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(195, 6)
		'
		'saveFaceToolStripMenuItem
		'
		Me.saveFaceToolStripMenuItem.Enabled = False
		Me.saveFaceToolStripMenuItem.Name = "saveFaceToolStripMenuItem"
		Me.saveFaceToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
		Me.saveFaceToolStripMenuItem.Text = "&Save face as image ..."
		'
		'saveFaceAsDataToolStripMenuItem
		'
		Me.saveFaceAsDataToolStripMenuItem.Enabled = False
		Me.saveFaceAsDataToolStripMenuItem.Name = "saveFaceAsDataToolStripMenuItem"
		Me.saveFaceAsDataToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
		Me.saveFaceAsDataToolStripMenuItem.Text = "Sa&ve face as data ..."
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(195, 6)
		'
		'convertToolStripMenuItem
		'
		Me.convertToolStripMenuItem.Enabled = False
		Me.convertToolStripMenuItem.Name = "convertToolStripMenuItem"
		Me.convertToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
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
		'fcRecordSaveFileDialog
		'
		Me.fcRecordSaveFileDialog.Filter = "FCRecord Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'fcRecordOpenFileDialog
		'
		Me.fcRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'rawImageOpenFileDialog
		'
		Me.rawImageOpenFileDialog.Filter = resources.GetString("rawImageOpenFileDialog.Filter")
		'
		'saveRawFileDialog
		'
		Me.saveRawFileDialog.Filter = "JPEG Files (*.jpg;*.jpeg;*.jpe;*.jif;*.jfif;*.jfi;*.jpb;*.jpl)|*.jpg;*.jpeg;*.jpe" & _
			";*.jif;*.jfif;*.jfi;*.jpb;*.jpl|JPEG 2000 Files (*.jp2)|*.jp2"
		'
		'cbeffRecordOpenFileDialog
		'
		Me.cbeffRecordOpenFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'cbeffRecordSaveFileDialog
		'
		Me.cbeffRecordSaveFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(729, 513)
		Me.Controls.Add(Me.mainSplitContainer)
		Me.Controls.Add(Me.mainMenuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "FCRecord Editor"
		Me.mainSplitContainer.Panel1.ResumeLayout(False)
		Me.mainSplitContainer.Panel2.ResumeLayout(False)
		Me.mainSplitContainer.ResumeLayout(False)
		Me.leftSplitContainer.Panel1.ResumeLayout(False)
		Me.leftSplitContainer.Panel2.ResumeLayout(False)
		Me.leftSplitContainer.ResumeLayout(False)
		Me.mainMenuStrip.ResumeLayout(False)
		Me.mainMenuStrip.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private mainSplitContainer As System.Windows.Forms.SplitContainer
	Private leftSplitContainer As System.Windows.Forms.SplitContainer
	Private WithEvents treeView As System.Windows.Forms.TreeView
	Private propertyGrid As System.Windows.Forms.PropertyGrid
	Private Shadows mainMenuStrip As System.Windows.Forms.MenuStrip
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fcRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private fcRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fileToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private imageOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fcView As Neurotec.Biometrics.Standards.Gui.FCView
	Private WithEvents convertToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFaceFromImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFaceFromRawToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents removeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private rawImageOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents saveFaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveFaceAsDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private saveImageFileDialog As System.Windows.Forms.SaveFileDialog
	Private saveRawFileDialog As System.Windows.Forms.SaveFileDialog
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents openToolStripMenuItemCbeff As System.Windows.Forms.ToolStripMenuItem
	Private cbeffRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private cbeffRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
End Class