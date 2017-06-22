Imports Microsoft.VisualBasic
Imports System
Partial Public Class SubjectTreeControl
	''' <summary> 
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	#Region "Component Designer generated code"

	''' <summary> 
	''' Required method for Designer support - do not modify 
	''' the contents of this method with the code editor.
	''' </summary>
	Private Sub InitializeComponent()
		Me.treeView = New System.Windows.Forms.TreeView
		Me.tableLayoutPanel = New System.Windows.Forms.TableLayoutPanel
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip
		Me.tsbRemove = New System.Windows.Forms.ToolStripButton
		Me.tableLayoutPanel.SuspendLayout()
		Me.toolStrip1.SuspendLayout()
		Me.SuspendLayout()
		'
		'treeView
		'
		Me.treeView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.treeView.FullRowSelect = True
		Me.treeView.HideSelection = False
		Me.treeView.Location = New System.Drawing.Point(3, 28)
		Me.treeView.Name = "treeView"
		Me.treeView.ShowLines = False
		Me.treeView.ShowPlusMinus = False
		Me.treeView.Size = New System.Drawing.Size(441, 408)
		Me.treeView.TabIndex = 0
		'
		'tableLayoutPanel
		'
		Me.tableLayoutPanel.ColumnCount = 1
		Me.tableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tableLayoutPanel.Controls.Add(Me.treeView, 0, 1)
		Me.tableLayoutPanel.Controls.Add(Me.toolStrip1, 0, 0)
		Me.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel.Name = "tableLayoutPanel"
		Me.tableLayoutPanel.RowCount = 2
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel.Size = New System.Drawing.Size(447, 439)
		Me.tableLayoutPanel.TabIndex = 1
		'
		'toolStrip1
		'
		Me.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbRemove})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
		Me.toolStrip1.Size = New System.Drawing.Size(447, 25)
		Me.toolStrip1.TabIndex = 1
		Me.toolStrip1.Text = "toolStrip1"
		'
		'tsbRemove
		'
		Me.tsbRemove.Enabled = False
		Me.tsbRemove.Image = Global.Neurotec.Samples.My.Resources.Resources.Delete
		Me.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.tsbRemove.Name = "tsbRemove"
		Me.tsbRemove.Size = New System.Drawing.Size(116, 22)
		Me.tsbRemove.Text = "Remove selected"
		'
		'SubjectTreeControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tableLayoutPanel)
		Me.Name = "SubjectTreeControl"
		Me.Size = New System.Drawing.Size(447, 439)
		Me.tableLayoutPanel.ResumeLayout(False)
		Me.tableLayoutPanel.PerformLayout()
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	#End Region

	Private WithEvents treeView As System.Windows.Forms.TreeView
	Private tableLayoutPanel As System.Windows.Forms.TableLayoutPanel
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private WithEvents tsbRemove As System.Windows.Forms.ToolStripButton

End Class
