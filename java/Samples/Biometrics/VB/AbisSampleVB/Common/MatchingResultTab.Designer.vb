Imports Microsoft.VisualBasic
Imports System
Partial Public Class MatchingResultTab
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
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
		Me.propertyGrid = New System.Windows.Forms.PropertyGrid
		Me.lblInfo = New System.Windows.Forms.Label
		Me.pbThumbnail = New System.Windows.Forms.PictureBox
		Me.lblStatus = New System.Windows.Forms.Label
		Me.lblGalerySubject = New System.Windows.Forms.Label
		Me.panelProbeView = New System.Windows.Forms.Panel
		Me.panelGaleryView = New System.Windows.Forms.Panel
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.lblProbeInfo = New System.Windows.Forms.Label
		Me.lblGaleryInfo = New System.Windows.Forms.Label
		Me.tlpSelection = New System.Windows.Forms.TableLayoutPanel
		Me.label1 = New System.Windows.Forms.Label
		Me.cbMatched = New System.Windows.Forms.ComboBox
		Me.lblProbeSubject = New System.Windows.Forms.Label
		Me.tableLayoutPanel1.SuspendLayout()
		Me.TableLayoutPanel3.SuspendLayout()
		CType(Me.pbThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tlpSelection.SuspendLayout()
		Me.SuspendLayout()
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 2
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.lblGalerySubject, 1, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.panelProbeView, 0, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.panelGaleryView, 1, 4)
		Me.tableLayoutPanel1.Controls.Add(Me.tableLayoutPanel2, 0, 2)
		Me.tableLayoutPanel1.Controls.Add(Me.lblProbeInfo, 0, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.lblGaleryInfo, 1, 5)
		Me.tableLayoutPanel1.Controls.Add(Me.tlpSelection, 0, 6)
		Me.tableLayoutPanel1.Controls.Add(Me.lblProbeSubject, 0, 3)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 7
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(659, 457)
		Me.tableLayoutPanel1.TabIndex = 0
		'
		'TableLayoutPanel3
		'
		Me.TableLayoutPanel3.AutoSize = True
		Me.TableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
		Me.TableLayoutPanel3.ColumnCount = 3
		Me.tableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel3, 2)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Controls.Add(Me.propertyGrid, 2, 1)
		Me.TableLayoutPanel3.Controls.Add(Me.lblInfo, 1, 1)
		Me.TableLayoutPanel3.Controls.Add(Me.pbThumbnail, 0, 1)
		Me.TableLayoutPanel3.Controls.Add(Me.lblStatus, 0, 0)
		Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 3)
		Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
		Me.TableLayoutPanel3.RowCount = 2
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.TableLayoutPanel3.Size = New System.Drawing.Size(653, 173)
		Me.TableLayoutPanel3.TabIndex = 1
		'
		'propertyGrid
		'
		Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
		Me.propertyGrid.HelpVisible = False
		Me.propertyGrid.Location = New System.Drawing.Point(301, 27)
		Me.propertyGrid.Name = "propertyGrid"
		Me.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort
		Me.propertyGrid.Size = New System.Drawing.Size(348, 142)
		Me.propertyGrid.TabIndex = 2
		Me.propertyGrid.ToolbarVisible = False
		Me.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Control
		'
		'lblInfo
		'
		Me.lblInfo.AutoSize = True
		Me.lblInfo.BackColor = System.Drawing.SystemColors.Control
		Me.lblInfo.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblInfo.Location = New System.Drawing.Point(178, 24)
		Me.lblInfo.MaximumSize = New System.Drawing.Size(400, 0)
		Me.lblInfo.Name = "lblInfo"
		Me.lblInfo.Padding = New System.Windows.Forms.Padding(20, 20, 0, 20)
		Me.lblInfo.Size = New System.Drawing.Size(116, 148)
		Me.lblInfo.TabIndex = 7
		Me.lblInfo.Text = "Subject: '{0}'" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Score: {1}"
		'
		'pbThumbnail
		'
		Me.pbThumbnail.Dock = System.Windows.Forms.DockStyle.Fill
		Me.pbThumbnail.Location = New System.Drawing.Point(4, 27)
		Me.pbThumbnail.Name = "pbThumbnail"
		Me.pbThumbnail.Size = New System.Drawing.Size(167, 142)
		Me.pbThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
		Me.pbThumbnail.TabIndex = 6
		Me.pbThumbnail.TabStop = False
		'
		'lblStatus
		'
		Me.lblStatus.AutoSize = True
		Me.lblStatus.BackColor = System.Drawing.Color.Orange
		Me.TableLayoutPanel3.SetColumnSpan(Me.lblStatus, 3)
		Me.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill
		Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblStatus.ForeColor = System.Drawing.Color.White
		Me.lblStatus.Location = New System.Drawing.Point(4, 1)
		Me.lblStatus.Name = "lblStatus"
		Me.lblStatus.Padding = New System.Windows.Forms.Padding(0, 3, 0, 3)
		Me.lblStatus.Size = New System.Drawing.Size(645, 22)
		Me.lblStatus.TabIndex = 5
		Me.lblStatus.Text = "Operation in progress. Please wait ..."
		Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'lblGalerySubject
		'
		Me.lblGalerySubject.AutoSize = True
		Me.lblGalerySubject.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblGalerySubject.Location = New System.Drawing.Point(332, 185)
		Me.lblGalerySubject.Name = "lblGalerySubject"
		Me.lblGalerySubject.Size = New System.Drawing.Size(112, 16)
		Me.lblGalerySubject.TabIndex = 15
		Me.lblGalerySubject.Text = "Galery subject:"
		'
		'panelProbeView
		'
		Me.panelProbeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.panelProbeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelProbeView.Location = New System.Drawing.Point(3, 204)
		Me.panelProbeView.Name = "panelProbeView"
		Me.panelProbeView.Size = New System.Drawing.Size(323, 202)
		Me.panelProbeView.TabIndex = 8
		'
		'panelGaleryView
		'
		Me.panelGaleryView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.panelGaleryView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.panelGaleryView.Location = New System.Drawing.Point(332, 204)
		Me.panelGaleryView.Name = "panelGaleryView"
		Me.panelGaleryView.Size = New System.Drawing.Size(324, 202)
		Me.panelGaleryView.TabIndex = 9
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.AutoSize = True
		Me.tableLayoutPanel2.ColumnCount = 2
		Me.tableLayoutPanel1.SetColumnSpan(Me.tableLayoutPanel2, 2)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 182)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 1
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(653, 1)
		Me.tableLayoutPanel2.TabIndex = 12
		'
		'lblProbeInfo
		'
		Me.lblProbeInfo.AutoSize = True
		Me.lblProbeInfo.Location = New System.Drawing.Point(3, 409)
		Me.lblProbeInfo.Name = "lblProbeInfo"
		Me.lblProbeInfo.Size = New System.Drawing.Size(55, 13)
		Me.lblProbeInfo.TabIndex = 10
		Me.lblProbeInfo.Text = "Probe info"
		'
		'lblGaleryInfo
		'
		Me.lblGaleryInfo.AutoSize = True
		Me.lblGaleryInfo.Location = New System.Drawing.Point(332, 409)
		Me.lblGaleryInfo.Name = "lblGaleryInfo"
		Me.lblGaleryInfo.Size = New System.Drawing.Size(57, 13)
		Me.lblGaleryInfo.TabIndex = 11
		Me.lblGaleryInfo.Text = "Galery info"
		'
		'tlpSelection
		'
		Me.tlpSelection.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
					Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlpSelection.ColumnCount = 4
		Me.tableLayoutPanel1.SetColumnSpan(Me.tlpSelection, 2)
		Me.tlpSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tlpSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tlpSelection.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpSelection.Controls.Add(Me.label1, 0, 0)
		Me.tlpSelection.Controls.Add(Me.cbMatched, 1, 0)
		Me.tlpSelection.Location = New System.Drawing.Point(3, 425)
		Me.tlpSelection.Name = "tlpSelection"
		Me.tlpSelection.RowCount = 1
		Me.tlpSelection.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tlpSelection.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29.0!))
		Me.tlpSelection.Size = New System.Drawing.Size(653, 29)
		Me.tlpSelection.TabIndex = 13
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(102, 29)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Matched biometrics:"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'cbMatched
		'
		Me.tlpSelection.SetColumnSpan(Me.cbMatched, 3)
		Me.cbMatched.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cbMatched.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbMatched.FormattingEnabled = True
		Me.cbMatched.Location = New System.Drawing.Point(111, 3)
		Me.cbMatched.Name = "cbMatched"
		Me.cbMatched.Size = New System.Drawing.Size(539, 21)
		Me.cbMatched.TabIndex = 1
		'
		'lblProbeSubject
		'
		Me.lblProbeSubject.AutoSize = True
		Me.lblProbeSubject.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblProbeSubject.Location = New System.Drawing.Point(3, 185)
		Me.lblProbeSubject.Name = "lblProbeSubject"
		Me.lblProbeSubject.Size = New System.Drawing.Size(108, 16)
		Me.lblProbeSubject.TabIndex = 14
		Me.lblProbeSubject.Text = "Probe subject:"
		'
		'MatchingResultTab
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Name = "MatchingResultTab"
		Me.Size = New System.Drawing.Size(659, 457)
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.TableLayoutPanel3.ResumeLayout(False)
		Me.TableLayoutPanel3.PerformLayout()
		CType(Me.pbThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tlpSelection.ResumeLayout(False)
		Me.tlpSelection.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private lblStatus As System.Windows.Forms.Label
	Private pbThumbnail As System.Windows.Forms.PictureBox
	Private lblInfo As System.Windows.Forms.Label
	Private panelGaleryView As System.Windows.Forms.Panel
	Private lblProbeInfo As System.Windows.Forms.Label
	Private lblGaleryInfo As System.Windows.Forms.Label
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private tlpSelection As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private WithEvents cbMatched As System.Windows.Forms.ComboBox
	Private lblGalerySubject As System.Windows.Forms.Label
	Private lblProbeSubject As System.Windows.Forms.Label
	Private panelProbeView As System.Windows.Forms.Panel
	Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
	Friend WithEvents propertyGrid As System.Windows.Forms.PropertyGrid
End Class
