Imports Microsoft.VisualBasic
Imports System

Partial Public Class EnrollFromScanner
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnrollFromScanner))
		Me.scannersGroupBox = New System.Windows.Forms.GroupBox
		Me.chbScanAutomatically = New System.Windows.Forms.CheckBox
		Me.btnForce = New System.Windows.Forms.Button
		Me.rbRight = New System.Windows.Forms.RadioButton
		Me.rbLeft = New System.Windows.Forms.RadioButton
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnRefresh = New System.Windows.Forms.Button
		Me.btnScan = New System.Windows.Forms.Button
		Me.lbScanners = New System.Windows.Forms.ListBox
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.btnSaveImage = New System.Windows.Forms.Button
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.irisView = New Neurotec.Biometrics.Gui.NIrisView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.lblStatus = New System.Windows.Forms.Label
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.scannersGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'scannersGroupBox
		'
		Me.scannersGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.scannersGroupBox.Controls.Add(Me.chbScanAutomatically)
		Me.scannersGroupBox.Controls.Add(Me.btnForce)
		Me.scannersGroupBox.Controls.Add(Me.rbRight)
		Me.scannersGroupBox.Controls.Add(Me.rbLeft)
		Me.scannersGroupBox.Controls.Add(Me.btnCancel)
		Me.scannersGroupBox.Controls.Add(Me.btnRefresh)
		Me.scannersGroupBox.Controls.Add(Me.btnScan)
		Me.scannersGroupBox.Controls.Add(Me.lbScanners)
		Me.scannersGroupBox.Location = New System.Drawing.Point(3, 51)
		Me.scannersGroupBox.Name = "scannersGroupBox"
		Me.scannersGroupBox.Size = New System.Drawing.Size(583, 115)
		Me.scannersGroupBox.TabIndex = 12
		Me.scannersGroupBox.TabStop = False
		Me.scannersGroupBox.Text = "Scanners list"
		'
		'chbScanAutomatically
		'
		Me.chbScanAutomatically.AutoSize = True
		Me.chbScanAutomatically.Checked = True
		Me.chbScanAutomatically.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbScanAutomatically.Location = New System.Drawing.Point(330, 85)
		Me.chbScanAutomatically.Name = "chbScanAutomatically"
		Me.chbScanAutomatically.Size = New System.Drawing.Size(115, 17)
		Me.chbScanAutomatically.TabIndex = 15
		Me.chbScanAutomatically.Text = "Scan automatically"
		Me.chbScanAutomatically.UseVisualStyleBackColor = True
		'
		'btnForce
		'
		Me.btnForce.Enabled = False
		Me.btnForce.Location = New System.Drawing.Point(249, 81)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(75, 23)
		Me.btnForce.TabIndex = 14
		Me.btnForce.Text = "Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'rbRight
		'
		Me.rbRight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.rbRight.AutoSize = True
		Me.rbRight.Location = New System.Drawing.Point(511, 53)
		Me.rbRight.Name = "rbRight"
		Me.rbRight.Size = New System.Drawing.Size(66, 17)
		Me.rbRight.TabIndex = 13
		Me.rbRight.Text = "Right Iris"
		Me.rbRight.UseVisualStyleBackColor = True
		'
		'rbLeft
		'
		Me.rbLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.rbLeft.AutoSize = True
		Me.rbLeft.Checked = True
		Me.rbLeft.Location = New System.Drawing.Point(511, 30)
		Me.rbLeft.Name = "rbLeft"
		Me.rbLeft.Size = New System.Drawing.Size(59, 17)
		Me.rbLeft.TabIndex = 12
		Me.rbLeft.TabStop = True
		Me.rbLeft.Text = "Left Iris"
		Me.rbLeft.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Enabled = False
		Me.btnCancel.Location = New System.Drawing.Point(168, 81)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 11
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnRefresh
		'
		Me.btnRefresh.Location = New System.Drawing.Point(6, 81)
		Me.btnRefresh.Name = "btnRefresh"
		Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
		Me.btnRefresh.TabIndex = 10
		Me.btnRefresh.Text = "Refresh list"
		Me.btnRefresh.UseVisualStyleBackColor = True
		'
		'btnScan
		'
		Me.btnScan.Location = New System.Drawing.Point(87, 81)
		Me.btnScan.Name = "btnScan"
		Me.btnScan.Size = New System.Drawing.Size(75, 23)
		Me.btnScan.TabIndex = 9
		Me.btnScan.Text = "Scan"
		Me.btnScan.UseVisualStyleBackColor = True
		'
		'lbScanners
		'
		Me.lbScanners.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lbScanners.Location = New System.Drawing.Point(4, 19)
		Me.lbScanners.Name = "lbScanners"
		Me.lbScanners.Size = New System.Drawing.Size(501, 56)
		Me.lbScanners.TabIndex = 6
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Location = New System.Drawing.Point(490, 330)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(96, 23)
		Me.btnSaveTemplate.TabIndex = 16
		Me.btnSaveTemplate.Text = "&Save Template"
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'btnSaveImage
		'
		Me.btnSaveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveImage.Enabled = False
		Me.btnSaveImage.Image = CType(resources.GetObject("btnSaveImage.Image"), System.Drawing.Image)
		Me.btnSaveImage.Location = New System.Drawing.Point(388, 330)
		Me.btnSaveImage.Name = "btnSaveImage"
		Me.btnSaveImage.Size = New System.Drawing.Size(96, 23)
		Me.btnSaveImage.TabIndex = 15
		Me.btnSaveImage.Text = "Save Image"
		Me.btnSaveImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveImage.UseVisualStyleBackColor = True
		'
		'irisView
		'
		Me.irisView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.irisView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.irisView.InnerBoundaryColor = System.Drawing.Color.Red
		Me.irisView.InnerBoundaryWidth = 2
		Me.irisView.Iris = Nothing
		Me.irisView.Location = New System.Drawing.Point(3, 172)
		Me.irisView.Name = "irisView"
		Me.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView.OuterBoundaryWidth = 2
		Me.irisView.Size = New System.Drawing.Size(583, 152)
		Me.irisView.TabIndex = 17
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(133, 330)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider1.TabIndex = 18
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.irisView
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Location = New System.Drawing.Point(4, 335)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(0, 13)
		Me.lblStatus.TabIndex = 19
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Devices.IrisScanners"
		Me.licensePanel.Size = New System.Drawing.Size(586, 45)
		Me.licensePanel.TabIndex = 14
		'
		'EnrollFromScanner
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.irisView)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Controls.Add(Me.btnSaveImage)
		Me.Controls.Add(Me.scannersGroupBox)
		Me.Name = "EnrollFromScanner"
		Me.Size = New System.Drawing.Size(589, 356)
		Me.scannersGroupBox.ResumeLayout(False)
		Me.scannersGroupBox.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private scannersGroupBox As System.Windows.Forms.GroupBox
	Private rbRight As System.Windows.Forms.RadioButton
	Private rbLeft As System.Windows.Forms.RadioButton
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private WithEvents btnRefresh As System.Windows.Forms.Button
	Private WithEvents btnScan As System.Windows.Forms.Button
	Private WithEvents lbScanners As System.Windows.Forms.ListBox
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private WithEvents btnSaveImage As System.Windows.Forms.Button
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private licensePanel As LicensePanel
	Private WithEvents irisView As Neurotec.Biometrics.Gui.NIrisView
	Friend WithEvents chbScanAutomatically As System.Windows.Forms.CheckBox
	Friend WithEvents btnForce As System.Windows.Forms.Button
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents lblStatus As System.Windows.Forms.Label
End Class

