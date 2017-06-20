Imports Microsoft.VisualBasic
Imports System

Partial Public Class IdentifyFace
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(IdentifyFace))
		Me.label2 = New System.Windows.Forms.Label
		Me.templatesCountLabel = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.label1 = New System.Windows.Forms.Label
		Me.identifyButton = New System.Windows.Forms.Button
		Me.groupBox2 = New System.Windows.Forms.GroupBox
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.faceView = New Neurotec.Biometrics.Gui.NFaceView
		Me.fileForIdentificationLabel = New System.Windows.Forms.Label
		Me.openImageButton = New System.Windows.Forms.Button
		Me.groupBox1 = New System.Windows.Forms.GroupBox
		Me.openTemplatesButton = New System.Windows.Forms.Button
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.matchingGroupBox = New System.Windows.Forms.GroupBox
		Me.defaultMatchingFARButton = New System.Windows.Forms.Button
		Me.matchingFarComboBox = New System.Windows.Forms.ComboBox
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.listView = New System.Windows.Forms.ListView
		Me.groupBox3 = New System.Windows.Forms.GroupBox
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.licensePanel = New Neurotec.Samples.LicensePanel
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
		'templatesCountLabel
		'
		Me.templatesCountLabel.AutoSize = True
		Me.templatesCountLabel.Location = New System.Drawing.Point(142, 34)
		Me.templatesCountLabel.Name = "templatesCountLabel"
		Me.templatesCountLabel.Size = New System.Drawing.Size(82, 13)
		Me.templatesCountLabel.TabIndex = 7
		Me.templatesCountLabel.Text = "templates count"
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
		'identifyButton
		'
		Me.identifyButton.Enabled = False
		Me.identifyButton.Location = New System.Drawing.Point(9, 25)
		Me.identifyButton.Name = "identifyButton"
		Me.identifyButton.Size = New System.Drawing.Size(92, 23)
		Me.identifyButton.TabIndex = 0
		Me.identifyButton.Text = "&Identify"
		Me.identifyButton.UseVisualStyleBackColor = True
		'
		'groupBox2
		'
		Me.groupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox2.Controls.Add(Me.nViewZoomSlider1)
		Me.groupBox2.Controls.Add(Me.faceView)
		Me.groupBox2.Controls.Add(Me.fileForIdentificationLabel)
		Me.groupBox2.Controls.Add(Me.openImageButton)
		Me.groupBox2.Location = New System.Drawing.Point(3, 109)
		Me.groupBox2.Name = "groupBox2"
		Me.groupBox2.Size = New System.Drawing.Size(461, 101)
		Me.groupBox2.TabIndex = 13
		Me.groupBox2.TabStop = False
		Me.groupBox2.Text = "Image / template for identification"
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(182, 72)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(273, 23)
		Me.nViewZoomSlider1.TabIndex = 17
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.faceView
		'
		'faceView
		'
		Me.faceView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.faceView.Face = Nothing
		Me.faceView.FaceIds = Nothing
		Me.faceView.Location = New System.Drawing.Point(9, 16)
		Me.faceView.Name = "faceView"
		Me.faceView.Size = New System.Drawing.Size(446, 50)
		Me.faceView.TabIndex = 11
		'
		'fileForIdentificationLabel
		'
		Me.fileForIdentificationLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.fileForIdentificationLabel.Location = New System.Drawing.Point(107, 77)
		Me.fileForIdentificationLabel.Name = "fileForIdentificationLabel"
		Me.fileForIdentificationLabel.Size = New System.Drawing.Size(348, 13)
		Me.fileForIdentificationLabel.TabIndex = 10
		'
		'openImageButton
		'
		Me.openImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.openImageButton.Image = CType(resources.GetObject("openImageButton.Image"), System.Drawing.Image)
		Me.openImageButton.Location = New System.Drawing.Point(9, 72)
		Me.openImageButton.Name = "openImageButton"
		Me.openImageButton.Size = New System.Drawing.Size(92, 23)
		Me.openImageButton.TabIndex = 8
		Me.openImageButton.Text = "Open"
		Me.openImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.openImageButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.toolTip.SetToolTip(Me.openImageButton, "Open fingerprint image for indentification")
		Me.openImageButton.UseVisualStyleBackColor = True
		'
		'groupBox1
		'
		Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox1.Controls.Add(Me.templatesCountLabel)
		Me.groupBox1.Controls.Add(Me.label1)
		Me.groupBox1.Controls.Add(Me.openTemplatesButton)
		Me.groupBox1.Location = New System.Drawing.Point(3, 46)
		Me.groupBox1.Name = "groupBox1"
		Me.groupBox1.Size = New System.Drawing.Size(461, 57)
		Me.groupBox1.TabIndex = 12
		Me.groupBox1.TabStop = False
		Me.groupBox1.Text = "Templates loading"
		'
		'openTemplatesButton
		'
		Me.openTemplatesButton.Image = CType(resources.GetObject("openTemplatesButton.Image"), System.Drawing.Image)
		Me.openTemplatesButton.Location = New System.Drawing.Point(6, 24)
		Me.openTemplatesButton.Name = "openTemplatesButton"
		Me.openTemplatesButton.Size = New System.Drawing.Size(30, 23)
		Me.openTemplatesButton.TabIndex = 5
		Me.toolTip.SetToolTip(Me.openTemplatesButton, "Open templates files (*.dat) ")
		Me.openTemplatesButton.UseVisualStyleBackColor = True
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Score"
		Me.columnHeader2.Width = 100
		'
		'matchingGroupBox
		'
		Me.matchingGroupBox.Controls.Add(Me.defaultMatchingFARButton)
		Me.matchingGroupBox.Controls.Add(Me.matchingFarComboBox)
		Me.matchingGroupBox.Location = New System.Drawing.Point(112, 8)
		Me.matchingGroupBox.Name = "matchingGroupBox"
		Me.matchingGroupBox.Size = New System.Drawing.Size(159, 43)
		Me.matchingGroupBox.TabIndex = 21
		Me.matchingGroupBox.TabStop = False
		Me.matchingGroupBox.Text = "Matching FAR"
		'
		'defaultMatchingFARButton
		'
		Me.defaultMatchingFARButton.Location = New System.Drawing.Point(88, 17)
		Me.defaultMatchingFARButton.Name = "defaultMatchingFARButton"
		Me.defaultMatchingFARButton.Size = New System.Drawing.Size(63, 23)
		Me.defaultMatchingFARButton.TabIndex = 20
		Me.defaultMatchingFARButton.Text = "Default"
		Me.defaultMatchingFARButton.UseVisualStyleBackColor = True
		'
		'matchingFarComboBox
		'
		Me.matchingFarComboBox.FormattingEnabled = True
		Me.matchingFarComboBox.Location = New System.Drawing.Point(9, 19)
		Me.matchingFarComboBox.Name = "matchingFarComboBox"
		Me.matchingFarComboBox.Size = New System.Drawing.Size(73, 21)
		Me.matchingFarComboBox.TabIndex = 19
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
		Me.listView.Location = New System.Drawing.Point(6, 53)
		Me.listView.Name = "listView"
		Me.listView.Size = New System.Drawing.Size(452, 133)
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
		Me.groupBox3.Controls.Add(Me.identifyButton)
		Me.groupBox3.Location = New System.Drawing.Point(0, 216)
		Me.groupBox3.Name = "groupBox3"
		Me.groupBox3.Size = New System.Drawing.Size(464, 192)
		Me.groupBox3.TabIndex = 14
		Me.groupBox3.TabStop = False
		Me.groupBox3.Text = "Identification"
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
		Me.licensePanel.Size = New System.Drawing.Size(461, 45)
		Me.licensePanel.TabIndex = 15
		'
		'IdentifyFace
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.groupBox2)
		Me.Controls.Add(Me.groupBox1)
		Me.Controls.Add(Me.groupBox3)
		Me.Name = "IdentifyFace"
		Me.Size = New System.Drawing.Size(467, 411)
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
	Private templatesCountLabel As System.Windows.Forms.Label
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private label1 As System.Windows.Forms.Label
	Private WithEvents identifyButton As System.Windows.Forms.Button
	Private groupBox2 As System.Windows.Forms.GroupBox
	Private fileForIdentificationLabel As System.Windows.Forms.Label
	Private WithEvents openImageButton As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
	Private groupBox1 As System.Windows.Forms.GroupBox
	Private WithEvents openTemplatesButton As System.Windows.Forms.Button
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private matchingGroupBox As System.Windows.Forms.GroupBox
	Private WithEvents defaultMatchingFARButton As System.Windows.Forms.Button
	Private WithEvents matchingFarComboBox As System.Windows.Forms.ComboBox
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private listView As System.Windows.Forms.ListView
	Private groupBox3 As System.Windows.Forms.GroupBox
	Private licensePanel As LicensePanel
	Private WithEvents faceView As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class

