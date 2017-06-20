Imports Microsoft.VisualBasic
Imports System

Partial Public Class EnrollFromCamera
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
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.btnSaveImage = New System.Windows.Forms.Button
		Me.cbCameras = New System.Windows.Forms.ComboBox
		Me.btnRefreshList = New System.Windows.Forms.Button
		Me.btnStart = New System.Windows.Forms.Button
		Me.btnStop = New System.Windows.Forms.Button
		Me.groupBox = New System.Windows.Forms.GroupBox
		Me.chbCaptureAutomatically = New System.Windows.Forms.CheckBox
		Me.backgroundWorker = New System.ComponentModel.BackgroundWorker
		Me.btnStartExtraction = New System.Windows.Forms.Button
		Me.lblStatus = New System.Windows.Forms.Label
		Me.saveTemplateDialog = New System.Windows.Forms.SaveFileDialog
		Me.saveImageDialog = New System.Windows.Forms.SaveFileDialog
		Me.facesView = New Neurotec.Biometrics.Gui.NFaceView
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.chbCheckLiveness = New System.Windows.Forms.CheckBox
		Me.groupBox.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.btnSaveTemplate.Location = New System.Drawing.Point(445, 337)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveTemplate.TabIndex = 9
		Me.btnSaveTemplate.Text = "&Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'btnSaveImage
		'
		Me.btnSaveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveImage.Enabled = False
		Me.btnSaveImage.Location = New System.Drawing.Point(556, 337)
		Me.btnSaveImage.Name = "btnSaveImage"
		Me.btnSaveImage.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveImage.TabIndex = 10
		Me.btnSaveImage.Text = "Save &Image"
		Me.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveImage.UseVisualStyleBackColor = True
		'
		'cbCameras
		'
		Me.cbCameras.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbCameras.FormattingEnabled = True
		Me.cbCameras.Location = New System.Drawing.Point(6, 13)
		Me.cbCameras.Name = "cbCameras"
		Me.cbCameras.Size = New System.Drawing.Size(641, 21)
		Me.cbCameras.TabIndex = 15
		'
		'btnRefreshList
		'
		Me.btnRefreshList.Location = New System.Drawing.Point(6, 40)
		Me.btnRefreshList.Name = "btnRefreshList"
		Me.btnRefreshList.Size = New System.Drawing.Size(75, 21)
		Me.btnRefreshList.TabIndex = 17
		Me.btnRefreshList.Text = "Refresh list"
		Me.btnRefreshList.UseVisualStyleBackColor = True
		'
		'btnStart
		'
		Me.btnStart.Location = New System.Drawing.Point(87, 40)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(86, 21)
		Me.btnStart.TabIndex = 18
		Me.btnStart.Text = "Start capturing"
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'btnStop
		'
		Me.btnStop.Enabled = False
		Me.btnStop.Location = New System.Drawing.Point(179, 40)
		Me.btnStop.Name = "btnStop"
		Me.btnStop.Size = New System.Drawing.Size(74, 21)
		Me.btnStop.TabIndex = 19
		Me.btnStop.Text = "Stop"
		Me.btnStop.UseVisualStyleBackColor = True
		'
		'groupBox
		'
		Me.groupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox.Controls.Add(Me.chbCheckLiveness)
		Me.groupBox.Controls.Add(Me.chbCaptureAutomatically)
		Me.groupBox.Controls.Add(Me.btnStop)
		Me.groupBox.Controls.Add(Me.cbCameras)
		Me.groupBox.Controls.Add(Me.btnStart)
		Me.groupBox.Controls.Add(Me.btnRefreshList)
		Me.groupBox.Location = New System.Drawing.Point(3, 51)
		Me.groupBox.Name = "groupBox"
		Me.groupBox.Size = New System.Drawing.Size(658, 72)
		Me.groupBox.TabIndex = 20
		Me.groupBox.TabStop = False
		Me.groupBox.Text = "Cameras"
		'
		'chbCaptureAutomatically
		'
		Me.chbCaptureAutomatically.AutoSize = True
		Me.chbCaptureAutomatically.Location = New System.Drawing.Point(259, 43)
		Me.chbCaptureAutomatically.Name = "chbCaptureAutomatically"
		Me.chbCaptureAutomatically.Size = New System.Drawing.Size(127, 17)
		Me.chbCaptureAutomatically.TabIndex = 20
		Me.chbCaptureAutomatically.Text = "Capture automatically"
		Me.chbCaptureAutomatically.UseVisualStyleBackColor = True
		'
		'backgroundWorker
		'
		Me.backgroundWorker.WorkerSupportsCancellation = True
		'
		'btnStartExtraction
		'
		Me.btnStartExtraction.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnStartExtraction.Enabled = False
		Me.btnStartExtraction.Location = New System.Drawing.Point(3, 337)
		Me.btnStartExtraction.Name = "btnStartExtraction"
		Me.btnStartExtraction.Size = New System.Drawing.Size(91, 23)
		Me.btnStartExtraction.TabIndex = 23
		Me.btnStartExtraction.Text = "Start extraction"
		Me.btnStartExtraction.UseVisualStyleBackColor = True
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.Location = New System.Drawing.Point(100, 337)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(226, 23)
		Me.lblStatus.TabIndex = 24
		Me.lblStatus.Text = "Status"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'facesView
		'
		Me.facesView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.facesView.Face = Nothing
		Me.facesView.FaceIds = Nothing
		Me.facesView.Location = New System.Drawing.Point(3, 129)
		Me.facesView.Name = "facesView"
		Me.facesView.Size = New System.Drawing.Size(658, 202)
		Me.facesView.TabIndex = 26
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(190, 337)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider2.TabIndex = 8
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Me.facesView
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(0, 0)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Devices.Cameras"
		Me.licensePanel.Size = New System.Drawing.Size(664, 45)
		Me.licensePanel.TabIndex = 25
		'
		'chbCheckLiveness
		'
		Me.chbCheckLiveness.AutoSize = True
		Me.chbCheckLiveness.Location = New System.Drawing.Point(392, 43)
		Me.chbCheckLiveness.Name = "chbCheckLiveness"
		Me.chbCheckLiveness.Size = New System.Drawing.Size(98, 17)
		Me.chbCheckLiveness.TabIndex = 21
		Me.chbCheckLiveness.Text = "Check liveness"
		Me.chbCheckLiveness.UseVisualStyleBackColor = True
		'
		'EnrollFromCamera
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.nViewZoomSlider2)
		Me.Controls.Add(Me.facesView)
		Me.Controls.Add(Me.licensePanel)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.btnStartExtraction)
		Me.Controls.Add(Me.groupBox)
		Me.Controls.Add(Me.btnSaveImage)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Name = "EnrollFromCamera"
		Me.Size = New System.Drawing.Size(664, 363)
		Me.groupBox.ResumeLayout(False)
		Me.groupBox.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private WithEvents btnSaveImage As System.Windows.Forms.Button
	Private WithEvents cbCameras As System.Windows.Forms.ComboBox
	Private WithEvents btnRefreshList As System.Windows.Forms.Button
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private WithEvents btnStop As System.Windows.Forms.Button
	Private groupBox As System.Windows.Forms.GroupBox
	Private WithEvents backgroundWorker As System.ComponentModel.BackgroundWorker
	Private WithEvents btnStartExtraction As System.Windows.Forms.Button
	Private lblStatus As System.Windows.Forms.Label
	Private saveTemplateDialog As System.Windows.Forms.SaveFileDialog
	Private saveImageDialog As System.Windows.Forms.SaveFileDialog
	Private licensePanel As LicensePanel
	Private WithEvents facesView As Neurotec.Biometrics.Gui.NFaceView
	Friend WithEvents chbCaptureAutomatically As System.Windows.Forms.CheckBox
	Private WithEvents nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Friend WithEvents chbCheckLiveness As System.Windows.Forms.CheckBox
End Class

