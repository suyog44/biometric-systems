Imports Microsoft.VisualBasic
Imports System
Namespace Controls
	Partial Public Class InfoPanel
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
			Me.panelThumnail = New System.Windows.Forms.Panel
			Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
			Me.lblThumbnailKey = New System.Windows.Forms.Label
			Me.pictureBoxThumbnail = New System.Windows.Forms.PictureBox
			Me.btnOpen = New System.Windows.Forms.Button
			Me.btnCapture = New System.Windows.Forms.Button
			Me.tableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel
			Me.propertyGrid = New System.Windows.Forms.PropertyGrid
			Me.openFileDialog = New System.Windows.Forms.OpenFileDialog
			Me.panelThumnail.SuspendLayout()
			Me.tableLayoutPanel2.SuspendLayout()
			CType(Me.pictureBoxThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.tableLayoutPanelMain.SuspendLayout()
			Me.SuspendLayout()
			'
			'panelThumnail
			'
			Me.panelThumnail.Controls.Add(Me.tableLayoutPanel2)
			Me.panelThumnail.Dock = System.Windows.Forms.DockStyle.Fill
			Me.panelThumnail.Location = New System.Drawing.Point(3, 3)
			Me.panelThumnail.Name = "panelThumnail"
			Me.panelThumnail.Size = New System.Drawing.Size(281, 233)
			Me.panelThumnail.TabIndex = 1
			'
			'tableLayoutPanel2
			'
			Me.tableLayoutPanel2.ColumnCount = 3
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
			Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
			Me.tableLayoutPanel2.Controls.Add(Me.lblThumbnailKey, 0, 0)
			Me.tableLayoutPanel2.Controls.Add(Me.pictureBoxThumbnail, 0, 1)
			Me.tableLayoutPanel2.Controls.Add(Me.btnOpen, 2, 0)
			Me.tableLayoutPanel2.Controls.Add(Me.btnCapture, 1, 0)
			Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
			Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
			Me.tableLayoutPanel2.RowCount = 2
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
			Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
			Me.tableLayoutPanel2.Size = New System.Drawing.Size(281, 233)
			Me.tableLayoutPanel2.TabIndex = 1
			'
			'lblThumbnailKey
			'
			Me.lblThumbnailKey.AutoSize = True
			Me.lblThumbnailKey.Dock = System.Windows.Forms.DockStyle.Fill
			Me.lblThumbnailKey.Location = New System.Drawing.Point(3, 0)
			Me.lblThumbnailKey.Name = "lblThumbnailKey"
			Me.lblThumbnailKey.Size = New System.Drawing.Size(93, 32)
			Me.lblThumbnailKey.TabIndex = 0
			Me.lblThumbnailKey.Text = "ThumbnailKey"
			Me.lblThumbnailKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
			'
			'pictureBoxThumbnail
			'
			Me.pictureBoxThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
			Me.tableLayoutPanel2.SetColumnSpan(Me.pictureBoxThumbnail, 3)
			Me.pictureBoxThumbnail.Dock = System.Windows.Forms.DockStyle.Fill
			Me.pictureBoxThumbnail.Location = New System.Drawing.Point(3, 35)
			Me.pictureBoxThumbnail.Name = "pictureBoxThumbnail"
			Me.pictureBoxThumbnail.Size = New System.Drawing.Size(275, 195)
			Me.pictureBoxThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
			Me.pictureBoxThumbnail.TabIndex = 2
			Me.pictureBoxThumbnail.TabStop = False
			'
			'btnOpen
			'
			Me.btnOpen.Location = New System.Drawing.Point(193, 3)
			Me.btnOpen.Name = "btnOpen"
			Me.btnOpen.Size = New System.Drawing.Size(85, 26)
			Me.btnOpen.TabIndex = 1
			Me.btnOpen.Text = "Open image"
			Me.btnOpen.UseVisualStyleBackColor = True
			'
			'btnCapture
			'
			Me.btnCapture.Location = New System.Drawing.Point(102, 3)
			Me.btnCapture.Name = "btnCapture"
			Me.btnCapture.Size = New System.Drawing.Size(85, 26)
			Me.btnCapture.TabIndex = 3
			Me.btnCapture.Text = "Capture"
			Me.btnCapture.UseVisualStyleBackColor = True
			'
			'tableLayoutPanelMain
			'
			Me.tableLayoutPanelMain.ColumnCount = 2
			Me.tableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.0!))
			Me.tableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.0!))
			Me.tableLayoutPanelMain.Controls.Add(Me.panelThumnail, 0, 0)
			Me.tableLayoutPanelMain.Controls.Add(Me.propertyGrid, 1, 0)
			Me.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill
			Me.tableLayoutPanelMain.Location = New System.Drawing.Point(0, 0)
			Me.tableLayoutPanelMain.Name = "tableLayoutPanelMain"
			Me.tableLayoutPanelMain.RowCount = 1
			Me.tableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
			Me.tableLayoutPanelMain.Size = New System.Drawing.Size(654, 239)
			Me.tableLayoutPanelMain.TabIndex = 2
			'
			'propertyGrid
			'
			Me.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill
			Me.propertyGrid.HelpVisible = False
			Me.propertyGrid.Location = New System.Drawing.Point(290, 3)
			Me.propertyGrid.Name = "propertyGrid"
			Me.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized
			Me.propertyGrid.Size = New System.Drawing.Size(361, 233)
			Me.propertyGrid.TabIndex = 2
			Me.propertyGrid.ToolbarVisible = False
			'
			'InfoPanel
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.tableLayoutPanelMain)
			Me.Name = "InfoPanel"
			Me.Size = New System.Drawing.Size(654, 239)
			Me.panelThumnail.ResumeLayout(False)
			Me.tableLayoutPanel2.ResumeLayout(False)
			Me.tableLayoutPanel2.PerformLayout()
			CType(Me.pictureBoxThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
			Me.tableLayoutPanelMain.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private panelThumnail As System.Windows.Forms.Panel
		Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
		Private lblThumbnailKey As System.Windows.Forms.Label
		Private WithEvents btnOpen As System.Windows.Forms.Button
		Private pictureBoxThumbnail As System.Windows.Forms.PictureBox
		Private tableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
		Private propertyGrid As System.Windows.Forms.PropertyGrid
		Private openFileDialog As System.Windows.Forms.OpenFileDialog
		Private WithEvents btnCapture As System.Windows.Forms.Button
	End Class
End Namespace
