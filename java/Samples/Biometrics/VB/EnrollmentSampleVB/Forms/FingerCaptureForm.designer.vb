Imports Microsoft.VisualBasic
Imports System
Imports Neurotec.Samples.Controls

Namespace Forms
	Partial Public Class FingerCaptureForm
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FingerCaptureForm))
			Me.splitContainer1 = New System.Windows.Forms.SplitContainer
			Me.groupBox1 = New System.Windows.Forms.GroupBox
			Me.lvQueue = New System.Windows.Forms.ListView
			Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
			Me.btnAccept = New System.Windows.Forms.Button
			Me.btnRescan = New System.Windows.Forms.Button
			Me.btnNext = New System.Windows.Forms.Button
			Me.btnPrevious = New System.Windows.Forms.Button
			Me.fSelector = New Neurotec.Samples.Controls.FingerSelector
			Me.tableLayoutPanelCenter = New System.Windows.Forms.TableLayoutPanel
			Me.lblInfo = New System.Windows.Forms.Label
			Me.fingerView = New Neurotec.Biometrics.Gui.NFingerView
			Me.lblStatus = New System.Windows.Forms.Label
			Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
			Me.splitContainer1.Panel1.SuspendLayout()
			Me.splitContainer1.Panel2.SuspendLayout()
			Me.splitContainer1.SuspendLayout()
			Me.groupBox1.SuspendLayout()
			Me.tableLayoutPanelCenter.SuspendLayout()
			Me.SuspendLayout()
			'
			'splitContainer1
			'
			Me.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
			Me.splitContainer1.IsSplitterFixed = True
			Me.splitContainer1.Location = New System.Drawing.Point(0, 0)
			Me.splitContainer1.Name = "splitContainer1"
			'
			'splitContainer1.Panel1
			'
			Me.splitContainer1.Panel1.Controls.Add(Me.groupBox1)
			Me.splitContainer1.Panel1.Controls.Add(Me.btnAccept)
			Me.splitContainer1.Panel1.Controls.Add(Me.btnRescan)
			Me.splitContainer1.Panel1.Controls.Add(Me.btnNext)
			Me.splitContainer1.Panel1.Controls.Add(Me.btnPrevious)
			Me.splitContainer1.Panel1.Controls.Add(Me.fSelector)
			'
			'splitContainer1.Panel2
			'
			Me.splitContainer1.Panel2.Controls.Add(Me.tableLayoutPanelCenter)
			Me.splitContainer1.Size = New System.Drawing.Size(901, 496)
			Me.splitContainer1.SplitterDistance = 220
			Me.splitContainer1.TabIndex = 3
			'
			'groupBox1
			'
			Me.groupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.groupBox1.Controls.Add(Me.lvQueue)
			Me.groupBox1.Location = New System.Drawing.Point(3, 157)
			Me.groupBox1.Name = "groupBox1"
			Me.groupBox1.Size = New System.Drawing.Size(212, 334)
			Me.groupBox1.TabIndex = 7
			Me.groupBox1.TabStop = False
			Me.groupBox1.Text = "Scan queue"
			'
			'lvQueue
			'
			Me.lvQueue.Activation = System.Windows.Forms.ItemActivation.OneClick
			Me.lvQueue.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1})
			Me.lvQueue.Dock = System.Windows.Forms.DockStyle.Fill
			Me.lvQueue.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.lvQueue.FullRowSelect = True
			Me.lvQueue.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
			Me.lvQueue.HideSelection = False
			Me.lvQueue.Location = New System.Drawing.Point(3, 16)
			Me.lvQueue.MultiSelect = False
			Me.lvQueue.Name = "lvQueue"
			Me.lvQueue.Size = New System.Drawing.Size(206, 315)
			Me.lvQueue.TabIndex = 0
			Me.lvQueue.UseCompatibleStateImageBehavior = False
			Me.lvQueue.View = System.Windows.Forms.View.Details
			'
			'columnHeader1
			'
			Me.columnHeader1.Width = 185
			'
			'btnAccept
			'
			Me.btnAccept.Enabled = False
			Me.btnAccept.Image = Global.Neurotec.Samples.My.Resources.Resources.Ok
			Me.btnAccept.Location = New System.Drawing.Point(153, 111)
			Me.btnAccept.Name = "btnAccept"
			Me.btnAccept.Size = New System.Drawing.Size(50, 40)
			Me.btnAccept.TabIndex = 5
			Me.toolTip.SetToolTip(Me.btnAccept, "Accept image and extract recors")
			Me.btnAccept.UseVisualStyleBackColor = True
			'
			'btnRescan
			'
			Me.btnRescan.Enabled = False
			Me.btnRescan.Image = Global.Neurotec.Samples.My.Resources.Resources.Repeat
			Me.btnRescan.Location = New System.Drawing.Point(103, 111)
			Me.btnRescan.Name = "btnRescan"
			Me.btnRescan.Size = New System.Drawing.Size(50, 40)
			Me.btnRescan.TabIndex = 4
			Me.toolTip.SetToolTip(Me.btnRescan, "Repeat")
			Me.btnRescan.UseVisualStyleBackColor = True
			'
			'btnNext
			'
			Me.btnNext.Enabled = False
			Me.btnNext.Image = Global.Neurotec.Samples.My.Resources.Resources.GoToNext
			Me.btnNext.Location = New System.Drawing.Point(53, 111)
			Me.btnNext.Name = "btnNext"
			Me.btnNext.Size = New System.Drawing.Size(50, 40)
			Me.btnNext.TabIndex = 3
			Me.toolTip.SetToolTip(Me.btnNext, "Next")
			Me.btnNext.UseVisualStyleBackColor = True
			'
			'btnPrevious
			'
			Me.btnPrevious.Enabled = False
			Me.btnPrevious.Image = Global.Neurotec.Samples.My.Resources.Resources.GoToPrevious
			Me.btnPrevious.Location = New System.Drawing.Point(3, 111)
			Me.btnPrevious.Name = "btnPrevious"
			Me.btnPrevious.Size = New System.Drawing.Size(50, 40)
			Me.btnPrevious.TabIndex = 2
			Me.toolTip.SetToolTip(Me.btnPrevious, "Back")
			Me.btnPrevious.UseVisualStyleBackColor = True
			'
			'fSelector
			'
			Me.fSelector.AllowHighlight = False
			Me.fSelector.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.fSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.fSelector.IsRolled = False
			Me.fSelector.Location = New System.Drawing.Point(3, 3)
			Me.fSelector.MissingPositions = New Neurotec.Biometrics.NFPosition(-1) {}
			Me.fSelector.Name = "fSelector"
			Me.fSelector.SelectedPosition = Neurotec.Biometrics.NFPosition.Unknown
			Me.fSelector.Size = New System.Drawing.Size(212, 102)
			Me.fSelector.TabIndex = 1
			'
			'tableLayoutPanelCenter
			'
			Me.tableLayoutPanelCenter.ColumnCount = 1
			Me.tableLayoutPanelCenter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
			Me.tableLayoutPanelCenter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanelCenter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanelCenter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanelCenter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanelCenter.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
			Me.tableLayoutPanelCenter.Controls.Add(Me.lblInfo, 0, 0)
			Me.tableLayoutPanelCenter.Controls.Add(Me.fingerView, 0, 1)
			Me.tableLayoutPanelCenter.Controls.Add(Me.lblStatus, 0, 2)
			Me.tableLayoutPanelCenter.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanelCenter.Location = New System.Drawing.Point(0, 0)
			Me.tableLayoutPanelCenter.Name = "tableLayoutPanelCenter"
			Me.tableLayoutPanelCenter.RowCount = 4
			Me.tableLayoutPanelCenter.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanelCenter.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
			Me.tableLayoutPanelCenter.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanelCenter.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanelCenter.Size = New System.Drawing.Size(675, 494)
			Me.tableLayoutPanelCenter.TabIndex = 7
			'
			'lblInfo
			'
			Me.lblInfo.AutoSize = True
			Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill
			Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.lblInfo.Location = New System.Drawing.Point(0, 0)
			Me.lblInfo.Margin = New System.Windows.Forms.Padding(0)
			Me.lblInfo.Name = "lblInfo"
			Me.lblInfo.Padding = New System.Windows.Forms.Padding(0, 5, 0, 5)
			Me.lblInfo.Size = New System.Drawing.Size(675, 23)
			Me.lblInfo.TabIndex = 6
			Me.lblInfo.Text = "Info"
			Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
			'
			'fingerView
			'
			Me.fingerView.BackColor = System.Drawing.SystemColors.Control
			Me.fingerView.BoundingRectColor = System.Drawing.Color.Red
			Me.fingerView.Dock = System.Windows.Forms.DockStyle.Fill
			Me.fingerView.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.fingerView.Location = New System.Drawing.Point(3, 26)
			Me.fingerView.MinutiaColor = System.Drawing.Color.Red
			Me.fingerView.Name = "fingerView"
			Me.fingerView.NeighborMinutiaColor = System.Drawing.Color.Orange
			Me.fingerView.ResultImageColor = System.Drawing.Color.Green
			Me.fingerView.SelectedMinutiaColor = System.Drawing.Color.Magenta
			Me.fingerView.SelectedSingularPointColor = System.Drawing.Color.Magenta
			Me.fingerView.SingularPointColor = System.Drawing.Color.Red
			Me.fingerView.Size = New System.Drawing.Size(669, 444)
			Me.fingerView.TabIndex = 5
			Me.fingerView.TreeColor = System.Drawing.Color.Crimson
			Me.fingerView.TreeMinutiaNumberDiplayFormat = Neurotec.Biometrics.Gui.MinutiaNumberDiplayFormat.DontDisplay
			Me.fingerView.TreeMinutiaNumberFont = New System.Drawing.Font("Arial", 10.0!)
			Me.fingerView.TreeWidth = 2
			'
			'lblStatus
			'
			Me.lblStatus.AutoSize = True
			Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
			Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
			Me.lblStatus.Location = New System.Drawing.Point(3, 473)
			Me.lblStatus.Margin = New System.Windows.Forms.Padding(3, 0, 3, 5)
			Me.lblStatus.Name = "lblStatus"
			Me.lblStatus.Size = New System.Drawing.Size(669, 16)
			Me.lblStatus.TabIndex = 6
			Me.lblStatus.Text = "Status: starting capture ..."
			Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
			Me.lblStatus.Visible = False
			'
			'FingerCaptureForm
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(901, 496)
			Me.Controls.Add(Me.splitContainer1)
			Me.DoubleBuffered = True
			Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
			Me.MinimumSize = New System.Drawing.Size(250, 250)
			Me.Name = "FingerCaptureForm"
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Capturing ..."
			Me.splitContainer1.Panel1.ResumeLayout(False)
			Me.splitContainer1.Panel2.ResumeLayout(False)
			Me.splitContainer1.ResumeLayout(False)
			Me.groupBox1.ResumeLayout(False)
			Me.tableLayoutPanelCenter.ResumeLayout(False)
			Me.tableLayoutPanelCenter.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private fSelector As FingerSelector
		Private splitContainer1 As System.Windows.Forms.SplitContainer
		Private WithEvents fingerView As Neurotec.Biometrics.Gui.NFingerView
		Private WithEvents btnRescan As System.Windows.Forms.Button
		Private WithEvents btnNext As System.Windows.Forms.Button
		Private WithEvents btnPrevious As System.Windows.Forms.Button
		Private WithEvents btnAccept As System.Windows.Forms.Button
		Private lblInfo As System.Windows.Forms.Label
		Private toolTip As System.Windows.Forms.ToolTip
		Private tableLayoutPanelCenter As System.Windows.Forms.TableLayoutPanel
		Private groupBox1 As System.Windows.Forms.GroupBox
		Private lblStatus As System.Windows.Forms.Label
		Private WithEvents lvQueue As System.Windows.Forms.ListView
		Private columnHeader1 As System.Windows.Forms.ColumnHeader
	End Class
End Namespace
