Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureIcaoCompliantImage
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
		Me.groupBox = New System.Windows.Forms.GroupBox
		Me.btnStop = New System.Windows.Forms.Button
		Me.cbCameras = New System.Windows.Forms.ComboBox
		Me.btnStart = New System.Windows.Forms.Button
		Me.btnRefreshList = New System.Windows.Forms.Button
		Me.nViewZoomSlider2 = New Neurotec.Gui.NViewZoomSlider
		Me.faceView = New Neurotec.Biometrics.Gui.NFaceView
		Me.lblStatus = New System.Windows.Forms.Label
		Me.btnForce = New System.Windows.Forms.Button
		Me.btnSaveImage = New System.Windows.Forms.Button
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.icaoWarningView = New Neurotec.Samples.IcaoWarningView
		Me.saveImageDialog = New System.Windows.Forms.SaveFileDialog
		Me.saveTemplateDialog = New System.Windows.Forms.SaveFileDialog
		Me.btnSaveTemplate = New System.Windows.Forms.Button
		Me.licensePanel = New Neurotec.Samples.LicensePanel
		Me.groupBox.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'groupBox
		'
		Me.groupBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.groupBox.Controls.Add(Me.btnStop)
		Me.groupBox.Controls.Add(Me.cbCameras)
		Me.groupBox.Controls.Add(Me.btnStart)
		Me.groupBox.Controls.Add(Me.btnRefreshList)
		Me.groupBox.Location = New System.Drawing.Point(3, 54)
		Me.groupBox.Name = "groupBox"
		Me.groupBox.Size = New System.Drawing.Size(783, 72)
		Me.groupBox.TabIndex = 27
		Me.groupBox.TabStop = False
		Me.groupBox.Text = "Cameras"
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
		'cbCameras
		'
		Me.cbCameras.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbCameras.FormattingEnabled = True
		Me.cbCameras.Location = New System.Drawing.Point(6, 13)
		Me.cbCameras.Name = "cbCameras"
		Me.cbCameras.Size = New System.Drawing.Size(766, 21)
		Me.cbCameras.TabIndex = 15
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
		'btnRefreshList
		'
		Me.btnRefreshList.Location = New System.Drawing.Point(6, 40)
		Me.btnRefreshList.Name = "btnRefreshList"
		Me.btnRefreshList.Size = New System.Drawing.Size(75, 21)
		Me.btnRefreshList.TabIndex = 17
		Me.btnRefreshList.Text = "Refresh list"
		Me.btnRefreshList.UseVisualStyleBackColor = True
		'
		'nViewZoomSlider2
		'
		Me.nViewZoomSlider2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider2.Location = New System.Drawing.Point(315, 484)
		Me.nViewZoomSlider2.Name = "nViewZoomSlider2"
		Me.nViewZoomSlider2.Size = New System.Drawing.Size(249, 23)
		Me.nViewZoomSlider2.TabIndex = 29
		Me.nViewZoomSlider2.Text = "nViewZoomSlider2"
		Me.nViewZoomSlider2.View = Nothing
		'
		'faceView
		'
		Me.faceView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceView.Face = Nothing
		Me.faceView.FaceIds = Nothing
		Me.faceView.IcaoArrowsColor = System.Drawing.Color.Red
		Me.faceView.Location = New System.Drawing.Point(209, 3)
		Me.faceView.Name = "faceView"
		Me.faceView.ShowAge = False
		Me.faceView.ShowEmotions = False
		Me.faceView.ShowExpression = False
		Me.faceView.ShowGender = False
		Me.faceView.ShowIcaoArrows = True
		Me.faceView.ShowProperties = False
		Me.faceView.ShowTokenImageRectangle = True
		Me.faceView.Size = New System.Drawing.Size(571, 340)
		Me.faceView.TabIndex = 29
		Me.faceView.TokenImageRectangleColor = System.Drawing.Color.White
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.Location = New System.Drawing.Point(100, 484)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(209, 23)
		Me.lblStatus.TabIndex = 33
		Me.lblStatus.Text = "Status"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'btnForce
		'
		Me.btnForce.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnForce.Enabled = False
		Me.btnForce.Location = New System.Drawing.Point(3, 484)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(91, 23)
		Me.btnForce.TabIndex = 32
		Me.btnForce.Text = "Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'btnSaveImage
		'
		Me.btnSaveImage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveImage.Enabled = False
		Me.btnSaveImage.Location = New System.Drawing.Point(681, 484)
		Me.btnSaveImage.Name = "btnSaveImage"
		Me.btnSaveImage.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveImage.TabIndex = 31
		Me.btnSaveImage.Text = "Save &Image"
		Me.btnSaveImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveImage.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.faceView, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.icaoWarningView, 0, 0)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 132)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 1
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(783, 346)
		Me.tableLayoutPanel1.TabIndex = 34
		'
		'icaoWarningView
		'
		Me.icaoWarningView.AutoSize = True
		Me.icaoWarningView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.icaoWarningView.Face = Nothing
		Me.icaoWarningView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.icaoWarningView.IndeterminateColor = System.Drawing.Color.Orange
		Me.icaoWarningView.Location = New System.Drawing.Point(3, 3)
		Me.icaoWarningView.MinimumSize = New System.Drawing.Size(200, 0)
		Me.icaoWarningView.Name = "icaoWarningView"
		Me.icaoWarningView.NoWarningColor = System.Drawing.Color.Green
		Me.icaoWarningView.Size = New System.Drawing.Size(200, 340)
		Me.icaoWarningView.TabIndex = 30
		Me.icaoWarningView.WarningColor = System.Drawing.Color.Red
		'
		'btnSaveTemplate
		'
		Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnSaveTemplate.Enabled = False
		Me.btnSaveTemplate.Image = Global.Neurotec.Samples.My.Resources.Resources.saveHS
		Me.btnSaveTemplate.Location = New System.Drawing.Point(570, 484)
		Me.btnSaveTemplate.Name = "btnSaveTemplate"
		Me.btnSaveTemplate.Size = New System.Drawing.Size(105, 23)
		Me.btnSaveTemplate.TabIndex = 30
		Me.btnSaveTemplate.Text = "&Save Template"
		Me.btnSaveTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveTemplate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSaveTemplate.UseVisualStyleBackColor = True
		'
		'licensePanel
		'
		Me.licensePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.licensePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.licensePanel.Location = New System.Drawing.Point(3, 3)
		Me.licensePanel.Name = "licensePanel"
		Me.licensePanel.OptionalComponents = ""
		Me.licensePanel.RequiredComponents = "Biometrics.FaceExtraction,Biometrics.FaceSegmentsDetection,Devices.Cameras"
		Me.licensePanel.Size = New System.Drawing.Size(783, 45)
		Me.licensePanel.TabIndex = 26
		'
		'CaptureIcaoCompliantImage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.nViewZoomSlider2)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.btnForce)
		Me.Controls.Add(Me.btnSaveImage)
		Me.Controls.Add(Me.btnSaveTemplate)
		Me.Controls.Add(Me.groupBox)
		Me.Controls.Add(Me.licensePanel)
		Me.Name = "CaptureIcaoCompliantImage"
		Me.Size = New System.Drawing.Size(789, 510)
		Me.groupBox.ResumeLayout(False)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private licensePanel As LicensePanel
	Private groupBox As System.Windows.Forms.GroupBox
	Private WithEvents btnStop As System.Windows.Forms.Button
	Private WithEvents cbCameras As System.Windows.Forms.ComboBox
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private WithEvents btnRefreshList As System.Windows.Forms.Button
	Private nViewZoomSlider2 As Neurotec.Gui.NViewZoomSlider
	Private lblStatus As System.Windows.Forms.Label
	Private WithEvents btnForce As System.Windows.Forms.Button
	Private WithEvents btnSaveImage As System.Windows.Forms.Button
	Private WithEvents btnSaveTemplate As System.Windows.Forms.Button
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private faceView As Neurotec.Biometrics.Gui.NFaceView
	Private saveImageDialog As System.Windows.Forms.SaveFileDialog
	Private saveTemplateDialog As System.Windows.Forms.SaveFileDialog
	Private icaoWarningView As IcaoWarningView
End Class
