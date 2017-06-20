Imports Microsoft.VisualBasic
Imports System

Partial Public Class SchemaBuilderForm
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
		Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Sample Data", System.Windows.Forms.HorizontalAlignment.Left)
		Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Biographic Data", System.Windows.Forms.HorizontalAlignment.Left)
		Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Custom data", System.Windows.Forms.HorizontalAlignment.Left)
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SchemaBuilderForm))
		Me.btnOk = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.tableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
		Me.listView = New System.Windows.Forms.ListView
		Me.columnHeader1 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
		Me.columnHeader3 = New System.Windows.Forms.ColumnHeader
		Me.btnDelete = New System.Windows.Forms.Button
		Me.tableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
		Me.label1 = New System.Windows.Forms.Label
		Me.cbType = New System.Windows.Forms.ComboBox
		Me.label2 = New System.Windows.Forms.Label
		Me.tbName = New System.Windows.Forms.TextBox
		Me.label3 = New System.Windows.Forms.Label
		Me.tbDbColumn = New System.Windows.Forms.TextBox
		Me.btnAdd = New System.Windows.Forms.Button
		Me.dataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewComboBoxColumn
		Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
		Me.tableLayoutPanel1.SuspendLayout()
		Me.tableLayoutPanel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'btnOk
		'
		Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOk.Location = New System.Drawing.Point(479, 419)
		Me.btnOk.Name = "btnOk"
		Me.btnOk.Size = New System.Drawing.Size(75, 23)
		Me.btnOk.TabIndex = 0
		Me.btnOk.Text = "Ok"
		Me.btnOk.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Location = New System.Drawing.Point(560, 419)
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.Size = New System.Drawing.Size(75, 23)
		Me.btnCancel.TabIndex = 1
		Me.btnCancel.Text = "Cancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel1
		'
		Me.tableLayoutPanel1.ColumnCount = 4
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel1.Controls.Add(Me.btnCancel, 3, 3)
		Me.tableLayoutPanel1.Controls.Add(Me.listView, 1, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.btnDelete, 0, 1)
		Me.tableLayoutPanel1.Controls.Add(Me.tableLayoutPanel2, 0, 0)
		Me.tableLayoutPanel1.Controls.Add(Me.btnOk, 2, 3)
		Me.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
		Me.tableLayoutPanel1.Name = "tableLayoutPanel1"
		Me.tableLayoutPanel1.RowCount = 4
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
		Me.tableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
		Me.tableLayoutPanel1.Size = New System.Drawing.Size(638, 445)
		Me.tableLayoutPanel1.TabIndex = 2
		'
		'listView
		'
		Me.listView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
		Me.tableLayoutPanel1.SetColumnSpan(Me.listView, 3)
		Me.listView.Dock = System.Windows.Forms.DockStyle.Fill
		Me.listView.FullRowSelect = True
		ListViewGroup1.Header = "Sample Data"
		ListViewGroup1.Name = "lvgSampleData"
		ListViewGroup2.Header = "Biographic Data"
		ListViewGroup2.Name = "lvgBiographicData"
		ListViewGroup3.Header = "Custom data"
		ListViewGroup3.Name = "lvgCustomData"
		Me.listView.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
		Me.listView.Location = New System.Drawing.Point(39, 39)
		Me.listView.MultiSelect = False
		Me.listView.Name = "listView"
		Me.listView.Size = New System.Drawing.Size(596, 364)
		Me.listView.TabIndex = 3
		Me.listView.UseCompatibleStateImageBehavior = False
		Me.listView.View = System.Windows.Forms.View.Details
		'
		'columnHeader1
		'
		Me.columnHeader1.Text = "Type"
		Me.columnHeader1.Width = 155
		'
		'columnHeader2
		'
		Me.columnHeader2.Text = "Name"
		Me.columnHeader2.Width = 198
		'
		'columnHeader3
		'
		Me.columnHeader3.Text = "Db Column"
		Me.columnHeader3.Width = 206
		'
		'btnDelete
		'
		Me.btnDelete.AutoSize = True
		Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
		Me.btnDelete.Enabled = False
		Me.btnDelete.Image = Global.Neurotec.Samples.My.Resources.Resources.Delete
		Me.btnDelete.Location = New System.Drawing.Point(3, 39)
		Me.btnDelete.Name = "btnDelete"
		Me.btnDelete.Size = New System.Drawing.Size(30, 30)
		Me.btnDelete.TabIndex = 4
		Me.btnDelete.UseVisualStyleBackColor = True
		'
		'tableLayoutPanel2
		'
		Me.tableLayoutPanel2.ColumnCount = 7
		Me.tableLayoutPanel1.SetColumnSpan(Me.tableLayoutPanel2, 4)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
		Me.tableLayoutPanel2.Controls.Add(Me.label1, 0, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.cbType, 1, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.label2, 2, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.tbName, 3, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.label3, 4, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.tbDbColumn, 5, 0)
		Me.tableLayoutPanel2.Controls.Add(Me.btnAdd, 6, 0)
		Me.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
		Me.tableLayoutPanel2.Name = "tableLayoutPanel2"
		Me.tableLayoutPanel2.RowCount = 1
		Me.tableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tableLayoutPanel2.Size = New System.Drawing.Size(632, 30)
		Me.tableLayoutPanel2.TabIndex = 5
		'
		'label1
		'
		Me.label1.AutoSize = True
		Me.label1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label1.Location = New System.Drawing.Point(3, 0)
		Me.label1.Name = "label1"
		Me.label1.Size = New System.Drawing.Size(31, 30)
		Me.label1.TabIndex = 0
		Me.label1.Text = "Type"
		Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'cbType
		'
		Me.cbType.Dock = System.Windows.Forms.DockStyle.Fill
		Me.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cbType.FormattingEnabled = True
		Me.cbType.Location = New System.Drawing.Point(40, 3)
		Me.cbType.Name = "cbType"
		Me.cbType.Size = New System.Drawing.Size(187, 21)
		Me.cbType.TabIndex = 1
		Me.toolTip.SetToolTip(Me.cbType, resources.GetString("cbType.ToolTip"))
		'
		'label2
		'
		Me.label2.AutoSize = True
		Me.label2.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label2.Location = New System.Drawing.Point(233, 0)
		Me.label2.Name = "label2"
		Me.label2.Size = New System.Drawing.Size(38, 30)
		Me.label2.TabIndex = 2
		Me.label2.Text = "Name:"
		Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'tbName
		'
		Me.tbName.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbName.Location = New System.Drawing.Point(277, 3)
		Me.tbName.Name = "tbName"
		Me.tbName.Size = New System.Drawing.Size(100, 20)
		Me.tbName.TabIndex = 3
		Me.toolTip.SetToolTip(Me.tbName, "Element name")
		'
		'label3
		'
		Me.label3.AutoSize = True
		Me.label3.Dock = System.Windows.Forms.DockStyle.Fill
		Me.label3.Location = New System.Drawing.Point(383, 0)
		Me.label3.Name = "label3"
		Me.label3.Size = New System.Drawing.Size(59, 30)
		Me.label3.TabIndex = 4
		Me.label3.Text = "Db Column"
		Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'tbDbColumn
		'
		Me.tbDbColumn.Dock = System.Windows.Forms.DockStyle.Fill
		Me.tbDbColumn.Location = New System.Drawing.Point(448, 3)
		Me.tbDbColumn.Name = "tbDbColumn"
		Me.tbDbColumn.Size = New System.Drawing.Size(100, 20)
		Me.tbDbColumn.TabIndex = 5
		Me.toolTip.SetToolTip(Me.tbDbColumn, "If database column name does not match the name of element in application, it can" & _
				" be specified in Db Column (optional)")
		'
		'btnAdd
		'
		Me.btnAdd.Location = New System.Drawing.Point(554, 3)
		Me.btnAdd.Name = "btnAdd"
		Me.btnAdd.Size = New System.Drawing.Size(75, 23)
		Me.btnAdd.TabIndex = 6
		Me.btnAdd.Text = "Add"
		Me.btnAdd.UseVisualStyleBackColor = True
		'
		'dataGridViewComboBoxColumn1
		'
		Me.dataGridViewComboBoxColumn1.DataPropertyName = "Type"
		Me.dataGridViewComboBoxColumn1.HeaderText = "Type"
		Me.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1"
		'
		'toolTip
		'
		Me.toolTip.AutoPopDelay = 15000
		Me.toolTip.InitialDelay = 200
		Me.toolTip.ReshowDelay = 100
		'
		'SchemaBuilderForm
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(638, 445)
		Me.Controls.Add(Me.tableLayoutPanel1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.MinimizeBox = False
		Me.Name = "SchemaBuilderForm"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "Schema Builder"
		Me.tableLayoutPanel1.ResumeLayout(False)
		Me.tableLayoutPanel1.PerformLayout()
		Me.tableLayoutPanel2.ResumeLayout(False)
		Me.tableLayoutPanel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private WithEvents btnOk As System.Windows.Forms.Button
	Private btnCancel As System.Windows.Forms.Button
	Private tableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
	Private dataGridViewComboBoxColumn1 As System.Windows.Forms.DataGridViewComboBoxColumn
	Private WithEvents listView As System.Windows.Forms.ListView
	Private columnHeader1 As System.Windows.Forms.ColumnHeader
	Private columnHeader2 As System.Windows.Forms.ColumnHeader
	Private columnHeader3 As System.Windows.Forms.ColumnHeader
	Private WithEvents btnDelete As System.Windows.Forms.Button
	Private tableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
	Private label1 As System.Windows.Forms.Label
	Private cbType As System.Windows.Forms.ComboBox
	Private label2 As System.Windows.Forms.Label
	Private tbName As System.Windows.Forms.TextBox
	Private label3 As System.Windows.Forms.Label
	Private tbDbColumn As System.Windows.Forms.TextBox
	Private WithEvents btnAdd As System.Windows.Forms.Button
	Private toolTip As System.Windows.Forms.ToolTip
End Class
