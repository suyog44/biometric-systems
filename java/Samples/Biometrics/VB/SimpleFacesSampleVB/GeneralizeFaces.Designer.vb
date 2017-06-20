Imports Microsoft.VisualBasic
Imports System
Partial Public Class GeneralizeFaces
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralizeFaces))
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.lblImageCount = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.btnOpenImages = New System.Windows.Forms.Button
		Me.faceView = New Neurotec.Biometrics.Gui.NFaceView
		Me.lblStatus = New System.Windows.Forms.Label
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.saveTemplateDialog = New System.Windows.Forms.SaveFileDialog
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.lblImageCount)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.btnOpenImages)
		Me.groupBox1.Location = New System.Drawing.Point(3, 54)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(514, 54)
		Me.groupBox1.TabIndex = 18
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Load face images"
		'
		'lblImageCount
		'
		Me.lblImageCount.AutoSize = True
		Me.lblImageCount.Location = New System.Drawing.Point(142, 24)
		Me.lblImageCount.Name = "lblImageCount"
		Me.lblImageCount.Size = New System.Drawing.Size(13, 13)
		Me.lblImageCount.TabIndex = 10
		Me.lblImageCount.Text = "0"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(42, 24)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(79, 13)
		Me.label1.TabIndex = 9
		Me.label1.Text = "Images loaded:"
		'
		'btnOpenImages
		'
		Me.btnOpenImages.Image = CType(resources.GetObject("btnOpenImages.Image"), System.Drawing.Image)
		Me.btnOpenImages.Location = New System.Drawing.Point(6, 19)
		Me.btnOpenImages.Name = "btnOpenImages"
		Me.btnOpenImages.Size = New System.Drawing.Size(30, 23)
		Me.btnOpenImages.TabIndex = 8
		Me.btnOpenImages.UseVisualStyleBackColor = True
		'
		'faceView
		'
		Me.faceView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.faceView.Face = Nothing
		Me.faceView.FaceIds = Nothing
		Me.faceView.Location = New System.Drawing.Point(3, 114)
		Me.faceView.Name = "faceView"
		Me.faceView.Size = New System.Drawing.Size(514, 116)
		Me.faceView.TabIndex = 19
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.Location = New System.Drawing.Point(114, 233)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(181, 23)
		Me.lblStatus.TabIndex = 26
		Me.lblStatus.Text = "Status"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.lblStatus.Visible = False
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.btnSaveTemplate.Location = New System.Drawing.Point(3, 233)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveTemplate.TabIndex = 25
		Me.btnSaveTemplate.Text = "&Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'openFileDialog
		'
		Me.openFileDialog.Multiselect = True
		Me.openFileDialog.Title = "Select multiple images for generalization"
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(247, 233)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(273, 23)
		Me.nViewZoomSlider1.TabIndex = 28
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.faceView
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 3)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(517, 45)
		Me.licensePanel.TabIndex = 17
		'
		'GeneralizeFaces
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Controls.Add(Me.faceView)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.licensePanel)
		Me.Name = "GeneralizeFaces"
		Me.Size = New System.Drawing.Size(520, 259)
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private licensePanel As LicensePanel
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private lblImageCount As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private WithEvents btnOpenImages As System.Windows.Forms.Button
	Private faceView As Neurotec.Biometrics.Gui.NFaceView
	Private lblStatus As System.Windows.Forms.Label
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveTemplateDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider

End Class
