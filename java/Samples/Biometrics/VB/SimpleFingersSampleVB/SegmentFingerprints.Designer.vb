Imports Microsoft.VisualBasic
Imports System
Partial Public Class SegmentFingerprints
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SegmentFingerprints))
		Me.chlbMissing = New System.Windows.Forms.CheckedListBox
		Me.label1 = New System.Windows.Forms.Label
		Me.label2 = New System.Windows.Forms.Label
		Me.btnSaveImages = New System.Windows.Forms.Button
		Me.lbPosition = New System.Windows.Forms.ListBox
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.Panel5 = New System.Windows.Forms.Panel
		Me.segmentButton = New System.Windows.Forms.Button
		Me.openButton = New System.Windows.Forms.Button
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
		Me.folderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog
		Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
		Me.originalFingerView = New Neurotec.Biometrics.Gui.NFingerView
		Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.Panel4 = New System.Windows.Forms.Panel
		Me.lbClass4 = New System.Windows.Forms.Label
		Me.lbQuality4 = New System.Windows.Forms.Label
		Me.lbPosition4 = New System.Windows.Forms.Label
		Me.pictureBox4 = New System.Windows.Forms.PictureBox
		Me.Panel3 = New System.Windows.Forms.Panel
		Me.lbClass3 = New System.Windows.Forms.Label
		Me.lbQuality3 = New System.Windows.Forms.Label
		Me.lbPosition3 = New System.Windows.Forms.Label
		Me.pictureBox3 = New System.Windows.Forms.PictureBox
		Me.Panel2 = New System.Windows.Forms.Panel
		Me.lbClass2 = New System.Windows.Forms.Label
		Me.lbQuality2 = New System.Windows.Forms.Label
		Me.lbPosition2 = New System.Windows.Forms.Label
		Me.pictureBox2 = New System.Windows.Forms.PictureBox
		Me.Panel1 = New System.Windows.Forms.Panel
		Me.lbClass1 = New System.Windows.Forms.Label
		Me.lbQuality1 = New System.Windows.Forms.Label
		Me.lbPosition1 = New System.Windows.Forms.Label
		Me.pictureBox1 = New System.Windows.Forms.PictureBox
		Me.lblStatus = New System.Windows.Forms.Label
		Me.nViewZoomSlider1 = New Neurotec.Gui.NViewZoomSlider
		Me.LicensePanel1 = New Neurotec.Samples.LicensePanel
		Me.tableLayoutPanel1.SuspendLayout()
		Me.Panel5.SuspendLayout()
		Me.SplitContainer1.Panel1.SuspendLayout()
		Me.SplitContainer1.Panel2.SuspendLayout()
		Me.SplitContainer1.SuspendLayout()
		Me.TableLayoutPanel2.SuspendLayout()
		Me.Panel4.SuspendLayout()
		CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel3.SuspendLayout()
		CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel2.SuspendLayout()
		CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel1.SuspendLayout()
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'chlbMissing
		'
		Me.chlbMissing.Dock = System.Windows.Forms.DockStyle.Fill
		Me.chlbMissing.FormattingEnabled = True
		Me.chlbMissing.Location = New System.Drawing.Point(168, 16)
		Me.chlbMissing.Name = "chlbMissing"
		Me.chlbMissing.Size = New System.Drawing.Size(159, 64)
		Me.chlbMissing.TabIndex = 3
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(159, 13)
		Me.label1.TabIndex = 5
		Me.label1.Text = "Position"
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.label2.Location = New System.Drawing.Point(168, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(159, 13)
		Me.label2.TabIndex = 6
		Me.label2.Text = "Missing positions"
		'
		'btnSaveImages
		'
		Me.btnSaveImages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.btnSaveImages.Enabled = False
		Me.btnSaveImages.Image = CType(resources.GetObject("btnSaveImages.Image"), System.Drawing.Image)
		Me.btnSaveImages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.btnSaveImages.Location = New System.Drawing.Point(3, 349)
		Me.btnSaveImages.Name = "btnSaveImages"
		Me.btnSaveImages.Size = New System.Drawing.Size(96, 23)
		Me.btnSaveImages.TabIndex = 10
		Me.btnSaveImages.Text = "Save Images"
		Me.btnSaveImages.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.btnSaveImages.UseVisualStyleBackColor = True
		'
		'lbPosition
		'
		Me.lbPosition.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lbPosition.FormattingEnabled = True
		Me.lbPosition.Location = New System.Drawing.Point(3, 16)
		Me.lbPosition.Name = "lbPosition"
		Me.lbPosition.Size = New System.Drawing.Size(159, 69)
		Me.lbPosition.TabIndex = 11
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tableLayoutPanel1.ColumnCount = 3
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.lbPosition, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.label2, 1, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.chlbMissing, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.Panel5, 2, 1)
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(6, 42)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 2
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(429, 96)
		Me.tableLayoutPanel1.TabIndex = 12
		'
		'Panel5
		'
		Me.Panel5.Controls.Add(Me.segmentButton)
		Me.Panel5.Controls.Add(Me.openButton)
		Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel5.Location = New System.Drawing.Point(333, 16)
		Me.Panel5.Name = "Panel5"
		Me.Panel5.Size = New System.Drawing.Size(93, 77)
		Me.Panel5.TabIndex = 12
		'
		'segmentButton
		'
		Me.segmentButton.Location = New System.Drawing.Point(0, 46)
		Me.segmentButton.Name = "segmentButton"
		Me.segmentButton.Size = New System.Drawing.Size(93, 23)
		Me.segmentButton.TabIndex = 3
		Me.segmentButton.Text = "Segment"
		Me.segmentButton.UseVisualStyleBackColor = True
		'
		'openButton
		'
		Me.openButton.Image = CType(resources.GetObject("openButton.Image"), System.Drawing.Image)
		Me.openButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.openButton.Location = New System.Drawing.Point(0, 15)
		Me.openButton.Name = "openButton"
		Me.openButton.Size = New System.Drawing.Size(93, 25)
		Me.openButton.TabIndex = 2
		Me.openButton.Tag = "Open"
		Me.openButton.Text = "Open Image"
		Me.openButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		Me.openButton.UseVisualStyleBackColor = True
		'
		'folderBrowserDialog
		'
		Me.folderBrowserDialog.Description = "Select directory where to save cut images"
		'
		'SplitContainer1
		'
		Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
					Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.SplitContainer1.Location = New System.Drawing.Point(9, 144)
		Me.SplitContainer1.Name = "SplitContainer1"
		Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
		'
		'SplitContainer1.Panel1
		'
		Me.SplitContainer1.Panel1.Controls.Add(Me.originalFingerView)
		'
		'SplitContainer1.Panel2
		'
		Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
		Me.SplitContainer1.Size = New System.Drawing.Size(426, 199)
		Me.SplitContainer1.SplitterDistance = 89
		Me.SplitContainer1.TabIndex = 13
		'
		'originalFingerView
		'
		Me.originalFingerView.AutoScroll = True
		Me.originalFingerView.BackColor = System.Drawing.SystemColors.Control
		Me.originalFingerView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.originalFingerView.BoundingRectColor = System.Drawing.Color.Red
		Me.originalFingerView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.originalFingerView.Location = New System.Drawing.Point(0, 0)
		Me.originalFingerView.MinutiaColor = System.Drawing.Color.Red
		Me.originalFingerView.Name = "originalFingerView"
		Me.originalFingerView.NeighborMinutiaColor = System.Drawing.Color.Orange
		Me.originalFingerView.ResultImageColor = System.Drawing.Color.Green
		Me.originalFingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
		Me.originalFingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
		Me.originalFingerView.SingularPointColor = System.Drawing.Color.Red
		Me.originalFingerView.Size = New System.Drawing.Size(426, 89)
		Me.originalFingerView.TabIndex = 0
		Me.originalFingerView.TreeColor = System.Drawing.Color.Crimson
		Me.originalFingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
		Me.originalFingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
		Me.originalFingerView.TreeWidth = 2
		Me.originalFingerView.ZoomToFit = False
		'
		'TableLayoutPanel2
		'
		Me.TableLayoutPanel2.ColumnCount = 4
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
		Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
		Me.TableLayoutPanel2.Controls.Add(Me.Panel4, 3, 0)
		Me.TableLayoutPanel2.Controls.Add(Me.Panel3, 2, 0)
		Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 1, 0)
		Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
		Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
		Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
		Me.TableLayoutPanel2.RowCount = 1
		Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel2.Size = New System.Drawing.Size(426, 106)
		Me.TableLayoutPanel2.TabIndex = 0
		'
		'Panel4
		'
		Me.Panel4.Controls.Add(Me.lbClass4)
		Me.Panel4.Controls.Add(Me.lbQuality4)
		Me.Panel4.Controls.Add(Me.lbPosition4)
		Me.Panel4.Controls.Add(Me.pictureBox4)
		Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel4.Location = New System.Drawing.Point(321, 3)
		Me.Panel4.Name = "Panel4"
		Me.Panel4.Size = New System.Drawing.Size(102, 100)
		Me.Panel4.TabIndex = 3
		'
		'lbClass4
		'
		Me.lbClass4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbClass4.AutoSize = True
		Me.lbClass4.Location = New System.Drawing.Point(12, 61)
		Me.lbClass4.Name = "lbClass4"
		Me.lbClass4.Size = New System.Drawing.Size(35, 13)
		Me.lbClass4.TabIndex = 4
		Me.lbClass4.Text = "Class:"
		'
		'lbQuality4
		'
		Me.lbQuality4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbQuality4.AutoSize = True
		Me.lbQuality4.Location = New System.Drawing.Point(12, 49)
		Me.lbQuality4.Name = "lbQuality4"
		Me.lbQuality4.Size = New System.Drawing.Size(42, 13)
		Me.lbQuality4.TabIndex = 6
		Me.lbQuality4.Text = "Quality:"
		'
		'lbPosition4
		'
		Me.lbPosition4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbPosition4.AutoSize = True
		Me.lbPosition4.Location = New System.Drawing.Point(12, 36)
		Me.lbPosition4.Name = "lbPosition4"
		Me.lbPosition4.Size = New System.Drawing.Size(47, 13)
		Me.lbPosition4.TabIndex = 5
		Me.lbPosition4.Text = "Position:"
		'
		'pictureBox4
		'
		Me.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pictureBox4.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pictureBox4.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox4.Name = "pictureBox4"
		Me.pictureBox4.Size = New System.Drawing.Size(102, 100)
		Me.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pictureBox4.TabIndex = 3
		Me.pictureBox4.TabStop = False
		'
		'Panel3
		'
		Me.Panel3.Controls.Add(Me.lbClass3)
		Me.Panel3.Controls.Add(Me.lbQuality3)
		Me.Panel3.Controls.Add(Me.lbPosition3)
		Me.Panel3.Controls.Add(Me.pictureBox3)
		Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel3.Location = New System.Drawing.Point(215, 3)
		Me.Panel3.Name = "Panel3"
		Me.Panel3.Size = New System.Drawing.Size(100, 100)
		Me.Panel3.TabIndex = 2
		'
		'lbClass3
		'
		Me.lbClass3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbClass3.AutoSize = True
		Me.lbClass3.Location = New System.Drawing.Point(12, 62)
		Me.lbClass3.Name = "lbClass3"
		Me.lbClass3.Size = New System.Drawing.Size(35, 13)
		Me.lbClass3.TabIndex = 4
		Me.lbClass3.Text = "Class:"
		'
		'lbQuality3
		'
		Me.lbQuality3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbQuality3.AutoSize = True
		Me.lbQuality3.Location = New System.Drawing.Point(12, 49)
		Me.lbQuality3.Name = "lbQuality3"
		Me.lbQuality3.Size = New System.Drawing.Size(42, 13)
		Me.lbQuality3.TabIndex = 6
		Me.lbQuality3.Text = "Quality:"
		'
		'lbPosition3
		'
		Me.lbPosition3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbPosition3.AutoSize = True
		Me.lbPosition3.Location = New System.Drawing.Point(12, 36)
		Me.lbPosition3.Name = "lbPosition3"
		Me.lbPosition3.Size = New System.Drawing.Size(47, 13)
		Me.lbPosition3.TabIndex = 5
		Me.lbPosition3.Text = "Position:"
		'
		'pictureBox3
		'
		Me.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pictureBox3.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox3.Name = "pictureBox3"
		Me.pictureBox3.Size = New System.Drawing.Size(100, 100)
		Me.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pictureBox3.TabIndex = 3
		Me.pictureBox3.TabStop = False
		'
		'Panel2
		'
		Me.Panel2.Controls.Add(Me.lbClass2)
		Me.Panel2.Controls.Add(Me.lbQuality2)
		Me.Panel2.Controls.Add(Me.lbPosition2)
		Me.Panel2.Controls.Add(Me.pictureBox2)
		Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel2.Location = New System.Drawing.Point(109, 3)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(100, 100)
		Me.Panel2.TabIndex = 1
		'
		'lbClass2
		'
		Me.lbClass2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbClass2.AutoSize = True
		Me.lbClass2.Location = New System.Drawing.Point(12, 61)
		Me.lbClass2.Name = "lbClass2"
		Me.lbClass2.Size = New System.Drawing.Size(35, 13)
		Me.lbClass2.TabIndex = 4
		Me.lbClass2.Text = "Class:"
		'
		'lbQuality2
		'
		Me.lbQuality2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbQuality2.AutoSize = True
		Me.lbQuality2.Location = New System.Drawing.Point(12, 49)
		Me.lbQuality2.Name = "lbQuality2"
		Me.lbQuality2.Size = New System.Drawing.Size(42, 13)
		Me.lbQuality2.TabIndex = 6
		Me.lbQuality2.Text = "Quality:"
		'
		'lbPosition2
		'
		Me.lbPosition2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbPosition2.AutoSize = True
		Me.lbPosition2.Location = New System.Drawing.Point(12, 36)
		Me.lbPosition2.Name = "lbPosition2"
		Me.lbPosition2.Size = New System.Drawing.Size(47, 13)
		Me.lbPosition2.TabIndex = 5
		Me.lbPosition2.Text = "Position:"
		'
		'pictureBox2
		'
		Me.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pictureBox2.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox2.Name = "pictureBox2"
		Me.pictureBox2.Size = New System.Drawing.Size(100, 100)
		Me.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pictureBox2.TabIndex = 3
		Me.pictureBox2.TabStop = False
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.lbClass1)
		Me.Panel1.Controls.Add(Me.lbQuality1)
		Me.Panel1.Controls.Add(Me.lbPosition1)
		Me.Panel1.Controls.Add(Me.pictureBox1)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.Panel1.Location = New System.Drawing.Point(3, 3)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(100, 100)
		Me.Panel1.TabIndex = 0
		'
		'lbClass1
		'
		Me.lbClass1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbClass1.AutoSize = True
		Me.lbClass1.Location = New System.Drawing.Point(12, 62)
		Me.lbClass1.Name = "lbClass1"
		Me.lbClass1.Size = New System.Drawing.Size(35, 13)
		Me.lbClass1.TabIndex = 0
		Me.lbClass1.Text = "Class:"
		'
		'lbQuality1
		'
		Me.lbQuality1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbQuality1.AutoSize = True
		Me.lbQuality1.Location = New System.Drawing.Point(12, 49)
		Me.lbQuality1.Name = "lbQuality1"
		Me.lbQuality1.Size = New System.Drawing.Size(42, 13)
		Me.lbQuality1.TabIndex = 2
		Me.lbQuality1.Text = "Quality:"
		'
		'lbPosition1
		'
		Me.lbPosition1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lbPosition1.AutoSize = True
		Me.lbPosition1.Location = New System.Drawing.Point(12, 36)
		Me.lbPosition1.Name = "lbPosition1"
		Me.lbPosition1.Size = New System.Drawing.Size(47, 13)
		Me.lbPosition1.TabIndex = 1
		Me.lbPosition1.Text = "Position:"
		'
		'pictureBox1
		'
		Me.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
		Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pictureBox1.Location = New System.Drawing.Point(0, 0)
		Me.pictureBox1.Name = "pictureBox1"
		Me.pictureBox1.Size = New System.Drawing.Size(100, 100)
		Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pictureBox1.TabIndex = 0
		Me.pictureBox1.TabStop = False
		'
		'lblStatus
		'
		Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.lblStatus.AutoSize = True
		Me.lblStatus.Location = New System.Drawing.Point(105, 354)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Size = New System.Drawing.Size(0, 13)
		Me.lblStatus.TabIndex = 14
		'
		'nViewZoomSlider1
		'
		Me.nViewZoomSlider1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.nViewZoomSlider1.Location = New System.Drawing.Point(160, 349)
		Me.nViewZoomSlider1.Name = "nViewZoomSlider1"
		Me.nViewZoomSlider1.Size = New System.Drawing.Size(275, 23)
		Me.nViewZoomSlider1.TabIndex = 21
		Me.nViewZoomSlider1.Text = "nViewZoomSlider1"
		Me.nViewZoomSlider1.View = Me.originalFingerView
		'
		'LicensePanel1
		'
		Me.LicensePanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.LicensePanel1.Location = New System.Drawing.Point(3, 0)
		Me.LicensePanel1.Name = "LicensePanel1"
		Me.LicensePanel1.OptionalComponents = "Images.WSQ,Biometrics.FingerQualityAssessmentBase"
		Me.LicensePanel1.RequiredComponents = "Biometrics.FingerSegmentation"
		Me.LicensePanel1.Size = New System.Drawing.Size(435, 36)
		Me.LicensePanel1.TabIndex = 15
		'
		'SegmentFingerprints
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.nViewZoomSlider1)
		Me.Controls.Add(Me.LicensePanel1)
		Me.Controls.Add(Me.lblStatus)
		Me.Controls.Add(Me.SplitContainer1)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Controls.Add(Me.btnSaveImages)
		Me.Name = "SegmentFingerprints"
		Me.Size = New System.Drawing.Size(438, 377)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.Panel5.ResumeLayout(False)
		Me.SplitContainer1.Panel1.ResumeLayout(False)
		Me.SplitContainer1.Panel2.ResumeLayout(False)
		Me.SplitContainer1.ResumeLayout(False)
		Me.TableLayoutPanel2.ResumeLayout(False)
		Me.Panel4.ResumeLayout(False)
		Me.Panel4.PerformLayout()
		CType(Me.pictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel3.ResumeLayout(False)
		Me.Panel3.PerformLayout()
		CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private licensePanel As LicensePanel
	Private chlbMissing As System.Windows.Forms.CheckedListBox
	Private label1 As System.Windows.Forms.Label
	Private label2 As System.Windows.Forms.Label
	Private WithEvents btnSaveImages As System.Windows.Forms.Button
	Private WithEvents lbPosition As System.Windows.Forms.ListBox
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private folderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
	Private WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
	Friend WithEvents originalFingerView As Neurotec.Biometrics.Gui.NFingerView
	Private WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents Panel4 As System.Windows.Forms.Panel
	Friend WithEvents Panel3 As System.Windows.Forms.Panel
	Friend WithEvents Panel2 As System.Windows.Forms.Panel
	Friend WithEvents Panel1 As System.Windows.Forms.Panel
	Friend WithEvents lbPosition1 As System.Windows.Forms.Label
	Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
	Friend WithEvents lbClass4 As System.Windows.Forms.Label
	Private WithEvents lbQuality4 As System.Windows.Forms.Label
	Friend WithEvents lbPosition4 As System.Windows.Forms.Label
	Private WithEvents pictureBox4 As System.Windows.Forms.PictureBox
	Friend WithEvents lbClass3 As System.Windows.Forms.Label
	Private WithEvents lbQuality3 As System.Windows.Forms.Label
	Friend WithEvents lbPosition3 As System.Windows.Forms.Label
	Private WithEvents pictureBox3 As System.Windows.Forms.PictureBox
	Friend WithEvents lbClass2 As System.Windows.Forms.Label
	Private WithEvents lbQuality2 As System.Windows.Forms.Label
	Friend WithEvents lbPosition2 As System.Windows.Forms.Label
	Private WithEvents pictureBox2 As System.Windows.Forms.PictureBox
	Friend WithEvents lbClass1 As System.Windows.Forms.Label
	Private WithEvents lbQuality1 As System.Windows.Forms.Label
	Friend WithEvents LicensePanel1 As Neurotec.Samples.LicensePanel
	Private WithEvents lblStatus As System.Windows.Forms.Label
	Friend WithEvents Panel5 As System.Windows.Forms.Panel
	Friend WithEvents segmentButton As System.Windows.Forms.Button
	Private WithEvents openButton As System.Windows.Forms.Button
	Private WithEvents nViewZoomSlider1 As Neurotec.Gui.NViewZoomSlider
End Class
