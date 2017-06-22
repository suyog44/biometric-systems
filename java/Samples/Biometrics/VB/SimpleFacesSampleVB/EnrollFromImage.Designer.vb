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
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.tsbOpenImage = New System.Windows.Forms.ToolStripButton
		Me.tlsLabel = New System.Windows.Forms.ToolStripLabel
		Me.cbRollAngle = New System.Windows.Forms.ToolStripComboBox
		Me.toolStripLabel1 = New System.Windows.Forms.ToolStripLabel
		Me.cbYawAngle = New System.Windows.Forms.ToolStripComboBox
		Me.tsbExtract = New System.Windows.Forms.ToolStripButton
		Me.saveFileDialog = New System.Windows.Forms.SaveFileDialog
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.facesView = New Neurotec.Biometrics.Gui.NFaceView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.lblStatus = New System.Windows.Forms.Label
		Me.toolStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.btnSaveTemplate.Location = New System.Drawing.Point(0, 242)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveTemplate.TabIndex = 5
		Me.btnSaveTemplate.Text = "&Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'toolStrip1
		'
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenImage, Me.tlsLabel, Me.cbRollAngle, Me.toolStripLabel1, Me.cbYawAngle, Me.tsbExtract})
		Me.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(743, 23)
		Me.toolStrip1.TabIndex = 7
		Me.toolStrip1.Text = "toolStrip1"
		'
		'tsbOpenImage
		'
		Me.tsbOpenImage.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenImage.Name = "tsbOpenImage"
		Me.tsbOpenImage.Size = New System.Drawing.Size(101, 20)
		Me.tsbOpenImage.Text = "&Open image..."
		'
		'tlsLabel
		'
		Me.tlsLabel.Name = "tlsLabel"
		Me.tlsLabel.Size = New System.Drawing.Size(136, 15)
		Me.tlsLabel.Text = "Max roll angle deviation:"
		'
		'cbRollAngle
		'
		Me.cbRollAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbRollAngle.Name = "cbRollAngle"
		Me.cbRollAngle.Size = New System.Drawing.Size(121, 23)
		'
		'toolStripLabel1
		'
		Me.toolStripLabel1.Name = "toolStripLabel1"
		Me.toolStripLabel1.Size = New System.Drawing.Size(140, 15)
		Me.toolStripLabel1.Text = "Max yaw angle deviation:"
		'
		'cbYawAngle
		'
		Me.cbYawAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbYawAngle.Name = "cbYawAngle"
		Me.cbYawAngle.Size = New System.Drawing.Size(121, 23)
		'
		'tsbExtract
		'
		Me.tsbExtract.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsbExtract.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
		Me.tsbExtract.Image = CType(resources.GetObject("tsbExtract.Image"), System.Drawing.Image)
		Me.tsbExtract.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbExtract.Name = "tsbExtract"
		Me.tsbExtract.Size = New System.Drawing.Size(105, 19)
		Me.tsbExtract.Text = "&Extract template"
		'
		'openFileDialog
		'
		Me.openFileDialog.FileName = "openFileDialog"
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(3, 26)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = "Biometrics.FaceSegmentsDetection"
		Me.licensePanel.RequiredComponents = "Biometrics.FaceExtraction"
		Me.licensePanel.Size = New System.Drawing.Size(737, 45)
		Me.licensePanel.TabIndex = 9
		'
		'facesView
		'
		Me.facesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.facesView.Face = Nothing
		Me.facesView.FaceIds = Nothing
		Me.facesView.Location = New System.Drawing.Point(0, 77)
		Me.facesView.Name = "facesView"
		Me.facesView.Size = New System.Drawing.Size(740, 159)
		Me.facesView.TabIndex = 10
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(467, 242)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(273, 23)
		Me.nViewZoomSlider1.TabIndex = 11
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.facesView
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Location = New System.Drawing.Point(111, 247)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(37, 13)
		Me.lblStatus.TabIndex = 12
		Me.lblStatus.Text = "Status"
		'
		'EnrollFromImage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.facesView)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.toolStrip1)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Name = "EnrollFromImage"
		Me.Size = New System.Drawing.Size(743, 268)
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private WithEvents tsbOpenImage As System.Windows.Forms.ToolStripButton
	Private tlsLabel As System.Windows.Forms.ToolStripLabel
	Private WithEvents cbRollAngle As System.Windows.Forms.ToolStripComboBox
	Private toolStripLabel1 As System.Windows.Forms.ToolStripLabel
	Private WithEvents cbYawAngle As System.Windows.Forms.ToolStripComboBox
	Private WithEvents tsbExtract As System.Windows.Forms.ToolStripButton
	Private saveFileDialog As System.Windows.Forms.SaveFileDialog
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private licensePanel As LicensePanel
	Private WithEvents facesView As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
	Private WithEvents lblStatus As System.Windows.Forms.Label
End Class

