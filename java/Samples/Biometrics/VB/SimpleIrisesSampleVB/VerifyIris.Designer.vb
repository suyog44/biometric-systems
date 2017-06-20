Imports Microsoft.VisualBasic
Imports System

Partial Public Class VerifyIris
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerifyIris))
		Me.btnClear = New System.Windows.Forms.Button
		Me.btnOpenImage1 = New System.Windows.Forms.Button
		Me.btnOpenImage2 = New System.Windows.Forms.Button
		Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.matchingGroupBox = New System.Windows.Forms.GroupBox
		Me.btnDefault = New System.Windows.Forms.Button
		Me.matchingFarLabel = New System.Windows.Forms.Label
		Me.cbMatchingFAR = New System.Windows.Forms.ComboBox
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.lblTemplateRight = New System.Windows.Forms.Label
		Me.lblTemplateLeft = New System.Windows.Forms.Label
		Me.templateNameLabel2 = New System.Windows.Forms.Label
		Me.templateNameLabel1 = New System.Windows.Forms.Label
		Me.btnVerify = New System.Windows.Forms.Button
		Me.lblMsg = New System.Windows.Forms.Label
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.irisView1 = New Neurotec.Biometrics.Gui.NIrisView
		Me.irisView2 = New Neurotec.Biometrics.Gui.NIrisView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.tableLayoutPanel3.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.matchingGroupBox.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnClear
		'
		Me.btnClear.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.btnClear.Location = New System.Drawing.Point(237, 0)
		Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
		Me.btnClear.Name = "btnClear"
		Me.btnClear.Size = New System.Drawing.Size(108, 23)
		Me.btnClear.TabIndex = 25
		Me.btnClear.Text = "Clear Images"
		Me.btnClear.UseVisualStyleBackColor = True
		'
		'btnOpenImage1
		'
		Me.btnOpenImage1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOpenImage1.Image = CType(resources.GetObject("btnOpenImage1.Image"), System.Drawing.Image)
		Me.btnOpenImage1.Location = New System.Drawing.Point(183, 35)
		Me.btnOpenImage1.Name = "btnOpenImage1"
		Me.btnOpenImage1.Size = New System.Drawing.Size(30, 23)
		Me.btnOpenImage1.TabIndex = 21
		Me.btnOpenImage1.UseVisualStyleBackColor = True
		'
		'btnOpenImage2
		'
		Me.btnOpenImage2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnOpenImage2.Image = CType(resources.GetObject("btnOpenImage2.Image"), System.Drawing.Image)
		Me.btnOpenImage2.Location = New System.Drawing.Point(368, 35)
		Me.btnOpenImage2.Name = "btnOpenImage2"
		Me.btnOpenImage2.Size = New System.Drawing.Size(30, 23)
		Me.btnOpenImage2.TabIndex = 22
		Me.btnOpenImage2.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel3
		'
		Me.tableLayoutPanel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel3.ColumnCount = 1
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.Controls.Add(Me.btnClear, 0, 0)
		Me.tableLayoutPanel3.Location = New System.Drawing.Point(3, 277)
		Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
		Me.tableLayoutPanel3.RowCount = 1
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.Size = New System.Drawing.Size(582, 24)
		Me.tableLayoutPanel3.TabIndex = 44
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.ColumnCount = 3
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 149.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel2.Controls.Add(Me.btnOpenImage1, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.btnOpenImage2, 2, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.matchingGroupBox, 1, 0)
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 51)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 1
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(582, 61)
		Me.tableLayoutPanel2.TabIndex = 43
		'
		'matchingGroupBox
		'
		Me.matchingGroupBox.Anchor = System.Windows.Forms.AnchorStyles.None
		Me.matchingGroupBox.Controls.Add(Me.btnDefault)
		Me.matchingGroupBox.Controls.Add(Me.matchingFarLabel)
		Me.matchingGroupBox.Controls.Add(Me.cbMatchingFAR)
		Me.matchingGroupBox.Location = New System.Drawing.Point(219, 3)
		Me.matchingGroupBox.MaximumSize = New System.Drawing.Size(600, 200)
		Me.matchingGroupBox.Name = "matchingGroupBox"
		Me.matchingGroupBox.Size = New System.Drawing.Size(143, 54)
		Me.matchingGroupBox.TabIndex = 32
		Me.matchingGroupBox.TabStop = False
		'
		'btnDefault
		'
		Me.btnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnDefault.Location = New System.Drawing.Point(74, 26)
		Me.btnDefault.Name = "btnDefault"
		Me.btnDefault.Size = New System.Drawing.Size(63, 23)
		Me.btnDefault.TabIndex = 20
		Me.btnDefault.Text = "Default"
		Me.btnDefault.UseVisualStyleBackColor = True
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
		'cbMatchingFAR
		'
		Me.cbMatchingFAR.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbMatchingFAR.FormattingEnabled = True
		Me.cbMatchingFAR.Location = New System.Drawing.Point(9, 28)
		Me.cbMatchingFAR.Name = "cbMatchingFAR"
		Me.cbMatchingFAR.Size = New System.Drawing.Size(59, 21)
		Me.cbMatchingFAR.TabIndex = 19
		'
		'lblTemplateRight
		'
		Me.lblTemplateRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblTemplateRight.AutoSize = True
		Me.lblTemplateRight.Location = New System.Drawing.Point(120, 328)
		Me.lblTemplateRight.Name = "lblTemplateRight"
		Me.lblTemplateRight.Size = New System.Drawing.Size(64, 13)
		Me.lblTemplateRight.TabIndex = 41
		Me.lblTemplateRight.Text = "template left"
		'
		'lblTemplateLeft
		'
		Me.lblTemplateLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblTemplateLeft.AutoSize = True
		Me.lblTemplateLeft.Location = New System.Drawing.Point(120, 304)
		Me.lblTemplateLeft.Name = "lblTemplateLeft"
		Me.lblTemplateLeft.Size = New System.Drawing.Size(64, 13)
		Me.lblTemplateLeft.TabIndex = 40
		Me.lblTemplateLeft.Text = "template left"
		'
		'templateNameLabel2
		'
		Me.templateNameLabel2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateNameLabel2.AutoSize = True
		Me.templateNameLabel2.Location = New System.Drawing.Point(3, 328)
		Me.templateNameLabel2.Name = "templateNameLabel2"
		Me.templateNameLabel2.Size = New System.Drawing.Size(117, 13)
		Me.templateNameLabel2.TabIndex = 39
		Me.templateNameLabel2.Text = "Image or template right:"
		'
		'templateNameLabel1
		'
		Me.templateNameLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.templateNameLabel1.AutoSize = True
		Me.templateNameLabel1.Location = New System.Drawing.Point(3, 304)
		Me.templateNameLabel1.Name = "templateNameLabel1"
		Me.templateNameLabel1.Size = New System.Drawing.Size(111, 13)
		Me.templateNameLabel1.TabIndex = 38
		Me.templateNameLabel1.Text = "Image or template left:"
		'
		'btnVerify
		'
		Me.btnVerify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnVerify.Enabled = False
		Me.btnVerify.Location = New System.Drawing.Point(6, 355)
		Me.btnVerify.Name = "btnVerify"
		Me.btnVerify.Size = New System.Drawing.Size(121, 23)
		Me.btnVerify.TabIndex = 37
		Me.btnVerify.Text = "Verify"
		Me.btnVerify.UseVisualStyleBackColor = True
		'
		'lblMsg
		'
		Me.lblMsg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblMsg.AutoSize = True
		Me.lblMsg.Location = New System.Drawing.Point(3, 388)
		Me.lblMsg.Name = "lblMsg"
		Me.lblMsg.Size = New System.Drawing.Size(33, 13)
		Me.lblMsg.TabIndex = 36
		Me.lblMsg.Text = "score"
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(3, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Biometrics.IrisMatching"
		Me.licensePanel.Size = New System.Drawing.Size(585, 45)
		Me.licensePanel.TabIndex = 45
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.irisView1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.irisView2, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider1, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider2, 1, 1)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 118)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(582, 157)
		Me.tableLayoutPanel1.TabIndex = 46
		'
		'irisView1
		'
		Me.irisView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.irisView1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.irisView1.InnerBoundaryColor = System.Drawing.Color.Red
		Me.irisView1.InnerBoundaryWidth = 2
		Me.irisView1.Iris = Nothing
		Me.irisView1.Location = New System.Drawing.Point(3, 3)
		Me.irisView1.Name = "irisView1"
		Me.irisView1.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView1.OuterBoundaryWidth = 2
		Me.irisView1.Size = New System.Drawing.Size(285, 122)
		Me.irisView1.TabIndex = 0
		'
		'irisView2
		'
		Me.irisView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.irisView2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.irisView2.InnerBoundaryColor = System.Drawing.Color.Red
		Me.irisView2.InnerBoundaryWidth = 2
		Me.irisView2.Iris = Nothing
		Me.irisView2.Location = New System.Drawing.Point(294, 3)
		Me.irisView2.Name = "irisView2"
		Me.irisView2.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView2.OuterBoundaryWidth = 2
		Me.irisView2.Size = New System.Drawing.Size(285, 122)
		Me.irisView2.TabIndex = 1
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(3, 131)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(285, 23)
		Me.nViewZoomSlider1.TabIndex = 2
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.irisView1
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(294, 131)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(285, 23)
		Me.nViewZoomSlider2.TabIndex = 3
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.irisView2
		'
		'VerifyIris
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.tableLayoutPanel3)
		Me.Controls.Add(Me.tableLayoutPanel2)
		Me.Controls.Add(Me.lblTemplateRight)
		Me.Controls.Add(Me.lblTemplateLeft)
		Me.Controls.Add(Me.templateNameLabel2)
		Me.Controls.Add(Me.templateNameLabel1)
		Me.Controls.Add(Me.btnVerify)
		Me.Controls.Add(Me.lblMsg)
		Me.Name = "VerifyIris"
		Me.Size = New System.Drawing.Size(588, 409)
		Me.tableLayoutPanel3.ResumeLayout(False)
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.matchingGroupBox.ResumeLayout(False)
		Me.matchingGroupBox.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents btnClear As System.Windows.Forms.Button
	Private WithEvents btnOpenImage1 As System.Windows.Forms.Button
	Private WithEvents btnOpenImage2 As System.Windows.Forms.Button
	Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private matchingGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents btnDefault As System.Windows.Forms.Button
	Private matchingFarLabel As System.Windows.Forms.Label
	Private WithEvents cbMatchingFAR As System.Windows.Forms.ComboBox
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private lblTemplateRight As System.Windows.Forms.Label
	Private lblTemplateLeft As System.Windows.Forms.Label
	Private templateNameLabel2 As System.Windows.Forms.Label
	Private templateNameLabel1 As System.Windows.Forms.Label
	Private WithEvents btnVerify As System.Windows.Forms.Button
	Private lblMsg As System.Windows.Forms.Label
	Private licensePanel As LicensePanel
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents irisView1 As Neurotec.Biometrics.Gui.NIrisView
	Private WithEvents irisView2 As Neurotec.Biometrics.Gui.NIrisView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
End Class

