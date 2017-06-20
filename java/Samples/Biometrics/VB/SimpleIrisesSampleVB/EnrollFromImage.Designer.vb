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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EnrollFromImage))
		Me.panel2 = New System.Windows.Forms.Panel
		Me.lblQuality = New System.Windows.Forms.Label
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.panel1 = New System.Windows.Forms.Panel
		Me.btnOpen = New System.Windows.Forms.Button
		Me.irisView = New Neurotec.Biometrics.Gui.NIrisView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.panel2.SuspendLayout()
		Me.panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'panel2
		'
		Me.panel2.Controls.Add(Me.nViewZoomSlider1)
		Me.panel2.Controls.Add(Me.lblQuality)
		Me.panel2.Controls.Add(Me.btnSaveTemplate)
		Me.panel2.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.panel2.Location = New System.Drawing.Point(0, 335)
		Me.panel2.Name = "panel2"
		Me.panel2.Size = New System.Drawing.Size(497, 46)
		Me.panel2.TabIndex = 7
		'
		'lblQuality
		'
		Me.lblQuality.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblQuality.Location = New System.Drawing.Point(315, 11)
		Me.lblQuality.Name = "lblQuality"
		Me.lblQuality.Size = New System.Drawing.Size(170, 23)
		Me.lblQuality.TabIndex = 1
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Location = New System.Drawing.Point(3, 11)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(96, 23)
		Me.btnSaveTemplate.TabIndex = 6
		Me.btnSaveTemplate.Text = "&Save Template"
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'licensePanel
		'
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Dock = System.Windows.Forms.DockStyle.Top
		Me.licensePanel.Location = New System.Drawing.Point(0, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.IrisExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(497, 45)
		Me.licensePanel.TabIndex = 8
		'
		'panel1
		'
		Me.panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.panel1.Controls.Add(Me.btnOpen)
		Me.panel1.Location = New System.Drawing.Point(0, 51)
		Me.panel1.Name = "panel1"
		Me.panel1.Size = New System.Drawing.Size(497, 35)
		Me.panel1.TabIndex = 5
		'
		'btnOpen
		'
		Me.btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), System.Drawing.Image)
		Me.btnOpen.Location = New System.Drawing.Point(3, 3)
		Me.btnOpen.Name = "btnOpen"
		Me.btnOpen.Size = New System.Drawing.Size(93, 23)
		Me.btnOpen.TabIndex = 3
		Me.btnOpen.Tag = "Open"
		Me.btnOpen.Text = "Open Image"
		Me.btnOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpen.UseVisualStyleBackColor = True
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
		Me.irisView.Location = New System.Drawing.Point(3, 84)
		Me.irisView.Name = "irisView"
		Me.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView.OuterBoundaryWidth = 2
		Me.irisView.Size = New System.Drawing.Size(494, 245)
		Me.irisView.TabIndex = 9
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(245, 11)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider1.TabIndex = 8
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.irisView
		'
		'EnrollFromImage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.irisView)
		Me.Controls.Add(Me.panel1)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.panel2)
		Me.Name = "EnrollFromImage"
		Me.Size = New System.Drawing.Size(497, 381)
		Me.panel2.ResumeLayout(False)
		Me.panel1.ResumeLayout(False)
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private panel2 As System.Windows.Forms.Panel
	Private lblQuality As System.Windows.Forms.Label
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private licensePanel As LicensePanel
	Private WithEvents panel1 As System.Windows.Forms.Panel
	Private WithEvents btnOpen As System.Windows.Forms.Button
	Private WithEvents irisView As Neurotec.Biometrics.Gui.NIrisView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class

