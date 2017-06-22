Imports Microsoft.VisualBasic
Imports System

Partial Public Class VerifyFace
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerifyFace))
		Me.clearImagesButton = New System.Windows.Forms.Button
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.openImageButton1 = New System.Windows.Forms.Button
		Me.openImageButton2 = New System.Windows.Forms.Button
		Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.matchingGroupBox = New System.Windows.Forms.GroupBox
		Me.defaultButton = New System.Windows.Forms.Button
		Me.matchingFarLabel = New System.Windows.Forms.Label
		Me.matchingFarComboBox = New System.Windows.Forms.ComboBox
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.templateRightLabel = New System.Windows.Forms.Label
		Me.templateLeftLabel = New System.Windows.Forms.Label
		Me.templateNameLabel2 = New System.Windows.Forms.Label
		Me.templateNameLabel1 = New System.Windows.Forms.Label
		Me.verifyButton = New System.Windows.Forms.Button
		Me.msgLabel = New System.Windows.Forms.Label
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.faceView1 = New Neurotec.Biometrics.Gui.NFaceView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.faceView2 = New Neurotec.Biometrics.Gui.NFaceView
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.tableLayoutPanel3.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.matchingGroupBox.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'clearImagesButton
		'
		Me.clearImagesButton.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.clearImagesButton.Location = New System.Drawing.Point(232, 0)
		Me.clearImagesButton.Margin = New System.Windows.Forms.Padding(0)
		Me.clearImagesButton.Name = "clearImagesButton"
		Me.clearImagesButton.Size = New System.Drawing.Size(108, 23)
		Me.clearImagesButton.TabIndex = 25
		Me.clearImagesButton.Text = "Clear Images"
		Me.clearImagesButton.UseVisualStyleBackColor = True
		'
		'openImageButton1
		'
		Me.openImageButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.openImageButton1.Image = CType(resources.GetObject("openImageButton1.Image"), System.Drawing.Image)
		Me.openImageButton1.Location = New System.Drawing.Point(175, 35)
		Me.openImageButton1.Name = "openImageButton1"
		Me.openImageButton1.Size = New System.Drawing.Size(30, 23)
		Me.openImageButton1.TabIndex = 21
		Me.toolTip.SetToolTip(Me.openImageButton1, "Open first fingerprint image or template (*.dat) file")
		Me.openImageButton1.UseVisualStyleBackColor = True
		'
		'openImageButton2
		'
		Me.openImageButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.openImageButton2.Image = CType(resources.GetObject("openImageButton2.Image"), System.Drawing.Image)
		Me.openImageButton2.Location = New System.Drawing.Point(366, 35)
		Me.openImageButton2.Name = "openImageButton2"
		Me.openImageButton2.Size = New System.Drawing.Size(30, 23)
		Me.openImageButton2.TabIndex = 22
		Me.toolTip.SetToolTip(Me.openImageButton2, "Open second fingerprint image or template (*.dat) file")
		Me.openImageButton2.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel3
		'
		Me.tableLayoutPanel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel3.ColumnCount = 1
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.Controls.Add(Me.clearImagesButton, 0, 0)
		Me.tableLayoutPanel3.Location = New System.Drawing.Point(0, 240)
		Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
		Me.tableLayoutPanel3.RowCount = 1
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.Size = New System.Drawing.Size(572, 24)
		Me.tableLayoutPanel3.TabIndex = 44
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.ColumnCount = 3
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel2.Controls.Add(Me.openImageButton1, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.openImageButton2, 2, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.matchingGroupBox, 1, 0)
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(0, 51)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 1
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(572, 61)
		Me.tableLayoutPanel2.TabIndex = 43
		'
		'matchingGroupBox
		'
		Me.matchingGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.matchingGroupBox.Controls.Add(Me.defaultButton)
		Me.matchingGroupBox.Controls.Add(Me.matchingFarLabel)
		Me.matchingGroupBox.Controls.Add(Me.matchingFarComboBox)
		Me.matchingGroupBox.Location = New System.Drawing.Point(211, 3)
		Me.matchingGroupBox.MaximumSize = New System.Drawing.Size(600, 200)
		Me.matchingGroupBox.Name = "matchingGroupBox"
		Me.matchingGroupBox.Size = New System.Drawing.Size(149, 54)
		Me.matchingGroupBox.TabIndex = 32
		Me.matchingGroupBox.TabStop = False
		'
		'defaultButton
		'
		Me.defaultButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.defaultButton.Location = New System.Drawing.Point(80, 26)
		Me.defaultButton.Name = "defaultButton"
		Me.defaultButton.Size = New System.Drawing.Size(63, 23)
		Me.defaultButton.TabIndex = 20
		Me.defaultButton.Text = "Default"
		Me.defaultButton.UseVisualStyleBackColor = True
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
		'matchingFarComboBox
		'
		Me.matchingFarComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.matchingFarComboBox.FormattingEnabled = True
		Me.matchingFarComboBox.Location = New System.Drawing.Point(9, 28)
		Me.matchingFarComboBox.Name = "matchingFarComboBox"
		Me.matchingFarComboBox.Size = New System.Drawing.Size(63, 21)
		Me.matchingFarComboBox.TabIndex = 19
		'
		'templateRightLabel
		'
		Me.templateRightLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateRightLabel.AutoSize = True
		Me.templateRightLabel.Location = New System.Drawing.Point(120, 291)
		Me.templateRightLabel.Name = "templateRightLabel"
		Me.templateRightLabel.Size = New System.Drawing.Size(64, 13)
		Me.templateRightLabel.TabIndex = 41
		Me.templateRightLabel.Text = "template left"
		'
		'templateLeftLabel
		'
		Me.templateLeftLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateLeftLabel.AutoSize = True
		Me.templateLeftLabel.Location = New System.Drawing.Point(120, 267)
		Me.templateLeftLabel.Name = "templateLeftLabel"
		Me.templateLeftLabel.Size = New System.Drawing.Size(64, 13)
		Me.templateLeftLabel.TabIndex = 40
		Me.templateLeftLabel.Text = "template left"
		'
		'templateNameLabel2
		'
		Me.templateNameLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateNameLabel2.AutoSize = True
		Me.templateNameLabel2.Location = New System.Drawing.Point(3, 291)
		Me.templateNameLabel2.Name = "templateNameLabel2"
		Me.templateNameLabel2.Size = New System.Drawing.Size(117, 13)
		Me.templateNameLabel2.TabIndex = 39
		Me.templateNameLabel2.Text = "Image or template right:"
		'
		'templateNameLabel1
		'
		Me.templateNameLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateNameLabel1.AutoSize = True
		Me.templateNameLabel1.Location = New System.Drawing.Point(3, 267)
		Me.templateNameLabel1.Name = "templateNameLabel1"
		Me.templateNameLabel1.Size = New System.Drawing.Size(111, 13)
		Me.templateNameLabel1.TabIndex = 38
		Me.templateNameLabel1.Text = "Image or template left:"
		'
		'verifyButton
		'
		Me.verifyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.verifyButton.Enabled = False
		Me.verifyButton.Location = New System.Drawing.Point(6, 318)
		Me.verifyButton.Name = "verifyButton"
		Me.verifyButton.Size = New System.Drawing.Size(121, 23)
		Me.verifyButton.TabIndex = 37
		Me.verifyButton.Text = "Verify"
		Me.verifyButton.UseVisualStyleBackColor = True
		'
		'msgLabel
		'
		Me.msgLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.msgLabel.AutoSize = True
		Me.msgLabel.Location = New System.Drawing.Point(3, 351)
		Me.msgLabel.Name = "msgLabel"
		Me.msgLabel.Size = New System.Drawing.Size(33, 13)
		Me.msgLabel.TabIndex = 36
		Me.msgLabel.Text = "score"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.faceView1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider1, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.faceView2, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider2, 1, 1)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 115)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(572, 123)
		Me.tableLayoutPanel1.TabIndex = 46
		'
		'faceView1
		'
		Me.faceView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.faceView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceView1.Face = Nothing
		Me.faceView1.FaceIds = Nothing
		Me.faceView1.Location = New System.Drawing.Point(3, 3)
		Me.faceView1.Name = "faceView1"
		Me.faceView1.Size = New System.Drawing.Size(280, 88)
		Me.faceView1.TabIndex = 4
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(3, 97)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(280, 23)
		Me.nViewZoomSlider1.TabIndex = 6
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.faceView1
		'
		'faceView2
		'
		Me.faceView2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.faceView2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceView2.Face = Nothing
		Me.faceView2.FaceIds = Nothing
		Me.faceView2.Location = New System.Drawing.Point(289, 3)
		Me.faceView2.Name = "faceView2"
		Me.faceView2.Size = New System.Drawing.Size(280, 88)
		Me.faceView2.TabIndex = 3
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(289, 97)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(280, 23)
		Me.nViewZoomSlider2.TabIndex = 7
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.faceView2
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(3, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Biometrics.FaceMatching"
		Me.licensePanel.Size = New System.Drawing.Size(569, 45)
		Me.licensePanel.TabIndex = 45
		'
		'VerifyFace
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
		Me.Name = "VerifyFace"
		Me.Size = New System.Drawing.Size(572, 367)
		Me.tableLayoutPanel3.ResumeLayout(False)
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.matchingGroupBox.ResumeLayout(False)
		Me.matchingGroupBox.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents clearImagesButton As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
	Private WithEvents openImageButton1 As System.Windows.Forms.Button
	Private WithEvents openImageButton2 As System.Windows.Forms.Button
	Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private matchingGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents defaultButton As System.Windows.Forms.Button
	Private matchingFarLabel As System.Windows.Forms.Label
	Private WithEvents matchingFarComboBox As System.Windows.Forms.ComboBox
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private templateRightLabel As System.Windows.Forms.Label
	Private templateLeftLabel As System.Windows.Forms.Label
	Private templateNameLabel2 As System.Windows.Forms.Label
	Private templateNameLabel1 As System.Windows.Forms.Label
	Private WithEvents verifyButton As System.Windows.Forms.Button
	Private msgLabel As System.Windows.Forms.Label
	Private licensePanel As LicensePanel
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents faceView2 As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents faceView1 As Neurotec.Biometrics.Gui.NFaceView
End Class

