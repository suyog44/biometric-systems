Imports Microsoft.VisualBasic
Imports System

Partial Public Class IdentifyIris
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IdentifyIris))
		Me.label2 = New System.Windows.Forms.Label
		Me.lblTemplatesCount = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.label1 = New System.Windows.Forms.Label
		Me.btnIdentify = New System.Windows.Forms.Button
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.irisView = New Neurotec.Biometrics.Gui.NIrisView
		Me.lblFileForIdentification = New System.Windows.Forms.Label
		Me.btnOpenImage = New System.Windows.Forms.Button
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.btnOpenTemplates = New System.Windows.Forms.Button
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.matchingGroupBox = New System.Windows.Forms.GroupBox
		Me.btnDefault = New System.Windows.Forms.Button
		Me.cbMatchingFar = New System.Windows.Forms.ComboBox
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.listView = New System.Windows.Forms.ListView
		Me.groupBox3 = New System.Windows.Forms.GroupBox
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.groupBox2.SuspendLayout()
		Me.groupBox1.SuspendLayout()
		Me.matchingGroupBox.SuspendLayout()
		Me.groupBox3.SuspendLayout()
		Me.SuspendLayout()
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(12, 56)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(0, 13)
		Me.label2.TabIndex = 1
		'
		'lblTemplatesCount
		'
		Me.lblTemplatesCount.AutoSize = True
		Me.lblTemplatesCount.Location = New System.Drawing.Point(142, 34)
		Me.lblTemplatesCount.Name = "lblTemplatesCount"
		Me.lblTemplatesCount.Size = New System.Drawing.Size(82, 13)
		Me.lblTemplatesCount.TabIndex = 7
		Me.lblTemplatesCount.Text = "templates count"
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(42, 34)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(94, 13)
		Me.label1.TabIndex = 6
		Me.label1.Text = "Templates loaded:"
		'
		'btnIdentify
		'
		Me.btnIdentify.Enabled = False
		Me.btnIdentify.Location = New System.Drawing.Point(9, 19)
		Me.btnIdentify.Name = "btnIdentify"
		Me.btnIdentify.Size = New System.Drawing.Size(92, 23)
		Me.btnIdentify.TabIndex = 0
		Me.btnIdentify.Text = "&Identify"
		Me.btnIdentify.UseVisualStyleBackColor = True
		'
		'groupBox2
		'
		Me.groupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox2.Controls.Add(Me.nViewZoomSlider1)
		Me.groupBox2.Controls.Add(Me.irisView)
		Me.groupBox2.Controls.Add(Me.lblFileForIdentification)
		Me.groupBox2.Controls.Add(Me.btnOpenImage)
		Me.groupBox2.Location = New System.Drawing.Point(3, 114)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(547, 161)
		Me.groupBox2.TabIndex = 13
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Image for identification"
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
		Me.irisView.Location = New System.Drawing.Point(6, 19)
		Me.irisView.Name = "irisView"
		Me.irisView.OuterBoundaryColor = System.Drawing.Color.GreenYellow
		Me.irisView.OuterBoundaryWidth = 2
		Me.irisView.Size = New System.Drawing.Size(535, 94)
		Me.irisView.TabIndex = 11
		'
		'lblFileForIdentification
		'
		Me.lblFileForIdentification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.lblFileForIdentification.Location = New System.Drawing.Point(3, 145)
		Me.lblFileForIdentification.Name = "lblFileForIdentification"
		Me.lblFileForIdentification.Size = New System.Drawing.Size(434, 13)
		Me.lblFileForIdentification.TabIndex = 10
		Me.lblFileForIdentification.Text = "file for identification"
		'
		'btnOpenImage
		'
		Me.btnOpenImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnOpenImage.Image = CType(resources.GetObject("btnOpenImage.Image"), System.Drawing.Image)
		Me.btnOpenImage.Location = New System.Drawing.Point(6, 119)
		Me.btnOpenImage.Name = "btnOpenImage"
		Me.btnOpenImage.Size = New System.Drawing.Size(92, 23)
		Me.btnOpenImage.TabIndex = 8
		Me.btnOpenImage.Text = "Open Image"
		Me.btnOpenImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpenImage.UseVisualStyleBackColor = True
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.lblTemplatesCount)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.btnOpenTemplates)
		Me.groupBox1.Location = New System.Drawing.Point(3, 51)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(544, 57)
		Me.groupBox1.TabIndex = 12
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Templates loading"
		'
		'btnOpenTemplates
		'
		Me.btnOpenTemplates.Image = CType(resources.GetObject("btnOpenTemplates.Image"), System.Drawing.Image)
		Me.btnOpenTemplates.Location = New System.Drawing.Point(6, 24)
		Me.btnOpenTemplates.Name = "btnOpenTemplates"
		Me.btnOpenTemplates.Size = New System.Drawing.Size(30, 23)
		Me.btnOpenTemplates.TabIndex = 5
		Me.btnOpenTemplates.UseVisualStyleBackColor = True
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Score"
		Me.columnHeader2.Width = 100
		'
		'matchingGroupBox
		'
		Me.matchingGroupBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.matchingGroupBox.Controls.Add(Me.btnDefault)
		Me.matchingGroupBox.Controls.Add(Me.cbMatchingFar)
		Me.matchingGroupBox.Location = New System.Drawing.Point(383, 14)
		Me.matchingGroupBox.Name = "matchingGroupBox"
		Me.matchingGroupBox.Size = New System.Drawing.Size(155, 45)
		Me.matchingGroupBox.TabIndex = 21
		Me.matchingGroupBox.TabStop = False
		Me.matchingGroupBox.Text = "Matching FAR"
		'
		'btnDefault
		'
		Me.btnDefault.Location = New System.Drawing.Point(88, 16)
		Me.btnDefault.Name = "btnDefault"
		Me.btnDefault.Size = New System.Drawing.Size(63, 23)
		Me.btnDefault.TabIndex = 20
		Me.btnDefault.Text = "Default"
		Me.btnDefault.UseVisualStyleBackColor = True
		'
		'cbMatchingFar
		'
		Me.cbMatchingFar.FormattingEnabled = True
		Me.cbMatchingFar.Location = New System.Drawing.Point(9, 19)
		Me.cbMatchingFar.Name = "cbMatchingFar"
		Me.cbMatchingFar.Size = New System.Drawing.Size(73, 21)
		Me.cbMatchingFar.TabIndex = 19
		'
		'columnHeader1
		'
		Me.columnHeader1.Text = "ID"
		Me.columnHeader1.Width = 300
		'
		'listView
		'
		Me.listView.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.listView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2})
		Me.listView.Location = New System.Drawing.Point(9, 65)
		Me.listView.Name = "listView"
		Me.listView.Size = New System.Drawing.Size(529, 127)
		Me.listView.TabIndex = 2
		Me.listView.UseCompatibleStateImageBehavior = False
		Me.listView.View = System.Windows.Forms.View.Details
		'
		'groupBox3
		'
		Me.groupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox3.Controls.Add(Me.matchingGroupBox)
		Me.groupBox3.Controls.Add(Me.listView)
		Me.groupBox3.Controls.Add(Me.label2)
		Me.groupBox3.Controls.Add(Me.btnIdentify)
		Me.groupBox3.Location = New System.Drawing.Point(3, 281)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(544, 198)
		Me.groupBox3.TabIndex = 14
		Me.groupBox3.TabStop = False
		Me.groupBox3.Text = "Identification"
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.IrisExtraction,Biometrics.IrisMatching"
		Me.licensePanel.Size = New System.Drawing.Size(550, 45)
		Me.licensePanel.TabIndex = 15
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(292, 119)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider1.TabIndex = 9
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.irisView
		'
		'IdentifyIris
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.groupBox3)
		Me.Name = "IdentifyIris"
		Me.Size = New System.Drawing.Size(550, 482)
		Me.groupBox2.ResumeLayout(False)
		Me.groupBox1.ResumeLayout(False)
		Me.groupBox1.PerformLayout()
		Me.matchingGroupBox.ResumeLayout(False)
		Me.groupBox3.ResumeLayout(False)
		Me.groupBox3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private label2 As System.Windows.Forms.Label
	Private lblTemplatesCount As System.Windows.Forms.Label
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private label1 As System.Windows.Forms.Label
	Private WithEvents btnIdentify As System.Windows.Forms.Button
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private lblFileForIdentification As System.Windows.Forms.Label
	Private WithEvents btnOpenImage As System.Windows.Forms.Button
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private WithEvents btnOpenTemplates As System.Windows.Forms.Button
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private matchingGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents btnDefault As System.Windows.Forms.Button
	Private WithEvents cbMatchingFar As System.Windows.Forms.ComboBox
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView As System.Windows.Forms.ListView
	Private groupBox3 As System.Windows.Forms.GroupBox
	Private licensePanel As LicensePanel
	Private WithEvents irisView As Neurotec.Biometrics.Gui.NIrisView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class

