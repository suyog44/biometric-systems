Imports Microsoft.VisualBasic
Imports System
Partial Public Class CapturePalmsPage
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
		Me.palmSelector = New Neurotec.Samples.PalmSelector
		Me.gbSource = New System.Windows.Forms.GroupBox
		Me.rbFile = New System.Windows.Forms.RadioButton
		Me.rbScanner = New System.Windows.Forms.RadioButton
		Me.palmView = New Neurotec.Biometrics.Gui.NFingerView
		Me.splitContainer1 = New System.Windows.Forms.SplitContainer
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.btnCapture = New System.Windows.Forms.Button
		Me.cbSelectedPosition = New System.Windows.Forms.ComboBox
		Me.gbOptions = New System.Windows.Forms.GroupBox
		Me.chbWithGeneralization = New System.Windows.Forms.CheckBox
		Me.chbCaptureAutomatically = New System.Windows.Forms.CheckBox
		Me.label2 = New System.Windows.Forms.Label
		Me.cbImpression = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.palmsTree = New Neurotec.Samples.SubjectTreeControl
		Me.btnCancel = New System.Windows.Forms.Button
		Me.btnForce = New System.Windows.Forms.Button
		Me.btnOpen = New System.Windows.Forms.Button
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.btnFinish = New System.Windows.Forms.Button
		Me.horizontalZoomSlider = New Neurotec.Gui.NViewZoomSlider
		Me.chbShowReturned = New System.Windows.Forms.CheckBox
		Me.generalizeProgressView = New Neurotec.Samples.GeneralizeProgressView
		Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.busyIndicator = New Neurotec.Samples.BusyIndicator
		Me.lblStatus = New System.Windows.Forms.Label
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.gbSource.SuspendLayout()
		Me.splitContainer1.Panel1.SuspendLayout()
		Me.splitContainer1.Panel2.SuspendLayout()
		Me.splitContainer1.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.gbOptions.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.TableLayoutPanel3.SuspendLayout()
		Me.SuspendLayout()
		'
		'palmSelector
		'
		Me.palmSelector.AllowedPositions = New Neurotec.Biometrics.NFPosition() {Neurotec.Biometrics.NFPosition.RightUpperPalm, Neurotec.Biometrics.NFPosition.RightLowerPalm, Neurotec.Biometrics.NFPosition.RightInterdigital, Neurotec.Biometrics.NFPosition.RightHypothenar, Neurotec.Biometrics.NFPosition.RightThenar, Neurotec.Biometrics.NFPosition.RightFullPalm, Neurotec.Biometrics.NFPosition.LeftUpperPalm, Neurotec.Biometrics.NFPosition.LeftLowerPalm, Neurotec.Biometrics.NFPosition.LeftInterdigital, Neurotec.Biometrics.NFPosition.LeftHypothenar, Neurotec.Biometrics.NFPosition.LeftThenar, Neurotec.Biometrics.NFPosition.LeftFullPalm}
		Me.palmSelector.AllowHighlight = True
		Me.palmSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.palmSelector, 2)
		Me.palmSelector.IsRolled = False
		Me.palmSelector.Location = New System.Drawing.Point(3, 170)
		Me.palmSelector.MissingPositions = New Neurotec.Biometrics.NFPosition(-1) {}
		Me.palmSelector.Name = "palmSelector"
		Me.palmSelector.SelectedPosition = Neurotec.Biometrics.NFPosition.Unknown
		Me.palmSelector.ShowFingerNails = False
		Me.palmSelector.ShowPalmCreases = True
		Me.palmSelector.Size = New System.Drawing.Size(217, 93)
		Me.palmSelector.TabIndex = 0
		Me.palmSelector.Text = "palmSelector"
		'
		'gbSource
		'
		Me.gbSource.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.gbSource, 2)
		Me.gbSource.Controls.Add(Me.rbFile)
		Me.gbSource.Controls.Add(Me.rbScanner)
		Me.gbSource.Location = New System.Drawing.Point(3, 3)
		Me.gbSource.Name = "gbSource"
		Me.gbSource.Size = New System.Drawing.Size(217, 72)
		Me.gbSource.TabIndex = 1
		Me.gbSource.TabStop = False
		Me.gbSource.Text = "Source"
		'
		'rbFile
		'
		Me.rbFile.AutoSize = True
		Me.rbFile.Location = New System.Drawing.Point(13, 42)
		Me.rbFile.Name = "rbFile"
		Me.rbFile.Size = New System.Drawing.Size(41, 17)
		Me.rbFile.TabIndex = 1
		Me.rbFile.Text = "File"
		Me.rbFile.UseVisualStyleBackColor = True
		'
		'rbScanner
		'
		Me.rbScanner.AutoSize = True
		Me.rbScanner.Checked = True
		Me.rbScanner.Location = New System.Drawing.Point(13, 19)
		Me.rbScanner.Name = "rbScanner"
		Me.rbScanner.Size = New System.Drawing.Size(65, 17)
		Me.rbScanner.TabIndex = 0
		Me.rbScanner.TabStop = True
		Me.rbScanner.Text = "Scanner"
		Me.rbScanner.UseVisualStyleBackColor = True
		'
		'palmView
		'
		Me.palmView.BackColor = System.Drawing.SystemColors.Control
		Me.palmView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.palmView.BoundingRectColor = System.Drawing.Color.Red
		Me.tableLayoutPanel1.SetColumnSpan(Me.palmView, 3)
		Me.palmView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.palmView.Location = New System.Drawing.Point(3, 3)
		Me.palmView.MinutiaColor = System.Drawing.Color.Red
		Me.palmView.Name = "palmView"
		Me.palmView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.palmView.ResultImageColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer))
		Me.palmView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.palmView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.palmView.SingularPointColor = System.Drawing.Color.Red
		Me.palmView.Size = New System.Drawing.Size(672, 338)
		Me.palmView.TabIndex = 2
		Me.palmView.TreeColor = System.Drawing.Color.Crimson
		Me.palmView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.palmView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.palmView.TreeWidth = 2
		'
		'splitContainer1
		'
		Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
		Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
		Me.splitContainer1.Name = "splitContainer1"
		'
		'splitContainer1.Panel1
		'
		Me.splitContainer1.Panel1.Controls.Add(Me.tableLayoutPanel2)
		'
		'splitContainer1.Panel2
		'
		Me.splitContainer1.Panel2.Controls.Add(Me.tableLayoutPanel1)
		Me.splitContainer1.Size = New System.Drawing.Size(905, 446)
		Me.splitContainer1.SplitterDistance = 223
		Me.splitContainer1.TabIndex = 3
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.ColumnCount = 2
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Controls.Add(Me.btnCapture, 0, 5)
		Me.tableLayoutPanel2.Controls.Add(Me.cbSelectedPosition, 1, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.palmSelector, 0, 3)
		Me.tableLayoutPanel2.Controls.Add(Me.gbOptions, 0, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.gbSource, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.label1, 0, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.palmsTree, 0, 8)
		Me.tableLayoutPanel2.Controls.Add(Me.btnCancel, 1, 5)
		Me.tableLayoutPanel2.Controls.Add(Me.btnForce, 0, 6)
		Me.tableLayoutPanel2.Controls.Add(Me.btnOpen, 0, 7)
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 9
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(223, 446)
		Me.tableLayoutPanel2.TabIndex = 0
		'
		'btnCapture
		'
		Me.btnCapture.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnCapture.Location = New System.Drawing.Point(3, 296)
		Me.btnCapture.Name = "btnCapture"
		Me.btnCapture.Size = New System.Drawing.Size(91, 23)
		Me.btnCapture.TabIndex = 5
		Me.btnCapture.Text = "&Capture"
		Me.btnCapture.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnCapture.UseVisualStyleBackColor = True
		'
		'cbSelectedPosition
		'
		Me.cbSelectedPosition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbSelectedPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbSelectedPosition.Enabled = False
		Me.cbSelectedPosition.FormattingEnabled = True
		Me.cbSelectedPosition.Location = New System.Drawing.Point(100, 269)
		Me.cbSelectedPosition.MaxDropDownItems = 20
		Me.cbSelectedPosition.Name = "cbSelectedPosition"
		Me.cbSelectedPosition.Size = New System.Drawing.Size(120, 21)
		Me.cbSelectedPosition.TabIndex = 5
		'
		'gbOptions
		'
		Me.gbOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.SetColumnSpan(Me.gbOptions, 2)
		Me.gbOptions.Controls.Add(Me.chbWithGeneralization)
		Me.gbOptions.Controls.Add(Me.chbCaptureAutomatically)
		Me.gbOptions.Controls.Add(Me.label2)
		Me.gbOptions.Controls.Add(Me.cbImpression)
		Me.gbOptions.Location = New System.Drawing.Point(3, 81)
		Me.gbOptions.Name = "gbOptions"
		Me.gbOptions.Size = New System.Drawing.Size(217, 83)
		Me.gbOptions.TabIndex = 15
		Me.gbOptions.TabStop = False
		Me.gbOptions.Text = "Options"
		'
		'chbWithGeneralization
		'
		Me.chbWithGeneralization.AutoSize = True
		Me.chbWithGeneralization.Location = New System.Drawing.Point(9, 63)
		Me.chbWithGeneralization.Name = "chbWithGeneralization"
		Me.chbWithGeneralization.Size = New System.Drawing.Size(116, 17)
		Me.chbWithGeneralization.TabIndex = 6
		Me.chbWithGeneralization.Text = "With generalization"
		Me.chbWithGeneralization.UseVisualStyleBackColor = True
		'
		'chbCaptureAutomatically
		'
		Me.chbCaptureAutomatically.AutoSize = True
		Me.chbCaptureAutomatically.Checked = True
		Me.chbCaptureAutomatically.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chbCaptureAutomatically.Location = New System.Drawing.Point(9, 40)
		Me.chbCaptureAutomatically.Name = "chbCaptureAutomatically"
		Me.chbCaptureAutomatically.Size = New System.Drawing.Size(127, 17)
		Me.chbCaptureAutomatically.TabIndex = 5
		Me.chbCaptureAutomatically.Text = "Capture automatically"
		Me.chbCaptureAutomatically.UseVisualStyleBackColor = True
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(6, 16)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(57, 13)
		Me.label2.TabIndex = 4
		Me.label2.Text = "Impression"
		'
		'cbImpression
		'
		Me.cbImpression.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbImpression.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbImpression.Enabled = False
		Me.cbImpression.FormattingEnabled = True
		Me.cbImpression.Location = New System.Drawing.Point(69, 13)
		Me.cbImpression.MaxDropDownItems = 20
		Me.cbImpression.Name = "cbImpression"
		Me.cbImpression.Size = New System.Drawing.Size(142, 21)
		Me.cbImpression.TabIndex = 3
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 266)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(91, 27)
		Me.label1.TabIndex = 16
		Me.label1.Text = "Selected position:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'palmsTree
		'
		Me.palmsTree.AllowNew = Neurotec.Biometrics.NBiometricType.None
		Me.palmsTree.AllowRemove = True
		Me.tableLayoutPanel2.SetColumnSpan(Me.palmsTree, 2)
		Me.palmsTree.Dock = System.Windows.Forms.DockStyle.Fill
		Me.palmsTree.Location = New System.Drawing.Point(3, 383)
		Me.palmsTree.Name = "palmsTree"
		Me.palmsTree.SelectedItem = Nothing
		Me.palmsTree.ShowBiometricsOnly = True
		Me.palmsTree.ShownTypes = Neurotec.Biometrics.NBiometricType.PalmPrint
		Me.palmsTree.Size = New System.Drawing.Size(217, 60)
		Me.palmsTree.Subject = Nothing
		Me.palmsTree.TabIndex = 17
		'
		'btnCancel
		'
		Me.btnCancel.Location = New System.Drawing.Point(100, 296)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(91, 23)
		Me.btnCancel.TabIndex = 19
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'btnForce
		'
		Me.btnForce.Location = New System.Drawing.Point(3, 325)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(91, 23)
		Me.btnForce.TabIndex = 20
		Me.btnForce.Text = "&Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'btnOpen
		'
		Me.btnOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnOpen.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.btnOpen.Location = New System.Drawing.Point(3, 354)
		Me.btnOpen.Name = "btnOpen"
		Me.btnOpen.Size = New System.Drawing.Size(91, 23)
		Me.btnOpen.TabIndex = 18
		Me.btnOpen.Text = "Open"
		Me.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpen.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 3
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.19643!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.80357!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.palmView, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnFinish, 2, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.horizontalZoomSlider, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.chbShowReturned, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.generalizeProgressView, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 2)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 4
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(678, 446)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'btnFinish
		'
		Me.btnFinish.Location = New System.Drawing.Point(599, 414)
		Me.btnFinish.Name = "btnFinish"
		Me.btnFinish.Size = New System.Drawing.Size(75, 23)
		Me.btnFinish.TabIndex = 4
		Me.btnFinish.Text = "&Finish"
		Me.btnFinish.UseVisualStyleBackColor = True
		'
		'horizontalZoomSlider
		'
		Me.horizontalZoomSlider.Location = New System.Drawing.Point(3, 414)
		Me.horizontalZoomSlider.Name = "horizontalZoomSlider"
		Me.horizontalZoomSlider.Size = New System.Drawing.Size(249, 29)
		Me.horizontalZoomSlider.TabIndex = 26
		Me.horizontalZoomSlider.View = Me.palmView
		'
		'chbShowReturned
		'
		Me.chbShowReturned.AutoSize = True
		Me.chbShowReturned.Dock = System.Windows.Forms.DockStyle.Fill
		Me.chbShowReturned.Location = New System.Drawing.Point(266, 414)
		Me.chbShowReturned.Name = "chbShowReturned"
		Me.chbShowReturned.Size = New System.Drawing.Size(327, 29)
		Me.chbShowReturned.TabIndex = 27
		Me.chbShowReturned.Text = "Show binarized image"
		Me.chbShowReturned.UseVisualStyleBackColor = True
		'
		'generalizeProgressView
		'
		Me.generalizeProgressView.AutoSize = True
		Me.generalizeProgressView.Biometrics = Nothing
		Me.tableLayoutPanel1.SetColumnSpan(Me.generalizeProgressView, 3)
		Me.generalizeProgressView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.generalizeProgressView.EnableMouseSelection = False
		Me.generalizeProgressView.Generalized = Nothing
		Me.generalizeProgressView.Location = New System.Drawing.Point(3, 347)
		Me.generalizeProgressView.Name = "generalizeProgressView"
		Me.generalizeProgressView.Selected = Nothing
		Me.generalizeProgressView.Size = New System.Drawing.Size(672, 35)
		Me.generalizeProgressView.StatusText = "Generalization status"
		Me.generalizeProgressView.TabIndex = 28
		Me.generalizeProgressView.View = Me.palmView
		'
		'TableLayoutPanel3
		'
		Me.TableLayoutPanel3.ColumnCount = 2
		Me.tableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel3, 3)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Controls.Add(Me.busyIndicator, 0, 0)
		Me.TableLayoutPanel3.Controls.Add(Me.lblStatus, 1, 0)
		Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 388)
		Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
		Me.TableLayoutPanel3.RowCount = 1
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Size = New System.Drawing.Size(672, 20)
		Me.TableLayoutPanel3.TabIndex = 29
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
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.BackColor = System.Drawing.Color.Red
		Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.ForeColor = System.Drawing.Color.White
		Me.lblStatus.Location = New System.Drawing.Point(23, 0)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(646, 20)
		Me.lblStatus.TabIndex = 3
		Me.lblStatus.Text = "Status:"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'CapturePalmsPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.splitContainer1)
		Me.Name = "CapturePalmsPage"
		Me.Size = New System.Drawing.Size(905, 446)
		Me.gbSource.ResumeLayout(False)
		Me.gbSource.PerformLayout()
		Me.splitContainer1.Panel1.ResumeLayout(False)
		Me.splitContainer1.Panel2.ResumeLayout(False)
		Me.splitContainer1.ResumeLayout(False)
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.gbOptions.ResumeLayout(False)
		Me.gbOptions.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.TableLayoutPanel3.ResumeLayout(False)
		Me.TableLayoutPanel3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents palmSelector As PalmSelector
	Private gbSource As System.Windows.Forms.GroupBox
	Private WithEvents rbFile As System.Windows.Forms.RadioButton
	Private WithEvents rbScanner As System.Windows.Forms.RadioButton
	Private palmView As Neurotec.Biometrics.Gui.NFingerView
	Private splitContainer1 As System.Windows.Forms.SplitContainer
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents btnCapture As System.Windows.Forms.Button
	Private WithEvents cbSelectedPosition As System.Windows.Forms.ComboBox
	Private gbOptions As System.Windows.Forms.GroupBox
	Private label2 As System.Windows.Forms.Label
	Private cbImpression As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private WithEvents palmsTree As SubjectTreeControl
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private horizontalZoomSlider As Neurotec.Gui.NViewZoomSlider
	Private lblStatus As System.Windows.Forms.Label
	Private WithEvents btnOpen As System.Windows.Forms.Button
	Private WithEvents btnFinish As System.Windows.Forms.Button
	Private WithEvents chbShowReturned As System.Windows.Forms.CheckBox
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private chbCaptureAutomatically As System.Windows.Forms.CheckBox
	Private WithEvents btnForce As System.Windows.Forms.Button
	Private chbWithGeneralization As System.Windows.Forms.CheckBox
	Private generalizeProgressView As GeneralizeProgressView
	Private busyIndicator As BusyIndicator
	Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
End Class
