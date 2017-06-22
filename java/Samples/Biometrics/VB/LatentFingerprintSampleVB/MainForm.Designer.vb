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
		Dim zoomToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.contextZoomMenuRight = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.tsmiZoomInRight = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiZoomOutRight = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiZoomOriginalRight = New System.Windows.Forms.ToolStripMenuItem
		Me.btnMatcher = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.contextZoomMenuLeft = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.zoomInToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.zoomOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.originalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.zoomToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.zoomToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
		Me.panel2 = New System.Windows.Forms.Panel
		Me.lblLatentResolution = New System.Windows.Forms.Label
		Me.lblLatentSize = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.cbInvert = New System.Windows.Forms.CheckBox
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.leftPartPanel = New System.Windows.Forms.Panel
		Me.nfViewLeft = New Neurotec.Biometrics.Gui.NFingerView
		Me.contextMenuLeft = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
		Me.tsmiDeleteFeature = New System.Windows.Forms.ToolStripMenuItem
		Me.tsLeft = New System.Windows.Forms.ToolStrip
		Me.tsbOpenLeft = New System.Windows.Forms.ToolStripButton
		Me.tsbSaveTemplate = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
		Me.tsbExtractLeft = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.tscbZoomLeft = New System.Windows.Forms.ToolStripComboBox
		Me.tsbLeftZoomIn = New System.Windows.Forms.ToolStripButton
		Me.tsbLeftZoomOut = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
		Me.tsbView = New System.Windows.Forms.ToolStripDropDownButton
		Me.tsmiViewOriginalLeft = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripSplitButton2 = New System.Windows.Forms.ToolStripDropDownButton
		Me.tsmiRotate90cw = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiRotate90ccw = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiRotate180 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
		Me.tsmiFlipHorz = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiFlipVert = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem12 = New System.Windows.Forms.ToolStripSeparator
		Me.tsmiCropToSel = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem13 = New System.Windows.Forms.ToolStripSeparator
		Me.tsmiInvertMinutiae = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem14 = New System.Windows.Forms.ToolStripSeparator
		Me.performBandpassFilteringToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.lblLeftFilename = New System.Windows.Forms.Label
		Me.rightSidePanel = New System.Windows.Forms.Panel
		Me.nfViewRight = New Neurotec.Biometrics.Gui.NFingerView
		Me.tsRight = New System.Windows.Forms.ToolStrip
		Me.tsbOpenRight = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.tscbZoomRight = New System.Windows.Forms.ToolStripComboBox
		Me.tsbRightZoomIn = New System.Windows.Forms.ToolStripButton
		Me.tsbRightZoomOut = New System.Windows.Forms.ToolStripButton
		Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
		Me.toolStripSplitButton3 = New System.Windows.Forms.ToolStripDropDownButton
		Me.tsmiViewOriginalRight = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator
		Me.lblRightFilename = New System.Windows.Forms.Label
		Me.panel3 = New System.Windows.Forms.Panel
		Me.panel1 = New System.Windows.Forms.Panel
		Me.lblBrightnessB = New System.Windows.Forms.Label
		Me.lblBrightnessG = New System.Windows.Forms.Label
		Me.lblBrightnessR = New System.Windows.Forms.Label
		Me.lblContrastBValue = New System.Windows.Forms.Label
		Me.lblContrastGValue = New System.Windows.Forms.Label
		Me.lblContrastRValue = New System.Windows.Forms.Label
		Me.btnResetAll = New System.Windows.Forms.Button
		Me.btnResetContrast = New System.Windows.Forms.Button
		Me.btnResetBrightness = New System.Windows.Forms.Button
		Me.label3 = New System.Windows.Forms.Label
		Me.cbGroupContrastSliders = New System.Windows.Forms.CheckBox
		Me.cbGrayscale = New System.Windows.Forms.CheckBox
		Me.pictureBox1 = New System.Windows.Forms.PictureBox
		Me.sliderBrightnessGreen = New Neurotec.Samples.ColorSlider
		Me.sliderContrastBlue = New Neurotec.Samples.ColorSlider
		Me.sliderBrightnessRed = New Neurotec.Samples.ColorSlider
		Me.cbGroupBrightnessSliders = New System.Windows.Forms.CheckBox
		Me.sliderContrastGreen = New Neurotec.Samples.ColorSlider
		Me.sliderBrightnessBlue = New Neurotec.Samples.ColorSlider
		Me.sliderContrastRed = New Neurotec.Samples.ColorSlider
		Me.pictureBox2 = New System.Windows.Forms.PictureBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.rbAddDoubleCoreTool = New System.Windows.Forms.RadioButton
		Me.rbAddDeltaTool = New System.Windows.Forms.RadioButton
		Me.rbAddCoreTool = New System.Windows.Forms.RadioButton
		Me.rbAddBifurcationMinutia = New System.Windows.Forms.RadioButton
		Me.rbAddEndMinutiaTool = New System.Windows.Forms.RadioButton
		Me.rbSelectAreaTool = New System.Windows.Forms.RadioButton
		Me.rbPointerTool = New System.Windows.Forms.RadioButton
		Me.label1 = New System.Windows.Forms.Label
		Me.toolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem
		Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.saveTemplateDialog = New System.Windows.Forms.SaveFileDialog
		Me.panel5 = New System.Windows.Forms.Panel
		Me.panel7 = New System.Windows.Forms.Panel
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.rbUseEditedImage = New System.Windows.Forms.RadioButton
		Me.rbUseOriginalImage = New System.Windows.Forms.RadioButton
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.matchingFarComboBox = New System.Windows.Forms.ComboBox
		Me.lblMatchingScore = New System.Windows.Forms.Label
		Me.panel6 = New System.Windows.Forms.Panel
		Me.lblReferenceResolution = New System.Windows.Forms.Label
		Me.lblReferenceSize = New System.Windows.Forms.Label
		Me.label5 = New System.Windows.Forms.Label
		Me.errorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
		Me.menuStrip1 = New System.Windows.Forms.MenuStrip
		Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiSaveTemplate = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
		Me.tsmiSaveLatentImage = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiSaveReferenceImage = New System.Windows.Forms.ToolStripMenuItem
		Me.toolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
		Me.tsmiFileExit = New System.Windows.Forms.ToolStripMenuItem
		Me.settingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiExtractionSettings = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiHelpAbout = New System.Windows.Forms.ToolStripMenuItem
		Me.saveImageDialog = New System.Windows.Forms.SaveFileDialog
		zoomToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
		Me.contextZoomMenuRight.SuspendLayout()
		Me.contextZoomMenuLeft.SuspendLayout()
		Me.panel2.SuspendLayout()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.leftPartPanel.SuspendLayout()
		Me.contextMenuLeft.SuspendLayout()
		Me.tsLeft.SuspendLayout()
		Me.rightSidePanel.SuspendLayout()
		Me.tsRight.SuspendLayout()
		Me.panel3.SuspendLayout()
		Me.panel1.SuspendLayout()
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.panel5.SuspendLayout()
		Me.panel7.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		Me.groupBox1.SuspendLayout()
		Me.panel6.SuspendLayout()
		CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.menuStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'zoomToolStripMenuItem1
		'
		zoomToolStripMenuItem1.DropDown = Me.contextZoomMenuRight
		zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1"
		zoomToolStripMenuItem1.Size = New System.Drawing.Size(116, 22)
		zoomToolStripMenuItem1.Text = "&Zoom"
		'
		'contextZoomMenuRight
		'
		Me.contextZoomMenuRight.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiZoomInRight, Me.tsmiZoomOutRight, Me.tsmiZoomOriginalRight})
		Me.contextZoomMenuRight.Name = "contextZoomMenuLeft"
		Me.contextZoomMenuRight.OwnerItem = zoomToolStripMenuItem1
		Me.contextZoomMenuRight.Size = New System.Drawing.Size(128, 70)
		'
		'tsmiZoomInRight
		'
		Me.tsmiZoomInRight.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomIn
		Me.tsmiZoomInRight.Name = "tsmiZoomInRight"
		Me.tsmiZoomInRight.Size = New System.Drawing.Size(127, 22)
		Me.tsmiZoomInRight.Text = "Zoom &in"
		'
		'tsmiZoomOutRight
		'
		Me.tsmiZoomOutRight.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomOut
		Me.tsmiZoomOutRight.Name = "tsmiZoomOutRight"
		Me.tsmiZoomOutRight.Size = New System.Drawing.Size(127, 22)
		Me.tsmiZoomOutRight.Text = "Zoom &out"
		'
		'tsmiZoomOriginalRight
		'
		Me.tsmiZoomOriginalRight.Name = "tsmiZoomOriginalRight"
		Me.tsmiZoomOriginalRight.Size = New System.Drawing.Size(127, 22)
		Me.tsmiZoomOriginalRight.Text = "&Original"
		'
		'btnMatcher
		'
		Me.btnMatcher.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnMatcher.Enabled = False
		Me.btnMatcher.Location = New System.Drawing.Point(335, 7)
		Me.btnMatcher.Name = "btnMatcher"
		Me.btnMatcher.Size = New System.Drawing.Size(91, 23)
		Me.btnMatcher.TabIndex = 0
		Me.btnMatcher.Text = "&Match"
		Me.btnMatcher.UseVisualStyleBackColor = True
		'
		'openFileDialog
		'
		Me.openFileDialog.FileName = "openFileDialog1"
		'
		'contextZoomMenuLeft
		'
		Me.contextZoomMenuLeft.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.zoomInToolStripMenuItem, Me.zoomOutToolStripMenuItem, Me.originalToolStripMenuItem})
		Me.contextZoomMenuLeft.Name = "contextZoomMenuLeft"
		Me.contextZoomMenuLeft.OwnerItem = Me.zoomToolStripMenuItem2
		Me.contextZoomMenuLeft.Size = New System.Drawing.Size(128, 70)
		'
		'zoomInToolStripMenuItem
		'
		Me.zoomInToolStripMenuItem.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomIn
		Me.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem"
		Me.zoomInToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
		Me.zoomInToolStripMenuItem.Text = "Zoom &in"
		'
		'zoomOutToolStripMenuItem
		'
		Me.zoomOutToolStripMenuItem.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomOut
		Me.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem"
		Me.zoomOutToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
		Me.zoomOutToolStripMenuItem.Text = "Zoom &out"
		'
		'originalToolStripMenuItem
		'
		Me.originalToolStripMenuItem.Name = "originalToolStripMenuItem"
		Me.originalToolStripMenuItem.Size = New System.Drawing.Size(127, 22)
		Me.originalToolStripMenuItem.Text = "&Original"
		'
		'zoomToolStripMenuItem
		'
		Me.zoomToolStripMenuItem.DropDown = Me.contextZoomMenuLeft
		Me.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem"
		Me.zoomToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
		Me.zoomToolStripMenuItem.Text = "&Zoom"
		'
		'zoomToolStripMenuItem2
		'
		Me.zoomToolStripMenuItem2.DropDown = Me.contextZoomMenuLeft
		Me.zoomToolStripMenuItem2.Name = "zoomToolStripMenuItem2"
		Me.zoomToolStripMenuItem2.Size = New System.Drawing.Size(107, 22)
		Me.zoomToolStripMenuItem2.Text = "Zoom"
		'
		'panel2
		'
		Me.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel2.Controls.Add(Me.lblLatentResolution)
		Me.panel2.Controls.Add(Me.lblLatentSize)
		Me.panel2.Controls.Add(Me.label2)
		Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
		Me.panel2.Location = New System.Drawing.Point(0, 0)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(185, 59)
		Me.panel2.TabIndex = 2
		'
		'lblLatentResolution
		'
		Me.lblLatentResolution.Location = New System.Drawing.Point(4, 37)
		Me.lblLatentResolution.Name = "lblLatentResolution"
		Me.lblLatentResolution.Size = New System.Drawing.Size(173, 18)
		Me.lblLatentResolution.TabIndex = 3
		Me.lblLatentResolution.Text = "Resolution:"
		'
		'lblLatentSize
		'
		Me.lblLatentSize.Location = New System.Drawing.Point(4, 22)
		Me.lblLatentSize.Name = "lblLatentSize"
		Me.lblLatentSize.Size = New System.Drawing.Size(173, 20)
		Me.lblLatentSize.TabIndex = 2
		Me.lblLatentSize.Text = "Size:"
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
		Me.label2.Location = New System.Drawing.Point(4, 3)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(97, 16)
		Me.label2.TabIndex = 1
		Me.label2.Text = "Latent image"
		'
		'cbInvert
		'
		Me.cbInvert.AutoSize = True
		Me.cbInvert.Location = New System.Drawing.Point(10, 20)
		Me.cbInvert.Name = "cbInvert"
		Me.cbInvert.Size = New System.Drawing.Size(52, 17)
		Me.cbInvert.TabIndex = 0
		Me.cbInvert.Text = "invert"
		Me.cbInvert.UseVisualStyleBackColor = True
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.Location = New System.Drawing.Point(88, 24)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.leftPartPanel)
		Me.splitContainer1.Panel1.Controls.Add(Me.lblLeftFilename)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.rightSidePanel)
		Me.splitContainer1.Panel2.Controls.Add(Me.lblRightFilename)
		Me.splitContainer1.Size = New System.Drawing.Size(804, 588)
		Me.splitContainer1.SplitterDistance = 390
		Me.splitContainer1.TabIndex = 22
		'
		'leftPartPanel
		'
		Me.leftPartPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.leftPartPanel.Controls.Add(Me.nfViewLeft)
		Me.leftPartPanel.Controls.Add(Me.tsLeft)
		Me.leftPartPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.leftPartPanel.Location = New System.Drawing.Point(0, 17)
		Me.leftPartPanel.Name = "leftPartPanel"
		Me.leftPartPanel.Size = New System.Drawing.Size(390, 571)
		Me.leftPartPanel.TabIndex = 0
		'
		'nfViewLeft
		'
		Me.nfViewLeft.AutomaticRotateFlipImage = False
		Me.nfViewLeft.AutoScroll = True
		Me.nfViewLeft.BackColor = System.Drawing.SystemColors.Control
		Me.nfViewLeft.BoundingRectColor = System.Drawing.Color.Red
		Me.nfViewLeft.ContextMenuStrip = Me.contextMenuLeft
		Me.nfViewLeft.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nfViewLeft.Location = New System.Drawing.Point(0, 25)
		Me.nfViewLeft.MinutiaColor = System.Drawing.Color.Red
		Me.nfViewLeft.Name = "nfViewLeft"
		Me.nfViewLeft.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.nfViewLeft.ResultImageColor = System.Drawing.Color.Chartreuse
		Me.nfViewLeft.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.nfViewLeft.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.nfViewLeft.SingularPointColor = System.Drawing.Color.Red
		Me.nfViewLeft.Size = New System.Drawing.Size(386, 542)
		Me.nfViewLeft.TabIndex = 0
		Me.nfViewLeft.Text = "nfView1"
		Me.nfViewLeft.TreeColor = System.Drawing.Color.Crimson
		Me.nfViewLeft.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.nfViewLeft.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.nfViewLeft.TreeWidth = 3
		Me.nfViewLeft.ZoomToFit = False
		'
		'contextMenuLeft
		'
		Me.contextMenuLeft.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.zoomToolStripMenuItem2, Me.toolStripSeparator4, Me.tsmiDeleteFeature})
		Me.contextMenuLeft.Name = "contextMenuLeft"
		Me.contextMenuLeft.Size = New System.Drawing.Size(108, 54)
		'
		'toolStripSeparator4
		'
		Me.toolStripSeparator4.Name = "toolStripSeparator4"
		Me.toolStripSeparator4.Size = New System.Drawing.Size(104, 6)
		'
		'tsmiDeleteFeature
		'
		Me.tsmiDeleteFeature.Name = "tsmiDeleteFeature"
		Me.tsmiDeleteFeature.Size = New System.Drawing.Size(107, 22)
		Me.tsmiDeleteFeature.Text = "Delete"
		'
		'tsLeft
		'
		Me.tsLeft.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenLeft, Me.tsbSaveTemplate, Me.toolStripSeparator3, Me.tsbExtractLeft, Me.toolStripSeparator1, Me.tscbZoomLeft, Me.tsbLeftZoomIn, Me.tsbLeftZoomOut, Me.toolStripSeparator5, Me.tsbView, Me.toolStripSplitButton2})
		Me.tsLeft.Location = New System.Drawing.Point(0, 0)
		Me.tsLeft.Name = "tsLeft"
		Me.tsLeft.Size = New System.Drawing.Size(386, 25)
		Me.tsLeft.TabIndex = 1
		'
		'tsbOpenLeft
		'
		Me.tsbOpenLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbOpenLeft.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.tsbOpenLeft.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenLeft.Name = "tsbOpenLeft"
		Me.tsbOpenLeft.Size = New System.Drawing.Size(23, 22)
		Me.tsbOpenLeft.Text = "Open"
		'
		'tsbSaveTemplate
		'
		Me.tsbSaveTemplate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbSaveTemplate.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.tsbSaveTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbSaveTemplate.Name = "tsbSaveTemplate"
		Me.tsbSaveTemplate.Size = New System.Drawing.Size(23, 22)
		Me.tsbSaveTemplate.Text = "Save template"
		'
		'toolStripSeparator3
		'
		Me.toolStripSeparator3.Name = "toolStripSeparator3"
		Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
		'
		'tsbExtractLeft
		'
		Me.tsbExtractLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbExtractLeft.Image = Global.Neurotec.Samples.My.Resources.Resources.extract
		Me.tsbExtractLeft.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbExtractLeft.Name = "tsbExtractLeft"
		Me.tsbExtractLeft.Size = New System.Drawing.Size(23, 22)
		Me.tsbExtractLeft.Text = "Extract"
		'
		'toolStripSeparator1
		'
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		'
		'tscbZoomLeft
		'
		Me.tscbZoomLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.tscbZoomLeft.Name = "tscbZoomLeft"
		Me.tscbZoomLeft.Size = New System.Drawing.Size(75, 25)
		'
		'tsbLeftZoomIn
		'
		Me.tsbLeftZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbLeftZoomIn.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomIn
		Me.tsbLeftZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbLeftZoomIn.Name = "tsbLeftZoomIn"
		Me.tsbLeftZoomIn.Size = New System.Drawing.Size(23, 22)
		Me.tsbLeftZoomIn.Text = "Zoom In"
		'
		'tsbLeftZoomOut
		'
		Me.tsbLeftZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbLeftZoomOut.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomOut
		Me.tsbLeftZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbLeftZoomOut.Name = "tsbLeftZoomOut"
		Me.tsbLeftZoomOut.Size = New System.Drawing.Size(23, 22)
		Me.tsbLeftZoomOut.Text = "Zoom Out"
		'
		'toolStripSeparator5
		'
		Me.toolStripSeparator5.Name = "toolStripSeparator5"
		Me.toolStripSeparator5.Size = New System.Drawing.Size(6, 25)
		'
		'tsbView
		'
		Me.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsbView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiViewOriginalLeft, Me.toolStripMenuItem4, Me.zoomToolStripMenuItem})
		Me.tsbView.Image = CType(resources.GetObject("tsbView.Image"), System.Drawing.Image)
		Me.tsbView.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbView.Name = "tsbView"
		Me.tsbView.Size = New System.Drawing.Size(45, 22)
		Me.tsbView.Text = "&View"
		'
		'tsmiViewOriginalLeft
		'
		Me.tsmiViewOriginalLeft.Checked = True
		Me.tsmiViewOriginalLeft.CheckState = System.Windows.Forms.CheckState.Checked
		Me.tsmiViewOriginalLeft.Name = "tsmiViewOriginalLeft"
		Me.tsmiViewOriginalLeft.Size = New System.Drawing.Size(116, 22)
		Me.tsmiViewOriginalLeft.Text = "&Original"
		'
		'toolStripMenuItem4
		'
		Me.toolStripMenuItem4.Name = "toolStripMenuItem4"
		Me.toolStripMenuItem4.Size = New System.Drawing.Size(113, 6)
		'
		'toolStripSplitButton2
		'
		Me.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.toolStripSplitButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiRotate90cw, Me.tsmiRotate90ccw, Me.tsmiRotate180, Me.toolStripMenuItem3, Me.tsmiFlipHorz, Me.tsmiFlipVert, Me.toolStripMenuItem12, Me.tsmiCropToSel, Me.toolStripMenuItem13, Me.tsmiInvertMinutiae, Me.toolStripMenuItem14, Me.performBandpassFilteringToolStripMenuItem})
		Me.toolStripSplitButton2.Image = CType(resources.GetObject("toolStripSplitButton2.Image"), System.Drawing.Image)
		Me.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripSplitButton2.Name = "toolStripSplitButton2"
		Me.toolStripSplitButton2.Size = New System.Drawing.Size(75, 22)
		Me.toolStripSplitButton2.Text = "Transform"
		'
		'tsmiRotate90cw
		'
		Me.tsmiRotate90cw.Name = "tsmiRotate90cw"
		Me.tsmiRotate90cw.Size = New System.Drawing.Size(228, 22)
		Me.tsmiRotate90cw.Text = "Rotate 90° clockwise"
		'
		'tsmiRotate90ccw
		'
		Me.tsmiRotate90ccw.Name = "tsmiRotate90ccw"
		Me.tsmiRotate90ccw.Size = New System.Drawing.Size(228, 22)
		Me.tsmiRotate90ccw.Text = "Rotate 90° counter-clockwise"
		'
		'tsmiRotate180
		'
		Me.tsmiRotate180.Name = "tsmiRotate180"
		Me.tsmiRotate180.Size = New System.Drawing.Size(228, 22)
		Me.tsmiRotate180.Text = "Rotate 180°"
		'
		'toolStripMenuItem3
		'
		Me.toolStripMenuItem3.Name = "toolStripMenuItem3"
		Me.toolStripMenuItem3.Size = New System.Drawing.Size(225, 6)
		'
		'tsmiFlipHorz
		'
		Me.tsmiFlipHorz.Name = "tsmiFlipHorz"
		Me.tsmiFlipHorz.Size = New System.Drawing.Size(228, 22)
		Me.tsmiFlipHorz.Text = "Flip Horizontally"
		'
		'tsmiFlipVert
		'
		Me.tsmiFlipVert.Name = "tsmiFlipVert"
		Me.tsmiFlipVert.Size = New System.Drawing.Size(228, 22)
		Me.tsmiFlipVert.Text = "Flip Vertically"
		'
		'toolStripMenuItem12
		'
		Me.toolStripMenuItem12.Name = "toolStripMenuItem12"
		Me.toolStripMenuItem12.Size = New System.Drawing.Size(225, 6)
		'
		'tsmiCropToSel
		'
		Me.tsmiCropToSel.Name = "tsmiCropToSel"
		Me.tsmiCropToSel.Size = New System.Drawing.Size(228, 22)
		Me.tsmiCropToSel.Text = "Crop to selection"
		'
		'toolStripMenuItem13
		'
		Me.toolStripMenuItem13.Name = "toolStripMenuItem13"
		Me.toolStripMenuItem13.Size = New System.Drawing.Size(225, 6)
		'
		'tsmiInvertMinutiae
		'
		Me.tsmiInvertMinutiae.Name = "tsmiInvertMinutiae"
		Me.tsmiInvertMinutiae.Size = New System.Drawing.Size(228, 22)
		Me.tsmiInvertMinutiae.Text = "&Invert minutiae"
		'
		'toolStripMenuItem14
		'
		Me.toolStripMenuItem14.Name = "toolStripMenuItem14"
		Me.toolStripMenuItem14.Size = New System.Drawing.Size(225, 6)
		'
		'performBandpassFilteringToolStripMenuItem
		'
		Me.performBandpassFilteringToolStripMenuItem.Name = "performBandpassFilteringToolStripMenuItem"
		Me.performBandpassFilteringToolStripMenuItem.Size = New System.Drawing.Size(228, 22)
		Me.performBandpassFilteringToolStripMenuItem.Text = "Perform Bandpass filtering"
		'
		'lblLeftFilename
		'
		Me.lblLeftFilename.AutoEllipsis = True
		Me.lblLeftFilename.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.lblLeftFilename.Dock = System.Windows.Forms.DockStyle.Top
		Me.lblLeftFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.lblLeftFilename.Location = New System.Drawing.Point(0, 0)
		Me.lblLeftFilename.Name = "lblLeftFilename"
		Me.lblLeftFilename.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
		Me.lblLeftFilename.Size = New System.Drawing.Size(390, 17)
		Me.lblLeftFilename.TabIndex = 8
		Me.lblLeftFilename.Text = "Untitled"
		Me.lblLeftFilename.UseMnemonic = False
		'
		'rightSidePanel
		'
		Me.rightSidePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.rightSidePanel.Controls.Add(Me.nfViewRight)
		Me.rightSidePanel.Controls.Add(Me.tsRight)
		Me.rightSidePanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rightSidePanel.Location = New System.Drawing.Point(0, 17)
		Me.rightSidePanel.Name = "rightSidePanel"
		Me.rightSidePanel.Size = New System.Drawing.Size(410, 571)
		Me.rightSidePanel.TabIndex = 0
		'
		'nfViewRight
		'
		Me.nfViewRight.AutomaticRotateFlipImage = False
		Me.nfViewRight.AutoScroll = True
		Me.nfViewRight.BackColor = System.Drawing.SystemColors.Control
		Me.nfViewRight.BoundingRectColor = System.Drawing.Color.Red
		Me.nfViewRight.ContextMenuStrip = Me.contextZoomMenuRight
		Me.nfViewRight.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nfViewRight.Location = New System.Drawing.Point(0, 25)
		Me.nfViewRight.MatedMinutiaIndex = 1
		Me.nfViewRight.MinutiaColor = System.Drawing.Color.Red
		Me.nfViewRight.Name = "nfViewRight"
		Me.nfViewRight.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.nfViewRight.ResultImageColor = System.Drawing.Color.Chartreuse
		Me.nfViewRight.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.nfViewRight.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.nfViewRight.SingularPointColor = System.Drawing.Color.Red
		Me.nfViewRight.Size = New System.Drawing.Size(406, 542)
		Me.nfViewRight.TabIndex = 0
		Me.nfViewRight.Text = "nfView1"
		Me.nfViewRight.TreeColor = System.Drawing.Color.Crimson
		Me.nfViewRight.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.nfViewRight.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.nfViewRight.TreeWidth = 3
		Me.nfViewRight.ZoomToFit = False
		'
		'tsRight
		'
		Me.tsRight.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenRight, Me.toolStripSeparator2, Me.tscbZoomRight, Me.tsbRightZoomIn, Me.tsbRightZoomOut, Me.toolStripSeparator6, Me.toolStripSplitButton3})
		Me.tsRight.Location = New System.Drawing.Point(0, 0)
		Me.tsRight.Name = "tsRight"
		Me.tsRight.Size = New System.Drawing.Size(406, 25)
		Me.tsRight.TabIndex = 1
		Me.tsRight.Text = "toolStrip3"
		'
		'tsbOpenRight
		'
		Me.tsbOpenRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbOpenRight.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.tsbOpenRight.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenRight.Name = "tsbOpenRight"
		Me.tsbOpenRight.Size = New System.Drawing.Size(23, 22)
		Me.tsbOpenRight.Text = "Open"
		'
		'toolStripSeparator2
		'
		Me.toolStripSeparator2.Name = "toolStripSeparator2"
		Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
		'
		'tscbZoomRight
		'
		Me.tscbZoomRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.tscbZoomRight.Name = "tscbZoomRight"
		Me.tscbZoomRight.Size = New System.Drawing.Size(75, 25)
		'
		'tsbRightZoomIn
		'
		Me.tsbRightZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbRightZoomIn.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomIn
		Me.tsbRightZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbRightZoomIn.Name = "tsbRightZoomIn"
		Me.tsbRightZoomIn.Size = New System.Drawing.Size(23, 22)
		Me.tsbRightZoomIn.Text = "Zoom In"
		'
		'tsbRightZoomOut
		'
		Me.tsbRightZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbRightZoomOut.Image = Global.Neurotec.Samples.My.Resources.Resources.ZoomOut
		Me.tsbRightZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbRightZoomOut.Name = "tsbRightZoomOut"
		Me.tsbRightZoomOut.Size = New System.Drawing.Size(23, 22)
		Me.tsbRightZoomOut.Text = "Zoom Out"
		'
		'toolStripSeparator6
		'
		Me.toolStripSeparator6.Name = "toolStripSeparator6"
		Me.toolStripSeparator6.Size = New System.Drawing.Size(6, 25)
		'
		'toolStripSplitButton3
		'
		Me.toolStripSplitButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.toolStripSplitButton3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiViewOriginalRight, Me.toolStripMenuItem5, zoomToolStripMenuItem1})
		Me.toolStripSplitButton3.Image = CType(resources.GetObject("toolStripSplitButton3.Image"), System.Drawing.Image)
		Me.toolStripSplitButton3.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripSplitButton3.Name = "toolStripSplitButton3"
		Me.toolStripSplitButton3.Size = New System.Drawing.Size(45, 22)
		Me.toolStripSplitButton3.Text = "&View"
		'
		'tsmiViewOriginalRight
		'
		Me.tsmiViewOriginalRight.Checked = True
		Me.tsmiViewOriginalRight.CheckState = System.Windows.Forms.CheckState.Checked
		Me.tsmiViewOriginalRight.Name = "tsmiViewOriginalRight"
		Me.tsmiViewOriginalRight.Size = New System.Drawing.Size(116, 22)
		Me.tsmiViewOriginalRight.Text = "&Original"
		'
		'toolStripMenuItem5
		'
		Me.toolStripMenuItem5.Name = "toolStripMenuItem5"
		Me.toolStripMenuItem5.Size = New System.Drawing.Size(113, 6)
		'
		'lblRightFilename
		'
		Me.lblRightFilename.AutoEllipsis = True
		Me.lblRightFilename.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.lblRightFilename.Dock = System.Windows.Forms.DockStyle.Top
		Me.lblRightFilename.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.lblRightFilename.Location = New System.Drawing.Point(0, 0)
		Me.lblRightFilename.Name = "lblRightFilename"
		Me.lblRightFilename.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
		Me.lblRightFilename.Size = New System.Drawing.Size(410, 17)
		Me.lblRightFilename.TabIndex = 9
		Me.lblRightFilename.Text = "Untitled"
		Me.lblRightFilename.UseMnemonic = False
		'
		'panel3
		'
		Me.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel3.Controls.Add(Me.panel1)
		Me.panel3.Controls.Add(Me.tableLayoutPanel1)
		Me.panel3.Controls.Add(Me.label1)
		Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
		Me.panel3.Location = New System.Drawing.Point(0, 24)
		Me.panel3.Name = "panel3"
		Me.panel3.Size = New System.Drawing.Size(88, 647)
		Me.panel3.TabIndex = 0
		'
		'panel1
		'
		Me.panel1.Controls.Add(Me.lblBrightnessB)
		Me.panel1.Controls.Add(Me.lblBrightnessG)
		Me.panel1.Controls.Add(Me.lblBrightnessR)
		Me.panel1.Controls.Add(Me.lblContrastBValue)
		Me.panel1.Controls.Add(Me.lblContrastGValue)
		Me.panel1.Controls.Add(Me.lblContrastRValue)
		Me.panel1.Controls.Add(Me.btnResetAll)
		Me.panel1.Controls.Add(Me.btnResetContrast)
		Me.panel1.Controls.Add(Me.btnResetBrightness)
		Me.panel1.Controls.Add(Me.label3)
		Me.panel1.Controls.Add(Me.cbGroupContrastSliders)
		Me.panel1.Controls.Add(Me.cbGrayscale)
		Me.panel1.Controls.Add(Me.cbInvert)
		Me.panel1.Controls.Add(Me.pictureBox1)
		Me.panel1.Controls.Add(Me.sliderBrightnessGreen)
		Me.panel1.Controls.Add(Me.sliderContrastBlue)
		Me.panel1.Controls.Add(Me.sliderBrightnessRed)
		Me.panel1.Controls.Add(Me.cbGroupBrightnessSliders)
		Me.panel1.Controls.Add(Me.sliderContrastGreen)
		Me.panel1.Controls.Add(Me.sliderBrightnessBlue)
		Me.panel1.Controls.Add(Me.sliderContrastRed)
		Me.panel1.Controls.Add(Me.pictureBox2)
		Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.panel1.Location = New System.Drawing.Point(0, 188)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(84, 452)
		Me.panel1.TabIndex = 17
		'
		'lblBrightnessB
		'
		Me.lblBrightnessB.Location = New System.Drawing.Point(54, 172)
		Me.lblBrightnessB.Name = "lblBrightnessB"
		Me.lblBrightnessB.Size = New System.Drawing.Size(30, 21)
		Me.lblBrightnessB.TabIndex = 26
		Me.lblBrightnessB.Text = "0"
		Me.lblBrightnessB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblBrightnessG
		'
		Me.lblBrightnessG.Location = New System.Drawing.Point(27, 172)
		Me.lblBrightnessG.Name = "lblBrightnessG"
		Me.lblBrightnessG.Size = New System.Drawing.Size(30, 21)
		Me.lblBrightnessG.TabIndex = 25
		Me.lblBrightnessG.Text = "0"
		Me.lblBrightnessG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblBrightnessR
		'
		Me.lblBrightnessR.Location = New System.Drawing.Point(-1, 172)
		Me.lblBrightnessR.Name = "lblBrightnessR"
		Me.lblBrightnessR.Size = New System.Drawing.Size(30, 21)
		Me.lblBrightnessR.TabIndex = 24
		Me.lblBrightnessR.Text = "0"
		Me.lblBrightnessR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblContrastBValue
		'
		Me.lblContrastBValue.Location = New System.Drawing.Point(53, 353)
		Me.lblContrastBValue.Name = "lblContrastBValue"
		Me.lblContrastBValue.Size = New System.Drawing.Size(30, 21)
		Me.lblContrastBValue.TabIndex = 23
		Me.lblContrastBValue.Text = "0"
		Me.lblContrastBValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblContrastGValue
		'
		Me.lblContrastGValue.Location = New System.Drawing.Point(26, 353)
		Me.lblContrastGValue.Name = "lblContrastGValue"
		Me.lblContrastGValue.Size = New System.Drawing.Size(30, 21)
		Me.lblContrastGValue.TabIndex = 22
		Me.lblContrastGValue.Text = "0"
		Me.lblContrastGValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblContrastRValue
		'
		Me.lblContrastRValue.Location = New System.Drawing.Point(-2, 353)
		Me.lblContrastRValue.Name = "lblContrastRValue"
		Me.lblContrastRValue.Size = New System.Drawing.Size(30, 21)
		Me.lblContrastRValue.TabIndex = 21
		Me.lblContrastRValue.Text = "0"
		Me.lblContrastRValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'btnResetAll
		'
		Me.btnResetAll.Location = New System.Drawing.Point(4, 424)
		Me.btnResetAll.Name = "btnResetAll"
		Me.btnResetAll.Size = New System.Drawing.Size(73, 20)
		Me.btnResetAll.TabIndex = 20
		Me.btnResetAll.Text = "reset all"
		Me.btnResetAll.UseVisualStyleBackColor = True
		'
		'btnResetContrast
		'
		Me.btnResetContrast.Location = New System.Drawing.Point(27, 225)
		Me.btnResetContrast.Name = "btnResetContrast"
		Me.btnResetContrast.Size = New System.Drawing.Size(46, 20)
		Me.btnResetContrast.TabIndex = 19
		Me.btnResetContrast.Text = "reset"
		Me.btnResetContrast.UseVisualStyleBackColor = True
		'
		'btnResetBrightness
		'
		Me.btnResetBrightness.Location = New System.Drawing.Point(29, 43)
		Me.btnResetBrightness.Name = "btnResetBrightness"
		Me.btnResetBrightness.Size = New System.Drawing.Size(46, 20)
		Me.btnResetBrightness.TabIndex = 18
		Me.btnResetBrightness.Text = "reset"
		Me.btnResetBrightness.UseVisualStyleBackColor = True
		'
		'label3
		'
		Me.label3.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.label3.Dock = System.Windows.Forms.DockStyle.Top
		Me.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.label3.Location = New System.Drawing.Point(0, 0)
		Me.label3.Name = "label3"
		Me.label3.Padding = New System.Windows.Forms.Padding(0, 2, 2, 0)
		Me.label3.Size = New System.Drawing.Size(84, 17)
		Me.label3.TabIndex = 17
		Me.label3.Text = "Colors"
		'
		'cbGroupContrastSliders
		'
		Me.cbGroupContrastSliders.AutoSize = True
		Me.cbGroupContrastSliders.Checked = True
		Me.cbGroupContrastSliders.CheckState = System.Windows.Forms.CheckState.Checked
		Me.cbGroupContrastSliders.Location = New System.Drawing.Point(14, 377)
		Me.cbGroupContrastSliders.Name = "cbGroupContrastSliders"
		Me.cbGroupContrastSliders.Size = New System.Drawing.Size(53, 17)
		Me.cbGroupContrastSliders.TabIndex = 15
		Me.cbGroupContrastSliders.Text = "group"
		Me.cbGroupContrastSliders.UseVisualStyleBackColor = True
		'
		'cbGrayscale
		'
		Me.cbGrayscale.AutoSize = True
		Me.cbGrayscale.Location = New System.Drawing.Point(10, 406)
		Me.cbGrayscale.Name = "cbGrayscale"
		Me.cbGrayscale.Size = New System.Drawing.Size(58, 17)
		Me.cbGrayscale.TabIndex = 5
		Me.cbGrayscale.Text = "to gray"
		Me.cbGrayscale.UseVisualStyleBackColor = True
		'
		'pictureBox1
		'
		Me.pictureBox1.Image = Global.Neurotec.Samples.My.Resources.Resources.brightness
		Me.pictureBox1.Location = New System.Drawing.Point(4, 43)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(17, 20)
		Me.pictureBox1.TabIndex = 3
		Me.pictureBox1.TabStop = False
		'
		'sliderBrightnessGreen
		'
		Me.sliderBrightnessGreen.BackColor = System.Drawing.Color.Transparent
		Me.sliderBrightnessGreen.BarInnerColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderBrightnessGreen.BarOuterColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderBrightnessGreen.BorderRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderBrightnessGreen.ElapsedInnerColor = System.Drawing.Color.Lime
		Me.sliderBrightnessGreen.ElapsedOuterColor = System.Drawing.Color.Green
		Me.sliderBrightnessGreen.LargeChange = CType(5UI, UInteger)
		Me.sliderBrightnessGreen.Location = New System.Drawing.Point(31, 69)
		Me.sliderBrightnessGreen.Minimum = -100
		Me.sliderBrightnessGreen.Name = "sliderBrightnessGreen"
		Me.sliderBrightnessGreen.Orientation = System.Windows.Forms.Orientation.Vertical
		Me.sliderBrightnessGreen.Size = New System.Drawing.Size(18, 100)
		Me.sliderBrightnessGreen.SmallChange = CType(1UI, UInteger)
		Me.sliderBrightnessGreen.TabIndex = 9
		Me.sliderBrightnessGreen.Tag = "1"
		Me.sliderBrightnessGreen.Text = "colorSlider2"
		Me.sliderBrightnessGreen.ThumbImage = Nothing
		Me.sliderBrightnessGreen.ThumbRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderBrightnessGreen.Value = 0
		'
		'sliderContrastBlue
		'
		Me.sliderContrastBlue.BackColor = System.Drawing.Color.Transparent
		Me.sliderContrastBlue.BarInnerColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
		Me.sliderContrastBlue.BarOuterColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.sliderContrastBlue.BorderRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderContrastBlue.ElapsedInnerColor = System.Drawing.Color.Blue
		Me.sliderContrastBlue.ElapsedOuterColor = System.Drawing.Color.Navy
		Me.sliderContrastBlue.LargeChange = CType(5UI, UInteger)
		Me.sliderContrastBlue.Location = New System.Drawing.Point(58, 251)
		Me.sliderContrastBlue.Minimum = -100
		Me.sliderContrastBlue.Name = "sliderContrastBlue"
		Me.sliderContrastBlue.Orientation = System.Windows.Forms.Orientation.Vertical
		Me.sliderContrastBlue.Size = New System.Drawing.Size(18, 100)
		Me.sliderContrastBlue.SmallChange = CType(1UI, UInteger)
		Me.sliderContrastBlue.TabIndex = 14
		Me.sliderContrastBlue.Tag = "2"
		Me.sliderContrastBlue.Text = "colorSlider3"
		Me.sliderContrastBlue.ThumbImage = Nothing
		Me.sliderContrastBlue.ThumbRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderContrastBlue.Value = 0
		'
		'sliderBrightnessRed
		'
		Me.sliderBrightnessRed.BackColor = System.Drawing.Color.Transparent
		Me.sliderBrightnessRed.BarInnerColor = System.Drawing.Color.Maroon
		Me.sliderBrightnessRed.BarOuterColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderBrightnessRed.BorderRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderBrightnessRed.ElapsedInnerColor = System.Drawing.Color.Red
		Me.sliderBrightnessRed.ElapsedOuterColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderBrightnessRed.LargeChange = CType(5UI, UInteger)
		Me.sliderBrightnessRed.Location = New System.Drawing.Point(4, 69)
		Me.sliderBrightnessRed.Minimum = -100
		Me.sliderBrightnessRed.Name = "sliderBrightnessRed"
		Me.sliderBrightnessRed.Orientation = System.Windows.Forms.Orientation.Vertical
		Me.sliderBrightnessRed.Size = New System.Drawing.Size(18, 100)
		Me.sliderBrightnessRed.SmallChange = CType(1UI, UInteger)
		Me.sliderBrightnessRed.TabIndex = 8
		Me.sliderBrightnessRed.Tag = "0"
		Me.sliderBrightnessRed.Text = "colorSlider1"
		Me.sliderBrightnessRed.ThumbImage = Nothing
		Me.sliderBrightnessRed.ThumbRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderBrightnessRed.Value = 0
		'
		'cbGroupBrightnessSliders
		'
		Me.cbGroupBrightnessSliders.AutoSize = True
		Me.cbGroupBrightnessSliders.Checked = True
		Me.cbGroupBrightnessSliders.CheckState = System.Windows.Forms.CheckState.Checked
		Me.cbGroupBrightnessSliders.Location = New System.Drawing.Point(11, 197)
		Me.cbGroupBrightnessSliders.Name = "cbGroupBrightnessSliders"
		Me.cbGroupBrightnessSliders.Size = New System.Drawing.Size(53, 17)
		Me.cbGroupBrightnessSliders.TabIndex = 11
		Me.cbGroupBrightnessSliders.Text = "group"
		Me.cbGroupBrightnessSliders.UseVisualStyleBackColor = True
		'
		'sliderContrastGreen
		'
		Me.sliderContrastGreen.BackColor = System.Drawing.Color.Transparent
		Me.sliderContrastGreen.BarInnerColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderContrastGreen.BarOuterColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderContrastGreen.BorderRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderContrastGreen.ElapsedInnerColor = System.Drawing.Color.Lime
		Me.sliderContrastGreen.ElapsedOuterColor = System.Drawing.Color.Green
		Me.sliderContrastGreen.LargeChange = CType(5UI, UInteger)
		Me.sliderContrastGreen.Location = New System.Drawing.Point(31, 251)
		Me.sliderContrastGreen.Minimum = -100
		Me.sliderContrastGreen.Name = "sliderContrastGreen"
		Me.sliderContrastGreen.Orientation = System.Windows.Forms.Orientation.Vertical
		Me.sliderContrastGreen.Size = New System.Drawing.Size(18, 100)
		Me.sliderContrastGreen.SmallChange = CType(1UI, UInteger)
		Me.sliderContrastGreen.TabIndex = 13
		Me.sliderContrastGreen.Tag = "1"
		Me.sliderContrastGreen.Text = "colorSlider2"
		Me.sliderContrastGreen.ThumbImage = Nothing
		Me.sliderContrastGreen.ThumbRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderContrastGreen.Value = 0
		'
		'sliderBrightnessBlue
		'
		Me.sliderBrightnessBlue.BackColor = System.Drawing.Color.Transparent
		Me.sliderBrightnessBlue.BarInnerColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
		Me.sliderBrightnessBlue.BarOuterColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
		Me.sliderBrightnessBlue.BorderRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderBrightnessBlue.ElapsedInnerColor = System.Drawing.Color.Blue
		Me.sliderBrightnessBlue.ElapsedOuterColor = System.Drawing.Color.Navy
		Me.sliderBrightnessBlue.LargeChange = CType(5UI, UInteger)
		Me.sliderBrightnessBlue.Location = New System.Drawing.Point(59, 69)
		Me.sliderBrightnessBlue.Minimum = -100
		Me.sliderBrightnessBlue.Name = "sliderBrightnessBlue"
		Me.sliderBrightnessBlue.Orientation = System.Windows.Forms.Orientation.Vertical
		Me.sliderBrightnessBlue.Size = New System.Drawing.Size(18, 100)
		Me.sliderBrightnessBlue.SmallChange = CType(1UI, UInteger)
		Me.sliderBrightnessBlue.TabIndex = 10
		Me.sliderBrightnessBlue.Tag = "2"
		Me.sliderBrightnessBlue.Text = "colorSlider3"
		Me.sliderBrightnessBlue.ThumbImage = Nothing
		Me.sliderBrightnessBlue.ThumbRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderBrightnessBlue.Value = 0
		'
		'sliderContrastRed
		'
		Me.sliderContrastRed.BackColor = System.Drawing.Color.Transparent
		Me.sliderContrastRed.BarInnerColor = System.Drawing.Color.Maroon
		Me.sliderContrastRed.BarOuterColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderContrastRed.BorderRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderContrastRed.ElapsedInnerColor = System.Drawing.Color.Red
		Me.sliderContrastRed.ElapsedOuterColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.sliderContrastRed.LargeChange = CType(5UI, UInteger)
		Me.sliderContrastRed.Location = New System.Drawing.Point(3, 251)
		Me.sliderContrastRed.Minimum = -100
		Me.sliderContrastRed.Name = "sliderContrastRed"
		Me.sliderContrastRed.Orientation = System.Windows.Forms.Orientation.Vertical
		Me.sliderContrastRed.Size = New System.Drawing.Size(18, 100)
		Me.sliderContrastRed.SmallChange = CType(1UI, UInteger)
		Me.sliderContrastRed.TabIndex = 12
		Me.sliderContrastRed.Tag = "0"
		Me.sliderContrastRed.Text = "colorSlider1"
		Me.sliderContrastRed.ThumbImage = Nothing
		Me.sliderContrastRed.ThumbRoundRectSize = New System.Drawing.Size(8, 8)
		Me.sliderContrastRed.Value = 0
		'
		'pictureBox2
		'
		Me.pictureBox2.Image = Global.Neurotec.Samples.My.Resources.Resources.contrast
		Me.pictureBox2.Location = New System.Drawing.Point(4, 225)
		Me.pictureBox2.Name = "pictureBox2"
		Me.pictureBox2.Size = New System.Drawing.Size(17, 20)
		Me.pictureBox2.TabIndex = 4
		Me.pictureBox2.TabStop = False
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.13636!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.86364!))
		Me.tableLayoutPanel1.Controls.Add(Me.rbAddDoubleCoreTool, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.rbAddDeltaTool, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.rbAddCoreTool, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.rbAddBifurcationMinutia, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.rbAddEndMinutiaTool, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.rbSelectAreaTool, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.rbPointerTool, 0, 0)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 17)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 4
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(84, 171)
		Me.tableLayoutPanel1.TabIndex = 18
		'
		'rbAddDoubleCoreTool
		'
		Me.rbAddDoubleCoreTool.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbAddDoubleCoreTool.AutoSize = True
		Me.rbAddDoubleCoreTool.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolDoubleCore
		Me.rbAddDoubleCoreTool.Location = New System.Drawing.Point(1, 126)
		Me.rbAddDoubleCoreTool.Margin = New System.Windows.Forms.Padding(1)
		Me.rbAddDoubleCoreTool.Name = "rbAddDoubleCoreTool"
		Me.rbAddDoubleCoreTool.Size = New System.Drawing.Size(38, 38)
		Me.rbAddDoubleCoreTool.TabIndex = 6
		Me.toolTip1.SetToolTip(Me.rbAddDoubleCoreTool, "Add Double Core")
		Me.rbAddDoubleCoreTool.UseVisualStyleBackColor = True
		'
		'rbAddDeltaTool
		'
		Me.rbAddDeltaTool.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbAddDeltaTool.AutoSize = True
		Me.rbAddDeltaTool.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolDelta
		Me.rbAddDeltaTool.Location = New System.Drawing.Point(1, 85)
		Me.rbAddDeltaTool.Margin = New System.Windows.Forms.Padding(1)
		Me.rbAddDeltaTool.Name = "rbAddDeltaTool"
		Me.rbAddDeltaTool.Size = New System.Drawing.Size(38, 38)
		Me.rbAddDeltaTool.TabIndex = 5
		Me.toolTip1.SetToolTip(Me.rbAddDeltaTool, "Add Delta")
		Me.rbAddDeltaTool.UseVisualStyleBackColor = True
		'
		'rbAddCoreTool
		'
		Me.rbAddCoreTool.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbAddCoreTool.AutoSize = True
		Me.rbAddCoreTool.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolCore
		Me.rbAddCoreTool.Location = New System.Drawing.Point(43, 85)
		Me.rbAddCoreTool.Margin = New System.Windows.Forms.Padding(1)
		Me.rbAddCoreTool.Name = "rbAddCoreTool"
		Me.rbAddCoreTool.Size = New System.Drawing.Size(38, 38)
		Me.rbAddCoreTool.TabIndex = 4
		Me.toolTip1.SetToolTip(Me.rbAddCoreTool, "Add Core")
		Me.rbAddCoreTool.UseVisualStyleBackColor = True
		'
		'rbAddBifurcationMinutia
		'
		Me.rbAddBifurcationMinutia.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbAddBifurcationMinutia.AutoSize = True
		Me.rbAddBifurcationMinutia.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolMinutiaBifurcation
		Me.rbAddBifurcationMinutia.Location = New System.Drawing.Point(43, 45)
		Me.rbAddBifurcationMinutia.Margin = New System.Windows.Forms.Padding(1)
		Me.rbAddBifurcationMinutia.Name = "rbAddBifurcationMinutia"
		Me.rbAddBifurcationMinutia.Size = New System.Drawing.Size(38, 38)
		Me.rbAddBifurcationMinutia.TabIndex = 3
		Me.toolTip1.SetToolTip(Me.rbAddBifurcationMinutia, "Add Bifurcation Minutia")
		Me.rbAddBifurcationMinutia.UseVisualStyleBackColor = True
		'
		'rbAddEndMinutiaTool
		'
		Me.rbAddEndMinutiaTool.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbAddEndMinutiaTool.AutoSize = True
		Me.rbAddEndMinutiaTool.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolMinutiaEnd
		Me.rbAddEndMinutiaTool.Location = New System.Drawing.Point(1, 45)
		Me.rbAddEndMinutiaTool.Margin = New System.Windows.Forms.Padding(1)
		Me.rbAddEndMinutiaTool.Name = "rbAddEndMinutiaTool"
		Me.rbAddEndMinutiaTool.Size = New System.Drawing.Size(38, 38)
		Me.rbAddEndMinutiaTool.TabIndex = 2
		Me.toolTip1.SetToolTip(Me.rbAddEndMinutiaTool, "Add End Minutia")
		Me.rbAddEndMinutiaTool.UseVisualStyleBackColor = True
		'
		'rbSelectAreaTool
		'
		Me.rbSelectAreaTool.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbSelectAreaTool.AutoSize = True
		Me.rbSelectAreaTool.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolAreaSelect
		Me.rbSelectAreaTool.Location = New System.Drawing.Point(43, 1)
		Me.rbSelectAreaTool.Margin = New System.Windows.Forms.Padding(1)
		Me.rbSelectAreaTool.Name = "rbSelectAreaTool"
		Me.rbSelectAreaTool.Size = New System.Drawing.Size(38, 38)
		Me.rbSelectAreaTool.TabIndex = 1
		Me.toolTip1.SetToolTip(Me.rbSelectAreaTool, "Area Select Tool")
		Me.rbSelectAreaTool.UseVisualStyleBackColor = True
		'
		'rbPointerTool
		'
		Me.rbPointerTool.Appearance = System.Windows.Forms.Appearance.Button
		Me.rbPointerTool.AutoSize = True
		Me.rbPointerTool.Checked = True
		Me.rbPointerTool.Image = Global.Neurotec.Samples.My.Resources.Resources.ToolMoveRotate
		Me.rbPointerTool.Location = New System.Drawing.Point(1, 1)
		Me.rbPointerTool.Margin = New System.Windows.Forms.Padding(1)
		Me.rbPointerTool.Name = "rbPointerTool"
		Me.rbPointerTool.Size = New System.Drawing.Size(38, 38)
		Me.rbPointerTool.TabIndex = 0
		Me.rbPointerTool.TabStop = True
		Me.toolTip1.SetToolTip(Me.rbPointerTool, "Move/Rotate Tool")
		Me.rbPointerTool.UseVisualStyleBackColor = True
		'
		'label1
		'
		Me.label1.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.label1.Dock = System.Windows.Forms.DockStyle.Top
		Me.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.label1.Location = New System.Drawing.Point(0, 0)
		Me.label1.Name = "label1"
		Me.label1.Padding = New System.Windows.Forms.Padding(0, 2, 0, 2)
		Me.label1.Size = New System.Drawing.Size(84, 17)
		Me.label1.TabIndex = 7
		Me.label1.Text = "Tools"
		'
		'toolStripMenuItem6
		'
		Me.toolStripMenuItem6.Name = "toolStripMenuItem6"
		Me.toolStripMenuItem6.Size = New System.Drawing.Size(130, 22)
		Me.toolStripMenuItem6.Text = "Zoom &in"
		'
		'toolStripMenuItem7
		'
		Me.toolStripMenuItem7.Name = "toolStripMenuItem7"
		Me.toolStripMenuItem7.Size = New System.Drawing.Size(130, 22)
		Me.toolStripMenuItem7.Text = "Zoom &out"
		'
		'toolStripMenuItem8
		'
		Me.toolStripMenuItem8.Name = "toolStripMenuItem8"
		Me.toolStripMenuItem8.Size = New System.Drawing.Size(130, 22)
		Me.toolStripMenuItem8.Text = "&Original"
		'
		'toolStripMenuItem9
		'
		Me.toolStripMenuItem9.Name = "toolStripMenuItem9"
		Me.toolStripMenuItem9.Size = New System.Drawing.Size(130, 22)
		Me.toolStripMenuItem9.Text = "Zoom &in"
		'
		'toolStripMenuItem10
		'
		Me.toolStripMenuItem10.Name = "toolStripMenuItem10"
		Me.toolStripMenuItem10.Size = New System.Drawing.Size(130, 22)
		Me.toolStripMenuItem10.Text = "Zoom &out"
		'
		'toolStripMenuItem11
		'
		Me.toolStripMenuItem11.Name = "toolStripMenuItem11"
		Me.toolStripMenuItem11.Size = New System.Drawing.Size(130, 22)
		Me.toolStripMenuItem11.Text = "&Original"
		'
		'saveTemplateDialog
		'
		Me.saveTemplateDialog.Title = "Save template file"
		'
		'panel5
		'
		Me.panel5.Controls.Add(Me.panel7)
		Me.panel5.Controls.Add(Me.panel6)
		Me.panel5.Controls.Add(Me.panel2)
		Me.panel5.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.panel5.Location = New System.Drawing.Point(88, 612)
		Me.panel5.Name = "panel5"
		Me.panel5.Size = New System.Drawing.Size(804, 59)
		Me.panel5.TabIndex = 23
		'
		'panel7
		'
		Me.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel7.Controls.Add(Me.groupBox2)
		Me.panel7.Controls.Add(Me.groupBox1)
		Me.panel7.Controls.Add(Me.btnMatcher)
		Me.panel7.Controls.Add(Me.lblMatchingScore)
		Me.panel7.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panel7.Location = New System.Drawing.Point(185, 0)
		Me.panel7.Name = "panel7"
		Me.panel7.Size = New System.Drawing.Size(434, 59)
		Me.panel7.TabIndex = 4
		'
		'groupBox2
		'
		Me.groupBox2.Controls.Add(Me.rbUseEditedImage)
		Me.groupBox2.Controls.Add(Me.rbUseOriginalImage)
		Me.groupBox2.Location = New System.Drawing.Point(3, 1)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(149, 51)
		Me.groupBox2.TabIndex = 23
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Working image:"
		'
		'rbUseEditedImage
		'
		Me.rbUseEditedImage.AutoSize = True
		Me.rbUseEditedImage.Location = New System.Drawing.Point(81, 19)
		Me.rbUseEditedImage.Name = "rbUseEditedImage"
		Me.rbUseEditedImage.Size = New System.Drawing.Size(55, 17)
		Me.rbUseEditedImage.TabIndex = 1
		Me.rbUseEditedImage.Text = "Edited"
		Me.rbUseEditedImage.UseVisualStyleBackColor = True
		'
		'rbUseOriginalImage
		'
		Me.rbUseOriginalImage.AutoSize = True
		Me.rbUseOriginalImage.Checked = True
		Me.rbUseOriginalImage.Location = New System.Drawing.Point(15, 19)
		Me.rbUseOriginalImage.Name = "rbUseOriginalImage"
		Me.rbUseOriginalImage.Size = New System.Drawing.Size(60, 17)
		Me.rbUseOriginalImage.TabIndex = 0
		Me.rbUseOriginalImage.TabStop = True
		Me.rbUseOriginalImage.Text = "Original"
		Me.rbUseOriginalImage.UseVisualStyleBackColor = True
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.matchingFarComboBox)
		Me.groupBox1.Location = New System.Drawing.Point(179, 1)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(150, 51)
		Me.groupBox1.TabIndex = 22
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Matching FAR"
		'
		'matchingFarComboBox
		'
		Me.matchingFarComboBox.FormattingEnabled = True
		Me.matchingFarComboBox.Location = New System.Drawing.Point(6, 21)
		Me.matchingFarComboBox.Name = "matchingFarComboBox"
		Me.matchingFarComboBox.Size = New System.Drawing.Size(135, 21)
		Me.matchingFarComboBox.TabIndex = 19
		'
		'lblMatchingScore
		'
		Me.lblMatchingScore.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblMatchingScore.Location = New System.Drawing.Point(338, 33)
		Me.lblMatchingScore.Name = "lblMatchingScore"
		Me.lblMatchingScore.Size = New System.Drawing.Size(89, 16)
		Me.lblMatchingScore.TabIndex = 21
		Me.lblMatchingScore.Text = "Score:"
		'
		'panel6
		'
		Me.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel6.Controls.Add(Me.lblReferenceResolution)
		Me.panel6.Controls.Add(Me.lblReferenceSize)
		Me.panel6.Controls.Add(Me.label5)
		Me.panel6.Dock = System.Windows.Forms.DockStyle.Right
		Me.panel6.Location = New System.Drawing.Point(619, 0)
		Me.panel6.Name = "panel6"
		Me.panel6.Size = New System.Drawing.Size(185, 59)
		Me.panel6.TabIndex = 3
		'
		'lblReferenceResolution
		'
		Me.lblReferenceResolution.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblReferenceResolution.Location = New System.Drawing.Point(3, 37)
		Me.lblReferenceResolution.Name = "lblReferenceResolution"
		Me.lblReferenceResolution.Size = New System.Drawing.Size(175, 20)
		Me.lblReferenceResolution.TabIndex = 6
		Me.lblReferenceResolution.Text = "Resolution:"
		'
		'lblReferenceSize
		'
		Me.lblReferenceSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblReferenceSize.Location = New System.Drawing.Point(3, 22)
		Me.lblReferenceSize.Name = "lblReferenceSize"
		Me.lblReferenceSize.Size = New System.Drawing.Size(175, 20)
		Me.lblReferenceSize.TabIndex = 5
		Me.lblReferenceSize.Text = "Size:"
		'
		'label5
		'
		Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.label5.AutoSize = True
		Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
		Me.label5.Location = New System.Drawing.Point(1, 3)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(127, 16)
		Me.label5.TabIndex = 4
		Me.label5.Text = "Reference image"
		'
		'errorProvider1
		'
		Me.errorProvider1.ContainerControl = Me
		'
		'menuStrip1
		'
		Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.settingsToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip1.Name = "menuStrip1"
		Me.menuStrip1.Size = New System.Drawing.Size(892, 24)
		Me.menuStrip1.TabIndex = 24
		Me.menuStrip1.Text = "menuStrip1"
		'
		'fileToolStripMenuItem
		'
		Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiSaveTemplate, Me.toolStripMenuItem1, Me.tsmiSaveLatentImage, Me.tsmiSaveReferenceImage, Me.toolStripMenuItem2, Me.tsmiFileExit})
		Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
		Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
		Me.fileToolStripMenuItem.Text = "&File"
		'
		'tsmiSaveTemplate
		'
		Me.tsmiSaveTemplate.Name = "tsmiSaveTemplate"
		Me.tsmiSaveTemplate.Size = New System.Drawing.Size(195, 22)
		Me.tsmiSaveTemplate.Text = "Save &template..."
		'
		'toolStripMenuItem1
		'
		Me.toolStripMenuItem1.Name = "toolStripMenuItem1"
		Me.toolStripMenuItem1.Size = New System.Drawing.Size(192, 6)
		'
		'tsmiSaveLatentImage
		'
		Me.tsmiSaveLatentImage.Name = "tsmiSaveLatentImage"
		Me.tsmiSaveLatentImage.Size = New System.Drawing.Size(195, 22)
		Me.tsmiSaveLatentImage.Text = "Save &latent image..."
		'
		'tsmiSaveReferenceImage
		'
		Me.tsmiSaveReferenceImage.Name = "tsmiSaveReferenceImage"
		Me.tsmiSaveReferenceImage.Size = New System.Drawing.Size(195, 22)
		Me.tsmiSaveReferenceImage.Text = "Save &reference image..."
		'
		'toolStripMenuItem2
		'
		Me.toolStripMenuItem2.Name = "toolStripMenuItem2"
		Me.toolStripMenuItem2.Size = New System.Drawing.Size(192, 6)
		'
		'tsmiFileExit
		'
		Me.tsmiFileExit.Name = "tsmiFileExit"
		Me.tsmiFileExit.Size = New System.Drawing.Size(195, 22)
		Me.tsmiFileExit.Text = "&Exit"
		'
		'settingsToolStripMenuItem
		'
		Me.settingsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiExtractionSettings})
		Me.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem"
		Me.settingsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
		Me.settingsToolStripMenuItem.Text = "&Settings"
		'
		'tsmiExtractionSettings
		'
		Me.tsmiExtractionSettings.Name = "tsmiExtractionSettings"
		Me.tsmiExtractionSettings.Size = New System.Drawing.Size(135, 22)
		Me.tsmiExtractionSettings.Text = "&Extraction..."
		'
		'helpToolStripMenuItem
		'
		Me.helpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiHelpAbout})
		Me.helpToolStripMenuItem.Name = "helpToolStripMenuItem"
		Me.helpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
		Me.helpToolStripMenuItem.Text = "&Help"
		'
		'tsmiHelpAbout
		'
		Me.tsmiHelpAbout.Name = "tsmiHelpAbout"
		Me.tsmiHelpAbout.Size = New System.Drawing.Size(107, 22)
		Me.tsmiHelpAbout.Text = "&About"
		'
		'saveImageDialog
		'
		Me.saveImageDialog.Title = "Save Image"
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(892, 671)
		Me.Controls.Add(Me.splitContainer1)
		Me.Controls.Add(Me.panel5)
		Me.Controls.Add(Me.panel3)
		Me.Controls.Add(Me.menuStrip1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MainMenuStrip = Me.menuStrip1
		Me.MinimumSize = New System.Drawing.Size(900, 700)
		Me.Name = "MainForm"
		Me.Text = "LatentFingerprintSample"
		Me.contextZoomMenuRight.ResumeLayout(False)
		Me.contextZoomMenuLeft.ResumeLayout(False)
		Me.panel2.ResumeLayout(False)
		Me.panel2.PerformLayout()
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.leftPartPanel.ResumeLayout(False)
		Me.leftPartPanel.PerformLayout()
		Me.contextMenuLeft.ResumeLayout(False)
		Me.tsLeft.ResumeLayout(False)
		Me.tsLeft.PerformLayout()
		Me.rightSidePanel.ResumeLayout(False)
		Me.rightSidePanel.PerformLayout()
		Me.tsRight.ResumeLayout(False)
		Me.tsRight.PerformLayout()
		Me.panel3.ResumeLayout(False)
		Me.panel1.ResumeLayout(False)
		Me.panel1.PerformLayout()
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.panel5.ResumeLayout(False)
		Me.panel7.ResumeLayout(False)
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox2.PerformLayout()
		Me.groupBox1.ResumeLayout(False)
		Me.panel6.ResumeLayout(False)
		Me.panel6.PerformLayout()
		CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.menuStrip1.ResumeLayout(False)
		Me.menuStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private WithEvents btnMatcher As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private panel2 As System.Windows.Forms.Panel
	Private WithEvents cbInvert As System.Windows.Forms.CheckBox
	Private contextZoomMenuLeft As System.Windows.Forms.ContextMenuStrip
	Private WithEvents zoomInToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents zoomOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents originalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private leftPartPanel As System.Windows.Forms.Panel
	Private WithEvents nfViewLeft As Neurotec.Biometrics.Gui.NFingerView
	Private tsLeft As System.Windows.Forms.ToolStrip
	Private rightSidePanel As System.Windows.Forms.Panel
	Private WithEvents nfViewRight As Neurotec.Biometrics.Gui.NFingerView
	Private panel3 As System.Windows.Forms.Panel
	Private WithEvents tsbOpenLeft As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbExtractLeft As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private tsRight As System.Windows.Forms.ToolStrip
	Private WithEvents tsbOpenRight As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private contextZoomMenuRight As System.Windows.Forms.ContextMenuStrip
	Private WithEvents tsmiZoomInRight As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiZoomOutRight As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiZoomOriginalRight As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSplitButton3 As System.Windows.Forms.ToolStripDropDownButton
	Private WithEvents tsmiViewOriginalRight As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem5 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents toolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents toolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents toolStripMenuItem8 As System.Windows.Forms.ToolStripMenuItem
	Private tsbView As System.Windows.Forms.ToolStripDropDownButton
	Private WithEvents tsmiViewOriginalLeft As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
	Private zoomToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSplitButton2 As System.Windows.Forms.ToolStripDropDownButton
	Private WithEvents tsmiRotate90cw As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiRotate90ccw As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiRotate180 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsmiFlipHorz As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiFlipVert As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents toolStripMenuItem9 As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents toolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents toolStripMenuItem11 As System.Windows.Forms.ToolStripMenuItem
	Private pictureBox1 As System.Windows.Forms.PictureBox
	Private pictureBox2 As System.Windows.Forms.PictureBox
	Private WithEvents cbGrayscale As System.Windows.Forms.CheckBox
	Private toolStripMenuItem12 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsmiCropToSel As System.Windows.Forms.ToolStripMenuItem
	Private toolTip1 As System.Windows.Forms.ToolTip
	Private label1 As System.Windows.Forms.Label
	Private WithEvents sliderBrightnessRed As ColorSlider
	Private WithEvents sliderBrightnessBlue As ColorSlider
	Private WithEvents sliderBrightnessGreen As ColorSlider
	Private cbGroupBrightnessSliders As System.Windows.Forms.CheckBox
	Private cbGroupContrastSliders As System.Windows.Forms.CheckBox
	Private WithEvents sliderContrastBlue As ColorSlider
	Private WithEvents sliderContrastGreen As ColorSlider
	Private WithEvents sliderContrastRed As ColorSlider
	Private label2 As System.Windows.Forms.Label
	Private lblLatentResolution As System.Windows.Forms.Label
	Private lblLatentSize As System.Windows.Forms.Label
	Private panel1 As System.Windows.Forms.Panel
	Private label3 As System.Windows.Forms.Label
	Private WithEvents contextMenuLeft As System.Windows.Forms.ContextMenuStrip
	Private zoomToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsmiDeleteFeature As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsbSaveTemplate As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
	Private saveTemplateDialog As System.Windows.Forms.SaveFileDialog
	Private panel5 As System.Windows.Forms.Panel
	Private panel6 As System.Windows.Forms.Panel
	Private lblReferenceResolution As System.Windows.Forms.Label
	Private lblReferenceSize As System.Windows.Forms.Label
	Private label5 As System.Windows.Forms.Label
	Private panel7 As System.Windows.Forms.Panel
	Private WithEvents matchingFarComboBox As System.Windows.Forms.ComboBox
	Private lblMatchingScore As System.Windows.Forms.Label
	Private errorProvider1 As System.Windows.Forms.ErrorProvider
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private menuStrip1 As System.Windows.Forms.MenuStrip
	Private settingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiExtractionSettings As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents btnResetAll As System.Windows.Forms.Button
	Private WithEvents btnResetContrast As System.Windows.Forms.Button
	Private WithEvents btnResetBrightness As System.Windows.Forms.Button
	Private lblLeftFilename As System.Windows.Forms.Label
	Private lblRightFilename As System.Windows.Forms.Label
	Private WithEvents tsbLeftZoomIn As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbLeftZoomOut As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsbRightZoomIn As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbRightZoomOut As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private rbUseOriginalImage As System.Windows.Forms.RadioButton
	Private rbUseEditedImage As System.Windows.Forms.RadioButton
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents rbPointerTool As System.Windows.Forms.RadioButton
	Private WithEvents rbSelectAreaTool As System.Windows.Forms.RadioButton
	Private WithEvents rbAddDoubleCoreTool As System.Windows.Forms.RadioButton
	Private WithEvents rbAddDeltaTool As System.Windows.Forms.RadioButton
	Private WithEvents rbAddCoreTool As System.Windows.Forms.RadioButton
	Private WithEvents rbAddBifurcationMinutia As System.Windows.Forms.RadioButton
	Private WithEvents rbAddEndMinutiaTool As System.Windows.Forms.RadioButton
	Private lblContrastRValue As System.Windows.Forms.Label
	Private lblContrastBValue As System.Windows.Forms.Label
	Private lblContrastGValue As System.Windows.Forms.Label
	Private lblBrightnessB As System.Windows.Forms.Label
	Private lblBrightnessG As System.Windows.Forms.Label
	Private lblBrightnessR As System.Windows.Forms.Label
	Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiSaveTemplate As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsmiSaveLatentImage As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiSaveReferenceImage As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsmiFileExit As System.Windows.Forms.ToolStripMenuItem
	Private saveImageDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents tscbZoomLeft As System.Windows.Forms.ToolStripComboBox
	Private WithEvents tscbZoomRight As System.Windows.Forms.ToolStripComboBox
	Private toolStripMenuItem13 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsmiInvertMinutiae As System.Windows.Forms.ToolStripMenuItem
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents tsmiHelpAbout As System.Windows.Forms.ToolStripMenuItem
	Private toolStripMenuItem14 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents performBandpassFilteringToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class

