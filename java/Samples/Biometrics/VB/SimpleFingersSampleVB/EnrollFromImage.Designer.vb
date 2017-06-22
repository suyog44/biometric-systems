Imports Microsoft.VisualBasic
Imports System

Partial Public Class EnrollFromImage
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnrollFromImage))
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.fingerView2 = New Neurotec.Biometrics.Gui.NFingerView
		Me.fingerView1 = New Neurotec.Biometrics.Gui.NFingerView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.panel1 = New System.Windows.Forms.Panel
		Me.chbShowBinarizedImage = New System.Windows.Forms.CheckBox
		Me.lblQuality = New System.Windows.Forms.Label
		Me.saveTemplateButton = New System.Windows.Forms.Button
		Me.saveImageButton = New System.Windows.Forms.Button
		Me.panel2 = New System.Windows.Forms.Panel
		Me.openButton = New System.Windows.Forms.Button
		Me.defaultButton = New System.Windows.Forms.Button
		Me.extractFeaturesButton = New System.Windows.Forms.Button
		Me.thresholdNumericUpDown = New System.Windows.Forms.NumericUpDown
		Me.ThresholdLabel = New System.Windows.Forms.Label
		Me.LicensePanel1 = New Neurotec.Samples.LicensePanel
		Me.tableLayoutPanel1.SuspendLayout()
		Me.panel1.SuspendLayout()
		Me.panel2.SuspendLayout()
		CType(Me.thresholdNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.fingerView2, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.fingerView1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider1, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider2, 1, 1)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 86)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(688, 310)
		Me.tableLayoutPanel1.TabIndex = 18
		'
		'fingerView2
		'
		Me.fingerView2.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.fingerView2.BoundingRectColor = System.Drawing.Color.Red
		Me.fingerView2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingerView2.Location = New System.Drawing.Point(347, 3)
		Me.fingerView2.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView2.Name = "fingerView2"
		Me.fingerView2.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView2.ResultImageColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.fingerView2.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView2.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView2.ShownImage = Neurotec.Biometrics.Gui.ShownImage.Result
		Me.fingerView2.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView2.Size = New System.Drawing.Size(338, 275)
		Me.fingerView2.TabIndex = 4
		Me.fingerView2.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView2.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView2.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView2.TreeWidth = 2
		'
		'fingerView1
		'
		Me.fingerView1.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.fingerView1.BoundingRectColor = System.Drawing.Color.Red
		Me.fingerView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingerView1.Location = New System.Drawing.Point(3, 3)
		Me.fingerView1.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView1.Name = "fingerView1"
		Me.fingerView1.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView1.ResultImageColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.fingerView1.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView1.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView1.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView1.Size = New System.Drawing.Size(338, 275)
		Me.fingerView1.TabIndex = 3
		Me.fingerView1.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView1.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView1.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView1.TreeWidth = 2
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(3, 284)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(338, 23)
		Me.nViewZoomSlider1.TabIndex = 5
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.fingerView1
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(347, 284)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(338, 23)
		Me.nViewZoomSlider2.TabIndex = 6
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.fingerView2
		'
		'panel1
		'
		Me.panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.panel1.Controls.Add(Me.chbShowBinarizedImage)
		Me.panel1.Controls.Add(Me.lblQuality)
		Me.panel1.Controls.Add(Me.saveTemplateButton)
		Me.panel1.Controls.Add(Me.saveImageButton)
		Me.panel1.Location = New System.Drawing.Point(3, 402)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(685, 29)
		Me.panel1.TabIndex = 16
		'
		'chbShowBinarizedImage
		'
		Me.chbShowBinarizedImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.chbShowBinarizedImage.AutoSize = True
		Me.chbShowBinarizedImage.Checked = True
		Me.chbShowBinarizedImage.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbShowBinarizedImage.Enabled = False
		Me.chbShowBinarizedImage.Location = New System.Drawing.Point(342, 7)
		Me.chbShowBinarizedImage.Name = "chbShowBinarizedImage"
		Me.chbShowBinarizedImage.Size = New System.Drawing.Size(136, 17)
		Me.chbShowBinarizedImage.TabIndex = 7
		Me.chbShowBinarizedImage.Text = "Show binarized image"
		Me.chbShowBinarizedImage.UseVisualStyleBackColor = True
		'
		'lblQuality
		'
		Me.lblQuality.Location = New System.Drawing.Point(3, 3)
		Me.lblQuality.Name = "lblQuality"
		Me.lblQuality.Size = New System.Drawing.Size(170, 23)
		Me.lblQuality.TabIndex = 1
		'
		'saveTemplateButton
		'
		Me.saveTemplateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.saveTemplateButton.Enabled = False
		Me.saveTemplateButton.Location = New System.Drawing.Point(586, 3)
		Me.saveTemplateButton.Name = "saveTemplateButton"
		Me.saveTemplateButton.Size = New System.Drawing.Size(96, 23)
		Me.saveTemplateButton.TabIndex = 6
		Me.saveTemplateButton.Text = "&Save Template"
		Me.toolTip.SetToolTip(Me.saveTemplateButton, "Save extracted template to file")
		Me.saveTemplateButton.UseVisualStyleBackColor = True
		'
		'saveImageButton
		'
		Me.saveImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.saveImageButton.Enabled = False
		Me.saveImageButton.Image = CType(resources.GetObject("saveImageButton.Image"), System.Drawing.Image)
		Me.saveImageButton.Location = New System.Drawing.Point(484, 3)
		Me.saveImageButton.Name = "saveImageButton"
		Me.saveImageButton.Size = New System.Drawing.Size(96, 23)
		Me.saveImageButton.TabIndex = 1
		Me.saveImageButton.Text = "Save Image"
		Me.saveImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.saveImageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.toolTip.SetToolTip(Me.saveImageButton, "Save Binarized Image")
		Me.saveImageButton.UseVisualStyleBackColor = True
		'
		'panel2
		'
		Me.panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.panel2.Controls.Add(Me.openButton)
		Me.panel2.Controls.Add(Me.defaultButton)
		Me.panel2.Controls.Add(Me.extractFeaturesButton)
		Me.panel2.Controls.Add(Me.thresholdNumericUpDown)
		Me.panel2.Controls.Add(Me.ThresholdLabel)
		Me.panel2.Location = New System.Drawing.Point(0, 48)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(691, 32)
		Me.panel2.TabIndex = 17
		'
		'openButton
		'
		Me.openButton.Image = CType(resources.GetObject("openButton.Image"), System.Drawing.Image)
		Me.openButton.Location = New System.Drawing.Point(3, 3)
		Me.openButton.Name = "openButton"
		Me.openButton.Size = New System.Drawing.Size(93, 23)
		Me.openButton.TabIndex = 0
		Me.openButton.Tag = "Open"
		Me.openButton.Text = "Open Image"
		Me.openButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.openButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.toolTip.SetToolTip(Me.openButton, "Open Fingerprint Image")
		Me.openButton.UseVisualStyleBackColor = True
		'
		'defaultButton
		'
		Me.defaultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.defaultButton.Enabled = False
		Me.defaultButton.Location = New System.Drawing.Point(528, 3)
		Me.defaultButton.Name = "defaultButton"
		Me.defaultButton.Size = New System.Drawing.Size(62, 23)
		Me.defaultButton.TabIndex = 10
		Me.defaultButton.Text = "Default"
		Me.defaultButton.UseVisualStyleBackColor = True
		'
		'extractFeaturesButton
		'
		Me.extractFeaturesButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.extractFeaturesButton.Enabled = False
		Me.extractFeaturesButton.Location = New System.Drawing.Point(595, 3)
		Me.extractFeaturesButton.Name = "extractFeaturesButton"
		Me.extractFeaturesButton.Size = New System.Drawing.Size(93, 23)
		Me.extractFeaturesButton.TabIndex = 2
		Me.extractFeaturesButton.Text = "&Extract Features"
		Me.extractFeaturesButton.UseVisualStyleBackColor = True
		'
		'thresholdNumericUpDown
		'
		Me.thresholdNumericUpDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.thresholdNumericUpDown.Location = New System.Drawing.Point(474, 6)
		Me.thresholdNumericUpDown.Name = "thresholdNumericUpDown"
		Me.thresholdNumericUpDown.Size = New System.Drawing.Size(48, 20)
		Me.thresholdNumericUpDown.TabIndex = 9
		Me.thresholdNumericUpDown.Value = New Decimal(New Integer() {39, 0, 0, 0})
		'
		'ThresholdLabel
		'
		Me.ThresholdLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ThresholdLabel.AutoSize = True
		Me.ThresholdLabel.Location = New System.Drawing.Point(411, 8)
		Me.ThresholdLabel.Name = "ThresholdLabel"
		Me.ThresholdLabel.Size = New System.Drawing.Size(57, 13)
		Me.ThresholdLabel.TabIndex = 8
		Me.ThresholdLabel.Text = "Threshold:"
		'
		'LicensePanel1
		'
		Me.LicensePanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LicensePanel1.Location = New System.Drawing.Point(0, 3)
		Me.LicensePanel1.Name = "LicensePanel1"
		Me.LicensePanel1.OptionalComponents = "Images.WSQ"
		Me.LicensePanel1.RequiredComponents = "Biometrics.FingerExtraction"
		Me.LicensePanel1.Size = New System.Drawing.Size(691, 39)
		Me.LicensePanel1.TabIndex = 14
		'
		'EnrollFromImage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.panel1)
		Me.Controls.Add(Me.panel2)
		Me.Controls.Add(Me.LicensePanel1)
		Me.Name = "EnrollFromImage"
		Me.Size = New System.Drawing.Size(691, 437)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.panel1.ResumeLayout(False)
		Me.panel1.PerformLayout()
		Me.panel2.ResumeLayout(False)
		Me.panel2.PerformLayout()
		CType(Me.thresholdNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private toolTip As System.Windows.Forms.ToolTip
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private licensePanel As LicensePanel
	Friend WithEvents LicensePanel1 As Neurotec.Samples.LicensePanel
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents fingerView2 As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents fingerView1 As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents panel1 As System.Windows.Forms.Panel
	Private WithEvents chbShowBinarizedImage As System.Windows.Forms.CheckBox
	Private WithEvents lblQuality As System.Windows.Forms.Label
	Private WithEvents saveTemplateButton As System.Windows.Forms.Button
	Private WithEvents saveImageButton As System.Windows.Forms.Button
	Private WithEvents panel2 As System.Windows.Forms.Panel
	Private WithEvents openButton As System.Windows.Forms.Button
	Private WithEvents defaultButton As System.Windows.Forms.Button
	Private WithEvents extractFeaturesButton As System.Windows.Forms.Button
	Private WithEvents thresholdNumericUpDown As System.Windows.Forms.NumericUpDown
	Private WithEvents ThresholdLabel As System.Windows.Forms.Label
End Class

