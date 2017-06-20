Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureFacePage
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

#Region "Windows Form Designer generated code"

	''' <summary>
	''' Required method for Designer support - do not modify
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.btnForceStart = New System.Windows.Forms.Button
		Me.btnCapture = New System.Windows.Forms.Button
		Me.rbFromVideo = New System.Windows.Forms.RadioButton
		Me.chbWithGeneralization = New System.Windows.Forms.CheckBox
		Me.chbManual = New System.Windows.Forms.CheckBox
		Me.chbStream = New System.Windows.Forms.CheckBox
		Me.rbFromFile = New System.Windows.Forms.RadioButton
		Me.rbFromCamera = New System.Windows.Forms.RadioButton
		Me.faceView = New Neurotec.Biometrics.Gui.NFaceView
		Me.lblStatus = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.btnForceEnd = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnFinish = New System.Windows.Forms.Button
		Me.generalizationView = New Neurotec.Samples.GeneralizeProgressView
		Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.btnRepeat = New System.Windows.Forms.Button
		Me.gbCaptureOptions = New System.Windows.Forms.GroupBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.chbCheckIcaoCompliance = New System.Windows.Forms.CheckBox
		Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.busyIndicator = New Neurotec.Samples.BusyIndicator
		Me.tableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
		Me.chbMirrorHorizontally = New System.Windows.Forms.CheckBox
		Me.horizontalZoomSlider = New Neurotec.Gui.NViewZoomSlider
		Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel
		Me.subjectTreeControl = New Neurotec.Samples.SubjectTreeControl
		Me.icaoWarningView = New Neurotec.Samples.IcaoWarningView
		Me.tableLayoutPanel3.SuspendLayout()
		Me.gbCaptureOptions.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.TableLayoutPanel2.SuspendLayout()
		Me.tableLayoutPanel4.SuspendLayout()
		Me.TableLayoutPanel5.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnForceStart
		'
		Me.btnForceStart.Location = New System.Drawing.Point(399, 599)
		Me.btnForceStart.Name = "btnForceStart"
		Me.btnForceStart.Size = New System.Drawing.Size(75, 23)
		Me.btnForceStart.TabIndex = 3
		Me.btnForceStart.Text = "&Start"
		Me.btnForceStart.UseVisualStyleBackColor = True
		'
		'btnCapture
		'
		Me.btnCapture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnCapture.Location = New System.Drawing.Point(104, 49)
		Me.btnCapture.Name = "btnCapture"
		Me.btnCapture.Size = New System.Drawing.Size(75, 23)
		Me.btnCapture.TabIndex = 9
		Me.btnCapture.Text = "Capture"
		Me.btnCapture.UseVisualStyleBackColor = True
		'
		'rbFromVideo
		'
		Me.rbFromVideo.AutoSize = True
		Me.rbFromVideo.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rbFromVideo.Location = New System.Drawing.Point(3, 49)
		Me.rbFromVideo.Name = "rbFromVideo"
		Me.rbFromVideo.Size = New System.Drawing.Size(95, 23)
		Me.rbFromVideo.TabIndex = 8
		Me.rbFromVideo.Text = "From video file"
		Me.rbFromVideo.UseVisualStyleBackColor = True
		'
		'chbWithGeneralization
		'
		Me.chbWithGeneralization.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.chbWithGeneralization, 2)
		Me.chbWithGeneralization.Location = New System.Drawing.Point(104, 26)
		Me.chbWithGeneralization.Name = "chbWithGeneralization"
		Me.chbWithGeneralization.Size = New System.Drawing.Size(116, 17)
		Me.chbWithGeneralization.TabIndex = 12
		Me.chbWithGeneralization.Text = "With generalization"
		Me.chbWithGeneralization.UseVisualStyleBackColor = True
		'
		'chbManual
		'
		Me.chbManual.AutoSize = True
		Me.chbManual.Location = New System.Drawing.Point(318, 3)
		Me.chbManual.Name = "chbManual"
		Me.chbManual.Size = New System.Drawing.Size(61, 17)
		Me.chbManual.TabIndex = 1
		Me.chbManual.Text = "Manual"
		Me.chbManual.UseVisualStyleBackColor = True
		'
		'chbStream
		'
		Me.chbStream.AutoSize = True
		Me.chbStream.Checked = True
		Me.chbStream.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbStream.Location = New System.Drawing.Point(253, 3)
		Me.chbStream.Name = "chbStream"
		Me.chbStream.Size = New System.Drawing.Size(59, 17)
		Me.chbStream.TabIndex = 0
		Me.chbStream.Text = "Stream"
		Me.chbStream.UseVisualStyleBackColor = True
		'
		'rbFromFile
		'
		Me.rbFromFile.AutoSize = True
		Me.rbFromFile.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rbFromFile.Location = New System.Drawing.Point(3, 26)
		Me.rbFromFile.Name = "rbFromFile"
		Me.rbFromFile.Size = New System.Drawing.Size(95, 17)
		Me.rbFromFile.TabIndex = 7
		Me.rbFromFile.Text = "From image file"
		Me.rbFromFile.UseVisualStyleBackColor = True
		'
		'rbFromCamera
		'
		Me.rbFromCamera.AutoSize = True
		Me.rbFromCamera.Checked = True
		Me.rbFromCamera.Dock = System.Windows.Forms.DockStyle.Fill
		Me.rbFromCamera.Location = New System.Drawing.Point(3, 3)
		Me.rbFromCamera.Name = "rbFromCamera"
		Me.rbFromCamera.Size = New System.Drawing.Size(95, 17)
		Me.rbFromCamera.TabIndex = 6
		Me.rbFromCamera.TabStop = True
		Me.rbFromCamera.Text = "From camera"
		Me.rbFromCamera.UseVisualStyleBackColor = True
		'
		'faceView
		'
		Me.faceView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.faceView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.faceView.Face = Nothing
		Me.faceView.FaceIds = Nothing
		Me.faceView.IcaoArrowsColor = System.Drawing.Color.Red
		Me.faceView.Location = New System.Drawing.Point(209, 3)
		Me.faceView.Name = "faceView"
		Me.TableLayoutPanel5.SetRowSpan(Me.faceView, 2)
		Me.faceView.ShowIcaoArrows = True
		Me.faceView.ShowTokenImageRectangle = True
		Me.faceView.Size = New System.Drawing.Size(744, 417)
		Me.faceView.TabIndex = 6
		Me.faceView.TokenImageRectangleColor = System.Drawing.Color.White
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.BackColor = System.Drawing.Color.Red
		Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.ForeColor = System.Drawing.Color.White
		Me.lblStatus.Location = New System.Drawing.Point(23, 0)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(930, 20)
		Me.lblStatus.TabIndex = 7
		Me.lblStatus.Text = "Extraction status: None"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'btnForceEnd
		'
		Me.btnForceEnd.Location = New System.Drawing.Point(480, 599)
		Me.btnForceEnd.Name = "btnForceEnd"
		Me.btnForceEnd.Size = New System.Drawing.Size(75, 23)
		Me.btnForceEnd.TabIndex = 4
		Me.btnForceEnd.Text = "&End"
		Me.btnForceEnd.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnCancel.Location = New System.Drawing.Point(321, 599)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(72, 23)
		Me.btnCancel.TabIndex = 8
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnFinish
		'
		Me.btnFinish.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnFinish.Location = New System.Drawing.Point(884, 628)
		Me.btnFinish.Name = "btnFinish"
		Me.btnFinish.Size = New System.Drawing.Size(75, 23)
		Me.btnFinish.TabIndex = 10
		Me.btnFinish.Text = "&Finish"
		Me.btnFinish.UseVisualStyleBackColor = True
		'
		'generalizationView
		'
		Me.generalizationView.AutoSize = True
		Me.generalizationView.Biometrics = Nothing
		Me.tableLayoutPanel3.SetColumnSpan(Me.generalizationView, 6)
		Me.generalizationView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.generalizationView.EnableMouseSelection = True
		Me.generalizationView.Generalized = Nothing
		Me.generalizationView.IcaoView = Nothing
		Me.generalizationView.Location = New System.Drawing.Point(3, 532)
		Me.generalizationView.Name = "generalizationView"
		Me.generalizationView.Selected = Nothing
		Me.generalizationView.Size = New System.Drawing.Size(956, 35)
		Me.generalizationView.StatusText = "Status"
		Me.generalizationView.TabIndex = 13
		Me.generalizationView.View = Me.faceView
		'
		'tableLayoutPanel3
		'
		Me.tableLayoutPanel3.ColumnCount = 6
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel3.Controls.Add(Me.btnForceEnd, 3, 4)
		Me.tableLayoutPanel3.Controls.Add(Me.btnForceStart, 2, 4)
		Me.tableLayoutPanel3.Controls.Add(Me.btnCancel, 1, 4)
		Me.tableLayoutPanel3.Controls.Add(Me.generalizationView, 0, 2)
		Me.tableLayoutPanel3.Controls.Add(Me.btnRepeat, 4, 4)
		Me.tableLayoutPanel3.Controls.Add(Me.gbCaptureOptions, 0, 0)
		Me.tableLayoutPanel3.Controls.Add(Me.TableLayoutPanel2, 0, 3)
		Me.tableLayoutPanel3.Controls.Add(Me.tableLayoutPanel4, 0, 5)
		Me.tableLayoutPanel3.Controls.Add(Me.btnFinish, 5, 5)
		Me.tableLayoutPanel3.Controls.Add(Me.TableLayoutPanel5, 0, 1)
		Me.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
		Me.tableLayoutPanel3.RowCount = 6
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel3.Size = New System.Drawing.Size(962, 663)
		Me.tableLayoutPanel3.TabIndex = 10
		'
		'btnRepeat
		'
		Me.btnRepeat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnRepeat.Image = Global.Neurotec.Samples.My.Resources.Resources.Repeat
		Me.btnRepeat.Location = New System.Drawing.Point(561, 599)
		Me.btnRepeat.Name = "btnRepeat"
		Me.btnRepeat.Size = New System.Drawing.Size(79, 23)
		Me.btnRepeat.TabIndex = 18
		Me.btnRepeat.Text = "&Repeat"
		Me.btnRepeat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnRepeat.UseVisualStyleBackColor = True
		'
		'gbCaptureOptions
		'
		Me.gbCaptureOptions.AutoSize = True
		Me.tableLayoutPanel3.SetColumnSpan(Me.gbCaptureOptions, 6)
		Me.gbCaptureOptions.Controls.Add(Me.tableLayoutPanel1)
		Me.gbCaptureOptions.Location = New System.Drawing.Point(3, 3)
		Me.gbCaptureOptions.Name = "gbCaptureOptions"
		Me.gbCaptureOptions.Size = New System.Drawing.Size(388, 94)
		Me.gbCaptureOptions.TabIndex = 19
		Me.gbCaptureOptions.TabStop = False
		Me.gbCaptureOptions.Text = "Capture options"
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.AutoSize = True
		Me.tableLayoutPanel1.ColumnCount = 4
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.Controls.Add(Me.rbFromCamera, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.rbFromVideo, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.chbManual, 3, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.rbFromFile, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.chbStream, 2, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.chbCheckIcaoCompliance, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.chbWithGeneralization, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnCapture, 1, 2)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 16)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 3
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(382, 75)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'chbCheckIcaoCompliance
		'
		Me.chbCheckIcaoCompliance.AutoSize = True
		Me.chbCheckIcaoCompliance.Checked = True
		Me.chbCheckIcaoCompliance.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbCheckIcaoCompliance.Location = New System.Drawing.Point(104, 3)
		Me.chbCheckIcaoCompliance.Name = "chbCheckIcaoCompliance"
		Me.chbCheckIcaoCompliance.Size = New System.Drawing.Size(143, 17)
		Me.chbCheckIcaoCompliance.TabIndex = 13
		Me.chbCheckIcaoCompliance.Text = "Check ICAO Compliance"
		Me.chbCheckIcaoCompliance.UseVisualStyleBackColor = True
		'
		'TableLayoutPanel2
		'
		Me.TableLayoutPanel2.ColumnCount = 2
		Me.tableLayoutPanel3.SetColumnSpan(Me.TableLayoutPanel2, 6)
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.Controls.Add(Me.busyIndicator, 0, 0)
		Me.TableLayoutPanel2.Controls.Add(Me.lblStatus, 1, 0)
		Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 573)
		Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
		Me.TableLayoutPanel2.RowCount = 1
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.Size = New System.Drawing.Size(956, 20)
		Me.TableLayoutPanel2.TabIndex = 20
		'
		'busyIndicator
		'
		Me.busyIndicator.Dock = System.Windows.Forms.DockStyle.Fill
		Me.busyIndicator.Location = New System.Drawing.Point(3, 3)
		Me.busyIndicator.Name = "busyIndicator"
		Me.busyIndicator.Size = New System.Drawing.Size(14, 14)
		Me.busyIndicator.TabIndex = 0
		Me.busyIndicator.Visible = False
		'
		'tableLayoutPanel4
		'
		Me.tableLayoutPanel4.AutoSize = True
		Me.tableLayoutPanel4.ColumnCount = 2
		Me.tableLayoutPanel3.SetColumnSpan(Me.tableLayoutPanel4, 5)
		Me.tableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel4.Controls.Add(Me.chbMirrorHorizontally, 0, 0)
		Me.tableLayoutPanel4.Controls.Add(Me.horizontalZoomSlider, 1, 0)
		Me.tableLayoutPanel4.Location = New System.Drawing.Point(3, 628)
		Me.tableLayoutPanel4.Name = "tableLayoutPanel4"
		Me.tableLayoutPanel4.RowCount = 1
		Me.tableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel4.Size = New System.Drawing.Size(364, 32)
		Me.tableLayoutPanel4.TabIndex = 22
		'
		'chbMirrorHorizontally
		'
		Me.chbMirrorHorizontally.AutoSize = True
		Me.chbMirrorHorizontally.Checked = True
		Me.chbMirrorHorizontally.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbMirrorHorizontally.Dock = System.Windows.Forms.DockStyle.Fill
		Me.chbMirrorHorizontally.Location = New System.Drawing.Point(3, 2)
		Me.chbMirrorHorizontally.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
		Me.chbMirrorHorizontally.Name = "chbMirrorHorizontally"
		Me.chbMirrorHorizontally.Size = New System.Drawing.Size(77, 27)
		Me.chbMirrorHorizontally.TabIndex = 21
		Me.chbMirrorHorizontally.Text = "Mirror view"
		Me.chbMirrorHorizontally.UseVisualStyleBackColor = True
		'
		'horizontalZoomSlider
		'
		Me.horizontalZoomSlider.Location = New System.Drawing.Point(86, 3)
		Me.horizontalZoomSlider.Name = "horizontalZoomSlider"
		Me.horizontalZoomSlider.Size = New System.Drawing.Size(275, 26)
		Me.horizontalZoomSlider.TabIndex = 11
		Me.horizontalZoomSlider.View = Me.faceView
		'
		'TableLayoutPanel5
		'
		Me.TableLayoutPanel5.ColumnCount = 2
		Me.tableLayoutPanel3.SetColumnSpan(Me.TableLayoutPanel5, 6)
		Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel5.Controls.Add(Me.subjectTreeControl, 0, 1)
		Me.TableLayoutPanel5.Controls.Add(Me.icaoWarningView, 0, 0)
		Me.TableLayoutPanel5.Controls.Add(Me.faceView, 1, 0)
		Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel5.Location = New System.Drawing.Point(3, 103)
		Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
		Me.TableLayoutPanel5.RowCount = 2
		Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.TableLayoutPanel5.Size = New System.Drawing.Size(956, 423)
		Me.TableLayoutPanel5.TabIndex = 23
		'
		'subjectTreeControl
		'
		Me.subjectTreeControl.AllowNew = Neurotec.Biometrics.NBiometricType.None
		Me.subjectTreeControl.AllowRemove = False
		Me.subjectTreeControl.AutoSize = True
		Me.subjectTreeControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.subjectTreeControl.Location = New System.Drawing.Point(3, 370)
		Me.subjectTreeControl.MinimumSize = New System.Drawing.Size(200, 50)
		Me.subjectTreeControl.Name = "subjectTreeControl"
		Me.subjectTreeControl.SelectedItem = Nothing
		Me.subjectTreeControl.ShowBiometricsOnly = True
		Me.subjectTreeControl.ShownTypes = CType(((((Neurotec.Biometrics.NBiometricType.Face Or Neurotec.Biometrics.NBiometricType.Voice) _
					Or Neurotec.Biometrics.NBiometricType.Fingerprint) _
					Or Neurotec.Biometrics.NBiometricType.Iris) _
					Or Neurotec.Biometrics.NBiometricType.PalmPrint), Neurotec.Biometrics.NBiometricType)
		Me.subjectTreeControl.Size = New System.Drawing.Size(200, 50)
		Me.subjectTreeControl.Subject = Nothing
		Me.subjectTreeControl.TabIndex = 27
		'
		'icaoWarningView
		'
		Me.icaoWarningView.AutoScroll = True
		Me.icaoWarningView.AutoSize = True
		Me.icaoWarningView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.icaoWarningView.Face = Nothing
		Me.icaoWarningView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.icaoWarningView.IndeterminateColor = System.Drawing.Color.Orange
		Me.icaoWarningView.Location = New System.Drawing.Point(3, 3)
		Me.icaoWarningView.MinimumSize = New System.Drawing.Size(200, 0)
		Me.icaoWarningView.Name = "icaoWarningView"
		Me.icaoWarningView.NoWarningColor = System.Drawing.Color.Green
		Me.icaoWarningView.Size = New System.Drawing.Size(200, 361)
		Me.icaoWarningView.TabIndex = 26
		Me.icaoWarningView.Visible = False
		Me.icaoWarningView.WarningColor = System.Drawing.Color.Red
		'
		'CaptureFacePage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel3)
		Me.Name = "CaptureFacePage"
		Me.Size = New System.Drawing.Size(962, 663)
		Me.tableLayoutPanel3.ResumeLayout(False)
		Me.tableLayoutPanel3.PerformLayout()
		Me.gbCaptureOptions.ResumeLayout(False)
		Me.gbCaptureOptions.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.TableLayoutPanel2.ResumeLayout(False)
		Me.TableLayoutPanel2.PerformLayout()
		Me.tableLayoutPanel4.ResumeLayout(False)
		Me.tableLayoutPanel4.PerformLayout()
		Me.TableLayoutPanel5.ResumeLayout(False)
		Me.TableLayoutPanel5.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents btnForceStart As System.Windows.Forms.Button
	Private chbManual As System.Windows.Forms.CheckBox
	Private chbStream As System.Windows.Forms.CheckBox
	Private faceView As Neurotec.Biometrics.Gui.NFaceView
	Private lblStatus As System.Windows.Forms.Label
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents btnForceEnd As System.Windows.Forms.Button
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private WithEvents rbFromCamera As System.Windows.Forms.RadioButton
	Private WithEvents rbFromFile As System.Windows.Forms.RadioButton
	Private WithEvents rbFromVideo As System.Windows.Forms.RadioButton
	Private WithEvents btnFinish As System.Windows.Forms.Button
	Private WithEvents btnCapture As System.Windows.Forms.Button
	Private chbWithGeneralization As System.Windows.Forms.CheckBox
	Private generalizationView As GeneralizeProgressView
	Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnRepeat As System.Windows.Forms.Button
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private gbCaptureOptions As System.Windows.Forms.GroupBox
	Private busyIndicator As BusyIndicator
	Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents tableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents chbMirrorHorizontally As System.Windows.Forms.CheckBox
	Private WithEvents horizontalZoomSlider As Neurotec.Gui.NViewZoomSlider
	Friend WithEvents chbCheckIcaoCompliance As System.Windows.Forms.CheckBox
	Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents subjectTreeControl As Neurotec.Samples.SubjectTreeControl
	Private WithEvents icaoWarningView As Neurotec.Samples.IcaoWarningView
End Class
