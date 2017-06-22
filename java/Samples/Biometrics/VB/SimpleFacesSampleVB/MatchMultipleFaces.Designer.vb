Imports Microsoft.VisualBasic
Imports System

Partial Public Class MatchMultipleFaces
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
		Me.tsbOpenReference = New System.Windows.Forms.ToolStripButton
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.tsbOpenMultifaceImage = New System.Windows.Forms.ToolStripButton
		Me.openImageFileDlg = New System.Windows.Forms.OpenFileDialog
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.label1 = New System.Windows.Forms.Label
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.faceViewMultiFace = New Neurotec.Biometrics.Gui.NFaceView
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.faceViewReference = New Neurotec.Biometrics.Gui.NFaceView
		Me.label2 = New System.Windows.Forms.Label
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.toolStrip1.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'tsbOpenReference
		'
		Me.tsbOpenReference.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.tsbOpenReference.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenReference.Name = "tsbOpenReference"
		Me.tsbOpenReference.Size = New System.Drawing.Size(156, 22)
		Me.tsbOpenReference.Text = "Open Reference Image..."
		'
		'toolStrip1
		'
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenReference, Me.tsbOpenMultifaceImage})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(570, 25)
		Me.toolStrip1.TabIndex = 3
		Me.toolStrip1.Text = "toolStrip1"
		'
		'tsbOpenMultifaceImage
		'
		Me.tsbOpenMultifaceImage.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.tsbOpenMultifaceImage.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenMultifaceImage.Name = "tsbOpenMultifaceImage"
		Me.tsbOpenMultifaceImage.Size = New System.Drawing.Size(154, 22)
		Me.tsbOpenMultifaceImage.Text = "Open Multiface Image..."
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
		Me.tableLayoutPanel1.AutoSize = True
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider1, 1, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.faceViewMultiFace, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.nViewZoomSlider2, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.faceViewReference, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 1, 0)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 79)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 3
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(570, 317)
		Me.tableLayoutPanel1.TabIndex = 18
		'
		'label1
		'
		Me.label1.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.label1.Dock = System.Windows.Forms.DockStyle.Top
		Me.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(279, 20)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Reference Image"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(288, 288)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(279, 26)
		Me.nViewZoomSlider1.TabIndex = 7
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.faceViewMultiFace
		'
		'faceViewMultiFace
		'
		Me.faceViewMultiFace.AutoScrollMinSize = New System.Drawing.Size(1, 1)
		Me.faceViewMultiFace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.faceViewMultiFace.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceViewMultiFace.Face = Nothing
		Me.faceViewMultiFace.FaceIds = Nothing
		Me.faceViewMultiFace.Location = New System.Drawing.Point(288, 23)
		Me.faceViewMultiFace.Name = "faceViewMultiFace"
		Me.faceViewMultiFace.ShowFaceConfidence = False
		Me.faceViewMultiFace.Size = New System.Drawing.Size(279, 259)
		Me.faceViewMultiFace.TabIndex = 2
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(3, 288)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(279, 26)
		Me.nViewZoomSlider2.TabIndex = 6
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.faceViewReference
		'
		'faceViewReference
		'
		Me.faceViewReference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.faceViewReference.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceViewReference.Face = Nothing
		Me.faceViewReference.FaceIds = Nothing
		Me.faceViewReference.Location = New System.Drawing.Point(3, 23)
		Me.faceViewReference.Name = "faceViewReference"
		Me.faceViewReference.Size = New System.Drawing.Size(279, 259)
		Me.faceViewReference.TabIndex = 1
		'
		'label2
		'
		Me.label2.BackColor = System.Drawing.SystemColors.ActiveCaption
		Me.label2.Dock = System.Windows.Forms.DockStyle.Top
		Me.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.label2.Location = New System.Drawing.Point(288, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(279, 20)
		Me.label2.TabIndex = 1
		Me.label2.Text = "Multiple Face Image"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 28)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Biometrics.FaceMatching"
		Me.licensePanel.Size = New System.Drawing.Size(570, 45)
		Me.licensePanel.TabIndex = 16
		'
		'MatchMultipleFaces
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.toolStrip1)
		Me.Name = "MatchMultipleFaces"
		Me.Size = New System.Drawing.Size(570, 399)
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents tsbOpenReference As System.Windows.Forms.ToolStripButton
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private WithEvents tsbOpenMultifaceImage As System.Windows.Forms.ToolStripButton
	Private openImageFileDlg As System.Windows.Forms.OpenFileDialog
	Private licensePanel As LicensePanel
	Private WithEvents tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents label1 As System.Windows.Forms.Label
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents faceViewMultiFace As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents faceViewReference As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents label2 As System.Windows.Forms.Label
End Class

