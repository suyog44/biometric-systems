Imports Microsoft.VisualBasic
Imports System
Namespace Forms
	Partial Public Class EditInfoForm
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EditInfoForm))
			Me.dataGridView = New System.Windows.Forms.DataGridView
			Me.ColumnKey = New System.Windows.Forms.DataGridViewTextBoxColumn
			Me.btnOk = New System.Windows.Forms.Button
			Me.btnCancel = New System.Windows.Forms.Button
			Me.toolTip = New System.Windows.Forms.ToolTip(Me.components)
			Me.btnUp = New System.Windows.Forms.Button
			Me.btnDown = New System.Windows.Forms.Button
			Me.panel1 = New System.Windows.Forms.Panel
			Me.btnDelete = New System.Windows.Forms.Button
			Me.cbThumbnailField = New System.Windows.Forms.ComboBox
			Me.label1 = New System.Windows.Forms.Label
			CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panel1.SuspendLayout()
			Me.SuspendLayout()
			'
			'dataGridView
			'
			Me.dataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.dataGridView.BackgroundColor = System.Drawing.Color.White
			Me.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
			Me.dataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnKey})
			Me.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
			Me.dataGridView.Location = New System.Drawing.Point(35, 0)
			Me.dataGridView.MultiSelect = False
			Me.dataGridView.Name = "dataGridView"
			Me.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
			Me.dataGridView.Size = New System.Drawing.Size(560, 337)
			Me.dataGridView.TabIndex = 0
			'
			'ColumnKey
			'
			Me.ColumnKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
			Me.ColumnKey.HeaderText = "Key"
			Me.ColumnKey.Name = "ColumnKey"
			Me.ColumnKey.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
			'
			'btnOk
			'
			Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.btnOk.Location = New System.Drawing.Point(445, 382)
			Me.btnOk.Name = "btnOk"
			Me.btnOk.Size = New System.Drawing.Size(75, 23)
			Me.btnOk.TabIndex = 1
			Me.btnOk.Text = "&OK"
			Me.btnOk.UseVisualStyleBackColor = True
			'
			'btnCancel
			'
			Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.btnCancel.Location = New System.Drawing.Point(526, 382)
			Me.btnCancel.Name = "btnCancel"
			Me.btnCancel.Size = New System.Drawing.Size(75, 23)
			Me.btnCancel.TabIndex = 2
			Me.btnCancel.Text = "&Cancel"
			Me.btnCancel.UseVisualStyleBackColor = True
			'
			'btnUp
			'
			Me.btnUp.Image = Global.Neurotec.Samples.My.Resources.Resources.ArrowUp
			Me.btnUp.Location = New System.Drawing.Point(0, 0)
			Me.btnUp.Name = "btnUp"
			Me.btnUp.Size = New System.Drawing.Size(29, 30)
			Me.btnUp.TabIndex = 6
			Me.btnUp.UseVisualStyleBackColor = True
			'
			'btnDown
			'
			Me.btnDown.Image = Global.Neurotec.Samples.My.Resources.Resources.ArrowDown
			Me.btnDown.Location = New System.Drawing.Point(0, 36)
			Me.btnDown.Name = "btnDown"
			Me.btnDown.Size = New System.Drawing.Size(29, 30)
			Me.btnDown.TabIndex = 7
			Me.btnDown.UseVisualStyleBackColor = True
			'
			'panel1
			'
			Me.panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
						Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.panel1.Controls.Add(Me.btnDelete)
			Me.panel1.Controls.Add(Me.cbThumbnailField)
			Me.panel1.Controls.Add(Me.label1)
			Me.panel1.Controls.Add(Me.btnUp)
			Me.panel1.Controls.Add(Me.btnDown)
			Me.panel1.Controls.Add(Me.dataGridView)
			Me.panel1.Location = New System.Drawing.Point(3, 1)
			Me.panel1.Name = "panel1"
			Me.panel1.Size = New System.Drawing.Size(598, 375)
			Me.panel1.TabIndex = 8
			'
			'btnDelete
			'
			Me.btnDelete.Image = Global.Neurotec.Samples.My.Resources.Resources.Bad
			Me.btnDelete.Location = New System.Drawing.Point(0, 72)
			Me.btnDelete.Name = "btnDelete"
			Me.btnDelete.Size = New System.Drawing.Size(29, 30)
			Me.btnDelete.TabIndex = 10
			Me.btnDelete.UseVisualStyleBackColor = True
			'
			'cbThumbnailField
			'
			Me.cbThumbnailField.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
						Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
			Me.cbThumbnailField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
			Me.cbThumbnailField.FormattingEnabled = True
			Me.cbThumbnailField.Location = New System.Drawing.Point(137, 349)
			Me.cbThumbnailField.Name = "cbThumbnailField"
			Me.cbThumbnailField.Size = New System.Drawing.Size(458, 21)
			Me.cbThumbnailField.TabIndex = 9
			'
			'label1
			'
			Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(32, 352)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(99, 13)
			Me.label1.TabIndex = 8
			Me.label1.Text = "Subject image field:"
			'
			'EditInfoForm
			'
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(604, 408)
			Me.Controls.Add(Me.panel1)
			Me.Controls.Add(Me.btnCancel)
			Me.Controls.Add(Me.btnOk)
			Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
			Me.MinimumSize = New System.Drawing.Size(620, 400)
			Me.Name = "EditInfoForm"
			Me.Text = "Edit Info"
			CType(Me.dataGridView, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panel1.ResumeLayout(False)
			Me.panel1.PerformLayout()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents dataGridView As System.Windows.Forms.DataGridView
		Private WithEvents btnOk As System.Windows.Forms.Button
		Private btnCancel As System.Windows.Forms.Button
		Private toolTip As System.Windows.Forms.ToolTip
		Private WithEvents btnUp As System.Windows.Forms.Button
		Private WithEvents btnDown As System.Windows.Forms.Button
		Private panel1 As System.Windows.Forms.Panel
		Private cbThumbnailField As System.Windows.Forms.ComboBox
		Private label1 As System.Windows.Forms.Label
		Private ColumnKey As System.Windows.Forms.DataGridViewTextBoxColumn
		Private WithEvents btnDelete As System.Windows.Forms.Button
	End Class
End Namespace
