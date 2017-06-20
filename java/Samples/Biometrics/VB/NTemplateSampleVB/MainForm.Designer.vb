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
		If disposing Then
			If _template IsNot Nothing Then
				_template.Dispose()
			End If
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
		Me.fingerView = New Neurotec.Biometrics.Gui.NFingerView
		Me.applicationMenuStrip = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.fileToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFingersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFacesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addIrisesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addPalmsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addVoicesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.addFingersFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFacesFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addIrisesFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addPalmsFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addVoicesFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.addFingerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFaceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addIrisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addPalmToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addVoiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.addFingerFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addFaceFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addIrisFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addPalmFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.addVoiceFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.removeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.editToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
		Me.saveItemToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.nfRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nTemplateSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nlRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.neRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nfTemplateSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nlTemplateSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.neTemplateSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nTemplateOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.nfTemplateOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.nlTemplateOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.neTemplateOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.nfRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.nlRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.neRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.imageOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.nsTemplateSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nsRecordSaveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nsRecordOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.nsTemplateOpenFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.nViewZoomSlider = New Neurotec.Gui.NViewZoomSlider
		Me.mainSplitContainer.Panel1.SuspendLayout()
		Me.mainSplitContainer.Panel2.SuspendLayout()
		Me.mainSplitContainer.SuspendLayout()
		Me.leftSplitContainer.Panel1.SuspendLayout()
		Me.leftSplitContainer.Panel2.SuspendLayout()
		Me.leftSplitContainer.SuspendLayout()
		Me.applicationMenuStrip.SuspendLayout()
		Me.TableLayoutPanel1.SuspendLayout()
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
		Me.mainSplitContainer.Panel2.Controls.Add(Me.TableLayoutPanel1)
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
		Me.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical
		Me.propertyGrid.Size = New System.Drawing.Size(243, 301)
		Me.propertyGrid.TabIndex = 0
		Me.propertyGrid.ToolbarVisible = False
		'
		'fingerView
		'
		Me.fingerView.AutoScroll = True
		Me.fingerView.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView.BoundingRectColor = System.Drawing.Color.Red
		Me.fingerView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingerView.Location = New System.Drawing.Point(3, 3)
		Me.fingerView.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView.Name = "fingerView"
		Me.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView.ResultImageColor = System.Drawing.Color.Green
		Me.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView.Size = New System.Drawing.Size(476, 454)
		Me.fingerView.TabIndex = 0
		Me.fingerView.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView.TreeWidth = 2
		Me.fingerView.ZoomToFit = False
		'
		'applicationMenuStrip
		'
		Me.applicationMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.editToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.applicationMenuStrip.Location = New System.Drawing.Point(0, 0)
		Me.applicationMenuStrip.Name = "applicationMenuStrip"
		Me.applicationMenuStrip.Size = New System.Drawing.Size(729, 24)
		Me.applicationMenuStrip.TabIndex = 1
		Me.applicationMenuStrip.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.saveToolStripMenuItem, Me.fileToolStripSeparator1, Me.exitToolStripMenuItem})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'newToolStripMenuItem
		'
		Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
		Me.newToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
		Me.newToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
		Me.newToolStripMenuItem.Text = "&New"
		'
		'openToolStripMenuItem
		'
		Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
		Me.openToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
		Me.openToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
		Me.openToolStripMenuItem.Text = "&Open..."
		'
		'saveToolStripMenuItem
		'
		Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
		Me.saveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
		Me.saveToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
		Me.saveToolStripMenuItem.Text = "&Save..."
		'
		'fileToolStripSeparator1
		'
		Me.fileToolStripSeparator1.Name = "fileToolStripSeparator1"
		Me.fileToolStripSeparator1.Size = New System.Drawing.Size(152, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'editToolStripMenuItem
		'
		Me.editToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.addFingersToolStripMenuItem, Me.addFacesToolStripMenuItem, Me.addIrisesToolStripMenuItem, Me.addPalmsToolStripMenuItem, Me.addVoicesToolStripMenuItem, Me.editToolStripSeparator1, Me.addFingersFromFileToolStripMenuItem, Me.addFacesFromFileToolStripMenuItem, Me.addIrisesFromFileToolStripMenuItem, Me.addPalmsFromFileToolStripMenuItem, Me.addVoicesFromFileToolStripMenuItem, Me.editToolStripSeparator2, Me.addFingerToolStripMenuItem, Me.addFaceToolStripMenuItem, Me.addIrisToolStripMenuItem, Me.addPalmToolStripMenuItem, Me.addVoiceToolStripMenuItem, Me.editToolStripSeparator3, Me.addFingerFromFileToolStripMenuItem, Me.addFaceFromFileToolStripMenuItem, Me.addIrisFromFileToolStripMenuItem, Me.addPalmFromFileToolStripMenuItem, Me.addVoiceFromFileToolStripMenuItem, Me.editToolStripSeparator4, Me.removeToolStripMenuItem, Me.editToolStripSeparator5, Me.saveItemToolStripMenuItem})
		Me.editToolStripMenuItem.Name = "editToolStripMenuItem"
		Me.editToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
		Me.editToolStripMenuItem.Text = "&Edit"
		'
		'addFingersToolStripMenuItem
		'
		Me.addFingersToolStripMenuItem.Name = "addFingersToolStripMenuItem"
		Me.addFingersToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFingersToolStripMenuItem.Text = "Add fingers"
		'
		'addFacesToolStripMenuItem
		'
		Me.addFacesToolStripMenuItem.Name = "addFacesToolStripMenuItem"
		Me.addFacesToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFacesToolStripMenuItem.Text = "Add faces"
		'
		'addIrisesToolStripMenuItem
		'
		Me.addIrisesToolStripMenuItem.Name = "addIrisesToolStripMenuItem"
		Me.addIrisesToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addIrisesToolStripMenuItem.Text = "Add irises"
		'
		'addPalmsToolStripMenuItem
		'
		Me.addPalmsToolStripMenuItem.Name = "addPalmsToolStripMenuItem"
		Me.addPalmsToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addPalmsToolStripMenuItem.Text = "Add palms"
		'
		'addVoicesToolStripMenuItem
		'
		Me.addVoicesToolStripMenuItem.Name = "addVoicesToolStripMenuItem"
		Me.addVoicesToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addVoicesToolStripMenuItem.Text = "Add voices"
		'
		'editToolStripSeparator1
		'
		Me.editToolStripSeparator1.Name = "editToolStripSeparator1"
		Me.editToolStripSeparator1.Size = New System.Drawing.Size(189, 6)
		'
		'addFingersFromFileToolStripMenuItem
		'
		Me.addFingersFromFileToolStripMenuItem.Name = "addFingersFromFileToolStripMenuItem"
		Me.addFingersFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFingersFromFileToolStripMenuItem.Text = "Add fingers from file..."
		'
		'addFacesFromFileToolStripMenuItem
		'
		Me.addFacesFromFileToolStripMenuItem.Name = "addFacesFromFileToolStripMenuItem"
		Me.addFacesFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFacesFromFileToolStripMenuItem.Text = "Add faces from file..."
		'
		'addIrisesFromFileToolStripMenuItem
		'
		Me.addIrisesFromFileToolStripMenuItem.Name = "addIrisesFromFileToolStripMenuItem"
		Me.addIrisesFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addIrisesFromFileToolStripMenuItem.Text = "Add irises from file..."
		'
		'addPalmsFromFileToolStripMenuItem
		'
		Me.addPalmsFromFileToolStripMenuItem.Name = "addPalmsFromFileToolStripMenuItem"
		Me.addPalmsFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addPalmsFromFileToolStripMenuItem.Text = "Add palms from file..."
		'
		'addVoicesFromFileToolStripMenuItem
		'
		Me.addVoicesFromFileToolStripMenuItem.Name = "addVoicesFromFileToolStripMenuItem"
		Me.addVoicesFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addVoicesFromFileToolStripMenuItem.Text = "Add voices from file..."
		'
		'editToolStripSeparator2
		'
		Me.editToolStripSeparator2.Name = "editToolStripSeparator2"
		Me.editToolStripSeparator2.Size = New System.Drawing.Size(189, 6)
		'
		'addFingerToolStripMenuItem
		'
		Me.addFingerToolStripMenuItem.Name = "addFingerToolStripMenuItem"
		Me.addFingerToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFingerToolStripMenuItem.Text = "Add finger..."
		'
		'addFaceToolStripMenuItem
		'
		Me.addFaceToolStripMenuItem.Name = "addFaceToolStripMenuItem"
		Me.addFaceToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFaceToolStripMenuItem.Text = "Add face"
		'
		'addIrisToolStripMenuItem
		'
		Me.addIrisToolStripMenuItem.Name = "addIrisToolStripMenuItem"
		Me.addIrisToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addIrisToolStripMenuItem.Text = "Add iris..."
		'
		'addPalmToolStripMenuItem
		'
		Me.addPalmToolStripMenuItem.Name = "addPalmToolStripMenuItem"
		Me.addPalmToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addPalmToolStripMenuItem.Text = "Add palm..."
		'
		'addVoiceToolStripMenuItem
		'
		Me.addVoiceToolStripMenuItem.Name = "addVoiceToolStripMenuItem"
		Me.addVoiceToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addVoiceToolStripMenuItem.Text = "Add voice..."
		'
		'editToolStripSeparator3
		'
		Me.editToolStripSeparator3.Name = "editToolStripSeparator3"
		Me.editToolStripSeparator3.Size = New System.Drawing.Size(189, 6)
		'
		'addFingerFromFileToolStripMenuItem
		'
		Me.addFingerFromFileToolStripMenuItem.Name = "addFingerFromFileToolStripMenuItem"
		Me.addFingerFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFingerFromFileToolStripMenuItem.Text = "Add finger from file..."
		'
		'addFaceFromFileToolStripMenuItem
		'
		Me.addFaceFromFileToolStripMenuItem.Name = "addFaceFromFileToolStripMenuItem"
		Me.addFaceFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addFaceFromFileToolStripMenuItem.Text = "Add face from file..."
		'
		'addIrisFromFileToolStripMenuItem
		'
		Me.addIrisFromFileToolStripMenuItem.Name = "addIrisFromFileToolStripMenuItem"
		Me.addIrisFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addIrisFromFileToolStripMenuItem.Text = "Add iris from file..."
		'
		'addPalmFromFileToolStripMenuItem
		'
		Me.addPalmFromFileToolStripMenuItem.Name = "addPalmFromFileToolStripMenuItem"
		Me.addPalmFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addPalmFromFileToolStripMenuItem.Text = "Add palm from file..."
		'
		'addVoiceFromFileToolStripMenuItem
		'
		Me.addVoiceFromFileToolStripMenuItem.Name = "addVoiceFromFileToolStripMenuItem"
		Me.addVoiceFromFileToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.addVoiceFromFileToolStripMenuItem.Text = "Add voice from file..."
		'
		'editToolStripSeparator4
		'
		Me.editToolStripSeparator4.Name = "editToolStripSeparator4"
		Me.editToolStripSeparator4.Size = New System.Drawing.Size(189, 6)
		'
		'removeToolStripMenuItem
		'
		Me.removeToolStripMenuItem.Name = "removeToolStripMenuItem"
		Me.removeToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.removeToolStripMenuItem.Text = "&Remove"
		'
		'editToolStripSeparator5
		'
		Me.editToolStripSeparator5.Name = "editToolStripSeparator5"
		Me.editToolStripSeparator5.Size = New System.Drawing.Size(189, 6)
		'
		'saveItemToolStripMenuItem
		'
		Me.saveItemToolStripMenuItem.Name = "saveItemToolStripMenuItem"
		Me.saveItemToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
		Me.saveItemToolStripMenuItem.Text = "&Save item..."
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
		'nfRecordSaveFileDialog
		'
		Me.nfRecordSaveFileDialog.Filter = "NFRecord Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nTemplateSaveFileDialog
		'
		Me.nTemplateSaveFileDialog.Filter = "NTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nlRecordSaveFileDialog
		'
		Me.nlRecordSaveFileDialog.Filter = "NLRecord Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'neRecordSaveFileDialog
		'
		Me.neRecordSaveFileDialog.Filter = "NERecord Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nfTemplateSaveFileDialog
		'
		Me.nfTemplateSaveFileDialog.Filter = "NFTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nlTemplateSaveFileDialog
		'
		Me.nlTemplateSaveFileDialog.Filter = "NLTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'neTemplateSaveFileDialog
		'
		Me.neTemplateSaveFileDialog.Filter = "NETemplate Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nTemplateOpenFileDialog
		'
		Me.nTemplateOpenFileDialog.Filter = resources.GetString("nTemplateOpenFileDialog.Filter")
		'
		'nfTemplateOpenFileDialog
		'
		Me.nfTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFTemplate Files (*.dat)|*.dat|NFRecord Files (" & _
			"*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nlTemplateOpenFileDialog
		'
		Me.nlTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NLTemplate Files (*.dat)|*.dat|NLRecord Files (" & _
			"*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'neTemplateOpenFileDialog
		'
		Me.neTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NETemplate Files (*.dat)|*.dat|NERecord Files (" & _
			"*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nfRecordOpenFileDialog
		'
		Me.nfRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'nlRecordOpenFileDialog
		'
		Me.nlRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NLRecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'neRecordOpenFileDialog
		'
		Me.neRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NERecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'nsTemplateSaveFileDialog
		'
		Me.nsTemplateSaveFileDialog.Filter = "NSTemplate Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nsRecordSaveFileDialog
		'
		Me.nsRecordSaveFileDialog.Filter = "NSRecord Files (*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'nsRecordOpenFileDialog
		'
		Me.nsRecordOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NERecord Files (*.dat)|*.dat|All Files (*.*)|*." & _
			"*"
		'
		'nsTemplateOpenFileDialog
		'
		Me.nsTemplateOpenFileDialog.Filter = "All Supported Files (*.dat)|*.dat|NFTemplate Files (*.dat)|*.dat|NFRecord Files (" & _
			"*.dat)|*.dat|All Files (*.*)|*.*"
		'
		'TableLayoutPanel1
		'
		Me.TableLayoutPanel1.ColumnCount = 1
		Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.Controls.Add(Me.fingerView, 0, 0)
		Me.TableLayoutPanel1.Controls.Add(Me.nViewZoomSlider, 0, 1)
		Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
		Me.TableLayoutPanel1.RowCount = 2
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.TableLayoutPanel1.Size = New System.Drawing.Size(482, 489)
		Me.TableLayoutPanel1.TabIndex = 1
		'
		'nViewZoomSlider
		'
		Me.nViewZoomSlider.Location = New System.Drawing.Point(3, 463)
		Me.nViewZoomSlider.Name = "nViewZoomSlider"
		Me.nViewZoomSlider.Size = New System.Drawing.Size(276, 23)
		Me.nViewZoomSlider.TabIndex = 1
		Me.nViewZoomSlider.Text = "NViewZoomSlider1"
		Me.nViewZoomSlider.View = Me.fingerView
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(729, 513)
		Me.Controls.Add(Me.mainSplitContainer)
		Me.Controls.Add(Me.applicationMenuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "NTemplate Editor"
		Me.mainSplitContainer.Panel1.ResumeLayout(False)
		Me.mainSplitContainer.Panel2.ResumeLayout(False)
		Me.mainSplitContainer.ResumeLayout(False)
		Me.leftSplitContainer.Panel1.ResumeLayout(False)
		Me.leftSplitContainer.Panel2.ResumeLayout(False)
		Me.leftSplitContainer.ResumeLayout(False)
		Me.applicationMenuStrip.ResumeLayout(False)
		Me.applicationMenuStrip.PerformLayout()
		Me.TableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private mainSplitContainer As System.Windows.Forms.SplitContainer
	Private leftSplitContainer As System.Windows.Forms.SplitContainer
	Private WithEvents treeView As System.Windows.Forms.TreeView
	Private propertyGrid As System.Windows.Forms.PropertyGrid
	Private applicationMenuStrip As System.Windows.Forms.MenuStrip
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private nfRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nTemplateSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nlRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private neRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nfTemplateSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nlTemplateSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private neTemplateSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nTemplateOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private fileToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents editToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFingersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFacesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addIrisesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addPalmsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFingersFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFacesFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addIrisesFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addPalmsFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFingerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents addFingerFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addFaceFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addIrisFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addPalmFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private editToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents removeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private nfTemplateOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private nlTemplateOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private neTemplateOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private nfRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private nlRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private neRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private editToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents saveItemToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private imageOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents addFaceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addIrisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addPalmToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addVoicesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addVoicesFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addVoiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents addVoiceFromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private nsTemplateSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nsRecordSaveFileDialog As System.Windows.Forms.SaveFileDialog
	Private nsRecordOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private nsTemplateOpenFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents fingerView As Neurotec.Biometrics.Gui.NFingerView
	Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents nViewZoomSlider As Neurotec.Gui.NViewZoomSlider
End Class
