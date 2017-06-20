Imports Microsoft.VisualBasic
Imports System
Partial Public Class DetectFaces
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DetectFaces))
		Me.tsbDetectFacialFeatures = New System.Windows.Forms.ToolStripButton
		Me.cbRollAngle = New System.Windows.Forms.ToolStripComboBox
		Me.tlsLabel = New System.Windows.Forms.ToolStripLabel
		Me.tsbOpenImage = New System.Windows.Forms.ToolStripButton
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.toolStripLabel1 = New System.Windows.Forms.ToolStripLabel
		Me.cbYawAngle = New System.Windows.Forms.ToolStripComboBox
		Me.openFaceImageDlg = New System.Windows.Forms.OpenFileDialog
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.facesView = New Neurotec.Biometrics.Gui.NFaceView
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.toolStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'tsbDetectFacialFeatures
		'
		Me.tsbDetectFacialFeatures.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.tsbDetectFacialFeatures.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
		Me.tsbDetectFacialFeatures.Image = CType(resources.GetObject("tsbDetectFacialFeatures.Image"), System.Drawing.Image)
		Me.tsbDetectFacialFeatures.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbDetectFacialFeatures.Name = "tsbDetectFacialFeatures"
		Me.tsbDetectFacialFeatures.Size = New System.Drawing.Size(134, 22)
		Me.tsbDetectFacialFeatures.Text = "&Detect Facial Features"
		'
		'cbRollAngle
		'
		Me.cbRollAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbRollAngle.Name = "cbRollAngle"
		Me.cbRollAngle.Size = New System.Drawing.Size(121, 25)
		'
		'tlsLabel
		'
		Me.tlsLabel.Name = "tlsLabel"
		Me.tlsLabel.Size = New System.Drawing.Size(136, 22)
		Me.tlsLabel.Text = "Max roll angle deviation:"
		'
		'tsbOpenImage
		'
		Me.tsbOpenImage.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.tsbOpenImage.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenImage.Name = "tsbOpenImage"
		Me.tsbOpenImage.Size = New System.Drawing.Size(101, 22)
		Me.tsbOpenImage.Text = "&Open image..."
		'
		'toolStrip1
		'
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenImage, Me.tlsLabel, Me.cbRollAngle, Me.toolStripLabel1, Me.cbYawAngle, Me.tsbDetectFacialFeatures})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(792, 25)
		Me.toolStrip1.TabIndex = 3
		Me.toolStrip1.Text = "toolStrip1"
		'
		'toolStripLabel1
		'
		Me.toolStripLabel1.Name = "toolStripLabel1"
		Me.toolStripLabel1.Size = New System.Drawing.Size(140, 22)
		Me.toolStripLabel1.Text = "Max yaw angle deviation:"
		'
		'cbYawAngle
		'
		Me.cbYawAngle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbYawAngle.Name = "cbYawAngle"
		Me.cbYawAngle.Size = New System.Drawing.Size(121, 25)
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 25)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceDetection"
		Me.licensePanel.Size = New System.Drawing.Size(792, 45)
		Me.licensePanel.TabIndex = 4
		'
		'facesView
		'
		Me.facesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.facesView.BaseFeaturePointSize = 4
		Me.facesView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.facesView.Face = Nothing
		Me.facesView.FaceIds = Nothing
		Me.facesView.Location = New System.Drawing.Point(3, 76)
		Me.facesView.Name = "facesView"
		Me.facesView.Size = New System.Drawing.Size(792, 261)
		Me.facesView.TabIndex = 5
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(3, 343)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(273, 23)
		Me.nViewZoomSlider1.TabIndex = 6
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.facesView
		'
		'DetectFaces
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.facesView)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.toolStrip1)
		Me.Name = "DetectFaces"
		Me.Size = New System.Drawing.Size(792, 372)
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private WithEvents tsbDetectFacialFeatures As System.Windows.Forms.ToolStripButton
	Private WithEvents cbRollAngle As System.Windows.Forms.ToolStripComboBox
	Private tlsLabel As System.Windows.Forms.ToolStripLabel
	Private WithEvents tsbOpenImage As System.Windows.Forms.ToolStripButton
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private openFaceImageDlg As System.Windows.Forms.OpenFileDialog
	Private toolStripLabel1 As System.Windows.Forms.ToolStripLabel
	Private WithEvents cbYawAngle As System.Windows.Forms.ToolStripComboBox
	Private licensePanel As LicensePanel
	Private WithEvents facesView As Neurotec.Biometrics.Gui.NFaceView
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class
