Imports Microsoft.VisualBasic
Imports System
Partial Public Class TenPrintCardForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(TenPrintCardForm))
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.tsbOpenFile = New System.Windows.Forms.ToolStripButton()
		Me.tbsScanDefault = New System.Windows.Forms.ToolStripSplitButton()
		Me.tsbScan = New System.Windows.Forms.ToolStripMenuItem()
		Me.selectDeviceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
		Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
		Me.tsbOK = New System.Windows.Forms.ToolStripButton()
		Me.openFileDialog1 = New System.Windows.Forms.OpenFileDialog()
		Me.toolStrip1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' toolStrip1
		' 
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.tsbOpenFile, Me.tbsScanDefault, Me.toolStripSeparator1, Me.tsbOK})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(814, 25)
		Me.toolStrip1.TabIndex = 0
		Me.toolStrip1.Text = "toolStrip"
		' 
		' tsbOpenFile
		' 
		Me.tsbOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tsbOpenFile.Image = (CType(resources.GetObject("tsbOpenFile.Image"), System.Drawing.Image))
		Me.tsbOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenFile.Name = "tsbOpenFile"
		Me.tsbOpenFile.Size = New System.Drawing.Size(23, 22)
		Me.tsbOpenFile.Text = "Open image from file"
'		Me.tsbOpenFile.Click += New System.EventHandler(Me.TsbOpenFileClick);
		' 
		' tbsScanDefault
		' 
		Me.tbsScanDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
		Me.tbsScanDefault.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.tsbScan, Me.selectDeviceToolStripMenuItem})
		Me.tbsScanDefault.Image = (CType(resources.GetObject("tbsScanDefault.Image"), System.Drawing.Image))
		Me.tbsScanDefault.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tbsScanDefault.Name = "tbsScanDefault"
		Me.tbsScanDefault.Size = New System.Drawing.Size(32, 22)
		Me.tbsScanDefault.Text = "Acquire image from scanner"
'		Me.tbsScanDefault.ButtonClick += New System.EventHandler(Me.TbsScanDefaultButtonClick);
		' 
		' tsbScan
		' 
		Me.tsbScan.Name = "tsbScan"
		Me.tsbScan.Size = New System.Drawing.Size(151, 22)
		Me.tsbScan.Text = "Scan..."
'		Me.tsbScan.Click += New System.EventHandler(Me.TsbScanClick);
		' 
		' selectDeviceToolStripMenuItem
		' 
		Me.selectDeviceToolStripMenuItem.Name = "selectDeviceToolStripMenuItem"
		Me.selectDeviceToolStripMenuItem.Size = New System.Drawing.Size(151, 22)
		Me.selectDeviceToolStripMenuItem.Text = "Select device..."
'		Me.selectDeviceToolStripMenuItem.Click += New System.EventHandler(Me.SelectDeviceToolStripMenuItemClick);
		' 
		' toolStripSeparator1
		' 
		Me.toolStripSeparator1.Name = "toolStripSeparator1"
		Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
		' 
		' tsbOK
		' 
		Me.tsbOK.Enabled = False
		Me.tsbOK.Image = (CType(resources.GetObject("tsbOK.Image"), System.Drawing.Image))
		Me.tsbOK.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOK.Name = "tsbOK"
		Me.tsbOK.Size = New System.Drawing.Size(113, 22)
		Me.tsbOK.Text = "Add fingerprints"
'		Me.tsbOK.Click += New System.EventHandler(Me.TsbOKClick);
		' 
		' openFileDialog1
		' 
		Me.openFileDialog1.Filter = resources.GetString("openFileDialog1.Filter")
		Me.openFileDialog1.RestoreDirectory = True
		' 
		' TenPrintCardForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.AppWorkspace
		Me.ClientSize = New System.Drawing.Size(814, 714)
		Me.Controls.Add(Me.toolStrip1)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.KeyPreview = True
		Me.Name = "TenPrintCardForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "TenPrint Card"
'		Me.Load += New System.EventHandler(Me.TenPrintCardFormLoad);
'		Me.KeyDown += New System.Windows.Forms.KeyEventHandler(Me.TenPrintCardFormKeyDown);
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private WithEvents tsbOpenFile As System.Windows.Forms.ToolStripButton
	Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Private WithEvents tsbOK As System.Windows.Forms.ToolStripButton
	Private openFileDialog1 As System.Windows.Forms.OpenFileDialog
	Private WithEvents tbsScanDefault As System.Windows.Forms.ToolStripSplitButton
	Private WithEvents tsbScan As System.Windows.Forms.ToolStripMenuItem
	Private WithEvents selectDeviceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
