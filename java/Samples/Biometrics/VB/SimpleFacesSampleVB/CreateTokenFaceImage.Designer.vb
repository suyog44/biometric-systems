<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CreateTokenFaceImage
	Inherits System.Windows.Forms.UserControl

	'UserControl overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		Try
			If disposing AndAlso components IsNot Nothing Then
				components.Dispose()
			End If
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.btnSaveImage = New System.Windows.Forms.Button
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.tsbOpenImage = New System.Windows.Forms.ToolStripButton
		Me.openImageFileDlg = New System.Windows.Forms.OpenFileDialog
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.faceViewToken = New Neurotec.Biometrics.Gui.NFaceView
		Me.lbQuality = New System.Windows.Forms.Label
		Me.lbSharpness = New System.Windows.Forms.Label
		Me.lbUniformity = New System.Windows.Forms.Label
		Me.lbDensity = New System.Windows.Forms.Label
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.faceViewOriginal = New Neurotec.Biometrics.Gui.NFaceView
		Me.label2 = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.toolStrip1.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.faceViewToken.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnSaveImage
		'
		Me.btnSaveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveImage.Enabled = False
		Me.btnSaveImage.Location = New System.Drawing.Point(463, 411)
		Me.btnSaveImage.Name = "btnSaveImage"
		Me.btnSaveImage.Size = New System.Drawing.Size(104, 23)
		Me.btnSaveImage.TabIndex = 24
		Me.btnSaveImage.Text = "&Save token image"
		Me.btnSaveImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveImage.UseVisualStyleBackColor = True
		'
		'toolStrip1
		'
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenImage})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(570, 25)
		Me.toolStrip1.TabIndex = 22
		Me.toolStrip1.Text = "toolStrip1"
		'
		'tsbOpenImage
		'
		Me.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenImage.Name = "tsbOpenImage"
		Me.tsbOpenImage.Size = New System.Drawing.Size(76, 22)
		Me.tsbOpenImage.Text = "Open Image"
		'
		'openImageFileDlg
		'
		Me.openImageFileDlg.Title = "Open Face Image"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider1, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider2, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.faceViewToken, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.faceViewOriginal, 0, 1)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 81)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 3
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(564, 324)
		Me.tableLayoutPanel1.TabIndex = 25
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(285, 298)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(276, 23)
		Me.nViewZoomSlider1.TabIndex = 8
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.faceViewToken
		'
		'faceViewToken
		'
		Me.faceViewToken.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.faceViewToken.Controls.Add(Me.lbQuality)
		Me.faceViewToken.Controls.Add(Me.lbSharpness)
		Me.faceViewToken.Controls.Add(Me.lbUniformity)
		Me.faceViewToken.Controls.Add(Me.lbDensity)
		Me.faceViewToken.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceViewToken.Face = Nothing
		Me.faceViewToken.FaceIds = Nothing
		Me.faceViewToken.FaceRectangleColor = System.Drawing.Color.Transparent
		Me.faceViewToken.Location = New System.Drawing.Point(285, 23)
		Me.faceViewToken.Name = "faceViewToken"
		Me.faceViewToken.ShowBaseFeaturePoints = False
		Me.faceViewToken.ShowEmotions = False
		Me.faceViewToken.ShowExpression = False
		Me.faceViewToken.ShowEyes = False
		Me.faceViewToken.ShowFaceConfidence = False
		Me.faceViewToken.ShowGender = False
		Me.faceViewToken.ShowMouth = False
		Me.faceViewToken.ShowNose = False
		Me.faceViewToken.ShowProperties = False
		Me.faceViewToken.Size = New System.Drawing.Size(276, 269)
		Me.faceViewToken.TabIndex = 2
		'
		'lbQuality
		'
		Me.lbQuality.Anchor = System.Windows.Forms.AnchorStyles.Bottom
		Me.lbQuality.AutoSize = True
		Me.lbQuality.Location = New System.Drawing.Point(71, 206)
		Me.lbQuality.Name = "lbQuality"
		Me.lbQuality.Size = New System.Drawing.Size(42, 13)
		Me.lbQuality.TabIndex = 3
		Me.lbQuality.Text = "Quality:"
		Me.lbQuality.Visible = False
		'
		'lbSharpness
		'
		Me.lbSharpness.Anchor = System.Windows.Forms.AnchorStyles.Bottom
		Me.lbSharpness.AutoSize = True
		Me.lbSharpness.Location = New System.Drawing.Point(71, 219)
		Me.lbSharpness.Name = "lbSharpness"
		Me.lbSharpness.Size = New System.Drawing.Size(89, 13)
		Me.lbSharpness.TabIndex = 2
		Me.lbSharpness.Text = "Sharpness score:"
		Me.lbSharpness.Visible = False
		'
		'lbUniformity
		'
		Me.lbUniformity.Anchor = System.Windows.Forms.AnchorStyles.Bottom
		Me.lbUniformity.AutoSize = True
		Me.lbUniformity.Location = New System.Drawing.Point(71, 232)
		Me.lbUniformity.Name = "lbUniformity"
		Me.lbUniformity.Size = New System.Drawing.Size(144, 13)
		Me.lbUniformity.TabIndex = 1
		Me.lbUniformity.Text = "Background uniformity score:"
		Me.lbUniformity.Visible = False
		'
		'lbDensity
		'
		Me.lbDensity.Anchor = System.Windows.Forms.AnchorStyles.Bottom
		Me.lbDensity.AutoSize = True
		Me.lbDensity.Location = New System.Drawing.Point(71, 245)
		Me.lbDensity.Name = "lbDensity"
		Me.lbDensity.Size = New System.Drawing.Size(122, 13)
		Me.lbDensity.TabIndex = 0
		Me.lbDensity.Text = "Grayscale density score:"
		Me.lbDensity.Visible = False
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(3, 298)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(276, 23)
		Me.nViewZoomSlider2.TabIndex = 8
		Me.nViewZoomSlider2.Text = "s"
		Me.nViewZoomSlider2.View = Me.faceViewOriginal
		'
		'faceViewOriginal
		'
		Me.faceViewOriginal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.faceViewOriginal.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceViewOriginal.Face = Nothing
		Me.faceViewOriginal.FaceIds = Nothing
		Me.faceViewOriginal.FaceRectangleColor = System.Drawing.Color.Transparent
		Me.faceViewOriginal.Location = New System.Drawing.Point(3, 23)
		Me.faceViewOriginal.Name = "faceViewOriginal"
		Me.faceViewOriginal.ShowBaseFeaturePoints = False
		Me.faceViewOriginal.ShowEmotions = False
		Me.faceViewOriginal.ShowExpression = False
		Me.faceViewOriginal.ShowEyes = False
		Me.faceViewOriginal.ShowFaceConfidence = False
		Me.faceViewOriginal.ShowGender = False
		Me.faceViewOriginal.ShowMouth = False
		Me.faceViewOriginal.ShowNose = False
		Me.faceViewOriginal.ShowProperties = False
		Me.faceViewOriginal.Size = New System.Drawing.Size(276, 269)
		Me.faceViewOriginal.TabIndex = 1
		'
		'label2
		'
		Me.label2.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.label2.Dock = System.Windows.Forms.DockStyle.Top
		Me.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.label2.Location = New System.Drawing.Point(285, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(276, 20)
		Me.label2.TabIndex = 1
		Me.label2.Text = "Token face image"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'label1
		'
		Me.label1.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.label1.Dock = System.Windows.Forms.DockStyle.Top
		Me.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(276, 20)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Original face image"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 30)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceDetection,Biometrics.FaceSegmentation,Biometrics.FaceQualityAssess" & _
			"ment"
		Me.licensePanel.Size = New System.Drawing.Size(570, 45)
		Me.licensePanel.TabIndex = 23
		'
		'CreateTokenFaceImage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.btnSaveImage)
		Me.Controls.Add(Me.toolStrip1)
		Me.Controls.Add(Me.licensePanel)
		Me.Name = "CreateTokenFaceImage"
		Me.Size = New System.Drawing.Size(570, 435)
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.faceViewToken.ResumeLayout(False)
		Me.faceViewToken.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Private WithEvents saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents btnSaveImage As System.Windows.Forms.Button
	Private WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
	Private WithEvents tsbOpenImage As System.Windows.Forms.ToolStripButton
	Private WithEvents openImageFileDlg As System.Windows.Forms.OpenFileDialog
	Private WithEvents licensePanel As Neurotec.Samples.LicensePanel
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents faceViewToken As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents lbQuality As System.Windows.Forms.Label
	Private WithEvents lbSharpness As System.Windows.Forms.Label
	Private WithEvents lbUniformity As System.Windows.Forms.Label
	Private WithEvents lbDensity As System.Windows.Forms.Label
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents faceViewOriginal As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents label2 As System.Windows.Forms.Label
	Private WithEvents label1 As System.Windows.Forms.Label

End Class
