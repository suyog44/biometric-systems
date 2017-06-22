Imports Microsoft.VisualBasic
Imports System
Partial Public Class GeneralizeFinger
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GeneralizeFinger))
		Me.lblStatus = New System.Windows.Forms.Label
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.lblImageCount = New System.Windows.Forms.Label
		Me.label1 = New System.Windows.Forms.Label
		Me.btnOpenImages = New System.Windows.Forms.Button
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.fingerView = New Neurotec.Biometrics.Gui.NFingerView
		Me.chbShowBinarizedImage = New System.Windows.Forms.CheckBox
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.groupBox1.SuspendLayout()
		Me.SuspendLayout()
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.Location = New System.Drawing.Point(262, 284)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(436, 23)
		Me.lblStatus.TabIndex = 30
		Me.lblStatus.Text = "Status: None"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.lblStatus.Visible = False
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
		Me.groupBox1.Size = New System.Drawing.Size(695, 54)
		Me.groupBox1.TabIndex = 27
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Load finger images (Min 3, Max 10)"
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
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Image = CType(resources.GetObject("btnSaveTemplate.Image"), System.Drawing.Image)
		Me.btnSaveTemplate.Location = New System.Drawing.Point(9, 284)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveTemplate.TabIndex = 2
		Me.btnSaveTemplate.Text = "Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'openFileDialog
		'
		Me.openFileDialog.Multiselect = True
		'
		'fingerView
		'
		Me.fingerView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.fingerView.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView.BoundingRectColor = System.Drawing.Color.Red
		Me.fingerView.Location = New System.Drawing.Point(3, 114)
		Me.fingerView.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView.Name = "fingerView"
		Me.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView.ResultImageColor = System.Drawing.Color.Green
		Me.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView.ShownImage = Neurotec.Biometrics.Gui.ShownImage.Result
		Me.fingerView.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView.Size = New System.Drawing.Size(695, 164)
		Me.fingerView.TabIndex = 32
		Me.fingerView.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView.TreeWidth = 2
		'
		'chbShowBinarizedImage
		'
		Me.chbShowBinarizedImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chbShowBinarizedImage.AutoSize = True
		Me.chbShowBinarizedImage.Checked = True
		Me.chbShowBinarizedImage.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbShowBinarizedImage.Location = New System.Drawing.Point(120, 288)
		Me.chbShowBinarizedImage.Name = "chbShowBinarizedImage"
		Me.chbShowBinarizedImage.Size = New System.Drawing.Size(136, 17)
		Me.chbShowBinarizedImage.TabIndex = 8
		Me.chbShowBinarizedImage.Text = "Show binarized image"
		Me.chbShowBinarizedImage.UseVisualStyleBackColor = True
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.Location = New System.Drawing.Point(3, 3)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = "Images.WSQ"
		Me.licensePanel.RequiredComponents = "Biometrics.FingerExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(695, 45)
		Me.licensePanel.TabIndex = 31
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(423, 284)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(275, 23)
		Me.nViewZoomSlider1.TabIndex = 34
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.fingerView
		'
		'GeneralizeFinger
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.chbShowBinarizedImage)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Controls.Add(Me.fingerView)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.groupBox1)
		Me.Name = "GeneralizeFinger"
		Me.Size = New System.Drawing.Size(701, 310)
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private lblStatus As System.Windows.Forms.Label
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private lblImageCount As System.Windows.Forms.Label
	Private label1 As System.Windows.Forms.Label
	Private WithEvents btnOpenImages As System.Windows.Forms.Button
	Private licensePanel As LicensePanel
	Private fingerView As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents chbShowBinarizedImage As System.Windows.Forms.CheckBox
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class
