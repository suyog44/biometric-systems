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
		Me.newFMRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.openToolStripMenuItemCbeff = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.edToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFingerViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.removeFingerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.deleteSelectedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.activeToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.pointerToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFeatureToolToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.convertToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fmRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.fmRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.fmView = New Neurotec.Biometrics.Standards.Gui.FMView
		Me.toolStrip = New System.Windows.Forms.ToolStrip
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.btnPointerTool = New System.Windows.Forms.ToolStripButton
		Me.btnAddFeatureTool = New System.Windows.Forms.ToolStripButton
		Me.btnDeleteFeature = New System.Windows.Forms.ToolStripButton
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.splitContainer2 = New System.Windows.Forms.SplitContainer
		Me.cbeffRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.cbeffRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.mainMenuStrip.SuspendLayout()
		Me.toolStrip.SuspendLayout()
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
		Me.treeView.Size = New System.Drawing.Size(246, 271)
		Me.treeView.TabIndex = 0
		'
		'propertyGrid
		'
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.Location = New System.Drawing.Point(0, 0)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.Size = New System.Drawing.Size(246, 243)
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
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newFMRecordToolStripMenuItem, Me.openToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.toolStripSeparator3, Me.openToolStripMenuItemCbeff, Me.toolStripSeparator4, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'newFMRecordToolStripMenuItem
		'
		Me.newFMRecordToolStripMenuItem.Name = "newFMRecordToolStripMenuItem"
		Me.newFMRecordToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.newFMRecordToolStripMenuItem.Size = New System.Drawing.Size(215, 22)
		Me.newFMRecordToolStripMenuItem.Text = "&New FMRecord ..."
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.ShortcutKeyDisplayString = ""
		Me.openToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(215, 22)
		Me.openToolStripMenuItem.Text = "&Open FMRecord ..."
		'
		'saveAsToolStripMenuItem
		'
		Me.saveAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control
		Me.saveAsToolStripMenuItem.Enabled = False
		Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
		Me.saveAsToolStripMenuItem.ShortcutKeyDisplayString = ""
		Me.saveAsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
		Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(215, 22)
		Me.saveAsToolStripMenuItem.Text = "&Save FMRecord ..."
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(212, 6)
		'
		'openToolStripMenuItemCbeff
		'
		Me.openToolStripMenuItemCbeff.Name = "openToolStripMenuItemCbeff"
		Me.openToolStripMenuItemCbeff.Size = New System.Drawing.Size(215, 22)
		Me.openToolStripMenuItemCbeff.Text = "&Open CbeffRecord ..."
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(212, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(215, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'edToolStripMenuItem
		'
		Me.edToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addFingerViewToolStripMenuItem, Me.removeFingerToolStripMenuItem, Me.deleteSelectedToolStripMenuItem, Me.toolStripSeparator1, Me.activeToolToolStripMenuItem, Me.convertToolStripMenuItem})
		Me.edToolStripMenuItem.Name = "edToolStripMenuItem"
		Me.edToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.edToolStripMenuItem.Text = "&Edit"
		'
		'addFingerViewToolStripMenuItem
		'
		Me.addFingerViewToolStripMenuItem.Enabled = False
		Me.addFingerViewToolStripMenuItem.Name = "addFingerViewToolStripMenuItem"
		Me.addFingerViewToolStripMenuItem.Size = New System.Drawing.Size(298, 22)
		Me.addFingerViewToolStripMenuItem.Text = "A&dd finger view"
		'
		'removeFingerToolStripMenuItem
		'
		Me.removeFingerToolStripMenuItem.Enabled = False
		Me.removeFingerToolStripMenuItem.Name = "removeFingerToolStripMenuItem"
		Me.removeFingerToolStripMenuItem.Size = New System.Drawing.Size(298, 22)
		Me.removeFingerToolStripMenuItem.Text = "&Remove finger view"
		'
		'deleteSelectedToolStripMenuItem
		'
		Me.deleteSelectedToolStripMenuItem.Enabled = False
		Me.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem"
		Me.deleteSelectedToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
		Me.deleteSelectedToolStripMenuItem.Size = New System.Drawing.Size(298, 22)
		Me.deleteSelectedToolStripMenuItem.Text = "Delete selected minutia/core/delta"
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(295, 6)
		'
		'activeToolToolStripMenuItem
		'
		Me.activeToolToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.pointerToolToolStripMenuItem, Me.addFeatureToolToolStripMenuItem})
		Me.activeToolToolStripMenuItem.Name = "activeToolToolStripMenuItem"
		Me.activeToolToolStripMenuItem.Size = New System.Drawing.Size(298, 22)
		Me.activeToolToolStripMenuItem.Text = "Active &tool"
		'
		'pointerToolToolStripMenuItem
		'
		Me.pointerToolToolStripMenuItem.Name = "pointerToolToolStripMenuItem"
		Me.pointerToolToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
		Me.pointerToolToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
		Me.pointerToolToolStripMenuItem.Text = "&Pointer tool"
		'
		'addFeatureToolToolStripMenuItem
		'
		Me.addFeatureToolToolStripMenuItem.Name = "addFeatureToolToolStripMenuItem"
		Me.addFeatureToolToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
		Me.addFeatureToolToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
		Me.addFeatureToolToolStripMenuItem.Text = "Add &feature tool"
		'
		'convertToolStripMenuItem
		'
		Me.convertToolStripMenuItem.Enabled = False
		Me.convertToolStripMenuItem.Name = "convertToolStripMenuItem"
		Me.convertToolStripMenuItem.Size = New System.Drawing.Size(298, 22)
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
		'fmRecordOpenFileDialog
		'
		Me.fmRecordOpenFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FIRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
		 "*"
		Me.fmRecordOpenFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*"
		'
		'fmRecordSaveFileDialog
		'
		Me.fmRecordSaveFileDialog.DefaultExt = "All Supported Files (*.dat)|*.dat|FCRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
		 "*"
		Me.fmRecordSaveFileDialog.Filter = "Data files |*.dat;*.data|All files (.*)|*.*"
		'
		'cbeffRecordOpenFileDialog
		'
		Me.cbeffRecordOpenFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'cbeffRecordSaveFileDialog
		'
		Me.cbeffRecordSaveFileDialog.Filter = "Data files |*.dat;*.data;*.bin|All files (.*)|*.*"
		'
		'fmView
		'
		Me.fmView.BackColor = System.Drawing.SystemColors.ControlLight
		Me.fmView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fmView.DrawFeatureArea = True
		Me.fmView.FeatureAreaColor = System.Drawing.Color.LightGray
		Me.fmView.Location = New System.Drawing.Point(0, 0)
		Me.fmView.MinutiaColor = System.Drawing.Color.Red
		Me.fmView.Name = "fmView"
		Me.fmView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fmView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fmView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fmView.SingularPointColor = System.Drawing.Color.Red
		Me.fmView.Size = New System.Drawing.Size(694, 518)
		Me.fmView.TabIndex = 6
		Me.fmView.TreeColor = System.Drawing.Color.Crimson
		Me.fmView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fmView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fmView.TreeWidth = 2
		'
		'toolStrip
		'
		Me.toolStrip.Dock = System.Windows.Forms.DockStyle.None
		Me.toolStrip.Enabled = False
		Me.toolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripSeparator2, Me.btnPointerTool, Me.btnAddFeatureTool, Me.btnDeleteFeature})
		Me.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
		Me.toolStrip.Location = New System.Drawing.Point(243, 0)
		Me.toolStrip.Name = "toolStrip"
		Me.toolStrip.Size = New System.Drawing.Size(107, 23)
		Me.toolStrip.TabIndex = 7
		Me.toolStrip.Text = "toolStrip"
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 23)
		'
		'btnPointerTool
		'
		Me.btnPointerTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.btnPointerTool.Image = Global.Neurotec.Samples.Resources.Pointer
		Me.btnPointerTool.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.btnPointerTool.Name = "btnPointerTool"
		Me.btnPointerTool.Size = New System.Drawing.Size(23, 20)
		Me.btnPointerTool.Text = "Pointer tool - Use it to move or rotate details. (Ctrl + P)"
		'
		'btnAddFeatureTool
		'
		Me.btnAddFeatureTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.btnAddFeatureTool.Image = Global.Neurotec.Samples.Resources.AddFeatrue
		Me.btnAddFeatureTool.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.btnAddFeatureTool.Name = "btnAddFeatureTool"
		Me.btnAddFeatureTool.Size = New System.Drawing.Size(23, 20)
		Me.btnAddFeatureTool.Text = "Add Feature tool - Add new minutiae, cores or deltas. (Ctrl + F)"
		'
		'btnDeleteFeature
		'
		Me.btnDeleteFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.btnDeleteFeature.Enabled = False
		Me.btnDeleteFeature.Image = Global.Neurotec.Samples.Resources.Delete
		Me.btnDeleteFeature.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.btnDeleteFeature.Name = "btnDeleteFeature"
		Me.btnDeleteFeature.Size = New System.Drawing.Size(23, 20)
		Me.btnDeleteFeature.Text = "Delete selected - delete unwanted minutiae, cores or deltas. (Ctrl + D)"
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
		Me.splitContainer1.Panel2.Controls.Add(Me.fmView)
		Me.splitContainer1.Size = New System.Drawing.Size(944, 518)
		Me.splitContainer1.SplitterDistance = 246
		Me.splitContainer1.TabIndex = 8
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
		Me.splitContainer2.Size = New System.Drawing.Size(246, 518)
		Me.splitContainer2.SplitterDistance = 271
		Me.splitContainer2.TabIndex = 0
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(944, 542)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.toolStrip)
		Me.Controls.Add(Me.mainMenuStrip)
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "FMRecord Editor"
		Me.mainMenuStrip.ResumeLayout(False)
		Me.mainMenuStrip.PerformLayout()
		Me.toolStrip.ResumeLayout(False)
		Me.toolStrip.PerformLayout()
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
	Private fmRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fmRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents removeFingerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private activeToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents pointerToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFeatureToolToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fmView As Neurotec.Biometrics.Standards.Gui.FMView
	Private WithEvents newFMRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFingerViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStrip As System.Windows.Forms.ToolStrip
	Private WithEvents btnPointerTool As System.Windows.Forms.ToolStripButton
	Private WithEvents btnAddFeatureTool As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents btnDeleteFeature As System.Windows.Forms.ToolStripButton
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private splitContainer2 As System.Windows.Forms.SplitContainer
	Private WithEvents deleteSelectedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents openToolStripMenuItemCbeff As System.Windows.Forms.ToolStripMenuItem
	Private cbeffRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private cbeffRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
End Class