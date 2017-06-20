Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Samples.Controls

Namespace Forms
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
			Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
			Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
			Me.menuStrip1 = New System.Windows.Forms.MenuStrip
			Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
			Me.exportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.saveTemplateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.saveImagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
			Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.optionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.changeScannerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.optionsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
			Me.editRequiredInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
			Me.splitContainer = New System.Windows.Forms.SplitContainer
			Me.groupBox1 = New System.Windows.Forms.GroupBox
			Me.chbCapturePlainFingers = New System.Windows.Forms.CheckBox
			Me.chbCaptureSlaps = New System.Windows.Forms.CheckBox
			Me.btnStartCapturing = New System.Windows.Forms.Button
			Me.chbCaptureRolled = New System.Windows.Forms.CheckBox
			Me.gbFingerSelector = New System.Windows.Forms.GroupBox
			Me.fSelector = New Neurotec.Samples.Controls.FingerSelector
			Me.tabControl = New System.Windows.Forms.TabControl
			Me.tabSlaps = New System.Windows.Forms.TabPage
			Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
			Me.panel1 = New System.Windows.Forms.Panel
			Me.nfvLeftFour = New Neurotec.Biometrics.Gui.NFingerView
			Me.toolStripViewControls = New System.Windows.Forms.ToolStrip
			Me.tsbSaveImage = New System.Windows.Forms.ToolStripButton
			Me.tsbSaveRecord = New System.Windows.Forms.ToolStripButton
			Me.label3 = New System.Windows.Forms.Label
			Me.label2 = New System.Windows.Forms.Label
			Me.label1 = New System.Windows.Forms.Label
			Me.panel2 = New System.Windows.Forms.Panel
			Me.nfvThumbs = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel3 = New System.Windows.Forms.Panel
			Me.nfvRightFour = New Neurotec.Biometrics.Gui.NFingerView
			Me.tabFingers = New System.Windows.Forms.TabPage
			Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
			Me.label8 = New System.Windows.Forms.Label
			Me.label7 = New System.Windows.Forms.Label
			Me.label6 = New System.Windows.Forms.Label
			Me.label5 = New System.Windows.Forms.Label
			Me.label4 = New System.Windows.Forms.Label
			Me.label9 = New System.Windows.Forms.Label
			Me.label10 = New System.Windows.Forms.Label
			Me.label11 = New System.Windows.Forms.Label
			Me.label12 = New System.Windows.Forms.Label
			Me.label13 = New System.Windows.Forms.Label
			Me.chbShowOriginal = New System.Windows.Forms.CheckBox
			Me.panel4 = New System.Windows.Forms.Panel
			Me.nfvLeftThumb = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel5 = New System.Windows.Forms.Panel
			Me.nfvLeftIndex = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel6 = New System.Windows.Forms.Panel
			Me.nfvLeftMiddle = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel7 = New System.Windows.Forms.Panel
			Me.nfvLeftRing = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel8 = New System.Windows.Forms.Panel
			Me.nfvLeftLittle = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel9 = New System.Windows.Forms.Panel
			Me.nfvRightThumb = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel10 = New System.Windows.Forms.Panel
			Me.nfvRightIndex = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel11 = New System.Windows.Forms.Panel
			Me.nfvRightMiddle = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel12 = New System.Windows.Forms.Panel
			Me.nfvRightRing = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel13 = New System.Windows.Forms.Panel
			Me.nfvRightLittle = New Neurotec.Biometrics.Gui.NFingerView
			Me.tabRolled = New System.Windows.Forms.TabPage
			Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
			Me.label14 = New System.Windows.Forms.Label
			Me.label15 = New System.Windows.Forms.Label
			Me.label16 = New System.Windows.Forms.Label
			Me.label17 = New System.Windows.Forms.Label
			Me.label18 = New System.Windows.Forms.Label
			Me.label19 = New System.Windows.Forms.Label
			Me.label20 = New System.Windows.Forms.Label
			Me.label21 = New System.Windows.Forms.Label
			Me.label22 = New System.Windows.Forms.Label
			Me.label23 = New System.Windows.Forms.Label
			Me.chbShowOriginalRolled = New System.Windows.Forms.CheckBox
			Me.panel14 = New System.Windows.Forms.Panel
			Me.nfvLeftThumbRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel15 = New System.Windows.Forms.Panel
			Me.nfvLeftIndexRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel16 = New System.Windows.Forms.Panel
			Me.nfvLeftMiddleRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel17 = New System.Windows.Forms.Panel
			Me.nfvLeftRingRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel18 = New System.Windows.Forms.Panel
			Me.nfvLeftLittleRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel19 = New System.Windows.Forms.Panel
			Me.nfvRightThumbRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel20 = New System.Windows.Forms.Panel
			Me.nfvRightIndexRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel21 = New System.Windows.Forms.Panel
			Me.nfvRightMiddleRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel22 = New System.Windows.Forms.Panel
			Me.nfvRightRingRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.panel23 = New System.Windows.Forms.Panel
			Me.nfvRightLittleRolled = New Neurotec.Biometrics.Gui.NFingerView
			Me.tabInformation = New System.Windows.Forms.TabPage
			Me.infoPanel = New Neurotec.Samples.Controls.InfoPanel
			Me.menuStrip1.SuspendLayout()
			Me.splitContainer.Panel1.SuspendLayout()
			Me.splitContainer.Panel2.SuspendLayout()
			Me.splitContainer.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.gbFingerSelector.SuspendLayout()
			Me.tabControl.SuspendLayout()
			Me.tabSlaps.SuspendLayout()
			Me.tableLayoutPanel1.SuspendLayout()
			Me.panel1.SuspendLayout()
			Me.toolStripViewControls.SuspendLayout()
			Me.panel2.SuspendLayout()
			Me.panel3.SuspendLayout()
			Me.tabFingers.SuspendLayout()
			Me.tableLayoutPanel2.SuspendLayout()
			Me.panel4.SuspendLayout()
			Me.panel5.SuspendLayout()
			Me.panel6.SuspendLayout()
			Me.panel7.SuspendLayout()
			Me.panel8.SuspendLayout()
			Me.panel9.SuspendLayout()
			Me.panel10.SuspendLayout()
			Me.panel11.SuspendLayout()
			Me.panel12.SuspendLayout()
			Me.panel13.SuspendLayout()
			Me.tabRolled.SuspendLayout()
			Me.tableLayoutPanel3.SuspendLayout()
			Me.panel14.SuspendLayout()
			Me.panel15.SuspendLayout()
			Me.panel16.SuspendLayout()
			Me.panel17.SuspendLayout()
			Me.panel18.SuspendLayout()
			Me.panel19.SuspendLayout()
			Me.panel20.SuspendLayout()
			Me.panel21.SuspendLayout()
			Me.panel22.SuspendLayout()
			Me.panel23.SuspendLayout()
			Me.tabInformation.SuspendLayout()
			Me.SuspendLayout()
			'
			'menuStrip1
			'
			Me.menuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem, Me.optionsToolStripMenuItem, Me.helpToolStripMenuItem})
			Me.menuStrip1.Location = New System.Drawing.Point(0, 0)
			Me.menuStrip1.Name = "menuStrip1"
			Me.menuStrip1.Size = New System.Drawing.Size(1000, 24)
			Me.menuStrip1.TabIndex = 2
			Me.menuStrip1.Text = "menuStrip1"
			'
			'fileToolStripMenuItem
			'
			Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.toolStripSeparator2, Me.exportToolStripMenuItem, Me.saveTemplateToolStripMenuItem, Me.saveImagesToolStripMenuItem, Me.toolStripSeparator1, Me.exitToolStripMenuItem})
			Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
			Me.fileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
			Me.fileToolStripMenuItem.Text = "&File"
			'
			'newToolStripMenuItem
			'
			Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
			Me.newToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
			Me.newToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.newToolStripMenuItem.Text = "&New"
			'
			'toolStripSeparator2
			'
			Me.toolStripSeparator2.Name = "toolStripSeparator2"
			Me.toolStripSeparator2.Size = New System.Drawing.Size(149, 6)
			'
			'exportToolStripMenuItem
			'
			Me.exportToolStripMenuItem.Name = "exportToolStripMenuItem"
			Me.exportToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.exportToolStripMenuItem.Text = "&Save All"
			'
			'saveTemplateToolStripMenuItem
			'
			Me.saveTemplateToolStripMenuItem.Name = "saveTemplateToolStripMenuItem"
			Me.saveTemplateToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.saveTemplateToolStripMenuItem.Text = "Save &Template"
			'
			'saveImagesToolStripMenuItem
			'
			Me.saveImagesToolStripMenuItem.Name = "saveImagesToolStripMenuItem"
			Me.saveImagesToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.saveImagesToolStripMenuItem.Text = "Save &Images"
			'
			'toolStripSeparator1
			'
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(149, 6)
			'
			'exitToolStripMenuItem
			'
			Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
			Me.exitToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.exitToolStripMenuItem.Text = "E&xit"
			'
			'optionsToolStripMenuItem
			'
			Me.optionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.changeScannerToolStripMenuItem, Me.optionsToolStripMenuItem1, Me.editRequiredInfoToolStripMenuItem})
			Me.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem"
			Me.optionsToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
			Me.optionsToolStripMenuItem.Text = "&Options"
			'
			'changeScannerToolStripMenuItem
			'
			Me.changeScannerToolStripMenuItem.Name = "changeScannerToolStripMenuItem"
			Me.changeScannerToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
			Me.changeScannerToolStripMenuItem.Text = "&Change Scanner"
			'
			'optionsToolStripMenuItem1
			'
			Me.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1"
			Me.optionsToolStripMenuItem1.Size = New System.Drawing.Size(171, 22)
			Me.optionsToolStripMenuItem1.Text = "&Extraction Options"
			'
			'editRequiredInfoToolStripMenuItem
			'
			Me.editRequiredInfoToolStripMenuItem.Name = "editRequiredInfoToolStripMenuItem"
			Me.editRequiredInfoToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
			Me.editRequiredInfoToolStripMenuItem.Text = "Edit Required &Info"
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
			'splitContainer
			'
			Me.splitContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
			Me.splitContainer.IsSplitterFixed = True
			Me.splitContainer.Location = New System.Drawing.Point(0, 28)
			Me.splitContainer.Name = "splitContainer"
			Me.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
			'
			'splitContainer.Panel1
			'
			Me.splitContainer.Panel1.Controls.Add(Me.groupBox1)
			Me.splitContainer.Panel1.Controls.Add(Me.gbFingerSelector)
			'
			'splitContainer.Panel2
			'
			Me.splitContainer.Panel2.Controls.Add(Me.tabControl)
			Me.splitContainer.Size = New System.Drawing.Size(1000, 555)
			Me.splitContainer.SplitterDistance = 131
			Me.splitContainer.SplitterWidth = 5
			Me.splitContainer.TabIndex = 0
			'
			'groupBox1
			'
			Me.groupBox1.Controls.Add(Me.chbCapturePlainFingers)
			Me.groupBox1.Controls.Add(Me.chbCaptureSlaps)
			Me.groupBox1.Controls.Add(Me.btnStartCapturing)
			Me.groupBox1.Controls.Add(Me.chbCaptureRolled)
			Me.groupBox1.Location = New System.Drawing.Point(4, 3)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(145, 122)
			Me.groupBox1.TabIndex = 5
			Me.groupBox1.TabStop = False
			'
			'chbCapturePlainFingers
			'
			Me.chbCapturePlainFingers.AutoSize = True
			Me.chbCapturePlainFingers.Checked = True
			Me.chbCapturePlainFingers.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chbCapturePlainFingers.Location = New System.Drawing.Point(9, 7)
			Me.chbCapturePlainFingers.Name = "chbCapturePlainFingers"
			Me.chbCapturePlainFingers.Size = New System.Drawing.Size(122, 17)
			Me.chbCapturePlainFingers.TabIndex = 5
			Me.chbCapturePlainFingers.Text = "Capture plain fingers"
			Me.chbCapturePlainFingers.UseVisualStyleBackColor = True
			'
			'chbCaptureSlaps
			'
			Me.chbCaptureSlaps.AutoSize = True
			Me.chbCaptureSlaps.Checked = True
			Me.chbCaptureSlaps.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chbCaptureSlaps.Location = New System.Drawing.Point(29, 30)
			Me.chbCaptureSlaps.Name = "chbCaptureSlaps"
			Me.chbCaptureSlaps.Size = New System.Drawing.Size(90, 17)
			Me.chbCaptureSlaps.TabIndex = 3
			Me.chbCaptureSlaps.Text = "Capture slaps"
			Me.chbCaptureSlaps.UseVisualStyleBackColor = True
			'
			'btnStartCapturing
			'
			Me.btnStartCapturing.Location = New System.Drawing.Point(9, 76)
			Me.btnStartCapturing.Name = "btnStartCapturing"
			Me.btnStartCapturing.Size = New System.Drawing.Size(125, 40)
			Me.btnStartCapturing.TabIndex = 0
			Me.btnStartCapturing.Text = "S&tart capturing"
			Me.btnStartCapturing.UseVisualStyleBackColor = True
			'
			'chbCaptureRolled
			'
			Me.chbCaptureRolled.AutoSize = True
			Me.chbCaptureRolled.Checked = True
			Me.chbCaptureRolled.CheckState = System.Windows.Forms.CheckState.Checked
			Me.chbCaptureRolled.Location = New System.Drawing.Point(9, 53)
			Me.chbCaptureRolled.Name = "chbCaptureRolled"
			Me.chbCaptureRolled.Size = New System.Drawing.Size(125, 17)
			Me.chbCaptureRolled.TabIndex = 4
			Me.chbCaptureRolled.Text = "Capture rolled fingers"
			Me.chbCaptureRolled.UseVisualStyleBackColor = True
			'
			'gbFingerSelector
			'
			Me.gbFingerSelector.Controls.Add(Me.fSelector)
			Me.gbFingerSelector.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.gbFingerSelector.Location = New System.Drawing.Point(155, 3)
			Me.gbFingerSelector.Name = "gbFingerSelector"
			Me.gbFingerSelector.Size = New System.Drawing.Size(246, 125)
			Me.gbFingerSelector.TabIndex = 2
			Me.gbFingerSelector.TabStop = False
			Me.gbFingerSelector.Text = "Press on finger to select missing fingers"
			'
			'fSelector
			'
			Me.fSelector.AllowHighlight = True
			Me.fSelector.IsRolled = False
			Me.fSelector.Location = New System.Drawing.Point(13, 19)
			Me.fSelector.MissingPositions = New Neurotec.Biometrics.NFPosition(-1) {}
			Me.fSelector.Name = "fSelector"
			Me.fSelector.SelectedPosition = Neurotec.Biometrics.NFPosition.Unknown
			Me.fSelector.Size = New System.Drawing.Size(227, 103)
			Me.fSelector.TabIndex = 0
			'
			'tabControl
			'
			Me.tabControl.Controls.Add(Me.tabSlaps)
			Me.tabControl.Controls.Add(Me.tabFingers)
			Me.tabControl.Controls.Add(Me.tabRolled)
			Me.tabControl.Controls.Add(Me.tabInformation)
			Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tabControl.Location = New System.Drawing.Point(0, 0)
			Me.tabControl.Name = "tabControl"
			Me.tabControl.SelectedIndex = 0
			Me.tabControl.Size = New System.Drawing.Size(998, 417)
			Me.tabControl.TabIndex = 0
			'
			'tabSlaps
			'
			Me.tabSlaps.Controls.Add(Me.tableLayoutPanel1)
			Me.tabSlaps.Location = New System.Drawing.Point(4, 22)
			Me.tabSlaps.Name = "tabSlaps"
			Me.tabSlaps.Padding = New System.Windows.Forms.Padding(3)
			Me.tabSlaps.Size = New System.Drawing.Size(990, 391)
			Me.tabSlaps.TabIndex = 0
			Me.tabSlaps.Text = "Slaps"
			Me.tabSlaps.UseVisualStyleBackColor = True
			'
			'tableLayoutPanel1
			'
			Me.tableLayoutPanel1.ColumnCount = 3
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
			Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
			Me.tableLayoutPanel1.Controls.Add(Me.panel1, 0, 1)
			Me.tableLayoutPanel1.Controls.Add(Me.label3, 2, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.label2, 1, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
			Me.tableLayoutPanel1.Controls.Add(Me.panel2, 1, 1)
			Me.tableLayoutPanel1.Controls.Add(Me.panel3, 2, 1)
			Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
			Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
			Me.tableLayoutPanel1.RowCount = 2
			Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
			Me.tableLayoutPanel1.Size = New System.Drawing.Size(984, 385)
			Me.tableLayoutPanel1.TabIndex = 0
			'
			'panel1
			'
			Me.panel1.Controls.Add(Me.nfvLeftFour)
			Me.panel1.Controls.Add(Me.toolStripViewControls)
			Me.panel1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel1.Location = New System.Drawing.Point(3, 16)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(321, 366)
			Me.panel1.TabIndex = 9
			'
			'nfvLeftFour
			'
			Me.nfvLeftFour.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftFour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftFour.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftFour.Dock = System.Windows.Forms.DockStyle.Fill
			Me.nfvLeftFour.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftFour.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftFour.Name = "nfvLeftFour"
			Me.nfvLeftFour.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftFour.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftFour.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftFour.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftFour.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftFour.Size = New System.Drawing.Size(321, 366)
			Me.nfvLeftFour.TabIndex = 9
			Me.nfvLeftFour.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftFour.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftFour.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftFour.TreeWidth = 2
			'
			'toolStripViewControls
			'
			Me.toolStripViewControls.Dock = System.Windows.Forms.DockStyle.None
			Me.toolStripViewControls.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbSaveImage, Me.tsbSaveRecord})
			Me.toolStripViewControls.Location = New System.Drawing.Point(3, 0)
			Me.toolStripViewControls.Name = "toolStripViewControls"
			Me.toolStripViewControls.Size = New System.Drawing.Size(190, 25)
			Me.toolStripViewControls.TabIndex = 8
			Me.toolStripViewControls.Text = "toolStrip2"
			'
			'tsbSaveImage
			'
			Me.tsbSaveImage.Image = Global.Neurotec.Samples.My.Resources.Resources.SaveHS
			Me.tsbSaveImage.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbSaveImage.Name = "tsbSaveImage"
			Me.tsbSaveImage.Size = New System.Drawing.Size(87, 22)
			Me.tsbSaveImage.Text = "Save Image"
			'
			'tsbSaveRecord
			'
			Me.tsbSaveRecord.Image = Global.Neurotec.Samples.My.Resources.Resources.SaveHS
			Me.tsbSaveRecord.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.tsbSaveRecord.Name = "tsbSaveRecord"
			Me.tsbSaveRecord.Size = New System.Drawing.Size(91, 22)
			Me.tsbSaveRecord.Text = "Save Record"
			'
			'label3
			'
			Me.label3.AutoSize = True
			Me.label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label3.Location = New System.Drawing.Point(658, 0)
			Me.label3.Name = "label3"
			Me.label3.Size = New System.Drawing.Size(105, 13)
			Me.label3.TabIndex = 5
			Me.label3.Text = "Right four fingers"
			'
			'label2
			'
			Me.label2.AutoSize = True
			Me.label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label2.Location = New System.Drawing.Point(330, 0)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(51, 13)
			Me.label2.TabIndex = 4
			Me.label2.Text = "Thumbs"
			'
			'label1
			'
			Me.label1.AutoSize = True
			Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label1.Location = New System.Drawing.Point(3, 0)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(97, 13)
			Me.label1.TabIndex = 3
			Me.label1.Text = "Left four fingers"
			'
			'panel2
			'
			Me.panel2.Controls.Add(Me.nfvThumbs)
			Me.panel2.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel2.Location = New System.Drawing.Point(330, 16)
			Me.panel2.Name = "panel2"
			Me.panel2.Size = New System.Drawing.Size(322, 366)
			Me.panel2.TabIndex = 10
			'
			'nfvThumbs
			'
			Me.nfvThumbs.BackColor = System.Drawing.SystemColors.Control
			Me.nfvThumbs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvThumbs.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvThumbs.Dock = System.Windows.Forms.DockStyle.Fill
			Me.nfvThumbs.Location = New System.Drawing.Point(0, 0)
			Me.nfvThumbs.MinutiaColor = System.Drawing.Color.Red
			Me.nfvThumbs.Name = "nfvThumbs"
			Me.nfvThumbs.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvThumbs.ResultImageColor = System.Drawing.Color.Green
			Me.nfvThumbs.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvThumbs.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvThumbs.SingularPointColor = System.Drawing.Color.Red
			Me.nfvThumbs.Size = New System.Drawing.Size(322, 366)
			Me.nfvThumbs.TabIndex = 10
			Me.nfvThumbs.TreeColor = System.Drawing.Color.Crimson
			Me.nfvThumbs.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvThumbs.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvThumbs.TreeWidth = 2
			'
			'panel3
			'
			Me.panel3.Controls.Add(Me.nfvRightFour)
			Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel3.Location = New System.Drawing.Point(658, 16)
			Me.panel3.Name = "panel3"
			Me.panel3.Size = New System.Drawing.Size(323, 366)
			Me.panel3.TabIndex = 11
			'
			'nfvRightFour
			'
			Me.nfvRightFour.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightFour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightFour.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightFour.Dock = System.Windows.Forms.DockStyle.Fill
			Me.nfvRightFour.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightFour.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightFour.Name = "nfvRightFour"
			Me.nfvRightFour.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightFour.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightFour.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightFour.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightFour.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightFour.Size = New System.Drawing.Size(323, 366)
			Me.nfvRightFour.TabIndex = 10
			Me.nfvRightFour.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightFour.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightFour.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightFour.TreeWidth = 2
			'
			'tabFingers
			'
			Me.tabFingers.Controls.Add(Me.tableLayoutPanel2)
			Me.tabFingers.Location = New System.Drawing.Point(4, 22)
			Me.tabFingers.Name = "tabFingers"
			Me.tabFingers.Padding = New System.Windows.Forms.Padding(3)
			Me.tabFingers.Size = New System.Drawing.Size(990, 391)
			Me.tabFingers.TabIndex = 1
			Me.tabFingers.Text = "Fingers"
			Me.tabFingers.UseVisualStyleBackColor = True
			'
			'tableLayoutPanel2
			'
			Me.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control
			Me.tableLayoutPanel2.ColumnCount = 5
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel2.Controls.Add(Me.label8, 4, 1)
			Me.tableLayoutPanel2.Controls.Add(Me.label7, 3, 1)
			Me.tableLayoutPanel2.Controls.Add(Me.label6, 2, 1)
			Me.tableLayoutPanel2.Controls.Add(Me.label5, 1, 1)
			Me.tableLayoutPanel2.Controls.Add(Me.label4, 0, 1)
			Me.tableLayoutPanel2.Controls.Add(Me.label9, 0, 3)
			Me.tableLayoutPanel2.Controls.Add(Me.label10, 1, 3)
			Me.tableLayoutPanel2.Controls.Add(Me.label11, 2, 3)
			Me.tableLayoutPanel2.Controls.Add(Me.label12, 3, 3)
			Me.tableLayoutPanel2.Controls.Add(Me.label13, 4, 3)
			Me.tableLayoutPanel2.Controls.Add(Me.chbShowOriginal, 0, 0)
			Me.tableLayoutPanel2.Controls.Add(Me.panel4, 0, 2)
			Me.tableLayoutPanel2.Controls.Add(Me.panel5, 1, 2)
			Me.tableLayoutPanel2.Controls.Add(Me.panel6, 2, 2)
			Me.tableLayoutPanel2.Controls.Add(Me.panel7, 3, 2)
			Me.tableLayoutPanel2.Controls.Add(Me.panel8, 4, 2)
			Me.tableLayoutPanel2.Controls.Add(Me.panel9, 0, 4)
			Me.tableLayoutPanel2.Controls.Add(Me.panel10, 1, 4)
			Me.tableLayoutPanel2.Controls.Add(Me.panel11, 2, 4)
			Me.tableLayoutPanel2.Controls.Add(Me.panel12, 3, 4)
			Me.tableLayoutPanel2.Controls.Add(Me.panel13, 4, 4)
			Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
			Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
			Me.tableLayoutPanel2.RowCount = 5
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
			Me.tableLayoutPanel2.Size = New System.Drawing.Size(984, 385)
			Me.tableLayoutPanel2.TabIndex = 0
			'
			'label8
			'
			Me.label8.AutoSize = True
			Me.label8.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label8.Location = New System.Drawing.Point(787, 23)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(194, 13)
			Me.label8.TabIndex = 4
			Me.label8.Text = "Left Little"
			Me.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label7
			'
			Me.label7.AutoSize = True
			Me.label7.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label7.Location = New System.Drawing.Point(591, 23)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(190, 13)
			Me.label7.TabIndex = 3
			Me.label7.Text = "Left Ring"
			Me.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label6
			'
			Me.label6.AutoSize = True
			Me.label6.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label6.Location = New System.Drawing.Point(395, 23)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(190, 13)
			Me.label6.TabIndex = 2
			Me.label6.Text = "Left Middle"
			Me.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label5
			'
			Me.label5.AutoSize = True
			Me.label5.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label5.Location = New System.Drawing.Point(199, 23)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(190, 13)
			Me.label5.TabIndex = 1
			Me.label5.Text = "Left Index"
			Me.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label4
			'
			Me.label4.AutoSize = True
			Me.label4.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label4.Location = New System.Drawing.Point(3, 23)
			Me.label4.Name = "label4"
			Me.label4.Size = New System.Drawing.Size(190, 13)
			Me.label4.TabIndex = 0
			Me.label4.Text = "Left Thumb"
			Me.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label9
			'
			Me.label9.AutoSize = True
			Me.label9.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label9.Location = New System.Drawing.Point(3, 204)
			Me.label9.Name = "label9"
			Me.label9.Size = New System.Drawing.Size(190, 13)
			Me.label9.TabIndex = 5
			Me.label9.Text = "Right Thumb"
			Me.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label10
			'
			Me.label10.AutoSize = True
			Me.label10.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label10.Location = New System.Drawing.Point(199, 204)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(190, 13)
			Me.label10.TabIndex = 6
			Me.label10.Text = "Right Index"
			Me.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label11
			'
			Me.label11.AutoSize = True
			Me.label11.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label11.Location = New System.Drawing.Point(395, 204)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(190, 13)
			Me.label11.TabIndex = 7
			Me.label11.Text = "Right Middle"
			Me.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label12
			'
			Me.label12.AutoSize = True
			Me.label12.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label12.Location = New System.Drawing.Point(591, 204)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(190, 13)
			Me.label12.TabIndex = 8
			Me.label12.Text = "Right Ring"
			Me.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label13
			'
			Me.label13.AutoSize = True
			Me.label13.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label13.Location = New System.Drawing.Point(787, 204)
			Me.label13.Name = "label13"
			Me.label13.Size = New System.Drawing.Size(194, 13)
			Me.label13.TabIndex = 9
			Me.label13.Text = "Right Little"
			Me.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'chbShowOriginal
			'
			Me.chbShowOriginal.AutoSize = True
			Me.chbShowOriginal.Location = New System.Drawing.Point(3, 3)
			Me.chbShowOriginal.Name = "chbShowOriginal"
			Me.chbShowOriginal.Size = New System.Drawing.Size(125, 17)
			Me.chbShowOriginal.TabIndex = 20
			Me.chbShowOriginal.Text = "Show original images"
			Me.chbShowOriginal.UseVisualStyleBackColor = True
			'
			'panel4
			'
			Me.panel4.Controls.Add(Me.nfvLeftThumb)
			Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel4.Location = New System.Drawing.Point(3, 39)
			Me.panel4.Name = "panel4"
			Me.panel4.Size = New System.Drawing.Size(190, 162)
			Me.panel4.TabIndex = 21
			'
			'nfvLeftThumb
			'
			Me.nfvLeftThumb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftThumb.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftThumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftThumb.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftThumb.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftThumb.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftThumb.Name = "nfvLeftThumb"
			Me.nfvLeftThumb.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftThumb.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftThumb.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftThumb.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftThumb.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftThumb.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftThumb.TabIndex = 10
			Me.nfvLeftThumb.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftThumb.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftThumb.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftThumb.TreeWidth = 2
			'
			'panel5
			'
			Me.panel5.Controls.Add(Me.nfvLeftIndex)
			Me.panel5.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel5.Location = New System.Drawing.Point(199, 39)
			Me.panel5.Name = "panel5"
			Me.panel5.Size = New System.Drawing.Size(190, 162)
			Me.panel5.TabIndex = 22
			'
			'nfvLeftIndex
			'
			Me.nfvLeftIndex.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftIndex.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftIndex.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftIndex.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftIndex.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftIndex.Name = "nfvLeftIndex"
			Me.nfvLeftIndex.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftIndex.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftIndex.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftIndex.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftIndex.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftIndex.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftIndex.TabIndex = 11
			Me.nfvLeftIndex.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftIndex.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftIndex.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftIndex.TreeWidth = 2
			'
			'panel6
			'
			Me.panel6.Controls.Add(Me.nfvLeftMiddle)
			Me.panel6.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel6.Location = New System.Drawing.Point(395, 39)
			Me.panel6.Name = "panel6"
			Me.panel6.Size = New System.Drawing.Size(190, 162)
			Me.panel6.TabIndex = 23
			'
			'nfvLeftMiddle
			'
			Me.nfvLeftMiddle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftMiddle.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftMiddle.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftMiddle.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftMiddle.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftMiddle.Name = "nfvLeftMiddle"
			Me.nfvLeftMiddle.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftMiddle.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftMiddle.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftMiddle.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftMiddle.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftMiddle.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftMiddle.TabIndex = 12
			Me.nfvLeftMiddle.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftMiddle.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftMiddle.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftMiddle.TreeWidth = 2
			'
			'panel7
			'
			Me.panel7.Controls.Add(Me.nfvLeftRing)
			Me.panel7.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel7.Location = New System.Drawing.Point(591, 39)
			Me.panel7.Name = "panel7"
			Me.panel7.Size = New System.Drawing.Size(190, 162)
			Me.panel7.TabIndex = 24
			'
			'nfvLeftRing
			'
			Me.nfvLeftRing.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftRing.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftRing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftRing.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftRing.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftRing.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftRing.Name = "nfvLeftRing"
			Me.nfvLeftRing.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftRing.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftRing.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftRing.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftRing.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftRing.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftRing.TabIndex = 13
			Me.nfvLeftRing.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftRing.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftRing.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftRing.TreeWidth = 2
			'
			'panel8
			'
			Me.panel8.Controls.Add(Me.nfvLeftLittle)
			Me.panel8.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel8.Location = New System.Drawing.Point(787, 39)
			Me.panel8.Name = "panel8"
			Me.panel8.Size = New System.Drawing.Size(194, 162)
			Me.panel8.TabIndex = 25
			'
			'nfvLeftLittle
			'
			Me.nfvLeftLittle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftLittle.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftLittle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftLittle.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftLittle.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftLittle.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftLittle.Name = "nfvLeftLittle"
			Me.nfvLeftLittle.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftLittle.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftLittle.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftLittle.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftLittle.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftLittle.Size = New System.Drawing.Size(194, 162)
			Me.nfvLeftLittle.TabIndex = 14
			Me.nfvLeftLittle.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftLittle.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftLittle.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftLittle.TreeWidth = 2
			'
			'panel9
			'
			Me.panel9.Controls.Add(Me.nfvRightThumb)
			Me.panel9.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel9.Location = New System.Drawing.Point(3, 220)
			Me.panel9.Name = "panel9"
			Me.panel9.Size = New System.Drawing.Size(190, 162)
			Me.panel9.TabIndex = 26
			'
			'nfvRightThumb
			'
			Me.nfvRightThumb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightThumb.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightThumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightThumb.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightThumb.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightThumb.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightThumb.Name = "nfvRightThumb"
			Me.nfvRightThumb.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightThumb.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightThumb.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightThumb.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightThumb.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightThumb.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightThumb.TabIndex = 15
			Me.nfvRightThumb.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightThumb.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightThumb.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightThumb.TreeWidth = 2
			'
			'panel10
			'
			Me.panel10.Controls.Add(Me.nfvRightIndex)
			Me.panel10.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel10.Location = New System.Drawing.Point(199, 220)
			Me.panel10.Name = "panel10"
			Me.panel10.Size = New System.Drawing.Size(190, 162)
			Me.panel10.TabIndex = 27
			'
			'nfvRightIndex
			'
			Me.nfvRightIndex.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightIndex.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightIndex.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightIndex.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightIndex.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightIndex.Name = "nfvRightIndex"
			Me.nfvRightIndex.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightIndex.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightIndex.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightIndex.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightIndex.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightIndex.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightIndex.TabIndex = 16
			Me.nfvRightIndex.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightIndex.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightIndex.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightIndex.TreeWidth = 2
			'
			'panel11
			'
			Me.panel11.Controls.Add(Me.nfvRightMiddle)
			Me.panel11.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel11.Location = New System.Drawing.Point(395, 220)
			Me.panel11.Name = "panel11"
			Me.panel11.Size = New System.Drawing.Size(190, 162)
			Me.panel11.TabIndex = 28
			'
			'nfvRightMiddle
			'
			Me.nfvRightMiddle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightMiddle.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightMiddle.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightMiddle.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightMiddle.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightMiddle.Name = "nfvRightMiddle"
			Me.nfvRightMiddle.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightMiddle.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightMiddle.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightMiddle.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightMiddle.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightMiddle.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightMiddle.TabIndex = 17
			Me.nfvRightMiddle.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightMiddle.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightMiddle.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightMiddle.TreeWidth = 2
			'
			'panel12
			'
			Me.panel12.Controls.Add(Me.nfvRightRing)
			Me.panel12.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel12.Location = New System.Drawing.Point(591, 220)
			Me.panel12.Name = "panel12"
			Me.panel12.Size = New System.Drawing.Size(190, 162)
			Me.panel12.TabIndex = 29
			'
			'nfvRightRing
			'
			Me.nfvRightRing.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightRing.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightRing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightRing.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightRing.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightRing.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightRing.Name = "nfvRightRing"
			Me.nfvRightRing.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightRing.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightRing.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightRing.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightRing.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightRing.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightRing.TabIndex = 18
			Me.nfvRightRing.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightRing.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightRing.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightRing.TreeWidth = 2
			'
			'panel13
			'
			Me.panel13.Controls.Add(Me.nfvRightLittle)
			Me.panel13.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel13.Location = New System.Drawing.Point(787, 220)
			Me.panel13.Name = "panel13"
			Me.panel13.Size = New System.Drawing.Size(194, 162)
			Me.panel13.TabIndex = 30
			'
			'nfvRightLittle
			'
			Me.nfvRightLittle.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightLittle.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightLittle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightLittle.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightLittle.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightLittle.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightLittle.Name = "nfvRightLittle"
			Me.nfvRightLittle.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightLittle.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightLittle.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightLittle.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightLittle.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightLittle.Size = New System.Drawing.Size(194, 162)
			Me.nfvRightLittle.TabIndex = 19
			Me.nfvRightLittle.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightLittle.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightLittle.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightLittle.TreeWidth = 2
			'
			'tabRolled
			'
			Me.tabRolled.Controls.Add(Me.tableLayoutPanel3)
			Me.tabRolled.Location = New System.Drawing.Point(4, 22)
			Me.tabRolled.Name = "tabRolled"
			Me.tabRolled.Padding = New System.Windows.Forms.Padding(3)
			Me.tabRolled.Size = New System.Drawing.Size(990, 391)
			Me.tabRolled.TabIndex = 2
			Me.tabRolled.Text = "Rolled Fingers"
			Me.tabRolled.UseVisualStyleBackColor = True
			'
			'tableLayoutPanel3
			'
			Me.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control
			Me.tableLayoutPanel3.ColumnCount = 5
			Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
			Me.tableLayoutPanel3.Controls.Add(Me.label14, 4, 1)
			Me.tableLayoutPanel3.Controls.Add(Me.label15, 3, 1)
			Me.tableLayoutPanel3.Controls.Add(Me.label16, 2, 1)
			Me.tableLayoutPanel3.Controls.Add(Me.label17, 1, 1)
			Me.tableLayoutPanel3.Controls.Add(Me.label18, 0, 1)
			Me.tableLayoutPanel3.Controls.Add(Me.label19, 0, 3)
			Me.tableLayoutPanel3.Controls.Add(Me.label20, 1, 3)
			Me.tableLayoutPanel3.Controls.Add(Me.label21, 2, 3)
			Me.tableLayoutPanel3.Controls.Add(Me.label22, 3, 3)
			Me.tableLayoutPanel3.Controls.Add(Me.label23, 4, 3)
			Me.tableLayoutPanel3.Controls.Add(Me.chbShowOriginalRolled, 0, 0)
			Me.tableLayoutPanel3.Controls.Add(Me.panel14, 0, 2)
			Me.tableLayoutPanel3.Controls.Add(Me.panel15, 1, 2)
			Me.tableLayoutPanel3.Controls.Add(Me.panel16, 2, 2)
			Me.tableLayoutPanel3.Controls.Add(Me.panel17, 3, 2)
			Me.tableLayoutPanel3.Controls.Add(Me.panel18, 4, 2)
			Me.tableLayoutPanel3.Controls.Add(Me.panel19, 0, 4)
			Me.tableLayoutPanel3.Controls.Add(Me.panel20, 1, 4)
			Me.tableLayoutPanel3.Controls.Add(Me.panel21, 2, 4)
			Me.tableLayoutPanel3.Controls.Add(Me.panel22, 3, 4)
			Me.tableLayoutPanel3.Controls.Add(Me.panel23, 4, 4)
			Me.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
			Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
			Me.tableLayoutPanel3.RowCount = 5
			Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
			Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
			Me.tableLayoutPanel3.Size = New System.Drawing.Size(984, 385)
			Me.tableLayoutPanel3.TabIndex = 1
			'
			'label14
			'
			Me.label14.AutoSize = True
			Me.label14.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label14.Location = New System.Drawing.Point(787, 23)
			Me.label14.Name = "label14"
			Me.label14.Size = New System.Drawing.Size(194, 13)
			Me.label14.TabIndex = 4
			Me.label14.Text = "Left Little"
			Me.label14.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label15
			'
			Me.label15.AutoSize = True
			Me.label15.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label15.Location = New System.Drawing.Point(591, 23)
			Me.label15.Name = "label15"
			Me.label15.Size = New System.Drawing.Size(190, 13)
			Me.label15.TabIndex = 3
			Me.label15.Text = "Left Ring"
			Me.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label16
			'
			Me.label16.AutoSize = True
			Me.label16.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label16.Location = New System.Drawing.Point(395, 23)
			Me.label16.Name = "label16"
			Me.label16.Size = New System.Drawing.Size(190, 13)
			Me.label16.TabIndex = 2
			Me.label16.Text = "Left Middle"
			Me.label16.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label17
			'
			Me.label17.AutoSize = True
			Me.label17.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label17.Location = New System.Drawing.Point(199, 23)
			Me.label17.Name = "label17"
			Me.label17.Size = New System.Drawing.Size(190, 13)
			Me.label17.TabIndex = 1
			Me.label17.Text = "Left Index"
			Me.label17.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label18
			'
			Me.label18.AutoSize = True
			Me.label18.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label18.Location = New System.Drawing.Point(3, 23)
			Me.label18.Name = "label18"
			Me.label18.Size = New System.Drawing.Size(190, 13)
			Me.label18.TabIndex = 0
			Me.label18.Text = "Left Thumb"
			Me.label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label19
			'
			Me.label19.AutoSize = True
			Me.label19.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label19.Location = New System.Drawing.Point(3, 204)
			Me.label19.Name = "label19"
			Me.label19.Size = New System.Drawing.Size(190, 13)
			Me.label19.TabIndex = 5
			Me.label19.Text = "Right Thumb"
			Me.label19.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label20
			'
			Me.label20.AutoSize = True
			Me.label20.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label20.Location = New System.Drawing.Point(199, 204)
			Me.label20.Name = "label20"
			Me.label20.Size = New System.Drawing.Size(190, 13)
			Me.label20.TabIndex = 6
			Me.label20.Text = "Right Index"
			Me.label20.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label21
			'
			Me.label21.AutoSize = True
			Me.label21.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label21.Location = New System.Drawing.Point(395, 204)
			Me.label21.Name = "label21"
			Me.label21.Size = New System.Drawing.Size(190, 13)
			Me.label21.TabIndex = 7
			Me.label21.Text = "Right Middle"
			Me.label21.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label22
			'
			Me.label22.AutoSize = True
			Me.label22.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label22.Location = New System.Drawing.Point(591, 204)
			Me.label22.Name = "label22"
			Me.label22.Size = New System.Drawing.Size(190, 13)
			Me.label22.TabIndex = 8
			Me.label22.Text = "Right Ring"
			Me.label22.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'label23
			'
			Me.label23.AutoSize = True
			Me.label23.Dock = System.Windows.Forms.DockStyle.Fill
			Me.label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.label23.Location = New System.Drawing.Point(787, 204)
			Me.label23.Name = "label23"
			Me.label23.Size = New System.Drawing.Size(194, 13)
			Me.label23.TabIndex = 9
			Me.label23.Text = "Right Little"
			Me.label23.TextAlign = System.Drawing.ContentAlignment.TopCenter
			'
			'chbShowOriginalRolled
			'
			Me.chbShowOriginalRolled.AutoSize = True
			Me.chbShowOriginalRolled.Location = New System.Drawing.Point(3, 3)
			Me.chbShowOriginalRolled.Name = "chbShowOriginalRolled"
			Me.chbShowOriginalRolled.Size = New System.Drawing.Size(125, 17)
			Me.chbShowOriginalRolled.TabIndex = 20
			Me.chbShowOriginalRolled.Text = "Show original images"
			Me.chbShowOriginalRolled.UseVisualStyleBackColor = True
			'
			'panel14
			'
			Me.panel14.Controls.Add(Me.nfvLeftThumbRolled)
			Me.panel14.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel14.Location = New System.Drawing.Point(3, 39)
			Me.panel14.Name = "panel14"
			Me.panel14.Size = New System.Drawing.Size(190, 162)
			Me.panel14.TabIndex = 21
			'
			'nfvLeftThumbRolled
			'
			Me.nfvLeftThumbRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftThumbRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftThumbRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftThumbRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftThumbRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftThumbRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftThumbRolled.Name = "nfvLeftThumbRolled"
			Me.nfvLeftThumbRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftThumbRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftThumbRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftThumbRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftThumbRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftThumbRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftThumbRolled.TabIndex = 10
			Me.nfvLeftThumbRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftThumbRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftThumbRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftThumbRolled.TreeWidth = 2
			'
			'panel15
			'
			Me.panel15.Controls.Add(Me.nfvLeftIndexRolled)
			Me.panel15.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel15.Location = New System.Drawing.Point(199, 39)
			Me.panel15.Name = "panel15"
			Me.panel15.Size = New System.Drawing.Size(190, 162)
			Me.panel15.TabIndex = 22
			'
			'nfvLeftIndexRolled
			'
			Me.nfvLeftIndexRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftIndexRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftIndexRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftIndexRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftIndexRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftIndexRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftIndexRolled.Name = "nfvLeftIndexRolled"
			Me.nfvLeftIndexRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftIndexRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftIndexRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftIndexRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftIndexRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftIndexRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftIndexRolled.TabIndex = 11
			Me.nfvLeftIndexRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftIndexRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftIndexRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftIndexRolled.TreeWidth = 2
			'
			'panel16
			'
			Me.panel16.Controls.Add(Me.nfvLeftMiddleRolled)
			Me.panel16.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel16.Location = New System.Drawing.Point(395, 39)
			Me.panel16.Name = "panel16"
			Me.panel16.Size = New System.Drawing.Size(190, 162)
			Me.panel16.TabIndex = 23
			'
			'nfvLeftMiddleRolled
			'
			Me.nfvLeftMiddleRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftMiddleRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftMiddleRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftMiddleRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftMiddleRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftMiddleRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftMiddleRolled.Name = "nfvLeftMiddleRolled"
			Me.nfvLeftMiddleRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftMiddleRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftMiddleRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftMiddleRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftMiddleRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftMiddleRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftMiddleRolled.TabIndex = 12
			Me.nfvLeftMiddleRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftMiddleRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftMiddleRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftMiddleRolled.TreeWidth = 2
			'
			'panel17
			'
			Me.panel17.Controls.Add(Me.nfvLeftRingRolled)
			Me.panel17.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel17.Location = New System.Drawing.Point(591, 39)
			Me.panel17.Name = "panel17"
			Me.panel17.Size = New System.Drawing.Size(190, 162)
			Me.panel17.TabIndex = 24
			'
			'nfvLeftRingRolled
			'
			Me.nfvLeftRingRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftRingRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftRingRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftRingRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftRingRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftRingRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftRingRolled.Name = "nfvLeftRingRolled"
			Me.nfvLeftRingRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftRingRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftRingRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftRingRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftRingRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftRingRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvLeftRingRolled.TabIndex = 13
			Me.nfvLeftRingRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftRingRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftRingRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftRingRolled.TreeWidth = 2
			'
			'panel18
			'
			Me.panel18.Controls.Add(Me.nfvLeftLittleRolled)
			Me.panel18.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel18.Location = New System.Drawing.Point(787, 39)
			Me.panel18.Name = "panel18"
			Me.panel18.Size = New System.Drawing.Size(194, 162)
			Me.panel18.TabIndex = 25
			'
			'nfvLeftLittleRolled
			'
			Me.nfvLeftLittleRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvLeftLittleRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvLeftLittleRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvLeftLittleRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvLeftLittleRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvLeftLittleRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvLeftLittleRolled.Name = "nfvLeftLittleRolled"
			Me.nfvLeftLittleRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvLeftLittleRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvLeftLittleRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvLeftLittleRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvLeftLittleRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvLeftLittleRolled.Size = New System.Drawing.Size(194, 162)
			Me.nfvLeftLittleRolled.TabIndex = 14
			Me.nfvLeftLittleRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvLeftLittleRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvLeftLittleRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvLeftLittleRolled.TreeWidth = 2
			'
			'panel19
			'
			Me.panel19.Controls.Add(Me.nfvRightThumbRolled)
			Me.panel19.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel19.Location = New System.Drawing.Point(3, 220)
			Me.panel19.Name = "panel19"
			Me.panel19.Size = New System.Drawing.Size(190, 162)
			Me.panel19.TabIndex = 26
			'
			'nfvRightThumbRolled
			'
			Me.nfvRightThumbRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightThumbRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightThumbRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightThumbRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightThumbRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightThumbRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightThumbRolled.Name = "nfvRightThumbRolled"
			Me.nfvRightThumbRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightThumbRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightThumbRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightThumbRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightThumbRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightThumbRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightThumbRolled.TabIndex = 15
			Me.nfvRightThumbRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightThumbRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightThumbRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightThumbRolled.TreeWidth = 2
			'
			'panel20
			'
			Me.panel20.Controls.Add(Me.nfvRightIndexRolled)
			Me.panel20.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel20.Location = New System.Drawing.Point(199, 220)
			Me.panel20.Name = "panel20"
			Me.panel20.Size = New System.Drawing.Size(190, 162)
			Me.panel20.TabIndex = 27
			'
			'nfvRightIndexRolled
			'
			Me.nfvRightIndexRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightIndexRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightIndexRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightIndexRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightIndexRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightIndexRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightIndexRolled.Name = "nfvRightIndexRolled"
			Me.nfvRightIndexRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightIndexRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightIndexRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightIndexRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightIndexRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightIndexRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightIndexRolled.TabIndex = 16
			Me.nfvRightIndexRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightIndexRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightIndexRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightIndexRolled.TreeWidth = 2
			'
			'panel21
			'
			Me.panel21.Controls.Add(Me.nfvRightMiddleRolled)
			Me.panel21.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel21.Location = New System.Drawing.Point(395, 220)
			Me.panel21.Name = "panel21"
			Me.panel21.Size = New System.Drawing.Size(190, 162)
			Me.panel21.TabIndex = 28
			'
			'nfvRightMiddleRolled
			'
			Me.nfvRightMiddleRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightMiddleRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightMiddleRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightMiddleRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightMiddleRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightMiddleRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightMiddleRolled.Name = "nfvRightMiddleRolled"
			Me.nfvRightMiddleRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightMiddleRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightMiddleRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightMiddleRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightMiddleRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightMiddleRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightMiddleRolled.TabIndex = 17
			Me.nfvRightMiddleRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightMiddleRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightMiddleRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightMiddleRolled.TreeWidth = 2
			'
			'panel22
			'
			Me.panel22.Controls.Add(Me.nfvRightRingRolled)
			Me.panel22.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel22.Location = New System.Drawing.Point(591, 220)
			Me.panel22.Name = "panel22"
			Me.panel22.Size = New System.Drawing.Size(190, 162)
			Me.panel22.TabIndex = 29
			'
			'nfvRightRingRolled
			'
			Me.nfvRightRingRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightRingRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightRingRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightRingRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightRingRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightRingRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightRingRolled.Name = "nfvRightRingRolled"
			Me.nfvRightRingRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightRingRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightRingRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightRingRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightRingRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightRingRolled.Size = New System.Drawing.Size(190, 162)
			Me.nfvRightRingRolled.TabIndex = 18
			Me.nfvRightRingRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightRingRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightRingRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightRingRolled.TreeWidth = 2
			'
			'panel23
			'
			Me.panel23.Controls.Add(Me.nfvRightLittleRolled)
			Me.panel23.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panel23.Location = New System.Drawing.Point(787, 220)
			Me.panel23.Name = "panel23"
			Me.panel23.Size = New System.Drawing.Size(194, 162)
			Me.panel23.TabIndex = 30
			'
			'nfvRightLittleRolled
			'
			Me.nfvRightLittleRolled.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.nfvRightLittleRolled.BackColor = System.Drawing.SystemColors.Control
			Me.nfvRightLittleRolled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.nfvRightLittleRolled.BoundingRectColor = System.Drawing.Color.Red
			Me.nfvRightLittleRolled.Location = New System.Drawing.Point(0, 0)
			Me.nfvRightLittleRolled.MinutiaColor = System.Drawing.Color.Red
			Me.nfvRightLittleRolled.Name = "nfvRightLittleRolled"
			Me.nfvRightLittleRolled.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.nfvRightLittleRolled.ResultImageColor = System.Drawing.Color.Green
			Me.nfvRightLittleRolled.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.nfvRightLittleRolled.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.nfvRightLittleRolled.SingularPointColor = System.Drawing.Color.Red
			Me.nfvRightLittleRolled.Size = New System.Drawing.Size(194, 162)
			Me.nfvRightLittleRolled.TabIndex = 19
			Me.nfvRightLittleRolled.TreeColor = System.Drawing.Color.Crimson
			Me.nfvRightLittleRolled.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.nfvRightLittleRolled.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.nfvRightLittleRolled.TreeWidth = 2
			'
			'tabInformation
			'
			Me.tabInformation.Controls.Add(Me.infoPanel)
			Me.tabInformation.Location = New System.Drawing.Point(4, 22)
			Me.tabInformation.Name = "tabInformation"
			Me.tabInformation.Padding = New System.Windows.Forms.Padding(3)
			Me.tabInformation.Size = New System.Drawing.Size(990, 391)
			Me.tabInformation.TabIndex = 3
			Me.tabInformation.Text = "Information"
			Me.tabInformation.UseVisualStyleBackColor = True
			'
			'infoPanel
			'
			Me.infoPanel.DeviceManager = Nothing
			Me.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill
			Me.infoPanel.Location = New System.Drawing.Point(3, 3)
			Me.infoPanel.Model = Nothing
			Me.infoPanel.Name = "infoPanel"
			Me.infoPanel.Size = New System.Drawing.Size(984, 385)
			Me.infoPanel.TabIndex = 0
			'
			'MainForm
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(1000, 583)
			Me.Controls.Add(Me.menuStrip1)
			Me.Controls.Add(Me.splitContainer)
			Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
			Me.MainMenuStrip = Me.menuStrip1
			Me.MinimumSize = New System.Drawing.Size(250, 250)
			Me.Name = "MainForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Enrollment Sample"
			Me.menuStrip1.ResumeLayout(False)
			Me.menuStrip1.PerformLayout()
			Me.splitContainer.Panel1.ResumeLayout(False)
			Me.splitContainer.Panel2.ResumeLayout(False)
			Me.splitContainer.ResumeLayout(False)
			Me.groupBox1.ResumeLayout(False)
			Me.groupBox1.PerformLayout()
			Me.gbFingerSelector.ResumeLayout(False)
			Me.tabControl.ResumeLayout(False)
			Me.tabSlaps.ResumeLayout(False)
			Me.tableLayoutPanel1.ResumeLayout(False)
			Me.tableLayoutPanel1.PerformLayout()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.toolStripViewControls.ResumeLayout(False)
			Me.toolStripViewControls.PerformLayout()
			Me.panel2.ResumeLayout(False)
			Me.panel3.ResumeLayout(False)
			Me.tabFingers.ResumeLayout(False)
			Me.tableLayoutPanel2.ResumeLayout(False)
			Me.tableLayoutPanel2.PerformLayout()
			Me.panel4.ResumeLayout(False)
			Me.panel5.ResumeLayout(False)
			Me.panel6.ResumeLayout(False)
			Me.panel7.ResumeLayout(False)
			Me.panel8.ResumeLayout(False)
			Me.panel9.ResumeLayout(False)
			Me.panel10.ResumeLayout(False)
			Me.panel11.ResumeLayout(False)
			Me.panel12.ResumeLayout(False)
			Me.panel13.ResumeLayout(False)
			Me.tabRolled.ResumeLayout(False)
			Me.tableLayoutPanel3.ResumeLayout(False)
			Me.tableLayoutPanel3.PerformLayout()
			Me.panel14.ResumeLayout(False)
			Me.panel15.ResumeLayout(False)
			Me.panel16.ResumeLayout(False)
			Me.panel17.ResumeLayout(False)
			Me.panel18.ResumeLayout(False)
			Me.panel19.ResumeLayout(False)
			Me.panel20.ResumeLayout(False)
			Me.panel21.ResumeLayout(False)
			Me.panel22.ResumeLayout(False)
			Me.panel23.ResumeLayout(False)
			Me.tabInformation.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

		#End Region

		Private splitContainer As System.Windows.Forms.SplitContainer
		Private WithEvents fSelector As FingerSelector
		Private WithEvents btnStartCapturing As System.Windows.Forms.Button
		Private WithEvents chbCaptureRolled As System.Windows.Forms.CheckBox
		Private WithEvents chbCaptureSlaps As System.Windows.Forms.CheckBox
		Private gbFingerSelector As System.Windows.Forms.GroupBox
		Private WithEvents tabControl As System.Windows.Forms.TabControl
		Private tabSlaps As System.Windows.Forms.TabPage
		Private tabFingers As System.Windows.Forms.TabPage
		Private tabRolled As System.Windows.Forms.TabPage
		Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
		Private label3 As System.Windows.Forms.Label
		Private label2 As System.Windows.Forms.Label
		Private label1 As System.Windows.Forms.Label
		Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
		Private label8 As System.Windows.Forms.Label
		Private label7 As System.Windows.Forms.Label
		Private label6 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private label4 As System.Windows.Forms.Label
		Private label9 As System.Windows.Forms.Label
		Private label10 As System.Windows.Forms.Label
		Private label11 As System.Windows.Forms.Label
		Private label12 As System.Windows.Forms.Label
		Private label13 As System.Windows.Forms.Label
		Private WithEvents nfvRightRing As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightMiddle As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightIndex As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightThumb As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftLittle As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftRing As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftMiddle As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftIndex As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftThumb As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightLittle As Neurotec.Biometrics.Gui.NFingerView
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private WithEvents chbCapturePlainFingers As System.Windows.Forms.CheckBox
		Private WithEvents chbShowOriginal As System.Windows.Forms.CheckBox
		Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
		Private WithEvents nfvRightLittleRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightRingRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightMiddleRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightIndexRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvRightThumbRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftLittleRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftRingRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftMiddleRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents nfvLeftIndexRolled As Neurotec.Biometrics.Gui.NFingerView
		Private label14 As System.Windows.Forms.Label
		Private label15 As System.Windows.Forms.Label
		Private label16 As System.Windows.Forms.Label
		Private label17 As System.Windows.Forms.Label
		Private label18 As System.Windows.Forms.Label
		Private label19 As System.Windows.Forms.Label
		Private label20 As System.Windows.Forms.Label
		Private label21 As System.Windows.Forms.Label
		Private label22 As System.Windows.Forms.Label
		Private label23 As System.Windows.Forms.Label
		Private WithEvents nfvLeftThumbRolled As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents chbShowOriginalRolled As System.Windows.Forms.CheckBox
		Private saveFileDialog As System.Windows.Forms.SaveFileDialog
		Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
		Private WithEvents toolStripViewControls As System.Windows.Forms.ToolStrip
		Private WithEvents tsbSaveImage As System.Windows.Forms.ToolStripButton
		Private WithEvents tsbSaveRecord As System.Windows.Forms.ToolStripButton
		Private panel1 As System.Windows.Forms.Panel
		Private panel2 As System.Windows.Forms.Panel
		Private panel3 As System.Windows.Forms.Panel
		Private panel4 As System.Windows.Forms.Panel
		Private panel5 As System.Windows.Forms.Panel
		Private panel6 As System.Windows.Forms.Panel
		Private panel7 As System.Windows.Forms.Panel
		Private panel8 As System.Windows.Forms.Panel
		Private panel9 As System.Windows.Forms.Panel
		Private panel10 As System.Windows.Forms.Panel
		Private panel11 As System.Windows.Forms.Panel
		Private panel12 As System.Windows.Forms.Panel
		Private panel13 As System.Windows.Forms.Panel
		Private panel14 As System.Windows.Forms.Panel
		Private panel15 As System.Windows.Forms.Panel
		Private panel16 As System.Windows.Forms.Panel
		Private panel17 As System.Windows.Forms.Panel
		Private panel18 As System.Windows.Forms.Panel
		Private panel19 As System.Windows.Forms.Panel
		Private panel20 As System.Windows.Forms.Panel
		Private panel21 As System.Windows.Forms.Panel
		Private panel22 As System.Windows.Forms.Panel
		Private panel23 As System.Windows.Forms.Panel
		Private menuStrip1 As System.Windows.Forms.MenuStrip
		Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents saveTemplateToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents saveImagesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private optionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents changeScannerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents optionsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private tabInformation As System.Windows.Forms.TabPage
		Private infoPanel As InfoPanel
		Private WithEvents exportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private WithEvents editRequiredInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private nfvLeftFour As Neurotec.Biometrics.Gui.NFingerView
		Private nfvThumbs As Neurotec.Biometrics.Gui.NFingerView
		Private nfvRightFour As Neurotec.Biometrics.Gui.NFingerView

	End Class
End Namespace

