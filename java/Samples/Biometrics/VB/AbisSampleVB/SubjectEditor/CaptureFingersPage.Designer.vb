Imports Microsoft.VisualBasic
Imports System
Partial Public Class CaptureFingersPage
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
		Me.components = New System.ComponentModel.Container
		Me.fingerSelector = New Neurotec.Samples.FingerSelector
		Me.gbOptions = New System.Windows.Forms.GroupBox
		Me.chbWithGeneralization = New System.Windows.Forms.CheckBox
		Me.chbCaptureAutomatically = New System.Windows.Forms.CheckBox
		Me.label2 = New System.Windows.Forms.Label
		Me.cbImpression = New System.Windows.Forms.ComboBox
		Me.cbScenario = New System.Windows.Forms.ComboBox
		Me.label1 = New System.Windows.Forms.Label
		Me.gbSource = New System.Windows.Forms.GroupBox
		Me.rbTenPrintCard = New System.Windows.Forms.RadioButton
		Me.rbFiles = New System.Windows.Forms.RadioButton
		Me.rbScanner = New System.Windows.Forms.RadioButton
		Me.btnStart = New System.Windows.Forms.Button
		Me.btnSkip = New System.Windows.Forms.Button
		Me.btnRepeat = New System.Windows.Forms.Button
		Me.btnOpenImage = New System.Windows.Forms.Button
		Me.fingerView = New Neurotec.Biometrics.Gui.NFingerView
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.btnCancel = New System.Windows.Forms.Button
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.fingersTree = New Neurotec.Samples.SubjectTreeControl
		Me.lblNote = New System.Windows.Forms.Label
		Me.horizontalZoomSlider = New Neurotec.Gui.NViewZoomSlider
		Me.contextMnuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
		Me.tsmiSelect = New System.Windows.Forms.ToolStripMenuItem
		Me.tsmiMissing = New System.Windows.Forms.ToolStripMenuItem
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.panelNavigations = New System.Windows.Forms.TableLayoutPanel
		Me.btnForce = New System.Windows.Forms.Button
		Me.bntFinish = New System.Windows.Forms.Button
		Me.chbShowReturned = New System.Windows.Forms.CheckBox
		Me.generalizeProgressView = New Neurotec.Samples.GeneralizeProgressView
		Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
		Me.busyIndicator = New Neurotec.Samples.BusyIndicator
		Me.lblStatus = New System.Windows.Forms.Label
		Me.tableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.gbOptions.SuspendLayout()
		Me.gbSource.SuspendLayout()
		Me.tableLayoutPanel1.SuspendLayout()
		Me.contextMnuStrip.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.panelNavigations.SuspendLayout()
		Me.TableLayoutPanel4.SuspendLayout()
		Me.tableLayoutPanel3.SuspendLayout()
		Me.SuspendLayout()
		'
		'fingerSelector
		'
		Me.fingerSelector.AllowedPositions = New Neurotec.Biometrics.NFPosition() {Neurotec.Biometrics.NFPosition.RightThumb, Neurotec.Biometrics.NFPosition.RightIndexFinger, Neurotec.Biometrics.NFPosition.RightMiddleFinger, Neurotec.Biometrics.NFPosition.RightRingFinger, Neurotec.Biometrics.NFPosition.RightLittleFinger, Neurotec.Biometrics.NFPosition.LeftThumb, Neurotec.Biometrics.NFPosition.LeftIndexFinger, Neurotec.Biometrics.NFPosition.LeftMiddleFinger, Neurotec.Biometrics.NFPosition.LeftRingFinger, Neurotec.Biometrics.NFPosition.LeftLittle}
		Me.fingerSelector.AllowHighlight = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.fingerSelector, 2)
		Me.fingerSelector.IsRolled = False
		Me.fingerSelector.Location = New System.Drawing.Point(3, 3)
		Me.fingerSelector.MissingPositions = New Neurotec.Biometrics.NFPosition(-1) {}
		Me.fingerSelector.Name = "fingerSelector"
		Me.fingerSelector.SelectedPosition = Neurotec.Biometrics.NFPosition.Unknown
		Me.fingerSelector.ShowFingerNails = True
		Me.fingerSelector.ShowPalmCreases = False
		Me.fingerSelector.Size = New System.Drawing.Size(240, 119)
		Me.fingerSelector.TabIndex = 0
		Me.fingerSelector.Text = "fingerSelector1"
		'
		'gbOptions
		'
		Me.gbOptions.Controls.Add(Me.chbWithGeneralization)
		Me.gbOptions.Controls.Add(Me.chbCaptureAutomatically)
		Me.gbOptions.Controls.Add(Me.label2)
		Me.gbOptions.Controls.Add(Me.cbImpression)
		Me.gbOptions.Controls.Add(Me.cbScenario)
		Me.gbOptions.Controls.Add(Me.label1)
		Me.gbOptions.Location = New System.Drawing.Point(255, 3)
		Me.gbOptions.Name = "gbOptions"
		Me.gbOptions.Size = New System.Drawing.Size(324, 85)
		Me.gbOptions.TabIndex = 13
		Me.gbOptions.TabStop = False
		Me.gbOptions.Text = "Options"
		'
		'chbWithGeneralization
		'
		Me.chbWithGeneralization.AutoSize = True
		Me.chbWithGeneralization.Location = New System.Drawing.Point(207, 64)
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
		Me.chbCaptureAutomatically.Location = New System.Drawing.Point(73, 64)
		Me.chbCaptureAutomatically.Name = "chbCaptureAutomatically"
		Me.chbCaptureAutomatically.Size = New System.Drawing.Size(127, 17)
		Me.chbCaptureAutomatically.TabIndex = 5
		Me.chbCaptureAutomatically.Text = "Capture automatically"
		Me.chbCaptureAutomatically.UseVisualStyleBackColor = True
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Location = New System.Drawing.Point(6, 40)
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
		Me.cbImpression.Location = New System.Drawing.Point(73, 37)
		Me.cbImpression.MaxDropDownItems = 20
		Me.cbImpression.Name = "cbImpression"
		Me.cbImpression.Size = New System.Drawing.Size(245, 21)
		Me.cbImpression.TabIndex = 3
		'
		'cbScenario
		'
		Me.cbScenario.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cbScenario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbScenario.FormattingEnabled = True
		Me.cbScenario.Location = New System.Drawing.Point(73, 10)
		Me.cbScenario.Name = "cbScenario"
		Me.cbScenario.Size = New System.Drawing.Size(245, 21)
		Me.cbScenario.TabIndex = 2
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Location = New System.Drawing.Point(6, 13)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(49, 13)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Scenario"
		'
		'gbSource
		'
		Me.gbSource.AutoSize = True
		Me.gbSource.Controls.Add(Me.rbTenPrintCard)
		Me.gbSource.Controls.Add(Me.rbFiles)
		Me.gbSource.Controls.Add(Me.rbScanner)
		Me.gbSource.Location = New System.Drawing.Point(3, 3)
		Me.gbSource.MaximumSize = New System.Drawing.Size(0, 85)
		Me.gbSource.MinimumSize = New System.Drawing.Size(246, 0)
		Me.gbSource.Name = "gbSource"
		Me.gbSource.Size = New System.Drawing.Size(246, 85)
		Me.gbSource.TabIndex = 14
		Me.gbSource.TabStop = False
		Me.gbSource.Text = "Source"
		'
		'rbTenPrintCard
		'
		Me.rbTenPrintCard.AutoSize = True
		Me.rbTenPrintCard.Location = New System.Drawing.Point(6, 53)
		Me.rbTenPrintCard.Name = "rbTenPrintCard"
		Me.rbTenPrintCard.Size = New System.Drawing.Size(91, 17)
		Me.rbTenPrintCard.TabIndex = 2
		Me.rbTenPrintCard.Text = "Ten print card"
		Me.rbTenPrintCard.UseVisualStyleBackColor = True
		'
		'rbFiles
		'
		Me.rbFiles.AutoSize = True
		Me.rbFiles.Location = New System.Drawing.Point(6, 33)
		Me.rbFiles.Name = "rbFiles"
		Me.rbFiles.Size = New System.Drawing.Size(41, 17)
		Me.rbFiles.TabIndex = 1
		Me.rbFiles.Text = "File"
		Me.rbFiles.UseVisualStyleBackColor = True
		'
		'rbScanner
		'
		Me.rbScanner.AutoSize = True
		Me.rbScanner.Checked = True
		Me.rbScanner.Location = New System.Drawing.Point(6, 13)
		Me.rbScanner.Name = "rbScanner"
		Me.rbScanner.Size = New System.Drawing.Size(65, 17)
		Me.rbScanner.TabIndex = 0
		Me.rbScanner.TabStop = True
		Me.rbScanner.Text = "Scanner"
		Me.rbScanner.UseVisualStyleBackColor = True
		'
		'btnStart
		'
		Me.btnStart.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnStart.Location = New System.Drawing.Point(3, 141)
		Me.btnStart.Name = "btnStart"
		Me.btnStart.Size = New System.Drawing.Size(109, 23)
		Me.btnStart.TabIndex = 15
		Me.btnStart.Text = "&Start capturing"
		Me.btnStart.UseVisualStyleBackColor = True
		'
		'btnSkip
		'
		Me.btnSkip.Image = Global.Neurotec.Samples.My.Resources.Resources.GoToNext
		Me.btnSkip.Location = New System.Drawing.Point(265, 3)
		Me.btnSkip.Name = "btnSkip"
		Me.btnSkip.Size = New System.Drawing.Size(90, 23)
		Me.btnSkip.TabIndex = 16
		Me.btnSkip.Text = "&Next"
		Me.btnSkip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnSkip.UseVisualStyleBackColor = True
		'
		'btnRepeat
		'
		Me.btnRepeat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnRepeat.Image = Global.Neurotec.Samples.My.Resources.Resources.Repeat
		Me.btnRepeat.Location = New System.Drawing.Point(145, 3)
		Me.btnRepeat.Name = "btnRepeat"
		Me.btnRepeat.Size = New System.Drawing.Size(114, 23)
		Me.btnRepeat.TabIndex = 17
		Me.btnRepeat.Text = "&Repeat"
		Me.btnRepeat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnRepeat.UseVisualStyleBackColor = True
		'
		'btnOpenImage
		'
		Me.btnOpenImage.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.btnOpenImage.Image = Global.Neurotec.Samples.My.Resources.Resources.openfolderHS
		Me.btnOpenImage.Location = New System.Drawing.Point(3, 170)
		Me.btnOpenImage.Name = "btnOpenImage"
		Me.btnOpenImage.Size = New System.Drawing.Size(109, 23)
		Me.btnOpenImage.TabIndex = 18
		Me.btnOpenImage.Text = "&Open image"
		Me.btnOpenImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
		Me.btnOpenImage.UseVisualStyleBackColor = True
		'
		'fingerView
		'
		Me.fingerView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.fingerView.BackColor = System.Drawing.SystemColors.Control
		Me.fingerView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.fingerView.BoundingRectColor = System.Drawing.Color.Red
		Me.tableLayoutPanel2.SetColumnSpan(Me.fingerView, 3)
		Me.fingerView.ForeColor = System.Drawing.Color.Red
		Me.fingerView.Location = New System.Drawing.Point(3, 3)
		Me.fingerView.MinutiaColor = System.Drawing.Color.Red
		Me.fingerView.Name = "fingerView"
		Me.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.fingerView.ResultImageColor = System.Drawing.Color.Green
		Me.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.fingerView.SingularPointColor = System.Drawing.Color.Red
		Me.fingerView.Size = New System.Drawing.Size(724, 378)
		Me.fingerView.TabIndex = 19
		Me.fingerView.TreeColor = System.Drawing.Color.Crimson
		Me.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.fingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.fingerView.TreeWidth = 2
		'
		'btnCancel
		'
		Me.btnCancel.Location = New System.Drawing.Point(457, 3)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(90, 23)
		Me.btnCancel.TabIndex = 20
		Me.btnCancel.Text = "&Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.fingerSelector, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.fingersTree, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.lblNote, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnOpenImage, 0, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.btnStart, 0, 2)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(3, 97)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 6
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(246, 519)
		Me.tableLayoutPanel1.TabIndex = 21
		'
		'fingersTree
		'
		Me.fingersTree.AllowNew = Neurotec.Biometrics.NBiometricType.None
		Me.fingersTree.AllowRemove = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.fingersTree, 2)
		Me.fingersTree.Dock = System.Windows.Forms.DockStyle.Fill
		Me.fingersTree.Location = New System.Drawing.Point(3, 199)
		Me.fingersTree.Name = "fingersTree"
		Me.fingersTree.SelectedItem = Nothing
		Me.fingersTree.ShowBiometricsOnly = True
		Me.fingersTree.ShownTypes = Neurotec.Biometrics.NBiometricType.Fingerprint
		Me.fingersTree.Size = New System.Drawing.Size(240, 317)
		Me.fingersTree.Subject = Nothing
		Me.fingersTree.TabIndex = 21
		'
		'lblNote
		'
		Me.lblNote.AutoSize = True
		Me.tableLayoutPanel1.SetColumnSpan(Me.lblNote, 2)
		Me.lblNote.Location = New System.Drawing.Point(3, 125)
		Me.lblNote.Name = "lblNote"
		Me.lblNote.Size = New System.Drawing.Size(33, 13)
		Me.lblNote.TabIndex = 22
		Me.lblNote.Text = "Note:"
		'
		'horizontalZoomSlider
		'
		Me.horizontalZoomSlider.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.horizontalZoomSlider.Location = New System.Drawing.Point(3, 492)
		Me.horizontalZoomSlider.Name = "horizontalZoomSlider"
		Me.horizontalZoomSlider.Size = New System.Drawing.Size(239, 24)
		Me.horizontalZoomSlider.TabIndex = 22
		Me.horizontalZoomSlider.View = Me.fingerView
		'
		'contextMnuStrip
		'
		Me.contextMnuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmiSelect, Me.tsmiMissing})
		Me.contextMnuStrip.Name = "contextMenuStrip"
		Me.contextMnuStrip.Size = New System.Drawing.Size(160, 48)
		'
		'tsmiSelect
		'
		Me.tsmiSelect.Name = "tsmiSelect"
		Me.tsmiSelect.Size = New System.Drawing.Size(159, 22)
		Me.tsmiSelect.Text = "Select"
		'
		'tsmiMissing
		'
		Me.tsmiMissing.Name = "tsmiMissing"
		Me.tsmiMissing.Size = New System.Drawing.Size(159, 22)
		Me.tsmiMissing.Text = "Mark as missing"
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel2.ColumnCount = 3
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tableLayoutPanel2.Controls.Add(Me.horizontalZoomSlider, 0, 5)
		Me.tableLayoutPanel2.Controls.Add(Me.fingerView, 0, 1)
		Me.tableLayoutPanel2.Controls.Add(Me.panelNavigations, 0, 4)
		Me.tableLayoutPanel2.Controls.Add(Me.bntFinish, 2, 5)
		Me.tableLayoutPanel2.Controls.Add(Me.chbShowReturned, 1, 5)
		Me.tableLayoutPanel2.Controls.Add(Me.generalizeProgressView, 0, 2)
		Me.tableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 0, 3)
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(252, 97)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 6
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(730, 519)
		Me.tableLayoutPanel2.TabIndex = 24
		'
		'panelNavigations
		'
		Me.panelNavigations.ColumnCount = 4
		Me.tableLayoutPanel2.SetColumnSpan(Me.panelNavigations, 3)
		Me.panelNavigations.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.36306!))
		Me.panelNavigations.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.panelNavigations.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.panelNavigations.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.63694!))
		Me.panelNavigations.Controls.Add(Me.btnRepeat, 0, 0)
		Me.panelNavigations.Controls.Add(Me.btnSkip, 1, 0)
		Me.panelNavigations.Controls.Add(Me.btnCancel, 3, 0)
		Me.panelNavigations.Controls.Add(Me.btnForce, 2, 0)
		Me.panelNavigations.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelNavigations.Location = New System.Drawing.Point(3, 454)
		Me.panelNavigations.Name = "panelNavigations"
		Me.panelNavigations.RowCount = 1
		Me.panelNavigations.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.panelNavigations.Size = New System.Drawing.Size(724, 32)
		Me.panelNavigations.TabIndex = 25
		'
		'btnForce
		'
		Me.btnForce.Location = New System.Drawing.Point(361, 3)
		Me.btnForce.Name = "btnForce"
		Me.btnForce.Size = New System.Drawing.Size(90, 23)
		Me.btnForce.TabIndex = 21
		Me.btnForce.Text = "&Force"
		Me.btnForce.UseVisualStyleBackColor = True
		'
		'bntFinish
		'
		Me.bntFinish.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.bntFinish.Location = New System.Drawing.Point(652, 492)
		Me.bntFinish.Name = "bntFinish"
		Me.bntFinish.Size = New System.Drawing.Size(75, 24)
		Me.bntFinish.TabIndex = 30
		Me.bntFinish.Text = "&Finish"
		Me.bntFinish.UseVisualStyleBackColor = True
		'
		'chbShowReturned
		'
		Me.chbShowReturned.AutoSize = True
		Me.chbShowReturned.Dock = System.Windows.Forms.DockStyle.Fill
		Me.chbShowReturned.Enabled = False
		Me.chbShowReturned.Location = New System.Drawing.Point(248, 492)
		Me.chbShowReturned.Name = "chbShowReturned"
		Me.chbShowReturned.Size = New System.Drawing.Size(398, 24)
		Me.chbShowReturned.TabIndex = 31
		Me.chbShowReturned.Text = "Show binarized image"
		Me.chbShowReturned.UseVisualStyleBackColor = True
		'
		'generalizeProgressView
		'
		Me.generalizeProgressView.AutoSize = True
		Me.generalizeProgressView.Biometrics = Nothing
		Me.tableLayoutPanel2.SetColumnSpan(Me.generalizeProgressView, 3)
		Me.generalizeProgressView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.generalizeProgressView.EnableMouseSelection = False
		Me.generalizeProgressView.Generalized = Nothing
		Me.generalizeProgressView.Location = New System.Drawing.Point(3, 387)
		Me.generalizeProgressView.Name = "generalizeProgressView"
		Me.generalizeProgressView.Selected = Nothing
		Me.generalizeProgressView.Size = New System.Drawing.Size(724, 35)
		Me.generalizeProgressView.StatusText = "Generalize progress control"
		Me.generalizeProgressView.TabIndex = 32
		Me.generalizeProgressView.View = Me.fingerView
		'
		'TableLayoutPanel4
		'
		Me.TableLayoutPanel4.ColumnCount = 2
		Me.tableLayoutPanel2.SetColumnSpan(Me.TableLayoutPanel4, 3)
		Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel4.Controls.Add(Me.busyIndicator, 0, 0)
		Me.TableLayoutPanel4.Controls.Add(Me.lblStatus, 1, 0)
		Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel4.Location = New System.Drawing.Point(3, 428)
		Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
		Me.TableLayoutPanel4.RowCount = 1
		Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel4.Size = New System.Drawing.Size(724, 20)
		Me.TableLayoutPanel4.TabIndex = 33
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
		Me.lblStatus.Size = New System.Drawing.Size(698, 20)
		Me.lblStatus.TabIndex = 24
		Me.lblStatus.Text = "Status"
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'tableLayoutPanel3
		'
		Me.tableLayoutPanel3.ColumnCount = 2
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel3.Controls.Add(Me.gbSource, 0, 0)
		Me.tableLayoutPanel3.Controls.Add(Me.gbOptions, 1, 0)
		Me.tableLayoutPanel3.Location = New System.Drawing.Point(6, 3)
		Me.tableLayoutPanel3.Name = "tableLayoutPanel3"
		Me.tableLayoutPanel3.RowCount = 1
		Me.tableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel3.Size = New System.Drawing.Size(976, 94)
		Me.tableLayoutPanel3.TabIndex = 25
		'
		'CaptureFingersPage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel3)
		Me.Controls.Add(Me.tableLayoutPanel2)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "CaptureFingersPage"
		Me.Size = New System.Drawing.Size(985, 619)
		Me.gbOptions.ResumeLayout(False)
		Me.gbOptions.PerformLayout()
		Me.gbSource.ResumeLayout(False)
		Me.gbSource.PerformLayout()
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.contextMnuStrip.ResumeLayout(False)
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.panelNavigations.ResumeLayout(False)
		Me.TableLayoutPanel4.ResumeLayout(False)
		Me.TableLayoutPanel4.PerformLayout()
		Me.tableLayoutPanel3.ResumeLayout(False)
		Me.tableLayoutPanel3.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents fingerSelector As Neurotec.Samples.FingerSelector
	Private gbOptions As System.Windows.Forms.GroupBox
	Private label2 As System.Windows.Forms.Label
	Private cbImpression As System.Windows.Forms.ComboBox
	Private WithEvents cbScenario As System.Windows.Forms.ComboBox
	Private label1 As System.Windows.Forms.Label
	Private gbSource As System.Windows.Forms.GroupBox
	Private WithEvents rbFiles As System.Windows.Forms.RadioButton
	Private WithEvents rbScanner As System.Windows.Forms.RadioButton
	Private WithEvents btnStart As System.Windows.Forms.Button
	Private WithEvents btnSkip As System.Windows.Forms.Button
	Private WithEvents btnRepeat As System.Windows.Forms.Button
	Private WithEvents btnOpenImage As System.Windows.Forms.Button
	Private fingerView As Neurotec.Biometrics.Gui.NFingerView
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents btnCancel As System.Windows.Forms.Button
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private WithEvents fingersTree As SubjectTreeControl
	Private horizontalZoomSlider As Neurotec.Gui.NViewZoomSlider
	Private WithEvents contextMnuStrip As System.Windows.Forms.ContextMenuStrip
	Private tsmiSelect As System.Windows.Forms.ToolStripMenuItem
	Private tsmiMissing As System.Windows.Forms.ToolStripMenuItem
	Private lblNote As System.Windows.Forms.Label
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private lblStatus As System.Windows.Forms.Label
	Private WithEvents bntFinish As System.Windows.Forms.Button
	Private panelNavigations As System.Windows.Forms.TableLayoutPanel
	Private WithEvents chbShowReturned As System.Windows.Forms.CheckBox
	Private WithEvents rbTenPrintCard As System.Windows.Forms.RadioButton
	Private tableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Private chbCaptureAutomatically As System.Windows.Forms.CheckBox
	Private WithEvents btnForce As System.Windows.Forms.Button
	Private chbWithGeneralization As System.Windows.Forms.CheckBox
	Private generalizeProgressView As GeneralizeProgressView
	Private busyIndicator As BusyIndicator
	Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
End Class
