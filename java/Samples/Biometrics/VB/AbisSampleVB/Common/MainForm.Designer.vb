Partial Public Class MainForm
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
		Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.tsbNewSubject = New System.Windows.Forms.ToolStripButton()
		Me.tsbOpenSubject = New System.Windows.Forms.ToolStripButton()
		Me.tsbGetSubject = New System.Windows.Forms.ToolStripButton()
		Me.tsbSettings = New System.Windows.Forms.ToolStripButton()
		Me.tbsChangeDatabase = New System.Windows.Forms.ToolStripButton()
		Me.tsbAbout = New System.Windows.Forms.ToolStripButton()
		Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
		Me.tabControl = New Neurotec.Samples.CloseableTabControl()
		Me.toolStrip1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' toolStrip1
		' 
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbNewSubject, Me.tsbOpenSubject, Me.tsbGetSubject, Me.tsbSettings, Me.tbsChangeDatabase, Me.tsbAbout})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(1008, 25)
		Me.toolStrip1.TabIndex = 0
		Me.toolStrip1.Text = "toolStrip1"
		' 
		' tsbNewSubject
		' 
		Me.tsbNewSubject.Image = Global.Neurotec.Samples.My.Resources.NewDocumentHS
		Me.tsbNewSubject.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbNewSubject.Name = "tsbNewSubject"
		Me.tsbNewSubject.Size = New System.Drawing.Size(93, 22)
		Me.tsbNewSubject.Text = "&New Subject"
		' 
		' tsbOpenSubject
		' 
		Me.tsbOpenSubject.Image = Global.Neurotec.Samples.My.Resources.openfolderHS
		Me.tsbOpenSubject.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbOpenSubject.Name = "tsbOpenSubject"
		Me.tsbOpenSubject.Size = New System.Drawing.Size(98, 22)
		Me.tsbOpenSubject.Text = "&Open Subject"
		' 
		' tsbGetSubject
		' 
		Me.tsbGetSubject.Image = Global.Neurotec.Samples.My.Resources._Get
		Me.tsbGetSubject.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbGetSubject.Name = "tsbGetSubject"
		Me.tsbGetSubject.Size = New System.Drawing.Size(87, 22)
		Me.tsbGetSubject.Text = "&Get Subject"
		' 
		' tsbSettings
		' 
		Me.tsbSettings.Image = Global.Neurotec.Samples.My.Resources.settings
		Me.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbSettings.Name = "tsbSettings"
		Me.tsbSettings.Size = New System.Drawing.Size(69, 22)
		Me.tsbSettings.Text = "&Settings"
		' 
		' tbsChangeDatabase
		' 
		Me.tbsChangeDatabase.Image = Global.Neurotec.Samples.My.Resources.newSource
		Me.tbsChangeDatabase.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tbsChangeDatabase.Name = "tbsChangeDatabase"
		Me.tbsChangeDatabase.Size = New System.Drawing.Size(118, 22)
		Me.tbsChangeDatabase.Text = "&Change database"
		' 
		' tsbAbout
		' 
		Me.tsbAbout.Image = Global.Neurotec.Samples.My.Resources.Help
		Me.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbAbout.Name = "tsbAbout"
		Me.tsbAbout.Size = New System.Drawing.Size(60, 22)
		Me.tsbAbout.Text = "&About"
		' 
		' tabControl
		' 
		Me.tabControl.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
		Me.tabControl.LastPageIndex = -1
		Me.tabControl.Location = New System.Drawing.Point(0, 25)
		Me.tabControl.Name = "tabControl"
		Me.tabControl.Padding = New System.Drawing.Point(16, 3)
		Me.tabControl.SelectedIndex = 0
		Me.tabControl.Size = New System.Drawing.Size(1008, 704)
		Me.tabControl.TabIndex = 1
		' 
		' MainForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
		Me.ClientSize = New System.Drawing.Size(1008, 729)
		Me.Controls.Add(Me.tabControl)
		Me.Controls.Add(Me.toolStrip1)
		Me.Icon = (CType(resources.GetObject("$this.Icon"), System.Drawing.Icon))
		Me.MinimumSize = New System.Drawing.Size(550, 310)
		Me.Name = "MainForm"
		Me.Text = "Multibiometric Sample"
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

#End Region

	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private WithEvents tsbNewSubject As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbOpenSubject As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbSettings As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbAbout As System.Windows.Forms.ToolStripButton
	Private tabControl As Neurotec.Samples.CloseableTabControl
	Private openFileDialog As System.Windows.Forms.OpenFileDialog
	Private WithEvents tbsChangeDatabase As System.Windows.Forms.ToolStripButton
	Private WithEvents tsbGetSubject As System.Windows.Forms.ToolStripButton




End Class