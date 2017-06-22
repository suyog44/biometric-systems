Imports Microsoft.VisualBasic
Imports System

Partial Public Class VerifyFinger
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerifyFinger))
		Me.templateRightLabel = New System.Windows.Forms.Label
		Me.templateLeftLabel = New System.Windows.Forms.Label
		Me.matchingFarLabel = New System.Windows.Forms.Label
		Me.matchingGroupBox = New System.Windows.Forms.GroupBox
		Me.defaultButton = New System.Windows.Forms.Button
		Me.matchingFarComboBox = New System.Windows.Forms.ComboBox
		Me.templateNameLabel2 = New System.Windows.Forms.Label
		Me.templateNameLabel1 = New System.Windows.Forms.Label
		Me.verifyButton = New System.Windows.Forms.Button
		Me.openImageButton2 = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.msgLabel = New System.Windows.Forms.Label
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.openImageButton1 = New System.Windows.Forms.Button
		Me.clearImagesButton = New System.Windows.Forms.Button
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.fingerView1 = New Neurotec.Biometrics.Gui.NFingerView
		Me.fingerView2 = New Neurotec.Biometrics.Gui.NFingerView
		Me.chbShowBinarizedImage2 = New System.Windows.Forms.CheckBox
		Me.chbShowBinarizedImage1 = New System.Windows.Forms.CheckBox
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.matchingGroupBox.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.tableLayoutPanel3.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'templateRightLabel
		'
		Me.templateRightLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateRightLabel.AutoSize = True
		Me.templateRightLabel.Location = New System.Drawing.Point(120, 485)
		Me.templateRightLabel.Name = "templateRightLabel"
		Me.templateRightLabel.Size = New System.Drawing.Size(64, 13)
		Me.templateRightLabel.TabIndex = 30
		Me.templateRightLabel.Text = "template left"
		'
		'templateLeftLabel
		'
		Me.templateLeftLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateLeftLabel.AutoSize = True
		Me.templateLeftLabel.Location = New System.Drawing.Point(120, 461)
		Me.templateLeftLabel.Name = "templateLeftLabel"
		Me.templateLeftLabel.Size = New System.Drawing.Size(64, 13)
		Me.templateLeftLabel.TabIndex = 29
		Me.templateLeftLabel.Text = "template left"
		'
		'matchingFarLabel
		'
		Me.matchingFarLabel.AutoSize = True
		Me.matchingFarLabel.Location = New System.Drawing.Point(11, 10)
		Me.matchingFarLabel.Name = "matchingFarLabel"
		Me.matchingFarLabel.Size = New System.Drawing.Size(78, 13)
		Me.matchingFarLabel.TabIndex = 18
		Me.matchingFarLabel.Text = "Matching &FAR:"
		'
		'matchingGroupBox
		'
		Me.matchingGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.matchingGroupBox.Controls.Add(Me.defaultButton)
		Me.matchingGroupBox.Controls.Add(Me.matchingFarLabel)
		Me.matchingGroupBox.Controls.Add(Me.matchingFarComboBox)
		Me.matchingGroupBox.Location = New System.Drawing.Point(242, 3)
		Me.matchingGroupBox.MaximumSize = New System.Drawing.Size(600, 200)
		Me.matchingGroupBox.Name = "matchingGroupBox"
		Me.matchingGroupBox.Size = New System.Drawing.Size(151, 54)
		Me.matchingGroupBox.TabIndex = 32
		Me.matchingGroupBox.TabStop = False
		'
		'defaultButton
		'
		Me.defaultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.defaultButton.Location = New System.Drawing.Point(82, 26)
		Me.defaultButton.Name = "defaultButton"
		Me.defaultButton.Size = New System.Drawing.Size(63, 23)
		Me.defaultButton.TabIndex = 20
		Me.defaultButton.Text = "Default"
		Me.defaultButton.UseVisualStyleBackColor = True
		'
		'matchingFarComboBox
		'
		Me.matchingFarComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.matchingFarComboBox.FormattingEnabled = True
		Me.matchingFarComboBox.Location = New System.Drawing.Point(9, 28)
		Me.matchingFarComboBox.Name = "matchingFarComboBox"
		Me.matchingFarComboBox.Size = New System.Drawing.Size(67, 21)
		Me.matchingFarComboBox.TabIndex = 19
		'
		'templateNameLabel2
		'
		Me.templateNameLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateNameLabel2.AutoSize = True
		Me.templateNameLabel2.Location = New System.Drawing.Point(3, 485)
		Me.templateNameLabel2.Name = "templateNameLabel2"
		Me.templateNameLabel2.Size = New System.Drawing.Size(117, 13)
		Me.templateNameLabel2.TabIndex = 28
		Me.templateNameLabel2.Text = "Image or template right:"
		'
		'templateNameLabel1
		'
		Me.templateNameLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateNameLabel1.AutoSize = True
		Me.templateNameLabel1.Location = New System.Drawing.Point(3, 461)
		Me.templateNameLabel1.Name = "templateNameLabel1"
		Me.templateNameLabel1.Size = New System.Drawing.Size(111, 13)
		Me.templateNameLabel1.TabIndex = 27
		Me.templateNameLabel1.Text = "Image or template left:"
		'
		'verifyButton
		'
		Me.verifyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.verifyButton.Enabled = False
		Me.verifyButton.Location = New System.Drawing.Point(6, 512)
		Me.verifyButton.Name = "verifyButton"
		Me.verifyButton.Size = New System.Drawing.Size(121, 23)
		Me.verifyButton.TabIndex = 26
		Me.verifyButton.Text = "Verify"
		Me.verifyButton.UseVisualStyleBackColor = True
		'
		'openImageButton2
		'
		Me.openImageButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.openImageButton2.Image = CType(resources.GetObject("openImageButton2.Image"), System.Drawing.Image)
		Me.openImageButton2.Location = New System.Drawing.Point(399, 35)
		Me.openImageButton2.Name = "openImageButton2"
		Me.openImageButton2.Size = New System.Drawing.Size(30, 23)
		Me.openImageButton2.TabIndex = 22
		Me.toolTip.SetToolTip(Me.openImageButton2, "Open second fingerprint image or template (*.dat) file")
		Me.openImageButton2.UseVisualStyleBackColor = True
		'
		'msgLabel
		'
		Me.msgLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.msgLabel.AutoSize = True
		Me.msgLabel.Location = New System.Drawing.Point(3, 545)
		Me.msgLabel.Name = "msgLabel"
		Me.msgLabel.Size = New System.Drawing.Size(33, 13)
		Me.msgLabel.TabIndex = 24
		Me.msgLabel.Text = "score"
		'
		'openImageButton1
		'
		Me.openImageButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.openImageButton1.Image = CType(resources.GetObject("openImageButton1.Image"), System.Drawing.Image)
		Me.openImageButton1.Location = New System.Drawing.Point(206, 35)
		Me.openImageButton1.Name = "openImageButton1"
		Me.openImageButton1.Size = New System.Drawing.Size(30, 23)
		Me.openImageButton1.TabIndex = 21
		Me.toolTip.SetToolTip(Me.openImageButton1, "Open first fingerprint image or template (*.dat) file")
		Me.openImageButton1.UseVisualStyleBackColor = True
		'
		'clearImagesButton
		'
		Me.clearImagesButton.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.clearImagesButton.Location = New System.Drawing.Point(264, 0)
		Me.clearImagesButton.Margin = New System.Windows.Forms.Padding(0)
		Me.clearImagesButton.Name = "clearImagesButton"
		Me.clearImagesButton.Size = New System.Drawing.Size(108, 23)
		Me.clearImagesButton.TabIndex = 25
		Me.clearImagesButton.Text = "Clear Images"
		Me.clearImagesButton.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.ColumnCount = 3
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 157.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel2.Controls.Add(Me.openImageButton1, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.openImageButton2, 2, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.matchingGroupBox, 1, 0)
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 54)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 1
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(636, 61)
		Me.tableLayoutPanel2.TabIndex = 34
		'
		'tableLayoutPanel3
		'
		Me.tableLayoutPanel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel3.ColumnCount = 1
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.Controls.Add(Me.clearImagesButton, 0, 0)
		Me.tableLayoutPanel3.Location = New System.Drawing.Point(3, 434)
		Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
		Me.tableLayoutPanel3.RowCount = 1
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.Size = New System.Drawing.Size(636, 24)
		Me.tableLayoutPanel3.TabIndex = 35
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.Location = New System.Drawing.Point(3, 3)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = "Images.WSQ"
		Me.licensePanel.RequiredComponents = "Biometrics.FingerExtraction,Biometrics.FingerMatching"
		Me.licensePanel.Size = New System.Drawing.Size(639, 45)
		Me.licensePanel.TabIndex = 36
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 4
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.72956!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.11321!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.64151!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.83019!))
		Me.tableLayoutPanel1.Controls.Add(Me.fingerView1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.fingerView2, 2, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.chbShowBinarizedImage2, 2, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.chbShowBinarizedImage1, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider1, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider2, 3, 1)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 117)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(636, 314)
		Me.tableLayoutPanel1.TabIndex = 37
		'
		'fingerView1
		'
		Me.fingerView1.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.fingerView1.BoundingRectColor = System.Drawing.Color.Red
		Me.tableLayoutPanel1.SetColumnSpan(Me.fingerView1, 2)
		Me.fingerView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingerView1.Location = New System.Drawing.Point(3, 3)
		Me.fingerView1.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView1.Name = "fingerView1"
		Me.fingerView1.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView1.ResultImageColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.fingerView1.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView1.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView1.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView1.Size = New System.Drawing.Size(309, 279)
		Me.fingerView1.TabIndex = 24
		Me.fingerView1.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView1.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView1.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView1.TreeWidth = 2
		'
		'fingerView2
		'
		Me.fingerView2.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.fingerView2.BoundingRectColor = System.Drawing.Color.Red
		Me.tableLayoutPanel1.SetColumnSpan(Me.fingerView2, 2)
		Me.fingerView2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingerView2.Location = New System.Drawing.Point(318, 3)
		Me.fingerView2.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView2.Name = "fingerView2"
		Me.fingerView2.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView2.ResultImageColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.fingerView2.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView2.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView2.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView2.Size = New System.Drawing.Size(315, 279)
		Me.fingerView2.TabIndex = 23
		Me.fingerView2.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView2.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView2.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView2.TreeWidth = 2
		'
		'chbShowBinarizedImage2
		'
		Me.chbShowBinarizedImage2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chbShowBinarizedImage2.AutoSize = True
		Me.chbShowBinarizedImage2.Checked = True
		Me.chbShowBinarizedImage2.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbShowBinarizedImage2.Enabled = False
		Me.chbShowBinarizedImage2.Location = New System.Drawing.Point(318, 288)
		Me.chbShowBinarizedImage2.Name = "chbShowBinarizedImage2"
		Me.chbShowBinarizedImage2.Size = New System.Drawing.Size(136, 23)
		Me.chbShowBinarizedImage2.TabIndex = 26
		Me.chbShowBinarizedImage2.Text = "Show binarized image"
		Me.chbShowBinarizedImage2.UseVisualStyleBackColor = True
		'
		'chbShowBinarizedImage1
		'
		Me.chbShowBinarizedImage1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.chbShowBinarizedImage1.AutoSize = True
		Me.chbShowBinarizedImage1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.chbShowBinarizedImage1.Checked = True
		Me.chbShowBinarizedImage1.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbShowBinarizedImage1.Enabled = False
		Me.chbShowBinarizedImage1.Location = New System.Drawing.Point(176, 288)
		Me.chbShowBinarizedImage1.Name = "chbShowBinarizedImage1"
		Me.chbShowBinarizedImage1.Size = New System.Drawing.Size(136, 23)
		Me.chbShowBinarizedImage1.TabIndex = 25
		Me.chbShowBinarizedImage1.Text = "Show binarized image"
		Me.chbShowBinarizedImage1.UseVisualStyleBackColor = True
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(3, 288)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(163, 23)
		Me.nViewZoomSlider1.TabIndex = 27
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.fingerView1
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(461, 288)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(172, 23)
		Me.nViewZoomSlider2.TabIndex = 28
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.fingerView2
		'
		'VerifyFinger
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.tableLayoutPanel3)
		Me.Controls.Add(Me.tableLayoutPanel2)
		Me.Controls.Add(Me.templateRightLabel)
		Me.Controls.Add(Me.templateLeftLabel)
		Me.Controls.Add(Me.templateNameLabel2)
		Me.Controls.Add(Me.templateNameLabel1)
		Me.Controls.Add(Me.verifyButton)
		Me.Controls.Add(Me.msgLabel)
		Me.Name = "VerifyFinger"
		Me.Size = New System.Drawing.Size(642, 561)
		Me.matchingGroupBox.ResumeLayout(False)
		Me.matchingGroupBox.PerformLayout()
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel3.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private templateRightLabel As System.Windows.Forms.Label
	Private templateLeftLabel As System.Windows.Forms.Label
	Private matchingFarLabel As System.Windows.Forms.Label
	Private matchingGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents defaultButton As System.Windows.Forms.Button
	Private WithEvents matchingFarComboBox As System.Windows.Forms.ComboBox
	Private templateNameLabel2 As System.Windows.Forms.Label
	Private templateNameLabel1 As System.Windows.Forms.Label
	Private WithEvents verifyButton As System.Windows.Forms.Button
	Private WithEvents openImageButton2 As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private msgLabel As System.Windows.Forms.Label
	Private WithEvents openImageButton1 As System.Windows.Forms.Button
	Private WithEvents clearImagesButton As System.Windows.Forms.Button
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Private licensePanel As LicensePanel
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents fingerView1 As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents fingerView2 As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents chbShowBinarizedImage2 As System.Windows.Forms.CheckBox
	Private WithEvents chbShowBinarizedImage1 As System.Windows.Forms.CheckBox
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
End Class

