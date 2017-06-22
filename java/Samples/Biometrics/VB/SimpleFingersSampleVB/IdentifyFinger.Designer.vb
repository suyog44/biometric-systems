Imports Microsoft.VisualBasic
Imports System

Partial Public Class IdentifyFinger
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

#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IdentifyFinger))
		Me.openTemplatesButton = New System.Windows.Forms.Button
		Me.defaultButton = New System.Windows.Forms.Button
		Me.thresholdNumericUpDown = New System.Windows.Forms.NumericUpDown
		Me.ThresholdLabel = New System.Windows.Forms.Label
		Me.defaultMatchingFARButton = New System.Windows.Forms.Button
		Me.matchingFarComboBox = New System.Windows.Forms.ComboBox
		Me.groupBox3 = New System.Windows.Forms.GroupBox
		Me.listView = New System.Windows.Forms.ListView
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.label2 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.identifyButton = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.templatesCountLabel = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.openImageButton = New System.Windows.Forms.Button
		Me.LicensePanel1 = New Neurotec.Samples.LicensePanel
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.fingerView = New Neurotec.Biometrics.Gui.NFingerView
		Me.chbShowBinarizedImage = New System.Windows.Forms.CheckBox
		Me.panel = New System.Windows.Forms.Panel
		Me.fileForIdentificationLabel = New System.Windows.Forms.Label
		CType(Me.thresholdNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.groupBox3.SuspendLayout()
		Me.groupBox1.SuspendLayout()
		Me.groupBox2.SuspendLayout()
		Me.panel.SuspendLayout()
		Me.SuspendLayout()
		'
		'openTemplatesButton
		'
		Me.openTemplatesButton.Image = CType(resources.GetObject("openTemplatesButton.Image"), System.Drawing.Image)
		Me.openTemplatesButton.Location = New System.Drawing.Point(6, 24)
		Me.openTemplatesButton.Name = "openTemplatesButton"
		Me.openTemplatesButton.Size = New System.Drawing.Size(30, 23)
		Me.openTemplatesButton.TabIndex = 5
		Me.toolTip.SetToolTip(Me.openTemplatesButton, "Open templates files (*.dat) ")
		Me.openTemplatesButton.UseVisualStyleBackColor = True
		'
		'defaultButton
		'
		Me.defaultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.defaultButton.Enabled = False
		Me.defaultButton.Location = New System.Drawing.Point(224, 167)
		Me.defaultButton.Name = "defaultButton"
		Me.defaultButton.Size = New System.Drawing.Size(63, 23)
		Me.defaultButton.TabIndex = 10
		Me.defaultButton.Text = "Default"
		Me.defaultButton.UseVisualStyleBackColor = True
		'
		'thresholdNumericUpDown
		'
		Me.thresholdNumericUpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.thresholdNumericUpDown.Location = New System.Drawing.Point(170, 168)
		Me.thresholdNumericUpDown.Name = "thresholdNumericUpDown"
		Me.thresholdNumericUpDown.Size = New System.Drawing.Size(48, 20)
		Me.thresholdNumericUpDown.TabIndex = 9
		Me.thresholdNumericUpDown.Value = New Decimal(New Integer() {39, 0, 0, 0})
		'
		'ThresholdLabel
		'
		Me.ThresholdLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.ThresholdLabel.AutoSize = True
		Me.ThresholdLabel.Location = New System.Drawing.Point(107, 172)
		Me.ThresholdLabel.Name = "ThresholdLabel"
		Me.ThresholdLabel.Size = New System.Drawing.Size(57, 13)
		Me.ThresholdLabel.TabIndex = 8
		Me.ThresholdLabel.Text = "Threshold:"
		'
		'defaultMatchingFARButton
		'
		Me.defaultMatchingFARButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.defaultMatchingFARButton.Location = New System.Drawing.Point(719, 19)
		Me.defaultMatchingFARButton.Name = "defaultMatchingFARButton"
		Me.defaultMatchingFARButton.Size = New System.Drawing.Size(63, 23)
		Me.defaultMatchingFARButton.TabIndex = 20
		Me.defaultMatchingFARButton.Text = "Default"
		Me.defaultMatchingFARButton.UseVisualStyleBackColor = True
		'
		'matchingFarComboBox
		'
		Me.matchingFarComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.matchingFarComboBox.FormattingEnabled = True
		Me.matchingFarComboBox.Location = New System.Drawing.Point(640, 21)
		Me.matchingFarComboBox.Name = "matchingFarComboBox"
		Me.matchingFarComboBox.Size = New System.Drawing.Size(73, 21)
		Me.matchingFarComboBox.TabIndex = 19
		'
		'groupBox3
		'
		Me.groupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox3.Controls.Add(Me.defaultMatchingFARButton)
		Me.groupBox3.Controls.Add(Me.matchingFarComboBox)
		Me.groupBox3.Controls.Add(Me.listView)
		Me.groupBox3.Controls.Add(Me.label2)
		Me.groupBox3.Controls.Add(Me.Label3)
		Me.groupBox3.Controls.Add(Me.identifyButton)
		Me.groupBox3.Location = New System.Drawing.Point(6, 335)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(788, 223)
		Me.groupBox3.TabIndex = 11
		Me.groupBox3.TabStop = False
		Me.groupBox3.Text = "Identification"
		'
		'listView
		'
		Me.listView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.listView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
		Me.listView.Location = New System.Drawing.Point(9, 48)
		Me.listView.Name = "listView"
		Me.listView.Size = New System.Drawing.Size(773, 169)
		Me.listView.TabIndex = 2
		Me.listView.UseCompatibleStateImageBehavior = False
		Me.listView.View = System.Windows.Forms.View.Details
		'
		'columnHeader1
		'
		Me.columnHeader1.Text = "ID"
		Me.columnHeader1.Width = 300
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Score"
		Me.columnHeader2.Width = 100
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(12, 56)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(0, 13)
		Me.label2.TabIndex = 1
		'
		'Label3
		'
		Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(556, 24)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(78, 13)
		Me.Label3.TabIndex = 8
		Me.Label3.Text = "Matching FAR:"
		'
		'identifyButton
		'
		Me.identifyButton.Enabled = False
		Me.identifyButton.Location = New System.Drawing.Point(9, 19)
		Me.identifyButton.Name = "identifyButton"
		Me.identifyButton.Size = New System.Drawing.Size(92, 23)
		Me.identifyButton.TabIndex = 0
		Me.identifyButton.Text = "&Identify"
		Me.identifyButton.UseVisualStyleBackColor = True
		'
		'templatesCountLabel
		'
		Me.templatesCountLabel.AutoSize = True
		Me.templatesCountLabel.Location = New System.Drawing.Point(142, 34)
		Me.templatesCountLabel.Name = "templatesCountLabel"
		Me.templatesCountLabel.Size = New System.Drawing.Size(82, 13)
		Me.templatesCountLabel.TabIndex = 7
		Me.templatesCountLabel.Text = "templates count"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(42, 34)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(94, 13)
		Me.label1.TabIndex = 6
		Me.label1.Text = "Templates loaded:"
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.templatesCountLabel)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.openTemplatesButton)
		Me.groupBox1.Location = New System.Drawing.Point(6, 51)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(788, 57)
		Me.groupBox1.TabIndex = 9
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Templates loading"
		'
		'openImageButton
		'
		Me.openImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.openImageButton.Image = CType(resources.GetObject("openImageButton.Image"), System.Drawing.Image)
		Me.openImageButton.Location = New System.Drawing.Point(9, 167)
		Me.openImageButton.Name = "openImageButton"
		Me.openImageButton.Size = New System.Drawing.Size(92, 23)
		Me.openImageButton.TabIndex = 8
		Me.openImageButton.Text = "Open"
		Me.openImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.openImageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.toolTip.SetToolTip(Me.openImageButton, "Open fingerprint image for indentification")
		Me.openImageButton.UseVisualStyleBackColor = True
		'
		'LicensePanel1
		'
		Me.LicensePanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LicensePanel1.Location = New System.Drawing.Point(0, 0)
		Me.LicensePanel1.Name = "LicensePanel1"
		Me.LicensePanel1.OptionalComponents = "Images.WSQ"
		Me.LicensePanel1.RequiredComponents = "Biometrics.FingerExtraction,Biometrics.FingerMatching"
		Me.LicensePanel1.Size = New System.Drawing.Size(791, 45)
		Me.LicensePanel1.TabIndex = 12
		'
		'groupBox2
		'
		Me.groupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox2.Controls.Add(Me.defaultButton)
		Me.groupBox2.Controls.Add(Me.thresholdNumericUpDown)
		Me.groupBox2.Controls.Add(Me.nViewZoomSlider1)
		Me.groupBox2.Controls.Add(Me.ThresholdLabel)
		Me.groupBox2.Controls.Add(Me.chbShowBinarizedImage)
		Me.groupBox2.Controls.Add(Me.panel)
		Me.groupBox2.Controls.Add(Me.fileForIdentificationLabel)
		Me.groupBox2.Controls.Add(Me.openImageButton)
		Me.groupBox2.Location = New System.Drawing.Point(3, 114)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(788, 215)
		Me.groupBox2.TabIndex = 13
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Image / template for identification"
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(505, 167)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(275, 23)
		Me.nViewZoomSlider1.TabIndex = 18
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.fingerView
		'
		'fingerView
		'
		Me.fingerView.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView.BoundingRectColor = System.Drawing.Color.Red
		Me.fingerView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingerView.Location = New System.Drawing.Point(0, 0)
		Me.fingerView.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView.Name = "fingerView"
		Me.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView.ResultImageColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView.ShownImage = Neurotec.Biometrics.Gui.ShownImage.Result
		Me.fingerView.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView.Size = New System.Drawing.Size(769, 139)
		Me.fingerView.TabIndex = 0
		Me.fingerView.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView.TreeWidth = 2
		'
		'chbShowBinarizedImage
		'
		Me.chbShowBinarizedImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.chbShowBinarizedImage.AutoSize = True
		Me.chbShowBinarizedImage.Checked = True
		Me.chbShowBinarizedImage.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbShowBinarizedImage.Enabled = False
		Me.chbShowBinarizedImage.Location = New System.Drawing.Point(363, 171)
		Me.chbShowBinarizedImage.Name = "chbShowBinarizedImage"
		Me.chbShowBinarizedImage.Size = New System.Drawing.Size(136, 17)
		Me.chbShowBinarizedImage.TabIndex = 12
		Me.chbShowBinarizedImage.Text = "Show binarized image"
		Me.chbShowBinarizedImage.UseVisualStyleBackColor = True
		'
		'panel
		'
		Me.panel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.panel.AutoScroll = True
		Me.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.panel.Controls.Add(Me.fingerView)
		Me.panel.Location = New System.Drawing.Point(9, 20)
		Me.panel.Name = "panel"
		Me.panel.Size = New System.Drawing.Size(773, 143)
		Me.panel.TabIndex = 11
		'
		'fileForIdentificationLabel
		'
		Me.fileForIdentificationLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.fileForIdentificationLabel.Location = New System.Drawing.Point(6, 199)
		Me.fileForIdentificationLabel.Name = "fileForIdentificationLabel"
		Me.fileForIdentificationLabel.Size = New System.Drawing.Size(675, 13)
		Me.fileForIdentificationLabel.TabIndex = 10
		'
		'IdentifyFinger
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.LicensePanel1)
		Me.Controls.Add(Me.groupBox3)
		Me.Controls.Add(Me.groupBox1)
		Me.Name = "IdentifyFinger"
		Me.Size = New System.Drawing.Size(794, 561)
		CType(Me.thresholdNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
		Me.groupBox3.ResumeLayout(False)
		Me.groupBox3.PerformLayout()
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox2.PerformLayout()
		Me.panel.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents openTemplatesButton As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
	Private WithEvents defaultButton As System.Windows.Forms.Button
	Private WithEvents thresholdNumericUpDown As System.Windows.Forms.NumericUpDown
	Private ThresholdLabel As System.Windows.Forms.Label
	Private WithEvents defaultMatchingFARButton As System.Windows.Forms.Button
	Private WithEvents matchingFarComboBox As System.Windows.Forms.ComboBox
	Private groupBox3 As System.Windows.Forms.GroupBox
	Private listView As System.Windows.Forms.ListView
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private label2 As System.Windows.Forms.Label
	Private WithEvents identifyButton As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private templatesCountLabel As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private licensePanel As LicensePanel
	Friend WithEvents LicensePanel1 As Neurotec.Samples.LicensePanel
	Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents fingerView As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents chbShowBinarizedImage As System.Windows.Forms.CheckBox
	Private WithEvents panel As System.Windows.Forms.Panel
	Private WithEvents fileForIdentificationLabel As System.Windows.Forms.Label
	Private WithEvents openImageButton As System.Windows.Forms.Button
	Private WithEvents Label3 As System.Windows.Forms.Label
End Class

