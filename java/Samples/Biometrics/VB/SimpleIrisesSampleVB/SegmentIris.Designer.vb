Imports Microsoft.VisualBasic
Imports System

Partial Public Class SegmentIris
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SegmentIris))
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.irisView2 = New Neurotec.Biometrics.Gui.NIrisView
		Me.tbGrayLevelSpread = New System.Windows.Forms.TextBox
		Me.btnSaveImage = New System.Windows.Forms.Button
		Me.lblGrayScaleUtilisation = New System.Windows.Forms.Label
		Me.tbInterlace = New System.Windows.Forms.TextBox
		Me.label12 = New System.Windows.Forms.Label
		Me.tbSharpness = New System.Windows.Forms.TextBox
		Me.label10 = New System.Windows.Forms.Label
		Me.tbMarginAdequacy = New System.Windows.Forms.TextBox
		Me.label9 = New System.Windows.Forms.Label
		Me.tbPupilBoundaryCircularity = New System.Windows.Forms.TextBox
		Me.label8 = New System.Windows.Forms.Label
		Me.tbIrisPupilConcentricity = New System.Windows.Forms.TextBox
		Me.label7 = New System.Windows.Forms.Label
		Me.tbIrisPupilContrast = New System.Windows.Forms.TextBox
		Me.label6 = New System.Windows.Forms.Label
		Me.tbIrisScleraContrast = New System.Windows.Forms.TextBox
		Me.label5 = New System.Windows.Forms.Label
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.tbUsableIrisArea = New System.Windows.Forms.TextBox
		Me.label4 = New System.Windows.Forms.Label
		Me.tbPupilToIrisRatio = New System.Windows.Forms.TextBox
		Me.label3 = New System.Windows.Forms.Label
		Me.tbQuality = New System.Windows.Forms.TextBox
		Me.label1 = New System.Windows.Forms.Label
		Me.irisView1 = New Neurotec.Biometrics.Gui.NIrisView
		Me.btnSegmentIris = New System.Windows.Forms.Button
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.btnOpenImage = New System.Windows.Forms.Button
		Me.gbResults = New System.Windows.Forms.GroupBox
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.gbResults.SuspendLayout()
		Me.SuspendLayout()
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(327, 177)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider2.TabIndex = 38
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.irisView2
		'
		'irisView2
		'
		Me.irisView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.irisView2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.irisView2.InnerBoundaryColor = System.Drawing.Color.Red
		Me.irisView2.InnerBoundaryWidth = 2
		Me.irisView2.Iris = Nothing
		Me.irisView2.Location = New System.Drawing.Point(6, 103)
		Me.irisView2.Name = "irisView2"
		Me.irisView2.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView2.OuterBoundaryWidth = 2
		Me.irisView2.Size = New System.Drawing.Size(570, 70)
		Me.irisView2.TabIndex = 11
		'
		'tbGrayLevelSpread
		'
		Me.tbGrayLevelSpread.Location = New System.Drawing.Point(173, 77)
		Me.tbGrayLevelSpread.Name = "tbGrayLevelSpread"
		Me.tbGrayLevelSpread.ReadOnly = True
		Me.tbGrayLevelSpread.Size = New System.Drawing.Size(48, 20)
		Me.tbGrayLevelSpread.TabIndex = 37
		'
		'btnSaveImage
		'
		Me.btnSaveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnSaveImage.Image = CType(resources.GetObject("btnSaveImage.Image"), System.Drawing.Image)
		Me.btnSaveImage.Location = New System.Drawing.Point(6, 177)
		Me.btnSaveImage.Name = "btnSaveImage"
		Me.btnSaveImage.Size = New System.Drawing.Size(96, 23)
		Me.btnSaveImage.TabIndex = 10
		Me.btnSaveImage.Text = "Save Image"
		Me.btnSaveImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveImage.UseVisualStyleBackColor = True
		'
		'lblGrayScaleUtilisation
		'
		Me.lblGrayScaleUtilisation.AutoSize = True
		Me.lblGrayScaleUtilisation.Location = New System.Drawing.Point(66, 80)
		Me.lblGrayScaleUtilisation.Name = "lblGrayScaleUtilisation"
		Me.lblGrayScaleUtilisation.Size = New System.Drawing.Size(110, 13)
		Me.lblGrayScaleUtilisation.TabIndex = 36
		Me.lblGrayScaleUtilisation.Text = "Gray Scale Utilisation:"
		'
		'tbInterlace
		'
		Me.tbInterlace.Location = New System.Drawing.Point(528, 33)
		Me.tbInterlace.Name = "tbInterlace"
		Me.tbInterlace.ReadOnly = True
		Me.tbInterlace.Size = New System.Drawing.Size(48, 20)
		Me.tbInterlace.TabIndex = 35
		'
		'label12
		'
		Me.label12.AutoSize = True
		Me.label12.Location = New System.Drawing.Point(475, 36)
		Me.label12.Name = "label12"
		Me.label12.Size = New System.Drawing.Size(57, 13)
		Me.label12.TabIndex = 34
		Me.label12.Text = "Interlace:"
		'
		'tbSharpness
		'
		Me.tbSharpness.Location = New System.Drawing.Point(528, 13)
		Me.tbSharpness.Name = "tbSharpness"
		Me.tbSharpness.ReadOnly = True
		Me.tbSharpness.Size = New System.Drawing.Size(48, 20)
		Me.tbSharpness.TabIndex = 31
		'
		'label10
		'
		Me.label10.AutoSize = True
		Me.label10.Location = New System.Drawing.Point(472, 14)
		Me.label10.Name = "label10"
		Me.label10.Size = New System.Drawing.Size(60, 13)
		Me.label10.TabIndex = 30
		Me.label10.Text = "Sharpness:"
		'
		'tbMarginAdequacy
		'
		Me.tbMarginAdequacy.Location = New System.Drawing.Point(528, 54)
		Me.tbMarginAdequacy.Name = "tbMarginAdequacy"
		Me.tbMarginAdequacy.ReadOnly = True
		Me.tbMarginAdequacy.Size = New System.Drawing.Size(48, 20)
		Me.tbMarginAdequacy.TabIndex = 29
		'
		'label9
		'
		Me.label9.AutoSize = True
		Me.label9.Location = New System.Drawing.Point(439, 57)
		Me.label9.Name = "label9"
		Me.label9.Size = New System.Drawing.Size(93, 13)
		Me.label9.TabIndex = 28
		Me.label9.Text = "Margin Adequacy:"
		'
		'tbPupilBoundaryCircularity
		'
		Me.tbPupilBoundaryCircularity.Location = New System.Drawing.Point(362, 74)
		Me.tbPupilBoundaryCircularity.Name = "tbPupilBoundaryCircularity"
		Me.tbPupilBoundaryCircularity.ReadOnly = True
		Me.tbPupilBoundaryCircularity.Size = New System.Drawing.Size(48, 20)
		Me.tbPupilBoundaryCircularity.TabIndex = 27
		'
		'label8
		'
		Me.label8.AutoSize = True
		Me.label8.Location = New System.Drawing.Point(233, 77)
		Me.label8.Name = "label8"
		Me.label8.Size = New System.Drawing.Size(129, 13)
		Me.label8.TabIndex = 26
		Me.label8.Text = "Pupil Boundary Circularity:"
		'
		'tbIrisPupilConcentricity
		'
		Me.tbIrisPupilConcentricity.Location = New System.Drawing.Point(362, 54)
		Me.tbIrisPupilConcentricity.Name = "tbIrisPupilConcentricity"
		Me.tbIrisPupilConcentricity.ReadOnly = True
		Me.tbIrisPupilConcentricity.Size = New System.Drawing.Size(48, 20)
		Me.tbIrisPupilConcentricity.TabIndex = 25
		'
		'label7
		'
		Me.label7.AutoSize = True
		Me.label7.Location = New System.Drawing.Point(249, 57)
		Me.label7.Name = "label7"
		Me.label7.Size = New System.Drawing.Size(113, 13)
		Me.label7.TabIndex = 24
		Me.label7.Text = "Iris Pupil Concentricity:"
		'
		'tbIrisPupilContrast
		'
		Me.tbIrisPupilContrast.Location = New System.Drawing.Point(362, 34)
		Me.tbIrisPupilContrast.Name = "tbIrisPupilContrast"
		Me.tbIrisPupilContrast.ReadOnly = True
		Me.tbIrisPupilContrast.Size = New System.Drawing.Size(48, 20)
		Me.tbIrisPupilContrast.TabIndex = 23
		'
		'label6
		'
		Me.label6.AutoSize = True
		Me.label6.Location = New System.Drawing.Point(271, 37)
		Me.label6.Name = "label6"
		Me.label6.Size = New System.Drawing.Size(91, 13)
		Me.label6.TabIndex = 22
		Me.label6.Text = "Iris Pupil Contrast:"
		'
		'tbIrisScleraContrast
		'
		Me.tbIrisScleraContrast.Location = New System.Drawing.Point(362, 13)
		Me.tbIrisScleraContrast.Name = "tbIrisScleraContrast"
		Me.tbIrisScleraContrast.ReadOnly = True
		Me.tbIrisScleraContrast.Size = New System.Drawing.Size(48, 20)
		Me.tbIrisScleraContrast.TabIndex = 21
		'
		'label5
		'
		Me.label5.AutoSize = True
		Me.label5.Location = New System.Drawing.Point(264, 16)
		Me.label5.Name = "label5"
		Me.label5.Size = New System.Drawing.Size(98, 13)
		Me.label5.TabIndex = 20
		Me.label5.Text = "Iris Sclera Contrast:"
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 1)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Biometrics.IrisSegmentation"
		Me.licensePanel.Size = New System.Drawing.Size(594, 45)
		Me.licensePanel.TabIndex = 20
		'
		'tbUsableIrisArea
		'
		Me.tbUsableIrisArea.Location = New System.Drawing.Point(173, 57)
		Me.tbUsableIrisArea.Name = "tbUsableIrisArea"
		Me.tbUsableIrisArea.ReadOnly = True
		Me.tbUsableIrisArea.Size = New System.Drawing.Size(48, 20)
		Me.tbUsableIrisArea.TabIndex = 19
		'
		'label4
		'
		Me.label4.AutoSize = True
		Me.label4.Location = New System.Drawing.Point(93, 60)
		Me.label4.Name = "label4"
		Me.label4.Size = New System.Drawing.Size(84, 13)
		Me.label4.TabIndex = 18
		Me.label4.Text = "Usable Iris Area:"
		'
		'tbPupilToIrisRatio
		'
		Me.tbPupilToIrisRatio.Location = New System.Drawing.Point(173, 34)
		Me.tbPupilToIrisRatio.Name = "tbPupilToIrisRatio"
		Me.tbPupilToIrisRatio.ReadOnly = True
		Me.tbPupilToIrisRatio.Size = New System.Drawing.Size(48, 20)
		Me.tbPupilToIrisRatio.TabIndex = 17
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Location = New System.Drawing.Point(83, 36)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(93, 13)
		Me.label3.TabIndex = 16
		Me.label3.Text = "Pupil To Iris Ratio:"
		'
		'tbQuality
		'
		Me.tbQuality.Location = New System.Drawing.Point(173, 13)
		Me.tbQuality.Name = "tbQuality"
		Me.tbQuality.ReadOnly = True
		Me.tbQuality.Size = New System.Drawing.Size(48, 20)
		Me.tbQuality.TabIndex = 13
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(134, 16)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(42, 13)
		Me.label1.TabIndex = 12
		Me.label1.Text = "Quality:"
		'
		'irisView1
		'
		Me.irisView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.irisView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.irisView1.InnerBoundaryColor = System.Drawing.Color.Red
		Me.irisView1.InnerBoundaryWidth = 2
		Me.irisView1.Iris = Nothing
		Me.irisView1.Location = New System.Drawing.Point(3, 32)
		Me.irisView1.Name = "irisView1"
		Me.irisView1.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView1.OuterBoundaryWidth = 2
		Me.irisView1.Size = New System.Drawing.Size(582, 75)
		Me.irisView1.TabIndex = 0
		'
		'btnSegmentIris
		'
		Me.btnSegmentIris.Location = New System.Drawing.Point(3, 3)
		Me.btnSegmentIris.Name = "btnSegmentIris"
		Me.btnSegmentIris.Size = New System.Drawing.Size(92, 23)
		Me.btnSegmentIris.TabIndex = 13
		Me.btnSegmentIris.Text = "Segment Iris"
		Me.btnSegmentIris.UseVisualStyleBackColor = True
		'
		'splitContainer1
		'
		Me.splitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.splitContainer1.Location = New System.Drawing.Point(3, 52)
		Me.splitContainer1.Name = "splitContainer1"
		Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.nViewZoomSlider1)
		Me.splitContainer1.Panel1.Controls.Add(Me.irisView1)
		Me.splitContainer1.Panel1.Controls.Add(Me.btnOpenImage)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.btnSegmentIris)
		Me.splitContainer1.Panel2.Controls.Add(Me.gbResults)
		Me.splitContainer1.Size = New System.Drawing.Size(588, 349)
		Me.splitContainer1.SplitterDistance = 110
		Me.splitContainer1.TabIndex = 19
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(336, 3)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider1.TabIndex = 38
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.irisView1
		'
		'btnOpenImage
		'
		Me.btnOpenImage.Image = CType(resources.GetObject("btnOpenImage.Image"), System.Drawing.Image)
		Me.btnOpenImage.Location = New System.Drawing.Point(3, 3)
		Me.btnOpenImage.Name = "btnOpenImage"
		Me.btnOpenImage.Size = New System.Drawing.Size(92, 23)
		Me.btnOpenImage.TabIndex = 9
		Me.btnOpenImage.Text = "Open Image"
		Me.btnOpenImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpenImage.UseVisualStyleBackColor = True
		'
		'gbResults
		'
		Me.gbResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.gbResults.Controls.Add(Me.nViewZoomSlider2)
		Me.gbResults.Controls.Add(Me.irisView2)
		Me.gbResults.Controls.Add(Me.tbGrayLevelSpread)
		Me.gbResults.Controls.Add(Me.btnSaveImage)
		Me.gbResults.Controls.Add(Me.lblGrayScaleUtilisation)
		Me.gbResults.Controls.Add(Me.tbInterlace)
		Me.gbResults.Controls.Add(Me.label12)
		Me.gbResults.Controls.Add(Me.tbSharpness)
		Me.gbResults.Controls.Add(Me.label10)
		Me.gbResults.Controls.Add(Me.tbMarginAdequacy)
		Me.gbResults.Controls.Add(Me.label9)
		Me.gbResults.Controls.Add(Me.tbPupilBoundaryCircularity)
		Me.gbResults.Controls.Add(Me.label8)
		Me.gbResults.Controls.Add(Me.tbIrisPupilConcentricity)
		Me.gbResults.Controls.Add(Me.label7)
		Me.gbResults.Controls.Add(Me.tbIrisPupilContrast)
		Me.gbResults.Controls.Add(Me.label6)
		Me.gbResults.Controls.Add(Me.tbIrisScleraContrast)
		Me.gbResults.Controls.Add(Me.label5)
		Me.gbResults.Controls.Add(Me.tbUsableIrisArea)
		Me.gbResults.Controls.Add(Me.label4)
		Me.gbResults.Controls.Add(Me.tbPupilToIrisRatio)
		Me.gbResults.Controls.Add(Me.label3)
		Me.gbResults.Controls.Add(Me.tbQuality)
		Me.gbResults.Controls.Add(Me.label1)
		Me.gbResults.Location = New System.Drawing.Point(3, 32)
		Me.gbResults.Name = "gbResults"
		Me.gbResults.Size = New System.Drawing.Size(582, 200)
		Me.gbResults.TabIndex = 12
		Me.gbResults.TabStop = False
		Me.gbResults.Text = "Results"
		'
		'SegmentIris
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.splitContainer1)
		Me.Name = "SegmentIris"
		Me.Size = New System.Drawing.Size(594, 403)
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.gbResults.ResumeLayout(False)
		Me.gbResults.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents irisView2 As Neurotec.Biometrics.Gui.NIrisView
	Private WithEvents tbGrayLevelSpread As System.Windows.Forms.TextBox
	Private WithEvents btnSaveImage As System.Windows.Forms.Button
	Private WithEvents lblGrayScaleUtilisation As System.Windows.Forms.Label
	Private WithEvents tbInterlace As System.Windows.Forms.TextBox
	Private WithEvents label12 As System.Windows.Forms.Label
	Private WithEvents tbSharpness As System.Windows.Forms.TextBox
	Private WithEvents label10 As System.Windows.Forms.Label
	Private WithEvents tbMarginAdequacy As System.Windows.Forms.TextBox
	Private WithEvents label9 As System.Windows.Forms.Label
	Private WithEvents tbPupilBoundaryCircularity As System.Windows.Forms.TextBox
	Private WithEvents label8 As System.Windows.Forms.Label
	Private WithEvents tbIrisPupilConcentricity As System.Windows.Forms.TextBox
	Private WithEvents label7 As System.Windows.Forms.Label
	Private WithEvents tbIrisPupilContrast As System.Windows.Forms.TextBox
	Private WithEvents label6 As System.Windows.Forms.Label
	Private WithEvents tbIrisScleraContrast As System.Windows.Forms.TextBox
	Private WithEvents label5 As System.Windows.Forms.Label
	Private WithEvents licensePanel As Neurotec.Samples.LicensePanel
	Private WithEvents tbUsableIrisArea As System.Windows.Forms.TextBox
	Private WithEvents label4 As System.Windows.Forms.Label
	Private WithEvents tbPupilToIrisRatio As System.Windows.Forms.TextBox
	Private WithEvents label3 As System.Windows.Forms.Label
	Private WithEvents tbQuality As System.Windows.Forms.TextBox
	Private WithEvents label1 As System.Windows.Forms.Label
	Private WithEvents irisView1 As Neurotec.Biometrics.Gui.NIrisView
	Private WithEvents btnSegmentIris As System.Windows.Forms.Button
	Private WithEvents splitContainer1 As System.Windows.Forms.SplitContainer
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents btnOpenImage As System.Windows.Forms.Button
	Private WithEvents gbResults As System.Windows.Forms.GroupBox
End Class

