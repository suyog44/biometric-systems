Imports Microsoft.VisualBasic
Imports System
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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.menuStrip = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.closeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fileToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.saveAsNTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fileToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.changeVersionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addRecordMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.addType2RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType3RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType4RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType5RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType6RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType7RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType8RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType9RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType10RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType13RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType14RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType15RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType16RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType17RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addType99RecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
		Me.addRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addRecordToolStripDropDownButton = New System.Windows.Forms.ToolStripDropDownButton
		Me.removeRecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.clearRecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.saveRecordDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveAsNFRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.addFieldToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editFieldToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.removeFieldToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.versionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.recordTypesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.charsetsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.toolsToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.validateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.mainToolStrip = New System.Windows.Forms.ToolStrip
		Me.newToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.openToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.saveToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.mainToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.removeRecordToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.mainToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.addFieldToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.editFieldToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.removeFieldToolStripButton = New System.Windows.Forms.ToolStripButton
		Me.statusStrip = New System.Windows.Forms.StatusStrip
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.splitContainer2 = New System.Windows.Forms.SplitContainer
		Me.recordListView = New System.Windows.Forms.ListView
		Me.recordTypeColumnHeader = New System.Windows.Forms.ColumnHeader
		Me.recordNameColumnHeader = New System.Windows.Forms.ColumnHeader
		Me.recordIdcColumnHeader = New System.Windows.Forms.ColumnHeader
		Me.panel1 = New System.Windows.Forms.Panel
		Me.tabControl1 = New System.Windows.Forms.TabControl
		Me.highLevelPropertiesTabPage = New System.Windows.Forms.TabPage
		Me.noHighLevelPropertiesLabel = New System.Windows.Forms.Label
		Me.highLevelPropertyGrid = New System.Windows.Forms.PropertyGrid
		Me.tabPage2 = New System.Windows.Forms.TabPage
		Me.fieldListView = New System.Windows.Forms.ListView
		Me.fieldNumberColumnHeader = New System.Windows.Forms.ColumnHeader
		Me.fieldNameColumnHeader = New System.Windows.Forms.ColumnHeader
		Me.fieldValueColumnHeader = New System.Windows.Forms.ColumnHeader
		Me.anRecordView = New Neurotec.Biometrics.Standards.Gui.ANView
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
		Me.imageErrorToolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.recordDataSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.imageSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nfRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nTemplateSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.menuStrip.SuspendLayout()
		Me.addRecordMenu.SuspendLayout()
		Me.mainToolStrip.SuspendLayout()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.splitContainer2.Panel1.SuspendLayout()
		Me.splitContainer2.Panel2.SuspendLayout()
		Me.splitContainer2.SuspendLayout()
		Me.panel1.SuspendLayout()
		Me.tabControl1.SuspendLayout()
		Me.highLevelPropertiesTabPage.SuspendLayout()
		Me.tabPage2.SuspendLayout()
		Me.SuspendLayout()
		'
		'menuStrip
		'
		Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.toolsToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.menuStrip.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip.Name = "menuStrip"
		Me.menuStrip.Size = New System.Drawing.Size(817, 24)
		Me.menuStrip.TabIndex = 0
		Me.menuStrip.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.closeToolStripMenuItem, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.fileToolStripSeparator1, Me.saveAsNTemplateToolStripMenuItem, Me.fileToolStripSeparator2, Me.changeVersionToolStripMenuItem, Me.toolStripSeparator3, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'newToolStripMenuItem
		'
		Me.newToolStripMenuItem.Image = Global.Neurotec.Samples.My.Resources.Resources.NewDocumentHS
		Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
		Me.newToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.newToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.newToolStripMenuItem.Text = "&New"
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Image = Global.Neurotec.Samples.My.Resources.Resources.openHS
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.openToolStripMenuItem.Text = "&Open..."
		'
		'closeToolStripMenuItem
		'
		Me.closeToolStripMenuItem.Name = "closeToolStripMenuItem"
		Me.closeToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.closeToolStripMenuItem.Text = "C&lose"
		'
		'saveToolStripMenuItem
		'
		Me.saveToolStripMenuItem.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
		Me.saveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
		Me.saveToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.saveToolStripMenuItem.Text = "&Save"
		'
		'saveAsToolStripMenuItem
		'
		Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
		Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.saveAsToolStripMenuItem.Text = "Save &as..."
		'
		'fileToolStripSeparator1
		'
		Me.fileToolStripSeparator1.Name = "fileToolStripSeparator1"
		Me.fileToolStripSeparator1.Size = New System.Drawing.Size(180, 6)
		'
		'saveAsNTemplateToolStripMenuItem
		'
		Me.saveAsNTemplateToolStripMenuItem.Name = "saveAsNTemplateToolStripMenuItem"
		Me.saveAsNTemplateToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.saveAsNTemplateToolStripMenuItem.Text = "Save as NTemplate..."
		'
		'fileToolStripSeparator2
		'
		Me.fileToolStripSeparator2.Name = "fileToolStripSeparator2"
		Me.fileToolStripSeparator2.Size = New System.Drawing.Size(180, 6)
		'
		'changeVersionToolStripMenuItem
		'
		Me.changeVersionToolStripMenuItem.Name = "changeVersionToolStripMenuItem"
		Me.changeVersionToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.changeVersionToolStripMenuItem.Text = "&Change &version..."
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(180, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'editToolStripMenuItem
		'
		Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addToolStripMenuItem, Me.removeRecordsToolStripMenuItem, Me.clearRecordsToolStripMenuItem, Me.editToolStripSeparator1, Me.saveRecordDataToolStripMenuItem, Me.saveImageToolStripMenuItem, Me.saveAsNFRecordToolStripMenuItem, Me.editToolStripSeparator2, Me.addFieldToolStripMenuItem, Me.editFieldToolStripMenuItem, Me.removeFieldToolStripMenuItem})
		Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
		Me.editToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.editToolStripMenuItem.Text = "&Edit"
		'
		'addToolStripMenuItem
		'
		Me.addToolStripMenuItem.DropDown = Me.addRecordMenu
		Me.addToolStripMenuItem.Name = "addToolStripMenuItem"
		Me.addToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.addToolStripMenuItem.Text = "Add"
		'
		'addRecordMenu
		'
		Me.addRecordMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addType2RecordToolStripMenuItem, Me.addType3RecordToolStripMenuItem, Me.addType4RecordToolStripMenuItem, Me.addType5RecordToolStripMenuItem, Me.addType6RecordToolStripMenuItem, Me.addType7RecordToolStripMenuItem, Me.addType8RecordToolStripMenuItem, Me.addType9RecordToolStripMenuItem, Me.addType10RecordToolStripMenuItem, Me.addType13RecordToolStripMenuItem, Me.addType14RecordToolStripMenuItem, Me.addType15RecordToolStripMenuItem, Me.addType16RecordToolStripMenuItem, Me.addType17RecordToolStripMenuItem, Me.addType99RecordToolStripMenuItem, Me.toolStripMenuItem2, Me.addRecordToolStripMenuItem})
		Me.addRecordMenu.Name = "addRecordMenu"
		Me.addRecordMenu.OwnerItem = Me.addToolStripMenuItem
		Me.addRecordMenu.Size = New System.Drawing.Size(226, 362)
		'
		'addType2RecordToolStripMenuItem
		'
		Me.addType2RecordToolStripMenuItem.Name = "addType2RecordToolStripMenuItem"
		Me.addType2RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType2RecordToolStripMenuItem.Text = "Add type-2 record..."
		'
		'addType3RecordToolStripMenuItem
		'
		Me.addType3RecordToolStripMenuItem.Name = "addType3RecordToolStripMenuItem"
		Me.addType3RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType3RecordToolStripMenuItem.Text = "Add type-3 record..."
		'
		'addType4RecordToolStripMenuItem
		'
		Me.addType4RecordToolStripMenuItem.Name = "addType4RecordToolStripMenuItem"
		Me.addType4RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType4RecordToolStripMenuItem.Text = "Add type-4 record..."
		'
		'addType5RecordToolStripMenuItem
		'
		Me.addType5RecordToolStripMenuItem.Name = "addType5RecordToolStripMenuItem"
		Me.addType5RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType5RecordToolStripMenuItem.Text = "Add type-5 record..."
		'
		'addType6RecordToolStripMenuItem
		'
		Me.addType6RecordToolStripMenuItem.Name = "addType6RecordToolStripMenuItem"
		Me.addType6RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType6RecordToolStripMenuItem.Text = "Add type-6 record..."
		'
		'addType7RecordToolStripMenuItem
		'
		Me.addType7RecordToolStripMenuItem.Name = "addType7RecordToolStripMenuItem"
		Me.addType7RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType7RecordToolStripMenuItem.Text = "Add type-7 record..."
		'
		'addType8RecordToolStripMenuItem
		'
		Me.addType8RecordToolStripMenuItem.Name = "addType8RecordToolStripMenuItem"
		Me.addType8RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType8RecordToolStripMenuItem.Text = "Add type-8 record..."
		'
		'addType9RecordToolStripMenuItem
		'
		Me.addType9RecordToolStripMenuItem.Name = "addType9RecordToolStripMenuItem"
		Me.addType9RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType9RecordToolStripMenuItem.Text = "Add type-9 record..."
		'
		'addType10RecordToolStripMenuItem
		'
		Me.addType10RecordToolStripMenuItem.Name = "addType10RecordToolStripMenuItem"
		Me.addType10RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType10RecordToolStripMenuItem.Text = "Add type-10 record..."
		'
		'addType13RecordToolStripMenuItem
		'
		Me.addType13RecordToolStripMenuItem.Name = "addType13RecordToolStripMenuItem"
		Me.addType13RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType13RecordToolStripMenuItem.Text = "Add type-13 record..."
		'
		'addType14RecordToolStripMenuItem
		'
		Me.addType14RecordToolStripMenuItem.Name = "addType14RecordToolStripMenuItem"
		Me.addType14RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType14RecordToolStripMenuItem.Text = "Add type-14 record..."
		'
		'addType15RecordToolStripMenuItem
		'
		Me.addType15RecordToolStripMenuItem.Name = "addType15RecordToolStripMenuItem"
		Me.addType15RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType15RecordToolStripMenuItem.Text = "Add type-15 record..."
		'
		'addType16RecordToolStripMenuItem
		'
		Me.addType16RecordToolStripMenuItem.Name = "addType16RecordToolStripMenuItem"
		Me.addType16RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType16RecordToolStripMenuItem.Text = "Add type-16 record..."
		'
		'addType17RecordToolStripMenuItem
		'
		Me.addType17RecordToolStripMenuItem.Name = "addType17RecordToolStripMenuItem"
		Me.addType17RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType17RecordToolStripMenuItem.Text = "Add type-17 record..."
		'
		'addType99RecordToolStripMenuItem
		'
		Me.addType99RecordToolStripMenuItem.Name = "addType99RecordToolStripMenuItem"
		Me.addType99RecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addType99RecordToolStripMenuItem.Text = "Add type-99 record..."
		'
		'toolStripMenuItem2
		'
		Me.toolStripMenuItem2.Name = "toolStripMenuItem2"
		Me.toolStripMenuItem2.Size = New System.Drawing.Size(222, 6)
		'
		'addRecordToolStripMenuItem
		'
		Me.addRecordToolStripMenuItem.Name = "addRecordToolStripMenuItem"
		Me.addRecordToolStripMenuItem.Size = New System.Drawing.Size(225, 22)
		Me.addRecordToolStripMenuItem.Text = "Add record (not validated) ..."
		'
		'addRecordToolStripDropDownButton
		'
		Me.addRecordToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.addRecordToolStripDropDownButton.DropDown = Me.addRecordMenu
		Me.addRecordToolStripDropDownButton.Image = CType(resources.GetObject("addRecordToolStripDropDownButton.Image"), System.Drawing.Image)
		Me.addRecordToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.addRecordToolStripDropDownButton.Name = "addRecordToolStripDropDownButton"
		Me.addRecordToolStripDropDownButton.Size = New System.Drawing.Size(42, 22)
		Me.addRecordToolStripDropDownButton.Text = "Add"
		Me.addRecordToolStripDropDownButton.ToolTipText = "Add record"
		'
		'removeRecordsToolStripMenuItem
		'
		Me.removeRecordsToolStripMenuItem.Name = "removeRecordsToolStripMenuItem"
		Me.removeRecordsToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.removeRecordsToolStripMenuItem.Text = "Remove record(s)"
		'
		'clearRecordsToolStripMenuItem
		'
		Me.clearRecordsToolStripMenuItem.Name = "clearRecordsToolStripMenuItem"
		Me.clearRecordsToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.clearRecordsToolStripMenuItem.Text = "Clear records"
		'
		'editToolStripSeparator1
		'
		Me.editToolStripSeparator1.Name = "editToolStripSeparator1"
		Me.editToolStripSeparator1.Size = New System.Drawing.Size(173, 6)
		'
		'saveRecordDataToolStripMenuItem
		'
		Me.saveRecordDataToolStripMenuItem.Name = "saveRecordDataToolStripMenuItem"
		Me.saveRecordDataToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.saveRecordDataToolStripMenuItem.Text = "Save record data..."
		'
		'saveImageToolStripMenuItem
		'
		Me.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem"
		Me.saveImageToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.saveImageToolStripMenuItem.Text = "Save image..."
		'
		'saveAsNFRecordToolStripMenuItem
		'
		Me.saveAsNFRecordToolStripMenuItem.Name = "saveAsNFRecordToolStripMenuItem"
		Me.saveAsNFRecordToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.saveAsNFRecordToolStripMenuItem.Text = "Save as NFRecord..."
		'
		'editToolStripSeparator2
		'
		Me.editToolStripSeparator2.Name = "editToolStripSeparator2"
		Me.editToolStripSeparator2.Size = New System.Drawing.Size(173, 6)
		'
		'addFieldToolStripMenuItem
		'
		Me.addFieldToolStripMenuItem.Name = "addFieldToolStripMenuItem"
		Me.addFieldToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.addFieldToolStripMenuItem.Text = "Add field..."
		'
		'editFieldToolStripMenuItem
		'
		Me.editFieldToolStripMenuItem.Name = "editFieldToolStripMenuItem"
		Me.editFieldToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.editFieldToolStripMenuItem.Text = "Edit &field"
		'
		'removeFieldToolStripMenuItem
		'
		Me.removeFieldToolStripMenuItem.Name = "removeFieldToolStripMenuItem"
		Me.removeFieldToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
		Me.removeFieldToolStripMenuItem.Text = "Remove field(s)"
		'
		'toolsToolStripMenuItem
		'
		Me.toolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.versionsToolStripMenuItem, Me.recordTypesToolStripMenuItem, Me.charsetsToolStripMenuItem, Me.toolsToolStripSeparator1, Me.validateToolStripMenuItem})
		Me.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem"
		Me.toolsToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
		Me.toolsToolStripMenuItem.Text = "&Tools"
		'
		'versionsToolStripMenuItem
		'
		Me.versionsToolStripMenuItem.Name = "versionsToolStripMenuItem"
		Me.versionsToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
		Me.versionsToolStripMenuItem.Text = "&Versions"
		'
		'recordTypesToolStripMenuItem
		'
		Me.recordTypesToolStripMenuItem.Name = "recordTypesToolStripMenuItem"
		Me.recordTypesToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
		Me.recordTypesToolStripMenuItem.Text = "Record &types"
		'
		'charsetsToolStripMenuItem
		'
		Me.charsetsToolStripMenuItem.Name = "charsetsToolStripMenuItem"
		Me.charsetsToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
		Me.charsetsToolStripMenuItem.Text = "&Charsets"
		'
		'toolsToolStripSeparator1
		'
		Me.toolsToolStripSeparator1.Name = "toolsToolStripSeparator1"
		Me.toolsToolStripSeparator1.Size = New System.Drawing.Size(139, 6)
		'
		'validateToolStripMenuItem
		'
		Me.validateToolStripMenuItem.Name = "validateToolStripMenuItem"
		Me.validateToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
		Me.validateToolStripMenuItem.Text = "Va&lidate..."
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
		'mainToolStrip
		'
		Me.mainToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripButton, Me.openToolStripButton, Me.saveToolStripButton, Me.mainToolStripSeparator1, Me.addRecordToolStripDropDownButton, Me.removeRecordToolStripButton, Me.mainToolStripSeparator2, Me.addFieldToolStripButton, Me.editFieldToolStripButton, Me.removeFieldToolStripButton})
		Me.mainToolStrip.Location = New System.Drawing.Point(0, 24)
		Me.mainToolStrip.Name = "mainToolStrip"
		Me.mainToolStrip.Size = New System.Drawing.Size(817, 25)
		Me.mainToolStrip.TabIndex = 1
		Me.mainToolStrip.Text = "toolStrip1"
		'
		'newToolStripButton
		'
		Me.newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.newToolStripButton.Image = Global.Neurotec.Samples.My.Resources.Resources.NewDocumentHS
		Me.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.newToolStripButton.Name = "newToolStripButton"
		Me.newToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.newToolStripButton.Text = "New"
		'
		'openToolStripButton
		'
		Me.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.openToolStripButton.DoubleClickEnabled = True
		Me.openToolStripButton.Image = Global.Neurotec.Samples.My.Resources.Resources.openHS
		Me.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.openToolStripButton.Name = "openToolStripButton"
		Me.openToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.openToolStripButton.Text = "Open"
		'
		'saveToolStripButton
		'
		Me.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.saveToolStripButton.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.saveToolStripButton.Name = "saveToolStripButton"
		Me.saveToolStripButton.Size = New System.Drawing.Size(23, 22)
		Me.saveToolStripButton.Text = "Save"
		'
		'mainToolStripSeparator1
		'
		Me.mainToolStripSeparator1.Name = "mainToolStripSeparator1"
		Me.mainToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'removeRecordToolStripButton
		'
		Me.removeRecordToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.removeRecordToolStripButton.Image = CType(resources.GetObject("removeRecordToolStripButton.Image"), System.Drawing.Image)
		Me.removeRecordToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.removeRecordToolStripButton.Name = "removeRecordToolStripButton"
		Me.removeRecordToolStripButton.Size = New System.Drawing.Size(104, 22)
		Me.removeRecordToolStripButton.Text = "Remove record(s)"
		'
		'mainToolStripSeparator2
		'
		Me.mainToolStripSeparator2.Name = "mainToolStripSeparator2"
		Me.mainToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
		'
		'addFieldToolStripButton
		'
		Me.addFieldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.addFieldToolStripButton.Image = CType(resources.GetObject("addFieldToolStripButton.Image"), System.Drawing.Image)
		Me.addFieldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.addFieldToolStripButton.Name = "addFieldToolStripButton"
		Me.addFieldToolStripButton.Size = New System.Drawing.Size(68, 22)
		Me.addFieldToolStripButton.Text = "Add field..."
		'
		'editFieldToolStripButton
		'
		Me.editFieldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.editFieldToolStripButton.Image = CType(resources.GetObject("editFieldToolStripButton.Image"), System.Drawing.Image)
		Me.editFieldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.editFieldToolStripButton.Name = "editFieldToolStripButton"
		Me.editFieldToolStripButton.Size = New System.Drawing.Size(57, 22)
		Me.editFieldToolStripButton.Text = "Edit field"
		'
		'removeFieldToolStripButton
		'
		Me.removeFieldToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.removeFieldToolStripButton.Image = CType(resources.GetObject("removeFieldToolStripButton.Image"), System.Drawing.Image)
		Me.removeFieldToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.removeFieldToolStripButton.Name = "removeFieldToolStripButton"
		Me.removeFieldToolStripButton.Size = New System.Drawing.Size(93, 22)
		Me.removeFieldToolStripButton.Text = "Remove field(s)"
		'
		'statusStrip
		'
		Me.statusStrip.Location = New System.Drawing.Point(0, 424)
		Me.statusStrip.Name = "statusStrip"
		Me.statusStrip.Size = New System.Drawing.Size(817, 22)
		Me.statusStrip.TabIndex = 2
		Me.statusStrip.Text = "statusStrip1"
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(0, 49)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.splitContainer2)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.anRecordView)
		Me.splitContainer1.Size = New System.Drawing.Size(817, 375)
		Me.splitContainer1.SplitterDistance = 327
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
		Me.splitContainer2.Panel1.Controls.Add(Me.recordListView)
		'
		'splitContainer2.Panel2
		'
		Me.splitContainer2.Panel2.Controls.Add(Me.panel1)
		Me.splitContainer2.Size = New System.Drawing.Size(327, 375)
		Me.splitContainer2.SplitterDistance = 155
		Me.splitContainer2.TabIndex = 1
		'
		'recordListView
		'
		Me.recordListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.recordTypeColumnHeader, Me.recordNameColumnHeader, Me.recordIdcColumnHeader})
		Me.recordListView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.recordListView.FullRowSelect = True
		Me.recordListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.recordListView.HideSelection = False
		Me.recordListView.Location = New System.Drawing.Point(0, 0)
		Me.recordListView.Name = "recordListView"
		Me.recordListView.Size = New System.Drawing.Size(327, 155)
		Me.recordListView.TabIndex = 1
		Me.recordListView.UseCompatibleStateImageBehavior = False
		Me.recordListView.View = System.Windows.Forms.View.Details
		'
		'recordTypeColumnHeader
		'
		Me.recordTypeColumnHeader.Text = "Type"
		'
		'recordNameColumnHeader
		'
		Me.recordNameColumnHeader.Text = "Name"
		Me.recordNameColumnHeader.Width = 180
		'
		'recordIdcColumnHeader
		'
		Me.recordIdcColumnHeader.Text = "Idc"
		Me.recordIdcColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.recordIdcColumnHeader.Width = 50
		'
		'panel1
		'
		Me.panel1.Controls.Add(Me.tabControl1)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel1.Location = New System.Drawing.Point(0, 0)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(327, 216)
		Me.panel1.TabIndex = 0
		'
		'tabControl1
		'
		Me.tabControl1.Controls.Add(Me.highLevelPropertiesTabPage)
		Me.tabControl1.Controls.Add(Me.tabPage2)
		Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tabControl1.Location = New System.Drawing.Point(0, 0)
		Me.tabControl1.Name = "tabControl1"
		Me.tabControl1.SelectedIndex = 0
		Me.tabControl1.Size = New System.Drawing.Size(327, 216)
		Me.tabControl1.TabIndex = 2
		'
		'highLevelPropertiesTabPage
		'
		Me.highLevelPropertiesTabPage.BackColor = System.Drawing.Color.Silver
		Me.highLevelPropertiesTabPage.Controls.Add(Me.noHighLevelPropertiesLabel)
		Me.highLevelPropertiesTabPage.Controls.Add(Me.highLevelPropertyGrid)
		Me.highLevelPropertiesTabPage.Location = New System.Drawing.Point(4, 22)
		Me.highLevelPropertiesTabPage.Name = "highLevelPropertiesTabPage"
		Me.highLevelPropertiesTabPage.Padding = New System.Windows.Forms.Padding(3)
		Me.highLevelPropertiesTabPage.Size = New System.Drawing.Size(319, 190)
		Me.highLevelPropertiesTabPage.TabIndex = 0
		Me.highLevelPropertiesTabPage.Text = "High level"
		Me.highLevelPropertiesTabPage.UseVisualStyleBackColor = True
		'
		'noHighLevelPropertiesLabel
		'
		Me.noHighLevelPropertiesLabel.BackColor = System.Drawing.Color.Transparent
		Me.noHighLevelPropertiesLabel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.noHighLevelPropertiesLabel.Location = New System.Drawing.Point(3, 3)
		Me.noHighLevelPropertiesLabel.Margin = New System.Windows.Forms.Padding(10, 0, 10, 0)
		Me.noHighLevelPropertiesLabel.Name = "noHighLevelPropertiesLabel"
		Me.noHighLevelPropertiesLabel.Padding = New System.Windows.Forms.Padding(10)
		Me.noHighLevelPropertiesLabel.Size = New System.Drawing.Size(313, 184)
		Me.noHighLevelPropertiesLabel.TabIndex = 0
		Me.noHighLevelPropertiesLabel.Text = "No properties are available."
		'
		'highLevelPropertyGrid
		'
		Me.highLevelPropertyGrid.BackColor = System.Drawing.SystemColors.Control
		Me.highLevelPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.highLevelPropertyGrid.HelpVisible = False
		Me.highLevelPropertyGrid.Location = New System.Drawing.Point(3, 3)
		Me.highLevelPropertyGrid.Name = "highLevelPropertyGrid"
		Me.highLevelPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
		Me.highLevelPropertyGrid.Size = New System.Drawing.Size(313, 184)
		Me.highLevelPropertyGrid.TabIndex = 2
		Me.highLevelPropertyGrid.ToolbarVisible = False
		'
		'tabPage2
		'
		Me.tabPage2.Controls.Add(Me.fieldListView)
		Me.tabPage2.Location = New System.Drawing.Point(4, 22)
		Me.tabPage2.Name = "tabPage2"
		Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
		Me.tabPage2.Size = New System.Drawing.Size(319, 190)
		Me.tabPage2.TabIndex = 1
		Me.tabPage2.Text = "Low level"
		Me.tabPage2.UseVisualStyleBackColor = True
		'
		'fieldListView
		'
		Me.fieldListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.fieldNumberColumnHeader, Me.fieldNameColumnHeader, Me.fieldValueColumnHeader})
		Me.fieldListView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fieldListView.FullRowSelect = True
		Me.fieldListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
		Me.fieldListView.HideSelection = False
		Me.fieldListView.Location = New System.Drawing.Point(3, 3)
		Me.fieldListView.Name = "fieldListView"
		Me.fieldListView.Size = New System.Drawing.Size(313, 184)
		Me.fieldListView.TabIndex = 1
		Me.fieldListView.UseCompatibleStateImageBehavior = False
		Me.fieldListView.View = System.Windows.Forms.View.Details
		'
		'fieldNumberColumnHeader
		'
		Me.fieldNumberColumnHeader.Text = "Number"
		Me.fieldNumberColumnHeader.Width = 68
		'
		'fieldNameColumnHeader
		'
		Me.fieldNameColumnHeader.Text = "Name"
		Me.fieldNameColumnHeader.Width = 200
		'
		'fieldValueColumnHeader
		'
		Me.fieldValueColumnHeader.Text = "Value"
		Me.fieldValueColumnHeader.Width = 200
		'
		'anRecordView
		'
		Me.anRecordView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.anRecordView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.anRecordView.Location = New System.Drawing.Point(0, 0)
		Me.anRecordView.Name = "anRecordView"
		Me.anRecordView.Record = Nothing
		Me.anRecordView.Size = New System.Drawing.Size(486, 375)
		Me.anRecordView.TabIndex = 1
		'
		'openFileDialog
		'
		Me.openFileDialog.RestoreDirectory = True
		'
		'folderBrowserDialog
		'
		Me.folderBrowserDialog.ShowNewFolderButton = False
		'
		'recordDataSaveFileDialog
		'
		Me.recordDataSaveFileDialog.Filter = "All Files (*.*)|*.*"
		'
		'nfRecordSaveFileDialog
		'
		Me.nfRecordSaveFileDialog.Filter = "NFRecord Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nTemplateSaveFileDialog
		'
		Me.nTemplateSaveFileDialog.Filter = "NTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(817, 446)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.statusStrip)
		Me.Controls.Add(Me.mainToolStrip)
		Me.Controls.Add(Me.menuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "ANSI/NIST File Editor"
		Me.menuStrip.ResumeLayout(False)
		Me.menuStrip.PerformLayout()
		Me.addRecordMenu.ResumeLayout(False)
		Me.mainToolStrip.ResumeLayout(False)
		Me.mainToolStrip.PerformLayout()
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.splitContainer2.Panel1.ResumeLayout(False)
		Me.splitContainer2.Panel2.ResumeLayout(False)
		Me.splitContainer2.ResumeLayout(False)
		Me.panel1.ResumeLayout(False)
		Me.tabControl1.ResumeLayout(False)
		Me.highLevelPropertiesTabPage.ResumeLayout(False)
		Me.tabPage2.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private menuStrip As System.Windows.Forms.MenuStrip
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents recordTypesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private mainToolStrip As System.Windows.Forms.ToolStrip
	Private statusStrip As System.Windows.Forms.StatusStrip
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fileToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents newToolStripButton As System.Windows.Forms.ToolStripButton
	Private WithEvents openToolStripButton As System.Windows.Forms.ToolStripButton
	Private WithEvents saveToolStripButton As System.Windows.Forms.ToolStripButton
	Private WithEvents fieldListView As System.Windows.Forms.ListView
	Private fieldNumberColumnHeader As System.Windows.Forms.ColumnHeader
	Private fieldNameColumnHeader As System.Windows.Forms.ColumnHeader
	Private fieldValueColumnHeader As System.Windows.Forms.ColumnHeader
	Private WithEvents changeVersionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private fileToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents versionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents charsetsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolsToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents validateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
	Private editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents editFieldToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFieldToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents removeFieldToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private mainToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private mainToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFieldToolStripButton As System.Windows.Forms.ToolStripButton
	Private WithEvents editFieldToolStripButton As System.Windows.Forms.ToolStripButton
	Private WithEvents removeFieldToolStripButton As System.Windows.Forms.ToolStripButton
	Private WithEvents removeRecordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents clearRecordsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents removeRecordToolStripButton As System.Windows.Forms.ToolStripButton
	Private splitContainer2 As System.Windows.Forms.SplitContainer
	Private WithEvents recordListView As System.Windows.Forms.ListView
	Private recordTypeColumnHeader As System.Windows.Forms.ColumnHeader
	Private recordNameColumnHeader As System.Windows.Forms.ColumnHeader
	Private recordIdcColumnHeader As System.Windows.Forms.ColumnHeader
	Private imageErrorToolTip As System.Windows.Forms.ToolTip
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private anRecordView As Neurotec.Biometrics.Standards.Gui.ANView
	Private WithEvents saveRecordDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveAsNFRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private recordDataSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private imageSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nfRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents saveAsNTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private nTemplateSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private panel1 As System.Windows.Forms.Panel
	Private addRecordMenu As System.Windows.Forms.ContextMenuStrip
	Private addRecordToolStripDropDownButton As System.Windows.Forms.ToolStripDropDownButton
	Private WithEvents addType2RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addRecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private addToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType3RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private tabControl1 As System.Windows.Forms.TabControl
	Private highLevelPropertiesTabPage As System.Windows.Forms.TabPage
	Private tabPage2 As System.Windows.Forms.TabPage
	Private noHighLevelPropertiesLabel As System.Windows.Forms.Label
	Private highLevelPropertyGrid As System.Windows.Forms.PropertyGrid
	Private WithEvents addType9RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType7RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType4RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType5RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType6RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType8RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType99RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType10RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType16RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType13RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType14RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType15RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addType17RecordToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents closeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class

