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
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
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
		Me.menuStrip = New System.Windows.Forms.MenuStrip
		Me.deviceManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.closeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.deviceManagerToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
		Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.deviceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.connectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.disconnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.deviceToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
		Me.showPluginToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.helpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.aboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
		Me.mainSplitContainer = New System.Windows.Forms.SplitContainer
		Me.topSplitContainer = New System.Windows.Forms.SplitContainer
		Me.deviceTreeView = New System.Windows.Forms.TreeView
		Me.endSequenceButton = New System.Windows.Forms.Button
		Me.startSequenceButton = New System.Windows.Forms.Button
		Me.customizeFormatButton = New System.Windows.Forms.Button
		Me.formatsComboBox = New System.Windows.Forms.ComboBox
		Me.cbGatherImages = New System.Windows.Forms.CheckBox
		Me.lblMiliseconds = New System.Windows.Forms.Label
		Me.tbMiliseconds = New System.Windows.Forms.TextBox
		Me.cbUseTimeout = New System.Windows.Forms.CheckBox
		Me.cbAutomatic = New System.Windows.Forms.CheckBox
		Me.rlCheckBox = New System.Windows.Forms.CheckBox
		Me.rrCheckBox = New System.Windows.Forms.CheckBox
		Me.rmCheckBox = New System.Windows.Forms.CheckBox
		Me.riCheckBox = New System.Windows.Forms.CheckBox
		Me.rtCheckBox = New System.Windows.Forms.CheckBox
		Me.ltCheckBox = New System.Windows.Forms.CheckBox
		Me.liCheckBox = New System.Windows.Forms.CheckBox
		Me.lmCheckBox = New System.Windows.Forms.CheckBox
		Me.lrCheckBox = New System.Windows.Forms.CheckBox
		Me.llCheckBox = New System.Windows.Forms.CheckBox
		Me.biometricDeviceImpressionTypeComboBox = New System.Windows.Forms.ComboBox
		Me.biometricDevicePositionComboBox = New System.Windows.Forms.ComboBox
		Me.deviceCaptureButton = New System.Windows.Forms.Button
		Me.bottomSplitContainer = New System.Windows.Forms.SplitContainer
		Me.devicePropertyGrid = New System.Windows.Forms.PropertyGrid
		Me.typeLabel = New System.Windows.Forms.Label
		Me.logRichTextBox = New System.Windows.Forms.RichTextBox
		Me.menuStrip.SuspendLayout()
		Me.mainSplitContainer.Panel1.SuspendLayout()
		Me.mainSplitContainer.Panel2.SuspendLayout()
		Me.mainSplitContainer.SuspendLayout()
		Me.topSplitContainer.Panel1.SuspendLayout()
		Me.topSplitContainer.Panel2.SuspendLayout()
		Me.topSplitContainer.SuspendLayout()
		Me.bottomSplitContainer.Panel1.SuspendLayout()
		Me.bottomSplitContainer.Panel2.SuspendLayout()
		Me.bottomSplitContainer.SuspendLayout()
		Me.SuspendLayout()
		'
		'menuStrip
		'
		Me.menuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.deviceManagerToolStripMenuItem, Me.deviceToolStripMenuItem, Me.helpToolStripMenuItem})
		Me.menuStrip.Location = New System.Drawing.Point(0, 0)
		Me.menuStrip.Name = "menuStrip"
		Me.menuStrip.Size = New System.Drawing.Size(865, 24)
		Me.menuStrip.TabIndex = 0
		Me.menuStrip.Text = "Main"
		'
		'deviceManagerToolStripMenuItem
		'
		Me.deviceManagerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.newToolStripMenuItem, Me.closeToolStripMenuItem, Me.deviceManagerToolStripSeparator2, Me.exitToolStripMenuItem})
		Me.deviceManagerToolStripMenuItem.Name = "deviceManagerToolStripMenuItem"
		Me.deviceManagerToolStripMenuItem.Size = New System.Drawing.Size(104, 20)
		Me.deviceManagerToolStripMenuItem.Text = "Device &manager"
		'
		'newToolStripMenuItem
		'
		Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
		Me.newToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
		Me.newToolStripMenuItem.Text = "&New"
		'
		'closeToolStripMenuItem
		'
		Me.closeToolStripMenuItem.Name = "closeToolStripMenuItem"
		Me.closeToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
		Me.closeToolStripMenuItem.Text = "&Close"
		'
		'deviceManagerToolStripSeparator2
		'
		Me.deviceManagerToolStripSeparator2.Name = "deviceManagerToolStripSeparator2"
		Me.deviceManagerToolStripSeparator2.Size = New System.Drawing.Size(100, 6)
		'
		'exitToolStripMenuItem
		'
		Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		Me.exitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
		Me.exitToolStripMenuItem.Text = "E&xit"
		'
		'deviceToolStripMenuItem
		'
		Me.deviceToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.connectToolStripMenuItem, Me.disconnectToolStripMenuItem, Me.deviceToolStripSeparator1, Me.showPluginToolStripMenuItem})
		Me.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem"
		Me.deviceToolStripMenuItem.Size = New System.Drawing.Size(54, 20)
		Me.deviceToolStripMenuItem.Text = "&Device"
		'
		'connectToolStripMenuItem
		'
		Me.connectToolStripMenuItem.Name = "connectToolStripMenuItem"
		Me.connectToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
		Me.connectToolStripMenuItem.Text = "&Connect..."
		'
		'disconnectToolStripMenuItem
		'
		Me.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem"
		Me.disconnectToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
		Me.disconnectToolStripMenuItem.Text = "&Disconnect"
		'
		'deviceToolStripSeparator1
		'
		Me.deviceToolStripSeparator1.Name = "deviceToolStripSeparator1"
		Me.deviceToolStripSeparator1.Size = New System.Drawing.Size(137, 6)
		'
		'showPluginToolStripMenuItem
		'
		Me.showPluginToolStripMenuItem.Name = "showPluginToolStripMenuItem"
		Me.showPluginToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
		Me.showPluginToolStripMenuItem.Text = "Show &plugin"
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
		Me.aboutToolStripMenuItem.Text = "About"
		'
		'mainSplitContainer
		'
		Me.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.mainSplitContainer.Location = New System.Drawing.Point(0, 24)
		Me.mainSplitContainer.Name = "mainSplitContainer"
		Me.mainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'mainSplitContainer.Panel1
		'
		Me.mainSplitContainer.Panel1.Controls.Add(Me.topSplitContainer)
		'
		'mainSplitContainer.Panel2
		'
		Me.mainSplitContainer.Panel2.Controls.Add(Me.bottomSplitContainer)
		Me.mainSplitContainer.Size = New System.Drawing.Size(865, 705)
		Me.mainSplitContainer.SplitterDistance = 364
		Me.mainSplitContainer.TabIndex = 1
		'
		'topSplitContainer
		'
		Me.topSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.topSplitContainer.Location = New System.Drawing.Point(0, 0)
		Me.topSplitContainer.Name = "topSplitContainer"
		'
		'topSplitContainer.Panel1
		'
		Me.topSplitContainer.Panel1.Controls.Add(Me.deviceTreeView)
		'
		'topSplitContainer.Panel2
		'
		Me.topSplitContainer.Panel2.Controls.Add(Me.endSequenceButton)
		Me.topSplitContainer.Panel2.Controls.Add(Me.startSequenceButton)
		Me.topSplitContainer.Panel2.Controls.Add(Me.customizeFormatButton)
		Me.topSplitContainer.Panel2.Controls.Add(Me.formatsComboBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.cbGatherImages)
		Me.topSplitContainer.Panel2.Controls.Add(Me.lblMiliseconds)
		Me.topSplitContainer.Panel2.Controls.Add(Me.tbMiliseconds)
		Me.topSplitContainer.Panel2.Controls.Add(Me.cbUseTimeout)
		Me.topSplitContainer.Panel2.Controls.Add(Me.cbAutomatic)
		Me.topSplitContainer.Panel2.Controls.Add(Me.rlCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.rrCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.rmCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.riCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.rtCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.ltCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.liCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.lmCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.lrCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.llCheckBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.biometricDeviceImpressionTypeComboBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.biometricDevicePositionComboBox)
		Me.topSplitContainer.Panel2.Controls.Add(Me.deviceCaptureButton)
		Me.topSplitContainer.Size = New System.Drawing.Size(865, 364)
		Me.topSplitContainer.SplitterDistance = 288
		Me.topSplitContainer.TabIndex = 0
		'
		'deviceTreeView
		'
		Me.deviceTreeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.deviceTreeView.HideSelection = False
		Me.deviceTreeView.Location = New System.Drawing.Point(0, 0)
		Me.deviceTreeView.Name = "deviceTreeView"
		Me.deviceTreeView.Size = New System.Drawing.Size(288, 364)
		Me.deviceTreeView.TabIndex = 0
		'
		'endSequenceButton
		'
		Me.endSequenceButton.Location = New System.Drawing.Point(234, 184)
		Me.endSequenceButton.Name = "endSequenceButton"
		Me.endSequenceButton.Size = New System.Drawing.Size(93, 23)
		Me.endSequenceButton.TabIndex = 23
		Me.endSequenceButton.Text = "End sequence"
		Me.endSequenceButton.UseVisualStyleBackColor = True
		'
		'startSequenceButton
		'
		Me.startSequenceButton.Location = New System.Drawing.Point(135, 184)
		Me.startSequenceButton.Name = "startSequenceButton"
		Me.startSequenceButton.Size = New System.Drawing.Size(93, 23)
		Me.startSequenceButton.TabIndex = 22
		Me.startSequenceButton.Text = "Start sequence"
		Me.startSequenceButton.UseVisualStyleBackColor = True
		'
		'customizeFormatButton
		'
		Me.customizeFormatButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.customizeFormatButton.Location = New System.Drawing.Point(333, 143)
		Me.customizeFormatButton.Name = "customizeFormatButton"
		Me.customizeFormatButton.Size = New System.Drawing.Size(75, 23)
		Me.customizeFormatButton.TabIndex = 19
		Me.customizeFormatButton.Text = "Custom..."
		Me.customizeFormatButton.UseVisualStyleBackColor = True
		'
		'formatsComboBox
		'
		Me.formatsComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.formatsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.formatsComboBox.FormattingEnabled = True
		Me.formatsComboBox.Location = New System.Drawing.Point(14, 145)
		Me.formatsComboBox.Name = "formatsComboBox"
		Me.formatsComboBox.Size = New System.Drawing.Size(313, 21)
		Me.formatsComboBox.TabIndex = 18
		'
		'cbGatherImages
		'
		Me.cbGatherImages.AutoSize = True
		Me.cbGatherImages.Location = New System.Drawing.Point(14, 112)
		Me.cbGatherImages.Name = "cbGatherImages"
		Me.cbGatherImages.Size = New System.Drawing.Size(94, 17)
		Me.cbGatherImages.TabIndex = 17
		Me.cbGatherImages.Text = "&Gather images"
		Me.cbGatherImages.UseVisualStyleBackColor = True
		Me.cbGatherImages.Visible = False
		'
		'lblMiliseconds
		'
		Me.lblMiliseconds.AutoSize = True
		Me.lblMiliseconds.Location = New System.Drawing.Point(175, 90)
		Me.lblMiliseconds.Name = "lblMiliseconds"
		Me.lblMiliseconds.Size = New System.Drawing.Size(20, 13)
		Me.lblMiliseconds.TabIndex = 16
		Me.lblMiliseconds.Text = "ms"
		'
		'tbMiliseconds
		'
		Me.tbMiliseconds.Enabled = False
		Me.tbMiliseconds.Location = New System.Drawing.Point(102, 87)
		Me.tbMiliseconds.Name = "tbMiliseconds"
		Me.tbMiliseconds.Size = New System.Drawing.Size(67, 20)
		Me.tbMiliseconds.TabIndex = 15
		Me.tbMiliseconds.Text = "0"
		Me.tbMiliseconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		'
		'cbUseTimeout
		'
		Me.cbUseTimeout.AutoSize = True
		Me.cbUseTimeout.Location = New System.Drawing.Point(14, 89)
		Me.cbUseTimeout.Name = "cbUseTimeout"
		Me.cbUseTimeout.Size = New System.Drawing.Size(82, 17)
		Me.cbUseTimeout.TabIndex = 14
		Me.cbUseTimeout.Text = "&Use timeout"
		Me.cbUseTimeout.UseVisualStyleBackColor = True
		'
		'cbAutomatic
		'
		Me.cbAutomatic.AutoSize = True
		Me.cbAutomatic.Checked = True
		Me.cbAutomatic.CheckState = System.Windows.Forms.CheckState.Checked
		Me.cbAutomatic.Location = New System.Drawing.Point(14, 65)
		Me.cbAutomatic.Name = "cbAutomatic"
		Me.cbAutomatic.Size = New System.Drawing.Size(73, 17)
		Me.cbAutomatic.TabIndex = 13
		Me.cbAutomatic.Text = "&Automatic"
		Me.cbAutomatic.UseVisualStyleBackColor = True
		'
		'rlCheckBox
		'
		Me.rlCheckBox.AutoSize = True
		Me.rlCheckBox.Location = New System.Drawing.Point(454, 46)
		Me.rlCheckBox.Name = "rlCheckBox"
		Me.rlCheckBox.Size = New System.Drawing.Size(40, 17)
		Me.rlCheckBox.TabIndex = 12
		Me.rlCheckBox.Text = "RL"
		Me.rlCheckBox.UseVisualStyleBackColor = True
		'
		'rrCheckBox
		'
		Me.rrCheckBox.AutoSize = True
		Me.rrCheckBox.Location = New System.Drawing.Point(439, 28)
		Me.rrCheckBox.Name = "rrCheckBox"
		Me.rrCheckBox.Size = New System.Drawing.Size(42, 17)
		Me.rrCheckBox.TabIndex = 11
		Me.rrCheckBox.Text = "RR"
		Me.rrCheckBox.UseVisualStyleBackColor = True
		'
		'rmCheckBox
		'
		Me.rmCheckBox.AutoSize = True
		Me.rmCheckBox.Location = New System.Drawing.Point(414, 15)
		Me.rmCheckBox.Name = "rmCheckBox"
		Me.rmCheckBox.Size = New System.Drawing.Size(43, 17)
		Me.rmCheckBox.TabIndex = 10
		Me.rmCheckBox.Text = "RM"
		Me.rmCheckBox.UseVisualStyleBackColor = True
		'
		'riCheckBox
		'
		Me.riCheckBox.AutoSize = True
		Me.riCheckBox.Location = New System.Drawing.Point(395, 32)
		Me.riCheckBox.Name = "riCheckBox"
		Me.riCheckBox.Size = New System.Drawing.Size(37, 17)
		Me.riCheckBox.TabIndex = 9
		Me.riCheckBox.Text = "RI"
		Me.riCheckBox.UseVisualStyleBackColor = True
		'
		'rtCheckBox
		'
		Me.rtCheckBox.AutoSize = True
		Me.rtCheckBox.Location = New System.Drawing.Point(385, 64)
		Me.rtCheckBox.Name = "rtCheckBox"
		Me.rtCheckBox.Size = New System.Drawing.Size(41, 17)
		Me.rtCheckBox.TabIndex = 8
		Me.rtCheckBox.Text = "RT"
		Me.rtCheckBox.UseVisualStyleBackColor = True
		'
		'ltCheckBox
		'
		Me.ltCheckBox.AutoSize = True
		Me.ltCheckBox.Location = New System.Drawing.Point(341, 64)
		Me.ltCheckBox.Name = "ltCheckBox"
		Me.ltCheckBox.Size = New System.Drawing.Size(39, 17)
		Me.ltCheckBox.TabIndex = 7
		Me.ltCheckBox.Text = "LT"
		Me.ltCheckBox.UseVisualStyleBackColor = True
		'
		'liCheckBox
		'
		Me.liCheckBox.AutoSize = True
		Me.liCheckBox.Location = New System.Drawing.Point(327, 32)
		Me.liCheckBox.Name = "liCheckBox"
		Me.liCheckBox.Size = New System.Drawing.Size(35, 17)
		Me.liCheckBox.TabIndex = 6
		Me.liCheckBox.Text = "LI"
		Me.liCheckBox.UseVisualStyleBackColor = True
		'
		'lmCheckBox
		'
		Me.lmCheckBox.AutoSize = True
		Me.lmCheckBox.Location = New System.Drawing.Point(300, 15)
		Me.lmCheckBox.Name = "lmCheckBox"
		Me.lmCheckBox.Size = New System.Drawing.Size(41, 17)
		Me.lmCheckBox.TabIndex = 5
		Me.lmCheckBox.Text = "LM"
		Me.lmCheckBox.UseVisualStyleBackColor = True
		'
		'lrCheckBox
		'
		Me.lrCheckBox.AutoSize = True
		Me.lrCheckBox.Location = New System.Drawing.Point(273, 28)
		Me.lrCheckBox.Name = "lrCheckBox"
		Me.lrCheckBox.Size = New System.Drawing.Size(40, 17)
		Me.lrCheckBox.TabIndex = 4
		Me.lrCheckBox.Text = "LR"
		Me.lrCheckBox.UseVisualStyleBackColor = True
		'
		'llCheckBox
		'
		Me.llCheckBox.AutoSize = True
		Me.llCheckBox.Location = New System.Drawing.Point(250, 46)
		Me.llCheckBox.Name = "llCheckBox"
		Me.llCheckBox.Size = New System.Drawing.Size(38, 17)
		Me.llCheckBox.TabIndex = 3
		Me.llCheckBox.Text = "LL"
		Me.llCheckBox.UseVisualStyleBackColor = True
		'
		'biometricDeviceImpressionTypeComboBox
		'
		Me.biometricDeviceImpressionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.biometricDeviceImpressionTypeComboBox.FormattingEnabled = True
		Me.biometricDeviceImpressionTypeComboBox.Location = New System.Drawing.Point(14, 11)
		Me.biometricDeviceImpressionTypeComboBox.Name = "biometricDeviceImpressionTypeComboBox"
		Me.biometricDeviceImpressionTypeComboBox.Size = New System.Drawing.Size(214, 21)
		Me.biometricDeviceImpressionTypeComboBox.TabIndex = 1
		'
		'biometricDevicePositionComboBox
		'
		Me.biometricDevicePositionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.biometricDevicePositionComboBox.FormattingEnabled = True
		Me.biometricDevicePositionComboBox.Location = New System.Drawing.Point(14, 37)
		Me.biometricDevicePositionComboBox.Name = "biometricDevicePositionComboBox"
		Me.biometricDevicePositionComboBox.Size = New System.Drawing.Size(214, 21)
		Me.biometricDevicePositionComboBox.TabIndex = 2
		'
		'deviceCaptureButton
		'
		Me.deviceCaptureButton.Location = New System.Drawing.Point(14, 184)
		Me.deviceCaptureButton.Name = "deviceCaptureButton"
		Me.deviceCaptureButton.Size = New System.Drawing.Size(75, 23)
		Me.deviceCaptureButton.TabIndex = 0
		Me.deviceCaptureButton.Text = "Capture"
		Me.deviceCaptureButton.UseVisualStyleBackColor = True
		'
		'bottomSplitContainer
		'
		Me.bottomSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
		Me.bottomSplitContainer.Location = New System.Drawing.Point(0, 0)
		Me.bottomSplitContainer.Name = "bottomSplitContainer"
		'
		'bottomSplitContainer.Panel1
		'
		Me.bottomSplitContainer.Panel1.Controls.Add(Me.devicePropertyGrid)
		Me.bottomSplitContainer.Panel1.Controls.Add(Me.typeLabel)
		'
		'bottomSplitContainer.Panel2
		'
		Me.bottomSplitContainer.Panel2.Controls.Add(Me.logRichTextBox)
		Me.bottomSplitContainer.Size = New System.Drawing.Size(865, 337)
		Me.bottomSplitContainer.SplitterDistance = 288
		Me.bottomSplitContainer.TabIndex = 0
		'
		'devicePropertyGrid
		'
		Me.devicePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.devicePropertyGrid.Location = New System.Drawing.Point(0, 13)
		Me.devicePropertyGrid.Name = "devicePropertyGrid"
		Me.devicePropertyGrid.Size = New System.Drawing.Size(288, 324)
		Me.devicePropertyGrid.TabIndex = 0
		'
		'typeLabel
		'
		Me.typeLabel.Dock = System.Windows.Forms.DockStyle.Top
		Me.typeLabel.Location = New System.Drawing.Point(0, 0)
		Me.typeLabel.Name = "typeLabel"
		Me.typeLabel.Size = New System.Drawing.Size(288, 13)
		Me.typeLabel.TabIndex = 1
		Me.typeLabel.Text = "Type"
		'
		'logRichTextBox
		'
		Me.logRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill
		Me.logRichTextBox.Location = New System.Drawing.Point(0, 0)
		Me.logRichTextBox.Name = "logRichTextBox"
		Me.logRichTextBox.ReadOnly = True
		Me.logRichTextBox.Size = New System.Drawing.Size(573, 337)
		Me.logRichTextBox.TabIndex = 1
		Me.logRichTextBox.Text = ""
		'
		'MainForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(865, 729)
		Me.Controls.Add(Me.mainSplitContainer)
		Me.Controls.Add(Me.menuStrip)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "MainForm"
		Me.Text = "Device Manager"
		Me.menuStrip.ResumeLayout(False)
		Me.menuStrip.PerformLayout()
		Me.mainSplitContainer.Panel1.ResumeLayout(False)
		Me.mainSplitContainer.Panel2.ResumeLayout(False)
		Me.mainSplitContainer.ResumeLayout(False)
		Me.topSplitContainer.Panel1.ResumeLayout(False)
		Me.topSplitContainer.Panel2.ResumeLayout(False)
		Me.topSplitContainer.Panel2.PerformLayout()
		Me.topSplitContainer.ResumeLayout(False)
		Me.bottomSplitContainer.Panel1.ResumeLayout(False)
		Me.bottomSplitContainer.Panel2.ResumeLayout(False)
		Me.bottomSplitContainer.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private menuStrip As System.Windows.Forms.MenuStrip
	Private helpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents aboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private mainSplitContainer As System.Windows.Forms.SplitContainer
	Private topSplitContainer As System.Windows.Forms.SplitContainer
	Private WithEvents deviceTreeView As System.Windows.Forms.TreeView
	Private bottomSplitContainer As System.Windows.Forms.SplitContainer
	Private logRichTextBox As System.Windows.Forms.RichTextBox
	Private devicePropertyGrid As System.Windows.Forms.PropertyGrid
	Private deviceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents showPluginToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents deviceCaptureButton As System.Windows.Forms.Button
	Private WithEvents biometricDeviceImpressionTypeComboBox As System.Windows.Forms.ComboBox
	Private biometricDevicePositionComboBox As System.Windows.Forms.ComboBox
	Private rlCheckBox As System.Windows.Forms.CheckBox
	Private rrCheckBox As System.Windows.Forms.CheckBox
	Private rmCheckBox As System.Windows.Forms.CheckBox
	Private riCheckBox As System.Windows.Forms.CheckBox
	Private rtCheckBox As System.Windows.Forms.CheckBox
	Private ltCheckBox As System.Windows.Forms.CheckBox
	Private liCheckBox As System.Windows.Forms.CheckBox
	Private lmCheckBox As System.Windows.Forms.CheckBox
	Private lrCheckBox As System.Windows.Forms.CheckBox
	Private llCheckBox As System.Windows.Forms.CheckBox
	Private deviceManagerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents closeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private lblMiliseconds As System.Windows.Forms.Label
	Private tbMiliseconds As System.Windows.Forms.TextBox
	Private WithEvents cbUseTimeout As System.Windows.Forms.CheckBox
	Private cbAutomatic As System.Windows.Forms.CheckBox
	Private typeLabel As System.Windows.Forms.Label
	Private cbGatherImages As System.Windows.Forms.CheckBox
	Private formatsComboBox As System.Windows.Forms.ComboBox
	Private WithEvents customizeFormatButton As System.Windows.Forms.Button
	Private WithEvents endSequenceButton As System.Windows.Forms.Button
	Private WithEvents startSequenceButton As System.Windows.Forms.Button
	Private deviceManagerToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents connectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents disconnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	Private deviceToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
End Class
