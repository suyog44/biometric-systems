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
		Me.components = New System.ComponentModel.Container
		Me.lblQuality = New System.Windows.Forms.Label
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.saveImageButton = New System.Windows.Forms.Button
		Me.panel = New System.Windows.Forms.Panel
		Me.fingerView = New Neurotec.Biometrics.Gui.NFingerView
		Me.cancelScanningButton = New System.Windows.Forms.Button
		Me.refreshListButton = New System.Windows.Forms.Button
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.scanButton = New System.Windows.Forms.Button
		Me.scannersGroupBox = New System.Windows.Forms.GroupBox
		Me.chbScanAutomatically = New System.Windows.Forms.CheckBox
		Me.btnForce = New System.Windows.Forms.Button
		Me.scannersListBox = New System.Windows.Forms.ListBox
		Me.saveTemplateButton = New System.Windows.Forms.Button
		Me.scanWorker = New System.ComponentModel.BackgroundWorker
		Me.chbShowBinarizedImage = New System.Windows.Forms.CheckBox
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.LicensePanel1 = New Neurotec.Samples.LicensePanel
		Me.panel.SuspendLayout()
		Me.scannersGroupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'lblQuality
		'
		Me.lblQuality.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblQuality.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblQuality.Location = New System.Drawing.Point(3, 381)
		Me.lblQuality.Name = "lblQuality"
		Me.lblQuality.Size = New System.Drawing.Size(636, 20)
		Me.lblQuality.TabIndex = 13
		'
		'saveImageButton
		'
		Me.saveImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.saveImageButton.Enabled = False
		Me.saveImageButton.Location = New System.Drawing.Point(3, 407)
		Me.saveImageButton.Name = "saveImageButton"
		Me.saveImageButton.Size = New System.Drawing.Size(97, 23)
		Me.saveImageButton.TabIndex = 12
		Me.saveImageButton.Text = "Save &Image"
		Me.saveImageButton.UseVisualStyleBackColor = True
		'
		'panel
		'
		Me.panel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
		   Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.panel.Controls.Add(Me.fingerView)
		Me.panel.Location = New System.Drawing.Point(3, 172)
		Me.panel.Name = "panel"
		Me.panel.Size = New System.Drawing.Size(636, 206)
		Me.panel.TabIndex = 9
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
		Me.fingerView.ResultImageColor = System.Drawing.Color.Green
		Me.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView.Size = New System.Drawing.Size(634, 204)
		Me.fingerView.TabIndex = 0
		Me.fingerView.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView.TreeWidth = 2
		'
		'cancelScanningButton
		'
		Me.cancelScanningButton.Enabled = False
		Me.cancelScanningButton.Location = New System.Drawing.Point(168, 81)
		Me.cancelScanningButton.Name = "cancelScanningButton"
		Me.cancelScanningButton.Size = New System.Drawing.Size(75, 23)
		Me.cancelScanningButton.TabIndex = 11
		Me.cancelScanningButton.Text = "Cancel"
		Me.cancelScanningButton.UseVisualStyleBackColor = True
		'
		'refreshListButton
		'
		Me.refreshListButton.Location = New System.Drawing.Point(6, 81)
		Me.refreshListButton.Name = "refreshListButton"
		Me.refreshListButton.Size = New System.Drawing.Size(75, 23)
		Me.refreshListButton.TabIndex = 10
		Me.refreshListButton.Text = "Refresh list"
		Me.refreshListButton.UseVisualStyleBackColor = True
		'
		'scanButton
		'
		Me.scanButton.Location = New System.Drawing.Point(87, 81)
		Me.scanButton.Name = "scanButton"
		Me.scanButton.Size = New System.Drawing.Size(75, 23)
		Me.scanButton.TabIndex = 9
		Me.scanButton.Text = "Scan"
		Me.scanButton.UseVisualStyleBackColor = True
		'
		'scannersGroupBox
		'
		Me.scannersGroupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.scannersGroupBox.Controls.Add(Me.chbScanAutomatically)
		Me.scannersGroupBox.Controls.Add(Me.btnForce)
		Me.scannersGroupBox.Controls.Add(Me.cancelScanningButton)
		Me.scannersGroupBox.Controls.Add(Me.refreshListButton)
		Me.scannersGroupBox.Controls.Add(Me.scanButton)
		Me.scannersGroupBox.Controls.Add(Me.scannersListBox)
		Me.scannersGroupBox.Location = New System.Drawing.Point(3, 51)
		Me.scannersGroupBox.Name = "scannersGroupBox"
		Me.scannersGroupBox.Size = New System.Drawing.Size(636, 115)
		Me.scannersGroupBox.TabIndex = 11
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
		Me.chbScanAutomatically.TabIndex = 13
		Me.chbScanAutomatically.Text = "Scan automatically"
		Me.chbScanAutomatically.UseVisualStyleBackColor = True
		'
		'btnForce
		'
		Me.btnForce.Enabled = False
		Me.btnForce.Location = New System.Drawing.Point(249, 81)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(75, 23)
		Me.btnForce.TabIndex = 12
		Me.btnForce.Text = "Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'scannersListBox
		'
		Me.scannersListBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.scannersListBox.Location = New System.Drawing.Point(4, 19)
		Me.scannersListBox.Name = "scannersListBox"
		Me.scannersListBox.Size = New System.Drawing.Size(626, 56)
		Me.scannersListBox.TabIndex = 6
		'
		'saveTemplateButton
		'
		Me.saveTemplateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.saveTemplateButton.Enabled = False
		Me.saveTemplateButton.Location = New System.Drawing.Point(106, 407)
		Me.saveTemplateButton.Name = "saveTemplateButton"
		Me.saveTemplateButton.Size = New System.Drawing.Size(97, 23)
		Me.saveTemplateButton.TabIndex = 10
		Me.saveTemplateButton.Text = "Save t&emplate"
		Me.saveTemplateButton.UseVisualStyleBackColor = True
		'
		'scanWorker
		'
		Me.scanWorker.WorkerSupportsCancellation = True
		'
		'chbShowBinarizedImage
		'
		Me.chbShowBinarizedImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.chbShowBinarizedImage.AutoSize = True
		Me.chbShowBinarizedImage.Enabled = False
		Me.chbShowBinarizedImage.Location = New System.Drawing.Point(209, 411)
		Me.chbShowBinarizedImage.Name = "chbShowBinarizedImage"
		Me.chbShowBinarizedImage.Size = New System.Drawing.Size(136, 17)
		Me.chbShowBinarizedImage.TabIndex = 17
		Me.chbShowBinarizedImage.Text = "Show binarized image"
		Me.chbShowBinarizedImage.UseVisualStyleBackColor = True
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(364, 407)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(275, 23)
		Me.nViewZoomSlider1.TabIndex = 18
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.fingerView
		'
		'LicensePanel1
		'
		Me.LicensePanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
		   Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LicensePanel1.Location = New System.Drawing.Point(4, 3)
		Me.LicensePanel1.Name = "LicensePanel1"
		Me.LicensePanel1.OptionalComponents = "Images.WSQ"
		Me.LicensePanel1.RequiredComponents = "Biometrics.FingerExtraction,Devices.FingerScanners"
		Me.LicensePanel1.Size = New System.Drawing.Size(634, 45)
		Me.LicensePanel1.TabIndex = 14
		'
		'EnrollFromScanner
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.chbShowBinarizedImage)
		Me.Controls.Add(Me.LicensePanel1)
		Me.Controls.Add(Me.lblQuality)
		Me.Controls.Add(Me.saveImageButton)
		Me.Controls.Add(Me.panel)
		Me.Controls.Add(Me.scannersGroupBox)
		Me.Controls.Add(Me.saveTemplateButton)
		Me.Name = "EnrollFromScanner"
		Me.Size = New System.Drawing.Size(642, 436)
		Me.panel.ResumeLayout(False)
		Me.scannersGroupBox.ResumeLayout(False)
		Me.scannersGroupBox.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private lblQuality As System.Windows.Forms.Label
	Private toolTip As System.Windows.Forms.ToolTip
	Private WithEvents saveImageButton As System.Windows.Forms.Button
	Private panel As System.Windows.Forms.Panel
	Private WithEvents cancelScanningButton As System.Windows.Forms.Button
	Private WithEvents refreshListButton As System.Windows.Forms.Button
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents scanButton As System.Windows.Forms.Button
	Private scannersGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents scannersListBox As System.Windows.Forms.ListBox
	Private WithEvents saveTemplateButton As System.Windows.Forms.Button
	Private WithEvents scanWorker As System.ComponentModel.BackgroundWorker
	Private licensePanel As LicensePanel
	Friend WithEvents fingerView As Neurotec.Biometrics.Gui.NFingerView
	Friend WithEvents LicensePanel1 As Neurotec.Samples.LicensePanel
	Private WithEvents chbShowBinarizedImage As System.Windows.Forms.CheckBox
	Friend WithEvents chbScanAutomatically As System.Windows.Forms.CheckBox
	Friend WithEvents btnForce As System.Windows.Forms.Button
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class

